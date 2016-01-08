//**********************************************************************************
//* サンプル アプリ・コントローラ
//**********************************************************************************

//**********************************************************************************
//* クラス名        ：PingController
//* クラス日本語名  ：Html.BeginForm用サンプル アプリ・コントローラ
//*
//* 作成日時        ：－
//* 作成者          ：sas 生技
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/12/28  Sai               Added controller to prevent session timeout
//**********************************************************************************

//System
using System.Web.Mvc;

namespace MVC_Sample.Controllers
{
    /// <summary>
    /// Html.BeginForm用サンプル アプリ・コントローラ
    /// </summary>
    public class PingController : Controller
    {
        //
        // GET: /Ping/

        /// <summary>
        /// 画面の初期表示
        /// </summary>
        /// <returns>初期表示状態の画面 (ViewResult)</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns session id in Json format.
        /// </summary>
        /// <returns>session id in Json format.</returns>
        [HttpGet]
        public JsonResult Ping()
        {
            //Returns session id in Json format.
            return Json(Session.SessionID, JsonRequestBehavior.AllowGet);
        }
    }
}
