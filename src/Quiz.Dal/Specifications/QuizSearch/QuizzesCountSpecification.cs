using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch
{
    public class QuizzesCountSpecification : BaseSpecification<QuizEntity>
    {
        public QuizzesCountSpecification(QuizSearchParams quizSearchParams)
        : base(x => string.IsNullOrEmpty(quizSearchParams.Search) || x.Name.ToLower().Contains(quizSearchParams.Search))
        {
        }
    }

}
