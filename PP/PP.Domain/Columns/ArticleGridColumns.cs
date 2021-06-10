using PP.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Columns
{
    public class ArticleGridColumns : DomainObject
    {
        [NotMapped]
        public int NrCrt { get; set; }
        public int? ArticleDeatilsID { get; set; }
        public int? Num { get; set; }
        public string Articolo { get; set; }
        public string Finezza { get; set; }
        public int? MachineNumber { get; set; }
        public int? CapiPrevisti { get; set; }
        public DateTime? DataInizioProd { get; set; }
        public string? Notes { get; set; }

        public DateTime? DataArrivoSchedePr { get; set; }
        public string ProgrammerPR { get; set; }
        public DateTime? StartPr { get; set; }
        public DateTime? EndPr { get; set; }
        public DateTime? DataConsegnaProt { get; set; }
        public DateTime? DataArrSchedaCa{ get; set; }

        public string ProgrammerCa { get; set; }
        public DateTime? StartCa { get; set; }
        public DateTime? EndCa { get; set; }
        public DateTime? DataConsegnaCa { get; set; }

        public DateTime? DataArrivoTagliaBase { get; set; }
        public DateTime? DataArrivoInzioTagliaBase { get; set; }
        public DateTime? DataArrivoFineTagliaBase { get; set; }

        public DateTime? DataArrivoSchedaCo { get; set; }
        public string ProgrammerCo { get; set; }
        public DateTime? StartCo { get; set; }
        public DateTime? EndCo { get; set; }
        public DateTime? DataConsegnaCo { get; set; }

        public DateTime? DataArrivoSchedaDisco { get; set; }
        public string ProgrammerPP { get; set; }
        public double? DiffGGProdData { get; set; }
        public double? DiffGGProgData { get; set; }

        public DateTime? StartPP { get; set; }
        public DateTime? EndPP { get; set; }
        public DateTime? DataConsegnaPP { get; set; }
        public DateTime? GG1 { get; set; }

        public DateTime? Ok { get; set; }

        public string? ProgrammerSvTg { get; set; }
        public DateTime? DataInizioSvilTgBase { get; set; }
        public DateTime? DataFineSvilTgBase { get; set; }

        public DateTime? GG2 { get; set; }
        public bool? Finish { get; set; }

        //#region removed
        //public DateTime? ConfCamp { get; set; }
        //public DateTime? DataArrivoFilo { get; set; }

        //public DateTime? DataEntrataInProd { get; set; }
        //public DateTime? DataArrivoSchema { get; set; }

        //public DateTime? Gg1 { get; set; }
        //public DateTime? DataArrivoSchede { get; set; }
        //public DateTime? DataArrivoDisco { get; set; }

        //public DateTime? Gg2 { get; set; }

        //public DateTime? Gg3 { get; set; }
        //public string ProgrammerSv { get; set; }
        //public DateTime? StartSv { get; set; }
        //public DateTime? EndSv { get; set; }

        //public DateTime? Gg4 { get; set; }
        //public int? TotGg { get; set; }
        //public bool? Finished { get; set; }
        //public double? Weights { get; set; }
        //public int? Time { get; set; }
        //public bool? ConfPp { get; set; }
        //public string Note { get; set; }
        //#endregion

    }
}