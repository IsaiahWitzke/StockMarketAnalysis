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
        GraphicsProcessor graphicsProcessor; //all things to do with zooming in/out
        ChartZoom chartZoom;

        Plot highPlot;
        Plot lowPlot;
        
        //when an event happens here, the chartZoom's methods are called
        protected override void OnMouseWheel(MouseEventArgs mouseEvent) { chartZoom.xAxisZoom(mouseEvent); }
        private void aYAxisZoomIn_Click(object sender, EventArgs e) { chartZoom.yAxisZoomIn(); }
        private void aYAxisZoomOut_Click(object sender, EventArgs e) { chartZoom.yAxisZoomOut(); }
        private void aMainChart_MouseClick(object sender, MouseEventArgs e) { linePlotter.draw(e); }
        private void button1_Click(object sender, EventArgs e) { chartHandler.loadStock(textBox1.Text); linePlotter.updateGaps(); } //for loading different symbols

        public aMainForm()
        {
            InitializeComponent();

            highPlot = new Plot("highs", ChartHandler.chart, Color.ForestGreen);
            lowPlot = new Plot("lows", ChartHandler.chart, Color.ForestGreen);
            linePlotter = new LinePlotter(ChartHandler.chart);
            graphicsProcessor = new GraphicsProcessor(ChartHandler.chart);
            chartZoom = new ChartZoom(ChartHandler.chart);

            //chart events
            this.Controls.Add(ChartHandler.chart);
            ChartHandler.chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick);
            ChartHandler.chart.Paint += new PaintEventHandler(graphicsProcessor.paint);
            ChartHandler.chart.MouseMove += new MouseEventHandler(graphicsProcessor.mouseMove);
            ChartHandler.chart.MouseLeave += new EventHandler(graphicsProcessor.exit);
            ChartHandler.chart.MouseEnter += new EventHandler(graphicsProcessor.enter);


            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //for debugging (so we don't have to click button every time)
            chartHandler.loadStock("MSFT");
            linePlotter.updateGaps();
        }



        //testing the plot class
        private void button2_Click(object sender, EventArgs e)
        {
            if (highPlot.noData)
            {
                for (int i = 0; i < ChartHandler.chart.Series[0].Points.Count(); i++)
                {
                    highPlot.addPoint(ChartHandler.chart.Series[0].Points[i].XValue, ChartHandler.chart.Series[0].Points[i].YValues[0]);
                }
            }
            highPlot.toggleDisplay();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(lowPlot.noData)
            { 
                for (int i = 0; i < ChartHandler.chart.Series[0].Points.Count(); i++)
                {
                    lowPlot.addPoint(ChartHandler.chart.Series[0].Points[i].XValue, ChartHandler.chart.Series[0].Points[i].YValues[1]);
                }
            }
            lowPlot.toggleDisplay();
        }

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