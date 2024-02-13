using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications;
using Quiz.Dal.Specifications.QuestionSearch;
using Quiz.Dal.Specifications.QuizSearch;

namespace Quiz.Bll.Services.QuizService
{
    public class QuizService(IUnitOfWork unitOfWork) : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto)
        {
            var spec = new QuizWithQuestionsSpecification(createQuizDto.Name);

            var existingQuiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec);
            if (existingQuiz != null)
            {
                throw new Exception("Quiz with this name already exists");
            }

            if (createQuizDto.Questions != null && createQuizDto.Questions.GroupBy(item => item.QuestionText).Any(group => group.Count() > 1))
                throw new Exception("Duplicate QuestionText found in the list.");


            var questionTexts = createQuizDto.Questions?.Select(q => q.QuestionText).ToList() ?? [];
            var questionTextSpec = new QuestionsSearchSpecification(questionTexts);

            var existingQuestionByName = await _unitOfWork.QuestionRepository.ListAsync(questionTextSpec);

            if (existingQuestionByName.Any()) throw new Exception("Question already exits reuse the question by forwarding its id in the create quiz request");

            var newQuiz = BuildQuizEntity(createQuizDto);

            var questionIdSpec = new QuestionsSearchSpecification(createQuizDto.QuestionsIds?.Distinct().ToList() ?? []);
            var existingQuestionById = await _unitOfWork.QuestionRepository.ListAsync(questionIdSpec);

            newQuiz.Questions = [.. newQuiz.Questions, .. existingQuestionById];

            _unitOfWork.QuizRepository.Add(newQuiz);
            await _unitOfWork.CompleteAsync();

            return BuildQuizResponse(newQuiz);

        }

        public async Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions)
        {
            QuizEntity quiz;

            if (includeQuestions)
            {
                var spec = new QuizWithQuestionsSpecification(id);
                quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec) ?? throw new Exception("No quiz with this id");
            }
            else
            {
                quiz = await _unitOfWork.QuizRepository.GetByIdAsync(id) ?? throw new Exception("No quiz with this id");
            }

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


        public async Task<QuizResponseDto> UpdateQuiz(Guid id, UpdateQuizDto updateQuizDto)
        {
            var quizSpec = new QuizWithQuestionsSpecification(id);
            var quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(quizSpec) ?? throw new Exception("No quiz with this id");

            var questionSpec = new QuestionsSearchSpecification(updateQuizDto.QuestionsIds?.Distinct().ToList() ?? []);
            var questions = await _unitOfWork.QuestionRepository.ListAsync(questionSpec);

            quiz.Name = updateQuizDto.Name;
            quiz.Questions = [.. questions];

            _unitOfWork.QuizRepository.Update(quiz);
            await _unitOfWork.CompleteAsync();

            return BuildQuizResponse(quiz);
        }

        private static QuizEntity BuildQuizEntity(CreateQuizDto createQuizDto)
        {
            var questions = createQuizDto.Questions?.Select(q => new QuestionEntity { QuestionText = q.QuestionText, QuestionAnswer = q.QuestionAnswer });
            var quiz = new QuizEntity
            {
                Name = createQuizDto.Name,
                Questions = questions?.ToList() ?? []
            };

            return quiz;
        }

        private static QuizResponseDto BuildQuizResponse(QuizEntity quizEntity)
        {
            return new QuizResponseDto(quizEntity.Id, quizEntity.Name, quizEntity.Questions);
        }

        private static QuizSearchParams BuildQuizSearchParams(SearchQuizzesQuery searchQuizzesQuery)
        {

            return new QuizSearchParams
            {
                PageIndex = searchQuizzesQuery.PageIndex,
                PageSize = searchQuizzesQuery.PageSize,
                Sort = searchQuizzesQuery.Sort,
                Search = searchQuizzesQuery.Search,
                IncludeQuestions = searchQuizzesQuery.IncludeQuestions
            };
        }
    }
}

//TODO add needed busses logic comments
