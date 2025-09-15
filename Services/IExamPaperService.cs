using MHS.QuizSystem;

public interface IExamPaperService
{
    public ExamPaper GeneratePaper(Student student, DateTime date, int scCount, int tfCount, int pCount);
    public string GenerateReport(ExamPaper paper, string[] answers);
}
