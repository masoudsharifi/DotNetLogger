using System;
using System.Text;
using MongoDB.Driver;
using System.Collections.Generic;

using DotNetLogger.Models;

namespace DotNetLogger.Mongo
{
    /// <summary>
    /// This is the MongoDB Context
    /// </summary>
    public class MongoLogDbContext
    {
        #region Properties...
        /// <summary>
        /// This is the database context object
        /// </summary>
        public IMongoDatabase DatabaseContext { get; }
        /// <summary>
        /// This is the Logs collection
        /// </summary>
        public IMongoCollection<Log> Logs
        {
            get
            {
                return this.DatabaseContext.GetCollection<Log>("Logs");
            }
        }
        #endregion

        #region Constructors 
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MongoLogDbContext(string connectionString)
        {
            MongoClient client = new MongoClient(connectionString);
            this.DatabaseContext = client.GetDatabase("DotNetLogger");
        }
        #endregion
        

    }
}
