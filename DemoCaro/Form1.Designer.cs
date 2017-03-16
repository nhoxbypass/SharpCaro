namespace DemoCaro
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pnl_CaroChess = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerVsPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerVsComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bt_PlayWithHuman = new System.Windows.Forms.Button();
            this.bt_PlayWithNormalCom = new System.Windows.Forms.Button();
            this.pnl_GameGuide = new System.Windows.Forms.Panel();
            this.lblGameGuide = new System.Windows.Forms.Label();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.timer_GameGuide = new System.Windows.Forms.Timer(this.components);
            this.bt_PlayWithEasyCom = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.pnl_GameGuide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_CaroChess
            // 
            this.pnl_CaroChess.BackColor = System.Drawing.Color.Azure;
            this.pnl_CaroChess.Location = new System.Drawing.Point(275, 39);
            this.pnl_CaroChess.Name = "pnl_CaroChess";
            this.pnl_CaroChess.Size = new System.Drawing.Size(501, 501);
            this.pnl_CaroChess.TabIndex = 0;
            this.pnl_CaroChess.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_CaroChess_Paint);
            this.pnl_CaroChess.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_CaroChess_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerVsPlayerToolStripMenuItem,
            this.playerVsComputerToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            // 
            // playerVsPlayerToolStripMenuItem
            // 
            this.playerVsPlayerToolStripMenuItem.Name = "playerVsPlayerToolStripMenuItem";
            this.playerVsPlayerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.playerVsPlayerToolStripMenuItem.Text = "Player vs Player";
            this.playerVsPlayerToolStripMenuItem.Click += new System.EventHandler(this.PvsP);
            // 
            // playerVsComputerToolStripMenuItem
            // 
            this.playerVsComputerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.normalToolStripMenuItem});
            this.playerVsComputerToolStripMenuItem.Name = "playerVsComputerToolStripMenuItem";
            this.playerVsComputerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.playerVsComputerToolStripMenuItem.Text = "Player vs Com";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.easyToolStripMenuItem.Text = "Easy";
            this.easyToolStripMenuItem.Click += new System.EventHandler(this.PvsEasyCom);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // bt_PlayWithHuman
            // 
            this.bt_PlayWithHuman.Location = new System.Drawing.Point(12, 488);
            this.bt_PlayWithHuman.Name = "bt_PlayWithHuman";
            this.bt_PlayWithHuman.Size = new System.Drawing.Size(116, 51);
            this.bt_PlayWithHuman.TabIndex = 2;
            this.bt_PlayWithHuman.Text = "Chơi với người";
            this.bt_PlayWithHuman.UseVisualStyleBackColor = true;
            this.bt_PlayWithHuman.Click += new System.EventHandler(this.PvsP);
            // 
            // bt_PlayWithNormalCom
            // 
            this.bt_PlayWithNormalCom.Enabled = false;
            this.bt_PlayWithNormalCom.Location = new System.Drawing.Point(134, 440);
            this.bt_PlayWithNormalCom.Name = "bt_PlayWithNormalCom";
            this.bt_PlayWithNormalCom.Size = new System.Drawing.Size(122, 42);
            this.bt_PlayWithNormalCom.TabIndex = 3;
            this.bt_PlayWithNormalCom.Text = "Chơi với máy thường";
            this.bt_PlayWithNormalCom.UseVisualStyleBackColor = true;
            // 
            // pnl_GameGuide
            // 
            this.pnl_GameGuide.BackColor = System.Drawing.Color.Azure;
            this.pnl_GameGuide.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_GameGuide.Controls.Add(this.lblGameGuide);
            this.pnl_GameGuide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_GameGuide.ForeColor = System.Drawing.Color.Red;
            this.pnl_GameGuide.Location = new System.Drawing.Point(12, 243);
            this.pnl_GameGuide.Name = "pnl_GameGuide";
            this.pnl_GameGuide.Size = new System.Drawing.Size(244, 191);
            this.pnl_GameGuide.TabIndex = 4;
            // 
            // lblGameGuide
            // 
            this.lblGameGuide.AutoSize = true;
            this.lblGameGuide.Location = new System.Drawing.Point(3, 174);
            this.lblGameGuide.Name = "lblGameGuide";
            this.lblGameGuide.Size = new System.Drawing.Size(153, 16);
            this.lblGameGuide.TabIndex = 0;
            this.lblGameGuide.Text = "Hướng dẫn cách chơi";
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(134, 488);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(122, 51);
            this.bt_Exit.TabIndex = 6;
            this.bt_Exit.Text = "Thoát";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // timer_GameGuide
            // 
            this.timer_GameGuide.Interval = 25;
            this.timer_GameGuide.Tick += new System.EventHandler(this.timer_GameGuide_Tick);
            // 
            // bt_PlayWithEasyCom
            // 
            this.bt_PlayWithEasyCom.Location = new System.Drawing.Point(11, 440);
            this.bt_PlayWithEasyCom.Name = "bt_PlayWithEasyCom";
            this.bt_PlayWithEasyCom.Size = new System.Drawing.Size(116, 42);
            this.bt_PlayWithEasyCom.TabIndex = 7;
            this.bt_PlayWithEasyCom.Text = "Chơi với máy dễ";
            this.bt_PlayWithEasyCom.UseVisualStyleBackColor = true;
            this.bt_PlayWithEasyCom.Click += new System.EventHandler(this.PvsEasyCom);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DemoCaro.Properties.Resources.game_co_caro;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(12, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(244, 198);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(787, 561);
            this.Controls.Add(this.bt_PlayWithEasyCom);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.pnl_GameGuide);
            this.Controls.Add(this.bt_PlayWithNormalCom);
            this.Controls.Add(this.bt_PlayWithHuman);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnl_CaroChess);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Demo Caro v1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnl_GameGuide.ResumeLayout(false);
            this.pnl_GameGuide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.Panel pnl_CaroChess;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button bt_PlayWithHuman;
        private System.Windows.Forms.Button bt_PlayWithNormalCom;
        private System.Windows.Forms.Panel pnl_GameGuide;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Label lblGameGuide;
        private System.Windows.Forms.Timer timer_GameGuide;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerVsPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerVsComputerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.Button bt_PlayWithEasyCom;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    }
}

