
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WashingtonTrafficApi;

namespace TrafficApiStandaloneTester
{
    public class TestTrafficApi
    {
        private TestTrafficApiLog _log;

        public TestTrafficApi()
        {
            _log = new TestTrafficApiLog();
            _log.LogWrite("TrafficApi constructor called...");
        }

        /// <summary>
        /// Requests traffic events from the wsdot API
        /// </summary>
        /// <returns>A list of deserialized traffic events</returns>
        public List<TestAlert> GetTrafficAlerts()
        {
            _log.LogWrite("Getting traffic alerts from wsdot.wa.gov");

            var fileConfig = ConfigurationManager.AppSettings["APIUrl"];
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(fileConfig);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";

            WebResponse webResponse = webRequest.GetResponse();
            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            using (StreamReader responseReader = new StreamReader(webStream))
            {
                string response = responseReader.ReadToEnd();
                var alerts = JsonConvert.DeserializeObject<List<TestAlert>>(response);

                return alerts ?? new List<TestAlert>();
            }
        }
    }
}
