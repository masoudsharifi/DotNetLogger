using System;
using System.Text;
using System.Collections.Generic;

using DotNetLogger.Interfaces;
using System.Runtime.CompilerServices;
using DotNetLogger.Sql;

namespace DotNetLogger
{
    public class SqlLogger : ILogger
    {
        /// <summary>
        /// Connection string to the SQL database
        /// </summary>
        private string _ConnectionString { get; set; }

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlLogger(string connectionString)
        {
            this._ConnectionString = connectionString;
        }
        #endregion

        /// <summary>
        /// Logs an exception object to the SQL log database
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logSignature">This is a unique string that can be used to identify these types of exceptiosn (e.g. Sql Connection Exception)</param>
        /// <param name="caller">This will be automatically </param>
        public void LogException(Exception ex, string logSignature="", [CallerMemberName] string caller = "")
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
        public void LogError(string error, string logSignature = "", [CallerMemberName] string caller = "")
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
        public void LogInfo(string info, string logSignature = "", [CallerMemberName] string caller = "")
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
        public void LogWarning(string warning, string logSignature = "", [CallerMemberName] string caller = "")
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
    }
}
