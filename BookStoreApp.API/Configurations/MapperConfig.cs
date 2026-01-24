using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTOs.Author;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateAuthorDto, Author>().ReverseMap();
            CreateMap<ReadOnlyAuthorDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorDto, Author>().ReverseMap();
        }
    }
}
