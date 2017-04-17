using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goceen.Website.Services;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(ISysConfigService config,ISysUserService user)
        {
            _sysconfigService = config;
            _sysuserSerice = user;           
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "系统用户管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = base._sysuserSerice.LoadAllByPage(out total, page, rows, order, sort);
            var result = new { total = total, rows = list };
            return Json(result);
        }

        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            base._sysuserSerice.Delete(idList.Cast<int>().ToList());
            return Json(new CommonResult{ Success = true, Message = "删除成功" });
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            SysUser entity = null;
            if (id.HasValue)
            {
                entity = base._sysuserSerice.Get(id.Value);
            }
            entity = entity ?? new SysUser
            {
                Account = string.Empty,
                Name = string.Empty,
            };
            return View(entity);
        }

        [Authorize]
        public ActionResult Save(SysUser entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.Id == 0)
                {
                    if (_sysuserSerice.IsExist(entity.Account))
                    {
                        return Json(new CommonResult { Success = false, Message = "帐号已经存在，请重新输入" }, "text/html", JsonRequestBehavior.AllowGet); 
                    }
                    entity.CreateTime = DateTime.Now;
                    _sysuserSerice.Save(entity);

                }
                else
                {
                    var user = base._sysuserSerice.Get(entity.Id);
                    user.Name = entity.Name;
                    user.IsEnabled = entity.IsEnabled;
                    if (!string.IsNullOrEmpty(entity.Password))
                    {
                        base._sysuserSerice.Update(user, entity.Password);
                    }
                    else
                    {
                        base._sysuserSerice.Update(user);
                    }
                }
                return Json(new CommonResult { Success = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("AddOrEdit", entity);
        }


    }
}