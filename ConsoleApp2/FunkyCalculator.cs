using System.Threading.Tasks;
using System.Threading;
using System;

namespace ConsoleApp
{
    public class FunkyCalculator
    {
        public static void AddRandomNumbers()
        {
            int sum = 0;
            var rand = new Random();

            for (var i = 0; i < 1000; i++)
            {
                sum += rand.Next(1, 100);
                Console.WriteLine("Current sum is: " + sum);
            }

            Console.WriteLine("Final sum is: " + sum);
        }
    }
}
