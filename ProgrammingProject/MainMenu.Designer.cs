namespace ProgrammingProject
{
    partial class MainMenu
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
            LB_MainMenuTitle = new Label();
            BT_helpMenu = new Button();
            BT_play = new Button();
            BT_leaderboard = new Button();
            BT_mainMenuClose = new Button();
            SuspendLayout();
            // 
            // LB_MainMenuTitle
            // 
            LB_MainMenuTitle.AutoSize = true;
            LB_MainMenuTitle.Font = new Font("Showcard Gothic", 30F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LB_MainMenuTitle.Location = new Point(757, 33);
            LB_MainMenuTitle.Name = "LB_MainMenuTitle";
            LB_MainMenuTitle.Size = new Size(294, 50);
            LB_MainMenuTitle.TabIndex = 0;
            LB_MainMenuTitle.Text = "BATTLESHIPS";
            LB_MainMenuTitle.Click += LB_MainMenuTitle_Click;
            // 
            // BT_helpMenu
            // 
            BT_helpMenu.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_helpMenu.Location = new Point(418, 264);
            BT_helpMenu.Name = "BT_helpMenu";
            BT_helpMenu.Size = new Size(207, 110);
            BT_helpMenu.TabIndex = 1;
            BT_helpMenu.Text = "HELP MENU";
            BT_helpMenu.UseVisualStyleBackColor = true;
            BT_helpMenu.Click += BT_helpMenu_Click;
            // 
            // BT_play
            // 
            BT_play.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_play.Location = new Point(816, 264);
            BT_play.Name = "BT_play";
            BT_play.Size = new Size(207, 110);
            BT_play.TabIndex = 2;
            BT_play.Text = "PLAY";
            BT_play.UseVisualStyleBackColor = true;
            BT_play.Click += BT_play_Click;
            // 
            // BT_leaderboard
            // 
            BT_leaderboard.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_leaderboard.Location = new Point(1213, 264);
            BT_leaderboard.Name = "BT_leaderboard";
            BT_leaderboard.Size = new Size(207, 110);
            BT_leaderboard.TabIndex = 3;
            BT_leaderboard.Text = "LEADERBOARD";
            BT_leaderboard.UseVisualStyleBackColor = true;
            BT_leaderboard.Click += BT_leaderboard_Click;
            // 
            // BT_mainMenuClose
            // 
            BT_mainMenuClose.BackColor = Color.FromArgb(255, 128, 128);
            BT_mainMenuClose.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_mainMenuClose.Location = new Point(12, 12);
            BT_mainMenuClose.Name = "BT_mainMenuClose";
            BT_mainMenuClose.Size = new Size(174, 71);
            BT_mainMenuClose.TabIndex = 4;
            BT_mainMenuClose.Text = "CLOSE";
            BT_mainMenuClose.UseVisualStyleBackColor = false;
            BT_mainMenuClose.Click += BT_mainMenuClose_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1455, 638);
            Controls.Add(BT_mainMenuClose);
            Controls.Add(BT_leaderboard);
            Controls.Add(BT_play);
            Controls.Add(BT_helpMenu);
            Controls.Add(LB_MainMenuTitle);
            Name = "MainMenu";
            Text = "MainMenu";
            Load += MainMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB_MainMenuTitle;
        private Button BT_helpMenu;
        private Button BT_play;
        private Button BT_leaderboard;
        private Button BT_mainMenuClose;
    }
}