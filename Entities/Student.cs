using System.Text;
using MHS.QuizSystem;

public class Student
{
    public int Id {get; set;}
    public string Name {get; set;}

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string ToReportString()
    {
        StringBuilder builder = new();
        builder.Append($"学号：{Id}\n")
            .Append($"姓名：{Name}\n");
        return builder.ToString();
    }
}
