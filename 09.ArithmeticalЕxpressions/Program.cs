using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
//using System.Linq;
using System.Text;
using System.Threading;
using _09.ArithmeticalЕxpressions.Objects;

namespace _09.ArithmeticalЕxpressions
{
    class Program
    {
        static Stack<string> operatorsStack = new Stack<string>(); //hold not yet used operators
        static Queue<string> outputQueue = new Queue<string>();
        static string[] arithmeticOperator = { "-", "+", "/", "*" };  //sorted by their Priority
        static string[] parenthesesAndComma = { "(", ",", ")" };
        static string[] mathematicalFunctions = { "ln", "sqrt", "pow" };
        static string input = string.Empty;

        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/09.%20Arithmetical%20expressions/README.md
            //simulate -> DELETE after complete!
            StringReader reader = new StringReader("2+(-3)");
            Console.SetIn(reader);                //(3+5.3)*2.7-ln(22)/pow(2.2,-1.7)

            //input
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; //console settings
            input = Console.ReadLine().Replace(" ", "").ToLower(); //clean white-space from user

            //converter
            InfixToReversePolishNotation(input);

            //test output
            Console.WriteLine(string.Join("", outputQueue));
        }

        private static void InfixToReversePolishNotation(string input) //based on "Shunting-yard algorithm"
        {
            StringBuilder tempToken = new StringBuilder();
            string currentTokenType = null;
            string previousTokenType = TokenType.NoneToken;

            input = input + " "; //this white-space is needed to check correctly if last element is digit token

            //calculation
            for (int i = 0; i < input.Length; i++) //read token by token
            {
                //1.token is digit
                if (char.IsDigit(input[i]) == true || (input[i] == '.' && previousTokenType == TokenType.Digit)) //(input[i] == '.' && previousTokenType == "digit") <- this will no parse wrong input like comma: 2,2 //Най-добре да направя ексепшън и подавайки му 2,2 то да каже, че е прешно въведен формат
                {
                    if (previousTokenType != TokenType.Digit) previousTokenType = TokenType.Digit;
                    tempToken.Append(input[i]);
                }
                else if (previousTokenType == TokenType.Digit)
                {
                    outputQueue.Enqueue(tempToken.ToString());
                    tempToken.Clear();
                }
                //2.token is arithmeticOperator
                if (Array.IndexOf(arithmeticOperator, input[i].ToString()) > -1)
                {
                    currentTokenType = TokenType.ArithmeticOperator;

                    AddTokenToStackOrQueue(currentTokenType, input[i].ToString(), previousTokenType);
                    if (previousTokenType != currentTokenType) previousTokenType = currentTokenType;
                    tempToken.Clear();
                }
                //3.token is parenthesesAndComma
                else if (Array.IndexOf(parenthesesAndComma, input[i].ToString()) > -1)
                {
                    currentTokenType = TokenType.ParenthesesAndComma;

                    AddTokenToStackOrQueue(currentTokenType, input[i].ToString(), previousTokenType);
                    if (previousTokenType != currentTokenType) previousTokenType = currentTokenType;
                    tempToken.Clear();
                }
                //4.all above false, so current token IS, or it's a PART of mathematicalFunctions
                //4a.token is a part of mathematicalFunctions
                else if (previousTokenType != TokenType.Digit)
                {
                    tempToken.Append(input[i]);
                }
                //4b.token is mathematicalFunctions
                if (Array.IndexOf(mathematicalFunctions, tempToken.ToString()) > -1)
                {
                    currentTokenType = TokenType.MathematicalFunctions;

                    AddTokenToStackOrQueue(currentTokenType, tempToken.ToString(), previousTokenType);
                    if (previousTokenType != currentTokenType) previousTokenType = currentTokenType;
                    tempToken.Clear();
                }
            }

            //roll back stack to queue
            for (int i = operatorsStack.Count - 1; i >= 0; i--)
            {
                ErrorIfFoundParentheses(operatorsStack.Peek());
                outputQueue.Enqueue(operatorsStack.Pop());
            }
        }

        private static void AddTokenToStackOrQueue(string currentTokenType, string tempToken, string previousTokenType)
        {
            if (currentTokenType == TokenType.ArithmeticOperator)
            {
                while (true)
                {
                    //get ArithmeticOperators priority 
                    sbyte currentOperatorPriorityIndex = (sbyte)Array.IndexOf(arithmeticOperator, tempToken);

                    sbyte lastStackOperatorPriorityIndex = -1;
                    if (operatorsStack.Count > 0)
                    {
                        lastStackOperatorPriorityIndex = (sbyte)Array.IndexOf(arithmeticOperator, operatorsStack.Peek());
                    }

                    //check ArithmeticOperators priority and Push them to operatorsStack or to outputQueue
                    if (currentOperatorPriorityIndex > lastStackOperatorPriorityIndex)  //if "lastStackOperatorPriorityIndex" is still "-1", at the last position in stack, there is NO ArithmeticOperator
                    {
                        operatorsStack.Push(tempToken);
                        break;
                    }
                    else if (currentOperatorPriorityIndex < lastStackOperatorPriorityIndex)
                    {
                        outputQueue.Enqueue(operatorsStack.Pop());
                    }
                }

            }
            else if (currentTokenType == TokenType.ParenthesesAndComma)
            {
                switch (tempToken)
                {
                    case "(": operatorsStack.Push(tempToken); break;

                    case ",":
                        PopOperatorsFromTheStack(); //Until the top of the stack is a left parenthesis, pop operators from stack and add them to queue.
                        break;

                    case ")":
                        PopOperatorsFromTheStack(); //Until the top of the stack is a left parenthesis, pop operators from stack and add them to queue.
                        operatorsStack.Pop(); //Last token in stack need to be left parenthesis (see above operations), so pop this token.
                        if (operatorsStack.Count > 0 && (Array.IndexOf(mathematicalFunctions, operatorsStack.Peek())) > -1) //If the top of the stack is a function, pop it onto the queue.
                        {
                            outputQueue.Enqueue(operatorsStack.Pop());
                        }
                        break;
                }
            }
            else if (currentTokenType == TokenType.MathematicalFunctions)
            {
                operatorsStack.Push(tempToken);
            }
        }

        private static void PopOperatorsFromTheStack()
        {
            LeftParenthesesCheckerExeption();

            while (true)
            {
                if (operatorsStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorsStack.Pop());
                }
                else if (operatorsStack.Peek() == "(")
                {
                    break;
                }
                LeftParenthesesCheckerExeption();
            }
        }

        //EXEPTIONS
        private static void LeftParenthesesCheckerExeption()
        {
            if (operatorsStack.Count == 0) //If left parentheses is not reached -> error 
            {
                throw new ArgumentException("Invalid Expression, no left parentheses!");
            }
        }

        private static void ErrorIfFoundParentheses(string lastStackToken)
        {
            if (lastStackToken == "(")
            {
                throw new AggregateException("Invalid Expression! There is one more Parenthesis!");
            }
        }
    }
}
