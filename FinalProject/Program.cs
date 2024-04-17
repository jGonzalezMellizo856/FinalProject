


namespace ConnectFourGame
{


    class GameBoard
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private int[,] Board;

        public GameBoard()
        {

        }

        private void InitializeBoard()
        {
            Board = new int[Cols, Rows];

        }

    }
    

    class Program
    {
       static void Main(string[] args)
        {

            bool exitOption = false;

            while (!exitOption)
            {
                Console.WriteLine("Welcome to my Connect Four Game");
                Console.WriteLine("\nTo use the menu enter one of the three numbers");
                Console.WriteLine("1. 1 Player");
                Console.WriteLine("2. 2 PLayer");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice here:");

                string userIn = Console.ReadLine();

                switch(userIn)
                {
                    case "1":
                        Console.WriteLine("you selected single player mode");
                        break;
                    case "2":
                        Console.WriteLine("you have selected two player mode");
                        break;
                    case "3":
                        Console.WriteLine("Exiting program...");
                        exitOption = true;
                        break;
                    default:
                        Console.WriteLine("invalid choice, please enter a valid option");
                        break;
                }

                Console.WriteLine();

            }
        }
    }
}
