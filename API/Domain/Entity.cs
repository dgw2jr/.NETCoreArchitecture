using System;

namespace Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            ID = Guid.NewGuid();    
        }

        public virtual Guid ID { get; set; }
    }
}