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
    public class BaseController : Controller
    {

        protected SysConfig _sysConfig { get; set; }
        protected ISysConfigService _sysconfigService { get; set; }

        protected ICacheManager _cacheManager { get; set; }

        protected ISysUserService _sysuserSerice { get; set; }

        protected ISysArticleService _sysarticleService { get; set; }

        protected ISysCategoryService _syscategoryService { get; set; }

        protected ISysChannelService _syschannelService { get; set; }

        protected ISysSlideService _sysslideService { get; set; }

        protected ISysMessageService _sysmessageService { get; set; }

        protected ISysNewsService _sysnewsService { get; set; }

        protected ISysIconService _sysiconService { get; set; }

        protected ISysCarouselService _syscarouselService { get; set; }

        public BaseController()
        {            
            //ViewBag.sysConfig = _sysconfigService.LoadConfig();

            //ViewBag.slideList = _sysslideService.LoadAllEnable();
        }

      
    }
}