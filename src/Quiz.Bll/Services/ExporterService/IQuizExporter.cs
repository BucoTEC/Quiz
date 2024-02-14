using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Bll.Services.ExporterService
{
    public interface IQuizExporter
    {
        byte[] ExportQuiz(QuizEntity quiz);
    }
}
