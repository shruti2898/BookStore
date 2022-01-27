using BookStoreModels.BooksModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        Task<BookModel> AddBook(BookModel book);
        Task<bool> DeleteBook(int bookId);
        Task<BookModel> UpdateBook(BookModel book);
    }
}
