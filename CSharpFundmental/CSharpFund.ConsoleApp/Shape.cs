using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    internal abstract class Shape
    {
        protected string Name { get; set; }   
        public abstract double CalceArea();
       
        public virtual void PrintArea()
        {
            Console.WriteLine($"the area ={CalceArea()}");
        }
        public abstract void Test();
       
    }
}
