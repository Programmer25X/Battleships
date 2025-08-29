using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingProject
{
    public partial class HelpMenu : Form
    {
        public HelpMenu()
        {
            InitializeComponent();
        }
        private void HelpMenu_Load(object sender, EventArgs e)
        {
            SetFullScreen(); // This procedure sets the layout fullscreen
        }

        private void BT_helpBack_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show(); // This shows the main menu layout on screen
            this.Hide(); //  This hides the help menu layout
        }
        private void SetFullScreen() // This code automatically sets the layout to fit the size of the user's screen
        {
            FormBorderStyle = FormBorderStyle.None; // This stops the form layout from being resized. 
            WindowState = FormWindowState.Maximized; // This maximises the form layout so that it is fullscreen
            TopMost = true; // This makes the layout appear above the other layouts when the user(s) is/are using it.
        }

        private void lb_HowToPlay_Click(object sender, EventArgs e)
        {

        }
    }
}
