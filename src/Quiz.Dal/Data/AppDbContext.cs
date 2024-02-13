using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.Dal.Entities;

namespace Quiz.Dal.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<QuizEntity> Quizzes { get; set; }

        public DbSet<QuestionEntity> Questions { get; set; }

    }
}
