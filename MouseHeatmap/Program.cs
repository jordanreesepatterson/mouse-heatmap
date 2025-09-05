using System;
using System.Diagnostics;
using MouseHeatmap.Services;
using MouseHeatmapDemo.Services;

class Program
{
    static void Main(string[] args)
    {
        var tracker = new MouseTracker();
        var generator = new HeatmapGenerator();

        Console.WriteLine("Mouse heatmap demo");
        Console.WriteLine("Commands: start, stop, exit");

        bool running = true;
        while (running)
        {
            Console.Write("> ");
            var command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "start":
                    tracker.Start();
                    Console.WriteLine("Tracking started...");
                    break;
                case "stop":
                    tracker.Stop();
                    Console.WriteLine("Tracking stopped.");
                    var positions = tracker.GetPositions();
                    string path = Path.Combine(Environment.CurrentDirectory, "heatmap.png");
                    generator.Generate(positions, path);
                    Console.WriteLine($"Heatmap saved as {path}");

                    // Open the heatmap automatically
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = path,
                            UseShellExecute = true 
                        };
                        Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to open heatmap: {ex.Message}");
                    }
                    break;
                case "exit":
                    tracker.Stop();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
