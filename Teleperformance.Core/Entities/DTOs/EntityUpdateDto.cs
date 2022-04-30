using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Entities.DTOs
{
    public class EntityUpdateDto : IDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
