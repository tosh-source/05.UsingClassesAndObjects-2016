using System;

namespace _06.TriangleSurface_byTwoSidesAndAngle
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/06.%20Triangle%20surface%20by%20two%20sides%20and%20an%20angle/README.md
          //formula: S = ½.(A.B.Sin(AB-angle)) 
            double a = double.Parse(Console.ReadLine());
            double b = double.Parse(Console.ReadLine());
            double ABangle = double.Parse(Console.ReadLine());
            //calculation
            double ABangleInRadians = (ABangle * Math.PI) / 180; //Math.Sin() use radians, NOT degrees!
            double S = (a * b * Math.Sin(ABangleInRadians)) * 0.5;
            //print
            Console.WriteLine("{0:F2}", S);
        }
    }
}
