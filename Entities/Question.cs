using System.Text;
namespace MHS.QuizSystem;

public class Question
{
    public Guid Id {get; init;}
    public int Number {get; set;}
    public QuestionType Type {get; set;}
    public string Content {get; set;}
    public string Answer {get; set;}

    public Question(Guid id, int number, QuestionType type, string content, string answer)
    {
        Id = id;
        Number = number;
        Type = type;
        Content = content;
        Answer = answer;   
    }
    public Question(int number, QuestionType type, string content, string answer)
        :this(Guid.NewGuid(), number, type, content, answer) {}

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append($"Id: {Id}\n")
            .Append($"编号：{Number}\n")
            .Append($"类型：{Type.GetFriendlyString()}\n")
            .Append($"内容：{Content}\n")
            .Append($"答案：{Answer}\n");
        return builder.ToString();
    }
    public string ToQuizString()
    {
        StringBuilder builder = new();
        builder.Append($"题目类型：{Type.GetFriendlyString()}\n")
            .Append($"题目内容：{Content}\n");
        return builder.ToString();
    }
    public string ToReportString()
    {
        StringBuilder builder = new();
        builder.Append($"题目编号：{Number}\n")
            .Append($"题目类型：{Type.GetFriendlyString()}\n")
            .Append($"题目内容：{Content}\n")
            .Append($"题目答案：{Answer}\n");
        return builder.ToString();
    }

    public string ToSearchString()
    {
        StringBuilder builder = new();
        builder.Append($"ID: {Id}\t")
            .Append($"编号：{Number.ToString().PadLeft(3,' ')}\t")
            .Append($"类型：{Type.GetFriendlyString()}\n");
        return builder.ToString();
    }

    public static Question Parse(string text, string separator)
    {
        if(string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException($"text为空");

        string[] lines = text.Split(separator);
        if(lines.Length != 5)
            throw new InvalidDataException($"数据不完整");

        if(!Guid.TryParse(lines[0]?.Trim(), out Guid id))
            throw new FormatException($"ID非法：'{lines[0]}'");

        if(!int.TryParse(lines[1]?.Trim(), out int number))
            throw new FormatException($"题号非法：'{lines[1]}'");

        string? typeString = lines[2]?.Trim();
        if(string.IsNullOrWhiteSpace(typeString))
            throw new InvalidDataException($"类型不能为空");
        if(!Enum.TryParse(typeString, true, out QuestionType type))
            throw new InvalidDataException($"类型非法");

        string? content = lines[3]?.Trim();
        if(string.IsNullOrWhiteSpace(content))
            throw new InvalidDataException($"内容不能为空");

        string? answer = lines[4]?.Trim();
        if(string.IsNullOrWhiteSpace(answer))
            throw new InvalidDataException($"答案不能为空");
        return new(number, type, content, answer);
    }
}

