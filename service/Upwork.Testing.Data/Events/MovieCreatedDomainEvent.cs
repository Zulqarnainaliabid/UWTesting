using MediatR;
using Upwork.Testing.Data.Models;

namespace Upwork.Testing.Data.Events
{
    public class MovieCreatedDomainEvent : INotification
    {
        public Movie Movie { get; }

        public MovieCreatedDomainEvent(Movie movie)
        {
            Movie = movie;
        }
    }
}
