using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Movie
{
    public class DeleteMovieCommand : IRequest<bool>
    {
        public int MovieId { get; set; }
    }

    public class DeleteMovieCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteMovieCommand, bool>
    {
        public DeleteMovieCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.MovieId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Movie)} Id must be provided.");
            }

            var Movie = await Database.Movies.FindAsync(new object[] { request.MovieId }, cancellationToken);
            if (Movie == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Movie)} not found.");
            }

            Database.Movies.Remove(Movie);

            await Database.SaveChangesAsync(cancellationToken);

            Debug.Assert(Database.Movies.Find(Movie.Id) == null);

            return true;
        }
    }
}
