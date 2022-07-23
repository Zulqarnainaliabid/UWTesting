using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Upwork.Testing.Data.Models.Auth;

namespace Upwork.Testing.Data.Models
{
    public class Review : AuditModel<long>
    {
        public long MovieId { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        [JsonIgnore]
        public long UserId { get; set; }
        public User User { get; set; }
        [MaxLength(28)]
        public string Title { get; set; }
        [MaxLength(128)]
        public string Text { get; set; }
        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
            }
        }
    }
}
