using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using DotNetLogger.Models;

namespace DotNetLogger.Sql
{
    /// <summary>
    /// This is the SQL Log DB Context
    /// </summary>
    public class SqlLogDbContext : DbContext
    {
        #region Properties.....
        private string _DbConnnectionString = String.Empty;
        /// <summary>
        /// Logs entity
        /// </summary>
        public DbSet<Log> Logs { get; set;}
        #endregion

        #region Constructors...        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlLogDbContext(string connectionString) : base()
        {
            this._DbConnnectionString = connectionString;
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DotNetLogger.txt", true))
                {
                    writer.WriteLine($"An error occured while initializing the database for the DotNetLogger:{Environment.NewLine}{ex.ToString()}");
                    writer.Close();
                }
            }
        }
        #endregion

        #region Overrides......
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._DbConnnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Log>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Log>()
                .HasIndex(l => new { l.CreatedOn });

            modelBuilder.Entity<Log>()
                .HasIndex(l => new { l.Signature });

            modelBuilder.Entity<Log>()
                .HasIndex(l => new { l.Message });
        }
        #endregion

        #region Private Methods
        private void CreateDb()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DotNetLogger.txt", true))
                {
                    writer.WriteLine($"An error occured while initializing the database for the DotNetLogger:{Environment.NewLine}{ex.ToString()}");
                    writer.Close();
                }
            }
        }
        #endregion
    }
}
