using Quiz.Dal.Data;
using Quiz.Dal.Repositories.QuizRepo;
using Quiz.Dal.Repositories.QuestionRepo;

namespace Quiz.Dal.Repositories.Uow;

/// <inheritdoc/>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    /// <inheritdoc/>
    public IQuizRepository QuizRepository { get; private set; }

    /// <inheritdoc/>
    public IQuestionRepository QuestionRepository { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        QuizRepository = new QuizRepository(context);
        QuestionRepository = new QuestionRepository(context);

    }

    /// <inheritdoc/>
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _context.Dispose();
    }
}
