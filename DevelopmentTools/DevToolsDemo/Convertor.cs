namespace OneSystemToOther
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Converts from any numeral system (between 2 and 16) to any numeral system (between 2 and 16)
    /// </summary>
    public static class Convertor
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Please specify the base s: ");
            int baseS = 10;

            Console.WriteLine("Please enter the base {0} number to convert from: ", baseS);
            string number = "1985";

            Console.WriteLine("Please specify the base d: ");
            int baseD = 2;

            string convertedNumber = Convertor.Execute(baseS, number, baseD);

            Console.WriteLine("{0} in base({1}) system is\n{2} in base({3}) system", number, baseS, convertedNumber, baseD);
        }

        public static string Execute(int baseS, string number, int baseD)
        {            
            ValidateBases(baseS, baseD);
            ValidateNumber(number, baseS);

            string convertedValue = Convert(number, baseS, baseD);
            
            return convertedValue;
        }
        
        private static string Convert(string number, int baseS, int baseD)
        {
            BigInteger decimalNum = ConvertToDecimal(number, baseS);
            string convertedValue = ConvertFromDecimal(decimalNum, baseD);
            convertedValue = RemoveLeadingZeroes(convertedValue);

            return convertedValue;
        }

        /// <summary>
        /// Validates that the bases are always in the range between 2 and 16 and never equal to each other. 
        /// </summary>
        /// <param name="baseFrom">The base to convert from</param>
        /// <param name="baseTo">The base to convert to</param>
        private static void ValidateBases(int baseFrom, int baseTo)
        {
            if (baseFrom < 2 || baseFrom > 16)
            {
                throw new ArgumentOutOfRangeException("baseS", "Cannot be less than 2 or greater than 16");
            }

            if (baseTo > 16 || baseTo < 2)
            {
                throw new ArgumentOutOfRangeException("baseD", "Cannot be greater than 16 or less than 2");
            }

            if (baseTo == baseFrom)
            {
                throw new ArgumentOutOfRangeException("Bases cannot be equal!");
            }
        }

        /// <summary>
        /// Removes any leading zeros, if any
        /// </summary>
        /// <param name="convertedValue">The number to be cleaned from zeros</param>
        /// <returns>The number's string with no leading zeros</returns>
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

        /// <summary>
        /// Validates that the passed number corresponds to the passed base system
        /// </summary>
        /// <param name="number">The number to be validated</param>
        /// <param name="baseS">The base system against which is validating</param>
        private static void ValidateNumber(string number, int baseS)
        {
            char[] masterChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            char[] allowedChars = new char[baseS];

            for (int i = 0; i < allowedChars.Length; i++)
            {
                allowedChars[i] = masterChars[i];
            }

            foreach (char symbol in number)
            {
                if (!allowedChars.Contains(symbol))
                {
                    throw new ArgumentException("The number you have entered has some invalid digits");
                }
            }
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
                        decimalNum += (BigInteger)(10 * Math.Pow(baseS, i));
                        break;

                    case 'B':
                        decimalNum += (BigInteger)(11 * Math.Pow(baseS, i));
                        break;

                    case 'C':
                        decimalNum += (BigInteger)(12 * Math.Pow(baseS, i));
                        break;

                    case 'D':
                        decimalNum += (BigInteger)(13 * Math.Pow(baseS, i));
                        break;

                    case 'E':
                        decimalNum += (BigInteger)(14 * Math.Pow(baseS, i));
                        break;

                    case 'F':
                        decimalNum += (BigInteger)(15 * Math.Pow(baseS, i));
                        break;

                    default:
                        decimalNum += (BigInteger)((number[lastIndex] - '0') * Math.Pow(baseS, i));
                        break;
                }

                lastIndex--;
            }

            return decimalNum;
        }

        private static string ConvertFromDecimal(BigInteger decimalNum, int baseD)
        {
            string convertedValue = string.Empty;
            string tempValue = string.Empty;

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
