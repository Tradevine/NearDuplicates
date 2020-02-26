using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NearDuplicatesAnalysis.Model.Models;

namespace NearDuplicatesAnalysis.Model.Repository
{
    public class ListingMap : EntityTypeConfiguration<Listing>
    {
        public ListingMap()
        {
            Property(t => t.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasKey(t => t.id, configuration => configuration.IsClustered(false));
            HasIndex(t => new { t.mcat_path }).IsClustered(true).HasName("C_mcat_path");
            HasIndex(t => new { t.seller_id }).IsClustered(false).HasName("IX_seller_id");
            Ignore(t => t.ngrams_title);
            Ignore(t => t.ngrams_description);
            Ignore(t => t.word_ids_title);
            Ignore(t => t.word_ids_description);
            Ignore(t => t.minhash_title);
            Ignore(t => t.minhash_description);
            ToTable("Listing");
            Property(x => x.seller_id).IsRequired();
            Property(x => x.category_id).IsRequired();
            Property(x => x.seller_name).IsRequired().HasMaxLength(15);
            Property(x => x.mcat_path).IsRequired().HasMaxLength(256);
            Property(x => x.title).IsRequired().HasMaxLength(80);
            Property(x => x.description).IsRequired().HasMaxLength(2050);
        }
    }
}
