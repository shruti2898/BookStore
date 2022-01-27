using BookStoreManager.Interface;
using BookStoreModels.BooksModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookManager manager;
        public BooksController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBook([FromBody] BookModel book)
        {
            try
            {
                BookModel data = await this.manager.AddBook(book);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Added Succesfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to add book" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("{bookId}/delete")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                var result = await this.manager.DeleteBook(bookId);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Book deleted successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Unable to delete book" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{bookId}/update")]
        public async Task<IActionResult> UpdateBook(int bookId,[FromBody] BookModel book)
        {
            try
            {
                BookModel data = await this.manager.UpdateBook(book);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Book details updated successfully", Data=data });
                }
                return this.BadRequest(new { Status = false, Message = "Unable to update book details" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
