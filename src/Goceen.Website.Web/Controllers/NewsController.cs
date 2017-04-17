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
    public class NewsController : BaseController
    {
        public NewsController(ISysConfigService config, ICacheManager cache, ISysUserService user,
             ISysNewsService news)
        {
            _cacheManager = cache;
            _sysuserSerice = user;
            _sysnewsService = news;
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
            var list = _sysnewsService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Title,
                entity.Id,
                entity.OrderNo,
                entity.Url,
                entity.IsEnabled,
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
            SysNews entity = null;
            ViewBag.Title = "添加景区动态新闻";
            if (id.HasValue)
            {
                entity = _sysnewsService.Get(id.Value);
                ViewBag.Title = "修改景区动态新闻";
            }
            entity = entity ?? new SysNews
            {
                Title = string.Empty,

            };
            //ViewBag.CategoryType = ExSelectListItem.ToSelectListItem(typeof(CategoryType));
            return View(entity);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(SysNews entity)
        {
            var result = new CommonResult { Success = false, Message = "添加或修改失败，请联系管理员" };
            if (ModelState.IsValid)
            {
                if (0 == entity.Id)
                {
                    if (!base._sysnewsService.IsExist(entity.Id))
                    {
                        entity.CreateDate = DateTime.Now;
                        base._sysnewsService.Save(entity);
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
                    var model = base._sysnewsService.Get(entity.Id);
                    model.Title = entity.Title;
                    model.OrderNo = entity.OrderNo;
                    model.Url = entity.Url;
                    model.IsEnabled = entity.IsEnabled;
                    model.CreateDate = entity.CreateDate;
                    model.UpdateDate = DateTime.Now;
                    base._sysnewsService.Update(model);
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
            _sysnewsService.Delete(idList);
            return Json(new CommonResult { Success = true, Message = "删除成功" });
        }
    }
}