
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
    public class TrafficApi
    {
        private TrafficApiLog _log;

        public TrafficApi()
        {
            _log = new TrafficApiLog();
            _log.LogWrite("TrafficApi constructor called...");
        }

        /// <summary>
        /// Requests traffic events from the wsdot API
        /// </summary>
        /// <returns>A list of deserialized traffic events</returns>
        public List<Alert> GetTrafficAlerts()
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
                var alerts = JsonConvert.DeserializeObject<List<Alert>>(response);

                return alerts ?? new List<Alert>();
            }
        }
    }
}
