using System.ComponentModel.DataAnnotations;

namespace Upwork.Testing.Data.Models
{
    public abstract class BaseModel<TType>
    {
        [Key]
        public TType Id { get; set; }
    }
}
