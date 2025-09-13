
public enum QuestionType
{
    SingleChoice,
    TrueFalse,
    Programming
}

public static class QuestionTypeExtensions
{
    public static string GetFriendlyString(this QuestionType type) => 
        type switch
        {
            QuestionType.SingleChoice => "单选题",
            QuestionType.TrueFalse => "判断题",
            QuestionType.Programming => "编程题",
            _ => type.ToString()
        };
}
