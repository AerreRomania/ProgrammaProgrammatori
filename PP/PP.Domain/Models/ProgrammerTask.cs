using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.Domain.Models
{
    public class ProgrammerTask : DomainObject
    {
        public int Id { get => ProgrammerTaskID; set => ProgrammerTaskID = value; }
        public int ProgrammerTaskID { get; set; }
        public DateTime StartTask { get; set; }
        public DateTime EndTask { get; set; }
        public string Note { get; set; }
        public bool TaskCompleted { get; set; }
        public Angajati Programmer { get; set; }
        [ForeignKey("Programmer")]
        public int ProgrammerID { get; set; }
        [ForeignKey("Article")]
        public int ArticleID { get; set; }
        public Articole Article { get; set; }
        public int JobTypeID { get; set; }
        public ICollection<ProgrammerProgress> ProgrammerProgress { get; set; } = new List<ProgrammerProgress>();
        public string ArticleTitle { get; set; }

    }
}
