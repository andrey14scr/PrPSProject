using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrPS.DAL.Core.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public Guid OldSubdivisionId { get; set; }
        public virtual Subdivision OldSubdivision { get; set; }

        public Guid NewSubdivisionId { get; set; }
        public virtual Subdivision NewSubdivision { get; set; }

        public Guid DocActionId { get; set; }
        public virtual DocAction DocAction { get; set; }
    }
}
