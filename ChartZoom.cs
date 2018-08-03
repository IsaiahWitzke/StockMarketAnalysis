using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    class ChartZoom
    {
        //chart zoom variables
        private double xAxisZoomSpeed = 0.1;
        private int xAxisZoomMultiple = 0;

        private int yAxisZoomSpeed = 2;
        private int yAxisZoomMultiple = 0;

        Chart chart;

        public ChartZoom(Chart chart)
        {
            this.chart = chart;
        }

        public void yAxisZoomIn()
        {
            //this is here to prevent breaking the program before the chart is initialized
            if (chart == null)
                return;

            //zooming in
            if (chart.ChartAreas[0].AxisY.Minimum + (yAxisZoomMultiple + 1) * yAxisZoomSpeed <
                    chart.ChartAreas[0].AxisY.Maximum - (yAxisZoomMultiple + 1) * yAxisZoomSpeed)
                yAxisZoomMultiple++;

            //zooming in the x axis
            chart.ChartAreas[0].AxisY.ScaleView.Zoom(
                    chart.ChartAreas[0].AxisY.Minimum + yAxisZoomMultiple * yAxisZoomSpeed,
                    chart.ChartAreas[0].AxisY.Maximum - yAxisZoomMultiple * yAxisZoomSpeed);
        }

        public void yAxisZoomOut()
        {
            if (chart == null)
                return;

            if (yAxisZoomMultiple > 0)
                yAxisZoomMultiple--;

            chart.ChartAreas[0].AxisY.ScaleView.Zoom(
                chart.ChartAreas[0].AxisY.Minimum + yAxisZoomMultiple * yAxisZoomSpeed,
                chart.ChartAreas[0].AxisY.Maximum - yAxisZoomMultiple * yAxisZoomSpeed);
        }

        public void xAxisZoom(MouseEventArgs mouseEvent)
        {
            var rawMouseX = mouseEvent.X;
            var mouseX = chart.ChartAreas[0].AxisX.PixelPositionToValue(rawMouseX);

            //zooming in
            if (mouseEvent.Delta > 0 && xAxisZoomMultiple != 10)
                xAxisZoomMultiple++;

            //zooming out
            if (mouseEvent.Delta < 0 && xAxisZoomMultiple != 0)
                xAxisZoomMultiple--;

            //the ratio between these need to stay the same as we zoom in:
            double rightSideOffset = chart.ChartAreas[0].AxisX.Maximum -
                (
                    (chart.ChartAreas[0].AxisX.Maximum - mouseX) *
                    xAxisZoomSpeed * (double)xAxisZoomMultiple
                );
            double leftSideOffset = (chart.ChartAreas[0].AxisX.Minimum + mouseX) *
                (xAxisZoomSpeed * (double)xAxisZoomMultiple);

            //zooming in/out the x axis. the if statment limits zoom
            if (mouseEvent.Delta < 0 || rightSideOffset - 10 > leftSideOffset)
            {
                chart.ChartAreas[0].AxisX.ScaleView.Zoom(
                    leftSideOffset,
                    rightSideOffset);
            }
            else
            {
                xAxisZoomMultiple--;
            }
        }

    }
}
