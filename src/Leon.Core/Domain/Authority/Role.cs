using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Authority
{
    public class Role : EntityBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        ///public virtual ISet<Permission> Permission { get; set; }
        
        /// <summary>
        /// 角色对应的用户
        /// </summary>
        public virtual ISet<User> Users { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public virtual ISet<Module> Modules { get; set; }
    }
}
