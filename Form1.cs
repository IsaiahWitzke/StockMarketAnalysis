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

        private void aMainChart_MouseClick(object sender, MouseEventArgs e)
        {   
            //store location
            var pos = e.Location;
            var results = aMainChart.HitTest(pos.X, pos.Y, false, ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var x = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    var y = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                    Console.WriteLine("x:" + x + " y:" + y);
                }
            }
        }

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
                aMainChart.ChartAreas[0].AxisX.IsReversed = true;
            }
        }
    }
}
