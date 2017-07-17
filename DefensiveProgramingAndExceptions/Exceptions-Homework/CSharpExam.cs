using System;

public class CSharpExam : Exam
{
    public CSharpExam(int score)
    {
        if (score < 0)
        {
            throw new ArgumentException("Not allowed negative score");
        }

        if (score > 100)
        {
            throw new ArgumentException("Not allowed score greater than 100");
        }

        this.Score = score;
    }

    public int Score { get; private set; }

    public override ExamResult GetExamResult()
    {
        return new ExamResult(this.Score, 0, 100, "Exam results calculated by score.");
    }
}
