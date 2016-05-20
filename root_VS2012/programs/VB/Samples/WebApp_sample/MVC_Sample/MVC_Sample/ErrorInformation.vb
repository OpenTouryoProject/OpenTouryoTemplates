'**********************************************************************************
'* サンプル アプリ・コントローラ
'**********************************************************************************

'**********************************************************************************
'* クラス名        ：ErrorInformation class
'* クラス日本語名  ：Html.BeginForm用サンプル アプリ・コントローラ
'*
'* 作成日時        ：－
'* 作成者          ：sas 生技
'* 更新履歴        ：
'*
'*  日時        更新者            内容
'*  ----------  ----------------  -------------------------------------------------
'*  2015/08/28  Supragyan         Created ErrorInformation class to define error properties
'**********************************************************************************
'System
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

'MVC_Sample
Imports MVC_Sample.Controllers

''' <summary>
''' ErrorInformation class
''' </summary>
Public NotInheritable Class ErrorInformation
    Private Sub New()
    End Sub
    ''' <summary>
    ''' ErrorMessage
    ''' </summary>
    Public Shared Property ErrorMessage() As String
        Get
            Return m_ErrorMessage
        End Get
        Set(value As String)
            m_ErrorMessage = Value
        End Set
    End Property
    Private Shared m_ErrorMessage As String

    ''' <summary>
    ''' ErrorInformation
    ''' </summary>
    Public Shared Property ErrorInfo() As String
        Get
            Return m_ErrorInfo
        End Get
        Set(value As String)
            m_ErrorInfo = Value
        End Set
    End Property
    Private Shared m_ErrorInfo As String

    ''' <summary>
    ''' ErrorDatas
    ''' </summary>
    Public Shared Property ErrorDatas() As List(Of ExceptionData)
        Get
            Return m_ErrorDatas
        End Get
        Set(value As List(Of ExceptionData))
            m_ErrorDatas = Value
        End Set
    End Property
    Private Shared m_ErrorDatas As List(Of ExceptionData)
End Class
