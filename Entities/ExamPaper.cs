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
        builder.Append($"学生姓名：{StudentId}\n")
            .Append($"学生编号：{StudentName}\n")
            .Append($"答题时间：{Date}\n")
            .Append(new string('=', 20));
        foreach(var (q, a) in Questions.Zip(Answers))
        {
            builder.Append($"\n")
                .Append($"编号：{q.Number}\n")
                .Append($"类型：{q.Type.GetFriendlyString()}\n")
                .Append($"题目：{q.Content}\n")
                .Append($"答案：\n{q.Answer}\n")
                .Append($"学生答案：\n{a}\n")
                .Append(new string('-', 20));
        }
        return builder.ToString();
    }
}
