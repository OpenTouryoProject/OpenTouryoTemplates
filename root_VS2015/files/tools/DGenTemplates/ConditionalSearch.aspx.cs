﻿//**********************************************************************************
//* 三層データバインド・アプリ画面
//**********************************************************************************

//**********************************************************************************
//* クラス名        ：_TableName_ConditionalSearch
//* クラス日本語名  ：三層データバインド・検索一覧表示画面（_TableName_）
//*
//* 作成日時        ：_TimeStamp_
//* 作成者          ：自動生成ツール（墨壺２）, _UserName_
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

using MyType;

// System
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

// System.Web
using System.Web;
using System.Web.Security;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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

/// <summary>三層データバインド・サンプル アプリ画面（検索一覧表示）</summary>
public partial class _TableName_ConditionalSearch : MyBaseController
{
    /// <summary>Page_InitイベントでASP.NET標準イベントハンドラを設定</summary>
    protected void Page_Init(object sender, EventArgs e)
    {
        // 行選択についてのイベント
        this.gvwGridView1.SelectedIndexChanging += new GridViewSelectEventHandler(gvwGridView1_SelectedIndexChanging);
    }

    #region ページロードのUOCメソッド

    /// <summary>
    /// ページロードのUOCメソッド（個別：初回ロード）
    /// </summary>
    /// <remarks>
    /// 実装必須
    /// </remarks>
    protected override void UOC_FormInit()
    {
        // フォーム初期化（初回ロード）時に実行する処理を実装する

        // TODO:
    }

    /// <summary>
    /// ページロードのUOCメソッド（個別：ポストバック）
    /// </summary>
    /// <remarks>
    /// 実装必須
    /// </remarks>
    protected override void UOC_FormInit_PostBack()
    {
        // フォーム初期化（ポストバック）時に実行する処理を実装する

        // TODO:
        Session["DAP"] = "_DAP_";
           
        Session["DBMS"] = DbEnum.DBMSType._DBMS_;
      
    }

    #endregion

    #region イベントハンドラ

    /// <summary>追加ボタン</summary>
    /// <param name="fxEventArgs">イベントハンドラの共通引数</param>
    /// <returns>URL</returns>
    protected string UOC_btnInsert_Click(FxEventArgs fxEventArgs)
    {
        // 画面遷移（詳細表示）
        return "_TableName_Detail.aspx";
    }

    /// <summary>検索ボタン</summary>
    /// <param name="fxEventArgs">イベントハンドラの共通引数</param>
    /// <returns>URL</returns>
    protected string UOC_btnSearch_Click(FxEventArgs fxEventArgs)
    {
        // GridViewをリセット
        this.gvwGridView1.PageIndex = 0;
        this.gvwGridView1.Sort("", SortDirection.Ascending);

        // 検索条件の収集
        // AndEqualSearchConditions
        Dictionary<string, object> andEqualSearchConditions = new Dictionary<string, object>();
        // ControlComment:LoopStart-PKColumn
        andEqualSearchConditions.Add("_ColumnName_", this.txt_ColumnName__And.Text);
        // ControlComment:LoopEnd-PKColumn
        // ControlComment:LoopStart-ElseColumn
        andEqualSearchConditions.Add("_ColumnName_", this.txt_ColumnName__And.Text);
        // ControlComment:LoopEnd-ElseColumn
        Session["AndEqualSearchConditions"] = andEqualSearchConditions;

        // AndLikeSearchConditions
        Dictionary<string, string> andLikeSearchConditions = new Dictionary<string, string>();
        // ControlComment:LoopStart-PKColumn
        andLikeSearchConditions.Add("_ColumnName_", this.txt_ColumnName__And_Like.Text);
        // ControlComment:LoopEnd-PKColumn
        // ControlComment:LoopStart-ElseColumn
        andLikeSearchConditions.Add("_ColumnName_", this.txt_ColumnName__And_Like.Text);
        // ControlComment:LoopEnd-ElseColumn
        Session["AndLikeSearchConditions"] = andLikeSearchConditions;

        // OrEqualSearchConditions
        Dictionary<string, object[]> orEqualSearchConditions = new Dictionary<string, object[]>();
        // ControlComment:LoopStart-PKColumn
        orEqualSearchConditions.Add("_ColumnName_", this.txt_ColumnName__OR.Text.Split(' '));
        // ControlComment:LoopEnd-PKColumn
        // ControlComment:LoopStart-ElseColumn
        orEqualSearchConditions.Add("_ColumnName_", this.txt_ColumnName__OR.Text.Split(' '));
        // ControlComment:LoopEnd-ElseColumn
        Session["OrEqualSearchConditions"] = orEqualSearchConditions;

        // OrLikeSearchConditions
        Dictionary<string, string[]> orLikeSearchConditions = new Dictionary<string, string[]>();
        // ControlComment:LoopStart-PKColumn
        orLikeSearchConditions.Add("_ColumnName_", this.txt_ColumnName__OR_Like.Text.Split(' '));
        // ControlComment:LoopEnd-PKColumn
        // ControlComment:LoopStart-ElseColumn
        orLikeSearchConditions.Add("_ColumnName_", this.txt_ColumnName__OR_Like.Text.Split(' '));
        // ControlComment:LoopEnd-ElseColumn
        Session["OrLikeSearchConditions"] = orLikeSearchConditions;

        //// ElseSearchConditions
        //Dictionary<string, object> ElseSearchConditions = new Dictionary<string, object>();
        //ElseSearchConditions.Add("myp1", 1);
        //ElseSearchConditions.Add("myp2", 40);
        //Session["ElseSearchConditions"] = ElseSearchConditions;
        //Session["ElseWhereSQL"] = "AND [ProductID] BETWEEN @myp1 AND @myp2";

        // ソート条件の初期化
        Session["SortExpression"] = "_PKFirstColumn_"; // 主キーを指定
        Session["SortDirection"] = "ASC";        // ASCを指定

        // gvwGridView1をObjectDataSourceに連結。
        this.gvwGridView1.DataSourceID = "ObjectDataSource1";

        // ヘッダーを設定する。
        this.gvwGridView1.Columns[_ColumnNmbr_].HeaderText = "選択";
        // ControlComment:LoopStart-PKColumn
        this.gvwGridView1.Columns[_ColumnNmbr_].HeaderText = "_ColumnName_";
        // ControlComment:LoopEnd-PKColumn
        // ControlComment:LoopStart-ElseColumn
        this.gvwGridView1.Columns[_ColumnNmbr_].HeaderText = "_ColumnName_";
        // ControlComment:LoopEnd-ElseColumn

        // 画面遷移しない。
        return string.Empty;
    }

    /// <summary>gvwGridView1のSortingイベント</summary>
    /// <param name="fxEventArgs">イベントハンドラの共通引数</param>
    /// <param name="e">オリジナルのイベント引数</param>
    /// <returns>URL</returns>
    protected string UOC_gvwGridView1_Sorting(FxEventArgs fxEventArgs, GridViewSortEventArgs e)
    {
        // ソート条件の変更
        Session["SortExpression"] = e.SortExpression;

        if ((string)Session["SortDirection"] == "ASC")
        {
            e.SortDirection = SortDirection.Descending;
            Session["SortDirection"] = "DESC";
        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
            Session["SortDirection"] = "ASC";
        }

        // 画面遷移しない。
        return string.Empty;
    }

    /// <summary>GridViewの行の選択ボタンがクリックされ、行が選択される前に発生するイベント</summary>
    protected void gvwGridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       
        // 選択されたレコードの主キーとタイムスタンプ列を取得
        DataTable dt = (DataTable)Session["SearchResult"];
        Dictionary<string, object> PrimaryKeyAndTimeStamp = new Dictionary<string, object>();

        // 主キーとタイムスタンプ列
        // 主キー列
        // ControlComment:LoopStart-PKColumn
        PrimaryKeyAndTimeStamp.Add("_ColumnName_", dt.Rows[e.NewSelectedIndex]["_ColumnName_"].ToString());
        // ControlComment:LoopEnd-PKColumn
        
        // タイムスタンプ列                
        TS_CommentOut_ if(dt.Rows[e.NewSelectedIndex]["_TimeStampColName_"].GetType()!=typeof(System.DBNull))
        TS_CommentOut_ {
        TS_CommentOut_ PrimaryKeyAndTimeStamp.Add("_TimeStampColName_", dt.Rows[e.NewSelectedIndex]["_TimeStampColName_"]);
        TS_CommentOut_ }
        Session["PrimaryKeyAndTimeStamp"] = PrimaryKeyAndTimeStamp;       
    }

    /// <summary>gvwGridView1の行選択後イベント</summary>
    /// <param name="fxEventArgs">イベントハンドラの共通引数</param>
    /// <returns>URL</returns>
    protected string UOC_gvwGridView1_SelectedIndexChanged(FxEventArgs fxEventArgs)
    {
        // 画面遷移（詳細表示）
        return "_TableName_Detail.aspx";
    }

    #endregion
}