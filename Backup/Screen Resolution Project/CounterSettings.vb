Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D

Public Class CounterSettings
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim Customer_Code As New List(Of String)
    Dim Company_Codes As New List(Of String)
    Dim Payment_Type As New List(Of String)
    Dim Counter_Codes As New List(Of String)
    Dim AdvPayment_Codes As New List(Of String)
    Dim PriceList_Codes As New List(Of String)

    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_CompanyCodes As New AutoCompleteStringCollection()
    Dim MySource_PaymentType As New AutoCompleteStringCollection()
    Dim MySource_CustomerCodes As New AutoCompleteStringCollection()
    Dim MySource_PriceListCodes As New AutoCompleteStringCollection()
    Dim MySource_CounterCodes As New AutoCompleteStringCollection()
    Dim MySource_AdvPaymentCodes As New AutoCompleteStringCollection()
    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Dock = DockStyle.Fill
            SetResolution()

            lstviewCountSettings.Columns.Add("SNo", lblCountSNo.Width - 5, HorizontalAlignment.Center)
            lstviewCountSettings.Columns.Add("Company Code", lblCountSetCompCode.Width, HorizontalAlignment.Center)
            lstviewCountSettings.Columns.Add("Company Name", lblCountSetCompName.Width, HorizontalAlignment.Left)
            lstviewCountSettings.Columns.Add("Location Code", lblCountSetLocCode.Width, HorizontalAlignment.Center)
            lstviewCountSettings.Columns.Add("Location Name", lblCountSetLocName.Width, HorizontalAlignment.Left)
            lstviewCountSettings.Columns.Add("Counter Code", lblCountSetCountCode.Width - 20, HorizontalAlignment.Center)

            lstviewCountSettings.View = View.Details
            lstviewCountSettings.GridLines = True
            lstviewCountSettings.FullRowSelect = True

            load_AllCountSettings()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub load_AllCountSettings()
        Try
            lstviewCountSettings.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select POCS_COMP_CODE,COMP_NAME,POCS_LOCN_CODE,LOCN_NAME,POCS_COUNTER_CODE from OM_POS_OPTIONS_COUNTER_SETUP,OM_LOCATION,OM_POS_COUNTER,FM_COMPANY where POCS_LOCN_CODE = LOCN_CODE and POCS_COUNTER_CODE = POSCNT_NO and POCS_COMP_CODE = COMP_CODE group by POCS_LOCN_CODE,LOCN_NAME,POCS_COUNTER_CODE,POCS_COMP_CODE,COMP_NAME order by POCS_LOCN_CODE,POCS_COUNTER_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewCountSettings.Items.Add(i + 1)
                lstviewCountSettings.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewCountSettings.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewCountSettings.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewCountSettings.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewCountSettings.Items(i).SubItems.Add(row.Item(4).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub SetResolution()
        Try
            ' set resolution sub checks all the controls on the screen. Containers (tabcontrol, panel, ‘groupbox, tablelayoutpanel) do not resize on general control search for the form – so ‘they have to be done separate by name

            Dim perX, perY As Double, prvheight, prvWidth As Int32
            Dim shoAdd As Short

            Dim desktopSize As Size = Windows.Forms.SystemInformation.PrimaryMonitorSize
            prvheight = desktopSize.Height
            prvWidth = desktopSize.Width
            Dim p_shoWhatSize As Double
            ' in Windows 7, in the display section of the control panel, a user can ‘be set to see their screen larger – the settings are 100%, 125%, and ‘150%. In my programs preferences, I allow my software user to select ‘if they are using the 125% or the 150% settings. I set the global ‘p_shoWhatSize (short) varible to 1 for 125% and 2 for 150% screen. ‘This section ajusts for this

            If p_shoWhatSize = 1 Then
                prvheight = prvheight * 0.8
                prvWidth = prvWidth * 0.8
            End If
            If p_shoWhatSize = 2 Then
                prvheight = prvheight * 0.6666
                prvWidth = prvWidth * 0.6666
            End If

            ' the development resolution for my project is 1024 x 768 – change this ‘to your development resolution
            ' get new 'ratio' for screen
            perX = prvWidth / 1024
            perY = prvheight / 768

            ' listboxes don’t resize vertically correctly for all resolutions due ‘to the font size. shoAdd is used to ‘tweek’ the size of the list ‘boxes to help adjust for this – requires some testing on your screens ‘in different resolutions. I have some set at 10 and some as high as ‘14.
            If prvheight > 768 Then shoAdd = Int((prvheight - 768) / 12)

            Dim shoFont As Short

            ' if res is 1024 x 768 then perX and PerY will equal 1
            If perX <> 1 Or perY <> 1 Then
                For Each ctl As Control In Me.Controls

                    ' if you change the fonts of panels or groupbox containers, it messes
                    ' with the controls in those containers. Therefore, I skip the font 
                    ' resize for these
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next ctl


                For Each ctl As Control In pnlBottomHolder.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next


                For Each ctl As Control In pnlButtonHolder.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next


                For Each ctl As Control In pnlSet_shift.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next

                For Each ctl As Control In pnl_counter.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next


                For Each ctl As Control In pnlCounterMaster.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next


                settingsType = "Counter Settings"


                For Each ctl As Control In pnlCounterSetgAdd.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next

                For Each ctl As Control In PnlCtrSetg_Edit.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next


                ' if you are not maximizing your screen afterwards, then include this code
                Me.Top = (prvheight / 2) - (Me.Height / 2)
                Me.Left = (prvWidth / 2) - (Me.Width / 2)
            Else
                
                settingsType = "Counter Settings"
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationSettings.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            AdminSettings.MdiParent = Home
            AdminSettings.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnctrSettingsHome.Click
        Try
            If settingsType = "Counter Settings" Then
                pnlCounterSetgAdd.Hide()
                PnlCtrSetg_Edit.Hide()
                pnlCounterSetgAdd.SendToBack()
                PnlCtrSetg_Edit.SendToBack()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnctrSettingsEdit.Click

        Try
            If pnlCounterSetgAdd.Visible Then
                btnCounterAddCancel_Click(sender, e)
            End If
            If lstviewCountSettings.SelectedItems.Count > 0 Then
                callEditCountSettings()
            Else
                MsgBox("Please Select a row")
            End If

            '
        Catch ex As Exception
            MsgBox(ex.StackTrace)
        End Try

    End Sub


    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnctrSettingsDelete.Click
        Try
            If PnlCtrSetg_Edit.Visible Then
                btnCtrSettEditCancel_Click(sender, e)
            End If

            If Not lstviewCountSettings.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
            Else
                Dim stQuery As String

                stQuery = " Delete from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE='" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and POCS_COUNTER_CODE='" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewCountSettings.SelectedItems.Clear()
                load_AllCountSettings()
            End If


        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnCounterEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = PnlCtrSetg_Edit.Height
            While i > 0
                PnlCtrSetg_Edit.Height = PnlCtrSetg_Edit.Height - 1
                PnlCtrSetg_Edit.Location = New Point(lblCountSNo.Location.X, PnlCtrSetg_Edit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While
            PnlCtrSetg_Edit.Visible = False
            PnlCtrSetg_Edit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub

    Private Sub txtCounterAddLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettLoccode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCtrSettLoccode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettLocName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Counter_Codes.Clear()
                stQuery = "select POSCNT_NO from OM_POS_COUNTER where poscnt_locn_code = '" & txtCtrSettLoccode.Text & "'"
                errLog.WriteToErrorLog("POSCNT_NO OM_POS_COUNTER", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow
                count = ds.Tables("Table").Rows.Count
                i = 0
                Counter_Codes.Clear()
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Counter_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While

                MySource_CounterCodes.AddRange(Counter_Codes.ToArray)
                txtCtrSettCounterCode.AutoCompleteCustomSource = MySource_CounterCodes
                txtCtrSettCounterCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtCtrSettCounterCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            Else
                txtCtrSettLocName.Text = ""
                txtCtrSettCounterCode.AutoCompleteMode = AutoCompleteMode.None
                txtCtrSettCounterCode.Text = ""

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub txt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettCompcode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select COMP_NAME from FM_COMPANY  WHERE COMP_CODE='" & txtCtrSettCompcode.Text & "'"


            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettCompName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCtrSettCompName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddSave.Click
        Try
            If txtCtrSettCompcode.Text = "" Then
                MsgBox("Enter Company Code!")
                Exit Sub
            ElseIf txtCtrSettCompName.Text = "" Then
                MsgBox("Enter  valid Company Name!")
                Exit Sub
            ElseIf txtCtrSettLoccode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            ElseIf txtCtrSettLocName.Text = "" Then
                MsgBox("Enter Valid Location Name!")
                Exit Sub
            ElseIf txtCtrSettCounterCode.Text = "" Then
                MsgBox("Enter Counter Code!")
                Exit Sub
            ElseIf txtCtrSettCounterName.Text = "" Then
                MsgBox("Enter Valid Counter Name!")
                Exit Sub
            ElseIf Not txtCtrSettLocName.Text = "" And Not txtCtrSettCounterName.Text = "" Then
                Dim stQuery As String
                Dim ds As DataSet
                stQuery = "select distinct POCS_LOCN_CODE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE = '" & txtCtrSettLoccode.Text & "' and POCS_COUNTER_CODE= '" & txtCtrSettCounterCode.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    If txtCrtSettCustCode.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','CUST_CODE','" & txtCrtSettCustCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettPLCode.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','PL_CODE','" & txtCrtSettPLCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettAdvPCCode.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','ADV_PAY_CODE','" & txtCrtSettAdvPCCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettPCCode.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','PAY_CODE','" & txtCrtSettPCCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettWL1.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','LINE_DISP_WL_1','" & txtCrtSettWL1.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettWL2.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','LINE_DISP_WL_2','" & txtCrtSettWL2.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettTL1.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','LINE_DISP_TL_1','" & txtCrtSettTL1.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If

                    If txtCrtSettTL2.Text <> "" Then
                        stQuery = "INSERT INTO OM_POS_OPTIONS_COUNTER_SETUP(POCS_COMP_CODE,POCS_LOCN_CODE,POCS_COUNTER_CODE,POCS_KEY,POCS_VALUE,POCS_CR_UID,POCS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCtrSettCompcode.Text & "','" & txtCtrSettLoccode.Text & "','" & txtCtrSettCounterCode.Text & "','LINE_DISP_TL_2','" & txtCrtSettTL2.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    End If


                    MsgBox("Counter Settings Saved Successfully." & vbNewLine & "The Changes will be effective in the next login.", MsgBoxStyle.Information)

                    load_AllCountSettings()
                Else
                    MsgBox("Counter settings already exists!")
                    Exit Sub
                End If
            End If
            Dim i As Integer = pnlCounterSetgAdd.Height
            While i > 0
                pnlCounterSetgAdd.Height = pnlCounterSetgAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While
            pnlCounterSetgAdd.Visible = False
            pnlCounterSetgAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddCancel.Click
        Try
            Dim i As Integer = pnlCounterSetgAdd.Height
            While i > 0
                pnlCounterSetgAdd.Height = pnlCounterSetgAdd.Height - 1
                pnlCounterSetgAdd.Location = New Point(lblCountSNo.Location.X, pnlCounterSetgAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlCounterSetgAdd.Visible = False
            pnlCounterSetgAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try
    End Sub

    Private Sub lstviewCounterMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewCountSettings.DoubleClick

        callEditCountSettings()

    End Sub

    Private Sub callEditCountSettings()
        txtCtrSettEditCustCode.Text = ""
        txtCtrSettEditCustName.Text = ""
        txtCtrSettEditPLCode.Text = ""
        txtCtrSettEditPLName.Text = ""
        txtCtrSettEditPLCurCode.Text = ""
        txtCtrSettEditAdvPCCode.Text = ""
        txtCtrSettEditAdvPCName.Text = ""
        txtCtrSettEditPayCode.Text = ""
        txtCtrSettEditPCName.Text = ""
        txtCrtSettEditTL1.Text = ""
        txtCrtSettEditTL2.Text = ""
        txtCrtSettEditWL1.Text = ""
        txtCrtSettEditWL2.Text = ""

        Dim stQuery As String
        Dim ds As DataSet
        If lstviewCountSettings.SelectedItems.Count > 0 Then

            Customer_Code.Clear()

            Dim j As Integer
            Dim count As Integer
            Dim row As System.Data.DataRow
            'stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
            stQuery = "SELECT CUST_CODE,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1)"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            j = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(j)
                Customer_Code.Add(row.Item(0).ToString)
                j = j + 1
                count = count - 1
            End While

            MySource_CustomerCodes.AddRange(Customer_Code.ToArray)
            txtCtrSettEditCustCode.AutoCompleteCustomSource = MySource_CustomerCodes
            txtCtrSettEditCustCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtCtrSettEditCustCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            PriceList_Codes.Clear()
            'stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
            stQuery = "SELECT DISTINCT PL_CODE, PL_NAME, PL_CURR_CODE FROM OM_PRICE_LIST WHERE PL_CUST_PL_NUM = 2 AND PL_COMP_CODE = '001' AND PL_FRZ_FLAG_NUM = 2"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            'MsgBox(count)
            j = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(j)
                PriceList_Codes.Add(row.Item(0).ToString)
                j = j + 1
                count = count - 1
            End While
            MySource_PriceListCodes.AddRange(PriceList_Codes.ToArray)
            txtCtrSettEditPLCode.AutoCompleteCustomSource = MySource_PriceListCodes
            txtCtrSettEditPLCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtCtrSettEditPLCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            Payment_Type.Clear()

            stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
            'stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            j = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(j)
                Payment_Type.Add(row.Item(0).ToString)
                j = j + 1
                count = count - 1
            End While
            MySource_PaymentType.AddRange(Payment_Type.ToArray)
            txtCtrSettEditPayCode.AutoCompleteCustomSource = MySource_PaymentType
            txtCtrSettEditPayCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtCtrSettEditPayCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            AdvPayment_Codes.Clear()

            stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
            'stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            j = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(j)
                AdvPayment_Codes.Add(row.Item(0).ToString)
                j = j + 1
                count = count - 1
            End While
            MySource_AdvPaymentCodes.AddRange(AdvPayment_Codes.ToArray)
            txtCtrSettEditAdvPCCode.AutoCompleteCustomSource = MySource_AdvPaymentCodes
            txtCtrSettEditAdvPCCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtCtrSettEditAdvPCCode.AutoCompleteSource = AutoCompleteSource.CustomSource


            If Not PnlCtrSetg_Edit.Visible Then
                PnlCtrSetg_Edit.BringToFront()
                PnlCtrSetg_Edit.Height = lblCountSNo.Height + lstviewCountSettings.Height + 1
                Dim i As Integer = PnlCtrSetg_Edit.Height
                While i >= lblCountSNo.Location.Y
                    PnlCtrSetg_Edit.Location = New Point(lblCountSNo.Location.X, i)
                    PnlCtrSetg_Edit.Show()
                    Threading.Thread.Sleep(0.5)
                    i = (i - 1)
                End While

            End If

            'Customer_Code.Clear()

            'Dim j As Integer
            'Dim count As Integer
            'Dim row As System.Data.DataRow
            ''stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
            'stQuery = "SELECT CUST_CODE,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1)"
            'ds = db.SelectFromTableODBC(stQuery)
            'count = ds.Tables("Table").Rows.Count
            'j = 0
            'While count > 0
            '    row = ds.Tables("Table").Rows.Item(j)
            '    Customer_Code.Add(row.Item(0).ToString)
            '    j = j + 1
            '    count = count - 1
            'End While
            'MsgBox(Customer_Code.Count)
            'MySource_CustomerCodes.AddRange(Customer_Code.ToArray)
            'txtCtrSettEditCustCode.AutoCompleteCustomSource = MySource_CustomerCodes
            'txtCtrSettEditCustCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtCtrSettEditCustCode.AutoCompleteSource = AutoCompleteSource.CustomSource



            'PriceList_Codes.Clear()
            ''stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
            'stQuery = "SELECT DISTINCT PL_CODE, PL_NAME, PL_CURR_CODE FROM OM_PRICE_LIST WHERE PL_CUST_PL_NUM = 2 AND PL_COMP_CODE = '001' AND PL_FRZ_FLAG_NUM = 2"
            'ds = db.SelectFromTableODBC(stQuery)
            'count = ds.Tables("Table").Rows.Count
            ''MsgBox(count)
            'j = 0
            'While count > 0
            '    row = ds.Tables("Table").Rows.Item(j)
            '    PriceList_Codes.Add(row.Item(0).ToString)
            '    j = j + 1
            '    count = count - 1
            'End While
            'MySource_PriceListCodes.AddRange(PriceList_Codes.ToArray)
            'txtCtrSettEditPLCode.AutoCompleteCustomSource = MySource_PriceListCodes
            'txtCtrSettEditPLCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtCtrSettEditPLCode.AutoCompleteSource = AutoCompleteSource.CustomSource



            'Payment_Type.Clear()

            'stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
            ''stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            'ds = db.SelectFromTableODBC(stQuery)

            'count = ds.Tables("Table").Rows.Count
            'j = 0
            'While count > 0
            '    row = ds.Tables("Table").Rows.Item(j)
            '    Payment_Type.Add(row.Item(0).ToString)
            '    j = j + 1
            '    count = count - 1
            'End While
            'MySource_PaymentType.AddRange(Payment_Type.ToArray)
            'txtCtrSettEditPayCode.AutoCompleteCustomSource = MySource_PaymentType
            'txtCtrSettEditPayCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtCtrSettEditPayCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            'AdvPayment_Codes.Clear()

            'stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
            ''stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            'ds = db.SelectFromTableODBC(stQuery)

            'count = ds.Tables("Table").Rows.Count
            'j = 0
            'While count > 0
            '    row = ds.Tables("Table").Rows.Item(j)
            '    AdvPayment_Codes.Add(row.Item(0).ToString)
            '    j = j + 1
            '    count = count - 1
            'End While
            'MySource_AdvPaymentCodes.AddRange(AdvPayment_Codes.ToArray)
            'txtCtrSettEditAdvPCCode.AutoCompleteCustomSource = MySource_AdvPaymentCodes
            'txtCtrSettEditAdvPCCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtCtrSettEditAdvPCCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            txtCtrSettEditCompCode.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(1).Text
            txtCtrSettEditCompName.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(2).Text
            txtCtrSettEditLocCode.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text
            txtCtrSettEditLocName.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(4).Text
            txtCtrSettEditCntrCode.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text
            'txtCtrSettEditCntrName.Text = lstviewCountSettings.SelectedItems.Item(0).SubItems(6).Text

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='CUST_CODE'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettEditCustCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='PL_CODE'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettEditPLCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='ADV_PAY_CODE'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettEditAdvPCCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='PAY_CODE'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettEditPayCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='LINE_DISP_WL_1'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCrtSettEditWL1.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='LINE_DISP_WL_2'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCrtSettEditWL2.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='LINE_DISP_TL_1'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCrtSettEditTL1.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If

            stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='LINE_DISP_TL_2'"
            errLog.WriteToErrorLog("Rowid", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCrtSettEditTL2.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
            End If


        Else
            MsgBox("Please select a row!")
        End If
    End Sub

    Private Sub btnSalesmanMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsSalesmanMaster.MdiParent = Home
        SettingsSalesmanMaster.Show()
    End Sub

    Private Sub btnCounterMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterSettings.Click
        settingsType = "Counter Settings"
        lblMasterHeader.Text = "Counter Settings"
    End Sub

    Private Sub btnDenominationMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsDenominationMaster.MdiParent = Home
        SettingsDenominationMaster.Show()
    End Sub

    Private Sub btnPaymentMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsPaymentMaster.MdiParent = Home
        SettingsPaymentMaster.Show()
    End Sub


    Private Sub txtCrtSett_AddCustCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCrtSettCustCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        Try
            'stQuery = "SELECT CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = '1' AND  CUST_CODE='" + txtCrtSett_AddCustCode.Text + "'"
            stQuery = "SELECT CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) AND  CUST_CODE='" + txtCrtSettCustCode.Text + "'"
            'SELECT CUST_CODE,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1)

            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCrtSettCustName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCrtSettCustName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtctrsettg_AddcountNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCtrSettCounterCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select POSCNT_NAME from OM_POS_COUNTER where POSCNT_FRZ_FLAG_NUM = 2 and POSCNT_NO='" & txtCtrSettCounterCode.Text & "' and POSCNT_LOCN_CODE='" & txtCtrSettLoccode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCtrSettCounterName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCtrSettCounterName.Text = ""
        End If
    End Sub

    Private Sub txtCtrsett_Paymentcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCrtSettPCCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet

        stQuery = "select PPD_NAME from OM_POS_PAYMENT_DET where PPD_CODE='" & txtCrtSettPCCode.Text & "'"
        'stQuery = "SELECT VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='"  txtCtrsett_Paymentcode  "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCrtSettPCName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCrtSettPCName.Text = ""
        End If
    End Sub

    Private Sub txtCtrSettg_AddAdvPaycode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCrtSettAdvPCCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet

        stQuery = "select PPD_NAME from OM_POS_PAYMENT_DET where PPD_CODE='" & txtCrtSettAdvPCCode.Text & "'"
        'stQuery = "SELECT VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='"  txtCtrsett_Paymentcode  "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCrtSettAdvPCName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCrtSettAdvPCName.Text = ""
        End If
    End Sub


    Private Sub btnctrSettingsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnctrSettingsAdd.Click
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            If settingsType = "Counter Settings" Then
                If Not pnlCounterSetgAdd.Visible Then
                    pnlCounterSetgAdd.Height = lblCountSNo.Height + lstviewCountSettings.Height + 1
                    pnlCounterSetgAdd.BringToFront()
                    Dim i As Integer = pnlCounterSetgAdd.Height
                    While i >= lblCountSNo.Location.Y
                        pnlCounterSetgAdd.Location = New Point(lblCountSNo.Location.X, i)
                        pnlCounterSetgAdd.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    PnlCtrSetg_Edit.Visible = False

                    txtCtrSettCompcode.Text = ""
                    txtCtrSettCompName.Text = ""
                    txtCtrSettLoccode.Text = ""
                    txtCtrSettLocName.Text = ""
                    txtCtrSettCounterCode.Text = ""
                    txtCtrSettCounterName.Text = ""
                    txtCrtSettCustCode.Text = ""
                    txtCrtSettCustName.Text = ""
                    txtCrtSettPLCode.Text = ""
                    txtCrtSettPLName.Text = ""
                    txtCrtSettPLCurrCode.Text = ""
                    txtCrtSettAdvPCCode.Text = ""
                    txtCrtSettAdvPCName.Text = ""
                    txtCrtSettPCCode.Text = ""
                    txtCrtSettPCName.Text = ""

                    Company_Codes.Clear()
                    stQuery = "select COMP_CODE from FM_COMPANY"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Company_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_CompanyCodes.AddRange(Company_Codes.ToArray)
                    txtCtrSettCompcode.AutoCompleteCustomSource = MySource_CompanyCodes
                    txtCtrSettCompcode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCtrSettCompcode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Location_Codes.Clear()
                    stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
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
                    txtCtrSettLoccode.AutoCompleteCustomSource = MySource_LocationCodes
                    txtCtrSettLoccode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCtrSettLoccode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Customer_Code.Clear()
                    'stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
                    stQuery = "SELECT CUST_CODE,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1)"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Customer_Code.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_CustomerCodes.AddRange(Customer_Code.ToArray)
                    txtCrtSettCustCode.AutoCompleteCustomSource = MySource_CustomerCodes
                    txtCrtSettCustCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCrtSettCustCode.AutoCompleteSource = AutoCompleteSource.CustomSource


                    PriceList_Codes.Clear()
                    'stQuery = "select CUST_CODE from OM_CUSTOMER where CUST_FRZ_FLAG_NUM = '2'"
                    stQuery = "SELECT DISTINCT PL_CODE, PL_NAME, PL_CURR_CODE FROM OM_PRICE_LIST WHERE PL_CUST_PL_NUM = 2 AND PL_COMP_CODE = '001' AND PL_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    'MsgBox(count)
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        PriceList_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_PriceListCodes.AddRange(PriceList_Codes.ToArray)
                    txtCrtSettPLCode.AutoCompleteCustomSource = MySource_PriceListCodes
                    txtCrtSettPLCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCrtSettPLCode.AutoCompleteSource = AutoCompleteSource.CustomSource



                    Payment_Type.Clear()

                    stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
                    'stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Payment_Type.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_PaymentType.AddRange(Payment_Type.ToArray)
                    txtCrtSettPCCode.AutoCompleteCustomSource = MySource_PaymentType
                    txtCrtSettPCCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCrtSettPCCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    AdvPayment_Codes.Clear()

                    stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET"
                    'stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        AdvPayment_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_AdvPaymentCodes.AddRange(AdvPayment_Codes.ToArray)
                    txtCrtSettAdvPCCode.AutoCompleteCustomSource = MySource_AdvPaymentCodes
                    txtCrtSettAdvPCCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCrtSettAdvPCCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCrtSettPLCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCrtSettPLCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select PL_NAME,PL_CURR_CODE from OM_PRICE_LIST where PL_CODE = '" & txtCrtSettPLCode.Text & "'"
        'stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterAddLocationCode.Text & "'"

        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCrtSettPLName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            txtCrtSettPLCurrCode.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
        Else
            txtCrtSettPLName.Text = ""
            txtCrtSettPLCurrCode.Text = ""
        End If
    End Sub

    Private Sub txtCtrSettEditCustCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettEditCustCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        Try
            stQuery = "SELECT CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) AND  CUST_CODE='" + txtCtrSettEditCustCode.Text + "'"
            'SELECT CUST_CODE,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1)

            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCtrSettEditCustName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCtrSettEditCustName.Text = ""
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCtrSettEditPLCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettEditPLCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select PL_NAME,PL_CURR_CODE from OM_PRICE_LIST where PL_CODE = '" & txtCtrSettEditPLCode.Text & "'"
        'stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterAddLocationCode.Text & "'"

        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCtrSettEditPLName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            txtCtrSettEditPLCurCode.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
        Else
            txtCtrSettEditPLName.Text = ""
            txtCtrSettEditPLCurCode.Text = ""
        End If
    End Sub


    Private Sub txtCtrSettEditPCCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettEditPayCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet

        stQuery = "select PPD_NAME from OM_POS_PAYMENT_DET where PPD_CODE='" & txtCtrSettEditPayCode.Text & "'"
        'stQuery = "SELECT VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='"  txtCtrsett_Paymentcode  "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCtrSettEditPCName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCtrSettEditPCName.Text = ""
        End If
    End Sub

    Private Sub txtCtrSettEditAdvPCCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtrSettEditAdvPCCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select PPD_NAME from OM_POS_PAYMENT_DET where PPD_CODE='" & txtCtrSettEditAdvPCCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCtrSettEditAdvPCName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCtrSettEditAdvPCName.Text = ""
        End If
    End Sub

    Private Sub btnCtrSettEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCtrSettEditUpdate.Click
        Dim stQuery As String

        'Dim PLCODE As String

        Dim ds As DataSet


        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET  POCS_VALUE ='" & txtCtrSettEditCustCode.Text & "' where POCS_KEY='CUST_CODE' AND POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "'"
        errLog.WriteToErrorLog("Rowid", stQuery, "")
        ds = db.SelectFromTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCtrSettEditPLCode.Text & "' where POCS_KEY ='PL_CODE' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCtrSettEditAdvPCCode.Text & "' where POCS_KEY ='ADV_PAY_CODE' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCtrSettEditPayCode.Text & "' where POCS_KEY ='PAY_CODE' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCrtSettEditWL1.Text & "' where POCS_KEY ='LINE_DISP_WL_1' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCrtSettEditWL2.Text & "' where POCS_KEY ='LINE_DISP_WL_2' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCrtSettEditTL1.Text & "' where POCS_KEY ='LINE_DISP_TL_1' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        stQuery = "UPDATE OM_POS_OPTIONS_COUNTER_SETUP SET POCS_VALUE = '" & txtCrtSettEditTL2.Text & "' where POCS_KEY ='LINE_DISP_TL_2' AND POCS_LOCN_CODE = '" & txtCtrSettEditLocCode.Text & "'  AND POCS_COUNTER_CODE = '" & txtCtrSettEditCntrCode.Text & "'"
        errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
        db.SaveToTableODBC(stQuery)

        MsgBox("Counter settings Updated." & vbNewLine & "The Changes will be effective in the next login.", MsgBoxStyle.Information)

        'From Edit


        'stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='CUST_CODE'"
        'errLog.WriteToErrorLog("Rowid", stQuery, "")
        'ds = db.SelectFromTableODBC(stQuery)
        'If ds.Tables("Table").Rows.Count > 0 Then
        '    txtCtrSettEditCustCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
        'End If

        'stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='PL_CODE'"
        'errLog.WriteToErrorLog("Rowid", stQuery, "")
        'ds = db.SelectFromTableODBC(stQuery)
        'If ds.Tables("Table").Rows.Count > 0 Then
        '    txtCtrSettEditPLCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
        'End If

        'stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='ADV_PAY_CODE'"
        'errLog.WriteToErrorLog("Rowid", stQuery, "")
        'ds = db.SelectFromTableODBC(stQuery)
        'If ds.Tables("Table").Rows.Count > 0 Then
        '    txtCtrSettEditAdvPCCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
        'End If

        'stQuery = "select POCS_VALUE from OM_POS_OPTIONS_COUNTER_SETUP where POCS_LOCN_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POCS_COUNTER_CODE= '" & lstviewCountSettings.SelectedItems.Item(0).SubItems(5).Text & "' and POCS_KEY='PAY_CODE'"
        'errLog.WriteToErrorLog("Rowid", stQuery, "")
        'ds = db.SelectFromTableODBC(stQuery)
        'If ds.Tables("Table").Rows.Count > 0 Then
        '    txtCtrSettEditPCCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()
        'End If

        'Close

        Dim i As Integer = PnlCtrSetg_Edit.Height
        While i > 0
            PnlCtrSetg_Edit.Height = PnlCtrSetg_Edit.Height - 1
            'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
            i = i - 1
            Threading.Thread.Sleep(0.5)
        End While

        PnlCtrSetg_Edit.Visible = False
        PnlCtrSetg_Edit.SendToBack()

        load_AllCountSettings()
    End Sub

    Private Sub btnCtrSettEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCtrSettEditCancel.Click

        load_AllCountSettings()
        Dim i As Integer = PnlCtrSetg_Edit.Height
        While i > 0
            PnlCtrSetg_Edit.Height = PnlCtrSetg_Edit.Height - 1
            PnlCtrSetg_Edit.Location = New Point(lblCountSNo.Location.X, PnlCtrSetg_Edit.Location.Y + 1)
            i = i - 1
            Threading.Thread.Sleep(0.5)
        End While

        PnlCtrSetg_Edit.Visible = False
        PnlCtrSetg_Edit.SendToBack()

    End Sub

    Private Sub txtCtrSettEditCntrCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCtrSettEditCntrCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select POSCNT_NAME from OM_POS_COUNTER where POSCNT_FRZ_FLAG_NUM = 2 and POSCNT_NO='" & txtCtrSettEditCntrCode.Text & "' and POSCNT_LOCN_CODE='" & txtCtrSettEditLocCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCtrSettEditCntrName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCtrSettEditCntrName.Text = ""
        End If
    End Sub

    Private Sub Grp_Box_Ctr_Settings_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grp_Box_Ctr_Settings.Enter

    End Sub
End Class