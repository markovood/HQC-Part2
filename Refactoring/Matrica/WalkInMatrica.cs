using System;

namespace Matrica
{
    public static class WalkInMatrica
    {
        private static int[,] matrix;

        private static int n, cellCounter, row, col, directionX, directionY;

        private static void Change(ref int directionX, ref int directionY)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int cd = 0;
            for (int count = 0; count < 8; count++)
            {
                if (dirX[count] == directionX && dirY[count] == directionY)
                {
                    cd = count;
                    break;
                }
            }

            if (cd == 7)
            {
                directionX = dirX[0];
                directionY = dirY[0];
                return;
            }

            directionX = dirX[cd + 1];
            directionY = dirY[cd + 1];
        }

        private static bool Proverka(int[,] arr, int x, int y)
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

        private static void FindCell(int[,] arr, out int x, out int y)
        {
            x = 0;
            y = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[i, j] == 0)
                    {
                        x = i;
                        y = j;
                        return;
                    }
                }
            }
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write("{0,3}", matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private static void Walk()
        {
            while (true)
            {
                matrix[row, col] = cellCounter;

                if (!Proverka(matrix, row, col))
                {
                    break;
                }

                if (row + directionX >= n ||
                    row + directionX < 0 ||
                    col + directionY >= n ||
                    col + directionY < 0 ||
                    matrix[row + directionX, col + directionY] != 0)
                {
                    while (row + directionX >= n ||
                            row + directionX < 0 ||
                            col + directionY >= n ||
                            col + directionY < 0 ||
                            matrix[row + directionX, col + directionY] != 0)
                    {
                        Change(ref directionX, ref directionY);
                    }
                }

                row += directionX;
                col += directionY;
                cellCounter++;
            }
        }

        public static void Main()
        {
            //Console.WriteLine("Enter a positive number: ");
            //string input = Console.ReadLine();
            //int n = 0;
            //while ( !int.TryParse( input, out n ) || n < 0 || n > 100 )
            //{
            //    Console.WriteLine( "You haven't entered a correct positive number" );
            //    input = Console.ReadLine(  );
            //}
            n = 6;
            matrix = new int[n, n];
            cellCounter = 1;
            row = 0;
            col = 0;
            directionX = 1;
            directionY = 1;

            Walk();

            PrintMatrix();

            FindCell(matrix, out row, out col);

            if (row != 0 && col != 0)
            { // taka go napravih, zashtoto funkciqta ne mi davashe da ne si definiram out parametrite
                directionX = 1;
                directionY = 1;

                Walk();
            }

            PrintMatrix();
        }
    }
}
