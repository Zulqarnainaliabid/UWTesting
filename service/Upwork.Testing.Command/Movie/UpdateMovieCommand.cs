using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.DTOs;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Movie
{
    public class UpdateMovieCommand : IRequest<MovieDto>
    {
        public MovieDto Movie { get; set; }
    }

    public class UpdateMovieCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateMovieCommand, MovieDto>
    {
        public UpdateMovieCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Movie;

            if (dto.Id <= 0)
            {
                throw new BadRequestException($"{nameof(Data.Models.Movie)} Id must be greater than zero.");
            }

            var model = await Database.Movies
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (model == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Movie)} with Id {dto.Id} not found.");
            }

            // ensure uniqueness of name
            bool nameAlreadyUsed = await Database.Movies.AnyAsync(e => e.Title.Trim() == dto.Title.Trim(), cancellationToken) && dto.Title != (model.Title);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.Title)} {dto.Title} already used.");
            }

            model.Title = dto.Title;
            model.Plot = dto.Plot;
            model.ReleaseDate = dto.ReleaseDate;
            model.Runtime = dto.Runtime;
            model.StoryLine = dto.StoryLine;

            Database.Movies.Update(model);

            await Database.SaveChangesAsync(cancellationToken);

            return Mapper.Map<MovieDto>(model);
        }
    }
}
