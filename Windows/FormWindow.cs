using System.Text;
using MHS.QuizSystem;


public class FormWindow : Window
{
    public string[] Options {get; set;}
    public Area[] OptionAreas {get; set;}
    public string[] Inputs {get; set;}
    public Area[] InputAreas {get; set;}
    public int Selection {get; protected set;}

    public FormWindow() : this(Area.Empty, "", Area.Empty, new string[] {""}, new Area[] {Area.Empty}, new string[] {""}, new Area[] {Area.Empty}) {}
    public FormWindow(Area area, string title, Area titleArea, string[] options, Area[] optionAreas, string[] inputs, Area[] inputAreas) : base(area, title, titleArea) 
    {
        Area = area;
        Title = title;
        TitleArea = titleArea;
        Options = options;
        OptionAreas = optionAreas;
        Inputs = inputs;
        InputAreas = inputAreas;
        Selection = 0;
    }

    protected override void Render()
    {
        base.Render();
        RenderOptions();
        RenderInputs();
    }

    protected virtual void RenderOptions() => Render(Options, OptionAreas);
    protected virtual void RenderInputs() => Render(Inputs, InputAreas);
    private void Render(string[] contents, Area[] areas)
    {
        for(int i = 0; i < contents.Length; i++)
            if(i == Selection)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                RenderText(contents[i], areas[i]);
                Console.ResetColor();
            }
            else RenderText(contents[i], areas[i]);
    }

    protected override void HandleInput()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch(key.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.LeftArrow:
                if(Selection > 0) Selection--;
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.RightArrow:
                if(Selection < Options.Length - 1) Selection++;
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
    }

    protected virtual void HandleShiftEnter() => Inputs[Selection] += '\n';
    protected virtual void HandleEnter()
    {
        if(CheckInput()) running = false;
        else 
        {
            ConfirmationWindow w = new("输入格式有误");
            w.Run();
        }
    }
    protected virtual void HandleBackspace()
    {
        if(Inputs[Selection].Length > 0) 
            Inputs[Selection] = Inputs[Selection].Remove(Inputs[Selection].Length - 1, 1);
    }
    protected virtual void HandleDefault(ConsoleKeyInfo key)
    {
        if(!char.IsControl(key.KeyChar)) Inputs[Selection] += key.KeyChar;
    }
    protected virtual bool CheckInput() => true;
}

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



public class ConfirmationWindow : FormWindow
{
    public ConfirmationWindow(string title)
    { 
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Title = title;
        int titleWidth = GetStringWidth(title);
        TitleArea = new(Area.X + Area.Width / 2 - titleWidth / 2, Area.Y + Area.Height / 4 - 2, titleWidth, 1);
        Options = new string[] {"确认", "取消"};
        OptionAreas = new Area[] {
            new(Area.X + Area.Width / 2 - 5, Area.Y + Area.Height / 2 + 2, 4, 1),
            new(Area.X + Area.Width / 2 + 1, Area.Y + Area.Height / 2 + 2, 4, 1),
        };
    }
}

public class QuestionIdWindow : FormWindow
{
    public Guid? Id {get; set;}
    public QuestionIdWindow()
    {
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Options = new string[] {"编  号："};
        OptionAreas = new Area[] 
        {
            new(Area.X + Area.Width / 2 - 8, Area.Y + Area.Height / 2, 8, 1),
        };
        Inputs = new string[] {""};
        InputAreas = new Area[] 
        {
            new(Area.X + Area.Width / 2, Area.Y + Area.Height / 2, 20, 1),
        };
        Id = null;
    }

    protected override bool CheckInput()
    {
        if(!Guid.TryParse(Inputs[0]?.Trim(), out Guid id))
            return false;                  
        else 
        {
            Id = id;
            return true;
        }
    }
}

public class ReportWindow : FormWindow
{
    public ReportWindow(string report)
    {
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Title = "成绩单";
        TitleArea = new(Area.X + Area.Width / 2 - 3, Area.Y + 1, 6, 1);
        Options = new string[] {report};
        OptionAreas = new Area[]
        {
            new(Area.X + Area.Width / 5, Area.Y + 2, (Area.Width - 2) / 5 * 4, Area.Height - 2)
        };
    }
}
public class StudentInfoWindow : FormWindow
{
    public Student? Student {get; set;}
    public StudentInfoWindow()
    {
        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Options = new string[] {"答题人：", "学  号："};
        OptionAreas = new Area[] {
            new(Area.X + Area.Width / 2 - 8, Area.Y + Area.Height / 2 + 1, 8, 1),
            new(Area.X + Area.Width / 2 - 8, Area.Y + Area.Height / 2 + 3, 8, 1),
        };
        Inputs = new string[] {"", ""};
        InputAreas = new Area[] {
            new(Area.X + Area.Width / 2, Area.Y + Area.Height / 2 + 1, 10, 1),
            new(Area.X + Area.Width / 2, Area.Y + Area.Height / 2 + 3, 10, 1),
        };
        Student = null;
    }

    protected override bool CheckInput()
    {
        if(string.IsNullOrEmpty(Inputs[0]) || 
            string.IsNullOrEmpty(Inputs[1]) ||
            !int.TryParse(Inputs[1], out int id))
            return false; 
        else 
        {
            Student = new(id, Inputs[0]);
            return true;
        }
    }
}

