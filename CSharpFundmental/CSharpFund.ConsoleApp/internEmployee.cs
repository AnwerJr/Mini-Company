using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    internal class InternEmployee : Employee
    {

        public override decimal GetSalary()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 3000;
        }
    }
}
