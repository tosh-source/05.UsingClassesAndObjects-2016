using System;

namespace _05.TriangleSurface_byThreeSides
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/05.%20Triangle%20surface%20by%20three%20sides/README.md
          // formula: https://bg.wikipedia.org/wiki/%D0%A5%D0%B5%D1%80%D0%BE%D0%BD%D0%BE%D0%B2%D0%B0_%D1%84%D0%BE%D1%80%D0%BC%D1%83%D0%BB%D0%B0
            double a = double.Parse(Console.ReadLine());
            double b = double.Parse(Console.ReadLine());
            double c = double.Parse(Console.ReadLine());
            //calculation
            double p = (a + b + c) * 0.5;
            double S = Math.Sqrt(p * ((p - a) * (p - b) * (p - c)));
            //print
            Console.WriteLine("{0:F2}", S);
        }
    }
}
