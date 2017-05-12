using System;

namespace _04.TriangleSurface_bySydeAndAltitude
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/04.%20Triangle%20surface%20by%20side%20and%20altitude/README.md
          //triangle surface formula: "S = ½.a.h"
            decimal side = decimal.Parse(Console.ReadLine());
            decimal altitude = decimal.Parse(Console.ReadLine());
            //calculation
            decimal result = (side * altitude) * 0.5m;
            //print
            Console.WriteLine("{0:F2}", result);
        }
    }
}
