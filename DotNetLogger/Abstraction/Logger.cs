﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//...Internal References
using DotNetLogger.Models;
using System.Linq.Expressions;

namespace DotNetLogger.Abstraction
{
    public abstract class Logger
    {
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        internal abstract void LogException(Exception ex, string logSignature = "", string caller = "");
        /// <summary>
        /// Logs an error passed as a string
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        internal abstract void LogError(string error, string logSignature = "", string caller = "");
        /// <summary>
        /// Logs information
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        internal abstract void LogInfo(string info, string logSignature = "", string caller = "");
        /// <summary>
        /// Logs warning passed as string
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="logSignature"></param>
        /// <param name="caller"></param>
        internal abstract void LogWarning(string warning, string logSignature = "", string caller = "");
        ///// <summary>
        ///// Finds a log record by its ID
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //internal abstract Log FindByID(string id);
        /// <summary>
        /// Finds logs record for a time range that contain a string in their signature or message field
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="partialSearchString"></param>
        /// <returns></returns>
        internal abstract IList<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearchString, string type, string origin);
    }
}
