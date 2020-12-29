using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class Comenzi : DomainObject
    {
        public int Id { get; set; }

        public int IdArticol { get; set; }

        [ForeignKey("Clienti")]
        public int IdClient { get; set; }

        public Clienti Clienti { get; set; }
    }
}