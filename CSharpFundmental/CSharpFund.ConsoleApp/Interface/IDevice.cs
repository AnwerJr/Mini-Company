using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp.Interface
{
    internal interface IDevice 
    {

        /* كلاس خاص بالكمبيوتر والموبايل فون بس interface 
         * متشابه زي ال Abstract class
         */

        void TurnOn();
        void TurnOff();
        void Test();
        

    }
}
