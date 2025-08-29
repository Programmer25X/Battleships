using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammingProject
{
    public partial class VsCPUGameboard : Form
    {

        private const int gridSize = 10;

        int playerCurrentlyPlaying;

        string[,] playerGrid = new string[10, 10];
        bool[,] isPlayerButtonEnabled = new bool[10, 10];
        bool[,] isCPUButtonEnabled = new bool[10, 10];
        string[,] opponentGrid = new string[10, 10];
        string addToLeaderboardFilepath = "scores.txt";

        int shipNumOneParts = 5;
        int shipNumTwoParts = 5;
        int shipNumThreeParts = 5;
        int shipNumFourParts = 5;
        int shipNumFiveParts = 5;

        int CPUShipOneParts = 5;
        int CPUShipTwoParts = 5;
        int CPUShipThreeParts = 5;
        int CPUShipFourParts = 5;
        int CPUShipFiveParts = 5;

        List<Player> playerList = new List<Player>();

        PictureBox[] playerOneHearts = new PictureBox[5];
        PictureBox[] computerHearts = new PictureBox[5];

        List<Button> boatButtonList = new List<Button>();
        List<Button> opponentButtonList = new List<Button>();


        Computer computer = new Computer("CPU", 0, 4);



        public VsCPUGameboard()
        {
            InitializeComponent();
        }

        private void Gameboard_Load(object sender, EventArgs e)
        {
            string playerOneName = File.ReadAllText("playerOneName.txt").ToString();  // This retrieves Player One's name from the 'playerOneName.txt' file.

            playerList.Add(new Player(playerOneName, 0, 0, 4));

            LB_Player1Name.Text = playerList[0].GetPlayerName().ToString();
            LB_Player2Name.Text = computer.GetComputerName().ToString();

            lb_player1Score.Text = $"Score: {playerList[0].GetScore()}";
            lb_computerScore.Text = $"Score: {computer.GetScore()}";

            SetFullScreen();
            CreatePlayerBoard();
            CreateOpponentBoard();
            GenerateComputerShips();
            lb_announcement.Text = $"{playerList[0].GetPlayerName()}, place your ships";

            // Player One heart pictureboxes are set to an individual index in an array.
            playerOneHearts[0] = PB_p1Heart1;
            playerOneHearts[1] = PB_p1Heart2;
            playerOneHearts[2] = PB_p1Heart3;
            playerOneHearts[3] = PB_p1Heart4;
            playerOneHearts[4] = PB_p1Heart5;

            // CPU heart pictureboxes are set to an individual index in an array.
            computerHearts[0] = PB_p2Heart1;
            computerHearts[1] = PB_p2Heart2;
            computerHearts[2] = PB_p2Heart3;
            computerHearts[3] = PB_p2Heart4;
            computerHearts[4] = PB_p2Heart5;

            for (int i = 0; i < 5; i++)
            {
                playerOneHearts[i].Visible = false;
                computerHearts[i].Visible = true;
            }

            BT_vsComputerLoad.Enabled = false;
            BT_vsComputerSave.Enabled = false;
        }

        private void SetFullScreen() // This procedure automatically sets the layout to fit the size of the user's screen.
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void CreatePlayerBoard()
        {
            const int squareSize = 50;
            const int spacing = 5;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button playerSquare = new Button();
                    playerSquare.Size = new System.Drawing.Size(squareSize, squareSize);
                    playerSquare.Location = new System.Drawing.Point(col * (squareSize + spacing) + 100, row * (squareSize + spacing) + 200); // This spaces out the buttons that are created to form the grid.
                    playerSquare.Name = "(" + (row).ToString() + "," + (col).ToString() + ")"; // This uses the local row and col variables to give the button a name by converting their assigned values to a string.
                    playerSquare.Click += PlaceShip; // This is what procedure is called when one of the buttons is clicked.
                    playerSquare.MouseHover += selectedButton_MouseHover; // This is the procedure that is called when the mosue hovers over a coordinate.
                    playerSquare.MouseLeave += selectedButton_MouseLeave; // This is the procedure that is called when the mosue stops hovering over a coordinate.
                    boatButtonList.Add(playerSquare); // This addds the playerSquare to the boatButtonList.
                    Controls.Add(playerSquare);
                    playerSquare.BackColor = Color.LightSeaGreen; // This changes the button colour to sea-green.
                    isPlayerButtonEnabled[row, col] = true;
                }
            }
        }

        private void CreateOpponentBoard()
        {
            const int squareSize = 50;
            const int spacing = 5;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button computerSquare = new Button();
                    computerSquare.Size = new System.Drawing.Size(squareSize, squareSize);
                    computerSquare.Location = new System.Drawing.Point(col * (squareSize + spacing) + 1000, row * (squareSize + spacing) + 200); // This spaces out the buttons that are created to form the grid.
                    computerSquare.Name = "(" + (row).ToString() + "," + (col).ToString() + ")"; // This uses the local row and col variables to give the button a name by converting their assigned values to a string.
                    computerSquare.Click += AttackComputerShips; // This is what procedure is called when one of the buttons is clicked.
                    Controls.Add(computerSquare);
                    opponentButtonList.Add(computerSquare); // This adds the computerSquare to the opponentButtonList
                    computerSquare.BackColor = Color.LightSeaGreen; // This changes the button colour to sea-green.
                    computerSquare.Enabled = false; // This prevents the player from clicking on the opponent's grid by disabling every button.
                    isCPUButtonEnabled[row, col] = true;
                }
            }
        }

        private void PlaceShip(object sender, EventArgs e) // This procedure manages how the player chooses where to place their vessels at the start of the game.
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int row = int.Parse(clickedButton.Name.Substring(1, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's y-coordinate can be found.
                int columnn = int.Parse(clickedButton.Name.Substring(3, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's x-coordinate can be found.
                string selectedColumn = columnn.ToString();
                string selectedRow = row.ToString();
                string coordinate = selectedRow + selectedColumn;


                if (playerGrid[row, columnn] != "ship_one" || playerGrid[row, columnn] != "ship_two" || playerGrid[row, columnn] != "ship_three" || playerGrid[row, columnn] != "ship_four" || playerGrid[row, columnn] != "ship_five" && playerList[playerCurrentlyPlaying].GetNumberOfShips() < 5)
                {
                    MessageBox.Show($"You clicked on:\nRow: {row + 1}\nColumn: {columnn + 1}"); // Using the coordinate values, I display a message on screen to inform the user about their selected square.
                    {
                        if (columnn < 6 && boatButtonList[Convert.ToInt32(coordinate)].Enabled == true && boatButtonList[Convert.ToInt32(coordinate) + 1].Enabled == true && boatButtonList[Convert.ToInt32(coordinate) + 2].Enabled == true && boatButtonList[Convert.ToInt32(coordinate) + 3].Enabled == true && boatButtonList[Convert.ToInt32(coordinate) + 4].Enabled == true)
                        {
                            for (int i = 0; i < 5; i++)
                            {

                                boatButtonList[Convert.ToInt32(coordinate) + i].BackColor = Color.LimeGreen;
                                boatButtonList[Convert.ToInt32(coordinate) + i].Enabled = false;
                                isPlayerButtonEnabled[row, columnn + i] = false;

                                if (playerList[0].GetNumberOfShips() == 0)
                                {
                                    playerGrid[row, columnn + i] = "ship_one"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                                }
                                else if (playerList[0].GetNumberOfShips() == 1)
                                {
                                    playerGrid[row, columnn + i] = "ship_two"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                                }
                                else if (playerList[0].GetNumberOfShips() == 2)
                                {
                                    playerGrid[row, columnn + i] = "ship_three"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                                }
                                else if (playerList[0].GetNumberOfShips() == 3)
                                {
                                    playerGrid[row, columnn + i] = "ship_four"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                                }
                                else if (playerList[0].GetNumberOfShips() == 4)
                                {
                                    playerGrid[row, columnn + i] = "ship_five";  // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                                }
                            }

                            playerList[playerCurrentlyPlaying].AddShip(); // This incrementes the player's number of ships by one.
                            MakeVisiblePlayerOneHearts(); // This procedure updates the number of hearts on displayed on the form.
                            clickedButton.Enabled = false; // This disables the selcetd button as the user no longer has to interact with it.

                        }
                    }

                }

                if (playerList[0].GetNumberOfShips() == 5)
                {
                    BT_vsComputerLoad.Enabled = true;
                    BT_vsComputerSave.Enabled = true;

                    for (int i = 0; i < boatButtonList.Count; i++) // This prevents the player from adding any more ships. This is achieved by disabling the buttons.
                    {
                        boatButtonList[i].Enabled = false;
                    }

                    for (int j = 0; j < opponentButtonList.Count; j++) // This allows the player to fire at the opponent. This is done by enabling the other buttons on the other grid. 
                    {
                        opponentButtonList[j].Enabled = true;
                    }

                    Random random = new Random();
                    playerCurrentlyPlaying = random.Next(0, 2);

                    if (playerCurrentlyPlaying == 0)  // Checks whether it is the player's turn.
                    {
                        lb_announcement.Text = $"{playerList[0].GetPlayerName()}'s turn"; // Informs the user that it is the their turn.
                    }
                    else if (playerCurrentlyPlaying == 1) // Checks whether it is the CPU's turn.
                    {
                        lb_announcement.Text = $"{computer.GetComputerName()}'s turn"; // Informs the user that it is the CPU's turn.
                        ComputerAttack();
                    }
                }

            }
        }

        private void MakeVisiblePlayerOneHearts() // This procedure handles how the hearts are displayed on the screeen during the game.
        {
            if (playerCurrentlyPlaying == 0)  // This updates the number of hearts displayed on screen, according to the number of Player One's remaining ships. 
            {
                switch (playerList[0].GetNumberOfShips())
                {
                    case 1:
                        playerOneHearts[0].Visible = true;
                        break;
                    case 2:
                        playerOneHearts[1].Visible = true;
                        break;
                    case 3:
                        playerOneHearts[2].Visible = true;
                        break;
                    case 4:
                        playerOneHearts[3].Visible = true;
                        break;
                    case 5:
                        playerOneHearts[4].Visible = true;
                        break;
                }
            }

        }

        private void GenerateComputerShips() // This procdeure generates five random locations for the CPU's boats. 
        {
            var random = new Random();
            int random_row, random_col, positionCol = 0, positionRow = 0;

            while (computer.GetNumberOfComputerShips() < 5)
            {
                random_row = random.Next(0, 10); // This generates a random y-coordinate between 0 and 9.
                random_col = random.Next(0, 10); // This generates a random x-coordinate between 0 and 9.
                positionRow = random_row;
                positionCol = random_col;
                string columnStr = positionCol.ToString();
                string rowString = positionRow.ToString();
                string coordinate = rowString + columnStr;

                if (positionCol < 6 && opponentGrid[positionRow, positionCol] == null && opponentGrid[positionRow, positionCol + 1] == null && opponentGrid[positionRow, positionCol + 2] == null && opponentGrid[positionRow, positionCol + 3] == null && opponentGrid[positionRow, positionCol + 4] == null) // This checks whether the coordinate is already occupied by one of the CPU's boats.
                {
                    for (int i = 0; i < 5; i++)
                    {
                       // opponentButtonList[Convert.ToInt32(coordinate) + i].BackColor = Color.Orange; (For testing purposes

                        if (computer.GetNumberOfComputerShips() == 0)
                        {
                            opponentGrid[positionRow, positionCol + i] = "ship_one"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (computer.GetNumberOfComputerShips() == 1)
                        {
                            opponentGrid[positionRow, positionCol + i] = "ship_two"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (computer.GetNumberOfComputerShips() == 2)
                        {
                            opponentGrid[positionRow, positionCol + i] = "ship_three"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (computer.GetNumberOfComputerShips() == 3)
                        {
                            opponentGrid[positionRow, positionCol + i] = "ship_four"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                        else if (computer.GetNumberOfComputerShips() == 4)
                        {
                            opponentGrid[positionRow, positionCol + i] = "ship_five"; // This marks the selected square as occupied, so that a ship can no longer be placed at that location.
                        }
                    }

                    computer.AddShip(); // This increments the number of ships the CPU has by one.    
                }
            }

        }


        private void AttackComputerShips(object sender, EventArgs e) // This procedure manages how the player attacks the CPU during their turn.
        {
            if (playerCurrentlyPlaying == 0 && computer.GetNumberOfComputerShips() > 0)
            {
                Button clickedButton = sender as Button;

                if (clickedButton != null)
                {
                    int row = int.Parse(clickedButton.Name.Substring(1, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's y-coordinate can be found.
                    int column = int.Parse(clickedButton.Name.Substring(3, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's x-coordinate can be found.

                    if (playerCurrentlyPlaying == 0 && (opponentGrid[row, column] == "ship_one" || opponentGrid[row, column] == "ship_two" || opponentGrid[row, column] == "ship_three" || opponentGrid[row, column] == "ship_four" || opponentGrid[row, column] == "ship_five")) // Checks whether a valid move has been made by the player and whether a ship was hit.
                    {
                        MessageBox.Show($"You hit {computer.GetComputerName()}'s boat!"); // This informs the player that they have successfully hit one of the CPU's ships.

                        if (opponentGrid[row, column] != null) // This checks whether one of the CPU's boats has been hit.
                        {
                            if (opponentGrid[row, column] == "ship_one")
                            {
                                MessageBox.Show($"You hit boat one!");
                                CPUShipOneParts--;
                                opponentGrid[row, column] = "hit";
                                if (CPUShipOneParts == 0)
                                {
                                    computerHearts[0].Visible = false;  // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    computer.RemoveHeart(); // This changes the index that needs to be accessed in 'computerHearts'.
                                    computer.DestroyShip(); // Decreases the number of ships the CPU has by one.
                                }
                            }
                            else if (opponentGrid[row, column] == "ship_two")
                            {
                                MessageBox.Show($"You hit boat two!");
                                CPUShipTwoParts--;
                                opponentGrid[row, column] = "hit";
                                if (CPUShipTwoParts == 0)
                                {
                                    computerHearts[1].Visible = false;  // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    computer.RemoveHeart(); // This changes the index that needs to be accessed in 'computerHearts'
                                    computer.DestroyShip(); // Decreases the number of ships the CPU has by one.
                                }
                            }
                            else if (opponentGrid[row, column] == "ship_three")
                            {
                                MessageBox.Show("You hit boat three!");
                                CPUShipThreeParts--;
                                opponentGrid[row, column] = "hit";
                                if (CPUShipThreeParts == 0)
                                {
                                    computerHearts[2].Visible = false;  // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    computer.RemoveHeart(); // This changes the index that needs to be accessed in 'computerHearts'
                                    computer.DestroyShip(); // Decreases the number of ships the CPU has by one.   
                                }
                            }
                            else if (opponentGrid[row, column] == "ship_four")
                            {
                                MessageBox.Show("You hit boat four!");
                                CPUShipFourParts--;
                                opponentGrid[row, column] = "hit";
                                if (CPUShipFourParts == 0)
                                {
                                    computerHearts[3].Visible = false; // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    computer.RemoveHeart(); // This changes the index that needs to be accessed in 'computerHearts'
                                    computer.DestroyShip(); // Decreases the number of ships the CPU has by one.
                                }
                            }
                            else if (opponentGrid[row, column] == "ship_five")
                            {
                                MessageBox.Show("You hit boat five!");
                                CPUShipFiveParts--;
                                opponentGrid[row, column] = "hit";
                                if (CPUShipFiveParts == 0)
                                {
                                    computerHearts[4].Visible = false; // This removes one of the CPU's hearts to indicate that one of its ships has been destroyed.
                                    computer.RemoveHeart(); // This changes the index that needs to be accessed in 'computerHearts'
                                    computer.DestroyShip(); // Decreases the number of ships the CPU has by one.
                                }
                            }
                        }
                        clickedButton.BackColor = Color.Red; // This turns the selected buttom red.
                        playerList[0].AddScoreForSuccessfulHit(); // This adds score to the player's total score.
                    }
                    else
                    {
                        MessageBox.Show($"{playerList[0].GetPlayerName()} missed!"); // This informs the player that they have missed the CPU's ships.
                        opponentGrid[row, column] = "selected";
                        clickedButton.BackColor = Color.Blue;
                        playerCurrentlyPlaying = 1; // This changes the current turn so that the CPU can have its turn.
                        lb_announcement.Text = $"{computer.GetComputerName()}'s turn";
                        ComputerAttack(); // This procdeure is called so that the CPU can attack the player's fleet.
                    }


                    playerList[0].AddAttempt(); // This increments the number of attempts made by the player by one. 
                    clickedButton.Enabled = false; // This disables the buttons during the CPU's turn, so that the player cannot shoot during the CPU's turn.
                    isCPUButtonEnabled[row, column] = false;
                    lb_player1Score.Text = $"Score: {playerList[0].GetScore()}";
                    CheckWin(); // This procedure checks whether the player or CPU has won.
                }
            }
        }

        private void ComputerAttack()
        {
            var random = new Random();
            int row;
            int col;
            bool validMoveMade = false;
            bool hasMissed = false;
            string selectedRow;
            string selectedColumn;
            string coordinate;

            while (!validMoveMade || !hasMissed && playerList[0].GetNumberOfShips() > 0) // Loops while the CPU either has not made a valid move or missed, and while the number of ships the player has is greater than zero.
            {
                col = random.Next(0, 10); // Generates a random y-ccordinate between 0 and 9.
                row = random.Next(0, 10);  // Generates a random x-ccordinate between 0 and 9.

                if (playerCurrentlyPlaying == 1 && (playerGrid[row, col] == "ship_one" || playerGrid[row, col] == "ship_two" || playerGrid[row, col] == "ship_three" || playerGrid[row, col] == "ship_four" || playerGrid[row, col] == "ship_five") && playerGrid[row, col] != "selected" && playerGrid[row,col] != "hit") // Checks whether a valid move has been made by the CPU and whether a ship was hit.
                {
                    MessageBox.Show($"{computer.GetComputerName()} hit one of {playerList[0].GetPlayerName()}'s boats!"); // Inmforms the player a ship has been hit.;
                    selectedColumn = col.ToString();
                    selectedRow = row.ToString();
                    coordinate = selectedRow + selectedColumn;
                    boatButtonList[Convert.ToInt32(coordinate)].BackColor = Color.Red; // This uses the coordinate to access an index in the list so that the selected square's colour can change. 


                    if (playerGrid[row, col].Trim() == "ship_one")
                    {
                        MessageBox.Show($"{computer.GetComputerName()} hit boat one!");
                        playerGrid[row, col] = "hit";
                        isPlayerButtonEnabled[row, col] = false;
                        shipNumOneParts--;
                        if (shipNumOneParts == 0)
                        {
                            playerOneHearts[0].Visible = false; // This removes one of the player's hearts to indicate that one of their ships has been destroyed.
                            playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                            playerList[0].DestroyShip(); // Decreases the number of ships the player has by one.
                        }
                    }
                    else if (playerGrid[row, col] == "ship_two")
                    {
                        MessageBox.Show($"{computer.GetComputerName()} hit boat two!");
                        playerGrid[row, col] = "hit";
                        isPlayerButtonEnabled[row, col] = false;
                        shipNumTwoParts--;
                        if (shipNumTwoParts == 0)
                        {
                            playerOneHearts[1].Visible = false; // This removes one of the player's hearts to indicate that one of their ships has been destroyed.
                            playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                            playerList[0].DestroyShip(); // Decreases the number of ships the player has by one
                        }
                    }
                    else if (playerGrid[row, col] == "ship_three")
                    {
                        MessageBox.Show($"{computer.GetComputerName()} hit boat three!");
                        playerGrid[row, col] = "hit";
                        isPlayerButtonEnabled[row, col] = false;
                        shipNumThreeParts--;
                        if (shipNumThreeParts == 0)
                        {
                            playerOneHearts[2].Visible = false; // This removes one of the player's hearts to indicate that one of their ships has been destroyed.
                            playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                            playerList[0].DestroyShip(); // Decreases the number of ships the player has by one   
                        }
                    }
                    else if (playerGrid[row, col] == "ship_four")
                    {
                        MessageBox.Show($"{computer.GetComputerName()} hit boat four!");
                        playerGrid[row, col] = "hit";
                        shipNumFourParts--;
                        if (shipNumFourParts == 0)
                        {
                            playerOneHearts[3].Visible = false; // This removes one of the player's hearts to indicate that one of their ships has been destroyed.
                            playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                            playerList[0].DestroyShip(); // Decreases the number of ships the player has by one
                        }
                    }
                    else if (playerGrid[row, col] == "ship_five")
                    {
                        MessageBox.Show($"{computer.GetComputerName()} hit boat five!");
                        playerGrid[row, col] = "hit";
                        shipNumFiveParts--;
                        if (shipNumFiveParts == 0)
                        {
                            playerOneHearts[4].Visible = false; // This removes one of the player's hearts to indicate that one of their ships has been destroyed.
                            playerList[0].RemoveHeart(); // This changes the index that needs to be accessed in 'playerOneHearts'.
                            playerList[0].DestroyShip(); // Decreases the number of ships the player has by one
                        }
                    }

                    computer.AddAttemptScore(); // This increments the number of attempts the CPU has by one.
                    computer.AddScoreForSuccessfulHit(); // This adds score to the CPU's total score.                   
                    lb_computerScore.Text = $"Score: {computer.GetScore()}"; // This updates the CPU's score on screen.
                    validMoveMade = true;
                    hasMissed = false;
                    CheckWin(); // Checks whether the game has finished. 
                }
                else if (playerCurrentlyPlaying == 1 && playerGrid[row, col] != "selected" && playerGrid[row,col] != "hit") // Checks whether a valid move has been made if a ship was not hit.
                {
                    MessageBox.Show($"{computer.GetComputerName()} missed!"); // Informs the player that the CPU missed.
                    lb_announcement.Text = $"{playerList[0].GetPlayerName()}'s turn";
                    playerGrid[row, col] = "selected";
                    selectedColumn = col.ToString();
                    selectedRow = row.ToString();
                    coordinate = selectedRow + selectedColumn;
                    boatButtonList[Convert.ToInt32(coordinate)].BackColor = Color.Blue; // This uses the coordinate to access an index in the list so that the selected square's colour can change.
                    computer.AddAttemptScore(); // This adds score to the CPU's total score.
                    lb_computerScore.Text = $"Score: {computer.GetScore()}"; // This updates the CPU's score on screen.
                    validMoveMade = true;
                    hasMissed = true;

                }
            }

            CheckWin(); // This procedure checks whether the player or CPU has won.
            validMoveMade = false;
            playerCurrentlyPlaying = 0;
        }

        private void CheckWin() // This procedure checks whether the player or CPU has won.
        {
            string winner; // Stores the value name of the winner.

            if (playerList[0].GetNumberOfShips() == 0) // Checks if all the player's ships have been destroyed.
            {
                winner = "CPU"; // If all of the player's ships have been destroyed, the CPU wins.
                lb_announcement.Text = $"{winner} wins!"; // The UI updates to inform the player that the CPU won the game.
            }
            else if (computer.GetNumberOfComputerShips() == 0)  // Checks if all of the CPU's ships have been destroyed.
            {
                winner = playerList[0].GetPlayerName(); // If all of the CPU's ships have been destroyed, the player wins.
                lb_announcement.Text = $"{winner} wins!"; // The UI updates to inform the player that they won the game. 
                playerList[0].CalculateleaderboardScore(); // This adds calulates player's final score
                string entry = $"{playerList[0].GetPlayerName()},{playerList[0].GetLeaderboardScore()}";
                File.AppendAllLines(addToLeaderboardFilepath, new string[] { entry }); // This adds the player's name and final score to the text file.
            }

            if ((playerList[0].GetNumberOfShips() == 0) || computer.GetNumberOfComputerShips() == 0) // Checks whether either the player or CPU has won.
            {
                for (int i = 0; i < opponentButtonList.Count; i++)
                {
                    opponentButtonList[i].Enabled = false; // Disables the buttons that the player can interact with as the game has ended. 
                }

                BT_vsComputerLoad.Enabled = false; // Prevents the player from loading a previous game.
                BT_vsComputerSave.Enabled = false; // Prevents the player from saving the state of the game.
            }

        }

        private void LB_Player1Name_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BT_vsComputerBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Have you made sure that you have saved your game?\nWould you like to exit the game?", "Would you like to close the game?", MessageBoxButtons.YesNo); // This will provide a prompt and ask the user(s) whether or not they want to close the game
            if (result == DialogResult.Yes)
            {
                Thread.Sleep(2000); // Creates a two second delay before the application closes
                Application.Exit(); // This allows the program to terminate safely
            }
        }

        private void BT_vsComputerSave_Click(object sender, EventArgs e) // Saving the game 
        {
            try
            {
                string cpuGameSaveState = "SaveFileCPUGameState.txt"; // Where the state of the game is saved.
                string cpuGameSaveName = "SaveNameCPUGame.txt"; // Where the player's name is saved. 

                File.Delete(cpuGameSaveState); // The older version of the SaveFileCPUGameState.txt is deleted so that they can be replaced by the most recent version.
                File.Delete(cpuGameSaveName);  // The older version of the SaveNameCPUGame.txt is deleted so that they can be replaced by the most recent version.

                File.AppendAllLines(cpuGameSaveName, new string[] { playerList[0].GetPlayerName() }); // Records the player's name into SaveNameCPUGame.txt. 

                for (int row = 0; row < gridSize; row++) // Saves the state of each square on the player's board into SaveFileCPUGameState.txt and appends each square a corresponding value in a loop.
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if ((playerGrid[row, col] == "ship_one" || playerGrid[row, col] == "ship_two" || playerGrid[row, col] == "ship_three" || playerGrid[row, col] == "ship_four" || playerGrid[row, col] == "ship_five") && playerGrid[row, col] != null)
                        {
                            isPlayerButtonEnabled[row, col] = true;

                            switch (playerGrid[row, col]) // Checks which ship it is.
                            {
                                case "ship_one":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "player_ship_one" }); // Records that the square/button is part of ship one into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_two":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "player_ship_two" }); // Records that the square/button is part of ship two into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_three":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "player_ship_three" }); // Records that the square/button is part of ship three into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_four":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "player_ship_four" }); // Records that the square/button is part of ship four into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_five":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "player_ship_five" }); // Records that the square/button is part of ship five into SaveFileCPUGameState.txt.
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (playerGrid[row, col] == null)
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "player_button_enabled" }); // Records that the square/button is not part of a ship but is enabled into SaveFileCPUGameState.txt.
                        }
                        else if (playerGrid[row, col] == "selected")
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "player_button_disabled" }); // Records that the square/button is not part of a ship and is disabled into SaveFileCPUGameState.txt.
                        }
                        else if (playerGrid[row, col] == "hit")
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "player_hit_button_disabled" }); // Records that the square/button is part of ship five but has been hit into SaveFileCPUGameState.txt.
                        }
                    }
                }

                for (int row = 0; row < gridSize; row++) // Saves the state of each square on the CPU's board in the file and appends each square a corresponding value.
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if (opponentGrid[row, col] == "ship_one" || opponentGrid[row, col] == "ship_two" || opponentGrid[row, col] == "ship_three" || opponentGrid[row, col] == "ship_four" || opponentGrid[row, col] == "ship_five" && opponentGrid[row, col] != null)
                        {
                            isCPUButtonEnabled[row, col] = true;

                            switch (opponentGrid[row, col])
                            {
                                case "ship_one":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_ship_one" });  // Records that the square/button is part of ship one into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_two":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_ship_two" }); // Records that the square/button is part of ship two into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_three":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_ship_three" }); // Records that the square/button is part of ship three into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_four":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_ship_four" }); // Records that the square/button is part of ship four into SaveFileCPUGameState.txt.
                                    break;

                                case "ship_five":
                                    File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_ship_five" }); // Records that the square/button is part of ship five into SaveFileCPUGameState.txt.
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (opponentGrid[row, col] == null)
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_button_enabled" }); // Records that the square/button is not part of a ship but is enabled into SaveFileCPUGameState.txt.
                        }
                        else if (opponentGrid[row, col] == "selected")
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_button_disabled" }); // Records that the square/button is not part of a ship and is disabled into SaveFileCPUGameState.txt.
                        }
                        else if (opponentGrid[row, col] == "hit")
                        {
                            File.AppendAllLines(cpuGameSaveState, new string[] { "cpu_hit_button_disabled"});  // Records that the square/button is part of ship five but has been hit into SaveFileCPUGameState.txt. 
                        }
                    }
                }

                // Saving the amount of damage each of the player's ships have taken.
                File.AppendAllLines(cpuGameSaveState, new string[] { shipNumOneParts.ToString() }); // Saves the number of parts ship one has into SaveFileCPUGameState.txt (player).
                File.AppendAllLines(cpuGameSaveState, new string[] { shipNumTwoParts.ToString() }); // Saves the number of parts ship two has into SaveFileCPUGameState.txt (player).
                File.AppendAllLines(cpuGameSaveState, new string[] { shipNumThreeParts.ToString() }); // Saves the number of parts ship three has into SaveFileCPUGameState.txt (player).
                File.AppendAllLines(cpuGameSaveState, new string[] { shipNumFourParts.ToString() }); // Saves the number of parts ship four has into SaveFileCPUGameState.txt (player).
                File.AppendAllLines(cpuGameSaveState, new string[] { shipNumFiveParts.ToString() }); // Saves the number of parts ship five has into SaveFileCPUGameState.txt (player).

                // Saving the amount of damage each of the CPU's ships have taken.
                File.AppendAllLines(cpuGameSaveState, new string[] { CPUShipOneParts.ToString() }); // Saves the number of parts ship one has into SaveFileCPUGameState.txt (CPU).
                File.AppendAllLines(cpuGameSaveState, new string[] { CPUShipTwoParts.ToString() }); // Saves the number of parts ship two has into SaveFileCPUGameState.txt (CPU).
                File.AppendAllLines(cpuGameSaveState, new string[] { CPUShipThreeParts.ToString() }); // Saves the number of parts ship three has into SaveFileCPUGameState.txt (CPU).
                File.AppendAllLines(cpuGameSaveState, new string[] { CPUShipFourParts.ToString() }); // Saves the number of parts ship four has into SaveFileCPUGameState.txt (CPU).
                File.AppendAllLines(cpuGameSaveState, new string[] { CPUShipFiveParts.ToString() }); // Saves the number of parts ship five has into SaveFileCPUGameState.txt (CPU).

                File.AppendAllLines(cpuGameSaveState, new string[] { playerList[0].GetHearts().ToString() }); // Saves how many hearts the player has into SaveFileCPUGameState.txt.
                File.AppendAllLines(cpuGameSaveState, new string[] { computer.GetHearts().ToString() }); // Saves how many hearts the CPU has into SaveFileCPUGameState.txt.

                File.AppendAllLines(cpuGameSaveState, new string[] { playerList[0].GetNumberOfAttempts().ToString() }); // Saves how many attempts the player has into SaveFileCPUGameState.txt.

                File.AppendAllLines(cpuGameSaveState, new string[] { playerList[0].GetScore().ToString()}); // Saves the player's score into SaveFileCPUGameState.txt.
                File.AppendAllLines(cpuGameSaveState, new string[] { computer.GetScore().ToString() }); // Saves the CPU's score into SaveFileCPUGameState.txt.

                File.AppendAllLines(cpuGameSaveState, new string[] { playerList[0].GetNumberOfShips().ToString() }); // Saves the number of ships the player has into SaveFileCPUGameState.txt.
                File.AppendAllLines(cpuGameSaveState, new string[] {computer.GetNumberOfComputerShips().ToString() }); // Saves the number of ships the CPU has into SaveFileCPUGameState.txt.

                File.AppendAllLines(cpuGameSaveState, new string[] { playerCurrentlyPlaying.ToString() }); // Saves whose turn it is into SaveFileCPUGameState.txt.

            }
            catch (Exception error) 
            {
                MessageBox.Show(error.ToString());  // Displays the cause of an error if one occurs. 
            }
        }
        private void BT_vsComputerLoad_Click(object sender, EventArgs e)
        {
            
            for(int i = 0; i < 100; i++)
            {
                boatButtonList[i].BackColor = Color.LightSeaGreen; // Sets each button on the player's board to light-sea green.
                opponentButtonList[i].BackColor = Color.LightSeaGreen; // Sets each button on the CPU's board to light-sea green.
            }

            try
            {
                List<string> saveData = new List<string>();

                string cpuGameSaveState = "SaveFileCPUGameState.txt";

                saveData = File.ReadAllLines(cpuGameSaveState).ToList();  // Assigns each line in SaveFileCPUGameState.txt to an index in the saveData list. 

                for (int i = 0; i < 100; i++)
                {
                    if (saveData[i] != null && saveData[i] == "player_button_enabled") // Checks whether button is enabled and is not part of a ship.
                    {
                        boatButtonList[i].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = null;  // Marks the player's grid at row, col to null, so the CPU can select that coordinate.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_button_disabled") // Checks whether button is disabled and is not part of a ship. 
                    {
                        boatButtonList[i].BackColor = Color.Blue; // Sets the colour of the button to blue.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "selected"; // Marks the player's grid at row, col to selected, so the CPU cannot select that coordinate again.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_hit_button_disabled") // Checks whether button is disabled and is part of a ship. 
                    {
                        boatButtonList[i].BackColor = Color.Red; // Sets the colour of the button to red.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "hit"; // Marks the player's grid at row, col to as having part of a ship that has been destroyed.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_ship_one") // Checks whether button is enabled part of a ship one. 
                    {
                        boatButtonList[i].BackColor = Color.LimeGreen; // Sets the colour of the button to lime-green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "ship_one";  // Marks the player's grid at row, col to as having part of ship one present.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_ship_two") // Checks whether button is enabled part of a ship two.
                    {
                        boatButtonList[i].BackColor = Color.LimeGreen; // Sets the colour of the button to lime-green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "ship_two";  // Marks the player's grid at row, col to as having part of ship two present.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_ship_three") // Checks whether button is enabled part of a ship three.
                    {
                        boatButtonList[i].BackColor = Color.LimeGreen; // Sets the colour of the button to lime-green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "ship_three";  // Marks the player's grid at row, col to as having part of ship three present.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_ship_four") // Checks whether button is enabled part of a ship four.
                    {
                        boatButtonList[i].BackColor = Color.LimeGreen; // Sets the colour of the button to lime-green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "ship_four";  // Marks the player's grid at row, col to as having part of ship four present.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "player_ship_five") // Checks whether button is enabled part of a ship five.
                    {
                        boatButtonList[i].BackColor = Color.LimeGreen; // Sets the colour of the button to lime-green.
                        int row = int.Parse(boatButtonList[i].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(boatButtonList[i].Name.Substring(3, 1)); // Gets the col index.
                        playerGrid[row, col] = "ship_five"; // Marks the player's grid at row, col to as having part of ship five present.
                        boatButtonList[i].Enabled = false; // The button/square is disabled. 
                    }

                }

                for (int i = 100; i < 200; i++)
                {
                    if (saveData[i] != null && saveData[i] == "cpu_button_enabled")  // Checks whether button is enabled and is not part of a ship.
                    {
                        opponentButtonList[i-100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = null; // Marks the CPU's grid at row, col to null, so the player can select that coordinate.
                        opponentButtonList[i-100].Enabled = true; // The button/square is enabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_button_disabled") // Checks whether button is disabled and is not part of a ship. 
                    {
                        opponentButtonList[i-100].BackColor = Color.Blue; // Sets the colour of the button to blue.
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "selected";  // Marks the CPU's grid at row, col to selected, so the player cannot select that coordinate again.
                        opponentButtonList[i-100].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_hit_button_disabled") // Checks whether button is disabled and is part of a ship.
                    {
                        opponentButtonList[i-100].BackColor = Color.Red; // Sets the colour of the button to red.
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "hit"; // Marks the CPU's grid at row, col to as having part of a ship that has been destroyed.
                        opponentButtonList[i-100].Enabled = false; // The button/square is disabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_ship_one") // Checks whether button is enabled part of a ship one.
                    {
                        opponentButtonList[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // opponentButtonList[i-100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "ship_one";  // Marks the CPU's grid at row, col to as having part of ship one present.
                        opponentButtonList[i-100].Enabled = true; // The button/square is enabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_ship_two") // Checks whether button is enabled part of a ship two.
                    {
                        opponentButtonList[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // opponentButtonList[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "ship_two";  // Marks the CPU's grid at row, col to as having part of ship two present.
                        opponentButtonList[i-100].Enabled = true; // The button/square is enabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_ship_three") // Checks whether button is enabled part of a ship three.
                    {
                        opponentButtonList[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // opponentButtonList[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "ship_three";  // Marks the CPU's grid at row, col to as having part of ship three present.
                        opponentButtonList[i - 100].Enabled = true; // The button/square is enabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_ship_four") // Checks whether button is enabled part of a ship four.
                    {
                        opponentButtonList[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                       // opponentButtonList[i - 100].BackColor = Color.Yellow;// Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "ship_four";  // Marks the CPU's grid at row, col to as having part of ship four present.
                        opponentButtonList[i-100].Enabled = true; // The button/square is enabled. 
                    }
                    else if (saveData[i] != null && saveData[i] == "cpu_ship_five") // Checks whether button is enabled part of a ship five.
                    {
                        opponentButtonList[i - 100].BackColor = Color.LightSeaGreen; // Sets the colour of the button to light-sea green.
                        // opponentButtonList[i - 100].BackColor = Color.Yellow; // Sets the colour of the button to yellow. (For testing purposes)
                        int row = int.Parse(opponentButtonList[i - 100].Name.Substring(1, 1)); // Gets the row index.
                        int col = int.Parse(opponentButtonList[i - 100].Name.Substring(3, 1)); // Gets the col index.
                        opponentGrid[row, col] = "ship_five";  // Marks the CPU's grid at row, col to as having part of ship five present.
                        opponentButtonList[i-100].Enabled = true; // The button/square is enabled. 
                    }
                }

                playerCurrentlyPlaying = int.Parse(saveData[217]); // Sets the value of playerCurrentlyPlaying to the value on line 218 in SaveFileCPUGameState.txt. 

                playerList[0].SetSavedPlayerName("vsCPU", 1); // Retrieves the player's saved name from SaveNameCPUGame.txt.
                LB_Player1Name.Text = $"{playerList[0].GetPlayerName()}"; // Updates the text that displays the player's name.

                playerList[0].SetSavedNumberOfShips("vsCPU", 1); // Retrieves the number of ships the player has from SaveFileCPUGameState.txt.
                playerList[0].SetSavedScore("vsCPU", 1); // Retrieves the player's score from SaveFileCPUGameState.txt.
                lb_player1Score.Text = $"Score: {playerList[0].GetScore()}"; // Updates the text that displays the player's score.

                computer.SetSavedScore(); // Retrieves the CPU's score from SaveFileCPUGameState.txt.
                computer.SetSavedNumberOfShips(); // Retrieves the number of ships the CPU has from SaveFileCPUGameState.txt.
                lb_computerScore.Text = $"Score: {computer.GetScore()}"; // Updates the text that displays the CPU's score.

                playerList[0].SetSavedAttempts("vsCPU", 1); // Retrieves the number of attempts the player has had from SaveFileCPUGameState.txt.

                shipNumOneParts = int.Parse(saveData[200]); // Sets the value of shipNumOneParts to the value on line 201 in SaveFileCPUGameState.txt.
                shipNumTwoParts = int.Parse(saveData[201]); // Sets the value of shipNumTwoParts to the value on line 202 in SaveFileCPUGameState.txt.
                shipNumThreeParts = int.Parse(saveData[202]); // Sets the value of shipNumThreeParts to the value on line 203 in SaveFileCPUGameState.txt.
                shipNumFourParts = int.Parse(saveData[203]); // Sets the value of shipNumFourParts to the value on line 204 in SaveFileCPUGameState.txt.
                shipNumFiveParts = int.Parse(saveData[204]); // Sets the value of shipNumFiveParts to the value on line 205 in SaveFileCPUGameState.txt.

                CPUShipOneParts = int.Parse(saveData[205]); // Sets the value of CPUShipOneParts to the value on line 206 in SaveFileCPUGameState.txt.
                CPUShipTwoParts = int.Parse(saveData[206]); // Sets the value of CPUShipTwoParts to the value on line 207 in SaveFileCPUGameState.txt.
                CPUShipThreeParts = int.Parse(saveData[207]); // Sets the value of CPUShipThreeParts to the value on line 208 in SaveFileCPUGameState.txt.
                CPUShipFourParts = int.Parse(saveData[208]); // Sets the value of CPUShipFourParts to the value on line 209 in SaveFileCPUGameState.txt.
                CPUShipFiveParts = int.Parse(saveData[209]); // Sets the value of CPUShipFiveParts to the value on line 210 in SaveFileCPUGameState.txt.

                computer.SetSavedHearts(); // Retrieves the number of hearts the CPU has from SaveFileCPUGameState.txt.
                playerList[0].SetSavedHearts("vsCPU", 1); // Retrieves the number of hearts the player has from SaveFileCPUGameState.txt.

                // These if statements determine which hearts should be displayed on screen.
                if (shipNumOneParts == 0)
                {
                    playerOneHearts[0].Visible = false;
                }
                else
                {
                    playerOneHearts[0].Visible = true;
                }


                if (shipNumTwoParts == 0)
                {
                    playerOneHearts[1].Visible = false;
                }
                else
                {
                    playerOneHearts[1].Visible = true;
                }


                if (shipNumThreeParts == 0)
                {
                    playerOneHearts[2].Visible = false;
                }
                else
                {
                   playerOneHearts[2].Visible = true;
                }


                if (shipNumFourParts == 0)
                {
                    playerOneHearts[3].Visible = false;
                }
                else
                {
                    playerOneHearts[3].Visible = true;
                }

                if (shipNumFiveParts == 0)
                {
                    playerOneHearts[4].Visible = false;
                }
                else
                {
                    playerOneHearts[4].Visible = true;
                }

                if(CPUShipOneParts == 0)
                {
                    computerHearts[0].Visible = false;
                }
                else
                {
                    computerHearts[0].Visible = true;
                }

                if (CPUShipTwoParts == 0)
                {
                    computerHearts[1].Visible = false;
                }
                else
                {
                    computerHearts[1].Visible = true;
                }

                if (CPUShipThreeParts == 0)
                {
                    computerHearts[2].Visible = false;
                }
                else
                {
                    computerHearts[2].Visible = true;
                }

                if (CPUShipFourParts == 0)
                {
                    computerHearts[3].Visible = false;;
                }
                else
                {
                    computerHearts[3].Visible = true;
                }

                if (CPUShipFiveParts == 0)
                {
                    computerHearts[4].Visible = false;
                }
                else
                {
                    computerHearts[4].Visible = true;
                }

                lb_announcement.Text = $"{playerList[0].GetPlayerName()}'s turn"; // Updates the announcement text at the top of the screen.

            }
            catch (Exception error)
            {
                 MessageBox.Show(error.ToString()); // Displays the cause of an error if one occurs.
            }

            BT_vsComputerLoad.Enabled = false; // Disables the button so that the game cannot be loaded again. 
                  
        } 

        private void selectedButton_Click(object sender, EventArgs e)
        {
        }
        private void selectedButton_MouseHover(object sender, EventArgs e)
        {
            Button highlightButton = sender as Button;
            int row = int.Parse(highlightButton.Name.Substring(1, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's y-coordinate can be found.
            int columnn = int.Parse(highlightButton.Name.Substring(3, 1)); // This converts elements of the clickedButton's name to an integer. This is so the square's x-coordinate can be found.
            string selectedColumn = columnn.ToString();
            string selectedRow = row.ToString();
            string coordinate = selectedRow + selectedColumn;



            if (highlightButton.Enabled == true || opponentButtonList[(Convert.ToInt32(coordinate))].Enabled == true)
            {
                highlightButton.BackColor = ColorTranslator.FromHtml("#c1fec1");
            }
            else if (highlightButton.Enabled == false || opponentButtonList[(Convert.ToInt32(coordinate))].Enabled == false)
            {
                highlightButton.BackColor = ColorTranslator.FromHtml("#cb4f5c");
            }
        }

        private void selectedButton_MouseLeave(object sender, EventArgs e)
        {
            Button highlightButton = sender as Button;
            highlightButton.BackColor = Color.LightSeaGreen;
        }
    }


    class Player
    {
        private string playerName;
        private int numberOfShips;
        private int numberOfAttempts;
        private int score;
        private int hearts;
        private double leaderboardScore;

        public Player(string pPlayerName, int pNumberOfShips,  int pNumberOfAttempts, int pHearts)
        {
            playerName = pPlayerName;
            numberOfShips = pNumberOfShips;
            numberOfAttempts = pNumberOfAttempts;
            hearts = pHearts; 
        }

        public string GetPlayerName() { return playerName; } // Retrieves the player(s)' name after it is set by the player(s)'. 

        public void SetSavedPlayerName(string gameMode, int player)
        {
            if(gameMode == "vsCPU")
            {
                List<string> savedName = new List<string>();
                string cpuGameSaveName = "SaveNameCPUGame.txt";
                savedName = File.ReadAllLines(cpuGameSaveName).ToList();
                playerName = savedName[0].Trim(); // Gets the saved name of the player from line 1 in SaveNameCPUGame.txt.
            }
            else if(gameMode == "vsPlayer")
            {
                if(player == 1)
                {
                    List<string> savedName = new List<string>();
                    string playerVsPlayerGameSaveName = "SaveNamePlayerVsPlayerGame.txt";
                    savedName = File.ReadAllLines(playerVsPlayerGameSaveName).ToList();
                    playerName = savedName[0].Trim(); // Gets the saved name of Player One from line 1 in SaveNamePlayerVsPlayerGame.txt.
                }
                else if(player == 2)
                {
                    List<string> savedName = new List<string>();
                    string playerVsPlayerGameSaveName = "SaveNamePlayerVsPlayerGame.txt";
                    savedName = File.ReadAllLines(playerVsPlayerGameSaveName).ToList();
                    playerName = savedName[1].Trim(); // Gets the saved name of Player Two from line 2 in SaveNamePlayerVsPlayerGame.txt.
                }
            }
 
        }
        public void AddShip() { numberOfShips++; } // Increments the number of ships the player has by one.
        public void DestroyShip() { numberOfShips--; } // This decrements the number of ships the player has by one.
        public int GetNumberOfShips() {  return numberOfShips; } //  This retrieves the number of ships a player has, which also affects elements of the UI.
        public void SetSavedNumberOfShips(string gameMode, int player)
        {
            if (gameMode == "vsCPU")
            {
                List<string> saveData = new List<string>();
                string cpuGameSaveState = "SaveFileCPUGameState.txt";
                saveData = File.ReadAllLines(cpuGameSaveState).ToList();
                numberOfShips = Convert.ToInt32(saveData[215]); // Gets the saved number of ships that the player has from line 216 in SaveFileCPUGameState.txt.
            }
            else if (gameMode == "vsPlayer")
            {
                if (player == 1)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    numberOfShips = Convert.ToInt32(playerVsPlayerSaveData[216]); // Gets the saved number of ships that Player One has from line 217 in SaveFilePlayerVsPlayerGameState.txt.
                }
                else if (player == 2)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    numberOfShips = Convert.ToInt32(playerVsPlayerSaveData[217]); // Gets the saved number of ships that Player Two has from line 218 in SaveFilePlayerVsPlayerGameState.txt.
                }

            }
        }
        public int GetNumberOfAttempts() {  return numberOfAttempts; } // This retrieves the number of player attempts, which will be used when calculating the player score.
        public void SetSavedAttempts(string gameMode, int player)
        {
            if (gameMode == "vsCPU")
            {
                List<string> saveData = new List<string>();
                string cpuGameSaveState = "SaveFileCPUGameState.txt";
                saveData = File.ReadAllLines(cpuGameSaveState).ToList();
                numberOfAttempts = Convert.ToInt32(saveData[212]); // Gets the saved number of attempts that the player has had from line 213 in SaveFileCPUGameState.txt.
            }
            else if(gameMode == "vsPlayer")
            {
                if(player == 1)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    numberOfAttempts = Convert.ToInt32(playerVsPlayerSaveData[212]); // Gets the saved number of attempts that Player One has had from line 213 in SaveFilePlayerVsPlayerGameState.txt.
                }
                else if(player == 2)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    numberOfAttempts = Convert.ToInt32(playerVsPlayerSaveData[213]); // Gets the saved number of attempts that Player Two has had from line 214 in SaveFilePlayerVsPlayerGameState.txt.
                }
            }
        }

        public void AddAttempt() { numberOfAttempts++; score += 20; } // After a shot is taken, this increments the number of attempts made by the player by one and adds 20 to the player's score.
        public int GetScore() { return score; }
        public void SetSavedScore(string gameMode, int player)
        {
            if(gameMode == "vsCPU")
            {
                List<string> saveData = new List<string>();
                string cpuGameSaveState = "SaveFileCPUGameState.txt";
                saveData = File.ReadAllLines(cpuGameSaveState).ToList();
                score = Convert.ToInt32(saveData[213]); // Gets the saved value of the player's score from line 214 in SaveFileCPUGameState.txt.
            }
            else if(gameMode == "vsPlayer")
            {
                if(player == 1)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    score = Convert.ToInt32(playerVsPlayerSaveData[214]); // Gets the saved value of Player One's score from line 215 in SaveFilePlayerVsPlayerGameState.txt.
                }
                else if(player == 2)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    score = Convert.ToInt32(playerVsPlayerSaveData[215]);  // Gets the saved value of Player Two's score from line 216 in SaveFilePlayerVsPlayerGameState.txt.
                }
            }
 
        }

        public void AddScoreForSuccessfulHit() { score += 100; } // This adds 100 to the player's score.
        public double GetLeaderboardScore() { return leaderboardScore; } // This returns the player's final calculated score at the end of the game.
        public void CalculateleaderboardScore() { leaderboardScore = (score * (100 * (numberOfShips) / numberOfAttempts)); } // This is a score multiplier which will give the player's final score.
        public void RemoveHeart() { hearts--; } // This decreases the value of hearts so that the correct index in either 'playerOneHearts' or 'playerTwoHearts'. 
        public int GetHearts() { return hearts; } // Returns the index of hearts in the playerOneHearts array and or playerTwoHearts array.
        public void SetSavedHearts(string gameMode, int player)
        {
            if (gameMode == "vsCPU")
            {
                List<string> cpuGameSaveData = new List<string>();
                string cpuGameSaveState = "SaveFileCPUGameState.txt";
                cpuGameSaveData = File.ReadAllLines(cpuGameSaveState).ToList();
                hearts = Convert.ToInt32(cpuGameSaveData[211]); // Gets the saved value of the index in playerOneHearts from line 212 in SaveFileCPUGameState.txt.
            }
            else if (gameMode == "vsPlayer")
            {
                if (player == 1)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    hearts = Convert.ToInt32(playerVsPlayerSaveData[210]); // Gets the saved value of the index in playerOneHearts from line 211 in SaveFilePlayerVsPlayerGameState.txt.
                }
                else if (player == 2)
                {
                    List<string> playerVsPlayerSaveData = new List<string>();
                    string playerVsPlayerGameSaveState = "SaveFilePlayerVsPlayerGameState.txt";
                    playerVsPlayerSaveData = File.ReadAllLines(playerVsPlayerGameSaveState).ToList();
                    hearts = Convert.ToInt32(playerVsPlayerSaveData[211]); // Gets the saved value of the index in playerTwoHearts from line 212 in SaveFilePlayerVsPlayerGameState.txt.
                }

            }
        }
    }

    class Computer
    {
        private string computerName;
        private int numberOfComputerShips;
        private int score;
        private int hearts; 

        public Computer(string pComputerName, int pNumberOfComputerShips, int pHearts)
        {
            computerName = pComputerName;
            numberOfComputerShips = pNumberOfComputerShips;
            hearts = pHearts;
            
        }

        public string GetComputerName() { return computerName; } // This retrieves the name of the computer opponent.
        public void AddShip() { numberOfComputerShips++; } // This increments the number of ships the computer opponent has by one.
        public void DestroyShip() { numberOfComputerShips--; } // This decrements the number of ships the computer opponent has by one.
        public int GetNumberOfComputerShips() { return numberOfComputerShips; }  // This retrieves the number of ships the computer has, which also affects elements of the UI.
        public void SetSavedNumberOfShips()
        {
            List<string> saveData = new List<string>();
            string cpuGameSaveState = "SaveFileCPUGameState.txt";
            saveData = File.ReadAllLines(cpuGameSaveState).ToList();
            numberOfComputerShips = Convert.ToInt32(saveData[216]); // Gets the saved number of ships the CPU has from line 217 in SaveFileCPUGameState.txt.
        }
        public void AddAttemptScore() { score += 20; } // This adds 20 to the CPU's score
        public int GetScore() { return score; } // Returns the value of the CPU's score.

        public void SetSavedScore()
        {
            List<string> saveData = new List<string>();
            string cpuGameSaveState = "SaveFileCPUGameState.txt"; 
            saveData = File.ReadAllLines(cpuGameSaveState).ToList();
            score = Convert.ToInt32(saveData[214]);  // Gets the saved value of the CPU's score from line 215 in SaveFileCPUGameState.txt. 
        }
        public void AddScoreForSuccessfulHit() { score += 100; } // This adds 100 to the CPU's score.
        public void RemoveHeart() { hearts--; } // This decreases the value of hearts so that the correct index in 'computerHearts'. 
        public int GetHearts() { return hearts; } // Returns the index of hearts in the computerHearts array.
        public void SetSavedHearts()
        {
            List<string> saveData = new List<string>();
            string cpuGameSaveState = "SaveFileCPUGameState.txt";
            saveData = File.ReadAllLines(cpuGameSaveState).ToList();
             hearts = int.Parse(saveData[211]); // Gets the saved value of the index in computerHearts from line 212 in SaveFileCPUGameState.txt. 
        }

    }
       
}
