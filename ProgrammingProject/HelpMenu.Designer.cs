namespace ProgrammingProject
{
    partial class HelpMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpMenu));
            LB_helpMenuTitle = new Label();
            BT_helpBack = new Button();
            lb_HowToPlay = new Label();
            SuspendLayout();
            // 
            // LB_helpMenuTitle
            // 
            LB_helpMenuTitle.AutoSize = true;
            LB_helpMenuTitle.Font = new Font("Showcard Gothic", 30F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LB_helpMenuTitle.ImageAlign = ContentAlignment.TopCenter;
            LB_helpMenuTitle.Location = new Point(797, 45);
            LB_helpMenuTitle.Name = "LB_helpMenuTitle";
            LB_helpMenuTitle.Size = new Size(127, 50);
            LB_helpMenuTitle.TabIndex = 0;
            LB_helpMenuTitle.Text = "HELP";
            // 
            // BT_helpBack
            // 
            BT_helpBack.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BT_helpBack.Location = new Point(29, 968);
            BT_helpBack.Name = "BT_helpBack";
            BT_helpBack.Size = new Size(177, 81);
            BT_helpBack.TabIndex = 1;
            BT_helpBack.Text = "BACK";
            BT_helpBack.UseVisualStyleBackColor = true;
            BT_helpBack.Click += BT_helpBack_Click;
            // 
            // lb_HowToPlay
            // 
            lb_HowToPlay.AutoSize = true;
            lb_HowToPlay.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_HowToPlay.ImageAlign = ContentAlignment.MiddleLeft;
            lb_HowToPlay.Location = new Point(287, 126);
            lb_HowToPlay.Name = "lb_HowToPlay";
            lb_HowToPlay.Size = new Size(1207, 567);
            lb_HowToPlay.TabIndex = 2;
            lb_HowToPlay.Text = resources.GetString("lb_HowToPlay.Text");
            lb_HowToPlay.TextAlign = ContentAlignment.TopCenter;
            lb_HowToPlay.Click += lb_HowToPlay_Click;
            // 
            // HelpMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1627, 1061);
            Controls.Add(lb_HowToPlay);
            Controls.Add(BT_helpBack);
            Controls.Add(LB_helpMenuTitle);
            Name = "HelpMenu";
            Text = "HelpMenu";
            Load += HelpMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB_helpMenuTitle;
        private Button BT_helpBack;
        private Label lb_HowToPlay;
    }
}