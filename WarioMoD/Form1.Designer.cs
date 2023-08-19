namespace WarioMoD {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.findRoute = new System.Windows.Forms.Button();
			this.runTimer = new System.Windows.Forms.Button();
			this.timerDelay = new System.Windows.Forms.NumericUpDown();
			this.searchBar = new System.Windows.Forms.ProgressBar();
			this.searchLabel = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.aaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.isHumanRunning = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.timerDelay)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// findRoute
			// 
			this.findRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.findRoute.Location = new System.Drawing.Point(196, 114);
			this.findRoute.Name = "findRoute";
			this.findRoute.Size = new System.Drawing.Size(92, 25);
			this.findRoute.TabIndex = 0;
			this.findRoute.Text = "Find Route";
			this.findRoute.UseVisualStyleBackColor = true;
			this.findRoute.Click += new System.EventHandler(this.FindRoute);
			// 
			// runTimer
			// 
			this.runTimer.Location = new System.Drawing.Point(196, 27);
			this.runTimer.Name = "runTimer";
			this.runTimer.Size = new System.Drawing.Size(95, 25);
			this.runTimer.TabIndex = 1;
			this.runTimer.Text = "Start Timer";
			this.runTimer.UseVisualStyleBackColor = true;
			this.runTimer.Click += new System.EventHandler(this.RequestTimerStart);
			// 
			// timerDelay
			// 
			this.timerDelay.Location = new System.Drawing.Point(94, 28);
			this.timerDelay.Name = "timerDelay";
			this.timerDelay.Size = new System.Drawing.Size(95, 23);
			this.timerDelay.TabIndex = 2;
			this.timerDelay.ValueChanged += new System.EventHandler(this.timerDelay_ValueChanged);
			// 
			// searchBar
			// 
			this.searchBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchBar.Location = new System.Drawing.Point(12, 145);
			this.searchBar.Maximum = 60;
			this.searchBar.Name = "searchBar";
			this.searchBar.Size = new System.Drawing.Size(276, 34);
			this.searchBar.TabIndex = 3;
			// 
			// searchLabel
			// 
			this.searchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.searchLabel.AutoSize = true;
			this.searchLabel.Location = new System.Drawing.Point(12, 124);
			this.searchLabel.Name = "searchLabel";
			this.searchLabel.Size = new System.Drawing.Size(36, 15);
			this.searchLabel.TabIndex = 4;
			this.searchLabel.Text = "60/60";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "DS",
            "3DS"});
			this.comboBox1.Location = new System.Drawing.Point(196, 58);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(95, 23);
			this.comboBox1.TabIndex = 5;
			this.comboBox1.Text = "3DS";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Starting Seed:";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aaaToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(300, 24);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// aaaToolStripMenuItem
			// 
			this.aaaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isHumanRunning});
			this.aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
			this.aaaToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.aaaToolStripMenuItem.Text = "Edit";
			// 
			// isHumanRunning
			// 
			this.isHumanRunning.Checked = true;
			this.isHumanRunning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isHumanRunning.Name = "isHumanRunning";
			this.isHumanRunning.Size = new System.Drawing.Size(162, 22);
			this.isHumanRunning.Text = "Human Running";
			this.isHumanRunning.Click += new System.EventHandler(this.isHumanRunning_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 191);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.searchLabel);
			this.Controls.Add(this.searchBar);
			this.Controls.Add(this.timerDelay);
			this.Controls.Add(this.runTimer);
			this.Controls.Add(this.findRoute);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(316, 189);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.timerDelay)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button findRoute;
		private Button runTimer;
		private NumericUpDown timerDelay;
		private ProgressBar searchBar;
		private Label searchLabel;
		private ComboBox comboBox1;
		private Label label1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem aaaToolStripMenuItem;
		private ToolStripMenuItem aboutToolStripMenuItem;
		private ToolStripMenuItem isHumanRunning;
	}
}