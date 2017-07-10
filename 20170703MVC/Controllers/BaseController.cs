using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20170703MVC.Controllers
{
    //Abstract類別，重要特性，不能被new，一定必須要繼承，因此建議把BaseController也宣告為抽象類別，避免它人直接執行 /Base/Index
    public abstract class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            base.Redirect("/").ExecuteResult(this.ControllerContext); //找不到路由Action直接轉到原本的預設路由
            //base.HandleUnknownAction(actionName);
        }
    }
}