
using MHS.QuizSystem;

public class MainWindow : FormWindow
{
    private readonly IQuestionRepository qr;
    private readonly IExamPaperRepository pr;
    private readonly IExamPaperService ps;

    public MainWindow(IQuestionRepository qr, IExamPaperRepository pr, IExamPaperService ps)
    {
        this.qr = qr;
        this.pr = pr;
        this.ps = ps;    

        Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
        Options = new string[] {"开始练习", "添加题目", "删除题目", "修改题目", "搜索题目", "退    出"};
        OptionAreas = new Area[] {
            new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 1, 8, 1),
                new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 3, 8, 1),
                new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 5, 8, 1),
                new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 7, 8, 1),
                new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 9, 8, 1),
                new(Area.X + Area.Width / 2 - 4, Area.Y + Area.Height / 4 + 11, 8, 1),
        };
    } 

    protected override void HandleEnter()
    {
        switch(Selection)
        {
            case 0:
                Start();
                break;
            case 1:
                Add();
                break;
            case 2:
                Delete();
                break;
            case 3:
                Update();
                break;
            case 4:
                Search();
                break;
            case 5:
                running = false;
                return;
        } 
    }

    private void Start()
    {
        StudentInfoWindow w = new();        
        w.Run();

        Student? student = w.Student;
        if(student == null) return;

        ExamPaper paper = ps.GeneratePaper(student, DateTime.Now, 3, 3, 1);

        QuizWindow qw = new(paper.ToQuizString());
        qw.Run();

        string[] answers = qw.Answers;
        string report = ps.GenerateReport(paper, answers);

        ReportWindow rw = new(report); 
        rw.Run();

        pr.Save(paper);
    }


    private void Add() 
    {
        QuestionWindow w = new();
        w.Run();

        ConfirmationWindow cw = new("是否添加？");
        cw.Run();

        if(cw.Selection == 0) 
        {
            qr.Add(w.Question);
            qr.Save();
        }
    }
    private void Delete() 
    {
        QuestionIdWindow w = new();
        w.Run();

        Guid? id = w.Id;
        if(!id.HasValue) return;

        ConfirmationWindow cw = new("是否删除？");
        cw.Run();

        if(cw.Selection == 0) 
        {
            qr.Delete(id.Value);
            qr.Save();
        }
    }
    private void Update() 
    {
        QuestionIdWindow w = new();
        w.Run();

        Guid? id = w.Id;
        if(!id.HasValue) return;

        Question q;
        try
        {
            q = qr.GetById(id.Value);
        }
        catch(Exception)
        {
            ConfirmationWindow error = new("题目不存在");
            error.Run();
            return;            
        }
        QuestionWindow qw = new(q);
        qw.Run();

        ConfirmationWindow cw = new("是否保存？");
        cw.Run();

        if(cw.Selection == 0) 
        {
            qr.Update(qw.Question);
            qr.Save();
        }
    }
    private void Search() 
    {
        SearchWindow w = new(qr);
        w.Run();
    }
}
