using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp.Interface
{
    internal class MobilePhone : IDevice , IRestartable
    {
        public void TurnOn()
        {
            Console.WriteLine("Mobile Phone Calling...");
        }

        public void TurnOff()
        {
            Console.WriteLine("Mobile Phone End Call...");
        }
       
        public void Test()
        {
            Console.WriteLine("Computer Is Restarting1234...");
        }
    }
}
