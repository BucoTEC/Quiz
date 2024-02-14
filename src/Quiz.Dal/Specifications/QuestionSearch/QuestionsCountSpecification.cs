using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch
{
    public class QuestionsCountSpecification : BaseSpecification<QuestionEntity>
    {
        public QuestionsCountSpecification(QuestionSearchParams questionSearchParams)
        : base(x => string.IsNullOrEmpty(questionSearchParams.Search) || x.QuestionText.ToLower().Contains(questionSearchParams.Search))
        {
        }
    }
}
