using ConsoleApp;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program2
    {
        static void Main(string[] args)
        {
            var message1 = new Message("tcase0174@gmail.com", "tcase0174@gmail.com", "MANGO1", "DAY1", DateTime.Now);
            var message2 = new Message("tcase0174@gmail.com", "tcase0174@gmail.com", "MANGO2", "DAY2", DateTime.Now);
            var message3 = new Message("tcase0174@gmail.com", "tcase0174@gmail.com", "MANGO3", "DAY3", DateTime.Now);
            var message4 = new Message("tcase0174@gmail.com", "tcase0174@gmail.com", "MANGO4", "DAY4", DateTime.Now);
            var message5 = new Message("tcase0174@gmail.com", "tcase0174@gmail.com", "MANGO5", "DAY5", DateTime.Now);
            var message6 = new Message("tcase0174@gmail.com", "anyemail@gamil.com", "MANGO6", "DAY6", DateTime.Now);

            var messages = new List<Message> { message1, message2, message3, message4, message5, message6 };            

            EmailClient.SendMessage(message1); // overload1: send a single message
            EmailClient.SendMessage(messages); // overlaod2: send a list of messages

            Console.ReadLine();
        }
    }
}
