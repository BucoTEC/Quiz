using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch;

/// <summary>
/// Represents a specification for counting quizzes based on search parameters.
/// </summary>
public class QuizzesCountSpecification : BaseSpecification<QuizEntity>
{
    public QuizzesCountSpecification(QuizSearchParams quizSearchParams)
    : base(x => string.IsNullOrEmpty(quizSearchParams.Search) || x.Name.ToLower().Contains(quizSearchParams.Search))
    {
    }
}
