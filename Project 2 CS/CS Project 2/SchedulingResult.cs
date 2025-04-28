using System.Collections.Generic;
using System.Linq;

namespace CPUSchedulingSimulation
{
    public class SchedulingResult
    {
        public string AlgorithmName { get; }
        public double AvgWaitingTime { get; private set; }
        public double AvgTurnaroundTime { get; private set; }
        public double CpuUtilization { get; private set; }
        public double Throughput { get; private set; }
        public double AvgResponseTime { get; private set; }
        public int TotalCpuTime { get; set; }
        private readonly List<int> _waitingTimes = new();
        private readonly List<int> _turnaroundTimes = new();
        private readonly List<int> _responseTimes = new();

        public List<Process> ScheduledProcesses { get; set; }

        public SchedulingResult(string name) => AlgorithmName = name;

        public void AddProcessMetrics(Process p)
        {
            _waitingTimes.Add(p.WaitingTime);
            _turnaroundTimes.Add(p.TurnaroundTime);
            _responseTimes.Add(p.ResponseTime);
        }

        public void CalculateFinalMetrics(int totalTime)
        {
            AvgWaitingTime = _waitingTimes.Average();
            AvgTurnaroundTime = _turnaroundTimes.Average();
            AvgResponseTime = _responseTimes.Average();
            Throughput = (double)_waitingTimes.Count / totalTime;
            CpuUtilization = (double)TotalCpuTime / totalTime * 100;
        }

        public override string ToString() =>
            $"{AlgorithmName,-10} {AvgWaitingTime,8:F2} {AvgTurnaroundTime,8:F2} " +
            $"{AvgResponseTime,8:F2} {CpuUtilization,8:F2}% {Throughput,8:F2}/s";
    }
}



