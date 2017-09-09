Imports PdfSharp.Pdf
Imports PdfSharp.Drawing

Public Class ReferalReport

    Dim locnCodeval As String = ""
    Dim compCodeval As String = ""
    Dim salesmanCodeval As String = ""

    Dim _page As Integer
    Dim bitmaps As New List(Of Bitmap)

    Dim ds As New DataSet
    Dim dsnew As New DataSet
    Dim db As New DBConnection
    Dim Count As Integer
    Dim Count1 As Integer
    Dim CountList As Integer
    Dim Query As String
    Dim dt As DataTable
    Dim stDateval As String
    Dim endDateval As String
    Dim strArrLoc As Array
    Dim strArrSM As Array
    Dim itemlist As String = ""
    Dim groupval As String = ""
    Dim conditionst As String
    Private pnlReportPages As New List(Of Panel)
    Private pnlLines As New List(Of Panel)
    Private lblCompany As New List(Of Label)
    Private lblCompanyVal As New List(Of Label)
    Private lblLocation As New List(Of Label)
    Private lblLocationVal As New List(Of Label)
    Private lblSalesman As New List(Of Label)
    Private lblSalesmanVal As New List(Of Label)
    Private lblTransDate As New List(Of Label)
    Private lblTransNo As New List(Of Label)
    Private lblTransHospID As New List(Of Label)
    Private lblTransHospName As New List(Of Label)
    Private lblTransDoctorID As New List(Of Label)
    Private lblTransDoctorName As New List(Of Label)
    Private lblItemDispercHead As New List(Of Label)
    Private lblItemGrossvalHead As New List(Of Label)
    Private lblTrans_TxnCode As New List(Of Label)
    Private lblTrans_TransNo As New List(Of Label)
    Private lblTrans_TransDate As New List(Of Label)
    Private lblTrans_TransSalesman As New List(Of Label)
    Private lstviewTrans As New List(Of ListView)

    Dim lstview As ListView

    Dim currentpage As String = ""
    Dim currentpositon As Integer = 0
    Dim lstviewposition As Integer = 0

    Private Sub frmEndofthedayrep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetResolution()
            Me.Dock = DockStyle.Fill
            LoadLocation()
            cmbLocation.Text = Location_Code
            'cmbLocation_SelectedValueChanged(sender, e)
            SplitContainer1.Panel2.Select()
            LoadSM()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
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

                For Each ctl As Control In pnlReportHead.Controls
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







                For Each ctl As Control In SplitContainer1.Controls
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


            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.StackTrace, ex.Message, "Error")
        End Try

    End Sub


    Public Function LoadLocation()
        'Me.cmbLocation.DataSource = ds.Tables("Table")
        'Me.cmbLocation.DisplayMember = "locdisplay"
        'Me.cmbLocation.ValueMember = "loccode"
        Dim stQuery = New String("")
        stQuery = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from om_location order by locdisplay"
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count

        Me.cmbLocation.DataSource = ds.Tables("Table")
        Me.cmbLocation.DisplayMember = "locdisplay"
        Me.cmbLocation.ValueMember = "loccode"

        Return ds
    End Function

    Private Sub LoadSM()
        Try
            'If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " And cmbCounter.Text <> "System.Data.DataRowView" And cmbCounter.Text <> " " Then
            If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " Then
                ds.Dispose()
                strArrLoc = cmbLocation.Text.Split("-")
                'Query = "SELECT SM_CODE as salemancode, SM_CODE  FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & strArrLoc(0) & "' AND SMC_COUNT_CODE = '" & cmbCounter.Text & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
                Query = "SELECT SM_CODE as salemancode, SM_CODE  FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & strArrLoc(0) & "'  AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
                errLog.WriteToErrorLog(Query, "", "OM_SALESMAN")
                ds = db.SelectFromTableODBC(Query)
                cmbSm.Items.Clear()
                If ds.Tables("Table").Rows.Count > 0 Then
                    cmbSm.Items.Add("All")
                    cmbSm.Text = "All"
                    For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
                        cmbSm.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
                    Next
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    'Public Sub LoadCounter()
    '    Try
    '        If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " Then
    '            ds.Dispose()
    '            strArrLoc = cmbLocation.Text.Split("-")
    '            Dim Query As String = "select poscnt_no from om_pos_counter where poscnt_locn_code='" & strArrLoc(0) & "' AND POSCNT_FRZ_FLAG_NUM=2"
    '            ds = db.SelectFromTableODBC(Query)
    '            cmbCounter.Items.Clear()
    '            cmbCounter.Text = ""
    '            Dim count As Integer = ds.Tables("Table").Rows.Count
    '            Dim i As Integer = 0
    '            While count > 0
    '                cmbCounter.Text = ds.Tables("Table").Rows.Item(i).Item(0).ToString
    '                cmbCounter.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
    '                i = i + 1
    '                count = count - 1
    '            End While
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    'Public Sub LoadShift()
    '    Try
    '        If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " Then
    '            ds.Dispose()
    '            Dim Query As String = "select shift_code from om_pos_shift where shift_locn_code='" & strArrLoc(0) & "' and SHIFT_FRZ_FLAG_NUM='2'"
    '            ds = db.SelectFromTableODBC(Query)
    '            cmbShift.Items.Clear()
    '            cmbShift.Text = ""
    '            If ds.Tables("Table").Rows.Count > 0 Then
    '                cmbShift.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
    '                For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
    '                    cmbShift.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    'Private Sub cmbLocation_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedValueChanged
    '    'LoadCounter()
    '    'LoadShift()
    '    LoadSM()
    'End Sub

    'Private Sub cmbCounter_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    ' LoadShift()
    '    LoadSM()
    'End Sub


    'Private Sub cmbShift_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    LoadSM()
    'End Sub

    Private Sub CreatePage_WithHeader()
        Me.SplitContainer1.Panel2.AutoScrollPosition = New System.Drawing.Point(0, 0)
        Dim pnl As Panel
        Dim n As Integer

        n = pnlReportPages.Count
        pnl = New Panel
        With pnl
            If n = 0 Then
                .Location = New Point(3, (n * 980) + 3)
            Else
                .Location = New Point(3, (n * 980) + ((n + 1) * 3))
            End If
            .Name = "pnlReportPage" & n.ToString
            currentpage = "pnlReportPage" & n.ToString
            .Size = New Size(770, 980)
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.White
        End With
        Me.pnlReportPages.Add(pnl)
        Me.SplitContainer1.Panel2.Controls.Add(pnl)

        If pnlReportPages.Count = 1 Then
            Dim pic As New PictureBox
            With pic
                .Location = New Point(330, 0)
                .Name = "picReport"
                .Size = New Size(80, 64)
                .Image = My.Resources.clientlogo1
                .SizeMode = PictureBoxSizeMode.StretchImage
            End With
            Me.Controls.Find(currentpage, True)(0).Controls.Add(pic)
        End If

        Dim headlbl As New Label
        With headlbl
            .Location = New Point(285, 65)
            .Name = "lblhead"
            .Size = New Size(170, 25)
            If pnlReportPages.Count = 1 Then
                .Text = "Referral Report"
            Else
                .Text = "(" & (n + 1).ToString & ")"
            End If
            .TextAlign = ContentAlignment.TopCenter
            .Font = New Font("Times New Roman", 10, FontStyle.Bold)
            .ForeColor = Color.DarkBlue
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(headlbl)

        Dim datelbl As New Label
        With datelbl
            .Location = New Point(30, 80)
            .Name = "lbldatefrom"
            .Size = New Size(45, 20)
            .Text = "From:"
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .TextAlign = ContentAlignment.BottomLeft
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(datelbl)

        datelbl = New Label
        With datelbl
            .Location = New Point(75, 80)
            .Name = "lbldatefromvalue"
            .Size = New Size(70, 20)
            .Text = dtstDate.Value.ToString("dd-MM-yyyy")
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Regular)
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(datelbl)

        datelbl = New Label
        With datelbl
            .Location = New Point(150, 80)
            .Name = "lbldateto"
            .Size = New Size(30, 20)
            .Text = "To:"
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .TextAlign = ContentAlignment.BottomLeft
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(datelbl)

        datelbl = New Label
        With datelbl
            .Location = New Point(180, 80)
            .Name = "lbldatetovalue"
            .Size = New Size(70, 20)
            .Text = dtendDate.Value.ToString("dd-MM-yyyy")
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Regular)
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(datelbl)

        Dim pnlLine As New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(30, 101)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(710, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        Dim lbl As New Label
        n = lblCompany.Count
        With lbl
            .Location = New Point(30, 105)
            .Name = "lblCompany" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Company  "
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
        End With
        Me.lblCompany.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblCompany.Count
        With lbl
            .Location = New Point(100, 105)
            .Name = "lblCompanyVal" & n.ToString
            .Size = New Size(450, 20)
            .Text = compCodeval
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Regular)
        End With
        Me.lblCompanyVal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocation.Count
        With lbl
            .Location = New Point(30, 130)
            .Name = "lblLocation" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Location  "
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
        End With
        Me.lblLocation.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocation.Count
        With lbl
            .Location = New Point(100, 130)
            .Name = "lblLocationVal" & n.ToString
            .Size = New Size(500, 20)
            .Text = locnCodeval
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Regular)
        End With
        Me.lblLocationVal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblSalesman.Count
        With lbl
            .Location = New Point(30, 155)
            .Name = "lblSalesman" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Salesman  "
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
        End With
        Me.lblSalesman.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblSalesmanVal.Count
        With lbl
            .Location = New Point(100, 155)
            .Name = "lblSalesmanVal" & n.ToString
            .Size = New Size(500, 20)
            If cmbSm.Text = "All" Then
                .Text = "All"
            Else
                .Text = salesmanCodeval
            End If
            .TextAlign = ContentAlignment.BottomLeft
            .Font = New Font("Times New Roman", 8, FontStyle.Regular)
        End With
        Me.lblSalesmanVal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(30, 180)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(710, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)


        lbl = New Label
        n = lblTransDate.Count
        With lbl
            .Location = New Point(30, 183)
            .Name = "lblTransDate" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Trans. Date"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransDate.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblTransNo.Count
        With lbl
            .Location = New Point(100, 183)
            .Name = "lblTransNo" & n.ToString
            .Size = New Size(80, 20)
            .Text = "Trans. No"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransNo.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblTransHospID.Count
        With lbl
            .Location = New Point(180, 183)
            .Name = "lblTransHospID" & n.ToString
            .Size = New Size(90, 20)
            .Text = "Hospital ID"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransHospID.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblTransHospName.Count
        With lbl
            .Location = New Point(270, 183)
            .Name = "lblTransHospName" & n.ToString
            .Size = New Size(200, 20)
            .Text = "Hospital Name"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransHospName.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblTransDoctorID.Count
        With lbl
            .Location = New Point(470, 183)
            .Name = "lblTransDoctorID" & n.ToString
            .Size = New Size(90, 20)
            .Text = "Doctor ID"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransDoctorID.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblTransDoctorName.Count
        With lbl
            .Location = New Point(560, 183)
            .Name = "lblTransDoctorName" & n.ToString
            .Size = New Size(180, 20)
            .Text = "Doctor Name"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblTransDoctorName.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)


        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(30, 204)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(710, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

    End Sub

    Private Sub CreateListView()

        lstview = New ListView
        Dim m As Integer
        m = lstviewTrans.Count
        With lstview
            .Location = New Point(30, currentpositon + 3)
            .Name = "lstviewTrans" & m.ToString
            .Size = New Size(710, 675)
            .Font = New Font("Times New Roman", 10, FontStyle.Regular)
            .Columns.Add("Trans Date", 68, HorizontalAlignment.Center)
            .Columns.Add("Trans No", 80, HorizontalAlignment.Center)
            .Columns.Add("Hospital Code", 90, HorizontalAlignment.Center)
            .Columns.Add("Hospital Name", 200, HorizontalAlignment.Left)
            .Columns.Add("Doctor ID", 90, HorizontalAlignment.Center)
            .Columns.Add("Doctor Name", 178, HorizontalAlignment.Left)
            .HeaderStyle = ColumnHeaderStyle.None
            .BorderStyle = BorderStyle.None
            .GridLines = False
            .View = View.Details
            .MultiSelect = False
        End With
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lstview)
    End Sub


    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click
        Try

            lblReportTitle.Visible = False

            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim i As Integer

            stQuery = " select comp_name from fm_company where comp_code='" & CompanyCode & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                compCodeval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            stQuery = "select locn_name from om_location where locn_code='" & cmbLocation.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                locnCodeval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            stQuery = "select sm_name from om_salesman where sm_code='" & cmbSm.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                salesmanCodeval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If



            Me.SplitContainer1.Panel2.Controls.Clear()
            pnlReportPages.Clear()

            CreatePage_WithHeader()

            Dim invquerycount As Integer = 0
            Dim lbl As Label
            ds.Clear()
            'Dim strSMArr() As String = cmbSm.Text.Split("-")
            If cmbSm.Text = "All" Then
                'stQuery = "SELECT INVRH_INVH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_INVOICE_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVH_FLEX_19= '" & cmbShift.Text & "' and INVH_FLEX_20= '" & cmbCounter.Text & "' and INVRH_INVH_SYS_ID = INVH_SYS_ID"
                stQuery = "SELECT INVRH_INVH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_INVOICE_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "000000','dd-MM-yyyy hh24miss') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "235959','dd-MM-yyyy hh24miss')  and INVRH_INVH_SYS_ID = INVH_SYS_ID"

            Else
                'stQuery = "SELECT INVRH_INVH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_INVOICE_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVH_FLEX_19= '" & cmbShift.Text & "' and INVH_FLEX_20= '" & cmbCounter.Text & "' and INVH_SM_CODE = '" & cmbSm.Text & "'and INVRH_INVH_SYS_ID = INVH_SYS_ID"
                stQuery = "SELECT INVRH_INVH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_INVOICE_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "000000','dd-MM-yyyy hh24miss') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "235959','dd-MM-yyyy hh24miss')  and INVH_SM_CODE = '" & cmbSm.Text & "' and INVRH_INVH_SYS_ID = INVH_SYS_ID"
            End If
            'Dim stQuery As String = "SELECT  INVH_SYS_ID,INVH_COMP_CODE,INVH_TXN_CODE,INVH_NO,INVH_NO,INVH_NO FROM  ot_invoice_head WHERE rownum <= 25"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            invquerycount = count
            i = 0
            currentpositon = 208
            Dim c As Integer = 0
            If count > 0 Then
                lbl = New Label
                With lbl
                    .Location = New Point(30, 208)
                    .Name = "lblSalesInvoiceHead"
                    .Size = New Size(120, 20)
                    currentpositon = currentpositon + 20
                    .Text = "Sales Invoice"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                    .BackColor = Color.LightGray
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                CreateListView()


                While count > 0
                    If Not i = 0 Then
                        If i Mod 21 = 0 Then
                            CreatePage_WithHeader()
                            CreateListView()
                            c = 0
                        End If
                    End If

                    lstview.Items.Add(ds.Tables("Table").Rows.Item(i).Item(5).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(2).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(3).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(4).ToString)
                    lstviewposition = currentpositon + ((c + 1) * 16)

                    c = c + 1

                    lstview.Items.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstviewposition = currentpositon + ((c + 1) * 16)

                    c = c + 1
                    count = count - 1
                    i = i + 1
                End While

            End If

            If Not invquerycount = 0 Then
                lstview.Height = c * 16
            Else
                c = 40
                lstviewposition = 228
            End If
            'MsgBox((700 + 231) - lstviewposition)

            'stQuery = "SELECT  INVH_SYS_ID,INVH_COMP_CODE,INVH_TXN_CODE,INVH_NO,INVH_NO,INVH_NO FROM  ot_invoice_head WHERE rownum <=1"
            If cmbSm.Text = "All" Then
                'stQuery = "SELECT INVRH_SOH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_SO_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and SOH_FLEX_19= '" & cmbShift.Text & "' and SOH_FLEX_20= '" & cmbCounter.Text & "' and INVRH_SOH_SYS_ID = SOH_SYS_ID"
                stQuery = "SELECT INVRH_SOH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_SO_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "000000','dd-MM-yyyy hh24miss') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "235959','dd-MM-yyyy hh24miss')   and INVRH_SOH_SYS_ID = SOH_SYS_ID"
            Else
                'stQuery = "SELECT INVRH_SOH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_SO_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and SOH_FLEX_19= '" & cmbShift.Text & "' and SOH_FLEX_20= '" & cmbCounter.Text & "' and SOH_SM_CODE = '" & cmbSm.Text & "'and INVRH_SOH_SYS_ID = SOH_SYS_ID"
                stQuery = "SELECT INVRH_SOH_NO,INVRH_REF_HOSPITAL_CODE,VSSV_NAME, INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, to_char(INVRH_CR_DT,'dd-MM-yyyy') as INVRH_CR_DT FROM OT_INVOICE_REF_HOSPITAL,IM_VS_STATIC_VALUE,OT_SO_HEAD WHERE INVRH_COMP_CODE='001' AND INVRH_LOCN_CODE ='" + cmbLocation.Text + "' AND INVRH_REF_HOSPITAL_CODE=VSSV_CODE AND VSSV_VS_CODE ='REF_HOSPITAL' and INVRH_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd-MM-yyyy") + "000000','dd-MM-yyyy hh24miss') and INVRH_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd-MM-yyyy") + "235959','dd-MM-yyyy hh24miss')  and INVRH_SOH_SYS_ID = SOH_SYS_ID and SOH_SM_CODE='" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("SO Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0

            If count > 0 Then

                Dim remaininglistview As Integer = (40 - c) / 2
                If remaininglistview = 0 Then
                    If Not invquerycount = 0 Then
                        CreatePage_WithHeader()
                        CreateListView()
                    Else
                        currentpositon = 228
                        CreateListView()
                    End If
                    lbl = New Label
                    With lbl
                        .Location = New Point(30, 208)
                        .Name = "lblSalesOrderHead"
                        .Size = New Size(120, 20)
                        .Text = "Sales Order"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                        .BackColor = Color.LightGray
                        .BorderStyle = BorderStyle.None
                        .BringToFront()
                    End With
                    Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
                Else
                    lbl = New Label
                    With lbl
                        .Location = New Point(30, lstviewposition + 5)
                        .Name = "lblSalesOrderHead"
                        .Size = New Size(120, 20)
                        .Text = "Sales Order"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                        .BackColor = Color.LightGray
                        .BorderStyle = BorderStyle.FixedSingle
                        .BringToFront()
                    End With
                    Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                    lstview = New ListView
                    Dim m As Integer
                    m = lstviewTrans.Count
                    With lstview
                        .Location = New Point(30, lstviewposition + 30)
                        .Name = "lstviewTrans" & m.ToString
                        .Size = New Size(710, remaininglistview * 33)
                        .Font = New Font("Times New Roman", 10, FontStyle.Regular)
                        .Columns.Add("Trans Date", 68, HorizontalAlignment.Center)
                        .Columns.Add("Trans No", 80, HorizontalAlignment.Center)
                        .Columns.Add("Hospital Code", 90, HorizontalAlignment.Center)
                        .Columns.Add("Hospital Name", 200, HorizontalAlignment.Left)
                        .Columns.Add("Doctor ID", 90, HorizontalAlignment.Center)
                        .Columns.Add("Doctor Name", 178, HorizontalAlignment.Left)
                        .HeaderStyle = ColumnHeaderStyle.None
                        .BorderStyle = BorderStyle.None
                        .GridLines = False
                        .View = View.Details
                        .MultiSelect = False
                    End With
                    Me.Controls.Find(currentpage, True)(0).Controls.Add(lstview)
                End If

                c = 0
                Dim tempval As Integer = remaininglistview
                'Dim c As Integer = 0
                While count > 0
                    If remaininglistview > 0 Then
                        remaininglistview = remaininglistview - 1
                        'MsgBox(remaininglistview)
                    End If
                    If Not i = 0 Then
                        If remaininglistview = 0 Then
                            If (i - tempval) Mod 21 = 0 Then
                                CreatePage_WithHeader()
                                CreateListView()
                                c = 0
                            End If
                        End If
                    End If

                    lstview.Items.Add(ds.Tables("Table").Rows.Item(i).Item(5).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(2).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(3).ToString)
                    lstview.Items(c).SubItems.Add(ds.Tables("Table").Rows.Item(i).Item(4).ToString)
                    lstviewposition = currentpositon + ((c + 1) * 20)

                    c = c + 1

                    lstview.Items.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstview.Items(c).SubItems.Add("")
                    lstviewposition = currentpositon + ((c + 1) * 20)

                    c = c + 1
                    count = count - 1
                    i = i + 1
                End While
            End If

            SplitContainer1.Panel2.Select()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SplitContainer1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SplitContainer1.MouseWheel
        Dim myview As Point = Me.SplitContainer1.Panel2.AutoScrollPosition
        myview.X = myview.X + 50
        myview.Y = myview.Y + 50
        Me.SplitContainer1.Panel2.AutoScrollPosition = myview
    End Sub

    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        Try
            If Not pnlReportPages.Count > 0 Then
                Exit Sub
            End If
            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
            PrintDialog1.AllowSomePages = True

            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
                'totPages = pnlReportPages.Count
                'MsgBox(totPages)
                For i = 0 To pnlReportPages.Count - 1
                    pnlReportPages(i).BorderStyle = BorderStyle.None
                    Dim bmpimg = New Bitmap(Me.Controls.Find(pnlReportPages(i).Name, True)(0).Width, Me.Controls.Find(pnlReportPages(i).Name, True)(0).Height)
                    Me.Controls.Find(pnlReportPages(i).Name, True)(0).DrawToBitmap(bmpimg, Me.Controls.Find(pnlReportPages(i).Name, True)(0).ClientRectangle)
                    bitmaps.Add(bmpimg)
                    pnlReportPages(i).BorderStyle = BorderStyle.FixedSingle
                Next
                PrintDocument1.Print()
                'Next
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim g As Graphics = e.Graphics
        Dim bitmap As Bitmap = Me.bitmaps(_page)
        g.DrawImage(bitmap, New Rectangle(15, 25, bitmap.Width, bitmap.Height))

        e.HasMorePages = System.Threading.Interlocked.Increment(_page) < Me.bitmaps.Count
        g.Dispose()
    End Sub

    Private Sub btnrefreshEOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshEOD.Click
        Try
            If Not pnlReportPages.Count > 0 Then
                Exit Sub
            End If
            Dim doc As New PdfDocument()
            Dim bmplist As New List(Of Bitmap)
            Dim xgrlist As New List(Of XGraphics)
            For i = 0 To pnlReportPages.Count - 1
                pnlReportPages(i).BorderStyle = BorderStyle.None
                Dim bmpimg = New Bitmap(Me.Controls.Find(pnlReportPages(i).Name, True)(0).Width, Me.Controls.Find(pnlReportPages(i).Name, True)(0).Height)
                Me.Controls.Find(pnlReportPages(i).Name, True)(0).DrawToBitmap(bmpimg, Me.Controls.Find(pnlReportPages(i).Name, True)(0).ClientRectangle)
                doc.Pages.Add(New PdfPage())
                Dim xgrGraph As XGraphics = XGraphics.FromPdfPage(doc.Pages(i))
                Dim imgX As XImage = XImage.FromGdiPlusImage(bmpimg)
                xgrGraph.DrawImage(imgX, 15, 20)
                pnlReportPages(i).BorderStyle = BorderStyle.FixedSingle
            Next

            SaveFileDialog1.Filter = "PDF Files (*.pdf*)|*.pdf"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                doc.Save(SaveFileDialog1.FileName)
                doc.Close()
                MsgBox("File has been saved successfully at '" + SaveFileDialog1.FileName + "'")
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub pnlReportHead_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlReportHead.Paint

    End Sub
End Class