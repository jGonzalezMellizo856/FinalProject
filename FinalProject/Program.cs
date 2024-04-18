
using System;

namespace ConnectFourGame
{

   
    class GameBoard //class to initialize the game board might change later for player usage
    {
        private const int Rows = 6;
        private const int Cols = 7;
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

        public void PrintBoard()
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.WriteLine("|");
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


        public void StartingSinglePlayerGame()
        {
            
        }
    }


    public class TwoPlayerGame : ITwoPlayerGame
    {
        // add logic for two player game and other features 


        public void StartingTwoPlayerGame()
        {

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
                    ITwoPlayerGame twoPlayerGame = new TwoPlayerGame();
                    twoPlayerGame.StartingTwoPlayerGame();
                    break;
                case "3":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Incvalid choice, please enter a valid option");
                    break; 
            }
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

            



            
        }
    }
}
