using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace StockMarketAnalysis
{
    public partial class aMainForm : Form
    {
        public aMainForm()
        {
            InitializeComponent();
        }

        private int zoomSpeed = 10;
        private int zoomMultiple = 0;

        protected override void OnMouseWheel(MouseEventArgs mouseEvent)
        {

            //zooming in
            if (mouseEvent.Delta > 0)
            {
                if (zoomMultiple < 100)
                {
                    zoomMultiple++;
                }
            }

            //zooming out
            if (mouseEvent.Delta < 0)
            {
                if (zoomMultiple > 0)
                {
                    zoomMultiple--;
                }
            }

            //zooming in the x axis
            aMainChart.ChartAreas[0].AxisX.ScaleView.Zoom(
                    aMainChart.ChartAreas[0].AxisX.Minimum + zoomMultiple * zoomSpeed,
                    aMainChart.ChartAreas[0].AxisX.Maximum - zoomMultiple * zoomSpeed);

            //recalculates the y axis
            aMainChart.ChartAreas[0].AxisY.Maximum = Double.NaN;
            aMainChart.ChartAreas[0].AxisY.Minimum = Double.NaN;
            aMainChart.ChartAreas[0].RecalculateAxesScale();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //getting the data from online:
            string symbol = textBox1.Text;
            initChart(symbol);
        }

        static Chart aMainChart;
        ChartArea aMainChartArea = new ChartArea();
        Series aMainSeries = new Series();

        private void initChart(string symbol)
        {
            string rawDataPath = "../../RawData/";
            string strCmdText;
            strCmdText = "/C alpha-vantage-cli -s " + symbol + " -k TPMQDECWM5ATUR1L -o " + rawDataPath + symbol;

            //if the data hasn't already been downloaded, then do the alpha vantage download:
            if (!File.Exists(rawDataPath + symbol))
            {
                //to execute alpah vantage cli commands in the background
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();

                //wait for output file to download
                while (!process.HasExited)
                { }
            }

            //at this point the file should be made, if not, then it was an invalid symbol
            if (!File.Exists(rawDataPath + symbol))
            {
                MessageBox.Show("Couldn't find " + symbol);
                return;
            }

            //make the chart

            this.aMainChartArea.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            this.aMainChartArea.AxisY.IsStartedFromZero = false;
            this.aMainChartArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.aMainChartArea.Name = "aMainChartArea";

            aMainChart = new Chart();
            aMainChart.ChartAreas.Add(aMainChartArea);
            aMainChart.Location = new System.Drawing.Point(97, 101);
            aMainChart.Name = "aMainChart";
            aMainChart.Series.Add(aMainSeries);
            aMainChart.Size = new System.Drawing.Size(1668, 750);
            aMainChart.TabIndex = 2;

            
            this.aMainSeries.ChartArea = "aMainChartArea";
            this.aMainSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            this.aMainSeries.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.aMainSeries.IsXValueIndexed = true;    // this seems to be very important. (removes weekends)
            aMainChart.ChartAreas[0].AxisX.IsReversed = true;   // when the weekends are removed the chart seems to be revesed, this line fixes it
            this.aMainSeries.Name = "aCandleSticks";
            this.aMainSeries.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.aMainSeries.YValuesPerPoint = 4;

            this.Controls.Add(aMainChart);

            //reading the output file:
            using (var reader = new StreamReader(rawDataPath + symbol))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    if (isFirstLine)
                    {
                        reader.ReadLine();
                        isFirstLine = false;
                    }
                    
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //date stuff
                    var stringDateArr = values[0].Split('-');
                    int[] dateInfo = new int[3];
                    dateInfo[0] = Convert.ToInt32(stringDateArr[0]);
                    dateInfo[1] = Convert.ToInt32(stringDateArr[1]);
                    dateInfo[2] = Convert.ToInt32(stringDateArr[2]);

                    aMainChart.Series[0].XValueType = ChartValueType.DateTime;
                    System.DateTime x = new System.DateTime(dateInfo[0], dateInfo[1], dateInfo[2]);
                    //chart1.Series[0].Points.AddXY(x.ToOADate(), 34);

                    //candle stick data
                    double open = Convert.ToDouble(values[1]);
                    double high = Convert.ToDouble(values[2]);
                    double low = Convert.ToDouble(values[3]);
                    double close = Convert.ToDouble(values[4]);
                    double[] data = { high, low, open, close };
                    DataPoint candleStick = new DataPoint(x.ToOADate(), data);
                    aMainChart.Series[0].Points.Add(candleStick);
                }   
            }
        }

        //testing the plot class
        Plot highPlot;
        Plot lowPlot;

        private void button2_Click(object sender, EventArgs e)
        {
            highPlot = new Plot("highs", aMainChart);
            for (int i = 0; i < aMainChart.Series[0].Points.Count(); i++)
            {
                highPlot.data.Add(aMainChart.Series[0].Points[i].XValue, aMainChart.Series[0].Points[i].YValues[0]);
            }

            highPlot.drawPlot();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lowPlot = new Plot("lows", aMainChart);
            for (int i = 0; i < aMainChart.Series[0].Points.Count(); i++)
            {
                lowPlot.data.Add(aMainChart.Series[0].Points[i].XValue, aMainChart.Series[0].Points[i].YValues[1]);
            }

            lowPlot.drawPlot();
        }
    }
}