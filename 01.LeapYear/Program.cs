using System;

namespace _01.LeapYear
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/01.%20Leap%20year/README.md
            int input = int.Parse(Console.ReadLine());
            bool isLeapOrNot = DateTime.IsLeapYear(input);
            Console.WriteLine(isLeapOrNot? "Leap" : "Common");
        }
    }
}
