using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _09.ArithmeticalЕxpressions
{
    class RPNconverter  //convert Infix to Reverse Polish Notation
    {
        private static Stack<string> operatorsStack = new Stack<string>(); //hold not yet used operators
        private static Queue<string> outputQueue = new Queue<string>();
        private static string[] arithmeticOperator = { "-", "+", "/", "*" };
        private static char[][] arithmeticOperatorPrecedence = {            //operator by their Priority
            new char[] {'-', '+'},                                          //precedence 1
            new char[] {'/', '*'}                                           //precedence 2
        };
        private static string[] parenthesesAndComma = { "(", ",", ")" };
        private static string[] mathematicalFunctions = { "ln", "sqrt", "pow" };

        public static string Convert(string infixNotation)
        {
            string output = string.Empty;

            infixNotation = infixNotation.Replace(" ", string.Empty).ToLower(); //clean white-space from user
            output = ConvertToRPN(infixNotation);

            return output;
        }

        private static string ConvertToRPN(string input) //based on "Shunting-yard algorithm"
        {
            StringBuilder tempToken = new StringBuilder();
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
                    if (input[i].ToString() == "-")
                    {
                        LookingForNegativeDigit(tempToken, input, i, previousTokenType);
                    }
                    else
                    {
                        ArithmeticOperatorProcessing(input[i]);
                    }
                    if (previousTokenType != TokenType.ArithmeticOperator) previousTokenType = TokenType.ArithmeticOperator;
                }
                //3.token is parenthesesAndComma
                else if (Array.IndexOf(parenthesesAndComma, input[i].ToString()) > -1)
                {
                    ParenthesesAndCommaProcessing(input[i].ToString());
                    if (previousTokenType != TokenType.ParenthesesAndComma) previousTokenType = TokenType.ParenthesesAndComma;
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
                    operatorsStack.Push(tempToken.ToString());
                    tempToken.Clear();

                    if (previousTokenType != TokenType.MathematicalFunctions) previousTokenType = TokenType.MathematicalFunctions;
                }
            }

            //roll back stack to queue
            for (int i = operatorsStack.Count - 1; i >= 0; i--)
            {
                if (operatorsStack.Peek() == "(") //Error if found parentheses
                {
                    throw new AggregateException("Invalid Expression! There is one more Parenthesis!");
                }

                outputQueue.Enqueue(operatorsStack.Pop());
            }

            return string.Join(" ", outputQueue);
        }

        private static void LookingForNegativeDigit(StringBuilder tempToken, string input, int i, string previousTokenType)
        {
            if (previousTokenType != TokenType.NoneToken && (input[i - 1].ToString() == "," || input[i - 1].ToString() == "(") && char.IsDigit(input[i + 1])) //1.when we have negative operator (for negative digit) IN function like: "pow(2,-1.5)"
            {                                                                                                                                                 //2.when we have negative operator (for negative digit) SURROUND with parentheses, like: "2 + (-4)" or "pow(-2,6)"
                tempToken.Append(input[i]);
            }
            else if ((previousTokenType == TokenType.NoneToken || previousTokenType == TokenType.ArithmeticOperator) && char.IsDigit(input[i + 1])) //1.when we have negative operator (for negative digit) at the BEGINNING, like: -6 + 4
            {                                                                                                                                       //2.when we have negative operator (for negative digit) after ANOTHER arithmetic operator, like: 10 + -2
                tempToken.Append(input[i]);
            }
            else
            {
                ArithmeticOperatorProcessing(input[i]);
            }
        }

        private static void ArithmeticOperatorProcessing(char tempToken)
        {
            while (true)
            {
                //get ArithmeticOperators priority(Precedence)
                sbyte currentOperatorPrecedence = ArithmeticOperatorPrecedenceProcessing(tempToken);

                sbyte lastStackOperatorPrecedence = 0;
                if (operatorsStack.Count > 0)
                {
                    lastStackOperatorPrecedence = ArithmeticOperatorPrecedenceProcessing(operatorsStack.Peek()[0]);  //когато оптимизирам програмата и махна стринговете, това "[0]" също трябва да изчезне!!!
                }

                //check ArithmeticOperators priority and Push them to operatorsStack or to outputQueue
                if (currentOperatorPrecedence > lastStackOperatorPrecedence)  //if "lastStackOperatorPrecedence" is still "0", at the last position in the stack, has NO ArithmeticOperator
                {
                    operatorsStack.Push(tempToken.ToString());
                    break;
                }
                else if (currentOperatorPrecedence <= lastStackOperatorPrecedence)
                {
                    outputQueue.Enqueue(operatorsStack.Pop());
                }
            }
        }

        private static sbyte ArithmeticOperatorPrecedenceProcessing(char tempToken)
        {
            if (arithmeticOperatorPrecedence[0].Contains(tempToken))        //Operators with Precedence 1
            {
                return 1;
            }
            else if (arithmeticOperatorPrecedence[1].Contains(tempToken))    //Operators with Precedence 2
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        private static void ParenthesesAndCommaProcessing(string tempToken)
        {
            switch (tempToken.ToString())
            {
                case "(": operatorsStack.Push(tempToken.ToString()); break;

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

        private static void LeftParenthesesCheckerExeption()
        {
            if (operatorsStack.Count == 0) //If left parentheses is not reached -> error 
            {
                throw new ArgumentException("Invalid Expression, no left parentheses!");
            }
        }
    }
}
