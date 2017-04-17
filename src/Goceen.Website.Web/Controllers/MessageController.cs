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
    public class MessageController : BaseController
    {
        public MessageController(ISysConfigService config, ICacheManager cache, ISysUserService user,
             ISysMessageService message)
        {
            _cacheManager = cache;
            _sysuserSerice = user;
            _sysmessageService = message;
            _sysconfigService = config;
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "留言管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = _sysmessageService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Name,
                entity.Id,
                entity.OrderNo,
                entity.Content,
                entity.IsReview,
                entity.Reply,
                entity.CreateDate,
                entity.UpdateDate
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            //var channellist = _syschannelService.LoadAllEnable();

            //var list = channellist.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            //ViewBag.ChannelList = list.ToList<SelectListItem>();
            //ViewBag.ChannelList = list.Select(x => new SelectListItem { Text=x.Name,Value=x.Id.ToString() });
            SysMessage entity = null;
            ViewBag.Title = "添加留言";
            if (id.HasValue)
            {
                entity = _sysmessageService.Get(id.Value);
                ViewBag.Title = "修改留言";
            }
            entity = entity ?? new SysMessage
            {
                Name = string.Empty,

            };
            //ViewBag.CategoryType = ExSelectListItem.ToSelectListItem(typeof(CategoryType));
            return View(entity);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(SysMessage entity)
        {
            var result = new CommonResult { Success = false, Message = "添加或修改失败，请联系管理员" };
            if (ModelState.IsValid)
            {
                if (0 == entity.Id)
                {
                    if (!base._sysmessageService.IsExist(entity.Id))
                    {
                        entity.CreateDate = DateTime.Now;
                        base._sysmessageService.Save(entity);
                        result.Success = true;
                        result.Message = "添加成功";
                    }
                    else
                    {
                        result.Message = "添加失败";
                    }
                }
                else
                {
                    var model = base._sysmessageService.Get(entity.Id);
                    model.Name = entity.Name;
                    model.OrderNo = entity.OrderNo;
                    model.Content = entity.Content;
                    model.IsReview = entity.IsReview;
                    model.Reply = entity.Reply;
                    model.CreateDate = entity.CreateDate;
                    model.UpdateDate = DateTime.Now;
                    base._sysmessageService.Update(model);
                }

                return Json(new CommonResult { Success = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("AddOrEdit", entity);
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            _sysmessageService.Delete(idList);
            return Json(new CommonResult { Success = true, Message = "删除成功" });
        }
    }
}