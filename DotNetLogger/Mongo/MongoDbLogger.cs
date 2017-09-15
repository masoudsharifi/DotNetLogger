using System;
using System.Text;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using DotNetLogger.Models;
using DotNetLogger.Interfaces;
using MongoDB.Driver;

namespace DotNetLogger.Mongo
{
    /// <summary>
    /// This class will write logs to a MongoDb database
    /// </summary>
    public class MongoDbLogger : ILogger
    {
        #region Properties....
        private MongoLogDbContext _DbContext = null;
        #endregion

        #region Constructor...
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MongoDbLogger(string connectionString)
        {
            this._DbContext = new MongoLogDbContext(connectionString);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Logs an error message to the MongoDb log database
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
            this._DbContext.Logs.InsertOne(log);
        }
        /// <summary>
        /// Logs an exception object to the MongoDb log database
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
            this._DbContext.Logs.InsertOne(log);
        }
        /// <summary>
        /// Logs an information message to the MongoDb log database
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
            this._DbContext.Logs.InsertOne(log);
        }
        /// <summary>
        /// Logs a warning message to the MongoDb log database
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
            this._DbContext.Logs.InsertOne(log);
        }

        public List<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearch)
        {
            QueryBuilder<Log> qb = new QueryBuilder<Log>();
            var gteQ = qb.GTE<DateTime>(l => l.CreatedOn, fromDate);
            var lteQ = qb.LTE<DateTime>(l => l.CreatedOn, toDate);
            var sigQ = qb.Where(l => l.Signature.Contains(partialSearch));
            var msgQ = qb.Where(l => l.Message.Contains(partialSearch));
            qb.And(new IMongoQuery[] { gteQ, lteQ, sigQ, msgQ });

            var logs = this._DbContext.Logs.Find(qb.ToString()).ToList<Log>();
            return logs;
        }
        #endregion
    }
}
