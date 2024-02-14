using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch
{
    public class QuestionWithQuizzesSpecification : BaseSpecification<QuestionEntity>
    {
        public QuestionWithQuizzesSpecification(Guid id) : base(q => q.Id == id)
        {
            AddInclude(q => q.Quizzes);
        }
    }
}
