using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp.Interface
{
    internal interface IRestartable
    {
        public void Restart()
        {
            Console.WriteLine("Device is restarting mmm mmm...");
        }
    }
}
