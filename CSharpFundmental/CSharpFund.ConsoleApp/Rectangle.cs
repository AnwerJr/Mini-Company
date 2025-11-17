using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    internal class Rectangle : Shape
    {
        public int Length { get; internal set; }
        public int Width { get; internal set; }
        double Lenght { get; set; }
        double width { get; set; }
        public override double CalceArea()
        {
            return Lenght * Width;
        }
        public override void Test()
        {
            Console.WriteLine("Test Rectangle");
        }
        override public void PrintArea()
        {
            Console.WriteLine($"Area of Rectangle with length {Lenght} And with the width {Width} = {CalceArea}");
        }
    }
}
