using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch
{
    public class SearchQuizzesSpecification : BaseSpecification<QuizEntity>
    {
        public SearchQuizzesSpecification(QuizSearchParams quizSearchParams)
        : base(x => string.IsNullOrEmpty(quizSearchParams.Search) || x.Name.ToLower().Contains(quizSearchParams.Search))
        {
            ApplyPaging(quizSearchParams.PageSize * (quizSearchParams.PageIndex - 1), quizSearchParams.PageSize);

            if (!string.IsNullOrEmpty(quizSearchParams.Sort))
            {
                switch (quizSearchParams.Sort)
                {
                    case "oldest":
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
    }
}
