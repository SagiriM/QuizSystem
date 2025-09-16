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
