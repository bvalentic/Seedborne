using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seedborne;

namespace HackWeekTests
{
    class TestMethods
    {
        public class TestPositiveResponseRetriever : ConsoleRetriever
        {
            public override string GetResponse()
            {
                return "yes";
            }
        }

        public class TestNegativeResponseRetriever : ConsoleRetriever
        {
            public override string GetResponse()
            {
                return "no";
            }
        }
    }
}
