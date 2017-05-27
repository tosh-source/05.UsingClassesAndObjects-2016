using System;
using System.Collections.Generic;
using System.Text;

namespace _09.ArithmeticalЕxpressions
{
    class RPNcalculator
    {
        public static string Calculations(Queue<string> RPN) //based on "Reverse Polish Notation"
        {
            Stack<decimal> values = new Stack<decimal>();
            decimal firstDigit = 0;
            decimal secondDigit = 0;
            decimal testDigit = 0;
            StringBuilder testString = new StringBuilder();

            while (RPN.Count > 0)
            {
                if (decimal.TryParse(RPN.Peek(), out testDigit))  //token is value
                {
                    values.Push(decimal.Parse(RPN.Dequeue()));
                }
                else                                              //token is operator (or function)
                {
                    testString.Append(RPN.Peek());  //StringBuilder is used to safe invoking of a .ToString() method in every if-else statement

                    if (testString[0] == '-')
                    {
                        RPN.Dequeue();

                        secondDigit = CheckedCountOfnArguments(values);
                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push(firstDigit - secondDigit);
                    }
                    else if (testString[0] == '+')
                    {
                        RPN.Dequeue();

                        secondDigit = CheckedCountOfnArguments(values);
                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push(firstDigit + secondDigit);
                    }
                    else if (testString[0] == '/')
                    {
                        RPN.Dequeue();

                        secondDigit = CheckedCountOfnArguments(values);
                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push(firstDigit / secondDigit);
                    }
                    else if (testString[0] == '*')
                    {
                        RPN.Dequeue();

                        secondDigit = CheckedCountOfnArguments(values);
                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push(firstDigit * secondDigit);
                    }
                    else if (testString[0] == 'l') //l ==> ln(x) (logarithm)
                    {
                        RPN.Dequeue();

                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push((decimal)Math.Log((double)firstDigit));
                    }
                    else if (testString[0] == 's') //s ==> sqrt(x) (square root)
                    {
                        RPN.Dequeue();

                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push((decimal)Math.Sqrt((double)firstDigit));
                    }
                    else if (testString[0] == 'p') //p ==> pow(x,y) (Power)
                    {
                        RPN.Dequeue();

                        secondDigit = CheckedCountOfnArguments(values);
                        firstDigit = CheckedCountOfnArguments(values);
                        values.Push((decimal)Math.Pow((double)firstDigit, (double)secondDigit));
                    }

                    testString.Clear();
                }
            }

            //return value
            if (values.Count == 1)
            {
                return values.Pop().ToString();
            }
            else
            {
                throw new ArgumentException("Invalid expression, there is too many tokens!");
            }
        }

        private static decimal CheckedCountOfnArguments(Stack<decimal> values)
        {
            if (values.Count == 0)
            {
                throw new ArgumentException("Invalid expression, there is no enough tokens!");
            }

            return values.Pop();
        }
    }
}

//Notice: for Decimal vs Double visit -> https://stackoverflow.com/questions/329613/decimal-vs-double-speed
