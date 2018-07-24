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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnMouseWheel(MouseEventArgs mouseEvent)
        {
            //zooming in
            if (mouseEvent.Delta > 0)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, 1);
            }

            //zooming out
            if (mouseEvent.Delta < 0)
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //getting the data from online:
            string symbol = textBox1.Text;
            string rawDataPath = "C:/Users/witzk/source/repos/StockMarketAnalysis/StockMarketAnalysis/RawData/";
            string strCmdText;
            strCmdText = "/C alpha-vantage-cli -s " + symbol + " -k TPMQDECWM5ATUR1L -o " + rawDataPath + symbol;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);

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

                    chart1.Series[0].XValueType = ChartValueType.DateTime;
                    System.DateTime x = new System.DateTime(dateInfo[0], dateInfo[1], dateInfo[2]);
                    //chart1.Series[0].Points.AddXY(x.ToOADate(), 34);

                    //candle stick data
                    double open = Convert.ToDouble(values[1]);
                    double high = Convert.ToDouble(values[2]);
                    double low = Convert.ToDouble(values[3]);
                    double close = Convert.ToDouble(values[4]);
                    double[] data = { open, high, low, close };
                    DataPoint candleStick = new DataPoint(x.ToOADate(), data);
                    chart1.Series["Series1"].Points.Add(candleStick);
                }
            }            
        }

    }
}
