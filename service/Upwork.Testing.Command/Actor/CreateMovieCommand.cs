using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data;
using Upwork.Testing.Data.DTOs;
using Upwork.Testing.Data.Exceptions;

namespace Upwork.Testing.Command.Person
{
    public class CreatePersonCommand : IRequest<PersonDto>
    {
        public PersonDto Person { get; set; }
    }

    public class CreatePersonCommandHandler : CommandHandlerBase,
        IRequestHandler<CreatePersonCommand, PersonDto>
    {
        public CreatePersonCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Person;

            bool nameAlreadyUsed = await Database.Persons.AnyAsync(e => e.FullName.Trim() == dto.FullName.Trim(), cancellationToken);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.FullName)} '{dto.FullName}' already used.");
            }

            var model = new Data.Models.Person()
            {
                FullName = dto.FullName,
                Gender=dto.Gender,
                ImageURL=dto.ImageURL,
            };

            Database.Persons.Add(model);

            await Database.SaveChangesAsync(cancellationToken);


            return Mapper.Map<PersonDto>(model);
        }
    }
}
