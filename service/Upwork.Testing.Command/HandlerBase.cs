using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Upwork.Testing.Data;

namespace Upwork.Testing.Command
{
    /// <summary>
    /// Base class of all handlers.
    /// </summary>
    public abstract class HandlerBase
    {
        protected IMediator Mediator { get; }

        protected UpworkTestingDbContext Database { get; }

        protected IMapper Mapper { get; }

        protected IAuthorizationService AuthorizationService { get; }

        protected HandlerBase(
            IMediator mediator,
            UpworkTestingDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            Mediator = mediator;
            Database = database;
            Mapper = mapper;
            AuthorizationService = authorizationService;
        }
    }
}
