Imports System.Drawing
Imports Microsoft.Office.Interop

Public Class frmGVRep
    Inherits Form
    Dim ds As New DataSet
    Dim db As New DBConnection
    Dim dt As DataTable
    Dim i As Integer = 0
    Dim Count As Integer
    Dim condition As String
    Dim stDate As String
    Dim index As Integer
    Dim stQuery As String
    Dim Query As String
    Dim StatusVal As String
    Dim edDate As String
    Dim strArrLoc As Array
    Dim strArrSM As Array
    Dim Shiftval As String
    Dim Counterval As String
    Private WithEvents StartWorker As System.ComponentModel.BackgroundWorker
    Private Sub frmStatusinfoRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        SetResolution()
        dtStDate.Format = DateTimePickerFormat.Custom
        dtStDate.CustomFormat = "dd-MMM-yyyy"
        dtendDate.Format = DateTimePickerFormat.Custom
        dtendDate.CustomFormat = "dd-MMM-yyyy"
        listGV.View = View.Details
        listGV.GridLines = True
        listGV.FullRowSelect = False
        listGV.Columns.Add("S.No", 100, HorizontalAlignment.Center)
        listGV.Columns.Add("GV Code", 270, HorizontalAlignment.Center)
        listGV.Columns.Add("GV No", 270, HorizontalAlignment.Center)
        listGV.Columns.Add("Amount", 270, HorizontalAlignment.Center)
        repsalesLocationfrom()
        cmbLocation.Text = Location_Code
        LoadSM()
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        'LoadShift()
        'LoadCounter()
        'repsalesLocationto()
        'lblusername.Text = User_Name
    End Sub
    'Public Sub New()
    '    InitializeComponent()
    '    MenuStrip7.Renderer = New Renderer()
    'End Sub
    Public Sub repsalesLocationfrom()
        Try
            ds.Dispose()
            Query = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from crm_om_location order by locdisplay"
            ds = db.SelectFromTableODBC(Query)
            If ds.Tables("Table").Rows.Count <> 0 Then
                cmbLocation.DataSource = ds.Tables("Table")
                cmbLocation.DisplayMember = "locdisplay"
                cmbLocation.ValueMember = "loccode"
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
        'ds = comfun.GetLocation()
        'Me.cmbLocation.DataSource = ds.Tables("Table")
        'Me.cmbLocation.DisplayMember = "locdisplay"
        'Me.cmbLocation.ValueMember = "loccode"
    End Sub
    Private Sub LoadSM()
        Try
            ' If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " And cmbCounter.Text <> "System.Data.DataRowView" And cmbCounter.Text <> " " Then
            If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " Then
                ds.Dispose()
                strArrLoc = cmbLocation.Text.Split("-")
                'Query = "SELECT SM_CODE || '-' || SM_NAME as salemancode,SM_CODE FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '001' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & strArrLoc(0) & "' AND SMC_COUNT_CODE = '" & cmbCounter.Text & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMS_CODE FROM OM_POS_SALESMAN_SHIFT WHERE SMS_LOCN_CODE = '" & strArrLoc(0) & "' AND SMS_SHIFT_CODE = '" & cmbShift.Text & "' AND SMS_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
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
    '            Dim Query As String = "select distinct SMC_COUNT_CODE from OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & strArrLoc(0) & "' AND SMC_FRZ_FLAG_NUM=2"
    '            ds = db.SelectFromTableODBC(Query)
    '            cmbCounter.Items.Clear()
    '            cmbCounter.Text = ""
    '            If ds.Tables("Table").Rows.Count > 0 Then
    '                cmbCounter.Text = ds.Tables("Table").Rows(0).Item(0).ToString
    '                For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
    '                    cmbCounter.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    'Public Sub LoadShift()
    '    Try
    '        If cmbLocation.Text <> "System.Data.DataRowView" And cmbLocation.Text <> " " Then
    '            ds.Dispose()
    '            Dim Query As String = "SELECT distinct SMS_SHIFT_CODE FROM OM_POS_SALESMAN_SHIFT WHERE SMS_LOCN_CODE = '" & strArrLoc(0) & "' and SMS_FRZ_FLAG_NUM='2'"
    '            ds = db.SelectFromTableODBC(Query)
    '            cmbShift.Items.Clear()
    '            cmbShift.Text = ""
    '            If ds.Tables("Table").Rows.Count > 0 Then
    '                cmbShift.Text = ds.Tables("Table").Rows(0).Item(0).ToString
    '                For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
    '                    cmbShift.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub
    'Public Sub repsalesLocationto()
    '    ds = comfun.GetLocation()
    '    Me.cbLocationto.DataSource = ds.Tables("Table")
    '    Me.cbLocationto.DisplayMember = "locdisplay"
    '    Me.cbLocationto.ValueMember = "loccode"
    'End Sub

    Private Sub getdatafunction()
        Try
            'Dim stDateval As String = dtstDate.Value.ToString("dd-MMM-yyyy")
            'Dim endDateval As String = dtendDate.Value.ToString("dd-MMM-yyyy")
            If cmbSm.Text = "All" Then
                'stQuery = "SELECT  PINVGV_GVNO,PINVGV_GVCODE,PINVGV_GV_VALUE FROM OT_POS_INVOICE_GV,OT_INVOICE_HEAD WHERE  PINVGV_INVH_SYS_ID=INVH_SYS_ID AND INVH_LOCN_CODE = '013' AND PINVGV_CR_DT >= '01-Aug-2012' AND PINVGV_CR_DT <= '22-Aug-2013' AND INVH_FLEX_20 = '013' AND INVH_FLEX_19 = '8 AM TO 8 AM' AND INVH_SM_CODE = '2011'"
                stQuery = "SELECT  PINVGV_GVNO,PINVGV_GVCODE,PINVGV_GV_VALUE FROM OT_POS_INVOICE_GV,OT_INVOICE_HEAD WHERE PINVGV_INVH_SYS_ID=INVH_SYS_ID AND  INVH_LOCN_CODE = '" & strArrLoc(0) & "'  AND PINVGV_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd/MM/yyyy") + " 000000','dd/MM/yyyy hh24miss') AND PINVGV_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd/MM/yyyy") + " 235959','dd/MM/yyyy hh24miss') "
            Else
                stQuery = "SELECT  PINVGV_GVNO,PINVGV_GVCODE,PINVGV_GV_VALUE FROM OT_POS_INVOICE_GV,OT_INVOICE_HEAD WHERE PINVGV_INVH_SYS_ID=INVH_SYS_ID AND  INVH_LOCN_CODE = '" & strArrLoc(0) & "'  AND PINVGV_CR_DT >= TO_DATE('" + dtstDate.Value.ToString("dd/MM/yyyy") + " 000000','dd/MM/yyyy hh24miss') AND PINVGV_CR_DT <= TO_DATE('" + dtendDate.Value.ToString("dd/MM/yyyy") + " 235959','dd/MM/yyyy hh24miss') AND INVH_SM_CODE = '" & strArrSM(0) & "'"
            End If
            errLog.WriteToErrorLog("GV Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            dt = ds.Tables("Table")
            Count = ds.Tables("Table").Rows.Count
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub LstViewSales()
        Try
            Dim j As Integer
            If Count <> 0 Then
                For j = 0 To ds.Tables("Table").Rows.Count - 1
                    listGV.Items.Add(j + 1)
                    listGV.Items(j).SubItems.Add(dt.Rows(j).Item(0).ToString)
                    listGV.Items(j).SubItems.Add(dt.Rows(j).Item(1).ToString)
                    listGV.Items(j).SubItems.Add(dt.Rows(j).Item(2).ToString)
                Next
                lblNoList.Hide()
            Else
                lblNoList.Image = Nothing
                lblNoList.Text = "No Data Available"
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub StartWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles StartWorker.DoWork
        Try
            getdatafunction()
            StartWorker.ReportProgress(100)
            Threading.Thread.Sleep(100)


        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub StartWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles StartWorker.ProgressChanged
        Try

            LstViewSales()


        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub StartWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles StartWorker.RunWorkerCompleted

    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            'listGV.Items.Clear()
            'lblNoList.Show()
            'lblNoList.Image = My.Resources.loading
            'lblNoList.Text = vbCrLf & vbCrLf & vbCrLf & "Loading..."
            'stDate = dtstDate.Value.ToString("dd-MMM-yyyy")
            'edDate = dtEddate.Value.ToString("dd-MMM-yyyy")
            'Dim strArr As Array
            'strArr = cbLocationfrom.Text.Split("-")
            'Dim strArrto As Array
            'strArrto = cbLocationto.Text.Split("-")
            'If cbLocationfrom.Text <> "" And cbLocationto.Text = "" Then
            '    condition = "and INVH_LOCN_CODE >= '" + strArr(0) + "'"
            'ElseIf cbLocationfrom.Text = "" And cbLocationto.Text <> "" Then
            '    condition = "and INVH_LOCN_CODE <= '" + strArrto(0) + "'"
            'ElseIf cbLocationfrom.Text <> "" And cbLocationto.Text <> "" Then
            '    condition = "and INVH_LOCN_CODE >= '" + strArr(0) + "' and INVH_LOCN_CODE <= '" + strArrto(0) + "'"
            'End If
            ''condition = "and INVH_LOCN_CODE >= '" + cbLocationfrom.SelectedValue.ToString + "' and INVH_LOCN_CODE <= '" + cbLocationto.SelectedValue.ToString + "'"
            'StatusVal = cmbStatusRep.Text
            'StartWorker = New System.ComponentModel.BackgroundWorker
            'StartWorker.WorkerReportsProgress = True
            'StartWorker.WorkerSupportsCancellation = True
            'StartWorker.RunWorkerAsync()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    'Private Sub SalesAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmSalesAdhoc.MdiParent = frmHome
    '    frmSalesAdhoc.Show()
    'End Sub
    'Private Sub Top100CustomersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmRFMtop100Customers.MdiParent = frmHome
    '    frmRFMtop100Customers.Show()
    'End Sub
    'Private Sub ProductSalesAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmBrandSales.MdiParent = frmHome
    '    frmBrandSales.Show()
    'End Sub

    'Private Sub BenchmarkReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmFastProducts.MdiParent = frmHome
    '    frmFastProducts.Show()
    'End Sub
    'Private Sub SalesAnalysisToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesAnalysisToolStripMenuItem.Click
    '    Me.Close()
    '    frmSalesAdhoc.MdiParent = frmHome
    '    frmSalesAdhoc.Show()
    'End Sub

    'Private Sub RFMSettingsToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmRFMSettings.MdiParent = frmHome
    '    frmRFMSettings.Show()
    'End Sub



    'Private Sub PotetialsSAlesForecastToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmDiscountReport.MdiParent = frmHome
    '    frmDiscountReport.Show()
    'End Sub




    'Private Sub IncomingPotentialsAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmPeakHrSales.MdiParent = frmHome
    '    frmPeakHrSales.Show()
    'End Sub


    Private Sub listsalesrep_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles listGV.DrawColumnHeader
        'comfun.changeListHeaderColor(e)
        Dim strFormat As New StringFormat()
        strFormat.Alignment = StringAlignment.Center
        strFormat.LineAlignment = StringAlignment.Center
        e.DrawBackground()
        e.Graphics.FillRectangle(Brushes.DarkSlateBlue, e.Bounds)
        e.Graphics.DrawRectangle(Pens.GhostWhite, e.Bounds)
        Dim headerFont As New Font("Arial", 8, FontStyle.Bold)
        e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.White, e.Bounds, strFormat)
    End Sub


    Private Sub listsalesrep_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles listGV.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub listsalesrep_DrawSubItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles listGV.DrawSubItem
        e.DrawDefault = True
    End Sub

    'Private Sub StockStatusReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmStockReport.MdiParent = frmHome
    '    frmStockReport.Show()
    'End Sub


    'Private Sub SalesmanPerformanceAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmSalesmanPerform.MdiParent = frmHome
    '    frmSalesmanPerform.Show()
    'End Sub

    'Private Sub PurchaseReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReportToolStripMenuItem.Click

    '    Me.Close()
    '    frmPurchaseReport.MdiParent = frmHome
    '    frmPurchaseReport.Show()
    'End Sub

    Private Sub butAddExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAddExcel.Click
        Try
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim i As Integer

            xlApp = New Excel.ApplicationClass
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")
            Dim col As Integer = 1

            For j As Integer = 0 To listGV.Columns.Count - 1
                xlWorkSheet.Cells(1, col) = listGV.Columns(j).Text.ToString
                col = col + 1
            Next

            For i = 0 To listGV.Items.Count - 1
                xlWorkSheet.Cells(i + 2, 1) = listGV.Items.Item(i).Text.ToString
                xlWorkSheet.Cells(i + 2, 2) = listGV.Items.Item(i).SubItems(1).Text
                xlWorkSheet.Cells(i + 2, 3) = listGV.Items.Item(i).SubItems(2).Text
                xlWorkSheet.Cells(i + 2, 4) = listGV.Items.Item(i).SubItems(3).Text
            Next

            Dim dlg As New SaveFileDialog
            dlg.Filter = "Excel Files (*.xls)|*.xls"
            dlg.FilterIndex = 1
            dlg.InitialDirectory = My.Application.Info.DirectoryPath & "\EXCEL\\EICHER\REPORT\"
            dlg.FileName = "GV Report"
            Dim ExcelFile As String = ""


            If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                ExcelFile = dlg.FileName
                xlWorkSheet.SaveAs(ExcelFile)
            End If
            xlWorkBook.Close()
            xlApp.Quit()
            ReleaseComObject(xlWorkSheet)
            ReleaseComObject(xlWorkBook)
            ReleaseComObject(xlApp)

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        Finally
            GC.Collect()
        End Try
    End Sub
    Public Sub ReleaseComObject(ByRef Reference As Object)
        Try
            Do Until _
             System.Runtime.InteropServices.Marshal.ReleaseComObject(Reference) <= 0
            Loop
        Catch
        Finally
            Reference = Nothing
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





            For Each ctl As Control In pnlCampaignHead.Controls
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






            For Each ctl As Control In Grpbox_GVoptions.Controls
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

            ' do panels separate also – separate for/next for each ‘panel by name



            

            ' if you are not maximizing your screen afterwards, then include this code
            Me.Top = (prvheight / 2) - (Me.Height / 2)
            Me.Left = (prvWidth / 2) - (Me.Width / 2)
        End If
    End Sub



    'Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
    '    ' LoadCounter()
    '    'LoadShift()
    '    LoadSM()
    'End Sub

    'Private Sub cmbShift_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    LoadSM()
    'End Sub

    Private Sub btView_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click
        Try

            listGV.Items.Clear()
            lblNoList.Show()
            'lblNoList.Image = My.Resources.loading
            lblNoList.Text = vbCrLf & vbCrLf & vbCrLf & "Loading..."
            stDate = dtstDate.Value.ToString("dd/MM/yyyy")
            edDate = dtendDate.Value.ToString("dd/MM/yyyy")
            strArrLoc = cmbLocation.Text.Split("-")
            Dim strArr As Array
            strArr = cmbLocation.Text.Split("-")
            strArrSM = cmbSm.Text.Split("-")
            'Shiftval = cmbShift.Text
            'Counterval = cmbCounter.Text
            StartWorker = New System.ComponentModel.BackgroundWorker
            StartWorker.WorkerReportsProgress = True
            StartWorker.WorkerSupportsCancellation = True
            StartWorker.RunWorkerAsync()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub pnlCampaignHead_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlCampaignHead.Paint

    End Sub

End Class