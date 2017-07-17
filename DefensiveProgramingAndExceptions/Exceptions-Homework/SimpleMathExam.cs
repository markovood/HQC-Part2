using System;

public class SimpleMathExam : Exam
{
    public SimpleMathExam(int problemsSolved)
    {
        if (problemsSolved < 0)
        {
            this.ProblemsSolved = 0;
        }

        if (problemsSolved > 2)
        {
            this.ProblemsSolved = 2;
        }

        this.ProblemsSolved = problemsSolved;
    }

    public int ProblemsSolved { get; private set; }
    
    public override ExamResult GetExamResult()
    {
        if (ProblemsSolved == 0)
        {
            return new ExamResult(2, 2, 6, "Bad result: nothing done.");
        }
        else if (ProblemsSolved == 1)
        {
            return new ExamResult(4, 2, 6, "Average result: good.");
        }
        else if (ProblemsSolved == 2)
        {
            return new ExamResult(6, 2, 6, "Great result: good job.");
        }

        throw new InvalidOperationException("Invalid number of problems solved!");
    }
}
