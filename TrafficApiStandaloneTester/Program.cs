using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficApiStandaloneTester
{
    internal class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TestPoller poller = new TestPoller();
            poller.StartPoller();
            Console.Read();
        }
    }
}
