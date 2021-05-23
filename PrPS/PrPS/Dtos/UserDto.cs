using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrPS.DAL.Core.Entities;

namespace PrPS.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
