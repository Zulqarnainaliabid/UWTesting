namespace Upwork.Testing.Data.Models
{
    public class MovieCast:AuditModel<long>
    {
        //public long MovieId { get; set; }
        //public long PersonId { get; set; }
        public long MovieId { get; set; }
        public long PersonId { get; set; }
        public RoleType Role { get; set; }
        public Movie Movie { get; set; }
        public Person Person { get; set; }
    }
}
