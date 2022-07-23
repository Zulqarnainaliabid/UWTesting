using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.DTOs
{
    public class GenreDto : AuditDto<long>
    {

        /// <summary>
        /// Name of the Genre
        /// </summary>
        [MaxLength(64)]
        public string Value { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                if (Value.Length > 64)
                {
                    yield return new ValidationResult($"{nameof(Value)} must be less than {64} characters.", new[] { nameof(Value) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(Value)} cannot be null or empty.", new[] { nameof(Value) });
            }
        }
    }
}
