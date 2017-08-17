using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevToolsDemo
{
    public class DevToolsDemo
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Console.Write("Please specify the base s: ");

            int baseS = int.Parse(Console.ReadLine());

            Console.Write("Please enter the base {0} number to convert from: ", baseS);
            string number = Console.ReadLine().ToUpper();

            Console.Write("Please specify the base d: ");
            int baseD = int.Parse(Console.ReadLine());

            #region Validations

            if (baseS < 2 || baseS > 16)
            {
                while (baseS < 2 || baseS > 16)
                {
                    Console.Write("base s cannot be less than 2 a greater than 16\nPlease enter base s so s >= 2 and s <= 16: ");
                    baseS = int.Parse(Console.ReadLine());
                }
            }

            if (baseD > 16 || baseD < 2)
            {
                while (baseD > 16 || baseD < 2)
                {
                    Console.Write("base d cannot be greater than 16 or less than 2\nPlease enter base d so d <= 16 and d >=2: ");
                    baseD = int.Parse(Console.ReadLine());
                }
            }

            if (baseD == baseS)
            {
                while (baseD == baseS || baseD > 16)
                {
                    Console.Write("base d must be less than or greater than base s\nPlease enter base d so d != s and d <= 16: ");
                    baseD = int.Parse(Console.ReadLine());
                }
            }

            #endregion Validations

            number = Validate(number, baseS);

            BigInteger decimalNum = ConvertToDecimal(number, baseS);
            string convertedValue = ConvertFromDecimal(decimalNum, baseD);

            Console.Clear();
            convertedValue = RemoveLeadingZeroes(convertedValue);
            Console.WriteLine("{0} in base({1}) system is\n{2} in base({3}) system", number, baseS, convertedValue, baseD);
        }

        private static string RemoveLeadingZeroes(string convertedValue)
        {
            StringBuilder converted = new StringBuilder(convertedValue);

            int index = 0;
            int length = 0;

            while (converted[index] == '0')
            {
                length++;
                index++;
            }

            converted.Remove(0, length);
            return converted.ToString();
        }

        private static string Validate(string number, int baseS)
        {
            bool charIsValid = false;
            char[] masterChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            char[] allowedChars = new char[baseS];

            for (int i = 0; i < allowedChars.Length; i++)
            {
                allowedChars[i] = masterChars[i];
            }

            while (!charIsValid)
            {
                foreach (char symbol in number)
                {
                    if (allowedChars.Contains(symbol))
                    {
                        charIsValid = true;
                    }
                    else
                    {
                        charIsValid = false;
                        Console.Write("The number you have entered has some invalid digits\nTry again: ");
                        number = Console.ReadLine().ToUpper();
                        break;
                    }
                }
            }

            return number;
        }

        private static BigInteger ConvertToDecimal(string number, int baseS)
        {
            BigInteger decimalNum = 0;
            int lastIndex = number.Length - 1;

            for (int i = 0; i < number.Length; i++)
            {
                switch (number[lastIndex])
                {
                    case 'A':
                        decimalNum += (BigInteger)(10 * (Math.Pow(baseS, i)));
                        break;

                    case 'B':
                        decimalNum += (BigInteger)(11 * (Math.Pow(baseS, i)));
                        break;

                    case 'C':
                        decimalNum += (BigInteger)(12 * (Math.Pow(baseS, i)));
                        break;

                    case 'D':
                        decimalNum += (BigInteger)(13 * (Math.Pow(baseS, i)));
                        break;

                    case 'E':
                        decimalNum += (BigInteger)(14 * (Math.Pow(baseS, i)));
                        break;

                    case 'F':
                        decimalNum += (BigInteger)(15 * (Math.Pow(baseS, i)));
                        break;

                    default:
                        decimalNum += (BigInteger)((number[lastIndex] - '0') * (Math.Pow(baseS, i)));
                        break;
                }

                lastIndex--;
            }

            return decimalNum;
        }

        private static string ConvertFromDecimal(BigInteger decimalNum, int baseD)
        {
            string convertedValue = "";
            string tempValue = "";

            while (decimalNum != 0)
            {
                BigInteger remain = decimalNum % baseD;

                if (remain == 10)
                {
                    tempValue += 'A';
                }
                else if (remain == 11)
                {
                    tempValue += 'B';
                }
                else if (remain == 12)
                {
                    tempValue += 'C';
                }
                else if (remain == 13)
                {
                    tempValue += 'D';
                }
                else if (remain == 14)
                {
                    tempValue += 'E';
                }
                else if (remain == 15)
                {
                    tempValue += 'F';
                }
                else
                {
                    tempValue += remain;
                }

                decimalNum /= baseD;
            }

            // reversing
            for (int i = tempValue.Length - 1; i >= 0; i--)
            {
                convertedValue += tempValue[i];
            }

            return convertedValue;
        }
    }
}
