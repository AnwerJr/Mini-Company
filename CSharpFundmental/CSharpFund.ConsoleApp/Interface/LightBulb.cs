using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp.Interface
{
    internal class LightBulb : IDevice

    {
        public void TurnOn()
        {
            Console.WriteLine("Bulb Is Turned On...");
        }

        public void TurnOff()
        {
            Console.WriteLine("Bulb Is Turned OFF...");
        }
     
        

        public void Test()
        {
            Console.WriteLine("Computer Is Restarting12222222...");
        }
    }
}
