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
    public class ArticleController : BaseController
    {

        public ArticleController(ISysConfigService config, ICacheManager cache, ISysUserService user,
            ISysChannelService channel, ISysCategoryService category,ISysArticleService article)
        {
            _cacheManager = cache;
            _sysuserSerice = user;
            _syscategoryService = category;
            _sysconfigService = config;
            _syschannelService = channel;
            _sysarticleService = article;
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Title = "新闻管理";
            return View();
        }

        [Authorize]
        public ActionResult LoadAllByPage(int page, int rows, string order, string sort)
        {
            long total = 0;
            var list = base._sysarticleService.LoadAllByPage(out total, page, rows, order, sort).Select(entity => new
            {
                entity.Title,
                entity.Id,
                entity.CreateDate,
                entity.UpdateDate,
                entity.IsEnabled,
                entity.IsFirst,
                entity.ViewCount,
                entity.RecommendImg,
                CategoryId = entity.Category.Id,
                CategoryName = entity.Category.Name
            });

            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var list = base._syschannelService.LoadAllEnable();
            ViewBag.ChannelList = list.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList<SelectListItem>();
                        
            SysArticle entity = null;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                entity = base._sysarticleService.Get(Convert.ToInt32(id));
            }
            entity = entity ?? new SysArticle
            {
                Title = string.Empty,
            };
            return View(entity);
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetCategoryList(int channelId)
        {
            var categorylist = base._syscategoryService.LoadAllEnable(channelId);
            var list = categorylist.Where(m => m.CategoryType == CategoryType.list).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            list = list.ToList<SelectListItem>();
            if (list != null)
            {
                return Json(new {Success = true,Data = list}, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false, Message = "暂无分类，请先添加" }, "text/html", JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save(SysArticle entity, int categoryId)
        {

            var category = base._syscategoryService.Get(categoryId);
            if (category == null)
            {
                return Json(new CommonResult
                {
                    Success = false,
                    Message = "未选择分类，保存失败！"
                }, "text/html", JsonRequestBehavior.AllowGet);
            }

            if (entity.Id == 0)
            {
                if (_sysarticleService.IsExist(entity.Title))
                {
                    return Json(new CommonResult
                    {
                        Success = false,
                        Message = "标题已经存在，请重新输入！"
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.Category = category;
                base._sysarticleService.Save(entity);
            }
            else
            {
                var model = base._sysarticleService.Get(entity.Id);
                model.Title = entity.Title;
                model.UpdateDate = DateTime.Now;
                model.IsEnabled = entity.IsEnabled;
                model.Keyword = entity.Keyword;
                model.Content = entity.Content;
                model.Description = entity.Description;
                model.IsFirst = entity.IsFirst;
                model.RecommendImg = entity.RecommendImg;
                //model.ArticelType = entity.ArticelType;
                entity.Category = category;
                base._sysarticleService.Update(model);
            }

            return Json(new CommonResult{ Success = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(int id)
        {
            var entity = base._sysarticleService.Get(id);
            if (entity == null)
            {
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            var categoryList = base._syscategoryService.LoadAllEnable(entity.Category.Channel.Id);
            ViewBag.entity = entity;
            ViewBag.Category = base._syscategoryService.Get(entity.Category.Id);
            ViewBag.CateId = entity.Category.Id;
            ViewBag.ChannelInfo = entity.Category.Channel;
            ViewBag.CateList = categoryList;
            base._sysarticleService.ViewsAdd(id);
            return View();
        }

        [Authorize]
        public ActionResult Delete(IList<int> idList)
        {
            base._sysarticleService.Delete(idList.Cast<int>().ToList());
            return Json(new CommonResult { Success = true, Message = "删除成功" });
        }
    }
}