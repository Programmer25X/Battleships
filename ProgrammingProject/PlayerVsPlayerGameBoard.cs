using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace ProgrammingProject
{
    public partial class PlayerVsPlayerGameBoard : Form
    {
        private const int gridSize = 10;

        int playerCurrentlyPlaying = 1;

        int p1ShipOneParts = 5;
        int p1ShipTwoParts = 5;
        int p1ShipThreeParts = 5;
        int p1ShipFourParts = 5;
        int p1ShipFiveParts = 5;

        int p2ShipOneParts = 5;
        int p2ShipTwoParts = 5;
        int p2ShipThreeParts = 5;
        int p2ShipFourParts = 5;
        int p2ShipFiveParts = 5;

        string[,] playerOneGrid = new string[10, 10];
        string[,] playerTwoGrid = new string[10, 10];
        string addToLeaderboardFilepath = "scores.txt";

        List<Player> playerList = new List<Player>();

        PictureBox[] playerOneHearts = new PictureBox[5];
        PictureBox[] playerTwoHearts = new PictureBox[5];

        List<Button> playerOneBoard = new List<Button>();
        List<Button> playerTwoBoard = new List<Button>();


        public PlayerVsPlayerGameBoard()
        {
            InitializeComponent();

        }

        private void PlayerVsPlayerGameBoard_Load(object sender, EventArgs e)
        {
            string playerOneName = File.ReadAllText("playerOneName.txt").ToString();  // This retrieves Player One's name from the 'playerOneName.txt' file.
            string playerTwoName = File.ReadAllText("playerTwoName.txt").ToString(); // This retrieves Player Two's name from the 'playerTwoName.txt' file.

            playerList.Add(new Player(playerOneName, 0, 0, 4));
            playerList.Add(new Player(playerTwoName, 0, 0, 4));

            LB_Player1Name.Text = playerList[0].GetPlayerName().ToString();
            LB_Player2Name.Text = playerList[1].GetPlayerName().ToString();
            lb_player1Score.Text = $"Score: {playerList[0].GetScore()}";
            lb_player2Score.Text = $"Score: {playerList[1].GetScore()}";


            // Player One heart pictureboxes are set to an individual index in an array.
            playerOneHearts[0] = PB_p1Heart1;
            playerOneHearts[1] = PB_p1Heart2;
            playerOneHearts[2] = PB_p1Heart3;
            playerOneHearts[3] = PB_p1Heart4;
            playerOneHearts[4] = PB_p1Heart5;

            // Player Tne heart pictureboxes are set to an individual index in an array.
            playerTwoHearts[0] = PB_p2Heart1;
            playerTwoHearts[1] = PB_p2Heart2;
            playerTwoHearts[2] = PB_p2Heart3;
            playerTwoHearts[3] = PB_p2Heart4;
            playerTwoHearts[4] = PB_p2Heart5;

            SetFullScreen();
            CreatePlayerOneBoard();
            CreatePlayerTwoBoard();
            PlaceShips();

            Random random = new Random();
            playerCurrentlyPlaying = random.Next(0, 2); // Randomly generates a number betwwen 0 and 1 to determine who makes the first move.
            UpdateTurn();


        }

        private void CreatePlayerOneBoard()
        {
            const int squareSize = 50;
            const int spacing = 5;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button playerOneSquare = new Button();
                    playerOneSquare.Size = new System.Drawing.Size(squareSize, squareSize);
                    playerOneSquare.Location = new System.Drawing.Point(col * (squareSize + spacing) + 100, row * (squareSize + spacing) + 200); // This spaces out the buttons that are created to form the grid.
                    playerOneSquare.Name = "(" + (row).ToString() + "," + (col).ToString() + ")"; // This uses the local row and col variables to give the button a name by converting their assigned values to a string.
                    playerOneSquare.Click += AttackPlayerOneShips; // This is what procedure is called when one of the buttons is clicked.
                    playerOneBoard.Add(playerOneSquare); // This addds the playerOneSquare to playerOneBoard.
                    Controls.Add(playerOneSquare);
                    playerOneSquare.BackColor = Color.LightSeaGreen; // This changes the button colour to sea-green.
                    playerOneSquare.Enabled = true; // This prevents the player from clicking on the opponent's grid by disabling every button.

                }
            }
        }

        private void CreatePlayerTwoBoard()
        {
            const int squareSize = 50;
            const int spacing = 5;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button playerTwoSquare = new Button();
                    playerTwoSquare.Size = new System.Drawing.Size(squareSize, squareSize);
                    playerTwoSquare.Location = new System.Drawing.Point(col * (squareSize + spacing) + 1000, row * (squareSize + spacing) + 200); // This spaces out the buttons that are created to form the grid.
                    playerTwoSquare.Name = "(" + (row).ToString() + "," + (col).ToString() + ")"; // This uses the local row and col variables to give the button a name by converting their assigned values to a string.
                    playerTwoSquare.Click += AttackPlayerTwoShips; // This is what procedure is called when one of the buttons is clicked.
                    Controls.Add(playerTwoSquare);
                    playerTwoBoard.Add(playerTwoSquare); // This adds the playerTwoSquare to the playerTwoBoard.
                    playerTwoSquare.BackColor = Color.LightSeaGreen; // This changes the button colour to sea-green.
                    playerTwoSquare.Enabled = true; // This prevents the player from clicking on the opponent's grid by disabling every button.
                }
            }
        }

        private void CheckWin() // This procedure checks whether the Player 1 or PLayer 2 has won.
        {
            string winner; // Stores the value name of the winner.

            if (playerList[0].GetNumberOfShips() == 0) // Checks if all of Player 1's ships have been destroyed.
            {
                winner = playerList[1].GetPlayerName(); // If all of Player 1's ships have been destroyed, Player 2 wins.
                lb_announcement.Text = $"{winner} wins!"; // The UI updates to inform the players that Player 2 won the game.
                playerList[1].CalculateleaderboardScore();
                string entry = $"{playerList[1].GetPlayerName()},{playerList[1].GetLeaderboardScore()}"; // This adds calulates Player 2's final score.
                File.AppendAllLines(addToLeaderboardFilepath, new string[] { entry }); // This adds Player 2's name and final score to the text file.
            }
            else if (playerList[1].GetNumberOfShips() == 0)  // Checks if all of Player 2's ships have been destroyed.
            {

                winner = playerList[0].GetPlayerName(); // If all of Player 2's ships have been destroyed, Player 1 wins.
                lb_announcement.Text = $"{winner} wins!"; // The UI updates to inform the players that Player 1 won the game. 
                playerList[0].CalculateleaderboardScore();  // This adds calulates Player 1's final score.
                string entry = $"{playerList[0].GetPlayerName()},{playerList[0].GetLeaderboardScore()}";
                File.AppendAllLines(addToLeaderboardFilepath, new string[] { entry }); // This adds Player 1's name and final score to the text file.
            }

            if ((playerList[0].GetNumberOfShips() == 0) || playerList[1].GetNumberOfShips() == 0) // Checks whether either Player 1 or Player 2 has won.
            {
                for (int i = 0; i < playerTwoBoard.Count; i++)
                {
                    playerOneBoard[i].Enabled = false;
                    playerTwoBoard[i].Enabled = false; // Disables the buttons that the player can interact with as the game has ended. 
                }
            }

        }

        private void SetFullScreen() // This procedure automatically sets the layout to fit the size of the user's screen.
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void PB_p1Heart2_Click(object sender, EventArgs e)
        {

        }

        private void BT_PlayervsPlayerClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Have you made sure that you have saved your game?\nWould you like to exit the game?", "Would you like to close the game?", MessageBoxButtons.YesNo); // This will provide a prompt and ask the user(s) whether or not they want to close the game
            if (result == DialogResult.Yes)
            {
                Thread.Sleep(2000); // Creates a two second delay before the application closes
                Application.Exit(); // This allows the program to terminate safely
            }
        }

        private void AttackPlayerOneShips(object sender1, EventArgs e1)
        {
            if (playerCurrentlyPlaying == 1 && playerList[0].GetNumberOfShips() > 0)
            {
                Button clickedButton = sender1 as Button;

                if (clickedButton != null)
                {
                    int row = int.Parse(clickedButton.Name.Substring(1, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's y-coordinate can be found.
                    int column = int.Parse(clickedButton.Name.Substring(3, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's x-coordinate can be found.
                    MessageBox.Show($"{playerList[1].GetPlayerName()} clicked on:\nRow: {row + 1}\nColumn: {column + 1}"); // Using the coordinate values, I display a message on screen to inform the user about their selected square.

                    if (playerCurrentlyPlaying == 1 && (playerOneGrid[row, column] == "ship_one" || playerOneGrid[row, column] == "ship_two" || playerOneGrid[row, column] == "ship_three" || playerOneGrid[row, column] == "ship_four" || playerOneGrid[row, column] == "ship_five")) // Checks whether a valid move has been made by the player and whether a ship was hit.
                    {
                        MessageBox.Show($"{playerList[1].GetPlayerName()} hit {playerList[0].GetPlayerName()}'s boat!"); // This informs the player that they have successfully hit one of the CPU's ships.

                        if (playerOneGrid[row, column] != null) // This checks whether one of the CPU's boats has been hit.
                        {
                            if (playerOneGrid[row, column] == "ship_one")
                            {
                                MessageBox.Show($"{playerList[1].GetPlayerName()} hit boat one!");
                                p1ShipOneParts--;
                                playerOneGrid[row, column] = "hit";
                                if (p1ShipOneParts == 0)
                                {
                                    playerOneHearts[0].Visible = false; // This removes one of Player 1's hearts to indicate that one of its ships has been destroyed.
                                    playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                                    playerList[0].DestroyShip(); // Decreases the number of ships Player 1 has by one. .
                                }
                            }
                            else if (playerOneGrid[row, column] == "ship_two")
                            {
                                MessageBox.Show($"{playerList[1].GetPlayerName()} hit boat two!");
                                p1ShipTwoParts--;
                                playerOneGrid[row, column] = "hit";
                                if (p1ShipTwoParts == 0)
                                {
                                    playerOneHearts[1].Visible = false; // This removes one of Player 1's hearts to indicate that one of its ships has been destroyed
                                    playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                                    playerList[0].DestroyShip(); // // Decreases the number of ships Player 1 has by one. 
                                }
                            }
                            else if (playerOneGrid[row, column] == "ship_three")
                            {
                                MessageBox.Show($"{playerList[1].GetPlayerName()} hit boat three!");
                                p1ShipThreeParts--;
                                playerOneGrid[row, column] = "hit";
                                if (p1ShipThreeParts == 0)
                                {
                                    playerOneHearts[2].Visible = false; // This removes one of Player 1's hearts to indicate that one of its ships has been destroyed.
                                    playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                                    playerList[0].DestroyShip(); // Decreases the number of ships Player 1 has by one.    
                                }
                            }
                            else if (playerOneGrid[row, column] == "ship_four")
                            {
                                MessageBox.Show($"{playerList[1].GetPlayerName()} hit boat four!");
                                p1ShipFourParts--;
                                playerOneGrid[row, column] = "hit";
                                if (p1ShipFourParts == 0)
                                {
                                    playerOneHearts[3].Visible = false; // This removes one of Player 1's hearts to indicate that one of its ships has been destroyed.
                                    playerList[0].RemoveHeart();// This changes the index that needs to be accessed in 'playerOneHearts'.
                                    playerList[0].DestroyShip(); // Decreases the number of ships Player 1 has by one. 
                                }
                            }
                            else if (playerOneGrid[row, column] == "ship_five")
                            {
                                MessageBox.Show($"{playerList[1].GetPlayerName()} hit boat five!");
                                p1ShipFiveParts--;
                                playerOneGrid[row, column] = "hit";
                                if (p1ShipFiveParts == 0)
                                {
                                    playerOneHearts[4].Visible = false; // This removes one of Player 1's hearts to indicate that one of its ships has been destroyed.
                                    playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'
                                    playerList[0].DestroyShip(); // Decreases the number of ships Player 1 has by one. 
                                }
                            }
                        }
                        clickedButton.BackColor = Color.Red; // This turns the selected buttom red.
                        clickedButton.Enabled = false;
                        playerList[1].AddScoreForSuccessfulHit(); // This adds score to the player's total score.
                        lb_player1Score.Text = $"Score: {playerList[1].GetScore()}";  // Updates Player 2's score on the screen.
                    }
                    else
                    {
                        MessageBox.Show($"{playerList[1].GetPlayerName()} missed!"); // Informs the user that Player 2 missed.
                        clickedButton.BackColor = Color.Blue; // The clicked button turns blue.
                        clickedButton.Enabled = false; // The clicked button is disabled so it can't be selected again.
                        playerOneGrid[row, column] = "selected";
                        playerList[1].AddAttempt(); // Increases the number of attempts Player 2 has had by one.
                        lb_player2Score.Text = $"Score: {playerList[1].GetScore()}"; // Updates Player 2's score on the screen.
                        playerCurrentlyPlaying = 0;
                        lb_announcement.Text = $"{playerList[0].GetPlayerName()}'s turn"; // Infroms the players that it's Player 1's turn.
                        UpdateTurn(); // This allows the players to change turns.

                    }

                    CheckWin(); // This procedure checks whether Player 1 or Player 2 has won.
                }
            }
        }

        private void AttackPlayerTwoShips(object sender2, EventArgs e)
        {
            if (playerCurrentlyPlaying == 0 && playerList[1].GetNumberOfShips() > 0)
            {
                Button clickedButton = sender2 as Button;

                if (clickedButton != null)
                {
                    int row = int.Parse(clickedButton.Name.Substring(1, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's y-coordinate can be found.
                    int column = int.Parse(clickedButton.Name.Substring(3, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's x-coordinate can be found.
                    MessageBox.Show($"{playerList[0].GetPlayerName()} clicked on:\nRow: {row + 1}\nColumn: {column + 1}"); // Using the coordinate values, I display a message on screen to inform the user about their selected square.

                    if (playerCurrentlyPlaying == 0 && (playerTwoGrid[row, column] == "ship_one" || playerTwoGrid[row, column] == "ship_two" || playerTwoGrid[row, column] == "ship_three" || playerTwoGrid[row, column] == "ship_four" || playerTwoGrid[row, column] == "ship_five")) // Checks whether a valid move has been made by the player and whether a ship was hit.
                    {
                        MessageBox.Show($"{playerList[0].GetPlayerName()} hit {playerList[1].GetPlayerName()}'s boat!"); // This informs the player that they have successfully hit one of the CPU's ships.

                        if (playerTwoGrid[row, column] != null) // This checks whether one of Player 2's boats has been hit.
                        {
                            if (playerTwoGrid[row, column] == "ship_one")
                            {
                                MessageBox.Show($"{playerList[0].GetPlayerName()} hit boat one!");
                                p2ShipOneParts--;
                                playerTwoGrid[row, column] = "hit";
                                if (p2ShipOneParts == 0)
                                {
                                    playerTwoHearts[0].Visible = false;  // This removes one of Player 2's hearts to indicate that one of its ships has been destroyed.
                                    playerList[1].RemoveHeart(); // This changes the index that needs to be accessed in 'playerTwoHearts'.
                                    playerList[1].DestroyShip(); // Decreases the number of ships Player 2 has by one.
                                }
                            }
                            else if (playerTwoGrid[row, column] == "ship_two")
                            {
                                MessageBox.Show($"{playerList[0].GetPlayerName()} hit boat two!");
                                p2ShipTwoParts--;
                                playerTwoGrid[row, column] = "hit";
                                if (p2ShipTwoParts == 0)
                                {
                                    playerTwoHearts[1].Visible = false; // This removes one of Player 2's hearts to indicate that one of its ships has been destroyed.
                                    playerList[1].RemoveHeart(); // This changes the index that needs to be accessed in 'playerTwoHearts'.
                                    playerList[1].DestroyShip(); // Decreases the number of ships Player 2 has by one.
                                }
                            }
                            else if (playerTwoGrid[row, column] == "ship_three")
                            {
                                MessageBox.Show($"{playerList[0].GetPlayerName()} hit boat three!");
                                p2ShipThreeParts--;
                                playerTwoGrid[row, column] = "hit";
                                if (p2ShipThreeParts == 0)
                                {
                                    playerTwoHearts[2].Visible = false;  // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    playerList[1].RemoveHeart(); // This changes the index that needs to be accessed in 'playerTwoHearts'.
                                    playerList[1].DestroyShip(); // Decreases the number of ships Player 2 has by one. 
                                }
                            }
                            else if (playerTwoGrid[row, column] == "ship_four")
                            {
                                MessageBox.Show($"{playerList[0].GetPlayerName()} hit boat four!");
                                p2ShipFourParts--;
                                playerTwoGrid[row, column] = "hit";
                                if (p2ShipFourParts == 0)
                                {
                                    playerTwoHearts[3].Visible = false; // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    playerList[1].RemoveHeart(); // This changes the index that needs to be accessed in 'playerTwoHearts'.
                                    playerList[1].DestroyShip(); // Decreases the number of ships Player 2 has by one.
                                }
                            }
                            else if (playerTwoGrid[row, column] == "ship_five")
                            {
                                MessageBox.Show($"{playerList[0].GetPlayerName()} hit boat five!");
                                playerTwoGrid[row, column] = "hit";
                                p2ShipFiveParts--;
                                if (p2ShipFiveParts == 0)
                                {
                                    playerTwoHearts[4].Visible = false; // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    playerList[1].RemoveHeart(); // This changes the index that needs to be accessed in 'playerTwoHearts'.
                                    playerList[1].DestroyShip(); // Decreases the number of ships Player 2 has by one.
                                }
                            }
                        }
                        clickedButton.BackColor = Color.Red; // This turns the selected buttom red.
                        clickedButton.Enabled = false;
                        playerList[0].AddScoreForSuccessfulHit(); // This adds score to the player's total score.
                        lb_player1Score.Text = $"Score: {playerList[0].GetScore()}";  // Updates Player 1's score on the screen.
                    }
                    else
                    {
                        MessageBox.Show($"{playerList[0].GetPlayerName()} missed!"); // Informs the user that Player 1 missed.
                        clickedButton.BackColor = Color.Blue;  // The clicked button turns blue.
                        clickedButton.Enabled = false; // The clicked button is disabled so it can't be selected again.
                        playerTwoGrid[row, column] = "selected";
                        playerList[0].AddAttempt(); // Increases the number of attempts Player 1 has had by one.
                        lb_player1Score.Text = $"Score: {playerList[0].GetScore()}";  // Updates Player 1's score on the screen.
                        playerCurrentlyPlaying = 1;
                        lb_announcement.Text = $"{playerList[1].GetPlayerName()}'s turn"; // Infroms the players that it's Player 2's turn.
                        UpdateTurn(); // This allows the players to change turns.
                    }

                    CheckWin(); // This procedure checks whether Player 1 or Player 2 has won.
                }
            }
        }

        void PlaceShips()
        {
            while (playerList[0].GetNumberOfShips() < 5)
            {
                Random random = new Random();
                int random_row, random_col, positionCol = 0, positionRow = 0;
                random_row = random.Next(0, 10); // This generates a random y-coordinate between 0 and 9.
                random_col = random.Next(0, 10); // This generates a random x-coordinate between 0 and 9.
                positionRow = random_row;
                positionCol = random_col;
                string coordinate = positionRow.ToString() + positionCol.ToString();

                if (positionCol < 6 && playerOneGrid[positionRow, positionCol] == null && playerOneGrid[positionRow, positionCol + 1] == null && playerOneGrid[positionRow, positionCol + 2] == null && playerOneGrid[positionRow, positionCol + 3] == null && playerOneGrid[positionRow, positionCol + 4] == null) // This checks whether the coordinate is already occupied by one of the CPU's boats.
                {
                    for (int i = 0; i < 5; i++)
                    {
                        //  playerOneBoard[Convert.ToInt32(coordinate) + i].BackColor = Color.Orange; (For testing purposes)

                        if (playerList[0].GetNumberOfShips() == 0)
                        {
                            playerOneGrid[positionRow, positionCol + i] = "ship_one"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[0].GetNumberOfShips() == 1)
                        {
                            playerOneGrid[positionRow, positionCol + i] = "ship_two"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[0].GetNumberOfShips() == 2)
                        {
                            playerOneGrid[positionRow, positionCol + i] = "ship_three"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[0].GetNumberOfShips() == 3)
                        {
                            playerOneGrid[positionRow, positionCol + i] = "ship_four"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[0].GetNumberOfShips() == 4)
                        {
                            playerOneGrid[positionRow, positionCol + i] = "ship_five"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                    }

                    playerList[0].AddShip();
                }
            }

            while (playerList[1].GetNumberOfShips() < 5)
            {
                Random random = new Random();
                int random_row, random_col, positionCol = 0, positionRow = 0;

                random_row = random.Next(0, 10); // This generates a random y-coordinate between 0 and 9.
                random_col = random.Next(0, 10); // This generates a random x-coordinate between 0 and 9.
                positionRow = random_row;
                positionCol = random_col;
                string coordinate = positionRow.ToString() + positionCol.ToString();


                if (positionCol < 6 && playerTwoGrid[positionRow, positionCol] == null && playerTwoGrid[positionRow, positionCol + 1] == null && playerTwoGrid[positionRow, positionCol + 2] == null && playerTwoGrid[positionRow, positionCol + 3] == null && playerTwoGrid[positionRow, positionCol + 4] == null) // This checks whether the coordinate is already occupied by one of the CPU's boats.
                {
                    for (int i = 0; i < 5; i++)
                    {
                       // playerTwoBoard[Convert.ToInt32(coordinate) + i].BackColor = Color.Orange; (For testing purposes)

                        if (playerList[1].GetNumberOfShips() == 0)
                        {
                            playerTwoGrid[positionRow, positionCol + i] = "ship_one"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[1].GetNumberOfShips() == 1)
                        {
                            playerTwoGrid[positionRow, positionCol + i] = "ship_two"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[1].GetNumberOfShips() == 2)
                        {
                            playerTwoGrid[positionRow, positionCol + i] = "ship_three"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[1].GetNumberOfShips() == 3)
                        {
                            playerTwoGrid[positionRow, positionCol + i] = "ship_four"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (playerList[1].GetNumberOfShips() == 4)
                        {
                            playerTwoGrid[positionRow, positionCol + i] = "ship_five"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                    }

                    playerList[1].AddShip();
                }
            }
        }

        void UpdateTurn()
        {
            /* if (playerCurrentlyPlaying == 0) // Checks whether it's Player 1's turn
             {
                 for (int i = 0; i < playerOneBoard.Count; i++)
                 {
                     playerOneBoard[i].Enabled = false; // Disables the buttons in Player 1's grid so they can't be attacked during their turn.
                     playerTwoBoard[i].Enabled = true; // Enables the buttons in Player 2's grid so Player 1 can attack.
                 }

             }

             if (playerCurrentlyPlaying == 1)
             {
                 for (int i = 0; i < playerOneBoard.Count; i++)
                 {
                     playerOneBoard[i].Enabled = true; // Disables the buttons in Player 3's grid so they can't be attacked during their turn.
                     playerTwoBoard[i].Enabled = false;  // Enables the buttons in Player 1's grid so Player 2 can attack.
                 }
             } */

            lb_announcement.Text = $"{playerList[playerCurrentlyPlaying].GetPlayerName()}'s turn"; // This informs the player's whose current turn it is.
        }

        private void BT_PlayerVsPlayerSave_Click(object sender, EventArgs e)
        {
            try
            {
                string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";  // Where the state of the game is saved.
                string playerVsPlayerGameSaveName = "SaveNamePlayerVsPlayerGame.txt"; // Where the player's name is saved.

                File.Delete(playerVsPlayerGameSaveState); // The older version of the SaveFilePlayerVsPlayerGameState.txt is deleted so that they can be replaced by the most recent version.
                File.Delete(playerVsPlayerGameSaveName);  // The older version of the SaveNamePlayerVsPlayerGame.txt is deleted so that they can be replaced by the most recent version.

                File.AppendAllLines(playerVsPlayerGameSaveName, new string[] { playerList[0].GetPlayerName() }); // Records Player One's name into SaveNamePlayerVsPlayerGame.txt. 
                File.AppendAllLines(playerVsPlayerGameSaveName, new string[] { playerList[1].GetPlayerName() });  // Records Player Two's name into SaveNamePlayerVsPlayerGame.txt. 

                for (int row = 0; row < gridSize; row++) // Saves the state of each square on Player One's board into the file and appends each square a corresponding value in a loop.
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if ((playerOneGrid[row, col] == "ship_one" || playerOneGrid[row, col] == "ship_two" || playerOneGrid[row, col] == "ship_three" || playerOneGrid[row, col] == "ship_four" || playerOneGrid[row, col] == "ship_five") && playerOneGrid[row, col] != null)
                        {

                            switch (playerOneGrid[row, col])
                            {
                                case "ship_one":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_ship_one" }); // Records that the square/button is part of ship one into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_two":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_ship_two" }); // Records that the square/button is part of ship two into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_three":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_ship_three" }); // Records that the square/button is part of ship three into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_four":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_ship_four" });  // Records that the square/button is part of ship four into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_five":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_ship_five" }); // Records that the square/button is part of ship five into SaveFilePlayerVsPlayerGameState.txt.
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (playerOneGrid[row, col] == null)
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_button_enabled" });  // Records that the square/button is not part of a ship but is enabled into SaveFilePlayerVsPlayerGameState.txt.
                        }
                        else if (playerOneGrid[row, col] == "selected")
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_button_disabled" });  // Records that the square/button is not part of a ship and is disabled into SaveFilePlayerVsPlayerGameState.txt.
                        }
                        else if (playerOneGrid[row, col] == "hit")
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerOne_hit_button_disabled" }); // Records that the square/button is part of ship five but has been hit into SaveFilePlayerVsPlayerGameState.txt.
                        }
                    }
                }

                for (int row = 0; row < gridSize; row++) // Saves the state of each square on Player Two's board into the file and appends each square a corresponding value in a loop.
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if (playerTwoGrid[row, col] == "ship_one" || playerTwoGrid[row, col] == "ship_two" || playerTwoGrid[row, col] == "ship_three" || playerTwoGrid[row, col] == "ship_four" || playerTwoGrid[row, col] == "ship_five" && playerTwoGrid[row, col] != null)
                        {

                            switch (playerTwoGrid[row, col])
                            {
                                case "ship_one":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_ship_one" }); // Records that the square/button is part of ship one into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_two":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_ship_two" }); // Records that the square/button is part of ship two into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_three":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_ship_three" });  // Records that the square/button is part of ship three into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_four":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_ship_four" });  // Records that the square/button is part of ship four into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                case "ship_five":
                                    File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_ship_five" }); // Records that the square/button is part of ship five into SaveFilePlayerVsPlayerGameState.txt.
                                    break;

                                default:
                                    break;
                            }
                        }
                        else if (playerTwoGrid[row, col] == null)
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_button_enabled" });  // Records that the square/button is not part of a ship but is enabled into SaveFilePlayerVsPlayerGameState.txt.
                        }
                        else if (playerTwoGrid[row, col] == "selected")
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_button_disabled" }); // Records that the square/button is not part of a ship and is disabled into SaveFilePlayerVsPlayerGameState.txt.
                        }
                        else if (playerTwoGrid[row, col] == "hit")
                        {
                            File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { "playerTwo_hit_button_disabled" });  // Records that the square/button is part of ship five but has been hit into SaveFilePlayerVsPlayerGameState.txt.
                        }
                    }
                }

                // Saving the amount of damage each of Player One's ships have taken.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p1ShipOneParts.ToString() }); // Saves the number of parts ship one has into SaveFilePlayerVsPlayerGameState.txt (Player One).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p1ShipTwoParts.ToString() }); // Saves the number of parts ship two has into SaveFilePlayerVsPlayerGameState.txt (Player One).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p1ShipThreeParts.ToString() }); // Saves the number of parts ship three has into SaveFilePlayerVsPlayerGameState.txt (Player One).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p1ShipFourParts.ToString() });  // Saves the number of parts ship four has into SaveFilePlayerVsPlayerGameState.txt (Player One).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p1ShipFiveParts.ToString() });  // Saves the number of parts ship five has into SaveFilePlayerVsPlayerGameState.txt (Player One).

                // Saving the amount of damage each of Player Two's ships have taken.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p2ShipOneParts.ToString() }); // Saves the number of parts ship one has into SaveFilePlayerVsPlayerGameState.txt (Player Two).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p2ShipTwoParts.ToString() }); // Saves the number of parts ship two has into SaveFilePlayerVsPlayerGameState.txt (Player Two).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p2ShipThreeParts.ToString() }); // Saves the number of parts ship three has into SaveFilePlayerVsPlayerGameState.txt (Player Two).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p2ShipFourParts.ToString() }); // Saves the number of parts ship four has into SaveFilePlayerVsPlayerGameState.txt (Player Two).
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { p2ShipFiveParts.ToString() }); // Saves the number of parts ship five has into SaveFilePlayerVsPlayerGameState.txt (Player Two).

                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[0].GetHearts().ToString() }); // Saves how many hearts Player One has into SaveFilePlayerVsPlayerGameState.txt.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[1].GetHearts().ToString() });  // Saves how many hearts Player Two has into SaveFilePlayerVsPlayerGameState.txt.

                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[0].GetNumberOfAttempts().ToString() }); // Saves how many attempts Player One has had into SaveFilePlayerVsPlayerGameState.txt.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[1].GetNumberOfAttempts().ToString() }); // Saves how many attempts Player Two has had into SaveFilePlayerVsPlayerGameState.txt.

                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[0].GetScore().ToString() }); // Saves Player One's score into SaveFilePlayerVsPlayerGameState.txt.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[1].GetScore().ToString() }); // Saves Player Two's score into SaveFilePlayerVsPlayerGameState.txt.

                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[0].GetNumberOfShips().ToString() }); // Saves the number of ships Player One has into SaveFilePlayerVsPlayerGameState.txt.
                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerList[1].GetNumberOfShips().ToString() });  // Saves the number of ships Player Two has into SaveFilePlayerVsPlayerGameState.txt.

                File.AppendAllLines(playerVsPlayerGameSaveState, new string[] { playerCurrentlyPlaying.ToString() }); // Saves whose turn it is into SaveFilePlayerVsPlayerGameState.txt.

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString()); // Displays the cause of an error if one occurs.
            }
        }

        private void BT_PlayerVsPlayerLoad_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets each button on Player One's board to light-sea green.
                playerTwoBoard[i].BackColor = Color.LightSeaGreen; // Sets each button on Player Two's board to light-sea green.
            }

            try
            {
                List<string> saveData = new List<string>();
                string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                string playerVsPlayerGameSaveName = "SaveNamePlayerVsPlayerGame.txt";

                saveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList(); // Assigns each line in SaveFilePlayerVsPlayerGameState.txt to an index in the saveData list.

                for (int i = 0; i < 100; i++)
                {
                    if (saveData[i] != null && saveData[i] == "playerOne_button_enabled") // Checks whether button is enabled and is not part of a ship.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = null; // Marks the Player One's grid at row, col to null, so Player Two can select that coordinate.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_button_disabled")  // Checks whether button is disabled and is not part of a ship. 
                    {
                        playerOneBoard[i].BackColor = Color.Blue; // Sets the colour of the button to blue.
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "selected"; // Marks the Player One's grid at row, col to selected, so that Player Two cannot select that coordinate again.
                        playerOneBoard[i].Enabled = false;
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_hit_button_disabled") // Checks whether button is disabled and is part of a ship.
                    {
                        playerOneBoard[i].BackColor = Color.Red; // Sets the colour of the button to red.
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "hit"; // Marks the Player One's grid at row, col to as having part of a ship that has been destroyed.
                        playerOneBoard[i].Enabled = false;
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_ship_one") // Checks whether button is enabled part of a ship one.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // playerOneBoard[i].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "ship_one"; // Marks Player One's grid at row, col to as having part of ship onw present.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_ship_two")  // Checks whether button is enabled part of a ship two.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // playerOneBoard[i].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "ship_two";  // Marks Player One's grid at row, col to as having part of ship two present.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_ship_three") // Checks whether button is enabled part of a ship three.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // playerOneBoard[i].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "ship_three";  // Marks Player One's grid at row, col to as having part of ship three present.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_ship_four") // Checks whether button is enabled part of a ship four.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        //playerOneBoard[i].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "ship_four"; // Marks Player One's grid at row, col to as having part of ship four present.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerOne_ship_five") // Checks whether button is enabled part of a ship five.
                    {
                        playerOneBoard[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // playerOneBoard[i].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerOneBoard[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerOneBoard[i].Name.Substring(3, 1)); // Gets the col index.
                        playerOneGrid[row, col] = "ship_five";  // Marks Player One's grid at row, col to as having part of ship five present.
                        playerOneBoard[i].Enabled = true; // The button/square is enabled.
                    }

                }

                for (int i = 100; i < 200; i++)
                {
                    if (saveData[i] != null && saveData[i] == "playerTwo_button_enabled") // Checks whether button is enabled and is not part of a ship.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = null; // Marks the  Player Two's grid at row, col to null, so the player can select that coordinate
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_button_disabled") // Checks whether button is disabled and is not part of a ship.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.Blue; // Sets the colour of the button to blue.
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "selected"; // Marks the  Player Two's grid at row, col to selected, so the player cannot select that coordinate again.
                        playerTwoBoard[i - 100].Enabled = false;
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_hit_button_disabled") // Checks whether button is disabled and is part of a ship.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.Red; // Sets the colour of the button to red.
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "hit"; // Marks  Player Two's grid at row, col to as having part of a ship that has been destroyed.
                        playerTwoBoard[i - 100].Enabled = false;
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_ship_one") // Checks whether button is enabled part of a ship one.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                      //  playerTwoBoard[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "ship_one"; // Marks Player Two's grid at row, col to as having part of ship one present.
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_ship_two") // Checks whether button is enabled part of a ship two.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // playerTwoBoard[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "ship_two"; // Marks Player Two's grid at row, col to as having part of ship two present.
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_ship_three") // Checks whether button is enabled part of a ship three.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // playerTwoBoard[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "ship_three"; // Marks Player Two's grid at row, col to as having part of ship three present.
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_ship_four") // Checks whether button is enabled part of a ship four.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // playerTwoBoard[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "ship_four"; // Marks Player Two's grid at row, col to as having part of ship four present
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                    else if (saveData[i] != null && saveData[i] == "playerTwo_ship_five") // Checks whether button is enabled part of a ship five.
                    {
                        playerTwoBoard[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // playerTwoBoard[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(playerTwoBoard[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(playerTwoBoard[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        playerTwoGrid[row, col] = "ship_five"; // Marks Player Two's grid at row, col to as having part of ship five present.
                        playerTwoBoard[i - 100].Enabled = true; // The button/square is enabled.
                    }
                }

                playerList[0].SetSavedPlayerName("vsPlayer", 1); // Retrieves Player One's saved name from SaveNamePlayerVsPlayerGame.txt.
                LB_Player1Name.Text = $"{playerList[0].GetPlayerName()}"; // Updates the text that displays Player One's name.

                playerList[1].SetSavedPlayerName("vsPlayer", 2); // Retrieves Player Two's saved name from SaveNamePlayerVsPlayerGame.txt.
                LB_Player2Name.Text = $"{playerList[1].GetPlayerName()}"; // Updates the text that displays Player Two's name.

                playerList[0].SetSavedNumberOfShips("vsPlayer", 1); // Retrieves the number of ships Player One has from SaveFilePlayerVsPlayerGameState.txt.
                playerList[0].SetSavedScore("vsPlayer", 1); // Retrieves Player One's score from SaveFilePlayerVsPlayerGameState.txt.
                lb_player1Score.Text = $"Score: {playerList[0].GetScore()}"; // Updates the text that displays Player One's score.

                playerList[1].SetSavedNumberOfShips("vsPlayer", 2); // Retrieves the number of ships Player Two has from SaveFilePlayerVsPlayerGameState.txt.
                playerList[1].SetSavedScore("vsPlayer", 2);  // Retrieves Player Two's score from SaveFilePlayerVsPlayerGameState.txt.
                lb_player2Score.Text = $"Score: {playerList[1].GetScore()}"; // Updates the text that displays Player Two's score.

                playerList[0].SetSavedAttempts("vsPlayer", 1); // Retrieves the number of attempts Player One has had from SaveFilePlayerVsPlayerGameState.txt.
                playerList[1].SetSavedAttempts("vsPlayer", 2); // Retrieves the number of attempts Player Two has had from SaveFilePlayerVsPlayerGameState.txt.

                p1ShipOneParts = int.Parse(saveData[200]); // Sets the value of p1ShipOneParts to the value on line 201 in SaveFilePlayerVsPlayerGameState.txt.
                p1ShipTwoParts = int.Parse(saveData[201]); // Sets the value of p1ShipTwoParts to the value on line 202 in SaveFilePlayerVsPlayerGameState.txt.
                p1ShipThreeParts = int.Parse(saveData[202]); // Sets the value of p1ShipThreeParts to the value on line 203 in SaveFilePlayerVsPlayerGameState.txt.
                p1ShipFourParts = int.Parse(saveData[203]); // Sets the value of p1ShipFourParts to the value on line 204 in SaveFilePlayerVsPlayerGameState.txt.
                p1ShipFiveParts = int.Parse(saveData[204]); // Sets the value of p1ShipFiveParts to the value on line 205 in SaveFilePlayerVsPlayerGameState.txt.

                p2ShipOneParts = int.Parse(saveData[205]); // Sets the value of p2ShipOneParts to the value on line 206 in SaveFilePlayerVsPlayerGameState.txt.
                p2ShipTwoParts = int.Parse(saveData[206]);  // Sets the value of p2ShipTwoParts to the value on line 207 in SaveFilePlayerVsPlayerGameState.txt.
                p2ShipThreeParts = int.Parse(saveData[207]); // Sets the value of p2ShipThreeParts to the value on line 208 in SaveFilePlayerVsPlayerGameState.txt.
                p2ShipFourParts = int.Parse(saveData[208]); // Sets the value of p2ShipFourParts to the value on line 209 in SaveFilePlayerVsPlayerGameState.txt.
                p2ShipFiveParts = int.Parse(saveData[209]); // Sets the value of p2ShipFiveParts to the value on line 210 in SaveFilePlayerVsPlayerGameState.txt.

                playerList[0].SetSavedHearts("vsPlayer", 1); // Retrieves the number of hearts Player One has from SaveFilePlayerVsPlayerGameState.txt.
                playerList[1].SetSavedHearts("vsPlayer", 2); // Retrieves the number of hearts Player Two has from SaveFilePlayerVsPlayerGameState.txt.

                // These if statements determine which hearts should be displayed on screen.
                if (p1ShipOneParts == 0)
                {
                    playerOneHearts[0].Visible = false;
                }
                else
                {
                    playerOneHearts[0].Visible = true;
                }


                if (p1ShipTwoParts == 0)
                {
                    playerOneHearts[1].Visible = false;
                }
                else
                {
                    playerOneHearts[1].Visible = true;
                }


                if (p1ShipThreeParts == 0)
                {
                    playerOneHearts[2].Visible = false;
                }
                else
                {
                    playerOneHearts[2].Visible = true;
                }


                if (p1ShipFourParts == 0)
                {
                    playerOneHearts[3].Visible = false;

                }
                else
                {
                    playerOneHearts[3].Visible = true;
                }

                if (p1ShipFiveParts == 0)
                {
                    playerOneHearts[4].Visible = false;
                }
                else
                {
                    playerOneHearts[4].Visible = true;
                }

                if (p2ShipOneParts == 0)
                {
                    playerTwoHearts[0].Visible = false;
                }
                else
                {
                    playerTwoHearts[0].Visible = true;
                }

                if (p2ShipTwoParts == 0)
                {
                    playerTwoHearts[1].Visible = false;
                }
                else
                {
                    playerTwoHearts[1].Visible = true;
                }

                if (p2ShipThreeParts == 0)
                {
                    playerTwoHearts[2].Visible = false;
                }
                else
                {
                    playerTwoHearts[2].Visible = true;
                }

                if (p2ShipFourParts == 0)
                {
                    playerTwoHearts[3].Visible = false; 
                }
                else
                {
                    playerTwoHearts[3].Visible = true;
                }

                if (p2ShipFiveParts == 0)
                {
                    playerTwoHearts[4].Visible = false;
                }
                else
                {
                    playerTwoHearts[4].Visible = true;
                }

                playerCurrentlyPlaying = int.Parse(saveData[218]); // Sets the value of playerCurrentlyPlaying to the value on line 219 in SaveFilePlayerVsPlayerGameState.txt. 


                lb_announcement.Text = $"{playerList[playerCurrentlyPlaying].GetPlayerName()}'s turn"; // Updates the announcement text at the top of the screen.
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString()); // Displays the cause of an error if one occurs
            }

            BT_vsComputerLoad.Enabled = false; // Disables the button so that the game cannot be loaded again. 
        }
    }

} 
               
         




    

