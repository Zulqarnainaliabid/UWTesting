using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Person
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public int PersonId { get; set; }
    }

    public class DeletePersonCommandHandler : CommandHandlerBase,
        IRequestHandler<DeletePersonCommand, bool>
    {
        public DeletePersonCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            if (request.PersonId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Person)} Id must be provided.");
            }

            var Person = await Database.Persons.FindAsync(new object[] { request.PersonId }, cancellationToken);
            if (Person == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Person)} not found.");
            }

            Database.Persons.Remove(Person);

            await Database.SaveChangesAsync(cancellationToken);

            Debug.Assert(Database.Persons.Find(Person.Id) == null);

            return true;
        }
    }
}
