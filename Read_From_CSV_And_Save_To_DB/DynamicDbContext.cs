using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Read_From_CSV_And_Save_To_DB.DbContexts
{
    public class DynamicDbContext : DbContext
    {
        public DynamicDbContext() : base("DynamicConnectionString") { }

        public DbSet<Airport_IATA_codes> AirportIATACodes { get; set; }
    }
}