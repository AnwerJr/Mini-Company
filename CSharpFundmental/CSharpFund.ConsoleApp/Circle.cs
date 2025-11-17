using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    internal class Circle : Shape
    {
        public Circle()
        {
           Name = "Circle 23";
        }
        public double Radius { get; set; }
        public override double CalceArea()
        {
            return Math.PI * Radius * Radius;
        }

        public void PrintArea()
        {
            base.PrintArea();
            Console.WriteLine($"Circle Area: {CalceArea()} And The Type is {Name}");
        }

        public override void Test()
        {
            throw new NotImplementedException();
        }
    }
}
