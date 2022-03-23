using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESProjekat.Komponente
{
    public class Logger
    {
        private static string path = @"Logger.txt";
        private static readonly object padlock = new object();
        private static Logger instance = null;
        private static StreamWriter sw = null;
        static Logger()
        {
            sw = File.AppendText(path);

        }
        ~Logger()
        {
            sw.Close();
        }
        public static Logger Instanca()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }
        public void UpisLogger(string componentId, string text)//luka
        {
            if (componentId == null || text == null)
            {
                throw new Exception("Nema promena.");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Nova promena:" + "\r\n");
            sb.Append(string.Format("Vreme promene: {0}", DateTime.Now) + "\r\n");
            sb.Append(string.Format("Komponenta: {0}", componentId) + "\r\n");
            sb.Append(string.Format("Poruka: {0}\r\r\n", text) + "\r\n");
            sw.Write(sb);
            Trace.Write(sb);
        }
    }
}
