using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using DotNetLogger.Models;
using DotNetLogger.Abstraction;

namespace DotNetLogger.Mongo
{
    /// <summary>
    /// This class will write logs to a MongoDb database
    /// </summary>
    public class MongoDbLogger : Logger
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
        internal override void LogError(string error, string logSignature = "", string caller = "")
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
        internal override void LogException(Exception ex, string logSignature = "", string caller = "")
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
        internal override void LogInfo(string info, string logSignature = "", string caller = "")
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
        internal override void LogWarning(string warning, string logSignature = "", string caller = "")
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
        ///// <summary>
        ///// Finds a single log record by ID
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //internal override Log FindByID(string id)
        //{
        //    var filter = Builders<Log>.Filter.Eq("_id", new ObjectId(id));
        //    var log = this._DbContext.Logs.Find<Log>(filter).FirstOrDefault<Log>();

        //    return log;
        //}
        /// <summary>
        /// Find logs that contain the partialSearchString
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="partialSearch"></param>
        /// <param name="type"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        internal override IList<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearch = "", string type = "", string origin = "")
        {
            //QueryBuilder<Log> qb = new QueryBuilder<Log>();
            //var gteQ = qb.GTE<DateTime>(l => l.CreatedOn, fromDate);
            //var lteQ = qb.LTE<DateTime>(l => l.CreatedOn, toDate);
            //var sigQ = qb.Where(l => partialSearch == "" || l.Signature.Contains(partialSearch));
            //var msgQ = qb.Where(l => partialSearch == "" || l.Message.Contains(partialSearch));
            //var typeQ = qb.Where(l => type == "" || l.Type.Equals(type));
            //var orgQ = qb.Where(l => origin == "" || l.Origin.Equals(origin));
            //qb.And(new IMongoQuery[] { gteQ, lteQ, sigQ, msgQ });

            //var query = Query.And(
            //    Query<Log>.GTE(l => l.CreatedOn, fromDate),
            //    Query<Log>.LTE(l => l.CreatedOn, toDate),
            //    Query<Log>.Matches(l => l.Signature, new BsonRegularExpression(partialSearch)),
            //    Query<Log>.Matches(l => l.Message, new BsonRegularExpression(partialSearch)),
            //    Query<Log>.EQ(l => l.Type, type),
            //    Query<Log>.EQ(l => l.Origin, origin)
            //);

            var logs = this._DbContext.Logs.Find(l =>   l.CreatedOn >= fromDate &&
                                                        l.CreatedOn <= toDate &&
                                                        (partialSearch == "" || l.Signature.Contains(partialSearch) || l.Message.Contains(partialSearch)) &&
                                                        (type == "" || l.Type == type) &&
                                                        (origin == "" || l.Origin == origin)).ToList<Log>();
            return logs;
        }
        #endregion
    }
}
