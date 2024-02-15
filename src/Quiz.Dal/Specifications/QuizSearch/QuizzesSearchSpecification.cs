using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch;

/// <summary>
/// Represents a specification for searching quizzes based on search parameters.
/// </summary>

public class QuizzesSearchSpecification : BaseSpecification<QuizEntity>
{
    public QuizzesSearchSpecification(QuizSearchParams quizSearchParams)
    : base(x => string.IsNullOrEmpty(quizSearchParams.Search) || x.Name.ToLower().Contains(quizSearchParams.Search))
    {
        ApplyPaging(quizSearchParams.PageSize * (quizSearchParams.PageIndex - 1), quizSearchParams.PageSize);

        if (quizSearchParams.IncludeQuestions is true) AddInclude(o => o.Questions);

        if (!string.IsNullOrEmpty(quizSearchParams.Sort))
        {
            switch (quizSearchParams.Sort)
            {
                case "oldFirst":
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case "newFirst":
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    AddOrderByDescending(n => n.CreatedAt);
                    break;
            }
        }
    }

    public QuizzesSearchSpecification(List<Guid> ids) : base(o => ids.Contains(o.Id))
    {
    }
}
