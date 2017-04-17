using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Goceen.Website.Domain
{
    public class SysConfig
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        [Display(Name = "网站名称")]
        public virtual string WebName { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        [Display(Name = "网站域名")]
        public virtual string WebUrl { get; set; }

        /// <summary>
        /// 网站LOGO
        /// </summary>
        [Display(Name = "网站LOGO")]
        public virtual string WebLogo { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Display(Name = "公司名称")]
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        [Display(Name = "通讯地址")]
        public virtual string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 网站备案号
        /// </summary>
        [Display(Name = "网站备案号")]
        public virtual string IcpCode { get; set; }

        /// <summary>
        /// 网站首页标题
        /// </summary>
        [Display(Name = "网站首页标题")]
        public virtual string WebTitle { get; set; }

        /// <summary>
        /// 页面关健词
        /// </summary>
        [Display(Name = "页面关健词")]
        public virtual string WebKeyword { get; set; }

        /// <summary>
        /// 页面描述
        /// </summary>
        [Display(Name = "页面描述")]
        public virtual string WebDescription { get; set; }

        /// <summary>
        /// 版权信息
        /// </summary>
        [Display(Name = "版权信息")]
        public virtual string WebCopyright { get; set; }

        /// <summary>
        /// 是否关闭网站
        /// </summary>
        [Display(Name = "是否关闭网站")]
        public virtual bool WebStatus { get; set; }

        /// <summary>
        /// 关闭原因
        /// </summary>
        [Display(Name = "关闭原因")]
        public virtual string WebCloseReason { get; set; }

        /// <summary>
        /// 网站统计代码
        /// </summary>
        [Display(Name = "网站统计代码")]
        public virtual string WebCountCode { get; set; }

        /// <summary>
        /// 网站皮肤
        /// </summary>
        [Display(Name = "网站皮肤")]
        public virtual string WorkingThemeName { get; set; }
    }
}
