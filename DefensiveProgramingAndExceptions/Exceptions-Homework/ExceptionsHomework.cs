using System;
using System.Collections.Generic;
using System.Text;

public class ExceptionsHomework
{
    public static T[] Subsequence<T>(T[] arr, int startIndex, int count)
    {
        if (startIndex < 0 || startIndex > arr.Length)
        {
            throw new ArgumentOutOfRangeException("startIndex",
                "Cannot start from negative position or position that is greater than the whole length!");
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("count", "Cannot be negative!");
        }

        if (count == 0)
        {
            throw new ArgumentOutOfRangeException("count", "Cannot be 0!");
        }

        if (count > (arr.Length - startIndex))
        {
            throw new ArgumentOutOfRangeException("count", 
                "Cannot be greater than the residual length of the array!");
        }

        List<T> result = new List<T>();
        for (int i = startIndex; i < startIndex + count; i++)
        {
            result.Add(arr[i]);
        }

        return result.ToArray();
    }

    public static string ExtractEnding(string str, int count)
    {
        if (string.IsNullOrEmpty(str))
        {
            throw new ArgumentException("Cannot operate over null or empty string!");
        }

        if (count > str.Length)
        {
            throw new ArgumentOutOfRangeException("count", "Count is greater than the length of the string!");
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("count", "Not allowed negative count!");
        }

        if (count == 0)
        {
            throw new ArgumentOutOfRangeException("count", "Cannot be 0!");
        }

        StringBuilder result = new StringBuilder();
        for (int i = str.Length - count; i < str.Length; i++)
        {
            result.Append(str[i]);
        }

        return result.ToString();
    }

    public static bool IsPrime(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Not allowed negative numbers!");
        }

        for (int divisor = 2; divisor <= Math.Sqrt(number); divisor++)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static void Main()
    {
        var substr = Subsequence("Hello!".ToCharArray(), 2, 3);
        Console.WriteLine(substr);

        var subarr = Subsequence(new int[] { -1, 3, 2, 1 }, 0, 2);
        Console.WriteLine(String.Join(" ", subarr));

        var allarr = Subsequence(new int[] { -1, 3, 2, 1 }, 0, 4);
        Console.WriteLine(String.Join(" ", allarr));

        //var emptyarr = Subsequence(new int[] { -1, 3, 2, 1 }, 0, 0);

        Console.WriteLine(ExtractEnding("I love C#", 2));
        Console.WriteLine(ExtractEnding("Nakov", 4));
        Console.WriteLine(ExtractEnding("beer", 4));
        //Console.WriteLine(ExtractEnding("Hi", 100));

        Console.WriteLine("23 is prime --> {0}", IsPrime(23));
        Console.WriteLine("33 is prime --> {0}", IsPrime(33));

        List<Exam> peterExams = new List<Exam>()
        {
            new SimpleMathExam(2),
            new CSharpExam(55),
            new CSharpExam(100),
            new SimpleMathExam(1),
            new CSharpExam(0),
        };
        Student peter = new Student("Peter", "Petrov", peterExams);
        double peterAverageResult = peter.CalcAverageExamResultInPercents();
        Console.WriteLine("Average results = {0:p0}", peterAverageResult);
    }
}
