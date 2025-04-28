using System.Collections.Generic;
using System.Linq;

namespace CPUSchedulingSimulation
{
    public static class Algorithms
    {
        public static SchedulingResult SRTF(List<Process> processes)
        {
            var result = new SchedulingResult("SRTF");
            var time = 0;
            var completed = 0;
            var readyQueue = new List<Process>();
            var processList = processes.Select(p => new Process(p.Pid, p.ArrivalTime, p.BurstTime, p.Priority)).ToList();

            while (completed < processList.Count)
            {
                // add arriving processes
                readyQueue.AddRange(processList
                    .Where(p => p.ArrivalTime == time && !readyQueue.Contains(p) && p.RemainingTime > 0));

                if (readyQueue.Any())
                {
                    var current = readyQueue.OrderBy(p => p.RemainingTime).First();

                    if (!current.StartTime.HasValue)
                        current.StartTime = time;

                    current.RemainingTime--;
                    result.TotalCpuTime++;

                    if (current.RemainingTime == 0)
                    {
                        current.CompletionTime = time + 1;
                        result.AddProcessMetrics(current);
                        completed++;
                    }

                    readyQueue = readyQueue.Where(p => p.RemainingTime > 0).ToList();
                }
                time++;
            }

            result.CalculateFinalMetrics(time);
            result.ScheduledProcesses = processList;
            return result;
        }

        // fixed and improved MLFQ
        public static SchedulingResult MLFQ(List<Process> processes, int queues = 3, int[] quantums = null)
        {
            quantums ??= new[] { 4, 8, 12 };
            var result = new SchedulingResult("MLFQ");
            var time = 0;
            var completed = 0;
            var processList = processes.Select(p => new Process(p.Pid, p.ArrivalTime, p.BurstTime, p.Priority)).ToList();
            var readyQueues = new List<Queue<Process>>(queues);
            for (int i = 0; i < queues; i++) readyQueues.Add(new Queue<Process>());

            while (completed < processList.Count)
            {
                // add arriving processes - highest priority queue
                foreach (var p in processList.Where(p =>
                    p.ArrivalTime <= time &&
                    !readyQueues.Any(q => q.Contains(p)) &&
                    p.RemainingTime > 0 &&
                    (p.StartTime == null || p.RemainingTime == p.BurstTime)))
                {
                    readyQueues[0].Enqueue(p);
                }

                bool processed = false;
                for (int i = 0; i < queues; i++)
                {
                    if (readyQueues[i].Count > 0)
                    {
                        var current = readyQueues[i].Dequeue();
                        // set StartTime if it's the first run
                        if (!current.StartTime.HasValue)
                            current.StartTime = time;

                        var quantum = quantums.Length > i ? quantums[i] : quantums.Last();
                        var executionTime = System.Math.Min(quantum, current.RemainingTime);

                        // execute the process for executionTime units
                        for (int t = 0; t < executionTime; t++)
                        {
                            current.RemainingTime--;
                            result.TotalCpuTime++;
                            time++;

                            // add new arrivals at this time
                            foreach (var np in processList.Where(p =>
                                p.ArrivalTime == time &&
                                !readyQueues.Any(q => q.Contains(p)) &&
                                p.RemainingTime > 0 &&
                                (p.StartTime == null || p.RemainingTime == p.BurstTime)))
                            {
                                readyQueues[0].Enqueue(np);
                            }

                            if (current.RemainingTime == 0)
                            {
                                current.CompletionTime = time;
                                result.AddProcessMetrics(current);
                                completed++;
                                break;
                            }
                        }

                        if (current.RemainingTime > 0)
                        {
                            // demote to next queue if not finished
                            var nextQueue = System.Math.Min(i + 1, queues - 1);
                            readyQueues[nextQueue].Enqueue(current);
                        }

                        processed = true;
                        break;
                    }
                }

                if (!processed) time++;
            }

            result.CalculateFinalMetrics(time);
            result.ScheduledProcesses = processList;
            return result;
        }
    }
}



