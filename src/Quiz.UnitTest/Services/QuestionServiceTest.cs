using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quiz.Bll.Services.QuestionService;
using Quiz.Dal.Entities;
using Quiz.Bll.Dtos;
using Quiz.Bll.Exceptions;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Repositories.GenericRepo;
using Quiz.Dal.Specifications.QuestionSearch;

namespace Quiz.Bll.Tests.Services
{
    public class QuestionServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IQuestionService _questionService;

        public QuestionServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _questionService = new QuestionService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetQuestionById_ValidId_ReturnsQuestionResponseDto()
        {
            // Arrange
            var questionEntity = new QuestionEntity { QuestionText = "Sample Question", QuestionAnswer = "Sample Answer" };
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetEntityWithSpec(It.IsAny<QuestionWithQuizzesSpecification>())).ReturnsAsync(questionEntity);

            // Act
            var result = await _questionService.GetQuestionById(questionEntity.Id, true);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<QuestionResponseDto>(result);
            Assert.Equal(questionEntity.Id, result.Id);
            Assert.Equal("Sample Question", result.QuestionText);
            Assert.Equal("Sample Answer", result.QuestionAnswer);
        }

        [Fact]
        public async Task GetQuestionById_QuestionNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetEntityWithSpec(It.IsAny<QuestionWithQuizzesSpecification>())).ReturnsAsync((QuestionEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _questionService.GetQuestionById(questionId, true));
        }

        [Fact]
        public async Task SearchQuestions_ReturnsPaginationOfQuestionResponseDto()
        {
            // Arrange
            var searchQuery = new SearchQuestionsQuery
            {
                PageIndex = 1,
                PageSize = 10,
                Sort = "newFirst",
                SearchByQuestionText = "example",
                IncludeQuizzes = true
            };

            var mockQuestions = new List<QuestionEntity>
            {
                new() { QuestionText = "Example question 1", Quizzes = new List<QuizEntity>() },
                new() { QuestionText = "Example question 2", Quizzes = new List<QuizEntity>() }
            };

            var totalCount = mockQuestions.Count;

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.CountAsync(It.IsAny<QuestionsCountSpecification>())).ReturnsAsync(totalCount);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.ListAsync(It.IsAny<QuestionsSearchSpecification>())).ReturnsAsync(mockQuestions);

            // Act
            var pagination = await _questionService.SearchQuestions(searchQuery);

            // Assert
            Assert.NotNull(pagination);
            Assert.Equal(searchQuery.PageIndex, pagination.PageIndex);
            Assert.Equal(searchQuery.PageSize, pagination.PageSize);
            Assert.Equal(totalCount, pagination.Count);
            Assert.Equal(mockQuestions.Count, pagination.Data.Count());
        }

        [Fact]
        public async Task CreateQuestion_ThrowsBadRequestException_WhenQuestionWithSameTextExists()
        {
            // Arrange
            var existingQuestion = new QuestionEntity { QuestionText = "Example question" };
            var createQuestionDto = new CreateQuestionDto { QuestionText = "Example question", QuestionAnswer = "Answer" };

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetEntityWithSpec(It.IsAny<QuestionWithQuizzesSpecification>())).ReturnsAsync(existingQuestion);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _questionService.CreateQuestion(createQuestionDto));
        }

        [Fact]
        public async Task CreateQuestion_CreatesAndSavesNewQuestion_WhenQuestionWithSameTextDoesNotExist()
        {
            // Arrange
            var createQuestionDto = new CreateQuestionDto { QuestionText = "New question", QuestionAnswer = "Answer" };

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetEntityWithSpec(It.IsAny<QuestionWithQuizzesSpecification>())).ReturnsAsync((QuestionEntity)null);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.Add(It.IsAny<QuestionEntity>()));
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var createdQuestion = await _questionService.CreateQuestion(createQuestionDto);

            // Assert
            Assert.NotNull(createdQuestion);
            Assert.Equal(createQuestionDto.QuestionText, createdQuestion.QuestionText);
            Assert.Equal(createQuestionDto.QuestionAnswer, createdQuestion.QuestionAnswer);
        }

        [Fact]
        public async Task UpdateQuestion_ThrowsNotFoundException_WhenQuestionWithIdDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid(); // Generate a new Guid for the id
            var updateQuestionDto = new UpdateQuestionDto { QuestionText = "Updated question", QuestionAnswer = "Updated answer" };

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetByIdAsync(id)).ReturnsAsync((QuestionEntity)null);

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _questionService.UpdateQuestion(id, updateQuestionDto));
        }

        [Fact]
        public async Task UpdateQuestion_UpdatesAndSavesQuestion_WhenQuestionWithIdExists()
        {
            // Arrange
            var existingQuestion = new QuestionEntity { QuestionText = "Original question", QuestionAnswer = "Original answer" };
            var updateQuestionDto = new UpdateQuestionDto { QuestionText = "Updated question", QuestionAnswer = "Updated answer" };

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetByIdAsync(existingQuestion.Id)).ReturnsAsync(existingQuestion);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.Update(existingQuestion));
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var updatedQuestion = await _questionService.UpdateQuestion(existingQuestion.Id, updateQuestionDto);

            // Assert
            Assert.NotNull(updatedQuestion);
            Assert.Equal(existingQuestion.Id, updatedQuestion.Id);
            Assert.Equal(updateQuestionDto.QuestionText, updatedQuestion.QuestionText);
            Assert.Equal(updateQuestionDto.QuestionAnswer, updatedQuestion.QuestionAnswer);
        }

        [Fact]
        public async Task DeleteQuestion_ThrowsNotFoundException_WhenQuestionWithIdDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid(); // Generate a new Guid for the id

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetByIdAsync(id)).ReturnsAsync((QuestionEntity)null);

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _questionService.DeleteQuestion(id));
        }

        [Fact]
        public async Task DeleteQuestion_DeletesQuestion_WhenQuestionWithIdExists()
        {
            // Arrange
            var existingQuestion = new QuestionEntity { QuestionText = "Existing question", QuestionAnswer = "Existing answer" };

            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.GetByIdAsync(existingQuestion.Id)).ReturnsAsync(existingQuestion);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.Delete(existingQuestion));
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            await _questionService.DeleteQuestion(existingQuestion.Id);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.QuestionRepository.Delete(existingQuestion), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        // Add more test methods for other scenarios (e.g., NotFoundException, IncludeQuizzes = false, etc.)

        // Write similar test methods for other methods of the QuestionService class
    }
}
