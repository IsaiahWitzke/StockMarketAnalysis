using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketAnalysis
{
    public class Strategies
    {
        public struct Data
        {
            public static List<double> dates = new List<double>();
            public static Dictionary<double, double> highs = new Dictionary<double, double>();
            public static Dictionary<double, double> lows = new Dictionary<double, double>();
            public static Dictionary<double, double> opens = new Dictionary<double, double>();
            public static Dictionary<double, double> closes = new Dictionary<double, double>();
        }

        public static void updateData()
        {
            Data.highs = new Dictionary<double, double>();
            Data.lows = new Dictionary<double, double>();
            Data.opens = new Dictionary<double, double>();
            Data.closes = new Dictionary<double, double>();
            Data.dates = new List<double>();
            foreach (var dataPoint in ChartHandler.mainSeries.Points)
            {
                Data.highs.Add(dataPoint.XValue, dataPoint.YValues[0]);
                Data.lows.Add(dataPoint.XValue, dataPoint.YValues[1]);
                Data.opens.Add(dataPoint.XValue, dataPoint.YValues[2]);
                Data.closes.Add(dataPoint.XValue, dataPoint.YValues[3]);
                Data.dates.Add(dataPoint.XValue);
            }
        }
        

        //some functions here for ease of use
        //public Plot SimpleMovingAverage(Dictionary<double, double> data, int period)
        //{

        //}
    }
}
