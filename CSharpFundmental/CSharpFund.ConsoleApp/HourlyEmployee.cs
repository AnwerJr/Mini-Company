using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    internal class HourlyEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public int TotalHoursWorked { get; set; }
        public override decimal GetSalary()
        {   
            Console.ForegroundColor = ConsoleColor.Yellow;
            return HourlyRate * TotalHoursWorked;   
        }
    }
}
