using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Bll.Exceptions;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
using Quiz.Bll.Services.ExporterService;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications;
using Quiz.Dal.Specifications.QuestionSearch;
using Quiz.Dal.Specifications.QuizSearch;

namespace Quiz.Bll.Services.QuizService
{
    public class QuizService(IUnitOfWork unitOfWork, QuizExporterManager quizExporter) : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly QuizExporterManager _quizExporter = quizExporter;

        /// <inheritdoc/>
        public async Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions)
        {
            var spec = new QuizWithQuestionsSpecification(id, includeQuestions);
            var quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec) ?? throw new NotFoundException($"No quiz found with id:{id}");

            return BuildQuizResponse(quiz);
        }

        public async Task<Pagination<QuizResponseDto>> SearchQuizzes(SearchQuizzesQuery searchQuizzesQuery)
        {
            var quizSearchParams = BuildQuizSearchParams(searchQuizzesQuery);
            var countSpec = new QuizzesCountSpecification(quizSearchParams);
            var spec = new QuizzesSearchSpecification(quizSearchParams);


            var totalCountOfQuizzes = await _unitOfWork.QuizRepository.CountAsync(countSpec);
            var quizzes = await _unitOfWork.QuizRepository.ListAsync(spec);
            var data = quizzes.Select(quiz => BuildQuizResponse(quiz));

            return new Pagination<QuizResponseDto>(quizSearchParams.PageIndex, quizSearchParams.PageSize, totalCountOfQuizzes, data);
        }

        public async Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto)
        {
            // check if there is already a quiz with the same name
            var spec = new QuizWithQuestionsSpecification(createQuizDto.Name);
            var existingQuiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec);
            if (existingQuiz != null) throw new BadRequestException($"Quiz with this name already exists, quiz id: {existingQuiz.Id}");


            // filter list of questions for duplicates
            if (createQuizDto.Questions != null && createQuizDto.Questions.GroupBy(item => item.QuestionText).Any(group => group.Count() > 1))
                throw new BadRequestException("Duplicate QuestionTexts found in the list of forwarded questions.");


            // check that questions being created with quiz do not already exists
            var questionTexts = createQuizDto.Questions?.Select(q => q.QuestionText).ToList() ?? [];
            var questionTextSpec = new QuestionsSearchSpecification(questionTexts);
            var existingQuestionByName = await _unitOfWork.QuestionRepository.ListAsync(questionTextSpec);
            if (existingQuestionByName.Any()) throw new BadRequestException("Question already exits reuse the question by forwarding its id in the create quiz request");


            // reuse existing questions by searching with forwarder questions ids
            var questionIdsSpec = new QuestionsSearchSpecification(createQuizDto.QuestionsIds?.Distinct().ToList() ?? []);
            var existingQuestionsById = await _unitOfWork.QuestionRepository.ListAsync(questionIdsSpec);


            // create and save new quiz
            var newQuiz = BuildQuizEntity(createQuizDto);
            newQuiz.Questions = [.. newQuiz.Questions, .. existingQuestionsById];
            _unitOfWork.QuizRepository.Add(newQuiz);
            await _unitOfWork.CompleteAsync();

            return BuildQuizResponse(newQuiz);

        }

        public async Task<QuizResponseDto> UpdateQuiz(Guid id, UpdateQuizDto updateQuizDto)
        {
            // check if quiz with this id exists
            var quizSpec = new QuizWithQuestionsSpecification(id);
            var quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(quizSpec) ?? throw new NotFoundException($"No quiz found with id:{id}");


            // check if there is already a quiz with the same name
            var spec = new QuizWithQuestionsSpecification(updateQuizDto.Name);
            var existingQuiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec);
            if (existingQuiz != null) throw new BadRequestException($"Quiz with this name already exists, quiz id: {existingQuiz.Id}");


            // get questions of quiz that is being update
            var questionSpec = new QuestionsSearchSpecification(updateQuizDto.QuestionsIds?.Distinct().ToList() ?? []);
            var questions = await _unitOfWork.QuestionRepository.ListAsync(questionSpec);


            // update quiz and save changes
            quiz.Name = updateQuizDto.Name;
            quiz.Questions = [.. questions];
            _unitOfWork.QuizRepository.Update(quiz);
            await _unitOfWork.CompleteAsync();

            return BuildQuizResponse(quiz);
        }

        public async Task DeleteQuiz(Guid id)
        {
            var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(id) ?? throw new NotFoundException($"No quiz found with id:{id}");

            _unitOfWork.QuizRepository.Delete(quiz);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<ExportQuizResponseDto> ExportQuiz(string exporter, Guid id)
        {
            var quizSpec = new QuizWithQuestionsSpecification(id);
            var quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(quizSpec) ?? throw new NotFoundException($"No quiz found with id:{id}");
            var csvContent = _quizExporter.ExportQuizAsync(exporter, quiz); // Use injected QuizExporter service

            var data = new MemoryStream(csvContent.Data);
            return new ExportQuizResponseDto { QuizName = quiz.Name, QuizData = data, DataType = csvContent.DataType, ResponseFormat = csvContent.ResponseFormat };
        }


        public string[] GetAvailableExporters()
        {
            // var lazyExporters = 
            return _quizExporter.GetAvailableExporters();

            // Get exporter names from Lazy<T> instances
            // return lazyExporters.Select(exporter => exporter.GetType().Name).ToArray();

        }

        private static QuizEntity BuildQuizEntity(CreateQuizDto createQuizDto)
        {
            var questions = createQuizDto.Questions?.Select(q => new QuestionEntity { QuestionText = q.QuestionText, QuestionAnswer = q.QuestionAnswer });
            return new QuizEntity
            {
                Name = createQuizDto.Name,
                Questions = questions?.ToList() ?? []
            };

        }

        private static QuizResponseDto BuildQuizResponse(QuizEntity quizEntity)
        {
            return new QuizResponseDto(quizEntity.Id, quizEntity.Name, quizEntity.CreatedAt, quizEntity.UpdatedAt, quizEntity.Questions);
        }

        private static QuizSearchParams BuildQuizSearchParams(SearchQuizzesQuery searchQuizzesQuery)
        {
            return new QuizSearchParams
            {
                PageIndex = searchQuizzesQuery.PageIndex,
                PageSize = searchQuizzesQuery.PageSize,
                Sort = searchQuizzesQuery.Sort,
                Search = searchQuizzesQuery.SearchByQuizName,
                IncludeQuestions = searchQuizzesQuery.IncludeQuestions
            };
        }
    }
}
