using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace StockMarketAnalysis
{
    // this class is for ease of use, and allows a user to graph some new data onto the chart without woring about the series and chart areas...
    class Plot
    {
        private Dictionary<double, double> data = new Dictionary<double, double>();  // "key" is the x values and the "values" as the y values
        Chart chart; // will hold the main static chart obj.
        private string seriesName;
        public bool plotted = false;
        public bool noData = true;

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

        //adds a point to the dictionary
        public void addPoint(double x, double y)
        {
            noData = false;
            data.Add(x, y);
        }

        // goes through all the data and plots the dictionary as if the "key" is the x values and the "values" as the y values
        public void showPlot()
        {
            plotted = true;

            //now going through the main x axis. If there is no data point for this plot at that point, 
            //then we will make it the candle's high (as to not mess with the chart's y axis' zoom) and transparent,
            //otherwise, plot it as intended
            for (int i = 0; i < chart.Series[0].Points.Count(); i++)
            {
                if(data.ContainsKey(chart.Series[0].Points[i].XValue))
                {
                    chart.Series[seriesName].Points.AddXY(
                        chart.Series[0].Points[i].XValue,
                        data[chart.Series[0].Points[i].XValue]);

                    //this bit needs to be here or else an extra point is rendered to the screen
                    if (i > 1)
                    {
                        if (!data.ContainsKey(chart.Series[0].Points[i - 1].XValue))
                        {
                            chart.Series[seriesName].Points[i].Color = Color.Transparent;
                            continue;
                        }
                    }
                    chart.Series[seriesName].Points[i].Color = color;
                }
                else
                {
                    chart.Series[seriesName].Points.AddXY(
                        chart.Series[0].Points[i].XValue,
                        chart.Series[0].Points[i].YValues[0]);
                    chart.Series[seriesName].Points[i].Color = Color.Transparent;
                }
            }
        }
        

        public void toggleDisplay()
        {
            if(plotted)
            {
                hidePlot();
            }
            else
            {
                showPlot();
            }
        }

        public void hidePlot()
        {
            plotted = false;
            chart.Series[seriesName].Points.Clear();
        }

        public void deletePlot()
        {
            chart.Series.Remove(chart.Series[seriesName]);
        }

        public void savePlotToFile(string path, string fileName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(path + "/" + fileName);

                //goes through all of data, and outputs it to the file 
                string lineToSave = "Plot " + seriesName;
                foreach (var dataPoint in data)
                {
                    lineToSave += dataPoint.Key.ToString();
                    lineToSave += " ";
                    lineToSave += dataPoint.Value.ToString();
                    lineToSave += " ";
                }
                sw.WriteLine(lineToSave);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
