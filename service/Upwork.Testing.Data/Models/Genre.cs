using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.Models
{
    public class Genre : AuditModel<long>
    {

        /// <summary>
        /// Name of the Genre
        /// </summary>
        [MaxLength(64)]
        public string Value { get; set; }
    }
}
