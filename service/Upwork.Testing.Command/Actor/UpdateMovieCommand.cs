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

namespace Upwork.Testing.Command.Person
{
    public class UpdatePersonCommand : IRequest<PersonDto>
    {
        public PersonDto Person { get; set; }
    }

    public class UpdatePersonCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdatePersonCommand, PersonDto>
    {
        public UpdatePersonCommandHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<PersonDto> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Person;

            if (dto.Id <= 0)
            {
                throw new BadRequestException($"{nameof(Data.Models.Person)} Id must be greater than zero.");
            }

            var model = await Database.Persons
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (model == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Person)} with Id {dto.Id} not found.");
            }

            // ensure uniqueness of name
            bool nameAlreadyUsed = await Database.Persons.AnyAsync(e => e.FullName.Trim() == dto.FullName.Trim(), cancellationToken) && dto.FullName != (model.FullName);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.FullName)} {dto.FullName} already used.");
            }

            model.FullName = dto.FullName;
            model.Gender = dto.Gender;
            model.ImageURL = dto.ImageURL;

            Database.Persons.Update(model);

            await Database.SaveChangesAsync(cancellationToken);

            return Mapper.Map<PersonDto>(model);
        }
    }
}
