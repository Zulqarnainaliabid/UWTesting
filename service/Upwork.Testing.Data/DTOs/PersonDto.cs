using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upwork.Testing.Data.Models;

namespace Upwork.Testing.Data.DTOs
{
    public class PersonDto:AuditDto<long>
    {

        /// <summary>
        /// Full Name of the person
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string FullName { get; set; }

        /// <summary>
        /// Main image of the person
        /// </summary>
        [MaxLength(255)]
        public string ImageURL { get; set; }

        /// <summary>
        /// Gender of the person
        /// </summary>
        [MaxLength(2)]
        public Gender Gender { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(FullName))
            {
                if (FullName.Length > 128)
                {
                    yield return new ValidationResult($"{nameof(FullName)} must be less than {128} characters.", new[] { nameof(FullName) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(FullName)} cannot be null or empty.", new[] { nameof(FullName) });
            }
        }
    }
}
