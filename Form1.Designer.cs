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
            this.aGetHistoricalData = new System.Windows.Forms.Button();
            this.aTickerTextInput = new System.Windows.Forms.TextBox();
            this.aYAxisZoomIn = new System.Windows.Forms.Button();
            this.aYAxisZoomOut = new System.Windows.Forms.Button();
            this.drawMode = new System.Windows.Forms.Button();
            this.aMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAnnotationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAnnotatedGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.strategiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStrategyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStrategyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeStrategyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aUIChartTable = new System.Windows.Forms.TableLayoutPanel();
            this.aChartPanel = new System.Windows.Forms.Panel();
            this.aMenuStrip.SuspendLayout();
            this.aUIChartTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // aGetHistoricalData
            // 
            this.aGetHistoricalData.Location = new System.Drawing.Point(12, 37);
            this.aGetHistoricalData.Name = "aGetHistoricalData";
            this.aGetHistoricalData.Size = new System.Drawing.Size(108, 25);
            this.aGetHistoricalData.TabIndex = 0;
            this.aGetHistoricalData.Text = "Get Historical Data";
            this.aGetHistoricalData.UseVisualStyleBackColor = true;
            this.aGetHistoricalData.Click += new System.EventHandler(this.aGetHistoricalData_Click);
            // 
            // aTickerTextInput
            // 
            this.aTickerTextInput.Location = new System.Drawing.Point(126, 40);
            this.aTickerTextInput.Name = "aTickerTextInput";
            this.aTickerTextInput.Size = new System.Drawing.Size(54, 20);
            this.aTickerTextInput.TabIndex = 1;
            this.aTickerTextInput.Text = "msft";
            // 
            // aYAxisZoomIn
            // 
            this.aYAxisZoomIn.Location = new System.Drawing.Point(186, 39);
            this.aYAxisZoomIn.Name = "aYAxisZoomIn";
            this.aYAxisZoomIn.Size = new System.Drawing.Size(19, 23);
            this.aYAxisZoomIn.TabIndex = 5;
            this.aYAxisZoomIn.Text = "+";
            this.aYAxisZoomIn.UseVisualStyleBackColor = true;
            this.aYAxisZoomIn.Click += new System.EventHandler(this.aYAxisZoomIn_Click);
            // 
            // aYAxisZoomOut
            // 
            this.aYAxisZoomOut.Location = new System.Drawing.Point(186, 68);
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
            this.drawMode.Location = new System.Drawing.Point(3, 3);
            this.drawMode.Name = "drawMode";
            this.drawMode.Size = new System.Drawing.Size(75, 23);
            this.drawMode.TabIndex = 7;
            this.drawMode.Text = "Draw";
            this.drawMode.UseVisualStyleBackColor = true;
            this.drawMode.Click += new System.EventHandler(this.draw_Click);
            // 
            // aMenuStrip
            // 
            this.aMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.strategiesToolStripMenuItem});
            this.aMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.aMenuStrip.Name = "aMenuStrip";
            this.aMenuStrip.Size = new System.Drawing.Size(1836, 24);
            this.aMenuStrip.TabIndex = 8;
            this.aMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAnnotationsToolStripMenuItem,
            this.openAnnotatedGraphToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAnnotationsToolStripMenuItem
            // 
            this.saveAnnotationsToolStripMenuItem.Name = "saveAnnotationsToolStripMenuItem";
            this.saveAnnotationsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveAnnotationsToolStripMenuItem.Text = "Save Annotations";
            this.saveAnnotationsToolStripMenuItem.Click += new System.EventHandler(this.saveAnnotationsToolStripMenuItem_Click);
            // 
            // openAnnotatedGraphToolStripMenuItem
            // 
            this.openAnnotatedGraphToolStripMenuItem.Name = "openAnnotatedGraphToolStripMenuItem";
            this.openAnnotatedGraphToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openAnnotatedGraphToolStripMenuItem.Text = "Open Annotated Graph";
            this.openAnnotatedGraphToolStripMenuItem.Click += new System.EventHandler(this.openAnnotatedGraphToolStripMenuItem_Click);
            // 
            // strategiesToolStripMenuItem
            // 
            this.strategiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStrategyToolStripMenuItem,
            this.openStrategyToolStripMenuItem,
            this.executeStrategyToolStripMenuItem});
            this.strategiesToolStripMenuItem.Name = "strategiesToolStripMenuItem";
            this.strategiesToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.strategiesToolStripMenuItem.Text = "Strategies";
            // 
            // newStrategyToolStripMenuItem
            // 
            this.newStrategyToolStripMenuItem.Name = "newStrategyToolStripMenuItem";
            this.newStrategyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newStrategyToolStripMenuItem.Text = "New Strategy";
            this.newStrategyToolStripMenuItem.Click += new System.EventHandler(this.newStrategyToolStripMenuItem_Click);
            // 
            // openStrategyToolStripMenuItem
            // 
            this.openStrategyToolStripMenuItem.Name = "openStrategyToolStripMenuItem";
            this.openStrategyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openStrategyToolStripMenuItem.Text = "Edit Strategy";
            this.openStrategyToolStripMenuItem.Click += new System.EventHandler(this.openStrategyToolStripMenuItem_Click);
            // 
            // executeStrategyToolStripMenuItem
            // 
            this.executeStrategyToolStripMenuItem.Name = "executeStrategyToolStripMenuItem";
            this.executeStrategyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.executeStrategyToolStripMenuItem.Text = "Execute Strategy";
            this.executeStrategyToolStripMenuItem.Click += new System.EventHandler(this.executeStrategyToolStripMenuItem_Click);
            // 
            // aUIChartTable
            // 
            this.aUIChartTable.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.aUIChartTable.ColumnCount = 5;
            this.aUIChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.aUIChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.aUIChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.aUIChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.aUIChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.aUIChartTable.Controls.Add(this.drawMode, 0, 0);
            this.aUIChartTable.Location = new System.Drawing.Point(515, 695);
            this.aUIChartTable.Name = "aUIChartTable";
            this.aUIChartTable.RowCount = 1;
            this.aUIChartTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.aUIChartTable.Size = new System.Drawing.Size(711, 51);
            this.aUIChartTable.TabIndex = 8;
            // 
            // aChartPanel
            // 
            this.aChartPanel.Location = new System.Drawing.Point(211, 37);
            this.aChartPanel.Name = "aChartPanel";
            this.aChartPanel.Size = new System.Drawing.Size(954, 434);
            this.aChartPanel.TabIndex = 9;
            // 
            // aMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 896);
            this.Controls.Add(this.aChartPanel);
            this.Controls.Add(this.aUIChartTable);
            this.Controls.Add(this.aYAxisZoomOut);
            this.Controls.Add(this.aYAxisZoomIn);
            this.Controls.Add(this.aTickerTextInput);
            this.Controls.Add(this.aGetHistoricalData);
            this.Controls.Add(this.aMenuStrip);
            this.MainMenuStrip = this.aMenuStrip;
            this.Name = "aMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Market Analysis";
            this.aMenuStrip.ResumeLayout(false);
            this.aMenuStrip.PerformLayout();
            this.aUIChartTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aGetHistoricalData;
        private System.Windows.Forms.TextBox aTickerTextInput;
        private System.Windows.Forms.Button aYAxisZoomIn;
        private System.Windows.Forms.Button aYAxisZoomOut;
        private System.Windows.Forms.Button drawMode;
        private System.Windows.Forms.MenuStrip aMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAnnotationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAnnotatedGraphToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel aUIChartTable;
        private System.Windows.Forms.Panel aChartPanel;
        private System.Windows.Forms.ToolStripMenuItem strategiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStrategyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStrategyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeStrategyToolStripMenuItem;
    }
}




