using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<QuizEntity> Quizzes { get; set; }

        public DbSet<QuestionEntity> Questions { get; set; }

    }
}
