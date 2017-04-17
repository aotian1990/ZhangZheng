﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Leon.Core;
using Leon.Core.Configuration;

namespace Goceen.Website.Web.Framework.Themes
{
    public partial class ThemeProvider : IThemeProvider
    {
        private readonly IList<ThemeConfiguration> _themeConfigurations = new List<ThemeConfiguration>();

        private readonly string _basePath = string.Empty;

        public ThemeProvider(NopConfig nopConfig, IWebHelper webHelper)
        {
            _basePath = webHelper.MapPath(nopConfig.ThemeBasePath);
            LoadConfigurations();
        }

        public ThemeConfiguration GetThemeConfiguration(string themeName)
        {
            return _themeConfigurations
                .SingleOrDefault(x => x.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        public IList<ThemeConfiguration> GetThemeConfigurations()
        {
            return _themeConfigurations;
        }

        public bool ThemeConfigurationExists(string themeName)
        {
            return GetThemeConfigurations().Any(configuration => configuration.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        private void LoadConfigurations()
        {
            //TODO:Use IFileStorage?
            foreach (string themeName in Directory.GetDirectories(_basePath))
            {
                var configuration = CreateThemeConfiguration(themeName);
                if (configuration != null)
                {
                    _themeConfigurations.Add(configuration);
                }
            }
        }

        private ThemeConfiguration CreateThemeConfiguration(string themePath)
        {
            var themeDirectory = new DirectoryInfo(themePath);
            var themeConfigFile = new FileInfo(Path.Combine(themeDirectory.FullName, "theme.config"));
            if (themeConfigFile.Exists)
            {
                var doc = new XmlDocument();
                doc.Load(themeConfigFile.FullName);
                return new ThemeConfiguration(themeDirectory.Name, themeDirectory.FullName, doc);
            }

            return null;
        }

    }
}