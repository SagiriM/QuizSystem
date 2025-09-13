using System.IO;
using System.Text;

namespace MHS.QuizSystem;

public class FileExamPaperRepository : IExamPaperRepository
{
    private string path;

    public FileExamPaperRepository(string? path = null)
    {
        this.path = path??Path.Combine(Directory.GetCurrentDirectory(), "Results");
    }


    public void Save(ExamPaper paper)
    {
        Directory.CreateDirectory(path);
        {
            string filePath = Path.Combine(path, $"{paper.StudentId}-{paper.StudentName}.txt");
            using StreamWriter writer = new (filePath);
            string paperString = paper.ToString();
            writer.Write(paperString);
        }
    }
}
