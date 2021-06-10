using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class ProgrammerProgress : DomainObject
    {
        public new int Id { get => ProgrammerProgressID; set => ProgrammerProgressID = value; }
        public int ProgrammerProgressID { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime? EndWork { get; set; }
        public bool Repair { get; set; }
        public bool Assistance { get; set; }
        public string ArticleTitle { get; set; }
        public int WorkLocationID { get; set; }

        [ForeignKey("Progress")]
        public int ProgrammerTaskID { get; set; }

        public ProgrammerTask Progress { get; set; }
        [NotMapped]
        public string Duration { get => "Time worked: "+ (EndWork.Value - StartWork).Days +" days, "+ (EndWork.Value - StartWork).Hours+" hours and " + (EndWork.Value - StartWork).Minutes+" minutes"; }
    }
}