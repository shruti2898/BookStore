using BookStoreModels.BooksModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string connectionString;
        private IConfiguration Configuration;
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DbConnection");
        }
        public async Task<BookModel> AddBook(BookModel book)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spAddBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Image", book.Image);
                command.Parameters.AddWithValue("@Quantity", book.Quantity);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                command.Parameters.AddWithValue("@Rating", book.Rating);
                command.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                command.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                connection.Open();
                await command.ExecuteNonQueryAsync();
                var result = command.Parameters["@BookId"].Value;
                if (result != DBNull.Value)
                {
                    book.BookId = (int)result;
                    return book;
                }
                return null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public async Task<bool> DeleteBook(int bookId)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spDeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);
                connection.Open();
                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Image", book.Image);
                command.Parameters.AddWithValue("@Quantity", book.Quantity);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                command.Parameters.AddWithValue("@Rating", book.Rating);
                command.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                connection.Open();
                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                {
                    return book;
                }
                return null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
