namespace ProgrammingProject
{
    partial class EnterPlayerNamesVsCPU
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
            TB_EnterP1NameVsCPU = new TextBox();
            BT_startPlayerVsCPUGame = new Button();
            SuspendLayout();
            // 
            // LB_EnterYourNameTitle
            // 
            LB_EnterYourNameTitle.AutoSize = true;
            LB_EnterYourNameTitle.Font = new Font("Showcard Gothic", 30F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LB_EnterYourNameTitle.Location = new Point(555, 41);
            LB_EnterYourNameTitle.Name = "LB_EnterYourNameTitle";
            LB_EnterYourNameTitle.Size = new Size(391, 50);
            LB_EnterYourNameTitle.TabIndex = 1;
            LB_EnterYourNameTitle.Text = "ENTER YOUR NAME";
            // 
            // LB_EnterP1NamePrompt
            // 
            LB_EnterP1NamePrompt.AutoSize = true;
            LB_EnterP1NamePrompt.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            LB_EnterP1NamePrompt.Location = new Point(555, 168);
            LB_EnterP1NamePrompt.Name = "LB_EnterP1NamePrompt";
            LB_EnterP1NamePrompt.Size = new Size(134, 37);
            LB_EnterP1NamePrompt.TabIndex = 2;
            LB_EnterP1NamePrompt.Text = "PLAYER 1:";
            // 
            // TB_EnterP1NameVsCPU
            // 
            TB_EnterP1NameVsCPU.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            TB_EnterP1NameVsCPU.Location = new Point(712, 168);
            TB_EnterP1NameVsCPU.Name = "TB_EnterP1NameVsCPU";
            TB_EnterP1NameVsCPU.Size = new Size(233, 43);
            TB_EnterP1NameVsCPU.TabIndex = 4;
            TB_EnterP1NameVsCPU.TextChanged += TB_EnterP1NameVsCPU_TextChanged;
            // 
            // BT_startPlayerVsCPUGame
            // 
            BT_startPlayerVsCPUGame.Location = new Point(1242, 529);
            BT_startPlayerVsCPUGame.Name = "BT_startPlayerVsCPUGame";
            BT_startPlayerVsCPUGame.Size = new Size(201, 96);
            BT_startPlayerVsCPUGame.TabIndex = 8;
            BT_startPlayerVsCPUGame.Text = "START";
            BT_startPlayerVsCPUGame.UseVisualStyleBackColor = true;
            BT_startPlayerVsCPUGame.Click += BT_startPlayerVsCPUGame_Click;
            // 
            // EnterPlayerNamesVsCPU
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1455, 637);
            Controls.Add(BT_startPlayerVsCPUGame);
            Controls.Add(TB_EnterP1NameVsCPU);
            Controls.Add(LB_EnterP1NamePrompt);
            Controls.Add(LB_EnterYourNameTitle);
            Name = "EnterPlayerNamesVsCPU";
            Text = "EnterPlayerNames";
            Load += EnterPlayerNames_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB_EnterYourNameTitle;
        private Label LB_EnterP1NamePrompt;
        private TextBox TB_EnterP1NameVsCPU;
        private Button BT_startPlayerVsCPUGame;
    }
}