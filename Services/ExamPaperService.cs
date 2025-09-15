using MHS.QuizSystem;

public class ExamPaperService : IExamPaperService
{
    private readonly IQuestionRepository qr;

    public ExamPaperService(IQuestionRepository qr)
    {
        this.qr = qr;
    }

    public ExamPaper GeneratePaper(Student student, DateTime date, int scCount, int tfCount, int pCount)
    {
        IEnumerable<Question> scs = qr.GetRandomly(QuestionType.SingleChoice, scCount);
        IEnumerable<Question> tfs = qr.GetRandomly(QuestionType.TrueFalse, tfCount);
        IEnumerable<Question> ps = qr.GetRandomly(QuestionType.Programming, pCount);
        IEnumerable<Question> questions = scs.Concat(tfs).Concat(ps);
        string[] answers = new string[questions.Count()];
        for(int i = 0; i < answers.Length; i++)
            answers[i] = string.Empty;
        ExamPaper p = new (student, date, questions, answers);
        return p;
    }
    public string GenerateReport(ExamPaper paper, string[] answers)
    {
        paper.Answers = answers;
        return paper.ToReportString();
    }
}
