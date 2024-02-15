using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.GenericRepo;

namespace Quiz.Dal.Repositories.QuizRepo;

/// <summary>
/// Represents a repository for managing questions.
/// </summary>
public interface IQuizRepository : IGenericRepository<QuizEntity>
{

}
