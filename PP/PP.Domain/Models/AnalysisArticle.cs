using System;
using System.Collections.Generic;
using System.Text;

namespace PP.Domain.Models
{
    public class AnalysisArticle : DomainObject
    {
       // public int Id { get; set; }
        public string Client { get; set; }
        public string Stagione { get; set; }
        public string Finezza { get; set; }
        public string Programmer { get; set; }

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
        public double ComputerHours {get;set;}
        public double RetilineaHours { get; set; }
        private double _total;
        public double Total { get=>_total; set {
                _total = ComputerHours + RetilineaHours;
            } }
    }
}
