Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class SettingsSalesmanMaster
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim Company_Codes As New List(Of String)
    Dim Shift_Codes As New List(Of String)
    Dim Counter_Codes As New List(Of String)
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_CompanyCodes As New AutoCompleteStringCollection()
    Dim MySource_ShiftCodes As New AutoCompleteStringCollection()
    Dim MySource_CounterCodes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

        load_AllSalesmanDetails()
        settingsType = "Salesman Master"

    End Sub

    Private Sub load_AllSalesmanDetails()
        Try
            lstviewSalesmanMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SM_CODE,SM_NAME, SM_SHORT_NAME,SM_CR_UID,SM_FRZ_FLAG_NUM from OM_SALESMAN order by SM_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSalesmanMaster.Items.Add(i + 1)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(4).ToString)

                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub load_SMCompDetails(ByVal SMCode As Object)
        Try
            lstviewSalesmanMasterCMPY.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SMC_COMP_CODE,COMP_NAME, SMC_FRZ_FLAG_NUM from OM_SALESMAN_COMP,FM_COMPANY where SMC_COMP_CODE=COMP_CODE and SMC_CODE='" & SMCode & "'"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSalesmanMasterCMPY.Items.Add(i + 1)
                lstviewSalesmanMasterCMPY.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewSalesmanMasterCMPY.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSalesmanMasterCMPY.Items(i).SubItems.Add(row.Item(2).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub load_SMCounterDetails(ByVal SMCode As Object)
        Try
            lstviewSMMasterCOUNT.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SMC_LOCN_CODE,LOCN_NAME,SMC_COUNT_CODE,POSCNT_NAME, SMC_FRZ_FLAG_NUM from OM_POS_SALESMAN_COUNTER,OM_LOCATION,OM_POS_COUNTER where SMC_COUNT_CODE=POSCNT_NO and SMC_LOCN_CODE=LOCN_CODE and SMC_LOCN_CODE=POSCNT_LOCN_CODE and SMC_CODE='" & SMCode & "'"
            errLog.WriteToErrorLog("Select OM_POS_SALESMAN_COUNTER", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSMMasterCOUNT.Items.Add(i + 1)
                lstviewSMMasterCOUNT.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewSMMasterCOUNT.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSMMasterCOUNT.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewSMMasterCOUNT.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewSMMasterCOUNT.Items(i).SubItems.Add(row.Item(4).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub load_SMShiftDetails(ByVal SMCode As Object)
        Try
            lstviewSMMasterSHIFT.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SMS_LOCN_CODE,LOCN_NAME,SMS_SHIFT_CODE,SHIFT_DESC, SMS_FRZ_FLAG_NUM from OM_POS_SALESMAN_SHIFT,OM_LOCATION,OM_POS_SHIFT where SMS_SHIFT_CODE= SHIFT_CODE and SMS_LOCN_CODE=LOCN_CODE and SMS_LOCN_CODE= SHIFT_LOCN_CODE and SMS_CODE='" & SMCode & "'"
            errLog.WriteToErrorLog("Select OM_POS_SALESMAN_COUNTER", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSMMasterSHIFT.Items.Add(i + 1)
                lstviewSMMasterSHIFT.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewSMMasterSHIFT.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSMMasterSHIFT.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewSMMasterSHIFT.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewSMMasterSHIFT.Items(i).SubItems.Add(row.Item(4).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
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

            ' if you are not maximizing your screen afterwards, then include this code
            Me.Top = (prvheight / 2) - (Me.Height / 2)
            Me.Left = (prvWidth / 2) - (Me.Width / 2)
        End If

        

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


        For Each ctl As Control In pnlSalesManMaster.Controls
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

        For Each ctl As Control In pnlSalesmanAdd.Controls
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

        

        For Each ctl As Control In pnlSalesmanAddTabHolder.Controls()
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



        For Each ctl As Control In tabSalesmanMasterAdd.Controls
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


      

        For Each ctl As Control In TabSalesmanAddSMCMPY.Controls
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



        For Each ctl As Control In pnlSalesmanAddCompanyAdd.Controls
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
        For Each ctl As Control In pnlSMAddCompanyEdit.Controls
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

        For Each ctl As Control In pnlSalesmanEdit.Controls
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



        For Each ctl As Control In TabSalesmanAddSM_Countr.Controls
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




        For Each ctl As Control In pnlSMAddCntrAdd.Controls
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



        For Each ctl As Control In pnlSMAddCntrEdit.Controls
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



        For Each ctl As Control In TabSalesmanAddSM_Shift.Controls
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


        For Each ctl As Control In pnlSMAddShift.Controls
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
        For Each ctl As Control In pnlSMEditShift.Controls
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

        For Each ctl As Control In Pnl_SM_ShiftBtn.Controls
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

        For Each ctl As Control In Pnl_SM_CounterBtn.Controls
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
        For Each ctl As Control In pnl_SM_Salesmanbtn.Controls
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
        For Each ctl As Control In pn_SM_denombtn.Controls
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
        For Each ctl As Control In Pnl_SM_paymentbtn.Controls
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


        lstviewSalesmanMaster.Columns.Add("SNo", lblSalesmanSNo.Width - 3, HorizontalAlignment.Left)
        lstviewSalesmanMaster.Columns.Add("Salesman Code", lbl_SM_SalesmanCode.Width, HorizontalAlignment.Left)
        lstviewSalesmanMaster.Columns.Add("Salesman Name", lbl_SM_SalesmanName.Width, HorizontalAlignment.Left)
        lstviewSalesmanMaster.Columns.Add("Salesman Short Name", lbl_SM_SSHNAME.Width, HorizontalAlignment.Left)
        lstviewSalesmanMaster.Columns.Add("Created User", lbl_SM_CreatedUser.Width, HorizontalAlignment.Left)
        lstviewSalesmanMaster.Columns.Add("Status", lbl_SM_Status.Width - 20, HorizontalAlignment.Left)
        lstviewSalesmanMaster.View = View.Details
        lstviewSalesmanMaster.GridLines = True
        lstviewSalesmanMaster.FullRowSelect = True

        lstviewSalesmanMasterCMPY.Columns.Add("SNo", lblSalesmanAddCMPYAddSNO.Width - 3, HorizontalAlignment.Left)
        lstviewSalesmanMasterCMPY.Columns.Add("Company Code", lbl_SM_Cmpny_CmpnyCode.Width, HorizontalAlignment.Left)
        lstviewSalesmanMasterCMPY.Columns.Add("Company Name", lbl_SM_Cmpny_CmpnyName.Width, HorizontalAlignment.Left)
        lstviewSalesmanMasterCMPY.Columns.Add("Freeze", lbl_SM_Cmpny_Freeze.Width - 20, HorizontalAlignment.Left)
        lstviewSalesmanMasterCMPY.View = View.Details
        lstviewSalesmanMasterCMPY.GridLines = True
        lstviewSalesmanMasterCMPY.FullRowSelect = True

        lstviewSMMasterCOUNT.Columns.Add("SNo", lbl_SM_CounterSno.Width - 3, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.Columns.Add("Location Code", lbl_SM_CounterLocCode.Width, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.Columns.Add("Location Name", lbl_SM_Ctr_LocName.Width, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.Columns.Add("Counter Code", lbl_SM_Ctr_CounterCode.Width, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.Columns.Add("Counter Name", lbl_SM_Ctr_CountName.Width, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.Columns.Add("Freeze", lbl_SM_Counter_Freeze.Width - 3, HorizontalAlignment.Left)
        lstviewSMMasterCOUNT.View = View.Details
        lstviewSMMasterCOUNT.GridLines = True
        lstviewSMMasterCOUNT.FullRowSelect = True

        lstviewSMMasterSHIFT.Columns.Add("SNo", lbl_SM_Shift_Sno.Width - 3, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.Columns.Add("Location Code", lbl_SM_shift_LocCode.Width, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.Columns.Add("Location Name", lbl_SM_Shift_LocName.Width, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.Columns.Add("Shift Code", lbl_SM_Shift_SCode.Width, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.Columns.Add("Shift Name", lbl_Shift_SftName.Width, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.Columns.Add("Freeze", lbl_SM_shift_Freeze.Width - 3, HorizontalAlignment.Left)
        lstviewSMMasterSHIFT.View = View.Details
        lstviewSMMasterSHIFT.GridLines = True
        lstviewSMMasterSHIFT.FullRowSelect = True



    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesOrders.Click
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsShiftMaster.MdiParent = Home
        SettingsShiftMaster.Show()
    End Sub

    Private Sub btnSettingsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsAdd.Click
        Try
            If settingsType = "Salesman Master" Then
                If Not pnlSalesmanAdd.Visible Then
                    tabSalesmanMasterAdd.SelectedTab = TabSalesmanAddSMCMPY
                    'btnSMShiftAddCancel.Height = lblSalesmanSNo.Height + lstviewSalesmanMaster.Height + 1
                    pnlSalesmanAdd.Height = pnlSalesManMaster.Height - 1

                    pnlSalesmanAdd.BringToFront()
                    'Dim i As Integer = pnlSalesmanAdd.Height
                    'While i >= lblSalesmanSNo.Location.Y
                    'pnlSalesmanAdd.Location = New Point(lblSalesmanSNo.Location.X, i)
                    pnlSalesmanAdd.Show()
                    'Threading.Thread.Sleep(0.5)
                    'i = (i - 1)
                    'End While
                    lblSM_Header.Text = "ADD - NEW"
                    btnSMEditUpdate.Hide()
                    pnlSalesmanAddTabHolder.Enabled = False
                    btnSalesmanAddSave.Enabled = True
                    txtSalesmanAddSMCode.Enabled = True
                    txtSalesmanAddSMCode.Text = ""
                    txtSalesmanAddSMName.Enabled = True
                    txtSalesmanAddSMName.Text = ""
                    txtSalesmanAddSMSHR_Name.Enabled = True
                    txtSalesmanAddSMSHR_Name.Text = ""
                    pnlSalesmanEdit.Visible = False
                    chkboxSalesmanAddFreeze.Enabled = True
                    chkboxSalesmanAddFreeze.CheckState = CheckState.Unchecked
                    lstviewSalesmanMasterCMPY.Items.Clear()
                    lstviewSMMasterCOUNT.Items.Clear()
                    lstviewSMMasterSHIFT.Items.Clear()

                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsHome.Click
        Try
            If settingsType = "Salesman Master" Then
                pnlSalesmanAdd.Hide()
                pnlSalesmanEdit.Hide()
                pnlSalesmanAdd.SendToBack()
                pnlSalesmanEdit.SendToBack()
                load_AllSalesmanDetails()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsEdit.Click
        callEditSM()
    End Sub
    Private Sub callEditSM()
        Try
            If settingsType = "Salesman Master" Then
                If Not lstviewSalesmanMaster.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row !")
                Else

                    'lstviewSalesmanMaster.SelectedItems.Count > 0 Then
                    pnlSalesmanAdd.Height = lblSalesmanSNo.Height + lstviewSalesmanMaster.Height + 1 + 35
                    pnlSalesmanAdd.BringToFront()
                    'Dim i As Integer = pnlSalesmanAdd.Height
                    'While i >= lblSalesmanSNo.Location.Y
                    'pnlSalesmanAdd.Location = New Point(lblSalesmanSNo.Location.X, i)
                    pnlSalesmanAdd.Show()
                    'Threading.Thread.Sleep(0.5)
                    'i = (i - 1)
                    'End While
                    lblSM_Header.Text = "EDIT"
                    btnSMEditUpdate.Show()
                    chkboxSalesmanAddFreeze.Enabled = True
                    pnlSalesmanAddTabHolder.Enabled = True
                    btnSalesmanAddSave.Enabled = True
                    txtSalesmanAddSMCode.Enabled = True
                    txtSalesmanAddSMCode.Text = ""
                    txtSalesmanAddSMName.Enabled = True
                    txtSalesmanAddSMName.Text = ""
                    txtSalesmanAddSMSHR_Name.Enabled = True
                    txtSalesmanAddSMSHR_Name.Text = ""
                    txtSalesmanAddSMCode.Enabled = False
                    txtSalesmanAddSMCode.Text = lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text
                    txtSalesmanAddSMName.Text = lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(2).Text
                    txtSalesmanAddSMSHR_Name.Text = lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(3).Text
                    If lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(5).Text = "1" Then
                        chkboxSalesmanAddFreeze.CheckState = CheckState.Checked
                        chkboxSalesmanAddFreeze.Enabled = True
                    Else
                        chkboxSalesmanAddFreeze.CheckState = CheckState.Unchecked
                    End If
                    load_SMCompDetails(txtSalesmanAddSMCode.Text)
                    load_SMCounterDetails(txtSalesmanAddSMCode.Text)
                    load_SMShiftDetails(txtSalesmanAddSMCode.Text)
                    tabSalesmanMasterAdd.SelectedTab = TabSalesmanAddSMCMPY
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSettingsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettingsDelete.Click
        Try
            If lstviewSalesmanMaster.SelectedItems.Count = 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                Dim delQuery_Shift As String
                delQuery_Shift = "delete from OM_POS_SALESMAN_SHIFT where SMS_CODE='" & lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text & "'"
                errLog.WriteToErrorLog("delQuery_Shift", delQuery_Shift, "")
                db.SaveToTableODBC(delQuery_Shift)

                Dim delQuery_Counter As String
                delQuery_Counter = "delete from OM_POS_SALESMAN_COUNTER where SMC_CODE='" & lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text & "'"
                errLog.WriteToErrorLog("delQuery_Counter", delQuery_Counter, "")
                db.SaveToTableODBC(delQuery_Counter)

                Dim delQuery_Company As String
                delQuery_Company = "delete from OM_SALESMAN_COMP where SMC_CODE='" & lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text & "'"
                errLog.WriteToErrorLog("delQuery_Company", delQuery_Company, "")
                db.SaveToTableODBC(delQuery_Company)

                Dim delQuery_SM As String
                delQuery_SM = "delete from OM_SALESMAN where SM_CODE='" & lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text & "'"
                errLog.WriteToErrorLog("delQuery_SM", delQuery_SM, "")
                db.SaveToTableODBC(delQuery_SM)

                MsgBox("Deleted successfully!")
                load_AllSalesmanDetails()
            End If
        Catch ex As Exception
            'MsgBox(ex.Message.GetHashCode)
            'If ex.Message.GetHashCode = "1131387364" Then
            '    MsgBox("SalesmanCode in use! Cannot be deleted!")
            '    Exit Sub
            'ElseIf ex.Message.GetHashCode = "1197729895" Then
            '    MsgBox("SalesmanCode in use! Cannot be deleted!")
            '    Exit Sub
            'ElseIf ex.Message.GetHashCode = "-1172840326" Then
            '    MsgBox("SalesmanCode in use! Cannot be deleted!")
            '    Exit Sub
            'ElseIf ex.Message.GetHashCode = "-1468482023" Then
            '    MsgBox("SalesmanCode in use! Cannot be deleted!")
            '    Exit Sub
            'End If
            MsgBox("SalesmanCode in use! Cannot be deleted!")
            Exit Sub
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub txtCounterEditLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterEditLocationCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCounterEditLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCounterEditLocationDesc.Text = ""
        End If
    End Sub

    Private Sub btnCounterEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer = pnlSalesmanEdit.Height
        While i > 0
            pnlSalesmanEdit.Height = pnlSalesmanEdit.Height - 1
            pnlSalesmanEdit.Location = New Point(lblSalesmanSNo.Location.X, pnlSalesmanEdit.Location.Y + 1)
            i = i - 1
            Threading.Thread.Sleep(0.5)
        End While

        pnlSalesmanEdit.Visible = False
        pnlSalesmanEdit.SendToBack()
    End Sub

    Private Sub btnCounterEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
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
                load_AllSalesmanDetails()

            Else
                MsgBox("Unable to update!")
                Exit Sub
            End If
            Dim i As Integer = pnlSalesmanEdit.Height
            While i > 0
                pnlSalesmanEdit.Height = pnlSalesmanEdit.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlSalesmanEdit.Visible = False
            pnlSalesmanEdit.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCounterAddLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterAddLocationCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtCounterAddLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtCounterAddLocationDesc.Text = ""
        End If
    End Sub

    Private Sub btnCounterAddSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtSalesmanAddSMCode.Text = "" Then
                MsgBox("Enter Counter No!")
                Exit Sub
            ElseIf txtSalesmanAddSMName.Text = "" Then
                MsgBox("Enter Counter Name!")
                Exit Sub
            ElseIf txtCounterAddLocationCode.Text = "" Then
                MsgBox("Enter Location Code!")
                Exit Sub
            ElseIf txtSalesmanAddSMSHR_Name.Text = "" And "" = "" Then
                MsgBox("Enter either of Computer Name or IP Address!")
                Exit Sub
            Else
                If Not txtCounterAddLocationDesc.Text = "" Then
                    Dim stQuery As String
                    Dim ds As DataSet
                    stQuery = "SELECT POSCNT_NO FROM OM_POS_COUNTER WHERE POSCNT_NO = '" & txtSalesmanAddSMCode.Text & "' AND POSCNT_LOCN_CODE = '" & txtCounterAddLocationCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    If Not ds.Tables("Table").Rows.Count > 0 Then
                        Dim freeze As String
                        If chkboxSalesmanAddFreeze.Checked = True Then
                            freeze = "1"
                        Else
                            freeze = "2"
                        End If
                        stQuery = "INSERT INTO OM_POS_COUNTER(POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME,POSCNT_CR_DT,POSCNT_CR_UID)VALUES("
                        stQuery = stQuery & "'" & txtSalesmanAddSMCode.Text & "','" & txtSalesmanAddSMName.Text & "','" & txtCounterAddLocationCode.Text & "'," & freeze & ",'" & "" & "','" & txtSalesmanAddSMSHR_Name.Text & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("Insert Query OM_POS_COUNTER", stQuery, "")
                        db.SaveToTableODBC(stQuery)
                        MsgBox("Counter Saved Successfully")
                        txtSalesmanAddSMCode.Text = ""
                        txtSalesmanAddSMName.Text = ""
                        ' txtCounterAddIPAddr.Text = ""
                        txtSalesmanAddSMSHR_Name.Text = ""
                        txtCounterAddLocationCode.Text = ""
                        txtCounterAddLocationDesc.Text = ""
                        load_AllSalesmanDetails()
                    Else
                        MsgBox("Counter No. already exists in this location!")
                        Exit Sub
                    End If
                Else
                    MsgBox("Please select a valid location!")
                    Exit Sub
                End If
            End If

            Dim i As Integer = pnlSalesmanAdd.Height
            While i > 0
                pnlSalesmanAdd.Height = pnlSalesmanAdd.Height - 1
                'pnlShiftAdd.Location = New Point(lblShiftSNo.Location.X, pnlShiftAdd.Location.Y + 1)
                i = i - 1
                Threading.Thread.Sleep(0.5)
            End While

            pnlSalesmanAdd.Visible = False
            pnlSalesmanAdd.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterAddCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer = pnlSalesmanAdd.Height
        While i > 0
            pnlSalesmanAdd.Height = pnlSalesmanAdd.Height - 1
            pnlSalesmanAdd.Location = New Point(lblSalesmanSNo.Location.X, pnlSalesmanAdd.Location.Y + 1)
            i = i - 1
            Threading.Thread.Sleep(0.5)
        End While

        pnlSalesmanAdd.Visible = False
        pnlSalesmanAdd.SendToBack()
    End Sub

    Private Sub lstviewCounterMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not lstviewSalesmanMaster.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If
            Dim counterno As String = lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(1).Text
            Dim counterlocncode As String = lstviewSalesmanMaster.SelectedItems.Item(0).SubItems(3).Text

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select POSCNT_NO,POSCNT_NAME,POSCNT_LOCN_CODE,LOCN_NAME,POSCNT_FRZ_FLAG_NUM,POSCNT_IP_ADDRESS,POSCNT_COMPUTER_NAME from OM_POS_COUNTER a,OM_LOCATION b where POSCNT_NO='" & counterno & "' and POSCNT_LOCN_CODE='" & counterlocncode & "' and a.POSCNT_LOCN_CODE = b.LOCN_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Query", stQuery, "")
            If ds.Tables("Table").Rows.Count > 0 Then

                If Not pnlSalesmanEdit.Visible Then
                    pnlSalesmanEdit.Height = lblSalesmanSNo.Height + lstviewSalesmanMaster.Height + 1
                    pnlSalesmanEdit.BringToFront()
                    Dim i As Integer = pnlSalesmanEdit.Height
                    While i >= lblSalesmanSNo.Location.Y
                        pnlSalesmanEdit.Location = New Point(lblSalesmanSNo.Location.X, i)
                        pnlSalesmanEdit.Show()
                        Threading.Thread.Sleep(0.5)
                        i = (i - 1)
                    End While
                    pnlSalesmanAdd.Visible = False

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
                    chkboxCounterEditFreeze.Enabled = False
                End If
                txtCounterEditIPAddr.Text = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                txtCounterEditCompName.Text = ds.Tables("Table").Rows.Item(0).Item(6).ToString
                lstviewSalesmanMaster.SelectedItems.Clear()
            Else
                MsgBox("Not available for edit")
                lstviewSalesmanMaster.SelectedItems.Clear()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterMaster.Click
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsCounterMaster.MdiParent = Home
        SettingsCounterMaster.Show()
    End Sub


    Private Sub btnDenominationMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenominationMaster.Click
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsDenominationMaster.MdiParent = Home
        SettingsDenominationMaster.Show()
    End Sub

    Private Sub btnPaymentMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentMaster.Click
        For Each child As Form In Home.MdiChildren
            child.Close()
            child.Dispose()
        Next child
        SettingsPaymentMaster.MdiParent = Home
        SettingsPaymentMaster.Show()
    End Sub



    Private Sub btnSalesmanAddCMPY_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCMPY_Add.Click
        Try
            txtSalesmanAddCompanyCode.Text = ""
            txtSalesmanAddCompanyName.Text = ""
            chkSMCompFreeze.Checked = False
            If Not pnlSalesmanAddCompanyAdd.Visible Then
                pnlSalesmanAddCompanyAdd.Height = lblSalesmanAddCMPYAddSNO.Height + lstviewSalesmanMasterCMPY.Height + 3
                pnlSalesmanAddCompanyAdd.BringToFront()
                Dim i As Integer = TabSalesmanAddSMCMPY.Height
                While i >= lblSalesmanAddCMPYAddSNO.Location.Y
                    pnlSalesmanAddCompanyAdd.Location = New Point(0, i)
                    pnlSalesmanAddCompanyAdd.Show()
                    Threading.Thread.Sleep(0.5)
                    i = (i - 1)
                End While
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim row As System.Data.DataRow
                Company_Codes.Clear()
                stQuery = "SELECT COMP_CODE FROM FM_COMPANY"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Company_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                txtSalesmanAddCompanyCode.AutoCompleteSource = AutoCompleteSource.None
                MySource_CompanyCodes.AddRange(Company_Codes.ToArray)
                txtSalesmanAddCompanyCode.AutoCompleteCustomSource = MySource_CompanyCodes
                txtSalesmanAddCompanyCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSalesmanAddCompanyCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesmanAddCMPY_Home_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCMPY_Home.Click
        'If pnlSalesmanAddCompanyAdd.Visible Then

        '    Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
        '    While i >= 0
        '        pnlSalesmanAddCompanyAdd.Height = pnlSalesmanAddCompanyAdd.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSalesmanAddCompanyAdd.Visible = False
        'End If
        'If pnlSMAddCompanyEdit.Visible Then

        '    Dim i As Integer = pnlSMAddCompanyEdit.Height
        '    While i >= 0
        '        pnlSMAddCompanyEdit.Height = pnlSMAddCompanyEdit.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSMAddCompanyEdit.Visible = False
        'End If
    End Sub

    Private Sub btnSalesmanAddCompanyAddCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCompanyAddCancel.Click
        If pnlSalesmanAddCompanyAdd.Visible Then

            Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
            While i >= 0
                pnlSalesmanAddCompanyAdd.Height = pnlSalesmanAddCompanyAdd.Height - 1
                Threading.Thread.Sleep(0.5)
                i = (i - 1)
            End While
            pnlSalesmanAddCompanyAdd.Visible = False
        End If
    End Sub


    Private Sub BtnSalesmanAddCMPY_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalesmanAddCMPY_Edit.Click
        callEditCompany()
    End Sub

    Private Sub btnSalesmanAddCMPY_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCMPY_Delete.Click
        Try
            If lstviewSalesmanMasterCMPY.SelectedItems.Count = 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If
            If lstviewSalesmanMasterCMPY.SelectedItems.Item(0).SubItems(3).Text = 2 Then
                MsgBox("Cannot Delete!")
            Else
                Dim stQuery As String
                stQuery = "delete from OM_SALESMAN_COMP where SMC_CODE='" & txtSalesmanAddSMCode.Text & "'"
                db.SaveToTableODBC(stQuery)
                MsgBox("Deleted successfully!")
                lstviewSalesmanMasterCMPY.Items.Clear()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSalesmanAddCompanyCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesmanAddCompanyCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select COMP_NAME from FM_COMPANY where COMP_CODE='" & txtSalesmanAddCompanyCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtSalesmanAddCompanyName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtSalesmanAddCompanyName.Text = ""
        End If
    End Sub

    Private Sub btnSalesmanAddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddSave.Click
        Try
            If txtSalesmanAddSMCode.Text = "" Then
                MsgBox("Please enter Salesman Code")
                Exit Sub
            ElseIf txtSalesmanAddSMName.Text = "" Then
                MsgBox("Please enter Salesman Name")
                Exit Sub
            ElseIf txtSalesmanAddSMSHR_Name.Text = "" Then
                MsgBox("Please enter Salesman Short Name")
                Exit Sub
            End If

            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT SM_CODE FROM OM_SALESMAN WHERE SM_CODE = '" & txtSalesmanAddSMCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                MsgBox("Salesman Code already Exists!")
                Exit Sub
            Else

                stQuery = "INSERT INTO OM_SALESMAN (SM_CODE,SM_NAME,SM_SHORT_NAME,SM_MIN_MARKUP_PERC,SM_MAX_VARIANCE_PERC,SM_FRZ_FLAG_NUM,SM_BL_NAME,SM_BL_SHORT_NAME,SM_CR_DT,SM_CR_UID) VALUES ("
                stQuery = stQuery & "'" & txtSalesmanAddSMCode.Text & "','" & txtSalesmanAddSMName.Text & "','" & txtSalesmanAddSMSHR_Name.Text & "',0,0,2,'','',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                errLog.WriteToErrorLog("Insert Query OM_SALESMAN", stQuery, "")
                db.SaveToTableODBC(stQuery)
                MsgBox("New Salesman Created!")
                pnlSalesmanAddTabHolder.Enabled = True
                btnSalesmanAddSave.Enabled = False
                txtSalesmanAddSMCode.Enabled = False
                txtSalesmanAddSMName.Enabled = False
                txtSalesmanAddSMSHR_Name.Enabled = False
                chkSMCompEditFreeze.Enabled = True
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSalesmanAddCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCancel.Click
        lstviewSalesmanMasterCMPY.Items.Clear()
        load_AllSalesmanDetails()
        pnlSalesmanAdd.Hide()

    End Sub


    Private Sub btnSalesmanAddCompanyAddOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesmanAddCompanyAddOK.Click
        Try
            If txtSalesmanAddCompanyName.Text = "" Then
                MsgBox("Please Select a Valid Company Code")
            Else
                Dim stQuery As String
                Dim Freeze As String
                Dim stQuery_chk As String
                Dim ds As DataSet
                If chkSMCompFreeze.Checked = True Then
                    Freeze = "1"
                ElseIf chkSMCompFreeze.Checked = False Then
                    Freeze = "2"
                End If
                stQuery_chk = "select SMC_CODE from OM_SALESMAN_COMP where SMC_CODE='" & txtSalesmanAddSMCode.Text & "'"
                ds = db.SelectFromTableODBC(stQuery_chk)
                If ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("SalesMan already Exists in this Company!")
                Else
                    stQuery = "INSERT INTO OM_SALESMAN_COMP (SMC_CODE,SMC_COMP_CODE,SMC_FRZ_FLAG_NUM,SMC_CR_DT,SMC_CR_UID) VALUES ("
                    stQuery = stQuery & "'" & txtSalesmanAddSMCode.Text & "','" & txtSalesmanAddCompanyCode.Text & "','" & Freeze & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                    errLog.WriteToErrorLog("Insert Query OM_SALESMAN_COMP", stQuery, "")
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Company Details Saved!")
                    pnlSalesmanAddCompanyAdd.Hide()
                    load_SMCompDetails(txtSalesmanAddSMCode.Text)
                    txtSalesmanAddCompanyCode.Text = ""
                    txtSalesmanAddCompanyName.Text = ""
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If pnlSMAddCompanyEdit.Visible Then
            pnlSMAddCompanyEdit.Hide()
            load_SMCompDetails(txtSalesmanAddSMCode.Text)
            pnlSMAddCompanyEdit.Hide()
        End If


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim stQuery As String
            Dim Freeze As String = ""

            If chkSMCompEditFreeze.Checked = True Then
                Freeze = "1"
            Else
                Freeze = "2"
            End If
            stQuery = "UPDATE OM_SALESMAN_COMP SET SMC_COMP_CODE='" & txtSMEditCompCode.Text & "',SMC_FRZ_FLAG_NUM='" & Freeze & "',SMC_UPD_DT=to_date(sysdate,'DD-MM-YY'),SMC_UPD_UID='" & LogonUser & "' WHERE SMC_CODE='" & txtSalesmanAddSMCode.Text & "' "
            errLog.WriteToErrorLog("Update Query OM_SALESMAN_COMP", stQuery, "")
            db.SaveToTableODBC(stQuery)
            MsgBox("Updated Successfully")

            pnlSMAddCompanyEdit.Hide()
            load_SMCompDetails(txtSalesmanAddSMCode.Text)
            pnlSMAddCompanyEdit.Hide()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMAddCOUNT_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCOUNT_Add.Click
        Try
            txtSMAddLcnCode.Text = ""
            txtSMAddLcnName.Text = ""
            txtSMAddCountCode.Text = ""
            txtSMAddCountName.Text = ""
            txtSMAddCountFz.Checked = False
            If Not pnlSMAddCntrAdd.Visible Then
                pnlSMAddCntrAdd.Height = lblSalesmanAddCMPYAddSNO.Height + lstviewSalesmanMasterCMPY.Height + 3
                pnlSMAddCntrAdd.BringToFront()
                Dim i As Integer = TabSalesmanAddSMCMPY.Height
                While i >= lblSalesmanAddCMPYAddSNO.Location.Y
                    pnlSMAddCntrAdd.Location = New Point(0, i)
                    pnlSMAddCntrAdd.Show()
                    Threading.Thread.Sleep(0.5)
                    i = (i - 1)
                End While
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim row As System.Data.DataRow

                Location_Codes.Clear()
                stQuery = "SELECT LOCN_CODE FROM OM_LOCATION"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                txtSMAddLcnCode.AutoCompleteSource = AutoCompleteSource.None
                MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                txtSMAddLcnCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddLcnCode.AutoCompleteCustomSource = MySource_LocationCodes
                txtSMAddLcnCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub txtSMAddLcnCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddLcnCode.TextChanged
        Try
            Dim stQuery As String
            Dim stQuery1 As String
            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim count1 As Integer
            Dim row1 As System.Data.DataRow
            Dim i As Integer
            stQuery = "select LOCN_NAME from OM_LOCATION where LOCN_CODE='" & txtSMAddLcnCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddLcnName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Counter_Codes.Clear()
                stQuery1 = "SELECT  POSCNT_NO FROM OM_POS_COUNTER where POSCNT_LOCN_CODE='" & txtSMAddLcnCode.Text & "'"
                errLog.WriteToErrorLog("Query CNTER", stQuery1, "")
                ds1 = db.SelectFromTableODBC(stQuery1)
                count1 = ds1.Tables("Table").Rows.Count
                i = 0
                While count1 > 0
                    row1 = ds1.Tables("Table").Rows.Item(i)
                    Counter_Codes.Add(row1.Item(0).ToString)
                    i = i + 1
                    count1 = count1 - 1
                End While
                txtSMAddCountCode.AutoCompleteSource = AutoCompleteSource.None
                MySource_CounterCodes.AddRange(Counter_Codes.ToArray)
                txtSMAddCountCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddCountCode.AutoCompleteCustomSource = MySource_CounterCodes
                txtSMAddCountCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            Else
                txtSMAddLcnName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMAddCountcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCountcancel.Click
        If pnlSMAddCntrAdd.Visible Then
            Dim i As Integer = pnlSMAddCntrAdd.Height
            While i >= 0
                pnlSMAddCntrAdd.Height = pnlSMAddCntrAdd.Height - 1
                Threading.Thread.Sleep(0.5)
                i = (i - 1)
            End While
            pnlSMAddCntrAdd.Visible = False
        End If
    End Sub


    Private Sub txtSMAddCountCode_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddCountCode.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select POSCNT_NAME from OM_POS_COUNTER where POSCNT_NO='" & txtSMAddCountCode.Text & "' and POSCNT_LOCN_CODE='" & txtSMAddLcnCode.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtSMAddCountName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtSMAddCountName.Text = ""
        End If
    End Sub

    Private Sub btnSMAddCountok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCountok.Click
        Try
            If txtSMAddLcnName.Text = "" Then
                MsgBox("Please Select a Valid Location Code")
            ElseIf txtSMAddCountName.Text = "" Then
                MsgBox("Please Select a Valid Counter No")
            Else
                Dim stQuery As String
                Dim stQuery_chk As String
                Dim Freeze As String
                Dim ds As DataSet
                If txtSMAddCountFz.Checked = True Then
                    Freeze = 1
                Else
                    Freeze = 2
                End If
                stQuery_chk = "select SMC_CODE from OM_POS_SALESMAN_COUNTER where SMC_COUNT_CODE='" & txtSMAddCountCode.Text & "' and SMC_LOCN_CODE='" & txtSMAddLcnCode.Text & "' and SMC_CODE='" & txtSalesmanAddSMCode.Text & "'"
                ds = db.SelectFromTableODBC(stQuery_chk)
                If ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("The SalesMan already Exists in this Location Counter!")
                Else
                    stQuery = "INSERT INTO OM_POS_SALESMAN_COUNTER (SMC_CODE,SMC_COUNT_CODE,SMC_LOCN_CODE,SMC_FRZ_FLAG_NUM,SMC_CR_DT,SMC_CR_UID) VALUES ("
                    stQuery = stQuery & "'" & txtSalesmanAddSMCode.Text & "', '" & txtSMAddCountCode.Text & "','" & txtSMAddLcnCode.Text & "','" & Freeze & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                    errLog.WriteToErrorLog("Insert Query OM_POS_SALESMAN_COUNTER", stQuery, "")
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Counter Details Saved!")
                    pnlSMAddCntrAdd.Hide()
                    pnlSMAddCntrEdit.Hide()
                    load_SMCounterDetails(txtSalesmanAddSMCode.Text)
                    txtSMAddLcnName.Text = ""
                    txtSMAddLcnCode.Text = ""
                    txtSMAddCountCode.Text = ""
                    txtSMAddCountName.Text = ""
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnSMAddCOUNT_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCOUNT_Edit.Click
        callEditCounter()
    End Sub


    Private Sub btnSMAddCOUNT_Home_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCOUNT_Home.Click
        'If pnlSMAddCntrAdd.Visible Then
        '    Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
        '    While i >= 0
        '        pnlSMAddCntrAdd.Height = pnlSMAddCntrAdd.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSMAddCntrAdd.Visible = False
        'End If
        'If pnlSMAddCntrEdit.Visible Then
        '    Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
        '    While i >= 0
        '        pnlSMAddCntrEdit.Height = pnlSMAddCntrEdit.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSMAddCntrEdit.Visible = False
        'End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If pnlSMAddCntrEdit.Visible Then
            'Dim i As Integer = pnlSMAddCntrEdit.Height
            'While i >= 0
            '    pnlSMAddCntrEdit.Height = pnlSMAddCntrEdit.Height - 1
            '    Threading.Thread.Sleep(0.5)
            '    i = (i - 1)
            'End While
            'pnlSMAddCntrEdit.Visible = False
            pnlSMAddCntrEdit.Hide()
            load_SMCounterDetails(txtSalesmanAddSMCode.Text)
        End If
    End Sub


    Private Sub btnSMCountEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMCountEditUpdate.Click
        Try
            Dim stQuery_chk As String
            Dim ds As DataSet
            stQuery_chk = "select SMC_CODE from OM_POS_SALESMAN_COUNTER where SMC_COUNT_CODE='" & txtSMAddCountCodeEdit.Text & "' and SMC_LOCN_CODE='" & txtSMAddLcnCodeEdit.Text & "' and SMC_FRZ_FLAG_NUM = '1' and SMC_CODE='" & txtSalesmanAddSMCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery_chk)
            'If ds.Tables("Table").Rows.Count > 0 Then
            '    MsgBox("The SalesMan already Exists in this Location Counter!")
            'Else
            Dim stQuery As String
            Dim Freeze As String
            If txtSMEditCountFz.Checked = True Then
                Freeze = 1
            Else
                Freeze = 2
            End If
            stQuery = "UPDATE OM_POS_SALESMAN_COUNTER SET SMC_FRZ_FLAG_NUM='" & Freeze & "',SMC_UPD_DT=to_date(sysdate,'DD-MM-YY'),SMC_UPD_UID='" & LogonUser & "' WHERE SMC_CODE='" & txtSalesmanAddSMCode.Text & "' and SMC_LOCN_CODE='" & txtSMAddLcnCodeEdit.Text & "' and SMC_COUNT_CODE='" & txtSMAddCountCodeEdit.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_SALESMAN_COUNTER", stQuery, "")
            db.SaveToTableODBC(stQuery)
            MsgBox("Updated Successfully")
            pnlSMAddCntrEdit.Hide()
            load_SMCounterDetails(txtSalesmanAddSMCode.Text)
            'End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddLcnCodeEdit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddLcnCodeEdit.TextChanged
        Try
            Dim stQuery As String
            Dim stQuery1 As String
            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim count1 As Integer
            Dim row1 As System.Data.DataRow
            Dim i As Integer
            stQuery = "select LOCN_NAME from OM_LOCATION where LOCN_CODE='" & txtSMAddLcnCodeEdit.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddLcnNameEdit.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Counter_Codes.Clear()
                stQuery1 = "SELECT  POSCNT_NO FROM OM_POS_COUNTER where POSCNT_LOCN_CODE='" & txtSMAddLcnCodeEdit.Text & "'"
                errLog.WriteToErrorLog("Query CNTER", stQuery1, "")
                ds1 = db.SelectFromTableODBC(stQuery1)
                count1 = ds1.Tables("Table").Rows.Count
                i = 0
                While count1 > 0
                    row1 = ds1.Tables("Table").Rows.Item(i)
                    Counter_Codes.Add(row1.Item(0).ToString)
                    i = i + 1
                    count1 = count1 - 1
                End While
                txtSMAddCountCodeEdit.AutoCompleteSource = AutoCompleteSource.None
                MySource_CounterCodes.AddRange(Counter_Codes.ToArray)
                txtSMAddCountCodeEdit.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddCountCodeEdit.AutoCompleteCustomSource = MySource_CounterCodes
                txtSMAddCountCodeEdit.AutoCompleteSource = AutoCompleteSource.CustomSource
            Else
                txtSMAddLcnName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddCountCodeEdit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddCountCodeEdit.TextChanged
        Dim stQuery As String
        Dim ds As DataSet
        stQuery = "select POSCNT_NAME from OM_POS_COUNTER where POSCNT_NO='" & txtSMAddCountCodeEdit.Text & "' and POSCNT_LOCN_CODE='" & txtSMAddLcnCodeEdit.Text & "'"
        ds = db.SelectFromTableODBC(stQuery)
        If ds.Tables("Table").Rows.Count > 0 Then
            txtSMAddCountNameEdit.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
        Else
            txtSMAddCountNameEdit.Text = ""
        End If
    End Sub


    Private Sub lstviewSalesmanMasterCMPY_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSalesmanMasterCMPY.DoubleClick
        callEditCompany()
    End Sub
    Private Sub callEditCompany()
        If lstviewSalesmanMasterCMPY.SelectedItems.Count > 0 Then

            txtSMEditCompCode.Text = lstviewSalesmanMasterCMPY.SelectedItems.Item(0).SubItems(1).Text
            txtSMEditCompName.Text = lstviewSalesmanMasterCMPY.SelectedItems.Item(0).SubItems(2).Text
            If lstviewSalesmanMasterCMPY.SelectedItems.Item(0).SubItems(3).Text = "1" Then
                chkSMCompEditFreeze.CheckState = CheckState.Checked
                chkSMCompEditFreeze.Enabled = True
            Else
                chkSMCompEditFreeze.CheckState = CheckState.Unchecked
            End If
            'Dim i As Integer = lblSalesmanAddCMPYAddSNO.Height + lstviewSalesmanMasterCMPY.Height + 5
            'While i >= 0
            '    pnlSMAddCompanyEdit.Visible = True
            '    pnlSMAddCompanyEdit.Height = pnlSMAddCompanyEdit.Height - 1
            '    Threading.Thread.Sleep(0.5)
            '    i = (i - 1)
            'End While
            pnlSMAddCompanyEdit.Visible = True
            pnlSMAddCompanyEdit.BringToFront()
            pnlSalesmanAddCompanyAdd.Visible = False

        Else
            MsgBox("Please select a row!")
        End If
    End Sub
    Private Sub callEditCounter()
        Try
            If lstviewSMMasterCOUNT.SelectedItems.Count > 0 Then
                txtSMEditCountFz.Enabled = True
                Dim stQuery As String
                Dim ds As DataSet
                Dim Count As Integer
                Dim row As System.Data.DataRow
                Dim i As Integer
                Location_Codes.Clear()
                stQuery = "SELECT LOCN_CODE FROM OM_LOCATION"
                ds = db.SelectFromTableODBC(stQuery)
                Count = ds.Tables("Table").Rows.Count
                i = 0
                While Count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    Count = Count - 1
                End While
                txtSMAddLcnCodeEdit.AutoCompleteSource = AutoCompleteSource.None
                MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                txtSMAddLcnCodeEdit.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddLcnCodeEdit.AutoCompleteCustomSource = MySource_LocationCodes
                txtSMAddLcnCodeEdit.AutoCompleteSource = AutoCompleteSource.CustomSource

                'MsgBox(lstviewSalesmanMasterCMPY.SelectedItems.Item(0).SubItems(3).Text)
                txtSMAddLcnCodeEdit.Text = lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(1).Text
                txtSMAddLcnNameEdit.Text = lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(2).Text
                txtSMAddCountCodeEdit.Text = lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(3).Text
                txtSMAddCountNameEdit.Text = lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(4).Text
                txtSMAddLcnCodeEdit.Enabled = False
                txtSMAddCountCodeEdit.Enabled = False
                If lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(5).Text = "1" Then
                    txtSMEditCountFz.CheckState = CheckState.Checked
                    txtSMEditCountFz.Enabled = True
                Else
                    txtSMEditCountFz.CheckState = CheckState.Unchecked
                End If
                'Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
                'While i >= 0
                '    pnlSMAddCompanyEdit.Visible = True
                '    pnlSMAddCompanyEdit.Height = pnlSMAddCompanyEdit.Height - 1
                '    Threading.Thread.Sleep(0.5)
                '    i = (i - 1)
                'End While
                pnlSMAddCntrAdd.Visible = False
                pnlSMAddCntrEdit.Visible = True
                pnlSMAddCntrEdit.BringToFront()
            Else
                MsgBox("Please select a row!")
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub callEditShift()
        Try
            If lstviewSMMasterSHIFT.SelectedItems.Count > 0 Then
                chkSMEditShiftFz.Enabled = True
                Dim stQuery As String
                Dim ds As DataSet
                Dim Count As Integer
                Dim row As System.Data.DataRow
                Dim i As Integer
                Location_Codes.Clear()
                stQuery = "SELECT LOCN_CODE FROM OM_LOCATION"
                ds = db.SelectFromTableODBC(stQuery)
                Count = ds.Tables("Table").Rows.Count
                i = 0
                While Count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    Count = Count - 1
                End While
                txtSMAddShiftLcnCodeEdit.AutoCompleteSource = AutoCompleteSource.None
                MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                txtSMAddShiftLcnCodeEdit.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddShiftLcnCodeEdit.AutoCompleteCustomSource = MySource_LocationCodes
                txtSMAddShiftLcnCodeEdit.AutoCompleteSource = AutoCompleteSource.CustomSource
                txtSMAddShiftLcnCodeEdit.Enabled = False
                txtSMAddShiftLcnCodeEdit.Text = lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(1).Text
                txtSMAddShiftLcnNameEdit.Text = lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(2).Text
                txtSMAddShiftCodeEdit.Text = lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(3).Text
                txtSMAddShiftCodeEdit.Enabled = False
                txtSMAddShiftNameEdit.Text = lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(4).Text
                If lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(5).Text = "1" Then
                    chkSMEditShiftFz.CheckState = CheckState.Checked
                    chkSMEditShiftFz.Enabled = True
                Else
                    chkSMEditShiftFz.CheckState = CheckState.Unchecked
                End If
                pnlSMAddShift.Visible = False
                pnlSMEditShift.Visible = True
                pnlSMEditShift.BringToFront()
            Else
                MsgBox("Please select a row!")
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lstviewSMMasterCOUNT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSMMasterCOUNT.DoubleClick
        callEditCounter()
    End Sub

    Private Sub btnSMAddCOUNT_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddCOUNT_Del.Click
        Try
            If lstviewSMMasterCOUNT.SelectedItems.Count = 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                If lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(5).Text = "1" Then
                    MsgBox("Cannot Delete!")
                Else
                    Dim stQuery As String
                    stQuery = "delete from OM_POS_SALESMAN_COUNTER where SMC_CODE='" & txtSalesmanAddSMCode.Text & "' and SMC_LOCN_CODE='" & lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(1).Text & "' and SMC_COUNT_CODE='" & lstviewSMMasterCOUNT.SelectedItems.Item(0).SubItems(3).Text & "' and SMC_FRZ_FLAG_NUM='2'"
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Deleted successfully!")
                    load_SMCounterDetails(txtSalesmanAddSMCode.Text)
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMAddSHIFT_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddSHIFT_Add.Click
        Try
            txtSMAddShiftLcnCode.Text = ""
            txtSMAddShiftLcnName.Text = ""
            txtSMAddShiftCode.Text = ""
            txtSMAddShiftName.Text = ""
            txtSMAddShiftFz.Checked = False
            If Not pnlSMAddShift.Visible Then
                pnlSMAddShift.Height = lblSalesmanAddCMPYAddSNO.Height + lstviewSalesmanMasterCMPY.Height + 3
                pnlSMAddShift.BringToFront()
                Dim i As Integer = TabSalesmanAddSMCMPY.Height
                While i >= lblSalesmanAddCMPYAddSNO.Location.Y
                    pnlSMAddShift.Location = New Point(0, i)
                    pnlSMAddShift.Show()
                    Threading.Thread.Sleep(0.5)
                    i = (i - 1)
                End While
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim row As System.Data.DataRow

                Location_Codes.Clear()
                stQuery = "SELECT LOCN_CODE FROM OM_LOCATION"
                ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Location_Codes.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                txtSMAddShiftLcnCode.AutoCompleteSource = AutoCompleteSource.None
                MySource_LocationCodes.AddRange(Location_Codes.ToArray)
                txtSMAddShiftLcnCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddShiftLcnCode.AutoCompleteCustomSource = MySource_LocationCodes
                txtSMAddShiftLcnCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddShiftLcnCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddShiftLcnCode.TextChanged
        Try
            Dim stQuery As String
            Dim stQuery1 As String
            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim count1 As Integer
            Dim row1 As System.Data.DataRow
            Dim i As Integer
            stQuery = "select LOCN_NAME from OM_LOCATION where LOCN_CODE='" & txtSMAddShiftLcnCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddShiftLcnName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Shift_Codes.Clear()
                stQuery1 = "SELECT  SHIFT_CODE FROM OM_POS_SHIFT where SHIFT_LOCN_CODE='" & txtSMAddShiftLcnCode.Text & "'"
                errLog.WriteToErrorLog("Query CNTER", stQuery1, "")
                ds1 = db.SelectFromTableODBC(stQuery1)
                count1 = ds1.Tables("Table").Rows.Count
                i = 0
                While count1 > 0
                    row1 = ds1.Tables("Table").Rows.Item(i)
                    Shift_Codes.Add(row1.Item(0).ToString)
                    i = i + 1
                    count1 = count1 - 1
                End While
                txtSMAddShiftCode.AutoCompleteSource = AutoCompleteSource.None
                MySource_ShiftCodes.AddRange(Shift_Codes.ToArray)
                txtSMAddShiftCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddShiftCode.AutoCompleteCustomSource = MySource_ShiftCodes
                txtSMAddShiftCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            Else
                txtSMAddShiftLcnName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddShiftCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddShiftCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select  SHIFT_DESC from  OM_POS_SHIFT where SHIFT_CODE='" & txtSMAddShiftCode.Text & "' and SHIFT_LOCN_CODE='" & txtSMAddShiftLcnCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddShiftName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtSMAddShiftName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMShiftCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMShiftCancel.Click
        If pnlSMAddShift.Visible Then
            pnlSMAddShift.Hide()
            pnlSMEditShift.Hide()
            load_SMCounterDetails(txtSalesmanAddSMCode.Text)
            'pnlSMAddShift.Hide()
        End If
    End Sub

    Private Sub btnSMShiftAddOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMShiftAddOk.Click
        Try
            If txtSMAddShiftLcnName.Text = "" Then
                MsgBox("Please Select a Valid Location Code")
            ElseIf txtSMAddShiftName.Text = "" Then
                MsgBox("Please Select a Valid Shift Code")
            Else
                Dim stQuery As String
                Dim stQuery_chk As String
                Dim Freeze As String
                Dim ds As DataSet
                If txtSMAddShiftFz.Checked = True Then
                    Freeze = 1
                Else
                    Freeze = 2
                End If
                stQuery_chk = "select SMS_CODE from OM_POS_SALESMAN_SHIFT where SMS_SHIFT_CODE='" & txtSMAddShiftCode.Text & "' and SMS_LOCN_CODE='" & txtSMAddShiftLcnCode.Text & "' and SMS_CODE='" & txtSalesmanAddSMCode.Text & "'"
                ds = db.SelectFromTableODBC(stQuery_chk)
                If ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("The SalesMan already Exists in this Shift!")
                Else
                    stQuery = "INSERT INTO OM_POS_SALESMAN_SHIFT (SMS_CODE,SMS_SHIFT_CODE,SMS_LOCN_CODE,SMS_FRZ_FLAG_NUM,SMS_CR_DT,SMS_CR_UID) VALUES ("
                    stQuery = stQuery & "'" & txtSalesmanAddSMCode.Text & "', '" & txtSMAddShiftCode.Text & "','" & txtSMAddShiftLcnCode.Text & "','" & Freeze & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                    errLog.WriteToErrorLog("Insert Query OM_POS_SALESMAN_SHIFT", stQuery, "")
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Shift Details Saved!")
                    pnlSMAddShift.Hide()
                    'pnlSMAddCntrEdit.Hide()
                    load_SMShiftDetails(txtSalesmanAddSMCode.Text)
                    txtSMAddShiftLcnCode.Text = ""
                    txtSMAddShiftLcnName.Text = ""
                    txtSMAddShiftCode.Text = ""
                    txtSMAddShiftName.Text = ""
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnSMAddSHIFT_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddSHIFT_Edit.Click
        callEditShift()
    End Sub

    Private Sub lstviewSMMasterSHIFT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSMMasterSHIFT.DoubleClick
        callEditShift()
    End Sub

    Private Sub txtSMAddShiftLcnCodeEdit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddShiftLcnCodeEdit.TextChanged
        Try
            Dim stQuery As String
            Dim stQuery1 As String
            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim count1 As Integer
            Dim row1 As System.Data.DataRow
            Dim i As Integer
            stQuery = "select LOCN_NAME from OM_LOCATION where LOCN_CODE='" & txtSMAddShiftLcnCodeEdit.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddShiftLcnNameEdit.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Shift_Codes.Clear()
                stQuery1 = "SELECT  SHIFT_CODE FROM OM_POS_SHIFT where SHIFT_LOCN_CODE='" & txtSMAddShiftLcnCodeEdit.Text & "'"
                errLog.WriteToErrorLog("Query CNTER", stQuery1, "")
                ds1 = db.SelectFromTableODBC(stQuery1)
                count1 = ds1.Tables("Table").Rows.Count
                i = 0
                While count1 > 0
                    row1 = ds1.Tables("Table").Rows.Item(i)
                    Shift_Codes.Add(row1.Item(0).ToString)
                    i = i + 1
                    count1 = count1 - 1
                End While
                txtSMAddShiftCodeEdit.AutoCompleteSource = AutoCompleteSource.None
                MySource_CounterCodes.AddRange(Shift_Codes.ToArray)
                txtSMAddShiftCodeEdit.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtSMAddShiftCodeEdit.AutoCompleteCustomSource = MySource_CounterCodes
                txtSMAddShiftCodeEdit.AutoCompleteSource = AutoCompleteSource.CustomSource

            Else
                txtSMAddShiftNameEdit.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSMAddShiftCodeEdit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMAddShiftCodeEdit.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select  SHIFT_DESC from  OM_POS_SHIFT where SHIFT_CODE='" & txtSMAddShiftCodeEdit.Text & "' and SHIFT_LOCN_CODE='" & txtSMAddShiftLcnCodeEdit.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtSMAddShiftNameEdit.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtSMAddShiftNameEdit.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMShiftEditCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMShiftEditCancel.Click
        If pnlSMEditShift.Visible Then
            'Dim i As Integer = pnlSMAddCntrEdit.Height
            'While i >= 0
            '    pnlSMAddCntrEdit.Height = pnlSMAddCntrEdit.Height - 1
            '    Threading.Thread.Sleep(0.5)
            '    i = (i - 1)
            'End While
            'pnlSMAddCntrEdit.Visible = False
            pnlSMEditShift.Hide()
            load_SMShiftDetails(txtSalesmanAddSMCode.Text)
        End If
    End Sub

    Private Sub btnSMAddSHIFT_Home_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddSHIFT_Home.Click
        'If pnlSMAddShift.Visible Then
        '    Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
        '    While i >= 0
        '        pnlSMAddShift.Height = pnlSMAddCntrAdd.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSMAddShift.Visible = False
        'End If
        'If pnlSMEditShift.Visible Then
        '    Dim i As Integer = pnlSalesmanAddCompanyAdd.Height
        '    While i >= 0
        '        pnlSMEditShift.Height = pnlSMAddCntrAdd.Height - 1
        '        Threading.Thread.Sleep(0.5)
        '        i = (i - 1)
        '    End While
        pnlSMEditShift.Visible = False
        'End If
    End Sub

    Private Sub btnSMShiftEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMShiftEditUpdate.Click
        Try
            Dim stQuery_chk As String
            Dim ds As DataSet
            'stQuery_chk = "select SMS_CODE from OM_POS_SALESMAN_SHIFT where SMS_SHIFT_CODE='" & txtSMAddShiftCodeEdit.Text & "' and SMS_LOCN_CODE='" & txtSMAddShiftLcnCodeEdit.Text & "' and SMS_FRZ_FLAG_NUM = '1' and SMS_CODE='" & txtSalesmanAddSMCode.Text & "'"
            'ds = db.SelectFromTableODBC(stQuery_chk)
            'If ds.Tables("Table").Rows.Count > 0 Then
            '    MsgBox("The SalesMan already Exists in this Shift!")
            'Else
            Dim stQuery As String
            Dim Freeze As String
            If chkSMEditShiftFz.Checked = True Then
                Freeze = 1
            Else
                Freeze = 2
            End If
            stQuery = "UPDATE OM_POS_SALESMAN_SHIFT SET SMS_FRZ_FLAG_NUM='" & Freeze & "',SMS_UPD_DT=to_date(sysdate,'DD-MM-YY'),SMS_UPD_UID='" & LogonUser & "' WHERE SMS_CODE='" & txtSalesmanAddSMCode.Text & "' and SMS_LOCN_CODE='" & txtSMAddShiftLcnCodeEdit.Text & "' and SMS_SHIFT_CODE='" & txtSMAddShiftCodeEdit.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_POS_SALESMAN_SHIFT", stQuery, "")
            db.SaveToTableODBC(stQuery)
            MsgBox("Updated Successfully")
            pnlSMEditShift.Hide()
            load_SMShiftDetails(txtSalesmanAddSMCode.Text)
            'End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnSMAddSHIFT_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMAddSHIFT_Del.Click
        Try
            If lstviewSMMasterSHIFT.SelectedItems.Count = 0 Then
                MsgBox("Select a row!")
                Exit Sub
            Else
                If lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(5).Text = "1" Then
                    MsgBox("Cannot Delete!")
                Else
                    Dim stQuery As String
                    stQuery = "delete from OM_POS_SALESMAN_SHIFT where SMS_CODE='" & txtSalesmanAddSMCode.Text & "' and SMS_LOCN_CODE='" & lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(1).Text & "' and SMS_SHIFT_CODE='" & lstviewSMMasterSHIFT.SelectedItems.Item(0).SubItems(3).Text & "' and SMS_FRZ_FLAG_NUM='2'"
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Deleted successfully!")
                    load_SMShiftDetails(txtSalesmanAddSMCode.Text)
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnSMEditUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMEditUpdate.Click
        Try
            If txtSalesmanAddSMCode.Text = "" Then
                MsgBox("Please enter Salesman Code")
                Exit Sub
            ElseIf txtSalesmanAddSMName.Text = "" Then
                MsgBox("Please enter Salesman Name")
                Exit Sub
            ElseIf txtSalesmanAddSMSHR_Name.Text = "" Then
                MsgBox("Please enter Salesman Short Name")
                Exit Sub
            End If

            Dim stQuery As String
            Dim Freeze As String = ""

            If chkboxSalesmanAddFreeze.Checked = True Then
                Freeze = "1"
            Else
                Freeze = "2"
            End If
            stQuery = "UPDATE OM_SALESMAN SET SM_NAME='" & txtSalesmanAddSMName.Text & "',SM_SHORT_NAME='" & txtSalesmanAddSMSHR_Name.Text & "',SM_FRZ_FLAG_NUM='" & Freeze & "',SM_MIN_MARKUP_PERC='0',SM_MAX_VARIANCE_PERC='0',SM_BL_NAME='',SM_BL_SHORT_NAME='',SM_UPD_DT=to_date(sysdate,'DD-MM-YY'),SM_UPD_UID='" & LogonUser & "' WHERE SM_CODE='" & txtSalesmanAddSMCode.Text & "' "
            errLog.WriteToErrorLog("Update Query OM_SALESMAN", stQuery, "")
            db.SaveToTableODBC(stQuery)
            MsgBox("Updated Successfully")

            pnlSalesmanAdd.Hide()
            load_AllSalesmanDetails()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lstviewSalesmanMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSalesmanMaster.DoubleClick
        callEditSM()
    End Sub

    Private Sub lstviewSalesmanMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstviewSalesmanMaster.SelectedIndexChanged

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lstviewSalesmanMaster.Items.Clear()
            Dim ds As DataSet
            Dim stQuery As String
            stQuery = "select SM_CODE,SM_NAME, SM_SHORT_NAME,SM_CR_UID,SM_FRZ_FLAG_NUM from OM_SALESMAN where SM_CODE LIKE '" & txtScode.Text.Replace("'", "''") & "%' and SM_NAME LIKE '" & txtSName.Text.Replace("'", "''") & "%' order by SM_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer
            count = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSalesmanMaster.Items.Add(i + 1)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(2).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(3).ToString)
                lstviewSalesmanMaster.Items(i).SubItems.Add(row.Item(4).ToString)

                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try


    End Sub

    Private Sub txtScode_TextChanged(sender As Object, e As EventArgs) Handles txtScode.TextChanged

    End Sub

    Private Sub btnCounterEditUpdate_Click1(sender As Object, e As EventArgs) Handles btnCounterEditUpdate.Click

    End Sub
End Class