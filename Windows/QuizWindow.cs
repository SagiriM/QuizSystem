using MHS.QuizSystem;

public class QuizWindow : Window
{
    public string[] Questions {get; set;}
    public Area QuestionArea {get; set;}
    public string[] Answers {get; set;}
    public Area AnswerArea {get; set;}
    public int Selection {get; private set;}
    public QuizWindow(string[] questions)
    {
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Questions = questions;
        QuestionArea = new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2);
        Answers = new string[] {"", "", "", "", "", "", ""};
        AnswerArea =  new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2);
        Selection = 0;
    }
    protected override void Render()
    {
        base.Render();
        RenderText(Questions[Selection], QuestionArea);
        RenderText(Answers[Selection], AnswerArea);
    }

    protected override void HandleInput()
    {
        Console.CursorVisible = true;
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch(key.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.LeftArrow:
                if(Selection > 0) Selection--;
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.RightArrow:
                if(Selection < Questions.Length - 1) Selection++;
                break;
            case ConsoleKey.Enter:
                if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    HandleShiftEnter();
                else HandleEnter();
                break;
            case ConsoleKey.Backspace:
                HandleBackspace();
                break;
            case ConsoleKey.Escape:
                running = false;
                break;
            default:
                HandleDefault(key);
                break;
        }
        Console.CursorVisible = false;
    }

    private void HandleShiftEnter() => Answers[Selection] += '\n';
    private void HandleEnter() => running = false;
    private void HandleBackspace()
    {
        if(Answers[Selection].Length > 0) 
            Answers[Selection] = Answers[Selection].Remove(Answers[Selection].Length - 1, 1);
    }
    private void HandleDefault(ConsoleKeyInfo key)
    {
        if(!char.IsControl(key.KeyChar)) Answers[Selection] += key.KeyChar;
    }
}
