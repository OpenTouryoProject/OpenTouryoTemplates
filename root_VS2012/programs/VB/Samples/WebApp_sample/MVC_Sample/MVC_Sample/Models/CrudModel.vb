'**********************************************************************************
'* サンプル アプリ・モデル
'**********************************************************************************

'**********************************************************************************
'* クラス名        ：CrudModel
'* クラス日本語名  ：サンプル アプリ・モデル
'*
'* 作成日時        ：－
'* 作成者          ：sas 生技
'* 更新履歴        ：
'*
'*  日時        更新者            内容
'*  ----------  ----------------  -------------------------------------------------
'*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
'*
'**********************************************************************************

'System
Imports System
Imports System.Web
Imports System.Web.Mvc

Imports System.Collections.Generic

' DataSet をインポート
Imports MVC_Sample.DataSets

Namespace Models
    ''' <summary>
    ''' サンプル アプリ・モデル
    ''' </summary>
    Public Class CrudModel
        ''' <summary>shippersテーブル</summary>
        Public Property shippers() As DsNorthwind.ShippersDataTable
            Get
                Return m_shippers
            End Get
            Set(value As DsNorthwind.ShippersDataTable)
                m_shippers = Value
            End Set
        End Property
        Private m_shippers As DsNorthwind.ShippersDataTable

        ''' <summary>メッセージ</summary>
        Public Property Message() As String
            Get
                Return m_Message
            End Get
            Set(value As String)
                m_Message = Value
            End Set
        End Property
        Private m_Message As String

#Region "ドロップダウンリストに表示するアイテム"

        ''' <summary>
        ''' ddlDap に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlDapItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "SQL Server / SQL Client", _
                        .Value = "SQL", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "Multi-DB / OLEDB.NET", _
                        .Value = "OLE" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "Multi-DB / ODBC.NET", _
                        .Value = "ODB" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "Oracle / ODP.NET", _
                        .Value = "SQL" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "DB2 / DB2.NET", _
                        .Value = "DB2" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "HiRDB / HiRDB-DP", _
                        .Value = "HIR" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "MySQL Cnn/NET", _
                        .Value = "MCN" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "PostgreSQL / Npgsql", _
                        .Value = "NPS" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlMode1 に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlMode1Items() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "個別Ｄａｏ", _
                        .Value = "individual", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "共通Ｄａｏ", _
                        .Value = "common" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "自動生成Ｄａｏ（更新のみ）", _
                        .Value = "generate" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlMode2 に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlMode2Items() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "静的クエリ", _
                        .Value = "static", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "動的クエリ", _
                        .Value = "dynamic" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlIso に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlIsoItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "ノットコネクト", _
                        .Value = "NC" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "ノートランザクション", _
                        .Value = "NT", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "ダーティリード", _
                        .Value = "RU" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "リードコミット", _
                        .Value = "RC" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "リピータブルリード", _
                        .Value = "RR" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "シリアライザブル", _
                        .Value = "SZ" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "スナップショット", _
                        .Value = "SS" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "デフォルト", _
                        .Value = "DF" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlExRollback に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlExRollbackItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "正常時", _
                        .Value = "-", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "業務例外", _
                        .Value = "Business" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "システム例外", _
                        .Value = "System" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "その他、一般的な例外", _
                        .Value = "Other" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "業務例外への振替", _
                        .Value = "Other-Business" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "システム例外への振替", _
                        .Value = "Other-System" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlTransmission に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlTransmissionItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "Webサービス呼出", _
                        .Value = "testWebService", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "インプロセス呼出", _
                        .Value = "testInProcess" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlOrderColumn に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlOrderColumnItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "c1", _
                        .Value = "c1", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "c2", _
                        .Value = "c2" _
                    }, _
                    New SelectListItem() With { _
                        .Text = "c3", _
                        .Value = "c3" _
                    } _
                }
            End Get
        End Property

        ''' <summary>
        ''' ddlOrderSequence に表示するアイテムリスト
        ''' </summary>
        Public ReadOnly Property DdlOrderSequenceItems() As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem)() From { _
                    New SelectListItem() With { _
                        .Text = "ASC", _
                        .Value = "A", _
                        .Selected = True _
                    }, _
                    New SelectListItem() With { _
                        .Text = "DESC", _
                        .Value = "D" _
                    } _
                }
            End Get
        End Property

#End Region

#Region "HTML.BeginFormで値を復元用途"

        ''' <summary>HTML.BeginFormで値を復元するためのワーク領域</summary>
        Public Property InputValues() As Dictionary(Of String, String)
            Get
                Return m_InputValues
            End Get
            Set(value As Dictionary(Of String, String))
                m_InputValues = Value
            End Set
        End Property
        Private m_InputValues As Dictionary(Of String, String)

        ''' <summary>HTML.BeginFormDe値を復元するためのワーク領域の初期化</summary>
        ''' <param name="form">入力フォームの情報</param>
        Public Sub CopyInputValues(form As FormCollection)
            InputValues = New Dictionary(Of String, String)()

            For Each key As String In form.AllKeys
                InputValues.Add(key, form(key))
            Next
        End Sub

#End Region
    End Class
End Namespace
