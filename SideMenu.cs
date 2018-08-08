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
        public FlowLayoutPanel flowLayoutPanel;

        List<Button> buttons = new List<Button>();

        public SideMenu ()
        {
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel = new FlowLayoutPanel();

            this.flowLayoutPanel.SuspendLayout();

            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(10, 50);
            this.flowLayoutPanel.Name = "flowLayoutPanel1";
            this.flowLayoutPanel.Size = new System.Drawing.Size(175, 500);
            this.flowLayoutPanel.BackColor = Color.LightGray;
            this.flowLayoutPanel.TabIndex = 9;
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();

        }

        public void addStock(string ticker)
        {
            Button newButton = new Button();
            this.buttons.Add(newButton);

            this.buttons.Last().Location = new System.Drawing.Point(3, (3 * (buttons.Count() + 1) ));
            this.buttons.Last().Name = ticker;
            this.buttons.Last().Size = new System.Drawing.Size(75, 23);
            this.buttons.Last().TabIndex = 0;
            this.buttons.Last().Text = ticker;
            this.buttons.Last().UseVisualStyleBackColor = true;

            this.flowLayoutPanel.Controls.Add(buttons.Last());

            //this.buttons.Last().Click += new System.EventHandler(this.buttons)
        }

        public void scanForStockData(string rawDataPath)
        {
            try
            {
                foreach (string path in Directory.GetFiles(rawDataPath))
                {
                    string ticker = path.Replace(rawDataPath, "");
                    addStock(ticker);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't find any raw data file w/ path: " + rawDataPath);
                return;
            }
            

        }
    }
}
