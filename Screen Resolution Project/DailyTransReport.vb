Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports System.Math
Public Class DailyTransReport

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
    Private lblItemCodeHead As New List(Of Label)
    Private lblItemDescHead As New List(Of Label)
    Private lblItemQtyHead As New List(Of Label)
    Private lblItemRateHead As New List(Of Label)
    Private lblItemExpamtHead As New List(Of Label)
    Private lblItemDisamtHead As New List(Of Label)
    Private lblItemDispercHead As New List(Of Label)
    Private lblItemGrossvalHead As New List(Of Label)
    Private lblItemGrossTotal As New List(Of Label)

    Private lblItemCode As New List(Of Label)
    Private lblItemDesc As New List(Of Label)
    Private lblItemQty As New List(Of Label)
    Private lblItemRate As New List(Of Label)
    Private lblItemExpamt As New List(Of Label)
    Private lblItemDisamt As New List(Of Label)
    Private lblItemDisperc As New List(Of Label)
    Private lblItemGrossval As New List(Of Label)

    Private lblTrans_TxnCode As New List(Of Label)
    Private lblTrans_TransNo As New List(Of Label)
    Private lblTrans_TransDate As New List(Of Label)
    Private lblTrans_TransSalesman As New List(Of Label)
    Private lblTrans_NullItems As New List(Of Label)

    Dim salesinv_transnettotal As Double = 0
    Dim salesorder_transnettotal As Double = 0
    Dim salesrreturn_transnettotal As Double = 0
    Dim currentpage As String = ""
    Dim currentpositon As Integer = 0

    Private Sub frmEndofthedayrep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetResolution()
            Me.Dock = DockStyle.Fill
            Dim dtQuery As String
            Dim dt As DataSet
            dtQuery = "select to_char(sysdate,'dd-mm-yyyy') from dual"
            dt = db.SelectFromTableODBC(dtQuery)
            dtstDate.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)
            dtendDate.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)

            LoadLocation()
            cmbLocation.Text = Location_Code
            'cmbLocation_SelectedValueChanged(sender, e)
            SplitContainer1.Panel2.Select()
            cmbSm.Text = "All"
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
        stQuery = "select LOCN_CODE as loccode, LOCN_CODE || ' - ' || LOCN_SHORT_NAME as locdisplay from om_location order by locdisplay"
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count
        Dim i As Integer = 0
        cmbLocation.Items.Clear()
        While Count > 0
            cmbLocation.Items.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString)
            Count = Count - 1
            i = i + 1
        End While
        'Me.cmbLocation.DataSource = ds.Tables("Table")
        'Me.cmbLocation.DisplayMember = "locdisplay"
        'Me.cmbLocation.ValueMember = "loccode"

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
                Dim count As Integer = ds.Tables("Table").Rows.Count
                Dim i As Integer = 0
                cmbSm.Items.Clear()
                cmbSm.Items.Add("All")
                While count > 0
                    cmbSm.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
                'If ds.Tables("Table").Rows.Count <> 0 Then
                '    cmbSm.DataSource = ds.Tables("Table")
                '    cmbSm.DisplayMember = "salemancode"
                '    cmbSm.ValueMember = "SM_CODE"
                'End If
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
    '    'LoadShift()
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
                .Text = "Daily Transaction Report"
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
        n = lblItemCodeHead.Count
        With lbl
            .Location = New Point(30, 183)
            .Name = "lblItemCodeHead" & n.ToString
            .Size = New Size(120, 20)
            .Text = "Item Code"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemCodeHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemDescHead.Count
        With lbl
            .Location = New Point(150, 183)
            .Name = "lblItemDescHead" & n.ToString
            .Size = New Size(200, 20)
            .Text = "Item Desc"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemDescHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemQtyHead.Count
        With lbl
            .Location = New Point(350, 183)
            .Name = "lblItemQtyHead" & n.ToString
            .Size = New Size(40, 20)
            .Text = "Qty"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemQtyHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemRateHead.Count
        With lbl
            .Location = New Point(390, 183)
            .Name = "lblItemRateHead" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Item Rate"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemRateHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemExpamtHead.Count
        With lbl
            .Location = New Point(460, 183)
            .Name = "lblItemExpamtHead" & n.ToString
            .Size = New Size(60, 20)
            .Text = "Exp. Amt"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemExpamtHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemDisamtHead.Count
        With lbl
            .Location = New Point(520, 183)
            .Name = "lblItemDisamtHead" & n.ToString
            .Size = New Size(70, 20)
            .Text = "Disc. Amt"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemDisamtHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemDispercHead.Count
        With lbl
            .Location = New Point(590, 183)
            .Name = "lblItemDispercHead" & n.ToString
            .Size = New Size(60, 20)
            .Text = "Disc. Perc"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemDispercHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblItemGrossvalHead.Count
        With lbl
            .Location = New Point(650, 183)
            .Name = "lblItemGrossvalHead" & n.ToString
            .Size = New Size(90, 20)
            .Text = "Gross Val"
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Times New Roman", 8, FontStyle.Bold)
            .BackColor = Color.GhostWhite
        End With
        Me.lblItemGrossvalHead.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(30, 204)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(710, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)
        currentpositon = 208
    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click

        Try
            salesinv_transnettotal = 0
            salesorder_transnettotal = 0
            salesrreturn_transnettotal = 0

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

            Dim lbl As Label
            If cmbSm.Text = "All" Then
                'stQuery = "select INVH_SYS_ID,INVH_NO,INVH_TXN_CODE,to_char(INVH_DT,'DD/MM/YYYY'),SM_NAME from ot_invoice_head,om_salesman where INVH_SM_CODE=SM_CODE and INVH_COMP_CODE='" & CompanyCode & "' and INVH_TXN_CODE='POSINV' and INVH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and INVH_FLEX_19='" & cmbShift.Text & "' and INVH_FLEX_20='" & cmbCounter.Text & "' and INVH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and INVH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select INVH_SYS_ID,INVH_NO,INVH_TXN_CODE,to_char(INVH_DT,'DD/MM/YYYY'),SM_NAME from ot_invoice_head,om_salesman where INVH_SM_CODE=SM_CODE and INVH_COMP_CODE='" & CompanyCode & "' and INVH_TXN_CODE='POSINV' and INVH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "' and INVH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and INVH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by INVH_DOC_SRC_LOCN_CODE, INVH_NO"
            Else
                'stQuery = "select INVH_SYS_ID,INVH_NO,INVH_TXN_CODE,to_char(INVH_DT,'DD/MM/YYYY'),SM_NAME from ot_invoice_head,om_salesman where INVH_SM_CODE=SM_CODE and INVH_COMP_CODE='" & CompanyCode & "' and INVH_TXN_CODE='POSINV' and INVH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and INVH_FLEX_19='" & cmbShift.Text & "' and INVH_FLEX_20='" & cmbCounter.Text & "' and INVH_SM_CODE='" & cmbSm.Text & "' and INVH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and INVH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select INVH_SYS_ID,INVH_NO,INVH_TXN_CODE,to_char(INVH_DT,'DD/MM/YYYY'),SM_NAME from ot_invoice_head,om_salesman where INVH_SM_CODE=SM_CODE and INVH_COMP_CODE='" & CompanyCode & "' and INVH_TXN_CODE='POSINV' and INVH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "' and INVH_SM_CODE='" & cmbSm.Text & "' and INVH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and INVH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by INVH_DOC_SRC_LOCN_CODE, INVH_NO"
            End If
            errLog.WriteToErrorLog("DSTR SalesInvoice Head Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0

            While count > 0
                If i = 0 Then
                    lbl = New Label
                    With lbl
                        .Location = New Point(30, currentpositon)
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
                End If

                If currentpositon > 900 Then
                    CreatePage_WithHeader()
                End If

                Dim lblTrans As Label
                lblTrans = New Label
                Dim n = lblTrans_TxnCode.Count
                With lblTrans
                    .Location = New Point(30, currentpositon + 2)
                    .Name = "lblTrans_TxnCode" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans Code : " & ds.Tables("Table").Rows.Item(i).Item(2).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransNo.Count
                With lblTrans
                    .Location = New Point(170, currentpositon + 2)
                    .Name = "lblTrans_TransNo" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans No : " & ds.Tables("Table").Rows.Item(i).Item(1).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransDate.Count
                With lblTrans
                    .Location = New Point(310, currentpositon + 2)
                    .Name = "lblTrans_TransDate" & n.ToString
                    .Size = New Size(120, 20)
                    .Text = "Date : " & ds.Tables("Table").Rows.Item(i).Item(3).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransSalesman.Count
                With lblTrans
                    .Location = New Point(430, currentpositon + 2)
                    .Name = "lblTrans_TransSalesman" & n.ToString
                    .Size = New Size(310, 20)
                    .Text = "Salesman : " & ds.Tables("Table").Rows.Item(i).Item(4).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                currentpositon = currentpositon + 25

                FetchInvoiceDetails(ds.Tables("Table").Rows.Item(i).Item(0).ToString)

                count = count - 1
                i = i + 1
            End While
            Dim pnlLine
            Dim k As Integer
            If ds.Tables("Table").Rows.Count > 0 Then
                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 2)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 3

                lbl = New Label
                With lbl
                    .Name = "lblSalesinvTransTotalHead"
                    .Text = salesinv_transnettotal.ToString("0.00")
                    .Location = New Point(460, currentpositon)
                    .Size = New Size(190, 20)
                    .BackColor = Color.LightGray
                    .Font = New Font("Times New Roman", 9, FontStyle.Bold)
                    .Text = "Transaction Net Total"
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Name = "lblSalesinvTransTotal"
                    .Text = salesinv_transnettotal.ToString("0.00")
                    .Location = New Point(650, currentpositon)
                    .Size = New Size(90, 20)
                    .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                currentpositon = currentpositon + 20

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 1)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 2

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4
            End If

            '////  SO Fetch  //////////////////////////////

            If cmbSm.Text = "All" Then
                ' stQuery = "select SOH_SYS_ID,SOH_NO,SOH_TXN_CODE,to_char(SOH_DT,'DD/MM/YYYY'),SM_NAME from OT_SO_HEAD,om_salesman where SOH_SM_CODE=SM_CODE and SOH_COMP_CODE='" & CompanyCode & "' and SOH_TXN_CODE='SO' and SOH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and SOH_FLEX_19='" & cmbShift.Text & "' and SOH_FLEX_20='" & cmbCounter.Text & "' and SOH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and SOH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select SOH_SYS_ID,SOH_NO,SOH_TXN_CODE,to_char(SOH_DT,'DD/MM/YYYY'),SM_NAME from OT_SO_HEAD,om_salesman where SOH_SM_CODE=SM_CODE and SOH_COMP_CODE='" & CompanyCode & "' and SOH_TXN_CODE='SO' and SOH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "'  and SOH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and SOH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by SOH_DOC_SRC_LOCN_CODE,SOH_NO"
            Else
                'stQuery = "select SOH_SYS_ID,SOH_NO,SOH_TXN_CODE,to_char(SOH_DT,'DD/MM/YYYY'),SM_NAME from OT_SO_HEAD,om_salesman where SOH_SM_CODE=SM_CODE and SOH_COMP_CODE='" & CompanyCode & "' and SOH_TXN_CODE='SO' and SOH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and SOH_FLEX_19='" & cmbShift.Text & "' and SOH_FLEX_20='" & cmbCounter.Text & "' and SOH_SM_CODE='" & cmbSm.Text & "' and SOH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and SOH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select SOH_SYS_ID,SOH_NO,SOH_TXN_CODE,to_char(SOH_DT,'DD/MM/YYYY'),SM_NAME from OT_SO_HEAD,om_salesman where SOH_SM_CODE=SM_CODE and SOH_COMP_CODE='" & CompanyCode & "' and SOH_TXN_CODE='SO' and SOH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "'  and SOH_SM_CODE='" & cmbSm.Text & "' and SOH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and SOH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by SOH_DOC_SRC_LOCN_CODE,SOH_NO"
            End If
            errLog.WriteToErrorLog("Salesorder Head Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0

            While count > 0
                If i = 0 Then
                    If currentpositon > 900 Then
                        CreatePage_WithHeader()
                    End If
                    lbl = New Label
                    With lbl
                        .Location = New Point(30, currentpositon + 5)
                        .Name = "lblSalesOrderHead"
                        .Size = New Size(120, 20)
                        currentpositon = currentpositon + 25
                        .Text = "Sales Order"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                        .BackColor = Color.LightGray
                        .BorderStyle = BorderStyle.FixedSingle
                    End With
                    Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
                End If

                Dim lblTrans As Label
                lblTrans = New Label
                Dim n = lblTrans_TxnCode.Count
                With lblTrans
                    .Location = New Point(30, currentpositon + 2)
                    .Name = "lblTrans_TxnCode" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans Code : " & ds.Tables("Table").Rows.Item(i).Item(2).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransNo.Count
                With lblTrans
                    .Location = New Point(170, currentpositon + 2)
                    .Name = "lblTrans_TransNo" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans No : " & ds.Tables("Table").Rows.Item(i).Item(1).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransDate.Count
                With lblTrans
                    .Location = New Point(310, currentpositon + 2)
                    .Name = "lblTrans_TransDate" & n.ToString
                    .Size = New Size(120, 20)
                    .Text = "Date : " & ds.Tables("Table").Rows.Item(i).Item(3).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransSalesman.Count
                With lblTrans
                    .Location = New Point(430, currentpositon + 2)
                    .Name = "lblTrans_TransSalesman" & n.ToString
                    .Size = New Size(310, 20)
                    .Text = "Salesman : " & ds.Tables("Table").Rows.Item(i).Item(4).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                currentpositon = currentpositon + 25

                FetchSalesOrderDetails(ds.Tables("Table").Rows.Item(i).Item(0).ToString)

                count = count - 1
                i = i + 1
            End While

            If ds.Tables("Table").Rows.Count > 0 Then
                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 2)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 3

                lbl = New Label
                With lbl
                    .Name = "lblSalesorderTransTotalHead"
                    .Text = salesinv_transnettotal.ToString("0.00")
                    .Location = New Point(460, currentpositon)
                    .Size = New Size(190, 20)
                    .BackColor = Color.LightGray
                    .Font = New Font("Times New Roman", 9, FontStyle.Bold)
                    .Text = "Transaction Net Total"
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Name = "lblSalesorderTransTotal"
                    .Text = salesorder_transnettotal.ToString("0.00")
                    .Location = New Point(650, currentpositon)
                    .Size = New Size(90, 20)
                    .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                currentpositon = currentpositon + 20

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 1)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 2

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4
            End If
            '////// SARTN Fetch ////////////////////////////

            If cmbSm.Text = "All" Then
                ' stQuery = "select  CSRH_SYS_ID,CSRH_NO,CSRH_TXN_CODE,to_char(CSRH_DT,'DD/MM/YYYY'),SM_NAME from OT_CUST_SALE_RET_HEAD,om_salesman where CSRH_SM_CODE=SM_CODE and  CSRH_COMP_CODE='" & CompanyCode & "' and CSRH_TXN_CODE='SARTN' and CSRH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and  CSRH_FLEX_19='" & cmbShift.Text & "' and CSRH_FLEX_20='" & cmbCounter.Text & "' and CSRH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and CSRH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select  CSRH_SYS_ID,CSRH_NO,CSRH_TXN_CODE,to_char(CSRH_DT,'DD/MM/YYYY'),SM_NAME from OT_CUST_SALE_RET_HEAD,om_salesman where CSRH_SM_CODE=SM_CODE and  CSRH_COMP_CODE='" & CompanyCode & "' and CSRH_TXN_CODE='SARTN' and CSRH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "'  and CSRH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and CSRH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by CSRH_DOC_SRC_LOCN_CODE,CSRH_NO"
            Else
                'stQuery = "select  CSRH_SYS_ID,CSRH_NO,CSRH_TXN_CODE,to_char(CSRH_DT,'DD/MM/YYYY'),SM_NAME from OT_CUST_SALE_RET_HEAD,om_salesman where CSRH_SM_CODE=SM_CODE and  CSRH_COMP_CODE='" & CompanyCode & "' and CSRH_TXN_CODE='SARTN' and CSRH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text & "' and  CSRH_FLEX_19='" & cmbShift.Text & "' and CSRH_FLEX_20='" & cmbCounter.Text & "' and CSRH_SM_CODE='" & cmbSm.Text & "' and CSRH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and CSRH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "'"
                stQuery = "select  CSRH_SYS_ID,CSRH_NO,CSRH_TXN_CODE,to_char(CSRH_DT,'DD/MM/YYYY'),SM_NAME from OT_CUST_SALE_RET_HEAD,om_salesman where CSRH_SM_CODE=SM_CODE and  CSRH_COMP_CODE='" & CompanyCode & "' and CSRH_TXN_CODE='SARTN' and CSRH_DOC_SRC_LOCN_CODE='" & cmbLocation.Text.Split(" - ")(0) & "' and CSRH_SM_CODE='" & cmbSm.Text & "' and CSRH_DT>='" & dtstDate.Value.ToString("dd-MMM-yy") & "' and CSRH_DT<='" & dtendDate.Value.ToString("dd-MMM-yy") & "' order by CSRH_DOC_SRC_LOCN_CODE,CSRH_NO"
            End If
            errLog.WriteToErrorLog("Sales Return Head Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0

            While count > 0
                If i = 0 Then
                    If currentpositon > 900 Then
                        CreatePage_WithHeader()
                    End If
                    lbl = New Label
                    With lbl
                        .Location = New Point(30, currentpositon + 5)
                        .Name = "lblSalesReturnHead"
                        .Size = New Size(120, 20)
                        currentpositon = currentpositon + 25
                        .Text = "Sales Return"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                        .BackColor = Color.LightGray
                        .BorderStyle = BorderStyle.FixedSingle
                    End With
                    Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
                End If

                Dim lblTrans As Label
                lblTrans = New Label
                Dim n = lblTrans_TxnCode.Count
                With lblTrans
                    .Location = New Point(30, currentpositon + 2)
                    .Name = "lblTrans_TxnCode" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans Code : " & ds.Tables("Table").Rows.Item(i).Item(2).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransNo.Count
                With lblTrans
                    .Location = New Point(170, currentpositon + 2)
                    .Name = "lblTrans_TransNo" & n.ToString
                    .Size = New Size(140, 20)
                    .Text = "Trans No : " & ds.Tables("Table").Rows.Item(i).Item(1).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransDate.Count
                With lblTrans
                    .Location = New Point(310, currentpositon + 2)
                    .Name = "lblTrans_TransDate" & n.ToString
                    .Size = New Size(120, 20)
                    .Text = "Date : " & ds.Tables("Table").Rows.Item(i).Item(3).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                lblTrans = New Label
                n = lblTrans_TransSalesman.Count
                With lblTrans
                    .Location = New Point(430, currentpositon + 2)
                    .Name = "lblTrans_TransSalesman" & n.ToString
                    .Size = New Size(310, 20)
                    .Text = "Salesman : " & ds.Tables("Table").Rows.Item(i).Item(4).ToString
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Font = New Font("Times New Roman", 8, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lblTrans)

                currentpositon = currentpositon + 25

                FetchSalesReturnDetails(ds.Tables("Table").Rows.Item(i).Item(0).ToString)

                count = count - 1
                i = i + 1
            End While

            If ds.Tables("Table").Rows.Count > 0 Then
                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 2)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 3

                lbl = New Label
                With lbl
                    .Name = "lblSalesreturnTransTotalHead"
                    .Text = salesinv_transnettotal.ToString("0.00")
                    .Location = New Point(460, currentpositon)
                    .Size = New Size(190, 20)
                    .BackColor = Color.LightGray
                    .Font = New Font("Times New Roman", 9, FontStyle.Bold)
                    .Text = "Transaction Net Total"
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Name = "lblSalesreturnTransTotal"
                    .Text = salesrreturn_transnettotal.ToString("0.00")
                    .Location = New Point(650, currentpositon)
                    .Size = New Size(90, 20)
                    .Font = New Font("Times New Roman", 10, FontStyle.Bold)
                    .TextAlign = ContentAlignment.MiddleRight
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblItemGrossTotal.Add(lbl)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

                currentpositon = currentpositon + 20

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(650, currentpositon + 1)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(90, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 2

                pnlLine = New Panel
                k = pnlLines.Count
                With pnlLine
                    .Location = New Point(30, currentpositon + 3)
                    .Name = "pnlLine" & k.ToString
                    .Size = New Size(710, 1)
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlLines.Add(pnlLine)
                Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

                currentpositon = currentpositon + 4
            End If
            SplitContainer1.Panel2.Select()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FetchInvoiceDetails(ByVal invhsysid As String)

        Dim stQuery As String = ""
        Dim ds As DataSet
        stQuery = "select INVI_ITEM_CODE,INVI_ITEM_DESC,nvl(INVI_QTY,0),nvl(INVI_PL_RATE,0),nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as Expamt,nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as Disamt,INVI_DISC_PERC from ot_invoice_item where INVI_INVH_SYS_ID=" & invhsysid
        errLog.WriteToErrorLog("DSTR sales invoices", stQuery, "")
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        Dim i As Integer = 0
        count = ds.Tables("Table").Rows.Count
        Dim lbl As Label
        Dim n As Integer
        If Not count > 0 Then
            lbl = New Label
            n = lblTrans_NullItems.Count
            With lbl
                .Name = "lblTrans_NullItems" & n.ToString
                .Text = "No Item Details Found"
                .Location = New Point(280, currentpositon)
                .Size = New Size(120, 20)
            End With
            lblTrans_NullItems.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
            currentpositon = currentpositon + 20
        End If
        currentpositon = currentpositon + 3
        Dim grosstotal As Double = 0
        While count > 0
            If currentpositon > 900 Then
                CreatePage_WithHeader()
            End If

            Dim grossval As Double = (((Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) + Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString)) - Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString))
            Dim discpercval As Double = 0
            discpercval = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString) / (Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) * 100, 2)

            lbl = New Label
            n = lblItemCode.Count
            With lbl
                .Name = "lblItemCode" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                .Location = New Point(30, currentpositon)
                .Size = New Size(120, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemCode.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDesc.Count
            With lbl
                .Name = "lblItemDesc" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(1).ToString
                .Location = New Point(150, currentpositon)
                .Size = New Size(200, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDesc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemQty.Count
            With lbl
                .Name = "lblItemQty" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(2).ToString
                .Location = New Point(350, currentpositon)
                .Size = New Size(40, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleCenter
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemQty.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemRate.Count
            With lbl
                .Name = "lblItemRate" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString), 2).ToString("0.00")
                .Location = New Point(390, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemRate.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemExpamt.Count
            With lbl
                .Name = "lblItemExpamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString), 2).ToString("0.00")
                .Location = New Point(460, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemExpamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisamt.Count
            With lbl
                .Name = "lblItemDisamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString), 2).ToString("0.00")
                .Location = New Point(520, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisperc.Count
            With lbl
                .Name = "lblItemDisperc" & n.ToString
                .Text = discpercval.ToString("0.00")
                .Location = New Point(590, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisperc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemGrossval.Count
            With lbl
                .Name = "lblItemGrossval" & n.ToString
                .Text = grossval.ToString("0.00")
                .Location = New Point(650, currentpositon)
                .Size = New Size(90, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemGrossval.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            grosstotal = grosstotal + grossval
            currentpositon = currentpositon + 20

            i = i + 1
            count = count - 1
        End While

        Dim pnlLine As Panel
        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        lbl = New Label
        n = lblItemGrossTotal.Count
        With lbl
            .Name = "lblItemGrossTotal" & n.ToString
            .Text = grosstotal.ToString("0.00")
            .Location = New Point(650, currentpositon)
            .Size = New Size(90, 20)
            .Font = New Font("Times New Roman", 9, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleRight
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblItemGrossTotal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        currentpositon = currentpositon + 20

        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        salesinv_transnettotal = salesinv_transnettotal + grosstotal

    End Sub

    Private Sub FetchSalesOrderDetails(ByVal sohsysid As String)

        Dim stQuery As String = ""
        Dim ds As DataSet
        stQuery = "select SOI_ITEM_CODE, SOI_ITEM_DESC,nvl(SOI_QTY,0),nvl(SOI_PL_RATE,0),nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as Expamt,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID=SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as Disamt,SOI_DISC_PERC from OT_SO_ITEM where SOI_SOH_SYS_ID=" & sohsysid
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        Dim i As Integer = 0
        count = ds.Tables("Table").Rows.Count
        Dim lbl As Label
        Dim n As Integer
        If Not count > 0 Then
            lbl = New Label
            n = lblTrans_NullItems.Count
            With lbl
                .Name = "lblTrans_NullItems" & n.ToString
                .Text = "No Item Details Found"
                .Location = New Point(280, currentpositon)
                .Size = New Size(120, 20)
            End With
            lblTrans_NullItems.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
            currentpositon = currentpositon + 20
        End If
        currentpositon = currentpositon + 3
        Dim grosstotal As Double = 0
        While count > 0
            If currentpositon > 900 Then
                CreatePage_WithHeader()
            End If

            Dim grossval As Double = (((Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) + Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString)) - Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString))

            Dim discpercval As Double = 0
            discpercval = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString) / (Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) * 100, 2)

            lbl = New Label
            n = lblItemCode.Count
            With lbl
                .Name = "lblItemCode" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                .Location = New Point(30, currentpositon)
                .Size = New Size(120, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemCode.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDesc.Count
            With lbl
                .Name = "lblItemDesc" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(1).ToString
                .Location = New Point(150, currentpositon)
                .Size = New Size(200, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDesc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemQty.Count
            With lbl
                .Name = "lblItemQty" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(2).ToString
                .Location = New Point(350, currentpositon)
                .Size = New Size(40, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleCenter
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemQty.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemRate.Count
            With lbl
                .Name = "lblItemRate" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString), 2).ToString("0.00")
                .Location = New Point(390, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemRate.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemExpamt.Count
            With lbl
                .Name = "lblItemExpamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString), 2).ToString("0.00")
                .Location = New Point(460, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemExpamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisamt.Count
            With lbl
                .Name = "lblItemDisamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString), 2).ToString("0.00")
                .Location = New Point(520, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisperc.Count
            With lbl
                .Name = "lblItemDisperc" & n.ToString
                .Text = discpercval.ToString("0.00")
                .Location = New Point(590, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisperc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemGrossval.Count
            With lbl
                .Name = "lblItemGrossval" & n.ToString
                .Text = grossval.ToString("0.00")
                .Location = New Point(650, currentpositon)
                .Size = New Size(90, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemGrossval.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            grosstotal = grosstotal + grossval
            currentpositon = currentpositon + 20

            i = i + 1
            count = count - 1
        End While

        Dim pnlLine As Panel
        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        lbl = New Label
        n = lblItemGrossTotal.Count
        With lbl
            .Name = "lblItemGrossTotal" & n.ToString
            .Text = grosstotal.ToString("0.00")
            .Location = New Point(650, currentpositon)
            .Size = New Size(90, 20)
            .Font = New Font("Times New Roman", 9, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleRight
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblItemGrossTotal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        currentpositon = currentpositon + 20

        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        salesorder_transnettotal = salesorder_transnettotal + grosstotal

    End Sub

    Private Sub FetchSalesReturnDetails(ByVal csrhsysid As String)

        Dim stQuery As String = ""
        Dim ds As DataSet
        stQuery = "select CSRI_ITEM_CODE, CSRI_ITEM_DESC,nvl(CSRI_QTY,0), nvl(CSRI_RATE,0),nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as Expamt,nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID=CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as Disamt,CSRI_DISC_PERC from OT_CUST_SALE_RET_ITEM where CSRI_CSRH_SYS_ID=" & csrhsysid
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        Dim i As Integer = 0
        count = ds.Tables("Table").Rows.Count
        Dim lbl As Label
        Dim n As Integer
        If Not count > 0 Then
            lbl = New Label
            n = lblTrans_NullItems.Count
            With lbl
                .Name = "lblTrans_NullItems" & n.ToString
                .Text = "No Item Details Found"
                .Location = New Point(280, currentpositon)
                .Size = New Size(120, 20)
            End With
            lblTrans_NullItems.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)
            currentpositon = currentpositon + 20
        End If
        currentpositon = currentpositon + 3
        Dim grosstotal As Double = 0
        While count > 0
            If currentpositon > 900 Then
                CreatePage_WithHeader()
            End If

            Dim grossval As Double = (((Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) + Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString)) - Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString))
            Dim discpercval As Double = 0
            discpercval = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString) / (Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString) * Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)) * 100, 2)

            lbl = New Label
            n = lblItemCode.Count
            With lbl
                .Name = "lblItemCode" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                .Location = New Point(30, currentpositon)
                .Size = New Size(120, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemCode.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDesc.Count
            With lbl
                .Name = "lblItemDesc" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(1).ToString
                .Location = New Point(150, currentpositon)
                .Size = New Size(200, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDesc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemQty.Count
            With lbl
                .Name = "lblItemQty" & n.ToString
                .Text = ds.Tables("Table").Rows.Item(i).Item(2).ToString
                .Location = New Point(350, currentpositon)
                .Size = New Size(40, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleCenter
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemQty.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemRate.Count
            With lbl
                .Name = "lblItemRate" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString), 2).ToString("0.00")
                .Location = New Point(390, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemRate.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemExpamt.Count
            With lbl
                .Name = "lblItemExpamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(4).ToString), 2).ToString("0.00")
                .Location = New Point(460, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemExpamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisamt.Count
            With lbl
                .Name = "lblItemDisamt" & n.ToString
                .Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(5).ToString), 2).ToString("0.00")
                .Location = New Point(520, currentpositon)
                .Size = New Size(70, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisamt.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemDisperc.Count
            With lbl
                .Name = "lblItemDisperc" & n.ToString
                .Text = discpercval.ToString("0.00")
                .Location = New Point(590, currentpositon)
                .Size = New Size(60, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemDisperc.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            lbl = New Label
            n = lblItemGrossval.Count
            With lbl
                .Name = "lblItemGrossval" & n.ToString
                .Text = grossval.ToString("0.00")
                .Location = New Point(650, currentpositon)
                .Size = New Size(90, 20)
                .Font = New Font("Times New Roman", 8, FontStyle.Regular)
                .TextAlign = ContentAlignment.MiddleRight
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblItemGrossval.Add(lbl)
            Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

            grosstotal = grosstotal + grossval
            currentpositon = currentpositon + 20

            i = i + 1
            count = count - 1
        End While

        Dim pnlLine As Panel
        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        lbl = New Label
        n = lblItemGrossTotal.Count
        With lbl
            .Name = "lblItemGrossTotal" & n.ToString
            .Text = grosstotal.ToString("0.00")
            .Location = New Point(650, currentpositon)
            .Size = New Size(90, 20)
            .Font = New Font("Times New Roman", 9, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleRight
            '.BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblItemGrossTotal.Add(lbl)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(lbl)

        currentpositon = currentpositon + 20

        pnlLine = New Panel
        n = pnlLines.Count
        With pnlLine
            .Location = New Point(650, currentpositon + 1)
            .Name = "pnlLine" & n.ToString
            .Size = New Size(90, 1)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.pnlLines.Add(pnlLine)
        Me.Controls.Find(currentpage, True)(0).Controls.Add(pnlLine)

        currentpositon = currentpositon + 2

        salesrreturn_transnettotal = salesrreturn_transnettotal + grosstotal

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

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub cmbLocation_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedValueChanged
        'LoadCounter()
        'LoadShift()
        LoadSM()
    End Sub

End Class