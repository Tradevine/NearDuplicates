using System;
using System.Collections.Generic;

namespace NearDuplicatesAnalysis.Model.Services
{
    public static class ProgressManager
    {
        private static readonly Dictionary<string, decimal> jobs = new Dictionary<string, decimal>();

        public static void UpdateJobPercent(string id, decimal percent)
        {
            jobs[id] = percent;
        }

        public static void IncrementJobPercentBy(string id, decimal increment)
        {
            jobs[id] = jobs[id] + increment;
        }

        public static decimal GetJobPercentProgress(string id)
        {
            if (!jobs.ContainsKey(id))
                return 0;

            return Math.Round(jobs[id], 1);
        }

        public static decimal CalculateLoopIncrement(decimal loopTotal, decimal overallComponentPercent)
        {
            if (loopTotal == 0)
                return 0.001M;

            return (1M / loopTotal) * 100 * overallComponentPercent;
        }
    }
}
