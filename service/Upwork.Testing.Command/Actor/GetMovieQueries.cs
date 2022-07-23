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

namespace Upwork.Testing.Command.Person
{
    public class GetAllPersonsQuery : IRequest<IEnumerable<PersonDto>> { }

    public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public long PersonId { get; set; }
    }

    public class PersonQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonDto>>,
        IRequestHandler<GetPersonByIdQuery, PersonDto>
    {
        public PersonQueryHandler(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        // GET ALL
        public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await Database.Persons
                .Select(x => Mapper.Map<PersonDto>(x))
                .ToListAsync(cancellationToken);
        }

        // GET BY ID
        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.PersonId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Person)} Id must be provided.");
            }

            var innerResult = await Database.Persons.FindAsync(new object[] { request.PersonId }, cancellationToken);
            if (innerResult == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Person)} with Id {request.PersonId} cannot be found.");
            }
            var result= Mapper.Map<PersonDto>(innerResult);
            
            return result;
        }
    }
}
