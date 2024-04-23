
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConnectFourGame
{

   
    class GameBoard //class to initialize the game board might change later for player usage
    {
        public const int Rows = 6;
        public const int Cols = 7;
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
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write("|");
                    Console.Write(GetPieceSymbol(Board[j, i]));
                }
                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', Cols * 2 + 1));

            for (int i = 0; i < Cols; i++)
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

            if(column < 0 || column >= Cols)
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
                for (int j = 0; j < Cols; j++)
                {
                    if (Board[j, i] == player && Board[j, i + 1] == player && Board[j, i + 2] == player && Board[j, i + 3] == player)
                        return true;
                }
            }

            //check diagonally (top left to bottom right)
            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 0; j <= Cols - 4; j++)
                {
                    if (Board[j, i] == player && Board[j + 1, i + 1] == player && Board[j + 2, i + 2] == player && Board[j + 3, i + 3] == player)
                        return true;
                }
            }

            //check diagonally (top left to bottom left)
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
            for (int i = 0; i <= Cols; i++)
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
        void SetPlayersNames(string player1Name, string player2Name);
    }

    public class SinglePlayerGame : ISinglePlayerGame
    {
        // add logic for single player game and add other features for single player mode


        public void StartingSinglePlayerGame()
        {
         
            
        }
    }


    public class TwoPlayerGame : ITwoPlayerGame
    {
        // add logic for two player game and other features 
        private string Player1Name;
        private string Player2Name;
        private GameBoard gameBoard;

        public TwoPlayerGame()
        {
            gameBoard = new GameBoard();
        }

        public void StartingTwoPlayerGame()
        {
            Console.WriteLine($"Welcome {Player1Name} and {Player2Name} to the two player mode");
            gameBoard.PrintBoard();
            PlayGame();
        }

        public void SetPlayersNames(string player1Name, string player2Name)
        {
            this.Player1Name = player1Name;
            this.Player2Name = player2Name;
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
                        Console.WriteLine($"{GetPlayerName(currentPlayer)}");
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
                    ISinglePlayerGame singlePlayerGame = new SinglePlayerGame();
                    singlePlayerGame.StartingSinglePlayerGame();
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
            
            ITwoPlayerGame twoPlayerGame = new TwoPlayerGame();
            twoPlayerGame.SetPlayersNames(player1Name, player2Name);
            twoPlayerGame.StartingTwoPlayerGame();
            


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
