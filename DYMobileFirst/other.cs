using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace DYMobileFirst
{
    public class other
    {
        public static IHtmlString RenderScriptsIe(string ie, params string[] paths)
        {
            var tag = string.Format("<!--[if {0}]>{1}<![endif]-->", ie, Scripts.Render(paths));
            return new MvcHtmlString(tag);
        }

        public static IHtmlString RenderStylesIe(string ie, params string[] paths)
        {
            var tag = string.Format("<!--[if {0}]>{1}<![endif]-->", ie, Styles.Render(paths));
            return new MvcHtmlString(tag);
        }
    }
}