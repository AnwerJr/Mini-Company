using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp.Interface
{
    internal class Computer : IDevice , IRestartable

    {
        public void TurnOn()
        {
            Console.WriteLine("Computer Is Turned On...");
        }

        public void TurnOff()
        {
            Console.WriteLine("Computer Is Turned OFF...");
        }
        

        public void Test()
        {
            Console.WriteLine("Computer Is Restarting21111113...");
        }
    }
}
