namespace JapanesePractice.FrontEnd.WinForms
{
    partial class MainWindow
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newResourceSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPluginSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openResourceSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openPluginDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.startSessionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newResourceSessionToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "New";
            // 
            // newResourceSessionToolStripMenuItem
            // 
            this.newResourceSessionToolStripMenuItem.Name = "newResourceSessionToolStripMenuItem";
            this.newResourceSessionToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newResourceSessionToolStripMenuItem.Text = "Resource Session";
            this.newResourceSessionToolStripMenuItem.Click += new System.EventHandler(this.NewResourceSessionToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPluginSessionToolStripMenuItem,
            this.openResourceSessionToolStripMenuItem,
            this.toolStripSeparator2,
            this.openPluginDirectoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.resourceFileToolStripMenuItem,
            this.resourcePathToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // openPluginSessionToolStripMenuItem
            // 
            this.openPluginSessionToolStripMenuItem.Name = "openPluginSessionToolStripMenuItem";
            this.openPluginSessionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openPluginSessionToolStripMenuItem.Text = "Plug-in Session...";
            this.openPluginSessionToolStripMenuItem.Click += new System.EventHandler(this.OpenPluginSessionToolStripMenuItem_Click);
            // 
            // openResourceSessionToolStripMenuItem
            // 
            this.openResourceSessionToolStripMenuItem.Name = "openResourceSessionToolStripMenuItem";
            this.openResourceSessionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openResourceSessionToolStripMenuItem.Text = "Resource Session...";
            this.openResourceSessionToolStripMenuItem.Click += new System.EventHandler(this.OpenResourceSessionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // openPluginDirectoryToolStripMenuItem
            // 
            this.openPluginDirectoryToolStripMenuItem.Name = "openPluginDirectoryToolStripMenuItem";
            this.openPluginDirectoryToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openPluginDirectoryToolStripMenuItem.Text = "Plug-in Directory...";
            this.openPluginDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenPluginDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // resourceFileToolStripMenuItem
            // 
            this.resourceFileToolStripMenuItem.Name = "resourceFileToolStripMenuItem";
            this.resourceFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.resourceFileToolStripMenuItem.Text = "Resource File...";
            this.resourceFileToolStripMenuItem.Click += new System.EventHandler(this.OpenResourceFileToolStripMenuItem_Click);
            // 
            // resourcePathToolStripMenuItem
            // 
            this.resourcePathToolStripMenuItem.Name = "resourcePathToolStripMenuItem";
            this.resourcePathToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.resourcePathToolStripMenuItem.Text = "Resource Path...";
            this.resourcePathToolStripMenuItem.Click += new System.EventHandler(this.OpenResourcePathToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // startSessionToolStripMenuItem
            // 
            this.startSessionToolStripMenuItem.Name = "startSessionToolStripMenuItem";
            this.startSessionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startSessionToolStripMenuItem.Text = "Start Session";
            this.startSessionToolStripMenuItem.Click += new System.EventHandler(this.StartSessionToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(784, 515);
            this.mainPanel.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Japanese Practice";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourcePathToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newResourceSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openResourceSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openPluginSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openPluginDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem startSessionToolStripMenuItem;
    }
}

