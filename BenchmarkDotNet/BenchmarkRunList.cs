﻿using System.Collections.Generic;
using System.Linq;

namespace BenchmarkDotNet
{
    public class BenchmarkRunList : List<BenchmarkRun>
    {
        public void Print()
        {
            var minTicks = this.Min(run => run.ElapsedTicks);
            var maxTicks = this.Max(run => run.ElapsedTicks);
            var maxMs = this.Max(run => run.ElapsedMilliseconds);
            var tickWidth = maxTicks.ToString().Length;
            var msWidth = maxMs.ToString().Length;
            foreach (var run in this)
            {
                var hint = "";
                if (run.ElapsedTicks == minTicks)
                    hint = "min";
                if (run.ElapsedTicks == maxTicks)
                    hint = "max";
                run.Print(tickWidth, msWidth, hint);
            }
            PrintStatistic();
        }

        public void PrintStatistic()
        {
            ConsoleHelper.WriteLineStatistic("TickStats: Min={0}, Max={1}, Median={2}, Diff={3:00.00}%",
                              MinTicks, MaxTicks, MedianTicks, Error * 100);
            ConsoleHelper.WriteLineStatistic("MsStats: Min={0}, Max={1}, Median={2}",
                              MinMilliseconds, MaxMilliseconds, MedianMilliseconds);
        }

        public long MinTicks { get { return this.Min(run => run.ElapsedTicks); } }
        public long MaxTicks { get { return this.Max(run => run.ElapsedTicks); } }
        public long MedianTicks { get { return this.Median(run => run.ElapsedTicks); } }
        public long MinMilliseconds { get { return this.Min(run => run.ElapsedMilliseconds); } }
        public long MaxMilliseconds { get { return this.Max(run => run.ElapsedMilliseconds); } }
        public long MedianMilliseconds { get { return this.Median(run => run.ElapsedMilliseconds); } }
        public double Error
        {
            get { return (MaxTicks - MinTicks) * 1.0 / MinTicks; }
        }
    }
}