using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Repositories.QuestionRepo;
using Quiz.Dal.Repositories.QuizRepo;

namespace Quiz.Dal.Repositories.Uow
{
    public interface IUnitOfWork
    {
        IQuizRepository QuizRepository { get; }

        IQuestionRepository QuestionRepository { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
