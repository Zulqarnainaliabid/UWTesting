using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.DTOs;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Movie
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<MovieDto>> { }

    public class GetMovieByIdQuery : IRequest<MovieDto>
    {
        public long MovieId { get; set; }
    }

    public class MovieQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>,
        IRequestHandler<GetMovieByIdQuery, MovieDto>
    {
        public MovieQueryHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        // GET ALL
        public async Task<IEnumerable<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await Database.Movies
                .Select(x => Mapper.Map<MovieDto>(x))
                .ToListAsync(cancellationToken);
        }

        // GET BY ID
        public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.MovieId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Movie)} Id must be provided.");
            }

            var innerResult = await Database.Movies.Include(x=>x.Reviews).Include(x => x.Actors).ThenInclude(x=>x.Person).FirstAsync(x=>x.Id==request.MovieId);//.FindAsync(new object[] { request.MovieId }, cancellationToken);
            if (innerResult == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Movie)} with Id {request.MovieId} cannot be found.");
            }
            var result= Mapper.Map<MovieDto>(innerResult);
            GenreDto[] genres= { new GenreDto() { Id=1,Value="Drama"},new GenreDto { Id=2,Value= "Science Fiction" },new GenreDto { Id=3,Value="Thriller"} };
            result.Genres = genres;
            return result;
        }
    }
}
