using System;
using System.Linq;

namespace _08.SumIntegers
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Topics/05.%20Using-Classes-and-Objects/homework/08.%20Sum%20integers/README.md
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            Console.WriteLine(input.Sum());
        }
    }
}
