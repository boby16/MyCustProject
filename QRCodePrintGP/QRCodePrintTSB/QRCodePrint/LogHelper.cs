using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace QRCodePrint
{
    public class LogHelper
    {
        /// <summary>
        /// 日志部分
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLogs(string content)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(path))
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "logs";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                    if (File.Exists(path))
                    {
                        StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + content);
                        //  sw.WriteLine("----------------------------------------");
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        #region 写入系统日志
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="EventName">事件源名称</param>
        /// <param name="LogStr">日志内容</param>
        public static void WriteEventLog(string EventName, string LogStr)
        {
            try
            {
                if (!EventLog.SourceExists(EventName))
                {
                    EventLog.CreateEventSource(EventName, EventName);
                }
                EventLog.WriteEntry(EventName, LogStr);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="EventName">事件源名称</param>
        /// <param name="LogType">日志类型</param>
        /// <param name="LogStr">日志内容</param>
        public static void WriteEventLog(string EventName, string LogStr, EventLogEntryType LogType)
        {
            try
            {
                if (!EventLog.SourceExists(EventName))
                {
                    EventLog.CreateEventSource(EventName, EventName);
                }
                EventLog.WriteEntry(EventName, LogStr, LogType);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
