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
        public double Gennaio { get; set; }
        public double Febbraio { get; set; }
        public double Marzo { get; set; }
        public double Aprile { get; set; }
        public double Maggio { get; set; }
        public double Giugno { get; set; }
        public double Luglio { get; set; }
        public double Agosto { get; set; }
        public double Settembre { get; set; }
        public double Ottombre { get; set; }
        public double Novembre { get; set; }
        public double Dicembre { get; set; }
        public double Total { get; set; }
    }
}
