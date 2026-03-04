using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class BooksCategoriesServiceTests
    {
        //AddCategory
        [Fact]
        public void AddCategory_WhenCalled_ReturnsCategoryId()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            BookCategory category = new BookCategory
            {
                Name = "Fiction"
            };

            mockRepo.Setup(repo => repo.InsertBooksCategory(category)).Returns(1);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.AddCategory(category);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBooksCategory(category), Times.Once);
        }


        //UpdateCategory
        [Fact]
        public void UpdateCategory_WhenCalled_ReturnsTrue()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            BookCategory category = new BookCategory
            {
                CategoryId = 1,
                Name = "Science Fiction"
            };

            mockRepo.Setup(repo => repo.UpdateBooksCategory(category)).Returns(true);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.UpdateCategory(category);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateBooksCategory(category), Times.Once);
        }


        //RemoveCategory
        [Fact]
        public void RemoveCategory_WhenCalled_ReturnsTrue()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.DeleteBooksCategory(1)).Returns(true);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.RemoveCategory(1);

            Assert.True(result);
            mockRepo.Verify(repo => repo.DeleteBooksCategory(1), Times.Once);
        }


        //GetCategory
        [Fact]
        public void GetCategory_WhenCategoryExists_ReturnsCategory()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            BookCategory category = new BookCategory
            {
                CategoryId = 1,
                Name = "Fiction"
            };

            mockRepo.Setup(repo => repo.GetBooksCategoryById(1)).Returns(category);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.GetCategory(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Fiction", result.Name);
            mockRepo.Verify(repo => repo.GetBooksCategoryById(1), Times.Once);
        }

        [Fact]
        public void GetCategory_WhenCategoryDoesNotExist_ReturnsNull()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBooksCategoryById(999)).Returns((BookCategory?)null);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.GetCategory(999);

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBooksCategoryById(999), Times.Once);
        }


        //GetAllCategories
        [Fact]
        public void GetAllCategories_WhenCalled_ReturnsListOfCategories()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            List<BookCategory> categories = new()
            {
                new BookCategory { CategoryId = 1, Name = "Fiction" },
                new BookCategory { CategoryId = 2, Name = "Non-Fiction" },
                new BookCategory { CategoryId = 3, Name = "Science" }
            };

            mockRepo.Setup(repo => repo.GetBooksCategories()).Returns(categories);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.GetAllCategories();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("Fiction", result.First().Name);
            mockRepo.Verify(repo => repo.GetBooksCategories(), Times.Once);
        }

        [Fact]
        public void GetAllCategories_WhenNoCategoriesExist_ReturnsNull()
        {
            Mock<IBooksCategoriesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBooksCategories()).Returns((IEnumerable<BookCategory>?)null);

            BooksCategoriesService service = new(mockRepo.Object);

            var result = service.GetAllCategories();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBooksCategories(), Times.Once);
        }
    }
}
