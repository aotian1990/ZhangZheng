using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goceen.Website.Services;
using Goceen.Website.Domain;
using Leon.Core.Cache;

namespace Goceen.Website.Web.Controllers
{
    public class ChannelController : BaseController
    {
        public ChannelController(ISysConfigService config, ICacheManager cache, ISysUserService user,
            ISysChannelService channel)
        {
            _cacheManager = cache;
            _sysconfigService = config;
            _sysuserSerice = user;
            _syschannelService = channel;
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "频道管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = base._syschannelService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Name,
                entity.Id,
                entity.OrderNo,
                entity.Url,
                entity.IsEnabled
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(SysChannel entity)
        {
            var result = new CommonResult{ Success = false , Message ="添加或修改失败，请联系管理员"};
            
            if (ModelState.IsValid)
            {
                if (0 == entity.Id)
                {
                    if (!base._syschannelService.IsExist(entity.Name))
                    {
                        base._syschannelService.Save(entity);
                        result.Success = true;
                        result.Message = "添加成功";
                    }
                    else
                    {
                        result.Message = "频道名称已存在，请重新输入";
                    }
                }
                else
                {
                    var model = base._syschannelService.Get(entity.Id);
                    model.Name = entity.Name;
                    model.OrderNo = entity.OrderNo;
                    model.IsEnabled = entity.IsEnabled;
                    model.Keyword = entity.Keyword;
                    model.Url = entity.Url;
                    model.Description = entity.Description;
                    base._syschannelService.Update(model);
                    result.Success = true;
                    result.Message = "修改成功";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return View("AddOrEdit", entity);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            SysChannel entity = null;
            ViewBag.Title = "添加频道";
            if (id.HasValue)
            {
                entity = _syschannelService.Get(id.Value);
                ViewBag.Title = "修改频道";
            }
            entity = entity ?? new SysChannel
            {
                Name = string.Empty
            };
            return View(entity);
        }

        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            _syschannelService.Delete(idList);
            return Json(new CommonResult { Success = true, Message = "删除成功" });
        }

        // GET: Channel
        public ActionResult Index()
        {
            return View();
        }
    }
}