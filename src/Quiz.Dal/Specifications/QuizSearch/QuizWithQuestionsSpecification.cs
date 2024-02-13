using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuizSearch
{
    public class QuizWithQuestionsSpecification : BaseSpecification<QuizEntity>
    {
        public QuizWithQuestionsSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.Questions);
        }
    }
}
