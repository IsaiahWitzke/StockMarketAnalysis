using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace StockMarketAnalysis
{
    class SideMenu
    {
        //ChartHandler chartHandler = aMainForm.
        public FlowLayoutPanel flowLayoutPanel;

        List<Button> buttons = new List<Button>();
        List<Label> movingLabels = new List<Label>();   // moving meaning the moving of the price

        public SideMenu()
        {
            //
            // flowLayoutPanel1
            //
            flowLayoutPanel = new FlowLayoutPanel();

            flowLayoutPanel.SuspendLayout();

            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Location = new Point(10, 75);
            flowLayoutPanel.Name = "flowLayoutPanel1";
            flowLayoutPanel.Size = new Size(165, 500);
            flowLayoutPanel.BackColor = Color.LightGray;
            flowLayoutPanel.TabIndex = 9;
            flowLayoutPanel.ResumeLayout(true);
            flowLayoutPanel.PerformLayout();
        }

        public void addStockButton(string ticker)
        {
            //setting up the button
            buttons.Add(new Button());

            buttons.Last().Location = new Point(3, 3 * buttons.Count());
            buttons.Last().Name = ticker;
            buttons.Last().Size = new Size(75, 23);
            buttons.Last().TabIndex = 0;
            buttons.Last().Text = ticker;
            buttons.Last().UseVisualStyleBackColor = true;

            flowLayoutPanel.Controls.Add(buttons.Last());

            //the event when the button is pressed
            buttons.Last().Click += new EventHandler(SidebarButton_Clicked);

            //the bit of data of the last stock to the side of the 
            movingLabels.Add(new Label());

            //geting the last day's moving data
            //string lastDayMove = lastDayData(ticker).ToString();
            ////colors:
            //if (Convert.ToDouble(lastDayMove) < 0)
            //{ movingLabels.Last().ForeColor = Color.Red; }
            //else
            //{
            //    movingLabels.Last().ForeColor = Color.Green;
            //    lastDayMove = "+" + lastDayMove;
            //}

            movingLabels.Last().Location = new Point(50, 3 * buttons.Count());
            movingLabels.Last().Name = "data";
            movingLabels.Last().Size = new Size(75, 20);
            movingLabels.Last().TabIndex = 0;
            //movingLabels.Last().Text = lastDayMove;

            flowLayoutPanel.Controls.Add(movingLabels.Last());
        }

        //when a button to change the stock is actually pressed
        private void SidebarButton_Clicked(object sender, EventArgs e)
        {
            string[] senderText = sender.ToString().Split(' ');
            string ticker = senderText.Last();

            ChartHandler.loadStock(ticker);
        }

        public void scanForStockData(string rawDataPath)
        {
            try
            {
                //goes through all the files in the raw data directory, gets the names of the files, and turns them into the buttons
                foreach (string path in Directory.GetFiles(rawDataPath))
                {
                    string ticker = path.Replace(rawDataPath+"\\", "");
                    ticker = ticker.Replace(".csv", "");
                    //prevents duplicate buttons
                    bool isDuplicate = false;
                    foreach (Button iButton in buttons) 
                    {
                        if (iButton.Text == ticker) { isDuplicate = true; }
                    }
                    if (isDuplicate) { continue; }

                    addStockButton(ticker);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Couldn't find any raw data file w/ path: " + rawDataPath + e.ToString() 
                    + " (Or some error of the programmer in the SideMenu.cs class)");
                return;
            }
        }

        private double lastDayData(string symbol)
        {
            //get stock market data through alpha vantage
            string rawDataPath = "C:/Users/Public/Documents/RawData/";

            //reading the output file:
            using (var reader = new StreamReader(rawDataPath + symbol + ".csv"))
            {
                string lastLine = "";
                while (!reader.EndOfStream)
                {
                    lastLine = reader.ReadLine();
                }

                var values = lastLine.Split(',');
                double open = Convert.ToDouble(values[1]);
                double close = Convert.ToDouble(values[4]);

                reader.Close();

                return Math.Round(open - close, 4);
            }
        }
    }
}