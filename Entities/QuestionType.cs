
public enum QuestionType
{
    SingleChoice,
    TrueFalse,
    Programming
}

public static class QuestionTypeExtensions
{
    public static bool TryParseStrict(string value, out QuestionType result)
    {
        if(Enum.TryParse(value, out result) && Enum.IsDefined(typeof(QuestionType), result)) return true;
        else return TryParseChinese(value, out result);
    }

    private static bool TryParseChinese(string value, out QuestionType result)
    {
        switch(value?.Trim())
        {
            case "单选题":
                result = QuestionType.SingleChoice;
                return true;
            case "判断题":
                result = QuestionType.TrueFalse;
                return true;
            case "编程题":
                result = QuestionType.Programming;
                return true;
            default:
                result = QuestionType.SingleChoice;
                return false;
        }
    }
    public static string GetFriendlyString(this QuestionType type) => 
        type switch
        {
            QuestionType.SingleChoice => "单选题",
            QuestionType.TrueFalse => "判断题",
            QuestionType.Programming => "编程题",
            _ => type.ToString()
        };
}
