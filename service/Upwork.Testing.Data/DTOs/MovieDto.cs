using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.DTOs
{
    public class MovieDto : AuditDto<long>
    {

        /// <summary>
        /// Original Title of the movie
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Plot Summary of the movie
        /// </summary>
        [MaxLength(512)]
        public string Plot { get; set; }

        /// <summary>
        /// Story Line of the movie
        /// </summary>
        public string StoryLine { get; set; }

        /// <summary>
        /// Story Line of the movie
        /// </summary>
        public string PosterUrl { get; set; }
        
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
        public ICollection<GenreDto> Genres { get; set; } 

        /// <summary>
        /// Cast
        /// </summary>
        public ICollection<MovieCastDto> Actors { get; set; } 

        /// <summary>
        /// Movie reviews
        /// </summary>
        public ICollection<ReviewDto> Reviews { get; set; } 

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                if (Title.Length > 128)
                {
                    yield return new ValidationResult($"{nameof(Title)} must be less than {128} characters.", new[] { nameof(Title) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(Title)} cannot be null or empty.", new[] { nameof(Title) });
            }
        }
    }
}
