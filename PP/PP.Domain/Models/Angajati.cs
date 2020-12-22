using System;
using System.Collections.Generic;

namespace PP.Domain.Models
{
    public class Angajati : DomainObject
    {
        public int Id { get; set; }
        public string CodAngajat { get; set; }
        public string Angajat { get; set; }
        public string Sex { get; set; }
        public DateTime? DataNasterii { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime? DataAngajarii { get; set; }
        public bool? status { get; set; }
        public string Linie { get; set; }
        public int IdSector { get; set; }
        public bool? Active { get; set; }
        public DateTime? LastTimeLogged { get; set; }
        public string Mansione { get; set; }
        public ICollection<ProgrammerTask> ProgrammerTask { get; set; } = new List<ProgrammerTask>();
    }
}
