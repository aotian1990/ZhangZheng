using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Authority
{
    public class Permission : TreeNode
    {
        public virtual string PermissionName { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public virtual string ActionName { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public virtual string ControllerName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual ISet<Role> Roles { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public virtual ISet<Button> Buttons { get; set; }
    }
}
