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
    public partial class GamemodeSelection : Form
    {

        public GamemodeSelection()
        {
            InitializeComponent();
        }

        private void GamemodeSelection_Load(object sender, EventArgs e)
        {
            SetFullScreen();
        }

        private void SetFullScreen() // This code automatically sets the layout to fit the size of the user's screen.
        {
            FormBorderStyle = FormBorderStyle.None; // This stops the form layout from being resized. 
            WindowState = FormWindowState.Maximized; // This maximises the form layout so that it is fullscreen.
            TopMost = true; // This makes the layout appear above the other layouts when the user(s) is/are using it.
        }
    

    private void BT_SelectGamemodeBack_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();  // This displays the main menu onscreen.
            this.Hide(); // This hides the gamemode selction layout.
        }

        private void BT_twoPlayersOption_Click(object sender, EventArgs e)
        {
            EnterPlayerNamesTwoPlayer enterPlayerNamesTwoPlayer = new EnterPlayerNamesTwoPlayer();
            enterPlayerNamesTwoPlayer.Show();
            this.Hide(); 
        }

        private void BT_playerVsComputerOption_Click(object sender, EventArgs e)
        {
            EnterPlayerNamesVsCPU enterPlayerNamesVsCPU = new EnterPlayerNamesVsCPU();
            enterPlayerNamesVsCPU.Show(); // This displays the 'Player vs Computer' gameboard onscreen.
            this.Hide(); // This hides the gamemode selction layout.
        }
    }
}
