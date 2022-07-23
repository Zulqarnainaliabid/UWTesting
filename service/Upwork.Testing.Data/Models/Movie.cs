using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.Models
{
    public class Movie: AuditModel<long>
    {
        /// <summary>
        /// Original Title of the movie
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// Plot Summary of the movie
        /// </summary>
        [MaxLength(512)]
        public string Plot { get; set; }

        /// <summary>
        /// Poster Url of the movie
        /// </summary>
        [MaxLength(256)]
        public string PosterUrl { get; set; }

        /// <summary>
        /// Story Line of the movie
        /// </summary>
        public string StoryLine { get; set; }

        /// <summary>
        /// The date on movie was released
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Budget of the movie in USD
        /// </summary>
        [MaxLength(256)]
        public decimal Budget { get; set; }

        /// <summary>
        /// Length of the movie
        /// </summary>
        public TimeSpan Runtime { get; set; }

        /// <summary>
        /// Genres of the movie
        /// </summary>
        public virtual ICollection<Genre> Genres { get; set; } 

        /// <summary>
        /// Cast
        /// </summary>
        public virtual ICollection<MovieCast> Actors { get; set; } = new HashSet<MovieCast>();

        /// <summary>
        /// Movie reviews
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; } 
    }
}
