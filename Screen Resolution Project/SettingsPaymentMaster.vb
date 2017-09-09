Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class SettingsPaymentMaster
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim MainAcc_Codes As New List(Of String)
    Dim SubAcc_Codes As New List(Of String)
    Dim Division_Codes As New List(Of String)
    Dim Dept_Codes As New List(Of String)
    Dim PaymentType_Codes As New List(Of String)
    Dim MySource_MainAccCodes As New AutoCompleteStringCollection()
    Dim MySource_SubAccCodes As New AutoCompleteStringCollection()
    Dim MySource_DivisionCodes As New AutoCompleteStringCollection()
    Dim MySource_DeptCodes As New AutoCompleteStringCollection()
    Dim MySource_PaymentTypeCodes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

       
        'settingsType = "Payment Master"

    End Sub

    Private Sub load_AllPaymentDetails()
        Try
            lstviewPaymentMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "SELECT PPD_CODE,PPD_NAME,PPD_MAIN_ACNT_CODE,PPD_SUB_ACNT_CODE,PPD_DIVN_CODE,PPD_TYPE,PPD_CR_UID,PPD_FRZ_FLAG_NUM from OM_POS_PAYMENT_DET order by PPD_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewPaymentMaster.Items.Add(i + 1)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(4).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(5).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(6).ToString)
                lstviewPaymentMaster.Items(i).SubItems.Add(row.Item(7).ToString)
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

                For Each ctl As Control In pnl_pay_Shift.Controls
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

                For Each ctl As Control In pnl_pay_Counter.Controls
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
                For Each ctl As Control In pnl_pay_denom.Controls
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

                For Each ctl As Control In pnl_pay_sales.Controls
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

                For Each ctl As Control In pnl_pay_Paymentmaster.Controls
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

                For Each ctl As Control In pnlPaymentMaster.Controls
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



                '

                For Each ctl As Control In pnlPaymentAdd.Controls
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

                For Each ctl As Control In pnlPaymentEdit.Controls
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
            lstviewPaymentMaster.Columns.Add("SNo", lblPaymentSNo.Width - 3, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Payment Code", lblPaymentCode.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Payment Description", lblPaymentDesc.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Main Acc. Code", lblmain_acc.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Sub Acc. Code", lblSubAcc.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Div. Code", lblDivCode.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Payment Code", lblPaymentType.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Created User", lblpaymentcreated.Width, HorizontalAlignment.Left)
            lstviewPaymentMaster.Columns.Add("Status", lblpaymentStatus.Width - 18, HorizontalAlignment.Left)
            lstviewPaymentMaster.View = View.Details
            lstviewPaymentMaster.GridLines = True
            lstviewPaymentMaster.FullRowSelect = True

            load_AllPaymentDetails()
            settingsType = "Payment Master"
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
            If settingsType = "Payment Master" Then
                If Not pnlPaymentAdd.Visible Then
                    pnlPaymentAdd.Height = lblPaymentSNo.Height + lstviewPaymentMaster.Height + 1
                    pnlPaymentAdd.BringToFront()
                    Dim i As Integer = pnlPaymentAdd.Height
                    While i >= lblPaymentSNo.Location.Y
                        pnlPaymentAdd.Location = New Point(lblPaymentSNo.Location.X, i)
                        pnlPaymentAdd.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlPaymentEdit.Visible = False
                    MainAcc_Codes.Clear()
                    stQuery = "SELECT MAIN_ACNT_CODE, MAIN_ACNT_CODE||'-'||MAIN_ACNT_NAME AS ACCNAME FROM FM_MAIN_ACCOUNT WHERE MAIN_FRZ_FLAG = 'N'"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        MainAcc_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_MainAccCodes.AddRange(MainAcc_Codes.ToArray)
                    txtPaymentAddMainAccCode.AutoCompleteCustomSource = MySource_MainAccCodes
                    txtPaymentAddMainAccCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtPaymentAddMainAccCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Division_Codes.Clear()
                    stQuery = "SELECT DIVN_CODE, DIVN_CODE||'-'||DIVN_NAME AS DIVNAME FROM FM_DIVISION WHERE DIVN_FRZ_FLAG = 'N'"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Division_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_DivisionCodes.AddRange(Division_Codes.ToArray)
                    txtPaymentAddDivCode.AutoCompleteCustomSource = MySource_DivisionCodes
                    txtPaymentAddDivCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtPaymentAddDivCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    Dept_Codes.Clear()
                    stQuery = "SELECT DEPT_CODE, DEPT_CODE||'-'||DEPT_NAME AS DeptName FROM FM_DEPARTMENT WHERE DEPT_FRZ_FLAG = 'N'"
                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Dept_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_DeptCodes.AddRange(Dept_Codes.ToArray)
                    txtPaymentAddDeptCode.AutoCompleteCustomSource = MySource_DeptCodes
                    txtPaymentAddDeptCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtPaymentAddDeptCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                    PaymentType_Codes.Clear()
                    stQuery = "SELECT VSSV_CODE, VSSV_CODE||'-'||VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        PaymentType_Codes.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                    MySource_PaymentTypeCodes.AddRange(PaymentType_Codes.ToArray)
                    txtPaymentAddPayType.AutoCompleteCustomSource = MySource_PaymentTypeCodes
                    txtPaymentAddPayType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    txtPaymentAddPayType.AutoCompleteSource = AutoCompleteSource.CustomSource

                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Payment Master" Then
                pnlPaymentAdd.Hide()
                pnlPaymentEdit.Hide()
                pnlPaymentAdd.SendToBack()
                pnlPaymentEdit.SendToBack()
                load_AllPaymentDetails()
            End If
            Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")

        End Try

    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click
        Try
            If pnlPaymentAdd.Visible Then
                btnPaymentAddCancel_Click(sender, e)
            End If
            If Not lstviewPaymentMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                lstviewPaymentMaster_DoubleClick(sender, e)
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsDelete.Click
        Try
            If Not lstviewPaymentMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                If pnlPaymentAdd.Visible Then
                    btnPaymentAddCancel_Click(sender, e)
                ElseIf pnlPaymentEdit.Visible Then
                    btnPaymentEditCancel_Click(sender, e)
                End If
                Exit Sub
            Else
                If pnlPaymentAdd.Visible Then
                    btnPaymentAddCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                ElseIf pnlPaymentEdit.Visible Then
                    btnPaymentEditCancel_Click(sender, e)
                    btnSettingsDelete_Click(sender, e)
                    Exit Sub
                End If
                Dim paymentcode As String = lstviewPaymentMaster.SelectedItems.Item(0).SubItems(1).Text
                'Dim counterlocncode As String = lstviewPaymentMaster.SelectedItems.Item(0).SubItems(3).Text
                Dim stQuery As String
                stQuery = "delete from OM_POS_PAYMENT_DET where PPD_CODE='" & paymentcode & "'"
                errLog.WriteToErrorLog("OM_POS_PAYMENT_DET", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewPaymentMaster.SelectedItems.Clear()
                load_AllPaymentDetails()
            End If
        Catch ex As Exception
            'MsgBox(ex.Message.GetHashCode)
            If ex.Message.GetHashCode = "-1796980732" Then
                MsgBox("Payment Code in use! Cannot be deleted!")
                lstviewPaymentMaster.SelectedItems.Clear()
                Exit Sub
            Else
                MsgBox("Payment Code in use! Cannot be deleted!")
                lstviewPaymentMaster.SelectedItems.Clear()
                Exit Sub
            End If
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnCounterEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = pnlPaymentEdit.Height
            While i > 0
                pnlPaymentEdit.Height = pnlPaymentEdit.Height - 1
                pnlPaymentEdit.Location = New Point(lblPaymentSNo.Location.X, pnlPaymentEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentEdit.Visible = False
            pnlPaymentEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim counterno As String = "" 'txtCounterEditCountNo.Text
            Dim counterlocncode As String = "" 'txtCounterEditLocationCode.Text
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,LOCN_NAME,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME from OM_POS_COUNTER a,OM_LOCATION b where POSCNT_NO='" & counterno & "' and POSCNT_LOCN_CODE='" & counterlocncode & "' and a.POSCNT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then
                Dim freeze As String

                If chkboxPaymentEditFreeze.Checked = True Then
                    freeze = "1"
                Else
                    freeze = "2"
                End If
                stQuery = "UPDATE OM_POS_COUNTER SET POSCNT_NAME='" & "" & "',POSCNT_FRZ_FLAG_NUM=" & freeze & ",POSCNT_IP_ADDRESS='" & "" & "',POSCNT_COMPUTER_NAME='" & "" & "',POSCNT_UP_DT=to_date(sysdate,'DD-MM-YY'),POSCNT_UP_UID='" & LogonUser & "' WHERE POSCNT_LOCN_CODE='" & counterlocncode & "' and POSCNT_NO='" & counterno & "'"
                errLog.WriteToErrorLog("Update Query OM_POS_SHIFT", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully")
                load_AllPaymentDetails()

            Else
                MsgBox("Not able to update!")
                Exit Sub
            End If
            Dim i As Integer = pnlPaymentEdit.Height
            While i > 0
                pnlPaymentEdit.Height = pnlPaymentEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentEdit.Visible = False
            pnlPaymentEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtPaymentAddPaymentCode.Text = "" Then
                MsgBox("Enter Counter No!")
                Exit Sub
            ElseIf txtPaymentAddPaymentDesc.Text = "" Then
                MsgBox("Enter Counter Name!")
                Exit Sub
            ElseIf txtPaymentAddMainAccCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub

            Else
                If Not txtPaymentAddMainAccDesc.Text = "" Then
                    Dim stQuery As String
                    Dim ds As DataSet
                    stQuery = "SELECT POSCNT_NO FROM OM_POS_COUNTER WHERE POSCNT_NO = '" & txtPaymentAddPaymentCode.Text & "' AND POSCNT_LOCN_CODE = '" & txtPaymentAddMainAccCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        Dim freeze As String
                        If chkboxPaymentAddFreeze.Checked = True Then
                            freeze = "1"
                        Else
                            freeze = "2"
                        End If
                        stQuery = "INSERT INTO OM_POS_COUNTER(POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME,POSCNT_CR_DT,POSCNT_CR_UID)VALUES("
                        ' stQuery = stQuery & "'" & txtCounterAddCountNo.Text & "','" & txtCounterAddCountName.Text & "','" & txtCounterAddLocationCode.Text & "'," & freeze & ",'" & txtCounterAddIPAddr.Text & "','" & txtCounterAddCompName.Text & "',to_date(sysdate,'DD-MON-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("Insert Query OM_POS_COUNTER", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                        MsgBox("Counter Saved Successfully")
                        txtPaymentAddPaymentCode.Text = ""
                        txtPaymentAddPaymentDesc.Text = ""
                        txtPaymentAddPayShortName.Text = ""
                        'txtCounterAddCompName.Text = ""
                        txtPaymentAddMainAccCode.Text = ""
                        txtPaymentAddMainAccDesc.Text = ""
                        load_AllPaymentDetails()
                    Else
                        MsgBox("Counter No. already exists in this location!")
                        Exit Sub
                    End If
                Else
                    MsgBox("Please select a valid location!")
                    Exit Sub
                End If
            End If

            Dim i As Integer = pnlPaymentAdd.Height
            While i > 0
                pnlPaymentAdd.Height = pnlPaymentAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentAdd.Visible = False
            pnlPaymentAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = pnlPaymentAdd.Height
            While i > 0
                pnlPaymentAdd.Height = pnlPaymentAdd.Height - 1
                pnlPaymentAdd.Location = New Point(lblPaymentSNo.Location.X, pnlPaymentAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentAdd.Visible = False
            pnlPaymentAdd.SendToBack()
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

    Private Sub txtPaymentAddMainAccCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAddMainAccCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT MAIN_ACNT_NAME AS ACCNAME FROM FM_MAIN_ACCOUNT WHERE MAIN_FRZ_FLAG = 'N' and MAIN_ACNT_CODE='" & txtPaymentAddMainAccCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentAddMainAccDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString

                stQuery = "SELECT MS_SUB_ACNT_CODE, MS_SUB_ACNT_NAME FROM FM_MAIN_SUB WHERE MS_MAIN_ACNT_CODE = '" & txtPaymentAddMainAccCode.Text & "'"
                'SELECT VSSV_CODE, VSSV_NAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2
                ds = db.SelectFromTableODBC(stQuery)

                SubAcc_Codes.Clear()
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow

                count = ds.Tables("Table").Rows.Count
                i = 0
                If Not count > 0 Then
                    txtPaymentAddSubAccCode.Text = ""
                    txtPaymentAddSubAccDesc.Text = ""
                    SubAcc_Codes.Clear()
                    txtPaymentAddSubAccCode.AutoCompleteSource = AutoCompleteSource.None
                End If
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    SubAcc_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_SubAccCodes.AddRange(SubAcc_Codes.ToArray)
                txtPaymentAddSubAccCode.AutoCompleteCustomSource = MySource_SubAccCodes
                txtPaymentAddSubAccCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentAddSubAccCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            Else
                txtPaymentAddMainAccDesc.Text = ""
                txtPaymentAddSubAccCode.Text = ""
                txtPaymentAddSubAccDesc.Text = ""
                SubAcc_Codes.Clear()
                txtPaymentAddSubAccCode.AutoCompleteSource = AutoCompleteSource.None
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub txtPaymentAddDivCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAddDivCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT DIVN_NAME AS DIVNAME FROM FM_DIVISION WHERE DIVN_FRZ_FLAG = 'N' and DIVN_CODE='" & txtPaymentAddDivCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentAddDivDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentAddDivDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentAddDeptCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAddDeptCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT DEPT_NAME AS DeptName FROM FM_DEPARTMENT WHERE DEPT_FRZ_FLAG = 'N' and DEPT_CODE='" & txtPaymentAddDeptCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentAddDeptDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentAddDeptDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentAddPayType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAddPayType.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='" & txtPaymentAddPayType.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentAddPayDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentAddPayDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentAddSubAccCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAddSubAccCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT MS_SUB_ACNT_NAME FROM FM_MAIN_SUB WHERE MS_SUB_ACNT_CODE='" & txtPaymentAddSubAccCode.Text & "' and MS_MAIN_ACNT_CODE = '" & txtPaymentAddMainAccCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentAddSubAccDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentAddSubAccDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnPaymentAddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentAddSave.Click
        Try
            If txtPaymentAddPaymentCode.Text = "" Then
                MsgBox("Enter Payment Code!")
                Exit Sub
            ElseIf txtPaymentAddPaymentDesc.Text = "" Then
                MsgBox("Enter Payment Description!")
                Exit Sub
            ElseIf txtPaymentAddPayShortName.Text = "" Then
                MsgBox("Enter Payment Short Name!")
                Exit Sub
            ElseIf txtPaymentAddMainAccCode.Text = "" Then
                MsgBox("Enter Main Account Code!")
                Exit Sub
            ElseIf txtPaymentAddMainAccDesc.Text = "" Then
                MsgBox("Enter a valid Main Account Code!")
                Exit Sub
            ElseIf txtPaymentAddSubAccCode.Text = "" And SubAcc_Codes.Count > 0 Then
                MsgBox("Enter Sub Account Code!")
                Exit Sub
            ElseIf txtPaymentAddSubAccCode.Text = "" And SubAcc_Codes.Count > 0 Then
                MsgBox("Enter a valid sub Account Code!")
                Exit Sub
            ElseIf txtPaymentAddDivCode.Text = "" Then
                MsgBox("Enter Division Code!")
                Exit Sub
            ElseIf txtPaymentAddDivDesc.Text = "" Then
                MsgBox("Enter a valid Division Code!")
                Exit Sub
            ElseIf txtPaymentAddDeptCode.Text = "" Then
                MsgBox("Enter Department Code!")
                Exit Sub
            ElseIf txtPaymentAddDeptDesc.Text = "" Then
                MsgBox("Enter a valid Department Code!")
                Exit Sub
            ElseIf txtPaymentAddPayType.Text = "" Then
                MsgBox("Enter Payment Type!")
                Exit Sub
            ElseIf txtPaymentAddPayDesc.Text = "" Then
                MsgBox("Enter a valid Payment Type!")
                Exit Sub
            Else
                Dim stQuery As String
                    Dim ds As DataSet
                stQuery = "SELECT PPD_CODE from OM_POS_PAYMENT_DET where PPD_CODE= '" & txtPaymentAddPaymentCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                    Dim freeze As String
                    Dim descReq As String

                    If chkboxPaymentAddFreeze.Checked = True Then
                        freeze = "1"
                    Else
                        freeze = "2"
                    End If

                    If chkboxPaymentAddPayDescMandatoryYN.Checked = True Then
                        descReq = "1"
                    Else
                        descReq = "2"
                    End If
                    stQuery = "INSERT INTO OM_POS_PAYMENT_DET (PPD_CODE,PPD_NAME,PPD_SHORT_NAME,PPD_MAIN_ACNT_CODE,PPD_SUB_ACNT_CODE,PPD_DIVN_CODE,PPD_DEPT_CODE,PPD_TYPE,PPD_CHARGE_PERC,PPD_FRZ_FLAG_NUM,PPD_DESC_REQ_NUM,PPD_CR_DT,PPD_CR_UID) VALUES ("
                    stQuery = stQuery & "'" & txtPaymentAddPaymentCode.Text & "','" & txtPaymentAddPaymentDesc.Text & "','" & txtPaymentAddPayShortName.Text & "','" & txtPaymentAddMainAccCode.Text & "','" & txtPaymentAddSubAccCode.Text & "','" & txtPaymentAddDivCode.Text & "','" & txtPaymentAddDeptCode.Text & "','" & txtPaymentAddPayType.Text & "',0," & freeze & "," & descReq & ",to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                    errLog.WriteToErrorLog("Insert Query OM_POS_PAYMENT_DET", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                    MsgBox("Payment Saved Successfully")
                    txtPaymentAddPaymentCode.Text = ""
                        txtPaymentAddPaymentDesc.Text = ""
                        txtPaymentAddPayShortName.Text = ""

                        txtPaymentAddMainAccCode.Text = ""
                    txtPaymentAddMainAccDesc.Text = ""
                    txtPaymentAddSubAccCode.Text = ""
                    txtPaymentAddSubAccDesc.Text = ""
                    txtPaymentAddDivCode.Text = ""
                    txtPaymentAddDivDesc.Text = ""
                    txtPaymentAddDeptCode.Text = ""
                    txtPaymentAddDeptDesc.Text = ""
                    txtPaymentAddPayType.Text = ""
                    txtPaymentAddPayDesc.Text = ""
                    chkboxPaymentAddFreeze.CheckState = CheckState.Unchecked
                    chkboxPaymentAddPayDescMandatoryYN.CheckState = CheckState.Unchecked
                        load_AllPaymentDetails()
                    Else
                    MsgBox("Payment Code Exists already!")
                        Exit Sub
                    End If
                
            End If

                Dim i As Integer = pnlPaymentAdd.Height
                While i > 0
                    pnlPaymentAdd.Height = pnlPaymentAdd.Height - 1
                    'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                    i = i - 1
                    Threading.Thread.Sleep(0.5)
                End While

                pnlPaymentAdd.Visible = False
                pnlPaymentAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    
    Private Sub btnPaymentAddCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentAddCancel.Click
        Try
            Dim i As Integer = pnlPaymentAdd.Height
            While i > 0
                pnlPaymentAdd.Height = pnlPaymentAdd.Height - 1
                pnlPaymentAdd.Location = New Point(lblPaymentSNo.Location.X, pnlPaymentAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentAdd.Visible = False
            pnlPaymentAdd.SendToBack()
            txtPaymentAddPaymentCode.Text = ""
            txtPaymentAddPayDesc.Text = ""
            txtPaymentAddPayShortName.Text = ""
            txtPaymentAddPaymentDesc.Text = ""
            txtPaymentAddMainAccCode.Text = ""
            txtPaymentAddSubAccCode.Text = ""
            txtPaymentAddDivCode.Text = ""
            txtPaymentAddDeptCode.Text = ""
            txtPaymentAddDeptDesc.Text = ""
            txtPaymentAddPayType.Text = ""
            chkboxPaymentAddPayDescMandatoryYN.CheckState = CheckState.Unchecked


            chkboxPaymentAddFreeze.CheckState = CheckState.Unchecked

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    
    Private Sub btnPaymentEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentEditUpdate.Click
        Try
            If txtPaymentEditPaymentCode.Text = "" Then
                MsgBox("Enter Payment Code!")
                Exit Sub
            ElseIf txtPaymentEditPaymentDesc.Text = "" Then
                MsgBox("Enter Payment Description!")
                Exit Sub
            ElseIf txtPaymentEditPayShortName.Text = "" Then
                MsgBox("Enter Payment Short Name!")
                Exit Sub
            ElseIf txtPaymentEditMainAccCode.Text = "" Then
                MsgBox("Enter Main Account Code!")
                Exit Sub
            ElseIf txtPaymentEditMainAccDesc.Text = "" Then
                MsgBox("Enter a valid Main Account Code!")
                Exit Sub
            ElseIf txtPaymentEditSubAccCode.Text = "" And SubAcc_Codes.Count > 0 Then
                MsgBox("Enter Sub Account Code!")
                Exit Sub
            ElseIf txtPaymentEditSubAccCode.Text = "" And SubAcc_Codes.Count > 0 Then
                MsgBox("Enter a valid sub Account Code!")
                Exit Sub
            ElseIf txtPaymentEditDivCode.Text = "" Then
                MsgBox("Enter Division Code!")
                Exit Sub
            ElseIf txtPaymentEditDivDesc.Text = "" Then
                MsgBox("Enter a valid Division Code!")
                Exit Sub
            ElseIf txtPaymentEditDeptCode.Text = "" Then
                MsgBox("Enter Department Code!")
                Exit Sub
            ElseIf txtPaymentEditDeptDesc.Text = "" Then
                MsgBox("Enter a valid Department Code!")
                Exit Sub
            ElseIf txtPaymentEditPayType.Text = "" Then
                MsgBox("Enter Payment Type!")
                Exit Sub
            ElseIf txtPaymentEditPayDesc.Text = "" Then
                MsgBox("Enter a valid Payment Type!")
                Exit Sub
            End If

            Dim paymentcode As String = txtPaymentEditPaymentCode.Text
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT PPD_CODE,PPD_NAME,PPD_SHORT_NAME,PPD_MAIN_ACNT_CODE,PPD_SUB_ACNT_CODE,PPD_DIVN_CODE,PPD_DEPT_CODE,PPD_TYPE,PPD_FRZ_FLAG_NUM,PPD_DESC_REQ_NUM from OM_POS_PAYMENT_DET where PPD_CODE= '" & paymentcode & "'"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then
                Dim freeze As String
                Dim descReqNum As String
                If chkboxPaymentEditFreeze.Checked = True Then
                    freeze = "1"
                Else
                    freeze = "2"
                End If
                If chkboxPaymentEditPayDescMandatoryYN.Checked = True Then
                    descReqNum = "1"
                Else
                    descReqNum = "2"
                End If
                stQuery = "UPDATE OM_POS_PAYMENT_DET SET PPD_NAME='" & txtPaymentEditPaymentDesc.Text & "',PPD_FRZ_FLAG_NUM=" & freeze & ",PPD_SHORT_NAME='" & txtPaymentEditPayShortName.Text & "',PPD_MAIN_ACNT_CODE='" & txtPaymentEditMainAccCode.Text & "',PPD_SUB_ACNT_CODE='" & txtPaymentEditSubAccCode.Text & "',PPD_DIVN_CODE='" & txtPaymentEditDivCode.Text & "',PPD_DEPT_CODE='" & txtPaymentEditDeptCode.Text & "',PPD_TYPE='" & txtPaymentEditPayType.Text & "',PPD_DESC_REQ_NUM=" & descReqNum & ",PPD_UPD_DT=to_date(sysdate,'DD-MM-YY'),PPD_UPD_UID='" & LogonUser & "' WHERE PPD_CODE='" & paymentcode & "'"
                errLog.WriteToErrorLog("Update Query OM_POS_SHIFT", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully")
                load_AllPaymentDetails()

            Else
                MsgBox("Not able to update!")
                Exit Sub
            End If
            Dim i As Integer = pnlPaymentEdit.Height
            While i > 0
                pnlPaymentEdit.Height = pnlPaymentEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentEdit.Visible = False
            pnlPaymentEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lstviewPaymentMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewPaymentMaster.DoubleClick
        Try
            If Not lstviewPaymentMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If
            Dim paymentcode As String = lstviewPaymentMaster.SelectedItems.Item(0).SubItems(1).Text

            Dim stQuery As String
            Dim ds As DataSet
            Dim i As Integer = 0
            Dim count As Integer
            stQuery = "SELECT PPD_CODE,PPD_NAME,PPD_SHORT_NAME,PPD_MAIN_ACNT_CODE,PPD_SUB_ACNT_CODE,PPD_DIVN_CODE,PPD_DEPT_CODE,PPD_TYPE,PPD_FRZ_FLAG_NUM,PPD_DESC_REQ_NUM from OM_POS_PAYMENT_DET where PPD_CODE= '" & paymentcode & "'"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then

                If Not pnlPaymentEdit.Visible Then
                    pnlPaymentEdit.Height = lblPaymentSNo.Height + lstviewPaymentMaster.Height + 1
                    pnlPaymentEdit.BringToFront()
                    i = pnlPaymentEdit.Height
                    While i >= lblPaymentSNo.Location.Y
                        pnlPaymentEdit.Location = New Point(lblPaymentSNo.Location.X, i)
                        pnlPaymentEdit.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlPaymentAdd.Visible = False

                End If
                txtPaymentEditPaymentCode.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                txtPaymentEditPaymentDesc.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                txtPaymentEditPayShortName.Text = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                txtPaymentEditMainAccCode.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString
                txtPaymentEditMainAccCode_TextChanged(sender, e)
                txtPaymentEditSubAccCode.Text = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                txtPaymentEditSubAccCode_TextChanged(sender, e)
                txtPaymentEditDivCode.Text = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                txtPaymentEditDivCode_TextChanged(sender, e)
                txtPaymentEditDeptCode.Text = ds.Tables("Table").Rows.Item(0).Item(6).ToString
                txtPaymentEditDeptCode_TextChanged(sender, e)
                txtPaymentEditPayType.Text = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                txtPaymentEditPayType_TextChanged(sender, e)
                If ds.Tables("Table").Rows.Item(0).Item(8).ToString = "1" Then
                    chkboxPaymentEditFreeze.CheckState = CheckState.Checked
                    chkboxPaymentEditFreeze.Enabled = True
                ElseIf ds.Tables("Table").Rows.Item(0).Item(8).ToString = "2" Then
                    chkboxPaymentEditFreeze.CheckState = CheckState.Unchecked
                    chkboxPaymentEditFreeze.Enabled = True
                End If
                'MsgBox(ds.Tables("Table").Rows.Item(0).Item(9).ToString)
                If ds.Tables("Table").Rows.Item(0).Item(9).ToString = "1" Then
                    chkboxPaymentEditPayDescMandatoryYN.CheckState = CheckState.Checked
                    'chkboxPaymentEditPayDescMandatoryYN.Enabled = False
                ElseIf ds.Tables("Table").Rows.Item(0).Item(9).ToString = "2" Then
                    chkboxPaymentEditPayDescMandatoryYN.CheckState = CheckState.Unchecked
                    'chkboxPaymentEditFreeze.Enabled = True
                End If


                Dim row As System.Data.DataRow
                MainAcc_Codes.Clear()
                stQuery = "SELECT MAIN_ACNT_CODE, MAIN_ACNT_CODE||'-'||MAIN_ACNT_NAME AS ACCNAME FROM FM_MAIN_ACCOUNT WHERE MAIN_FRZ_FLAG = 'N'"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    MainAcc_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_MainAccCodes.AddRange(MainAcc_Codes.ToArray)
                txtPaymentEditMainAccCode.AutoCompleteCustomSource = MySource_MainAccCodes
                txtPaymentEditMainAccCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentEditMainAccCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                Division_Codes.Clear()
                stQuery = "SELECT DIVN_CODE, DIVN_CODE||'-'||DIVN_NAME AS DIVNAME FROM FM_DIVISION WHERE DIVN_FRZ_FLAG = 'N'"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Division_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_DivisionCodes.AddRange(Division_Codes.ToArray)
                txtPaymentEditDivCode.AutoCompleteCustomSource = MySource_DivisionCodes
                txtPaymentEditDivCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentEditDivCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                Dept_Codes.Clear()
                stQuery = "SELECT DEPT_CODE, DEPT_CODE||'-'||DEPT_NAME AS DeptName FROM FM_DEPARTMENT WHERE DEPT_FRZ_FLAG = 'N'"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Dept_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_DeptCodes.AddRange(Dept_Codes.ToArray)
                txtPaymentEditDeptCode.AutoCompleteCustomSource = MySource_DeptCodes
                txtPaymentEditDeptCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentEditDeptCode.AutoCompleteSource = AutoCompleteSource.CustomSource

                PaymentType_Codes.Clear()
                stQuery = "SELECT VSSV_CODE, VSSV_CODE||'-'||VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    PaymentType_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_PaymentTypeCodes.AddRange(PaymentType_Codes.ToArray)
                txtPaymentEditPayType.AutoCompleteCustomSource = MySource_PaymentTypeCodes
                txtPaymentEditPayType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentEditPayType.AutoCompleteSource = AutoCompleteSource.CustomSource

                lstviewPaymentMaster.SelectedItems.Clear()
            Else
                MsgBox("Not available for edit")
                lstviewPaymentMaster.SelectedItems.Clear()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentEditMainAccCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentEditMainAccCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT MAIN_ACNT_NAME AS ACCNAME FROM FM_MAIN_ACCOUNT WHERE MAIN_FRZ_FLAG = 'N' and MAIN_ACNT_CODE='" & txtPaymentEditMainAccCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentEditMainAccDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString

                stQuery = "SELECT MS_SUB_ACNT_CODE, MS_SUB_ACNT_NAME FROM FM_MAIN_SUB WHERE MS_MAIN_ACNT_CODE = '" & txtPaymentEditMainAccCode.Text & "'"
                'SELECT VSSV_CODE, VSSV_NAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2
                ds = db.SelectFromTableODBC(stQuery)

                SubAcc_Codes.Clear()
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow

                count = ds.Tables("Table").Rows.Count
                i = 0
                If Not count > 0 Then
                    txtPaymentEditSubAccCode.Text = ""
                    txtPaymentEditSubAccDesc.Text = ""
                    SubAcc_Codes.Clear()
                    txtPaymentEditSubAccCode.AutoCompleteSource = AutoCompleteSource.None
                End If
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    SubAcc_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                MySource_SubAccCodes.AddRange(SubAcc_Codes.ToArray)
                txtPaymentEditSubAccCode.AutoCompleteCustomSource = MySource_SubAccCodes
                txtPaymentEditSubAccCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtPaymentEditSubAccCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            Else
                txtPaymentEditMainAccDesc.Text = ""
                txtPaymentEditSubAccCode.Text = ""
                txtPaymentEditSubAccDesc.Text = ""
                SubAcc_Codes.Clear()
                txtPaymentEditSubAccCode.AutoCompleteSource = AutoCompleteSource.None
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentEditSubAccCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentEditSubAccCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT MS_SUB_ACNT_NAME FROM FM_MAIN_SUB WHERE MS_SUB_ACNT_CODE='" & txtPaymentEditSubAccCode.Text & "' and MS_MAIN_ACNT_CODE = '" & txtPaymentEditMainAccCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentEditSubAccDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentEditSubAccDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentEditDivCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentEditDivCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT DIVN_NAME AS DIVNAME FROM FM_DIVISION WHERE DIVN_FRZ_FLAG = 'N' and DIVN_CODE='" & txtPaymentEditDivCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentEditDivDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentEditDivDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentEditDeptCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentEditDeptCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT DEPT_NAME AS DeptName FROM FM_DEPARTMENT WHERE DEPT_FRZ_FLAG = 'N' and DEPT_CODE='" & txtPaymentEditDeptCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentEditDeptDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentEditDeptDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPaymentEditPayType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentEditPayType.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT VSSV_NAME AS VSNAME FROM IM_VALUE_SET, IM_VS_STATIC_VALUE WHERE VS_CODE = VSSV_VS_CODE AND VS_CODE = 'PMT_TYPE' AND VS_FRZ_FLAG_NUM = 2 AND VSSV_FRZ_FLAG_NUM = 2 and VSSV_CODE='" & txtPaymentEditPayType.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPaymentEditPayDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPaymentEditPayDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPaymentEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentEditCancel.Click
        Try
            Dim i As Integer = pnlPaymentEdit.Height
            While i > 0
                pnlPaymentEdit.Height = pnlPaymentEdit.Height - 1
                pnlPaymentEdit.Location = New Point(lblPaymentSNo.Location.X, pnlPaymentEdit.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlPaymentEdit.Visible = False
            pnlPaymentEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

End Class