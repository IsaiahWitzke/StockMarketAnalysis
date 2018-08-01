using System.Windows.Forms.DataVisualization.Charting;

using System.Collections.Generic;

namespace StockMarketAnalysis
{
    partial class aMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.aYAxisZoomIn = new System.Windows.Forms.Button();
            this.aYAxisZoomOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Historical Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "msft";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(464, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Highs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(579, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Lows";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // aYAxisZoomIn
            // 
            
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.aMainChart.ChartAreas.Add(chartArea1);
            this.aMainChart.Location = new System.Drawing.Point(97, 101);
            this.aMainChart.Name = "aMainChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            series1.IsXValueIndexed = true;
            series1.Name = "aCandleSticks";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            this.aMainChart.Series.Add(series1);
            this.aMainChart.Size = new System.Drawing.Size(1668, 750);
            this.aMainChart.TabIndex = 2;
            this.aMainChart.Text = "Main Chart";
            this.aMainChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.aMainChart_MouseClick);

            this.aYAxisZoomIn.Location = new System.Drawing.Point(13, 62);
            this.aYAxisZoomIn.Name = "aYAxisZoomIn";
            this.aYAxisZoomIn.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomIn.TabIndex = 5;
            this.aYAxisZoomIn.Text = "+";
            this.aYAxisZoomIn.UseVisualStyleBackColor = true;
            this.aYAxisZoomIn.Click += new System.EventHandler(this.aYAxisZoomIn_Click);
            // 
            // aYAxisZoomOut
            // 
            this.aYAxisZoomOut.Location = new System.Drawing.Point(13, 92);
            this.aYAxisZoomOut.Name = "aYAxisZoomOut";
            this.aYAxisZoomOut.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomOut.TabIndex = 6;
            this.aYAxisZoomOut.Text = "-";
            this.aYAxisZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.aYAxisZoomOut.UseVisualStyleBackColor = true;
            this.aYAxisZoomOut.Click += new System.EventHandler(this.aYAxisZoomOut_Click);
            // 
            // aMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 896);
            this.Controls.Add(this.aYAxisZoomOut);
            this.Controls.Add(this.aYAxisZoomIn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "aMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Market Analysis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        //private System.Windows.Forms.DataVisualization.Charting.Chart aMainChart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button aYAxisZoomIn;
        private System.Windows.Forms.Button aYAxisZoomOut;
    }
}




