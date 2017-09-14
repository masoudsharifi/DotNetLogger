using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetLogger.Models
{
    /// <summary>
    /// This is the log object
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }

        /// <summary>
        /// This is the type of log entry (Exception, Error, Warning, Information)
        /// </summary>
        [Required]
        [BsonElement]
        public string Type { get; set; }

        /// <summary>
        /// THis is the timestamp of the log entry in UTC
        /// </summary>
        [Required]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// This is where in the code the log is coming from
        /// </summary>
        [BsonElement]
        public string Origin { get; set; }

        /// <summary>
        /// This is a signature to identify different groups of messages (eg. Sql Connection Error)
        /// </summary>
        [BsonElement]
        public string Signature { get; set; }

        /// <summary>
        /// This is the message in the log entry
        /// </summary>
        [Required]
        [BsonElement]
        public string Message { get; set; }
    }
}
    