using ConnectFour;

class Program {

    private Boolean gameOver = false;
    private Turn? winner;
    private Boolean colFull = false;

    public static void Main() {

        Program program = new Program();
        Board board = new Board(Turn.X);
        board.InitializeBoard();

        while (true) {
            Console.Clear();
            if (program.gameOver) {
                if (program.winner is not null) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\nCongratulations, {program.winner}! You win!\n");
                    Console.WriteLine("Here's the final board:\n\n");
                    Console.Write(board.ToString());
                    Console.ResetColor();
                    break;
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It's a draw.\n");
                    Console.WriteLine("Here's the final board:\n\n");
                    Console.Write(board.ToString());
                    Console.ResetColor();
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

            if (program.colFull) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That column is full!");
                Console.ResetColor();
            }

            // Read a key from the console without displaying it
            var keyInfo = Console.ReadKey(intercept: true);

            //    // Check which key was pressed
            if (keyInfo.Key == ConsoleKey.Escape) {
                // Exit the loop if the Esc key is pressed
                break;
            } else if (keyInfo.Key == ConsoleKey.Spacebar) {
                if (board.DropPiece(board.turn)) {
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
                    program.colFull = true;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("That column is full!");
                    //Console.ResetColor();
                };
            } else if ((keyInfo.Key == ConsoleKey.LeftArrow) && (board.SelectedColumn > 0)) {
                board.SelectedColumn--;
            } else if ((keyInfo.Key == ConsoleKey.RightArrow) && (board.SelectedColumn < board.Columns - 1)) {
                board.SelectedColumn++;
            } else {
                continue;
            }
        }
    }
}
