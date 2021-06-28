using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class ArticleDetails : DomainObject
    {
        public int Id { get; set; }
        public int? MachineNumber { get; set; }
        public int? CapiPrevisti { get; set; }
        public DateTime? DataInizioProd { get; set; }
        public string? Notes { get; set; }
        public DateTime? DataArrSchedePr { get; set; }
        public DateTime? DataConsegnaPr { get; set; }
        public DateTime? DataArrSchedeCa { get; set; }
        public DateTime? DataConsegnaCa { get; set; }
        public DateTime? DataArrTagliaBase { get; set; }
        public DateTime? DataArrInzioTagliaBase { get; set; }
        public DateTime? DataArrFineTagliaBase { get; set; }
        public DateTime? DataArrSchedeCo { get; set; }
        public DateTime? DataConsegnaCo { get; set; }
        public DateTime? DataArrSchedaDisco { get; set; }
        public double? DiffGGProdData { get; set; }
        public double? DiffGGProgData { get; set; }
        public DateTime? DataConsegnaPP { get; set; }
        public double? GG1 { get; set; }
        public DateTime? Ok { get; set; }
        public DateTime? DataInizioSvilTgBase { get; set; }
        public DateTime? DataFineSvilTgBase { get; set; }
        public double? GG2 { get; set; }
        public bool? Finish { get; set; }

        [ForeignKey("Articole")]
        public int? ArticleID { get; set; }
        
        public Articole Articole { get; set; }
    }
}
