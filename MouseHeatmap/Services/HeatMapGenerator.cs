using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MouseHeatmap.Services
{
    public class HeatmapGenerator
    {
        public void Generate(List<Point> positions, string outputPath)
        {
            if (positions.Count == 0) return;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int cellSize = 10;
            int gridWidth = screenWidth / cellSize;
            int gridHeight = screenHeight / cellSize;
            double[,] grid = new double[gridHeight, gridWidth];

            foreach (var pos in positions)
            {
                int x = Math.Min(pos.X / cellSize, gridWidth - 1);
                int y = Math.Min(pos.Y / cellSize, gridHeight - 1);
                grid[y, x] += 1;
            }


            // Optionally, normalize the grid for better color scaling
            double max = 0;
            foreach (var value in grid) if (value > max) max = value;
            if (max > 0)
            {
                //for (int y = 0; y < screenHeight; y++)
                //    for (int x = 0; x < screenWidth; x++)
                //        grid[y, x] /= max; // scale 0..1
            }

            // Create ScottPlot with reduced size for performance
            var plt = new ScottPlot.Plot(screenWidth / 4, screenHeight / 4);

            // Add heatmap
            var hm = plt.AddHeatmap(
    grid,
    lockScales: false,
    colormap: ScottPlot.Drawing.Colormap.Inferno
);

            // Optionally invert Y so origin is top-left
            hm.XMin = 0;
            hm.XMax = screenWidth;
            hm.YMin = 0;
            hm.YMax = screenHeight;

            plt.SaveFig(outputPath);
        }
    }
}
