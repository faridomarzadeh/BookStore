using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTOs.Author
{
    public class UpdateAuthorDto: BaseDto
    {
        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }
        [StringLength(250)]
        public string? Bio { get; set; }
    }
}
