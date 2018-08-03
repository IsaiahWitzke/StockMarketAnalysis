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

        static Chart aMainChart;
        ChartArea aMainChartArea;
        Series aMainSeries;

        bool drawing;

        Plot userDrawn;
        Plot highPlot;
        Plot lowPlot;


        #region Chart's Zooming

        ChartZoom chartZoom;

        protected override void OnMouseWheel(MouseEventArgs mouseEvent) { chartZoom.xAxisZoom(mouseEvent); }
        private void aYAxisZoomIn_Click(object sender, EventArgs e) { chartZoom.yAxisZoomIn(); }
        private void aYAxisZoomOut_Click(object sender, EventArgs e) { chartZoom.yAxisZoomOut(); }

        #endregion

        public aMainForm()
        {
            InitializeComponent();
            
            aMainChartArea = new ChartArea();
            aMainSeries = new Series();
            createChart();

            drawing = false;
            userDrawn = new Plot("userDrawn", aMainChart, Color.Violet);
            highPlot = new Plot("highs", aMainChart, Color.ForestGreen);
            lowPlot = new Plot("lows", aMainChart, Color.ForestGreen);
            chartZoom = new ChartZoom(aMainChart);

            //aMainChart.Series.Add("userDrawn");
            //aMainChart.Series["userDrawn"].ChartType = SeriesChartType.Line;   // all plots using this constructed will be a line
            //aMainChart.Series["userDrawn"].IsXValueIndexed = true;

            //for debugging (so we don't have to click button every time)
            loadStock("MSFT");
        }

        //gets stock market data through alpha vantage
        /// <param name = "symbol" > ticker symbol of desired stock (ex. TSLA)</param>
        /// <param name = "rawDataPath" > path for data to be stored</param>
        private void getData(string symbol, string rawDataPath)
        {
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
        }

        private void createChart()
        {
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
            aMainChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.aMainChart_MouseClick); //catch mouse clicks on the graph


            this.aMainSeries.ChartArea = "aMainChartArea";
            this.aMainSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            this.aMainSeries.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.aMainSeries.IsXValueIndexed = true;    // this seems to be very important. (removes weekends)
            aMainChart.ChartAreas[0].AxisX.IsReversed = true;   // when the weekends are removed the chart seems to be revesed, this line fixes it
            this.aMainSeries.Name = "aCandleSticks";
            this.aMainSeries.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.aMainSeries.YValuesPerPoint = 4;

            this.Controls.Add(aMainChart);
        }

        private void loadStock(string symbol)
        {
            //get stock market data through alpha vantage
            string rawDataPath = "../../RawData/";
            getData(symbol, rawDataPath);

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
                    DateTime x = new DateTime(dateInfo[0], dateInfo[1], dateInfo[2]);

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




        #region Event Handlers
        private void button1_Click(object sender, EventArgs e)
        {
            string symbol = textBox1.Text;
            loadStock(symbol);
        }



        //testing the plot class
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < aMainChart.Series[0].Points.Count(); i++)
            {
                    highPlot.data.Add(aMainChart.Series[0].Points[i].XValue, aMainChart.Series[0].Points[i].YValues[0]);
            }
            highPlot.updatePlot();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < aMainChart.Series[0].Points.Count(); i++)
            {
                if(i == 3 || i == 4)
                lowPlot.data.Add(aMainChart.Series[0].Points[i].XValue, aMainChart.Series[0].Points[i].YValues[1]);
                Console.WriteLine(" x:" + aMainChart.Series[0].Points[i].XValue + " y " + aMainChart.Series[0].Points[i].YValues[1]);
            }

            lowPlot.updatePlot();
        }

        //draw on chart
        private void aMainChart_MouseClick(object sender, MouseEventArgs e)
        {   
            if(drawing)
            {
                //store location
                var pos = e.Location;

                var x = aMainChart.ChartAreas[0].AxisX.PixelPositionToValue(pos.X); //x value is number of bars  counting from the right of the graph
                var y = aMainChart.ChartAreas[0].AxisY.PixelPositionToValue(pos.Y); //y value translates on to graph directly (no changes necessary)

                //get closes data points
                var upper = Convert.ToInt32(Math.Ceiling(x));
                //var lower = Convert.ToInt32(Math.Floor(x));

                double upperValue = aMainChart.Series[0].Points[upper].XValue;
               /// double lowerValue = aMainChart.Series[0].Points[lower].XValue;


                Console.WriteLine(" x:" + x + " upper " + upperValue + " lower " + " " + y);

                userDrawn.data.Add(upperValue, y);
                userDrawn.updatePlot();

                //aMainChart.Series["userDrawn"].Points.AddY(y);

            }
        }

        //going in and out of drawing mode
        private void draw_Click(object sender, EventArgs e)
        {
            drawing = !drawing;

            if (drawing)
            {
                drawMode.Text = "Drawing";
            }
            else
            {
                drawMode.Text = "Draw";
            }
        }

        #endregion
    }
}