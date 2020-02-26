using System.Data.Entity;
using NearDuplicatesAnalysis.Model.Migrations;
using NearDuplicatesAnalysis.Model.Repository;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class DatabaseManager
    {
        public static void CheckDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NearDuplicatesDbContext, Configuration>());
        }
    }
}
