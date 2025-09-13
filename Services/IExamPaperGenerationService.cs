using MHS.QuizSystem;

public interface IExamPaperGenerationService
{
    public ExamPaper GenerateExamPaper(int id, string name, DateTime date, int scCount, int tfCount, int pCount);
    public string GenerateReport(ExamPaper paper, string[] answers);
}
