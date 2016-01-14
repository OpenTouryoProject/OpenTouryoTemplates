//**********************************************************************************
//* All Rights Reserved, Copyright (C) 2007,2012 Hitachi Solutions,Ltd.
//**********************************************************************************

// ------------------------------------------------------------
//  イベントハンドラ
// ------------------------------------------------------------

// ---------------------------------------------------------------
// ページがロードされた時に呼ばれる
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/02/06  Supragyan         Added condition for check AjaxPostBackElement in Fx_AjaxExtensionInitializeRequest
//*  2015/02/06  Supragyan         Added condition for check AjaxPostBackElement in Fx_AjaxExtensionEndRequest
//*  2015/02/09  Supragyan         Added condition for Trident on Internet Explorer
//*  2015/09/09  Sandeep           Added condition code to detect IE-9, IE-10 and IE-11, to suppress double transmission
//*  2015/12/28  Sai               Added Java script method for preventing session timeout.
//*  2015/12/28  Sai               Commented out window.setInterval method.
//*  2016/01/11  Sai               Removed unnecessary code.
//*  2016/01/12  Sai               Changed interval in method window.setInterval(HttpPing, 5000)
//*  2016/01/13  Sai               Removed Ajax extensions code and added JQuery's Ajax Complete method.
//**********************************************************************************

function Fx_Document_OnLoad() {
    // OnLoad処理を別スレッドで実行
    setTimeout("Fx_Document_OnLoad2()", 1);
}

// ---------------------------------------------------------------
// ページがロードされた時に呼ばれる
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_Document_OnLoad2() {
    window.returnValue = "";

    // マスクの初期化
    Fx_CreateMask();

    // プログレス ダイアログの初期化
    Fx_InitProgressDialog();

    // Webサーバへ一定時間ごとにpingを行う
   //window.setInterval(HttpPing, 5 * 60 * 1000);
}

//// ---------------------------------------------------------------
//// セッションタイムアウトを防ぐため、Webサーバへ一定期間ごとにPINGを行う
//// ---------------------------------------------------------------
//// 引数    －
//// 戻り値  －
//// ---------------------------------------------------------------
//function HttpPing() {
//    $.ajax({
//        type: 'GET',
//        url: URL,
//        contentType: "application/json; charset=utf-8",
//        data: {},
//        dataType: "json",
//        success: function () {},
//        error: function () {}
//    });
//}

// ダウンロード処理の場合、ダイアログを表示しない。
var IsDownload = false;

// ---------------------------------------------------------------
// サブミットする時に呼ばれ、２重送信を抑止する。
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_OnSubmit() {

    // ----------

    // このカバレージ（onSubmit）は、
    // ・ポスト バック
    // ・ASP.NET Ajax Extension
    // のどちらも、通過する。

    // ・IE6.0のhrefのdoPostBackだと動かない。
    // ・OperaのhrefのdoPostBack（二重送信時限定）だと動かない。

    // alert(navigator.appName);
    // alert(navigator.appVersion);

    // ----------

    // ↑Operaの場合、ポスト後直ちにタイマーが無効になるので、実質機能しない。
    //   ※ ただし、Ajaxの場合は、タイマーが無効にならない。

    // ----------

    if (navigator.appName == "Microsoft Internet Explorer" || navigator.appVersion.indexOf("Trident") != -1) {

        // スレイプニルもこちらに含まれる。

        if (navigator.appVersion.indexOf("MSIE 6.0") != -1) {
            // IE6.0では、hrefのdoPostBackの２重送信を抑止できない。
            // （onSubmitイベントがハンドルされないため）
        }
        else if (navigator.appVersion.indexOf("MSIE 7.0") != -1) {
            // IE7.0では完全に有効
        }
        else if (navigator.appVersion.indexOf("MSIE 8.0") != -1) {
            // IE8.0では完全に有効
        }
        else if (navigator.appVersion.indexOf("MSIE 9.0") != -1) {
            // IE9.0で問題の報告を受けていません。 
        }
        else if (navigator.appVersion.indexOf("MSIE 10.0") != -1) {
            // IE10.0で問題の報告を受けていません。 
        }
        else if (navigator.appVersion.indexOf("Trident/7") != -1) {
            // IE11.0で問題が合った場合、報告をお願いします。
        }

        if (document.readyState == "complete") {

            // 受信完了

            // Ajaxがないページ
            if (Ajax_IsProgressed == null || Ajax_IsProgressed == undefined) {
                // 送信許可
                return true;
            }

            // Ajaxでは、completeのままになるので、
            // フラグでのチェックが必要になる。
            if (Ajax_IsProgressed) {

                // Ajax有効で、処理中の場合

                //                // 送信不許可
                //                alert("二重送信です(IE + Ajax)");

                // → ダイアログ表示中は、pageRequestManagerの
                //    initializeRequest、endRequestのコールバック
                //    をブロックするのでコメントアウトした。
                return false;
            }
            else {

                // Ajax有効で、処理中でない場合

                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }
        }
        else {

            // 受信未完了

            //            // 送信不許可
            //            alert("二重送信です(IE)");

            // → ダイアログ表示中は、pageRequestManagerの
            //    initializeRequest、endRequestのコールバック
            //    をブロックするのでコメントアウトした。
            return false;
        }
    }
    else if (navigator.appName == "Netscape") {
        // 実際には、Netscape、Firefox、Safari、Chromeのカバレージ

        // Netscapeはブラウザ側の機能で２重送信を抑止していたが、
        // サポートがなくなったため、サポートしない。

        // ブラウザがデフォルトでonSubmit×２を抑止している。
        // ・Firefoxはブラウザ側の機能で２重送信を抑止している。
        // ・Safariはブラウザ側の機能で２重送信を抑止している。
        // ・Chromeはブラウザ側の機能で２重送信を抑止している。

        if (document.readyState == "complete") {
            // 実際には、Safari、Chromeのカバレージ

            // Ajaxがないページ
            if (Ajax_IsProgressed == null || Ajax_IsProgressed == undefined) {
                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }

            // Ajaxでは、completeのままになるので、
            // フラグでのチェックが必要になる。
            if (Ajax_IsProgressed) {

                // Ajax有効で、処理中の場合

                //                // 送信不許可
                //                alert("二重送信です(Netscape + Ajax)");

                // → ダイアログ表示中は、pageRequestManagerの
                //    initializeRequest、endRequestのコールバック
                //    をブロックするのでコメントアウトした。
                return false;
            }
            else {

                // Ajax有効で、処理中でない場合

                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }
        }
        else if (document.readyState == null || document.readyState == undefined) {

            // 実際には、Firefoxのカバレージ

            // FirefoxでFxの２重送信抑止機能をONにした場合、
            // hrefのdoPostBackでPOSTできなくなる現象を確認した。  

            // Ajaxがないページ
            if (Ajax_IsProgressed == null || Ajax_IsProgressed == undefined) {
                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }

            // Ajaxでは、completeのままになるので、
            // フラグでのチェックが必要になる。
            if (Ajax_IsProgressed) {

                // Ajax有効で、処理中の場合

                //                // 送信不許可
                //                alert("二重送信です(Firefox + Ajax)");

                // → ダイアログ表示中は、pageRequestManagerの
                //    initializeRequest、endRequestのコールバック
                //    をブロックするのでコメントアウトした。
                return false;
            }
            else {

                // Ajax有効で、処理中でない場合

                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }

            // Firefoxの制限事項は、通常のポストバック中にajaxの２重送信を抑止できない。
            // 逆（ajax中に通常のポストバックの２重送信）は抑止できる。

        }
        else {

            // 受信未完了（こちらは通過しない）
            alert(document.readyState); // ← 故に、表示されない。

            //            // 送信不許可
            //            alert("二重送信です(Firefox)");

            // → ダイアログ表示中は、pageRequestManagerの
            //    initializeRequest、endRequestのコールバック
            //    をブロックするのでコメントアウトした。
            return false;
        }
    }
    else if (navigator.appName == "Opera") {

        // Operaでは完全に有効

        // ただし、二重送信時のhrefのdoPostBackはハンドルできない。
        // しかし、こちらは、ブラウザ側の機能で抑止している模様。

        if (document.readyState == "complete") {

            // 受信完了

            // Ajaxがないページ
            if (Ajax_IsProgressed == null || Ajax_IsProgressed == undefined) {
                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }

            // Ajaxでは、completeのままになるので、
            // フラグでのチェックが必要になる。
            if (Ajax_IsProgressed) {

                // Ajax有効で、処理中の場合

                //                // 送信不許可
                //                alert("二重送信です(Opera + Ajax)");

                // → ダイアログ表示中は、pageRequestManagerの
                //    initializeRequest、endRequestのコールバック
                //    をブロックするのでコメントアウトした。
                return false;
            }
            else {

                // Ajax有効で、処理中でない場合

                // 送信許可
                Fx_SetProgressDialog();
                return true;
            }
        }
        else {

            // 受信未完了

            //            // 送信不許可
            //            alert("二重送信です(Opera)");

            // → ダイアログ表示中は、pageRequestManagerの
            //    initializeRequest、endRequestのコールバック
            //    をブロックするのでコメントアウトした。
            return false;
        }
    }
    else {
        // その他のブラウザ
        // ・・・
    }
}

// ---------------------------------------------------------------
// プログレス ダイアログ表示を仕掛ける。
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_SetProgressDialog() {
    if (IsDownload) {
        // ダウンロードの場合

        // フラグを戻す
        IsDownload = false;
    }
    else {
        // ダウンロードでない場合

        // ダイアログ表示（２秒後）
        ProgressDialogTimer = setTimeout("Fx_DisplayProgressDialog()", 2000);
    }
}

//**********************************************************************************

// ------------------------------------------------------------
//  プログレス ダイアログの表示
// ------------------------------------------------------------

// Ajax：プログレス ダイアログ（div）
var AjaxProgressDialog;

// Ajax：プログレス ダイアログの表示タイマ
var ProgressDialogTimer;

// ---------------------------------------------------------------
// プログレス ダイアログの初期化
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ★★★  ダイアログのデザインを変える場合は、ここを直接編集
// ---------------------------------------------------------------
function Fx_InitProgressDialog() {

    // divを生成   
    var _div = document.createElement("div");
    _div.id = "AjaxProgressDialog";

    // 幅を指定
    _div.style.width = AjaxProgressDialog_Width + "px";
    _div.style.height = AjaxProgressDialog_Height + "px";

    // スタイルを指定
    _div.style.top = "0px";
    _div.style.left = "0px";

    _div.style.paddingTop = "10px";
    _div.style.paddingLeft = "10px";
    _div.style.paddingRight = "10px";

    _div.style.textAlign = "center";
    _div.style.overflow = "auto";

    _div.style.position = "absolute";

    // 1000なら最前面だろうという仕様（ToMost相当が無い）
    _div.style.zIndex = "1001"; // Maskより前面に出す。

    // 内容を指定
    _div.innerHTML = "処理中です。しばらくお待ち下さい・・・<hr />";
    _div.style.backgroundColor = "lightcyan";

    // imgを生成
    var _img = document.createElement("img");

    _img.src = "/MVC_sample/Framework/Img/loading.gif";
    _img.style.width = "50px";
    _img.style.height = "50px";
    _img.alt = "処理中画像";

    // divにimgを追加
    _div.appendChild(_img);

    // div → プログレス ダイアログ
    AjaxProgressDialog = _div
}

// ---------------------------------------------------------------
// プログレス ダイアログ表示
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------

function Fx_DisplayProgressDialog() {
    // はじめにタイマをクリアする。
    clearTimeout(ProgressDialogTimer);

    try {
        // 表示位置の計算
        AjaxProgressDialog.style.top = (Fx_getContentsHeight() / 2) //(Fx_getBrowserHeight() / 2)
            - (AjaxProgressDialog_Height / 2) + "px";
        AjaxProgressDialog.style.left = (Fx_getBrowserWidth() / 2)
            - (AjaxProgressDialog_Width / 2) + "px";

        // プログレス ダイアログを表示する。
        document.body.appendChild(AjaxMask);
        document.body.appendChild(AjaxProgressDialog);

    } catch (e) {
        //alert( e );//エラー内容
    }
}

//**********************************************************************************

// ------------------------------------------------------------
//  マスクの表示
// ------------------------------------------------------------

// Ajax：マスク（div）
var AjaxMask;

// ---------------------------------------------------------------
// マスク生成
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_CreateMask() {

    var _div = document.createElement("div");

    _div.style.top = "0px";
    _div.style.left = "0px";
    _div.style.height = Fx_getContentsHeight(); //"100%";では、初期表示画面サイズになってしまう。
    _div.style.width = Fx_getBrowserWidth(); //"100%";では、初期表示画面サイズになってしまう。
    _div.style.position = "absolute";

    // 1000なら最前面だろうという仕様（ToMost相当が無い）
    _div.style.zIndex = "1000";

    // 分かりやすいように半透明 (0に設定しても良い)
    _div.style.opacity = 0.5;
    // IE 8用の透明度の設定(0に設定しても良い)
    _div.style.filter = "alpha(opacity=50)";
    // 分かりやすいように着色(若しくは無色)
    _div.style.backgroundColor = 'gray';

    AjaxMask = _div;
}

//**********************************************************************************

// ---------------------------------------------------------------
//  フレームワーク機能（Ajax）
// ---------------------------------------------------------------

// Ajax：プログレス ダイアログのサイズ（div）
var AjaxProgressDialog_Width = 300;
var AjaxProgressDialog_Height = 100;

// Ajax：プログレス中かどうか
var Ajax_IsProgressed = false;

// ---------------------------------------------------------------
// Ajax Extensionの終了後イベント処理
// ---------------------------------------------------------------
$(document).ajaxComplete(function () {
    // はじめにタイマをクリアする。
    clearTimeout(ProgressDialogTimer);

    // プログレス ダイアログを非表示にする。
    try {

        document.body.removeChild(AjaxMask);
        document.body.removeChild(AjaxProgressDialog);

    } catch (e) {
        //alert( e );//エラー内容
    }

    // 二重送信フラグの設定
    Ajax_IsProgressed = false;
});

// ---------------------------------------------------------------
// ClientCallbackや、WebServiceBridge、
// XMLHTTPRequest、jQueryなど、Ajax非同期通信用
// 汎用の開始前処理、終了後処理関数を用意する（予定）。
// 
// 技術毎に、開始終了処理のイベント実装の機構を持つが、
// それを使うか、どうか、などの検討も必要になる。
// ---------------------------------------------------------------

//**********************************************************************************

// ---------------------------------------------------------------
//  ユーティリティ：画面サイズ取得関数
// ---------------------------------------------------------------

// ---------------------------------------------------------------
// 画面の幅取得
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_getBrowserWidth() {
    if (window.innerWidth) {
        return window.innerWidth;
    }

    // documentがnullになることがある・・・
    if (document == null || document == undefined) {
        // 処理しない。
    }
    else {
        // 処理する。

        if (document.documentElement && document.documentElement.clientWidth != 0) {
            return document.documentElement.clientWidth;
        }

        if (document.body) {
            return document.body.clientWidth;
        }
    }

    return 0;
}

// ---------------------------------------------------------------
// ブラウザ画面の高さ取得
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_getBrowserHeight() {

    if (window.innerHeight) {
        return window.innerHeight;
    }

    // documentがnullになることがある・・・
    if (document == null || document == undefined) {
        // 処理しない。
    }
    else {
        // 処理する。

        if (document.documentElement && document.documentElement.clientHeight != 0) {
            return document.documentElement.clientHeight;
        }

        if (document.body) {
            return document.body.clientHeight;
        }
    }

    return 0;
}

// ---------------------------------------------------------------
// コンテンツ全体の高さ取得
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function Fx_getContentsHeight() {
    // コンテンツ全体の高さを取得する
    return Math.max.apply(null, [document.body.clientHeight, document.body.scrollHeight, document.documentElement.scrollHeight, document.documentElement.clientHeight]);
}

