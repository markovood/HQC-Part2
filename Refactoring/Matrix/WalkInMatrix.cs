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
            this.Walk(ref this.cellCounter);

            this.FindCell(ref this.row, ref this.col);

            // if position is matrix[0, 0] that means it is already full
            if (this.row != 0 && this.col != 0)
            {
                this.dx = 1;
                this.dy = 1;

                this.Walk(ref this.cellCounter);
            }

            this.Print(this.writer);
        }

        private void Walk(ref int cellCounter)
        {
            while (true)
            {
                this.matrix[this.row, this.col] = cellCounter;

                if (!this.CheckAvailability(this.row, this.col))
                {
                    cellCounter++;
                    break;
                }

                if (this.IsNextCellIllegal())
                {
                    while (this.IsNextCellIllegal())
                    {
                        this.ChangeDirection(ref this.dx, ref this.dy);
                    }
                }

                this.row += this.dx;
                this.col += this.dy;
                cellCounter++;
            }
        }

        private bool IsNextCellIllegal()
        {
            if (this.row + this.dx >= this.SizeN ||
                this.row + this.dx < 0 ||
                this.col + this.dy >= this.SizeN ||
                this.col + this.dy < 0 ||
                this.matrix[this.row + this.dx, this.col + this.dy] != 0)
            {
                return true;
            }

            return false;
        }

        private void ChangeDirection(ref int dx, ref int dy)
        {
            // possible directions according to current x/y (1 means below/next current row/col, -1 above/before, 0 the
            // same row/col)
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            int cd = 0;
            for (int i = 0; i < 8; i++)
            {
                if (dirX[i] == dx && dirY[i] == dy)
                {
                    cd = i;
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

        private bool CheckAvailability(int x, int y)
        {
            // possible directions according to current x/y (1 means below/next current row/col, -1 above/before, 0 the
            // same row/col)
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            // Check if position is inside the matrix
            for (int i = 0; i < 8; i++)
            {
                if (x + dirX[i] >= this.SizeN || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }

                if (y + dirY[i] >= this.SizeN || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            // Check if the cell is empty
            for (int i = 0; i < 8; i++)
            {
                if (this.matrix[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void FindCell(ref int x, ref int y)
        {
            x = 0;
            y = 0;
            for (int row = 0; row < this.SizeN; row++)
            {
                for (int col = 0; col < this.SizeN; col++)
                {
                    if (this.matrix[row, col] == 0)
                    {
                        x = row;
                        y = col;
                        return;
                    }
                }
            }
        }

        private void Print(IWritable writer)
        {
            for (int row = 0; row < this.SizeN; row++)
            {
                for (int col = 0; col < this.SizeN; col++)
                {
                    writer.Write(string.Format("{0,3}", this.matrix[row, col]));
                }

                writer.WriteLine();
            }
        }
    }
}
