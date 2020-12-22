namespace PP.Domain.Models
{
    public class WorkLocation : DomainObject
    {
        public new int Id { get => WorkLocationID; set => WorkLocationID = value; }
        public int WorkLocationID { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
    }
}
