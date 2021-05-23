using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrPS.DAL.Core.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public byte Corpus { get; set; }
        public byte Floor { get; set; }
        public float Area { get; set; }

        public Guid SubdivisionId { get; set; }
        public virtual Subdivision Subdivision { get; set; }

        public Guid RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }

        public Guid DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}
