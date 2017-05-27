using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace _09.ArithmeticalЕxpressions
{
    class MainProgram
    {
        static void Main(string[] arg)
        {//condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/09.%20Arithmetical%20expressions/README.md

            //console settings
            Console.Title = "Arithmetical Expression Calculator V0.1";
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //input
            string input = Console.ReadLine();

            //convert infix to Reverse Polish notation
            Queue<string> expressionAsRPN = RPNconverter.Converting(input);

            //Console.WriteLine(string.Join(" ", expressionAsRPN));  //<--uncomment to print RPN on console

            //calculate
            string result = RPNcalculator.Calculations(expressionAsRPN);

            //print
            Console.WriteLine(result);
        }
    }
}
//Arithmetical Expressions Calculator V0.1
//based on "Reverse Polish Notation" and "Shunting-yard algorithm"