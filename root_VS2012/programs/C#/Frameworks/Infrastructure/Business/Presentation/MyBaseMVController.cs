//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
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
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成（テンプレート）
//*  2015/08/04  Supragyan        Added code for SessionTimeout to OnActionExecuting method.
//**********************************************************************************

// System
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

// System.Web
using System.Web;
using System.Web.Mvc;

// 業務フレームワーク
using Touryo.Infrastructure.Business.Business;
using Touryo.Infrastructure.Business.Common;
using Touryo.Infrastructure.Business.Dao;
using Touryo.Infrastructure.Business.Exceptions;
using Touryo.Infrastructure.Business.Presentation;
using Touryo.Infrastructure.Business.Util;

// フレームワーク
using Touryo.Infrastructure.Framework.Business;
using Touryo.Infrastructure.Framework.Common;
using Touryo.Infrastructure.Framework.Dao;
using Touryo.Infrastructure.Framework.Exceptions;
using Touryo.Infrastructure.Framework.Presentation;
using Touryo.Infrastructure.Framework.Util;
using Touryo.Infrastructure.Framework.Transmission;

// 部品
using Touryo.Infrastructure.Public.Db;
using Touryo.Infrastructure.Public.IO;
using Touryo.Infrastructure.Public.Log;
using Touryo.Infrastructure.Public.Str;
using Touryo.Infrastructure.Public.Util;

namespace Touryo.Infrastructure.Business.Presentation
{
    /// <summary>画面コード親クラス２</summary>
    /// <remarks>（オーバーライドして）自由に利用できる。</remarks>
    public class MyBaseMVController : Controller
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
            System.Diagnostics.Debug.WriteLine("View 前");
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
            System.Diagnostics.Debug.WriteLine("View 前");
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
            System.Diagnostics.Debug.WriteLine(
                "OnActionExecuting 前"
                + " - " + filterContext.Controller.ToString()
                + " - " + filterContext.ActionDescriptor.ActionName);

            #region セッションタイムアウト検出処理

            // セッションタイムアウト検出処理の定義を取得
            string sessionTimeOutCheck =
                GetConfigParameter.GetConfigValue(FxLiteral.SESSION_TIMEOUT_CHECK);

            // デフォルト値対策：設定なし（null）の場合の扱いを決定
            if (sessionTimeOutCheck == null)
            {
                // OFF扱い
                sessionTimeOutCheck = FxLiteral.OFF;
            }

            // ON / OFF
            if (sessionTimeOutCheck.ToUpper() == FxLiteral.ON)
            {
                // セッションタイムアウト検出処理（ON）

                // セッション状態の確認
                if (Session.IsNewSession)
                {
                    // 新しいセッションが開始された

                    // セッションタイムアウト検出用Cookieをチェック
                    HttpCookie cookie = Request.Cookies.Get(FxHttpCookieIndex.SESSION_TIMEOUT);

                    if (cookie == null)
                    {
                        // セッションタイムアウト検出用Cookie無し → 新規のアクセス

                        // セッションタイムアウト検出用Cookieを新規作成（値は空文字以外、何でも良い）

                        // Set-Cookie HTTPヘッダをレスポンス
                        Response.Cookies.Set(FxCmnFunction.CreateCookieForSessionTimeoutDetection());
                    }
                    else
                    {
                        // セッションタイムアウト検出用Cookie有り

                        if (cookie.Value == "")
                        {
                            // セッションタイムアウト発生後の新規アクセス

                            // だが、値が消去されている（空文字に設定されている）場合は、
                            // 一度エラー or セッションタイムアウトになった後の新規のアクセスである。

                            // セッションタイムアウト検出用Cookieを再作成（値は空文字以外、何でも良い）

                            // Set-Cookie HTTPヘッダをレスポンス
                            Response.Cookies.Set(FxCmnFunction.CreateCookieForSessionTimeoutDetection());
                        }
                        else
                        {
                            // セッションタイムアウト発生

                            // エラー画面で以下の処理を実行する。
                            // ・セッションタイムアウト検出用Cookieを消去
                            // ・セッションを消去

                            // ※ エラー画面への遷移方法がTransferになっているため、
                            //    ここでセッションタイムアウト検出用Cookieを消去できないため。

                            // セッションタイムアウト例外を発生させる
                            throw new FrameworkException(
                                FrameworkExceptionMessage.SESSION_TIMEOUT[0],
                                FrameworkExceptionMessage.SESSION_TIMEOUT[1]);
                        }
                    }
                }
                else
                {
                    // セッション継続中
                }
            }
            else if (sessionTimeOutCheck.ToUpper() == FxLiteral.OFF)
            {
                // セッションタイムアウト検出処理（OFF）
            }
            else
            {
                // パラメータ・エラー（書式不正）
                throw new FrameworkException(
                    FrameworkExceptionMessage.ERROR_IN_WRITING_OF_FX_SWITCH1[0],
                    String.Format(FrameworkExceptionMessage.ERROR_IN_WRITING_OF_FX_SWITCH1[1],
                        FxLiteral.SESSION_TIMEOUT_CHECK));
            }

            #endregion

            // アクションの実行
            base.OnActionExecuting(filterContext);

            System.Diagnostics.Debug.WriteLine("OnActionExecuting 後");
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
            System.Diagnostics.Debug.WriteLine(
                "OnActionExecuted 前"
                + " - " + filterContext.Controller.ToString()
                + " - " + filterContext.ActionDescriptor.ActionName);
            
            base.OnActionExecuted(filterContext);
            
            System.Diagnostics.Debug.WriteLine("OnActionExecuted 後");
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
            //ASP.NET MVCでエラーハンドリングを行う場合 - waりとnaはてな日記
            //http://d.hatena.ne.jp/waritohutsu/20090526/1243313938

            System.Diagnostics.Debug.WriteLine(
                "OnException 前"
                + " - " + filterContext.Controller.ToString()
                + " - " + filterContext.Exception.Message);

            base.OnException(filterContext);

            System.Diagnostics.Debug.WriteLine("OnException 後");
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
            System.Diagnostics.Debug.WriteLine(
                "OnResultExecuting 前"
                + " - " + filterContext.Controller.ToString());

            base.OnResultExecuting(filterContext);

            System.Diagnostics.Debug.WriteLine("OnResultExecuting 後");
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
            System.Diagnostics.Debug.WriteLine(
                "OnResultExecuted 前"
                + " - " + filterContext.Controller.ToString());

            base.OnResultExecuted(filterContext);

            System.Diagnostics.Debug.WriteLine("OnResultExecuted 後");
        }
    }
}
