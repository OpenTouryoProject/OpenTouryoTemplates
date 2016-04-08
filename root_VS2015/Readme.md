﻿# Open 棟梁 Visual Studio2015 テンプレート・ベースの利用方法(How to Use OpenTouryo Visual Studio 2015 template base)


Open棟梁 Visual Studio2015 テンプレート・ベースに
同梱されるサンプルの実行手順は下記のとおりです。

(Execution procedure of the samples that are shipped with the
OpenTouryo Visual Studio 2015 template base is as follows.)

* 「/root_VS2015/」以下のフォルダを「C:\root」フォルダ以下に配置します。
   (Deploy the follwings under 「/root_VS2015/」folder to the「C:\root」folder.)
   
* Visual Studio 2015 と SQL Server のインストール
   (Installing Visual Studio 2015 and SQL Server.)
   
* サンプルDBの準備(Prepare Sample DB)

   - 下記からダウンロードしインストールします。
      (Download and install from the following.)
      
      Download: NorthWind and pubs Sample Databases for SQL Server 2000 - Microsoft Download Center - Download Details
      http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=23654
      
   - 下記コマンドを実行します(Run the following command)。
      "C:\Program Files\Microsoft SQL Server\100\Tools\Binn\SQLCMD.EXE" -S localhost\SQLExpress -E -i "C:\SQL Server 2000 Sample Databases\instnwnd.sql"

* セッション状態サービスの準備(Preparing the session state service)
   - 管理者としてコマンドプロンプトを起動し、下記コマンドを実行します。
      (Start a command prompt as an administrator, and then run the following command.)
      
      sc config aspnet_state start= auto
      net start aspnet_state

* プログラムのビルド(Building the program)
   C:\root\programs\C#
   C:\root\programs\VB

   フォルダ以下のビルドバッチを番号順に実行してプログラムをビルドします。
   (Build the program by running in numerical order the build batch of Above folder.)

   - 必要であれば、環境に合わせて、z_Common.bat内のBUILDFILEPATHを書き換えます。
     (If necessary, for your environment, you can rewrite the BUILDFILEPATH of z_Common.bat within.)
   
     Express Editionを使用している場合は、devenv.comが存在しないので、
     z_Common.batとz_Common2.batを差し替えてMSBuild.exeを使用して下さい。
     
     (If you are using the Express Edition,
     use the MSBuild.exe by replacing the z_Common2.bat and z_Common.bat.
     Because devenv.com does not exist.)
   
   - VB版を使用する場合は、"C:\root\programs\C#\"の
     1_DeleteDir.batから4_Build_Framework_Tool.batまでを実行した後に、
     "C:\root\programs\VB\"の1_DeleteDir.batから順次実行して下さい。
     
     If you use the VB version,
     after executing from "1_DeleteDir.bat" to "4_Build_Framework_Tool.bat" at the location of "C:\root\programs\C#\",
     please executing sequentially from "1_DeleteDir.bat" at the location of "C:\root\programs\VB\".
   
* VS2013のWebSiteで仮想パスのルートにプロジェクト名が
   入らなくなったことに起因して以下の対応が必要になりました。
   
   (The project name lost from root of the virtual path in the WebSite of VS2015.
   I now need to be addressed in the following.)
   
   e.g.: プロジェクト名 (project name) = ProjectX_sample
         - before version Visual Studio 2012 -> http://localhost:9999/ProjectX_sample/Aspx/start/menu.aspx
         - Visual Studio 2013 -> http://localhost:9999/Aspx/start/menu.aspx
         - Visual Studio 2015 -> http://localhost:9999/Aspx/start/menu.aspx
         
   asp.net - Upgrade VS2012 project to VS2013 web site and it won't let me specify IIS port - Stack Overflow
   http://stackoverflow.com/questions/19635467/upgrade-vs2012-project-to-vs2013-web-site-and-it-wont-let-me-specify-iis-port
   
   The above phenomenon is also persists in the VS2015 web site and it won't let me specify IIS port
   
   - 念のため、aspnet_regiisを実行します。
      CMDを管理者モードで起動して「aspnet_regiis.exe - i」コマンドを実行します。
      
      (Just in case, Run the aspnet_regiis. 
       You can run the "aspnet_regiis.exe - i" command to launch in administrator mode the CMD.)
       
      "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe" - i
  
   - IISの管理ツールにより、プロジェクトのルート・フォルダを
      IISの仮想ディレクトリに設定します（エイリアス：ProjectX_sample）。
      そして、その後、この仮想ディレクトリをアプリケーションに変換します。
      
      (Set root folder of the project to virtual directory of IIS (alias: ProjectX_sample),
      Thereafter, convert this to application by IIS management tool.)
      
      \root\programs\C#\Samples\WebApp_sample\ProjectX_sample
      \root\programs\VB\Samples\WebApp_sample\ProjectX_sample
      
   - VS2013を「管理者として実行」で起動して、
      プロジェクト・フォルダを既存のWebSiteとしてHTTP-IISから開きます。
      "ファイル -> 開く -> WebSite、ローカル IIS -> IIS Expressサイトではなく、IISサイトから選択"
      プロジェクトをデバッグ実行し、仮想パスのルートにプロジェクト名が入る事を確認します。
      
      (Start with "Run as Administrator" on VS2015, 
      I open it from the HTTP-IIS WebSite as an existing project folder.
      "File -> Open -> WebSite, Local IIS -> Select from IIS site rather than IIS express site"
      The debug run the project to see that the project name to enter the root of the virtual path.)
       
   - SQL Serverへの接続の暫定対策として偽装をします。
      (Impersonate as an interim measure for a connection to SQL Server.)
      
      <!-- 偽装する場合は以下を有効にする(Enable the following: If you want to impersonate) -->
      <identity impersonate="true" userName="xxxx" password="yyyy" />
      
      他の対応方法として、SQL Server認証を有効にしてもいけます。
      (In response other methods, it is also possible to enable the SQL Server authentication.)
      
      SQL Server の認証 - マイクロソフト系技術情報 Wiki
      (SQLServer's authentication - Microsoft-based technology information Wiki)
      http://techinfoofmicrosofttech.osscons.jp/index.php?SQL%20Server%20%E3%81%AE%E8%AA%8D%E8%A8%BC 
      
* サンプルの実行(Running the Sample)

   下記ファイルを開き実行する（VB版は一部の提供になっています）。
   ログイン画面が出た場合は、パスワードの確認は行っていないため、任意の数字を入力してください。
   
   (Open and run the following file (VB version provide some). 
   If the login screen appears, because not check the password, please enter the number of any.)
   
   - Web の場合(In the case of Web)：
      - ASP.NET
         C:\root\programs\C#\Samples\WebApp_sample\ProjectX_sample\ProjectX_sample.sln
      - ASP.NET MVC
         C:\\root\programs\C#\Samples\WebApp_sample\MVC_Sample\MVC_Sample.sln
      - ASP.NET MVC SPA
         C:\\root\programs\C#\Samples\WebApp_sample\SPA_Sample\SPA_Sample.sln
    
   - C/S 2階層の場合(In the case of two-tier C/S)：
      - Windows Forms
         C:\root\programs\C#\Samples\2CS_sample\2CSClientWin_sample\2CSClientWin_sample.sln
      - WPF
         C:\root\programs\C#\Samples\2CS_sample\2CSClientWPF_sample\2CSClientWPF_sample.sln
    
   - C/S 3階層の場合(In the case of three-tier C/S)：
      - Windows Forms
         C:\root\programs\C#\Samples\WS_sample\WSClient_sample\WSClientWin_sample\WSClientWin_sample.sln
         C:\root\programs\C#\Samples\WS_sample\WSClient_sample\WSClientWinCone_sample\WSClientWinCone_sample.sln
      - WPF
         C:\root\programs\C#\Samples\WS_sample\WSClient_sample\WSClientWPF_sample\WSClientWPF_sample.sln
    
   - Windowsストアアプリ の場合(In the case of Windows Store App )：
      C:\root\programs\C#\Samples\WS_sample\WSClient_sample\WSClientWinStore_samples\WSClientWinStore_samples.sln
   - Windows Azure の場合(In the case of Windows Azure)：
      C:\root\programs\C#\Samples\WinAzure_sample\WinAzure_sample.sln

* 各チュートリアルの内容に従いOpen棟梁の評価が可能です。
   (Evaluation of OpenTouryo is possible in accordance with the contents of each tutorial.)
   
   \OpenTouryoProject\OpenTouryoDocuments\2_Tutorial\
   
   ドキュメント類は[OpenTouryoProject/OpenTouryoDocuments](https://github.com/OpenTouryoProject/OpenTouryoDocuments)リポジトリに格納されています。
   (documents are located in the [OpenTouryoProject/OpenTouryoDocuments](https://github.com/OpenTouryoProject/OpenTouryoDocuments) repository.)
   
* また、テンプレート・ベースをチュートリアルの内容に従いカスタマイズすることで、
   当該Visual Studioバージョンの案件向けプロジェクト・テンプレートを作成できます。
   
   (Further, You  will customize template base according to the contents of the tutorial, 
   You can create project template for the Visual Studio version for the project.)
    
   \OpenTouryoProject\OpenTouryoDocuments\2_Tutorial\Tutorial_Template_development.doc
   
   ドキュメント類は[OpenTouryoProject/OpenTouryoDocuments](https://github.com/OpenTouryoProject/OpenTouryoDocuments)リポジトリに格納されています。
   (documents are located in the [OpenTouryoProject/OpenTouryoDocuments](https://github.com/OpenTouryoProject/OpenTouryoDocuments) repository.)
   
# 著作権、ライセンス(Copyright, license)

[License](https://github.com/OpenTouryoProject/OpenTouryoTemplates/tree/master/license)ディレクトリを確認下さい。
(Please check [License](https://github.com/OpenTouryoProject/OpenTouryoTemplates/tree/master/license) directory.)

# バグ対応(Bug fix)

バグの発見や通知があった場合、通知の妥当性の確認後、
バックログに加えられ任意のタイミングでフィックスされます。

バグ修正パッチの取込みは、最新版取込みにより実現されます。
若しくは、当該バグをトラッキング・ツール上から確認して
リポジトリからバグフィックス時のDIFFを取得し各自マージしてください。

If there is a notification or discovery of the bug,
after confirmation of the validity of the notification, 
It will be fixed at any time be added to the backlog. 

Incorporation of bug fixes are implemented by the latest version of incorporation. 
Or, check from the tracking tool on the bug 
Please have your own merge by get the DIFF of bug fixes from the repository at the time.

# データプロバイダの入手、輸出手続き、使用許諾への添付について(obtain the data provider. export procedures. attach to license.)


Open棟梁では、種々のデータ・プロバイダをサポートしていますが、
各データ・プロバイダの入手・輸出手続きに関しては、各自対応下さい。

The Open Touryo is support the data provider of various, 
For information on obtaining and export procedures for each data provider, please support their own.
