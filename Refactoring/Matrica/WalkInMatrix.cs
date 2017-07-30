using System;

namespace Matrica
{
    public class WalkInMatrix
    {
        private static void Change(ref int dx, ref int dy)
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
            // possible directions according to the current positionX
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            // possible directions according to the current positionY
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            for (int i = 0; i < 8; i++)
            {   // checks if passed x is inside the matrix
                if (x + dirX[i] >= matrix.GetLength(0) || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }
                // checks if passed y is inside the matrix
                if (y + dirY[i] >= matrix.GetLength(1) || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                // checks if the element at x,y is available
                if (matrix[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static void Find_cell(int[,] arr, out int x, out int y)
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

        private static void PrintMatrix(int[,] matrix)
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
            int n = 6;
            int[,] matrix = new int[n, n];

            int step = n, k = 1, row = 0, col = 0, dx = 1, dy = 1;
            while (true)
            {
                matrix[row, col] = k;

                if (!Proverka(matrix, row, col))
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
            Console.WriteLine("Enter a positive number: ");
            string input = Console.ReadLine();
            //int n = 0;
            while (!int.TryParse(input, out n) || n < 0 || n > 100)
            {
                Console.WriteLine("You haven't entered a correct positive number");
                input = Console.ReadLine();
            }
            //n = 6;
            matrix = new int[n, n];
            cellCounter = 1;
            row = 0;
            col = 0;
            directionX = 1;
            directionY = 1;

            PrintMatrix(matrix);

            Find_cell(matrix, out row, out col);

            Find_cell(matrica, out row, out col);

            if (row != 0 && col != 0)
            { // taka go napravih, zashtoto funkciqta ne mi davashe da ne si definiram out parametrite
                dx = 1; dy = 1;

                while (true)
                {
                    matrix[row, col] = k;
                    if (!Proverka(matrix, row, col))
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

                    row += dx; col += dy; k++;
                }
            }

            PrintMatrix(matrix);
        }
    }
}
