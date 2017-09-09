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





Public Class TransactionSlip
    Inherits System.Windows.Forms.Form

    Private TXNNO As String
    Private TXNTYPE As String
    Private rptLocationName As String = ""
    Private rptLocationAddr As String = ""
    Private rptLocationPhone As String = ""
    Private rptLocationEmail As String = ""
    Private rptDate As String = ""
    Private rptCustomerName As String = ""
    Private rptSalesmanName As String = ""
    Private rptCustomerPhone As String = ""
    Private rptCustomerEmail As String = ""
    Private rptCustomerSONo As String = ""
    Private rptTotalDiscount As String = ""




    Private CurrentY As Integer
    Private CurrentX As Integer
    Private leftMargin As Integer
    Private rightMargin As Integer
    Private topMargin As Integer
    Private bottomMargin As Integer
    Private InvoiceWidth As Integer
    Private InvoiceHeight As Integer

    Private InvTitle As String
    Private InvSubTitle1 As String
    Private InvSubTitle2 As String
    Private InvSubTitle3 As String
    Private InvSubTitle4 As String

    Private InvTitleFont As Font = New Font("Arial", 12, FontStyle.Bold)

    ' Title Font height
    Private InvTitleHeight As Integer
    ' SubTitle Font
    Private InvSubTitleFont As Font = New Font("Arial", 10, FontStyle.Bold)
    'Transaction Type Font
    Private InvTranstype As Font = New Font("Courier New", 10, FontStyle.Bold)
    ' SubTitle Font height
    Private InvSubTitleHeight As Integer
    ' Invoice Font
    Private InvoiceFont As Font = New Font("Arial", 10, FontStyle.Regular)
    ' Invoice Font height
    Private InvoiceFontHeight As Integer

    Private ItemDetailsFont As Font = New Font("Arial", 9, FontStyle.Regular)
    Private ItemDetailsFontHeight As Integer

    'Private GreenBrush As SolidBrush = New SolidBrush (color.Green)

    ' Blue Color
    Private BlueBrush As SolidBrush = New SolidBrush(Color.Blue)
    ' Red Color
    Private RedBrush As SolidBrush = New SolidBrush(Color.Red)
    ' Black Color
    Private BlackBrush As SolidBrush = New SolidBrush(Color.Black)
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

    'Dim db As New DBConnection

    'Private pnlPages As New List(Of Panel)
    'Private picReport As New List(Of PictureBox)
    'Private lblLocnName As New List(Of Label)
    'Private lblLocnAddr As New List(Of Label)
    'Private lblLocnPhone As New List(Of Label)
    'Private lblLocnEmail As New List(Of Label)
    'Private pnlTxnTypeDecl As New List(Of Panel)
    'Private lblTxnTypeDecl As New List(Of Label)
    'Private pnlInvDetails As New List(Of Panel)
    'Private pnlCustDetails As New List(Of Panel)
    'Private pnlItemHeader As New List(Of Panel)
    'Private pnlItemDetails As New List(Of Panel)
    'Private pnlTotalDetails As New List(Of Panel)
    'Private pnlGrandTotalDetails As New List(Of Panel)
    'Private pnlDeclaration As New List(Of Panel)
    'Private pnlAuthSign As New List(Of Panel)

    'Private lblINVNo_KEY As New List(Of Label)
    'Private lblINVNo_VALUE As New List(Of Label)
    'Private lblINVDate_KEY As New List(Of Label)
    'Private lblINVDate_VALUE As New List(Of Label)
    'Private lblINVSONo_KEY As New List(Of Label)
    'Private lblINVSONo_VALUE As New List(Of Label)
    'Private lblINVSMNo_KEY As New List(Of Label)
    'Private lblINVSMNo_VALUE As New List(Of Label)
    'Private lblINVCustName_KEY As New List(Of Label)
    'Private lblINVCustName_VALUE As New List(Of Label)
    'Private lblINVCustPhone_KEY As New List(Of Label)
    'Private lblINVCustPhone_VALUE As New List(Of Label)
    'Private lblINVCustEmail_KEY As New List(Of Label)
    'Private lblINVCustEmail_VALUE As New List(Of Label)

    'Private lblINVAdvPaid_KEY As New List(Of Label)
    'Private lblINVAdvPaid_VALUE As New List(Of Label)
    'Private lblINVBalance_KEY As New List(Of Label)
    'Private lblINVBalance_VALUE As New List(Of Label)
    'Private lblINVSubTotal_KEY As New List(Of Label)
    'Private lblINVSubTotal_VALUE As New List(Of Label)
    'Private lblINVExpTotal_KEY As New List(Of Label)
    'Private lblINVExpTotal_VALUE As New List(Of Label)
    'Private lblINVDisTotal_KEY As New List(Of Label)
    'Private lblINVDisTotal_VALUE As New List(Of Label)

    'Private lblRptSNOHeader As New List(Of Label)
    'Private lblRptItemCodeHeader As New List(Of Label)
    'Private lblRptUOMHeader As New List(Of Label)
    'Private lblRptRateHeader As New List(Of Label)
    'Private lblRptQtyHeader As New List(Of Label)
    'Private lblRptAmtHeader As New List(Of Label)

    'Private pnlRows As New List(Of Panel)

    'Private lblRptSNOValue As New List(Of Label)
    'Private lblRptItemCodeValue As New List(Of Label)
    'Private lblRptItemDescValue As New List(Of Label)
    'Private lblRptUOMValue As New List(Of Label)
    'Private lblRptRateValue As New List(Of Label)
    'Private lblRptQtyValue As New List(Of Label)
    'Private lblRptAmtValue As New List(Of Label)

    'Private lblRptEEO As New List(Of Label)
    'Private lblRptGrandTotal_KEY As New List(Of Label)
    'Private lblRptGrandTotal_VALUE As New List(Of Label)

    'Private lblDeclarationHeader As New List(Of Label)
    'Private lblDeclaration As New List(Of Label)
    'Private lblAuthSignature As New List(Of Label)

    'Private pnlFooter As New List(Of Panel)
    'Private lblFooterLine1 As New List(Of Label)
    'Private lblFooterLine2 As New List(Of Label)
    'Private lblFooterLine3 As New List(Of Label)

    'Dim totalDiscountamt As Double = 0
    'Dim totalExpenseamt As Double = 0
    'Dim subtotalamt As Double = 0

    'Private currentPage As String = ""
    'Private currentItemPanel As String = ""
    'Private currentPageNumber As String = ""

    'Dim _page As Integer
    'Dim bitmaps As New List(Of Bitmap)

    Dim db As New DBConnection
    Dim printds As DataSet

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
    Dim totheaddiscamtval As Double
    Dim totheadaddvalamt As Double

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
            stQuery = stQuery + " a.CSRI_FC_VAL as ItmAmt,"
            stQuery = stQuery + " nvl((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery & " nvl((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,CSRH_SM_CODE as salesman,CSRH_FLEX_03 as pm_cust_no"
            stQuery = stQuery + " from "
            stQuery = stQuery + " OT_CUST_SALE_RET_HEAD b,OT_CUST_SALE_RET_ITEM a,om_location c,om_address d"
            stQuery = stQuery + " where b.CSRH_NO = " + TXN_NO.ToString + " and"
            stQuery = stQuery + " b.CSRH_SYS_ID = a.CSRI_CSRH_SYS_ID and"
            stQuery = stQuery + " b.CSRH_LOCN_CODE = c.locn_code and c.locn_addr_code = d.addr_code"
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Sales Return Report Query", stQuery, "")

            Transactions.transtype = "Sales Return"
            Transactions.printdataset = ds
            printds = ds





            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
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
                    If i Mod 8 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 38)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 38)
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

                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
            Me.Controls.Find("lblRptGrandTotal_VALUE" & currentPageNumber, True)(0).Text = Round((subtotalamt + totalExpenseamt) - totalDiscountamt, 3).ToString("0.000")

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
            stQuery = stQuery + " a.INVI_FC_VAL as ItmAmt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,INVH_SM_CODE as salesman,INVH_FLEX_03 as pmcustno,INVH_REF_NO as refno"
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
            stQuery = stQuery + " where b.invh_no = " & TXN_NO & " and"
            stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
            stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Sales Invoice Report Query", stQuery, "")

            Transactions.transtype = "Sales Invoice"
            Transactions.printdataset = ds
            printds = ds



            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            rptTotalDiscount = ds.Tables("Table").Rows.Item(0).Item(22).ToString
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
                    If i Mod 8 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 38)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 38)
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
            stQuery = stQuery + " select rownum,b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,"
            stQuery = stQuery + " d.ADDR_LINE_1 as locn_name,"
            stQuery = stQuery + " d.ADDR_LINE_2|| ' ' || d.ADDR_LINE_3"
            stQuery = stQuery + " as Location_Address,"
            stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
            stQuery = stQuery + " case nvl(b.soH_FLEX_03,0) when '0' then (select cust_name from om_customer where cust_code = b.soh_cust_code)"
            stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where pm_cust_code = b.soh_flex_03) end as CustName,"
            stQuery = stQuery + " b.soh_BILL_ADDR_LINE_1||' '||b.soh_BILL_ADDR_LINE_2||' '||b.soh_BILL_COUNTRY_CODE as billing_addr,b.soH_BILL_TEL as billing_phone, b.soh_BILL_EMAIL as billing_email, b.soh_SHIP_ADDR_LINE_1||' '||b.soh_SHIP_ADDR_LINE_2||' '||b.soh_SHIP_COUNTRY_CODE as shipping_addr,"
            stQuery = stQuery + " a.soI_ITEM_CODE as ItemCode,a.soI_ITEM_DESC as ItemDesc,a.soI_UOM_CODE as ItmUOM,a.soI_PL_RATE as ItmPrice ,a.soI_QTY as ItmQty,a.soI_FC_VAL as ItmAmt,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt, "
            stQuery = stQuery & " nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,SOH_SM_CODE as salesman,SOH_FLEX_03 as pmcustno"
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_so_head b,ot_so_item a, om_location c,om_address d"
            stQuery = stQuery + " where b.soh_no = " + TXN_NO.ToString + " and b.soh_sys_id = a.soi_soh_sys_id and b.soh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Sales Order Report Query", stQuery, "")


            Transactions.transtype = "Sales Order"
            Transactions.printdataset = ds
            printds = ds


            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
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
                    If i Mod 8 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 38)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 38)
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

    Private Sub loadReportInvoice()
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
            stQuery = stQuery + " a.INVI_FC_VAL as ItmAmt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt,"
            stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,INVH_SM_CODE as salesman,INVH_FLEX_03 as pmcustno"
            stQuery = stQuery + " from "
            stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
            stQuery = stQuery + " where b.invh_no = " & TXN_NO & " and"
            stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
            stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("Direct Invoice Report Query", stQuery, "")

            Transactions.transtype = "Direct Invoice"
            Transactions.printdataset = ds
            printds = ds



            Dim rowcount As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0

            rptDate = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            'MsgBox(rptDate)
            rptLocationName = ds.Tables("Table").Rows.Item(0).Item(3).ToString
            'MsgBox(rptLocationName)
            rptLocationAddr = ds.Tables("Table").Rows.Item(0).Item(4).ToString
            'MsgBox(rptLocationAddr)
            rptLocationPhone = ds.Tables("Table").Rows.Item(0).Item(5).ToString
            'MsgBox(rptLocationPhone)
            rptLocationEmail = ds.Tables("Table").Rows.Item(0).Item(6).ToString
            'MsgBox(rptLocationEmail)
            'rptTotalDiscount = ds.Tables("Table").Rows.Item(0).Item(22).ToString
            Dim stSalesQuery As String = ""
            stSalesQuery = "Select SM_NAME from om_salesman where SM_CODE='" & ds.Tables("Table").Rows.Item(0).Item(20).ToString & "'"
            Dim dsSal As DataSet = db.SelectFromTableODBC(stSalesQuery)
            If dsSal.Tables("Table").Rows.Count > 0 Then
                rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString & " - " & dsSal.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            'rptSalesmanName = ds.Tables("Table").Rows.Item(0).Item(20).ToString
            If ds.Tables("Table").Rows.Item(0).Item(21).ToString = "" Then

                rptCustomerName = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                'MsgBox(rptCustomerName)
                rptCustomerPhone = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                'MsgBox(rptCustomerPhone)
                rptCustomerEmail = ds.Tables("Table").Rows.Item(0).Item(10).ToString
                'MsgBox(rptCustomerEmail)
            Else
                Dim stQueryPatient As String
                stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + ds.Tables("Table").Rows.Item(0).Item(21).ToString + "'"
                MsgBox(ds.Tables("Table").Rows.Item(0).Item(21).ToString)
                Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                errLog.WriteToErrorLog("INVREPORT PATIENT CHEK QUERY", stQueryPatient, "")
                If dsP.Tables("Table").Rows.Count > 0 Then
                    rptCustomerName = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                    'MsgBox(rptCustomerName)
                    rptCustomerPhone = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                    'MsgBox(rptCustomerPhone)
                    'rptCustomerEmail = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                    'MsgBox(rptCustomerEmail)
                End If
            End If

            CreatePage()
            Dim itemlines As Integer = 0
            While rowcount > 0
                If Not i = 1 Then
                    If i Mod 8 = 1 Then
                        CreatePage()
                        itemlines = 0
                    End If
                End If
                row = ds.Tables("Table").Rows.Item(i)

                Dim pnl As New Panel
                Dim n As Integer
                n = pnlRows.Count
                With pnl
                    .Location = New Point(0, itemlines * 38)
                    .Name = "pnlRows" & n.ToString
                    .Size = New Size(519, 38)
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

                itemlines = itemlines + 1
                rowcount = rowcount - 1
                i = i + 1
            End While

            'If Not rptTotalDiscount = 0 Then
            'lblDTamt.Text = Convert.ToDouble(rptTotalDiscount).ToString("0.00")
            'lblTotalamt.Text = (subtotalamt - Convert.ToDouble(rptTotalDiscount)).ToString("0.00")
            'Else
            ' Label8.Visible = False
            ' lblDTamt.Visible = False
            ' lblTotalamt.Text = (subtotalamt - Convert.ToDouble(rptTotalDiscount)).ToString("0.00")
            ' End If


            Me.Controls.Find("lblINVDisTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalDiscountamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVExpTotal_VALUE" & currentPageNumber, True)(0).Text = Round(totalExpenseamt, 3).ToString("0.000")
            Me.Controls.Find("lblINVSubTotal_VALUE" & currentPageNumber, True)(0).Text = Round(subtotalamt, 3).ToString("0.000")
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
        If logoYN And File.Exists(Application.StartupPath & "\LOGOS\" & locationLogo) Then
            n = picReport.Count
            With pic
                .Location = New Point(130, 0)
                .Name = "picReport" & n.ToString
                .Size = New Size(522, 57)
                If locationLogo = "" Then
                    .Image = My.Resources.clientlogo12
                Else
                    Dim bm As New Bitmap(Application.StartupPath & "\LOGOS\" & locationLogo)
                    .Image = bm
                End If
                .SizeMode = PictureBoxSizeMode.CenterImage
                '.BorderStyle = BorderStyle.FixedSingle
            End With
        Me.picReport.Add(pic)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pic)
        End If

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
            .Location = New Point(138, 90)
            .Name = "lblLocnPhone" & n.ToString
            .Size = New Size(504, 15)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            If Not rptLocationPhone = "" Then
                .Text = "Phone : " & rptLocationPhone
            End If
        End With
        Me.lblLocnPhone.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblLocnEmail.Count
        With lbl
            .Location = New Point(138, 106)
            .Name = "lblLocnEmail" & n.ToString
            .Size = New Size(504, 15)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleCenter
            If Not rptLocationEmail = "" Then
                .Text = "Email : " & rptLocationEmail
            End If
        End With
        Me.lblLocnEmail.Add(lbl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlTxnTypeDecl.Count
        With pnl
            .Location = New Point(130, 123)
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
            .Location = New Point(205, 0)
            If TXN_TYPE = "Invoice" Then
                .Text = "Direct Invoice"
            ElseIf TXN_TYPE = "Sales Order" Then
                .Text = "Sales Order"
            ElseIf TXN_TYPE = "Sales Invoice" Then
                .Text = "Sales Invoice"
            ElseIf TXN_TYPE = "Sales Return" Then
                .Text = "Sales Return"
            End If
            .ForeColor = Color.White
            .Name = "lblTxnTypeDecl" & n.ToString
            .TextAlign = ContentAlignment.TopCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(90, 15)
        End With
        Me.lblTxnTypeDecl.Add(lbl)
        Me.Controls.Find("pnlTxnTypeDecl" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlInvDetails.Count
        With pnl
            .Location = New Point(130, 143)
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
                .Text = "Invoice No.:"
            ElseIf TXN_TYPE = "Sales Order" Then
                .Text = "SO No.        :"
            ElseIf TXN_TYPE = "Sales Return" Then
                .Text = "SR No.        :"
            End If
            .Name = "lblINVNo_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVNo_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVNo_VALUE.Count
        With lbl
            .Location = New Point(90, 3)
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
            .Location = New Point(260, 3)
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
            .Location = New Point(340, 3)
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
            .Text = "Date            :"
            .Name = "lblINVDate_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVDate_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVDate_VALUE.Count
        With lbl
            .Location = New Point(90, 25)
            .Text = rptDate
            .Name = "lblINVDate_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(75, 20)
        End With
        Me.lblINVDate_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSMNo_KEY.Count
        With lbl
            .Location = New Point(260, 25)
            .Text = "Salesman   :"
            .Name = "lblINVSMNo_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVSMNo_KEY.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVSMNo_VALUE.Count
        With lbl
            .Location = New Point(340, 25)
            .Text = rptSalesmanName
            .Name = "lblINVSMNo_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(180, 20)
        End With
        Me.lblINVSMNo_VALUE.Add(lbl)
        Me.Controls.Find("pnlInvDetails" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlCustDetails.Count
        With pnl
            .Location = New Point(130, 190)
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
            .Text = "Customer :"
            .Name = "lblINVCustName_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVCustName_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustName_VALUE.Count
        With lbl
            .Location = New Point(90, 3)
            .Text = rptCustomerName
            .Name = "lblINVCustName_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(425, 20)
        End With
        Me.lblINVCustName_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustPhone_KEY.Count
        With lbl
            .Location = New Point(13, 25)
            .Text = "Phone        :"
            .Name = "lblINVCustPhone_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVCustPhone_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustPhone_VALUE.Count
        With lbl
            .Location = New Point(90, 25)
            .Text = rptCustomerPhone
            .Name = "lblINVCustPhone_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(160, 20)
        End With
        Me.lblINVCustPhone_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustEmail_KEY.Count
        With lbl
            .Location = New Point(260, 25)
            .Text = "Email           :"
            .Name = "lblINVCustEmail_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .Size = New Size(75, 20)
        End With
        Me.lblINVCustEmail_KEY.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVCustEmail_VALUE.Count
        With lbl
            .Location = New Point(340, 25)
            .Text = rptCustomerEmail
            .Name = "lblINVCustEmail_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Size = New Size(180, 20)
        End With
        Me.lblINVCustEmail_VALUE.Add(lbl)
        Me.Controls.Find("pnlCustDetails" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlItemHeader.Count
        With pnl
            .Location = New Point(130, 240)
            .Size = New Size(522, 30)
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
            .Text = "SNo"
            .Name = "lblRptSNOHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(32, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptSNOHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptItemCodeHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(32, 0)
            .Text = "Item"
            .Name = "lblRptItemCodeHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(248, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptItemCodeHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptUOMHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(280, 0)
            .Text = "UOM"
            .Name = "lblRptUOMHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(45, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptUOMHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptRateHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(325, 0)
            .Text = "Rate"
            .Name = "lblRptRateHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(60, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptRateHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptQtyHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(385, 0)
            .Text = "Qty"
            .Name = "lblRptQtyHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(45, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptQtyHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblRptAmtHeader.Count
        With lbl
            .BackColor = Color.MintCream
            .Location = New Point(430, 0)
            .Text = "Amount"
            .Name = "lblRptAmtHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(90, 29)
            .BorderStyle = BorderStyle.FixedSingle
        End With
        Me.lblRptAmtHeader.Add(lbl)
        Me.Controls.Find("pnlItemHeader" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlItemDetails.Count
        With pnl
            .Location = New Point(130, 270)
            .Size = New Size(522, 350)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlItemDetails" & n.ToString
            currentItemPanel = "pnlItemDetails" & n.ToString
        End With
        Me.pnlItemDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        pnl = New Panel
        n = pnlTotalDetails.Count
        With pnl
            .Location = New Point(130, 619)
            .Size = New Size(522, 68)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlTotalDetails" & n.ToString
        End With
        Me.pnlTotalDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblINVAdvPaid_KEY.Count
        With lbl
            .Location = New Point(21, 21)
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
            .Location = New Point(100, 21)
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
            .Location = New Point(21, 41)
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
            .Location = New Point(100, 41)
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
            .Location = New Point(329, 5)
            .Text = "Sub Total :"
            .Name = "lblINVSubTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(56, 16)
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
            .Location = New Point(329, 24)
            .Text = "Discount  :"
            .Name = "lblINVDisTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(60, 16)
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
            .Location = New Point(329, 42)
            .Text = "Expense   :"
            .Name = "lblINVExpTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(60, 16)
        End With
        Me.lblINVExpTotal_KEY.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblINVExpTotal_VALUE.Count
        With lbl
            .Location = New Point(419, 42)
            .Text = ""
            .Name = "lblINVExpTotal_VALUE" & n.ToString
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(96, 16)

        End With
        Me.lblINVExpTotal_VALUE.Add(lbl)
        Me.Controls.Find("pnlTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlGrandTotalDetails.Count
        With pnl
            .Location = New Point(130, 684)
            .Size = New Size(522, 29)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlGrandTotalDetails" & n.ToString
        End With
        Me.pnlGrandTotalDetails.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblRptEEO.Count
        With lbl
            .Location = New Point(3, 6)
            .Text = "E && OE."
            .Name = "lblRptEEO" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 9, FontStyle.Bold)
            .Size = New Size(45, 16)
        End With
        Me.lblRptEEO.Add(lbl)
        Me.Controls.Find("pnlGrandTotalDetails" & n.ToString, True)(0).Controls.Add(lbl)


        lbl = New Label
        n = lblRptGrandTotal_KEY.Count
        With lbl
            .Location = New Point(329, 6)
            .Text = "Total       : "
            .Name = "lblRptGrandTotal_KEY" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial", 9, FontStyle.Bold)
            .Size = New Size(60, 16)
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
            .Location = New Point(130, 712)
            .Size = New Size(311, 50)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlDeclaration" & n.ToString
        End With
        Me.pnlDeclaration.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblDeclarationHeader.Count
        With lbl
            .Location = New Point(3, 4)
            .Text = "Declaration"
            .Name = "lblDeclarationHeader" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(60, 16)
        End With
        Me.lblDeclarationHeader.Add(lbl)
        Me.Controls.Find("pnlDeclaration" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblDeclaration.Count
        With lbl
            .Location = New Point(44, 30)
            .Text = "The above said information is true and correct."
            .Name = "lblDeclaration" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Regular)
            .Size = New Size(232, 14)
        End With
        Me.lblDeclaration.Add(lbl)
        Me.Controls.Find("pnlDeclaration" & n.ToString, True)(0).Controls.Add(lbl)


        pnl = New Panel
        n = pnlAuthSign.Count
        With pnl
            .Location = New Point(440, 712)
            .Size = New Size(212, 50)
            .BorderStyle = BorderStyle.FixedSingle
            .Name = "pnlAuthSign" & n.ToString
        End With
        Me.pnlAuthSign.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblAuthSignature.Count
        With lbl
            .Location = New Point(5, 4)
            .Text = "Authorized Signature"
            .Name = "lblAuthSignature" & n.ToString
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(107, 16)
        End With
        Me.lblAuthSignature.Add(lbl)
        Me.Controls.Find("pnlAuthSign" & n.ToString, True)(0).Controls.Add(lbl)

        pnl = New Panel
        n = pnlFooter.Count
        With pnl
            .Location = New Point(120, 764)
            .Size = New Size(522, 60)
            .BorderStyle = BorderStyle.None
            .Name = "pnlFooter" & n.ToString
        End With
        Me.pnlFooter.Add(pnl)
        Me.Controls.Find(currentPage, True)(0).Controls.Add(pnl)

        lbl = New Label
        n = lblFooterLine1.Count
        With lbl
            .Location = New Point(5, 7)

            If Setup_Values.ContainsKey("LINE_DISP_TL_1") Then
                .Text = Setup_Values.Item("LINE_DISP_TL_1") '"THANK YOU VISITING ALJABER.  PLEASE VISIT AGAIN"
            Else
                .Text = "THANK YOU VISITING ALJABER.  PLEASE VISIT AGAIN"
            End If
            .Name = "lblFooterLine1" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(512, 16)
            .BorderStyle = BorderStyle.None
        End With
        Me.lblFooterLine1.Add(lbl)
        Me.Controls.Find("pnlFooter" & n.ToString, True)(0).Controls.Add(lbl)

        lbl = New Label
        n = lblFooterLine2.Count
        With lbl
            .Location = New Point(5, 24)
            If Setup_Values.ContainsKey("LINE_DISP_TL_2") Then
                .Text = Setup_Values.Item("LINE_DISP_TL_2") '"WARNING: Chocking Hazard; small parts, not suitable for children under(3) years old"
            Else
                .Text = "WARNING: Chocking Hazard; small parts, not suitable for children under(3) years old"
            End If
            .Name = "lblFooterLine2" & n.ToString
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Arial Narrow", 8, FontStyle.Bold)
            .Size = New Size(512, 16)
            .BorderStyle = BorderStyle.None
        End With
        Me.lblFooterLine2.Add(lbl)
        Me.Controls.Find("pnlFooter" & n.ToString, True)(0).Controls.Add(lbl)

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
        'MsgBox("Print Report")
        Try

            Transactions.TransactionSlipCallPrint()
            'MsgBox("TransslipFunc")
            btnCloseReport_Click(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try




        'Try
        '    If Not pnlPages.Count > 0 Then
        '        Exit Sub
        '    End If
        '    PrintDialog1.Document = PrintDocument1
        '    PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        '    PrintDialog1.AllowSomePages = True
        '    PrintDialog1.AllowCurrentPage = True
        '    PrintDialog1.AllowSelection = True

        '    If PrintDialog1.ShowDialog = DialogResult.OK Then
        '        PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        '        'totPages = pnlReportPages.Count
        '        'MsgBox(totPages)
        '        For i = 0 To pnlPages.Count - 1
        '            pnlPages(i).BorderStyle = BorderStyle.None
        '            'Dim bmpimg = New Bitmap(Me.Controls.Find(pnlPages(i).Name, True)(0).Width, Me.Controls.Find(pnlPages(i).Name, True)(0).Height)
        '            Dim bmpimg = New Bitmap(Me.Controls.Find(pnlPages(i).Name, True)(0).Width, Me.Controls.Find(pnlPages(i).Name, True)(0).Height)
        '            Me.Controls.Find(pnlPages(i).Name, True)(0).DrawToBitmap(bmpimg, Me.Controls.Find(pnlPages(i).Name, True)(0).ClientRectangle)
        '            bitmaps.Add(bmpimg)
        '            pnlPages(i).BorderStyle = BorderStyle.FixedSingle
        '        Next
        '        'PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("Custom", 20, 50)

        '        'PrintDocument1.DefaultPageSettings.PrinterResolution = New PrinterResolution()

        '        PrintDocument1.Print()

        '        'Next
        '    End If

        'Catch ex As Exception
        '    errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        'End Try
    End Sub
    'Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage


    '    'MsgBox("Printdoc1Func")



    '    '  Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

    '    leftMargin = Convert.ToInt32(e.MarginBounds.Left)
    '    rightMargin = Convert.ToInt32(e.MarginBounds.Right)
    '    topMargin = Convert.ToInt32(e.MarginBounds.Top - 100)
    '    bottomMargin = Convert.ToInt32(e.MarginBounds.Bottom)
    '    InvoiceWidth = Convert.ToInt32(e.MarginBounds.Width)
    '    InvoiceHeight = Convert.ToInt32(e.MarginBounds.Height)
    '    'MsgBox("COnversion")
    '    CurrentY = topMargin
    '    CurrentX = leftMargin
    '    Dim ImageHeight As Integer = 0
    '    Dim oInvImage As Bitmap = New Bitmap(My.Resources.clientlogo)
    '    ' Set Image Left to center Image:
    '    Dim xImage As Integer = CurrentX + (InvoiceWidth - oInvImage.Width) / 2
    '    ImageHeight = oInvImage.Height ' Get Image Height
    '    e.Graphics.DrawImage(oInvImage, xImage - 10, CurrentY)

    '    InvTitle = rptLocationName
    '    InvSubTitle1 = rptLocationAddr
    '    InvSubTitle2 = "Phone: " & rptLocationPhone
    '    InvSubTitle3 = "Email: " & rptLocationEmail
    '    InvSubTitle4 = "INVOICE"

    '    InvTitleHeight = Convert.ToInt32(InvTitleFont.GetHeight(e.Graphics))
    '    InvSubTitleHeight = Convert.ToInt32(InvSubTitleFont.GetHeight(e.Graphics))
    '    ' Get Titles Length:
    '    Dim lenInvTitle As Integer = Convert.ToInt32(e.Graphics.MeasureString(InvTitle, InvTitleFont).Width)
    '    Dim lenInvSubTitle1 As Integer = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle1, InvSubTitleFont).Width)
    '    Dim lenInvSubTitle2 As Integer = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle2, InvSubTitleFont).Width)
    '    Dim lenInvSubTitle3 As Integer = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle3, InvSubTitleFont).Width)
    '    Dim lenInvSubTitle4 As Integer = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle4, InvSubTitleFont).Width)
    '    ' Set Titles Left:
    '    Dim xInvTitle As Integer = CurrentX + (InvoiceWidth - lenInvTitle) / 2
    '    Dim xInvSubTitle1 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2
    '    Dim xInvSubTitle2 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2
    '    Dim xInvSubTitle3 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2
    '    Dim xInvSubTitle4 As Integer = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2

    '    If (InvTitle <> "") Then
    '        'MsgBox(InvTitle)
    '        CurrentY = CurrentY + ImageHeight
    '        e.Graphics.DrawString(InvTitle, InvTitleFont, BlueBrush, xInvTitle - 10, CurrentY)
    '    End If
    '    If (InvSubTitle1 <> "") Then
    '        'MsgBox(InvSubTitle1)
    '        CurrentY = CurrentY + InvTitleHeight
    '        e.Graphics.DrawString(InvSubTitle1, InvSubTitleFont, BlueBrush, xInvSubTitle1 - 10, CurrentY)
    '    End If
    '    If (InvSubTitle2 <> "") Then
    '        'MsgBox(InvSubTitle2)
    '        CurrentY = CurrentY + InvSubTitleHeight
    '        e.Graphics.DrawString(InvSubTitle2, InvSubTitleFont, BlueBrush, xInvSubTitle2 - 10, CurrentY)
    '    End If
    '    If (InvSubTitle3 <> "") Then
    '        'MsgBox(InvSubTitle3)
    '        CurrentY = CurrentY + InvSubTitleHeight
    '        e.Graphics.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, xInvSubTitle3 - 10, CurrentY)
    '    End If

    '    CurrentY = CurrentY + InvSubTitleHeight + 5
    '    If TXN_TYPE = "Invoice" Then
    '        'MsgBox("Invoice")
    '        e.Graphics.DrawString("INVOICE", InvTranstype, BlueBrush, xInvSubTitle4 - 15, CurrentY)
    '        'MsgBox("Aft Invoice")
    '    ElseIf TXN_TYPE = "Sales Return" Then
    '        'MsgBox("Sales Return")
    '        e.Graphics.DrawString("SALES RETURN", InvTranstype, BlueBrush, xInvSubTitle4 - 20, CurrentY)
    '    End If

    '    Dim FieldValue As String = ""
    '    InvoiceFontHeight = Convert.ToInt32(InvoiceFont.GetHeight(e.Graphics))
    '    ItemDetailsFontHeight = Convert.ToInt32(ItemDetailsFont.GetHeight(e.Graphics))
    '    ' Set Company Name:
    '    CurrentX = leftMargin - 100
    '    CurrentY = CurrentY + 18
    '    If TXN_TYPE = "Invoice" Then
    '        FieldValue = "Inv.No : " & TXN_NO
    '    ElseIf TXN_TYPE = "Sales Return" Then
    '        FieldValue = "SRTN.No : " & TXN_NO
    '    ElseIf TXN_TYPE = "Saler Order" Then
    '        FieldValue = "SOH.No :" & TXN_NO
    '    Else
    '        FieldValue = "Inv.No :" & TXN_NO

    '    End If

    '    e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)

    '    CurrentX = (InvoiceWidth / 2) + 80
    '    FieldValue = "Date : " & rptDate
    '    e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)

    '    CurrentX = leftMargin - 100
    '    CurrentY = CurrentY + InvoiceFontHeight + 4
    '    FieldValue = "Salesman: " & rptSalesmanName
    '    e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)

    '    CurrentY = CurrentY + InvoiceFontHeight + 8
    '    e.Graphics.DrawLine(New Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin + 100, CurrentY)

    '    Dim xProductID As Integer = leftMargin - 100
    '    CurrentY = CurrentY + 3 '+ InvoiceFontHeight
    '    e.Graphics.DrawString("Product ID", ItemDetailsFont, BlueBrush, xProductID, CurrentY)

    '    Dim xProductName As Integer = xProductID + Convert.ToInt32(e.Graphics.MeasureString("Product ID", ItemDetailsFont).Width) + 9
    '    e.Graphics.DrawString("Product Name", ItemDetailsFont, BlueBrush, xProductName, CurrentY)

    '    Dim xQuantity As Integer = xProductName + Convert.ToInt32(e.Graphics.MeasureString("Product Name", ItemDetailsFont).Width) + 34
    '    e.Graphics.DrawString("Qty", ItemDetailsFont, BlueBrush, xQuantity, CurrentY)

    '    Dim AmountPosition As Integer
    '    AmountPosition = xQuantity + Convert.ToInt32(e.Graphics.MeasureString("Qty", ItemDetailsFont).Width) + 24
    '    e.Graphics.DrawString("Price", ItemDetailsFont, BlueBrush, AmountPosition, CurrentY)

    '    CurrentY = CurrentY + InvoiceFontHeight + 8
    '    e.Graphics.DrawLine(New Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin + 100, CurrentY)

    '    CurrentY = CurrentY + ItemDetailsFontHeight

    '    Dim count As Integer = printds.Tables("Table").Rows.Count

    '    Dim i As Integer = 0
    '    Dim row As System.Data.DataRow

    '    totalDiscountamt = 0
    '    totalExpenseamt = 0
    '    subtotalamt = 0

    '    While count > 0
    '        ' MsgBox(count)
    '        row = printds.Tables("Table").Rows.Item(i)
    '        FieldValue = row.Item(12)
    '        If (FieldValue.Length > 10) Then
    '            FieldValue = FieldValue.Remove(10, FieldValue.Length - 10)
    '        End If
    '        e.Graphics.DrawString(FieldValue, ItemDetailsFont, BlackBrush, xProductID, CurrentY)
    '        FieldValue = row.Item(13)
    '        If (FieldValue.Length > 13) Then
    '            FieldValue = FieldValue.Remove(13, FieldValue.Length - 13)
    '        End If
    '        e.Graphics.DrawString(FieldValue, ItemDetailsFont, BlackBrush, xProductName, CurrentY)

    '        e.Graphics.DrawString(row.Item(16), ItemDetailsFont, BlackBrush, xQuantity + 5, CurrentY)
    '        FieldValue = String.Format("{0:0.00}", Convert.ToDecimal(row.Item(17)))
    '        Dim xAmount As Integer = AmountPosition + Convert.ToInt32(e.Graphics.MeasureString("Price", ItemDetailsFont).Width)
    '        xAmount = xAmount - Convert.ToInt32(e.Graphics.MeasureString(FieldValue, ItemDetailsFont).Width)
    '        e.Graphics.DrawString(FieldValue, ItemDetailsFont, BlackBrush, xAmount, CurrentY)



    '        totalDiscountamt = totalDiscountamt + Convert.ToDouble(row.Item(18).ToString)
    '        totalExpenseamt = totalExpenseamt + Convert.ToDouble(row.Item(19).ToString)
    '        subtotalamt = subtotalamt + Convert.ToDouble(row.Item(17).ToString)

    '        CurrentY = CurrentY + InvoiceFontHeight + 3
    '        count = count - 1
    '        i = i + 1

    '    End While





    '    'MsgBox("End While")
    '    CurrentY = CurrentY + InvoiceFontHeight + 3
    '    e.Graphics.DrawLine(New Pen(Brushes.Black, 2), CurrentX - 100, CurrentY, rightMargin + 100, CurrentY)

    '    Dim discAmount As Integer = AmountPosition + Convert.ToInt32(e.Graphics.MeasureString("Price", InvoiceFont).Width)
    '    discAmount = discAmount - Convert.ToInt32(e.Graphics.MeasureString(FieldValue, InvoiceFont).Width)
    '    If Not totheaddiscamtval = 0 Then
    '        CurrentX = leftMargin - 100
    '        CurrentY = CurrentY + InvoiceFontHeight + 3
    '        FieldValue = "Discount Total "
    '        e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)

    '        FieldValue = String.Format("{0:0.00}", Convert.ToDecimal(totheaddiscamtval))
    '        e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, discAmount, CurrentY)
    '    End If

    '    Dim addedvalueAmount As Integer = AmountPosition + Convert.ToInt32(e.Graphics.MeasureString("Price", InvoiceFont).Width)
    '    addedvalueAmount = addedvalueAmount - Convert.ToInt32(e.Graphics.MeasureString(FieldValue, InvoiceFont).Width)






    '    CurrentX = leftMargin - 100
    '    CurrentY = CurrentY + InvoiceFontHeight + 3
    '    FieldValue = "Total Amount"
    '    e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY)



    '    FieldValue = String.Format("{0:0.00}", Convert.ToDecimal(subtotalamt - Convert.ToDouble(rptTotalDiscount)))
    '    discAmount = AmountPosition + Convert.ToInt32(e.Graphics.MeasureString("Price", InvoiceFont).Width)
    '    discAmount = discAmount - Convert.ToInt32(e.Graphics.MeasureString(FieldValue, InvoiceFont).Width)
    '    e.Graphics.DrawString(FieldValue, InvoiceFont, BlackBrush, discAmount, CurrentY)




    '    CurrentX = leftMargin
    '    InvSubTitle2 = "THANK YOU VISITING ALJABER"
    '    'MsgBox(InvSubTitle2)
    '    lenInvSubTitle2 = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle2, InvSubTitleFont).Width)
    '    'MsgBox("Aft conversion")
    '    xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2
    '    CurrentY = CurrentY + InvSubTitleHeight + 35
    '    e.Graphics.DrawString(InvSubTitle2, InvSubTitleFont, BlueBrush, xInvSubTitle2 - 10, CurrentY)

    '    InvSubTitle2 = "PLEASE VISIT AGAIN"
    '    lenInvSubTitle2 = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle2, InvSubTitleFont).Width)
    '    xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2
    '    CurrentY = CurrentY + InvSubTitleHeight
    '    e.Graphics.DrawString(InvSubTitle2, InvSubTitleFont, BlueBrush, xInvSubTitle2 - 10, CurrentY)

    '    InvSubTitle4 = "Warning: Choking hazard;"
    '    lenInvSubTitle4 = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle4, InvTranstype).Width)
    '    xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2
    '    CurrentY = CurrentY + InvSubTitleHeight
    '    e.Graphics.DrawString(InvSubTitle4, InvTranstype, BlueBrush, xInvSubTitle4 - 10, CurrentY)

    '    InvSubTitle4 = "small parts, not suitable for"
    '    lenInvSubTitle4 = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle4, InvTranstype).Width)
    '    xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2
    '    CurrentY = CurrentY + InvSubTitleHeight
    '    e.Graphics.DrawString(InvSubTitle4, InvTranstype, BlueBrush, xInvSubTitle4 - 10, CurrentY)

    '    InvSubTitle4 = "children under(3) years old"
    '    lenInvSubTitle4 = Convert.ToInt32(e.Graphics.MeasureString(InvSubTitle4, InvTranstype).Width)
    '    xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2
    '    CurrentY = CurrentY + InvSubTitleHeight + 3
    '    e.Graphics.DrawString(InvSubTitle4, InvTranstype, BlueBrush, xInvSubTitle4 - 10, CurrentY)

    '    e.Graphics.Dispose()
    '    'MsgBox("Print Doc End")
    'End Sub
    'End Sub
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