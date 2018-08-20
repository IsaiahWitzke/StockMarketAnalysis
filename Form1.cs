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
using System.Diagnostics;

namespace StockMarketAnalysis
{
    public partial class aMainForm : Form
    {
        ChartHandler chartHandler = new ChartHandler();

        SideMenu sideMenu = new SideMenu();

        LinePlotter linePlotter;
        GraphicsProcessor graphicsProcessor; //all things to do with zooming in/out
        ChartZoom chartZoom;
        
        //when an event happens here, the chartZoom's methods are called
        protected override void OnMouseWheel(MouseEventArgs mouseEvent) { chartZoom.xAxisZoom(mouseEvent); }
        private void aYAxisZoomIn_Click(object sender, EventArgs e) { chartZoom.yAxisZoomIn(); }
        private void aYAxisZoomOut_Click(object sender, EventArgs e) { chartZoom.yAxisZoomOut(); }
        private void aMainChart_MouseClick(object sender, MouseEventArgs e) { linePlotter.draw(e); }
        private void aGetHistoricalData_Click(object sender, EventArgs e)
        {
            ChartHandler.loadStock(aTickerTextInput.Text);
            linePlotter.updateGaps();
            sideMenu.scanForStockData("C:/Users/Public/Documents/RawData");
        } //for loading different stocks

        public aMainForm()
        {
            //setting up files in the public documents if there are no files already there:
            //raw data
            if (!File.Exists(@"C:\Users\Public\Documents\RawData"))
            {
                Directory.CreateDirectory(@"C:\Users\Public\Documents\RawData");
            }
            //annotations
            if (!File.Exists(@"C:\Users\Public\Documents\SavedAnnotations"))
            {
                Directory.CreateDirectory(@"C:\Users\Public\Documents\SavedAnnotations");
            }
            //strategies
            if (!File.Exists(@"C:\Users\Public\Documents\Strategies"))
            {
                Directory.CreateDirectory(@"C:\Users\Public\Documents\Strategies");
            }

            InitializeComponent();

            linePlotter = new LinePlotter(ChartHandler.chart);
            graphicsProcessor = new GraphicsProcessor(ChartHandler.chart, linePlotter);
            chartZoom = new ChartZoom(ChartHandler.chart);

            //chart events
     
            aChartPanel.Controls.Add(ChartHandler.chart);
            ChartHandler.chart.MouseClick += new MouseEventHandler(this.aMainChart_MouseClick);
            ChartHandler.chart.Paint += new PaintEventHandler(graphicsProcessor.paint);
            ChartHandler.chart.MouseMove += new MouseEventHandler(graphicsProcessor.mouseMove);
            ChartHandler.chart.MouseLeave += new EventHandler(graphicsProcessor.exit);
            ChartHandler.chart.MouseEnter += new EventHandler(graphicsProcessor.enter);

            //sidebar
            Controls.Add(sideMenu.flowLayoutPanel);
            sideMenu.scanForStockData("C:/Users/Public/Documents/RawData");


            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //for debugging (so we don't have to click button every time)
            ChartHandler.loadStock("MSFT");
            linePlotter.updateGaps();
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
            AnnotationFiles.saveAnnotatedGraph();
        }

        // opening some annotations
        private void openAnnotatedGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnnotationFiles.openAnnotatedGraph();
        }

        //to create a new strategy
        private void newStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "C# Files (*.cs)|*.cs";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\Strategies";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(dialog.OpenFile()))
                    {
                        string className = dialog.FileName.Split('\\').Last();
                        //some auto generated code
                        sw.WriteLine("namespace StockMarketAnalysis\n" +
                            "{\n" +
                            "    class " + className + "\n" +
                            "    {\n" +
                            "    }\n" + 
                            "}");


                        sw.Flush();
                        sw.Close();
                    }

                    //then open the file in vs code (preferably)
                    try
                    {
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("code", "-g " + dialog.FileName);
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                        //if the user doesnt have vs code, then prompt them to download it, then open it with a default program
                        if (MessageBox.Show("Visual Studio Code is recommened, download?", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start("https://code.visualstudio.com/download");
                        }
                        Process.Start(dialog.FileName);
                    }
                }
            }
        }

        //opens existing strategy in visual studio
        private void openStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "C# files (*.cs)|*.cs";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\Strategies";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("code", "-g " + dialog.FileName);
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                        //if the user doesnt have vs code, then prompt them to download it, then open it with a default program
                        if(MessageBox.Show("Visual Studio Code is recommened, download?", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start("https://code.visualstudio.com/download");
                        }
                        Process.Start(dialog.FileName);
                    }
                    
                }
            }
        }
    }
}