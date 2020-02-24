using System;
using NearDuplicatesAnalysis.Model.Services;

namespace NearDuplicatesAnalysis.Console
{
    class Program
    {
        private static readonly ListingDownloader ListingDownloader = new ListingDownloader();

        static void Main(string[] args)
        {
            try
            {
                // Switch on arguments
                if (args.Length == 0)
                {
                    PrintUsage();
                }
                else if (args[0] == "-getnewlistings")
                {
                    ListingDownloader.GetNewListings();
                }
                else if (args[0] == "-analyse")
                {
                    AnalyseListings.ProcessSeller(6139471);
                }
                else
                {
                    PrintUsage();
                }

                System.Console.WriteLine("All good...");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                System.Console.ReadLine();
            }
        }

        private static void PrintUsage()
        {
            System.Console.WriteLine("-getnewlistings : Download new listings from TMDATA.Search database and save their ngram sets");
            System.Console.WriteLine("-analyse : Per-seller, calculate min-hashes and LSH and save closest listings if over our threshold");
            System.Console.WriteLine();
        }
    }
}
