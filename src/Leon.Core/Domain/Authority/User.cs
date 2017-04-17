using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Authority
{
    public class User : EntityBase
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 用户配置
        /// </summary>
        public virtual string ConfigJSON { get; set; }

        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public virtual bool IsEnable { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual ISet<Role> Roles { get; set; }
    }
}
