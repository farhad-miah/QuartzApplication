using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace QuartzWebApp.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(String LanguageAbbreviation)
        {
            if (LanguageAbbreviation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbreviation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbreviation);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbreviation;
            Response.Cookies.Add(cookie);

            return View("Index");
        }


    }
}