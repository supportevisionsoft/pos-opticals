Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class SettingsDenominationMaster
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Currency_Codes As New List(Of String)
    Dim Money_Types As New List(Of String)
    Dim Payment_Types As New List(Of String)
    Dim MySource_CurrencyCodes As New AutoCompleteStringCollection()
    Dim MySource_MoneyTypes As New AutoCompleteStringCollection()
    Dim MySource_PaymentTypes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

        
        settingsType = "Denomination Master"

    End Sub

    Private Sub load_AllDenominationDetails()
        Try
            lstviewDenominationMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select POSDENO_CODE,POSDENO_CURR_CODE,POSDENO_NOTE_COIN_FLAG,POSDENO_PMT_TYPE,POSDENO_CR_UID from OM_POS_DENOMINATION order by POSDENO_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewDenominationMaster.Items.Add(i + 1)
                lstviewDenominationMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewDenominationMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewDenominationMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewDenominationMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewDenominationMaster.Items(i).SubItems.Add(row.Item(4).ToString)

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


                For Each ctl As Control In pnl_shiftMaster.Controls
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

                For Each ctl As Control In pnl_CounterMaster.Controls
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


                For Each ctl As Control In pnl_Denom.Controls
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

                For Each ctl As Control In pnl_Payment.Controls
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

                For Each ctl As Control In pnlDenominationMaster.Controls
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


                For Each ctl As Control In pnlDenominationAdd.Controls
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
                For Each ctl As Control In pnlDenominationEdit.Controls
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
            End If
            lstviewDenominationMaster.Columns.Add("SNo", lblDenominationSNo.Width - 3, HorizontalAlignment.Left)
            lstviewDenominationMaster.Columns.Add("Denomination Code", lblDenominationCode.Width, HorizontalAlignment.Left)
            lstviewDenominationMaster.Columns.Add("Currency Code", lblDenom_CurrCode.Width, HorizontalAlignment.Left)
            lstviewDenominationMaster.Columns.Add("Money Type", lblDenom_Money.Width, HorizontalAlignment.Left)
            lstviewDenominationMaster.Columns.Add("Payment Type", lblDenom_Payment.Width, HorizontalAlignment.Left)
            lstviewDenominationMaster.Columns.Add("Created User", lblDenom_created.Width, HorizontalAlignment.Left)
            lstviewDenominationMaster.View = View.Details
            lstviewDenominationMaster.GridLines = True
            lstviewDenominationMaster.FullRowSelect = True

            load_AllDenominationDetails()
            settingsType = "Denomination Master"
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiftmaster.Click
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
            If settingsType = "Denomination Master" Then
                If Not pnlDenominationAdd.Visible Then
                    pnlDenominationAdd.Height = lblDenominationSNo.Height + lstviewDenominationMaster.Height + 1
                    pnlDenominationAdd.BringToFront()
                    Dim i As Integer = pnlDenominationAdd.Height
                    While i >= lblDenominationSNo.Location.Y
                        pnlDenominationAdd.Location = New Point(lblDenominationSNo.Location.X, i)
                        pnlDenominationAdd.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlDenominationEdit.Visible = False

                    Currency_Codes.Clear()
                    stQuery = "select CURR_CODE, CURR_NAME FROM FM_CURRENCY where CURR_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Currency_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_CurrencyCodes.AddRange(Currency_Codes.ToArray)
                    txtSMAddCurrCode.AutoCompleteCustomSource = MySource_CurrencyCodes
                    txtSMAddCurrCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtSMAddCurrCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Money_Types.Clear()
                    Money_Types.Add("Coin")
                    Money_Types.Add("Note")
                    Money_Types.Add("Others")
                    MySource_MoneyTypes.AddRange(Money_Types.ToArray)
                    txtSMAddMoneyType.AutoCompleteCustomSource = MySource_MoneyTypes
                    txtSMAddMoneyType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtSMAddMoneyType.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Payment_Types.Clear()
                    stQuery = "SELECT VSSV_CODE , VSSV_CODE||'-'||VSSV_NAME AS DESCRIPTION FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Payment_Types.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_PaymentTypes.AddRange(Payment_Types.ToArray)
                    txtSMAddPaymentType.AutoCompleteCustomSource = MySource_PaymentTypes
                    txtSMAddPaymentType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtSMAddPaymentType.AutoCompleteSource = AutoCompleteSource.CustomSource

                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Denomination Master" Then
                pnlDenominationAdd.Hide()
                pnlDenominationEdit.Hide()
                pnlDenominationAdd.SendToBack()
                pnlDenominationEdit.SendToBack()
                load_AllDenominationDetails()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click
        Try
            If pnlDenominationAdd.Visible Then
                btnCounterAddCancel_Click(sender, e)
            End If
            If Not lstviewDenominationMaster.SelectedItems.Count > 0 Then
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
            If Not lstviewDenominationMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                If pnlDenominationAdd.Visible Then
                    btnCounterAddCancel_Click(sender, e)
                ElseIf pnlDenominationEdit.Visible Then
                    btnCounterEditCancel_Click(sender, e)
                End If
                Exit Sub
            Else
                If pnlDenominationAdd.Visible Then
                    btnCounterAddCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                ElseIf pnlDenominationEdit.Visible Then
                    btnCounterEditCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                End If
                Dim denomcode As String = lstviewDenominationMaster.SelectedItems.Item(0).SubItems(1).Text
                Dim currcode As String = lstviewDenominationMaster.SelectedItems.Item(0).SubItems(2).Text
                Dim stQuery As String
                stQuery = "delete from OM_POS_DENOMINATION where POSDENO_CODE='" & denomcode & "' AND POSDENO_CURR_CODE='" & currcode & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewDenominationMaster.SelectedItems.Clear()
                load_AllDenominationDetails()
            End If
        Catch ex As Exception
            If ex.Message.GetHashCode = "-1172840326" Then
                MsgBox("Counter No used by a location! Cannot be deleted!")
                lstviewDenominationMaster.SelectedItems.Clear()
                Exit Sub
            End If
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


     

    Private Sub btnCounterEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterEditCancel.Click
        Try
            Dim i As Integer = pnlDenominationEdit.Height
            While i > 0
                pnlDenominationEdit.Height = pnlDenominationEdit.Height - 1
                pnlDenominationEdit.Location = New Point(lblDenominationSNo.Location.X, pnlDenominationEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlDenominationEdit.Visible = False
            pnlDenominationEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterEditUpdate.Click
        Try
            Dim denomcode As String = txtDenomEditDenomCode.Text
            Dim currcode As String = txtDenomEditCurrCode.Text
            Dim moneytype As String = txtDenomEditMoneyType.Text
            Dim stQuery As String
            Dim ds As DataSet
            If txtDenomEditMoneyType.Text = "" Then
                MsgBox("Select a Money type from Autocompleted list")
                Exit Sub
            ElseIf txtDenomEditPaymentType.Text = "" Then
                MsgBox("Payment type cannot be empty!")
                Exit Sub
            ElseIf txtDenomEditPaymentDesc.Text = "" Then
                MsgBox("Please select a valid payment type!")
                Exit Sub
            End If
            stQuery = "Select POSDENO_CODE,POSDENO_CURR_CODE,POSDENO_NOTE_COIN_FLAG,POSDENO_PMT_TYPE,CURR_NAME from OM_POS_DENOMINATION a, FM_CURRENCY b where POSDENO_CODE='" & denomcode & "' and POSDENO_CURR_CODE='" & currcode & "' and b.CURR_CODE=a.POSDENO_CURR_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then

                stQuery = "UPDATE OM_POS_DENOMINATION SET POSDENO_PMT_TYPE='" & txtDenomEditPaymentType.Text & "', POSDENO_UPD_DT=to_date(sysdate,'DD-MM-YY'),POSDENO_UPD_UID='" & LogonUser & "' WHERE POSDENO_CODE='" & denomcode & "' and POSDENO_CURR_CODE='" & currcode & "' and upper(POSDENO_NOTE_COIN_FLAG)='" & moneytype.ToUpper & "'"
                errLog.WriteToErrorLog("Update Query OM_POS_DENOMINATION", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully")
                load_AllDenominationDetails()

            Else
                MsgBox("Not able to update!")
                Exit Sub
            End If
            Dim i As Integer = pnlDenominationEdit.Height
            While i > 0
                pnlDenominationEdit.Height = pnlDenominationEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlDenominationEdit.Visible = False
            pnlDenominationEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

     

    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddSave.Click
        Try
            Dim strArray() As String = {"Notes", "Coins", "Others"}

            If txtSMAddDenomCode.Text = "" Then
                MsgBox("Enter Denomination Code!")
                Exit Sub
            ElseIf txtSMAddCurrCode.Text = "" Then
                MsgBox("Enter Currency Code!")
                Exit Sub
            ElseIf txtSMAddCurrDesc.Text = "" Then
                MsgBox("Invalid Currency Code")
                Exit Sub
            ElseIf txtSMAddMoneyType.Text = "" Then
                MsgBox("Enter Money Type")
                Exit Sub


                'ElseIf Array.Exists(myStrings,delegate(string s) { return s.Equals(test); });
            ElseIf strArray.Contains(txtSMAddMoneyType.Text, StringComparer.CurrentCultureIgnoreCase) Then
                MsgBox("Invalid Money Type! Please check case sensitive!")
                Exit Sub
            ElseIf txtSMAddPaymentType.Text = "" Then
                MsgBox("Enter Payment Type!")
                Exit Sub
            ElseIf txtSMAddPaymentDesc.Text = "" Then
                MsgBox("Invalid Payment Type!")
                Exit Sub
            Else
                Dim stQuery As String = ""
                Dim ds As DataSet
                stQuery = "SELECT POSDENO_CODE FROM OM_POS_DENOMINATION WHERE POSDENO_CODE = '" & txtSMAddDenomCode.Text & "' AND POSDENO_CURR_CODE = '" & txtSMAddCurrCode.Text & "' AND POSDENO_NOTE_COIN_FLAG = '" & txtSMAddMoneyType.Text.Substring(0, 1) & "'" '" & txtCounterAddLocationCode.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then

                    stQuery = "INSERT INTO OM_POS_DENOMINATION (POSDENO_CODE,POSDENO_CURR_CODE,POSDENO_NOTE_COIN_FLAG,POSDENO_PMT_TYPE,POSDENO_CR_DT,POSDENO_CR_UID) VALUES ("
                    stQuery = stQuery & "'" & txtSMAddDenomCode.Text & "','" & txtSMAddCurrCode.Text & "','" & txtSMAddMoneyType.Text.Substring(0, 1) & "','" & txtSMAddPaymentType.Text & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                    errLog.WriteToErrorLog("Insert Query OM_POS_COUNTER", stQuery, "")
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Denomination Saved Successfully")
                    txtSMAddDenomCode.Text = ""
                    txtSMAddCurrCode.Text = ""
                    txtSMAddCurrDesc.Text = ""
                    txtSMAddMoneyType.Text = ""
                    txtSMAddPaymentType.Text = ""
                    txtSMAddPaymentDesc.Text = ""

                    load_AllDenominationDetails()
                Else
                    MsgBox("Denomination Code already exists!")
                    Exit Sub
                End If

            End If

            Dim i As Integer = pnlDenominationAdd.Height
            While i > 0
                pnlDenominationAdd.Height = pnlDenominationAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlDenominationAdd.Visible = False
            pnlDenominationAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCounterAddCancel.Click
        Try
            Dim i As Integer = pnlDenominationAdd.Height
            While i > 0
                pnlDenominationAdd.Height = pnlDenominationAdd.Height - 1
                pnlDenominationAdd.Location = New Point(lblDenominationSNo.Location.X, pnlDenominationAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlDenominationAdd.Visible = False
            pnlDenominationAdd.SendToBack()
            txtSMAddDenomCode.Text = ""
            txtSMAddMoneyType.Text = ""
            txtSMAddCurrCode.Text = ""
            txtSMAddCurrDesc.Text = ""
            txtSMAddPaymentType.Text = ""
            txtSMAddPaymentDesc.Text = ""
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lstviewCounterMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewDenominationMaster.DoubleClick
        Try

            If Not lstviewDenominationMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If

            Money_Types.Clear()
            Money_Types.Add("Coin")
            Money_Types.Add("Note")
            Money_Types.Add("Others")
            MySource_MoneyTypes.AddRange(Money_Types.ToArray)
            txtDenomEditMoneyType.AutoCompleteCustomSource = MySource_MoneyTypes
            txtDenomEditMoneyType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtDenomEditMoneyType.AutoCompleteSource = AutoCompleteSource.CustomSource

            Dim denomCode As String = lstviewDenominationMaster.SelectedItems.Item(0).SubItems(1).Text
            Dim currCode As String = lstviewDenominationMaster.SelectedItems.Item(0).SubItems(2).Text
            Dim moneytype As String = lstviewDenominationMaster.SelectedItems.Item(0).SubItems(3).Text

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select POSDENO_CODE,POSDENO_CURR_CODE,POSDENO_NOTE_COIN_FLAG,POSDENO_PMT_TYPE,CURR_NAME from OM_POS_DENOMINATION a, FM_CURRENCY b where POSDENO_CODE='" & denomCode & "' and POSDENO_CURR_CODE='" & currCode & "' and upper(POSDENO_NOTE_COIN_FLAG)='" & moneytype.ToUpper & "' and b.CURR_CODE=a.POSDENO_CURR_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then

                If Not pnlDenominationEdit.Visible Then
                    pnlDenominationEdit.Height = lblDenominationSNo.Height + lstviewDenominationMaster.Height + 1
                    pnlDenominationEdit.BringToFront()
                    Dim i As Integer = pnlDenominationEdit.Height
                    While i >= lblDenominationSNo.Location.Y
                        pnlDenominationEdit.Location = New Point(lblDenominationSNo.Location.X, i)
                        pnlDenominationEdit.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlDenominationAdd.Visible = False

                End If

                Payment_Types.Clear()
                Dim row As System.Data.DataRow
                Dim dsI As DataSet
                stQuery = "SELECT VSSV_CODE , VSSV_CODE||'-'||VSSV_NAME AS DESCRIPTION FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2"
                dsI = db.SelectFromTableODBC(stQuery)
                Dim count As Integer
                Dim k As Integer
                count = dsI.Tables("Table").Rows.Count
                k = 0
                While count > 0
                    row = dsI.Tables("Table").Rows.Item(k)
                    Payment_Types.Add(row.Item(0).ToString)
                    k = k + 1
                    count = count - 1
                End While
                MySource_PaymentTypes.AddRange(Payment_Types.ToArray)
                txtDenomEditPaymentType.AutoCompleteCustomSource = MySource_PaymentTypes
                txtDenomEditPaymentType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtDenomEditPaymentType.AutoCompleteSource = AutoCompleteSource.CustomSource


                txtDenomEditDenomCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                txtDenomEditCurrCode.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString

                If ds.Tables("Table").Rows.Item(0).Item(2).ToString.ToUpper = "N" Then
                    txtDenomEditMoneyType.Text = "Notes"
                ElseIf ds.Tables("Table").Rows.Item(0).Item(2).ToString.ToUpper = "C" Then
                    txtDenomEditMoneyType.Text = "Coins"
                ElseIf ds.Tables("Table").Rows.Item(0).Item(2).ToString.ToUpper = "O" Then
                    txtDenomEditMoneyType.Text = "Others"
                End If
                txtDenomEditPaymentType.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString
                txtDenomEditCurrDesc.Text = ds.Tables("Table").Rows.Item(0).Item(4).ToString

                stQuery = "SELECT VSSV_CODE||'-'||VSSV_NAME AS DESCRIPTION FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='" & txtDenomEditPaymentType.Text & "'"
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    txtDenomEditPaymentDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                End If
                lstviewDenominationMaster.SelectedItems.Clear()
            Else
                MsgBox("Not available for edit")
                lstviewDenominationMaster.SelectedItems.Clear()
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

    Private Sub txtSMAddCurrCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddCurrCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select CURR_NAME FROM FM_CURRENCY where CURR_CODE='" & txtSMAddCurrCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddCurrDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtSMAddCurrDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddPaymentType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddPaymentType.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT VSSV_CODE||'-'||VSSV_NAME AS DESCRIPTION FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 AND VSSV_CODE='" & txtSMAddPaymentType.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddPaymentDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtSMAddPaymentDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    
    Private Sub txtDenomEditPaymentType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDenomEditPaymentType.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT VSSV_CODE||'-'||VSSV_NAME AS DESCRIPTION FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 AND VSSV_CODE='" & txtDenomEditPaymentType.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtDenomEditPaymentDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtDenomEditPaymentDesc.Text = ""
            End If
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

   

   

    Private Sub btnDenominationMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenominationMaster.Click

    End Sub
End Class