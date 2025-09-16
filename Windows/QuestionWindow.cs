using System.Text;
using MHS.QuizSystem;

public class QuestionWindow : FormWindow
{
    public Question Question {get; set;}

    public QuestionWindow():this(new Question(Guid.NewGuid(), 0, 0, "", ""))
    {
        Inputs = new string[] {"", "", "", ""};
    }
    
    public QuestionWindow(Question q)
    {
        Question = q;

        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Options = new string[] {"编  号：", "类  型：", "内  容：", "答  案:"};
        OptionAreas = new Area[] 
        {
            new(Area.X + Area.Width / 4 - 8, Area.Y + Area.Height / 4 + 1, 8, 1),
            new(Area.X + Area.Width / 4 - 8, Area.Y + Area.Height / 4 + 3, 8, 1),
            new(Area.X + Area.Width / 4 - 8, Area.Y + Area.Height / 4 + 5, 8, 20),
            new(Area.X + Area.Width / 4 - 8, Area.Y + Area.Height / 4 + 25, 8, 1),
        };
        Inputs = new string[] {q.Number.ToString(), q.Type.GetFriendlyString(), q.Content, q.Answer};
        InputAreas = new Area[] 
        {
            new(Area.X + Area.Width / 4, Area.Y + Area.Height / 4 + 1, 40, 1),
            new(Area.X + Area.Width / 4, Area.Y + Area.Height / 4 + 3, 40, 1),
            new(Area.X + Area.Width / 4, Area.Y + Area.Height / 4 + 5, 40, 20),
            new(Area.X + Area.Width / 4, Area.Y + Area.Height / 4 + 25, 40, 1),
        };
    }

    protected override bool CheckInput()
    {
        //Enum.TryParse 坑货函数，接受所有的数字 QAQ
        if(string.IsNullOrEmpty(Inputs[0]) ||
            string.IsNullOrEmpty(Inputs[1]) ||
            string.IsNullOrEmpty(Inputs[2]) ||
            string.IsNullOrEmpty(Inputs[3]) ||
            !int.TryParse(Inputs[0]?.Trim(), out int number) ||
            !QuestionTypeExtensions.TryParseStrict(Inputs[1]?.Trim()!, out QuestionType type))
            return false;                  
        else
        {
            Question.Number =  number;
            Question.Type = type;
            Question.Content = Inputs[2];
            Question.Answer = Inputs[3];
            return true;
        }
    }
}
