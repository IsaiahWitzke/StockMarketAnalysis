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
            this.drawMode = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Historical Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "msft";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(464, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Highs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(579, 36);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Lows";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // aYAxisZoomIn
            // 
            this.aYAxisZoomIn.Location = new System.Drawing.Point(195, 75);
            this.aYAxisZoomIn.Name = "aYAxisZoomIn";
            this.aYAxisZoomIn.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomIn.TabIndex = 5;
            this.aYAxisZoomIn.Text = "+";
            this.aYAxisZoomIn.UseVisualStyleBackColor = true;
            this.aYAxisZoomIn.Click += new System.EventHandler(this.aYAxisZoomIn_Click);
            // 
            // aYAxisZoomOut
            // 
            this.aYAxisZoomOut.Location = new System.Drawing.Point(195, 105);
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
            this.drawMode.Location = new System.Drawing.Point(678, 37);
            this.drawMode.Name = "drawMode";
            this.drawMode.Size = new System.Drawing.Size(75, 23);
            this.drawMode.TabIndex = 7;
            this.drawMode.Text = "Draw";
            this.drawMode.UseVisualStyleBackColor = true;
            this.drawMode.Click += new System.EventHandler(this.draw_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1836, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGraphToolStripMenuItem,
            this.saveGraphAsToolStripMenuItem,
            this.openGraphToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveGraphToolStripMenuItem
            // 
            this.saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            this.saveGraphToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveGraphToolStripMenuItem.Text = "Save Graph";
            this.saveGraphToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToolStripMenuItem_Click);
            // 
            // saveGraphAsToolStripMenuItem
            // 
            this.saveGraphAsToolStripMenuItem.Name = "saveGraphAsToolStripMenuItem";
            this.saveGraphAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveGraphAsToolStripMenuItem.Text = "Save Graph As";
            // 
            // openGraphToolStripMenuItem
            // 
            this.openGraphToolStripMenuItem.Name = "openGraphToolStripMenuItem";
            this.openGraphToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openGraphToolStripMenuItem.Text = "Open Graph";
            this.openGraphToolStripMenuItem.Click += new System.EventHandler(this.openGraphToolStripMenuItem_Click);
            // 
            // aMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 896);
            this.Controls.Add(this.drawMode);
            this.Controls.Add(this.aYAxisZoomOut);
            this.Controls.Add(this.aYAxisZoomIn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "aMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Market Analysis";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button aYAxisZoomIn;
        private System.Windows.Forms.Button aYAxisZoomOut;
        private System.Windows.Forms.Button drawMode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGraphToolStripMenuItem;
    }
}




