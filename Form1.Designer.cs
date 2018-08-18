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
            this.button3 = new System.Windows.Forms.Button();
            this.aYAxisZoomIn = new System.Windows.Forms.Button();
            this.aYAxisZoomOut = new System.Windows.Forms.Button();
            this.drawMode = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 25);
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(145, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Lows";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // aYAxisZoomIn
            // 
            this.aYAxisZoomIn.Location = new System.Drawing.Point(195, 50);
            this.aYAxisZoomIn.Name = "aYAxisZoomIn";
            this.aYAxisZoomIn.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomIn.TabIndex = 5;
            this.aYAxisZoomIn.Text = "+";
            this.aYAxisZoomIn.UseVisualStyleBackColor = true;
            this.aYAxisZoomIn.Click += new System.EventHandler(this.aYAxisZoomIn_Click);
            // 
            // aYAxisZoomOut
            // 
            this.aYAxisZoomOut.Location = new System.Drawing.Point(195, 80);
            this.aYAxisZoomOut.Name = "aYAxisZoomOut";
            this.aYAxisZoomOut.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomOut.TabIndex = 6;
            this.aYAxisZoomOut.Text = "-";
            this.aYAxisZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.aYAxisZoomOut.UseVisualStyleBackColor = true;
            this.aYAxisZoomOut.Click += new System.EventHandler(this.aYAxisZoomOut_Click);
            // 
            // drawMode
            // 
            this.drawMode.Location = new System.Drawing.Point(287, 3);
            this.drawMode.Name = "drawMode";
            this.drawMode.Size = new System.Drawing.Size(75, 23);
            this.drawMode.TabIndex = 7;
            this.drawMode.Text = "Draw";
            this.drawMode.UseVisualStyleBackColor = true;
            this.drawMode.Click += new System.EventHandler(this.draw_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Highs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.drawMode, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(515, 695);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(711, 51);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // chartPanel
            // 
            this.chartPanel.Location = new System.Drawing.Point(174, 98);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(954, 434);
            this.chartPanel.TabIndex = 9;
            // 
            // aMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 896);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.aYAxisZoomOut);
            this.Controls.Add(this.aYAxisZoomIn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "aMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Market Analysis";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button aYAxisZoomIn;
        private System.Windows.Forms.Button aYAxisZoomOut;
        private System.Windows.Forms.Button drawMode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel chartPanel;
    }
}




