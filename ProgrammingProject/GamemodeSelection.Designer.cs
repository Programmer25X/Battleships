namespace ProgrammingProject
{
    partial class GamemodeSelection
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
            LB_GamemodeSelectionTitle = new Label();
            BT_twoPlayersOption = new Button();
            BT_playerVsComputerOption = new Button();
            BT_SelectGamemodeBack = new Button();
            SuspendLayout();
            // 
            // LB_GamemodeSelectionTitle
            // 
            LB_GamemodeSelectionTitle.AutoSize = true;
            LB_GamemodeSelectionTitle.Font = new Font("Showcard Gothic", 30F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LB_GamemodeSelectionTitle.Location = new Point(646, 30);
            LB_GamemodeSelectionTitle.Name = "LB_GamemodeSelectionTitle";
            LB_GamemodeSelectionTitle.Size = new Size(408, 50);
            LB_GamemodeSelectionTitle.TabIndex = 0;
            LB_GamemodeSelectionTitle.Text = "Select Gamemode ";
            // 
            // BT_twoPlayersOption
            // 
            BT_twoPlayersOption.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_twoPlayersOption.Location = new Point(700, 142);
            BT_twoPlayersOption.Name = "BT_twoPlayersOption";
            BT_twoPlayersOption.Size = new Size(228, 56);
            BT_twoPlayersOption.TabIndex = 1;
            BT_twoPlayersOption.Text = "Player vs Player";
            BT_twoPlayersOption.UseVisualStyleBackColor = true;
            BT_twoPlayersOption.Click += BT_twoPlayersOption_Click;
            // 
            // BT_playerVsComputerOption
            // 
            BT_playerVsComputerOption.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_playerVsComputerOption.Location = new Point(700, 238);
            BT_playerVsComputerOption.Name = "BT_playerVsComputerOption";
            BT_playerVsComputerOption.Size = new Size(228, 56);
            BT_playerVsComputerOption.TabIndex = 2;
            BT_playerVsComputerOption.Text = "Player vs CPU";
            BT_playerVsComputerOption.UseVisualStyleBackColor = true;
            BT_playerVsComputerOption.Click += BT_playerVsComputerOption_Click;
            // 
            // BT_SelectGamemodeBack
            // 
            BT_SelectGamemodeBack.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_SelectGamemodeBack.Location = new Point(28, 968);
            BT_SelectGamemodeBack.Name = "BT_SelectGamemodeBack";
            BT_SelectGamemodeBack.Size = new Size(177, 81);
            BT_SelectGamemodeBack.TabIndex = 5;
            BT_SelectGamemodeBack.Text = "BACK";
            BT_SelectGamemodeBack.UseVisualStyleBackColor = true;
            BT_SelectGamemodeBack.Click += BT_SelectGamemodeBack_Click;
            // 
            // GamemodeSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1463, 1061);
            Controls.Add(BT_SelectGamemodeBack);
            Controls.Add(BT_playerVsComputerOption);
            Controls.Add(BT_twoPlayersOption);
            Controls.Add(LB_GamemodeSelectionTitle);
            Name = "GamemodeSelection";
            Text = "GamemodeSelection";
            Load += GamemodeSelection_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB_GamemodeSelectionTitle;
        private Button BT_twoPlayersOption;
        private Button BT_playerVsComputerOption;
        private Button BT_SelectGamemodeBack;
    }
}