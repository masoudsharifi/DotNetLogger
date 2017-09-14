using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DotNetLogger.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature"></param>
        /// <param name="callingMethod"></param>
        void LogException(Exception ex, string logSignature = "", [CallerMemberName] string callingMethod = "");
        /// <summary>
        /// Logs an error passed as a string
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logSignature"></param>
        /// <param name="callingMethod"></param>
        void LogError(string error, string logSignature = "", [CallerMemberName] string callingMethod = "");
        /// <summary>
        /// Logs information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logSignature"></param>
        /// <param name="callingMethod"></param>
        void LogInfo(string info, string logSignature = "", [CallerMemberName] string callingMethod = "");
        /// <summary>
        /// Logs warning passed as string
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="logSignature"></param>
        /// <param name="callingMethod"></param>
        void LogWarning(string warning, string logSignature = "", [CallerMemberName] string callingMethod = "");
    }
}
