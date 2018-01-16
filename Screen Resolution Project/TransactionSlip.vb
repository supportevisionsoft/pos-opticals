Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Math
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports System.Windows.Forms
Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class TransactionSlip
    Inherits System.Windows.Forms.Form

    Private TXNNO As String
    Private TXNTYPE As String
    Private rptLocationName As String = ""
    Private rptLocationAddr As String = ""
    Private rptLocationTaxTRN As String = ""
    Private rptLocatinNameArabic As String = ""
    Private rptLocatinAddrArabic As String = ""
    Private rptLocationPhone As String = ""
    Private rptLocationEmail As String = ""
    Private rptDate As String = ""
    Private rptCustomerName As String = ""
    Private rptSalesmanName As String = ""
    Private rptCustomerPhone As String = ""
    Private rptCustomerEmail As String = ""
    Private rptCustomerSONo As String = ""

    Public Property TXN_NO() As String
        Get
            Return TXNNO
        End Get
        Set(ByVal value As String)
            TXNNO = value
        End Set
    End Property

    Public Property TXN_TYPE() As String
        Get
            Return TXNTYPE
        End Get
        Set(ByVal value As String)
            TXNTYPE = value
        End Set
    End Property

    Dim db As New DBConnection

    Private pnlPages As New List(Of Panel)
    Private picReport As New List(Of PictureBox)
    Private lblLocnName As New List(Of Label)
    Private lblLocnAddr As New List(Of Label)
    Private lblLocnPhone As New List(Of Label)
    Private lblLocnEmail As New List(Of Label)
    Private pnlTxnTypeDecl As New List(Of Panel)
    Private lblTxnTypeDecl As New List(Of Label)
    Private pnlInvDetails As New List(Of Panel)
    Private pnlCustDetails As New List(Of Panel)
    Private pnlItemHeader As New List(Of Panel)
    Private pnlItemDetails As New List(Of Panel)
    Private pnlTotalDetails As New List(Of Panel)
    Private pnlGrandTotalDetails As New List(Of Panel)
    Private pnlDeclaration As New List(Of Panel)
    Private pnlAuthSign As New List(Of Panel)

    Private lblINVNo_KEY As New List(Of Label)
    Private lblINVNo_VALUE As New List(Of Label)
    Private lblINVDate_KEY As New List(Of Label)
    Private lblINVDate_VALUE As New List(Of Label)
    Private lblINVSONo_KEY As New List(Of Label)
    Private lblINVSONo_VALUE As New List(Of Label)
    Private lblINVSMNo_KEY As New List(Of Label)
    Private lblINVSMNo_VALUE As New List(Of Label)
    Private lblINVCustName_KEY As New List(Of Label)
    Private lblINVCustName_VALUE As New List(Of Label)
    Private lblINVCustPhone_KEY As New List(Of Label)
    Private lblINVCustPhone_VALUE As New List(Of Label)
    Private lblINVCustEmail_KEY As New List(Of Label)
    Private lblINVCustEmail_VALUE As New List(Of Label)

    Private lblINVAdvPaid_KEY As New List(Of Label)
    Private lblINVAdvPaid_VALUE As New List(Of Label)
    Private lblINVBalance_KEY As New List(Of Label)
    Private lblINVBalance_VALUE As New List(Of Label)
    Private lblINVSubTotal_KEY As New List(Of Label)
    Private lblINVSubTotal_VALUE As New List(Of Label)
    Private lblINVExpTotal_KEY As New List(Of Label)
    Private lblINVExpTotal_VALUE As New List(Of Label)
    Private lblINVDisTotal_KEY As New List(Of Label)
    Private lblINVDisTotal_VALUE As New List(Of Label)
    Private lblINVTaxTotal_KEY As New List(Of Label)
    Private lblINVTaxTotal_VALUE As New List(Of Label)
    Private lblINVTaxTRN_KEY_VALUE As New List(Of Label)

    Private lblRptSNOHeader As New List(Of Label)
    Private lblRptItemCodeHeader As New List(Of Label)
    Private lblRptUOMHeader As New List(Of Label)
    Private lblRptRateHeader As New List(Of Label)
    Private lblRptQtyHeader As New List(Of Label)
    Private lblRptAmtHeader As New List(Of Label)

    Private pnlRows As New List(Of Panel)

    Private lblRptSNOValue As New List(Of Label)
    Private lblRptItemCodeValue As New List(Of Label)
    Private lblRptItemDescValue As New List(Of Label)
    Private lblRptItemArabicValue As New List(Of Label)
    Private lblRptUOMValue As New List(Of Label)
    Private lblRptRateValue As New List(Of Label)
    Private lblRptQtyValue As New List(Of Label)
    Private lblRptAmtValue As New List(Of Label)

    Private lblRptEEO As New List(Of Label)
    Private lblRptGrandTotal_KEY As New List(Of Label)
    Private lblRptGrandTotal_VALUE As New List(Of Label)

    Private lblDeclarationHeader As New List(Of Label)
    Private lblDeclaration As New List(Of Label)
    Private lblAuthSignature As New List(Of Label)

    Private pnlFooter As New List(Of Panel)
    Private lblFooterLine1 As New List(Of Label)
    Private lblFooterLine2 As New List(Of Label)
    Private lblFooterLine3 As New List(Of Label)

    Dim totalDiscountamt As Double = 0
    Dim totalExpenseamt As Double = 0
    Dim subtotalamt As Double = 0
    Dim totalTaxAmount As Double = 0
    Dim taxPercentageValue As String = ""

    Private currentPage As String = ""
    Private currentItemPanel As String = ""
    Private currentPageNumber As String = ""

    Dim _page As Integer
    Dim bitmaps As New List(Of Bitmap)

    Private Sub TransactionSlip_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Dock = DockStyle.Fill
            pnlOuterContainer.Controls.Clear()
            If TXN_TYPE = "Invoice" Then
                loadReportInvoice()
            ElseIf TXN_TYPE = "Sales Order" Then
                loadReportSalesOrder()
            ElseIf TXN_TYPE = "Sales Invoice" Then
                loadReportSalesInvoice()
            ElseIf TXN_TYPE = "Sales Return" Then
                loadReportSalesReturn()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub loadReportSalesReturn()
        Try
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            stQuery = stQuery + " select rownum,b.CSRH_NO ,to_char( b.CSRH_DT,'DD/MM/YYYY') as InvoiceDate, "
            stQuery = stQuery + " d.ADDR_LINE_1 as locn_name,"
            stQuery = stQuery + " d.ADDR_LINE_2|| ' ' || d.ADDR_LINE_3"
            stQuery = stQuery + " as Location_Address,"
            stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
            stQuery = stQuery + " case nvl(b.CSRH_FLEX_03,0)"
            stQuery = stQuery + " when '0' then (select cust_name from om_customer where cust_code = b.CSRH_CUST_CODE)"
            stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where PM_CUST_NO = b.CSRH_FLEX_03)"
            stQuery = stQuery + " end as CustName,"
            stQuery = stQuery + " b.CSRH_BILL_ADDR_LINE_1||' '||b.CSRH_BILL_ADDR_LINE_2||' '||b.CSRH_BILL_COUNTRY_CODE as billing_addr,"
            stQuery = stQuery + " b.CSRH_BILL_TEL as billing_phone, b.CSRH_BILL_EMAIL as billing_email,"
            stQuery = stQuery + " b.CSRH_SHIP_ADDR_LINE_1||' '||b.CSRH_SHIP_ADDR_LINE_2||' '||b.CSRH_SHIP_COUNTRY_CODE as shipping_addr,"
            stQuery = stQuery + " a.CSRI_ITEM_CODE as ItemCode"
            stQuery = stQuery + ",a.CSRI_ITEM_DESC as ItemDesc,"
            stQuery = stQuery + " a.CSRI_UOM_CODE as ItmUOM,"
            stQuery = stQuery + " a.CSRI_RATE as ItmPrice ,"
            stQuery = stQuery + " a.CSRI_QTY as ItmQty,"
            'stQuery = stQuery + " a.CSRI_FC_VAL as ItmAmt,"
            stQuery = stQuery & " a.CSRI_RATE * a.CSRI_QTY as ItmAmt, "
            stQuery = stQuery + " nvl((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery & " nvl((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,CSRH_SM_CODE as salesman,CSRH_FLEX_03 as pm_cust_no, (select ITEM_BL_LONG_NAME_1 from om_item where ITEM_CODE=a.CSRI_ITEM_CODE) as ITEM_NAME_ARABIC, c.LOCN_BL_NAME as locnArabicName, d.ADDR_LINE_4||' '||d.ADDR_LINE_5 as locnArabicAddress, "
            stQuery = stQuery + " nvl((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TAX')),0) as taxamt, "
            stQuery = stQuery & " c.LOCN_FLEX_11 as taxTRN "
            stQuery = stQuery + " from "
            stQuery = stQuery + " OT_CUST_SALE_RET_HEAD b,OT_CUST_SALE_RET_ITEM a,om_location c,om_address d"
            stQuery = stQuery + " where b.CSRH_NO = " + TXN_NO.ToString + " and"
            stQuery = stQuery + " b.CSRH_SYS_ID = a.CSRI_CSRH_SYS_ID and"
            stQuery = stQuery + " b.CSRH_LOCN_CODE = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Sales Return Report Query", stQuery, "")
            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            rptLocatinNameArabic = ds.Tables("Table").Rows.Item(0).Item(23).ToString
            rptLocatinAddrArabic = ds.Tables("Table").Rows.Item(0).Item(24).ToString
            rptLocationTaxTRN = ds.Tables("Table").Rows.Item(0).Item(26).ToString
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            Dim stSalesQuery As String = ""
            stSalesQuery = "Select SM_NAME from om_salesman where SM_CODE='" & ds.Tables("Table").Rows.Item(0).Item(20).ToString & "'"
            Dim dsSal As DataSet = db.SelectFromTableODBC(stSalesQuery)
            If dsSal.Tables("Table").Rows.Count > 0 Then
                rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString & " - " & dsSal.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            If ds.Tables("Table").Rows.Item(0).Item(21).ToString = "" Then
                rptCustomerName = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                rptCustomerPhone = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                rptCustomerEmail = ds.Tables("Table").Rows.Item(0).Item(10).ToString
            Else
                Dim stQueryPatient As String
                stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + ds.Tables("Table").Rows.Item(0).Item(21).ToString + "'"
                Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                If dsP.Tables("Table").Rows.Count > 0 Then
                    rptCustomerName = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                    rptCustomerPhone = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                    rptCustomerEmail = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                End If
            End If

            CreatePage()
            Dim itemlines As Integer = 0
            While rowcount > 0
                If Not i = 1 Then
                    If i Mod 3 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 59)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 59)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRows.Add(pnl)
                Me.Controls.Find(currentItemPanel, True)(0).Controls.Add(pnl)

                Dim lbl As Label
                lbl = New Label
                n = lblRptSNOValue.Count
                With lbl
                    .Location = New Point(0, 11)
                    .Name = "lblRptSNOValue" & n.ToString
                    .Size = New Size(31, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = (i + 1).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptSNOValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemCodeValue.Count
                With lbl
                    .Location = New Point(32, 5)
                    .Name = "lblRptItemCodeValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(12).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemCodeValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemDescValue.Count
                With lbl
                    .Location = New Point(32, 19)
                    .Name = "lblRptItemDescValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(13).ToString & ")"
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemDescValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemArabicValue.Count
                With lbl
                    .Location = New Point(32, 35)
                    .Name = "lblRptItemArabicValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(22).ToString & ")"
                    .Font = New Font("Tahoma", 9, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemArabicValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptUOMValue.Count
                With lbl
                    .Location = New Point(280, 11)
                    .Name = "lblRptUOMValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(14).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptUOMValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptRateValue.Count
                With lbl
                    .Location = New Point(325, 11)
                    .Name = "lblRptRateValue" & n.ToString
                    .Size = New Size(59, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = row.Item(15).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptRateValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptQtyValue.Count
                With lbl
                    .Location = New Point(385, 11)
                    .Name = "lblRptQtyValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(16).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptQtyValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptAmtValue.Count
                With lbl
                    .Location = New Point(430, 11)
                    .Name = "lblRptAmtValue" & n.ToString
                    .Size = New Size(89, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = Convert.ToDouble(row.Item(17).ToString).ToString("0.000")
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptAmtValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                totalDiscountamt = totalDiscountamt + Convert.ToDouble(row.Item(18).ToString)
                totalExpenseamt = totalExpenseamt + Convert.ToDouble(row.Item(19).ToString)
                subtotalamt = subtotalamt + Convert.ToDouble(row.Item(17).ToString)
                totalTaxAmount = totalTaxAmount + Convert.ToDouble(row.Item(25).ToString)

                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
            Me.Controls.Find("lblRptGrandTotal_VALUE" & currentPageNumber, True)(0).Text = Round((subtotalamt + totalExpenseamt) - totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVTaxTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalTaxAmount, 3).ToString("0.000")

            CreationPageBottom()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub loadReportSalesInvoice()
        Try
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            stQuery = stQuery + " select rownum,b.invh_no ,to_char( b.invh_dt,'DD/MM/YYYY') as InvoiceDate, "
            stQuery = stQuery + " d.ADDR_LINE_1 as locn_name,"
            stQuery = stQuery + " d.ADDR_LINE_2|| ' ' || d.ADDR_LINE_3"
            stQuery = stQuery + " as Location_Address,"
            stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
            stQuery = stQuery + " case nvl(b.INVH_FLEX_03,0)"
            stQuery = stQuery + " when '0' then (select cust_name from om_customer where cust_code = b.invh_cust_code)"
            stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where PM_CUST_NO = b.INVH_FLEX_03)"
            stQuery = stQuery + " end as CustName,"
            stQuery = stQuery + " b.invh_BILL_ADDR_LINE_1||' '||b.invh_BILL_ADDR_LINE_2||' '||b.invh_BILL_COUNTRY_CODE as billing_addr,"
            stQuery = stQuery + " b.INVH_BILL_TEL as billing_phone, b.invh_BILL_EMAIL as billing_email,"
            stQuery = stQuery + " b.invh_SHIP_ADDR_LINE_1||' '||b.invh_SHIP_ADDR_LINE_2||' '||b.invh_SHIP_COUNTRY_CODE as shipping_addr,"
            stQuery = stQuery + " a.INVI_ITEM_CODE as ItemCode"
            stQuery = stQuery + ",a.INVI_ITEM_DESC as ItemDesc,"
            stQuery = stQuery + " a.INVI_UOM_CODE as ItmUOM,"
            stQuery = stQuery + " a.INVI_PL_RATE as ItmPrice ,"
            stQuery = stQuery + " a.INVI_QTY as ItmQty,"
            'stQuery = stQuery + " a.INVI_FC_VAL as ItmAmt,"
            stQuery = stQuery & " a.INVI_PL_RATE * a.INVI_QTY as ItmAmt, "
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,INVH_SM_CODE as salesman,INVH_FLEX_03 as pmcustno,INVH_REF_NO as refno, (select ITEM_BL_LONG_NAME_1 from om_item where ITEM_CODE=a.INVI_ITEM_CODE) as ITEM_NAME_ARABIC, c.LOCN_BL_NAME as locnArabicName, d.ADDR_LINE_4||' '||d.ADDR_LINE_5 as locnArabicAddress, "
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TAX')),0) as taxamt, "
            stQuery = stQuery + " c.LOCN_FLEX_11 as taxTRN "
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
            stQuery = stQuery + " where b.invh_no = " & TXN_NO & " and"
            stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
            stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Direct Invoice Report Query", stQuery, "")
            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            rptLocatinNameArabic = ds.Tables("Table").Rows.Item(0).Item(23).ToString
            rptLocatinAddrArabic = ds.Tables("Table").Rows.Item(0).Item(24).ToString
            rptLocationTaxTRN = ds.Tables("Table").Rows.Item(0).Item(27).ToString
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            Dim stSalesQuery As String = ""
            stSalesQuery = "Select SM_NAME from om_salesman where SM_CODE='" & ds.Tables("Table").Rows.Item(0).Item(20).ToString & "'"
            Dim dsSal As DataSet = db.SelectFromTableODBC(stSalesQuery)
            If dsSal.Tables("Table").Rows.Count > 0 Then
                rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString & " - " & dsSal.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            'rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString
            rptCustomerSONo = ds.Tables("Table").Rows.Item(0).Item(22).ToString
            If ds.Tables("Table").Rows.Item(0).Item(21).ToString = "" Then
                rptCustomerName = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                rptCustomerPhone = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                rptCustomerEmail = ds.Tables("Table").Rows.Item(0).Item(10).ToString
            Else
                Dim stQueryPatient As String
                stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + ds.Tables("Table").Rows.Item(0).Item(21).ToString + "'"
                Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                If dsP.Tables("Table").Rows.Count > 0 Then
                    rptCustomerName = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                    rptCustomerPhone = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                    rptCustomerEmail = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                End If
            End If

            CreatePage()
            Dim itemlines As Integer = 0
            While rowcount > 0
                If Not i = 1 Then
                    If i Mod 3 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 59)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 59)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRows.Add(pnl)
                Me.Controls.Find(currentItemPanel, True)(0).Controls.Add(pnl)

                Dim lbl As Label
                lbl = New Label
                n = lblRptSNOValue.Count
                With lbl
                    .Location = New Point(0, 11)
                    .Name = "lblRptSNOValue" & n.ToString
                    .Size = New Size(31, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = (i + 1).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptSNOValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemCodeValue.Count
                With lbl
                    .Location = New Point(32, 5)
                    .Name = "lblRptItemCodeValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(12).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemCodeValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemDescValue.Count
                With lbl
                    .Location = New Point(32, 19)
                    .Name = "lblRptItemDescValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(13).ToString & ")"
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemDescValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemArabicValue.Count
                With lbl
                    .Location = New Point(32, 35)
                    .Name = "lblRptItemArabicValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(23).ToString & ")"
                    .Font = New Font("Tahoma", 9, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemArabicValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptUOMValue.Count
                With lbl
                    .Location = New Point(280, 11)
                    .Name = "lblRptUOMValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(14).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptUOMValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptRateValue.Count
                With lbl
                    .Location = New Point(325, 11)
                    .Name = "lblRptRateValue" & n.ToString
                    .Size = New Size(59, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = row.Item(15).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptRateValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptQtyValue.Count
                With lbl
                    .Location = New Point(385, 11)
                    .Name = "lblRptQtyValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(16).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptQtyValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptAmtValue.Count
                With lbl
                    .Location = New Point(430, 11)
                    .Name = "lblRptAmtValue" & n.ToString
                    .Size = New Size(89, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = Convert.ToDouble(row.Item(17).ToString).ToString("0.000")
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptAmtValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                totalDiscountamt = totalDiscountamt + Convert.ToDouble(row.Item(18).ToString)
                totalExpenseamt = totalExpenseamt + Convert.ToDouble(row.Item(19).ToString)
                subtotalamt = subtotalamt + Convert.ToDouble(row.Item(17).ToString)
                totalTaxAmount = totalTaxAmount + Convert.ToDouble(row.Item(26).ToString)

                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
            Dim grandtotal As Double = 0
            grandtotal = (subtotalamt + totalExpenseamt) - totalDiscountamt
            Me.Controls.Find("lblRptGrandTotal_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal, 3).ToString("0.000")
            Me.Controls.Find("lblINVTaxTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalTaxAmount, 3).ToString("0.000")

            Dim stBalanceQuery As String
            stBalanceQuery = "select nvl(sum(pinvp_fc_val),0) as advance from ot_pos_so_payment a,ot_so_head b where b.soh_no = " + ds.Tables("Table").Rows.Item(0).Item(22).ToString + " and b.soh_sys_id = a.pinvp_invh_sys_id "
            Dim dsb As DataSet = db.SelectFromTableODBC(stBalanceQuery)
            If dsb.Tables("Table").Rows.Count > 0 Then
                errLog.WriteToErrorLog("BalanceCheck Query", stBalanceQuery, "")
                If Not dsb.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                    Me.Controls.Find("lblINVAdvPaid_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                    Me.Controls.Find("lblINVBalance_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal - Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString), 3).ToString("0.000")

                    'lblRptAdvancedPaid.Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                    'MsgBox(lblRptAdvancedPaid.Text)
                Else
                    Me.Controls.Find("lblINVAdvPaid_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Text = Convert.ToDouble("0").ToString("0.000")
                    Me.Controls.Find("lblINVBalance_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal - Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString), 3).ToString("0.000")

                    'lblRptAdvancedPaid.Text = "0"
                    ' MsgBox(lblRptAdvancedPaid.Text)
                End If
            End If

            CreationPageBottom()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub loadReportSalesOrder()
        Try
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            stQuery = stQuery + " select rownum,b.soh_no ,to_char( b.soh_cr_dt,'DD/MM/YYYY HH12:MI AM') as InvoiceDate,"
            stQuery = stQuery + " d.ADDR_LINE_1 as locn_name,"
            stQuery = stQuery + " d.ADDR_LINE_2|| ' ' || d.ADDR_LINE_3"
            stQuery = stQuery + " as Location_Address,"
            stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
            stQuery = stQuery + " case nvl(b.soH_FLEX_03,0) when '0' then (select cust_name from om_customer where cust_code = b.soh_cust_code)"
            stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where pm_cust_code = b.soh_flex_03) end as CustName,"
            stQuery = stQuery + " b.soh_BILL_ADDR_LINE_1||' '||b.soh_BILL_ADDR_LINE_2||' '||b.soh_BILL_COUNTRY_CODE as billing_addr,b.soH_BILL_TEL as billing_phone, b.soh_BILL_EMAIL as billing_email, b.soh_SHIP_ADDR_LINE_1||' '||b.soh_SHIP_ADDR_LINE_2||' '||b.soh_SHIP_COUNTRY_CODE as shipping_addr,"
            stQuery = stQuery + " a.soI_ITEM_CODE as ItemCode,a.soI_ITEM_DESC as ItemDesc,a.soI_UOM_CODE as ItmUOM,a.soI_PL_RATE as ItmPrice ,a.soI_QTY as ItmQty,a.soI_PL_RATE * a.soI_QTY as ItmAmt,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt, "
            stQuery = stQuery & " nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,SOH_SM_CODE as salesman,SOH_FLEX_03 as pmcustno, (select ITEM_BL_LONG_NAME_1 from om_item where ITEM_CODE=a.SOI_ITEM_CODE) as SOI_ITEM_NAME_ARABIC, c.LOCN_BL_NAME as locnArabicName, d.ADDR_LINE_4||' '||d.ADDR_LINE_5 as locnArabicAddress, "
            stQuery = stQuery & " nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TAX')),0) as taxamt, "
            stQuery = stQuery & " c.LOCN_FLEX_11 as taxTRN "
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_so_head b,ot_so_item a, om_location c,om_address d"
            stQuery = stQuery + " where b.soh_no = " + TXN_NO.ToString + " and b.soh_sys_id = a.soi_soh_sys_id and b.soh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Direct Invoice Report Query", stQuery, "")
            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            rptLocatinNameArabic = ds.Tables("Table").Rows.Item(0).Item(23).ToString
            rptLocatinAddrArabic = ds.Tables("Table").Rows.Item(0).Item(24).ToString
            rptLocationTaxTRN = ds.Tables("Table").Rows.Item(0).Item(26).ToString
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            Dim stSalesQuery As String = ""
            stSalesQuery = "Select SM_NAME from om_salesman where SM_CODE='" & ds.Tables("Table").Rows.Item(0).Item(20).ToString & "'"
            Dim dsSal As DataSet = db.SelectFromTableODBC(stSalesQuery)
            If dsSal.Tables("Table").Rows.Count > 0 Then
                rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString & " - " & dsSal.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            'rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString
            If ds.Tables("Table").Rows.Item(0).Item(21).ToString = "" Then
                rptCustomerName = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                rptCustomerPhone = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                rptCustomerEmail = ds.Tables("Table").Rows.Item(0).Item(10).ToString
            Else
                Dim stQueryPatient As String
                stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + ds.Tables("Table").Rows.Item(0).Item(21).ToString + "'"
                Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                If dsP.Tables("Table").Rows.Count > 0 Then
                    rptCustomerName = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                    rptCustomerPhone = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                    rptCustomerEmail = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                End If
            End If

            CreatePage()
            Dim itemlines As Integer = 0
            While rowcount > 0
                If Not i = 1 Then
                    If i Mod 3 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 57)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 57)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRows.Add(pnl)
                Me.Controls.Find(currentItemPanel, True)(0).Controls.Add(pnl)

                Dim lbl As Label
                lbl = New Label
                n = lblRptSNOValue.Count
                With lbl
                    .Location = New Point(0, 11)
                    .Name = "lblRptSNOValue" & n.ToString
                    .Size = New Size(31, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = (i + 1).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptSNOValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemCodeValue.Count
                With lbl
                    .Location = New Point(32, 5)
                    .Name = "lblRptItemCodeValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(12).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemCodeValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemDescValue.Count
                With lbl
                    .Location = New Point(32, 19)
                    .Name = "lblRptItemDescValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(13).ToString & ")"
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemDescValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemArabicValue.Count
                With lbl
                    .Location = New Point(32, 35)
                    .Name = "lblRptItemArabicValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(22).ToString & ")"
                    .Font = New Font("Tahoma", 9, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemArabicValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptUOMValue.Count
                With lbl
                    .Location = New Point(280, 11)
                    .Name = "lblRptUOMValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(14).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptUOMValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptRateValue.Count
                With lbl
                    .Location = New Point(325, 11)
                    .Name = "lblRptRateValue" & n.ToString
                    .Size = New Size(59, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = row.Item(15).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptRateValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptQtyValue.Count
                With lbl
                    .Location = New Point(385, 11)
                    .Name = "lblRptQtyValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(16).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptQtyValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptAmtValue.Count
                With lbl
                    .Location = New Point(430, 11)
                    .Name = "lblRptAmtValue" & n.ToString
                    .Size = New Size(89, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = Convert.ToDouble(row.Item(17).ToString).ToString("0.000")
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptAmtValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                totalDiscountamt = totalDiscountamt + Convert.ToDouble(row.Item(18).ToString)
                totalExpenseamt = totalExpenseamt + Convert.ToDouble(row.Item(19).ToString)
                subtotalamt = subtotalamt + Convert.ToDouble(row.Item(17).ToString)
                totalTaxAmount = totalTaxAmount + Convert.ToDouble(row.Item(25).ToString)

                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
            Dim grandtotal As Double = 0
            grandtotal = (subtotalamt + totalExpenseamt) - totalDiscountamt
            Me.Controls.Find("lblRptGrandTotal_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal, 3).ToString("0.000")
            Me.Controls.Find("lblINVTaxTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalTaxAmount, 3).ToString("0.000")

            Dim stBalanceQuery As String
            stBalanceQuery = "select nvl(sum(pinvp_fc_val),0) as advance from ot_pos_so_payment a,ot_so_head b where b.soh_no = " + TXN_NO.ToString + " and b.soh_sys_id = a.pinvp_invh_sys_id "
            Dim dsb As DataSet = db.SelectFromTableODBC(stBalanceQuery)
            If dsb.Tables("Table").Rows.Count > 0 Then
                errLog.WriteToErrorLog("BalanceCheck Query", stBalanceQuery, "")
                If Not dsb.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                    Me.Controls.Find("lblINVAdvPaid_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                    Me.Controls.Find("lblINVBalance_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal - Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString), 3).ToString("0.000")

                    'lblRptAdvancedPaid.Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                    'MsgBox(lblRptAdvancedPaid.Text)
                Else
                    Me.Controls.Find("lblINVAdvPaid_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVAdvPaid_VALUE" & currentPageNumber, True)(0).Text = Convert.ToDouble("0").ToString("0.000")
                    Me.Controls.Find("lblINVBalance_KEY" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Visible = True
                    Me.Controls.Find("lblINVBalance_VALUE" & currentPageNumber, True)(0).Text = Round(grandtotal - Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString), 3).ToString("0.000")

                    'lblRptAdvancedPaid.Text = "0"
                    ' MsgBox(lblRptAdvancedPaid.Text)
                End If
            End If


            CreationPageBottom()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Function getConvertedArabicText(ByVal textString As String) As String
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader

        request = DirectCast(WebRequest.Create("https://translation.googleapis.com/language/translate/v2?key=AIzaSyA8WG0q_cZUwqJqdpOo3xFjMPCsSKbXT5I&source=en&target=ar&q=" + textString), HttpWebRequest)

        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        Dim rawresp As String
        rawresp = reader.ReadToEnd()
        Dim ser As JObject = JObject.Parse(rawresp)
        Dim data As List(Of JToken) = ser.Children().ToList
        For Each item As JProperty In data
            item.CreateReader()
            Select Case item.Name
                Case "data"
                    For Each transitem As JProperty In item.Value
                        transitem.CreateReader()
                        Dim transTextArray As JArray
                        transTextArray = transitem.Value
                        Dim transTextVal As JObject = transTextArray.Item(0)
                        Dim textdata As List(Of JToken) = transTextVal.Children().ToList
                        For Each textvaldata As JProperty In textdata
                            textvaldata.CreateReader()
                            Return textvaldata.Value.ToString
                        Next
                    Next
            End Select
        Next
        Return ""
    End Function

    Private Sub loadReportInvoice()
        Try
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            stQuery = stQuery + " select rownum,b.invh_no ,to_char( b.invh_cr_dt,'DD/MM/YYYY HH12:MI AM') as InvoiceDate, "
            stQuery = stQuery + " d.ADDR_LINE_1 as locn_name,"
            stQuery = stQuery + " d.ADDR_LINE_2|| ' ' || d.ADDR_LINE_3"
            stQuery = stQuery + " as Location_Address,"
            stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
            stQuery = stQuery + " case nvl(b.INVH_FLEX_03,0)"
            stQuery = stQuery + " when '0' then (select cust_name from om_customer where cust_code = b.invh_cust_code)"
            stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where PM_CUST_NO = b.INVH_FLEX_03)"
            stQuery = stQuery + " end as CustName,"
            stQuery = stQuery + " b.invh_BILL_ADDR_LINE_1||' '||b.invh_BILL_ADDR_LINE_2||' '||b.invh_BILL_COUNTRY_CODE as billing_addr,"
            stQuery = stQuery + " b.INVH_BILL_TEL as billing_phone, b.invh_BILL_EMAIL as billing_email,"
            stQuery = stQuery + " b.invh_SHIP_ADDR_LINE_1||' '||b.invh_SHIP_ADDR_LINE_2||' '||b.invh_SHIP_COUNTRY_CODE as shipping_addr,"
            stQuery = stQuery + " a.INVI_ITEM_CODE as ItemCode"
            stQuery = stQuery + ",a.INVI_ITEM_DESC as ItemDesc,"
            stQuery = stQuery + " a.INVI_UOM_CODE as ItmUOM,"
            stQuery = stQuery + " a.INVI_PL_RATE as ItmPrice ,"
            stQuery = stQuery + " a.INVI_QTY as ItmQty,"
            'stQuery = stQuery + " a.INVI_FC_VAL as ItmAmt,"
            stQuery = stQuery + " (a.INVI_PL_RATE * a.INVI_QTY) as ItmAmt, "
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,INVH_SM_CODE as salesman,INVH_FLEX_03 as pmcustno, (select ITEM_BL_LONG_NAME_1 from om_item where ITEM_CODE=a.INVI_ITEM_CODE) as SOI_ITEM_NAME_ARABIC, c.LOCN_BL_NAME as locnArabicName, d.ADDR_LINE_4||' '||d.ADDR_LINE_5 as locnArabicAddress, "
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TAX')),0) as taxamt, "
            stQuery = stQuery + " nvl((select ITED_TED_RATE from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TAX')),0) as taxpercentage, "
            stQuery = stQuery + " c.LOCN_FLEX_11 as taxTRN "
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
            stQuery = stQuery + " where b.invh_no = " & TXN_NO & " and"
            stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
            stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Direct Invoice Report Query", stQuery, "")
            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            'rptLocationName = getConvertedArabicText(rptLocationName)
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            rptLocatinNameArabic = ds.Tables("Table").Rows.Item(0).Item(23).ToString
            rptLocatinAddrArabic = ds.Tables("Table").Rows.Item(0).Item(24).ToString
            rptLocationTaxTRN = ds.Tables("Table").Rows.Item(0).Item(27).ToString
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            Dim stSalesQuery As String = ""
            stSalesQuery = "Select SM_NAME from om_salesman where SM_CODE='" & ds.Tables("Table").Rows.Item(0).Item(20).ToString & "'"
            Dim dsSal As DataSet = db.SelectFromTableODBC(stSalesQuery)
            If dsSal.Tables("Table").Rows.Count > 0 Then
                rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString & " - " & dsSal.Tables("Table").Rows.Item(0).Item(0).ToString
                'rptSalesmanName = getConvertedArabicText(rptSalesmanName)
            End If
            'rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString
            If ds.Tables("Table").Rows.Item(0).Item(21).ToString = "" Then
                rptCustomerName = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                'rptCustomerName = getConvertedArabicText(rptCustomerName)
                rptCustomerPhone = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                rptCustomerEmail = ds.Tables("Table").Rows.Item(0).Item(10).ToString
            Else
                Dim stQueryPatient As String
                stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + ds.Tables("Table").Rows.Item(0).Item(21).ToString + "'"
                Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                If dsP.Tables("Table").Rows.Count > 0 Then
                    rptCustomerName = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                    'rptCustomerName = getConvertedArabicText(rptCustomerName)
                    rptCustomerPhone = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                    rptCustomerEmail = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                End If
            End If

            CreatePage()
            Dim itemlines As Integer = 0
            While rowcount > 0
                If Not i = 1 Then
                    If i Mod 3 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 59)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 59)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRows.Add(pnl)
                Me.Controls.Find(currentItemPanel, True)(0).Controls.Add(pnl)

                Dim lbl As Label
                lbl = New Label
                n = lblRptSNOValue.Count
                With lbl
                    .Location = New Point(0, 11)
                    .Name = "lblRptSNOValue" & n.ToString
                    .Size = New Size(31, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = (i + 1).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptSNOValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemCodeValue.Count
                With lbl
                    .Location = New Point(32, 5)
                    .Name = "lblRptItemCodeValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(12).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemCodeValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemDescValue.Count
                With lbl
                    .Location = New Point(32, 19)
                    .Name = "lblRptItemDescValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(13).ToString & ")"
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemDescValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptItemArabicValue.Count
                With lbl
                    .Location = New Point(32, 35)
                    .Name = "lblRptItemArabicValue" & n.ToString
                    .Size = New Size(247, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = "(" & row.Item(22).ToString & ")"
                    .Font = New Font("Tahoma", 9, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptItemArabicValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptUOMValue.Count
                With lbl
                    .Location = New Point(280, 11)
                    .Name = "lblRptUOMValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(14).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptUOMValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptRateValue.Count
                With lbl
                    .Location = New Point(325, 11)
                    .Name = "lblRptRateValue" & n.ToString
                    .Size = New Size(59, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = row.Item(15).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptRateValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptQtyValue.Count
                With lbl
                    .Location = New Point(385, 11)
                    .Name = "lblRptQtyValue" & n.ToString
                    .Size = New Size(44, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = row.Item(16).ToString
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptQtyValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                lbl = New Label
                n = lblRptAmtValue.Count
                With lbl
                    .Location = New Point(430, 11)
                    .Name = "lblRptAmtValue" & n.ToString
                    .Size = New Size(89, 15)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleRight
                    .Text = Convert.ToDouble(row.Item(17).ToString).ToString("0.000")
                    .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
                    '.BorderStyle = BorderStyle.FixedSingle
                End With
                Me.lblRptAmtValue.Add(lbl)
                Me.Controls.Find("pnlRows" & n.ToString, True)(0).Controls.Add(lbl)

                totalDiscountamt = totalDiscountamt + Convert.ToDouble(row.Item(18).ToString)
                totalExpenseamt = totalExpenseamt + Convert.ToDouble(row.Item(19).ToString)
                subtotalamt = subtotalamt + Convert.ToDouble(row.Item(17).ToString)
                totalTaxAmount = totalTaxAmount + Convert.ToDouble(row.Item(25).ToString)
                taxPercentageValue = row.Item(26).ToString
                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVTaxTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalTaxAmount, 3).ToString("0.000")
            'Me.Controls.Find("lblINVTaxTotal_KEY" & currentPageNumber, True)(0).Text = taxPercentageValue & "% " & Me.Controls.Find("lblINVTaxTotal_KEY" & currentPageNumber, True)(0).Text
            Me.Controls.Find("lblRptGrandTotal_VALUE" & currentPageNumber, True)(0).Text = Round((subtotalamt + totalExpenseamt) - totalDiscountamt, 3).ToString("0.000")

            CreationPageBottom()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub CreationPageBottom()
        pnlOuterContainer.AutoScrollPosition = New System.Drawing.Point(0, 0)

        Dim pnl As Panel
        Dim n As Integer
        n = pnlPages.Count
        pnl = New Panel

        With pnl
            .Location = New Point(5, (n * 864) + ((n + 1) * 5))
            .Name = "pnlPageBottom" & n.ToString
            'currentpage = "pnlReportPage" & n.ToString
            .Size = New Size(760, 5)
            .BorderStyle = BorderStyle.None
            '.BackColor = Color.White
        End With

        Me.pnlOuterContainer.Controls.Add(pnl)
    End Sub

    Private Sub CreatePage()

        pnlOuterContainer.AutoScrollPosition = New System.Drawing.Point(0, 0)

        Dim pnl As Panel
        Dim n As Integer
        n = pnlPages.Count
        pnl = New Panel

        With pnl
            If n = 0 Then
                .Location = New Point(5, (n * 864) + 5)
            Else
                .Location = New Point(5, (n * 864) + ((n + 1) * 5))
            End If
            .Name = "pnlPage" & n.ToString
            currentPage = "pnlPage" & n.ToString
            currentPageNumber = n.ToString
            .Size = New Size(760, 984)
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.White
            .Cursor = Cursors.Hand
        End With
        Dim ttPage As New ToolTip()
        ttPage.SetToolTip(pnl, "Page " & (n + 1).ToString)
        Me.pnlPages.Add(pnl)
        Me.pnlOuterContainer.Controls.Add(pnl)

        Dim pic As New PictureBox
        n = picReport.Count
        With pic
            .Location = New Point(344, 0)
            .Name = "picReport" & n.ToString
            .Size = New Size(100, 57)
            If locationLogo.Equals("") Or locationLogo.Length < 1 Or Not File.Exists(Application.StartupPath & "\LOGOS\" & locationLogo) Then
                .BackgroundImage = My.Resources.clientlogo12
                .BackgroundImageLayout = ImageLayout.Stretch
            Else
                .BackgroundImage = Image.FromFile(Application.StartupPath & "\LOGOS\" & locationLogo)
                .BackgroundImageLayout = ImageLayout.Stretch
            End If

            .SizeMode = PictureBoxSizeMode.Normal
        End With
        Me.picReport.Add(pic)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pic)

        Dim lbl As Label

        lbl = New Label
        n = lblLocnName.Count
        With lbl
            .Location = New Point(138, 58)
            .Name = "lblLocnName" & n.ToString
            .Size = New Size(504, 15)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = rptLocationName
            .Font = New Font("Arial", 9, FontStyle.Bold)
        End With
        Me.lblLocnName.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocnAddr.Count
        With lbl
            .Location = New Point(138, 74)
            .Name = "lblLocnAddr" & n.ToString
            .Size = New Size(504, 15)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = rptLocationAddr
            .Font = New Font("Arial", 8, FontStyle.Bold)
        End With
        Me.lblLocnAddr.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocnPhone.Count
        With lbl
            '.BorderStyle = BorderStyle.FixedSingle
            .Location = New Point(138, 90)
            .Font = New Font("Arial", 9, FontStyle.Bold)
            .Name = "lblLocnPhone" & n.ToString
            .Size = New Size(504, 36)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = rptLocatinNameArabic & ControlChars.NewLine & rptLocatinAddrArabic
            'If Not rptLocationPhone = "" Then
            '.Text = "Phone/هاتف : " & rptLocationPhone
            'End If
        End With
        Me.lblLocnPhone.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocnEmail.Count
        With lbl
            '.BorderStyle = BorderStyle.FixedSingle
            .Location = New Point(138, 127)
            .Name = "lblLocnEmail" & n.ToString
            .Size = New Size(504, 17)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            If Not rptLocationEmail = "" Then
                .Text = "البريد الالكتروني  \Email : " & rptLocationEmail & ",   هاتف \Phone : " & rptLocationPhone
            End If
        End With
        Me.lblLocnEmail.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlTxnTypeDecl.Count
        With pnl
            .Location = New Point(130, 148)
            .Size = New Size(522, 18)
            .BorderStyle = BorderStyle.FixedSingle
            .BackColor = Color.Silver
            .Name = "pnlTxnTypeDecl" & n.ToString
        End With
        Me.pnlTxnTypeDecl.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblTxnTypeDecl.Count
        With lbl
            .Location = New Point(150, 0)
            If TXN_TYPE = "Invoice" Then
                .Text = "TAX Invoice/فاتورة ضريبية"
            ElseIf TXN_TYPE = "Sales Order" Then
                .Text = "Sales Order/طلب المبيعات"
            ElseIf TXN_TYPE = "Sales Invoice" Then
                .Text = "TAX Invoice/فاتورة ضريبية"
            ElseIf TXN_TYPE = "Sales Return" Then
                .Text = "TAX CREDIT NOTE/أعادة قيمة الضريبة"
            End If
            .ForeColor = Color.White
            .Name = "lblTxnTypeDecl" & n.ToString
            .TextAlign = ContentAlignment.TopCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(210, 15)
        End With
        Me.lblTxnTypeDecl.Add(lbl)
        Me.Controls.Find("pnlTxnTypeDecl" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlInvDetails.Count
        With pnl
            .Location = New Point(130, 168)
            .Size = New Size(522, 48)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlInvDetails" & n.ToString
        End With
        Me.pnlInvDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblINVNo_KEY.Count
        With lbl
            .Location = New Point(13, 3)
            If TXN_TYPE = "Invoice" Or TXNTYPE = "Sales Invoice" Then
                .Text = "Invoice No./رقم الفاتورة:"
            ElseIf TXN_TYPE = "Sales Order" Then
                .Text = "SO No.        :"
            ElseIf TXN_TYPE = "Sales Return" Then
                .Text = "SR No.        :"
            End If
            .Name = "lblINVNo_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(100, 20)
        End With
        Me.lblINVNo_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVNo_VALUE.Count
        With lbl
            .Location = New Point(115, 3)
            .Text = TXN_NO
            .Name = "lblINVNo_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(75, 20)
        End With
        Me.lblINVNo_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSONo_KEY.Count
        With lbl
            .Location = New Point(250, 3)
            .Text = "SO. No.        :"
            .Name = "lblINVSONo_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
            If TXNTYPE = "Sales Invoice" Then
                .Visible = True
            Else
                .Visible = False
            End If
        End With
        Me.lblINVSONo_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSONo_VALUE.Count
        With lbl
            .Location = New Point(330, 3)
            .Text = rptCustomerSONo
            .Name = "lblINVSONo_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(180, 20)
            If TXNTYPE = "Sales Invoice" Then
                .Visible = True
            Else
                .Visible = False
            End If
        End With
        Me.lblINVSONo_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVDate_KEY.Count
        With lbl
            .Location = New Point(13, 25)
            .Text = "Date/تاريخ:"
            .Name = "lblINVDate_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(100, 20)
        End With
        Me.lblINVDate_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVDate_VALUE.Count
        With lbl
            .Location = New Point(115, 25)
            .Text = rptDate
            .Name = "lblINVDate_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(120, 20)
        End With
        Me.lblINVDate_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSMNo_KEY.Count
        With lbl
            .Location = New Point(250, 25)
            .Text = "Salesman/بائع   :"
            .Name = "lblINVSMNo_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(100, 20)
        End With
        Me.lblINVSMNo_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSMNo_VALUE.Count
        With lbl
            .Location = New Point(355, 25)
            .Text = rptSalesmanName
            .Name = "lblINVSMNo_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(155, 20)
        End With
        Me.lblINVSMNo_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlCustDetails.Count
        With pnl
            .Location = New Point(130, 215)
            .Size = New Size(522, 48)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlCustDetails" & n.ToString
        End With
        Me.pnlCustDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblINVCustName_KEY.Count
        With lbl
            .Location = New Point(13, 3)
            .Text = "Customer/العميل :"
            .Name = "lblINVCustName_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(95, 20)
        End With
        Me.lblINVCustName_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustName_VALUE.Count
        With lbl
            .Location = New Point(110, 3)
            .Text = rptCustomerName
            .Name = "lblINVCustName_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(400, 20)
        End With
        Me.lblINVCustName_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustPhone_KEY.Count
        With lbl
            .Location = New Point(13, 25)
            .Text = "Phone/هاتف :"
            .Name = "lblINVCustPhone_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(95, 20)
        End With
        Me.lblINVCustPhone_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustPhone_VALUE.Count
        With lbl
            .Location = New Point(105, 25)
            .Text = rptCustomerPhone
            .Name = "lblINVCustPhone_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(145, 20)
        End With
        Me.lblINVCustPhone_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustEmail_KEY.Count
        With lbl
            .Location = New Point(250, 25)
            .Text = "Email/البريد الإلكتر: "
            .Name = "lblINVCustEmail_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(100, 20)
        End With
        Me.lblINVCustEmail_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustEmail_VALUE.Count
        With lbl
            .Location = New Point(355, 25)
            .Text = rptCustomerEmail
            .Name = "lblINVCustEmail_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(160, 20)
        End With
        Me.lblINVCustEmail_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlItemHeader.Count
        With pnl
            .Location = New Point(130, 265)
            .Size = New Size(522, 47)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlItemHeader" & n.ToString
        End With
        Me.pnlItemHeader.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblRptSNOHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(0, 0)
            .Text = "رقم " & ControlChars.NewLine & "SNo"
            .Name = "lblRptSNOHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 7, FontStyle.Bold)
            .Size = New Size(32, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptSNOHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptItemCodeHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(32, 0)
            .Text = "الصنف" & ControlChars.NewLine & "Item"
            .Name = "lblRptItemCodeHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(248, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptItemCodeHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptUOMHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(280, 0)
            .Text = "وحدة" & ControlChars.NewLine & "UOM"
            .Name = "lblRptUOMHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(45, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptUOMHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptRateHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(325, 0)
            .Text = "معدل" & ControlChars.NewLine & "Rate"
            .Name = "lblRptRateHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(60, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptRateHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptQtyHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(385, 0)
            .Text = "كمية" & ControlChars.NewLine & "Qty"
            .Name = "lblRptQtyHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(45, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptQtyHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptAmtHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(430, 0)
            .Text = "السعر" & ControlChars.NewLine & "Amount"
            .Name = "lblRptAmtHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(90, 45)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptAmtHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlItemDetails.Count
        With pnl
            .Location = New Point(130, 312)
            .Size = New Size(522, 235)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlItemDetails" & n.ToString
            currentItemPanel = "pnlItemDetails" & n.ToString
        End With
        Me.pnlItemDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        pnl = New Panel
        n = pnlTotalDetails.Count
        With pnl
            .Location = New Point(130, 546)
            .Size = New Size(522, 93)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlTotalDetails" & n.ToString
        End With
        Me.pnlTotalDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblINVAdvPaid_KEY.Count
        With lbl
            .Location = New Point(21, 5)
            .Text = "Advance Paid :"
            .Name = "lblINVAdvPaid_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(75, 16)
            .Visible = False
        End With
        Me.lblINVAdvPaid_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVAdvPaid_VALUE.Count
        With lbl
            .Location = New Point(100, 5)
            .Text = "0"
            .Name = "lblINVAdvPaid_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(80, 16)
            .Visible = False
        End With
        Me.lblINVAdvPaid_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVBalance_KEY.Count
        With lbl
            .Location = New Point(21, 24)
            If TXN_TYPE = "Sales Invoice" Then
                .Text = "Balance Paid  :"
            Else
                .Text = "Balance          :"
            End If

            .Name = "lblINVBalance_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(75, 16)
            .Visible = False
        End With
        Me.lblINVBalance_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVBalance_VALUE.Count
        With lbl
            .Location = New Point(100, 24)
            .Text = "0"
            .Name = "lblINVBalance_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(80, 16)
            .Visible = False
        End With
        Me.lblINVBalance_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSubTotal_KEY.Count
        With lbl
            .Location = New Point(305, 5)
            .Text = "Sub Total/المجموع الفرعي :"
            .Name = "lblINVSubTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(130, 16)
        End With
        Me.lblINVSubTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSubTotal_VALUE.Count
        With lbl
            .Location = New Point(419, 5)
            .Text = ""
            .Name = "lblINVSubTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(96, 16)

        End With
        Me.lblINVSubTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVDisTotal_KEY.Count
        With lbl
            .Location = New Point(305, 24)
            .Text = "Discount/خصم :"
            .Name = "lblINVDisTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(100, 16)
        End With
        Me.lblINVDisTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVDisTotal_VALUE.Count
        With lbl
            .Location = New Point(419, 24)
            .Text = ""
            .Name = "lblINVDisTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(96, 16)

        End With
        Me.lblINVDisTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVExpTotal_KEY.Count
        With lbl
            .Location = New Point(305, 44)
            .Text = "Expense/مصروف  :"
            .Name = "lblINVExpTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(100, 16)
        End With
        Me.lblINVExpTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVExpTotal_VALUE.Count
        With lbl
            .Location = New Point(419, 44)
            .Text = ""
            .Name = "lblINVExpTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(96, 16)

        End With
        Me.lblINVExpTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVTaxTotal_KEY.Count
        With lbl
            .Location = New Point(21, 44)
            .Text = "5% Tax/ضريبة  :"
            .Name = "lblINVTaxTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(100, 16)
        End With
        Me.lblINVTaxTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVTaxTotal_VALUE.Count
        With lbl
            .Location = New Point(100, 44)
            .Text = ""
            .Name = "lblINVTaxTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(80, 16)

        End With
        Me.lblINVTaxTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVTaxTRN_KEY_VALUE.Count
        With lbl
            .Location = New Point(21, 64)
            If Not rptLocationTaxTRN.Equals("") Then
                .Text = "(Tax TRN : " & rptLocationTaxTRN & ")"
            Else
                .Text = ""
            End If
            .Name = "lblINVTaxTRN_KEY_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(180, 16)
        End With
        Me.lblINVTaxTRN_KEY_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)


        pnl = New Panel
        n = pnlGrandTotalDetails.Count
        With pnl
            .Location = New Point(130, 636)
            .Size = New Size(522, 29)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlGrandTotalDetails" & n.ToString
        End With
        Me.pnlGrandTotalDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        'lbl = New Label
        'n = lblRptEEO.Count
        'With lbl
        '    .Location = New Point(3, 6)
        '    .Text = "E && OE."
        '    .Name = "lblRptEEO" & n.ToString
        '    .TextAlign = ContentAlignment.MiddleLeft
        '    .Font = New Font("Arial Narrow", 9, FontStyle.Bold)
        '    .Size = New Size(60, 16)
        'End With
        'Me.lblRptEEO.Add(lbl)
        'Me.Controls.Find("pnlGrandTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)


        lbl = New Label
        n = lblRptGrandTotal_KEY.Count
        With lbl
            .Location = New Point(305, 6)
            .Text = "Total/مجموع       : "
            .Name = "lblRptGrandTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 9, FontStyle.Bold)
            .Size = New Size(100, 16)
        End With
        Me.lblRptGrandTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlGrandTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptGrandTotal_VALUE.Count
        With lbl
            .Location = New Point(388, 7)
            .Text = ""
            .Name = "lblRptGrandTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial", 9, FontStyle.Bold)
            .Size = New Size(127, 16)
        End With
        Me.lblRptGrandTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlGrandTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)


        pnl = New Panel
        n = pnlDeclaration.Count
        With pnl
            .Location = New Point(130, 664)
            .Size = New Size(331, 65)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlDeclaration" & n.ToString
        End With
        Me.pnlDeclaration.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        'lbl = New Label
        'n = lblDeclarationHeader.Count
        'With lbl
        '    .Location = New Point(3, 4)
        '    .Text = "OUR EXCHANGE POLICY"
        '    .Name = "lblDeclarationHeader" & n.ToString
        '    .TextAlign = ContentAlignment.MiddleLeft
        '    .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
        '    .Size = New Size(150, 16)
        'End With
        'Me.lblDeclarationHeader.Add(lbl)
        'Me.Controls.Find("pnlDeclaration" & n.ToString, True)(0).Controls.Add(lbl)
        Dim thankyou1 As String
        If Setup_Values.ContainsKey("LINE_DISP_TL_1") Then
            thankyou1 = Setup_Values("LINE_DISP_TL_1")
        Else
            thankyou1 = ""
        End If
        
        Dim thankyou2 As String
        If Setup_Values.ContainsKey("LINE_DISP_TL_2") Then
            thankyou2 = Setup_Values("LINE_DISP_TL_2")
        Else
            thankyou2 = ""
        End If
        
        Dim welcome1 As String
        If Setup_Values.ContainsKey("LINE_DISP_WL_1") Then
            welcome1 = Setup_Values("LINE_DISP_WL_1")
        Else
            welcome1 = ""
        End If
        
        Dim welcome2 As String
        If Setup_Values.ContainsKey("LINE_DISP_WL_2") Then
            welcome2 = Setup_Values("LINE_DISP_WL_2")
        Else
            welcome2 = ""
        End If
        
        lbl = New Label
        n = lblDeclaration.Count
        With lbl
            .Location = New Point(1, 1)
            .Text = welcome1 & ControlChars.NewLine & welcome2
            '.Text = "The goods can be exchanged within 7 days from the date of purchase with the original receipt and intact packaging" & ControlChars.NewLine & "يمكن استبدال المنتج/البضاعة خلال (7) أيام من تاريخ الشراء وفقط اذا كانت بنفس الحالة والمواصفات التي كانت عليها وقت الشراء."
            .Name = "lblDeclaration" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(329, 60)
        End With
        Me.lblDeclaration.Add(lbl)
        Me.Controls.Find("pnlDeclaration" & n.ToString, True)(0).Controls.Add(lbl)


        pnl = New Panel
        n = pnlAuthSign.Count
        With pnl
            .Location = New Point(460, 664)
            .Size = New Size(192, 65)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlAuthSign" & n.ToString
        End With
        Me.pnlAuthSign.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblAuthSignature.Count
        With lbl
            .Location = New Point(5, 4)
            .Text = "Authorized Signature" & ControlChars.NewLine & "توقيع معتمد"
            .Name = "lblAuthSignature" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(170, 32)
        End With
        Me.lblAuthSignature.Add(lbl)
        Me.Controls.Find("pnlAuthSign" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlFooter.Count
        With pnl
            .Location = New Point(120, 733)
            .Size = New Size(522, 60)
            .BorderStyle = BorderStyle.None
            .Name = "pnlFooter" & n.ToString
        End With
        Me.pnlFooter.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblFooterLine1.Count
        With lbl
            .Location = New Point(7, 2)
            .Text = thankyou1 & ControlChars.NewLine & thankyou2
            '.Text = "THANK YOU FOR VISITING . PLEASE VISIT AGAIN" & ControlChars.NewLine & "شكرا لزيارتك. يرجى زيارة مرة أخرى"
            .Name = "lblFooterLine1" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(512, 35)
            .BorderStyle = BorderStyle.None
        End With
        Me.lblFooterLine1.Add(lbl)
        Me.Controls.Find("pnlFooter" & n.ToString, True)(0).Controls.Add(lbl)

        'lbl = New Label
        'n = lblFooterLine2.Count
        'With lbl
        '    .Location = New Point(5, 24)
        '    .Text = "" '"WARNING: Chocking Hazard; small parts, not suitable for children under(3) years old"
        '    .Name = "lblFooterLine2" & n.ToString
        '    .TextAlign = ContentAlignment.MiddleCenter
        '    .Font = New Font("Arial Narrow", 9, FontStyle.Regular)
        '    .Size = New Size(512, 16)
        '    .BorderStyle = BorderStyle.None
        'End With
        'Me.lblFooterLine2.Add(lbl)
        'Me.Controls.Find("pnlFooter" & n.ToString, True)(0).Controls.Add(lbl)

    End Sub


    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Try
            Dim doc As New PdfDocument()

            Dim bmplist As New List(Of Bitmap)
            Dim xgrlist As New List(Of XGraphics)
            For i = 0 To pnlPages.Count - 1
                pnlPages(i).BorderStyle = BorderStyle.None
                Dim bmpimg = New Bitmap(Me.Controls.Find(pnlPages(i).Name, True)(0).Width, Me.Controls.Find(pnlPages(i).Name, True)(0).Height)
                Me.Controls.Find(pnlPages(i).Name, True)(0).DrawToBitmap(bmpimg, Me.Controls.Find(pnlPages(i).Name, True)(0).ClientRectangle)
                doc.Pages.Add(New PdfPage())
                'doc.Pages(i).Size = PdfSharp.PageSize.A5
                Dim xgrGraph As XGraphics = XGraphics.FromPdfPage(doc.Pages(i))
                'bmpimg.RotateFlip(RotateFlipType.RotateNoneFlipXY)
                Dim imgX As XImage = XImage.FromGdiPlusImage(bmpimg)
                xgrGraph.DrawImage(imgX, 0, 0)
                pnlPages(i).BorderStyle = BorderStyle.FixedSingle
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

    Private Sub btn_Print_Report_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print_Report.Click
        Try
            If Not pnlPages.Count > 0 Then
                Exit Sub
            End If
            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
            PrintDialog1.AllowSomePages = True
            PrintDialog1.AllowCurrentPage = True
            PrintDialog1.AllowSelection = True

            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
                'totPages = pnlReportPages.Count
                'MsgBox(totPages)
                For i = 0 To pnlPages.Count - 1
                    pnlPages(i).BorderStyle = BorderStyle.None
                    Dim bmpimg = New Bitmap(Me.Controls.Find(pnlPages(i).Name, True)(0).Width, Me.Controls.Find(pnlPages(i).Name, True)(0).Height)
                    Me.Controls.Find(pnlPages(i).Name, True)(0).DrawToBitmap(bmpimg, Me.Controls.Find(pnlPages(i).Name, True)(0).ClientRectangle)
                    bitmaps.Add(bmpimg)
                    pnlPages(i).BorderStyle = BorderStyle.FixedSingle
                Next
                'PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("Custom", 584, 827)
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
        g.DrawImage(bitmap, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        e.HasMorePages = System.Threading.Interlocked.Increment(_page) < Me.bitmaps.Count
        g.Dispose()
    End Sub

    Private Sub btnCloseReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseReport.Click
        Try
            Me.Close()
            SubHomeForm.MdiParent = Home
            SubHomeForm.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

End Class