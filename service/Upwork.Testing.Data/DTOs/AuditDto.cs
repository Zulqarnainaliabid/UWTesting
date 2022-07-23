using System;

namespace Upwork.Testing.Data.DTOs
{
    public abstract class AuditDto<TType> : BaseDto<TType>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
