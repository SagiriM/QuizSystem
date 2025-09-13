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
    public Question GetById(Guid id) => questions.Single(q => q.Id == id);
    public IEnumerable<Question> GetRandomly(QuestionType type, int count) 
    {
        if(count < 0) return new Question[0];
        else if(questions.Count(q => q.Type == type) < count) return new Question[0];
        else return questions.Where(q => q.Type == type).OrderBy(q => Random.Shared.Next()).Take(count).ToArray();
    }
    public void Save()
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"文件不存在：{path}"); 
        using StreamWriter writer = new (path);
        StringBuilder content = new();
        foreach(Question q in questions)
        {
            content.Append(q.Id).Append("---\n");
            content.Append(q.Number).Append("---\n");
            content.Append(q.Type).Append("---\n");
            content.Append(q.Content).Append("---\n");
            content.Append(q.Answer).Append("---\n");
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
        string[] lines = text.Split("---");
        for (int i = 0; i < lines.Length; i += 5)
        {
            // 检查是否有足够的行数
            if (i + 4 >= lines.Length)
                throw new InvalidDataException($"数据不完整");

            // 验证并转换ID
            if (!Guid.TryParse(lines[i]?.Trim(), out Guid id))
                throw new FormatException($"ID非法：第{i}行 '{lines[i]}'");

            // 验证并转换题号
            if (!int.TryParse(lines[i + 1]?.Trim(), out int number))
                throw new FormatException($"题号非法：第{i + 1}行 '{lines[i + 1]}'");

            // 检查类型是否为空
            string? typeString = lines[i + 2]?.Trim();
            if (string.IsNullOrWhiteSpace(typeString))
                throw new InvalidDataException($"类型不能为空：第{i + 2}行");
            if (!Enum.TryParse(typeString, true, out QuestionType type))
                throw new InvalidDataException($"类型非法：第{i + 2}行");

            // 检查内容是否为空
            string? content = lines[i + 3]?.Trim();
            if (string.IsNullOrWhiteSpace(content))
                throw new InvalidDataException($"内容不能为空：第{i + 3}行");

            // 检查答案是否为空
            string? answer = lines[i + 4]?.Trim();
            if (string.IsNullOrWhiteSpace(answer))
                throw new InvalidDataException($"答案不能为空：第{i + 4}行");

            Question q = new(id, number, type, content, answer);
            questions.Add(q);
        }
        return questions;
    }
}
