using MHS.QuizSystem;

/*
ConfirmationWindow w = new();
w.Run();
Console.WriteLine(w.Selection);
*/

/*
StudentInfoWindow w = new();
w.Run();
Console.WriteLine(w.Inputs[0]);
Console.WriteLine(w.Inputs[1]);
*/

/*
QuestionInfoWindow w = new(true);
w.Run();
Console.WriteLine(w.Inputs[0]);
Console.WriteLine(w.Inputs[1]);
Console.WriteLine(w.Inputs[2]);
Console.WriteLine(w.Inputs[3]);
*/

Area Area = new(0, 0, Console.WindowWidth, Console.WindowHeight);
Area[] aas = new Area[] {
    new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
};
Area[] ias = new Area[] {
    new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
        new(Area.X + Area.Width / 2 + 1, Area.Y + 1, (Area.Width - 3) / 2, Area.Height - 2),
};
IQuestionRepository qr = new FileQuestionRepository();
IExamPaperRepository pr = new FileExamPaperRepository();
IExamPaperService ps = new ExamPaperService(qr);
ExamPaper paper = ps.GeneratePaper(1001, "MHS", DateTime.Now, 3, 3, 1);
/*
string[] qs = paper.Questions.Select(q => q.ToReportString()).ToArray();

QuizWindow w = new(qs, aas, ias);
w.Run();
foreach(string a in w.Inputs)
    Console.WriteLine(a);
*/

string[] answers = new string[] {"11","22","33","44","55","66","77"};
string report = ps.GenerateReport(paper, answers);

ReportWindow w = new(report);
w.Run();








/*
IQuestionRepository qr = new FileQuestionRepository();
IExamPaperRepository pr = new FileExamPaperRepository();
IExamPaperService ps = new ExamPaperService(qr);

MainWindow mainWindow = new (qr, pr, ps);
mainWindow.Run();

Console.Clear();
Console.CursorVisible = true;
Console.WriteLine();
*/
Console.WriteLine("Hello, World!");
