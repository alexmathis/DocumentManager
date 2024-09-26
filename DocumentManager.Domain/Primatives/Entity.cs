using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Primatives
{
    public abstract class Entity
    {
        protected Entity(int id) => Id = id;

        protected Entity()
        {
        }

        public int Id { get; protected set; }
    }
}
