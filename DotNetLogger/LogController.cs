using DotNetLogger.Interfaces;
using DotNetLogger.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DotNetLogger
{
    public class LogController
    {
        private ILogger _Logger = null;
        public LogController(ILogger logger)
        {
            this._Logger = logger;
        }

        /// <summary>
        /// This will log an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogException(Exception ex, string logSignature = "", [CallerMemberName] string caller = "")
        {
            this._Logger.LogException(ex, logSignature, caller);
        }
        /// <summary>
        /// This will log an error
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogError(string error, string logSignature = "", [CallerMemberName] string caller = "")
        {
            this._Logger.LogError(error, logSignature, caller);
        }
        /// <summary>
        /// This will log information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogInfo(string info, string logSignature = "", string caller = "")
        {
            this._Logger.LogInfo(info, logSignature, caller);
        }
        /// <summary>
        /// This will log a warning
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        public void LogWarning(string warning, string logSignature = "", string caller = "")
        {
            this._Logger.LogWarning(warning, logSignature, caller);
        }
        /// <summary>
        /// Finds logs for specific date range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="partialSearchString">string that Signature or Message property may contain</param>
        /// <param name="type">Type of log (Exception, Error, Warning, Information)</param>
        /// <param name="origin">The method where this log entry was originated from</param>
        /// <returns></returns>
        public IList<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearchString = "", string type = "", string origin = "")
        {
            return this._Logger.FindLogs(fromDate, toDate, partialSearchString, type, origin);
        }
    }
}
