using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.DTOs;
using Upwork.Testing.Data.Events;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Movie
{
    public class CreateMovieCommand : IRequest<MovieDto>
    {
        public MovieDto Movie { get; set; }
    }

    public class CreateMovieCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateMovieCommand, MovieDto>
    {
        public CreateMovieCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Movie;

            bool nameAlreadyUsed = await Database.Movies.AnyAsync(e => e.Title.Trim() == dto.Title.Trim(), cancellationToken);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.Title)} '{dto.Title}' already used.");
            }

            var model = new Data.Models.Movie()
            {
                Title = dto.Title,
                Plot=dto.Plot,
                StoryLine=dto.StoryLine,
                Budget=dto.Budget,
                PosterUrl=dto.PosterUrl,
                ReleaseDate=dto.ReleaseDate,
                Runtime=dto.Runtime,
            };

            Database.Movies.Add(model);

            await Database.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new MovieCreatedDomainEvent(model), cancellationToken);

            return Mapper.Map<MovieDto>(model);
        }
    }
}
