using _20170703MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20170703MVC.Controllers
{
    public class HomeController : BaseController 
    {
        public ActionResult Index()
        {
            return View();
        }

        //[ActionName("About.php")] //特例使用方式，建議少用，一律在路由定義比較好
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
            //return View("About"); //自訂路由回傳View
        }

        const string HelloModule = "Hello"; //自定義屬性路由
        [Route(HelloModule + "/{name?}")]
        public ActionResult About(string name)
        {
            ViewBag.Message = "Hello " + name;

            return View(); 
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                return Redirect("/");

                //此段驗證商業邏輯搬入IValidatableObject驗證
                //if(login.UserName == "123" && login.Password == "123")
                //{
                //    return Redirect("/");
                //} 
            }

            return View();
        }

    }
}