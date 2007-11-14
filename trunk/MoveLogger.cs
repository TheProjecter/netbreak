using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Netbreak
{
    class MoveLogger
    {
        private static StreamWriter logFile;
		private static string fileName = "ChainShotLog-" + (DateTime.Now.ToString().Replace("/","-").Replace(":",".")) + ".log";
        
        public MoveLogger()
        {
            logFile = new StreamWriter(fileName);
            logFile.WriteLine("Chainshot Game Log File");
            logFile.WriteLine("------------------------");
        }

        public void addLog(string logLine)
        {
            logFile.WriteLine(logLine);
        }

        public void close()
        {
            logFile.Close();
        }
    }
}