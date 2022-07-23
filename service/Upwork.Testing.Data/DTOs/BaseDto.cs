using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.DTOs
{
    public abstract class BaseDto<TType> : IValidatableObject
    {
        public TType Id { get; set; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
