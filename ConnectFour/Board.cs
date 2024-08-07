using System.Text;

namespace ConnectFour {
    internal class Board {

        internal int Rows = 6;
        internal int Columns = 7;
        private String[,] grid;
        internal Turn turn = Turn.X;
        internal int SelectedColumn;

        public Board(Turn turn) {
            grid = new string[Rows, Columns];
            this.turn = turn;
        }

        internal void ChangeTurn() {
            if (this.turn == Turn.X) {
                this.turn = Turn.O;
            } else {
                this.turn = Turn.X;
            }
            SelectedColumn = Columns / 2;
        }

        internal void InitializeBoard() {
            for (int row = 0; row < Rows; row++) {
                for (int col = 0; col < Columns; col++) {
                    grid[row, col] = "[ ]";
                }
            }
            SelectedColumn = Columns / 2;
        }

        internal bool DropPiece(Turn turn) {

            for (int row = Rows - 1; row >= 0; row--) {
                if (grid[row, SelectedColumn] == "[ ]") {
                    grid[row, SelectedColumn] = $"[{turn.ToString()}]";
                    return true;
                }
            }

            return false;
        }

        internal string DropZone(Turn turn) {
            string blankSpace = "   ";

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < SelectedColumn; i++) {
                builder.Append(blankSpace);
            }

            builder.Append($" {turn.ToString()} \n");

            return builder.ToString();
        }

        public bool CheckWin(Turn turn) {
            // Check horizontal, vertical, and diagonal wins
            string relevantPiece = $"[{turn.ToString()}]";
            return CheckHorizontalWin(relevantPiece) || CheckVerticalWin(relevantPiece) || CheckDiagonalWin(relevantPiece);
        }

        private bool CheckHorizontalWin(string relevantPiece) {
            for (int row = 0; row < Rows; row++) {
                for (int col = 0; col < Columns - 3; col++) {
                    if (grid[row, col] == relevantPiece &&
                        grid[row, col + 1] == relevantPiece &&
                        grid[row, col + 2] == relevantPiece &&
                        grid[row, col + 3] == relevantPiece) {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckVerticalWin(string relevantPiece) {
            for (int col = 0; col < Columns; col++) {
                for (int row = 0; row < Rows - 3; row++) {
                    if (grid[row, col] == relevantPiece &&
                        grid[row + 1, col] == relevantPiece &&
                        grid[row + 2, col] == relevantPiece &&
                        grid[row + 3, col] == relevantPiece) {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckDiagonalWin(string relevantPiece) {
            for (int row = 0; row < Rows - 3; row++) {
                for (int col = 0; col < Columns - 3; col++) {
                    if (grid[row, col] == relevantPiece &&
                        grid[row + 1, col + 1] == relevantPiece &&
                        grid[row + 2, col + 2] == relevantPiece &&
                        grid[row + 3, col + 3] == relevantPiece) {
                        return true;
                    }
                }
            }

            for (int row = 3; row < Rows; row++) {
                for (int col = 0; col < Columns - 3; col++) {
                    if (grid[row, col] == relevantPiece &&
                        grid[row - 1, col + 1] == relevantPiece &&
                        grid[row - 2, col + 2] == relevantPiece &&
                        grid[row - 3, col + 3] == relevantPiece) {
                        return true;
                    }
                }
            }

            return false;
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; row < Rows; row++) {
                for (int col = 0; col < Columns; col++) {
                    builder.Append(grid[row, col]);
                }
                builder.Append('\n');
            }
            builder.Append('\n');
            return builder.ToString();
        }
    }
}