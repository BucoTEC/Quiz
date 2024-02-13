using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Dal.Entities
{
    /// <summary>
    /// Base class for all entities in the system.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; } = null;

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.Now.ToUniversalTime();

    }
}
