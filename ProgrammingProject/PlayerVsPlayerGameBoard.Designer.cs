namespace ProgrammingProject
{
    partial class PlayerVsPlayerGameBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerVsPlayerGameBoard));
            BT_PlayervsPlayerClose = new Button();
            LB_Player1Name = new Label();
            lb_announcement = new Label();
            LB_Player2Name = new Label();
            PB_p1Heart1 = new PictureBox();
            PB_p1Heart2 = new PictureBox();
            PB_p1Heart3 = new PictureBox();
            PB_p1Heart4 = new PictureBox();
            PB_p1Heart5 = new PictureBox();
            PB_p2Heart1 = new PictureBox();
            PB_p2Heart2 = new PictureBox();
            PB_p2Heart3 = new PictureBox();
            PB_p2Heart4 = new PictureBox();
            PB_p2Heart5 = new PictureBox();
            BT_vsComputerLoad = new Button();
            BT_vsComputerSave = new Button();
            lb_player1Score = new Label();
            lb_player2Score = new Label();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart5).BeginInit();
            SuspendLayout();
            // 
            // BT_PlayervsPlayerClose
            // 
            BT_PlayervsPlayerClose.BackColor = Color.FromArgb(255, 128, 128);
            BT_PlayervsPlayerClose.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_PlayervsPlayerClose.Location = new Point(12, 12);
            BT_PlayervsPlayerClose.Name = "BT_PlayervsPlayerClose";
            BT_PlayervsPlayerClose.Size = new Size(174, 71);
            BT_PlayervsPlayerClose.TabIndex = 15;
            BT_PlayervsPlayerClose.Text = "CLOSE";
            BT_PlayervsPlayerClose.UseVisualStyleBackColor = false;
            BT_PlayervsPlayerClose.Click += BT_PlayervsPlayerClose_Click;
            // 
            // LB_Player1Name
            // 
            LB_Player1Name.AutoSize = true;
            LB_Player1Name.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            LB_Player1Name.Location = new Point(298, 76);
            LB_Player1Name.Name = "LB_Player1Name";
            LB_Player1Name.Size = new Size(178, 33);
            LB_Player1Name.TabIndex = 16;
            LB_Player1Name.Text = "PLAYER ONE ";
            // 
            // lb_announcement
            // 
            lb_announcement.AutoSize = true;
            lb_announcement.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            lb_announcement.Location = new Point(728, 31);
            lb_announcement.Name = "lb_announcement";
            lb_announcement.Size = new Size(203, 33);
            lb_announcement.TabIndex = 18;
            lb_announcement.Text = "Player Turn";
            // 
            // LB_Player2Name
            // 
            LB_Player2Name.AutoSize = true;
            LB_Player2Name.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            LB_Player2Name.Location = new Point(1188, 76);
            LB_Player2Name.Name = "LB_Player2Name";
            LB_Player2Name.Size = new Size(184, 33);
            LB_Player2Name.TabIndex = 19;
            LB_Player2Name.Text = "PLAYER TWO";
            // 
            // PB_p1Heart1
            // 
            PB_p1Heart1.Image = (Image)resources.GetObject("PB_p1Heart1.Image");
            PB_p1Heart1.Location = new Point(159, 869);
            PB_p1Heart1.Name = "PB_p1Heart1";
            PB_p1Heart1.Size = new Size(63, 69);
            PB_p1Heart1.TabIndex = 20;
            PB_p1Heart1.TabStop = false;
            // 
            // PB_p1Heart2
            // 
            PB_p1Heart2.Image = (Image)resources.GetObject("PB_p1Heart2.Image");
            PB_p1Heart2.Location = new Point(253, 869);
            PB_p1Heart2.Name = "PB_p1Heart2";
            PB_p1Heart2.Size = new Size(63, 69);
            PB_p1Heart2.TabIndex = 21;
            PB_p1Heart2.TabStop = false;
            PB_p1Heart2.Click += PB_p1Heart2_Click;
            // 
            // PB_p1Heart3
            // 
            PB_p1Heart3.Image = (Image)resources.GetObject("PB_p1Heart3.Image");
            PB_p1Heart3.Location = new Point(350, 869);
            PB_p1Heart3.Name = "PB_p1Heart3";
            PB_p1Heart3.Size = new Size(63, 69);
            PB_p1Heart3.TabIndex = 22;
            PB_p1Heart3.TabStop = false;
            // 
            // PB_p1Heart4
            // 
            PB_p1Heart4.Image = (Image)resources.GetObject("PB_p1Heart4.Image");
            PB_p1Heart4.Location = new Point(443, 869);
            PB_p1Heart4.Name = "PB_p1Heart4";
            PB_p1Heart4.Size = new Size(63, 69);
            PB_p1Heart4.TabIndex = 23;
            PB_p1Heart4.TabStop = false;
            // 
            // PB_p1Heart5
            // 
            PB_p1Heart5.Image = (Image)resources.GetObject("PB_p1Heart5.Image");
            PB_p1Heart5.Location = new Point(539, 869);
            PB_p1Heart5.Name = "PB_p1Heart5";
            PB_p1Heart5.Size = new Size(63, 69);
            PB_p1Heart5.TabIndex = 24;
            PB_p1Heart5.TabStop = false;
            // 
            // PB_p2Heart1
            // 
            PB_p2Heart1.Image = (Image)resources.GetObject("PB_p2Heart1.Image");
            PB_p2Heart1.Location = new Point(1070, 869);
            PB_p2Heart1.Name = "PB_p2Heart1";
            PB_p2Heart1.Size = new Size(63, 69);
            PB_p2Heart1.TabIndex = 25;
            PB_p2Heart1.TabStop = false;
            // 
            // PB_p2Heart2
            // 
            PB_p2Heart2.Image = (Image)resources.GetObject("PB_p2Heart2.Image");
            PB_p2Heart2.Location = new Point(1161, 869);
            PB_p2Heart2.Name = "PB_p2Heart2";
            PB_p2Heart2.Size = new Size(63, 69);
            PB_p2Heart2.TabIndex = 26;
            PB_p2Heart2.TabStop = false;
            // 
            // PB_p2Heart3
            // 
            PB_p2Heart3.Image = (Image)resources.GetObject("PB_p2Heart3.Image");
            PB_p2Heart3.Location = new Point(1257, 869);
            PB_p2Heart3.Name = "PB_p2Heart3";
            PB_p2Heart3.Size = new Size(63, 69);
            PB_p2Heart3.TabIndex = 27;
            PB_p2Heart3.TabStop = false;
            // 
            // PB_p2Heart4
            // 
            PB_p2Heart4.Image = (Image)resources.GetObject("PB_p2Heart4.Image");
            PB_p2Heart4.Location = new Point(1351, 869);
            PB_p2Heart4.Name = "PB_p2Heart4";
            PB_p2Heart4.Size = new Size(63, 69);
            PB_p2Heart4.TabIndex = 28;
            PB_p2Heart4.TabStop = false;
            // 
            // PB_p2Heart5
            // 
            PB_p2Heart5.Image = (Image)resources.GetObject("PB_p2Heart5.Image");
            PB_p2Heart5.Location = new Point(1447, 869);
            PB_p2Heart5.Name = "PB_p2Heart5";
            PB_p2Heart5.Size = new Size(63, 69);
            PB_p2Heart5.TabIndex = 29;
            PB_p2Heart5.TabStop = false;
            // 
            // BT_PlayerVsPlayerLoad_Click
            // 
            BT_vsComputerLoad.BackColor = Color.FromArgb(128, 255, 255);
            BT_vsComputerLoad.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_vsComputerLoad.Location = new Point(757, 482);
            BT_vsComputerLoad.Name = "BT_vsComputerLoad";
            BT_vsComputerLoad.Size = new Size(174, 71);
            BT_vsComputerLoad.TabIndex = 30;
            BT_vsComputerLoad.Text = "LOAD";
            BT_vsComputerLoad.UseVisualStyleBackColor = false;
            BT_vsComputerLoad.Click += BT_PlayerVsPlayerLoad_Click;
            // 
            // BT_vsComputerSave
            // 
            BT_vsComputerSave.BackColor = Color.FromArgb(128, 255, 128);
            BT_vsComputerSave.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_vsComputerSave.Location = new Point(757, 321);
            BT_vsComputerSave.Name = "BT_vsComputerSave";
            BT_vsComputerSave.Size = new Size(174, 71);
            BT_vsComputerSave.TabIndex = 31;
            BT_vsComputerSave.Text = "SAVE";
            BT_vsComputerSave.UseVisualStyleBackColor = false;
            BT_vsComputerSave.Click += BT_PlayerVsPlayerSave_Click;
            // 
            // lb_player1Score
            // 
            lb_player1Score.AutoSize = true;
            lb_player1Score.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_player1Score.Location = new Point(323, 977);
            lb_player1Score.Name = "lb_player1Score";
            lb_player1Score.Size = new Size(111, 33);
            lb_player1Score.TabIndex = 32;
            lb_player1Score.Text = "Score: ";
            // 
            // lb_player2Score
            // 
            lb_player2Score.AutoSize = true;
            lb_player2Score.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lb_player2Score.Location = new Point(1236, 977);
            lb_player2Score.Name = "lb_player2Score";
            lb_player2Score.Size = new Size(111, 33);
            lb_player2Score.TabIndex = 33;
            lb_player2Score.Text = "Score: ";
            // 
            // PlayerVsPlayerGameBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1680, 1061);
            Controls.Add(lb_player2Score);
            Controls.Add(lb_player1Score);
            Controls.Add(BT_vsComputerSave);
            Controls.Add(BT_vsComputerLoad);
            Controls.Add(PB_p2Heart5);
            Controls.Add(PB_p2Heart4);
            Controls.Add(PB_p2Heart3);
            Controls.Add(PB_p2Heart2);
            Controls.Add(PB_p2Heart1);
            Controls.Add(PB_p1Heart5);
            Controls.Add(PB_p1Heart4);
            Controls.Add(PB_p1Heart3);
            Controls.Add(PB_p1Heart2);
            Controls.Add(PB_p1Heart1);
            Controls.Add(LB_Player2Name);
            Controls.Add(lb_announcement);
            Controls.Add(LB_Player1Name);
            Controls.Add(BT_PlayervsPlayerClose);
            Name = "PlayerVsPlayerGameBoard";
            Text = "PlayerVsPlayerGameBoard";
            Load += PlayerVsPlayerGameBoard_Load;
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p1Heart5).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart4).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_p2Heart5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BT_PlayervsPlayerClose;
        private Label LB_Player1Name;
        private Label lb_announcement;
        private Label LB_Player2Name;
        private PictureBox PB_p1Heart1;
        private PictureBox PB_p1Heart2;
        private PictureBox PB_p1Heart3;
        private PictureBox PB_p1Heart4;
        private PictureBox PB_p1Heart5;
        private PictureBox PB_p2Heart1;
        private PictureBox PB_p2Heart2;
        private PictureBox PB_p2Heart3;
        private PictureBox PB_p2Heart4;
        private PictureBox PB_p2Heart5;
        private Button BT_vsComputerLoad;
        private Button BT_vsComputerSave;
        private Label lb_player1Score;
        private Label lb_player2Score;
    }
}