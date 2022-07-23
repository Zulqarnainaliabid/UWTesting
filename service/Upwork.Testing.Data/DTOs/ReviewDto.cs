using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.DTOs
{
    public class ReviewDto:AuditDto<long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;//>= 1 && value <= 10 ? value : throw new Exception("Rating should not be outside of range (1,10)");
            }
        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                if (Text.Length > 28)
                {
                    yield return new ValidationResult($"{nameof(Title)} must be less than {28} characters.", new[] { nameof(Title) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(Title)} cannot be null or empty.", new[] { nameof(Title) });
            }
            if (!string.IsNullOrWhiteSpace(Text))
            {
                if (Text.Length > 128)
                {
                    yield return new ValidationResult($"{nameof(Text)} must be less than {128} characters.", new[] { nameof(Text) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(Text)} cannot be null or empty.", new[] { nameof(Text) });
            }
            if(Rating>10||Rating<1)
            {
                yield return new ValidationResult($"{Rating} should not be outside of range (1,10)", new[] { nameof(Rating) });
            }
        }
    }
}
