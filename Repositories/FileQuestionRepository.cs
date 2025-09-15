using System.IO;
using System.Text;

namespace MHS.QuizSystem;

public class FileQuestionRepository : IQuestionRepository
{
    private List<Question> questions;
    private string path;

    public FileQuestionRepository(string? path = null)
    {
        this.path = path??Path.Combine(Directory.GetCurrentDirectory(), "Questions.txt");
        questions = Load();
    }

    public void Add(Question q) => questions.Add(q);
    public void Delete(Guid id)
    {
        Question q = questions.Single(q => q.Id == id);
        questions.Remove(q);
    }
    public void Update(Question q)
    {
        Question question = questions.Single(question => question.Id == q.Id);
        question.Number = q.Number;
        question.Type = q.Type;
        question.Content = q.Content;
        question.Answer = q.Answer;
    }
    public IEnumerable<Question> GetAll() => questions;
    public IEnumerable<Question> GetByContent(string content) => questions.Where(q => q.Content.Contains(content));
    public Question GetById(Guid id) => questions.Single(q => q.Id == id);
    public IEnumerable<Question> GetRandomly(QuestionType type, int count) 
    {
        if(count < 0) return new Question[0];
        else if(questions.Count(q => q.Type == type) < count) return new Question[0];
        else return questions.Where(q => q.Type == type).OrderBy(q => Random.Shared.Next()).Take(count).ToArray();
    }
    public IEnumerable<Question> GetByType(QuestionType type) => questions.Where(q => q.Type == type);
    public void Save()
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"文件不存在：{path}"); 
        using StreamWriter writer = new (path);
        StringBuilder content = new();
        foreach(Question q in questions)
        {
            content.Append(q.Id).Append("---\n")
                .Append(q.Number).Append("---\n")
                .Append(q.Type).Append("---\n")
                .Append(q.Content).Append("---\n")
                .Append(q.Answer).Append("\n")
                .Append("===\n");
        }
        content.Remove(content.Length - 4, 4);
        writer.Write(content.ToString());
    }

    private List<Question> Load()
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"文件不存在：{path}"); 
        List<Question> questions = new();
        string text = File.ReadAllText(path);
        if(string.IsNullOrWhiteSpace(text)) return questions;
        string[] qStrings = text.Split("===");
        foreach (string qString in qStrings)
        {
            Question q = Question.Parse(qString, "---");
            questions.Add(q);
        }
        return questions;
    }
}
