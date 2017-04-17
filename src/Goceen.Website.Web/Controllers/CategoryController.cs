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
    public class CategoryController : BaseController
    {

        public CategoryController(ISysConfigService config, ICacheManager cache, ISysUserService user,
            ISysChannelService channel, ISysCategoryService category)
        {
            _cacheManager = cache;
            _sysuserSerice = user;
            _syscategoryService = category;
            _sysconfigService = config;
            _syschannelService = channel;
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "分类管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = _syscategoryService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Name,
                entity.Id,
                entity.OrderNo,
                entity.IsEnabled,
                entity.CategoryType,
                entity.Url,
                ChannelName = entity.Channel.Name,
                ChannelId = entity.Channel.Id
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var channellist = _syschannelService.LoadAllEnable();
            
            var list = channellist.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ChannelList = list.ToList<SelectListItem>();
            //ViewBag.ChannelList = list.Select(x => new SelectListItem { Text=x.Name,Value=x.Id.ToString() });
            SysCategory entity = null;
            ViewBag.Title = "添加分类";
            if (id.HasValue)
            {
                entity = _syscategoryService.Get(id.Value);
                ViewBag.Title = "修改分类";
            }
            entity = entity ?? new SysCategory
            {
                Name = string.Empty,
                
            };
            ViewBag.CategoryType = ExSelectListItem.ToSelectListItem(typeof(CategoryType));
            return View(entity);
        }

        [Authorize]        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(SysCategory entity,int? channelId)
        {
            if (ModelState.IsValid)
            {
                var forum = _syschannelService.Get(channelId.Value);
                if (forum == null)
                {
                    return Json(new CommonResult
                    {
                        Success = false,
                        Message = "未选择频道，保存失败！"
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
                if (entity.Id == 0)
                {
                    if (_syscategoryService.IsExist(entity.Name.Trim()))
                    {
                        return Json(new CommonResult
                        {
                            Success = false,
                            Message = "分类名称已经存在，请重新输入！"
                        }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    entity.Channel = forum;
                    base._syscategoryService.Save(entity);
                }
                else
                {
                    var model = base._syscategoryService.Get(entity.Id);
                    model.Name = entity.Name;
                    model.OrderNo = entity.OrderNo;
                    model.IsEnabled = entity.IsEnabled;
                    model.CategoryType = entity.CategoryType;
                    model.Content = entity.Content;
                    model.Url = entity.Url;
                    entity.Channel = forum;
                    base._syscategoryService.Update(model);
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
            _syscategoryService.Delete(idList);
            return Json(new CommonResult{ Success = true, Message = "删除成功" });
        }
    }
}