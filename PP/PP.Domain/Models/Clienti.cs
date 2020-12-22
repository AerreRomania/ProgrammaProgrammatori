using System.Collections.Generic;

namespace PP.Domain.Models
{
    public class Clienti : DomainObject
    {
        public int Id { get; set; }

        public string Client { get; set; }
        public ICollection<Comenzi> Comenzi { get; set; }

    }
}
