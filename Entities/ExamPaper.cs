using System.Text;

namespace MHS.QuizSystem;

public class ExamPaper
{
    public Guid Id {get; init;}
    public int StudentId {get; set;}
    public string StudentName {get; set;}
    public DateTime Date {get; set;}
    public IEnumerable<Question> Questions {get; set;}
    public IEnumerable<string> Answers{get; set;}

    public ExamPaper(Guid id, int sId, string sName, DateTime date, IEnumerable<Question> questions, IEnumerable<string> answers)
    {
        ArgumentNullException.ThrowIfNull(questions);
        ArgumentNullException.ThrowIfNull(answers);
        if(questions.Count() != answers.Count()) throw new ArgumentException($"题目数量({questions.Count()})与答案数量({answers.Count()})不匹配", nameof(answers));

        Id = id;
        StudentId = sId;
        StudentName = sName;
        Date = date;
        Questions = questions;
        Answers = answers;
    }
    public ExamPaper(int sId, string sName, DateTime date, IEnumerable<Question> questions, IEnumerable<string> answers) 
        : this(Guid.NewGuid(), sId, sName, date, questions, answers) {}
    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append($"学号：{StudentId}\n")
            .Append($"姓名：{StudentName}\n")
            .Append($"时间：{Date}\n")
            .Append(new string('=', 20)).Append("\n");
        foreach(var (q, a) in Questions.Zip(Answers))
        {
            builder.Append($"{q.ToReportString()}")
                .Append($"学生答案：{a}\n")
                .Append(new string('-', 20)).Append("\n");
        }
        builder.Remove(builder.Length - 21, 21);
        return builder.ToString();
    }
}
