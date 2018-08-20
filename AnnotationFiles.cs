using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    //class to deal with saving/opening annotation files
    class AnnotationFiles
    {
        public static void saveAnnotatedGraph()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "annotation files (*.an)|*.an";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\SavedAnnotations";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        stream.Flush();

                        StreamWriter sw = new StreamWriter(stream);
                        sw.WriteLine(ChartHandler.ticker);
                        LinePlotter.savePlotsToFile(sw);    // saving the plot data

                        sw.Close();
                        stream.Close();

                    }
                }
            }
        }

        public static void openAnnotatedGraph()
        {

            string path = @"C:\Users\Public\Documents\SavedAnnotations";
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
                            string firstLine = reader.ReadLine();
                            ChartHandler.loadStock(firstLine);

                            isFirstLine = false;
                        }

                        //read the next line, the first word of that next line is the kind of annotation that the line is
                        string line = reader.ReadLine();
                        string[] splitLine = line.Split(' ');

                        switch (splitLine[0])
                        {
                            case "Plot":
                                //for a plot line, the next 4 elements in "splitLine" are the 2 start/end points of the line
                                double startX = Convert.ToDouble(splitLine[1]);
                                double startY = Convert.ToDouble(splitLine[2]);
                                double endX = Convert.ToDouble(splitLine[3]);
                                double endY = Convert.ToDouble(splitLine[4]);
                                LinePlotter.addLine(startX, startY, endX, endY);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
