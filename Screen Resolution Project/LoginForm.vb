Imports System
Imports System.Net.Dns
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings

Public Class LoginForm
    Dim db As New DBConnection

    Dim Company_Codes As New List(Of String)
    Dim Location_Codes As New List(Of String)
    Dim Staff_Codes As New List(Of String)
    Dim MySource_CompanyCodes As New AutoCompleteStringCollection()
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_StaffCodes As New AutoCompleteStringCollection()
    Dim startDate = "1/01/2014"
    Dim enddDate = "30/12/2015"

    Dim licenseMessage As String = ""

    Shared Function GetIPAddress() As String
        Dim strHostName As String
        Dim strIPAddress As String
        strHostName = System.Net.Dns.GetHostName()
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
        GetIPAddress = strIPAddress
    End Function

    Public Function TestDecoding() As Boolean
        MsgBox("Test Decode")
        Dim cipherText As String = ConfigurationManager.AppSettings("DBPERIOD").ToString
        Dim password As String = "new123"
        Dim wrapper As New Simple3Des(password)
        Dim success As Boolean = True
        ' DecryptData throws if the wrong password is used. 
        Try
            MsgBox("Try entry")
            Dim plainText As String = wrapper.DecryptData(cipherText)
            Dim vals() As String = plainText.Split(" ")
            For i = 4 To vals.Count - 1
                licenseMessage = licenseMessage & vals(i) & " "
                MsgBox(licenseMessage)
            Next
            Dim stQuery As String
            Dim ds As DataSet

            stQuery = "select sysdate from dual where sysdate>=to_date('" & vals(0) & " 000000','" & vals(2) & " hh24miss') and sysdate<=to_date('" & vals(1) & " 235959','" & vals(2) & " hh24miss')"
            MsgBox(stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            'errLog.WriteToErrorLog(Licencecheckquery", , "stQuery")

            errLog.WriteToErrorLog("Licencecheck Query", stQuery, "")
            MsgBox("QueryPrint")
            'errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
            'If Not ds.Tables("Table").Rows.Count > 0 Then
            '    success = False
            'Else
            Dim dateval As Date = ds.Tables("Table").Rows.Item(0).Item(0)

            If Not CDate(dateval.ToString("dd-MM-yyy")) < CDate(vals(3)) Then
                MsgBox(licenseMessage)

            End If
            MsgBox("End of Licence message")
            'End If
        Catch ex As System.Security.Cryptography.CryptographicException

            'success = False
            success = True
            'MsgBox("The data could not be decrypted with the password.")
        End Try
        MsgBox("Try Exit")
        Return success

    End Function

    Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'Commented by Nandhini IP COnfig
            'If TestDecoding() Then
            '   MsgBox("IF PART")

            'Else
            '  MsgBox(licenseMessage)

            '   End
            'End If
            'POSCounterName = "PS39"
            'Path Set by Nandhini to extract Encrypted date from IPFILE to application '
            Dim FILE_NAME As String = Application.ExecutablePath + "..\..\IPFILE.txt"
            'MsgBox(FILE_NAME)
            If System.IO.File.Exists(FILE_NAME) = True Then

                Dim TextLine As String = ""
                Dim val As Boolean = False
                Dim objReader As New System.IO.StreamReader(FILE_NAME)
                Do While objReader.Peek() <> -1
                    TextLine = objReader.ReadLine()
                    'MsgBox(TextLine)


                Loop
                objReader.Close()

                Dim dtst As Byte() = Convert.FromBase64String(TextLine)
                'MsgBox(dtst.ToArray)
                Dim decryteddate As String = System.Text.Encoding.UTF8.GetString(dtst).Split("|")(1).ToString()

                'Dim decryteddate1 As String = System.Text.Encoding.UTF8.GetString(dtst).Split("|")(1).ToString
                'MsgBox(decryteddate1)
                ' MsgBox(decryteddate)
                'Dim stDate = decryteddate.Split(",")(0).ToString

                'MsgBox(startDate)
                'Dim endDate = decryteddate.Split(",")(1).ToString

                'MsgBox(enddDate)
                If startDate <> "" And enddDate <> "" Then
                    Dim NowDate As String = Now.Date.ToString("dd/MM/yyyy")
                    'Dim NowDate As Date
                    'If NowDate >= startDate And NowDate <= enddDate Then
                    val = True
                    'End If
                    If val = True Then
                        'loginfunc()
                    Else
                        MessageBox.Show("Your trail period is ended.", "Message")
                        End
                    End If
                Else
                    MessageBox.Show("Your trail period is ended.", "Message")
                    End
                End If
            Else
                MessageBox.Show("Your trail period is ended.", "Message")
                End
            End If

            Select Case Environment.GetEnvironmentVariable("SessionName").ToUpper.Substring(0, 3)
                Case "ICA"
                    MsgBox(System.Environment.GetEnvironmentVariable("CLIENTNAME", EnvironmentVariableTarget.Process))
                    POSCounterName = System.Environment.GetEnvironmentVariable("CLIENTNAME", EnvironmentVariableTarget.Process)
                    POSCounterIPAddress = ""
                Case "RDP"
                    POSCounterName = Environment.MachineName
                    POSCounterIPAddress = GetIPAddress()
                Case "CON"
                    POSCounterName = Environment.MachineName
                    POSCounterIPAddress = GetIPAddress()
            End Select

            'Please find this code as hardcoded one. To run dynamically, please comment this line of code '''

            'POSCounterName = "PS39"
            'POSCounterName = "OPTX2020"
            'POSCounterName = "ITTESTPC"
            'POSCounterName = "WAFIPC"
            'POSCounterName = "GCITRIX"
            'POSCounterName = "ROBINSONSDFCPC"
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim stQuery As String
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            stQuery = "select COMP_CODE || '-' || COMP_SHORT_NAME as comcode from FM_COMPANY"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Company_Codes.Add(row.Item(0).ToString)
                i = i + 1
                count = count - 1
            End While
            'MySource_CustCodes.AddRange(Customer_Codes.ToArray)
            MySource_CompanyCodes.AddRange(Company_Codes.ToArray)
            cmbCompanyCode.AutoCompleteCustomSource = MySource_CompanyCodes
            cmbCompanyCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbCompanyCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "select  LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Location_Codes.Add(row.Item(0).ToString)
                i = i + 1
                count = count - 1
            End While
            MySource_LocationCodes.AddRange(Location_Codes.ToArray)
            cmbLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
            cmbLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtUserName.Select()
        Catch ex As Exception
            If ex.Message.GetHashCode = -590130319 Then
                MsgBox("TNS Adapter Error!", MsgBoxStyle.Critical, "Database Error")
                End
            ElseIf ex.Message.GetHashCode = 1199841023 Then
                MsgBox("TNS no listener!", MsgBoxStyle.Critical, "Database Error")
                End
            End If
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    '
    'Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    Try
    '        'If TestDecoding() Then
    '        '   MsgBox("IF PART")

    '        'Else
    '        '  MsgBox(licenseMessage)

    '        '   End
    '        'End If
    '        'POSCounterName = "PS39"

    '        Dim FILE_NAME As String = Application.ExecutablePath + "..\..\IPFILE.txt"
    '        MsgBox(FILE_NAME)
    '        If System.IO.File.Exists(FILE_NAME) = True Then

    '            Dim TextLine As String = ""
    '            Dim val As Boolean = False
    '            Dim objReader As New System.IO.StreamReader(FILE_NAME)
    '            Do While objReader.Peek() <> -1
    '                TextLine = objReader.ReadLine()
    '                MsgBox(TextLine)
    '            Loop
    '            objReader.Close()

    '            Dim dtst As Byte() = Convert.FromBase64String(TextLine)
    '            'MsgBox(dtst.ToArray)
    '            Dim decryteddate As String = System.Text.Encoding.UTF8.GetString(dtst).Split("|")(1).ToString()

    '            'Dim decryteddate1 As String = System.Text.Encoding.UTF8.GetString(dtst).Split("|")(1).ToString
    '            'MsgBox(decryteddate1)
    '            ' MsgBox(decryteddate)
    '            'Dim stDate = decryteddate.Split(",")(0).ToString
    '            Dim startDate = "1/01/2014"
    '            'MsgBox(startDate)
    '            'Dim endDate = decryteddate.Split(",")(1).ToString
    '            Dim enddDate = "24/07/2014"
    '            'MsgBox(enddDate)
    '            If startDate <> "" And enddDate <> "" Then
    '                Dim NowDate As String = Now.Date.ToString("dd/MM/yyyy")
    '                'Dim NowDate As Date
    '                If NowDate >= startDate And NowDate <= enddDate Then
    '                    val = True
    '                End If
    '                If val = True Then
    '                    'loginfunc()
    '                Else
    '                    MessageBox.Show("Your trail period is ended.", "Message")
    '                    End
    '                End If
    '            Else
    '                MessageBox.Show("Your trail period is ended.", "Message")
    '                End
    '            End If
    '        Else
    '            MessageBox.Show("Your trail period is ended.", "Message")
    '            End
    '        End If


    '        Select Case Environment.GetEnvironmentVariable("SessionName").ToUpper.Substring(0, 3)
    '            Case "ICA"
    '                'MsgBox(System.Environment.GetEnvironmentVariable("CLIENTNAME", EnvironmentVariableTarget.Process))
    '                POSCounterName = System.Environment.GetEnvironmentVariable("CLIENTNAME", EnvironmentVariableTarget.Process)
    '                POSCounterIPAddress = ""
    '            Case "RDP"
    '                POSCounterName = Environment.MachineName
    '                POSCounterIPAddress = GetIPAddress()
    '            Case "CON"
    '                POSCounterName = Environment.MachineName
    '                POSCounterIPAddress = GetIPAddress()
    '        End Select

    '        'Please find this code as hardcoded one. To run dynamically, please comment this line of code '''

    '        POSCounterName = "PS39"

    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '        Dim stQuery As String
    '        Dim ds As DataSet
    '        Dim row As System.Data.DataRow
    '        stQuery = "select COMP_CODE || '-' || COMP_SHORT_NAME as comcode from FM_COMPANY"
    '        ds = db.SelectFromTableODBC(stQuery)
    '        Dim count As Integer
    '        count = ds.Tables("Table").Rows.Count
    '        Dim i As Integer = 0
    '        While count > 0
    '            row = ds.Tables("Table").Rows.Item(i)
    '            Company_Codes.Add(row.Item(0).ToString)
    '            i = i + 1
    '            count = count - 1
    '        End While
    '        'MySource_CustCodes.AddRange(Customer_Codes.ToArray)
    '        MySource_CompanyCodes.AddRange(Company_Codes.ToArray)
    '        cmbCompanyCode.AutoCompleteCustomSource = MySource_CompanyCodes
    '        cmbCompanyCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    '        cmbCompanyCode.AutoCompleteSource = AutoCompleteSource.CustomSource

    '        stQuery = "select  LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
    '        ds = db.SelectFromTableODBC(stQuery)

    '        count = ds.Tables("Table").Rows.Count
    '        i = 0
    '        While count > 0
    '            row = ds.Tables("Table").Rows.Item(i)
    '            Location_Codes.Add(row.Item(0).ToString)
    '            i = i + 1
    '            count = count - 1
    '        End While
    '        MySource_LocationCodes.AddRange(Location_Codes.ToArray)
    '        cmbLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
    '        cmbLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    '        cmbLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource
    '        txtUserName.Select()
    '    Catch ex As Exception
    '        If ex.Message.GetHashCode = -590130319 Then
    '            MsgBox("TNS Adapter Error!", MsgBoxStyle.Critical, "Database Error")
    '            End
    '        ElseIf ex.Message.GetHashCode = 1199841023 Then
    '            MsgBox("TNS no listener!", MsgBoxStyle.Critical, "Database Error")
    '            End
    '        End If
    '        errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub
    '
    Private Sub butLoginCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLoginCancel.Click
        End
    End Sub
    Private Sub txtUserName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not txtUserName.Text = "" Then
                txtPassword.Select()
            End If
        ElseIf e.KeyData = Keys.Tab Then
            If Not txtUserName.Text = "" Then
                txtPassword.Select()
            End If
        End If
    End Sub
    Private Sub txtUserName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.LostFocus
        Try
            If txtUserName.Text = "" Then

                Exit Sub
            End If
            Dim stQuery As String = "SELECT COMP_CODE, COMP_NAME FROM FM_COMPANY WHERE COMP_FRZ_FLAG = 'N' AND COMP_CODE IN (SELECT MUC_COMP_CODE FROM MENU_USER_COMP WHERE MUC_USER_ID ='" & txtUserName.Text & "')"
            Dim ds As DataSet
            ds = db.SelectFromTableODBC(stQuery)
            If Not ds.Tables("Table").Rows.Count > 0 Then
                If Not txtUserName.Text = "" Then
                    MsgBox("Not a Valid User!", MsgBoxStyle.Critical, "Application Error")
                    txtUserName.Text = ""
                    txtUserName.Select()
                    txtPassword.Text = ""
                    cmbCompanyCode.Text = ""
                    cmbLocationCode.Text = ""
                    'cmbStaffCode.Text = ""
                End If
            Else
                'txtPassword.Focus()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub txtUserName_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtUserName.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.GotFocus
        If txtUserName.Text = "" Then
            txtUserName.Select()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not txtPassword.Text = "" Then
                cmbCompanyCode.Select()
            End If
        ElseIf e.KeyData = Keys.Tab Then
            If Not txtPassword.Text = "" Then
                cmbCompanyCode.Select()
            End If
        End If
    End Sub

    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.LostFocus
        Try
            If txtPassword.Text = "" Then
                Exit Sub
            End If
            Dim stQuery As String = "SELECT COMP_CODE, COMP_NAME FROM FM_COMPANY WHERE COMP_FRZ_FLAG = 'N' AND COMP_CODE IN (SELECT MUC_COMP_CODE FROM MENU_USER_COMP WHERE MUC_USER_ID ='" & txtUserName.Text & "')"
            Dim ds As DataSet
            ds = db.SelectFromTableODBC(stQuery)
            If Not ds.Tables("Table").Rows.Count > 0 Then
                If Not txtUserName.Text = "" Then
                    MsgBox("Please enter a valid UserName!", MsgBoxStyle.Critical, "Application Error")
                    txtUserName.Text = ""
                    txtUserName.Select()
                End If
            Else
                stQuery = "SELECT USER_FIELD_05 FROM MENU_USER WHERE USER_ID = '" & txtUserName.Text & "' AND USER_FIELD_05 ='" & txtPassword.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    If Not txtPassword.Text = "" Then
                        txtPassword.Text = ""
                        txtPassword.Focus()
                        MsgBox("Not a Valid Password!", MsgBoxStyle.Critical, "Application Error")
                        cmbCompanyCode.Text = ""
                        cmbLocationCode.Text = ""
                        'cmbStaffCode.Text = ""
                    End If
                Else
                    'cmbCompanyCode.Focus()
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbCompanyCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompanyCode.GotFocus
        If txtUserName.Text = "" Then
            txtUserName.Select()
        ElseIf txtPassword.Text = "" Then
            txtPassword.Select()
        End If
    End Sub

    Private Sub cmbCompanyCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCompanyCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not cmbCompanyCode.Text = "" Then
                cmbLocationCode.Select()
            End If
        ElseIf e.KeyData = Keys.Tab Then
            If Not cmbCompanyCode.Text = "" Then
                cmbLocationCode.Select()
            End If
        End If
    End Sub


    Private Sub cmbCompanyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompanyCode.LostFocus
        Try
            If cmbCompanyCode.Text = "" Then
                Exit Sub
            End If
            Dim stQuery As String = "SELECT COMP_CODE, COMP_NAME FROM FM_COMPANY WHERE COMP_FRZ_FLAG = 'N' AND COMP_CODE IN (SELECT MUC_COMP_CODE FROM MENU_USER_COMP WHERE MUC_USER_ID ='" & txtUserName.Text & "')"
            Dim ds As DataSet
            ds = db.SelectFromTableODBC(stQuery)
            If Not ds.Tables("Table").Rows.Count > 0 Then
                If Not txtUserName.Text = "" Then
                    MsgBox("Please enter a valid UserName!", MsgBoxStyle.Critical, "Application Error")
                    txtUserName.Text = ""
                    txtUserName.Select()
                End If
            Else
                stQuery = "SELECT USER_FIELD_05 FROM MENU_USER WHERE USER_ID = '" & txtUserName.Text & "' AND USER_FIELD_05='" & txtPassword.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)


                If Not ds.Tables("Table").Rows.Count > 0 Then
                    If Not txtPassword.Text = "" Then
                        txtPassword.Text = ""
                        txtPassword.Focus()
                        MsgBox("Please enter a valid Password!", MsgBoxStyle.Critical, "Application Error")
                    End If
                Else
                    Dim strCompArr() As String = cmbCompanyCode.Text.Split("-")
                    CompanyCode = strCompArr(0)
                    If Not Regex.IsMatch(CompanyCode, "^[0-9 ]+$") Then
                        MsgBox("Select a valid Company!")
                        cmbCompanyCode.Select()
                        Exit Sub
                    End If
                    stQuery = "select COMP_CODE || '-' || COMP_SHORT_NAME as comcode from FM_COMPANY where COMP_CODE=" & CompanyCode
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        cmbCompanyCode.Text = ""
                        cmbCompanyCode.Select()
                        MsgBox("Invalid Company Code!")
                        cmbLocationCode.Text = ""
                        'cmbStaffCode.Text = ""
                    Else
                        'cmbLocationCode.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbLocationCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocationCode.GotFocus
        If txtUserName.Text = "" Then
            txtUserName.Select()
        ElseIf txtPassword.Text = "" Then
            txtPassword.Select()
        ElseIf cmbCompanyCode.Text = "" Then
            cmbCompanyCode.Select()
        End If
    End Sub

    Private Sub cmbStaffCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtUserName.Text = "" Then
            txtUserName.Select()
        ElseIf txtPassword.Text = "" Then
            txtPassword.Select()
        ElseIf cmbCompanyCode.Text = "" Then
            cmbCompanyCode.Select()
        ElseIf cmbLocationCode.Text = "" Then
            cmbLocationCode.Select()
        End If
    End Sub

    Private Sub txtPassword_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPassword.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub cmbCompanyCode_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles cmbCompanyCode.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub cmbLocationCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbLocationCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not cmbLocationCode.Text = "" Then
                'cmbStaffCode.Select()
                butLogin.Select()
            End If
        ElseIf e.KeyData = Keys.Tab Then
            If Not cmbCompanyCode.Text = "" Then
                'cmbStaffCode.Select()
                butLogin.Select()
            End If
        End If
    End Sub

    Private Sub cmbLocationCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocationCode.LostFocus
        Try
            If cmbLocationCode.Text = "" Then
                Exit Sub
            End If
            Dim stQuery As String = "SELECT COMP_CODE, COMP_NAME FROM FM_COMPANY WHERE COMP_FRZ_FLAG = 'N' AND COMP_CODE IN (SELECT MUC_COMP_CODE FROM MENU_USER_COMP WHERE MUC_USER_ID ='" & txtUserName.Text & "')"
            Dim ds As DataSet
            ds = db.SelectFromTableODBC(stQuery)
            If Not ds.Tables("Table").Rows.Count > 0 Then
                If Not txtUserName.Text = "" Then
                    MsgBox("Please enter a valid UserName!", MsgBoxStyle.Critical, "Application Error")
                    txtUserName.Text = ""
                    txtUserName.Select()
                End If
            Else
                stQuery = "SELECT USER_FIELD_05 FROM MENU_USER WHERE USER_ID = '" & txtUserName.Text & "' AND USER_FIELD_05 ='" & txtPassword.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    If Not txtPassword.Text = "" Then
                        txtPassword.Text = ""
                        txtPassword.Focus()
                        MsgBox("Please enter a valid Password!", MsgBoxStyle.Critical, "Application Error")
                    End If
                Else
                    Dim strCompArr() As String = cmbCompanyCode.Text.Split("-")
                    CompanyCode = strCompArr(0)
                    If Not Regex.IsMatch(CompanyCode, "^[0-9 ]+$") Then
                        MsgBox("Please Select a valid Company!")
                        cmbCompanyCode.Select()
                        Exit Sub
                    End If
                    stQuery = "select COMP_CODE || '-' || COMP_SHORT_NAME as comcode from FM_COMPANY where COMP_CODE=" & CompanyCode
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        cmbCompanyCode.Text = ""
                        cmbCompanyCode.Select()
                        MsgBox("Please Select a valid Company!")
                    Else
                        Dim strLocArr() As String = cmbLocationCode.Text.Split("-")
                        Location_Code = strLocArr(0)

                        stQuery = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & Location_Code & "'"
                        ds = db.SelectFromTableODBC(stQuery)
                        If Not ds.Tables("Table").Rows.Count > 0 Then
                            cmbLocationCode.Text = ""
                            cmbLocationCode.Select()
                            MsgBox("Invalid Location Code!")
                            'cmbStaffCode.Text = ""
                        Else
                            stQuery = "select POSCNT_NO,OM_POS_COUNTER.ROWID from om_pos_counter where poscnt_locn_code='" & Location_Code & "' and poscnt_frz_flag_num = 2  and (POSCNT_COMPUTER_NAME='" + POSCounterName + "' or POSCNT_IP_ADDRESS= '')"
                            errLog.WriteToErrorLog("LOGIN", stQuery, "")
                            ds = db.SelectFromTableODBC(stQuery)

                            If Not ds.Tables("Table").Rows.Count > 0 Then
                                MsgBox("Unable to identify Machine!", MsgBoxStyle.Critical, "Application Error")
                                cmbLocationCode.Focus()
                                'cmbStaffCode.Text = ""
                            Else
                                POSCounterNumber = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                                stQuery = "SELECT SM_CODE || '-' || SM_NAME as salemancode FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
                                ds = db.SelectFromTableODBC(stQuery)
                                If Not ds.Tables("Table").Rows.Count > 0 Then
                                    'cmbStaffCode.Text = ""
                                Else
                                    'cmbStaffCode.Text = ""
                                    Staff_Codes.Clear()
                                    Dim count As Integer
                                    Dim i As Integer
                                    Dim row As System.Data.DataRow
                                    count = ds.Tables("Table").Rows.Count
                                    i = 0
                                    While count > 0
                                        row = ds.Tables("Table").Rows.Item(i)
                                        Staff_Codes.Add(row.Item(0).ToString)
                                        i = i + 1
                                        count = count - 1
                                    End While
                                    'cmbStaffCode.Text = ""
                                    MySource_StaffCodes.AddRange(Staff_Codes.ToArray)
                                    'cmbStaffCode.AutoCompleteCustomSource = MySource_StaffCodes
                                    'cmbStaffCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                                    'cmbStaffCode.AutoCompleteSource = AutoCompleteSource.CustomSource
                                    'cmbStaffCode.Focus()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbLocationCode_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles cmbLocationCode.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub butLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLogin.Click
        Try
            Dim ds As DataSet
            Dim stQuery As String
            If cmbLocationCode.Text <> "System.Data.DataRowView" And cmbLocationCode.Text <> " " Then
                Dim strArr() As String

                strArr = cmbLocationCode.Text.Split("-")
                Location_Code = strArr(0)
                'Location_Name = strArr(1)

                stQuery = "select POSCNT_NO,OM_POS_COUNTER.ROWID from om_pos_counter where poscnt_locn_code='" & Location_Code & "' and poscnt_frz_flag_num = 2  and (POSCNT_COMPUTER_NAME='" + POSCounterName + "' or POSCNT_IP_ADDRESS= '')"
                errLog.WriteToErrorLog("LOGIN", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("Unable to identify Machine!", MsgBoxStyle.Critical, "Application Error")
                    cmbLocationCode.Focus()
                    Exit Sub
                Else

                End If
            End If

            Dim dt As New DataSet
            dt.Dispose()


            stQuery = "select USER_ID from menu_user where USER_ID ='" & txtUserName.Text & "' and USER_FIELD_05 = '" & txtPassword.Text & "'"
            errLog.WriteToErrorLog("USER_ID menu_user", stQuery, "")
            dt = db.SelectFromTableODBC(stQuery)
            Dim countrs As Integer
            countrs = dt.Tables("Table").Rows.Count
            If countrs = 0 Then
                MsgBox("Invalid Username or Password")
            ElseIf cmbCompanyCode.Text = "" Then
                MsgBox("Please Select the Company Code")
            ElseIf cmbLocationCode.Text = "" Then
                MsgBox("Please Select the Location Code")
                'ElseIf cmbStaffCode.Text = "" Then
                '    MsgBox("Staff Code not available")
            Else
                'Dim strUsrArr() As String = cmbStaffCode.Text.Split("-")
                'If strUsrArr.Count > 1 Then

                '    Staff_Code = strUsrArr(0)
                '    Staff_Name = strUsrArr(1)
                'Else
                '    MsgBox("Salesman code not valid!")
                '    cmbStaffCode.Text = ""
                '    cmbStaffCode.Focus()
                '    Exit Sub
                'End If


                stQuery = "select USER_ID from menu_user where USER_ID ='" & txtUserName.Text & "' and USER_FIELD_05 = '" & txtPassword.Text & "' and '" & Location_Code & "' in (SELECT ula_locn_code from user_location_access where ula_u_id = '" & txtUserName.Text & "' and ULA_PRIV_YN = 'Y')"
                errLog.WriteToErrorLog("Location Privileges", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("User does not have privilege to access this location!")
                    Exit Sub
                End If

                stQuery = "SELECT SM_CODE || '-' || SM_NAME as salemancode FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
                errLog.WriteToErrorLog("salemancode", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("Salesman not found for this location!")
                    'cmbStaffCode.Text = ""
                    'cmbStaffCode.Focus()
                    Exit Sub
                End If

                Dim strCompArr() As String = cmbCompanyCode.Text.Split("-")
                CompanyCode = strCompArr(0)

                'Dim strLocArr() As String = cmbLocationCode.Text.Split("-")
                'Location_Code = strLocArr(0)
                'Location_Name = strLocArr(1)

                LogonUser = txtUserName.Text
                'Dim locname As String = strLocArr(1)


                stQuery = "SELECT  POS_REPORT_HEAD_ID ,POS_REPORT_HEAD_TITLE FROM  POS_REPORT_HEAD A, USER_MENU_ACCESS B WHERE UMA_UG_ID = (SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" + LogonUser + "') AND B.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND B.UMA_PRIV_YN='Y'"
                errLog.WriteToErrorLog("USER_MENU_ACCESS", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer = ds.Tables("Table").Rows.Count
                Dim i As Integer
                Dim posReportHeadTitles As New ArrayList
                While count > 0
                    posReportHeadTitles.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While
                If posReportHeadTitles.Count > 0 Then
                    If posReportHeadTitles.Contains("Transaction") = True Then
                        Home.TransactionToolStripMenuItem.Enabled = True
                        stQuery = "SELECT distinct D.PSD_REPORT_TITLE FROM POS_REPORT_HEAD A, USER_MENU_ACCESS B,USER_SUBMENU_ACCESS C,POS_REPORT_DETAIL D WHERE UMA_UG_ID = (SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" + LogonUser + "') AND B.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND B.UMA_PRIV_YN='Y' AND C.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND D.PSD_REPORT_HEAD_ID=A.POS_REPORT_HEAD_ID AND A.POS_REPORT_HEAD_TITLE='Transaction' and C.USMA_PRIV_YN='Y'"
                        errLog.WriteToErrorLog("Transaction", stQuery, "")
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        i = 0
                        Dim transarray As New ArrayList
                        While count > 0
                            transarray.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                            count = count - 1
                            i = i + 1
                        End While
                    Else
                        Home.TransactionToolStripMenuItem.Enabled = False
                    End If
                    If posReportHeadTitles.Contains("Settings") = True Then
                        Home.SettingsToolStripMenuItem.Enabled = True
                        'stQuery = "SELECT distinct D.PSD_REPORT_TITLE FROM POS_REPORT_HEAD A, USER_MENU_ACCESS B,USER_SUBMENU_ACCESS C,POS_REPORT_DETAIL D WHERE UMA_UG_ID = (SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" + LogonUser + "') AND B.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND B.UMA_PRIV_YN='Y' AND C.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND D.PSD_REPORT_HEAD_ID=A.POS_REPORT_HEAD_ID AND A.POS_REPORT_HEAD_TITLE='Settings' and C.USMA_PRIV_YN='Y'"
                        stQuery = "SELECT DISTINCT USMA_SUB_REPORT_ID,D.PSD_REPORT_TITLE,USMA_PRIV_YN FROM POS_REPORT_HEAD A,USER_SUBMENU_ACCESS B, USER_MENU_ACCESS C,POS_REPORT_DETAIL D WHERE A.POS_REPORT_HEAD_TITLE='Settings' AND B.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND  C.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND C.UMA_REPORT_ID =B.USMA_REPORT_ID AND B.USMA_SUB_REPORT_ID=D.PSD_REPORT_ID AND B.USMA_UG_ID=(SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" & LogonUser & "') AND C.UMA_PRIV_YN='Y'"
                        errLog.WriteToErrorLog("Setting", stQuery, "")
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        i = 0
                        Dim settingsarray As New ArrayList
                        Dim settings_values As New Dictionary(Of String, String)
                        While count > 0
                            settings_values.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString, ds.Tables("Table").Rows.Item(i).Item(2).ToString)
                            'settingsarray.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                            count = count - 1
                            i = i + 1
                        End While
                        If settings_values("Location Settings") = "Y" Then
                            AdminSettings.btnSalesOrders.Enabled = True
                            CounterSettings.btnLocationSettings.Enabled = True
                        Else
                            AdminSettings.btnSalesOrders.Enabled = False
                            CounterSettings.btnLocationSettings.Enabled = False
                        End If
                        If settings_values("Counter Settings") = "Y" Then
                            AdminSettings.btnCounterMaster.Enabled = True
                            CounterSettings.btnCounterSettings.Enabled = True
                        Else
                            AdminSettings.btnCounterMaster.Enabled = False
                            CounterSettings.btnCounterSettings.Enabled = False
                        End If
                    Else
                        Home.SettingsToolStripMenuItem.Enabled = False
                    End If
                    If posReportHeadTitles.Contains("Masters") = True Then
                        Home.MastersToolStripMenuItem.Enabled = True
                        'stQuery = "SELECT distinct D.PSD_REPORT_TITLE FROM POS_REPORT_HEAD A, USER_MENU_ACCESS B,USER_SUBMENU_ACCESS C,POS_REPORT_DETAIL D WHERE UMA_UG_ID = (SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" + LogonUser + "') AND B.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND B.UMA_PRIV_YN='Y' AND C.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND D.PSD_REPORT_HEAD_ID=A.POS_REPORT_HEAD_ID AND A.POS_REPORT_HEAD_TITLE='Masters' and C.USMA_PRIV_YN='Y'"
                        stQuery = "SELECT DISTINCT USMA_SUB_REPORT_ID,D.PSD_REPORT_TITLE,USMA_PRIV_YN FROM POS_REPORT_HEAD A,USER_SUBMENU_ACCESS B, USER_MENU_ACCESS C,POS_REPORT_DETAIL D WHERE A.POS_REPORT_HEAD_TITLE='Masters' AND B.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND  C.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND C.UMA_REPORT_ID =B.USMA_REPORT_ID AND B.USMA_SUB_REPORT_ID=D.PSD_REPORT_ID AND B.USMA_UG_ID=(SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" & LogonUser & "') AND C.UMA_PRIV_YN='Y'"
                        errLog.WriteToErrorLog("Setting", stQuery, "")
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        i = 0
                        Dim mastersarray As New ArrayList
                        Dim master_values As New Dictionary(Of String, String)
                        While count > 0
                            'mastersarray.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                            master_values.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString, ds.Tables("Table").Rows.Item(i).Item(2).ToString)
                            count = count - 1
                            i = i + 1
                        End While
                        If master_values("Shift Master") = "Y" Then
                            SettingsShiftMaster.btnShift_SalesOrders.Enabled = True
                            SettingsCounterMaster.btnSalesOrders.Enabled = True
                            SettingsDenominationMaster.btnShiftmaster.Enabled = True
                            SettingsSalesmanMaster.btnSalesOrders.Enabled = True
                            SettingsPaymentMaster.btnSalesOrders.Enabled = True
                        Else
                            SettingsShiftMaster.btnShift_SalesOrders.Enabled = False
                            SettingsCounterMaster.btnSalesOrders.Enabled = False
                            SettingsDenominationMaster.btnShiftmaster.Enabled = False
                            SettingsSalesmanMaster.btnSalesOrders.Enabled = False
                            SettingsPaymentMaster.btnSalesOrders.Enabled = False
                        End If
                        If master_values("Counter Master") = "Y" Then
                            SettingsShiftMaster.btnCounterMaster.Enabled = True
                            SettingsCounterMaster.btnCounterMaster.Enabled = True
                            SettingsDenominationMaster.btnCounterMaster.Enabled = True
                            SettingsSalesmanMaster.btnCounterMaster.Enabled = True
                            SettingsPaymentMaster.btnCounterMaster.Enabled = True
                        Else
                            SettingsShiftMaster.btnCounterMaster.Enabled = False
                            SettingsCounterMaster.btnCounterMaster.Enabled = False
                            SettingsDenominationMaster.btnCounterMaster.Enabled = False
                            SettingsSalesmanMaster.btnCounterMaster.Enabled = False
                            SettingsPaymentMaster.btnCounterMaster.Enabled = False
                        End If
                        If master_values("Payment Master") = "Y" Then
                            SettingsShiftMaster.btnPaymentMaster.Enabled = True
                            SettingsCounterMaster.btnPaymentMaster.Enabled = True
                            SettingsDenominationMaster.btnPaymentMaster.Enabled = True
                            SettingsSalesmanMaster.btnPaymentMaster.Enabled = True
                            SettingsPaymentMaster.btnPaymentMaster.Enabled = True
                        Else
                            SettingsShiftMaster.btnPaymentMaster.Enabled = False
                            SettingsCounterMaster.btnPaymentMaster.Enabled = False
                            SettingsDenominationMaster.btnPaymentMaster.Enabled = False
                            SettingsSalesmanMaster.btnPaymentMaster.Enabled = False
                            SettingsPaymentMaster.btnPaymentMaster.Enabled = False
                        End If
                        If master_values("Salesman Master") = "Y" Then
                            SettingsShiftMaster.btnSalesmanMaster.Enabled = True
                            SettingsCounterMaster.btnSalesmanMaster.Enabled = True
                            SettingsDenominationMaster.btnSalesmanMaster.Enabled = True
                            SettingsSalesmanMaster.btnSalesmanMaster.Enabled = True
                            SettingsPaymentMaster.btnSalesmanMaster.Enabled = True
                        Else
                            SettingsShiftMaster.btnSalesmanMaster.Enabled = False
                            SettingsCounterMaster.btnSalesmanMaster.Enabled = False
                            SettingsDenominationMaster.btnSalesmanMaster.Enabled = False
                            SettingsSalesmanMaster.btnSalesmanMaster.Enabled = False
                            SettingsPaymentMaster.btnSalesmanMaster.Enabled = False
                        End If
                        If master_values("Denomination Master") = "Y" Then
                            SettingsShiftMaster.btnDenominationMaster.Enabled = True
                            SettingsCounterMaster.btnDenominationMaster.Enabled = True
                            SettingsDenominationMaster.btnDenominationMaster.Enabled = True
                            SettingsSalesmanMaster.btnDenominationMaster.Enabled = True
                            SettingsPaymentMaster.btnDenominationMaster.Enabled = True
                        Else
                            SettingsShiftMaster.btnDenominationMaster.Enabled = False
                            SettingsCounterMaster.btnDenominationMaster.Enabled = False
                            SettingsDenominationMaster.btnDenominationMaster.Enabled = False
                            SettingsSalesmanMaster.btnDenominationMaster.Enabled = False
                            SettingsPaymentMaster.btnDenominationMaster.Enabled = False
                        End If
                    Else
                        Home.MastersToolStripMenuItem.Enabled = False
                    End If

                    If posReportHeadTitles.Contains("Reports") = True Then
                        Home.ReportsToolStripMenuItem.Enabled = True
                        'stQuery = "SELECT distinct D.PSD_REPORT_TITLE FROM POS_REPORT_HEAD A, USER_MENU_ACCESS B,USER_SUBMENU_ACCESS C,POS_REPORT_DETAIL D WHERE UMA_UG_ID = (SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" + LogonUser + "') AND B.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND B.UMA_PRIV_YN='Y' AND C.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND D.PSD_REPORT_HEAD_ID=A.POS_REPORT_HEAD_ID AND A.POS_REPORT_HEAD_TITLE='Reports' and C.USMA_PRIV_YN='Y'"
                        stQuery = "SELECT DISTINCT USMA_SUB_REPORT_ID,D.PSD_REPORT_TITLE,USMA_PRIV_YN FROM POS_REPORT_HEAD A,USER_SUBMENU_ACCESS B, USER_MENU_ACCESS C,POS_REPORT_DETAIL D WHERE A.POS_REPORT_HEAD_TITLE='Reports' AND B.USMA_REPORT_ID=A.POS_REPORT_HEAD_ID AND  C.UMA_REPORT_ID = A.POS_REPORT_HEAD_ID AND C.UMA_REPORT_ID =B.USMA_REPORT_ID AND B.USMA_SUB_REPORT_ID=D.PSD_REPORT_ID AND B.USMA_UG_ID=(SELECT USER_GROUP_ID FROM MENU_USER WHERE USER_ID = '" & LogonUser & "') AND C.UMA_PRIV_YN='Y'"
                        errLog.WriteToErrorLog("Setting", stQuery, "")
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        i = 0
                        Dim mastersarray As New ArrayList
                        Dim reports_values As New Dictionary(Of String, String)
                        While count > 0
                            reports_values.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString, ds.Tables("Table").Rows.Item(i).Item(2).ToString)
                            'mastersarray.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                            count = count - 1
                            i = i + 1
                        End While
                        If reports_values("End Of the Day Report") = "Y" Then
                            Home.EndOfTheDayReportToolStripMenuItem.Enabled = True
                        Else
                            Home.EndOfTheDayReportToolStripMenuItem.Enabled = False
                        End If
                        If reports_values("Gift Voucher Report") = "Y" Then
                            Home.GiftVoucherReportToolStripMenuItem.Enabled = True
                        Else
                            Home.GiftVoucherReportToolStripMenuItem.Enabled = False
                        End If
                        If reports_values("Daily Transaction Report") = "Y" Then
                            Home.DailySalesTransactionReportToolStripMenuItem.Enabled = True
                        Else
                            Home.DailySalesTransactionReportToolStripMenuItem.Enabled = False
                        End If
                        If reports_values("Referral Report") = "Y" Then
                            Home.ReferralReportToolStripMenuItem.Enabled = True
                        Else
                            Home.ReferralReportToolStripMenuItem.Enabled = False
                        End If
                        If reports_values("Royalty Report") = "Y" Then
                            Home.RoyaltyReportToolStripMenuItem.Enabled = True
                        Else
                            Home.RoyaltyReportToolStripMenuItem.Enabled = False
                        End If
                    Else
                        Home.ReportsToolStripMenuItem.Enabled = False
                    End If

                End If

                stQuery = "SELECT POCS_KEY, POCS_VALUE FROM OM_POS_OPTIONS_COUNTER_SETUP WHERE POCS_COMP_CODE = '" + CompanyCode + "' AND POCS_LOCN_CODE = '" + Location_Code + "' AND POCS_COUNTER_CODE = '" + POSCounterNumber + "'"
                errLog.WriteToErrorLog("OM_POS_OPTIONS_COUNTER_SETUP", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                Dim row As System.Data.DataRow
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Setup_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While

                stQuery = "SELECT  POLS_KEY, POLS_VALUE FROM OM_POS_OPTIONS_LOCATION_SETUP WHERE POLS_COMP_CODE = '" + CompanyCode + "' AND  POLS_LOCN_CODE = '" + Location_Code + "'"
                errLog.WriteToErrorLog("OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Setup_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While

                stQuery = "SELECT  LOCN_FLEX_19,LOCN_FLEX_20 FROM OM_LOCATION WHERE LOCN_CODE = '" + Location_Code + "'"
                errLog.WriteToErrorLog("OM_LOCATION LOGO", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    If row.Item(0).ToString = "Y" Then
                        logoYN = True
                        locationLogo = row.Item(1).ToString
                    Else
                        logoYN = False
                        locationLogo = ""
                    End If
                    count = count - 1
                    i = i + 1
                End While

                Dim folderExists As Boolean = Directory.Exists(Application.StartupPath & "\LOGOS")
                If Not folderExists Then
                    Directory.CreateDirectory(Application.StartupPath & "\LOGOS")
                End If

                Home.Show()
                Me.Hide()

            End If

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbStaffCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            'If Not cmbStaffCode.Text = "" Then
            '    butLogin.Select()
            'End If
        ElseIf e.KeyData = Keys.Tab Then
            'If Not cmbStaffCode.Text = "" Then
            '    butLogin.Select()
            'End If
        End If
    End Sub

    Private Sub cmbStaffCode_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub cmbLocationCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocationCode.TextChanged

    End Sub


End Class