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

        Color color = Color.Black; 

        //for less copy/pasting, called in every init function
        private void basicInit(string name, Chart chart)
        {
            //update the static chart so that it has a new series
            this.chart = chart;
            this.chart.Series.Add(name);
            this.chart.Series[name].ChartType = SeriesChartType.Line;   // all plots using this constructed will be a line
            this.chart.Series[name].IsXValueIndexed = true;

            seriesName = name;  // for future index use
        }

        public Plot(string name, Chart chart)
        {
            basicInit(name, chart);
        }

        public Plot(string name, Chart chart, Color color)
        {
            basicInit(name, chart);
            this.color = color;
        }

        // goes through all the data and plots the dictionary as if the "key" is the x values and the "values" as the y values
        public void updatePlot()
        {
            //to refresh all points, they must first be removed
            chart.Series[seriesName].Points.Clear();

            //now going through the main x axis. If there is no data point for this plot at that point, 
            //then we will make it the candle's high (as to not mess with the chart's y axis' zoom) and transparent,
            //otherwise, plot it as intended
            for (int i = 0; i < chart.Series[0].Points.Count(); i++)
            {
                System.Console.WriteLine(chart.Series[0].Points[i].XValue);
                try
                {
                    this.chart.Series[seriesName].Points.AddXY(
                        chart.Series[0].Points[i].XValue, 
                        data[chart.Series[0].Points[i].XValue]);

                    //this bit needs to be here or else an extra point is rendered to the screen
                    if (i > 1)
                    {
                        if (!data.ContainsKey(chart.Series[0].Points[i - 1].XValue))
                        {
                            this.chart.Series[seriesName].Points[i].Color = Color.Transparent;
                            continue;
                        }
                    }
                    this.chart.Series[seriesName].Points[i].Color = color;
                }
                catch
                {
                    
                    this.chart.Series[seriesName].Points.AddXY(
                        chart.Series[0].Points[i].XValue, 
                        chart.Series[0].Points[i].YValues[0]);
                    this.chart.Series[seriesName].Points[i].Color = Color.Transparent;
                }
            }
        }
    }
}
