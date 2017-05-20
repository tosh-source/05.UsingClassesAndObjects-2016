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
            Console.SetIn(reader);                //(3+5.3)*2.7-ln(22)/pow(2.2,-1.7)
                                                  //(-2-5)-1+-7  за прихващане на отрицателни числа
            
            //input
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; //console settings
            string input = Console.ReadLine();

            //converter
            string outputToRPN = RPNconverter.Convert(input);

            //test output
            Console.WriteLine(string.Join(" ", outputToRPN));
            
        }
    }
}
