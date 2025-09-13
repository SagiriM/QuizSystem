namespace MHS.QuizSystem;

public interface IQuestionRepository
{
    public void Add(Question q);
    public void Delete(Guid id);
    public void Update(Question q);
    public IEnumerable<Question> GetAll();
    public Question GetById(Guid id);
    public IEnumerable<Question> GetRandomly(QuestionType type, int count);
    public void Save();
}
