using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upwork.Testing.Data.Models;

namespace Upwork.Testing.Data.DTOs
{
    public class MovieCastDto:AuditDto<long>
    {
        public RoleType Role { get; set; }
        public MovieDto Movie { get; set; }
        public PersonDto Person { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}
