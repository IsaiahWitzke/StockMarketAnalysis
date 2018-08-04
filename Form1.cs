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

        LinePlotter linePlotter;

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

            //userDrawn = new Plot("userDrawn", ChartHandler.chart, Color.Violet);
            highPlot = new Plot("highs", ChartHandler.chart, Color.ForestGreen);
            lowPlot = new Plot("lows", ChartHandler.chart, Color.ForestGreen);
            chartZoom = new ChartZoom(ChartHandler.chart);
            this.Controls.Add(ChartHandler.chart);
            ChartHandler.chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick);

            //chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick); //catch mouse clicks on the graph

            linePlotter = new LinePlotter(ChartHandler.chart);

            //for debugging (so we don't have to click button every time)
            chartHandler.loadStock("MSFT");

        }

        

        //for loadind different symbols
        private void button1_Click(object sender, EventArgs e) { chartHandler.loadStock(textBox1.Text); }

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
                lowPlot.data.Add(ChartHandler.chart.Series[0].Points[i].XValue, ChartHandler.chart.Series[0].Points[i].YValues[1]);
            }
            lowPlot.updatePlot();
        }

        //draw on chart
        private void aMainChart_MouseClick(object sender, MouseEventArgs e) { linePlotter.draw(linePlotter.isDrawing, e); }

        //going in and out of drawing mode
        private void draw_Click(object sender, EventArgs e)
        {
            linePlotter.isDrawing = !linePlotter.isDrawing;

            if (linePlotter.isDrawing)
            {
                drawMode.Text = "Drawing";
            }
            else
            {
                drawMode.Text = "Draw";
            }
        }
    }
}