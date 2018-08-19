using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.IO;

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
        public float firstPointRawX;
        public float firstPointRawY;
        public bool haveFirstPoint = false;

        public bool isDrawing = false;

        public LinePlotter(Chart chart)
        {
            this.chart = chart;
            plots = new List<Plot>();
            nonBusinessDays = new List<double>();
        }

        public void updateGaps()
        {
            //generate non business day list for given stock
            for (int i = 0; i < chart.Series[0].Points.Count() - 1; i++)
            {
                double diff = chart.Series[0].Points[i].XValue - chart.Series[0].Points[i + 1].XValue;
                //diff represents the type of missing dates
                // 1 is a regular day
                // 1 is a non business day within a week (ex Independace day on July 4th)
                // 3 is a weekend
                // 4 is a long weekend
                // 5 is an extra long weekend

                if (diff > 1)
                {
                    nonBusinessDays.Add(chart.Series[0].Points[i].XValue - 1);
                }
                if (diff > 2)
                {
                    nonBusinessDays.Add(chart.Series[0].Points[i].XValue - 2);
                }
                if (diff > 3)
                {
                    nonBusinessDays.Add(chart.Series[0].Points[i].XValue - 3);
                }
                if (diff > 4)
                {
                    nonBusinessDays.Add(chart.Series[0].Points[i].XValue - 4);
                }
            }
        }

        public void addLine(double xin1, double yin1, double xin2, double yin2)
        {
            Plot newLine = new Plot("userPlot#" + count, chart, Color.Indigo);
            plots.Add(newLine);

            //normalize values making x1 and y1 the smaller x value and rightmost point
            //allows proper intercept calculation (I have no idea why)
            double x1, y1, x2, y2;
            if (xin1 < xin2)
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
                newLine.addPoint(x, y);

                //on increment day count if current day is a business day
                if (!nonBusinessDays.Contains(x))
                {
                    actualDate++;
                }
            }

            newLine.showPlot();
            count++;
        }

        public void deleteAll()
        {
            foreach (var plot in plots)
            {
                plot.deletePlot();
            }
            plots.Clear();
        }

        public void draw(MouseEventArgs e)
        {
            if (!isDrawing) return;

            //get location on chart
            var pos = e.Location;

            //exit if click is not in chart data area
            ChartElementType cet = ChartHandler.chart.HitTest(pos.X, pos.Y).ChartElementType;
            switch (cet)
            {
                //valid input areas
                case ChartElementType.Gridlines:
                case ChartElementType.DataPoint:
                case ChartElementType.PlottingArea:
                    break;
                default:
                    return;
            }


            var x = ChartHandler.chart.ChartAreas[0].AxisX.PixelPositionToValue(pos.X); //x value is number of bars  counting from the right of the graph
            var y = ChartHandler.chart.ChartAreas[0].AxisY.PixelPositionToValue(pos.Y); //y value translates on to graph directly (no changes necessary)

            //get closes data point's x value
            var index = Convert.ToInt32(Math.Round(x));
            if (index < 0 || index >= ChartHandler.chart.Series[0].Points.Count) return; //pevents clicks just inside chart making invalid indices;
            var pointIndex = ChartHandler.chart.Series[0].Points[index].XValue;


            //save raw coordinates used for ghost line before real line is drawn
            firstPointRawX = (float)e.X;
            firstPointRawY = (float)e.Y;


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

                //holding shift continues drawing
                if (Control.ModifierKeys == Keys.Shift)
                {
                    draw(e);
                }
            }
        }

        //saves all the current plots to whatever stream reader
        public void savePlotsToFile(StreamWriter sw)
        {
            //iterate through every plot and call the plot's save funtion
            foreach (Plot plot in plots)
            {
                plot.savePlotToFile(sw);
            }
        }
    }
}
