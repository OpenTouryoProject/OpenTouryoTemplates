//**********************************************************************************
//* Copyright (C) 2007,2015 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
//  
// 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：MyBaseMVController
//* クラス日本語名  ：ASP.NET MVC用 画面コード親クラス２（テンプレート）
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//* 
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/08/04  Supragyan        Added code for SessionTimeout to OnActionExecuting method.
//*  2015/08/31  Supragyan        Modified OnException method to display error message on Error screen
//*  2015/09/03  Supragyan        Modified ExceptionType,Session,RedireResult on OnException method 
//*  2015/10/27  Sai              Moved moved the code of SessionTimeout from OnActionExecuting
//*                               method to BaseMVController class.    
//**********************************************************************************

// System
using System;

// System.Web
using System.Web;
using System.Web.Mvc;

// フレームワーク
using Touryo.Infrastructure.Framework.Exceptions;
using Touryo.Infrastructure.Framework.Util;

// 部品
using Touryo.Infrastructure.Public.Log;
using Touryo.Infrastructure.Public.Util;

namespace Touryo.Infrastructure.Business.Presentation
{
    /// <summary>画面コード親クラス２</summary>
    /// <remarks>（オーバーライドして）自由に利用できる。</remarks>
    public class MyBaseMVController : BaseMVController
    {
        /// <summary>
        /// 応答にビューを表示する ViewResult オブジェクトを作成します。
        /// Controller.View メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.view.aspx
        /// </summary>
        /// <param name="view">ビュー</param>
        /// <param name="model">モデル</param>
        /// <returns>ViewResult オブジェクト</returns>
        protected override ViewResult View(IView view, object model)
        {
            LogIF.InfoLog("ACCESS", "View 前");
            return base.View(view, model);
        }

        /// <summary>
        /// 応答にビューを表示する ViewResult オブジェクトを作成します。
        /// Controller.View メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.view.aspx
        /// </summary>
        /// <param name="viewName">ビュー名</param>
        /// <param name="masterName">マスター ページ名</param>
        /// <param name="model">モデル</param>
        /// <returns>ViewResult オブジェクト</returns>
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            LogIF.InfoLog("ACCESS", "View 前");
            return base.View(viewName, masterName, model);
        }

        /// <summary>
        /// アクション メソッドの呼び出し前に呼び出されます。  
        /// Controller.OnActionExecuting メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.onactionexecuting.aspx
        /// </summary>
        /// <param name="filterContext">
        /// 型: System.Web.Mvc.ActionExecutingContext
        /// 現在の要求およびアクションに関する情報。
        /// </param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string strLogMessage = "OnActionExecuting 前" + " - " + filterContext.Controller.ToString() + " - "
                               + filterContext.ActionDescriptor.ActionName;

            LogIF.InfoLog("ACCESS", strLogMessage);

            // アクションの実行
            base.OnActionExecuting(filterContext);
            LogIF.InfoLog("ACCESS", "OnActionExecuting 後");
        }

        /// <summary>
        /// アクション メソッドの呼び出し後に呼び出されます。
        /// Controller.OnActionExecuted メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.onactionexecuted.aspx
        /// </summary>
        /// <param name="filterContext">
        /// 型: System.Web.Mvc.ActionExecutedContext
        /// 現在の要求およびアクションに関する情報。
        /// </param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string strLogMessage = "OnActionExecuted 前" + " - " + filterContext.Controller.ToString() + " - "
                                   + filterContext.ActionDescriptor.ActionName;
            LogIF.InfoLog("ACCESS", strLogMessage);

            base.OnActionExecuted(filterContext);
            LogIF.InfoLog("ACCESS", "OnActionExecuted 後");
        }

        /// <summary>アクションでハンドルされない例外が発生したときに呼び出されます。</summary>
        /// <param name="filterContext">
        /// 型: System.Web.Mvc.ResultExecutingContext
        /// 現在の要求およびアクション結果に関する情報。
        /// </param>
        /// <remarks>
        /// web.config に customErrors mode="on" を追記（無い場合は、OnExceptionメソッドが動かない 
        /// </remarks>
        protected override void OnException(ExceptionContext filterContext)
        {
            string strLogMessage = "OnException 前" + " - " + filterContext.Controller.ToString() + " - "
                                   + filterContext.Exception.Message;
            LogIF.ErrorLog("ACCESS", strLogMessage);

            #region 例外型を判別しエラーメッセージIDを取得

            // エラーメッセージ
            string err_msg;

            // エラー情報をセッションから取得
            string err_info;

            // エラー画面へのパスを取得 --- チェック不要（ベースクラスでチェック済み）
            string errorScreenPath = GetConfigParameter.GetConfigValue(FxLiteral.ERROR_SCREEN_PATH);

            //// Store the exception information for a Session.
            Session["ExceptionInformation"] = filterContext.Exception.ToString();

            // エラーのタイプ

            // エラーメッセージＩＤ
            string errMsgId = "";

            // Check exception type 
            if (filterContext.Exception is BusinessSystemException)
            {
                // システム例外
                BusinessSystemException bsEx = (BusinessSystemException)filterContext.Exception;
                errMsgId = bsEx.messageID;
            }
            else if (filterContext.Exception is FrameworkException)
            {
                // フレームワーク例外
                FrameworkException fxEx = (FrameworkException)filterContext.Exception;
                errMsgId = fxEx.messageID;
            }
            else
            {
                // それ以外の例外
                errMsgId = "－";
            }

            #endregion

            #region エラー時に、セッションを開放しないで、業務を続行可能にする処理を追加。

            // 不正操作エラー or 画面遷移制御チェック エラー
            if (errMsgId == "IllegalOperationCheckError"
                || errMsgId == "ScreenControlCheckError")
            {
                // セッションをクリアしない
                Session[FxHttpContextIndex.SESSION_ABANDON_FLAG] = false;
            }
            else
            {
                // セッションをクリアする
                Session[FxHttpContextIndex.SESSION_ABANDON_FLAG] = true;
            }

            #endregion

            #region エラー画面に表示するエラー情報を作成

            err_msg = System.Environment.NewLine +
                "エラーメッセージＩＤ: " + errMsgId + System.Environment.NewLine +
                "エラーメッセージ: " + filterContext.Exception.Message.ToString();

            err_info = System.Environment.NewLine +
                "対象URL: " + Request.Url.ToString() + System.Environment.NewLine +
                "スタックトレース:" + filterContext.Exception.StackTrace.ToString() + System.Environment.NewLine +
                "Exception.ToString():" + filterContext.ToString();

            // Add exception information to Session。
            Session[FxHttpContextIndex.SYSTEM_EXCEPTION_MESSAGE] = err_msg;
            Session[FxHttpContextIndex.SYSTEM_EXCEPTION_INFORMATION] = err_info;

            #endregion

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();

            // エラー画面へ画面遷移

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JavaScriptResult() { Script = "location.href = '" + errorScreenPath + "'" };
            }
            else
            {
                filterContext.Result = new RedirectResult(errorScreenPath);
            }

            base.OnException(filterContext);
            LogIF.InfoLog("ACCESS", "OnException 後");
        }

        /// <summary>
        /// アクション メソッドによって返されたアクション結果が実行される前に呼び出されます。  
        /// Controller.OnResultExecuting メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.onresultexecuting.aspx
        /// </summary>
        /// <param name="filterContext">
        /// 型: System.Web.Mvc.ResultExecutingContext
        /// 現在の要求およびアクション結果に関する情報。
        /// </param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string strLogMessage = "OnResultExecuting 前" + " - " + filterContext.Controller.ToString();
            LogIF.InfoLog("ACCESS", strLogMessage);

            base.OnResultExecuting(filterContext);
            LogIF.InfoLog("ACCESS", "OnResultExecuting 後");
        }

        /// <summary>
        /// アクション メソッドによって返されたアクション結果が実行された後に呼び出されます。 
        /// Controller.OnResultExecuted メソッド (System.Web.Mvc)
        /// http://msdn.microsoft.com/ja-jp/library/system.web.mvc.controller.onresultexecuted.aspx
        /// </summary>
        /// <param name="filterContext">
        /// 型: System.Web.Mvc.ResultExecutingContext
        /// 現在の要求およびアクション結果に関する情報。
        /// </param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string strLogMessage = "OnResultExecuted 前" + " - " + filterContext.Controller.ToString();
            LogIF.InfoLog("ACCESS", strLogMessage);

            base.OnResultExecuted(filterContext);
            LogIF.InfoLog("ACCESS", "OnResultExecuted 後");
        }
    }
}
