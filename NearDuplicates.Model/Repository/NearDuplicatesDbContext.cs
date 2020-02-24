using System.Data.Entity;
using NearDuplicatesAnalysis.Model.Models;

namespace NearDuplicatesAnalysis.Model.Repository
{
    public class NearDuplicatesDbContext : DbContext
    {
        public NearDuplicatesDbContext() : base("NearDuplicates")
        {
        }

        public DbSet<Listing> Listings { get; set; }
    }
}
