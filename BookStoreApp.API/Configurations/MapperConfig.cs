using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTOs.Author;
using BookStoreApp.API.DTOs.Book;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateAuthorDto, Author>().ReverseMap();
            CreateMap<ReadOnlyAuthorDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorDto, Author>().ReverseMap();
            CreateMap<ReadOnlyBookDto, Book>().ReverseMap().ForMember(q=>q.AuthorName, d=> d.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"));
            CreateMap<DetailedBookDto, Book>().ReverseMap().ForMember(q => q.AuthorName, d => d.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"));
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
        }
    }
}
