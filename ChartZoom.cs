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
        private double xAxisZoomSpeed = 1.3;
        private double yAxisZoomSpeed = 1.3;

        Chart chart;

        public ChartZoom(Chart chart)
        {
            this.chart = chart;
        }

        public void xAxisZoom(MouseEventArgs e)
        {
            var xAxis = chart.ChartAreas[0].AxisX;
            double initialAxisMousePosition;

            try
            {
                initialAxisMousePosition = xAxis.PixelPositionToValue(e.Location.X);
            }
            catch (ArgumentException)
            {
                initialAxisMousePosition = (xAxis.ScaleView.ViewMinimum + xAxis.ScaleView.ViewMaximum) / 2;
            }

            if (e.Delta > 0)
            {
                double dToLeft = (initialAxisMousePosition - xAxis.ScaleView.ViewMinimum) / xAxisZoomSpeed;
                double dToRight = (xAxis.ScaleView.ViewMaximum - initialAxisMousePosition) / xAxisZoomSpeed;
                xAxis.ScaleView.Zoom(initialAxisMousePosition - dToLeft, initialAxisMousePosition + dToRight);
            }
            else
            {
                double dToLeft = (initialAxisMousePosition - xAxis.ScaleView.ViewMinimum) * xAxisZoomSpeed;
                double dToRight = (xAxis.ScaleView.ViewMaximum - initialAxisMousePosition) * xAxisZoomSpeed;
                if (dToLeft + dToRight >= xAxis.Maximum - xAxis.Minimum)
                    xAxis.ScaleView.ZoomReset();
                else
                    xAxis.ScaleView.Zoom(initialAxisMousePosition - dToLeft, initialAxisMousePosition + dToRight);
            }
        }

        public void yAxisZoom(MouseEventArgs e)
        {
            var yAxis = chart.ChartAreas[0].AxisY;
            double initialAxisMousePosition;

            try
            {
                initialAxisMousePosition = yAxis.PixelPositionToValue(e.Location.Y);
            }
            catch (ArgumentException)
            {
                initialAxisMousePosition = (yAxis.ScaleView.ViewMinimum + yAxis.ScaleView.ViewMaximum) / 2;
            }

            if (e.Delta > 0)
            {
                double dToLeft = (initialAxisMousePosition - yAxis.ScaleView.ViewMinimum) / yAxisZoomSpeed;
                double dToRight = (yAxis.ScaleView.ViewMaximum - initialAxisMousePosition) / yAxisZoomSpeed;
                yAxis.ScaleView.Zoom(initialAxisMousePosition - dToLeft, initialAxisMousePosition + dToRight);
            }
            else
            {
                double dToLeft = (initialAxisMousePosition - yAxis.ScaleView.ViewMinimum) * yAxisZoomSpeed;
                double dToRight = (yAxis.ScaleView.ViewMaximum - initialAxisMousePosition) * yAxisZoomSpeed;
                if ((dToLeft + dToRight) * 1.4 >= yAxis.Maximum - yAxis.Minimum)
                    yAxis.ScaleView.ZoomReset();
                else
                    yAxis.ScaleView.Zoom(initialAxisMousePosition - dToLeft, initialAxisMousePosition + dToRight);
            }
        }
    }
}
