using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Goceen.Website.Services;
using Goceen.Website.Domain;
using Leon.Core.Cache;

namespace Goceen.Website.Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(ISysConfigService config, ICacheManager cache,ISysUserService user,
            ISysChannelService channel,ISysCategoryService category)
        {
            _cacheManager = cache;
            _sysconfigService = config;
            _sysuserSerice = user;
            _syschannelService = channel;
            _syscategoryService = category;
           
        }
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.User = _sysuserSerice.Get(int.Parse(this.User.Identity.Name));
            var list = _syscategoryService.LoadAll().Where(w => (w.CategoryType == CategoryType.list && w.IsEnabled));
            ViewBag.CategoryList = list.ToList();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult SetWebConfig()
        {
            var entity = _sysconfigService.LoadConfig();
            return View(entity);
        }

        [Authorize]
        public ActionResult SaveConfig(SysConfig entity)
        {
            if (ModelState.IsValid)
            {
                _sysconfigService.SaveConfig(entity);
            }
            return RedirectToAction("SetWebConfig", entity);
        }

        public ActionResult CheckLogin(string account, string password, string code)
        {
            if (this.Session["ValidateCode"] == null || code != this.Session["ValidateCode"].ToString())
            {
                return Json(new { IsSuccess = false, Message = "验证码错误，请重新输入" });
            }

            var entity = _sysuserSerice.Get(account, password);
            if (entity == null)
            {
                return Json(new { IsSuccess = false, Message = "用户名或密码错误" });
            }

            if (!entity.IsEnabled)
            {
                return Json(new { IsSuccess = false, Message = "您的账号已被禁用，请联系管理员" });
            }
            FormsAuthentication.SetAuthCookie(entity.Id.ToString(), false);
            return Json(new { IsSuccess = true, Message = "登陆成功" });
        }

        public ActionResult ValidateCode()
        {
            var code = Framework.ValidateCode.CreateValidateNumber(4);
            this.Session["ValidateCode"] = code;
            return File(Framework.ValidateCode.CreateValidateGraphic(code), "image/jpeg");
        }

        [Authorize]
        public ActionResult ChangedPassword(string password, string oldPassword)
        {
            var entity = this._sysuserSerice.Get(int.Parse(this.User.Identity.Name));
            if (entity == null || this._sysuserSerice.Get(entity.Account, oldPassword) == null)
            {
                return Json(new { IsSuccess = false, Message = "密码错误" },
                    "text/x-json", JsonRequestBehavior.AllowGet);
            }

            this._sysuserSerice.Update(entity, password);
            return Json(new { IsSuccess = true, Message = "修改成功" },
                                "text/x-json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Admin/Login/");
        }
        
    }
}