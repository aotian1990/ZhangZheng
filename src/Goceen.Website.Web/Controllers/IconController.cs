using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goceen.Website.Services;
using Goceen.Website.Domain;
using Leon.Core.Cache;
using Goceen.Website.Web.Extensions;
using System.IO;

namespace Goceen.Website.Web.Controllers
{
    public class IconController : BaseController
    {

        public IconController(ISysConfigService config, ICacheManager cache, ISysUserService user,
            ISysIconService icon)
        {
            _cacheManager = cache;
            _sysuserSerice = user;
            _sysconfigService = config;
            _sysiconService = icon;

        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "图标管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = base._sysiconService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Title,
                entity.Id,
                entity.Img,
                entity.Url,
                entity.IsEnabled,
                entity.OrderNo
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var list = base._sysiconService.LoadAllEnable();    

            SysIcon entity = null;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                entity = base._sysiconService.Get(Convert.ToInt32(id));
            }
            entity = entity ?? new SysIcon
            {
                Title = string.Empty,
            };
            return View(entity);
        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(SysIcon entity)
        {

            

            if (entity.Id == 0)
            {
                if (_sysiconService.IsExist(entity.Id))
                {
                    return Json(new CommonResult
                    {
                        Success = false,
                        Message = "标题已经存在，请重新输入！"
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
                base._sysiconService.Save(entity);
            }
            else
            {
                var model = base._sysiconService.Get(entity.Id);
                model.Title = entity.Title;
                model.Img = entity.Img;
                model.OrderNo = entity.OrderNo;
                model.Url = entity.Url;
                model.IsEnabled = entity.IsEnabled;
                base._sysiconService.Update(model);
            }

            return Json(new CommonResult { Success = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Show(int id)
        //{
        //    var entity = base._sysiconService.Get(id);
        //    if (entity == null)
        //    {
        //        return Redirect(this.Request.UrlReferrer.ToString());
        //    }
        //    var categoryList = base._syscategoryService.LoadAllEnable(entity.Category.Channel.Id);
        //    ViewBag.entity = entity;
        //    ViewBag.Category = base._syscategoryService.Get(entity.Category.Id);
        //    ViewBag.CateId = entity.Category.Id;
        //    ViewBag.ChannelInfo = entity.Category.Channel;
        //    ViewBag.CateList = categoryList;
        //    base._sysiconService.ViewsAdd(id);
        //    return View();
        //}

        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            base._sysiconService.Delete(idList.Cast<int>().ToList());
            return Json(new CommonResult { Success = true, Message = "删除成功" });
        }


        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            string fileName = Path.GetFileName(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/" + fileName);
            string pic = "", error = "";
            try
            {
                upImg.SaveAs(filePhysicalPath);
                pic = "/upload/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }

        public ActionResult TestUpload()
        {
            return View();
        }
    }
}