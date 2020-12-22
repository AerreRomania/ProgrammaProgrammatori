using PP.Domain.Models;
using System;

namespace PP.Domain.Columns
{
    public class ArticleGridColumns : DomainObject
    {
        public int? PPArticleID { get; set; }
        public int? Num { get; set; }
        public string Articolo { get; set; }
        public string Cliente { get; set; }
        public string Model { get; set; }
        public string Fin { get; set; }
        public string ProgrammerPR { get; set; }
        public DateTime? DataArrivoSchedePr { get; set; }
        public DateTime? StartPr { get; set; }
        public DateTime? EndPr { get; set; }
        public string ProgrammerCa { get; set; }
        public DateTime? StartCa { get; set; }
        public DateTime? EndCa { get; set; }
        public DateTime? ConfCamp { get; set; }
        public DateTime? DataArrivoFilo { get; set; }
        public int? CapiPrevisti { get; set; }
        public int? NrMach { get; set; }
        public DateTime? DataEntrataInProd { get; set; }
        public DateTime? DataArrivoSchema { get; set; }
        public DateTime? DataInizioSvilTgBase { get; set; }
        public DateTime? DataFineSvilTgBase { get; set; }
        public DateTime? Gg1 { get; set; }
        public DateTime? DataArrivoSchede { get; set; }
        public DateTime? DataArrivoDisco { get; set; }
        public string ProgrammerPP { get; set; }
        public DateTime? StartPP { get; set; }
        public DateTime? EndPP { get; set; }
        public DateTime? Gg2 { get; set; }
        public string Ok { get; set; }
        public DateTime? Gg3 { get; set; }
        public string ProgrammerSv { get; set; }
        public DateTime? StartSv { get; set; }
        public DateTime? EndSv { get; set; }
        public DateTime? DataInizioProd { get; set; }
        public DateTime? Gg4 { get; set; }
        public int? TotGg { get; set; }
        public DateTime? Finish { get; set; }
        public bool? Finished { get; set; }
        public double? Weights { get; set; }
        public int? Time { get; set; }
        public bool? ConfPp { get; set; }
        public string Note { get; set; }
    }
}
