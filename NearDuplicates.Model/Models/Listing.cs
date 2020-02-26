using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NearDuplicatesAnalysis.Model.Models
{
    public class Listing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Index(IsClustered = false, IsUnique = false)]
        public int seller_id { get; set; }

        [MaxLength(15)]
        public string seller_name { get; set; }

        public int category_id { get; set; }

        [MaxLength(256)]
        [Index(IsClustered = false, IsUnique = false)]
        public string mcat_path { get; set; }

        [MaxLength(80)]
        public string title { get; set; }

        public decimal? buy_now_price { get; set; }

        [MaxLength(2040)]
        public string description { get; set; }

        public int? photo_id { get; set; }

        public long? likely_duplicate_id_by_title { get; set; }

        public long? likely_duplicate_id_by_description { get; set; }

        [Index(IsClustered = false, IsUnique = false)]
        public double similarity_title { get; set; }

        [Index(IsClustered = false, IsUnique = false)]
        public double similarity_description { get; set; }

        public virtual IDictionary<string, int> ngrams_title { get; set; } = new Dictionary<string, int>();
        public virtual List<int> word_ids_title { get; set; } = new List<int>();
        public virtual List<uint> minhash_title { get; set; } = new List<uint>();

        public virtual IDictionary<string, int> ngrams_description { get; set; } = new Dictionary<string, int>();
        public virtual List<int> word_ids_description { get; set; } = new List<int>();
        public virtual List<uint> minhash_description { get; set; } = new List<uint>();
    }
}
