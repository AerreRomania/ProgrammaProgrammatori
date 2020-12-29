using PP.Domain.Models;
using System;

namespace PP.Domain.Columns
{
    public class ProgrammerGridColumns : DomainObject
    {
        public string Client { get; set; }
        public int ProgrammerTaskID { get; set; }
        public int ArticleID { get; set; }
        public string ArticleHeader { get; set; }
        public int ProgrammerID { get; set; }
        public string ProgrammerName { get; set; }
        public DateTime? EnterInProduction { get; set; }
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
        public string Finezza { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Repair { get; set; }
        public bool Assistance { get; set; }
    }
}