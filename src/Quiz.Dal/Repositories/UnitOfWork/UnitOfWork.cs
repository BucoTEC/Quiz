using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Data;
using Quiz.Dal.Repositories.QuestionRepo;
using Quiz.Dal.Repositories.QuizRepo;

namespace Quiz.Dal.Repositories.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IQuizRepository QuizRepository { get; private set; }

        public IQuestionRepository QuestionRepository { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            QuizRepository = new QuizRepository(context);
            QuestionRepository = new QuestionRepository(context);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
