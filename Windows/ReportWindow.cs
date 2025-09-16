using System.Text;
using MHS.QuizSystem;

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
