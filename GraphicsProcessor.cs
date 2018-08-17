using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockMarketAnalysis
{
    class GraphicsProcessor
    {
        Pen crosshairLines, ghostLines;
        Chart chart;
        int x, y = 0;
        bool drawing = false;
        LinePlotter linePlotter;

        public GraphicsProcessor(Chart chart, LinePlotter linePlotter)
        {
            crosshairLines = new Pen(Color.RoyalBlue, 1);
            ghostLines = new Pen(Color.Purple, 1);
            this.chart = chart;
            this.linePlotter = linePlotter;
        }

        public void paint(object sender, PaintEventArgs e)
        {
            if (!drawing) return;
            e.Graphics.DrawLine(crosshairLines, x, 0, x, 10000);
            e.Graphics.DrawLine(crosshairLines, 0, y, 10000, y);

            if(linePlotter.haveFirstPoint)
            {
                e.Graphics.DrawLine(ghostLines, linePlotter.firstPointRawX, linePlotter.firstPointRawY, x, y);
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            chart.Invalidate();
        }

        public void enter(object sender, EventArgs e)
        {
            drawing = true;
            chart.Invalidate();
        }
        public void exit(object sender, EventArgs e)
        {
            drawing = false;
            chart.Invalidate();
        }
    }
}
