using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Persistence.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryManagement.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<LibraryManagementDbContext> _context;
        private readonly Mock<IBookRepository> _bookRepository;

        private  List<Book> _listBook = new List<Book> { new BookBuilder().Build(), new BookBuilder().Build() };

        public BookRepositoryTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _context = new Mock<LibraryManagementDbContext>();
            _bookRepository = new Mock<IBookRepository>();

        }

        [Fact]
        public async Task Execute_AddBook_ReturnIdBookIsValid()
        {

            var _dbBook = DbContextMock.GetQueryableMockDbSet<Book>(_listBook);

            _context.Setup(m => m.Books).Returns(_dbBook);

            var book = _listBook.FirstOrDefault();

            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            var bookRepository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            var response = await bookRepository.Add(book);

            response.Should().Be(book.Id);

            _context.Verify(x => x.Books.AddAsync(book, new CancellationToken()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task Execute_UpdateBook_ReturnSuccess()
        {
            var _dbBook = DbContextMock.GetQueryableMockDbSet<Book>(_listBook);

            _context.Setup(m => m.Books).Returns(_dbBook);

            var book = _listBook.FirstOrDefault();

            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            var bookRepository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            var response = bookRepository.Update(book);

            _context.Verify(x => x.Books.Update(book), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
        /*
        [Fact]
        public async Task Execute_AddGetAll_ReturnIdBookIsValid()
        {
            var _dbBook = DbContextMock.GetQueryableMockDbSet<Book>(_listBook);

            var dbSet = new Mock<DbSet<Book>>();

            var book = _listBook.FirstOrDefault();
            book.IsDeleted = false;
            _context.Setup(m => m.Books).Returns(_dbBook);

            _bookRepository.Setup(m => m.Exists(It.IsAny<int>())).ReturnsAsync(true);

            //_context.SetupSet(b => b.Books.Any()).Callback(() => { return true; });

            //dbSet.SetupSet(b => b.Any()).Callback(() => { return true; });

            //dbSet.As<IQueryable<Book>>().SetupSet(b => b.Any()).Callback(() => { return true; });

            //dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);

            _unitOfWorkMock.Setup(u => u.CompleteAsync());

            var bookRepository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            var response = await bookRepository.Exists(book.Id);

            response.Should().BeTrue();

            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }


        [Fact]
        public async Task Exists_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange

            var book = new BookBuilder().Build();

            var _dbBook = DbContextMock.GetQueryableMockDbSet<Book>(_listBook);

            _context.Setup(m => m.Books).Returns(_dbBook);

            _context.Setup(m => m.Books.FirstOrDefault(m => m.Id == book.Id)).Returns(_dbBook.FirstOrDefault());

            //_context.Setup(m => m.Books.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(_dbBook.FirstOrDefault()));

            var repository = new BookRepository(_unitOfWorkMock.Object, _context.Object);
            
            // Act
            var exists = await repository.Exists(book.Id);

            // Assert
            exists.Should().BeTrue();


            //_context.Verify(x => x.Books.Find(c => c.Id == id && !c.IsDeleted), Times.Once);
        }


        
        [Fact]
        public async Task Exists_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            var id = 1;
            //_context.Setup(x => x.Books.Find(c => c.Id == id && !c.IsDeleted)).Returns(Task.FromResult(false));
            var repository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            // Act
            var exists = await repository.Exists(id);

            // Assert
            Assert.False(exists);
            //_context.Verify(x => x.Books.Find(c => c.Id == id && !c.IsDeleted), Times.Once);
        }

        */

        
        /*
        [Fact]
        public async Task GetByIdAndHasQuantity_ShouldReturnBook_WhenBookExistsAndHasQuantity()
        {
            // Arrange
            var id = 1;
            //var book = new Book { Id = id, Quantity = 1 };
            //_context.Setup(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0)).Returns(Task.FromResult(book));
            var repository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            // Act
            var returnedBook = await repository.GetByIdAndHasQuantity(id);

            // Assert
            Assert.NotNull(returnedBook);
            //Assert.Equal(book.Id, returnedBook!.Id);
            //_context.Verify(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0), Times.Once);
        }

        

        [Fact]
        public async Task GetByIdAndHasQuantity_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            var id = 1;
            //_context.Setup(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0))
                       //.Returns(Task.FromResult<Book>(null));
            var repository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            // Act
            var returnedBook = await repository.GetByIdAndHasQuantity(id);

            // Assert
            Assert.Null(returnedBook);
            //_context.Verify(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0), Times.Once);
        }

        [Fact]
        public async Task GetByIdAndHasQuantity_ShouldReturnNull_WhenBookHasNoQuantity()
        {
            // Arrange
            var id = 1;
            //var book = new Book { Id = id, Quantity = 0 };
            //_context.Setup(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0))
            //          .Returns(Task.FromResult(book));
            var repository = new BookRepository(_unitOfWorkMock.Object, _context.Object);

            // Act
            var returnedBook = await repository.GetByIdAndHasQuantity(id);

            // Assert
            Assert.Null(returnedBook);
            //_context.Verify(x => x.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0), Times.Once);
        }*/
    }
}
