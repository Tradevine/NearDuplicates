
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace NearDuplicatesAnalysis.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NearDuplicatesAnalysis.Model.Repository.NearDuplicatesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
