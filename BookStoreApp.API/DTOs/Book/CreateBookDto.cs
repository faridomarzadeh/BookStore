using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTOs.Book
{
    public class CreateBookDto
    {
        [StringLength(50)]
        public required string Title { get; set; }
        [Range(1000,int.MaxValue)]
        public required int Year { get; set; }
        public required string Isbn { get; set; }
        [StringLength(250,MinimumLength =10)]
        public required string Summary { get; set; }

        [Range(0,int.MaxValue)]
        public required decimal Price { get; set; }
    }
}
