using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class Articole : DomainObject
    {
        
        public int Id { get; set; }
        public string Articol { get; set; }
        public string Descriere { get; set; }
        public double? CostProductie { get; set; }
        public int? IdStagiune { get; set; }
        public string Finete { get; set; }
        public double? Prezzo { get; set; }
        public double? Centes { get; set; }
        public string? Stagione { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
        public byte[] PdfView { get; set; }
        public int? IdTaglie { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<ProgrammerTask> ProgrammerTask { get; set; } = new List<ProgrammerTask>();
        public ICollection<ArticleDetails> ArticleDetails { get; set; } = new List<ArticleDetails>();

        [ForeignKey("Sector")]
        public int IdSector { get; set; }

        public virtual Sector Sector { get; set; }
    }
}