using MHS.QuizSystem;

public class ExamPaperGenerationService : IExamPaperGenerationService
{
    private readonly IQuestionRepository qr;

    public ExamPaperGenerationService(IQuestionRepository qr)
    {
        this.qr = qr;
    }

    public ExamPaper GenerateExamPaper(int id, string name, DateTime date, int scCount, int tfCount, int pCount)
    {
        IEnumerable<Question> scs = qr.GetRandomly(QuestionType.SingleChoice, scCount);
        IEnumerable<Question> tfs = qr.GetRandomly(QuestionType.TrueFalse, tfCount);
        IEnumerable<Question> ps = qr.GetRandomly(QuestionType.Programming, pCount);
        IEnumerable<Question> questions = scs.Concat(tfs).Concat(ps);
        string[] answers = new string[questions.Count()];
        for(int i = 0; i < answers.Length; i++)
            answers[i] = string.Empty;
        ExamPaper p = new (id, name, date, questions, answers);
        return p;
    }
    public string GenerateReport(ExamPaper paper, string[] answers)
    {
        paper.Answers = answers;
        return paper.ToString();
    }
}
