using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Goceen.Website.Services;
using Leon.Core.Cache;
using Goceen.Website.Domain;
using Webdiyer.WebControls.Mvc;

namespace Goceen.Website.Web.Controllers
{
    public class HomeController :  BaseController
    {
        public HomeController(ISysConfigService config, ICacheManager cache,ISysNewsService news,ISysIconService icon,ISysCarouselService carousel,ISysSlideService slide,ISysArticleService article)
        {
            _cacheManager = cache;
            _sysconfigService = config;
            _sysnewsService = news;
            _sysiconService = icon;
            _syscarouselService = carousel;
            _sysslideService = slide;
            _sysarticleService = article;
        }
        public ActionResult Index()
        {
            List<SysCarousel> list = _syscarouselService.LoadAllEnable().ToList();
            return View(list);

        }
        public ActionResult PicList(int catId,int? p)
        {
            int pageIndex = p.HasValue? p.Value : 1;
            int pageSize = 15;
            PagedList<SysArticle> list =  _sysarticleService.LoadAllEnable(catId).ToList().ToPagedList(pageIndex,pageSize);
            return View(list);
        }
        public ActionResult NewsList(int? p)
        {
            int pageIndex = p.HasValue ? p.Value : 1;
            int pageSize = 15;
            PagedList<SysArticle> list = _sysarticleService.LoadAllEnable(4).ToList().ToPagedList(pageIndex, pageSize);
            return View(list);
        }

        public ActionResult News(int id)
        {
            SysArticle model =  _sysarticleService.Get(id);
            return View(model);
        }
        public ActionResult Test()
        {
            List<SysArticle> list = _sysarticleService.LoadAll().ToList();
            return View(list);
        }

        public ActionResult HotArticle(int catId)
        {
            List<SysArticle> list = _sysarticleService.LoadAllEnable(catId).ToList();
            return View(list);
        }
        public ActionResult HotSpots()
        {
            List<SysArticle> list = _sysarticleService.LoadAllEnable(2).ToList();
            return View(list);
        }
        public ActionResult HotInfo()
        {
            List<SysArticle> list = _sysarticleService.LoadAllEnable(2).ToList();
            return View(list);
        }
    }
}