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
        Pen pen;
        Chart chart;
        int x, y = 0;
        bool drawing = false;

        public GraphicsProcessor(Chart chart)
        {
            pen = new Pen(Color.RoyalBlue, 1);
            this.chart = chart;
        }

        public void paint(object sender, PaintEventArgs e)
        {
            if (!drawing) return;
            e.Graphics.DrawLine(pen, x, 0, x, 10000);
            e.Graphics.DrawLine(pen, 0, y, 10000, y);
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
