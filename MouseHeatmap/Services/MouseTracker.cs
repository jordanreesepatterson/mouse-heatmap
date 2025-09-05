using System.Drawing;

namespace MouseHeatmapDemo.Services
{
    public class MouseTracker
    {
        private readonly List<Point> _positions = new();
        private bool _tracking;
        private Thread _worker;

        public void Start()
        {
            _tracking = true;
            _worker = new Thread(TrackMouse) { IsBackground = true };
            _worker.Start();
        }

        public void Stop()
        {
            _tracking = false;
            _worker?.Join();
        }

        public List<Point> GetPositions() => _positions;

        private void TrackMouse()
        {
            while (_tracking)
            {
                var pos = Cursor.Position;
                lock (_positions)
                {
                    // Console.WriteLine(pos);
                    _positions.Add(pos);
                }
                Thread.Sleep(250); // 1 second interval
            }
        }
    }
}
