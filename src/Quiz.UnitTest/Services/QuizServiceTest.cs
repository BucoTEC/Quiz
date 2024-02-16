using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Quiz.Bll.Dtos;
using Quiz.Bll.Exceptions;
using Quiz.Bll.SearchQueries;
using Quiz.Bll.Services.QuizExporterService;
using Quiz.Bll.Services.QuizService;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications.QuestionSearch;
using Quiz.Dal.Specifications.QuizSearch;

namespace Quiz.Bll.Tests.Services
{
    public class QuizServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<QuizExporterManager> _mockQuizExporterManager;
        private readonly QuizService _quizService;
        private readonly Fixture _fixture;

        public QuizServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockQuizExporterManager = new Mock<QuizExporterManager>();
            _quizService = new QuizService(_mockUnitOfWork.Object, _mockQuizExporterManager.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetQuizById_ThrowsNotFoundException_WhenQuizWithIdDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync((QuizEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _quizService.GetQuizById(id, true));
        }

        [Fact]
        public async Task GetQuizById_ReturnsQuizResponseDto_WhenQuizWithIdExists()
        {
            // Arrange
            var includeQuestions = true;
            var quizEntity = _fixture.Build<QuizEntity>().Without(x => x.Questions).Create();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync(quizEntity);

            // Act
            var result = await _quizService.GetQuizById(quizEntity.Id, includeQuestions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(quizEntity.Id, result.Id);
            Assert.Equal(quizEntity.Name, result.Name);
            // Add more assertions as needed
        }

        [Fact]
        public async Task SearchQuizzes_ReturnsPaginationOfQuizResponseDto()
        {
            // Arrange
            var searchQuizzesQuery = _fixture.Create<SearchQuizzesQuery>();
            var expectedQuizzes = _fixture.CreateMany<QuizEntity>().ToList();
            var totalCountOfQuizzes = expectedQuizzes.Count;

            var mockQuizzesSearchSpecification = new Mock<QuizzesSearchSpecification>(It.IsAny<QuizSearchParams>());
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.CountAsync(It.IsAny<QuizzesCountSpecification>())).ReturnsAsync(totalCountOfQuizzes);
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.ListAsync(It.IsAny<QuizzesSearchSpecification>())).ReturnsAsync(expectedQuizzes);

            // Act
            var pagination = await _quizService.SearchQuizzes(searchQuizzesQuery);

            // Assert
            Assert.NotNull(pagination);
            Assert.Equal(searchQuizzesQuery.PageIndex, pagination.PageIndex);
            Assert.Equal(searchQuizzesQuery.PageSize, pagination.PageSize);
            Assert.Equal(totalCountOfQuizzes, pagination.Count);

            var quizzes = pagination.Data.ToList();
            Assert.Equal(expectedQuizzes.Count, quizzes.Count);

            // Verify quiz response data
            for (int i = 0; i < expectedQuizzes.Count; i++)
            {
                Assert.Equal(expectedQuizzes[i].Id, quizzes[i].Id);
                Assert.Equal(expectedQuizzes[i].Name, quizzes[i].Name);
                // Add more assertions as needed
            }
        }

        [Fact]
        public async Task CreateQuiz_SuccessfulCreation_WhenNoExistingQuizWithSameName()
        {
            // Arrange
            var createQuizDto = _fixture.Create<CreateQuizDto>();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync((QuizEntity)null);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.ListAsync(It.IsAny<QuestionsSearchSpecification>())).ReturnsAsync(new List<QuestionEntity>());

            // Act
            var createdQuiz = await _quizService.CreateQuiz(createQuizDto);

            // Assert
            Assert.NotNull(createdQuiz);
            Assert.Equal(createQuizDto.Name, createdQuiz.Name);
            // Add more assertions as needed
        }

        [Fact]
        public async Task CreateQuiz_ThrowsBadRequestException_WhenQuizWithSameNameExists()
        {
            // Arrange
            var createQuizDto = _fixture.Create<CreateQuizDto>();
            var existingQuiz = _fixture.Build<QuizEntity>().With(x => x.Name, createQuizDto.Name).Create();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync(existingQuiz);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _quizService.CreateQuiz(createQuizDto));
        }

        [Fact]
        public async Task CreateQuiz_ThrowsBadRequestException_WhenDuplicateQuestionTextsInDto()
        {
            // Arrange
            var createQuizDto = _fixture.Build<CreateQuizDto>()
                .With(dto => dto.Name, "Sample Quiz")
                .Create();

            var existingQuiz = new QuizEntity { Name = createQuizDto.Name };

            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>()))
                           .ReturnsAsync(existingQuiz);// Ensure that ListAsync returns an empty list

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _quizService.CreateQuiz(createQuizDto));
        }

        [Fact]
        public async Task UpdateQuiz_WithSameNameAsExistingQuiz_ThrowsBadRequestException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateQuizDto = _fixture.Create<UpdateQuizDto>();
            var existingQuiz = _fixture.Build<QuizEntity>().With(q => q.Name, updateQuizDto.Name).Create();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync(existingQuiz);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _quizService.UpdateQuiz(id, updateQuizDto));
        }

        [Fact]
        public async Task UpdateQuiz_WithNonExistentQuiz_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateQuizDto = _fixture.Create<UpdateQuizDto>();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync((QuizEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _quizService.UpdateQuiz(id, updateQuizDto));
        }

        [Fact]
        public async Task UpdateQuiz_WithInvalidQuestionIds_ThrowsBadRequestException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateQuizDto = _fixture.Create<UpdateQuizDto>();
            var existingQuiz = _fixture.Create<QuizEntity>();
            var invalidQuestionIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync(existingQuiz);
            _mockUnitOfWork.Setup(uow => uow.QuestionRepository.ListAsync(It.IsAny<QuestionsSearchSpecification>())).ReturnsAsync(new List<QuestionEntity>());

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _quizService.UpdateQuiz(id, updateQuizDto));
        }

        [Fact]
        public async Task DeleteQuiz_WithValidId_DeletesQuiz()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingQuiz = _fixture.Create<QuizEntity>();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetByIdAsync(id)).ReturnsAsync(existingQuiz);

            // Act
            await _quizService.DeleteQuiz(id);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.QuizRepository.Delete(existingQuiz), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteQuiz_WithNonExistentId_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetByIdAsync(id)).ReturnsAsync((QuizEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _quizService.DeleteQuiz(id));
            _mockUnitOfWork.Verify(uow => uow.QuizRepository.Delete(It.IsAny<QuizEntity>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task ExportQuiz_WithNonExistentId_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var exporter = "SomeExporter";
            _mockUnitOfWork.Setup(uow => uow.QuizRepository.GetEntityWithSpec(It.IsAny<QuizWithQuestionsSpecification>())).ReturnsAsync((QuizEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _quizService.ExportQuiz(exporter, nonExistentId));
        }
    }
}