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


namespace StockMarketAnalysis
{
    public partial class aMainForm : Form
    {
        ChartHandler chartHandler = new ChartHandler();


        bool drawing;

        Plot userDrawn;
        Plot highPlot;
        Plot lowPlot;
        
        //all things to do with zooming in/out
        ChartZoom chartZoom;

        //when an event happens here, the chartZoom's methods are called
        protected override void OnMouseWheel(MouseEventArgs mouseEvent) { chartZoom.xAxisZoom(mouseEvent); }
        private void aYAxisZoomIn_Click(object sender, EventArgs e) { chartZoom.yAxisZoomIn(); }
        private void aYAxisZoomOut_Click(object sender, EventArgs e) { chartZoom.yAxisZoomOut(); }

        public aMainForm()
        {
            InitializeComponent();

            drawing = false;
            userDrawn = new Plot("userDrawn", ChartHandler.chart, Color.Violet);
            highPlot = new Plot("highs", ChartHandler.chart, Color.ForestGreen);
            lowPlot = new Plot("lows", ChartHandler.chart, Color.ForestGreen);
            chartZoom = new ChartZoom(ChartHandler.chart);
            this.Controls.Add(ChartHandler.chart);

            //chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick); //catch mouse clicks on the graph

            //aMainChart.Series.Add("userDrawn");
            //aMainChart.Series["userDrawn"].ChartType = SeriesChartType.Line;   // all plots using this constructed will be a line
            //aMainChart.Series["userDrawn"].IsXValueIndexed = true;

            //for debugging (so we don't have to click button every time)
            chartHandler.loadStock("MSFT");

        }

        

        #region Event Handlers
        private void button1_Click(object sender, EventArgs e)
        {
            string symbol = textBox1.Text;
            chartHandler.loadStock(symbol);
        }



        //testing the plot class
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChartHandler.chart.Series[0].Points.Count(); i++)
            {
                    highPlot.data.Add(ChartHandler.chart.Series[0].Points[i].XValue, ChartHandler.chart.Series[0].Points[i].YValues[0]);
            }
            highPlot.updatePlot();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChartHandler.chart.Series[0].Points.Count(); i++)
            {
                if(i == 3 || i == 4)
                lowPlot.data.Add(ChartHandler.chart.Series[0].Points[i].XValue, ChartHandler.chart.Series[0].Points[i].YValues[1]);
                Console.WriteLine(" x:" + ChartHandler.chart.Series[0].Points[i].XValue + " y " + ChartHandler.chart.Series[0].Points[i].YValues[1]);
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

                var x = ChartHandler.chart.ChartAreas[0].AxisX.PixelPositionToValue(pos.X); //x value is number of bars  counting from the right of the graph
                var y = ChartHandler.chart.ChartAreas[0].AxisY.PixelPositionToValue(pos.Y); //y value translates on to graph directly (no changes necessary)

                //get closes data points
                var upper = Convert.ToInt32(Math.Ceiling(x));
                //var lower = Convert.ToInt32(Math.Floor(x));

                double upperValue = ChartHandler.chart.Series[0].Points[upper].XValue;
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