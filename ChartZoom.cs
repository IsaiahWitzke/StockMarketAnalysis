using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    class ChartZoom
    {
        private Axis xAxis = ChartHandler.chart.ChartAreas[0].AxisX;
        private Axis yAxis = ChartHandler.chart.ChartAreas[0].AxisY;

        private bool dragging = false;

        //location of drag start
        private int startMouseX, startMouseY;

        //value on x and y axis per pixel mouse moved
        private double xPerPixel, yPerPixel;

        //original viewport 
        private double originalMaxX, originalMaxY, originalMinX, originalMinY;

        public void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                dragging = true;
                
                startMouseX = e.Location.X;
                startMouseY = e.Location.Y;
                originalMaxX = xAxis.ScaleView.ViewMaximum;
                originalMinX = xAxis.ScaleView.ViewMinimum;
                originalMaxY = yAxis.ScaleView.ViewMaximum;
                originalMinY = yAxis.ScaleView.ViewMinimum;
                //calculate change in axis value per change in pixel value at current zoom level
                xPerPixel = xAxis.PixelPositionToValue(0) - xAxis.PixelPositionToValue(1);
                yPerPixel = yAxis.PixelPositionToValue(0) - yAxis.PixelPositionToValue(1);
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                dragging = false;
            }
        }

        public void mouseLeave(object sender, EventArgs e)
        {
                dragging = false;
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            double diffX = (startMouseX - e.Location.X) * xPerPixel;
            double diffY = (startMouseY - e.Location.Y) * yPerPixel;

            //if new moved viewport is in range then change to that viewport
            if (originalMinX - diffX >= xAxis.Minimum && originalMaxX - diffX <= xAxis.Maximum)
                xAxis.ScaleView.Zoom(originalMinX - diffX, originalMaxX - diffX);

            if (originalMinY - diffY >= yAxis.Minimum && originalMaxY - diffY <= yAxis.Maximum)
                yAxis.ScaleView.Zoom(originalMinY - diffY, originalMaxY - diffY);
        }

        public void mouseScroll(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                yAxisZoom(e, 1.5);
            }
            else if (Control.ModifierKeys == Keys.Control)
            {
                scrollXAxis(e);
            }
            else
            {
                xAxisZoom(e, 1.3);
                yAxisZoom(e, 1.3);
            }
        }

        public void scrollXAxis(MouseEventArgs e)
        {
            double scrollFactor = 3.0;

            if (e.Delta > 0)
            {
                if (xAxis.ScaleView.ViewMinimum + scrollFactor >= xAxis.Minimum && xAxis.ScaleView.ViewMaximum + scrollFactor <= xAxis.Maximum)
                    xAxis.ScaleView.Zoom(xAxis.ScaleView.ViewMinimum + scrollFactor, xAxis.ScaleView.ViewMaximum + scrollFactor);
            }
            else
            {
                if (xAxis.ScaleView.ViewMinimum - scrollFactor >= xAxis.Minimum && xAxis.ScaleView.ViewMaximum - scrollFactor <= xAxis.Maximum)
                    xAxis.ScaleView.Zoom(xAxis.ScaleView.ViewMinimum - scrollFactor, xAxis.ScaleView.ViewMaximum - scrollFactor);
            }
        }

        public void xAxisZoom(MouseEventArgs e, double xAxisZoomSpeed)
        {
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

        public void yAxisZoom(MouseEventArgs e, double yAxisZoomSpeed)
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
