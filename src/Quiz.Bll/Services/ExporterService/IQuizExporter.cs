using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;

namespace Quiz.Bll.Services.ExporterService
{
    public interface IQuizExporter
    {
        ExportQuizData ExportQuiz(QuizEntity quiz);
    }
}
