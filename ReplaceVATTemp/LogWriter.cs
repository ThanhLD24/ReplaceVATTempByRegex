using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceVATTemp
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        public LogWriter()
        {
        }
        public void LogWrite(int id, string oldCss, string newCss)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(id, oldCss, newCss, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(int id, string oldCss, string newCss, TextWriter txtWriter)
        {
            try
            {
                //txtWriter.Write("\r\nLog Entry : ");
                //txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                //    DateTime.Now.ToLongDateString());
                //txtWriter.WriteLine("  :");
                //txtWriter.WriteLine("  :{0}", logMessage);
                //txtWriter.WriteLine("-------------------------------");
                txtWriter.Write("\r\nLog Entry ID : {0}", id);
                txtWriter.WriteLine("\r\n+++++++++++++++++++++ Old Css +++++++++++++++++++++");
                txtWriter.WriteLine(oldCss);
                txtWriter.WriteLine("\r\n+++++++++++++++++++++ NewCss +++++++++++++++++++++ ");
                txtWriter.WriteLine(newCss);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }

        public void LogTableName(string tableName)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    LogTableName(tableName, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void LogTableName(string tableName, TextWriter txtWriter)
        {
            try
            {
                //txtWriter.Write("\r\nLog Entry : ");
                //txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                //    DateTime.Now.ToLongDateString());
                //txtWriter.WriteLine("  :");
                //txtWriter.WriteLine("  :{0}", logMessage);
                //txtWriter.WriteLine("-------------------------------");
                txtWriter.WriteLine("========================================================{0}========================================================", tableName);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
