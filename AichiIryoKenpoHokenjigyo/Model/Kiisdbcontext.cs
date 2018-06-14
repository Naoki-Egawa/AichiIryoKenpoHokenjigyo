using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.Data.SQLite.EF6;


namespace AichiIryoKenpoHokenjigyo.Model
{
    class Kiisdbcontext : DbContext
    {
        public Kiisdbcontext()
            : base("name=kiisConnectionString")
        {
        }

        public DbSet<CompanyInfomationTable> CompanyInfomationTable { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<Kiisdbcontext>(modelBuilder);
        //    Database.SetInitializer(sqliteConnectionInitializer);
        //}
    }
}
