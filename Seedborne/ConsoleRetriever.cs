using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne
{
    public class ConsoleRetriever
    {
        public virtual string GetResponse()
        {
            return Console.ReadLine();
        }
    }
}
