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
}


