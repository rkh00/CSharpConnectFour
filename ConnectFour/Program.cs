using ConnectFour;

class Program {

    private Boolean gameOver = false;
    private Turn winner;

    public static void Main() {

        Program program = new Program();
        Board board = new Board(Turn.X);
        board.InitializeBoard();

        while (true) {
            if (program.gameOver) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Congratulations, {board.turn}! You win!\n");
                Console.WriteLine("Here's the final board:\n\n");
                Console.Write(board.ToString());
                Console.ResetColor();
                break;
            }
            Console.WriteLine($"It's {board.turn}'s turn.");
            Console.WriteLine("Press the left and right arrow keys to move your piece.");
            Console.WriteLine("Then press space to release your piece.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Esc to exit.\n\n");
            Console.ResetColor();

            Console.Write(board.DropZone(board.turn));
            Console.Write(board.ToString());

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
                        continue;
                    };
                    board.ChangeTurn();
                    continue;
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That column is full!");
                    Console.ResetColor();
                };
            } else if ((keyInfo.Key == ConsoleKey.LeftArrow) && (board.SelectedColumn >= 0)) {
                board.SelectedColumn--;
            } else if ((keyInfo.Key == ConsoleKey.RightArrow) && (board.SelectedColumn < board.Columns - 1)) {
                board.SelectedColumn++;
            } else {
                continue;
            }
            //}
        }
    }
}
