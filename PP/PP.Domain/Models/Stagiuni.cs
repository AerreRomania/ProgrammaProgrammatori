using System;
using System.Collections.Generic;
using System.Text;

namespace PP.Domain.Models
{
    public class Stagiuni: DomainObject
    {
        public int Id { get; set; }
        public string Stagiune { get; set; }
    }
}
