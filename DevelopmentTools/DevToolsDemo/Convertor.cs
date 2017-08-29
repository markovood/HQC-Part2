[assembly: log4net.Config.XmlConfigurator(Watch = true)]

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
        /// <summary>
        /// Logging information about the program's behavior in different levels: Debug, Info, Warn, Error and Fatal.
        /// All levels are logged to a file Log.txt
        /// Error and Fatal levels are logged also to the Console.
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Entry point of the OneSystemToOther Console Application
        /// </summary>
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            
            Console.WriteLine("Please specify the base s: ");
            int baseS = int.Parse(Console.ReadLine());  // 10
            
            Console.WriteLine("Please enter the base {0} number to convert from: ", baseS);
            string number = Console.ReadLine().ToUpper();   // "1985"

            Console.WriteLine("Please specify the base d: ");
            int baseD = int.Parse(Console.ReadLine());  // 2

            Logger.Info(string.Format("Started conversion: {0} from base{1} to base{2}", number, baseS, baseD));
            
            string convertedNumber = Convertor.Execute(baseS, number, baseD); // 11111000001

            Console.WriteLine("{0} in base({1}) system is\n{2} in base({3}) system", number, baseS, convertedNumber, baseD);

            Logger.Info("Conversion done successfully!");

            // prevents closing of the console window after execution
            Console.ReadLine();
        }

        /// <summary>
        /// Validates bases, than validates number and finally converts the number from baseS into number from baseD numeral system
        /// </summary>
        /// <param name="baseS">Numeral system base to convert from</param>
        /// <param name="number">Number in baseS numeral system</param>
        /// <param name="baseD">Numeral system base to convert to</param>
        /// <returns>Number in baseD numeral system</returns>
        public static string Execute(int baseS, string number, int baseD)
        {
            ValidateBases(baseS, baseD);
            Logger.Debug("Bases are valid :)");

            ValidateNumber(number, baseS);
            Logger.Debug("Number is valid :)");

            Logger.Info("Conversion started...");
            string convertedValue = Convert(number, baseS, baseD);

            Logger.Info("Conversion finished!");
            return convertedValue;
        }

        /// <summary>
        /// Converts the number from baseS system into base 10 system, then from base 10 into baseD system
        /// </summary>
        /// <param name="number">The number to be converted</param>
        /// <param name="baseS">Base to be converted from</param>
        /// <param name="baseD">Base to be converted to</param>
        /// <returns>Number converted from baseS system into baseD system, without any leading zeros</returns>
        private static string Convert(string number, int baseS, int baseD)
        {
            Logger.Debug(string.Format("Converting from base{0} to decimal...", baseS));
            BigInteger decimalNum = ConvertToDecimal(number, baseS);

            Logger.Debug(string.Format("Done!\nConverting from decimal to base{0}...", baseD));
            string convertedValue = ConvertFromDecimal(decimalNum, baseD);

            Logger.Debug("Done!\nRemoving leading zeros if any...");
            convertedValue = RemoveLeadingZeroes(convertedValue);

            Logger.Debug("Done!");
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
                Logger.Error(string.Format("Entered invalid input for baseS - {0}", baseFrom));
                Logger.Fatal("BaseS cannot be less than 2 or greater than 16");
                throw new ArgumentOutOfRangeException("baseS", "Cannot be less than 2 or greater than 16");
            }

            if (baseTo > 16 || baseTo < 2)
            {
                Logger.Error(string.Format("Entered invalid input for baseD - {0}", baseTo));
                Logger.Fatal("BaseD cannot be greater than 16 or less than 2");
                throw new ArgumentOutOfRangeException("baseD", "Cannot be greater than 16 or less than 2");
            }

            if (baseTo == baseFrom)
            {
                Logger.Fatal(string.Format("Bases cannot be equal! BaseS = {0} / BaseD = {1}", baseFrom, baseTo));
                throw new ArgumentOutOfRangeException("Bases cannot be equal!");
            }
        }

        /// <summary>
        /// Removes any leading zeros, if present
        /// </summary>
        /// <param name="convertedValue">The number to be cleaned from zeros</param>
        /// <returns>The number with no leading zeros</returns>
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
            if (length > 0)
            {
                Logger.Info(string.Format("Removed {0} zeros from the left side of the number!", length));
            }

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
                    Logger.Error("The number you have entered has an invalid digit!");
                    Logger.Fatal(string.Format("Invalid digit found - '{0}'", symbol));
                    throw new ArgumentException("The number you have entered has some invalid digits");
                }
            }
        }

        /// <summary>
        /// Converts the baseS number into base 10 system
        /// </summary>
        /// <param name="number">The baseS number</param>
        /// <param name="baseS">The base system to convert from</param>
        /// <returns>BigInteger number that represents the baseS number in base 10 numeral system</returns>
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

        /// <summary>
        /// Converts a BigInteger base 10 number into baseD numeral system
        /// </summary>
        /// <param name="decimalNum">A number in base 10</param>
        /// <param name="baseD">The base system to convert to</param>
        /// <returns>The converted baseD number</returns>
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
