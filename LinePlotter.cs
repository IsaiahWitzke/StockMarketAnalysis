using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockMarketAnalysis
{
    class LinePlotter
    {
        private Chart chart;
        private List<Plot> plots;
        private int count = 0;
        public List<double> nonBusinessDays;

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
    }
}
