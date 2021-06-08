using System;
using System.Collections.Generic;
using System.Text;

namespace PP.Domain.Models
{
    public class AnalisiOperatore
    {
       
        public int Luna { get; set; }
        private int _jobTypeId;
        public int JobTypeID
        {
            get => _jobTypeId;
            set
            {
                _jobTypeId = value;
                JobTypeName = JobTypeID switch
                {
                    0 => "",
                    1 => "Prototipo",
                    2 => "Campionario",
                    3 => "Preproduzione",
                    4 => "Svillupo Taglie",
                    5 => "Schede Tehnice",
                    6 => "Riparazione/Ass.Rep.",
                    7 => "Prove tecniche",
                    8 => "Contracampione",
                    9 => "Vacanza",
                    _ => JobTypeName
                };
            }
        }
        public string JobTypeName { get; set; }
        public string Articol { get; set; }

    }
    public class AnalisiOperatoriColumns
    {
        public string JobTypeName { get; set; }
        public int Gennaio { get; set; }
        public int Febbraio { get; set; }
        public int Marzo { get; set; }
        public int Aprile { get; set; }
        public int Maggio { get; set; }
        public int Giugno { get; set; }
        public int Luglio { get; set; }
        public int Agosto { get; set; }
        public int Settembre { get; set; }
        public int Ottombre { get; set; }
        public int Novembre { get; set; }
        public int Dicembre { get; set; }
        public int Total { get; set; }
    }
}
