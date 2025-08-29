using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingProject
{
    public partial class EnterPlayerNamesTwoPlayer : Form
    {

         string playerOneName;
         string playerTwoName;

        public EnterPlayerNamesTwoPlayer()
        {
            InitializeComponent();
        }

        private void EnterPlayerNamesTwoPlayer_Load(object sender, EventArgs e)
        {
            SetFullScreen();

            playerOneName = "";
            playerTwoName = "";
            TB_EnterP1NameVsPlayer.Text = "";
            TB_EnterP2NameVsPlayer.Text = "";
        }

        private void SetFullScreen() // This code automatically sets the layout to fit the size of the user's screen
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void TB_EnterP1NameVsPlayer_TextChanged(object sender, EventArgs e)
        {
            playerOneName = TB_EnterP1NameVsPlayer.Text;
            string entry = playerOneName;
            File.WriteAllText("playerOneName.txt", entry);
        }

        private void TB_EnterP2NameVsPlayer_TextChanged(object sender, EventArgs e)
        {
            playerTwoName = TB_EnterP2NameVsPlayer.Text;
            string entry = playerTwoName;
            File.WriteAllText("playerTwoName.txt", entry);
        }

        private void BT_startPlayervsPlayerGame_Click(object sender, EventArgs e)
        {
            if (playerOneName != "" && playerTwoName != "")
            {
                PlayerVsPlayerGameBoard playerVsPlayerGameBoard = new PlayerVsPlayerGameBoard();
                playerVsPlayerGameBoard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please, enter your player names."); 
            }
        }
    }
}
