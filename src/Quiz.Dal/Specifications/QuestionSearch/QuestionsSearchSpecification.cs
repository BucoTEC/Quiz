using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Specifications.QuestionSearch
{
    public class QuestionsSearchSpecification : BaseSpecification<QuestionEntity>
    {
        public QuestionsSearchSpecification(List<Guid> ids) : base(o => ids.Contains(o.Id))
        {
        }

        public QuestionsSearchSpecification(List<string> questionTextNames)
        : base(q => questionTextNames.Any(name => q.QuestionText.ToLower() == name.ToLower()))
        {
        }
    }
}
