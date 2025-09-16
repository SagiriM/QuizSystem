using System.Text;
using MHS.QuizSystem;

public class Area
{
    public int X {get; set;}
    public int Y {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}

    public Area()
    {
        X = 0;
        Y = 0;
        Width = Console.WindowWidth;
        Height = Console.WindowHeight;
    }
    public Area(int x, int y, int w, int h)
    {
        X = x;
        Y = y;
        Width = w;
        Height = h;
    }
    public static Area Empty => new Area(0, 0, 0, 0);
}

public class Window
{
    public string Title {get; set;}
    public Area TitleArea {get; set;}
    public Area Area {get; set;}
    protected bool running = true;

    public Window() : this(Area.Empty, "", Area.Empty) {}
    public Window(Area area, string title, Area titleArea)
    {
        Area = area;
        Title = title;
        TitleArea = titleArea;
    }

    public void Run()
    {
        while(running)
        {
            Render();
            HandleInput();
        }
    }

    protected virtual void Render() 
    {
        Console.Clear();
        RenderText(Title, TitleArea);
    }
    protected virtual void HandleInput() {}

    protected int GetStringWidth(string s)
    {
        int length = 0;
        foreach(char c in s)
            length += GetCharWidth(c);
        return length;
    }
    protected int GetCharWidth(char c)
    {
        if (char.IsControl(c)) return 0;
        if (c >= '\u1100' && c <= '\uFFEF' || c >= '\uFF01' && c <= '\uFF5E')
            return 2;
        return 1;
    }

    protected virtual void RenderText(string text, Area area)
    {
        string[] lines = text.Split('\n');
        int row = 0;
        for(int i = 0; i < lines.Length; i++)
        {
            row += RenderLine(lines[i], new(area.X, area.Y + i + row, area.Width, area.Height));
        }
    }

    private int RenderLine(string line, Area area)
    {
        int width = 0;
        int start = 0;
        int row = 0;
        for(int i = 0; i < line.Length; i++)
        {
            int cWidth = GetCharWidth(line[i]);
            if(width +  cWidth <= area.Width) width += cWidth;
            else
            {
                Console.SetCursorPosition(area.X, area.Y + row);
                Console.Write(line.Substring(start, i - start));
                start = i;
                width = cWidth;
                row++;
            } 
        }
        Console.SetCursorPosition(area.X, area.Y + row);
        Console.Write(line.Substring(start, line.Length - start));

        return row;
    }
    /*
    protected virtual void ClearArea(Area area)
    {
        for(int y = area.Y; y < area.Y + area.Height - 1; y++)
        {
            Console.SetCursorPosition(area.X, y);
            Console.Write(new string(' ', area.Width));
        }
    }
    protected virtual void RenderBorder(Area area)
    {
        Console.SetCursorPosition(area.X, area.Y);
        Console.Write(new string('*', area.Width));

        Console.SetCursorPosition(area.X, area.Y + area.Height);
        Console.Write(new string('*', area.Width));

        for(int y = area.Y + 1; y < area.Height; y++)
        {
            Console.SetCursorPosition(area.X, y);
            Console.Write('*');
            Console.SetCursorPosition(area.X + area.Width, y);
            Console.Write('*');
        }
    }
    */
}
