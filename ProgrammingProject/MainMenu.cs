using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingProject
{
    public partial class MainMenu : Form
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            SetFullScreen();
        }

        private void LB_MainMenuTitle_Click(object sender, EventArgs e)
        {

        }

        private void BT_helpMenu_Click(object sender, EventArgs e)
        {
            HelpMenu helpMenu = new HelpMenu();
            helpMenu.Show(); // This displays the help menu layout onto the screen.
            this.Hide(); // This hides the main menu layout.
        }

        private void BT_leaderboard_Click(object sender, EventArgs e)
        {
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.Show(); // This displays the leaderboard layout onto the screen
            this.Hide(); // This hides the main menu layout.
        }

        private void SetFullScreen()  // This code automatically sets the layout to fit the size of the user's screen.
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void BT_mainMenuClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit game?", "Would you like to close the game?", MessageBoxButtons.YesNo); // This will provide a prompt and ask the user(s) whether or not they want to close the game.
            if (result == DialogResult.Yes)
            {
                Thread.Sleep(2000); // Creates a two second delay before the application closes.
                Application.Exit(); // This allows the program to terminate safely.
            }
        }

        private void BT_play_Click(object sender, EventArgs e)
        {
            GamemodeSelection gamemodeSelection = new GamemodeSelection(); 
            gamemodeSelection.Show();  // This displays the gamemode selction layout onto the screen.
            this.Hide();  // This hides the main menu layout.
        }
    }
}
/**/