using System;

namespace Matrix
{
    public class WalkInMatrix
    {
        public static void Main()
        {
            Console.WriteLine("Enter a positive number ");
            string input = Console.ReadLine();
            int n = 0;
            while (!int.TryParse(input, out n) ||
                    n < 0 ||
                    n > 100)
            {
                Console.WriteLine("You haven't entered a correct positive number");
                input = Console.ReadLine();
            }

            // int n = 3;
            int[,] matrix = new int[n, n];
            int cellCounter = 1, row = 0, col = 0, dx = 1, dy = 1;
            while (true)
            {
                matrix[row, col] = cellCounter;

                if (!CheckAvailability(matrix, row, col))
                {
                    break;
                }

                if (row + dx >= n ||
                    row + dx < 0 ||
                    col + dy >= n ||
                    col + dy < 0 ||
                    matrix[row + dx, col + dy] != 0)
                {
                    while (row + dx >= n ||
                            row + dx < 0 ||
                            col + dy >= n ||
                            col + dy < 0 ||
                            matrix[row + dx, col + dy] != 0)
                    {
                        Change(ref dx, ref dy);
                    }
                }

                row += dx;
                col += dy;
                cellCounter++;
            }

            for (int p = 0; p < n; p++)
            {
                for (int q = 0; q < n; q++)
                {
                    Console.Write("{0,3}", matrix[p, q]);
                }

                Console.WriteLine();
            }

            FindCell(matrix, out row, out col);

            if (row != 0 && col != 0)
            { // taka go napravih, zashtoto funkciqta ne mi davashe da ne si definiram out parametrite
                dx = 1;
                dy = 1;

                while (true)
                {
                    matrix[row, col] = cellCounter;
                    if (!CheckAvailability(matrix, row, col))
                    {
                        break;
                    }

                    if (row + dx >= n ||
                        row + dx < 0 ||
                        col + dy >= n ||
                        col + dy < 0 ||
                        matrix[row + dx, col + dy] != 0)
                    {
                        while (row + dx >= n ||
                                row + dx < 0 ||
                                col + dy >= n ||
                                col + dy < 0 ||
                                matrix[row + dx, col + dy] != 0)
                        {
                            Change(ref dx, ref dy);
                        }
                    }

                    row += dx;
                    col += dy;
                    cellCounter++;
                }
            }

            for (int pp = 0; pp < n; pp++)
            {
                for (int qq = 0; qq < n; qq++)
                {
                    Console.Write("{0,3}", matrix[pp, qq]);
                }

                Console.WriteLine();
            }
        }

        private static void Change(ref int dx, ref int dy)
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

        private static bool CheckAvailability(int[,] arr, int x, int y)
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
    }
}
