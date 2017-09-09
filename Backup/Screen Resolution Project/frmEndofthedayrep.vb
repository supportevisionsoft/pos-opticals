Imports PdfSharp.Pdf
Imports PdfSharp.Drawing


Public Class frmEndofthedayrep
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

    Private Sub frmEndofthedayrep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Dock = DockStyle.Fill
            SetResolution()
            LoadLocation()
            cmbLocation.Text = Location_Code
            cmbLocation_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try

    End Sub
    'Public Sub LoadLocation()

    '    'ds = comfun.GetLocation()
    'Me.cmbLocation.DataSource = ds.Tables("Table")
    'Me.cmbLocation.DisplayMember = "locdisplay"
    'Me.cmbLocation.ValueMember = "loccode"



    '    stQuery = New String("")
    ''stQuery = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from crm_om_location where locn_code in (select UGLA_LOCN_CODE as loccode from ug_location_access where UGLA_UG_ID = ( select user_group_id from menu_user where user_id='" & User_Name & "') and UGLA_COMP_CODE='" & CompanyCode & "' and UGLA_PRIV_YN = 'Y') order by locdisplay"
    '    stQuery = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from crm_om_location where locn_code in (SELECT ula_locn_code from user_location_access where ula_u_id = '" & User_Name & "' and ULA_PRIV_YN = 'Y') order by locdisplay"
    '    ds = db.SelectFromTableODBC(stQuery)
    '    Count = ds.Tables("Table").Rows.Count
    '    Return ds
    'End Sub

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
    '            Dim Query As String = "select poscnt_no from om_pos_counter where poscnt_locn_code='" & strArrLoc(0) & "' AND POSCNT_FRZ_FLAG_NUM=2"
    '            ds = db.SelectFromTableODBC(Query)
    '            'cmbCounter.Items.Clear()
    '            'cmbCounter.Text = ""
    '            Dim count As Integer = ds.Tables("Table").Rows.Count
    '            Dim i As Integer = 0
    '            While count > 0
    '                'cmbCounter.Text = ds.Tables("Table").Rows(i).Item(0).ToString
    '                'cmbCounter.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
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

    Private Sub Call_invoice()
        Try
            If cmbSm.Text = "All" Then
                Query = "SELECT COUNT(INVH_NO) FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "'  AND INVH_DT >= '" & stDateval & "'  AND INVH_DT <= '" & endDateval & "' "
            Else

                'Query = "SELECT SUM(INVI_QTY) AS QTY,SUM(INVI_FC_VAL) AS VALUE FROM OT_INVOICE_HEAD , OT_INVOICE_ITEM  WHERE INVH_SYS_ID=INVI_INVH_SYS_ID AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "' AND INVH_DT >= '" & stDateval & "'  AND INVH_DT <= '" & endDateval & "' AND INVH_FLEX_19 = '" & cmbShift.Text & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' "
                Query = "SELECT COUNT(INVH_NO) FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "'  AND INVH_DT >= '" & stDateval & "'  AND INVH_DT <= '" & endDateval & "'  AND INVH_SM_CODE = '" & cmbSm.Text & "' "
            End If
            errLog.WriteToErrorLog("EOD:Invoice", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")
            'dtl.Rows(cPos).Item(0).ToString
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                invcount.Text = dt.Rows(i).Item(0).ToString
                'invval.Text = dt.Rows(i).Item(1).ToString
                'invval_LC.Text = dt.Rows(i).Item(1).ToString
            Next

            Dim stQuery As String

            If cmbSm.Text = "All" Then
                stQuery = "SELECT nvl(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND   INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'"
            Else
                stQuery = "SELECT nvl(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND   INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' "
            End If
            errLog.WriteToErrorLog("EOD: INV value without sales invoice", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            Dim invVal_withoutSINV As Double = 0
            If ds.Tables("Table").Rows.Count > 0 Then
                invVal_withoutSINV = Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString)
            End If
            invval.Text = invVal_withoutSINV.ToString("0.00")
            invval_LC.Text = invVal_withoutSINV.ToString("0.00")


            'If cmbSm.Text = "All" Then
            '    stQuery = "SELECT nvl(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND PINVP_FLEX_20 = 'CASH' AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'"
            'Else
            '    stQuery = "SELECT nvl(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND PINVP_FLEX_20 = 'CASH' AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' "
            'End If
            'errLog.WriteToErrorLog("EOD: INV value without sales invoice", stQuery, "")
            'ds = db.SelectFromTableODBC(stQuery)
            'Dim invVal_withoutSINV As Double = 0
            'If ds.Tables("Table").Rows.Count > 0 Then
            '    invVal_withoutSINV = Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString)
            'End If

            'If cmbSm.Text = "All" Then
            '    stQuery = "SELECT nvl(SUM(NVL(SOH_ADVANCE,0)),0) AS VALUE FROM OT_SO_HEAD, OT_INVOICE_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND INVH_REF_NO = SOH_NO AND INVH_REF_NO = SOH_NO AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND SOH_DT <= INVH_DT"
            'Else
            '    stQuery = "SELECT nvl(SUM(NVL(SOH_ADVANCE,0)),0) AS VALUE FROM OT_SO_HEAD, OT_INVOICE_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND INVH_REF_NO = SOH_NO AND INVH_REF_NO = SOH_NO AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' AND SOH_DT <= INVH_DT"
            'End If
            'errLog.WriteToErrorLog("EOD: INV value with sales invoice", stQuery, "")
            'ds = db.SelectFromTableODBC(stQuery)
            'Dim invVal_withSINV As Double = 0
            'If ds.Tables("Table").Rows.Count > 0 Then
            '    invVal_withSINV = Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString)
            'End If

            'invval.Text = (invVal_withoutSINV + invVal_withSINV).ToString("0.00")
            'invval_LC.Text = (invVal_withoutSINV + invVal_withSINV).ToString("0.00")
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub Call_salesreturn()
        Try
            'nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as Expamt,nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID=CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as Disamt
            Dim Querycount As String = ""
            If cmbSm.Text = "All" Then
                'Query = "SELECT SUM(CSRI_QTY) AS QTY,SUM(CSRI_FC_VAL) AS VALUE FROM OT_CUST_SALE_RET_HEAD CSRH, OT_CUST_SALE_RET_ITEM CSRI WHERE CSRH.CSRH_SYS_ID=CSRI.CSRI_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' AND CSRH_FLEX_19 = '" & cmbShift.Text & "' AND  CSRH_FLEX_20 = '" & cmbCounter.Text & "' AND CSRH_SM_CODE = '" & cmbSm.Text & "'"
                Query = "SELECT COUNT(CSRH_NO),SUM(CSRI_FC_VAL) AS VALUE, SUM(nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0)) as Expamt,  SUM(nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0)) as Disamt FROM OT_CUST_SALE_RET_HEAD CSRH, OT_CUST_SALE_RET_ITEM CSRI WHERE CSRH.CSRH_SYS_ID=CSRI.CSRI_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' "
                Querycount = "SELECT distinct CSRH_NO FROM OT_CUST_SALE_RET_HEAD CSRH WHERE  CSRH_COMP_CODE = '" & CompanyCode & "' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT COUNT(CSRH_NO),SUM(CSRI_FC_VAL) AS VALUE, SUM(nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0)) as Expamt,  SUM(nvl((select ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0)) as Disamt FROM OT_CUST_SALE_RET_HEAD CSRH, OT_CUST_SALE_RET_ITEM CSRI WHERE CSRH.CSRH_SYS_ID=CSRI.CSRI_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "'   AND CSRH_SM_CODE = '" & cmbSm.Text & "'"
                Querycount = "SELECT distinct CSRH_NO FROM OT_CUST_SALE_RET_HEAD CSRH WHERE  CSRH_COMP_CODE = '" & CompanyCode & "' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "'   AND CSRH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("EOD:salesreturn", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")

            Dim dscount As DataSet
            errLog.WriteToErrorLog("EOD:salesreturn count query", Querycount, "")
            dscount = db.SelectFromTableODBC(Querycount)

            salescount.Text = dscount.Tables("Table").Rows.Count


            'dtl.Rows(cPos).Item(0).ToString
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                'salescount.Text = dt.Rows(i).Item(0).ToString
                Dim val As Double = (Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(1).ToString) + Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(2).ToString)) - Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(3).ToString)
                'salesval.Text = dt.Rows(i).Item(1).ToString
                'salesval_LC.Text = dt.Rows(i).Item(1).ToString

                salesval.Text = val.ToString
                salesval_LC.Text = val.ToString
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub Call_holding()
        Try
            If cmbSm.Text = "All" Then
                'Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' AND INVH_FLEX_19 = '" & cmbShift.Text & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "'"
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("EOD:holding", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")
            'dtl.Rows(cPos).Item(0).ToString
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                holdcount.Text = dt.Rows(i).Item(0).ToString
                holdval.Text = dt.Rows(i).Item(1).ToString
                holdval_LC.Text = dt.Rows(i).Item(1).ToString
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        End Try
    End Sub
    Private Sub Call_Cancel()
        Try
            If cmbSm.Text = "All" Then
                'Query = "SELECT SUM(INVI_QTY) AS QTY,SUM(INVI_FC_VAL) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_INVOICE_ITEM OTI WHERE OTH.INVH_SYS_ID=OTI.INVI_INVH_SYS_ID AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_FLEX_20 = '013' AND INVH_DT = '" & Dateval & "'  AND INVH_FLEX_19 = '8 AM TO 8 AM' "
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=5 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=5 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("EOD:Cancel", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")
            'dtl.Rows(cPos).Item(0).ToString
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                cancelcount.Text = dt.Rows(i).Item(0).ToString
                cancelval.Text = dt.Rows(i).Item(1).ToString
                cancelval_LC.Text = dt.Rows(i).Item(1).ToString
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        End Try
    End Sub
    Private Sub Call_Deleted()
        Try
            If cmbSm.Text = "All" Then
                'Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=6 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' AND INVH_FLEX_19 = '" & cmbShift.Text & "'"
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=6 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT SUM(PRODQTY) AS QTY, SUM(PRODPRICE) AS VALUE FROM OT_POS_INVOICE_HEAD_LOG OTH, OT_POS_INVOICE_ITEM_LOG OTI WHERE OTH.INVH_SYS_ID=OTI.PROD_INVI_INVH_SYS_ID AND OTH.INVH_STATUS=6 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'AND INVH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("EOD:Deleted", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")
            'dtl.Rows(cPos).Item(0).ToString
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                delcount.Text = dt.Rows(i).Item(0).ToString
                delval.Text = dt.Rows(i).Item(1).ToString
                delval_LC.Text = dt.Rows(i).Item(1).ToString
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

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




            ' do groupboxs separate also – separate for/next for each control by ‘name

            For Each ctl As Control In GrpBox_SalesSummary.Controls
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

            'For Each ctl As Control In MenuStrip7.Controls
            '    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
            '    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
            '        shoFont = ctl.Font.Size * perY
            '        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
            '    End If

            '    'get new location
            '    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

            '    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
            '        ctl.Height = ctl.Size.Height * perY + shoAdd
            '        ctl.Width = ctl.Size.Width * perX
            '    Else
            '        ' get new height & width
            '        ctl.Height = ctl.Size.Height * perY
            '        ctl.Width = ctl.Size.Width * perX
            '    End If

            '    Application.DoEvents()
            'Next


            ' do panels separate also – separate for/next for each ‘panel by name

            For Each ctl As Control In pnl_EDRDetails.Controls
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

            For Each ctl As Control In Panel3.Controls
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

            For Each ctl As Control In Panel2.Controls
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

            For Each ctl As Control In Panel6.Controls
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

            For Each ctl As Control In pnldate.Controls
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

            For Each ctl As Control In pnl_endofthereport.Controls
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

            For Each ctl As Control In pnl_detailsofEDR.Controls
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

            For Each ctl As Control In pnlRptContainer.Controls
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
    Private Sub Call_DynamicPayment()
        Try
            'Dim drawLineGraph As System.Drawing.Graphics
            'drawLineGraph = Graphics.FromHwnd(Handle)
            'drawLineGraph.DrawLine(pen:=New Pen(Brushes.Blue), x1:=20, y1:=500, x2:=500, y2:=500)

            '------------------------------Invoice  Payments---------------------------------------------------
            Dim htcount As Integer = 0
            Dim n As Integer
            Dim k As Integer
            Dim lblhead As Label
            lblhead = New Label
            With lblhead
                .Location = New Point(20, (n * 20))
                .Name = "lblHead"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead.Text = "Invoice Payments".ToString
            htcount = htcount + 20 '------------ Header Count
            Me.pnl_detailsofEDR.Controls.Add(lblhead)
            If cmbSm.Text = "All" Then
                'Query = "SELECT OTI.PINVP_PPD_CODE,SUM(OTI.PINVP_FC_VAL) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI where OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_COMP_CODE = '001' AND INVH_FLEX_19 = '" & cmbShift.Text & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') GROUP BY PINVP_PPD_CODE"
                Query = "SELECT OTI.PINVP_PPD_CODE,SUM(OTI.PINVP_FC_VAL) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI where OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_COMP_CODE = '001'  AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') GROUP BY PINVP_PPD_CODE"
            Else
                Query = "SELECT OTI.PINVP_PPD_CODE,SUM(OTI.PINVP_FC_VAL) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI where OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_COMP_CODE = '001'  AND INVH_SM_CODE = '" & cmbSm.Text & "' AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') GROUP BY PINVP_PPD_CODE"
            End If
            errLog.WriteToErrorLog("EOD:Payment", Query, "")
            ds = db.SelectFromTableODBC(Query)
            dt = ds.Tables("Table")
            If (ds.Tables("Table").Rows.Count > 0) Then
                For k = 0 To ds.Tables("Table").Rows.Count - 1
                    Me.pnl_detailsofEDR.AutoScrollPosition = New System.Drawing.Point(0, 0)
                    Dim lbl As Label
                    Dim lblcurr As Label
                    Dim lblInvPayFC As Label
                    Dim lblInvPayLC As Label
                    n = k + 1
                    htcount = htcount + 20 '------------ Data Count
                    lbl = New Label
                    lblcurr = New Label
                    lblInvPayFC = New Label
                    lblInvPayLC = New Label
                    With lbl
                        .Location = New Point(36, (n * 20))
                        .Name = "lblSNO" & n.ToString
                        .Size = New Size(150, 20)
                        .TextAlign = ContentAlignment.TopLeft
                        '"lblSNO" & n.ToString.FontBold = True
                        '.Font = New System.Drawing.Font(lbl.Font, FontStyle.Bold)
                        '.BorderStyle = BorderStyle.FixedSingle
                    End With
                    With lblcurr
                        .Location = New Point(186, (n * 20))
                        .Name = "lblCURR" & n.ToString
                        .Size = New Size(150, 20)
                        .TextAlign = ContentAlignment.TopLeft
                        '.BorderStyle = BorderStyle.FixedSingle
                    End With
                    With lblInvPayFC
                        .Location = New Point(507, (n * 20))
                        .Name = "lblInvPayFC" & n.ToString
                        .Size = New Size(100, 20)
                        .TextAlign = ContentAlignment.TopLeft
                        '.BorderStyle = BorderStyle.FixedSingle
                    End With
                    With lblInvPayLC
                        .Location = New Point(650, (n * 20))
                        .Name = "lblInvPayLC" & n.ToString
                        .Size = New Size(80, 20)
                        .TextAlign = ContentAlignment.TopLeft
                        '.BorderStyle = BorderStyle.FixedSingle
                    End With
                    lbl.Text = dt.Rows(k).Item(0).ToString
                    ' "lblSNO" & n.ToString.FontBold = True
                    lblcurr.Text = "AED".ToString
                    lblInvPayFC.Text = dt.Rows(k).Item(1)
                    lblInvPayLC.Text = dt.Rows(k).Item(1)
                    Me.pnl_detailsofEDR.Controls.Add(lbl)
                    Me.pnl_detailsofEDR.Controls.Add(lblcurr)
                    Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC)
                    Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC)
                Next
            End If
            '------------------------------SalesReturn Payments---------------------------------------------------
            n = k + 2
            htcount = htcount + 2 '------------ space Count
            Dim ds1 As New DataSet
            Dim dt1 As New DataTable
            Dim lblhead1 As Label
            lblhead1 = New Label
            With lblhead1
                .Location = New Point(20, (n * 20))
                .Name = "lblHead1"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead1.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead1.Text = "SalesReturn Payments".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead1)
            htcount = htcount + 20 '------------ Header Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT PCSRTP_PPD_CODE,SUM(PCSRTP_FC_VAL) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' AND CSRH_FLEX_19 = '" & cmbShift.Text & "' AND CSRH_FLEX_20 = '" & cmbCounter.Text & "'  AND CSRH_SM_CODE = '" & cmbSm.Text & "' GROUP BY PCSRTP_PPD_CODE "
                Query = "SELECT PCSRTP_PPD_CODE,SUM(PCSRTP_FC_VAL) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "'  GROUP BY PCSRTP_PPD_CODE "
            Else
                Query = "SELECT PCSRTP_PPD_CODE,SUM(PCSRTP_FC_VAL) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "'  AND CSRH_SM_CODE = '" & cmbSm.Text & "' GROUP BY PCSRTP_PPD_CODE "
            End If
            errLog.WriteToErrorLog("EOD-CN:Payment", Query, "")
            ds1 = db.SelectFromTableODBC(Query)
            dt1 = ds1.Tables("Table")
            If (ds1.Tables("Table").Rows.Count > 0) Then
                Dim lbl1 As Label
                Dim lblcurr1 As Label
                Dim lblInvPayFC1 As Label
                Dim lblInvPayLC1 As Label
                n = k + 3
                htcount = htcount + 1 '------------ space Count
                lbl1 = New Label
                lblcurr1 = New Label
                lblInvPayFC1 = New Label
                lblInvPayLC1 = New Label
                With lbl1
                    .Location = New Point(36, (n * 20))
                    .Name = "lblSNO1"
                    .Size = New Size(150, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    '"lblSNO" & n.ToString.FontBold = True
                    '.Font = New System.Drawing.Font(lbl.Font, FontStyle.Bold)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                With lblcurr1
                    .Location = New Point(186, (n * 20))
                    .Name = "lblCURR1"
                    .Size = New Size(150, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                With lblInvPayFC1
                    .Location = New Point(507, (n * 20))
                    .Name = "lblInvPayFC1"
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                With lblInvPayLC1
                    .Location = New Point(650, (n * 20))
                    .Name = "lblInvPayLC1"
                    .Size = New Size(80, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                lbl1.Text = dt1.Rows(0).Item(0).ToString
                ' "lblSNO" & n.ToString.FontBold = True
                lblcurr1.Text = "AED".ToString
                lblInvPayFC1.Text = dt1.Rows(0).Item(1).ToString
                lblInvPayLC1.Text = dt1.Rows(0).Item(1).ToString
                Me.pnl_detailsofEDR.Controls.Add(lbl1)
                Me.pnl_detailsofEDR.Controls.Add(lblcurr1)
                Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC1)
                Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC1)
            End If
            htcount = htcount + 20 '------------ Data Count

            '------------------------------Total of Adjusted Amount from SO (prev)---------------------------------------------------
            n = k + 5
            htcount = htcount + 2 '------------ space Count
            Dim ds2 As New DataSet
            Dim dt2 As New DataTable
            Dim lblhead2 As Label
            lblhead2 = New Label
            With lblhead2
                .Location = New Point(20, (n * 20))
                .Name = "lblHead2"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                .Font = New System.Drawing.Font(lblhead2.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead2.Text = "Total of Adjusted Amount from SO (prev)".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead2)
            htcount = htcount + 20 '------------ Header Count
            'If cmbSm.Text = "All" Then
            ''Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "' AND SOH_FLEX_19 = '" & cmbShift.Text & "' AND SOH_FLEX_20 = '" & cmbCounter.Text & "'  AND SOH_SM_CODE = '" & cmbSm.Text & "'"
            '    Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "' "
            'Else
            '    Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "' AND SOH_SM_CODE = '" & cmbSm.Text & "'"
            'End If
            If cmbSm.Text = "All" Then
                Query = "SELECT nvl(SUM(NVL(SOH_ADVANCE,0)),0) AS VALUE FROM OT_SO_HEAD, OT_INVOICE_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND INVH_REF_NO = SOH_NO AND INVH_REF_NO = SOH_NO AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND SOH_DT <= INVH_DT"
            Else
                Query = "SELECT nvl(SUM(NVL(SOH_ADVANCE,0)),0) AS VALUE FROM OT_SO_HEAD, OT_INVOICE_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND INVH_REF_NO = SOH_NO AND INVH_REF_NO = SOH_NO AND INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_SM_CODE = '" & cmbSm.Text & "' AND SOH_DT <= INVH_DT"
            End If

            errLog.WriteToErrorLog("SO(Prev):Payment", Query, "")
            ds2 = db.SelectFromTableODBC(Query)
            dt2 = ds2.Tables("Table")
            Dim lblInvPayFC2 As Label
            Dim lblInvPayLC2 As Label
            lblInvPayFC2 = New Label
            lblInvPayLC2 = New Label
            With lblInvPayFC2
                .Location = New Point(507, (n * 20))
                .Name = "lblSNO2"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.Font = New System.Drawing.Font(lbl.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblInvPayLC2
                .Location = New Point(650, (n * 20))
                .Name = "lblCURR2"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblInvPayFC2.Text = dt2.Rows(0).Item(0).ToString
            lblInvPayLC2.Text = dt2.Rows(0).Item(0).ToString
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC2)
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC2)
            htcount = htcount + 20 '------------ Data Count
            '------------------------------Opening Cash(+) ---------------------------------------------------
            n = k + 7
            htcount = htcount + 2 '------------ space Count
            Dim ds3 As New DataSet
            Dim dt3 As New DataTable
            Dim lblhead3 As Label
            lblhead3 = New Label
            With lblhead3
                .Location = New Point(20, (n * 20))
                .Name = "lblHead3"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead3.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead3.Text = "Opening Cash(+) ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead3)
            htcount = htcount + 20 '------------ Header Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT NVL(SUM(PDD_BASE_CURR_VAL),0) FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'O' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "' AND PDH_SHIFT_CODE = '" & cmbShift.Text & "' AND PDH_CNT_NO = '" & cmbCounter.Text & "'  AND PDH_SM_CODE = '" & cmbSm.Text & "'"
                Query = "SELECT NVL(SUM(PDD_BASE_CURR_VAL),0) FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'O' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "'"
            Else
                Query = "SELECT NVL(SUM(PDD_BASE_CURR_VAL),0) FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'O' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "' AND PDH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("Opening Cash:Payment", Query, "")
            ds3 = db.SelectFromTableODBC(Query)
            dt3 = ds3.Tables("Table")
            Dim lblInvPayFC3 As Label
            Dim lblInvPayLC3 As Label
            lblInvPayFC3 = New Label
            lblInvPayLC3 = New Label
            With lblInvPayFC3
                .Location = New Point(507, (n * 20))
                .Name = "lblOpenCashFC3"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblInvPayLC3
                .Location = New Point(650, (n * 20))
                .Name = "lblOpenCashFC3"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblInvPayFC3.Text = dt3.Rows(0).Item(0).ToString
            lblInvPayLC3.Text = dt3.Rows(0).Item(0).ToString
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC3)
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC3)
            htcount = htcount + 20 '------------ Data Count
            '------------------------------Sales(+ Total Invoice Payments) ---------------------------------------------------
            n = k + 8
            htcount = htcount + 1 '------------ space Count
            Dim ds4 As New DataSet
            Dim dt4 As New DataTable
            Dim lblhead4 As Label
            lblhead4 = New Label
            With lblhead4
                .Location = New Point(20, (n * 20))
                .Name = "lblHead4"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead4.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead4.Text = "Sales(+ Total Invoice Payments) ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead4)
            htcount = htcount + 20 '------------ Header Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT NVL(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID  AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' AND INVH_FLEX_19 = '" & cmbShift.Text & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "'  AND INVH_SM_CODE = '" & cmbSm.Text & "'"
                Query = "SELECT NVL(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID  AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT NVL(SUM(PINVP_FC_VAL),0) AS VALUE FROM OT_INVOICE_HEAD OTH, OT_POS_INVOICE_PAYMENT OTI WHERE OTH.INVH_SYS_ID=OTI.PINVP_INVH_SYS_ID  AND (DECODE(PINVP_FLEX_19,NULL,'Curr',PINVP_FLEX_19) <> 'ADVANCE') AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'  AND INVH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("Sales(+ Total Invoice Payments)", Query, "")
            ds4 = db.SelectFromTableODBC(Query)
            dt4 = ds4.Tables("Table")
            Dim lblInvPayFC4 As Label
            Dim lblInvPayLC4 As Label
            lblInvPayFC4 = New Label
            lblInvPayLC4 = New Label
            With lblInvPayFC4
                .Location = New Point(507, (n * 20))
                .Name = "lblOpenCashFC4"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblInvPayLC4
                .Location = New Point(650, (n * 20))
                .Name = "lblOpenCashFC4"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblInvPayFC4.Text = dt4.Rows(0).Item(0).ToString
            lblInvPayLC4.Text = dt4.Rows(0).Item(0).ToString
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC4)
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC4)
            htcount = htcount + 20 '------------ Data Count
            '------------------------------Sales Return(- Sales Return Payments) ---------------------------------------------------
            n = k + 9
            htcount = htcount + 1 '------------ space Count
            Dim ds5 As New DataSet
            Dim dt5 As New DataTable
            Dim lblhead5 As Label
            lblhead5 = New Label
            With lblhead5
                .Location = New Point(20, (n * 20))
                .Name = "lblHead5"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead4.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead5.Text = "Sales Return(- Sales Return Payments) ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead5)
            htcount = htcount + 20 '------------ Header Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT NVL(SUM(PCSRTP_FC_VAL),0) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND PCSRTP_FLEX_20 = 'CREDIT NOTE' AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' AND CSRH_FLEX_19 = '" & cmbShift.Text & "' AND CSRH_FLEX_20 = '" & cmbCounter.Text & "'  AND CSRH_SM_CODE = '" & cmbSm.Text & "'"
                Query = "SELECT NVL(SUM(PCSRTP_FC_VAL),0) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND PCSRTP_FLEX_20 = 'CREDIT NOTE' AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT NVL(SUM(PCSRTP_FC_VAL),0) AS VALUE FROM OT_CUST_SALE_RET_HEAD OTH, OT_POS_CUST_SALE_RET_PAYMENT OTI WHERE OTH.CSRH_SYS_ID=OTI.PCSRTP_CSRH_SYS_ID AND PCSRTP_FLEX_20 = 'CREDIT NOTE' AND CSRH_COMP_CODE = '001' AND CSRH_LOCN_CODE = '" & strArrLoc(0) & "' AND CSRH_DT >= '" & stDateval & "' AND CSRH_DT <= '" & endDateval & "'   AND CSRH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("Sales Return(- Sales Return Payments))", Query, "")
            ds5 = db.SelectFromTableODBC(Query)
            dt5 = ds5.Tables("Table")
            Dim lblInvPayFC5 As Label
            Dim lblInvPayLC5 As Label
            lblInvPayFC5 = New Label
            lblInvPayLC5 = New Label
            With lblInvPayFC5
                .Location = New Point(507, (n * 20))
                .Name = "lblSalRetFC5"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblInvPayLC5
                .Location = New Point(650, (n * 20))
                .Name = "lblSalRetLC5"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblInvPayFC5.Text = dt5.Rows(0).Item(0).ToString
            lblInvPayLC5.Text = dt5.Rows(0).Item(0).ToString
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC5)
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC5)
            htcount = htcount + 20 '------------ Data Count
            '------------------------------Sales Orders - Advance Payments (+) ---------------------------------------------------
            n = k + 10
            htcount = htcount + 1 '------------ space Count
            Dim ds6 As New DataSet
            Dim dt6 As New DataTable
            Dim lblhead6 As Label
            lblhead6 = New Label
            With lblhead6
                .Location = New Point(20, (n * 20))
                .Name = "lblHead6"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead4.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead6.Text = "Sales Orders - Advance Payments (+) ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead6)
            htcount = htcount + 20 '------------ Header Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "' AND SOH_FLEX_19 = '" & cmbShift.Text & "' AND SOH_FLEX_20 = '" & cmbCounter.Text & "'  AND SOH_SM_CODE = '" & cmbSm.Text & "'"
                Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "' "
            Else
                Query = "SELECT NVL(SUM(SOH_ADVANCE),0) AS VALUE FROM OT_SO_HEAD WHERE NVL(SOH_ADVANCE,0) > 0 AND SOH_COMP_CODE = '001' AND SOH_LOCN_CODE = '" & strArrLoc(0) & "' AND SOH_DT >= '" & stDateval & "' AND SOH_DT <= '" & endDateval & "'   AND SOH_SM_CODE = '" & cmbSm.Text & "'"
            End If
            errLog.WriteToErrorLog("Sales Orders - Advance Payments (+)", Query, "")
            ds6 = db.SelectFromTableODBC(Query)
            dt6 = ds6.Tables("Table")
            Dim lblInvPayFC6 As Label
            Dim lblInvPayLC6 As Label
            lblInvPayFC6 = New Label
            lblInvPayLC6 = New Label
            With lblInvPayFC6
                .Location = New Point(507, (n * 20))
                .Name = "lblSalOrdFC6"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblInvPayLC6
                .Location = New Point(650, (n * 20))
                .Name = "lblSalOrdLC6"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblInvPayFC6.Text = dt6.Rows(0).Item(0).ToString
            lblInvPayLC6.Text = dt6.Rows(0).Item(0).ToString
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayFC6)
            Me.pnl_detailsofEDR.Controls.Add(lblInvPayLC6)
            htcount = htcount + 20 '------------ Data Count
            '------------------------------Net Amount---------------------------------------------------
            n = k + 11
            htcount = htcount + 1 '------------ space Count
            Dim pnlNetAmt As Panel
            pnlNetAmt = New Panel
            With pnlNetAmt
                .Location = New Point(10, (n * 20))
                .Name = "Panel1"
                .Size = New Size(700, 1)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            Me.pnl_detailsofEDR.Controls.Add(pnlNetAmt)
            htcount = htcount + 20 '------------ Panel Count

            n = k + 12
            htcount = htcount + 1 '------------ space Count
            Dim lblhead7 As Label
            Dim lblNetFC7 As Label
            Dim lblNetLC7 As Label
            lblhead7 = New Label
            lblNetFC7 = New Label
            lblNetLC7 = New Label
            With lblhead7
                .Location = New Point(20, (n * 20))
                .Name = "lblHead7"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead4.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead7.Text = "Net Collection ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead7)
            htcount = htcount + 20 '------------ Header Count
            With lblNetFC7
                .Location = New Point(507, (n * 20))
                .Name = "lblNetAmtFC7"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            With lblNetLC7
                .Location = New Point(650, (n * 20))
                .Name = "lblNetAmtLC7"
                .Size = New Size(80, 20)
                .TextAlign = ContentAlignment.TopLeft
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblNetFC7.Text = lblInvPayFC4.Text - lblInvPayFC5.Text + lblInvPayFC6.Text
            lblNetLC7.Text = lblInvPayLC4.Text - lblInvPayLC5.Text + lblInvPayLC6.Text
            Me.pnl_detailsofEDR.Controls.Add(lblNetFC7)
            Me.pnl_detailsofEDR.Controls.Add(lblNetLC7)
            htcount = htcount + 20 '------------ Data Count
            n = k + 13
            htcount = htcount + 1 '------------ space Count
            Dim pnlNetAmt1 As Panel
            pnlNetAmt1 = New Panel
            With pnlNetAmt1
                .Location = New Point(10, (n * 20))
                .Name = "Panel2"
                .Size = New Size(700, 1)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            Me.pnl_detailsofEDR.Controls.Add(pnlNetAmt1)
            htcount = htcount + 20 '------------ Panel Count
            '------------------------------Denomination Details Head---------------------------------------------------
            n = k + 14
            htcount = htcount + 1 '------------ space Count
            Dim lblhead8 As Label
            lblhead8 = New Label
            With lblhead8
                .Location = New Point(20, (n * 20))
                .Name = "lblHead8"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead8.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead8.Text = "Denomination Details ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead8)
            htcount = htcount + 20 '------------ Header Count
            n = k + 15
            htcount = htcount + 1 '------------ space Count
            Dim lblhead9 As Label
            Dim lblhead10 As Label
            Dim lblhead11 As Label
            Dim lblhead12 As Label
            Dim lblhead13 As Label
            Dim lblhead14 As Label
            lblhead9 = New Label
            lblhead10 = New Label
            lblhead11 = New Label
            lblhead12 = New Label
            lblhead13 = New Label
            lblhead14 = New Label

            With lblhead9
                .Location = New Point(20, (n * 20))
                .Name = "lblHead9"
                .Size = New Size(130, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead9.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead9.Text = "Currency Code ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead9)

            With lblhead10
                .Location = New Point(150, (n * 20))
                .Name = "lblHead10"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead10.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead10.Text = "Deno Flag ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead10)

            With lblhead11
                .Location = New Point(250, (n * 20))
                .Name = "lblHead11"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead11.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead11.Text = "Denomination code ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead11)

            With lblhead12
                .Location = New Point(350, (n * 20))
                .Name = "lblHead12"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead12.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead12.Text = "Count ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead12)

            With lblhead13
                .Location = New Point(450, (n * 20))
                .Name = "lblHead13"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead13.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead13.Text = "Value ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead13)

            With lblhead14
                .Location = New Point(550, (n * 20))
                .Name = "lblHead14"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead14.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead14.Text = "Base Value ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead14)
            htcount = htcount + 20 '------------ Denomination Table column Count
            '------------------------------Denomination Details Data---------------------------------------------------
            Dim m As Integer
            Dim ds7 As New DataSet
            Dim dt7 As New DataTable
            n = k + 16
            htcount = htcount + 1 '------------ space Count
             If cmbSm.Text = "All" Then
            'Query = "SELECT PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE, SUM(PDD_COUNT) AS PDD_COUNT, SUM(PDD_BASE_CURR_VAL) AS PDD_BASE_CURR_VAL FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'C' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "' AND PDH_SHIFT_CODE = '" & cmbShift.Text & "' AND PDH_CNT_NO = '" & cmbCounter.Text & "' AND PDH_SM_CODE = '" & cmbSm.Text & "'  GROUP BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE ORDER BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG,PDD_DENO_CODE"
                Query = "SELECT PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE, SUM(PDD_COUNT) AS PDD_COUNT, SUM(PDD_BASE_CURR_VAL) AS PDD_BASE_CURR_VAL FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'C' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "' GROUP BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE ORDER BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG,PDD_DENO_CODE"
            Else
                Query = "SELECT PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE, SUM(PDD_COUNT) AS PDD_COUNT, SUM(PDD_BASE_CURR_VAL) AS PDD_BASE_CURR_VAL FROM OT_POS_DENO_HEAD, OT_POS_DENO_DET WHERE PDH_SYS_ID = PDD_PDH_SYS_ID AND PDH_STATUS = 'C' AND PDH_COMP_CODE = '001' AND PDH_LOCN_CODE = '" & strArrLoc(0) & "' AND PDH_DT >= '" & stDateval & "' AND PDH_DT <= '" & endDateval & "'  AND PDH_SM_CODE = '" & cmbSm.Text & "'  GROUP BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG, PDD_DENO_CODE ORDER BY PDD_CURR_CODE, PDD_NOTE_COIN_FLAG,PDD_DENO_CODE"
            End If
            errLog.WriteToErrorLog("Denomination Det", Query, "")
            ds7 = db.SelectFromTableODBC(Query)
            dt7 = ds7.Tables("Table")
            For m = 0 To ds7.Tables("Table").Rows.Count - 1
                Dim lbldeno1 As Label
                Dim lbldeno2 As Label
                Dim lbldeno3 As Label
                Dim lbldeno4 As Label
                Dim lbldeno5 As Label
                Dim lbldeno6 As Label
                n = k + m + 16
                htcount = htcount + 20 '------------ Data Count
                lbldeno1 = New Label
                lbldeno2 = New Label
                lbldeno3 = New Label
                lbldeno4 = New Label
                lbldeno5 = New Label
                lbldeno6 = New Label
                With lbldeno1
                    .Location = New Point(20, (n * 20))
                    .Name = "lbldeno1val" & n.ToString
                    .Size = New Size(130, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno2
                    .Location = New Point(150, (n * 20))
                    .Name = "lbldeno2val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno3
                    .Location = New Point(250, (n * 20))
                    .Name = "lbldeno3val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno4
                    .Location = New Point(350, (n * 20))
                    .Name = "lbldeno4val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno5
                    .Location = New Point(450, (n * 20))
                    .Name = "lbldeno5val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno6
                    .Location = New Point(550, (n * 20))
                    .Name = "lbldeno6val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                'MsgBox(dt.Rows(m).Item(0).ToString & dt.Rows(m).Item(1).ToString & dt.Rows(m).Item(2) & dt.Rows(m).Item(3) & dt.Rows(m).Item(4))
                lbldeno1.Text = dt7.Rows(m).Item(0).ToString
                lbldeno2.Text = dt7.Rows(m).Item(1).ToString
                lbldeno3.Text = dt7.Rows(m).Item(2)
                lbldeno4.Text = dt7.Rows(m).Item(3)
                lbldeno5.Text = dt7.Rows(m).Item(4)
                lbldeno6.Text = dt7.Rows(m).Item(4)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno1)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno2)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno3)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno4)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno5)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno6)
            Next

            '------------------------------Holding Invoice Head---------------------------------------------------
            n = k + m + 17
            htcount = htcount + 1 '------------ space Count
            Dim lblhead15 As Label
            lblhead15 = New Label
            With lblhead15
                .Location = New Point(20, (n * 20))
                .Name = "lblHead15"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead15.Font, FontStyle.Bold)
                '.BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead15.Text = "Holding Invoices ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead15)
            htcount = htcount + 20 '------------ Header Count
            n = k + m + 18
            Dim lblhead16 As Label
            Dim lblhead17 As Label
            Dim lblhead18 As Label
            lblhead16 = New Label
            lblhead17 = New Label
            lblhead18 = New Label

            With lblhead16
                .Location = New Point(20, (n * 20))
                .Name = "lblHead16"
                .Size = New Size(250, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead16.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead16.Text = "Invoice Date ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead16)

            With lblhead17
                .Location = New Point(270, (n * 20))
                .Name = "lblHead17"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead17.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead17.Text = "Transaction Code".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead17)

            With lblhead18
                .Location = New Point(370, (n * 20))
                .Name = "lblHead18"
                .Size = New Size(100, 20)
                .TextAlign = ContentAlignment.TopLeft
                '"lblSNO" & n.ToString.FontBold = True
                .Font = New System.Drawing.Font(lblhead18.Font, FontStyle.Bold)
                .BorderStyle = BorderStyle.FixedSingle
            End With
            lblhead18.Text = "Invoice Number ".ToString
            Me.pnl_detailsofEDR.Controls.Add(lblhead18)
            htcount = htcount + 20 '------------ Hold inv Table column Count

            '------------------------------Holding Invoice Data---------------------------------------------------
            Dim p As Integer
            Dim ds8 As New DataSet
            Dim dt8 As New DataTable
            n = k + m + 19
            htcount = htcount + 1 '------------ space Count
            If cmbSm.Text = "All" Then
                'Query = "SELECT INVH_DT,INVH_TXN_CODE,INVH_NO FROM OT_POS_INVOICE_HEAD_LOG, OT_POS_INVOICE_ITEM_LOG WHERE INVH_SYS_ID = PROD_INVI_INVH_SYS_ID AND INVH_STATUS = 4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'  AND INVH_FLEX_19 = '" & cmbShift.Text & "' AND INVH_FLEX_20 = '" & cmbCounter.Text & "' AND INVH_SM_CODE = '" & cmbSm.Text & "'  GROUP BY INVH_TXN_CODE, INVH_NO,INVH_DT ORDER BY INVH_DT, INVH_NO ASC"
                Query = "SELECT INVH_DT,INVH_TXN_CODE,INVH_NO FROM OT_POS_INVOICE_HEAD_LOG, OT_POS_INVOICE_ITEM_LOG WHERE INVH_SYS_ID = PROD_INVI_INVH_SYS_ID AND INVH_STATUS = 4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'  GROUP BY INVH_TXN_CODE, INVH_NO,INVH_DT ORDER BY INVH_DT, INVH_NO ASC"
            Else
                Query = "SELECT INVH_DT,INVH_TXN_CODE,INVH_NO FROM OT_POS_INVOICE_HEAD_LOG, OT_POS_INVOICE_ITEM_LOG WHERE INVH_SYS_ID = PROD_INVI_INVH_SYS_ID AND INVH_STATUS = 4 AND INVH_COMP_CODE = '001' AND INVH_LOCN_CODE = '" & strArrLoc(0) & "' AND INVH_DT >= '" & stDateval & "' AND INVH_DT <= '" & endDateval & "'  AND  INVH_SM_CODE = '" & cmbSm.Text & "'  GROUP BY INVH_TXN_CODE, INVH_NO,INVH_DT ORDER BY INVH_DT, INVH_NO ASC"
            End If
            'Test Query = "select PDH_COMP_CODE,PDH_LOCN_CODE,PDH_SYS_ID from OT_POS_DENO_HEAD"
            errLog.WriteToErrorLog("Holding Inv", Query, "")
            ds8 = db.SelectFromTableODBC(Query)
            dt8 = ds8.Tables("Table")
            For p = 0 To ds.Tables("Table").Rows.Count - 1
                Dim lbldeno7 As Label
                Dim lbldeno8 As Label
                Dim lbldeno9 As Label
                n = k + m + p + 19
                htcount = htcount + 20 '------------ Data Count
                lbldeno7 = New Label
                lbldeno8 = New Label
                lbldeno9 = New Label
                With lbldeno7
                    .Location = New Point(20, (n * 20))
                    .Name = "lbldeno7val" & n.ToString
                    .Size = New Size(250, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno8
                    .Location = New Point(270, (n * 20))
                    .Name = "lbldeno8val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                With lbldeno9
                    .Location = New Point(370, (n * 20))
                    .Name = "lbldeno9val" & n.ToString
                    .Size = New Size(100, 20)
                    .TextAlign = ContentAlignment.TopLeft
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                'MsgBox(dt.Rows(p).Item(0).ToString & dt.Rows(p).Item(1).ToString & dt.Rows(p).Item(2))
                lbldeno7.Text = dt8.Rows(p).Item(0).ToString
                lbldeno8.Text = dt8.Rows(p).Item(1).ToString
                lbldeno9.Text = dt8.Rows(p).Item(2)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno7)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno8)
                Me.pnl_detailsofEDR.Controls.Add(lbldeno9)
            Next
            btView.Enabled = False

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        End Try
    End Sub

    Private Sub btView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btView.Click
        'pnl_EDRDetails.Refresh()
        stDateval = dtstDate.Value.ToString("dd-MMM-yyyy")
        endDateval = dtendDate.Value.ToString("dd-MMM-yyyy")
        strArrLoc = cmbLocation.Text.Split("-")
        Call_invoice()
        Call_Cancel()
        Call_holding()
        Call_salesreturn()
        Call_Deleted()
        Call_DynamicPayment()
        pnlRptContainer.Visible = True
        lblfrmDate.Text = stDateval
        lbltoDate.Text = endDateval
        lblLoc.Text = strArrLoc(0)
        'lblLoc.Text = cmbLocation.Text
        'lblCounter.Text = cmbCounter.Text
        'lblShift.Text = cmbShift.Text
        lblSm.Text = cmbSm.Text
        pnl_endofthereport.Visible = False
    End Sub

    Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        'LoadLocation()
        'LoadCounter()
        'LoadShift()
        LoadSM()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim bmp = New Bitmap(pnl_EDRDetails.Width, pnl_EDRDetails.Height)

        'pnl_EDRDetails.DrawToBitmap(bmp, pnl_EDRDetails.ClientRectangle)

        ''bmp.Save("ImageName.png", System.Drawing.Imaging.ImageFormat.Png)

        'Dim doc As New PdfDocument()

        'doc.Pages.Add(New PdfPage())

        'Dim xgr As XGraphics = XGraphics.FromPdfPage(doc.Pages(0))

        'Dim img As XImage = XImage.FromGdiPlusImage(bmp)

        'xgr.DrawImage(img, 0, 0)

        'SaveFileDialog1.Filter = "PDF Files (*.pdf*)|*.pdf"

        'If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

        '    doc.Save(SaveFileDialog1.FileName)

        '    doc.Close()

        '    MsgBox("File has been saved successfully at '" + SaveFileDialog1.FileName + "'")

        'End If

        Try
            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
            PrintDialog1.AllowSomePages = True

            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
                PrintDocument1.Print()
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    'Private Sub WorkOrderStatusReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WorkOrderStatusReportToolStripMenuItem.Click
    '    Me.Close()
    '    frmWorkorderReport.MdiParent = frmHome
    '    frmWorkorderReport.Show()
    'End Sub



    'Private Sub RFMSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RFMSettingsToolStripMenuItem.Click
    '    Me.Close()
    '    frmRFMSettings.MdiParent = frmHome
    '    frmRFMSettings.Show()
    'End Sub

    'Private Sub IncomingPotentialsAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncomingPotentialsAnalysisToolStripMenuItem.Click
    '    Me.Close()
    '    frmPeakHrSales.MdiParent = frmHome
    '    frmPeakHrSales.Show()
    'End Sub

    'Private Sub SalesAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesAnalysisToolStripMenuItem.Click
    '    Me.Close()
    '    frmSalesAdhoc.MdiParent = frmHome
    '    frmSalesAdhoc.Show()
    'End Sub

    'Private Sub SalesmanPerformanceAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesmanPerformanceAnalysisToolStripMenuItem.Click
    '    Me.Close()
    '    frmSalesmanPerform.MdiParent = frmHome
    '    frmSalesmanPerform.Show()
    'End Sub

    'Private Sub ProductSalesAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductSalesAnalysisToolStripMenuItem.Click
    '    Me.Close()
    '    frmBrandSales.MdiParent = frmHome
    '    frmBrandSales.Show()
    'End Sub


    'Private Sub BenchmarkReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BenchmarkReportToolStripMenuItem.Click
    '    Me.Close()
    '    frmFastProducts.MdiParent = frmHome
    '    frmFastProducts.Show()
    'End Sub



    'Private Sub StockStatusReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockStatusReportToolStripMenuItem.Click
    '    Me.Close()
    '    frmStockReport.MdiParent = frmHome
    '    frmStockReport.Show()
    'End Sub
    'Private Sub DeliveryStatusReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    frmStatusinfoRep.MdiParent = frmHome
    '    frmStatusinfoRep.Show()
    'End Sub


    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    'pnlall.Controls.Clear()
    '    'pnlRptContainer.Visible = False
    '    btView.Enabled = True
    '    frmSalesAdhoc.refreshSalesSummary(sender, e)

    'End Sub

    'Private Sub DiscountReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiscountReportToolStripMenuItem.Click

    '    Me.Close()
    '    frmDiscountReport.MdiParent = frmHome
    '    frmDiscountReport.Show()
    'End Sub

    'Private Sub DeliveryStatusReportToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveryStatusReportToolStripMenuItem.Click
    '    Me.Close()
    '    frmStatusinfoRep.MdiParent = frmHome
    '    frmStatusinfoRep.Show()
    'End Sub

    'Private Sub PurchaseReportToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReportToolStripMenuItem1.Click
    '    Me.Close()
    '    frmPurchaseReport.MdiParent = frmHome
    '    frmPurchaseReport.Show()
    'End Sub

    Private Sub cmbCounter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadSM()
    End Sub

    Private Sub cmbShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadSM()
    End Sub

    Private Sub cmbCounter_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtendDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtendDate.ValueChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrpBox_SalesSummary.Enter

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GrpBox_SalesSummary.Refresh()
        pnl_EDRDetails.Refresh()
        Home.RefreshEndoftheday(sender, e)
    End Sub


    Private Sub cmbShift_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadSM()
    End Sub



    Private Sub btnrefreshEOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshEOD.Click

        Home.RefreshEndoftheday(sender, e)
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bmp = New Bitmap(pnl_EDRDetails.Width, pnl_EDRDetails.Height)
        pnl_EDRDetails.DrawToBitmap(bmp, pnl_EDRDetails.ClientRectangle)
        Dim x As Integer = e.MarginBounds.X - 125
        Dim y As Integer = e.MarginBounds.Y - 100
        e.Graphics.DrawImage(bmp, x, y)
        e.HasMorePages = False
    End Sub

End Class