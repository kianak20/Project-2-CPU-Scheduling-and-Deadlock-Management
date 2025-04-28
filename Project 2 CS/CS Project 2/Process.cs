namespace CPUSchedulingSimulation
{
    public class Process
    {
        public int Pid { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int RemainingTime { get; set; }
        public int? StartTime { get; set; }
        public int? CompletionTime { get; set; }
        public int ResponseTime => StartTime.HasValue ? StartTime.Value - ArrivalTime : -1;
        public int WaitingTime => TurnaroundTime - BurstTime;
        public int TurnaroundTime => CompletionTime.HasValue ? CompletionTime.Value - ArrivalTime : -1;

        public Process(int pid, int arrivalTime, int burstTime, int priority = 0)
        {
            Pid = pid;
            ArrivalTime = arrivalTime;
            BurstTime = burstTime;
            Priority = priority;
            RemainingTime = burstTime;
        }
    }
}


