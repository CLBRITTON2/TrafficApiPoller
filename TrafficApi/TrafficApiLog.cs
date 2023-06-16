using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrafficApi
{
    /// <summary>
    /// To enable logging create a log.txt file in the executing assembly
    /// </summary>
    public static class TrafficApiLog
    {
        private static string _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _logPath = $"{_exePath}\\log.txt";
        private static bool _logExists = File.Exists(_logPath);

        public static void LogWrite(string logMessage)
        {
            if (!_logExists)
            {
                return;
            }

            try
            {
                using (StreamWriter w = File.AppendText(_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
