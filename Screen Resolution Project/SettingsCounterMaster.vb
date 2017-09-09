Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D



Public Class SettingsCounterMaster
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Dock = DockStyle.Fill
            SetResolution()


            'settingsType = "Counter Master"
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub load_AllCounterDetails()
        Try
            lstviewCounterMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "SELECT POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME,POSCNT_CR_UID,POSCNT_FRZ_FLAG_NUM FROM  OM_POS_COUNTER order by POSCNT_LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewCounterMaster.Items.Add(i + 1)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(4).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(5).ToString)
                lstviewCounterMaster.Items(i).SubItems.Add(row.Item(6).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetResolution()
        ' set resolution sub checks all the controls on the screen. Containers (tabcontrol, panel, ‘groupbox, tablelayoutpanel) do not resize on general control search for the form – so ‘they have to be done separate by name
        Try
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


                For Each ctl As Control In pnl_SalesMaster.Controls
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

                For Each ctl As Control In pnl_DenominationMaster.Controls
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
                For Each ctl As Control In pnl_Paymentmaster.Controls
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

                lstviewCounterMaster.Columns.Add("SNo", lblCounterSNo.Width - 3, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Counter No", lblCounterNo.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Counter Name", lblCountername.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Location Code", lbl_loccode.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("IP Address", lbl_IP.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Computer Name", lbl_Computer.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Created User", lbl_Createduser.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Status", lbl_status.Width - 18, HorizontalAlignment.Left)
                lstviewCounterMaster.View = View.Details
                lstviewCounterMaster.GridLines = True
                lstviewCounterMaster.FullRowSelect = True

                load_AllCounterDetails()
                settingsType = "Counter Master"


                For Each ctl As Control In pnlCounterAdd.Controls
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

                For Each ctl As Control In pnlCounterEdit.Controls
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
                lstviewCounterMaster.Columns.Add("SNo", lblCounterSNo.Width - 3, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Counter No", lblCounterNo.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Counter Name", lblCountername.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Location Code", lbl_loccode.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("IP Address", lbl_IP.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Computer Name", lbl_Computer.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Created User", lbl_Createduser.Width, HorizontalAlignment.Left)
                lstviewCounterMaster.Columns.Add("Status", lbl_status.Width - 18, HorizontalAlignment.Left)
                lstviewCounterMaster.View = View.Details
                lstviewCounterMaster.GridLines = True
                lstviewCounterMaster.FullRowSelect = True

                load_AllCounterDetails()
                settingsType = "Counter Master"
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesOrders.Click
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsShiftMaster.MdiParent = Home
            SettingsShiftMaster.Show()
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
            If settingsType = "Counter Master" Then
                If Not pnlCounterAdd.Visible Then
                    pnlCounterAdd.Height = lblCounterSNo.Height + lstviewCounterMaster.Height + 1
                    pnlCounterAdd.BringToFront()
                    Dim i As Integer = pnlCounterAdd.Height
                    While i >= lblCounterSNo.Location.Y
                        pnlCounterAdd.Location = New Point(lblCounterSNo.Location.X, i)
                        pnlCounterAdd.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlCounterEdit.Visible = False
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
                    txtCounterAddLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
                    txtCounterAddLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtCounterAddLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Counter Master" Then
                pnlCounterAdd.Hide()
                pnlCounterEdit.Hide()
                pnlCounterAdd.SendToBack()
                pnlCounterEdit.SendToBack()
                load_AllCounterDetails()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click
        Try
            If pnlCounterAdd.Visible Then
                btnCounterAddCancel_Click(sender, e)
            End If
            If Not lstviewCounterMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                lstviewCounterMaster_DoubleClick(sender, e)
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

   
    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsDelete.Click
        Try
            If Not lstviewCounterMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                If pnlCounterAdd.Visible Then
                    btnCounterAddCancel_Click(sender, e)
                ElseIf pnlCounterEdit.Visible Then
                    btnCounterEditCancel_Click(sender, e)
                End If
                Exit Sub
            Else
                If pnlCounterAdd.Visible Then
                    btnCounterAddCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                ElseIf pnlCounterEdit.Visible Then
                    btnCounterEditCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                End If
                Dim counterno As String = lstviewCounterMaster.SelectedItems.Item(0).SubItems(1).Text
                Dim counterlocncode As String = lstviewCounterMaster.SelectedItems.Item(0).SubItems(3).Text
                Dim stQuery As String
                stQuery = "delete from OM_POS_COUNTER where POSCNT_NO='" & counterno & "' AND POSCNT_LOCN_CODE='" & counterlocncode & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewCounterMaster.SelectedItems.Clear()
                load_AllCounterDetails()
            End If
        Catch ex As Exception
            If ex.Message.GetHashCode = "-1172840326" Then
                MsgBox("Counter No used by a location! Cannot be deleted!")
                lstviewCounterMaster.SelectedItems.Clear()
                Exit Sub
            End If
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub txtCounterEditLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCounterEditLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterEditLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCounterEditLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCounterEditLocationDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterEditCancel.Click
        Try
            Dim i As Integer = pnlCounterEdit.Height
            While i > 0
                pnlCounterEdit.Height = pnlCounterEdit.Height - 1
                pnlCounterEdit.Location = New Point(lblCounterSNo.Location.X, pnlCounterEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlCounterEdit.Visible = False
            pnlCounterEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterEditUpdate.Click
        Try
            If txtCounterEditCountNo.Text = "" Then
                MsgBox("Enter Counter No!")
                Exit Sub
            ElseIf txtCounterEditCountName.Text = "" Then
                MsgBox("Enter Counter Name!")
                Exit Sub
            ElseIf txtCounterEditLocationCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            ElseIf txtCounterEditLocationDesc.Text = "" Then
                MsgBox("Enter a valid Location Code!")
                Exit Sub
            ElseIf txtCounterEditIPAddr.Text = "" And txtCounterEditCompName.Text = "" Then
                MsgBox("Enter either of Computer Name or IP Address!")
                Exit Sub
            End If
            Dim counterno As String = txtCounterEditCountNo.Text
            Dim counterlocncode As String = txtCounterEditLocationCode.Text
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,LOCN_NAME,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME from OM_POS_COUNTER a,OM_LOCATION b where POSCNT_NO='" & counterno & "' and POSCNT_LOCN_CODE='" & counterlocncode & "' and a.POSCNT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then
                Dim freeze As String

                If chkboxCounterEditFreeze.Checked = True Then
                    freeze = "1"
                Else
                    freeze = "2"
                End If
                stQuery = "UPDATE OM_POS_COUNTER SET POSCNT_NAME='" & txtCounterEditCountName.Text & "',POSCNT_FRZ_FLAG_NUM=" & freeze & ",POSCNT_IP_ADDRESS='" & txtCounterEditIPAddr.Text & "',POSCNT_COMPUTER_NAME='" & txtCounterEditCompName.Text & "',POSCNT_UP_DT=to_date(sysdate,'DD-MM-YY'),POSCNT_UP_UID='" & LogonUser & "' WHERE POSCNT_LOCN_CODE='" & counterlocncode & "' and POSCNT_NO='" & counterno & "'"
                errLog.WriteToErrorLog("Update Query OM_POS_SHIFT", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully")
                load_AllCounterDetails()

            Else
                MsgBox("Not able to update!")
                Exit Sub
            End If
            Dim i As Integer = pnlCounterEdit.Height
            While i > 0
                pnlCounterEdit.Height = pnlCounterEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlCounterEdit.Visible = False
            pnlCounterEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCounterAddLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCounterAddLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterAddLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCounterAddLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCounterAddLocationDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddSave.Click
        Try
            If txtCounterAddCountNo.Text = "" Then
                MsgBox("Enter Counter No!")
                Exit Sub
            ElseIf txtCounterAddCountName.Text = "" Then
                MsgBox("Enter Counter Name!")
                Exit Sub
            ElseIf txtCounterAddLocationCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            ElseIf txtCounterAddCompName.Text = "" And txtCounterAddIPAddr.Text = "" Then
                MsgBox("Enter either of Computer Name or IP Address!")
                Exit Sub
            ElseIf txtCounterAddCompName.Text = "" And txtCounterAddIPAddr.Text = "" Then

                Exit Sub
            Else
                If Not txtCounterAddLocationDesc.Text = "" Then
                    Dim stQuery As String
                    Dim ds As DataSet
                    stQuery = "SELECT POSCNT_NO FROM OM_POS_COUNTER WHERE POSCNT_NO = '" & txtCounterAddCountNo.Text & "' AND POSCNT_LOCN_CODE = '" & txtCounterAddLocationCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        Dim freeze As String
                        If chkboxCounterAddFreeze.Checked = True Then
                            freeze = "1"
                        Else
                            freeze = "2"
                        End If
                        stQuery = "INSERT INTO OM_POS_COUNTER(POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME,POSCNT_CR_DT,POSCNT_CR_UID)VALUES("
                        stQuery = stQuery & "'" & txtCounterAddCountNo.Text & "','" & txtCounterAddCountName.Text & "','" & txtCounterAddLocationCode.Text & "'," & freeze & ",'" & txtCounterAddIPAddr.Text & "','" & txtCounterAddCompName.Text & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("Insert Query OM_POS_COUNTER", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                        MsgBox("Counter Saved Successfully")
                        txtCounterAddCountNo.Text = ""
                        txtCounterAddCountName.Text = ""
                        txtCounterAddIPAddr.Text = ""
                        txtCounterAddCompName.Text = ""
                        txtCounterAddLocationCode.Text = ""
                        txtCounterAddLocationDesc.Text = ""
                        load_AllCounterDetails()
                    Else
                        MsgBox("Counter No. already exists in this location!")
                        Exit Sub
                    End If
                Else
                    MsgBox("Please select a valid location!")
                    Exit Sub
                End If
            End If

            Dim i As Integer = pnlCounterAdd.Height
            While i > 0
                pnlCounterAdd.Height = pnlCounterAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlCounterAdd.Visible = False
            pnlCounterAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddCancel.Click
        Try
            Dim i As Integer = pnlCounterAdd.Height
            While i > 0
                pnlCounterAdd.Height = pnlCounterAdd.Height - 1
                pnlCounterAdd.Location = New Point(lblCounterSNo.Location.X, pnlCounterAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlCounterAdd.Visible = False
            pnlCounterAdd.SendToBack()
            txtCounterAddCountNo.Text = ""
            txtCounterAddCountName.Text = ""
            txtCounterAddIPAddr.Text = ""
            txtCounterAddCompName.Text = ""
            txtCounterAddLocationCode.Text = ""
            txtCounterAddLocationDesc.Text = ""
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lstviewCounterMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewCounterMaster.DoubleClick
        Try
            If Not lstviewCounterMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If
            Dim counterno As String = lstviewCounterMaster.SelectedItems.Item(0).SubItems(1).Text
            Dim counterlocncode As String = lstviewCounterMaster.SelectedItems.Item(0).SubItems(3).Text

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,LOCN_NAME,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME from OM_POS_COUNTER a,OM_LOCATION b where POSCNT_NO='" & counterno & "' and POSCNT_LOCN_CODE='" & counterlocncode & "' and a.POSCNT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then

                If Not pnlCounterEdit.Visible Then
                    pnlCounterEdit.Height = lblCounterSNo.Height + lstviewCounterMaster.Height + 1
                    pnlCounterEdit.BringToFront()
                    Dim i As Integer = pnlCounterEdit.Height
                    While i >= lblCounterSNo.Location.Y
                        pnlCounterEdit.Location = New Point(lblCounterSNo.Location.X, i)
                        pnlCounterEdit.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlCounterAdd.Visible = False

                End If


                txtCounterEditCountNo.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                txtCounterEditCountName.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                txtCounterEditLocationCode.Text = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                txtCounterEditLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString

                If ds.Tables("Table").Rows.Item(0).Item(4).ToString = "2" Then
                    chkboxCounterEditFreeze.CheckState = CheckState.Unchecked
                    chkboxCounterEditFreeze.Enabled = True
                ElseIf ds.Tables("Table").Rows.Item(0).Item(4).ToString = "1" Then
                    chkboxCounterEditFreeze.CheckState = CheckState.Checked
                    chkboxCounterEditFreeze.Enabled = True
                End If
                txtCounterEditIPAddr.Text = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                txtCounterEditCompName.Text = ds.Tables("Table").Rows.Item(0).Item(6).ToString
                lstviewCounterMaster.SelectedItems.Clear()
            Else
                MsgBox("Not available for edit")
                lstviewCounterMaster.SelectedItems.Clear()
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

End Class