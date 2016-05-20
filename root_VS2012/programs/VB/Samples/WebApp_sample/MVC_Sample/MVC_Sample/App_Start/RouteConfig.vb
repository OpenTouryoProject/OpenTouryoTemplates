'**********************************************************************************
'* クラス名        ：RouteConfig
'* クラス日本語名  ：ルート定義に関する指定
'*
'* 作成日時        ：－
'* 作成者          ：－
'* 更新履歴        ：－
'*
'*  日時        更新者            内容
'*  ----------  ----------------  -------------------------------------------------
'*  2015/11/03  Sai               Changed controller to CrudMu in RegisterRoutes method to display startup page.
'**********************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

''' <summary>
''' ルート定義に関する指定
''' </summary>
Public Class RouteConfig
    ''' <summary>
    ''' ［ASP.NET MVC］ルート定義を追加するには？［3.5、4、C#、VB］ － ＠IT
    ''' http://www.atmarkit.co.jp/fdotnet/dotnettips/1031aspmvcrouting1/aspmvcrouting1.html
    '''  RegisterRoutesメソッドはアプリケーション起動
    '''  （Startイベント・ハンドラ）のタイミングで呼び出されるメソッドで、
    '''  デフォルトのルート（名前はDefault）を追加している。
    ''' </summary>
    Public Shared Sub RegisterRoutes(routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        ' ルートを追加するには、ルートの集合を表すRouteCollectionオブジェクトから
        ' MapRouteメソッドを呼び出すだけだ。MapRouteメソッドの構文は、次のとおりである。

        ' MapRouteメソッドの構文 
        ' MapRoute(String name, String url [,Object defaults])  
        ' ・ name：ルート名。
        ' ・ url：URIパターン。
        ' ・ defaults：初期値。 

        ' Defaultルートを定義
        routes.MapRoute(name:="Default", url:="{controller}/{action}/{id}", defaults:=New With { _
            Key .controller = "CrudMu", _
            Key .action = "Index", _
            Key .id = UrlParameter.[Optional] _
        })

        routes.MapRoute(name:="Default2", url:="{controller}/{action}/{id}", defaults:=New With { _
            Key .controller = "CrudMu2", _
            Key .action = "Index", _
            Key .id = UrlParameter.[Optional] _
        })
    End Sub
End Class
