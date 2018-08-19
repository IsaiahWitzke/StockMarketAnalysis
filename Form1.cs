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
        ChartHandler chartHandler = new ChartHandler();

        SideMenu sideMenu = new SideMenu();

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
        private void button1_Click(object sender, EventArgs e)
        {
            ChartHandler.loadStock(textBox1.Text);
            linePlotter.updateGaps();
            sideMenu.scanForStockData("../../RawData/");
        } //for loading different stocks

        public aMainForm()
        {
            InitializeComponent();

            highPlot = new Plot("highs", ChartHandler.chart, Color.ForestGreen);
            lowPlot = new Plot("lows", ChartHandler.chart, Color.ForestGreen);
            linePlotter = new LinePlotter(ChartHandler.chart);
            graphicsProcessor = new GraphicsProcessor(ChartHandler.chart, linePlotter);
            chartZoom = new ChartZoom(ChartHandler.chart);

            //chart events
            this.Controls.Add(ChartHandler.chart);
            ChartHandler.chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick);
            ChartHandler.chart.Paint += new PaintEventHandler(graphicsProcessor.paint);
            ChartHandler.chart.MouseMove += new MouseEventHandler(graphicsProcessor.mouseMove);
            ChartHandler.chart.MouseLeave += new EventHandler(graphicsProcessor.exit);
            ChartHandler.chart.MouseEnter += new EventHandler(graphicsProcessor.enter);

            //sidebar
            Controls.Add(sideMenu.flowLayoutPanel);
            sideMenu.scanForStockData("../../RawData/");


            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //for debugging (so we don't have to click button every time)
            ChartHandler.loadStock("MSFT");
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
                linePlotter.haveFirstPoint = false;
                drawMode.Text = "Draw";
            }
        }

        // saving the annotations
        private void saveAnnotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "annotation files (*.an)|*.an";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = Path.GetFullPath("../../SavedAnnotations");
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        stream.Flush();
                        
                        StreamWriter sw = new StreamWriter(stream);
                        sw.WriteLine(ChartHandler.ticker);
                        linePlotter.savePlotsToFile(sw);    // saving the plot data

                        sw.Close();
                        stream.Close();

                    }
                }
            }
        }

        private void openAnnotatedGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "../../SavedAnnotations";
            string fileName = null;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Path.GetFullPath(path);
                dialog.Filter = "annotation files (*.an)|*.an";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                }
            }

            if (fileName != null)
            {
                //here is where we read the file data:
                using (var reader = new StreamReader(fileName))
                {
                    bool isFirstLine = true;
                    while (!reader.EndOfStream)
                    {
                        if (isFirstLine)
                        {
                            //first line is the ticker of the graph annotated
                            string line = reader.ReadLine();
                            ChartHandler.loadStock(line);

                            isFirstLine = false;
                        }
                    }
                }
            }
        }
    }
}