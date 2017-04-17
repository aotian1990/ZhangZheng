using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core
{
    [Serializable]
    public abstract class EntityBase
    {

        //public EntityBase()
        //{
        //    this.CreateTime = DateTime.Now;
        //}

        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Version { get; set; }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (this == other) return true;

            EntityBase entity = other as EntityBase;
            if (entity == null) return false;

            if (!this.GetType().Equals(other.GetType())) return false;

            if (!Id.Equals(entity.Id)) return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this.GetType().GetHashCode();
                result = 29 * result + Id.GetHashCode();
                return result;
            }
        }
    }
}
