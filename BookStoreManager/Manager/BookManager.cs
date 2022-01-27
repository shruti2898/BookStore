using BookStoreManager.Interface;
using BookStoreModels.BooksModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<BookModel> AddBook(BookModel book)
        {
            try
            {
                return await this.repository.AddBook(book);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            try
            {
                return await this.repository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            try
            {
                return await this.repository.UpdateBook(book);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
