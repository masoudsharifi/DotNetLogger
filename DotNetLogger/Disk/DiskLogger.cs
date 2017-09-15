﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//...Internal References
using DotNetLogger.Models;
using DotNetLogger.Interfaces;
//...3rd Party References
using Newtonsoft.Json;

namespace DotNetLogger.Disk
{
    /// <summary>
    /// This class will write logs to disk
    /// </summary>
    public class DiskLogger : ILogger
    {
        #region Public Methods.
        /// <summary>
        /// Logs an error message to the disk
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogError(string error, string logSignature = "", [CallerMemberName] string caller = "")
        {
            var log = new Log
            {
                Type = "Error",
                CreatedOn = DateTime.UtcNow,
                Message = error,
                Origin = caller,
                Signature = logSignature
            };
            this.WriteToDisk(log);
        }
        /// <summary>
        /// Logs a exception to the disk
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogException(Exception ex, string logSignature = "", [CallerMemberName] string caller = "")
        {
            var log = new Log
            {
                Type = "Exception",
                CreatedOn = DateTime.UtcNow,
                Message = ex.ToString(),
                Origin = caller,
                Signature = logSignature
            };
            this.WriteToDisk(log);
        }
        /// <summary>
        /// Logs a information message to the disk
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogInfo(string info, string logSignature = "", [CallerMemberName] string caller = "")
        {
            var log = new Log
            {
                Type = "Information",
                CreatedOn = DateTime.UtcNow,
                Message = info,
                Origin = caller,
                Signature = logSignature
            };
            this.WriteToDisk(log);
        }
        /// <summary>
        /// Logs a warning message to the disk
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogWarning(string warning, string logSignature = "", [CallerMemberName] string caller = "")
        {
            var log = new Log
            {
                Type = "Warning",
                CreatedOn = DateTime.UtcNow,
                Message = warning,
                Origin = caller,
                Signature = logSignature
            };
            this.WriteToDisk(log);
        }

        /// <summary>
        /// Find logs that contain the partialSearchString
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="partialSearchString"></param>
        /// <returns></returns>
        public IList<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearchString)
        {
            long startNumber = -1;
            string fstr = fromDate.ToString("yyyyMMdd");
            long.TryParse(fstr, out startNumber);

            long toNumber = -1;
            string tstr = toDate.ToString("yyyyMMdd");
            long.TryParse(tstr, out toNumber);
            string path = AppDomain.CurrentDomain.BaseDirectory + $"DotNetLogger\\";

            IList<Log> logs = new List<Log>();

            for(long i = startNumber; i <= toNumber;i++)
            {
                var filePath = path + i.ToString();
                if (File.Exists(filePath))
                {
                    var fileLogs = this.ReadLogFile(filePath);
                    var foundLogs = fileLogs.FindAll(l => l.Message.Contains(partialSearchString));
                    if(foundLogs != null)
                    {
                        for(int j = 0;j < foundLogs.Count; j++)
                        {
                            logs.Add(foundLogs[j]);
                        }
                    }
                }
            }

            return logs;
        }
        #endregion

        #region Private Methods
        private void WriteToDisk(Log log)
        {
            var dt = DateTime.Now;
            var filename = dt.ToString("yyyyMMdd");
            var serializedLog = JsonConvert.SerializeObject(log);
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + $"DotNetLogger\\{filename}", true))
            {                
                writer.WriteLineAsync(serializedLog);
            }
        }

        private List<Log> ReadLogFile(string filePath)
        {
            string content = String.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                content = $"[{reader.ReadToEnd()}]";
                reader.Close();
            }

            List<Log> logs = JsonConvert.DeserializeObject<List<Log>>(content);
            return logs;
        }
        #endregion
    }
}