using System.Collections.Generic;

namespace NearDuplicatesAnalysis.Model.Models
{
    public class Listing
    {
        public virtual long id { get; set; }

        public int seller_id { get; set; }

        public string seller_name { get; set; }

        public int category_id { get; set; }

        public string mcat_path { get; set; }

        public string title { get; set; }

        public decimal? buy_now_price { get; set; }

        public string description { get; set; }

        public int? photo_id { get; set; }

        public long? likely_duplicate_id_by_title { get; set; }

        public long? likely_duplicate_id_by_description { get; set; }

        public double similarity_title { get; set; }

        public double similarity_description { get; set; }

        internal IDictionary<string, int> ngrams_title { get; set; } = new Dictionary<string, int>();
        internal List<int> word_ids_title { get; set; } = new List<int>();
        internal List<uint> minhash_title { get; set; } = new List<uint>();

        internal IDictionary<string, int> ngrams_description { get; set; } = new Dictionary<string, int>();
        internal List<int> word_ids_description { get; set; } = new List<int>();
        internal List<uint> minhash_description { get; set; } = new List<uint>();
    }
}
