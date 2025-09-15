using MHS.QuizSystem;


IQuestionRepository qr = new FileQuestionRepository();
IExamPaperRepository pr = new FileExamPaperRepository();
IExamPaperService ps = new ExamPaperService(qr);

Console.CursorVisible = false;

MainWindow w = new (qr, pr, ps);
w.Run();

Console.CursorVisible = true;
Console.WriteLine("Hello, World!");

