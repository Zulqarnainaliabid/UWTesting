using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Upwork.Testing.Data;

namespace Upwork.Testing.Command
{
    /// <summary>
	/// Base class of all command handlers.
	/// </summary>
    public abstract class CommandHandlerBase : HandlerBase
    {
        protected CommandHandlerBase(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }
    }
}
