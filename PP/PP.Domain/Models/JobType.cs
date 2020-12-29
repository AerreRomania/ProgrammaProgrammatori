namespace PP.Domain.Models
{
    public class JobType : DomainObject
    {
        public new int Id { get => JobTypeID; set => JobTypeID = value; }

        public int JobTypeID { get; set; }

        public string JobTypeName { get; set; }

        public string Color { get; set; }
    }
}