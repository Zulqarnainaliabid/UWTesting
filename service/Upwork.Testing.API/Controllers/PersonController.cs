using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upwork.Testing.Command.Person;
using Upwork.Testing.Data.DTOs;

namespace Upwork.Testing.API.Controllers
{
    /// <summary>
    /// Controller for Person APIs.
    /// </summary>
    public class PersonController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance from dependency injection.</param>
        public PersonController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Get all Persons.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllPersons()
        {
            return Ok(await Mediator.Send(new GetAllPersonsQuery()));
        }

        /// <summary>
        /// Get a Person by its Id.
        /// </summary>
        /// <param name="id">ID of the Person to get.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(long id)
        {
            return Ok(await Mediator.Send(new GetPersonByIdQuery() { PersonId = id }));
        }

        /// <summary>
        /// Create a new Person.
        /// </summary>
        /// <param name="dto">A Person DTO.</param>
        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] PersonDto dto)
        {
            return Ok(await Mediator.Send(new CreatePersonCommand() { Person = dto }));
        }

        /// <summary>
        /// Update an existing Person.
        /// </summary>
        /// <param name="dto">Updated Person DTO.</param>
        [HttpPut]
        public async Task<ActionResult<PersonDto>> UpdatePerson([FromBody] PersonDto dto)
        {
            return Ok(await Mediator.Send(new UpdatePersonCommand() { Person = dto }));
        }

        /// <summary>
        /// Delete an existing Person.
        /// </summary>
        /// <param name="id">Id of the Person to be deleted.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonDto>> DeletePerson([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeletePersonCommand() { PersonId = id }));
        }
    }
}
