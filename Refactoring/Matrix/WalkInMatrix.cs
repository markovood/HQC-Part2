using System;

namespace Matrix
{
    public class WalkInMatrix
    {
        private int n;

        private int[,] matrix;

        private int cellCounter, row, col, dx, dy;

        private IReadable reader;

        private IWritable writer;

        public WalkInMatrix(IReadable reader, IWritable writer)
        {
            this.reader = reader;
            this.writer = writer;

            this.SizeN = reader.ReadSize();
            this.matrix = new int[this.SizeN, this.SizeN];
            this.cellCounter = 1;
            this.row = 0;
            this.col = 0;
            this.dx = 1;
            this.dy = 1;
        }

        public int[,] Matrix
        {
            get
            {
                return this.matrix;
            }
        }

        public int SizeN
        {
            get
            {
                return this.n;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("n", "Size cannot be less than 1");
                }

                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("n", "Size cannot be greater than 100");
                }

                this.n = value;
            }
        }

        public static void Main()
        {
            var reader = new Reader();
            var writer = new Writer();
            var walk = new WalkInMatrix(reader, writer);
            walk.Execute();
        }

        public void Execute()
        {
            this.Walk(this.matrix, this.row, this.col, this.dx, this.dy, ref this.cellCounter);

            this.FindCell(this.matrix, out this.row, out this.col);

            // if position is matrix[0, 0] that means it is already full
            if (this.row != 0 && this.col != 0)
            {
                this.dx = 1;
                this.dy = 1;

                this.Walk(this.matrix, this.row, this.col, this.dx, this.dy, ref this.cellCounter);
            }

            this.Print(this.matrix, this.writer);
        }

        private void Walk(int[,] matrix, int row, int col, int dx, int dy, ref int cellCounter)
        {
            while (true)
            {
                matrix[row, col] = cellCounter;

                if (!this.CheckAvailability(matrix, row, col))
                {
                    cellCounter++;
                    break;
                }

                if (row + dx >= this.SizeN ||
                    row + dx < 0 ||
                    col + dy >= this.SizeN ||
                    col + dy < 0 ||
                    matrix[row + dx, col + dy] != 0)
                {
                    while (row + dx >= this.SizeN ||
                            row + dx < 0 ||
                            col + dy >= this.SizeN ||
                            col + dy < 0 ||
                            matrix[row + dx, col + dy] != 0)
                    {
                        this.ChangeDirection(ref dx, ref dy);
                    }
                }

                row += dx;
                col += dy;
                cellCounter++;
            }
        }

        private void ChangeDirection(ref int dx, ref int dy)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            int cd = 0;
            for (int count = 0; count < 8; count++)
            {
                if (dirX[count] == dx && dirY[count] == dy)
                {
                    cd = count;
                    break;
                }
            }

            if (cd == 7)
            {
                dx = dirX[0];
                dy = dirY[0];
                return;
            }

            dx = dirX[cd + 1];
            dy = dirY[cd + 1];
        }

        private bool CheckAvailability(int[,] arr, int x, int y)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (x + dirX[i] >= arr.GetLength(0) || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }

                if (y + dirY[i] >= arr.GetLength(0) || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (arr[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void FindCell(int[,] arr, out int x, out int y)
        {
            x = 0;
            y = 0;
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(0); col++)
                {
                    if (arr[row, col] == 0)
                    {
                        x = row;
                        y = col;
                        return;
                    }
                }
            }
        }

        private void Print(int[,] matrix, IWritable writer)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    writer.Write(string.Format("{0,3}", matrix[row, col]));
                }

                writer.WriteLine();
            }
        }
    }
}
