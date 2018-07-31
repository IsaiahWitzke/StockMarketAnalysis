using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockMarketAnalysis
{
    // this class is for ease of use, and allows a user to graph some new data onto the chart without woring about the series and chart areas...
    class Plot
    {
        public Dictionary<double, double> data = new Dictionary<double, double>();  // "key" is the x values and the "values" as the y values
        Chart chart; // will hold the main static chart obj.
        string seriesName;

        public Plot(string name, Chart chart)
        {
            //update the static chart so that it has a new series
            this.chart = chart;
            this.chart.Series.Add(name);
            this.chart.Series[name].ChartType = SeriesChartType.Line;   // all plots using this constructed will be a line
            this.chart.Series[name].IsXValueIndexed = true;

            seriesName = name;  // for future index use
        }

        // goes through all the data and plots the dictionary as if the "key" is the x values and the "values" as the y values
        public void drawPlot()
        {
            chart.Series[seriesName].Points.Clear();    // don't really know how important this is atm, just know that if we ever want to update the plot, we need to update ALL the points

            foreach (var dataPoint in data)
            {
                this.chart.Series[seriesName].Points.AddXY(dataPoint.Key, dataPoint.Value);
            }


            for (int i = 0; i < this.chart.Series[seriesName].Points.Count(); i++)
            {
                if (i < 50)
                {
                    this.chart.Series[seriesName].Points[i].Color = Color.Transparent;
                }
                else
                {
                    this.chart.Series[seriesName].Points[i].Color = Color.Red;
                }
            }
        }
    }
}
