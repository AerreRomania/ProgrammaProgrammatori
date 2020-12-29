using System.Collections.Generic;

namespace PP.Domain.Models
{
    public class Sector : DomainObject
    {
        public new int Id { get => SectorID; set => SectorID = value; }
        public int SectorID { get; set; }
        public string SectorName { get; set; }
        public ICollection<Articole> Articole { get; set; }
    }
}