using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain
{
    /// <summary>
    /// 树型结构节点实体
    /// </summary>
    public abstract class TreeNode : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 标识树形结构的编码
        /// </summary>
        public virtual string TreeCode { get; set; }

        /// <summary>
        /// 是否叶节点
        /// </summary>
        public virtual bool Leaf { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public virtual string ParentId { get; set; }


        /// <summary>
        /// 节点深度
        /// </summary>
        public virtual int Level { get; set; }

    }
}
