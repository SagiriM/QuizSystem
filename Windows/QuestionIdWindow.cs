using System.Text;
using MHS.QuizSystem;

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

