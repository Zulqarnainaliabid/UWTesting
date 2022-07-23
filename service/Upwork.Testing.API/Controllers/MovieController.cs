using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upwork.Testing.Command.Movie;
using Upwork.Testing.Data.DTOs;

namespace Upwork.Testing.API.Controllers
{
    /// <summary>
    /// Controller for Movie APIs.
    /// </summary>
    public class MovieController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance from dependency injection.</param>
        public MovieController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Get all Movies.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            return Ok(await Mediator.Send(new GetAllMoviesQuery()));
        }

        /// <summary>
        /// Get a Movie by its Id.
        /// </summary>
        /// <param name="id">ID of the Movie to get.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(long id)
        {
            return Ok(await Mediator.Send(new GetMovieByIdQuery() { MovieId = id }));
        }

        /// <summary>
        /// Create a new Movie.
        /// </summary>
        /// <param name="dto">A Movie DTO.</param>
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie([FromBody] MovieDto dto)
        {
            return Ok(await Mediator.Send(new CreateMovieCommand() { Movie = dto }));
        }

        /// <summary>
        /// Update an existing Movie.
        /// </summary>
        /// <param name="dto">Updated Movie DTO.</param>
        [HttpPut]
        public async Task<ActionResult<MovieDto>> UpdateMovie([FromBody] MovieDto dto)
        {
            return Ok(await Mediator.Send(new UpdateMovieCommand() { Movie = dto }));
        }

        /// <summary>
        /// Delete an existing Movie.
        /// </summary>
        /// <param name="id">Id of the Movie to be deleted.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDto>> DeleteMovie([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteMovieCommand() { MovieId = id }));
        }
    }
}
