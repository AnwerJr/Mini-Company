using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    public static class MyStatic_Class
    {
        static MyStatic_Class()
        {
           Console.WriteLine("Hello from MyStatic_Class static constructor!");
        }
        public static void DoSomething()
        {
           // Console.WriteLine("Hello from MyStatic_Class!");
        }
    }
}
