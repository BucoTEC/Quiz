using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Data;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.GenericRepo;

namespace Quiz.Dal.Repositories.QuizRepo
{
    public class QuizRepository(AppDbContext context) : GenericRepository<QuizEntity>(context), IQuizRepository
    {
    }
}
