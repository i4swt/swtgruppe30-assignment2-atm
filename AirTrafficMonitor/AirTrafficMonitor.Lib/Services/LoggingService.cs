using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly string filePath = "Log.txt";

        //Logs the specified string to a textfile
        public void Log(string entry)
        {
            if(entry==null) throw new ArgumentNullException();

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(entry);
            }
        }
    }
}