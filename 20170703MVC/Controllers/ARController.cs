﻿using _20170703MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20170703MVC.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView("Index");
        }

        public ActionResult Content1()
        {
            return Content("測試Big5", "text/plain", System.Text.Encoding.GetEncoding("big5")); //上面的寫法等價底下的Content2，但編碼不同
        }

        public string Content2()
        {
            return "測試Unicode";
        }

        //回傳訊息錯誤方式
        public string ContentAlert1()
        {
            return "<script>alert('找不到網頁');history.back(-1);</script>";
        }

        //回傳訊息正確方式
        public ActionResult ContentAlert2()
        {
            //關注點方離，把View該處理的東西交給View自行處理。
            //若View為共用的，就建立在共用Shared內
            return View("GoBack");
        }

        public ActionResult File1()
        {
            //外部圖片超連結
            return File(Server.MapPath("~/Content/my.png"), "image/png");
        }

        public ActionResult File2()
        {
            //下載檔案模式
            return File(Server.MapPath("~/Content/my.png"), "image/png", "我的logo.png");
        }

        public ActionResult Json1()
        {
            ProductRepository reop = RepositoryHelper.GetProductRepository();
            reop.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false; //關閉導覽屬性，避免Loop的問題
            return Json(reop.All().Take(5), JsonRequestBehavior.AllowGet); //AllowGet 
        }

        public ActionResult Json2()
        {
            ProductRepository reop = RepositoryHelper.GetProductRepository();
            reop.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false; //關閉導覽屬性，避免Loop的問題
            return Json(reop.All().Take(5));  //安全性議題，關閉AllowGet，僅能透過Post來取得資料
        }
    }
}