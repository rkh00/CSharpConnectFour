using ConnectFour;

class Program {

    private Boolean gameOver = false;
    private Turn? winner;
    //private Boolean colFull = false;
    private int totalGamesPlayed = 0;
    private int gamesWonByX = 0;
    private int gamesDrawn = 0;
    //private Boolean outOfBounds = false;
    private GameError? gameError;
    private int? noOfGames;
    private int? noOfRows;
    private int? noOfCols;

    private static Boolean playAgain() {
        while (true) {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape) {
                return false;
            } else if (key.Key == ConsoleKey.Spacebar) {
                return true;
            }
        }
    }

    public static void Main() {

        Program program = new Program();

        //while ((program.noOfRows == null) || (program.noOfCols == null) || (program.noOfGames == null)) {
        //    Console.WriteLine("Welcome to Connect Four!");
        //    Console.WriteLine("Two players, X and O, are required. X will start.");
        //    Console.Write("How many rows should your game have? (Default is 7): ");
        //    string inputRows = Console.ReadLine();

        //    try {
        //        int number = int.Parse(inputRows);
        //        if (number < 1) {
        //            Console.WriteLine("You must have at least one row.");
        //        }
        //        Console.WriteLine($"You entered: {number}");
        //    } catch (FormatException) {
        //        Console.WriteLine("Invalid input. Please enter a valid integer.");
        //    } catch (OverflowException) {
        //        Console.WriteLine("The number is too large or too small.");
        //    }
        //}

        Board board = new Board(Turn.X, 6, 7);
        board.InitializeBoard();

        while (true) {
            Console.Clear();
            if (program.gameOver) {
                Console.ForegroundColor = ConsoleColor.Green;
                program.totalGamesPlayed++;
                if (program.winner is not null) {
                    Console.WriteLine($"\n\nCongratulations, {program.winner}! You win!\n");
                    if (program.winner == Turn.X) {
                        program.gamesWonByX++;
                    }
                } else {
                    Console.WriteLine("\n\nIt's a draw.\n");
                    program.gamesDrawn++;
                }
                Console.WriteLine("Here's the final board:\n\n");
                Console.Write(board.ToString());
                Console.ResetColor();
                Console.WriteLine($"X has won {program.gamesWonByX} game{(program.gamesWonByX == 1 ? "" : "s")}. O has won {program.totalGamesPlayed - program.gamesWonByX - program.gamesDrawn} game{(program.totalGamesPlayed - program.gamesWonByX - program.gamesDrawn == 1 ? "" : "s")}. There ha{(program.gamesDrawn == 1 ? "s" : "ve")} been {program.gamesDrawn} draw{(program.gamesDrawn == 1 ? "" : "s")}.");
                Console.WriteLine("Press space to play again.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Press Esc to exit.");
                Console.ResetColor();

                Boolean playAgain = Program.playAgain();

                if (playAgain) {
                    program.gameOver = false;
                    program.winner = null;
                    board.InitializeBoard();
                    continue;
                } else if (!playAgain) {
                    break;

                }
            }
            Console.WriteLine($"It's {board.turn}'s turn.");
            Console.WriteLine("Press the left and right arrow keys to move your piece.");
            Console.WriteLine("Then press space to release your piece.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Esc to exit.\n\n");
            Console.ResetColor();

            Console.Write(board.DropZone(board.turn));
            Console.Write(board.ToString());

            if (program.gameError == GameError.ColumnFullError) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That column is full!");
                Console.ResetColor();
            } else if (program.gameError == GameError.OutOfBoundsError) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't move any further that way!");
                Console.ResetColor();
            }
            program.gameError = null;

            // Read a key from the console without displaying it
            var keyInfo = Console.ReadKey(intercept: true);

            //    // Check which key was pressed
            if (keyInfo.Key == ConsoleKey.Escape) {
                // Exit the loop if the Esc key is pressed
                break;
            } else if (keyInfo.Key == ConsoleKey.Spacebar) {
                if (board.DropPiece(board.turn)) {
                    program.gameError = null;
                    if (board.CheckWin(board.turn)) {
                        program.gameOver = true;
                        program.winner = board.turn;
                        continue;
                    } else if (board.CheckTie()) {
                        program.gameOver = true;
                        program.winner = null;
                        continue;
                    };
                    board.ChangeTurn();
                    continue;
                } else {
                    program.gameError = GameError.ColumnFullError;
                };
            } else if (keyInfo.Key == ConsoleKey.LeftArrow) {
                if (board.SelectedColumn <= 0) {
                    program.gameError = GameError.OutOfBoundsError;
                } else {
                    board.SelectedColumn--;
                }
            } else if (keyInfo.Key == ConsoleKey.RightArrow) {
                if (board.SelectedColumn >= board.Columns - 1) {
                    program.gameError = GameError.OutOfBoundsError;
                } else {
                    board.SelectedColumn++;
                }
            }
        }
    }
}