using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program1
    {
        static void Main(string[] args)
        {
            // Running to check failed emails every 1 second in the background

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            var timer = new Timer((e) =>
            {
                EmailClient.ResendFailedMails();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        }
    }
}
