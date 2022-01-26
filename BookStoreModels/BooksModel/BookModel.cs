using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels.BooksModel
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
