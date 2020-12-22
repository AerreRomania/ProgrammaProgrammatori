using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class ProgrammaProgramatoriArticle : DomainObject
    {
        public int Id { get; set; }
        public string MODEL { get; set; }
        public DateTime? DATA_ARRIVO_SCHEDE_PR { get; set; }
        public DateTime? CONF_CAMP { get; set; }
        public DateTime? DATA_ARRIVO_FILO { get; set; }
        public int? CAPI_PREVISTI { get; set; }
        public int? NR_MACH { get; set; }
        public DateTime? DATA_ENTRATA_IN_PROD { get; set; }
        public DateTime? DATA_ARRIVO_SCHEMA { get; set; }
        public DateTime? DATA_INIZIO_SVIL_TG_BASE { get; set; }
        public DateTime? DATA_FINE_SVIL_TG_BASE { get; set; }
        public DateTime? GG1 { get; set; }
        public DateTime? DATA_ARRIVO_SCHEDE { get; set; }
        public DateTime? DATA_ARRIVO_DISCO { get; set; }
        public DateTime? GG2 { get; set; }
        public DateTime? GG3 { get; set; }
        public DateTime? DATA_INIZIO_PROD { get; set; }
        public DateTime? GG4 { get; set; }
        public int? TOT_GG { get; set; }
        public DateTime? INIZ_FINE { get; set; }
        public double? PESI_X_ITA { get; set; }
        public int? TEMP { get; set; }
        public bool? CONF_PP { get; set; }
        [ForeignKey("Articole")]
        public int? ArticleID { get; set; }
        public Articole Articole { get; set; }
        public bool? Finished { get; set; }
    }
}
