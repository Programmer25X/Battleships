using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingProject
{
    public partial class EnterPlayerNamesVsCPU : Form
    {
        string playerOneName;

        public EnterPlayerNamesVsCPU()
        {
            InitializeComponent();
        }

        private void EnterPlayerNames_Load(object sender, EventArgs e)
        {
            SetFullScreen();
            playerOneName = ""; 
            TB_EnterP1NameVsCPU.Text = "";
        }

        private void SetFullScreen() // This code automatically sets the layout to fit the size of the user's screen
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void BT_startPlayerVsCPUGame_Click(object sender, EventArgs e)
        {
            if (playerOneName != "")
            {
                VsCPUGameboard vsCPUGameBoard = new VsCPUGameboard();
                vsCPUGameBoard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please, enter your player name");
            }
        }

        private void TB_EnterP1NameVsCPU_TextChanged(object sender, EventArgs e)
        { 
            playerOneName = TB_EnterP1NameVsCPU.Text;
            string entry = playerOneName;
            File.WriteAllText("playerOneName.txt", entry); // Writes the player's name to the 'playerOneName.txt' file

        }
    }
}
