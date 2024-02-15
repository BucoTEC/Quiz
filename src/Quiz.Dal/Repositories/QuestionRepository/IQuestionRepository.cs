using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.GenericRepo;

namespace Quiz.Dal.Repositories.QuestionRepo;

/// <summary>
/// Represents a repository for managing questions.
/// </summary>
public interface IQuestionRepository : IGenericRepository<QuestionEntity> { }
