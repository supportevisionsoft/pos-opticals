Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports System.Data.Odbc

Public Class DBConnection

    Private strCon As String
    Private objCon As OleDb.OleDbConnection
    Private objODBCCon As Odbc.OdbcConnection

    Public Function GetConnectionString(ByVal strConnection As String) As String
        'Try

        If Not String.IsNullOrEmpty(strConnection) Then
            strCon = ConfigurationManager.ConnectionStrings(strConnection).ConnectionString
        Else
            If String.Equals(ConfigurationManager.AppSettings("DB_CON_TYPE").ToString, "ODBC") Then
                strCon = ConfigurationManager.ConnectionStrings("DBConnection").ConnectionString
            ElseIf String.Equals(ConfigurationManager.AppSettings("DB_CON_TYPE").ToString, "OLEDB") Then
                strCon = ConfigurationManager.ConnectionStrings("DBCON_OLEDB").ConnectionString
            End If
        End If
        Return strCon
        'Catch ex As Exception
        '    errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        'End Try
    End Function

    Public Sub GetConnection()
        Try
            objCon = New OleDb.OleDbConnection
            objCon.ConnectionString = GetConnectionString("")
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Sub InsertToTable(ByVal queryString As String)
        Try
            GetConnection()
            If (objCon.State) = ConnectionState.Open Then
                objCon.Close()
            End If
            objCon.Open()
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = objCon
            cmd.CommandText = queryString
            cmd.ExecuteNonQuery()
            objCon.Close()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Function SelectFromTable(ByVal queryString As String) As Object

        Dim ds As New DataSet
        Dim da As New OleDb.OleDbDataAdapter
        GetConnection()
        If (objCon.State) = ConnectionState.Open Then
            objCon.Close()
        End If
        objCon.Open()
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = objCon
        cmd.CommandText = queryString
        da.SelectCommand = cmd
        da.Fill(ds, "Table")
        objCon.Close()
        Return ds
        
    End Function
    Public Sub UpdateToTableODBC(ByVal queryString As String)
        GetODBCConnection()
        If (objODBCCon.State) = ConnectionState.Open Then
            objODBCCon.Close()
        End If
        objODBCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objODBCCon)
        cmd.ExecuteNonQuery()
        objODBCCon.Close()
    End Sub

    Public Sub callprocedure(ByVal queryString As String)
        GetODBCConnection()
        If (objODBCCon.State) = ConnectionState.Open Then
            objODBCCon.Close()
        End If
        objODBCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objODBCCon)
        Dim cmdcommit As OdbcCommand = New OdbcCommand("Commit", objODBCCon)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "{CALL " + queryString + "}"
        cmd.ExecuteNonQuery()
        cmdcommit.ExecuteNonQuery()
        objODBCCon.Close()
    End Sub

    Public Sub GetODBCConnection()
        objODBCCon = New Odbc.OdbcConnection(GetConnectionString(""))
        objODBCCon.Open()
    End Sub

    Public Sub SaveToTableODBC(ByVal queryString As String)

        GetODBCConnection()
        If (objODBCCon.State) = ConnectionState.Open Then
            objODBCCon.Close()
        End If
        objODBCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objODBCCon)
        cmd.ExecuteNonQuery()

        objODBCCon.Close()
    End Sub

    Public Function SelectFromTableODBC(ByVal queryString As String) As Object

        Dim ds As New DataSet
        Try
            Dim da As New Odbc.OdbcDataAdapter
            GetODBCConnection()
            If (objODBCCon.State) = ConnectionState.Open Then
                objODBCCon.Close()
            End If
            objODBCCon.Open()
            'objODBCCon.Execute("ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY-MM-DD HH:MI:SS'")

            Dim cmd As New Odbc.OdbcCommand
            cmd.Connection = objODBCCon
            cmd.CommandText = queryString
            da.SelectCommand = cmd
            da.Fill(ds, "Table")
            objODBCCon.Close()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return ds
    End Function

End Class
