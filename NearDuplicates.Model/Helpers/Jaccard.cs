using System.Collections.Generic;
using System.Linq;

namespace NearDuplicatesAnalysis.Model.Helpers
{
    public class Jaccard
    {
        public static double Calc(HashSet<int> hs1, HashSet<int> hs2)
        {
            return ((double)hs1.Intersect(hs2).Count() /
                    (double)hs1.Union(hs2).Count());
        }

        public static double Calc(List<int> ls1, List<int> ls2)
        {
            HashSet<int> hs1 = new HashSet<int>(ls1);
            HashSet<int> hs2 = new HashSet<int>(ls2);
            return Calc(hs1, hs2);
        }
    }
}
