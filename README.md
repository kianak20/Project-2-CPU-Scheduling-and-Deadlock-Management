# Project-2-CPU-Scheduling-and-Deadlock-Management
Project 2: CPU Scheduling and Deadlock Management: A System-Level Simulation


This project is a C# console application that simulates CPU scheduling using two advanced algorithms:
- Shortest Remaining Time First (SRTF)
- Multi-Level Feedback Queue (MLFQ)

## Features

- Simulates process scheduling for both small (hand-calculable) and large (random) workloads
- Measures and reports key performance metrics:
  - Average Waiting Time
  - Average Turnaround Time
  - CPU Utilization
  - Throughput
  - Response Time

## Getting Started

### Prerequisites

- [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)

### Running the Simulator

1. **Clone or download** this repository.
2. Open a terminal in the project folder.
3. Run the following command:


4. The program will output scheduling results for both SRTF and MLFQ algorithms.

### Project Structure

- `Program.cs` - Main entry point, runs tests and prints results
- `Process.cs` - Defines the process model
- `SchedulingResult.cs` - Collects and reports performance metrics
- `Algorithms.cs` - Contains SRTF and MLFQ implementations

## Customization

- To test different workloads, modify the process list in `Program.cs` or adjust the random workload generator.

## Report

See the included PDF report for detailed analysis and findings.



