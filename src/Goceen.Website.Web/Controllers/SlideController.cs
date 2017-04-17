using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goceen.Website.Services;
using Goceen.Website.Domain;
using Leon.Core.Cache;
using Goceen.Website.Web.Extensions;

namespace Goceen.Website.Web.Controllers
{
    public class SlideController :  BaseController
    {

        public SlideController(ISysSlideService slide)
        {
            base._sysslideService = slide;
        }

        [Authorize]
        // GET: Slide
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = _sysslideService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Id,
                entity.ImgTitle,
                entity.ImgUrl,
                entity.LinkUrl,
                entity.OrderNo
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            SysSlide entity = null;

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                entity = _sysslideService.Get(Convert.ToInt32(id));
            }
            entity = entity ?? new SysSlide
            {
                ImgUrl = string.Empty,
                ImgTitle = string.Empty,
                LinkUrl = string.Empty,
            };
            ViewBag.entity = entity;
            return View(entity);
        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(SysSlide entity)
        {

            if (entity.Id == 0)
            {
                base._sysslideService.Save(entity);
            }
            else
            {
                var model = base._sysslideService.Get(entity.Id);
                model.ImgTitle = entity.ImgTitle;
                model.ImgUrl = entity.ImgUrl;
                model.LinkUrl = entity.LinkUrl;
                model.OrderNo = entity.OrderNo;
                base._sysslideService.Update(model);
            }
            return Json(new CommonResult { Success = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            base._sysslideService.Delete(idList);
            return Json(new CommonResult{ Success = true, Message = "删除成功" });
        }
    }
}