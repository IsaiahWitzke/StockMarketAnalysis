using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockMarketAnalysis
{
    class GraphicsProcessor
    {
        Pen pen15;
        System.Drawing.Graphics formGraphics;

        GraphicsProcessor(Chart chart)
        {
            pen15 = new System.Drawing.Pen(System.Drawing.Color.Black);
            formGraphics = chart.CreateGraphics();
            formGraphics.DrawLine(pen15, 0, 0, 200, 200);
        }
    }
}
