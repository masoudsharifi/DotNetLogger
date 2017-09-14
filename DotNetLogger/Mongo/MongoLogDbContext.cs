using System;
using System.Text;
using MongoDB.Driver;
using System.Collections.Generic;


namespace DotNetLogger.Mongo
{
    public class MongoLogDbContext
    {
        public IMongoDatabase DatabaseContext { get; }

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
