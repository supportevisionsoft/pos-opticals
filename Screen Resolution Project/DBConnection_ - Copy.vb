Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports System.Data.Odbc
Imports Oracle.DataAccess.Client

Public Class DBConnection

    Private strCon As String
    Private objCon As OleDb.OleDbConnection
    Private objODBCCon As Odbc.OdbcConnection
    Private objOracleCCon As New OracleConnection

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
            ' objCon = New OleDb.OleDbConnection
            objOracleCCon.ConnectionString = GetConnectionString("")

            '  objCon.ConnectionString = GetConnectionString("")
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
        If (objOracleCCon.State) = ConnectionState.Open Then
            objOracleCCon.Close()
        End If
        objOracleCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objOracleCCon)
        cmd.ExecuteNonQuery()
        objOracleCCon.Close()
    End Sub

    Public Sub callprocedure(ByVal queryString As String)
        GetODBCConnection()
        If (objOracleCCon.State) = ConnectionState.Open Then
            objOracleCCon.Close()
        End If
        objOracleCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objOracleCCon)
        Dim cmdcommit As OdbcCommand = New OdbcCommand("Commit", objOracleCCon)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "{CALL " + queryString + "}"
        cmd.ExecuteNonQuery()
        cmdcommit.ExecuteNonQuery()
        objOracleCCon.Close()
    End Sub

    Public Sub GetODBCConnection()
        ' objODBCCon = New Odbc.OdbcConnection(GetConnectionString(""))
        objOracleCCon.ConnectionString = GetConnectionString("")

        objOracleCCon.Open()
    End Sub

    Public Sub SaveToTableODBC(ByVal queryString As String)

        GetODBCConnection()
        If (objOracleCCon.State) = ConnectionState.Open Then
            objOracleCCon.Close()
        End If
        objOracleCCon.Open()
        Dim cmd As OdbcCommand = New OdbcCommand(queryString, objOracleCCon)
        cmd.ExecuteNonQuery()

        objOracleCCon.Close()
    End Sub

    Public Function SelectFromTableODBC(ByVal queryString As String) As Object

        Dim ds As New DataSet
        Try
            Dim da As New Odbc.OdbcDataAdapter
            GetODBCConnection()
            If (objOracleCCon.State) = ConnectionState.Open Then
                objOracleCCon.Close()
            End If
            ' objODBCCon.Open()
            objOracleCCon.Open()

            'objOracleCCon.Execute("ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY-MM-DD HH:MI:SS'")

            Dim cmd As New Odbc.OdbcCommand
            cmd.Connection = objOracleCCon
            cmd.CommandText = queryString
            da.SelectCommand = cmd
            da.Fill(ds, "Table")
            objOracleCCon.Close()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return ds
    End Function

End Class
