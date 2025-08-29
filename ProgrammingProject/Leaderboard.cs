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
    public partial class Leaderboard : Form
    {
        public struct Scores // Record structure under the identifier 'Scores'.
        {
            public string playerName;
            public double playerScore;
        }

        Scores[] playerScores = new Scores[1000]; // This is an array of type 'Scores' under the idenifier 'playerScores'.
        List<string> readList = new List<string>(); // This is a list called 'readList' that is going to be used to access and split each line in the 'scores.txt' file.


        public Leaderboard()
        {
            InitializeComponent();
        }
        private void Leaderboard_Load(object sender, EventArgs e)
        {
            SetFullScreen(); // This sets the layout to full screen.
            SortScores(); // This procedure sorts the winning player entries and displays the top 10 highest scores on the leaderboard.
        }

        private void BT_leaderboardBack_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show(); // This displays the main menu layout onscreen.
            this.Hide(); // This hides the leaderbaord layout.
        }

        private void SetFullScreen() // This code automatically sets the layout to fit the size of the user's screen
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void Tablelay_Leaderboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SortScores() // This procedure sorts the winning player entries and displays the top 10 highest scores on the leaderboard.
        {
            try
            {
                readList= File.ReadAllLines("scores.txt").ToList(); // This adds all the entries in the 'scores.txt' file to a list.
                int count = File.ReadLines("scores.txt").Count();

                for (int i = 0; i < count; i++)
                {

                    List<string> tempList = readList[i].Split(',').ToList(); // This splits each line in the 'scores.txt' file so that the players' names and scores are sererated.
                    playerScores[i].playerName = tempList[0]; 
                    playerScores[i].playerScore = Math.Abs(Convert.ToInt32(tempList[1]));
                }

                for (int j = 1; j < playerScores.Count(); j++) // This is an insertion sort that sorts the playerScores array into desending order based on the values of the players' scores.
                {
                    int current = j;

                    while (current > 0)
                    {
                        if (playerScores[current].playerScore > playerScores[current - 1].playerScore) // This checks whether the current index has a greater value then the previous index
                        {
                            Scores temp;
                            temp = playerScores[current]; // The current index is set to temp so that the value playerScores[current] and playerScores[current-1] can be swapped.
                            playerScores[current] = playerScores[current - 1]; // Value of playerScores[current] is set to the value of playerScore[current-1].
                            playerScores[current - 1]= temp; // Then the value of playerScores[current-1] is set to the original value of playerScores[current].
                            // Now the the value of playerScores[current] and playerScores[current-1] are swapped.
                        }
                        else
                        {
                            break; // This terminates the while loops
                        }

                        current--;

                        
                    }
                }

                for(int k = 0; k < playerScores.Count(); k++)
                {
                    if (playerScores[k].playerName == "" || playerScores[k].playerName == null) // This checks whether an entry has no name becuase ten people have not yet won a game.
                    {
                        playerScores[k].playerName = "Guest"; // This sets the empty entry's name to 'Guest' to act as a placeholder.
                        playerScores[k].playerScore = 0; 
                    }
                }

                LB_1stName.Text= playerScores[0].playerName; // This displays the name of the player who achieved the current 1st best score.
                LB_1stScore.Text = playerScores[0].playerScore.ToString(); // This displays the score achievd by the current 1st best player.

                LB_2ndName.Text = playerScores[1].playerName; // This displays the name of the player who achieved the current 2nd best score.
                LB_2ndScore.Text = playerScores[1].playerScore.ToString(); // This displays the score achievd by the current 2nd best player.

                LB_3rdName.Text = playerScores[2].playerName; // This displays the name of the player who achieved the current 3rd best score.
                LB_3rdScore.Text = playerScores[2].playerScore.ToString(); // This displays the score achievd by the current 3rd best player.

                LB_4thName.Text = playerScores[3].playerName; // This displays the name of the player who achieved the current 4h best score.
                LB_4thScore.Text = playerScores[3].playerScore.ToString();// This displays the score achievd by the current 4th best player.

                LB_5thName.Text= playerScores[4].playerName; // This displays the name of the player who achieved the current 5th best score.
                LB_5thScore.Text = playerScores[4].playerScore.ToString(); // This displays the score achievd by the current 5th best player.

                LB_6thName.Text = playerScores[5].playerName; // This displays the name of the player who achieved the current 6th best score.
                LB_6thScore.Text = playerScores[5].playerScore.ToString(); // This displays the score achievd by the current 6th best player.

                LB_7thName.Text = playerScores[6].playerName; // This displays the name of the player who achieved the current 7th best score.
                LB_7thScore.Text = playerScores[6].playerScore.ToString(); // This displays the score achievd by the current 7th best player.

                LB_8thName.Text = playerScores[7].playerName; // This displays the name of the player who achieved the current 8th best score.
                LB_8thScore.Text = playerScores[7].playerScore.ToString(); // This displays the score achievd by the current 8th best player.

                LB_9thName.Text = playerScores[8].playerName; // This displays the name of the player who achieved the current 9th best score.
                LB_9thScore.Text = playerScores[8].playerScore.ToString(); // This displays the score achievd by the current 9th best player.

                LB_10thName.Text = playerScores[9].playerName; // This displays the name of the player who got the current 10th best score.
                LB_10thScore.Text = playerScores[9].playerScore.ToString(); // This displays the score achievd by the current 10th best player.

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString()); // This displays an error on screen if something is wrong.
            }
        }
    }
}
