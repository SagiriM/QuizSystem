using System.Text;
using MHS.QuizSystem;

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

