namespace ProgrammingProject
{
    partial class EnterPlayerNamesTwoPlayer
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
            LB_EnterYourNameTitle = new Label();
            LB_EnterP1NamePrompt = new Label();
            LB_EnterP2NamePrompt = new Label();
            TB_EnterP1NameVsPlayer = new TextBox();
            TB_EnterP2NameVsPlayer = new TextBox();
            BT_startPlayerVsPlayerGame = new Button();
            SuspendLayout();
            // 
            // LB_EnterYourNameTitle
            // 
            LB_EnterYourNameTitle.AutoSize = true;
            LB_EnterYourNameTitle.Font = new Font("Showcard Gothic", 30F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LB_EnterYourNameTitle.Location = new Point(584, 36);
            LB_EnterYourNameTitle.Name = "LB_EnterYourNameTitle";
            LB_EnterYourNameTitle.Size = new Size(391, 50);
            LB_EnterYourNameTitle.TabIndex = 2;
            LB_EnterYourNameTitle.Text = "ENTER YOUR NAME";
            // 
            // LB_EnterP1NamePrompt
            // 
            LB_EnterP1NamePrompt.AutoSize = true;
            LB_EnterP1NamePrompt.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            LB_EnterP1NamePrompt.Location = new Point(584, 171);
            LB_EnterP1NamePrompt.Name = "LB_EnterP1NamePrompt";
            LB_EnterP1NamePrompt.Size = new Size(134, 37);
            LB_EnterP1NamePrompt.TabIndex = 3;
            LB_EnterP1NamePrompt.Text = "PLAYER 1:";
            // 
            // LB_EnterP2NamePrompt
            // 
            LB_EnterP2NamePrompt.AutoSize = true;
            LB_EnterP2NamePrompt.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            LB_EnterP2NamePrompt.Location = new Point(584, 248);
            LB_EnterP2NamePrompt.Name = "LB_EnterP2NamePrompt";
            LB_EnterP2NamePrompt.Size = new Size(134, 37);
            LB_EnterP2NamePrompt.TabIndex = 4;
            LB_EnterP2NamePrompt.Text = "PLAYER 2:";
            // 
            // TB_EnterP1NameVsPlayer
            // 
            TB_EnterP1NameVsPlayer.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            TB_EnterP1NameVsPlayer.Location = new Point(742, 171);
            TB_EnterP1NameVsPlayer.Name = "TB_EnterP1NameVsPlayer";
            TB_EnterP1NameVsPlayer.Size = new Size(233, 43);
            TB_EnterP1NameVsPlayer.TabIndex = 5;
            TB_EnterP1NameVsPlayer.TextChanged += TB_EnterP1NameVsPlayer_TextChanged;
            // 
            // TB_EnterP2NameVsPlayer
            // 
            TB_EnterP2NameVsPlayer.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            TB_EnterP2NameVsPlayer.Location = new Point(742, 242);
            TB_EnterP2NameVsPlayer.Name = "TB_EnterP2NameVsPlayer";
            TB_EnterP2NameVsPlayer.Size = new Size(233, 43);
            TB_EnterP2NameVsPlayer.TabIndex = 6;
            TB_EnterP2NameVsPlayer.TextChanged += TB_EnterP2NameVsPlayer_TextChanged;
            // 
            // BT_startPlayerVsPlayerGame
            // 
            BT_startPlayerVsPlayerGame.Location = new Point(1259, 513);
            BT_startPlayerVsPlayerGame.Name = "BT_startPlayerVsPlayerGame";
            BT_startPlayerVsPlayerGame.Size = new Size(201, 96);
            BT_startPlayerVsPlayerGame.TabIndex = 7;
            BT_startPlayerVsPlayerGame.Text = "START";
            BT_startPlayerVsPlayerGame.UseVisualStyleBackColor = true;
            BT_startPlayerVsPlayerGame.Click += BT_startPlayervsPlayerGame_Click;
            // 
            // EnterPlayerNamesTwoPlayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1526, 742);
            Controls.Add(BT_startPlayerVsPlayerGame);
            Controls.Add(TB_EnterP2NameVsPlayer);
            Controls.Add(TB_EnterP1NameVsPlayer);
            Controls.Add(LB_EnterP2NamePrompt);
            Controls.Add(LB_EnterP1NamePrompt);
            Controls.Add(LB_EnterYourNameTitle);
            Name = "EnterPlayerNamesTwoPlayer";
            Text = "EnterPlayerNamesTwoPlayer";
            Load += EnterPlayerNamesTwoPlayer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB_EnterYourNameTitle;
        private Label LB_EnterP1NamePrompt;
        private Label LB_EnterP2NamePrompt;
        private TextBox TB_EnterP1NameVsPlayer;
        private TextBox TB_EnterP2NameVsPlayer;
        private Button BT_startPlayerVsPlayerGame;
    }
}