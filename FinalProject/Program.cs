
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConnectFourGame
{

   
    class GameBoard //class to initialize the game board might change later for player usage
    {
        public const int Rows = 6;
        public const int Cols = 8;
        private int[,] Board;

        public GameBoard()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Board = new int[Cols, Rows];

            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Board[i, j] = 0;
                }
            }

        }

        //prints the game board
        public void PrintBoard()
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Cols - 1; j++)
                {
                    Console.Write("|");
                    Console.Write(GetPieceSymbol(Board[j, i]));
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', Cols * 2 - 1));

            for (int i = 1; i <= Cols - 1; i++)
            {
                Console.Write(" " + i);
            }
            Console.WriteLine();
        }

        private Char GetPieceSymbol(int player)
        {
            switch (player)
            {
                case 1:
                    return 'X';
                case 2:
                    return 'O';
                default:
                    return ' ';
            }
        }

        //places the piece on the board
        public bool PlacePiece(int column, int player)
        {
            Console.Clear();
            column--;

            if (column < 0 || column >= Cols)
                return false;
            if (Board[column, Rows - 1] != 0)
                return false;

            for (int i = 0; i < Rows; i++)
            {
                if (Board[column, i] == 0)
                {
                    Board[column, i] = player;
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin(int player)
        {
            //check horizontaly
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j <= Cols - 4; j++)
                {
                    if (Board[j, i] == player && Board[j + 1, i] == player && Board[j + 2, i] == player && Board[j + 3, i] == player)
                        return true;
                }
            }
            // Check vertically

            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 0; j <= Cols - 4; j++)
                {
                    if (Board[j, i] == player && Board[j + 1, i + 1] == player && Board[j + 2, i + 2] == player && Board[j + 3, i + 3] == player)
                        return true;
                }
            }

            // Check diagonally (top left to bottom right)
            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 0; j <= Cols - 4; j++)
                {
                    if (Board[j, i] == player && Board[j + 1, i + 1] == player && Board[j + 2, i + 2] == player && Board[j + 3, i + 3] == player)
                        return true;
                }
            }

            // Check diagonally (top right to bottom left)
            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 3; j < Cols; j++)
                {
                    if (Board[j, i] == player && Board[j - 1, i + 1] == player && Board[j - 2, i + 2] == player && Board[j - 3, i + 3] == player)
                        return true;
                }
            }

            return false;
        }

        //checks if the board is full 
        public bool IsBoardFull()
        {
            for (int i = 0; i < Cols; i++)
            {
                if (Board[i, Rows - 1] == 0)
                    return false;
            }
            return true;
        }

    }


    //interface for the single player game mode
    public interface ISinglePlayerGame
    {
        void StartingSinglePlayerGame();
    }

    //interface for the two player game mode
    public interface ITwoPlayerGame
    {
        void StartingTwoPlayerGame();

    }

    public class SinglePlayerGame : ISinglePlayerGame
    {
        // add logic for single player game and add other features for single player mode

        private string PlayerName;
        private GameBoard gameBoard;

        public SinglePlayerGame(string playerName)
        {
            PlayerName = playerName;
            gameBoard = new GameBoard();
        }

        public void StartingSinglePlayerGame()
        {
            Console.WriteLine($"Welcome {PlayerName} to the single-player mode");
            gameBoard.PrintBoard();
            PlayGame();
        }

        private void PlayGame()
        {

            int currentPlayer = 1;
            bool gameFinished = false;

            Random random = new Random(); //generates computer moves

            while(!gameFinished)
            {

                if (currentPlayer == 1)
                {
                    Console.WriteLine($"It's your turn, {PlayerName}");
                    Console.WriteLine($"Enter the column number to place your piece:");
                    int column;

                    while(!int.TryParse(Console.ReadLine(), out column) || column < 0 || column >= GameBoard.Cols)
                    {
                        Console.WriteLine("Invalid input. please enter a valid column number.");
                    }

                    if(gameBoard.PlacePiece(column, currentPlayer))
                    {
                        gameBoard.PrintBoard();
                        if (gameBoard.CheckWin(currentPlayer))
                        {
                            Console.WriteLine($"{PlayerName} Wins!");
                            gameFinished = true;
                        }else if (gameBoard.IsBoardFull())
                        {
                            Console.WriteLine("The game end in a draw");
                            gameFinished = true;
                        }
                        else
                        {
                            currentPlayer = 2; // swtiches to computer move
                        }
                    }
                    else
                    {
                        Console.WriteLine("Column is full. Please choose another column");
                    }

                }
                else
                {
                    //computers turn

                    int column = random.Next(0, GameBoard.Cols);
                    Console.WriteLine($"Computer places its piece in the column {column}");
                    if (gameBoard.PlacePiece(column, currentPlayer))
                    {
                        Console.WriteLine("Computer Wins!");
                        gameFinished = true;
                    }else if (gameBoard.IsBoardFull())
                    {
                        Console.WriteLine("The game ends in a draw.");
                        gameFinished = true;
                    }
                    else
                    {
                        currentPlayer = 1; // switch back to player 1
                    }
                }
                
            }
        }
    }


    public class TwoPlayerGame : ITwoPlayerGame
    {
        // add logic for two player game and other features 
        private string Player1Name;
        private string Player2Name;
        private GameBoard gameBoard;

        public TwoPlayerGame(string player1Name, string player2Name)
        {
            Player1Name = player1Name;
            Player2Name = player2Name;  
            gameBoard = new GameBoard();

        }

        public void StartingTwoPlayerGame()
        {
            Console.WriteLine($"Welcome {Player1Name} and {Player2Name} to the two player mode");
            gameBoard.PrintBoard();
            PlayGame();
        }

        private void PlayGame()
        {
            int currentPlayer = 1;
            bool gameFinished = false;


            while (!gameFinished)
            {

                Console.WriteLine($"It's {GetPlayerName(currentPlayer)}'s turn");
                Console.WriteLine($"Enter the column number to place your piece:");
                int column;

                while (!int.TryParse(Console.ReadLine(), out column) ||  column < 0 || column >= GameBoard.Cols)
                {
                    Console.WriteLine("Invalid input. please enter a valid column number.");
                }

                if(gameBoard.PlacePiece(column, currentPlayer))
                {

                    gameBoard.PrintBoard();
                    if (gameBoard.CheckWin(currentPlayer))
                    {
                        Console.WriteLine($"{GetPlayerName(currentPlayer)} Wins");
                        gameFinished = true;
                    }
                    else if (gameBoard.IsBoardFull())
                    {
                        Console.WriteLine("The game ends in a draw.");
                        gameFinished = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 1) ? 2 : 1;
                    }
                }
                else
                {
                    Console.WriteLine("Column is full. PLease choose another Column");
                }
               
            }
        }

        //obtains the current players name
        private string GetPlayerName(int  player)
        {
            return (player == 1) ? Player1Name : Player2Name;
        }

    }



    class InitializeGame
    {

        private bool ExitOption = false;

       public void StartGame()
        {
            while (!ExitOption)
            {
                DisplayMenu();
                ProcessUserChoice();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Welcome to my Connect Four Game.");
            Console.WriteLine("\nTo use the menu enter one of the three numbers");
            Console.WriteLine("1. 1 player");
            Console.WriteLine("2. 2 player");
            Console.WriteLine("3. exit");
            Console.WriteLine("Enter you choice here:");
        }

        private void ProcessUserChoice()
        {
            string userIn = Console.ReadLine();

            switch (userIn)
            {
                case "1":
                    StartSinglePlayerGame();
                    break;
                case "2":
                    StartTwoPlayerGame();
                    break;
                case "3":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Incvalid choice, please enter a valid option");
                    break; 
            }
        }

        private void StartTwoPlayerGame()
        {

            Console.WriteLine("Enter Player 1's name:");
            string player1Name = Console.ReadLine();
            Console.WriteLine("Enter Player 2's name:");
            string player2Name = Console.ReadLine();
            Console.Clear();
            
            ITwoPlayerGame twoPlayerGame = new TwoPlayerGame(player1Name, player2Name);
            twoPlayerGame.StartingTwoPlayerGame();
            
        }


        private void StartSinglePlayerGame()
        {
            Console.WriteLine("Enter players name");
            string playerName = Console.ReadLine();

            ISinglePlayerGame singlePlayerGame = new SinglePlayerGame(playerName);
            singlePlayerGame.StartingSinglePlayerGame();
        }

        private void ExitGame()
        {
            Console.WriteLine("Exiting Program");
            ExitOption = true;
        }
    }



    

    class Program
    {
       static void Main(string[] args)
        {
            InitializeGame initialize  = new InitializeGame();

            initialize.StartGame();
        }
    }
}
