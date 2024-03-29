using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch;

/// <summary>
/// Represents a specification for searching questions based on search parameters.
/// </summary>
public class QuestionsSearchSpecification : BaseSpecification<QuestionEntity>
{
    public QuestionsSearchSpecification(QuestionSearchParams questionSearchParams)
    : base(x => string.IsNullOrEmpty(questionSearchParams.Search) || x.QuestionText.ToLower().Contains(questionSearchParams.Search))
    {
        ApplyPaging(questionSearchParams.PageSize * (questionSearchParams.PageIndex - 1), questionSearchParams.PageSize);

        if (questionSearchParams.IncludeQuizzes is true) AddInclude(o => o.Quizzes);

        if (!string.IsNullOrEmpty(questionSearchParams.Sort))
        {
            switch (questionSearchParams.Sort)
            {
                case "oldFirst":
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case "newFirst":
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
            }
        }
    }

    public QuestionsSearchSpecification(List<Guid> ids) : base(o => ids.Contains(o.Id))
    {
    }

    public QuestionsSearchSpecification(List<string> questionTextNames)
    : base(q => questionTextNames.Any(name => q.QuestionText.ToLower() == name.ToLower()))
    {
    }
}
