using System;

namespace Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            ID = Guid.NewGuid();    
        }

        public Guid ID { get; set; }
    }
}