using System;
using System.Threading;
using System.Globalization;
using System.IO;

namespace _09.ArithmeticalЕxpressions
{
    class MainProgram
    {
        static void Main(string[] arg)
        {//condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/09.%20Arithmetical%20expressions/README.md
            
            //simulate -> DELETE after complete!
            StringReader reader = new StringReader("(3 + 5.3) * 2.7 - ln(22)/pow(2.2,-1.7)");
            Console.SetIn(reader);
                                                  
            
            //input
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; //console settings
            string input = Console.ReadLine();

            //convert infix to Reverse Polish notation
            string outputToRPN = RPNconverter.Convert(input);

            //test output
            Console.WriteLine(string.Join(" ", outputToRPN));
            
        }
    }
}
