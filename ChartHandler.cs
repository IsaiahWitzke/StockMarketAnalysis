using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace StockMarketAnalysis
{
    public class ChartHandler
    {
        // the chart stuff that the user sees
        public static Chart chart = new Chart();
        ChartArea chartArea, annotationArea;
        public static Series mainSeries;
        public static string ticker = "";

        public ChartHandler()
        {
            chartArea = new ChartArea();
            annotationArea = new ChartArea();
            mainSeries = new Series();

            //make the chart
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartArea.AxisY.IsStartedFromZero = false;
            chartArea.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea.Position = new ElementPosition(0, 0, 100, 100);
            chartArea.AxisY.LabelStyle.Format = "0.00";
            chartArea.Name = "aMainChartArea";


            //set up annotation area
            annotationArea.AxisX.Minimum = 0;
            annotationArea.AxisX.Maximum = 100;
            annotationArea.AxisY.Minimum = 0;
            annotationArea.AxisY.Maximum = 100;
            annotationArea.BackColor = System.Drawing.Color.Transparent;
            annotationArea.Position = chartArea.Position;
            annotationArea.Name = "annotationArea";

            chart = new Chart();
            chart.ChartAreas.Add(chartArea);
            chart.ChartAreas.Add(annotationArea);
            chart.Name = "aMainChart";
            chart.Series.Add(mainSeries);
            chart.Dock = DockStyle.Fill;
            chart.TabIndex = 2;

            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
            chart.ChartAreas[1].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[1].AxisY.ScaleView.Zoomable = true;

            mainSeries.ChartArea = "aMainChartArea";
            mainSeries.ChartType = SeriesChartType.Candlestick;
            mainSeries.Color = System.Drawing.Color.FromArgb(0, 0, 64);
            mainSeries.IsXValueIndexed = true;    // this seems to be very important. (removes weekends)
            chart.ChartAreas[0].AxisX.IsReversed = true;   // when the weekends are removed the chart seems to be revesed, this line fixes it
            mainSeries.Name = "aCandleSticks";
            mainSeries.XValueType = ChartValueType.Date;
            mainSeries.YValuesPerPoint = 4;
        }

        /// <param name = "symbol" > ticker symbol of desired stock (ex. TSLA)</param>
        /// <param name = "rawDataPath" > path for data to be stored</param>
        //private static bool getData(string symbol, string rawDataPath)
        //{
        //    string strCmdText;
        //    strCmdText = "/C alpha-vantage-cli -s " + symbol + " -k TPMQDECWM5ATUR1L -o " + rawDataPath + symbol;

        //    //if the data hasn't already been downloaded, then do the alpha vantage download:
        //    if (!File.Exists(rawDataPath + symbol))
        //    {
        //        //to execute alpah vantage cli commands in the background
        //        System.Diagnostics.Process process = new System.Diagnostics.Process();
        //        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        //        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //        startInfo.FileName = "cmd.exe";
        //        startInfo.Arguments = strCmdText;
        //        process.StartInfo = startInfo;
        //        process.Start();

        //        //wait for output file to download
        //        while (!process.HasExited)
        //        { }
        //    }

        //    //at this point the file should be made, if not, then it was an invalid symbol
        //    if (!File.Exists(rawDataPath + symbol))
        //    {
        //        MessageBox.Show("Couldn't find " + symbol);
        //        return false;
        //    }

        //    return true;
        //}
        // api key: xG3hqCgeWDpLIVXE5GsP8VrUUE2DZtfFgczxuDqwSsV8OcPqeeLgxiOh5iYU
        private static async Task<bool> getDataAsync(string symbol, string rawDataPath)
        {
            // Since worldtradingdata api downloads are "expensive", we want to check if we already have the data
            // if the data hasn't already been downloaded, then do the download
            string path = rawDataPath + symbol + ".csv";
            if (File.Exists(path)) { return true; }
            string apiKey = "xG3hqCgeWDpLIVXE5GsP8VrUUE2DZtfFgczxuDqwSsV8OcPqeeLgxiOh5iYU";
            string requestUri = "https://api.worldtradingdata.com/api/v1/history?symbol="
                + symbol + "&sort=newest&api_token=" + apiKey;
            var client = new HttpClient();
            string retData = await client.GetStringAsync(requestUri);
            // JObject retDataJSON = JObject.Parse( await client.GetStringAsync(requestUri) );
            if(retData == "{\"Message\":\"Error! The requested stock(s) could not be found.\"}")
            {
                MessageBox.Show(retData + "Couldn't find " + symbol);
                return false;
            }
            File.WriteAllText(path, retData);
            return true;
        }

        public static async void loadStock(string symbol)
        {
            //MessageBox.Show(symbol);
            ticker = symbol;
            //get stock market data through worldtradingdata api
            string rawDataPath = @"C:\Users\Public\Documents\RawData\";
            if (!await getDataAsync(symbol, rawDataPath))
            { return; }

            //get rid of previous data
            chart.Series[0].Points.Clear();
            foreach (var series in chart.Series)
            { series.Points.Clear(); }

            JObject jsonData = JObject.Parse(File.ReadAllText(rawDataPath + symbol + ".csv"));

            foreach (JProperty intraday in jsonData["history"])
            {
                DateTime date = DateTime.Parse(intraday.Name);
                double[] data = {
                    (double)intraday.Value["high"],
                    (double)intraday.Value["low"],
                    (double)intraday.Value["open"],
                    (double)intraday.Value["close"] };
                DataPoint candleStick = new DataPoint(date.ToOADate(), data);
                chart.Series[0].XValueType = ChartValueType.DateTime;
                chart.Series[0].Points.Add(candleStick);
            }

            return;

            //reading the output file:
            using (var reader = new StreamReader(rawDataPath + symbol + ".csv"))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    //to get rid of the first line of gaff that alpha-vantage gives
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

                    chart.Series[0].XValueType = ChartValueType.DateTime;
                    DateTime x = new DateTime(dateInfo[0], dateInfo[1], dateInfo[2]);

                    //candle stick data
                    double open = Convert.ToDouble(values[1]);
                    double high = Convert.ToDouble(values[2]);
                    double low = Convert.ToDouble(values[3]);
                    double close = Convert.ToDouble(values[4]);
                    double[] data = { high, low, open, close };
                    DataPoint candleStick = new DataPoint(x.ToOADate(), data);
                    chart.Series[0].Points.Add(candleStick);
                }
            }

            //so that the user can use the actual data that is being shown on the graph in the strategies scriping
            Strategies.updateData();
        }
    }
}
