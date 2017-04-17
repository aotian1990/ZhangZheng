using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Goceen.Website.Services;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Framework.Themes
{
    public partial class ThemeContext : IThemeContext
    {
        private readonly IThemeProvider _themeProvider;

        private bool _themeIsCached;
        private string _cachedThemeName;
        private ISysConfigService _configService;

        public ThemeContext(IThemeProvider themeProvider,ISysConfigService configService)
        {
            this._themeProvider = themeProvider;
            this._configService = configService;
        }

        public string WorkingThemeName
        {
            get
            {
                if (_themeIsCached)
                    return _cachedThemeName;
                string theme = _configService.LoadConfig().WorkingThemeName;
                if (!_themeProvider.ThemeConfigurationExists(theme))
                {
                    var themeInstance = _themeProvider.GetThemeConfigurations()
                        .FirstOrDefault();
                    if (themeInstance == null)
                        throw new Exception("No theme could be loaded");
                    theme = themeInstance.ThemeName;
                }

                //cache theme
                this._cachedThemeName = theme;
                this._themeIsCached = true;
                return theme;
            }
            set
            {
                var config = _configService.LoadConfig();
                config.WorkingThemeName = value;
                _configService.SaveConfig(config);
                this._themeIsCached = false;
            }
        }
    }
}