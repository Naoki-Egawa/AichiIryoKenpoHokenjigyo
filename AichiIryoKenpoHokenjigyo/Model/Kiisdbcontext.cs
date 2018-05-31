using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;


namespace AichiIryoKenpoHokenjigyo.Model
{
    class Kiisdbcontext : DbContext
    {
        public Kiisdbcontext()
            : base("name=KiisConectionString")
        {
        }

        public DbSet<CompanyInfomationTable> CompanyInfomationTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MusicGameData>(modelBuilder);
            //Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
