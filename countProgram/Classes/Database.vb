
Imports System.Data.Odbc

Public Class Database
    Private Source As OdbcConnection = New OdbcConnection()
    Private Source_DSN_Name As String
    Private Source_DSN_DBName As String
    Private Source_DSN_ID As String
    Private Source_DSN_PASS As String

    Public Event Insert_Log(ByVal log As String)

    'Public Sub New(Source_DSN_Name As String, Source_DSN_DBName As String, Source_DSN_ID As String, Source_DSN_PASS As String)

    '    Me.Source_DSN_Name = Source_DSN_Name
    '    Me.Source_DSN_DBName = Source_DSN_DBName
    '    Me.Source_DSN_ID = Source_DSN_ID
    '    Me.Source_DSN_PASS = Source_DSN_PASS

    'End Sub

    Public Sub Setting(Source_DSN_Name As String, Source_DSN_DBName As String, Source_DSN_ID As String, Source_DSN_PASS As String)
        Me.Source_DSN_Name = Source_DSN_Name
        Me.Source_DSN_DBName = Source_DSN_DBName
        Me.Source_DSN_ID = Source_DSN_ID
        Me.Source_DSN_PASS = Source_DSN_PASS
    End Sub

    Public Sub Close()
        Try
            Source.Close()
            Source.Dispose()
        Catch ex As Exception
            RaiseEvent Insert_Log(ex.Message)
        End Try
    End Sub

    ' 대상 Database 연결
    Private Function DB_Connect(Database_Connection As OdbcConnection,
                                    DSN_Name As String,
                                    DSN_DBName As String,
                                    DSN_ID As String,
                                    DSN_Pass As String) As Boolean
        Try
            Dim ConnectionString As String = ""
            If Database_Connection.State = ConnectionState.Open Then Return True
            Database_Connection.Close()
            ConnectionString = "DSN=" & DSN_Name & ";DATABASE=" & DSN_DBName & ";UID=" & DSN_ID & ";PWD=" & DSN_Pass & ";"
            Database_Connection.ConnectionString = ConnectionString
            Database_Connection.Open()
            Return True
        Catch ex As Exception
            RaiseEvent Insert_Log(ex.Message)
            Return False
        End Try
    End Function

    Private Function DB_Query(Database_Connection As OdbcConnection, Query As String, ByRef Row_Count As Integer) As Boolean
        Try
            'Dim ReturnCount As Long = 0
            Dim CMD As OdbcCommand = New OdbcCommand(Query, Database_Connection)
            Row_Count = CMD.ExecuteNonQuery()
            CMD.Dispose()
            CMD = Nothing
            Return True
        Catch ex As Exception
            RaiseEvent Insert_Log(ex.Message)
            Return False
        End Try
    End Function

    Private Function DB_Query(Database_Connection As OdbcConnection, Query As String, ByRef Database_DataTable As DataTable) As Boolean
        Try
            Dim dtAdapter As System.Data.Odbc.OdbcDataAdapter
            dtAdapter = New System.Data.Odbc.OdbcDataAdapter(Query, Database_Connection)
            dtAdapter.Fill(Database_DataTable)
            dtAdapter.Dispose()
            dtAdapter = Nothing
            Return True
        Catch ex As Exception
            RaiseEvent Insert_Log(ex.Message)
            Return False
        End Try
    End Function

    Public Function Connect() As Boolean
        If DB_Connect(Source, Source_DSN_Name, Source_DSN_DBName, Source_DSN_ID, Source_DSN_PASS) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Query(sQuery As String, ByRef Count As Integer) As Boolean
        If DB_Query(Source, sQuery, Count) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Query(sQuery As String, DataTable As DataTable) As Boolean
        If DB_Query(Source, sQuery, DataTable) Then
            Return True
        Else
            Return False
        End If
    End Function

    ' 설정내용 확인
    Public Function Info() As String
        Return String.Format("{0} {1} {2} {3} {4} {5}",
                             Source_DSN_Name, Source_DSN_ID, Source_DSN_PASS
                             )
    End Function

    Public Shared Function Check(DSN As String, DBName As String, ID As String, PW As String, ByRef ReturnMsg As String) As Boolean
        Try
            Dim ConnectionString As String = ""

            Dim TempConnection As OdbcConnection = New OdbcConnection()
            TempConnection.Close()
            ConnectionString = "DSN=" & DSN & ";DATABASE=" & DBName & ";UID=" & ID & ";PWD=" & PW & ";"
            TempConnection.ConnectionString = ConnectionString
            TempConnection.Open()
            Return True
        Catch ex As Exception
            ReturnMsg = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function Check(DSN As String, DBName As String, ID As String, PW As String, TableName As String, KeyField As String, Select_Query As String, ByRef Insert_Query As String, ByRef Update_Query As String, ByRef ReturnMsg As String) As Boolean
        Try
            Dim ConnectionString As String = ""

            Dim TempConnection As OdbcConnection = New OdbcConnection()

            TempConnection.Close()
            ConnectionString = "DSN=" & DSN & ";DATABASE=" & DBName & ";UID=" & ID & ";PWD=" & PW & ";"
            TempConnection.ConnectionString = ConnectionString
            TempConnection.Open()

            Dim Temp_Datatable As DataTable = New DataTable
            Dim dtAdapter As System.Data.Odbc.OdbcDataAdapter
            dtAdapter = New System.Data.Odbc.OdbcDataAdapter(Select_Query, TempConnection)
            dtAdapter.Fill(Temp_Datatable)


            Dim Column_Name As String = ""
            Dim Column_Value As String = ""
            For Each TempColumn As DataColumn In Temp_Datatable.Columns
                If TempColumn.ColumnName <> "id" Then
                    If Column_Name <> "" Then Column_Name += ", "
                    If Column_Value <> "" Then Column_Value += ", "
                    Column_Name += TempColumn.ColumnName
                    Column_Value += "'값'"
                End If
            Next
            Insert_Query = (String.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, Column_Name, Column_Value))

            Dim Column_Update As String = ""
            Dim Column_Where As String = String.Format("{0} = '{1}'", KeyField, "값")
            For Each TempColumn As DataColumn In Temp_Datatable.Columns
                If TempColumn.ColumnName <> "id" And TempColumn.ColumnName <> KeyField Then
                    If Column_Update <> "" Then Column_Update += ", "
                    Column_Update += TempColumn.ColumnName + "='값'"
                End If
            Next
            Update_Query = (String.Format("UPDATE {0} SET {1} WHERE {2}", TableName, Column_Update, Column_Where))
            Temp_Datatable.Clear()
            Temp_Datatable.Dispose()
            dtAdapter.Dispose()
            TempConnection.Close()

            Return True
        Catch ex As Exception
            ReturnMsg = ex.Message
            Return False
        End Try
    End Function

End Class
