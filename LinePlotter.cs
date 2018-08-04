using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    class LinePlotter
    {
        private Chart chart;
        private List<Plot> plots;
        private int count = 0;
        public List<double> nonBusinessDays;

        private double firstPointX;
        private double firstPointY;
        private bool haveFirstPoint = false;

        public bool isDrawing = false;

        public LinePlotter(Chart chart)
        {
            this.chart = chart;
            plots = new List<Plot>();
            nonBusinessDays = new List<double>();
        }

        public void addLine(double xin1, double yin1, double xin2, double yin2)
        {
            Plot newLine = new Plot("userPlot#" + count, chart, Color.Indigo);
            plots.Add(newLine);

            //normalize values making x1 and y1 the smaller x value and rightmost point
            //allows proper intercept calculation (I have no idea why)
            double x1, y1, x2, y2;
            if(xin1 < xin2)
            {
                x1 = xin1;
                y1 = yin1;
                x2 = xin2;
                y2 = yin2;
            }
            else
            {
                x1 = xin2;
                y1 = yin2;
                x2 = xin1;
                y2 = yin1;
            }

            //count actual number of days between points
            double run = x2 > x1 ? 1 : -1; //fixes mysterious off by 1 error
            for (double x = x1; x <= x2; x++)
            {
                //don't count non business days
                if (!nonBusinessDays.Contains(x))
                {
                    run = x1 > x2 ? run + 1 : run - 1; //support counting in both directions
                }
            }

            double rise = y1 - y2;
            double slope = rise / run;
            double intercept = y1 - (slope * x1);
            double actualDate = x1; //the actual date that does not include weekends

            //loop between points
            for (double x = x1; x <= x2; x++)
            {
                double y = slope * actualDate + intercept;
                newLine.data.Add(x, y);

                //on increment day count if current day is a business day
                if(!nonBusinessDays.Contains(x))
                {
                    actualDate++;
                }
            }

            newLine.initPlot();
            count++;
        }

        public void deleteAll()
        {
            foreach (var plot in plots)
            {
                plot.removePlot();
            }
            plots.Clear();
        }

        public void draw(bool drawing, MouseEventArgs e)
        {
            if (!drawing) { return; }
            //get location on chart
            var pos = e.Location;
            var x = ChartHandler.chart.ChartAreas[0].AxisX.PixelPositionToValue(pos.X); //x value is number of bars  counting from the right of the graph
            var y = ChartHandler.chart.ChartAreas[0].AxisY.PixelPositionToValue(pos.Y); //y value translates on to graph directly (no changes necessary)

            //get closes data point's x value
            var index = Convert.ToInt32(Math.Round(x));
            var pointIndex = ChartHandler.chart.Series[0].Points[index].XValue;

            if (!haveFirstPoint)
            {
                firstPointX = pointIndex;
                firstPointY = y;
                haveFirstPoint = true;
            }
            else
            {
                addLine(firstPointX + 1, firstPointY, pointIndex + 1, y);
                haveFirstPoint = false;
            }
        }

    }
}
