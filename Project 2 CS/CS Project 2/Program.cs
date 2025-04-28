using System;
using System.Collections.Generic;
using System.Linq;

namespace CPUSchedulingSimulation
{
    class Program
    {
        static void PrintProcessDetails(List<Process> processes)
        {
            Console.WriteLine("PID | Arrival | Burst | Start | Finish | Waiting | Turnaround | Response");
            foreach (var p in processes.OrderBy(p => p.Pid))
            {
                Console.WriteLine($"{p.Pid,3} | {p.ArrivalTime,7} | {p.BurstTime,5} | {p.StartTime,5} | {p.CompletionTime,6} | " +
                                  $"{p.WaitingTime,7} | {p.TurnaroundTime,10} | {p.ResponseTime,8}");
            }
        }

        static void Main(string[] args)
        {
            // small hand-calculable test
            List<Process> processes = new List<Process>
            {
                new Process(1, 0, 8, 2),
                new Process(2, 1, 4, 1),
                new Process(3, 2, 9, 3),
                new Process(4, 3, 5, 2)
            };

            Console.WriteLine("Algorithm     AvgWait     AvgTAT     AvgResp   CPU_Util   Throughput");
            Console.WriteLine("---------------------------------------------------------------");

            var srtfResult = Algorithms.SRTF(processes);
            Console.WriteLine(srtfResult);
            Console.WriteLine("\nSRTF Process Details:");
            PrintProcessDetails(srtfResult.ScheduledProcesses);

            var mlfqResult = Algorithms.MLFQ(processes);
            Console.WriteLine(mlfqResult);
            Console.WriteLine("\nMLFQ Process Details:");
            PrintProcessDetails(mlfqResult.ScheduledProcesses);

            // large/random workload test
            Console.WriteLine("\n--- Large/Random Workload Test ---");
            var randomProcesses = GenerateRandomProcesses(20); // e.g., 20 processes

            var srtfLarge = Algorithms.SRTF(randomProcesses);
            Console.WriteLine(srtfLarge);

            var mlfqLarge = Algorithms.MLFQ(randomProcesses);
            Console.WriteLine(mlfqLarge);


        }

        // generate random processes for large-scale testing
        static List<Process> GenerateRandomProcesses(int count)
        {
            var rand = new Random();
            var list = new List<Process>();
            for (int i = 1; i <= count; i++)
            {
                int arrival = rand.Next(0, 10);      // arrival between 0 and 9
                int burst = rand.Next(1, 10);        // burst between 1 and 9
                int priority = rand.Next(1, 5);      // priority between 1 and 4
                list.Add(new Process(i, arrival, burst, priority));
            }
            return list;
        }
    }
}

