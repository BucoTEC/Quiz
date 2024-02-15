using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch;

/// <summary>
/// Represents a specification for counting questions based on search parameters.
/// </summary>
public class QuestionsCountSpecification : BaseSpecification<QuestionEntity>
{
    public QuestionsCountSpecification(QuestionSearchParams questionSearchParams)
    : base(x => string.IsNullOrEmpty(questionSearchParams.Search) || x.QuestionText.ToLower().Contains(questionSearchParams.Search))
    {
    }
}
