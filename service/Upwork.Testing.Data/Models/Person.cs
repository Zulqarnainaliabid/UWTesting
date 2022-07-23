using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.Models
{
    public class Person : AuditModel<long>
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
    }
}
