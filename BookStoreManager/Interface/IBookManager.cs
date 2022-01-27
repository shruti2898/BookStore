using BookStoreModels.BooksModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        Task<BookModel> AddBook(BookModel book);
        Task<bool> DeleteBook(int bookId);
        Task<BookModel> UpdateBook(BookModel book);
    }
}
