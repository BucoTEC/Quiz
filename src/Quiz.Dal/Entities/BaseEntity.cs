using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Dal.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = new();

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
