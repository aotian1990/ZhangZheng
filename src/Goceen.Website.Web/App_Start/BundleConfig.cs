using System.Web;
using System.Web.Optimization;

namespace Goceen.Website.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/jquery.easyui-{version}.js",
                        "~/Scripts/jquery.easyui.validatebox.fixed.js",
                        "~/Scripts/jquery.easyui.validatebox.extend.js",                        
                        "~/Scripts/locale/easyui-lang-zh_CN.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/metinfo").Include(
                      "~/Scripts/metinfo_ui.js"));

            bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/Logon.css"));

            bundles.Add(new StyleBundle("~/Content/themes/bootstrap/css").Include(
                        "~/Content/themes/bootstrap/easyui.css",
                        "~/Content/themes/icon.css"));

            bundles.Add(new StyleBundle("~/Content/themes/iconcss").Include("~/Content/themes/icon.css"));

            bundles.Add(new StyleBundle("~/Content/themes/default/css").Include(
                        "~/Content/themes/default/easyui.css"));
 
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Themes/Default/Content/css").Include(
                "~/Themes/Default/Content/metinfo.css",
                "~/Themes/Default/Content/metinfo_ui.css"));

            //BundleTable.EnableOptimizations = true;
            
        }
    }
}
