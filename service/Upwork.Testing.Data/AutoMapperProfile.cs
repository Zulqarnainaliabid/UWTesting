using AutoMapper;
using Upwork.Testing.Data.DTOs;
using Upwork.Testing.Data.DTOs.Auth;
using Upwork.Testing.Data.Models;
using Upwork.Testing.Data.Models.Auth;

namespace Upwork.Testing.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //domain to dto
            CreateMap<Movie, MovieDto>();
            CreateMap<Person, PersonDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<MovieCast, MovieCastDto>();
            CreateMap<Review, ReviewDto>();
            //user related
            CreateMap<User, UserSignUpDto>();

            //dto to domain
            CreateMap<MovieDto, Movie>();
            CreateMap<PersonDto, Person>();
            CreateMap<GenreDto, Genre>();
            CreateMap<MovieCastDto, MovieCast>();
            CreateMap<ReviewDto, Review>();
            //user related
            CreateMap<UserSignUpDto, User>();
        }
    }
}
