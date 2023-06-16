using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WashingtonTrafficApi
{
    public class Alert
    {
        public int AlertID { get; set; }
        public string HeadlineDescription { get; set; }
        public string EventCategory { get; set; }
        public string Priority { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
