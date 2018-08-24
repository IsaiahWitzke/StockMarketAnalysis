using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    class ChartZoom
    {
        //chart zoom variables
        private double xAxisZoomSpeed = 1.3;
        private double yAxisZoomSpeed = 1.3;
        private bool middleDrag = false;
        private double mouseX = 0;
        private double mouseY = 0;
        private Axis xAxis = ChartHandler.chart.ChartAreas[0].AxisX;
        private Axis yAxis = ChartHandler.chart.ChartAreas[0].AxisY;

        Chart chart;

        public ChartZoom(Chart chart)
        {
            this.chart = chart;
        }

        public void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                middleDrag = true;
                mouseX = xAxis.PixelPositionToValue(e.Location.X);
                mouseY = yAxis.PixelPositionToValue(e.Location.Y);
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                middleDrag = false;
            }
        }

        public void mouseLeave(object sender, EventArgs e)
        {
                middleDrag = false;
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (!middleDrag) return;

            double diffX = mouseX - xAxis.PixelPositionToValue(e.Location.X);
            double diffY = mouseY - yAxis.PixelPositionToValue(e.Location.Y);

            xAxis.ScaleView.Zoom(xAxis.ScaleView.ViewMinimum + diffX, xAxis.ScaleView.ViewMaximum + diffX);
            yAxis.ScaleView.Zoom(yAxis.ScaleView.ViewMinimum + diffY, yAxis.ScaleView.ViewMaximum + diffY);
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
