using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficApiStandaloneTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Poller poller = new Poller();
            poller.StartPoller();
            Console.Read();
        }
    }
}
