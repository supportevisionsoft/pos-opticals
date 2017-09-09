Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class SettingsShiftMaster
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

        settingsType = "Shift Master"
    End Sub

    Private Sub load_AllShiftDetails()
        Try
            lstviewShiftMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SHIFT_CODE,SHIFT_DESC,SHIFT_LOCN_CODE,SHIFT_FROM_TIME,SHIFT_TO_TIME,SHIFT_FRZ_FLAG_NUM,SHIFT_CR_UID from om_pos_shift order by SHIFT_LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewShiftMaster.Items.Add(i + 1)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(4).ToString)
                lstviewShiftMaster.Items(i).SubItems.Add(row.Item(5).ToString)
                i = i + 1
                count = count - 1
            End While
            'lstviewShiftMaster.Refresh()
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

                For Each ctl As Control In pnlShiftMaster.Controls
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

                lstviewShiftMaster.Columns.Add("SNo", lblShiftSNo.Width - 3, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift code", lblshift_Scode.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift Description ", lblshift_SDesc.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Location Code", lblShift_loc.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift From Time", lblshift_fromtime.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift To Time", lblshift_Totime.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Status", lblshift_status.Width - 18, HorizontalAlignment.Left)
                lstviewShiftMaster.View = View.Details
                lstviewShiftMaster.GridLines = True
                lstviewShiftMaster.FullRowSelect = True

                load_AllShiftDetails()
                lstviewShiftMaster.Refresh()
                settingsType = "Shift Master"

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

                For Each ctl As Control In pnl_salesmanmaster.Controls
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

                For Each ctl As Control In pnl_denominationmaster.Controls
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
                For Each ctl As Control In pnl_paymentmaster.Controls
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

                For Each ctl As Control In pnlShiftAdd.Controls
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

                For Each ctl As Control In pnlShiftEdit.Controls
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
                lstviewShiftMaster.Columns.Add("SNo", lblShiftSNo.Width - 3, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift code", lblshift_Scode.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift Description ", lblshift_SDesc.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Location Code", lblShift_loc.Width, HorizontalAlignment.Center)
                lstviewShiftMaster.Columns.Add("Shift From Time", lblshift_fromtime.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Shift To Time", lblshift_Totime.Width, HorizontalAlignment.Left)
                lstviewShiftMaster.Columns.Add("Status", lblshift_status.Width - 18, HorizontalAlignment.Center)
                lstviewShiftMaster.View = View.Details
                lstviewShiftMaster.GridLines = True
                lstviewShiftMaster.FullRowSelect = True

                load_AllShiftDetails()
                settingsType = "Shift Master"
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShift_SalesOrders.Click
        Try
            settingsType = "Shift Master"
            lblMasterHeader.Text = "Shift Master"
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
            If settingsType = "Shift Master" Then
                If Not pnlShiftAdd.Visible Then
                    pnlShiftAdd.BringToFront()
                    pnlShiftAdd.Height = lblShiftSNo.Height + lstviewShiftMaster.Height + 1

                    Dim i As Integer = pnlShiftAdd.Height
                    While i >= lblShiftSNo.Location.Y
                        pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, i)
                        pnlShiftAdd.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlShiftEdit.Visible = False
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
                    txtShiftAddLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
                    txtShiftAddLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtShiftAddLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource
                    dtpickShiftAddShiftFTime.Value = DateTime.Now
                    dtpickShiftAddShiftTTime.Value = DateTime.Now
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Shift Master" Then
                pnlShiftAdd.Hide()
                pnlShiftEdit.Hide()
                pnlShiftAdd.SendToBack()
                pnlShiftEdit.SendToBack()
                load_AllShiftDetails()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtShiftAddLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShiftAddLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtShiftAddLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtShiftAddLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtShiftAddLocationDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnShiftAddCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiftAddCancel.Click
        Try
            Dim i As Integer = pnlShiftAdd.Height
            While i > 0
                pnlShiftAdd.Height = pnlShiftAdd.Height - 1
                pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlShiftAdd.Visible = False
            pnlShiftAdd.SendToBack()
            txtShiftAddLocationCode.Text = ""
            txtShiftAddLocationDesc.Text = ""
            txtShiftAddShiftCode.Text = ""
            txtShiftAddShiftDesc.Text = ""
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnShiftAddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiftAddSave.Click
        Try
            If txtShiftAddShiftCode.Text = "" Then
                MsgBox("Enter Shift Code!")
                Exit Sub
            ElseIf txtShiftAddShiftCode.Text.Length > 12 Then
                MsgBox("Shift Code cannot be more than 12 character!")
                Exit Sub
            ElseIf txtShiftAddShiftDesc.Text = "" Then
                MsgBox("Enter Shift Desc!")
                Exit Sub
            ElseIf txtShiftAddLocationCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            Else
                If Not txtShiftAddLocationDesc.Text = "" Then
                    Dim stQuery As String
                    Dim ds As DataSet
                    stQuery = "SELECT SHIFT_CODE FROM OM_POS_SHIFT a,OM_LOCATION b WHERE SHIFT_CODE = '" + txtShiftAddShiftCode.Text + "' AND SHIFT_LOCN_CODE = '013' AND LOCN_FRZ_FLAG_NUM = 2 AND a.SHIFT_LOCN_CODE = b.LOCN_CODE"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        Dim freeze As String
                        If chkboxShiftAddFreeze.Checked = True Then
                            freeze = "1"
                        Else
                            freeze = "2"
                        End If
                        Dim Ftime As String = dtpickShiftAddShiftFTime.Value.ToLongTimeString
                        Dim Ttime As String = dtpickShiftAddShiftTTime.Value.ToLongTimeString

                        stQuery = "INSERT INTO OM_POS_SHIFT (SHIFT_CODE,SHIFT_DESC,SHIFT_LOCN_CODE,SHIFT_FROM_TIME,SHIFT_TO_TIME,SHIFT_FRZ_FLAG_NUM,SHIFT_CR_DT,SHIFT_CR_UID) VALUES ("
                        stQuery = stQuery & "'" & txtShiftAddShiftCode.Text & "','" & txtShiftAddShiftDesc.Text & "','" & txtShiftAddLocationCode.Text & "',to_date(to_char(sysdate || ' ' || '" + Ftime + "'),'DD-MM-YY HH12:MI:SS AM'),to_date(to_char(sysdate || ' ' || '" + Ttime + "'),'DD-MM-YY HH12:MI:SS AM')," & freeze & ",to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("Insert Query OM_POS_SHIFT", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                        MsgBox("Shift Saved Successfully")
                        txtShiftAddLocationCode.Text = ""
                        txtShiftAddLocationDesc.Text = ""
                        txtShiftAddShiftCode.Text = ""
                        txtShiftAddShiftDesc.Text = ""
                    Else
                        MsgBox("Shift Already Exists for this location")
                        Exit Sub
                    End If
                Else
                    MsgBox("Please select a valid location!")
                    Exit Sub
                End If
            End If

            Dim i As Integer = pnlShiftAdd.Height
            While i > 0
                pnlShiftAdd.Height = pnlShiftAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlShiftAdd.Visible = False
            pnlShiftAdd.SendToBack()

            lstviewShiftMaster.Refresh()
            load_AllShiftDetails()

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click

        Try
            If pnlShiftAdd.Visible Then
                btnShiftAddCancel_Click(sender, e)
            End If
            If Not lstviewShiftMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                lstviewShiftMaster_DoubleClick(sender, e)
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub
    Private Sub txtShiftEditLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShiftEditLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtShiftEditLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtShiftEditLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtShiftEditLocationDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnShiftEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiftEditUpdate.Click
        Try
            If txtShiftEditShiftCode.Text.Length > 12 Then
                MsgBox("Shift code cannot be more than 12 character!")
            ElseIf txtShiftEditShiftDesc.Text = "" Then
                MsgBox("Shift Description cannot be empty!")
            End If
            Dim shiftcode As String = txtShiftEditShiftCode.Text
            Dim shiftlocncode As String = txtShiftEditLocationCode.Text

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT SHIFT_CODE,SHIFT_DESC,SHIFT_LOCN_CODE,LOCN_NAME,SHIFT_FROM_TIME,SHIFT_TO_TIME,SHIFT_FRZ_FLAG_NUM FROM OM_POS_SHIFT a,OM_LOCATION b WHERE SHIFT_CODE = '" + shiftcode + "' AND SHIFT_LOCN_CODE = '" & shiftlocncode & "'  AND a.SHIFT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then
                Dim Ftime As String = dtpickShiftEditShiftFTime.Value.ToLongTimeString
                Dim Ttime As String = dtpickShiftEditShiftTTime.Value.ToLongTimeString
                Dim freeze As String

                If chkboxShiftEditFreeze.Checked = True Then
                    freeze = "1"
                Else
                    freeze = "2"
                End If
                stQuery = "UPDATE OM_POS_SHIFT SET SHIFT_DESC='" & txtShiftEditShiftDesc.Text & "',SHIFT_FROM_TIME=to_date(to_char(sysdate || ' ' || '" + Ftime + "'),'DD-MM-YY HH12:MI:SS AM'),SHIFT_TO_TIME=to_date(to_char(sysdate || ' ' || '" + Ttime + "'),'DD-MM-YY HH12:MI:SS AM'),SHIFT_FRZ_FLAG_NUM=" & freeze & ",SHIFT_UPD_UID='" & LogonUser & "',SHIFT_UPD_DT=to_date(sysdate,'DD-MM-YY') WHERE SHIFT_CODE='" & shiftcode & "' AND SHIFT_LOCN_CODE='" & shiftlocncode & "'"
                'stQuery = "UPDATE OM_POS_SHIFT SET SHIFT_DESC='8PM8PMPM',SHIFT_FROM_TIME=to_date(to_char(sysdate || ' ' || '12:00:00 AM'),'DD-MON-YY HH12:MI:SS AM'),SHIFT_TO_TIME=to_date(to_char(sysdate || ' ' || '6:21:27 PM'),'DD-MON-YY HH12:MI:SS AM'),SHIFT_FRZ_FLAG_NUM=2,SHIFT_UPD_UID='ESHACK',SHIFT_UPD_DT=to_date(sysdate,'DD-MON-YY') WHERE SHIFT_CODE='8PM8PMPM' AND SHIFT_LOCN_CODE='013'"
                errLog.WriteToErrorLog("Update Query OM_POS_SHIFT", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully")
                load_AllShiftDetails()
            Else
                MsgBox("Not able to update!")
                Exit Sub
            End If

            Dim i As Integer = pnlShiftEdit.Height
            While i > 0
                pnlShiftEdit.Height = pnlShiftEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlShiftEdit.Visible = False
            pnlShiftEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnShiftEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiftEditCancel.Click
        Try
            Dim i As Integer = pnlShiftEdit.Height
            While i > 0
                pnlShiftEdit.Height = pnlShiftEdit.Height - 1
                pnlShiftEdit.Location = New Point(lblShiftSNo.Location.X, pnlShiftEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlShiftEdit.Visible = False
            pnlShiftEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsDelete.Click
        Try
            If Not lstviewShiftMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                If pnlShiftAdd.Visible Then
                    btnShiftAddCancel_Click(sender, e)
                ElseIf pnlShiftEdit.Visible Then
                    btnShiftEditCancel_Click(sender, e)
                End If
                Exit Sub
            Else
                If pnlShiftAdd.Visible Then
                    btnShiftAddCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                ElseIf pnlShiftEdit.Visible Then
                    btnShiftEditCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                End If
                Dim shiftcode As String = lstviewShiftMaster.SelectedItems.Item(0).SubItems(1).Text
                Dim shiftlocncode As String = lstviewShiftMaster.SelectedItems.Item(0).SubItems(3).Text

                Dim stQuery As String
                stQuery = "delete from OM_POS_SHIFT where SHIFT_CODE='" & shiftcode & "' AND SHIFT_LOCN_CODE='" & shiftlocncode & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewShiftMaster.SelectedItems.Clear()
                load_AllShiftDetails()
            End If
        Catch ex As Exception
            If ex.Message.GetHashCode = 2034902428 Then
                MsgBox("Shift Code used in a location! Cannot be deleted!")
                lstviewShiftMaster.SelectedItems.Clear()
                Exit Sub
            End If
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnCounterMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterMaster.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsCounterMaster.MdiParent = Home
            SettingsCounterMaster.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub lstviewShiftMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewShiftMaster.DoubleClick
        Try
            If Not lstviewShiftMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If
            Dim shiftcode As String = lstviewShiftMaster.SelectedItems.Item(0).SubItems(1).Text
            Dim shiftlocncode As String = lstviewShiftMaster.SelectedItems.Item(0).SubItems(3).Text

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT SHIFT_CODE,SHIFT_DESC,SHIFT_LOCN_CODE,LOCN_NAME,SHIFT_FROM_TIME,SHIFT_TO_TIME,SHIFT_FRZ_FLAG_NUM FROM OM_POS_SHIFT a,OM_LOCATION b WHERE SHIFT_CODE = '" + shiftcode + "' AND SHIFT_LOCN_CODE = '" & shiftlocncode & "'  AND a.SHIFT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then
                Dim dsL As DataSet
                Dim count As Integer
                Dim row As System.Data.DataRow
                If settingsType = "Shift Master" Then
                    If Not pnlShiftEdit.Visible Then
                        pnlShiftEdit.BringToFront()
                        pnlShiftEdit.Height = lblShiftSNo.Height + lstviewShiftMaster.Height + 1
                        Dim i As Integer = pnlShiftEdit.Height
                        While i >= lblShiftSNo.Location.Y
                            pnlShiftEdit.Location = New Point(lblShiftSNo.Location.X, i)
                            pnlShiftEdit.Show()
                            Threading.Thread.Sleep(0.5)
                            i = (i - 1)
                        End While
                        pnlShiftAdd.Visible = False
                        Location_Codes.Clear()
                        stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
                        dsL = db.SelectFromTableODBC(stQuery)

                        count = dsL.Tables("Table").Rows.Count
                        i = 0
                        While count > 0
                            row = dsL.Tables("Table").Rows.Item(i)
                            Location_Codes.Add(row.Item(0).ToString)
                            i = i + 1
                            count = count - 1
                        End While
                        MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                        txtShiftEditLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
                        txtShiftEditLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                        txtShiftEditLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource
                    End If
                End If

                txtShiftEditShiftCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                txtShiftEditShiftDesc.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                txtShiftEditLocationCode.Text = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                txtShiftEditLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString
                Dim value As String = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                Dim time As DateTime = DateTime.Parse(value)
                dtpickShiftEditShiftFTime.Value = time
                value = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                time = DateTime.Parse(value)
                dtpickShiftEditShiftTTime.Value = time
                If ds.Tables("Table").Rows.Item(0).Item(6).ToString = "2" Then
                    chkboxShiftEditFreeze.CheckState = CheckState.Unchecked
                    chkboxShiftEditFreeze.Enabled = True
                ElseIf ds.Tables("Table").Rows.Item(0).Item(6).ToString = "1" Then
                    chkboxShiftEditFreeze.CheckState = CheckState.Checked
                    chkboxShiftEditFreeze.Enabled = True
                End If
                lstviewShiftMaster.SelectedItems.Clear()
            Else
                MsgBox("Not available for edit")
                lstviewShiftMaster.SelectedItems.Clear()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnSalesmanMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanMaster.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsSalesmanMaster.MdiParent = Home
            SettingsSalesmanMaster.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnDenominationMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenominationMaster.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsDenominationMaster.MdiParent = Home
            SettingsDenominationMaster.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnPaymentMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentMaster.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsPaymentMaster.MdiParent = Home
            SettingsPaymentMaster.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

   
    Private Sub lstviewShiftMaster_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstviewShiftMaster.SelectedIndexChanged
        Try
            lstviewShiftMaster.Refresh()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
   
     
End Class