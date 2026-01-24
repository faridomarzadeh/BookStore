namespace BookStoreApp.API.DTOs.Author
{
    public class ReadOnlyAuthorDto: BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}
