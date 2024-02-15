using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch;

/// <summary>
/// Represents a specification for retrieving a question with its associated quizzes.
/// </summary>
public class QuestionWithQuizzesSpecification : BaseSpecification<QuestionEntity>
{
    public QuestionWithQuizzesSpecification(Guid id, bool includeQuizzes) : base(q => q.Id == id)
    {
        if (includeQuizzes) AddInclude(q => q.Quizzes);
    }

    public QuestionWithQuizzesSpecification(string questionText) : base(q => q.QuestionText == questionText)
    {
    }
}
