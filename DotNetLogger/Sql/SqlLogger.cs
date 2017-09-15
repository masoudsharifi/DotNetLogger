using System;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//...Internal References
using DotNetLogger.Sql;
using DotNetLogger.Models;
using DotNetLogger.Abstraction;

namespace DotNetLogger.Sql
{
    /// <summary>
    /// This class will write logs to a SQL database
    /// </summary>
    public class SqlLogger : Logger
    {
        #region Properties....
        /// <summary>
        /// Connection string to the SQL database
        /// </summary>
        private string _ConnectionString { get; set; }
        #endregion

        #region Constructors..
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlLogger(string connectionString)
        {
            this._ConnectionString = connectionString;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Logs an exception object to the SQL log database
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature">This is a unique string that can be used to identify these types of exceptiosn (e.g. Sql Connection Exception)</param>
        /// <param name="caller">This will be automatically </param>
        internal override void LogException(Exception ex, string logSignature="", string caller = "")
        {
            using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
            {
                dbContext.Logs.Add(new Models.Log
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = "Exception",
                    Origin = caller,
                    Message = ex.ToString(),
                    CreatedOn = DateTime.UtcNow,
                    Signature = logSignature
                });

                dbContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Logs an error message to the SQL log database
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logSignature">This is a unique string that can be used to identify these types of exceptiosn (e.g. Sql Connection Exception)</param>
        /// <param name="caller"></param>
        internal override void LogError(string error, string logSignature = "", string caller = "")
        {
            using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
            {
                dbContext.Logs.Add(new Models.Log
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = "Error",
                    Origin = caller,
                    Message = error,
                    CreatedOn = DateTime.UtcNow,
                    Signature = logSignature
                });

                dbContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Logs information to the SQL log database
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logSignature">This is a unique string that can be used to identify these types of exceptiosn (e.g. Sql Connection Exception)</param>
        /// <param name="caller"></param>
        internal override void LogInfo(string info, string logSignature = "", string caller = "")
        {
            using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
            {
                dbContext.Logs.Add(new Models.Log
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = "Information",
                    Origin = caller,
                    Message = info,
                    CreatedOn = DateTime.UtcNow,
                    Signature = logSignature
                });

                dbContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Logs a warning message to the SQL log database
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="logSignature">This is a unique string that can be used to identify these types of exceptiosn (e.g. Sql Connection Exception)</param>
        /// <param name="caller"></param>
        internal override void LogWarning(string warning, string logSignature = "", string caller = "")
        {
            using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
            {
                dbContext.Logs.Add(new Models.Log
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = "Warning",
                    Origin = caller,
                    Message = warning,
                    CreatedOn = DateTime.UtcNow,
                    Signature = logSignature
                });

                dbContext.SaveChangesAsync();
            }
        }
        ///// <summary>
        ///// Finds a single log record by ID
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public Log FindByID(string id)
        //{
        //    Log log = null;
        //    using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
        //    {
        //        log = dbContext.Logs.Where(l => l.ID == id).FirstOrDefault();
        //    }

        //    return log;
        //}
        /// <summary>
        /// Find logs that is returned by the searchExpression
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="partialSearchString"></param>
        /// <param name="type"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        internal override IList<Log> FindLogs(DateTime fromDate, DateTime toDate, string partialSearchString = "", string type = "", string origin = "")
        {
            List<Log> logs = new List<Log>();
            using (SqlLogDbContext dbContext = new SqlLogDbContext(this._ConnectionString))
            {
                logs = dbContext.Logs
                            .Where(
                                l => l.CreatedOn >= fromDate &&
                                     l.CreatedOn <= toDate &&
                                     (partialSearchString == null || 
                                      partialSearchString == String.Empty ||
                                      l.Signature.Contains(partialSearchString) ||
                                      l.Message.Contains(partialSearchString)
                                     ) &&
                                     (type == "" || l.Type == type) &&
                                     (origin == "" || l.Origin == origin)
                            )
                            .ToList<Log>();                            
            }

            return logs;
        }
        #endregion
    }
}
