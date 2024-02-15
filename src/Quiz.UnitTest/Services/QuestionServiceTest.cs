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

        // Add more test methods for other scenarios (e.g., NotFoundException, IncludeQuizzes = false, etc.)

        // Write similar test methods for other methods of the QuestionService class
    }
}
