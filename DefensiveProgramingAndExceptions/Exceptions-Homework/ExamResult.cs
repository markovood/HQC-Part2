using System;

public class ExamResult
{
    public ExamResult(int grade, int minGrade, int maxGrade, string comments)
    {
        if (grade < 0)
        {
            throw new ArgumentException("Not allowed negative grades");
        }

        if (minGrade < 0)
        {
            throw new ArgumentException("Not allowed minGrade to be negative");
        }

        if (maxGrade <= minGrade)
        {
            throw new ArgumentException("maxGrade cannot be less than or equal to minGrade");
        }

        if (string.IsNullOrEmpty(comments))
        {
            throw new ArgumentNullException("comments", "Cannot be null or empty!");
        }

        this.Grade = grade;
        this.MinGrade = minGrade;
        this.MaxGrade = maxGrade;
        this.Comments = comments;
    }

    public int Grade { get; private set; }

    public int MinGrade { get; private set; }

    public int MaxGrade { get; private set; }

    public string Comments { get; private set; }
}
