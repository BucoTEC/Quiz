using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch
{
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
                    case "oldest": // TODO change to enum
                        AddOrderBy(p => p.CreatedAt);
                        break;
                    case "newest":
                        AddOrderByDescending(p => p.CreatedAt);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public QuizzesSearchSpecification(List<Guid> ids) : base(o => ids.Contains(o.Id))
        {
        }
    }
}
