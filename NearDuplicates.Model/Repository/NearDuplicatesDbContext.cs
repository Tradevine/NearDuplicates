using System.Data.Entity;
using System.Reflection;
using NearDuplicatesAnalysis.Model.Models;

namespace NearDuplicatesAnalysis.Model.Repository
{
    public class NearDuplicatesDbContext : DbContext
    {
        public NearDuplicatesDbContext() : base("NearDuplicates")
        {
        }

        public DbSet<Listing> Listings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateModelsFromMaps(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CreateModelsFromMaps(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(NearDuplicatesDbContext)));
        }
    }
}
