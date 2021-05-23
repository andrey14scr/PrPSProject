using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrPS.DAL.Core.Entities
{
    public class Subdivision
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string DatPad { get; set; }
        public string RodPad { get; set; }
    }
}
