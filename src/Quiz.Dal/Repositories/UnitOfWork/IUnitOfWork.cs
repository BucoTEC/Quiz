using Quiz.Dal.Repositories.QuizRepo;
using Quiz.Dal.Repositories.QuestionRepo;

namespace Quiz.Dal.Repositories.Uow;

/// <summary>
/// Represents a unit of work interface for managing repositories.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for managing quizzes.
    /// </summary>
    IQuizRepository QuizRepository { get; }

    /// <summary>
    /// Gets the repository for managing questions.
    /// </summary>
    IQuestionRepository QuestionRepository { get; }

    /// <summary>
    /// Completes the unit of work asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CompleteAsync();

    /// <summary>
    /// Disposes the resources associated with the unit of work.
    /// </summary>
    void Dispose();
}
