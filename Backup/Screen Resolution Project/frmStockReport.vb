Imports Microsoft.Office.Interop

Public Class frmStockReport
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim db As New DBConnection
    Dim Count As Integer
    Dim Newdate As String
    Dim stQuery As String
    Dim itemlist, conditionst As String
    Dim groupval As String = ""
    Dim test As Integer
    Dim strArrLocfrom As Array
    Dim strArrLocto As Array
    Private WithEvents TestWorker As System.ComponentModel.BackgroundWorker

    Dim Main_Group As New List(Of String)
    Dim Sub_Group As New List(Of String)
    Dim MySource_MainGroup As New AutoCompleteStringCollection()
    Dim MySource_SubGroup As New AutoCompleteStringCollection()

    Public Sub New()

        InitializeComponent()

        'MenuStrip7.Renderer = New Renderer()

    End Sub

    Private Sub frmStockReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

        'lblusername.Text = User_Name
        repsalesLocationfrom()
        cbLocationfrom.Text = Location_Code
        cbLocationfrom.Enabled = True
        'repsalesLocationto()
        MainGroup()
        listProduct.View = View.Details
        listProduct.OwnerDraw = True
        listProduct.GridLines = True
        listProduct.FullRowSelect = True
        listProduct.Columns.Add("S.No", 47)
        listProduct.Columns.Add("Item Code", 100)
        listProduct.Columns.Add("Item_Description", 130)
        listProduct.Columns.Add("Item_Bar_Code", 100)
        listProduct.Columns.Add("Location_Name", 100)
        listProduct.Columns.Add("UOM_Code", 100)
        listProduct.Columns.Add("GRADECODE_1", 100)
        listProduct.Columns.Add("GRADECODE_2", 100)
        listProduct.Columns.Add("PL_CODE", 80)
        listProduct.Columns.Add("PRICE1", 80)
        listProduct.Columns.Add("PRICE2", 80)
        listProduct.Columns.Add("CONF_STOCK_QTY", 110)
        listProduct.Columns.Add("UNCONF_REC_QTY", 110)
        listProduct.Columns.Add("UNCONF_ISS_QTY", 110)
        listProduct.Columns.Add("HOLD_QTY", 80)
        listProduct.Columns.Add("REJECTED_QTY", 100)
        listProduct.Columns.Add("OVERRESERVE_QTY", 120)
        listProduct.Columns.Add("PICK_QTY", 80)
        listProduct.Columns.Add("PACK_QTY", 80)
        listProduct.Columns.Add("AVAIL_STOCK_QTY", 110)
        listProduct.Columns.Add("RESERVE_QTY", 100)
        listProduct.Columns.Add("FREE_STOCK_QTY", 120)
        listProduct.Columns.Add("ITEM_ANLY_CO1", 120)
        listProduct.Columns.Add("ITEM_ANLY_CO2", 120)

    End Sub
    Public Sub repsalesLocationfrom()
        'ds = comfun.GetLocation()
        Try
            Dim Query As String
            ds.Dispose()
            Query = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from crm_om_location order by locdisplay"
            ds = db.SelectFromTableODBC(Query)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            If count > 0 Then
                cbLocationfrom.Items.Add("All")
                While count > 0
                    cbLocationfrom.Items.Add(ds.Tables("Table").Rows.Item(i).Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While
            End If
            'If ds.Tables("Table").Rows.Count <> 0 Then
            '    cbLocationfrom.DataSource = ds.Tables("Table")
            '    cbLocationfrom.DisplayMember = "locdisplay"
            '    cbLocationfrom.ValueMember = "loccode"
            'End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Public Sub MainGroup()
        'ds = comfun.GetMainGroup()
        Dim ds As New DataSet
        Dim db As New DBConnection
        Dim stQuery As String
        stQuery = "SELECT AD_CODE AS CD_ITEM_ANLY_CODE_01,AD_NAME AS CD_ITEM_ANLY_NAME_01,AD_SHORT_NAME  AS CD_ITEM_ANAY_SH_NAME_01 FROM OM_ANALYSIS_DETAIL WHERE AD_ANLY_NO=1 AND AD_ANLY_TYPE='ITEM'"
        ds = db.SelectFromTableODBC(stQuery)
        'Count = ds.Tables("Table").Rows.Count
        'If ds.Tables("Table").Rows.Count <> 0 Then
        '    cmbmaingrp.DataSource = ds.Tables("Table")
        '    cmbmaingrp.DisplayMember = "CD_ITEM_ANLY_CODE_01"
        '    cmbmaingrp.ValueMember = "CD_ITEM_ANLY_CODE_01"
        'End If
        For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
            cmbmaingrp.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
            'Main_Group.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
        Next
        'MySource_MainGroup.AddRange(Main_Group.ToArray)
        'txtmaingrp.AutoCompleteCustomSource = MySource_MainGroup
        'txtmaingrp.AutoCompleteMode = AutoCompleteMode.Suggest
        'txtmaingrp.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    'Public Sub repsalesLocationto()
    '    'ds = comfun.GetLocation()
    '    Me.cbLocationto.DataSource = ds.Tables("Table")
    '    Me.cbLocationto.DisplayMember = "locdisplay"
    '    Me.cbLocationto.ValueMember = "loccode"
    'End Sub
    Public Sub listProduct_DrawColumnHeader(ByVal sender As Object, ByVal e As DrawListViewColumnHeaderEventArgs) Handles listProduct.DrawColumnHeader

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

    Private Sub listproduct_DrawItem(ByVal sender As Object, ByVal e As DrawListViewItemEventArgs) Handles listProduct.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub listproduct_DrawSubItem(ByVal sender As Object, _
    ByVal e As DrawListViewSubItemEventArgs) Handles listProduct.DrawSubItem
        e.DrawDefault = True
    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click

        itemlist = ""
        conditionst = ""
        groupval = ""
        listProduct.Items.Clear()
        lblNoList.Show()
        'lblNoList.Image = My.Resources.loading
        lblNoList.Text = vbCrLf & vbCrLf & vbCrLf & "Loading..."
        butAddExcel.Enabled = False
        strArrLocfrom = cbLocationfrom.Text.Split("-")
        strArrLocto = cbLocationfrom.Text.Split("-")
        If cbLocationfrom.Text = "" Then
            MsgBox("Please select a location")
            Exit Sub
        Else
            If Not cbLocationfrom.Text = "All" Then
                conditionst = " and lcs_locn_code = '" + strArrLocto(0) + "'"
            End If
        End If

        'If cbLocationfrom.Text <> "" And cbLocationfrom.Text = "" Then
        '    conditionst = " and lcs_locn_code = '" + strArrLocfrom(0) + "'"
        'ElseIf cbLocationfrom.Text = "" And cbLocationfrom.Text <> "" Then
        '    conditionst = " and lcs_locn_code = '" + strArrLocto(0) + "'"
        'ElseIf cbLocationfrom.Text <> "" And cbLocationfrom.Text <> "" Then
        '    conditionst = " and lcs_locn_code >= '" + strArrLocfrom(0) + "'  and lcs_locn_code <= '" + strArrLocto(0) + "'"
        'End If

        If cmbitemfrom.Text <> "" And cmbitemto.Text = "" Then
            itemlist = " and om_item.item_code like '" + cmbitemfrom.Text + "%'"
        ElseIf cmbitemfrom.Text = "" And cmbitemto.Text <> "" Then
            itemlist = " and om_item.item_code like '" + cmbitemto.Text + "%'"
        ElseIf cmbitemfrom.Text.Contains("%") Or cmbitemto.Text.Contains("%") Then
            itemlist = " and om_item.item_code >= '" + cmbitemfrom.Text.Replace("%", "") + "'  and om_item.item_code <= '" + cmbitemto.Text.Replace("%", "zzzzzzz") + "'"
        ElseIf cmbitemfrom.Text <> "" And cmbitemto.Text <> "" Then
            itemlist = " and om_item.item_code >= '" + cmbitemfrom.Text + "'  and om_item.item_code <= '" + cmbitemto.Text + "'"
        End If

        If cmbmaingrp.Text <> "" And cmbsubgrp.Text <> "" Then
            groupval = "and LCS_ITEM_CODE in (select ITEM_CODE from OM_ITEM where ITEM_CODE is not null and  ITEM_ANLY_CODE_01 like '" + cmbmaingrp.Text + "%' and ITEM_ANLY_CODE_02 like '" + cmbsubgrp.Text + "%')"
        ElseIf cmbmaingrp.Text <> "" And cmbsubgrp.Text = "" Then
            groupval = "and LCS_ITEM_CODE in (select ITEM_CODE from OM_ITEM where ITEM_CODE is not null and  ITEM_ANLY_CODE_01 like '" + cmbmaingrp.Text + "%')"
        ElseIf cmbmaingrp.Text = "" And cmbsubgrp.Text <> "" Then
            groupval = "and LCS_ITEM_CODE in (select ITEM_CODE from OM_ITEM where ITEM_CODE is not null and  ITEM_ANLY_CODE_02 like '" + cmbsubgrp.Text + "%')"
        End If


        TestWorker = New System.ComponentModel.BackgroundWorker
        TestWorker.WorkerReportsProgress = True
        TestWorker.WorkerSupportsCancellation = True
        TestWorker.RunWorkerAsync()

    End Sub

    Private Sub TestWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles TestWorker.DoWork
        Try
            stocklist()
            TestWorker.ReportProgress(100)
            Threading.Thread.Sleep(100)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub TestWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles TestWorker.ProgressChanged
        Try

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub TestWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles TestWorker.RunWorkerCompleted
        datalist()
    End Sub

    Public Sub stocklist()
        Try
            stQuery = "SELECT om_item.item_code Item_Code, om_item.item_name Item_Description, A.item_bar_code Item_Bar_Code,locn_name Location_Name, om_item.item_uom_code UOM_Code, lcs_grade_code_1 GradeCode_1, lcs_grade_code_2  GradeCode_2, A.item_pli_pl_code  AS PL_Code, A.item_price_type_1 AS PRICE1, A.item_price_type_2  AS PRICE2, lcs_stk_qty_bu Conf_Stock_Qty, lcs_rcvd_qty_bu Unconf_Rec_Qty, lcs_issd_qty_bu UnConf_Iss_Qty, lcs_hold_qty_bu Hold_Qty, lcs_reject_qty_bu Rejected_Qty, lcs_overres_qty_bu OverReserve_Qty, lcs_pick_qty_bu  Pick_Qty,  lcs_pack_qty_bu  Pack_Qty, ( ( lcs_stk_qty_bu + lcs_rcvd_qty_bu ) - ( lcs_issd_qty_bu + lcs_hold_qty_bu + lcs_reject_qty_bu +  lcs_overres_qty_bu + lcs_pick_qty_bu + lcs_pack_qty_bu)) AS Avail_Stock_Qty, lcs_resv_qty_bu Reserve_Qty,((lcs_stk_qty_bu + lcs_rcvd_qty_bu ) - (lcs_issd_qty_bu + lcs_hold_qty_bu + lcs_reject_qty_bu + lcs_overres_qty_bu + lcs_pick_qty_bu + lcs_pack_qty_bu ) - lcs_resv_qty_bu ) Free_Stock_Qty,  om_item.ITEM_ANLY_CODE_01, om_item.ITEM_ANLY_CODE_02 FROM   os_locn_curr_stk, om_item, om_pos_item A,  crm_om_location WHERE  om_item.item_code = A.item_code  AND om_item.item_code = lcs_item_code  AND om_item.item_frz_flag_num = 2  AND lcs_comp_code = '001'  AND lcs_locn_code = locn_code  " & conditionst & "  AND item_pli_pl_code = 'OGENPL' " + itemlist + groupval + " GROUP  BY om_item.item_code,  om_item.item_name, A.item_bar_code,  locn_name,  om_item.item_uom_code,  lcs_grade_code_1,  lcs_grade_code_2,  A.item_pli_pl_code,  A.item_price_type_1,  A.item_price_type_2,  lcs_stk_qty_bu,  lcs_rcvd_qty_bu,  lcs_issd_qty_bu,  lcs_hold_qty_bu,  lcs_reject_qty_bu,  lcs_overres_qty_bu,  lcs_pick_qty_bu,  lcs_pack_qty_bu,  lcs_resv_qty_bu,  om_item.item_anly_code_01,  om_item.item_anly_code_02 HAVING SUM(( ( lcs_stk_qty_bu + lcs_rcvd_qty_bu ) -  ( lcs_issd_qty_bu + lcs_hold_qty_bu  + lcs_reject_qty_bu +  lcs_overres_qty_bu  + lcs_pick_qty_bu +  lcs_pack_qty_bu )  - lcs_resv_qty_bu )) > 0 ORDER  BY locn_name"
            errLog.WriteToErrorLog("Stock Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            dt = ds.Tables("Table")
            Count = ds.Tables("Table").Rows.Count
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Public Sub datalist()
        If Count <> 0 Then
            Dim i As Integer
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                listProduct.Items.Add(i + 1)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(0).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(1).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(2).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(3).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(4).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(5).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(6).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(7).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(8).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(9).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(10).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(11).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(12).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(13).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(14).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(15).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(16).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(17).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(18).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(19).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(20).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(21).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(22).ToString)
            Next
            lblNoList.Hide()
            butAddExcel.Enabled = True
        Else
            lblNoList.Text = "No Records Found"
            lblNoList.Image = Nothing
        End If
    End Sub


    Private Sub butAddExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAddExcel.Click
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

            For j As Integer = 0 To listProduct.Columns.Count - 1
                xlWorkSheet.Cells(1, col) = listProduct.Columns(j).Text.ToString
                col = col + 1
            Next

            For i = 0 To listProduct.Items.Count - 1
                xlWorkSheet.Cells(i + 2, 1) = listProduct.Items.Item(i).Text.ToString
                xlWorkSheet.Cells(i + 2, 2) = listProduct.Items.Item(i).SubItems(1).Text
                xlWorkSheet.Cells(i + 2, 3) = listProduct.Items.Item(i).SubItems(2).Text
                xlWorkSheet.Cells(i + 2, 4) = listProduct.Items.Item(i).SubItems(3).Text
                xlWorkSheet.Cells(i + 2, 5) = listProduct.Items.Item(i).SubItems(4).Text
                xlWorkSheet.Cells(i + 2, 6) = listProduct.Items.Item(i).SubItems(5).Text
                xlWorkSheet.Cells(i + 2, 7) = listProduct.Items.Item(i).SubItems(6).Text
                xlWorkSheet.Cells(i + 2, 8) = listProduct.Items.Item(i).SubItems(7).Text
                xlWorkSheet.Cells(i + 2, 9) = listProduct.Items.Item(i).SubItems(8).Text
                xlWorkSheet.Cells(i + 2, 10) = listProduct.Items.Item(i).SubItems(9).Text
                xlWorkSheet.Cells(i + 2, 11) = listProduct.Items.Item(i).SubItems(10).Text
                xlWorkSheet.Cells(i + 2, 12) = listProduct.Items.Item(i).SubItems(11).Text
                xlWorkSheet.Cells(i + 2, 13) = listProduct.Items.Item(i).SubItems(12).Text
                xlWorkSheet.Cells(i + 2, 14) = listProduct.Items.Item(i).SubItems(13).Text
                xlWorkSheet.Cells(i + 2, 15) = listProduct.Items.Item(i).SubItems(14).Text
                xlWorkSheet.Cells(i + 2, 16) = listProduct.Items.Item(i).SubItems(15).Text
                xlWorkSheet.Cells(i + 2, 17) = listProduct.Items.Item(i).SubItems(16).Text
                xlWorkSheet.Cells(i + 2, 18) = listProduct.Items.Item(i).SubItems(17).Text
                xlWorkSheet.Cells(i + 2, 19) = listProduct.Items.Item(i).SubItems(18).Text
                xlWorkSheet.Cells(i + 2, 20) = listProduct.Items.Item(i).SubItems(19).Text
                xlWorkSheet.Cells(i + 2, 21) = listProduct.Items.Item(i).SubItems(20).Text
                xlWorkSheet.Cells(i + 2, 22) = listProduct.Items.Item(i).SubItems(21).Text
                xlWorkSheet.Cells(i + 2, 23) = listProduct.Items.Item(i).SubItems(22).Text
                xlWorkSheet.Cells(i + 2, 24) = listProduct.Items.Item(i).SubItems(23).Text
            Next

            Dim dlg As New SaveFileDialog
            dlg.Filter = "Excel Files (*.xls)|*.xls"
            dlg.FilterIndex = 1
            dlg.InitialDirectory = My.Application.Info.DirectoryPath & "\EXCEL\\EICHER\REPORT\"
            dlg.FileName = "Stock Report"
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
    Private Sub ReleaseComObject(ByRef Reference As Object)
        Try
            Do Until _
             System.Runtime.InteropServices.Marshal.ReleaseComObject(Reference) <= 0
            Loop
        Catch
        Finally
            Reference = Nothing
        End Try
    End Sub



    'Private Sub PurchaseReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReportToolStripMenuItem.Click
    '    Me.Close()
    '    frmPurchaseReport.MdiParent = frmHome
    '    frmPurchaseReport.Show()
    'End Sub
    Public Sub ItemLoad()
        Try
            txtSubGroup.Text = ""
            Dim stQuery As String = ""
            stQuery = "SELECT AD_CODE AS CD_ITEM_ANLY_CODE_01, AD_NAME AS CD_ITEM_ANLY_NAME_01, AD_SHORT_NAME  AS CD_ITEM_ANAY_SH_NAME_01 FROM  OM_ANALYSIS_DETAIL WHERE AD_ANLY_NO=2 AND AD_ANLY_TYPE='ITEM' AND AD_PARENT_CODE like '" + cmbmaingrp.Text + "%'"
            errLog.WriteToErrorLog("Sub Group Query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            Sub_Group.Clear()
            cmbsubgrp.Items.Clear()
            For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
                cmbsubgrp.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
                'Sub_Group.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
            Next
            'MySource_SubGroup.AddRange(Sub_Group.ToArray)
            'txtSubGroup.AutoCompleteCustomSource = MySource_SubGroup
            'txtSubGroup.AutoCompleteMode = AutoCompleteMode.Suggest
            'txtSubGroup.AutoCompleteSource = AutoCompleteSource.CustomSource
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    'Private Sub itemsubgrp()
    '    MsgBox("2")
    '    Dim ds As New DataSet
    '    Dim db As New DBConnection
    '    ds = db.SelectFromTableODBC(stQuery)
    '    For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
    '        cmbsubgrp.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
    '    Next
    '    'Me.cmbsubgrp.DataSource = ds.Tables("Table")
    '    'Me.cmbsubgrp.DisplayMember = "CD_ITEM_ANLY_CODE_01"
    '    'Me.cmbsubgrp.ValueMember = "CD_ITEM_ANLY_CODE_01"
    'End Sub

    Private Sub itemfrom()
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count
        cmbitemfrom.Items.Clear()
        cmbitemfrom.Text = ""
        Dim i As Integer = 0
       
        While Count > 0
           
            cmbitemfrom.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
            If i = 0 Then
                cmbitemfrom.SelectedText = ds.Tables("Table").Rows.Item(i).Item(0).ToString
            End If
            Count = Count - 1
            i = i + 1
        End While


        'Me.cmbitemfrom.DataSource = ds.Tables("Table")
        'Me.cmbitemfrom.DisplayMember = "ItemCode"
        'Me.cmbitemfrom.ValueMember = "ItemCode"
    End Sub

    Private Sub itemto()
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count
        cmbitemto.Items.Clear()
        cmbitemto.Text = ""
        Dim i As Integer = 0
        While Count > 0
            cmbitemto.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
            If i = 0 Then
                cmbitemto.SelectedText = ds.Tables("Table").Rows.Item(i).Item(0).ToString
            End If
            Count = Count - 1
            i = i + 1
        End While

        'Me.cmbitemto.DataSource = ds.Tables("Table")
        'Me.cmbitemto.DisplayMember = "ItemCode"
        'Me.cmbitemto.ValueMember = "ItemCode"
    End Sub

    'Private Sub cmbsubgrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsubgrp.LostFocus
    '    Try
    '        Dim condition As String = ""
    '        If cmbmaingrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_01 like '" + txtmaingrp.Text + "%'"
    '        End If
    '        If cmbsubgrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_02 like '" + txtSubGroup.Text + "%'"
    '        End If
    '        stQuery = New String("")
    '        stQuery = "select ITEM_CODE as itemcode, ITEM_NAME as itemdisplay from OM_ITEM where ITEM_CODE is not null " + condition
    '        itemfrom()
    '        itemto()
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

    '    End Try
    'End Sub



    'Private Sub cmbmaingrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmaingrp.SelectedIndexChanged
    '    ItemLoad()
    'End Sub

    'Private Sub cmbsubgrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsubgrp.SelectedIndexChanged
    '    Try
    '        Dim condition As String = ""
    '        If cmbmaingrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_01 like '" + cmbmaingrp.Text + "%'"
    '        End If
    '        If cmbsubgrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_02 like '" + cmbsubgrp.Text + "%'"
    '        End If
    '        stQuery = New String("")
    '        stQuery = "select ITEM_CODE as itemcode, ITEM_NAME as itemdisplay from OM_ITEM where ITEM_CODE is not null " + condition
    '        itemfrom()
    '        itemto()
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

    '    End Try
    'End Sub

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

            ' do tabcontrols separate by name – a separate for/next loop per ‘control
            'For Each tp As TabPage In Me.TabAdhocChart.TabPages
            '    For Each ctl As Control In tp.Controls
            '        If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
            '        And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
            '            shoFont = ctl.Font.Size * perY
            '            ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
            '        End If

            '        'get new location
            '        ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

            '        If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
            '            ctl.Height = ctl.Size.Height * perY + shoAdd
            '            ctl.Width = ctl.Size.Width * perX
            '        Else
            '            ' get new height & width
            '            ctl.Height = ctl.Size.Height * perY
            '            ctl.Width = ctl.Size.Width * perX
            '        End If

            '        Application.DoEvents()
            '    Next
            'Next


            ' do groupboxs separate also – separate for/next for each control by ‘name

            For Each ctl As Control In GroupBox1.Controls
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



            For Each ctl As Control In Panel1.Controls
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
    End Sub

     

    Private Sub btnCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseForm.Click
        btnCloseForm.Visible = False
        Transactions.CloseStockQuery(sender, e)
    End Sub

    Private Sub txtmaingrp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmaingrp.TextChanged
        'ItemLoad()
    End Sub

    Private Sub txtSubGroup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubGroup.TextChanged
        'Try
        '    Dim condition As String = ""
        '    If cmbmaingrp.Text <> "" Then
        '        condition = condition + " and ITEM_ANLY_CODE_01 like '" + txtmaingrp.Text + "%'"
        '    End If
        '    If cmbsubgrp.Text <> "" Then
        '        condition = condition + " and ITEM_ANLY_CODE_02 like '" + txtSubGroup.Text + "%'"
        '    End If
        '    stQuery = New String("")
        '    stQuery = "select ITEM_CODE as itemcode, ITEM_NAME as itemdisplay from OM_ITEM where ITEM_CODE is not null " + condition
        '    itemfrom()
        '    itemto()
        'Catch ex As Exception
        '    errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        'End Try
    End Sub

    
    Private Sub cmbmaingrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbmaingrp.LostFocus
        ItemLoad()
    End Sub

    'Private Sub cmbmaingrp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbmaingrp.TextChanged
    '    ItemLoad()
    'End Sub

    'Private Sub cmbsubgrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsubgrp.LostFocus
    '    Try
    '        Dim condition As String = ""
    '        If cmbmaingrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_01 like '" + cmbmaingrp.Text + "%'"
    '        End If
    '        If cmbsubgrp.Text <> "" Then
    '            condition = condition + " and ITEM_ANLY_CODE_02 like '" + cmbsubgrp.Text + "%'"
    '        End If

    '        stQuery = New String("")
    '        stQuery = "select ITEM_CODE as itemcode, ITEM_NAME as itemdisplay from OM_ITEM where ITEM_CODE is not null " + condition
    '        itemfrom()
    '        itemto()
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

    '    End Try
    'End Sub

    Private Sub cmbsubgrp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsubgrp.TextChanged
        Try
            Dim condition As String = ""
            If cmbmaingrp.Text <> "" Then
                condition = condition + " and ITEM_ANLY_CODE_01 like '" + cmbmaingrp.Text + "%'"
            End If
            If cmbsubgrp.Text <> "" Then
                condition = condition + " and ITEM_ANLY_CODE_02 like '" + cmbsubgrp.Text + "%'"
            End If

            stQuery = New String("")
            stQuery = "select ITEM_CODE as itemcode, ITEM_NAME as itemdisplay from OM_ITEM where ITEM_CODE is not null " + condition
            itemfrom()
            itemto()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        End Try
    End Sub
End Class