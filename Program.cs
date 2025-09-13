using MHS.QuizSystem;

Question[] qs = {
    new(1000, QuestionType.SingleChoice, "选择题ABCD0", "A"),
    new(1001, QuestionType.SingleChoice, "选择题ABCD1", "B"),
    new(1002, QuestionType.TrueFalse, "判断题TF0", "T"),
    new(1003, QuestionType.TrueFalse, "判断题TF1", "F"),
    new(1004, QuestionType.Programming, "编程题0", "dotnet"),
    new(1005, QuestionType.Programming, "编程题1", "java"),
};
IQuestionRepository qr = new FileQuestionRepository();

/*
// Add
foreach(Question q in qs)
    qr.Add(q);

// Delete
qr.Delete(1);

// Update
Question question = new(2, 1001, QuestionType.SingleChoice, "更新-选择题ABCD1", "B");
qr.Update(question);

// GetAll
IEnumerable<Question> questions = qr.GetAll();
Console.WriteLine(questions.Count());

// GetById
question = qr.GetById(2);
Console.WriteLine($"{question.Id}, {question.Number}");

// GetRandomly
questions = qr.GetRandomly(QuestionType.Programming, 1);
Console.WriteLine(questions.Count());

// Save
qr.Save();

*/

/*
string[] answers = {
   "A",
   "B",
   "T",
   "F",
   "int main() {return 0;}",
   "WriteLine()" 
};
ExamPaper[] papers = {
    new (1, 1001, "莫航松", DateTime.Now, qs, answers),
    new (2, 1002, "莫航松-1", DateTime.Now, qs, answers)
};
Console.WriteLine(papers[0].GetReport());

IExamPaperRepository pr = new FileExamPaperRepository();
pr.Add(papers[0]);
pr.Add(papers[1]);
pr.Save();

*/

Console.WriteLine("Hello, World!");
