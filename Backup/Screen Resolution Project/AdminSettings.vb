Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D
Imports System.IO


Public Class AdminSettings
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim Company_Codes As New List(Of String)
    Dim Payment_Codes As New List(Of String)
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_CompanyCodes As New AutoCompleteStringCollection()
    Dim MySource_PaymentCodes As New AutoCompleteStringCollection()


    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Dock = DockStyle.Fill
            SetResolution()

            lstviewLocSettings.Columns.Add("SNo", lblLocationSNo.Width - 5, HorizontalAlignment.Center)
            lstviewLocSettings.Columns.Add("Company Code", lblLocSetCompCode.Width, HorizontalAlignment.Center)
            lstviewLocSettings.Columns.Add("Company Name", lblLocSetCompName.Width, HorizontalAlignment.Left)
            lstviewLocSettings.Columns.Add("Location Code", lblLocSetLocCode.Width, HorizontalAlignment.Center)
            lstviewLocSettings.Columns.Add("Location Name", lblLocSetLocName.Width - 20, HorizontalAlignment.Left)

            lstviewLocSettings.View = View.Details
            lstviewLocSettings.GridLines = True
            lstviewLocSettings.FullRowSelect = True

            load_AllLocSettings()
            'settingsType = "Counter Master"
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub load_AllLocSettings()
        lstviewLocSettings.Items.Clear()
        Dim ds As DataSet
        Dim stQuery As String
        stQuery = "select distinct POLS_LOCN_CODE,LOCN_NAME,POLS_COMP_CODE,COMP_NAME from OM_POS_OPTIONS_LOCATION_SETUP,OM_LOCATION,FM_COMPANY where POLS_COMP_CODE = COMP_CODE and POLS_LOCN_CODE = LOCN_CODE order by POLS_LOCN_CODE"
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer
        count = ds.Tables("Table").Rows.Count
        Dim i As Integer = 0
        Dim row As System.Data.DataRow
        While count > 0
            row = ds.Tables("Table").Rows.Item(i)
            lstviewLocSettings.Items.Add(i + 1)
            lstviewLocSettings.Items(i).SubItems.Add(row.Item(2).ToString)
            lstviewLocSettings.Items(i).SubItems.Add(row.Item(3).ToString)
            lstviewLocSettings.Items(i).SubItems.Add(row.Item(0).ToString)
            lstviewLocSettings.Items(i).SubItems.Add(row.Item(1).ToString)
            i = i + 1
            count = count - 1
        End While
    End Sub

    Private Sub SetResolution()
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



            settingsType = "Counter Master"


            For Each ctl As Control In pnlLocationAdd.Controls
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

            For Each ctl As Control In pnlLocationEdit.Controls
                If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                    shoFont = ctl.Font.Size * perY
                    ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                End If


                ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                    ctl.Height = ctl.Size.Height * perY + shoAdd
                    ctl.Width = ctl.Size.Width * perX
                Else

                    ctl.Height = ctl.Size.Height * perY
                    ctl.Width = ctl.Size.Width * perX
                End If

                Application.DoEvents()
            Next



            Me.Top = (prvheight / 2) - (Me.Height / 2)
            Me.Left = (prvWidth / 2) - (Me.Width / 2)
        Else

            settingsType = "Location Settings"
        End If
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesOrders.Click
        Try
            settingsType = "Location Settings"
            lblMasterHeader.Text = "Location Settings"
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsAdd.Click
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            If settingsType = "Location Settings" Then
                If Not pnlLocationAdd.Visible Then
                    pnlLocationAdd.Height = lblLocationSNo.Height + lstviewLocSettings.Height + 1
                    pnlLocationAdd.BringToFront()
                    Dim i As Integer = pnlLocationAdd.Height
                    While i >= lblLocationSNo.Location.Y
                        pnlLocationAdd.Location = New Point(lblLocationSNo.Location.X, i)
                        pnlLocationAdd.Show()
                        'Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    txtLocSetCompName.Text = ""
                    txtCompanyCode.Text = ""
                    txtLocationCode.Text = ""
                    txtLocSetLocName.Text = ""
                    txtLocSetAlertBT.Text = ""
                    txtLocSetAlertIT.Text = ""
                    chkLocSetBackDate.CheckState = CheckState.Checked
                    dtLocSetBusFromTime.Text = TimeOfDay
                    dtLocSetBusToTime.Text = TimeOfDay
                    pnlLocationEdit.Visible = False
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
                    txtCompanyCode.AutoCompleteCustomSource = MySource_CompanyCodes
                    txtCompanyCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCompanyCode.AutoCompleteSource = AutoCompleteSource.CustomSource


                End If

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Seetings Add Query", ex.StackTrace, "")
        End Try

    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Location Settings" Then
                pnlLocationAdd.Hide()
                pnlLocationEdit.Hide()
                pnlLocationAdd.SendToBack()
                pnlLocationEdit.SendToBack()

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Home Click", ex.StackTrace, "")
        End Try
    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click
        Try
            If pnlLocationAdd.Visible Then
                btnCounterAddCancel_Click(sender, e)
            End If
            If Not lstviewLocSettings.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                callEditLocSettings()

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Edit Query for Location", ex.StackTrace, "")
        End Try

    End Sub
    Private Sub callEditLocSettings()
        txtLocSettEditCompCode.Text = ""
        txtLocSettEditCompName.Text = ""
        txtLocSettEditLocCode.Text = ""
        txtLocSettEditLocName.Text = ""
        txtLocSettEditAlertBT.Text = ""
        txtLocSettEditAlertIT.Text = ""
        chkLocSettEditBackDate.CheckState = CheckState.Unchecked
        dtLocSettEditBusFromTime.Text = TimeOfDay
        dtLocSettEditBusToTime.Text = TimeOfDay
        Try
            If lstviewLocSettings.SelectedItems.Count > 0 Then
                If Not pnlLocationEdit.Visible Then
                    pnlLocationEdit.BringToFront()
                    pnlLocationEdit.Height = lblLocationSNo.Height + lstviewLocSettings.Height + 1
                    Dim i As Integer = pnlLocationEdit.Height
                    While i >= lblLocationSNo.Location.Y
                        pnlLocationEdit.Location = New Point(lblLocationSNo.Location.X, i)
                        pnlLocationEdit.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While

                End If

                pnlLocationEdit.BringToFront()
                pnlLocationEdit.Show()
                Dim stQuery As String
                Dim ds As DataSet
                Dim ftime As DateTime
                Dim ttime As DateTime

                txtLocSettEditCompCode.Text = lstviewLocSettings.SelectedItems.Item(0).SubItems(1).Text
                txtLocSettEditCompName.Text = lstviewLocSettings.SelectedItems.Item(0).SubItems(2).Text
                txtLocSettEditLocCode.Text = lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text
                txtLocSettEditLocName.Text = lstviewLocSettings.SelectedItems.Item(0).SubItems(4).Text

                stQuery = "select POLS_VALUE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE= '" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POLS_KEY='SHIFT_ALERT_BEFORE'"
                errLog.WriteToErrorLog("Rowid", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                txtLocSettEditAlertBT.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()

                stQuery = "select POLS_VALUE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE= '" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POLS_KEY='SHIFT_ALERT_INTERVAL'"
                errLog.WriteToErrorLog("Rowid", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                txtLocSettEditAlertIT.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString()

                stQuery = "select POLS_VALUE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE= '" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POLS_KEY='BUSINESS_HOUR_FROM'"
                errLog.WriteToErrorLog("Rowid", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                ftime = DateTime.ParseExact(ds.Tables("Table").Rows.Item(0).Item(0), "dd/MM/yyyy hh:mm:ss tt", Nothing)
                dtLocSettEditBusFromTime.Text = ftime

                stQuery = "select POLS_VALUE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE= '" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POLS_KEY='BUSINESS_HOUR_TO'"
                errLog.WriteToErrorLog("Rowid", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                ttime = DateTime.ParseExact(ds.Tables("Table").Rows.Item(0).Item(0), "dd/MM/yyyy hh:mm:ss tt", Nothing)
                dtLocSettEditBusToTime.Text = ttime

                stQuery = "select POLS_VALUE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE= '" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "' and  POLS_KEY='A_BK_DATE'"
                errLog.WriteToErrorLog("Rowid BackDate", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Item(0).Item(0) = "1" Then
                    chkLocSettEditBackDate.CheckState = CheckState.Checked
                Else
                    chkLocSettEditBackDate.CheckState = CheckState.Unchecked
                End If

                Dim count As Integer
                Dim k As Integer
                Dim row As System.Data.DataRow
                stQuery = "SELECT  LOCN_FLEX_19,LOCN_FLEX_20 FROM OM_LOCATION WHERE LOCN_CODE = '" + lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text + "'"
                errLog.WriteToErrorLog("OM_LOCATION LOGO", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                k = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(k)
                    If row.Item(0).ToString = "Y" Then
                        chkboxLogoYN.CheckState = CheckState.Checked
                        If row.Item(1).ToString = "" Then
                            picboxLogoEdit.BackgroundImage = Nothing
                        Else
                            lblLogoNameEdit.Text = row.Item(1).ToString
                            If File.Exists(Application.StartupPath & "\LOGOS\" & row.Item(1).ToString) Then
                                picboxLogoEdit.BackgroundImage = Image.FromFile(Application.StartupPath & "\LOGOS\" & row.Item(1).ToString)
                            Else
                                MsgBox("Logo assigned for this location is not Found!")
                                picboxLogoEdit.BackgroundImage = Nothing
                            End If
                        End If
                    Else
                            chkboxLogoYN.CheckState = CheckState.Unchecked
                            If row.Item(1).ToString = "" Then
                                picboxLogoEdit.BackgroundImage = Nothing
                            Else
                            lblLogoNameEdit.Text = row.Item(1).ToString
                            If File.Exists(Application.StartupPath & "\LOGOS\" & row.Item(1).ToString) Then
                                picboxLogoEdit.BackgroundImage = Image.FromFile(Application.StartupPath & "\LOGOS\" & row.Item(1).ToString)
                            Else
                                MsgBox("Logo assigned for this location is not Found!")
                                picboxLogoEdit.BackgroundImage = Nothing
                            End If
                        End If
                    End If
                        count = count - 1
                        k = k + 1
                End While

            Else
                MsgBox("Please select a row!")
            End If
            If Not pnlLocationEdit.Visible Then
                pnlLocationEdit.BringToFront()
                pnlLocationEdit.Height = lblLocationSNo.Height + lstviewLocSettings.Height + 1
                Dim i As Integer = pnlLocationEdit.Height
                While i >= lblLocationSNo.Location.Y
                    pnlLocationEdit.Location = New Point(lblLocationSNo.Location.X, i)
                    pnlLocationEdit.Show()
                    Threading.Thread.Sleep(0.5)
                    i = (i - 1)
                End While

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "error")
        End Try

    End Sub


    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsDelete.Click
        Try
            If pnlLocationEdit.Visible Then
                btnCounterAddCancel_Click(sender, e)
            End If
            If Not lstviewLocSettings.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
            Else
                Dim stQuery As String
                stQuery = "delete from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE='" & lstviewLocSettings.SelectedItems.Item(0).SubItems(3).Text & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewLocSettings.SelectedItems.Clear()
                load_AllLocSettings()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub txtLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtLocSetLocName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtLocSetLocName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Location Code Text change", ex.StackTrace, "")
        End Try
    End Sub
    Private Sub txtCompanyCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompanyCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        Try
            stQuery = "select COMP_NAME from FM_COMPANY where COMP_CODE='" & txtCompanyCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtLocSetCompName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Location_Codes.Clear()
                Dim Count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow
                stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
                ds = db.SelectFromTableODBC(stQuery)

                Count = ds.Tables("Table").Rows.Count
                i = 0
                While Count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    Count = Count - 1
                End While
                MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                txtLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
                txtLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            Else
                txtLocSetCompName.Text = ""
                txtLocationCode.Text = ""
                txtLocationCode.AutoCompleteMode = AutoCompleteMode.None
                txtLocSetLocName.Text = ""

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Company Code Text Change", ex.StackTrace, "")
        End Try


    End Sub
    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLocAddSave.Click
        Try
            If txtLocSetCompName.Text = "" Then
                MsgBox("Enter Company Code!")
                Exit Sub
            ElseIf txtLocationCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            Else
                If Not txtLocSetLocName.Text = "" Then

                    Dim stQuery As String
                    Dim ds As DataSet
                    Dim BT As String
                    Dim IT As String
                    stQuery = "select distinct POLS_LOCN_CODE from OM_POS_OPTIONS_LOCATION_SETUP where POLS_LOCN_CODE = '" & txtLocationCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        Dim BackDate As String
                        If chkLocSetBackDate.CheckState = CheckState.Checked Then
                            BackDate = 1
                        Else
                            BackDate = 0
                        End If
                        If txtLocSetAlertBT.Text = "" Then
                            BT = "0"
                        Else
                            BT = txtLocSetAlertBT.Text
                        End If
                        If txtLocSetAlertIT.Text = "" Then
                            IT = "0"
                        Else
                            IT = txtLocSetAlertIT.Text
                        End If
                        stQuery = "INSERT INTO OM_POS_OPTIONS_LOCATION_SETUP(POLS_COMP_CODE,POLS_LOCN_CODE,POLS_KEY,POLS_VALUE,POLS_CR_UID,POLS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCompanyCode.Text & "','" & txtLocationCode.Text & "','A_BK_DATE'," & BackDate & ",'" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)

                        stQuery = "INSERT INTO OM_POS_OPTIONS_LOCATION_SETUP(POLS_COMP_CODE,POLS_LOCN_CODE,POLS_KEY,POLS_VALUE,POLS_CR_UID,POLS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCompanyCode.Text & "','" & txtLocationCode.Text & "','BUSINESS_HOUR_FROM',TO_CHAR(sysdate, 'DD/MM/YYYY') || ' " & dtLocSetBusFromTime.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)

                        stQuery = "INSERT INTO OM_POS_OPTIONS_LOCATION_SETUP(POLS_COMP_CODE,POLS_LOCN_CODE,POLS_KEY,POLS_VALUE,POLS_CR_UID,POLS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCompanyCode.Text & "','" & txtLocationCode.Text & "','BUSINESS_HOUR_TO',TO_CHAR(sysdate, 'DD/MM/YYYY') || ' " & dtLocSetBusToTime.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)

                        stQuery = "INSERT INTO OM_POS_OPTIONS_LOCATION_SETUP(POLS_COMP_CODE,POLS_LOCN_CODE,POLS_KEY,POLS_VALUE,POLS_CR_UID,POLS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCompanyCode.Text & "','" & txtLocationCode.Text & "','SHIFT_ALERT_BEFORE','" & BT & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)

                        stQuery = "INSERT INTO OM_POS_OPTIONS_LOCATION_SETUP(POLS_COMP_CODE,POLS_LOCN_CODE,POLS_KEY,POLS_VALUE,POLS_CR_UID,POLS_CR_DT)VALUES("
                        stQuery = stQuery & "'" & txtCompanyCode.Text & "','" & txtLocationCode.Text & "','SHIFT_ALERT_INTERVAL','" & IT & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                        errLog.WriteToErrorLog("Insert Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
                        db.SaveToTableODBC(stQuery)

                        MsgBox("Location Settings Saved Successfully." & vbNewLine & "The Changes will be effective in the next login.", MsgBoxStyle.Information)

                        load_AllLocSettings()
                    Else
                        MsgBox("Location settings already exists!")
                        Exit Sub
                    End If
                Else
                    MsgBox("Please select a valid location!")
                    Exit Sub
                End If
            End If

            Dim i As Integer = pnlLocationAdd.Height
            While i > 0
                pnlLocationAdd.Height = pnlLocationAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlLocationAdd.Visible = False
            pnlLocationAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLocAddCancel.Click
        Try
            txtLocSetCompName.Text = ""
            txtCompanyCode.Text = ""
            txtLocationCode.Text = ""
            txtLocSetLocName.Text = ""
            txtLocSetAlertBT.Text = ""
            txtLocSetAlertIT.Text = ""
            chkLocSetBackDate.CheckState = CheckState.Checked
            dtLocSetBusFromTime.Text = TimeOfDay
            dtLocSetBusToTime.Text = TimeOfDay
            load_AllLocSettings()
            Dim i As Integer = pnlLocationAdd.Height
            While i > 0
                pnlLocationAdd.Height = pnlLocationAdd.Height - 1
                pnlLocationAdd.Location = New Point(lblLocationSNo.Location.X, pnlLocationAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlLocationAdd.Visible = False
            pnlLocationAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog("Save event", ex.StackTrace, "")
        End Try

    End Sub

    Private Sub btnCounterMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterMaster.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            CounterSettings.MdiParent = Home
            CounterSettings.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub
    Private Sub lstviewLocSettings_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewLocSettings.DoubleClick
        Try
            callEditLocSettings()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btLocSettEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLocSettEditUpdate.Click

        Try
            If chkboxLogoYN.CheckState = CheckState.Checked And lblLogoNameEdit.Text = "" Then
                MsgBox("Please upload/browse a logo!")
                Exit Sub
            End If

            Dim stQuery As String
            Dim BT As String
            Dim IT As String
            Dim BackDate As String
            If chkLocSettEditBackDate.CheckState = CheckState.Checked Then
                BackDate = 1
            Else
                BackDate = 0
            End If
            If txtLocSettEditAlertBT.Text = "" Then
                BT = "0"
            Else
                BT = txtLocSettEditAlertBT.Text
            End If
            If txtLocSettEditAlertIT.Text = "" Then
                IT = "0"
            Else
                IT = txtLocSettEditAlertIT.Text
            End If

            stQuery = "UPDATE OM_POS_OPTIONS_LOCATION_SETUP SET POLS_VALUE = '" & BackDate & "' where POLS_KEY ='A_BK_DATE' AND POLS_LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "UPDATE OM_POS_OPTIONS_LOCATION_SETUP SET POLS_VALUE = TO_CHAR(sysdate, 'DD/MM/YYYY') || ' " & dtLocSettEditBusFromTime.Text & "' where POLS_KEY ='BUSINESS_HOUR_FROM' AND POLS_LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "UPDATE OM_POS_OPTIONS_LOCATION_SETUP SET POLS_VALUE = TO_CHAR(sysdate, 'DD/MM/YYYY') || ' " & dtLocSettEditBusToTime.Text & "' where POLS_KEY ='BUSINESS_HOUR_TO' AND POLS_LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "UPDATE OM_POS_OPTIONS_LOCATION_SETUP SET POLS_VALUE = '" & BT & "' where POLS_KEY ='SHIFT_ALERT_BEFORE' AND POLS_LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "UPDATE OM_POS_OPTIONS_LOCATION_SETUP SET POLS_VALUE = '" & IT & "' where POLS_KEY ='SHIFT_ALERT_INTERVAL' AND POLS_LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_OPTIONS_LOCATION_SETUP", stQuery, "")
            db.SaveToTableODBC(stQuery)

            If chkboxLogoYN.CheckState = CheckState.Checked Then
                stQuery = "UPDATE OM_LOCATION SET LOCN_FLEX_20 = '" & lblLogoNameEdit.Text & "',LOCN_FLEX_19='Y' where LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
                errLog.WriteToErrorLog("Update Query OM_LOCATION LOGO", stQuery, "")
                db.SaveToTableODBC(stQuery)
            Else
                stQuery = "UPDATE OM_LOCATION SET LOCN_FLEX_19='N' where LOCN_CODE = '" & txtLocSettEditLocCode.Text & "'"
                errLog.WriteToErrorLog("Update Query OM_LOCATION LOGO", stQuery, "")
                db.SaveToTableODBC(stQuery)
            End If

            MsgBox("Location settings Updated." & vbNewLine & "The Changes will be effective in the next login.", MsgBoxStyle.Information)

            Dim i As Integer = pnlLocationEdit.Height
            While i > 0
                pnlLocationEdit.Height = pnlLocationEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlLocationEdit.Visible = False
            pnlLocationEdit.SendToBack()

            load_AllLocSettings()

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")

        End Try
    End Sub

    Private Sub btLocSettEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLocSettEditCancel.Click
        Try
            load_AllLocSettings()
            Dim i As Integer = pnlLocationEdit.Height
            While i > 0
                pnlLocationEdit.Height = pnlLocationEdit.Height - 1
                pnlLocationEdit.Location = New Point(lblLocationSNo.Location.X, pnlLocationEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlLocationEdit.Visible = False
            pnlLocationEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnUploadLogoEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadLogoEdit.Click
        Try
            If Not Environment.GetEnvironmentVariable("SessionName").ToUpper.Substring(0, 3) = "ICA" Then
                Dim openFileDialog1 As New OpenFileDialog
                openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp"
                
                If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    Dim fullpath As String = openFileDialog1.FileName
                    Dim extension As String = Path.GetExtension(fullpath)
                    Dim filenameval As String = txtLocSettEditLocCode.Text & "_Logo_" & DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") & extension
                    Dim newpath As String = Application.StartupPath & "\LOGOS\" & filenameval
                    If (File.Exists(newpath)) Then
                        Dim result As DialogResult = MsgBox("Do you want to replace the logo? Cannot be reverted again!", MessageBoxButtons.YesNo, "Alert")
                        If result = Windows.Forms.DialogResult.Yes Then
                            File.Copy(fullpath, newpath, True)
                            ImageResize(newpath, newpath, 64, 48)
                            picboxLogoEdit.BackgroundImage = Image.FromFile(newpath)
                            lblLogoNameEdit.Text = filenameval
                        End If
                    Else
                        File.Copy(fullpath, newpath, True)
                        'ImageResize(newpath, newpath, 64, 48)
                        picboxLogoEdit.BackgroundImage = Image.FromFile(newpath)
                        lblLogoNameEdit.Text = filenameval
                    End If
                End If
            End If
            'MsgBox(lblLogoNameEdit.Text)
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Function ImageResize(ByVal strImageSrcPath As String _
                         , ByVal strImageDesPath As String _
                         , Optional ByVal intWidth As Integer = 0 _
                         , Optional ByVal intHeight As Integer = 0) As String

        If System.IO.File.Exists(strImageSrcPath) = False Then Exit Function


        Dim objImage As System.Drawing.Image = System.Drawing.Image.FromFile(strImageSrcPath)

        If objImage.Width <= intWidth & objImage.Height <= intHeight Then
            Exit Function
        End If

        If intWidth > objImage.Width Then intWidth = objImage.Width
        If intHeight > objImage.Height Then intHeight = objImage.Height
        If intWidth = 0 And intHeight = 0 Then
            intWidth = objImage.Width
            intHeight = objImage.Height
        ElseIf intHeight = 0 And intWidth <> 0 Then
            intHeight = Fix(objImage.Height * intWidth / objImage.Width)
        ElseIf intWidth = 0 And intHeight <> 0 Then
            intWidth = Fix(objImage.Width * intHeight / objImage.Height)
        End If

        Dim imgOutput As New Bitmap(objImage, intWidth, intHeight)
        Dim imgFormat = objImage.RawFormat

        objImage.Dispose()
        objImage = Nothing

        If strImageSrcPath = strImageDesPath Then System.IO.File.Delete(strImageSrcPath)
        ' send the resized image to the viewer
        imgOutput.Save(strImageDesPath, imgFormat)
        imgOutput.Dispose()

        Return strImageDesPath

    End Function

    Private Sub btnBrowseLogoEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseLogoEdit.Click
        'Dim dialog As New OpenFileDialog
        'dialog.InitialDirectory = Application.StartupPath & "\LOGOS"
        'If dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    If Not Path.GetDirectoryName(dialog.FileName).ToString = Application.StartupPath & "\LOGOS" Then
        '        MsgBox("Select Logo from the specified location only")
        '        dialog.Dispose()
        '    End If
        'End If
        'If AdSelection.Visible Then
        '    MsgBox("in")
        '    AdSelection.Close()
        '    AdSelection.Dispose()
        'End If
        Dim adSelection As New AdSelection
        adSelection.ShowDialog()
    End Sub

    Private Sub chkboxLogoYN_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkboxLogoYN.CheckStateChanged
        Dim tbx As System.Windows.Forms.CheckBox = sender
        If tbx.CheckState = CheckState.Checked Then
            pnlLOGOHolder.Enabled = True
        Else
            pnlLOGOHolder.Enabled = False
        End If
    End Sub

End Class