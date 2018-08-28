using StockMarketAnalysis;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace Strategy
{
    class SimpleMovingAverageExample
    {
        //this function will excecuted when the "Execute Strategy" button is pressed 
        public static void Main()
        {
            //its a good idea to update the data so that the Strategies.Data is reflecting what is being shown on the graph
            Strategies.updateData();

            Plot SMA = new Plot("sma", Color.Red());
            
            List<double> averages = new List<double>();

            //just obtaining the daily averages
            foreach (var date in Strategies.Data.dates)
            {
                double average = Strategies.Data.highs[date];
                average += Strategies.Data.lows[date];
                average += Strategies.Data.closes[date];
                average += Strategies.Data.opens[date];

                average /= 4;

                averages.Add(average);
            }
            
            int periodSMA = 5;  // how many points back for average

            int counter = 0;

            foreach (var date in Strategies.Data.dates)
            {
                //if we havent passed "periodSMA" in the loop, then the plot will just be the average of that day
                if(counter < periodSMA)
                {
                    SMA.addPoint(date, averages[counter]);
                }
                else
                {                
                    //otherwise we want to find the average of the last "periodSMA" number of days and add that to the plot
                    double todaysPoint = 0;
                    for (int i = 0; i < periodSMA; i++)
                    {
                        todaysPoint += averages[counter - i];
                    }
                    
                    todaysPoint /= periodSMA;

                    SMA.addPoint(date, todaysPoint);
                }

                counter++;
            }

            SMA.showPlot(); // draws the plot to the graph
        
        }
    }
}
