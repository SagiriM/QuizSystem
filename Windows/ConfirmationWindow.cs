using System.Text;
using MHS.QuizSystem;

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
