using System.Text;
using MHS.QuizSystem;

public class SearchWindow : Window
{
    public string Input {get; set;}
    public Area InputArea {get; set;}
    public IEnumerable<Question>? Results {get; set;}
    public Area ResultArea {get; set;}

    private readonly IQuestionRepository qr;
    public SearchWindow(IQuestionRepository qr)
    {
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Title = "搜索：";
        TitleArea = new(Area.X + 2, Area.Y + 2, 6, 1);
        Input = "";
        InputArea = new(Area.X + 10, TitleArea.Y, Area.Width - 5, 1);
        Results = null;
        ResultArea = new(Area.X + 2, Area.Y + 4, Area.Width - 4, Area.Height - 4);

        this.qr = qr;
    }


    protected override void Render()
    {
        base.Render();
        RenderText(Input, InputArea);
        RenderResults();
    }

    protected override void HandleInput()
    {
        Console.CursorVisible = true;
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch(key.Key)
        {
            case ConsoleKey.Enter:
                HandleEnter();
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
    private void RenderResults()
    {
        if(Results == null)
        {
            RenderText("无结果", ResultArea);
            return;
        }
        StringBuilder builder = new();
        foreach(Question q in Results)
            builder.AppendLine(q.ToSearchString());
        RenderText(builder.ToString(), ResultArea);
    }

    private void HandleEnter() 
    {
        if(CheckInput() == false) 
        {
            Results = null;
            return;
        }
        Results = qr.GetByContent(Input);
    }
    private void HandleBackspace()
    {
        if(Input.Length > 0) Input = Input.Remove(Input.Length - 1, 1);
    }
    private void HandleDefault(ConsoleKeyInfo key)
    {
        if(!char.IsControl(key.KeyChar)) Input += key.KeyChar;
    }

    protected bool CheckInput() => true;
}

