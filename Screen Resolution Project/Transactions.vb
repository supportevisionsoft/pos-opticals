Imports System.Drawing.Printing
Imports System.Math
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.Odbc
Imports Oracle.DataAccess.Client

Public Class Transactions

    Public MAILLOGValues As New Dictionary(Of Integer, String)
    Public MAILLOGROWNUM As Integer = 0

    Dim txtName As String = ""
    Dim txtVal As String = ""

    'Inherits System.Windows.Forms.Form
    'Private WithEvents printButton As System.Windows.Forms.Button
    Dim cardpay As New CardPayment
    Public INVHNO As Integer = 0
    Public SOHNO As Integer = 0
    Public SOHSYSID As Integer = 0
    Public INVHSYSID As Integer = 0
    Public CSRH_NO As Integer = 0
    Public transtype As String = ""
    Public Customer_Values As New Dictionary(Of String, String)
    Public Salesman_Values As New Dictionary(Of String, String)
    Public Royalty_Values As New Dictionary(Of String, String)
    Dim ItemCodes_Values As New List(Of String)
    Dim Customer_Codes As New List(Of String)
    Dim Salesman_Codes As New List(Of String)
    Dim Item_Codes As New List(Of String)
    Dim Item_BarCodes As New List(Of String)
    Dim Discount_Codes As New List(Of String)
    Dim Location_Codes As New List(Of String)
    Dim Patient_Nos As New List(Of String)
    Dim PaymentTypes As New List(Of String)
    Dim CurrencyTypes As New List(Of String)
    Dim GVTypes As New List(Of String)
    Dim Royalty_Codes As New List(Of String)
    Dim Referal_Codes As New List(Of String)
    Dim Insurance_Codes As New List(Of String)
    Dim LineAddVal_Codes As New List(Of String)
    Dim db As New DBConnection
    Dim MySource_ItemCodes As New AutoCompleteStringCollection()
    Dim MySource_CustCodes As New AutoCompleteStringCollection()
    Dim MySource_SalesmanCodes As New AutoCompleteStringCollection()
    Dim MySource_DiscountCodes As New AutoCompleteStringCollection()
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_PatientNos As New AutoCompleteStringCollection()
    Dim MySource_PaymentTypes As New AutoCompleteStringCollection()
    Dim MySource_CurrencyTypes As New AutoCompleteStringCollection()
    Dim MySource_GVTypes As New AutoCompleteStringCollection()
    Dim List_MySource_DiscountCodes As New List(Of AutoCompleteStringCollection)
    Dim MySource_Royalty As New AutoCompleteStringCollection()
    Dim MySource_Referal As New AutoCompleteStringCollection()
    Dim MySource_Insurance As New AutoCompleteStringCollection()
    Dim MySource_LineAddVals As New AutoCompleteStringCollection()
    Private txtItemCode As New List(Of TextBox)
    Private txtItemDesc As New List(Of TextBox)
    Private txtItemQty As New List(Of TextBox)
    Private txtItemPrice As New List(Of TextBox)
    Private txtItemDisc As New List(Of ComboBox)
    Private txtItemDisamt As New List(Of TextBox)
    Private txtItemNetamt As New List(Of TextBox)
    Private txtItemAddval As New List(Of TextBox)
    Private txtItemAddvalCode As New List(Of TextBox)
    Private picItemEdit As New List(Of PictureBox)
    Private picItemDel As New List(Of PictureBox)
    'Private pnlItemHold As New List(Of Panel)
    Private custDetails(35) As String
    Private itemScroll_boolean As Boolean = False
    Dim rptList As New List(Of String())
    Dim expenseAmount As Double = 0
    Dim salesorders As String = ""

    Dim mprintDocument As New PrintDocument
    Dim mPrintBitMap As Bitmap

    Dim AddvalueButtonclicked As Boolean = False

    Public lastActiveItem As String = ""
    Dim patientnovalue As String = ""


    'Dim PC_Account_Year As String = 9
    'Dim PC_CAL_Year As String = 2012
    'Dim PC_CAL_Period As String = 9

    Dim ex As PrintPageEventArgs
    Dim startx As Integer
    Dim starty As Integer
    Dim endy As Integer
    Dim endx As Integer
    Dim finalx As Integer
    Dim finaly As Integer
    Dim mdown As Boolean
    Dim valx As Boolean
    Dim valy As Boolean

    Dim holderHeight As Integer
    Dim holderWidth As Integer
    Dim piclocationX As Integer
    Dim maxInvPOS As Integer
    '**************Declaring Panel sizes at form load******************
    Dim itmPanelSizeH As Integer
    Dim itmPanelSizeW As Integer
    '******************************************************************

    'Declare Sub Sleep Lib "kernel32.dll" (ByVal Milliseconds As Integer)
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, _
                                                                                ByVal nIndex As Integer) _
                                                                                As Integer
    Private Const GWL_STYLE As Integer = (-16)
    Private Const WS_VSCROLL As Integer = &H200000

    Dim totds As New DataSet
    Dim totcount As Integer = 0
    Dim toti As Integer = 0
    Dim flagval As Integer
    Dim stQuery As String = ""
    Dim ds As DataSet
    Dim dt As New DataTable
    Dim Count As Integer = 0
    Dim itemlist, conditionst As String
    Dim groupval As String = ""
    Dim strArrLocfrom As Array
    Dim strArrLocto As Array
    Private WithEvents TestWorker As System.ComponentModel.BackgroundWorker


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        'CreateItemRow()
        SetResolution()
        Try
            itmPanelSizeH = pnlItemDetails.Height
            itmPanelSizeW = pnlItemDetails.Width
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If

            If Location_Setup_Values("A_BK_DATE") = "1" Then
                dtpick.Enabled = True
                Dim dtQuery As String
                Dim dt As DataSet
                dtQuery = "select to_char(sysdate,'dd-mm-yyyy') from dual"
                errLog.WriteToErrorLog(dtQuery, "", "A_BK_DATE  query")
                ''MAILLOGValues.Add(MAILLOGROWNUM + 1, "A_BK_DATE  query -  " & dtQuery)
                dt = db.SelectFromTableODBC(dtQuery)
                dtpick.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)
            Else
                dtpick.Enabled = False
                Dim dtQuery As String
                Dim dt As DataSet
                dtQuery = "select to_char(sysdate,'dd-mm-yyyy') from dual"
                errLog.WriteToErrorLog(dtQuery, "", "A_BK_DATE  query")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "A_BK_DATE  query  -  " & dtQuery)
                dt = db.SelectFromTableODBC(dtQuery)
                dtpick.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)
            End If

            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String
            stQuery = "SELECT CUST_CODE ,CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) order by CUST_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Customer_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                Customer_Codes.Add(row.Item(0).ToString)
                txtCustomerCode.Items.Add(row.Item(0).ToString & " - " & row.Item(1).ToString)
                count = count - 1
                i = i + 1
            End While
            'MySource_CustCodes.AddRange(Customer_Codes.ToArray)
            'txtCustomerCode.AutoCompleteCustomSource = MySource_CustCodes
            'txtCustomerCode.AutoCompleteMode = AutoCompleteMode.Append
            'txtCustomerCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "SELECT SM_CODE || ' - ' ||  SM_NAME , SM_NAME  FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
            errLog.WriteToErrorLog("Trans Salesman query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans Salesman query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            Salesman_Codes.Clear()
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Salesman_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                Salesman_Codes.Add(row.Item(0).ToString)
                txtSalesmanCode.Items.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While

            'MySource_SalesmanCodes.AddRange(Salesman_Codes.ToArray)
            'txtSalesmanCode.AutoCompleteCustomSource = MySource_SalesmanCodes
            'txtSalesmanCode.AutoCompleteMode = AutoCompleteMode.Append
            'txtSalesmanCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "select distinct pm_locn_code from om_patient_master a,om_location b where a.pm_locn_code=b.locn_code"
            errLog.WriteToErrorLog("Trans Patient Location query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans Patient Location query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Location_Codes.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
            MySource_LocationCodes.AddRange(Location_Codes.ToArray)
            txtLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
            txtLocationCode.AutoCompleteMode = AutoCompleteMode.Append
            txtLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "select LOCN_CODE as loccode, LOCN_CODE || '-' || LOCN_SHORT_NAME as locdisplay from crm_om_location order by locdisplay"
            errLog.WriteToErrorLog("Stock Location query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Stock Location query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            cbLocationfrom.Items.Add("All")
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                cbLocationfrom.Items.Add(row.Item(1))
                count = count - 1
                i = i + 1
            End While

            stQuery = "select PPD_CODE from OM_POS_PAYMENT_DET"
            errLog.WriteToErrorLog("Trans PPD_CODE query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans PPD_CODE query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                PaymentTypes.Add(row.Item(0).ToString)
                cmbPayType.Items.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
            'MySource_PaymentTypes.AddRange(PaymentTypes.ToArray)
            'cmbPayType1.AutoCompleteCustomSource = MySource_PaymentTypes
            'cmbPayType1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'cmbPayType1.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "SELECT VSSV_CODE,VSSV_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='GIFT_VOUCHER' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S')"
            errLog.WriteToErrorLog("Trans GiftVouchers query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans GiftVouchers query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                GVTypes.Add(row.Item(0).ToString)
                txtGiftVoucherCode.Items.Add(row.Item(0).ToString & " - " & row.Item(1).ToString)
                count = count - 1
                i = i + 1
            End While
            'MySource_GVTypes.AddRange(GVTypes.ToArray)
            'txtGiftVoucherCode.AutoCompleteCustomSource = MySource_GVTypes
            'txtGiftVoucherCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtGiftVoucherCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            stQuery = "select CURR_CODE from FM_CURRENCY"
            errLog.WriteToErrorLog("Trans CURR_CODE query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans CURR_CODE query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                CurrencyTypes.Add(row.Item(0).ToString)
                txtCurrencyType.Items.Add(row.Item(0).ToString)
                txtGiftVoucherCurrType.Items.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
            'MySource_CurrencyTypes.AddRange(CurrencyTypes.ToArray)
            'txtCurrencyType.AutoCompleteCustomSource = MySource_CurrencyTypes
            'txtCurrencyType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtCurrencyType.AutoCompleteSource = AutoCompleteSource.CustomSource

            'txtGiftVoucherCurrType.AutoCompleteCustomSource = MySource_CurrencyTypes
            'txtGiftVoucherCurrType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtGiftVoucherCurrType.AutoCompleteSource = AutoCompleteSource.CustomSource

            txtCurrencyType.Text = "AED"
            txtGiftVoucherCurrType.Text = "AED"


            stQuery = "select INSURANCE_CODE FROM PERF_INSURANCE"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                'Royalty_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                Insurance_Codes.Add(row.Item(0).ToString)
                txtInsuranceCode.Items.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
            'txtInsuranceCode.AutoCompleteSource = AutoCompleteSource.None
            'MySource_Insurance.AddRange(Insurance_Codes.ToArray)
            'txtInsuranceCode.AutoCompleteCustomSource = MySource_Insurance
            'txtInsuranceCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'txtInsuranceCode.AutoCompleteSource = AutoCompleteSource.CustomSource


            stQuery = "SELECT PL_CURR_CODE FROM OM_PRICE_LIST WHERE '" & dtpick.Value.ToString("dd-MMM-yy") & "' BETWEEN PL_EFF_FROM_DT AND PL_EFF_TO_DT AND PL_COMP_CODE = '" & CompanyCode & "' AND PL_CODE = '" + Setup_Values.Item("PL_CODE") + "' AND PL_FRZ_FLAG_NUM=2 "
            errLog.WriteToErrorLog("Trans PL_CURR_CODE query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans PL_CURR_CODE query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                Currency_Code = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            stQuery = "SELECT CER_EXG_RATE FROM FM_EXCHANGE_RATE WHERE CER_CONV_FM_CURR_CODE = '" & Currency_Code & "' AND CER_CONV_TO_CURR_CODE = '" & Currency_Code & "' AND ((('AED' <> 'AED') AND CER_EXG_RATE_TYPE= 'B') OR ('AED' = 'AED')) AND '" & dtpick.Value.ToString("dd-MMM-yy") & "' BETWEEN CER_EFF_FRM_DT AND CER_EFF_TO_DT"
            errLog.WriteToErrorLog(stQuery, "", "Exchange rate query")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Exchange rate query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                Exchange_Rate = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            stQuery = "SELECT PC_ACNT_YEAR, PC_CAL_YEAR, PC_CAL_PERIOD FROM OS_PERIOD_CLOSE, OM_LOCATION, OM_ACNT_PERIOD WHERE LOCN_CLOSING_GROUP = PC_CLOSE_GROUP_CODE AND PC_COMP_CODE = APER_COMP_CODE AND PC_ACNT_YEAR = APER_ACNT_YEAR AND PC_CAL_YEAR = APER_CAL_YEAR AND PC_CAL_PERIOD = APER_CAL_MONTH AND PC_COMP_CODE = '" & CompanyCode & "' AND LOCN_CODE = '" & Location_Code & "' AND '" & dtpick.Value.ToString("dd-MMM-yy") & "' BETWEEN APER_FRM_DT AND APER_TO_DT AND NVL(PC_STATUS, 'X') = 'O'"
            'stQuery = "SELECT PC_ACNT_YEAR, PC_CAL_YEAR, PC_CAL_PERIOD FROM OS_PERIOD_CLOSE, OM_LOCATION, OM_ACNT_PERIOD WHERE LOCN_CLOSING_GROUP = PC_CLOSE_GROUP_CODE AND PC_COMP_CODE = APER_COMP_CODE AND PC_ACNT_YEAR = APER_ACNT_YEAR AND PC_CAL_YEAR = APER_CAL_YEAR AND PC_CAL_PERIOD = APER_CAL_MONTH AND PC_COMP_CODE = '" & CompanyCode & "' AND LOCN_CODE = '" & Location_Code & "' AND '" & "21-Apr-2012" & "' BETWEEN APER_FRM_DT AND APER_TO_DT AND NVL(PC_STATUS, 'X') = 'O'"
            errLog.WriteToErrorLog(stQuery, "", "Account Year query")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Account Year query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                PC_Account_Year = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                PC_CAL_Year = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                PC_CAL_Period = ds.Tables("Table").Rows.Item(0).Item(2).ToString
            End If

            If dtpick.Enabled Then
                dtpick.Select()
            Else
                txtCustomerCode.Select()
            End If

            CreateItemRow()

            stQuery = "SELECT TXN_CODE,TXN_TYPE  FROM OM_TXN WHERE TXN_CODE = 'POSINV' AND TXN_TYPE = 'INV'"
            errLog.WriteToErrorLog("Trans TXN CODE query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans TXN CODE query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                TXN_Code = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                TXN_Type = ds.Tables("Table").Rows.Item(0).Item(1).ToString
            End If

            stQuery = "select CUST_NAME from OM_CUSTOMER where CUST_CODE='" & Setup_Values("CUST_CODE") & "'"
            errLog.WriteToErrorLog("CUST NAME OM_CUSTOMER query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "CUST NAME OM_CUSTOMER query -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                txtCustomerCode.SelectedText = Setup_Values("CUST_CODE").Trim & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                txtCustomerName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                'txtCustomerCode.Text = Setup_Values("CUST_CODE") & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            ds = New DataSet
            stQuery = "SELECT CPT_TERM_CODE, OM_CUST_PAYMENT_TERM.ROWID FROM OM_CUST_PAYMENT_TERM WHERE CPT_CUST_CODE = '" & txtCustomerCode.Text.Split(" - ")(0).ToString & "'"
            errLog.WriteToErrorLog("Trans CPT TERM CODE query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Trans CPT TERM CODE query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                CPT_TermCode = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            cmbPayType.Text = CPT_TermCode

            Dim tt As New ToolTip()
            tt.SetToolTip(btnPatient, "Press F1 to Link Patient!")
            tt.SetToolTip(btnAddItem, "Press F2 to Link Patient!")
            tt.SetToolTip(btnPayments, "Press F3 to Link Patient!")
            MainGroup()
            cbLocationfrom.Text = Location_Code
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbmaingrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbmaingrp.LostFocus
        ItemLoad()
    End Sub

    Public Sub ItemLoad()
        Try
            Dim ds As DataSet
            cmbsubgrp.Items.Clear()
            cmbsubgrp.Text = ""
            Dim stQuery As String = ""
            stQuery = "SELECT AD_CODE AS CD_ITEM_ANLY_CODE_01, AD_NAME AS CD_ITEM_ANLY_NAME_01, AD_SHORT_NAME  AS CD_ITEM_ANAY_SH_NAME_01 FROM  OM_ANALYSIS_DETAIL WHERE AD_ANLY_NO=2 AND AD_ANLY_TYPE='ITEM' AND AD_PARENT_CODE like '" + cmbmaingrp.Text + "%'"
            errLog.WriteToErrorLog("Sub Group Query", stQuery, "")
            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
            ''MAILLOGValues.Add(MAILLOGROWNUM, "Sub Group Query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            cmbsubgrp.Items.Clear()
            'If ds.Tables("Table").Rows.Count > 0 Then
            '    cmbsubgrp.Text = ds.Tables("Table").Rows(0).Item(0).ToString
            'End If
            For i As Integer = 0 To ds.Tables("Table").Rows.Count - 1
                cmbsubgrp.Items.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
                'Sub_Group.Add(ds.Tables("Table").Rows(i).Item(0).ToString)
            Next
            'MySource_SubGroup.AddRange(Sub_Group.ToArray)
            'txtSubGroup.AutoCompleteCustomSource = MySource_SubGroup
            'txtSubGroup.AutoCompleteMode = AutoCompleteMode.Suggest
            'txtSubGroup.AutoCompleteSource = AutoCompleteSource.CustomSource
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Public Sub CreateItemRow()
        'Me.Text = ((GetWindowLong(pnlItemDetails.Handle, GWL_STYLE) And WS_VSCROLL) = WS_VSCROLL)
        Try
            Me.pnlItemDetails.AutoScrollPosition = New System.Drawing.Point(0, 0)
            Dim txt As TextBox
            Dim n As Integer
            n = txtItemCode.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmHead.Location.X, (n * 24))
                .Name = "ItemCode" & n.ToString
                .Size = New Size(lblItmHead.Width, 20)
            End With
            AddHandler txt.GotFocus, AddressOf Me.FindItem_GotFocus
            AddHandler txt.PreviewKeyDown, AddressOf Me.FindItem_PreviewKeyDown
            AddHandler txt.KeyDown, AddressOf Me.FindItem
            AddHandler txt.TextChanged, AddressOf Me.FindItem_TextChanged
            'AddHandler txt.LostFocus, AddressOf Me.FindItem_LostFocus
            Me.txtItemCode.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n2 As Integer
            n2 = txtItemDesc.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmDesc.Location.X, (n2 * 24))
                .Name = "ItemDesc" & n2.ToString
                .Size = New Size(lblItmDesc.Width, 20)
            End With
            txt.ReadOnly = True
            txt.BackColor = Color.White
            AddHandler txt.GotFocus, AddressOf Me.FindItemDesc_GotFocus
            AddHandler txt.KeyDown, AddressOf Me.FindItemDesc_KeyDown
            Me.txtItemDesc.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n3 As Integer
            n3 = txtItemQty.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmQty.Location.X, (n3 * 24))
                .Name = "ItemQty" & n3.ToString
                .Size = New Size(lblItmQty.Width, 20)
                .Text = "0"
            End With
            AddHandler txt.GotFocus, AddressOf Me.FindItemQty_GotFocus
            AddHandler txt.KeyPress, AddressOf Me.FindItmQty_KeyPress
            AddHandler txt.Leave, AddressOf Me.FindItmQty_Leave
            AddHandler txt.KeyDown, AddressOf Me.FindItemQty
            AddHandler txt.TextChanged, AddressOf Me.FindItmQty_TextChanged

            txt.TextAlign = HorizontalAlignment.Center
            Me.txtItemQty.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n4 As Integer
            n4 = txtItemPrice.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmPrice.Location.X, (n4 * 24))
                .Name = "ItemPrice" & n4.ToString
                .Size = New Size(lblItmPrice.Width, 20)
                .Text = "0"
            End With
            txt.TextAlign = HorizontalAlignment.Right
            txt.ReadOnly = True
            txt.BackColor = Color.White
            AddHandler txt.GotFocus, AddressOf Me.FindItemPrice_GotFocus
            AddHandler txt.KeyDown, AddressOf Me.FindItemPrice_KeyDown
            Me.txtItemPrice.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n5 As Integer
            n5 = txtItemDisc.Count + 1
            Dim txtcmb As ComboBox
            txtcmb = New ComboBox
            With txtcmb
                .Location = New Point(lblItmDiscCode.Location.X, (n5 * 24))
                .Name = "ItemDisc" & n5.ToString
                .Size = New Size(lblItmDiscCode.Width, 20)
                .DropDownStyle = ComboBoxStyle.DropDownList
            End With
            'MySource_DiscountCodes.AddRange(Discount_Codes.ToArray)
            'txt.AutoCompleteCustomSource = MySource_DiscountCodes
            'txt.AutoCompleteMode = AutoCompleteMode.Append
            'txt.AutoCompleteSource = AutoCompleteSource.CustomSource
            'txtcmb.TextAlign = HorizontalAlignment.Center
            AddHandler txtcmb.KeyDown, AddressOf Me.FindItemDisc
            AddHandler txtcmb.GotFocus, AddressOf Me.FindItemDisc_GotFocus
            AddHandler txtcmb.LostFocus, AddressOf Me.FindItemDisc_LostFocus
            AddHandler txtcmb.TextChanged, AddressOf Me.FindItemDisc_TextChanged
            Me.txtItemDisc.Add(txtcmb)
            Me.pnlItemDetails.Controls.Add(txtcmb)

            Dim n6 As Integer
            n6 = txtItemDisamt.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmDiscAmt.Location.X, (n6 * 24))
                .Name = "ItemDisamt" & n6.ToString
                .Size = New Size(lblItmDiscAmt.Width, 20)
                .Text = "0"
                .Cursor = Cursors.Hand
            End With
            txt.TextAlign = HorizontalAlignment.Right
            txt.ReadOnly = False
            txt.BackColor = Color.White
            AddHandler txt.GotFocus, AddressOf Me.FindItemDisamt_GotFocus
            AddHandler txt.KeyDown, AddressOf Me.FindItemDisamt
            AddHandler txt.KeyPress, AddressOf Me.FindItemDisamt_KeyPress
            AddHandler txt.Leave, AddressOf Me.FindItemDisamt_Leave
            AddHandler txt.MouseHover, AddressOf Me.FindItemDisamt_Hover
            'AddHandler txt.TextChanged, AddressOf Me.FindItemDisamt_TextChanged
            AddHandler txt.LostFocus, AddressOf Me.FindItemDisamt_LostFocus
            Dim ttDisamt As New ToolTip()
            ttDisamt.SetToolTip(txt, "Press F12 for Discount Percentage!")
            Me.txtItemDisamt.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n7 As Integer
            n7 = txtItemNetamt.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmNetAmt.Location.X, (n7 * 24))
                .Name = "ItemNetamt" & n7.ToString
                .Size = New Size(lblItmNetAmt.Width, 20)
                .Text = "0"
            End With
            txt.ReadOnly = True
            txt.BackColor = Color.White
            txt.TextAlign = HorizontalAlignment.Right
            AddHandler txt.GotFocus, AddressOf Me.FindItemNetamt_GotFocus
            AddHandler txt.KeyDown, AddressOf Me.FindItemNetamt_KeyDown
            Me.txtItemNetamt.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim n8 As Integer
            n8 = txtItemAddval.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmAddValue.Location.X, (n8 * 24))
                .Name = "ItemAddval" & n8.ToString
                .Size = New Size(lblItmAddValue.Width, 20)
                .Text = "0"
                .ReadOnly = True
                .BackColor = Color.White
            End With
            AddHandler txt.KeyDown, AddressOf Me.FindItemAddval
            AddHandler txt.KeyPress, AddressOf Me.FindItemAddval_KeyPress
            AddHandler txt.Leave, AddressOf Me.FindItemAddval_Leave
            AddHandler txt.GotFocus, AddressOf Me.FindItemAddval_GotFocus
            AddHandler txt.LostFocus, AddressOf Me.FindItemAddval_LostFocus
            txt.TextAlign = HorizontalAlignment.Right
            Me.txtItemAddval.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)


            Dim n9 As Integer
            n9 = txtItemAddvalCode.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmAddValue.Location.X + lblItmAddValue.Width + 1, (n9 * 24))
                .Name = "ItemAddvalCode" & n9.ToString
                .Size = New Size(6, 20)
                .Text = ""
                .Visible = False
            End With
            Me.txtItemAddvalCode.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim pic As PictureBox
            Dim n10 As Integer
            n10 = picItemDel.Count + 1
            pic = New PictureBox
            With pic
                .Location = New Point(lblItmDel.Location.X + lblItmDel.Width / 4, (n10 * 24))
                .Name = "ItemDel" & n10.ToString
                .Size = New Size(lblItmDel.Width - lblItmDel.Width / 2, 20)
            End With
            Me.picItemDel.Add(pic)
            pic.Image = My.Resources.recycle_full
            pic.SizeMode = PictureBoxSizeMode.StretchImage
            pic.Cursor = Cursors.Hand
            AddHandler pic.Click, AddressOf Me.RemoveItemRow
            Dim tt As New ToolTip()
            tt.SetToolTip(pic, "Delete")
            Me.pnlItemDetails.Controls.Add(pic)

            lastActiveItem = n.ToString

            'For i = 0 To pnlItemHold.Count - 1
            '    pnlItemHold(i).BackColor = Color.PaleTurquoise
            'Next
            'Dim n11 As Integer
            'Dim pnl As Panel
            'n11 = pnlItemHold.Count + 1
            'pnl = New Panel
            'With pnl
            '    .Location = New Point(lblItmHead.Location.X - 1, (n11 * 23))
            '    .Name = "pnlItemHold" & n11.ToString
            '    .Size = New Size((lblItmAddValue.Location.X + lblItmAddValue.Width + 2) - lblItmHead.Location.X, 23)
            '    .BackColor = Color.DarkCyan
            'End With
            'Me.pnlItemHold.Add(pnl)
            'Me.pnlItemDetails.Controls.Add(pnl)

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub RemoveItemRow(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded!", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim text As String = DirectCast(sender, PictureBox).Name
            Dim parts As String() = text.Split(New String() {"ItemDel"}, StringSplitOptions.None)
            Dim rowPosition As Integer = Convert.ToInt64(parts(1).ToString)

            If Not transtype = "Sales Invoice" Or transtype = "Sales Return" Then
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & rowPosition, True)
                Dim selitemcode As String = objectsToBeFound(0).Text
                If TXN_Code = "POSINV" Then
                    Dim itemoccurence_count As Integer = 0
                    For i = 1 To txtItemCode.Count
                        Dim itemFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & i, True)
                        If selitemcode.Equals(itemFound(0).Text) Then
                            itemoccurence_count = itemoccurence_count + 1
                        End If
                    Next
                    If Not itemoccurence_count > 1 Then
                        Dim stQuery As String
                        stQuery = "delete from OT_POS_INVOICE_ITEM_LOG where PROD_INVI_INVH_SYS_ID = " & INVHSYSID.ToString & " and (PRODCODE='" & selitemcode & "' or PRODBARCODE='" & selitemcode & "')"
                        errLog.WriteToErrorLog("Delete ITEMLOG", stQuery, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "Delete ITEMLOG  -  " & stQuery)
                        db.SaveToTableODBC(stQuery)

                    End If
                ElseIf TXN_Code = "SO" Then
                    Dim itemoccurence_count As Integer = 0
                    For i = 1 To txtItemCode.Count
                        Dim itemFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & i, True)
                        If selitemcode.Equals(itemFound(0).Text) Then
                            itemoccurence_count = itemoccurence_count + 1
                        End If
                    Next
                    If Not itemoccurence_count > 1 Then
                        Dim stQuery As String
                        stQuery = "delete from OT_POS_SO_ITEM_LOG where PROD_SOI_SOH_SYS_ID = " & SOHSYSID.ToString & " and (PRODCODE='" & selitemcode & "' or PRODBARCODE='" & selitemcode & "')"
                        errLog.WriteToErrorLog("Delete ITEMLOG", stQuery, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "Delete ITEMLOG  -  " & stQuery)
                        db.SaveToTableODBC(stQuery)
                    End If
                End If
            End If

            Dim rowsCount As Integer = picItemDel.Count
            If rowPosition = rowsCount Then
                Dim itmcode_name As String = "ItemCode" & parts(1).ToString
                Dim itmdesc_name As String = "ItemDesc" & parts(1).ToString
                Dim itmqty_name As String = "ItemQty" & parts(1).ToString
                Dim itmprice_name As String = "ItemPrice" & parts(1).ToString
                Dim itmdisc_name As String = "ItemDisc" & parts(1).ToString
                Dim itmdisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim itmnetamt_name As String = "ItemNetamt" & parts(1).ToString
                Dim itmaddval_name As String = "ItemAddval" & parts(1).ToString
                Dim itmaddvalCode_name As String = "ItemAddvalCode" & parts(1).ToString
                Dim itmdel_name As String = "ItemDel" & parts(1).ToString
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find(itmcode_name, True)
                txtItemCode.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdesc_name, True)
                txtItemDesc.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmqty_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemQty.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmprice_name, True)
                txtItemPrice.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisc_name, True)
                txtItemDisc.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisamt_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemDisamt.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmnetamt_name, True)
                txtItemNetamt.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddval_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemAddval.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddvalCode_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemAddvalCode.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdel_name, True)
                picItemDel.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                For k = 1 To txtItemQty.Count
                    Dim itmqtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)
                    Dim itmDescFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" + k.ToString, True)
                    If itmDescFound(0).Text = "" Then
                    Else
                        FindItmQty_TextChanged(itmqtyFound(0), EventArgs.Empty)
                    End If
                Next
                CreateItemRow()
                objectsToBeFound = Me.Controls.Find(itmcode_name, True)
                objectsToBeFound(0).Select()
                Calculate_TotalAmount()

            Else
                Dim LastItem_From As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" + rowsCount.ToString, True)
                If LastItem_From(0).Text = "" Then
                    Dim itmcode_name1 As String = "ItemCode" & rowsCount.ToString
                    Dim itmdesc_name1 As String = "ItemDesc" & rowsCount.ToString
                    Dim itmqty_name1 As String = "ItemQty" & rowsCount.ToString
                    Dim itmprice_name1 As String = "ItemPrice" & rowsCount.ToString
                    Dim itmdisc_name1 As String = "ItemDisc" & rowsCount.ToString
                    Dim itmdisamt_name1 As String = "ItemDisamt" & rowsCount.ToString
                    Dim itmnetamt_name1 As String = "ItemNetamt" & rowsCount.ToString
                    Dim itmaddval_name1 As String = "ItemAddval" & rowsCount.ToString
                    Dim itmaddvalcode_name1 As String = "ItemAddvalCode" & rowsCount.ToString
                    Dim itmdel_name1 As String = "ItemDel" & rowsCount.ToString
                    Dim objectsToBeFound1 As System.Windows.Forms.Control() = Me.Controls.Find(itmcode_name1, True)
                    txtItemCode.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmdesc_name1, True)
                    txtItemDesc.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmqty_name1, True)
                    txtItemQty.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmprice_name1, True)
                    txtItemPrice.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmdisc_name1, True)
                    txtItemDisc.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmdisamt_name1, True)
                    txtItemDisamt.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmnetamt_name1, True)
                    txtItemNetamt.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmaddval_name1, True)
                    txtItemAddval.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmaddvalcode_name1, True)
                    txtItemAddvalCode.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    objectsToBeFound1 = Me.Controls.Find(itmdel_name1, True)
                    picItemDel.RemoveAt(rowsCount - 1)
                    Me.pnlItemDetails.Controls.Remove(objectsToBeFound1(0))
                    rowsCount = rowsCount - 1
                End If

                For i = rowPosition + 1 To rowsCount

                    Dim itmdesc_name_into As String = "ItemDesc" & i - 1
                    Dim itmdesc_name_from As String = "ItemDesc" & i
                    Dim objectsToBeFound_From As System.Windows.Forms.Control() = Me.Controls.Find(itmdesc_name_from, True)

                    Dim objectsToBeFound_Into As System.Windows.Forms.Control() = Me.Controls.Find(itmdesc_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmcode_name_into As String = "ItemCode" & i - 1
                    Dim itmcode_name_from As String = "ItemCode" & i
                    objectsToBeFound_From = Me.Controls.Find(itmcode_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmcode_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmqty_name_into As String = "ItemQty" & i - 1
                    Dim itmqty_name_from As String = "ItemQty" & i
                    objectsToBeFound_From = Me.Controls.Find(itmqty_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmqty_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmprice_name_into As String = "ItemPrice" & i - 1
                    Dim itmprice_name_from As String = "ItemPrice" & i
                    objectsToBeFound_From = Me.Controls.Find(itmprice_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmprice_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmdisc_name_into As String = "ItemDisc" & i - 1
                    Dim itmdisc_name_from As String = "ItemDisc" & i
                    objectsToBeFound_From = Me.Controls.Find(itmdisc_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmdisc_name_into, True)
                    Dim tbxinto As System.Windows.Forms.ComboBox = objectsToBeFound_Into(0)
                    Dim tbxfrom As System.Windows.Forms.ComboBox = objectsToBeFound_From(0)
                    tbxinto.AutoCompleteCustomSource = tbxfrom.AutoCompleteCustomSource
                    tbxinto.AutoCompleteSource = tbxfrom.AutoCompleteSource
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmdisamt_name_into As String = "ItemDisamt" & i - 1
                    Dim itmdisamt_name_from As String = "ItemDisamt" & i
                    objectsToBeFound_From = Me.Controls.Find(itmdisamt_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmdisamt_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmnetamt_name_into As String = "ItemNetamt" & i - 1
                    Dim itmnetamt_name_from As String = "ItemNetamt" & i
                    objectsToBeFound_From = Me.Controls.Find(itmnetamt_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmnetamt_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmaddval_name_into As String = "ItemAddval" & i - 1
                    Dim itmaddval_name_from As String = "ItemAddval" & i
                    objectsToBeFound_From = Me.Controls.Find(itmaddval_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmaddval_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmaddvalcode_name_into As String = "ItemAddvalCode" & i - 1
                    Dim itmaddvalcode_name_from As String = "ItemAddvalCode" & i
                    objectsToBeFound_From = Me.Controls.Find(itmaddvalcode_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmaddvalcode_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                Next

                Dim itmcode_name As String = "ItemCode" & txtItemCode.Count
                Dim itmdesc_name As String = "ItemDesc" & txtItemDesc.Count
                Dim itmqty_name As String = "ItemQty" & txtItemQty.Count
                Dim itmprice_name As String = "ItemPrice" & txtItemPrice.Count
                Dim itmdisc_name As String = "ItemDisc" & txtItemDisc.Count
                Dim itmdisamt_name As String = "ItemDisamt" & txtItemDisamt.Count
                Dim itmnetamt_name As String = "ItemNetamt" & txtItemNetamt.Count
                Dim itmaddval_name As String = "ItemAddval" & txtItemAddval.Count
                Dim itmaddvalcode_name As String = "ItemAddvalCode" & txtItemAddval.Count
                Dim itmdel_name As String = "ItemDel" & picItemDel.Count
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find(itmcode_name, True)
                txtItemCode.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdesc_name, True)
                txtItemDesc.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmqty_name, True)
                txtItemQty.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmprice_name, True)
                txtItemPrice.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisc_name, True)
                txtItemDisc.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisamt_name, True)
                txtItemDisamt.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmnetamt_name, True)
                txtItemNetamt.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddval_name, True)
                txtItemAddval.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddvalcode_name, True)
                txtItemAddvalCode.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdel_name, True)
                picItemDel.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                For k = 1 To txtItemQty.Count
                    Dim itmqtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)
                    Dim itmDescFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" + k.ToString, True)
                    If itmDescFound(0).Text = "" Then
                    Else
                        FindItmQty_TextChanged(itmqtyFound(0), EventArgs.Empty)
                    End If
                Next
                Calculate_TotalAmount()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub Calculate_TotalAmount()
        Try
            Dim netamt_total As Double = 0
            Dim addval_total As Double = 0
            For i = 0 To txtItemNetamt.Count - 1
                If txtItemNetamt(i).Text = "" Then
                Else
                    netamt_total = netamt_total + Convert.ToDouble(txtItemNetamt(i).Text.ToString)
                End If
            Next
            For i = 0 To txtItemAddval.Count - 1
                If txtItemAddval(i).Text = "" Then
                Else
                    addval_total = addval_total + Convert.ToDouble(txtItemAddval(i).Text.ToString)
                End If

            Next
            txtTotalAmount.Text = Round((netamt_total + addval_total), 3).ToString
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemNetamt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemNetamt"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemDisamt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemPrice_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemPrice"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemQty_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemQty"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemDesc_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemDesc"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItem_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, TextBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemCode"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemAddval_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not CheckShiftTimings() Then
            MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
            Exit Sub
        End If

        If AddvalueButtonclicked Then
            AddvalueButtonclicked = False
        Else
            Exit Sub
        End If

        Dim text As String = DirectCast(sender, TextBox).Name
        Dim parts As String() = text.Split(New String() {"ItemAddval"}, StringSplitOptions.None)
        Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" & parts(1).ToString, True)
        If ItmPriceFound(0).Text = "" Then
            Exit Sub
        End If

        Dim activectl As String = Me.ActiveControl.Name
        txtLineAddValCode.Select()
        itemrowAddvalName.Text = activectl

        If Not pnlLineAddValue.Visible Then

            For Each ctl As Control In pnlItemDetails.Controls
                If ctl.Name.Equals(activectl) Then

                    If ItemCheck_AddVal() Then
                        Try
                            Dim stQuery As String
                            Dim ds As DataSet
                            Dim count As Integer
                            Dim i As Integer
                            Dim row As System.Data.DataRow
                            stQuery = "SELECT EXP_CODE, EXP_NAME FROM OM_EXPENSE,OM_EXPENSE_VALIDITY WHERE EXP_CODE = EV_EXP_CODE AND EXP_TYPE = 'R' AND NVL(EXP_FRZ_FLAG_NUM,2) = 2 AND EXP_LEVEL IN ('D','B') AND EV_TRAN_TYPE = '" & TXN_Type & "'"
                            errLog.WriteToErrorLog("OM_EXPENSE QUERY", stQuery, "")
                            ' 'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            ''MAILLOGValues.Add(MAILLOGROWNUM, "OM_EXPENSE QUERY  -  " & stQuery)
                            ds = db.SelectFromTableODBC(stQuery)
                            count = ds.Tables("Table").Rows.Count
                            i = 0
                            LineAddVal_Codes.Clear()
                            txtLineAddValCode.Items.Clear()
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                LineAddVal_Codes.Add(row.Item(0).ToString)
                                txtLineAddValCode.Items.Add(row.Item(0).ToString)
                                count = count - 1
                                i = i + 1
                            End While

                        Catch ex As Exception
                            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
                        End Try
                    Else
                        Exit Sub
                    End If
                Else

                End If
            Next


            Dim btn As Button = New Button
            With btn
                .Name = "btnLineAddValOK"
                .Location = New Point(txtLineAddValPerc.Location.X, txtLineAddValPerc.Location.Y + 40)
                .Size = New Size(60, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Add"
            End With
            AddHandler btn.Click, AddressOf Me.btnLineAddValOK_Click
            Me.Pnl_Lineaddvalue_details.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnLineAddValCancel"
                .Location = New Point(txtLineAddValPerc.Location.X + 70, txtLineAddValPerc.Location.Y + 40)
                .Size = New Size(60, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Close"
            End With
            AddHandler btn.Click, AddressOf Me.btnLineAddValCancel_Click
            Me.Pnl_Lineaddvalue_details.Controls.Add(btn)

            pnlLineAddValue.Height = Me.Height
            pnlButtonHolder.Visible = False
            pnlButtonHolder.SendToBack()
            pnlLineAddValue.BringToFront()
            'pnl1Patient.Width = Me.Width
            For i = 0 To pnlLineAddValue.Width
                pnlLineAddValue.Location = New Point(Me.Width - i, Me.Height - pnlLineAddValue.Height)
                pnlLineAddValue.Show()
                Threading.Thread.Sleep(0.5)
                i = i + 1
            Next
            txtLineAddValAmount.Text = DirectCast(sender, TextBox).Text
            txtLineAddValAmount_TextChanged(sender, e)
            txtLineAddValCode.Text = Me.Controls.Find("ItemAddvalCode" & parts(1).ToString, True)(0).Text
            txtLineAddValCode_SelectedValueChanged(sender, e)
        End If

        'btnLineAddvalue_Click(sender, e)

    End Sub

    Private Sub FindItemAddval_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender

            Calculate_TotalAmount()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub FindItemDisamt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim text1 As String = DirectCast(sender, TextBox).Name
            Dim parts1 As String() = text1.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
            lastActiveItem = parts1(1).ToString

            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim tbx As System.Windows.Forms.TextBox = sender
            If tbx.Text = "" Then
                tbx.Text = "0"
                Exit Sub
            ElseIf tbx.Text = "0" Then
                Exit Sub
            Else
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
                Dim ItemDisc_name As String = "ItemDisc" & parts(1).ToString
                Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
                Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItemDisc_name, True)
                Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
                Dim itemCode As String = ItmCodeFound(0).Text
                Dim itemDiscCode As String = ItmDiscFound(0).Text
                If itemDiscCode = "" Then
                    tbx.Text = "0"
                    Exit Sub
                End If
                Dim itemQty As String = ItmQtyFound(0).Text
                Dim itemDisamt As String = DirectCast(sender, TextBox).Text
                Dim itemPrice As String = ItmPriceFound(0).Text
                Dim stQueryDisc As String
                stQueryDisc = "SELECT ITEM_ANLY_CODE_01,ITEM_ANLY_CODE_02,ITEM_ANLY_CODE_03,ITEM_ANLY_CODE_04 FROM OM_ITEM where ITEM_CODE='" + itemCode + "' OR ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & itemCode & "')"
                Dim dsDisc As DataSet
                dsDisc = db.SelectFromTableODBC(stQueryDisc)

                Dim countDisc As Integer
                Dim row As System.Data.DataRow
                Dim iDisc As Integer
                Dim anlycode1 As String = ""
                Dim anlycode2 As String = ""
                Dim anlycode3 As String = ""
                Dim anlycode4 As String = ""
                countDisc = dsDisc.Tables("Table").Rows.Count
                iDisc = 0
                While countDisc > 0
                    row = dsDisc.Tables("Table").Rows.Item(iDisc)
                    anlycode1 = row.Item(0).ToString
                    anlycode2 = row.Item(1).ToString
                    anlycode3 = row.Item(2).ToString
                    anlycode4 = row.Item(3).ToString
                    countDisc = countDisc - 1
                    iDisc = iDisc + 1
                End While
                Try
                    stQueryDisc = ""
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DR_DISC_PERC DISC_PERC,DR_DISC_AMT DISC_AMT,DR_EFF_TO_DT EFF_TO_DT FROM "
                    stQueryDisc = stQueryDisc + " OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_RATE "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_RATE.DR_DISC_CODE AND '" + itemCode + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DR_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' AND FRD_DISC_CODE='" + itemDiscCode + "' "
                    stQueryDisc = stQueryDisc + " UNION "
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DTD_DISC_PERC DISC_PERC,DTD_DISC_AMT DISC_AMT,DTD_EFF_TO_DT EFF_TO_DT "
                    stQueryDisc = stQueryDisc + " FROM OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_THRESHOLD_DETL "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_THRESHOLD_DETL .DTD_DISC_CODE AND '" + itemCode + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DTD_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' AND FRD_DISC_CODE='" + itemDiscCode + "' ORDER BY EFF_TO_DT "
                    'stQueryDisc = "select disc_code,disc_perc from om_discount"
                    dsDisc = db.SelectFromTableODBC(stQueryDisc)
                    countDisc = dsDisc.Tables("Table").Rows.Count
                Catch ex As Exception
                    qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                End Try
                If countDisc < 1 Then
                    'MsgBox("Invalid discount code!")
                    tbx.Text = "0"
                    ItmDiscFound(0).Select()
                    FindItmQty_TextChanged(ItmQtyFound(0), e)
                    Exit Sub
                Else
                    Dim discPerc As String = dsDisc.Tables("Table").Rows.Item(0).Item(2).ToString
                    If Not discPerc = "" Then
                        Dim maxDisamt As Double = (Convert.ToDouble(itemQty) * Convert.ToDouble(itemPrice) * Convert.ToDouble(discPerc)) / 100
                        If Convert.ToDouble(itemDisamt) > maxDisamt Then
                            MsgBox("Discount is greater than the limit")
                            itemDisamt = itemDisamt.Substring(0, itemDisamt.Length - 1)
                            tbx.Text = itemDisamt
                            tbx.Select()
                            FindItmQty_TextChanged(ItmQtyFound(0), e)
                        Else
                            FindItmQty_TextChanged(ItmQtyFound(0), e)
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItmQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dim text1 As String = DirectCast(sender, TextBox).Name
            'Dim parts1 As String() = text1.Split(New String() {"ItemQty"}, StringSplitOptions.None)
            'lastActiveItem = parts1(1).ToString

            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim tbx As System.Windows.Forms.TextBox = sender
            If tbx.Text = "" Or tbx.Text = "0" Then
                Exit Sub
            Else

                Dim qty As Double = Convert.ToDouble(tbx.Text.ToString)
                Dim price As Double
                Dim disamt As Double
                Dim addval As Double
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemQty"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                Dim itemCode As String = ItmCodeFound(0).Text
                If itemCode = "" Then
                    Exit Sub
                End If
                Dim alreadyAddedItemCount As Integer = 0
                For k = 1 To txtItemCode.Count
                    Dim ItmCodeCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + k.ToString, True)
                    If String.Compare(ItmCodeCtl(0).Text, itemCode, True) = 0 Then
                        Dim ItmQtyCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)
                        alreadyAddedItemCount = alreadyAddedItemCount + Convert.ToInt64(ItmQtyCtl(0).Text)
                    Else
                    End If
                Next

                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow
                Dim itemAvailable As Integer = 0
                stQuery = "SELECT ITEM_STK_YN_NUM FROM OM_POS_ITEM WHERE ITEM_CODE = '" & itemCode & "' OR ITEM_BAR_CODE='" & itemCode & "'"
                errLog.WriteToErrorLog("ITEM_STK_YN_NUM Query", stQuery, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "ITEM_STK_YN_NUM QUERY  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                Dim ITEM_STK_YN_NUM As String = ""
                If count > 0 Then
                    ITEM_STK_YN_NUM = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                End If
                'MsgBox("ITEM_STK_YN_NUM " & ITEM_STK_YN_NUM)

                Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
                Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
                price = Convert.ToDouble(ItmPriceFound(0).Text.ToString)
                Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                disamt = Convert.ToDouble(ItmDisamtFound(0).Text.ToString)
                Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
                Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                addval = Convert.ToDouble(ItmAddvalFound(0).Text.ToString)
                Dim ItmAddvalCode_name As String = "ItemAddvalCode" & parts(1).ToString
                Dim ItmAddvalCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddvalCode_name, True)

                Dim ItmNetamt_name As String = "ItemNetamt" & parts(1).ToString
                Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmNetamt_name, True)

                Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
                Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
                Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
                Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
                'ItmNetamtFound(0).Text = Round((((qty * price) - disamt) + addval), 3).ToString
                'MsgBox(alreadyAddedItemCount)
                If ITEM_STK_YN_NUM = "1" Then
                    stQuery = "select item_code from om_pos_item where item_code='" & itemCode & "' or item_bar_code='" & itemCode & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    Dim itemcodeval As String = ""
                    If ds.Tables("Table").Rows.Count > 0 Then
                        itemcodeval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                    End If

                    stQuery = "select (LCS_STK_QTY_BU + LCS_RCVD_QTY_BU) - (LCS_ISSD_QTY_BU+LCS_HOLD_QTY_BU+LCS_REJECT_QTY_BU+LCS_PICK_QTY_BU+LCS_PACK_QTY_BU) as AvailableStk from OS_LOCN_CURR_STK where LCS_ITEM_CODE = '" & itemcodeval & "' and LCS_LOCN_CODE='" & Location_Code & "'"
                    errLog.WriteToErrorLog("Stock Check Query", stQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Stock Check QUERY  -  " & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        itemAvailable = Convert.ToInt64(row.Item(0).ToString)
                    Else
                        itemAvailable = 0
                    End If
                    If Not itemAvailable >= alreadyAddedItemCount Then
                        If Not TXN_Code = "SO" Then
                            ItmCodeFound(0).ForeColor = Color.Red
                            ItmDescFound(0).ForeColor = Color.Red
                            ItmPriceFound(0).ForeColor = Color.Red
                            ItmDiscFound(0).ForeColor = Color.Red
                            ItmDisamtFound(0).ForeColor = Color.Red
                            ItmNetamtFound(0).ForeColor = Color.Red
                            ItmNetamtFound(0).Text = "0"
                            ItmAddvalFound(0).ForeColor = Color.Red
                            ItmAddvalFound(0).Text = "0"
                            tbx.ForeColor = Color.Red
                            Home.ToolStripStatusLabel.Text = "Item '" + ItmCodeFound(0).Text + "' is low"
                            If itemAvailable = 0 Then
                                tbx.Text = "0"
                                MsgBox("Item stock not available in this location, select some other Delivery location!")
                                Exit Sub
                            End If
                            MsgBox("Item '" + ItmCodeFound(0).Text + "' is low")
                        Else
                            Home.ToolStripStatusLabel.Text = "POS"
                            createHeadLog()
                            createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
                            ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
                            Calculate_TotalAmount()
                        End If
                    Else
                        ItmCodeFound(0).ForeColor = Color.Black
                        ItmDescFound(0).ForeColor = Color.Black
                        ItmPriceFound(0).ForeColor = Color.Black
                        ItmDiscFound(0).ForeColor = Color.Black
                        ItmDisamtFound(0).ForeColor = Color.Black
                        ItmNetamtFound(0).ForeColor = Color.Black
                        ItmAddvalFound(0).ForeColor = Color.Black
                        tbx.ForeColor = Color.Black
                        Home.ToolStripStatusLabel.Text = "POS"
                        createHeadLog()
                        createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
                        ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
                        Calculate_TotalAmount()
                    End If
                Else
                    ItmCodeFound(0).ForeColor = Color.Black
                    ItmDescFound(0).ForeColor = Color.Black
                    ItmPriceFound(0).ForeColor = Color.Black
                    ItmDiscFound(0).ForeColor = Color.Black
                    ItmDisamtFound(0).ForeColor = Color.Black
                    ItmNetamtFound(0).ForeColor = Color.Black
                    ItmAddvalFound(0).ForeColor = Color.Black
                    tbx.ForeColor = Color.Black
                    Home.ToolStripStatusLabel.Text = "POS"
                    createHeadLog()
                    createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
                    ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
                    Calculate_TotalAmount()
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemAddval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then
                If Asc(e.KeyChar) = 46 Then
                    If tbx.Text.IndexOf(".") <> -1 Or Val(tbx.Text.Trim & e.KeyChar) >= 4 Then
                        e.Handled = True
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If
            If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Public Function Isnumber(ByVal KCode As String) As Boolean
        If Not IsNumeric(KCode) Then
            MsgBox("Please Enter Numbers only", MsgBoxStyle.OkOnly)
        End If
    End Function


    Private Sub FindItemDisamt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            'Dim tbx As System.Windows.Forms.TextBox = sender
            'If Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then
            '    If Asc(e.KeyChar) = 46 Then
            '        If tbx.Text.IndexOf(".") <> -1 Or Val(tbx.Text.Trim & e.KeyChar) >= 4 Then
            '            e.Handled = True
            '        Else
            '            Exit Sub
            '        End If
            '    Else
            '        Exit Sub
            '    End If
            'End If
            'If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) Then
            '    e.Handled = True
            'End If
            'If Not Isnumber(e.KeyChar) Then
            '    e.KeyChar = ""
            'End If
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") < 0) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") >= 0 And tbx.SelectionLength > 0 And tbx.SelectionStart <= tbx.Text.IndexOf(".") And tbx.SelectionStart + tbx.SelectionLength > tbx.Text.IndexOf("."))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        'Try
        '    Dim tbx As System.Windows.Forms.TextBox = sender
        '    If Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then
        '        If Asc(e.KeyChar) = 46 Then
        '            If tbx.Text.IndexOf(".") <> -1 Or Val(tbx.Text.Trim & e.KeyChar) >= 4 Then
        '                e.Handled = True
        '            Else
        '                'e.Handled = False
        '                Exit Sub
        '            End If
        '        Else
        '            'e.Handled = False
        '            Exit Sub
        '        End If
        '    End If
        '    If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) Then
        '        e.Handled = True
        '    End If
        'Catch ex As Exception
        '    errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        'End Try
    End Sub

    Private Sub FindItemAddval_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double

            If tbx.Text = "" Then
                tbx.Text = 0
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 3)
            Else
                tbx.Text = 0
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FindItemDisamt_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            Dim text As String = DirectCast(sender, TextBox).Name
            Dim parts As String() = text.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
            Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
            If Not ItmQtyFound(0).Text = "0" Or ItmQtyFound(0).Text = "" Then

                If tbx.Text = "" Then
                    tbx.Text = Round(0, 3)
                    'FindItmQty_TextChanged(ItmQtyFound(0), e)
                    Return
                End If
                If Not Double.TryParse(tbx.Text, value) Then
                    tbx.Text = Round(0, 3)
                ElseIf value > 0 Then
                    tbx.Text = Round(value, 3)
                Else
                    tbx.Text = Round(0, 3)
                End If
                If pnlItemDetails.Controls.Find(ItmQty_name, True).Length = 0 Then
                Else
                    'FindItmQty_TextChanged(ItmQtyFound(0), e)
                End If
            Else
                DirectCast(sender, TextBox).Text = "0"
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItmQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FindItmQty_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                'tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Function ItemCheck_AddVal()
        Dim success As Boolean = True
        Try
            For k = 1 To txtItemCode.Count
                Dim ItmCode_name As String = "ItemCode" & k
                Dim ItmDesc_name As String = "ItemDesc" & k
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
                If ItmCodeFound(0).ForeColor = Color.Red Then
                    ItmCodeFound(0).Select()
                    success = False
                    Return success
                    Exit Function
                End If
                If ItmDescFound(0).Text = "" Then
                    Exit For
                End If
                Dim itemCode As String = ItmCodeFound(0).Text
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer

                stQuery = "SELECT ITEM_SHORT_NAME FROM OM_PRICE_LIST_ITEM, OM_ITEM WHERE NVL(ITEM_FRZ_FLAG_NUM,2) = 2 AND NVL(PLI_FRZ_FLAG_NUM,2)=2 AND ITEM_CODE = PLI_ITEM_CODE AND (PLI_ITEM_CODE='" + itemCode + "' OR ITEM_HARMONISED_CODE='" + itemCode + "' ) AND PLI_PL_CODE='" + Setup_Values.Item("PL_CODE") + "'"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                If count < 1 Then
                    'MsgBox("Invalid item code!")
                    ItmCodeFound(0).Select()
                    Dim ItmAddval_name As String = "ItemAddval" & k
                    Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                    ItmAddvalFound(0).Text = "0"
                    Dim ItmDisamt_name As String = "ItemDisamt" & k
                    Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                    ItmDisamtFound(0).Text = "0"
                    success = False
                    Exit For
                End If
            Next

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return success
    End Function


    Private Function ItemCheck()
        Dim success As Boolean = True
        Try
            For k = 1 To txtItemCode.Count
                Dim ItmCode_name As String = "ItemCode" & k
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).ForeColor = Color.Red Then
                    ItmCodeFound(0).Select()
                    success = False
                    Return success
                    Exit Function
                End If
                Dim itemCode As String = ItmCodeFound(0).Text
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer

                stQuery = "SELECT ITEM_SHORT_NAME FROM OM_PRICE_LIST_ITEM, OM_ITEM WHERE NVL(ITEM_FRZ_FLAG_NUM,2) = 2 AND NVL(PLI_FRZ_FLAG_NUM,2)=2 AND ITEM_CODE = PLI_ITEM_CODE AND (PLI_ITEM_CODE='" + itemCode + "' OR ITEM_HARMONISED_CODE='" + itemCode + "' ) AND PLI_PL_CODE='" + Setup_Values.Item("PL_CODE") + "'"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                If count < 1 Then
                    'MsgBox("Invalid item code!")
                    ItmCodeFound(0).Select()
                    Dim ItmAddval_name As String = "ItemAddval" & k
                    Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                    ItmAddvalFound(0).Text = "0"
                    Dim ItmDisamt_name As String = "ItemDisamt" & k
                    Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                    ItmDisamtFound(0).Text = "0"
                    success = False
                    Exit For
                End If
            Next

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return success
    End Function

    Private Sub createHeadLog()
        Dim cnttxnval As Integer = 0
        If TXN_Code = "POSINV" Then

            If INVHNO = 0 Then
                Dim ds As DataSet
                Dim i As Integer = 0
                Dim stQuery As String
                Dim count As Integer
                Dim nextVal_HEADLOG As Integer
                Dim row As System.Data.DataRow
                Dim TXN_FM_NO As Integer
                Dim TXN_CURR_NO As Integer
                Dim TXN_TO_NO As Integer
                Dim success As Integer = 0

                While success < 1
                    stQuery = New String("")
                    stQuery = "SELECT TXND_CURR_NO, TXND_TO_NO, TXND_FM_NO FROM OM_TXN_DOC_RANGE WHERE TXND_COMP_CODE ='" & CompanyCode & "' AND TXND_TXN_CODE ='" & TXN_Code & "' AND TXND_LOCN_CODE ='" & Location_Code & "'"
                    errLog.WriteToErrorLog("sucessQuery", stQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "sucessQuery  -  " & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        TXN_CURR_NO = Convert.ToInt32(row.Item(0).ToString)
                        TXN_FM_NO = Convert.ToInt32(row.Item(2).ToString)
                        TXN_TO_NO = Convert.ToInt32(row.Item(1).ToString)
                    End If

                    Try
                        stQuery = "UPDATE OM_TXN_DOC_RANGE SET TXND_CURR_NO =" & TXN_CURR_NO + 1 & " WHERE TXND_COMP_CODE = '" & CompanyCode & "' AND TXND_TXN_CODE = '" & TXN_Code & "' AND TXND_LOCN_CODE = '" & Location_Code & "'"
                        errLog.WriteToErrorLog("Update Query aft success", stQuery, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "Update Query aft success  -  " & stQuery)
                        db.SaveToTableODBC(stQuery)

                        stQuery = "SELECT INVH_SYS_ID, OT_POS_INVOICE_HEAD_LOG.ROWID FROM OT_POS_INVOICE_HEAD_LOG WHERE INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & Location_Code & "' AND INVH_TXN_CODE = '" & TXN_Code & "' AND INVH_NO =" & TXN_CURR_NO + 1
                        errLog.WriteToErrorLog("Invoice No Increment", stQuery, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "Invoice No Increment  -  " & stQuery)
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        If count = 0 Then
                            'INVHNO = TXN_CURR_NO + 1
                            'success = 1
                            stQuery = "SELECT INVH_SYS_ID, OT_INVOICE_HEAD.ROWID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" & CompanyCode & "' AND INVH_LOCN_CODE = '" & Location_Code & "' AND INVH_TXN_CODE = '" & TXN_Code & "' AND INVH_NO =" & TXN_CURR_NO + 1
                            errLog.WriteToErrorLog("INVH INC QUERY", stQuery, "")
                            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                            ''MAILLOGValues.Add(MAILLOGROWNUM, "INVH INC QUERY  -  " & stQuery)
                            ds = db.SelectFromTableODBC(stQuery)
                            count = ds.Tables("Table").Rows.Count
                            If count = 0 Then
                                INVHNO = TXN_CURR_NO + 1
                                success = 1
                            Else
                                cnttxnval = cnttxnval + 1
                                success = 0
                                If cnttxnval > 9 Then
                                    MsgBox("Document Sequence Number Altered! Transaction Process Cannot be done!")
                                End If
                            End If
                        Else
                            success = 0
                        End If

                    Catch ex As Exception
                        success = 0
                    End Try
                End While

                Dim ds1 As DataSet
                Dim i1 As Integer = 0
                Dim stQuery1 As String
                Dim count1 As Integer

                stQuery1 = "SELECT POS_INVOICE_HEAD_LOG.NEXTVAL FROM DUAL"
                errLog.WriteToErrorLog("Headlog query", stQuery1, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "Headlog QUERY  -  " & stQuery1)
                ds1 = db.SelectFromTableODBC(stQuery1)
                If ds1.Tables("Table").Rows.Count > 0 Then
                    nextVal_HEADLOG = Convert.ToInt32(ds1.Tables("Table").Rows.Item(0).Item(0).ToString)
                End If
                'MsgBox(nextVal_HEADLOG)
                'MsgBox(frmSalesOrder.SOHNO)
                Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
                Dim salesmanCode As String = strSalesArr(0)
                Dim sms_shift_Code As New String("")
                stQuery1 = New String("")
                ds1 = New DataSet
                stQuery1 = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & salesmanCode & "'"
                errLog.WriteToErrorLog("ShiftCode Query", stQuery1, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "ShiftCode QUERY  -  " & stQuery1)
                ds1 = db.SelectFromTableODBC(stQuery1)
                count1 = ds1.Tables("Table").Rows.Count
                If count1 > 0 Then
                    sms_shift_Code = ds1.Tables("Table").Rows.Item(0).Item(0).ToString
                End If
                Try
                    Dim strCustArr() As String = txtCustomerCode.Text.Split(" - ")
                    'txtCustomerCode.Text = strCustArr(0)
                    Dim custCode As String = strCustArr(0) 'txtCustomerCode.Text

                    custDetails(0) = strCustArr(0)

                    custDetails(1) = txtCustomerName.Text
                    ds = New DataSet
                    Dim addrQuery As String
                    i = 0
                    addrQuery = "select ADDR_CODE, nvl(ADDR_SHORT_NAME,''),nvl(ADDR_CITY_CODE,''),nvl(ADDR_COUNTRY_CODE,''),nvl(ADDR_EMAIL,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_MOBILE,''),nvl(ADDR_LINE_1,''),nvl(ADDR_LINE_2,''),nvl(ADDR_COUNTY_CODE,''),nvl(ADDR_STATE_CODE,''),nvl(ADDR_ZIP_POSTAL_CODE,''),nvl(ADDR_PROVINCE_CODE,''),nvl(ADDR_CONTACT,''),nvl(ADDR_TEL,''),nvl(ADDR_LINE_3,''),nvl(ADDR_LINE_4,''),nvl(ADDR_LINE_5,'') from OM_ADDRESS where ADDR_CODE in ( select   CA_BILL_TO_ADDR_CODE from om_customer_address where ca_cust_code = '" & custCode & "')"
                    errLog.WriteToErrorLog("Bill Addr Code of Customer", addrQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Bill Addr Code of Customer  -  " & addrQuery)
                    ds = db.SelectFromTableODBC(addrQuery)
                    count = ds.Tables("Table").Rows.Count
                    While count > 0 'loop through for each row, add it to the datagridview
                        row = ds.Tables("Table").Rows.Item(i)
                        custDetails(2) = row.Item(0).ToString
                        custDetails(4) = row.Item(8).ToString
                        custDetails(5) = row.Item(9).ToString
                        custDetails(6) = row.Item(2).ToString
                        custDetails(7) = row.Item(10).ToString
                        custDetails(8) = row.Item(11).ToString
                        custDetails(9) = row.Item(12).ToString
                        custDetails(10) = row.Item(13).ToString
                        custDetails(11) = row.Item(3).ToString
                        custDetails(12) = row.Item(14).ToString
                        custDetails(13) = row.Item(4).ToString
                        custDetails(14) = row.Item(5).ToString
                        custDetails(15) = row.Item(15).ToString
                        custDetails(16) = row.Item(7).ToString
                        custDetails(30) = row.Item(16).ToString
                        custDetails(31) = row.Item(17).ToString
                        custDetails(32) = row.Item(18).ToString

                        count = count - 1
                        i = i + 1
                    End While

                    ds = New DataSet
                    Dim shipaddrQuery As String

                    i = 0
                    shipaddrQuery = "select ADDR_CODE, nvl(ADDR_SHORT_NAME,''),nvl(ADDR_CITY_CODE,''),nvl(ADDR_COUNTRY_CODE,''),nvl(ADDR_EMAIL,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_MOBILE,''),nvl(ADDR_LINE_1,''),nvl(ADDR_LINE_2,''),nvl(ADDR_COUNTY_CODE,''),nvl(ADDR_STATE_CODE,''),nvl(ADDR_ZIP_POSTAL_CODE,''),nvl(ADDR_PROVINCE_CODE,''),nvl(ADDR_CONTACT,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_LINE_3,''),nvl(ADDR_LINE_4,''),nvl(ADDR_LINE_5,'') from OM_ADDRESS where ADDR_CODE in ( select   CA_SHIP_TO_ADDR_CODE from om_customer_address where ca_cust_code = '" & custCode & "')"
                    errLog.WriteToErrorLog("Ship Addr Code of Customer", shipaddrQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Ship Addr Code of Customer  -  " & shipaddrQuery)
                    ds = db.SelectFromTableODBC(shipaddrQuery)
                    count = ds.Tables("Table").Rows.Count
                    While count > 0 'loop through for each row, add it to the datagridview
                        row = ds.Tables("Table").Rows.Item(i)
                        custDetails(3) = row.Item(0).ToString
                        custDetails(17) = row.Item(8).ToString
                        custDetails(18) = row.Item(9).ToString
                        custDetails(19) = row.Item(2).ToString
                        custDetails(20) = row.Item(10).ToString
                        custDetails(21) = row.Item(11).ToString
                        custDetails(22) = row.Item(12).ToString
                        custDetails(23) = row.Item(13).ToString
                        custDetails(24) = row.Item(3).ToString
                        custDetails(25) = row.Item(14).ToString
                        custDetails(26) = row.Item(4).ToString
                        custDetails(27) = row.Item(5).ToString
                        custDetails(28) = row.Item(15).ToString
                        custDetails(29) = row.Item(7).ToString
                        custDetails(33) = row.Item(16).ToString
                        custDetails(34) = row.Item(17).ToString
                        custDetails(35) = row.Item(18).ToString

                        count = count - 1
                        i = i + 1
                    End While

                    stQuery1 = "INSERT INTO OT_POS_INVOICE_HEAD_LOG(INVH_SYS_ID,INVH_COMP_CODE,INVH_TXN_CODE,INVH_NO,INVH_DT,INVH_DOC_SRC_LOCN_CODE,INVH_REF_FROM,INVH_REF_FROM_NUM,INVH_LOCN_CODE,INVH_DEL_LOCN_CODE,INVH_DEL_DT,INVH_CUST_CODE,INVH_SHIP_TO_ADDR_CODE,INVH_BILL_TO_ADDR_CODE,INVH_CURR_CODE,INVH_EXGE_RATE,INVH_DISC_PERC,INVH_DISC_VAL,INVH_SM_CODE,INVH_TERM_CODE,INVH_DNTOFOLLOW_NUM,INVH_STOP_AT_INV,INVH_STATUS,INVH_PRINT_STATUS,INVH_APPR_STATUS,INVH_APPR_UID,INVH_COS_FIN_STATUS,INVH_CUST_NAME,INVH_BILL_ADDR_LINE_1,INVH_BILL_ADDR_LINE_2,INVH_BILL_ADDR_LINE_3,INVH_BILL_ADDR_LINE_4,INVH_BILL_ADDR_LINE_5,INVH_BILL_CITY_CODE,INVH_BILL_COUNTY_CODE,INVH_BILL_STATE_CODE,INVH_BILL_POSTAL_CODE,INVH_BILL_COUNTRY_CODE,INVH_BILL_CONTACT,INVH_BILL_EMAIL,INVH_BILL_FAX,INVH_BILL_TEL,INVH_BILL_MOBILE,INVH_FLEX_10,INVH_FLEX_15,INVH_FLEX_16,INVH_FLEX_17,INVH_FLEX_18,INVH_FLEX_19,INVH_FLEX_20,INVH_CR_UID,INVH_CR_DT,INVH_ACNT_YEAR,INVH_CAL_YEAR,INVH_CAL_PERIOD)VALUES("
                    stQuery1 = stQuery1 & nextVal_HEADLOG & ",'" & CompanyCode & "','" & TXN_Code & "'," & INVHNO & ",'" & dtpick.Value.ToString("dd-MMM-yy") & "','" & Location_Code & "','D',1,'" & Location_Code & "','" & Location_Code & "','" & dtpick.Value.ToString("dd-MMM-yy") & "','" & custDetails(0) & "','" & custDetails(3) & "','" & custDetails(2) & "','" & Currency_Code & "'," & Exchange_Rate & ",0,0,'" & salesmanCode & "','" & CPT_TermCode & "',1,'N',4,1,0,'" & LogonUser & "','','" & custDetails(1) & "','" & custDetails(4) & "','" & custDetails(5) & "','" & custDetails(30) & "','" & custDetails(31) & "','" & custDetails(32) & "','" & custDetails(6) & "','" & custDetails(7) & "','" & custDetails(8) & "','" & custDetails(9) & "','" & custDetails(11) & "','" & custDetails(12) & "','" & custDetails(13) & "','" & custDetails(14) & "','" & custDetails(15) & "','" & custDetails(16) & "','N','','','','','" & sms_shift_Code & "','" & POSCounterNumber & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY')," & PC_Account_Year & "," & PC_CAL_Year & "," & PC_CAL_Period & ")"
                    'stQuery1 = stQuery1 & nextVal_HEADLOG & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & frmSalesOrder.SOHNO & ",to_date(sysdate,'DD-MM-YY'),'" & Location_Code & "',to_date(sysdate,'DD-MM-YY'),'D',1,'" & Location_Code & "','" & custDetails(0) & "','" & custDetails(3) & "','" & custDetails(2) & "','AED',1,0,0," & salesmanCode & ",'CASH',1,4,1,0,'" & LogonUser & "','','Y','N','','N','','','','','" & sms_shift_Code & "','" & POSCounterNumber & "','" & custDetails(1) & "','" & custDetails(4) & "','" & custDetails(5) & "','" & custDetails(30) & "','" & custDetails(31) & "','" & custDetails(32) & "','" & custDetails(6) & "','" & custDetails(7) & "','" & custDetails(8) & "','" & custDetails(9) & "','" & custDetails(11) & "','" & custDetails(12) & "','" & custDetails(13) & "','" & custDetails(14) & "','" & custDetails(15) & "','" & custDetails(16) & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                    errLog.WriteToErrorLog("Query OT_POS_INVOICE_HEAD_LOG", stQuery1, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Query OT_POS_INVOICE_HEAD_LOG  -  " & stQuery1)
                    db.SaveToTableODBC(stQuery1)
                    INVHSYSID = nextVal_HEADLOG

                    pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                    Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                    Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                    For z = oldloc To newloc
                        pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                        Threading.Thread.Sleep(1)
                    Next
                    pnlBtnSliderHolder.Enabled = False
                Catch ex As Exception
                    qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
                End Try
            End If
        ElseIf TXN_Code = "SO" Then
            Try
                If SOHNO = 0 Then
                    Dim ds As DataSet
                    Dim i As Integer = 0
                    Dim stQuery As String
                    Dim count As Integer
                    Dim nextVal_HEADLOG As Integer
                    Dim row As System.Data.DataRow
                    Dim TXN_FM_NO As Integer
                    Dim TXN_CURR_NO As Integer
                    Dim TXN_TO_NO As Integer
                    Dim success As Integer = 0

                    While success < 1
                        stQuery = New String("")
                        stQuery = "SELECT TXND_CURR_NO, TXND_TO_NO, TXND_FM_NO FROM OM_TXN_DOC_RANGE WHERE TXND_COMP_CODE ='" & CompanyCode & "' AND TXND_TXN_CODE ='" & TXN_Code & "' AND TXND_LOCN_CODE ='" & Location_Code & "'"
                        errLog.WriteToErrorLog("Succ chk query", stQuery, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "Succ chk query  -  " & stQuery)
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        If count > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            TXN_CURR_NO = Convert.ToInt32(row.Item(0).ToString)
                            TXN_FM_NO = Convert.ToInt32(row.Item(2).ToString)
                            TXN_TO_NO = Convert.ToInt32(row.Item(1).ToString)
                        End If

                        Try
                            stQuery = "UPDATE OM_TXN_DOC_RANGE SET TXND_CURR_NO =" & TXN_CURR_NO + 1 & " WHERE TXND_COMP_CODE = '" & CompanyCode & "' AND TXND_TXN_CODE = '" & TXN_Code & "' AND TXND_LOCN_CODE = '" & Location_Code & "'"
                            errLog.WriteToErrorLog("Update succ query", stQuery, "")
                            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                            ''MAILLOGValues.Add(MAILLOGROWNUM, "Update succ query  -  " & stQuery)
                            db.SaveToTableODBC(stQuery)

                            stQuery = "SELECT SOH_SYS_ID, OT_POS_SO_HEAD_LOG.ROWID FROM OT_POS_SO_HEAD_LOG WHERE SOH_COMP_CODE = '" & CompanyCode & "' AND SOH_LOCN_CODE = '" & Location_Code & "' AND SOH_TXN_CODE = '" & TXN_Code & "' AND SOH_NO =" & TXN_CURR_NO + 1
                            errLog.WriteToErrorLog("SO Inc Query", stQuery, "")
                            ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                            ''MAILLOGValues.Add(MAILLOGROWNUM, "SO Inc Query  -  " & stQuery)
                            ds = db.SelectFromTableODBC(stQuery)
                            count = ds.Tables("Table").Rows.Count
                            If count = 0 Then
                                'SOHNO = TXN_CURR_NO + 1
                                'success = 1
                                stQuery = "SELECT SOH_SYS_ID, OT_SO_HEAD.ROWID FROM OT_SO_HEAD WHERE SOH_COMP_CODE = '" & CompanyCode & "' AND SOH_LOCN_CODE = '" & Location_Code & "' AND SOH_TXN_CODE = '" & TXN_Code & "' AND SOH_NO =" & TXN_CURR_NO + 1
                                errLog.WriteToErrorLog("SO INCREMENT QUERY", stQuery, "")
                                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                                ''MAILLOGValues.Add(MAILLOGROWNUM, "SO INCREMENT QUERY  -  " & stQuery)
                                ds = db.SelectFromTableODBC(stQuery)
                                count = ds.Tables("Table").Rows.Count
                                If count = 0 Then
                                    SOHNO = TXN_CURR_NO + 1
                                    success = 1
                                Else
                                    cnttxnval = cnttxnval + 1
                                    success = 0
                                    If cnttxnval > 9 Then
                                        MsgBox("Document Sequence Number Altered! Transaction Process Cannot be done!")
                                    End If
                                    'success = 0
                                End If
                            Else
                                success = 0
                            End If

                        Catch ex As Exception
                            success = 0
                        End Try
                    End While

                    Dim ds1 As DataSet
                    Dim i1 As Integer = 0
                    Dim stQuery1 As String
                    Dim count1 As Integer

                    stQuery1 = "SELECT POS_SO_HEAD_LOG.NEXTVAL FROM DUAL"
                    errLog.WriteToErrorLog("Headlog query SO", stQuery1, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Headlog query SO  -  " & stQuery1)
                    ds1 = db.SelectFromTableODBC(stQuery1)
                    If ds1.Tables("Table").Rows.Count > 0 Then
                        nextVal_HEADLOG = Convert.ToInt32(ds1.Tables("Table").Rows.Item(0).Item(0).ToString)
                    End If

                    Dim sms_shift_Code As New String("")
                    stQuery1 = New String("")
                    ds1 = New DataSet
                    stQuery1 = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & salesmanCode & "'"
                    errLog.WriteToErrorLog("Shift Code Query", stQuery1, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Shift Code Query  -  " & stQuery1)
                    ds1 = db.SelectFromTableODBC(stQuery1)
                    count1 = ds1.Tables("Table").Rows.Count
                    If count1 > 0 Then
                        sms_shift_Code = ds1.Tables("Table").Rows.Item(0).Item(0).ToString
                    End If
                    Dim strCustArr() As String = txtCustomerCode.Text.Split(" - ")
                    Dim custCode As String = strCustArr(0)

                    custDetails(0) = strCustArr(0)

                    custDetails(1) = txtCustomerName.Text
                    ds = New DataSet
                    Dim addrQuery As String
                    i = 0
                    addrQuery = "select ADDR_CODE, nvl(ADDR_SHORT_NAME,''),nvl(ADDR_CITY_CODE,''),nvl(ADDR_COUNTRY_CODE,''),nvl(ADDR_EMAIL,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_MOBILE,''),nvl(ADDR_LINE_1,''),nvl(ADDR_LINE_2,''),nvl(ADDR_COUNTY_CODE,''),nvl(ADDR_STATE_CODE,''),nvl(ADDR_ZIP_POSTAL_CODE,''),nvl(ADDR_PROVINCE_CODE,''),nvl(ADDR_CONTACT,''),nvl(ADDR_TEL,''),nvl(ADDR_LINE_3,''),nvl(ADDR_LINE_4,''),nvl(ADDR_LINE_5,'') from OM_ADDRESS where ADDR_CODE IN ( select   CA_BILL_TO_ADDR_CODE from om_customer_address where ca_cust_code = '" & custCode & "')"
                    errLog.WriteToErrorLog("Headlog query", addrQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Headlog query  -  " & addrQuery)
                    ds = db.SelectFromTableODBC(addrQuery)
                    count = ds.Tables("Table").Rows.Count
                    While count > 0 'loop through for each row, add it to the datagridview
                        row = ds.Tables("Table").Rows.Item(i)
                        custDetails(2) = row.Item(0).ToString
                        custDetails(4) = row.Item(8).ToString
                        custDetails(5) = row.Item(9).ToString
                        custDetails(6) = row.Item(2).ToString
                        custDetails(7) = row.Item(10).ToString
                        custDetails(8) = row.Item(11).ToString
                        custDetails(9) = row.Item(12).ToString
                        custDetails(10) = row.Item(13).ToString
                        custDetails(11) = row.Item(3).ToString
                        custDetails(12) = row.Item(14).ToString
                        custDetails(13) = row.Item(4).ToString
                        custDetails(14) = row.Item(5).ToString
                        custDetails(15) = row.Item(15).ToString
                        custDetails(16) = row.Item(7).ToString
                        custDetails(30) = row.Item(16).ToString
                        custDetails(31) = row.Item(17).ToString
                        custDetails(32) = row.Item(18).ToString

                        count = count - 1
                        i = i + 1
                    End While

                    ds = New DataSet
                    Dim shipaddrQuery As String

                    i = 0
                    shipaddrQuery = "select ADDR_CODE, nvl(ADDR_SHORT_NAME,''),nvl(ADDR_CITY_CODE,''),nvl(ADDR_COUNTRY_CODE,''),nvl(ADDR_EMAIL,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_MOBILE,''),nvl(ADDR_LINE_1,''),nvl(ADDR_LINE_2,''),nvl(ADDR_COUNTY_CODE,''),nvl(ADDR_STATE_CODE,''),nvl(ADDR_ZIP_POSTAL_CODE,''),nvl(ADDR_PROVINCE_CODE,''),nvl(ADDR_CONTACT,''),nvl(ADDR_FAX,''),nvl(ADDR_TEL,''),nvl(ADDR_LINE_3,''),nvl(ADDR_LINE_4,''),nvl(ADDR_LINE_5,'') from OM_ADDRESS where ADDR_CODE in ( select   CA_SHIP_TO_ADDR_CODE from om_customer_address where ca_cust_code = '" & custCode & "')"
                    errLog.WriteToErrorLog("shipaddr Query", shipaddrQuery, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr Query  -  " & shipaddrQuery)
                    ds = db.SelectFromTableODBC(shipaddrQuery)
                    count = ds.Tables("Table").Rows.Count
                    While count > 0 'loop through for each row, add it to the datagridview
                        row = ds.Tables("Table").Rows.Item(i)
                        custDetails(3) = row.Item(0).ToString
                        custDetails(17) = row.Item(8).ToString
                        custDetails(18) = row.Item(9).ToString
                        custDetails(19) = row.Item(2).ToString
                        custDetails(20) = row.Item(10).ToString
                        custDetails(21) = row.Item(11).ToString
                        custDetails(22) = row.Item(12).ToString
                        custDetails(23) = row.Item(13).ToString
                        custDetails(24) = row.Item(3).ToString
                        custDetails(25) = row.Item(14).ToString
                        custDetails(26) = row.Item(4).ToString
                        custDetails(27) = row.Item(5).ToString
                        custDetails(28) = row.Item(15).ToString
                        custDetails(29) = row.Item(7).ToString
                        custDetails(33) = row.Item(16).ToString
                        custDetails(34) = row.Item(17).ToString
                        custDetails(35) = row.Item(18).ToString

                        count = count - 1
                        i = i + 1
                    End While

                    Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")

                    stQuery1 = "INSERT INTO OT_POS_SO_HEAD_LOG (SOH_SYS_ID,SOH_COMP_CODE,SOH_LOCN_CODE,SOH_TXN_CODE,SOH_NO,SOH_DT,SOH_DOC_SRC_LOCN_CODE,SOH_AMD_DT,SOH_REF_FROM,SOH_REF_FROM_NUM,SOH_DEL_LOCN_CODE,SOH_CUST_CODE,SOH_SHIP_TO_ADDR_CODE,SOH_BILL_TO_ADDR_CODE,SOH_CURR_CODE,SOH_EXGE_RATE,SOH_DISC_PERC,SOH_DISC_VAL,SOH_SM_CODE,SOH_TERM_CODE,SOH_RESVATCL_NUM,SOH_STATUS,SOH_PRINT_STATUS,SOH_APPR_STATUS,SOH_APPR_UID,SOH_CLO_STATUS,SOH_PARTSHIP_YN,SOH_ALLOCATE_YN,SOH_FLEX_01,SOH_FLEX_10,SOH_FLEX_15,SOH_FLEX_16,SOH_FLEX_17,SOH_FLEX_18,SOH_FLEX_19,SOH_FLEX_20,SOH_CUST_NAME,SOH_BILL_ADDR_LINE_1,SOH_BILL_ADDR_LINE_2,SOH_BILL_ADDR_LINE_3,SOH_BILL_ADDR_LINE_4,SOH_BILL_ADDR_LINE_5,SOH_BILL_CITY_CODE,SOH_BILL_COUNTY_CODE,SOH_BILL_STATE_CODE,SOH_BILL_POSTAL_CODE,SOH_BILL_COUNTRY_CODE,SOH_BILL_CONTACT,SOH_BILL_EMAIL,SOH_BILL_FAX,SOH_BILL_TEL,SOH_BILL_MOBILE,SOH_CR_UID,SOH_CR_DT)VALUES("
                    stQuery1 = stQuery1 & nextVal_HEADLOG & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & dtpick.Value.ToString("dd-MMM-yy") & "','" & Location_Code & "','" & dtpick.Value.ToString("dd-MMM-yy") & "','D',1,'" & Location_Code & "','" & custDetails(0) & "','" & custDetails(3) & "','" & custDetails(2) & "','AED',1,0,0,'" & strSalesArr(0) & "','CASH',1,4,1,0,'" & LogonUser & "','','Y','N','','N','','','','','" & sms_shift_Code & "','" & POSCounterNumber & "','" & custDetails(1) & "','" & custDetails(4) & "','" & custDetails(5) & "','" & custDetails(30) & "','" & custDetails(31) & "','" & custDetails(32) & "','" & custDetails(6) & "','" & custDetails(7) & "','" & custDetails(8) & "','" & custDetails(9) & "','" & custDetails(11) & "','" & custDetails(12) & "','" & custDetails(13) & "','" & custDetails(14) & "','" & custDetails(15) & "','" & custDetails(16) & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                    errLog.WriteToErrorLog("SO HEAD LOG", stQuery1, "")
                    errLog.WriteToErrorLog("Insert POS SO HEAD QUERY", stQuery1, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Insert POS SO HEAD QUERY  -  " & stQuery1)
                    db.SaveToTableODBC(stQuery1)
                    SOHSYSID = nextVal_HEADLOG

                    picboxTransactionType.Location = New Point(0, picboxTransactionType.Location.Y)
                    pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                    Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                    Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                    For z = oldloc To newloc
                        pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                        Threading.Thread.Sleep(1)
                    Next
                    pnlBtnSliderHolder.Enabled = False
                    btnCancelInvoice.Text = "Cancel Order"
                    'MsgBox(SOHNO)
                    'MsgBox(SOHSYSID)
                End If
            Catch ex As Exception
                qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub createItemLog(ByVal txtItem As TextBox, ByVal txtItemName As TextBox, ByVal txtQty As TextBox, ByVal txtPrice As TextBox, ByVal txtDisc As ComboBox, ByVal txtDisamt As TextBox, ByVal txtNetamt As TextBox, ByVal txtAddval As TextBox, ByVal txtAddvalCode As TextBox)

        Try
            Dim itemPreExist As Boolean = False
            If TXN_Code = "POSINV" Then
                Dim stQueryI As String
                'Dim dsI As DataSet
                Dim ds As DataSet
                Dim nextVal_ITEMLOG As Integer = 0
                'stQueryI = "SELECT POS_INVOICE_ITEM_LOG.NEXTVAL FROM DUAL"
                'dsI = db.SelectFromTableODBC(stQueryI)
                'If dsI.Tables("Table").Rows.Count > 0 Then
                '    nextVal_ITEMLOG = Convert.ToInt32(dsI.Tables("Table").Rows.Item(0).Item(0).ToString)
                'End If

                Dim itemDetails(24) As String
                itemDetails(0) = nextVal_ITEMLOG
                Dim itemCode As String = ""
                Dim stQueryItemBar As String
                stQueryItemBar = "select distinct item_code from om_item where (item_code='" + txtItem.Text + "' OR item_harmonised_code='" + txtItem.Text + "')"
                errLog.WriteToErrorLog("select item code Query", stQueryItemBar, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "select item code Query  -  " & stQueryItemBar)
                ds = db.SelectFromTableODBC(stQueryItemBar)
                If ds.Tables("Table").Rows.Count > 0 Then
                    itemCode = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Else
                    Exit Sub
                End If


                Dim stQuery As String

                stQuery = "select distinct nvl(ITEM_HARMONISED_CODE,''),nvl(ITEM_NAME,''), nvl(ITEM_UOM_CODE,''),nvl(ITEM_CODE,''),nvl(ITEM_SNO_YN_NUM,0),nvl(ITEM_BATCH_YN_NUM,0) from OM_ITEM  Where (ITEM_CODE= '" & itemCode & "' OR ITEM_HARMONISED_CODE='" & itemCode & "')"
                errLog.WriteToErrorLog("select Query", stQuery, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "select Query   -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    itemDetails(1) = itemCode
                    itemDetails(2) = txtItemName.Text
                    itemDetails(3) = txtQty.Text
                    itemDetails(4) = txtPrice.Text
                    itemDetails(5) = (Convert.ToDouble(txtPrice.Text.ToString) * Convert.ToDouble(txtQty.Text.ToString)).ToString
                    itemDetails(6) = txtDisamt.Text
                    itemDetails(7) = txtAddval.Text
                    itemDetails(8) = txtNetamt.Text
                    itemDetails(9) = Setup_Values.Item("PL_CODE")
                    itemDetails(10) = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                    itemDetails(11) = "0"
                    itemDetails(12) = "NA"
                    itemDetails(13) = "NA"
                    itemDetails(14) = txtDisc.Text
                    itemDetails(15) = txtAddvalCode.Text
                    itemDetails(16) = txtAddval.Text
                    itemDetails(17) = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                    itemDetails(18) = ""
                    itemDetails(19) = ""
                    itemDetails(20) = ""
                    itemDetails(21) = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                    itemDetails(22) = ""
                    itemDetails(23) = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                    itemDetails(24) = ""
                End If

                stQuery = "select * from OT_POS_INVOICE_ITEM_LOG where PROD_INVI_INVH_SYS_ID=" + INVHSYSID.ToString + " and PRODCODE='" + itemCode + "'"
                errLog.WriteToErrorLog("POS INVOICE ITEM QUERY", stQuery, "")
                ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                ''MAILLOGValues.Add(MAILLOGROWNUM, "POS INVOICE ITEM QUERY  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    Dim qty As Integer = 0
                    Dim price As Double = 0
                    Dim disamt As Double = 0
                    Dim addval As Double = 0
                    For k = 1 To txtItemCode.Count
                        Dim ItmCodeCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + k.ToString, True)
                        stQueryItemBar = "select distinct item_code from om_item where (item_code='" + ItmCodeCtl(0).Text + "' OR item_harmonised_code='" + ItmCodeCtl(0).Text + "')"
                        errLog.WriteToErrorLog("OM Item Query", stQueryItemBar, "")
                        ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                        ''MAILLOGValues.Add(MAILLOGROWNUM, "OM Item Query  -  " & stQueryItemBar)
                        ds = db.SelectFromTableODBC(stQueryItemBar)
                        Dim itemCodeLoop As String = ""
                        If ds.Tables("Table").Rows.Count > 0 Then
                            itemCodeLoop = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                        End If
                        If String.Compare(itemCodeLoop, itemCode, True) = 0 Then
                            Dim ItmQtyCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)

                            Dim ItmPriceCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" + k.ToString, True)
                            Dim ItmDisamtCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" + k.ToString, True)
                            Dim ItmAddvalCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" + k.ToString, True)

                            qty = qty + Convert.ToInt64(ItmQtyCtl(0).Text)
                            price = Convert.ToDouble(ItmPriceCtl(0).Text)
                            disamt = disamt + Convert.ToDouble(ItmDisamtCtl(0).Text)
                            addval = addval + Convert.ToDouble(ItmAddvalCtl(0).Text)
                            itemPreExist = True
                        Else
                        End If

                    Next
                    'If qty = "" Then
                    '    qty = "0"
                    'End If
                    stQueryI = "update OT_POS_INVOICE_ITEM_LOG set PRODQTY=" & qty.ToString & ",PRODPRICE=" & ((qty * price) - disamt).ToString & ", PRODDISCPER=" & (Round((disamt / (qty * price)) * 100, 3)).ToString & ", PRODDISCAMT=" & disamt.ToString & ", PRODDISCCODE='" & itemDetails(14).ToString & "', PRODEXPCODE='" & itemDetails(15).ToString & "',  PRODEXPAMT=" & addval.ToString & " where PROD_INVI_INVH_SYS_ID =" & INVHSYSID.ToString & " and PRODCODE='" & itemCode & "'"
                    'stQueryI = "update OT_POS_INVOICE_ITEM_LOG set PRODQTY=" & Convert.ToInt32(itemDetails(3).ToString) & ",PRODPRICE=" & ((Convert.ToInt32(itemDetails(3).ToString) * Convert.ToDouble(itemDetails(4).ToString)) - Convert.ToDouble(itemDetails(6).ToString)).ToString & ", PRODDISCPER=" & (Round((Convert.ToDouble(itemDetails(6).ToString) / Convert.ToInt32(itemDetails(5).ToString)) * 100, 3)).ToString & ", PRODDISCAMT=" & Convert.ToDouble(itemDetails(6).ToString) & ", PRODDISCCODE='" & itemDetails(14).ToString & "', PRODEXPCODE='" & itemDetails(15).ToString & "',  PRODEXPAMT=" & Convert.ToDouble(itemDetails(16).ToString) & " where PROD_INVI_INVH_SYS_ID =" & INVHSYSID.ToString & " and PRODCODE='" & itemCode & "'"
                    errLog.WriteToErrorLog("Update Query OT_POS_INVOICE_ITEM_LOG", stQueryI, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Update Query OT_POS_INVOICE_ITEM_LOG  -  " & stQueryI)

                    db.SaveToTableODBC(stQueryI)
                Else

                    stQueryI = "INSERT INTO OT_POS_INVOICE_ITEM_LOG(PROD_INVI_SYS_ID,PROD_INVI_INVH_SYS_ID,PRODCODE,PRODDESC,PRODITEMUOM,PRODQTY,PRODPRICE,PRODBARCODE,PRODRATE,PRODMINRATE,PRODDISCPER,PRODDISCAMT,PRODIGCODE,PRODGRADECODE_1,PRODGRADECODE_2,PRODPLCODE,PRODPLRATE,PRODBATCHNO,PRODSRNO,PRODRI,PRODDISCCODE,PRODVATCODE,PRODVATAMT,PRODEXPCODE,PRODEXPAMT,PRODBONUSCODE,PRODREASONCODE,PRODFOCITEM,PRODWARRANTYDT,PRODWARRANTYNO,PRODSTKITEM,PRODLOCNCODE,PRODSTOCKRESERV,PRODAFTHEADERDISC,PRODTAXABLEAMT,PRODUSERID,PRODITEMSTATUS,PRODWARRANTYTYPE,PRODWARRANTYDAYS)VALUES("
                    stQueryI = stQueryI & "POS_INVOICE_ITEM_LOG.NEXTVAL" & "," & INVHSYSID.ToString & ",'" & itemDetails(1) & "','" & itemDetails(2) & "','" & itemDetails(10) & "'," & Convert.ToInt32(itemDetails(3).ToString).ToString & "," & ((Convert.ToInt32(itemDetails(3).ToString) * Convert.ToDouble(itemDetails(4).ToString)) - Convert.ToDouble(itemDetails(6).ToString)).ToString & ",'" & itemDetails(17).ToString & "'," & Convert.ToDouble(itemDetails(4).ToString).ToString & ",0," & Convert.ToDouble(itemDetails(11).ToString).ToString & "," & Convert.ToDouble(itemDetails(6).ToString).ToString & ",'','" & itemDetails(12).ToString & "','" & itemDetails(13).ToString & "','" & itemDetails(9).ToString & "'," & Convert.ToDouble(itemDetails(4).ToString).ToString & ",'" & itemDetails(23).ToString & "','" & itemDetails(21).ToString & "','+','" & itemDetails(14).ToString & "','" & itemDetails(24).ToString & "',"
                    stQueryI = stQueryI & "0,'" & itemDetails(15).ToString & "'," & Convert.ToDouble(itemDetails(16).ToString).ToString & ",'','','N',to_date(sysdate,'DD-MM-YY'),'" & itemDetails(18).ToString & "','Y','" & Location_Code & "','N'," & Convert.ToDouble(itemDetails(8).ToString).ToString & ",0,0,'I','" & itemDetails(20).ToString & "',0)"
                    errLog.WriteToErrorLog("Query OT_POS_INVOICE_ITEM_LOG", stQueryI, "")
                    ''MAILLOGROWNUM = MAILLOGROWNUM + 1
                    ''MAILLOGValues.Add(MAILLOGROWNUM, "Query OT_POS_INVOICE_ITEM_LOG  -   " & stQueryI)
                    db.SaveToTableODBC(stQueryI)
                    'Beep()
                    'txtDisc.
                End If

                'If itemPreExist = False Then
                If Not txtDisc.Items.Count > 0 Then
                    'If Not txtDisc.AutoCompleteMode = AutoCompleteMode.Append Or txtDisc.AutoCompleteMode = AutoCompleteMode.Suggest Or txtDisc.AutoCompleteMode = AutoCompleteMode.SuggestAppend Then

                    Dim stQueryDisc As String
                    stQueryDisc = "SELECT ITEM_ANLY_CODE_01,ITEM_ANLY_CODE_02,ITEM_ANLY_CODE_03,ITEM_ANLY_CODE_04 FROM OM_ITEM where ITEM_CODE='" + txtItem.Text + "' OR ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & txtItem.Text & "')"
                    Dim dsDisc As DataSet
                    errLog.WriteToErrorLog("ITEM CODE QUERY", stQueryDisc, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "ITEM CODE QUERY  -  " & stQueryDisc)
                    dsDisc = db.SelectFromTableODBC(stQueryDisc)

                    Dim countDisc As Integer
                    Dim row As System.Data.DataRow
                    Dim iDisc As Integer
                    Dim anlycode1 As String = ""
                    Dim anlycode2 As String = ""
                    Dim anlycode3 As String = ""
                    Dim anlycode4 As String = ""
                    countDisc = dsDisc.Tables("Table").Rows.Count
                    iDisc = 0
                    While countDisc > 0
                        row = dsDisc.Tables("Table").Rows.Item(iDisc)
                        anlycode1 = row.Item(0).ToString
                        anlycode2 = row.Item(1).ToString
                        anlycode3 = row.Item(2).ToString
                        anlycode4 = row.Item(3).ToString
                        countDisc = countDisc - 1
                        iDisc = iDisc + 1
                    End While

                    stQueryDisc = ""
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DR_DISC_PERC DISC_PERC,DR_DISC_AMT DISC_AMT,DR_EFF_TO_DT EFF_TO_DT FROM "
                    stQueryDisc = stQueryDisc + " OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_RATE "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_RATE.DR_DISC_CODE AND '" + txtItem.Text + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DR_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' "
                    stQueryDisc = stQueryDisc + " UNION "
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DTD_DISC_PERC DISC_PERC,DTD_DISC_AMT DISC_AMT,DTD_EFF_TO_DT EFF_TO_DT "
                    stQueryDisc = stQueryDisc + " FROM OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_THRESHOLD_DETL "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_THRESHOLD_DETL .DTD_DISC_CODE AND '" + txtItem.Text + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DTD_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' ORDER BY EFF_TO_DT "
                    'stQueryDisc = "select disc_code,disc_perc from om_discount"
                    errLog.WriteToErrorLog("CreateItemlog Query", stQueryDisc, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "CreateItemlog Query  -  " & stQueryDisc)
                    dsDisc = db.SelectFromTableODBC(stQueryDisc)
                    countDisc = dsDisc.Tables("Table").Rows.Count

                    iDisc = 0
                    Discount_Codes = New List(Of String)
                    txtDisc.Items.Clear()
                    'txtDisc.Items.Add("")
                    While countDisc > 0
                        row = dsDisc.Tables("Table").Rows.Item(iDisc)
                        Discount_Codes.Add(row.Item(0).ToString)
                        txtDisc.Items.Add(row.Item(0).ToString)
                        countDisc = countDisc - 1
                        iDisc = iDisc + 1
                    End While

                    MySource_DiscountCodes = New AutoCompleteStringCollection()
                    MySource_DiscountCodes.AddRange(Discount_Codes.ToArray)
                    'Dim positionArray As Integer = List_MySource_DiscountCodes.Count
                    'List_MySource_DiscountCodes.Add(MySource_DiscountCodes)
                    'txtDisc.AutoCompleteCustomSource = MySource_DiscountCodes
                    'txtDisc.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    'txtDisc.AutoCompleteSource = AutoCompleteSource.CustomSource

                End If
            ElseIf TXN_Code = "SO" Then

                Dim stQueryI As String
                'Dim dsI As DataSet
                Dim ds As DataSet
                Dim nextVal_ITEMLOG As Integer = 0
                'stQueryI = "SELECT POS_INVOICE_ITEM_LOG.NEXTVAL FROM DUAL"
                'dsI = db.SelectFromTableODBC(stQueryI)
                'If dsI.Tables("Table").Rows.Count > 0 Then
                '    nextVal_ITEMLOG = Convert.ToInt32(dsI.Tables("Table").Rows.Item(0).Item(0).ToString)
                'End If

                Dim itemDetails(24) As String
                itemDetails(0) = nextVal_ITEMLOG
                Dim itemCode As String = ""
                Dim stQueryItemBar As String
                stQueryItemBar = "select distinct item_code from om_item where (item_code='" + txtItem.Text + "' OR item_harmonised_code='" + txtItem.Text + "')"
                errLog.WriteToErrorLog("OMITEM  Query", stQueryItemBar, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "OMITEM  Query  - " & stQueryItemBar)

                ds = db.SelectFromTableODBC(stQueryItemBar)
                If ds.Tables("Table").Rows.Count > 0 Then
                    itemCode = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Else
                    Exit Sub
                End If


                Dim stQuery As String

                stQuery = "select distinct nvl(ITEM_HARMONISED_CODE,''),nvl(ITEM_NAME,''), nvl(ITEM_UOM_CODE,''),nvl(ITEM_CODE,''),nvl(ITEM_SNO_YN_NUM,0),nvl(ITEM_BATCH_YN_NUM,0) from OM_ITEM  Where (ITEM_CODE= '" & itemCode & "' OR ITEM_HARMONISED_CODE='" & itemCode & "')"
                errLog.WriteToErrorLog("shipaddr Query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr Query  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    itemDetails(1) = itemCode
                    itemDetails(2) = txtItemName.Text
                    itemDetails(3) = txtQty.Text
                    itemDetails(4) = txtPrice.Text
                    itemDetails(5) = (Convert.ToDouble(txtPrice.Text.ToString) * Convert.ToDouble(txtQty.Text.ToString)).ToString
                    itemDetails(6) = txtDisamt.Text
                    itemDetails(7) = txtAddval.Text
                    itemDetails(8) = txtNetamt.Text
                    itemDetails(9) = "OGENPL"
                    itemDetails(10) = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                    itemDetails(11) = "0"
                    itemDetails(12) = "NA"
                    itemDetails(13) = "NA"
                    itemDetails(14) = txtDisc.Text
                    itemDetails(15) = txtAddvalCode.Text
                    itemDetails(16) = txtAddval.Text
                    itemDetails(17) = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                    itemDetails(18) = ""
                    itemDetails(19) = ""
                    itemDetails(20) = ""
                    itemDetails(21) = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                    itemDetails(22) = ""
                    itemDetails(23) = ds.Tables("Table").Rows.Item(0).Item(5).ToString
                    itemDetails(24) = ""
                End If

                stQuery = "select * from OT_POS_SO_ITEM_LOG where PROD_SOI_SOH_SYS_ID=" + SOHSYSID.ToString + " and PRODCODE='" + itemCode + "'"
                errLog.WriteToErrorLog("POS SO ITEM QUERY", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "POS SO ITEM QUERY  -  " & stQuery)

                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    Dim qty As Integer = 0
                    Dim price As Double = 0
                    Dim disamt As Double = 0
                    Dim addval As Double = 0
                    For k = 1 To txtItemCode.Count
                        Dim ItmCodeCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + k.ToString, True)
                        stQueryItemBar = "select distinct item_code from om_item where (item_code='" + ItmCodeCtl(0).Text + "' OR item_harmonised_code='" + ItmCodeCtl(0).Text + "')"
                        errLog.WriteToErrorLog("shipaddr Query", stQueryItemBar, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr Query  -  " & stQueryItemBar)
                        ds = db.SelectFromTableODBC(stQueryItemBar)
                        Dim itemCodeLoop As String = ""
                        If ds.Tables("Table").Rows.Count > 0 Then
                            itemCodeLoop = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                        End If
                        If String.Compare(itemCodeLoop, itemCode, True) = 0 Then
                            Dim ItmQtyCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)
                            Dim ItmPriceCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" + k.ToString, True)
                            Dim ItmDisamtCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" + k.ToString, True)
                            Dim ItmAddvalCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" + k.ToString, True)
                            qty = qty + Convert.ToInt64(ItmQtyCtl(0).Text)
                            price = Convert.ToDouble(ItmPriceCtl(0).Text)
                            disamt = disamt + Convert.ToDouble(ItmDisamtCtl(0).Text)
                            addval = addval + Convert.ToDouble(ItmAddvalCtl(0).Text)
                            itemPreExist = True
                        Else
                        End If
                    Next
                    stQueryI = "update OT_POS_SO_ITEM_LOG set PRODQTY=" & qty.ToString & ",PRODPRICE=" & ((qty * price) - disamt).ToString & ", PRODDISCPER=" & (Round((disamt / (qty * price)) * 100, 3)).ToString & ", PRODDISCAMT=" & disamt.ToString & ", PRODDISCCODE='" & itemDetails(14).ToString & "', PRODEXPCODE='" & itemDetails(15).ToString & "',  PRODEXPAMT=" & addval.ToString & " where PROD_SOI_SOH_SYS_ID =" & SOHSYSID.ToString & " and PRODCODE='" & itemCode & "'"
                    'stQueryI = "update OT_POS_INVOICE_ITEM_LOG set PRODQTY=" & Convert.ToInt32(itemDetails(3).ToString) & ",PRODPRICE=" & ((Convert.ToInt32(itemDetails(3).ToString) * Convert.ToDouble(itemDetails(4).ToString)) - Convert.ToDouble(itemDetails(6).ToString)).ToString & ", PRODDISCPER=" & (Round((Convert.ToDouble(itemDetails(6).ToString) / Convert.ToInt32(itemDetails(5).ToString)) * 100, 3)).ToString & ", PRODDISCAMT=" & Convert.ToDouble(itemDetails(6).ToString) & ", PRODDISCCODE='" & itemDetails(14).ToString & "', PRODEXPCODE='" & itemDetails(15).ToString & "',  PRODEXPAMT=" & Convert.ToDouble(itemDetails(16).ToString) & " where PROD_INVI_INVH_SYS_ID =" & INVHSYSID.ToString & " and PRODCODE='" & itemCode & "'"
                    errLog.WriteToErrorLog("Update Query OT_POS_SO_ITEM_LOG", stQueryI, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "Update Query OT_POS_SO_ITEM_LOG  -  " & stQueryI)

                    db.SaveToTableODBC(stQueryI)
                Else
                    stQueryI = "INSERT INTO OT_POS_SO_ITEM_LOG(PROD_SOI_SYS_ID,PROD_SOI_SOH_SYS_ID,PRODCODE,PRODDESC,PRODITEMUOM,PRODQTY,PRODPRICE,PRODBARCODE,PRODRATE,PRODMINRATE,PRODDISCPER,PRODDISCAMT,PRODIGCODE,PRODGRADECODE_1,PRODGRADECODE_2,PRODPLCODE,PRODPLRATE,PRODBATCHNO,PRODSRNO,PRODRI,PRODDISCCODE,PRODVATCODE,PRODVATAMT,PRODEXPCODE,PRODEXPAMT,PRODBONUSCODE,PRODREASONCODE,PRODFOCITEM,PRODWARRANTYDT,PRODWARRANTYNO,PRODSTKITEM,PRODLOCNCODE,PRODSTOCKRESERV,PRODAFTHEADERDISC,PRODTAXABLEAMT,PRODUSERID,PRODITEMSTATUS,PRODWARRANTYTYPE,PRODWARRANTYDAYS)VALUES("
                    stQueryI = stQueryI & "POS_SO_ITEM_LOG.NEXTVAL" & "," & SOHSYSID.ToString & ",'" & itemDetails(1) & "','" & itemDetails(2) & "','" & itemDetails(10) & "'," & Convert.ToInt32(itemDetails(3).ToString).ToString & "," & ((Convert.ToInt32(itemDetails(3).ToString) * Convert.ToDouble(itemDetails(4).ToString)) - Convert.ToDouble(itemDetails(6).ToString)).ToString & ",'" & itemDetails(17).ToString & "'," & Convert.ToDouble(itemDetails(4).ToString).ToString & ",0," & Convert.ToDouble(itemDetails(11).ToString).ToString & "," & Convert.ToDouble(itemDetails(6).ToString).ToString & ",'','" & itemDetails(12).ToString & "','" & itemDetails(13).ToString & "','" & itemDetails(9).ToString & "'," & Convert.ToDouble(itemDetails(4).ToString).ToString & ",'" & itemDetails(23).ToString & "','" & itemDetails(21).ToString & "','+','" & itemDetails(14).ToString & "','" & itemDetails(24).ToString & "',"
                    stQueryI = stQueryI & "0,'" & itemDetails(15).ToString & "'," & Convert.ToDouble(itemDetails(16).ToString).ToString & ",'','','N',to_date(sysdate,'DD-MM-YY'),'" & itemDetails(18).ToString & "','Y','" & Location_Code & "','N'," & Convert.ToDouble(itemDetails(8).ToString).ToString & ",0,0,'I','" & itemDetails(20).ToString & "',0)"
                    errLog.WriteToErrorLog("Query OT_POS_SO_ITEM_LOG", stQueryI, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "Query OT_POS_SO_ITEM_LOG  - " & stQueryI)
                    db.SaveToTableODBC(stQueryI)
                End If
                If Not txtDisc.Items.Count > 0 Then
                    Dim stQueryDisc As String
                    stQueryDisc = "SELECT ITEM_ANLY_CODE_01,ITEM_ANLY_CODE_02,ITEM_ANLY_CODE_03,ITEM_ANLY_CODE_04 FROM OM_ITEM where ITEM_CODE='" + txtItem.Text + "' OR ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & txtItem.Text & "')"
                    Dim dsDisc As DataSet
                    errLog.WriteToErrorLog("shipaddr Query", stQueryDisc, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr Query  -  " & stQueryDisc)
                    dsDisc = db.SelectFromTableODBC(stQueryDisc)

                    Dim countDisc As Integer
                    Dim row As System.Data.DataRow
                    Dim iDisc As Integer
                    Dim anlycode1 As String = ""
                    Dim anlycode2 As String = ""
                    Dim anlycode3 As String = ""
                    Dim anlycode4 As String = ""
                    countDisc = dsDisc.Tables("Table").Rows.Count
                    iDisc = 0
                    While countDisc > 0
                        row = dsDisc.Tables("Table").Rows.Item(iDisc)
                        anlycode1 = row.Item(0).ToString
                        anlycode2 = row.Item(1).ToString
                        anlycode3 = row.Item(2).ToString
                        anlycode4 = row.Item(3).ToString
                        countDisc = countDisc - 1
                        iDisc = iDisc + 1
                    End While

                    stQueryDisc = ""
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DR_DISC_PERC DISC_PERC,DR_DISC_AMT DISC_AMT,DR_EFF_TO_DT EFF_TO_DT FROM "
                    stQueryDisc = stQueryDisc + " OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_RATE "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_RATE.DR_DISC_CODE AND '" + txtItem.Text + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DR_EFF_TO_DT >='" & dtpick.Value.ToString("dd-MMM-yy") & "' "
                    stQueryDisc = stQueryDisc + " UNION "
                    stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DTD_DISC_PERC DISC_PERC,DTD_DISC_AMT DISC_AMT,DTD_EFF_TO_DT EFF_TO_DT "
                    stQueryDisc = stQueryDisc + " FROM OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_THRESHOLD_DETL "
                    stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
                    stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_THRESHOLD_DETL .DTD_DISC_CODE AND '" + txtItem.Text + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
                    stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
                    stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DTD_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' ORDER BY EFF_TO_DT "
                    'stQueryDisc = "select disc_code,disc_perc from om_discount"
                    errLog.WriteToErrorLog("shipaddr Query", stQueryDisc, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr QUERY  -  " & stQueryDisc)

                    dsDisc = db.SelectFromTableODBC(stQueryDisc)
                    countDisc = dsDisc.Tables("Table").Rows.Count

                    iDisc = 0
                    Discount_Codes = New List(Of String)
                    txtDisc.Items.Clear()
                    'txtDisc.Items.Add("")
                    While countDisc > 0
                        row = dsDisc.Tables("Table").Rows.Item(iDisc)
                        Discount_Codes.Add(row.Item(0).ToString)
                        txtDisc.Items.Add(row.Item(0).ToString)
                        countDisc = countDisc - 1
                        iDisc = iDisc + 1
                    End While

                    MySource_DiscountCodes = New AutoCompleteStringCollection()
                    MySource_DiscountCodes.AddRange(Discount_Codes.ToArray)
                    'Dim positionArray As Integer = List_MySource_DiscountCodes.Count
                    'List_MySource_DiscountCodes.Add(MySource_DiscountCodes)
                    'txtDisc.AutoCompleteCustomSource = MySource_DiscountCodes
                    'txtDisc.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    'txtDisc.AutoCompleteSource = AutoCompleteSource.CustomSource
                    'Else
                    '    txtDisc.Items.Clear()
                    '    'txtDisc.Items.Add("")
                    '    For i = 0 To Discount_Codes.Count - 1
                    '        txtDisc.Items.Add(Discount_Codes(i).ToString)
                    '    Next
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemAddval(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                'CreateItemRow()
                'Calculate_TotalAmount()

                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemAddval"}, StringSplitOptions.None)
                Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
                Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
                Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
                Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                'ItemCheck(ItmCodeFound(0).Text)
                If ItemCheck() = False Then
                    Calculate_TotalAmount()
                    Exit Sub
                End If
                'If ItmDescFound(0).Text = "" Then
                '    ItmCodeFound(0).Select()
                '    Exit Sub
                'End If
                If Not ItmQtyFound(0).Text = "0" Or Not ItmQtyFound(0).Text = "" Then
                    Calculate_TotalAmount()
                    Dim ItmHead_name As String = "ItemCode" & (Convert.ToInt64(parts(1).ToString) + 1).ToString

                    If pnlItemDetails.Controls.Find(ItmHead_name, True).Length = 0 Then
                        'createHeadLog()
                        'createItemLog(ItmCodeFound(0))
                        CreateItemRow()
                        Dim ItmHeadFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmHead_name, True)
                        ItmHeadFound(0).Select()
                    Else
                        Dim ItmHeadFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmHead_name, True)
                        ItmHeadFound(0).Select()
                    End If
                Else
                    DirectCast(sender, TextBox).Text = "0"
                End If
                'Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
                'Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
                'FindItmQty_TextChanged(ItmQtyFound(0), e)
            ElseIf e.KeyCode = Keys.Delete Then
                DirectCast(sender, TextBox).Text = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemNetamt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then

                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemNetamt"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).Text = "" Then
                    ItmCodeFound(0).Select()
                Else
                    Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
                    Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                    ItmAddvalFound(0).Select()
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then

                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemPrice"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).Text = "" Then
                    ItmCodeFound(0).Select()
                Else
                    Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
                    Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
                    ItmDiscFound(0).Select()
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemDesc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then

                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemDesc"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).Text = "" Then
                    ItmCodeFound(0).Select()
                Else
                    Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
                    Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
                    ItmQtyFound(0).Select()
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemDisamt(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).Text = "" Then
                    ItmCodeFound(0).Select()
                Else
                    Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
                    Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                    ItmAddvalFound(0).Select()
                End If

            ElseIf e.KeyCode = Keys.Delete Then
                DirectCast(sender, TextBox).Text = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemDisc(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Dim text As String = DirectCast(sender, ComboBox).Name
                Dim parts As String() = text.Split(New String() {"ItemDisc"}, StringSplitOptions.None)
                Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                ItmDisamtFound(0).Select()
                'ElseIf e.KeyCode = Keys.Delete Then
                '    DirectCast(sender, TextBox).Text = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItemDisc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim text As String = DirectCast(sender, ComboBox).Name
            Dim parts As String() = text.Split(New String() {"ItemDisc"}, StringSplitOptions.None)
            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" & parts(1).ToString, True)
            ItmDisamtFound(0).Text = 0
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FindItemDisc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim text1 As String = DirectCast(sender, ComboBox).Name
        Dim parts1 As String() = text1.Split(New String() {"ItemDisc"}, StringSplitOptions.None)
        lastActiveItem = parts1(1).ToString
    End Sub

    Private Sub FindItemDisc_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim itemCode As String = ""
            Dim itemDiscCode As String = ""
            Dim text As String = DirectCast(sender, ComboBox).Name
            itemDiscCode = DirectCast(sender, ComboBox).Text
            If itemDiscCode = "" Then
                Exit Sub
            End If
            Dim parts As String() = text.Split(New String() {"ItemDisc"}, StringSplitOptions.None)
            Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & parts(1).ToString, True)
            itemCode = ItmCodeFound(0).Text

            Dim stQueryDisc As String
            stQueryDisc = "SELECT ITEM_ANLY_CODE_01,ITEM_ANLY_CODE_02,ITEM_ANLY_CODE_03,ITEM_ANLY_CODE_04 FROM OM_ITEM where ITEM_CODE='" + itemCode + "' OR ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & itemCode & "')"
            Dim dsDisc As DataSet
            errLog.WriteToErrorLog("ITEM ANALY CODE", stQueryDisc, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "ITEM ANALY CODE  -  " & stQueryDisc)

            dsDisc = db.SelectFromTableODBC(stQueryDisc)

            Dim countDisc As Integer
            Dim row As System.Data.DataRow
            Dim iDisc As Integer
            Dim anlycode1 As String = ""
            Dim anlycode2 As String = ""
            Dim anlycode3 As String = ""
            Dim anlycode4 As String = ""
            countDisc = dsDisc.Tables("Table").Rows.Count
            iDisc = 0
            While countDisc > 0
                row = dsDisc.Tables("Table").Rows.Item(iDisc)
                anlycode1 = row.Item(0).ToString
                anlycode2 = row.Item(1).ToString
                anlycode3 = row.Item(2).ToString
                anlycode4 = row.Item(3).ToString
                countDisc = countDisc - 1
                iDisc = iDisc + 1
            End While

            stQueryDisc = ""
            stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DR_DISC_PERC DISC_PERC,DR_DISC_AMT DISC_AMT,DR_EFF_TO_DT EFF_TO_DT FROM "
            stQueryDisc = stQueryDisc + " OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_RATE "
            stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
            stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_RATE.DR_DISC_CODE AND '" + itemCode + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
            stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
            stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
            stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DR_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' AND FRD_DISC_CODE='" + itemDiscCode + "' "
            stQueryDisc = stQueryDisc + " UNION "
            stQueryDisc = stQueryDisc + " SELECT FRD_DISC_CODE DISC_CODE,DISC_DESC DISC_DESC,DTD_DISC_PERC DISC_PERC,DTD_DISC_AMT DISC_AMT,DTD_EFF_TO_DT EFF_TO_DT "
            stQueryDisc = stQueryDisc + " FROM OM_FUNCTION_RATE_DEFN, OM_DISCOUNT, OM_DISCOUNT_THRESHOLD_DETL "
            stQueryDisc = stQueryDisc + " WHERE OM_DISCOUNT.DISC_CODE = OM_FUNCTION_RATE_DEFN.FRD_DISC_CODE AND "
            stQueryDisc = stQueryDisc + " OM_DISCOUNT.DISC_CODE = OM_DISCOUNT_THRESHOLD_DETL .DTD_DISC_CODE AND '" + itemCode + "' BETWEEN FRD_FROM_LEV_01 AND FRD_UPTO_LEV_01 AND "
            stQueryDisc = stQueryDisc + " '" + Location_Code + "' BETWEEN FRD_FROM_LEV_02 AND FRD_UPTO_LEV_02 AND '" + anlycode1 + "' BETWEEN FRD_FROM_LEV_07 AND FRD_UPTO_LEV_07 AND "
            stQueryDisc = stQueryDisc + " '" + anlycode2 + "' BETWEEN FRD_FROM_LEV_08 AND FRD_UPTO_LEV_08 AND '" + anlycode3 + "' BETWEEN FRD_FROM_LEV_13 AND FRD_UPTO_LEV_13 AND "
            stQueryDisc = stQueryDisc + " '" + anlycode4 + "' BETWEEN FRD_FROM_LEV_14 AND FRD_UPTO_LEV_14 AND DTD_EFF_TO_DT >= '" & dtpick.Value.ToString("dd-MMM-yy") & "' AND FRD_DISC_CODE='" + itemDiscCode + "' ORDER BY EFF_TO_DT "
            'stQueryDisc = "select disc_code,disc_perc from om_discount"
            ''2664 COntinue
            errLog.WriteToErrorLog("shipaddr Query", stQueryDisc, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "shipaddr Query  -  " & stQueryDisc)
            dsDisc = db.SelectFromTableODBC(stQueryDisc)
            countDisc = dsDisc.Tables("Table").Rows.Count
            If countDisc < 1 Then
                MsgBox("Invalid discount code!")
                DirectCast(sender, TextBox).Text = ""
                DirectCast(sender, TextBox).Select()
                FindItmQty_TextChanged(ItmQtyFound(0), e)
                Exit Sub
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub FindItemQty(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemQty"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If ItmCodeFound(0).Text = "" Then
                    ItmCodeFound(0).Select()
                    DirectCast(sender, TextBox).Text = "0"
                Else
                    Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
                    Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
                    ItmDiscFound(0).Select()
                End If
            ElseIf e.KeyCode = Keys.Delete Then
                DirectCast(sender, TextBox).Text = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FindItem_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        Try
            If e.KeyData = Keys.Tab Then
                e.IsInputKey = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub FindItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim text1 As String = DirectCast(sender, TextBox).Name
            Dim parts1 As String() = text1.Split(New String() {"ItemCode"}, StringSplitOptions.None)
            lastActiveItem = parts1(1).ToString

            If txtCustomerName.Text = "" Then
                MsgBox("Please select Customer")
                txtCustomerCode.Select()
                Exit Sub
            ElseIf txtSalesmanName.Text = "" Then
                MsgBox("Please select Salesman")
                txtSalesmanCode.Select()
                Exit Sub
            End If
            Dim text As String = DirectCast(sender, TextBox).Name
            Dim parts As String() = text.Split(New String() {"ItemCode"}, StringSplitOptions.None)
            Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
            Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
            Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
            Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
            Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
            Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
            Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
            Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
            Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
            Dim ItmNetamt_name As String = "ItemNetamt" & parts(1).ToString
            Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmNetamt_name, True)
            DirectCast(sender, TextBox).ForeColor = Color.Black
            ItmDescFound(0).ForeColor = Color.Black
            ItmQtyFound(0).ForeColor = Color.Black
            ItmPriceFound(0).ForeColor = Color.Black
            ItmDiscFound(0).ForeColor = Color.Black
            ItmDisamtFound(0).ForeColor = Color.Black
            ItmNetamtFound(0).ForeColor = Color.Black
            ItmAddvalFound(0).ForeColor = Color.Black

            Dim itemCode As String = DirectCast(sender, TextBox).Text
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim i As Integer
            Dim row As System.Data.DataRow
            Dim tbx As System.Windows.Forms.TextBox = DirectCast(sender, TextBox)
            'If itemCode.Length > 4 Then
            '    loadItems(itemCode, tbx)
            'End If
            'If itemCode.Length > 4 Then
            '    stQuery = "select ITEM_CODE from OM_POS_ITEM where ITEM_CODE like '" & itemCode & "%' or ITEM_BAR_CODE like '" & itemCode & "'"
            '    ds = db.SelectFromTableODBC(stQuery)
            '    count = ds.Tables("Table").Rows.Count
            '    Item_Codes.Clear()
            '    i = 0
            '    While count > 0
            '        Item_Codes.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
            '        count = count - 1
            '        i = i + 1
            '    End While

            '    MySource_ItemCodes.AddRange(Item_Codes.ToArray)
            '    tbx.AutoCompleteCustomSource = MySource_ItemCodes
            '    tbx.AutoCompleteMode = AutoCompleteMode.Suggest
            '    tbx.AutoCompleteSource = AutoCompleteSource.CustomSource
            'Else
            '    Item_Codes.Clear()
            '    MySource_ItemCodes.Clear()
            '    tbx.AutoCompleteSource = AutoCompleteSource.None
            'End If


            'stQuery = "SELECT OM_ITEM.ITEM_NAME as Item_Name FROM OM_ITEM, OM_POS_ITEM A WHERE NVL (OM_ITEM.ITEM_FRZ_FLAG_NUM,2) = 2 AND OM_ITEM.ITEM_CODE = A.ITEM_CODE AND A.ITEM_PLI_PL_CODE='OGENPL' AND ITEM_COMP_CODE='" + CompanyCode + "' AND (OM_ITEM.ITEM_CODE='" + itemCode + "' OR OM_ITEM.ITEM_HARMONISED_CODE='" + itemCode + "')"
            'stQuery = "SELECT distinct ITEM_SHORT_NAME FROM OM_PRICE_LIST_ITEM, OM_ITEM WHERE NVL(ITEM_FRZ_FLAG_NUM,2) = 2 AND NVL(PLI_FRZ_FLAG_NUM,2)=2 AND ITEM_CODE = PLI_ITEM_CODE AND (PLI_ITEM_CODE='" + itemCode + "' OR ITEM_HARMONISED_CODE='" + itemCode + "' ) AND PLI_PL_CODE='" + Setup_Values.Item("PL_CODE") + "'"
            stQuery = "SELECT ITEM_NAME,ITEM_PRICE_TYPE_1 FROM OM_POS_ITEM WHERE ITEM_COMP_CODE= '" & CompanyCode & "' AND ITEM_PLI_PL_CODE= '" & Setup_Values.Item("PL_CODE") & "' AND (ITEM_CODE = '" & itemCode & "' OR ITEM_BAR_CODE = '" & itemCode & "')"
            errLog.WriteToErrorLog("OM_ITEM QUERY", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "OM_ITEM QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0
            If count < 1 Then
                ItmDescFound(0).Text = ""
                ItmAddvalFound(0).Text = "0"
                ItmDisamtFound(0).Text = "0"
                ItmQtyFound(0).Text = "0"
                ItmPriceFound(0).Text = "0"
                DirectCast(sender, TextBox).Select()
                Exit Sub
            End If

            While count > 0
                row = ds.Tables("Table").Rows.Item(0)
                ItmDescFound(0).Text = row.Item(0).ToString
                ItmPriceFound(0).Text = row.Item(1).ToString
                count = count - 1
                i = i + 1
            End While

            Home.ToolStripStatusLabel.Text = "POS"

            ''stQuery = "select distinct item_code from OM_POS_ITEM where item_bar_code='" + itemCode + "'"
            ''ds = db.SelectFromTableODBC(stQuery)
            ''count = ds.Tables("Table").Rows.Count
            ''If count > 1 Then
            ''    'MsgBox("More than one item has the same barcode!")
            ''    Exit Sub
            ''End If

            'stQuery = "select pli_rate  as pricelist from om_price_list_item where (pli_item_code='" & itemCode & "' OR pli_item_code = (select distinct item_code from OM_POS_ITEM where item_bar_code ='" & itemCode & "')) and pli_pl_code='" + Setup_Values.Item("PL_CODE") + "'"
            ''errLog.WriteToErrorLog("PRICELIST QUERY", stQuery, "")
            'ds = db.SelectFromTableODBC(stQuery)
            'count = ds.Tables("Table").Rows.Count
            'i = 0

            'If count > 0 Then
            '    row = ds.Tables("Table").Rows.Item(0)
            '    ItmPriceFound(0).Text = row.Item(0).ToString
            '    'count = count - 1
            '    'i = i + 1
            'End If

            'MsgBox("Item " + itemCode + " found")
            ItmAddvalFound(0).Text = "0"
            ItmDisamtFound(0).Text = "0"
            ItmQtyFound(0).Text = "1"

            'ItmQtyFound(0).Select()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub FindItem(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            Dim text1 As String = DirectCast(sender, TextBox).Name
            Dim parts1 As String() = text1.Split(New String() {"ItemCode"}, StringSplitOptions.None)
            lastActiveItem = parts1(1).ToString

            If e.KeyCode = Keys.Enter Then
                If DirectCast(sender, TextBox).Text = "" Then
                    MsgBox("Item code cannot be empty!")
                    DirectCast(sender, TextBox).Select()
                    Exit Sub
                End If
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemCode"}, StringSplitOptions.None)
                Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
                Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
                Dim itemCode As String = DirectCast(sender, TextBox).Text
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow
                'stQuery = "SELECT OM_ITEM.ITEM_NAME as Item_Name FROM OM_ITEM, OM_POS_ITEM A WHERE NVL (OM_ITEM.ITEM_FRZ_FLAG_NUM,2) = 2 AND OM_ITEM.ITEM_CODE = A.ITEM_CODE AND A.ITEM_PLI_PL_CODE='OGENPL' AND ITEM_COMP_CODE='" + CompanyCode + "' AND (OM_ITEM.ITEM_CODE='" + itemCode + "' OR OM_ITEM.ITEM_HARMONISED_CODE='" + itemCode + "')"
                'stQuery = "SELECT ITEM_SHORT_NAME FROM OM_PRICE_LIST_ITEM, OM_ITEM WHERE NVL(ITEM_FRZ_FLAG_NUM,2) = 2 AND NVL(PLI_FRZ_FLAG_NUM,2)=2 AND ITEM_CODE = PLI_ITEM_CODE AND (PLI_ITEM_CODE='" + itemCode + "' OR ITEM_HARMONISED_CODE='" + itemCode + "' ) AND PLI_PL_CODE='" + Setup_Values.Item("PL_CODE") + "'"
                stQuery = "SELECT ITEM_NAME,ITEM_PRICE_TYPE_1 FROM OM_POS_ITEM WHERE ITEM_COMP_CODE= '" & CompanyCode & "' AND ITEM_PLI_PL_CODE= '" & Setup_Values.Item("PL_CODE") & "' AND (ITEM_CODE = '" & itemCode & "' OR ITEM_BAR_CODE = '" & itemCode & "')"
                errLog.WriteToErrorLog("FindItem Query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "FindItem QUERY  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                If count < 1 Then
                    MsgBox("Item code not found!")
                    DirectCast(sender, TextBox).Select()
                    Exit Sub
                End If
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    ItmDescFound(0).Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While

                'stQuery = "select distinct item_code from OM_POS_ITEM where item_bar_code='" + itemCode + "'"
                'ds = db.SelectFromTableODBC(stQuery)
                'If ds.Tables("Table").Rows.Count > 1 Then
                '    MsgBox("More than one item has the same barcode!")
                '    Exit Sub
                'End If

                'stQuery = "select pli_rate  as pricelist from om_price_list_item where (pli_item_code='" & itemCode & "' OR pli_item_code = (select distinct item_code from om_item where item_harmonised_code ='" & itemCode & "')) and pli_pl_code='OGENPL'"
                'stQuery = "select pli_rate  as pricelist from om_price_list_item where (pli_item_code='" & itemCode & "' OR pli_item_code = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & itemCode & "')) and pli_pl_code='" + Setup_Values.Item("PL_CODE") + "'"
                'ds = db.SelectFromTableODBC(stQuery)

                count = ds.Tables("Table").Rows.Count
                i = 0
                Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
                Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    'ItmPriceFound(0).Text = row.Item(0).ToString
                    ItmPriceFound(0).Text = row.Item(1).ToString
                    count = count - 1
                    i = i + 1
                End While
                Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
                Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                ItmAddvalFound(0).Text = "0"
                Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                ItmDisamtFound(0).Text = "0"
                Dim ItmQty_name As String = "ItemQty" & parts(1).ToString
                Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmQty_name, True)
                ItmQtyFound(0).Text = "1"
                ItmQtyFound(0).Select()
            ElseIf e.KeyData = Keys.Tab Then
                If DirectCast(sender, TextBox).Text = "" Then
                    DirectCast(sender, TextBox).Select()
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Delete Then
                DirectCast(sender, TextBox).TextAlign = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = pnl1Patient.Width
            pnl1Patient.BringToFront()
            Do While i > 0
                pnl1Patient.Location = New Point(Me.Width - i, Me.Height - pnl1Patient.Height)
                pnl1Patient.Show()
                Threading.Thread.Sleep(0.5)
                i = i - 1
            Loop
            pnl1Patient.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = pnl1Patient.Width
            pnl1Patient.BringToFront()
            Do While i > 0
                pnl1Patient.Location = New Point(Me.Width - i, Me.Height - pnl1Patient.Height)

                Threading.Thread.Sleep(0.5)
                i = i - 1
            Loop
            pnl1Patient.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCustomeCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            txtCustomerCode.BackColor = Color.White

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCustomeCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                txtSalesmanCode.Select()
            ElseIf e.KeyCode = Keys.Delete Then  ' On delete key i have planned to remove the entry
                ' remove item from source
                CType(txtCustomerCode.AutoCompleteCustomSource, AutoCompleteStringCollection).Remove(txtCustomerCode.Text)
                ' Clear textbox
                'txtCustomerCode.Clear()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCustomeCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If CheckTransactionDate() Then
                If txtCustomerCode.Text = "" Then
                    txtCustomerCode.BackColor = Color.White
                    txtCustomerCode.Select()
                    Home.ToolStripStatusLabel.Text = "Customer Code cannot be empty!"
                Else
                    txtCustomerCode.BackColor = Color.GhostWhite
                End If
            Else
                dtpick.Select()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSalesmanCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtCustomerName.Text = "" Then
                txtCustomerCode.Select()
                txtCustomerCode.BackColor = Color.White
            Else
                txtSalesmanCode.BackColor = Color.White
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSalesmanCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Dim ItmHeadFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                ItmHeadFound(0).Select()
            ElseIf e.KeyCode = Keys.Delete Then  ' On delete key i have planned to remove the entry
                ' remove item from source
                CType(txtSalesmanCode.AutoCompleteCustomSource, AutoCompleteStringCollection).Remove(txtSalesmanCode.Text)
                ' Clear textbox
                ' txtSalesmanCode.Clear()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtSalesmanCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtSalesmanCode.Text = "" Then
                Home.ToolStripStatusLabel.Text = "Salesman code cannot be empty"
                txtSalesmanCode.Select()
                txtSalesmanCode.BackColor = Color.White
            ElseIf txtSalesmanName.Text = "" Then
                Home.ToolStripStatusLabel.Text = "Please select a valid Salesman"
                txtSalesmanCode.Select()
                txtSalesmanCode.BackColor = Color.White
            Else
                Home.ToolStripStatusLabel.Text = "POS"
                txtSalesmanCode.BackColor = Color.GhostWhite
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub txtCustomerCode_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.DropDown
        Dim senderComboBox As ComboBox = DirectCast(sender, ComboBox)
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim g As Graphics = senderComboBox.CreateGraphics()
        Dim font As Font = senderComboBox.Font
        Dim vertScrollBarWidth As Integer = If((senderComboBox.Items.Count > senderComboBox.MaxDropDownItems), SystemInformation.VerticalScrollBarWidth, 0)
        Dim newWidth As Integer
        For Each s As String In DirectCast(sender, ComboBox).Items
            newWidth = CInt(g.MeasureString(s, font).Width) + vertScrollBarWidth
            If width < newWidth Then
                width = newWidth
            End If
        Next
        senderComboBox.DropDownWidth = width + 10
    End Sub

    Private Sub txtCustomerCode_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.SelectedValueChanged
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String

            Dim strCustArr() As String = txtCustomerCode.Text.Split(" - ")
            txtCustomerCode.Text = strCustArr(0)

            stQuery = "SELECT CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) AND CUST_CODE='" + txtCustomerCode.Text + "'"
            errLog.WriteToErrorLog("custcodechange query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "custcodechange query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            If count = 0 Then
                txtCustomerName.Text = ""
                txtCustomerCode.Select()
                Home.ToolStripStatusLabel.Text = "Select a valid customer!"
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtCustomerName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    'Private Sub txtCustomerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged
    '    Try
    '        If Not CheckShiftTimings() Then
    '            MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
    '            Exit Sub
    '        End If
    '        Dim ds As DataSet
    '        Dim row As System.Data.DataRow
    '        Dim stQuery As String

    '        Dim strCustArr() As String = txtCustomerCode.Text.Split(" - ")
    '        txtCustomerCode.Text = strCustArr(0)

    '        stQuery = "SELECT CUST_NAME FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) AND CUST_CODE='" + txtCustomerCode.Text + "'"
    '        ds = db.SelectFromTableODBC(stQuery)
    '        Dim count As Integer = ds.Tables("Table").Rows.Count
    '        Dim i As Integer = 0
    '        If count = 0 Then
    '            txtCustomerName.Text = ""
    '            txtCustomerCode.Select()
    '            Home.ToolStripStatusLabel.Text = "Select a valid customer!"
    '        Else
    '            While count > 0
    '                row = ds.Tables("Table").Rows.Item(i)
    '                txtCustomerName.Text = row.Item(0).ToString
    '                count = count - 1
    '                i = i + 1
    '            End While
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    Private Sub txtSalesmanCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesmanCode.TextChanged
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String

            Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
            txtSalesmanCode.Text = strSalesArr(0)

            stQuery = "SELECT SM_NAME  FROM OM_SALESMAN WHERE SM_CODE='" & txtSalesmanCode.Text & "' AND SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
            errLog.WriteToErrorLog("salesmancodechange", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "salesmancodechange  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            If count = 0 Then
                txtSalesmanName.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtSalesmanName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCampaignCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCampaignCode.GotFocus
        Try
            txtCampaignCode.BackColor = Color.White
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtCampaignCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCampaignCode.LostFocus
        Try
            txtCampaignCode.BackColor = Color.GhostWhite
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        MsgBox(btnCancelRoyalty.Height.ToString + "," + btnCancelRoyalty.Width.ToString)
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

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

                For Each ctl As Control In pnlItemDetails.Controls
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


                For Each ctl As Control In pnlReportOuter.Controls
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

                For Each ctl As Control In PnlGrpBoxCont.Controls
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

                For Each ctl As Control In PnlReprintReport.Controls
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

                For Each ctl As Control In pnlTotalAmount.Controls
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
                For Each ctl As Control In pnl1Patient.Controls
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

                For Each ctl As Control In pnlPatientSearch.Controls
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

                For Each ctl As Control In pnlPatientDetails.Controls
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

                For Each ctl As Control In TabControl1.Controls
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

                For Each ctl As Control In RX_GLASSES.Controls
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

                For Each ctl As Control In TabPage2.Controls
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
                For Each ctl As Control In RX_CONTACTLENS.Controls
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

                For Each ctl As Control In TabPage3.Controls
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
                For Each ctl As Control In SLIT_K.Controls
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

                For Each ctl As Control In TabPage4.Controls
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
                For Each ctl As Control In TRIAL_D.Controls
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

                For Each ctl As Control In pnlRoyalty.Controls
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

                For Each ctl As Control In pnlRoyaltySearch.Controls
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

                lstRCDetail.Columns.Add("SNo", lbl_Royalty_Sno.Width - 3, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Type", lbl_Royalty_CardType.Width, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Name", lbl_Royalty_CardName.Width, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Number", lbl_Royalty_CardNumber.Width - 3, HorizontalAlignment.Center)

                For Each ctl As Control In pnlReferal.Controls
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
                For Each ctl As Control In Pnl_Refferal_hospdetails.Controls
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

                lstviewReferal.Columns.Add("SNo", lbl_Ref_Sno.Width - 3, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Hospital Code", lbl_Ref_HospCode.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Hospital Name", lbl_Ref_HospName.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Doctor ID", lbl_Ref_DoctorId.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Doctor Name", lbl_Ref_DoctorName.Width - 3, HorizontalAlignment.Center)

                For Each ctl As Control In pnlInsurance.Controls
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

                For Each ctl As Control In Pnl_Insurance_Details.Controls
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

                lstviewInsurance.Columns.Add("SNo", lbl_Insurance_Sno.Width - 3, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Code", lbl_Insurance_Code.Width, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Name", lbl_Insurance_Name.Width, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Number", lbl_Insurance_Number.Width - 3, HorizontalAlignment.Center)


                For Each ctl As Control In pnlLineAddValue.Controls
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


                For Each ctl As Control In Pnl_Lineaddvalue_details.Controls
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

                For Each ctl As Control In pnlGiftVouchers.Controls
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

                For Each ctl As Control In pnl_GIFT.Controls
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

                For Each ctl As Control In pnlPayment.Controls
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

                For Each ctl As Control In pnlCon_PAYMENT.Controls
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

                For Each ctl As Control In pnlTotalAmount.Controls
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

                For Each ctl As Control In pnlSalesOrders.Controls
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

                For Each ctl As Control In pnlSOSONO.Controls
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

                For Each ctl As Control In pnlSOlstview.Controls
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

                For Each ctl As Control In pnlSOSOLISTS.Controls
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


                For Each ctl As Control In pnlSalesReturn.Controls
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

                For Each ctl As Control In pnlSR_TRANSNO.Controls
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

                For Each ctl As Control In pnlCon_SalesReturn.Controls
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



                For Each ctl As Control In PnlSR_SEARCHLIST.Controls
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

                For Each ctl As Control In pnlHoldInvoice.Controls
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

                For Each ctl As Control In pnlHoldSearch.Controls
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

                listProduct.View = View.Details
                listProduct.OwnerDraw = True
                listProduct.GridLines = True
                listProduct.FullRowSelect = True
                listProduct.Columns.Add("Item Code", 125, HorizontalAlignment.Center)
                listProduct.Columns.Add("Stock", 50, HorizontalAlignment.Center)
                listProduct.Columns.Add("Grade Code", 75, HorizontalAlignment.Center)
                listProduct.Columns.Add("Item Desc", 140, HorizontalAlignment.Center)
                listProduct.Columns.Add("Item Bar Code", 90, HorizontalAlignment.Center)
                listProduct.Columns.Add("Price", 50, HorizontalAlignment.Center)
                listProduct.Columns.Add("Min Price", 65, HorizontalAlignment.Center)


                '

                ' if you are not maximizing your screen afterwards, then include this code
                Me.Top = (prvheight / 2) - (Me.Height / 2)
                Me.Left = (prvWidth / 2) - (Me.Width / 2)
            Else
                lstRCDetail.Columns.Add("SNo", lbl_Royalty_Sno.Width - 3, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Type", lbl_Royalty_CardType.Width, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Name", lbl_Royalty_CardName.Width, HorizontalAlignment.Center)
                lstRCDetail.Columns.Add("Card Number", lbl_Royalty_CardNumber.Width - 3, HorizontalAlignment.Center)
                lstRCDetail.View = View.Details
                lstRCDetail.GridLines = True
                lstRCDetail.FullRowSelect = True


                lstviewInsurance.Columns.Add("SNo", lbl_Insurance_Sno.Width - 3, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Code", lbl_Insurance_Code.Width, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Name", lbl_Insurance_Name.Width, HorizontalAlignment.Center)
                lstviewInsurance.Columns.Add("Insurance Number", lbl_Insurance_Number.Width - 3, HorizontalAlignment.Center)
                lstviewInsurance.View = View.Details
                lstviewInsurance.GridLines = True
                lstviewInsurance.FullRowSelect = True

                lstviewReferal.Columns.Add("SNo", lbl_Ref_Sno.Width - 3, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Hospital Code", lbl_Ref_HospCode.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Hospital Name", lbl_Ref_HospName.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Doctor ID", lbl_Ref_DoctorId.Width, HorizontalAlignment.Center)
                lstviewReferal.Columns.Add("Doctor Name", lbl_Ref_DoctorName.Width - 3, HorizontalAlignment.Center)
                lstviewReferal.View = View.Details
                lstviewReferal.GridLines = True
                lstviewReferal.FullRowSelect = True

                listProduct.View = View.Details
                listProduct.OwnerDraw = True
                listProduct.GridLines = True
                listProduct.FullRowSelect = True
                listProduct.Columns.Add("Item Code", 125, HorizontalAlignment.Center)
                listProduct.Columns.Add("Stock", 50, HorizontalAlignment.Center)
                listProduct.Columns.Add("Grade Code", 75, HorizontalAlignment.Center)
                listProduct.Columns.Add("Item Desc", 140, HorizontalAlignment.Center)
                listProduct.Columns.Add("Item Bar Code", 90, HorizontalAlignment.Center)
                listProduct.Columns.Add("Price", 50, HorizontalAlignment.Center)
                listProduct.Columns.Add("Min Price", 65, HorizontalAlignment.Center)
                

            End If


            GVListView.Columns.Add("SNo", lblG_SNO.Width - 3, HorizontalAlignment.Center)
            GVListView.Columns.Add("Voucher Type", lblG_VTYPE.Width, HorizontalAlignment.Center)
            GVListView.Columns.Add("Voucher Description ", lblG_DESC.Width, HorizontalAlignment.Center)
            GVListView.Columns.Add("Currency Type", lblG_CURTYPE.Width, HorizontalAlignment.Center)
            GVListView.Columns.Add("Voucher No", lblG_VNO.Width, HorizontalAlignment.Center)
            GVListView.Columns.Add("Amount", lblG_AMT.Width - 3, HorizontalAlignment.Right)
            GVListView.View = View.Details
            GVListView.GridLines = True
            GVListView.FullRowSelect = True

            PaymentlistView.Columns.Add("SNo", lblP_SNO.Width - 3, HorizontalAlignment.Center)
            PaymentlistView.Columns.Add("Payment Type", lblP_PAYTYPE.Width, HorizontalAlignment.Center)
            PaymentlistView.Columns.Add("Payment Description ", lblP_DESC.Width, HorizontalAlignment.Center)
            PaymentlistView.Columns.Add("Currency Type", lblP_CURTYPE.Width, HorizontalAlignment.Center)
            PaymentlistView.Columns.Add("Amount", lblP_AMT.Width - 3, HorizontalAlignment.Right)
            PaymentlistView.View = View.Details
            PaymentlistView.GridLines = True
            PaymentlistView.FullRowSelect = True

            lstviewSOSelected.Columns.Add("SNo", lblSO_SNO.Width - 3, HorizontalAlignment.Center)
            lstviewSOSelected.Columns.Add("Sales Order No", lblSO_SONO.Width, HorizontalAlignment.Center)
            lstviewSOSelected.Columns.Add("Sales Order Date", lblSO_DATE.Width, HorizontalAlignment.Center)
            lstviewSOSelected.Columns.Add("Total Amount", lblSO_TOT.Width, HorizontalAlignment.Center)
            lstviewSOSelected.Columns.Add("Advance Paid", lblSO_ADV.Width, HorizontalAlignment.Center)
            lstviewSOSelected.Columns.Add("Balance Amount", lblSO_BAL.Width - 3, HorizontalAlignment.Center)
            lstviewSOSelected.View = View.Details
            lstviewSOSelected.GridLines = True
            lstviewSOSelected.FullRowSelect = True

            lstviewSO.Columns.Add("SNo", lblSNOHold.Width - 3, HorizontalAlignment.Center)
            lstviewSO.Columns.Add("SNo", lblTransNoHold.Width, HorizontalAlignment.Center)
            lstviewSO.Columns.Add("SNo", lblTransDateHold.Width - 3, HorizontalAlignment.Center)
            lstviewSO.View = View.Details
            lstviewSO.GridLines = True
            lstviewSO.FullRowSelect = True

            lstviewHoldInvoices.Columns.Add("SNo", lblSO_SSNO.Width - 3, HorizontalAlignment.Center)
            lstviewHoldInvoices.Columns.Add("SNo", lblSO_SSONO.Width, HorizontalAlignment.Center)
            lstviewHoldInvoices.Columns.Add("SNo", lblSO_SDATE.Width - 3, HorizontalAlignment.Center)
            lstviewHoldInvoices.View = View.Details
            lstviewHoldInvoices.GridLines = True
            lstviewHoldInvoices.FullRowSelect = True

            lstviewSRDetails.Columns.Add("SNo", lblSR_SNO.Width - 3, HorizontalAlignment.Center)
            lstviewSRDetails.Columns.Add("Item Code", lblSR_ITEM.Width, HorizontalAlignment.Center)
            lstviewSRDetails.Columns.Add("Item Description", lblSR_IDES.Width, HorizontalAlignment.Center)
            lstviewSRDetails.Columns.Add("Qty", lblSR_QTY.Width, HorizontalAlignment.Center)
            lstviewSRDetails.Columns.Add("Price", lblSR_PRICE.Width - 3, HorizontalAlignment.Center)
            lstviewSRDetails.View = View.Details
            lstviewSRDetails.GridLines = True
            lstviewSRDetails.FullRowSelect = True


            LstView_SR_Search.Columns.Add("SNo", lbl_SR_SEARCH_SNo.Width - 3, HorizontalAlignment.Center)
            LstView_SR_Search.Columns.Add("Sales Invoice NO", lbl_SR_Search_InvoiceNo.Width, HorizontalAlignment.Center)
            LstView_SR_Search.Columns.Add("Sales Invoice Date", lbl_SR_SEARCH_InvDate.Width - 3, HorizontalAlignment.Center)
            LstView_SR_Search.View = View.Details
            LstView_SR_Search.GridLines = True
            LstView_SR_Search.FullRowSelect = True

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click

        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If txtItemCode.Count > 0 Then
                Dim ItmAddval_name As String = "ItemAddval" & txtItemCode.Count
                Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                'ByVal e As System.Windows.Forms.KeyEventArgs
                'FindItemAddval_KeyPress(ItmAddvalFound(0), New System.Windows.Forms.KeyPressEventHandler(Keys.Enter))
                FindItemAddval(ItmAddvalFound(0), New KeyEventArgs(Keys.Enter))
            Else
                CreateItemRow()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatient.Click
        dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            patientnovalue = txtPatientNo.Text

            If transtype = "Sales Invoice" Then

                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = False
                Next

                txtLocationCode.Text = Location_Code
                'txtLocationCode_TextChanged(sender, e)

                'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
                pnl1Patient.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnl1Patient.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnl1Patient.Width
                    pnl1Patient.Location = New Point(Me.Width - i, Me.Height - pnl1Patient.Height)
                    pnl1Patient.Show()
                    'Threading.Thread.Sleep(0.5)
                    i = i + 5
                Next i
                txtLocationCode.Select()

                For Each ctl As Control In pnlPatientDetails.Controls
                    Select Case ctl.GetType.ToString
                        Case "System.Windows.Forms.TextBox"
                            With DirectCast(ctl, TextBox)
                                .ReadOnly = True
                                .BackColor = Color.White
                                .Text = ""
                            End With

                        Case "System.Windows.Forms.CheckBox"
                            With DirectCast(ctl, CheckBox)
                                .Enabled = False
                                .BackColor = Color.White
                                .CheckState = CheckState.Unchecked
                            End With
                        Case "System.Windows.Forms.DateTimePicker"
                            With DirectCast(ctl, DateTimePicker)
                                .Enabled = False
                                .BackColor = Color.White
                            End With
                        Case "System.Windows.Forms.RadioButton"
                            With DirectCast(ctl, RadioButton)
                                .Enabled = False
                                .BackColor = Color.White
                                .Checked = CheckState.Unchecked
                            End With
                        Case "System.Windows.Forms.TabControl"
                            With DirectCast(ctl, TabControl)
                                .Enabled = False
                                .BackColor = Color.White
                            End With
                            'Case "System.Windows.Forms.GroupBox"
                            '    With DirectCast(ctl, GroupBox)
                            '        .Enabled = False
                            '        .BackColor = Color.White

                            '    End With
                    End Select
                Next

                For Each ctl In RX_GLASSES.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In RX_CONTACTLENS.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In SLIT_K.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In TRIAL_D.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next


                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                txtPatientNo.Text = patientnovalue
                txtPatientNo_TextChanged(sender, e)
            Else
                If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                    Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                    ItmCodeFound(0).Select()
                    Exit Sub
                End If
                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = False
                Next

                txtLocationCode.Text = Location_Code
                txtLocationCode_TextChanged(sender, e)

                'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
                pnl1Patient.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnl1Patient.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnl1Patient.Width
                    pnl1Patient.Location = New Point(Me.Width - i, Me.Height - pnl1Patient.Height)
                    pnl1Patient.Show()
                    'Threading.Thread.Sleep(0.5)
                    i = i + 5
                Next i
                txtLocationCode.Select()

                For Each ctl As Control In pnlPatientDetails.Controls
                    Select Case ctl.GetType.ToString
                        Case "System.Windows.Forms.TextBox"
                            With DirectCast(ctl, TextBox)
                                .ReadOnly = True
                                .BackColor = Color.White
                                .Text = ""
                            End With

                        Case "System.Windows.Forms.CheckBox"
                            With DirectCast(ctl, CheckBox)
                                .Enabled = False
                                .BackColor = Color.White
                                .CheckState = CheckState.Unchecked
                            End With
                        Case "System.Windows.Forms.DateTimePicker"
                            With DirectCast(ctl, DateTimePicker)
                                .Enabled = False
                                .BackColor = Color.White
                            End With
                        Case "System.Windows.Forms.RadioButton"
                            With DirectCast(ctl, RadioButton)
                                .Enabled = False
                                .BackColor = Color.White
                                .Checked = CheckState.Unchecked
                            End With
                        Case "System.Windows.Forms.TabControl"
                            With DirectCast(ctl, TabControl)
                                .Enabled = False
                                .BackColor = Color.White
                            End With
                            'Case "System.Windows.Forms.GroupBox"
                            '    With DirectCast(ctl, GroupBox)
                            '        .Enabled = False
                            '        .BackColor = Color.White

                            '    End With
                    End Select
                Next
                'dtpickPatDOB.Value = DateTime.Now


                For Each ctl In RX_GLASSES.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In RX_CONTACTLENS.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In SLIT_K.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In TRIAL_D.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Text = ""
                    End If
                Next
                'btnPatientAdd.BringToFront()
                'txtPatientNo.Enabled = True
                'btnPatientEdit.Enabled = True
                'btnPatientDelete.Enabled = True

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next
                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        ctl.Visible = False
                        Exit For
                    End If
                Next

                'pnl1Patient.Location = New Point(Me.Width - pnl1Patient.Width, Me.Height - pnl1Patient.Height)
                'pnl1Patient.Show()


            End If
            btnPatientAdd.BringToFront()
            txtPatientNo.Enabled = True
            btnPatientEdit.Enabled = True
            btnPatientDelete.Enabled = True
            btnPatientSearch.Enabled = True
            btnPatientAdd.Enabled = True
            btnPatientAdd.BringToFront()
            btnPatientNext.Visible = False
            btnPatientPrev.Visible = False

            txtPatientNo.Text = patientnovalue
            txtPatientNo_TextChanged(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub butPatientClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butPatientClose.Click
        Try
            If txtLocationCode.Text = "" Then
                MsgBox("Please select Location Code!")
                Exit Sub
            ElseIf txtPatientNo.Text = "" Then
                MsgBox("Please select Patient No.!")
                Exit Sub
            End If
            Dim i As Integer = 0
            Dim ds As DataSet
            Dim stQuery As String
            Dim count As Integer
            stQuery = "select pm_cust_no from om_patient_master where pm_cust_no='" + txtPatientNo.Text + "'"
            errLog.WriteToErrorLog("pmcustomer Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "pmcustomer Query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If Not count > 0 Then
                MsgBox("Patient does not exists!")
                Exit Sub
            End If
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next
            i = pnl1Patient.Width
            pnl1Patient.BringToFront()
            Do While i > 0
                pnl1Patient.Location = New Point(Me.Width - 2, Me.Height - pnl1Patient.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnl1Patient.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub txtLocationCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.GotFocus
        Try
            txtCustomerCode.BackColor = Color.White
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtLocationCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLocationCode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not txtLocationName.Text = "" Then
                    txtPatientNo.Select()
                Else
                    txtLocationCode.Select()
                End If
            ElseIf e.KeyCode = Keys.Delete Then  ' On delete key i have planned to remove the entry
                ' remove item from source
                CType(txtLocationCode.AutoCompleteCustomSource, AutoCompleteStringCollection).Remove(txtLocationCode.Text)
                ' Clear textbox
                txtLocationCode.Clear()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtLocationCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.LostFocus
        Try
            txtCampaignCode.BackColor = Color.GhostWhite
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.TextChanged
        Try
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String
            stQuery = "select distinct locn_name from om_patient_master a,om_location b where a.pm_locn_code=b.locn_code and a.pm_locn_code='" + txtLocationCode.Text + "'"
            errLog.WriteToErrorLog("locchange Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "locchange Query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            Dim i As Integer = 0
            If count = 0 Then
                txtLocationName.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtLocationName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
                If chkboxPatientNo.CheckState = CheckState.Checked Then
                    stQuery = "select pm_cust_no  from om_patient_master"
                    errLog.WriteToErrorLog("pmcust Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "pmcust Query  -  " & stQuery)

                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    i = 0
                    txtPatientNo.Text = "Please Wait..."
                    txtPatientNo.Enabled = False
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        Patient_Nos.Add(row.Item(0).ToString)
                        count = count - 1
                        i = i + 1
                    End While
                    txtPatientNo.Enabled = True
                    txtPatientNo.Text = ""
                    MySource_PatientNos.AddRange(Patient_Nos.ToArray)
                    txtPatientNo.AutoCompleteCustomSource = MySource_PatientNos
                    txtPatientNo.AutoCompleteMode = AutoCompleteMode.Append
                    txtPatientNo.AutoCompleteSource = AutoCompleteSource.CustomSource
                ElseIf chkboxPatientNo.CheckState = CheckState.Unchecked Then
                    txtPatientNo.AutoCompleteMode = AutoCompleteMode.None
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub chkboxPatientNo_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkboxPatientNo.CheckStateChanged

        Try
            If chkboxPatientNo.CheckState = CheckState.Checked Then
                Dim ds As DataSet
                Dim stQuery As String
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow
                stQuery = "select pm_cust_no  from om_patient_master"
                errLog.WriteToErrorLog("pmcustno Query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "pmcustno Query  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                txtPatientNo.Text = "Please Wait..."
                txtPatientNo.Enabled = False
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    Patient_Nos.Add(row.Item(0).ToString)
                    count = count - 1
                    i = i + 1
                End While
                txtPatientNo.Enabled = True
                txtPatientNo.Text = ""
                MySource_PatientNos.AddRange(Patient_Nos.ToArray)
                txtPatientNo.AutoCompleteCustomSource = MySource_PatientNos
                txtPatientNo.AutoCompleteMode = AutoCompleteMode.Append
                txtPatientNo.AutoCompleteSource = AutoCompleteSource.CustomSource
            ElseIf chkboxPatientNo.CheckState = CheckState.Unchecked Then
                txtPatientNo.AutoCompleteMode = AutoCompleteMode.None
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnCancelPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelPatient.Click
        Try
            'txtLocationCode.Text = ""
            txtPatientNo.AutoCompleteMode = AutoCompleteMode.None
            'txtPatientNo.Text = ""
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next
            i = pnl1Patient.Width
            pnl1Patient.BringToFront()
            Do While i > 0
                pnl1Patient.Location = New Point(Me.Width - 2, Me.Height - pnl1Patient.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnl1Patient.Visible = False
            pnlButtonHolder.Visible = True
            txtPatientNo.Text = ""
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub btnRoyaltyClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            i = pnlRoyalty.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlRoyalty.Location = New Point(Me.Width - 2, Me.Height - pnlRoyalty.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlRoyalty.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCancelRoyalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            i = pnlRoyalty.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlRoyalty.Location = New Point(Me.Width - 2, Me.Height - pnlRoyalty.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlRoyalty.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnRoyalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRoyalty.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If

            If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                ItmCodeFound(0).Select()
                Exit Sub
            Else

                'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
                Dim stQuery As String
                Dim count As Integer
                Dim i As Integer
                Dim ds As DataSet
                Dim row As System.Data.DataRow
                stQuery = "SELECT  VSSV_CODE,VSSV_NAME,VSSV_BL_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='LOYALTY_CARD' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S')"
                errLog.WriteToErrorLog("toyalty", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "toyalty  - " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    'Royalty_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                    Royalty_Codes.Add(row.Item(0).ToString)
                    txtRCCardType.Items.Add(row.Item(0).ToString & " - " & row.Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While

                Dim btn As Button = New Button
                With btn
                    .Name = "btnRCAdd"
                    .Location = New Point(txtRCCardNo.Location.X + txtRCCardNo.Width + 100, txtRCCardNo.Location.Y + 30)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Add"
                End With
                AddHandler btn.Click, AddressOf Me.btnRCAdd_Click
                Me.pnlRoyaltySearch.Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnRCOk"
                    .Location = New Point(txtRCCardNo.Location.X + txtRCCardNo.Width + 170, txtRCCardNo.Location.Y + 30)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Close"
                End With
                AddHandler btn.Click, AddressOf Me.btnRCOk_Click
                Me.pnlRoyaltySearch.Controls.Add(btn)

                'txtRCCardType1.AutoCompleteSource = AutoCompleteSource.None
                'MySource_Royalty.AddRange(Royalty_Codes.ToArray)
                'txtRCCardType1.AutoCompleteCustomSource = MySource_Royalty
                'txtRCCardType1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                'txtRCCardType1.AutoCompleteSource = AutoCompleteSource.CustomSource

                pnlRoyalty.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlRoyalty.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlRoyalty.Width
                    pnlRoyalty.Location = New Point(Me.Width - i, Me.Height - pnlRoyalty.Height)
                    pnlRoyalty.Show()
                    Threading.Thread.Sleep(0.5)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnGiftVouchers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiftVouchers.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If

            If transtype = "Sales Invoice" Then
                If FinalizeSalesInvoicetransaction() Then
                    For Each ctl As Control In pnlINVDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlItemDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlBottomHolder.Controls
                        ctl.Enabled = False
                    Next

                    Dim btn As Button = New Button
                    With btn
                        .Name = "btnAddGiftVoucher"
                        .Location = New Point(txtGiftVoucherNo.Location.X + txtGiftVoucherNo.Width + 100, txtGiftVoucherNo.Location.Y + 15)
                        .Size = New Size(60, 23)
                        .BackColor = Color.MediumTurquoise
                        .Text = "Add"
                    End With
                    AddHandler btn.Click, AddressOf Me.btnAddGiftVoucher_Click
                    Me.pnl_GIFT.Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnGVClose"
                        .Location = New Point(txtGiftVoucherNo.Location.X + txtGiftVoucherNo.Width + 170, txtGiftVoucherNo.Location.Y + 15)
                        .Size = New Size(60, 23)
                        .BackColor = Color.MediumTurquoise
                        .Text = "Close"
                    End With
                    AddHandler btn.Click, AddressOf Me.btnGVClose_Click
                    Me.pnl_GIFT.Controls.Add(btn)

                    pnlGiftVouchers.Height = Me.Height
                    pnlButtonHolder.Visible = False
                    pnlButtonHolder.SendToBack()
                    pnlGiftVouchers.BringToFront()
                    'pnl1Patient.Width = Me.Width
                    For i = 0 To pnlGiftVouchers.Width
                        pnlGiftVouchers.Location = New Point(Me.Width - i, Me.Height - pnlGiftVouchers.Height)
                        pnlGiftVouchers.Show()
                        Threading.Thread.Sleep(0.5)
                        i = i + 1
                    Next
                    lblpayAmounttotal.Text = Round(Convert.ToDouble(txtBalancePay.Text), 3).ToString
                End If
            ElseIf transtype = "Sales Return" Then
                If FinalizeSalesReturntransaction() Then
                    MsgBox("Gift Vouchers cannot be used in Sales Return!")
                    Exit Sub
                End If
            Else
                If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                    Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                    ItmCodeFound(0).Select()
                    Exit Sub
                End If
                If Not Finalizetransaction() = True Then
                    Exit Sub
                End If
                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = False
                Next

                Dim btn As Button = New Button
                With btn
                    .Name = "btnAddGiftVoucher"
                    .Location = New Point(txtGiftVoucherNo.Location.X + txtGiftVoucherNo.Width + 100, txtGiftVoucherNo.Location.Y + 15)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Add"
                End With
                AddHandler btn.Click, AddressOf Me.btnAddGiftVoucher_Click
                Me.pnl_GIFT.Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnGVClose"
                    .Location = New Point(txtGiftVoucherNo.Location.X + txtGiftVoucherNo.Width + 170, txtGiftVoucherNo.Location.Y + 15)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Close"
                End With
                AddHandler btn.Click, AddressOf Me.btnGVClose_Click

                Me.pnl_GIFT.Controls.Add(btn)
                pnlGiftVouchers.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlGiftVouchers.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlGiftVouchers.Width
                    pnlGiftVouchers.Location = New Point(Me.Width - i, Me.Height - pnlGiftVouchers.Height)
                    pnlGiftVouchers.Show()
                    Threading.Thread.Sleep(0.5)
                    i = i + 1
                Next
                lblpayAmounttotal.Text = Round(Convert.ToDouble(txtTotalAmount.Text), 3).ToString
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnGVClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnl_GIFT.Controls
                If ctl.Name = "btnAddGiftVoucher" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnAddGiftVoucher"
                    Me.pnl_GIFT.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnl_GIFT.Controls
                If ctl.Name = "btnGVClose" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnGVClose"
                    Me.pnl_GIFT.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            i = pnlGiftVouchers.Width
            pnlGiftVouchers.BringToFront()
            Do While i > 0
                pnlGiftVouchers.Location = New Point(Me.Width - 2, Me.Height - pnlGiftVouchers.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlGiftVouchers.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCloseGV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            i = pnlGiftVouchers.Width
            pnlGiftVouchers.BringToFront()
            Do While i > 0
                pnlGiftVouchers.Location = New Point(Me.Width - 2, Me.Height - pnlGiftVouchers.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlGiftVouchers.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayments.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If

            If transtype = "Sales Invoice" Then
                If FinalizeSalesInvoicetransaction() Then
                    For Each ctl As Control In pnlINVDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlItemDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlBottomHolder.Controls
                        ctl.Enabled = False
                    Next

                    pnlPayment.Height = Me.Height
                    pnlButtonHolder.Visible = False
                    pnlButtonHolder.SendToBack()
                    pnlPayment.BringToFront()
                    'pnl1Patient.Width = Me.Width
                    For i = 0 To pnlPayment.Width
                        pnlPayment.Location = New Point(Me.Width - i, Me.Height - pnlPayment.Height)
                        pnlPayment.Show()
                        Threading.Thread.Sleep(0.5)
                        i = i + 1
                    Next
                    lblpayAmounttotal.Text = Round(Convert.ToDouble(txtBalancePay.Text), 3).ToString
                End If
            ElseIf transtype = "Sales Return" Then
                cmbPayType.Text = "CN"
                If FinalizeSalesReturntransaction() Then
                    For Each ctl As Control In pnlINVDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlItemDetails.Controls
                        ctl.Enabled = False
                    Next
                    For Each ctl As Control In pnlBottomHolder.Controls
                        ctl.Enabled = False
                    Next
                    pnlPayment.Height = Me.Height
                    pnlButtonHolder.Visible = False
                    pnlButtonHolder.SendToBack()
                    pnlPayment.BringToFront()
                    'pnl1Patient.Width = Me.Width
                    For i = 0 To pnlPayment.Width
                        pnlPayment.Location = New Point(Me.Width - i, Me.Height - pnlPayment.Height)
                        pnlPayment.Show()
                        Threading.Thread.Sleep(0.5)
                        i = i + 1
                    Next
                    PaymentTypes = New List(Of String)
                    PaymentTypes.Clear()
                    'PaymentTypes.Add("CN")
                    cmbPayType.Items.Clear()
                    cmbPayType.Items.Add("CN")
                    'MySource_PaymentTypes.Clear()
                    'MySource_PaymentTypes.AddRange(PaymentTypes.ToArray)
                    'cmbPayType1.AutoCompleteCustomSource = MySource_PaymentTypes
                    'cmbPayType1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    'cmbPayType1.AutoCompleteSource = AutoCompleteSource.CustomSource
                    lblpayAmounttotal.Text = Round(Convert.ToDouble(txtTotalAmount.Text), 3).ToString
                End If
            Else
                If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                    Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                    ItmCodeFound(0).Select()
                    Exit Sub
                End If
                'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
                If Not Finalizetransaction() = True Then
                    Exit Sub
                End If
                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = False
                Next
                pnlPayment.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlPayment.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlPayment.Width
                    pnlPayment.Location = New Point(Me.Width - i, Me.Height - pnlPayment.Height)
                    pnlPayment.Show()
                    Threading.Thread.Sleep(0.5)
                    i = i + 1
                Next
                lblpayAmounttotal.Text = Round(Convert.ToDouble(txtTotalAmount.Text), 3).ToString
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim i As Integer
            If PaymentlistView.Items.Count < 1 Then
                MsgBox("You have not made any payments!")
                Exit Sub
            End If
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            If Not cardpay.GetReceivedAmount() > 0.0 Then
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = True
                Next
                btnAddItem.Enabled = True
            Else
                btnAddItem.Enabled = False
            End If
            If transtype = "Sales Return" Or transtype = "Sales Invoice" Then
                btnAddItem.Enabled = False
            End If
            i = pnlPayment.Width
            pnlPayment.BringToFront()
            Do While i > 0
                pnlPayment.Location = New Point(Me.Width - 2, Me.Height - pnlPayment.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlPayment.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpaymentCancel.Click
        Try
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            If Not cardpay.GetReceivedAmount() > 0.0 Then
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = True
                Next
                btnAddItem.Enabled = True
            Else
                btnAddItem.Enabled = False
            End If
            If transtype = "Sales Return" Or transtype = "Sales Invoice" Then
                btnAddItem.Enabled = False
            End If
            i = pnlPayment.Width
            pnlPayment.BringToFront()
            Do While i > 0
                pnlPayment.Location = New Point(Me.Width - 2, Me.Height - pnlPayment.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlPayment.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub Amountpayable_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Amountpayable.KeyPress

        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") < 0) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") >= 0 And tbx.SelectionLength > 0 And tbx.SelectionStart <= tbx.Text.IndexOf(".") And tbx.SelectionStart + tbx.SelectionLength > tbx.Text.IndexOf("."))) Then
                e.Handled = True

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Amountpayable_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Amountpayable.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 0
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0

            ElseIf value >= 0 Then
                tbx.Text = Round(value, 3)
            Else
                tbx.Text = Abs(Round(value, 3))
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub Amountpayable_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Amountpayable.TextChanged
        Try
            txtCurrencyType_SelectedValueChanged(sender, e)
            If Amountpayable.Text = "" Then
                lblreceiveamount.Text = cardpay.GetReceivedAmount.ToString
                lblBalance.Text = cardpay.GetBalanceAmount.ToString
            Else
                Dim receivedAmountCalc As Double
                Dim balanceAmountCalc As Double

                receivedAmountCalc = cardpay.calBalance(Convert.ToDouble(amountinaed.Text.ToString), Convert.ToDouble(lblpayAmounttotal.Text.ToString))
                balanceAmountCalc = Round(Convert.ToDouble(lblpayAmounttotal.Text.ToString) - receivedAmountCalc, 3)
                If balanceAmountCalc < 0 Then
                    'lblamountpay.Text = lblamountpay.Text.Substring(0, lblamountpay.Text.Length - 1)
                    'txtChangeReturn.Text = Round(Math.Abs(balanceAmountCalc), 3).ToString
                    lblBalance.Text = 0
                    lblreceiveamount.Text = receivedAmountCalc.ToString
                Else
                    lblBalance.Text = Round(balanceAmountCalc, 3).ToString
                    lblreceiveamount.Text = receivedAmountCalc.ToString
                    'txtChangeReturn.Text = "0"
                End If

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub PaymentOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentOK.Click
        Try
            If txtPaymentDesc.Text = "" Then
                MsgBox("Please select a valid Payment Type")
                Exit Sub
            End If
            If Amountpayable.Text = "" Or Amountpayable.Text = "0" Then
                Amountpayable.Text = "0"
                MsgBox("Please enter amount")
                Exit Sub
            End If

            If txtCurrencyType.Text = "" Then
                MsgBox("Please enter Currency type")
                Exit Sub
            Else
                Dim stQuery As String
                stQuery = "select CURR_CODE from FM_CURRENCY where CURR_CODE='" & txtCurrencyType.Text & "'"
                Dim ds As DataSet
                errLog.WriteToErrorLog("paymentok Query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "paymentok Query  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("Please enter a valid Currency type")
                    Exit Sub
                End If
            End If

            Dim rowItem(8) As String
            Dim balanceAmt As Double = 0

            If Convert.ToDouble(Amountpayable.Text.ToString) > 0.0 Then
                'Dim amountRec As String = Amountpayable.Text
                Dim amountRec As String = amountinaed.Text
                If cardpay.GetReceivedAmount() + Convert.ToDouble(amountRec) > Convert.ToDouble(lblpayAmounttotal.Text.ToString) Then
                    MsgBox("Payment exceeded total amount!")
                    Exit Sub
                End If
                cardpay.addCardPayment(Convert.ToDouble(amountinaed.Text.ToString))
                cardpay.SetBalanceAmount(Convert.ToDouble(lblBalance.Text.ToString))
                Amountpayable.Text = 0
                If cardpay.GetBalanceAmount() <= 0 Then
                    Amountpayable.Enabled = False
                    cmbPayType.Enabled = False
                    txtCurrencyType.Enabled = False
                    'txtGiftVoucherAmt.Enabled = False
                    'txtGiftVoucherCurrType.Enabled = False
                    'txtGiftVoucherCode.Enabled = False
                End If
                Dim listcount As Integer = PaymentlistView.Items.Count
                Dim listcountt As Integer = listcount + 1
                rowItem(0) = listcountt.ToString
                PaymentlistView.Items.Add(listcountt)

                rowItem(1) = cmbPayType.Text.ToString
                PaymentlistView.Items(listcount).SubItems.Add(rowItem(1))
                rowItem(2) = txtCurrencyType.Text.ToString
                'If Amountpayable.Enabled Then
                rowItem(3) = amountRec.ToString
                rowItem(4) = ""
                rowItem(5) = ""
                rowItem(6) = txtPaymentDesc.Text
                PaymentlistView.Items(listcount).SubItems.Add(rowItem(6))
                PaymentlistView.Items(listcount).SubItems.Add(rowItem(2))
                PaymentlistView.Items(listcount).SubItems.Add(rowItem(3))
                rowItem(7) = txtppdtype.Text
                cardpay.addToPayment(rowItem)

                rowItem(8) = Round(Convert.ToDouble("0"), 3)
                'MsgBox(cardpay.countCart)
                amountinaed.Text = 0
                cmbPayType.Text = ""
                txtPaymentDesc.Text = ""
                Amountpayable.Text = "0"
            Else
                'MsgBox(Convert.ToDouble(Amountpayable.Text.ToString))
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub PaymentlistView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentlistView.Click
        Try
            Dim result As DialogResult = MsgBox("Do you want to remove this item from cart?", MessageBoxButtons.YesNo, "Alert")
            If result = Windows.Forms.DialogResult.Yes Then
                Dim PaymentRow = cardpay.GetRowFromPayment(PaymentlistView.SelectedItems.Item(0).Index)
                Dim recAmount As Double = Round(cardpay.subCardPayment(Convert.ToDouble(PaymentRow(3).ToString)), 3)
                cardpay.setReceivedAmount(recAmount)
                Dim balAmount As Double = Round(Convert.ToDouble(lblpayAmounttotal.Text.ToString), 3) - recAmount
                cardpay.SetBalanceAmount(balAmount)
                lblreceiveamount.Text = recAmount.ToString
                lblBalance.Text = balAmount.ToString

                If cardpay.GetBalanceAmount() <= 0 Then
                    'txtChangeReturn.Text = recAmount - Round(Convert.ToDouble(lblpayAmounttotal.Text.ToString), 3)
                    lblBalance.Text = 0
                    Amountpayable.Enabled = False
                    cmbPayType.Enabled = False
                    txtCurrencyType.Enabled = False
                    txtGiftVoucherAmt.Enabled = False
                    txtGiftVoucherCode.Enabled = False
                    txtGiftVoucherCurrType.Enabled = False
                Else
                    'txtChangeReturn.Text = 0
                    Amountpayable.Enabled = True
                    cmbPayType.Enabled = True
                    txtCurrencyType.Enabled = True
                    txtGiftVoucherAmt.Enabled = True
                    txtGiftVoucherCode.Enabled = True
                    txtGiftVoucherCurrType.Enabled = True
                End If


                If cardpay.removeFromPayment(PaymentlistView.SelectedItems.Item(0).Index) Then
                    'Dim succString = "Payment Type - " & PaymentlistView.SelectedItems.Item(0).SubItems(1).ToString & " of Amount - " & PaymentlistView.SelectedItems.Item(0).SubItems(3).ToString & " is Removed"
                    'MsgBox(succString)
                    'MsgBox(cardpay.GetReceivedAmount - Convert.ToDouble(PaymentlistView.SelectedItems.Item(0).SubItems(3).ToString))

                    PaymentlistView.Items.Clear()
                    GVListView.Items.Clear()
                    Dim listPayment As New List(Of String())
                    listPayment = cardpay.GetPaymentItems
                    Dim count As Integer
                    For count = 0 To listPayment.Count - 1
                        Dim RowItemList(8) As String
                        RowItemList = listPayment.Item(count)
                        PaymentlistView.Items.Add(count + 1)
                        PaymentlistView.Items(count).SubItems.Add(RowItemList(1))
                        PaymentlistView.Items(count).SubItems.Add(RowItemList(6))
                        PaymentlistView.Items(count).SubItems.Add(RowItemList(2))
                        PaymentlistView.Items(count).SubItems.Add(RowItemList(3))
                        'MsgBox(RowItemList(1) + " - " + RowItemList(7))
                        If RowItemList(7) = "GV" Then
                            Dim cnt As Integer = GVListView.Items.Count
                            GVListView.Items.Add(cnt + 1)
                            GVListView.Items(cnt).SubItems.Add(RowItemList(1))
                            GVListView.Items(cnt).SubItems.Add(RowItemList(6))
                            GVListView.Items(cnt).SubItems.Add(RowItemList(2))
                            GVListView.Items(cnt).SubItems.Add(RowItemList(4))
                            GVListView.Items(cnt).SubItems.Add(RowItemList(3))
                        End If
                        'lblreceiveamount.Text = cardpay.GetReceivedAmount - Convert.ToDouble(PaymentlistView.SelectedItems.Item(0).SubItems(1).ToString)
                        'MsgBox(cardpay.GetReceivedAmount)
                        'MsgBox(cardpay.GetReceivedAmount)

                    Next
                    MsgBox("Payment Removed")
                End If
            Else
                Amountpayable.Focus()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
        'MsgBox(PaymentlistView.SelectedItems.Item(0).Index)
    End Sub


    Private Sub btnAddGiftVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtGiftVoucherDesc.Text = "" Then
                MsgBox("Please select a valid Gift Voucher")
                Exit Sub
            End If
            If txtGiftVoucherAmt.Text = "" Or txtGiftVoucherAmt.Text = "0" Then
                txtGiftVoucherAmt.Text = "0"
                MsgBox("Please enter Gift Voucher amount")
                Exit Sub
            End If
            If txtGiftVoucherCurrType.Text = "" Then
                MsgBox("Please enter Gift Voucher Currency type")
                Exit Sub
            Else
                Dim stQuery As String
                stQuery = "select CURR_CODE from FM_CURRENCY where CURR_CODE='" & txtGiftVoucherCurrType.Text & "'"
                Dim ds As DataSet
                errLog.WriteToErrorLog("giftVoucher query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "giftVoucher query  -  " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("Please enter a valid Gift Voucher Currency type")
                    Exit Sub
                End If
            End If
            Dim giftcount As Integer = GVListView.Items.Count
            Dim giftcountt As Integer = giftcount + 1
            GVListView.Items.Add(giftcountt.ToString)
            GVListView.Items(giftcount).SubItems.Add(txtGiftVoucherCode.Text.Split(" - ")(0))
            GVListView.Items(giftcount).SubItems.Add(txtGiftVoucherDesc.Text)
            GVListView.Items(giftcount).SubItems.Add(txtGiftVoucherCurrType.Text)
            GVListView.Items(giftcount).SubItems.Add(txtGiftVoucherNo.Text)
            GVListView.Items(giftcount).SubItems.Add(txtGiftVoucherAmt.Text)

            txtGiftVoucherCode.Text = ""
            txtGiftVoucherDesc.Text = ""
            txtGiftVoucherNo.Text = ""
            txtGiftVoucherAmt.Text = ""

            'Dim rowItem(8) As String
            'Dim balanceAmt As Double = 0

            'If Convert.ToDouble(txtGiftVoucherAmt.Text.ToString) > 0.0 Then

            '    Dim amountRec As String = txtGiftVoucherAmt.Text
            '    If cardpay.GetReceivedAmount() + Convert.ToDouble(amountRec) > Convert.ToDouble(lblpayAmounttotal.Text.ToString) Then
            '        MsgBox("Gift Voucher amount exceeded total amount!")
            '        Exit Sub
            '    End If
            '    Amountpayable.Text = amountRec
            '    Amountpayable_TextChanged(sender, e)
            '    cardpay.addCardPayment(Convert.ToDouble(txtGiftVoucherAmt.Text.ToString))
            '    cardpay.SetBalanceAmount(Convert.ToDouble(lblBalance.Text.ToString))
            '    'txtGiftVoucherAmt.Text = 0
            '    If cardpay.GetBalanceAmount() <= 0 Then
            '        txtGiftVoucherAmt.Enabled = False
            '        txtGiftVoucherCode.Enabled = False
            '        txtGiftVoucherCurrType.Enabled = False
            '        Amountpayable.Enabled = False
            '        cmbPayType.Enabled = False
            '        txtCurrencyType.Enabled = False
            '    End If
            '    Dim listcount As Integer = PaymentlistView.Items.Count
            '    Dim giftcount As Integer = GVListView.Items.Count
            '    Dim listcountt As Integer = listcount + 1
            '    Dim giftcountt As Integer = giftcount + 1
            '    rowItem(0) = listcountt.ToString
            '    PaymentlistView.Items.Add(listcountt)
            '    GVListView.Items.Add(giftcountt.ToString)
            '    rowItem(1) = txtGiftVoucherCode.Text.ToString
            '    PaymentlistView.Items(listcount).SubItems.Add(rowItem(1))
            '    GVListView.Items(giftcount).SubItems.Add(rowItem(1))
            '    rowItem(2) = txtGiftVoucherCurrType.Text.ToString
            '    'If Amountpayable.Enabled Then
            '    rowItem(3) = amountRec.ToString
            '    rowItem(4) = txtGiftVoucherNo.Text
            '    rowItem(5) = ""
            '    rowItem(6) = txtGiftVoucherDesc.Text
            '    PaymentlistView.Items(listcount).SubItems.Add(rowItem(6))
            '    GVListView.Items(giftcount).SubItems.Add(rowItem(6))
            '    PaymentlistView.Items(listcount).SubItems.Add(rowItem(2))
            '    GVListView.Items(giftcount).SubItems.Add(rowItem(2))
            '    PaymentlistView.Items(listcount).SubItems.Add(rowItem(3))
            '    GVListView.Items(giftcount).SubItems.Add(rowItem(4))
            '    GVListView.Items(giftcount).SubItems.Add(rowItem(3))
            '    rowItem(7) = "GV"

            '    rowItem(8) = Round(Convert.ToDouble("0"), 3)
            '    cardpay.addToPayment(rowItem)
            '    'MsgBox(cardpay.countCart)
            '    amountinaed.Text = 0
            '    txtGiftVoucherNo.Text = ""
            '    txtGiftVoucherAmt.Text = ""
            '    txtGiftVoucherCode.Text = ""
            '    txtGiftVoucherDesc.Text = ""
            '    Amountpayable.Text = ""

            '    For Each ctl As Control In pnl_GIFT.Controls
            '        If ctl.Name = "btnAddGiftVoucher" Then
            '            Dim btnrem As New Button
            '            btnrem.Name = "btnAddGiftVoucher"
            '            Me.pnl_GIFT.Controls.Remove(btnrem)
            '            Exit For
            '        End If
            '    Next
            '    For Each ctl As Control In pnl_GIFT.Controls
            '        If ctl.Name = "btnGVClose" Then
            '            Dim btnrem As New Button
            '            btnrem.Name = "btnGVClose"
            '            Me.pnl_GIFT.Controls.Remove(btnrem)
            '            Exit For
            '        End If
            '    Next
            'Else
            '    'MsgBox(Convert.ToDouble(Amountpayable.Text.ToString))
            'End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtGiftVoucherAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGiftVoucherAmt.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") < 0) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") >= 0 And tbx.SelectionLength > 0 And tbx.SelectionStart <= tbx.Text.IndexOf(".") And tbx.SelectionStart + tbx.SelectionLength > tbx.Text.IndexOf("."))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtGiftVoucherAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGiftVoucherAmt.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 0
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0

            ElseIf value >= 0 Then
                tbx.Text = Round(value, 3)
            Else
                tbx.Text = Abs(Round(value, 3))
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub loadReport()
        Try
            pnlReport.Visible = True
            pnlReport.Location = New Point(pnlINVDetails.Location.X, pnlINVDetails.Location.Y)
            pnlReport.Width = pnlINVDetails.Width
            pnlReport.Height = pnlINVDetails.Height + pnlItemDetails.Height + pnlBottomHolder.Height + 3
            lblReport.Width = pnlINVDetails.Width
            pnlReportOuter.Height = pnlINVDetails.Height + pnlItemDetails.Height + pnlBottomHolder.Height - 30

            lblSalesmanName.Text = txtSalesmanCode.Text

            Dim stQuery As String = ""
            Dim ds As DataSet
            If TXN_Code = "POSINV" Then
                stQuery = stQuery + " select rownum,b.invh_no ,to_char( b.invh_dt,'DD/MM/YYYY') as InvoiceDate, "
                stQuery = stQuery + " locn_name,"
                stQuery = stQuery + " d.addr_name|| d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||"
                stQuery = stQuery + "d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||"
                stQuery = stQuery + "d.ADDR_PROVINCE_CODE as Location_Address,"
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
                stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt"
                stQuery = stQuery + " from "
                stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
                stQuery = stQuery + " where b.invh_no = " + INVHNO.ToString + " and"
                stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
                stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"
            ElseIf TXN_Code = "SARTN" Then
                stQuery = stQuery + " select rownum,b.CSRH_NO ,to_char( b.CSRH_DT,'DD/MM/YYYY') as InvoiceDate, "
                stQuery = stQuery + " locn_name,"
                stQuery = stQuery + " d.addr_name|| d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||"
                stQuery = stQuery + "d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||"
                stQuery = stQuery + "d.ADDR_PROVINCE_CODE as Location_Address,"
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
                stQuery = stQuery + " NVL(a.CSRI_FC_VAL,'0') as ItmAmt,"
                stQuery = stQuery + " NVL((SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),'0') as disamt"
                stQuery = stQuery + " from "
                stQuery = stQuery + " OT_CUST_SALE_RET_HEAD b,OT_CUST_SALE_RET_ITEM a,om_location c,om_address d"
                stQuery = stQuery + " where b.CSRH_NO = " + CSRH_NO.ToString + " and"
                stQuery = stQuery + " b.CSRH_SYS_ID = a.CSRI_CSRH_SYS_ID and"
                stQuery = stQuery + " b.CSRH_LOCN_CODE = c.locn_code and c.locn_addr_code = d.addr_code"
                lblRptRptType.Text = "Sales Return"
                lblRptINVSONO.Text = "SRTN. No.:"

            ElseIf TXN_Code = "SO" Then

                stQuery = stQuery + " select rownum,b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,locn_name,"
                stQuery = stQuery + " d.addr_name||d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||d.ADDR_PROVINCE_CODE as Location_Address,"
                stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
                stQuery = stQuery + " case nvl(b.soH_FLEX_03,0) when '0' then (select cust_name from om_customer where cust_code = b.soh_cust_code)"
                stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where pm_cust_code = b.soh_flex_03) end as CustName,"
                stQuery = stQuery + " b.soh_BILL_ADDR_LINE_1||' '||b.soh_BILL_ADDR_LINE_2||' '||b.soh_BILL_COUNTRY_CODE as billing_addr,b.soH_BILL_TEL as billing_phone, b.soh_BILL_EMAIL as billing_email, b.soh_SHIP_ADDR_LINE_1||' '||b.soh_SHIP_ADDR_LINE_2||' '||b.soh_SHIP_COUNTRY_CODE as shipping_addr,"
                stQuery = stQuery + " a.soI_ITEM_CODE as ItemCode,a.soI_ITEM_DESC as ItemDesc,a.soI_UOM_CODE as ItmUOM,a.soI_PL_RATE as ItmPrice ,a.soI_QTY as ItmQty,a.soI_FC_VAL as ItmAmt,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID=SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt "
                stQuery = stQuery + " from "
                stQuery = stQuery + " ot_so_head b,ot_so_item a, om_location c,om_address d"
                stQuery = stQuery + " where b.soh_no = " + SOHNO.ToString + " and b.soh_sys_id = a.soi_soh_sys_id and b.soh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

                lblLblAdvance.Visible = True
                lblLblBalance.Visible = True

                Dim stBalanceQuery As String
                stBalanceQuery = "select sum(pinvp_fc_val) as advance from ot_pos_so_payment a,ot_so_head b where b.soh_no = " + SOHNO.ToString + " and b.soh_sys_id = a.pinvp_invh_sys_id "
                errLog.WriteToErrorLog("Balancheck Query", stBalanceQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "Balancheck QUERY  -  " & stBalanceQuery)
                Dim dsb As DataSet = db.SelectFromTableODBC(stBalanceQuery)
                If dsb.Tables("Table").Rows.Count > 0 Then
                    If Not dsb.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                        lblRptAdvancedPaid.Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                    Else
                        lblRptAdvancedPaid.Text = "0"
                    End If
                End If

            End If
            errLog.WriteToErrorLog("REPORT QUERY", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "Report query  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            If count > 0 Then
                lblRptInvoiceNo.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                lblRptInvoiceDate.Text = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                lblRptLocationName.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString
                lblRptLocationAddress.Text = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                If Not ds.Tables("Table").Rows.Item(0).Item(5).ToString = "" Then
                    lblPhone.Text = "Phone: " + ds.Tables("Table").Rows.Item(0).Item(5).ToString
                Else
                    lblPhone.Text = "Phone: " + "   -             "
                End If
                If Not ds.Tables("Table").Rows.Item(0).Item(6).ToString = "" Then
                    lblEmail.Text = "Email: " + ds.Tables("Table").Rows.Item(0).Item(6).ToString
                Else
                    lblEmail.Text = "Email: " + "   -             "
                End If
                If txtPatPatientNo.Text = "" Then
                    lblRptCustomerName.Text = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                    lblRptBillingAddress.Text = ds.Tables("Table").Rows.Item(0).Item(8).ToString
                    lblRptCustomerPhone.Text = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                    lblRptCustomerEmail.Text = ds.Tables("Table").Rows.Item(0).Item(10).ToString
                    lblRptShippingAddress.Text = ds.Tables("Table").Rows.Item(0).Item(11).ToString
                Else
                    Dim stQueryPatient As String
                    stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + txtPatPatientNo.Text + "'"
                    errLog.WriteToErrorLog("Patient Query", stQueryPatient, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "Patient Query  -  " & stQueryPatient)

                    Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                    If dsP.Tables("Table").Rows.Count > 0 Then
                        lblRptCustomerName.Text = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                        lblRptBillingAddress.Text = dsP.Tables("Table").Rows.Item(0).Item(4).ToString
                        lblRptCustomerPhone.Text = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                        lblRptCustomerEmail.Text = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                        lblRptShippingAddress.Text = dsP.Tables("Table").Rows.Item(0).Item(1).ToString
                    End If
                End If
                lblTotalPages.Text = "Tot.Pages: " + Math.Ceiling(count / 6).ToString

            End If
            Dim i As Integer = 0

            Dim j As Integer = 0

            While count > 0

                Dim rowItem(7) As String
                rowItem(0) = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                rowItem(1) = ds.Tables("Table").Rows.Item(i).Item(12).ToString
                rowItem(2) = ds.Tables("Table").Rows.Item(i).Item(13).ToString
                rowItem(3) = ds.Tables("Table").Rows.Item(i).Item(14).ToString
                rowItem(4) = ds.Tables("Table").Rows.Item(i).Item(15).ToString
                rowItem(5) = ds.Tables("Table").Rows.Item(i).Item(16).ToString
                rowItem(6) = ds.Tables("Table").Rows.Item(i).Item(17).ToString
                rowItem(7) = ds.Tables("Table").Rows.Item(i).Item(18).ToString
                i = i + 1
                count = count - 1
                rptList.Add(rowItem)
            End While
            Dim totalAmt As Double = 0
            Dim subtotalAmt As Double = 0
            Dim disAmt As Double = 0
            i = 0
            count = ds.Tables("Table").Rows.Count
            While count > 0

                If i / 5 > 1 Then
                    lblRptSubTotal.Visible = False
                    lblRptDiscount.Visible = False
                    lblRptTOTAL.Visible = False
                    lblRptExpense.Visible = False
                    lblRptAdvancedPaid.Visible = False
                    lblRptBalance.Visible = False
                    Exit Sub
                Else
                    If TXN_Code = "SO" Then
                        lblRptAdvancedPaid.Visible = True
                        lblRptBalance.Visible = True
                        totalAmt = Round(subtotalAmt - disAmt, 3)

                    End If
                    If transtype = "Sales Invoice" Then
                        lblRptAdvancedPaid.Visible = True
                        lblRptBalance.Visible = True
                        lblLblAdvance.Visible = True
                        lblLblBalance.Visible = True
                        lblRptRptType.Text = "Sales Invoice"
                    End If

                End If

                Me.pnlRptItemsHolder.AutoScrollPosition = New System.Drawing.Point(0, 0)
                Dim lbl As Label
                Dim n As Integer
                n = i + 1
                lbl = New Label
                With lbl
                    .Location = New Point(0, (n * 40))
                    .Name = "lblSNO" & n.ToString
                    .Size = New Size(lblRptSNOHead.Width - 1, 20)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = n
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptItemCodeHead.Location.X + 1, (n * 40))
                    .Name = "lblItemCode" & n.ToString
                    .Size = New Size(lblRptItemCodeHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(12).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptItemDescHead.Location.X + 1, (n * 40))
                    .Name = "lblItemDesc" & n.ToString
                    .Size = New Size(lblRptItemDescHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(13).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptUOMHead.Location.X + 1, (n * 40))
                    .Name = "lblItemUOM" & n.ToString
                    .Size = New Size(lblRptUOMHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(14).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptRateHead.Location.X + 1, (n * 40))
                    .Name = "lblItemRate" & n.ToString
                    .Size = New Size(lblRptRateHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(15).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptQtyHead.Location.X + 1, (n * 40))
                    .Name = "lblItemQty" & n.ToString
                    .Size = New Size(lblRptQtyHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(16).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptAmtHead.Location.X + 1, (n * 40))
                    .Name = "lblItemAmt" & n.ToString
                    .Size = New Size(lblRptAmtHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopRight
                End With

                lbl.Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(17).ToString), 3).ToString(".000")
                Me.pnlRptItemsHolder.Controls.Add(lbl)
                subtotalAmt = subtotalAmt + Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(17).ToString), 3)
                If ds.Tables("Table").Rows.Item(i).Item(18).ToString = "" Then
                    disAmt = disAmt + 0
                Else
                    disAmt = disAmt + Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(18).ToString), 3)
                End If

                lblRptSubTotal.Text = subtotalAmt.ToString("0.000")
                lblRptDiscount.Text = disAmt.ToString("0.000")
                totalAmt = Round(subtotalAmt - disAmt, 3)
                lblRptTOTAL.Text = totalAmt.ToString("0.000")

                i = i + 1
                count = count - 1
            End While
            callReportTotal()
            If transtype = "Sales Invoice" Then
                lblRptAdvancedPaid.Text = Convert.ToDouble(txtAdvPaid.Text).ToString("0.000")
                lblRptBalance.Text = Convert.ToDouble(txtBalancePay.Text).ToString("0.000")
                Label22.Visible = True
                Label28.Text = salesorders
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub callReportTotal()
        Try
            Dim totalAmt As Double = 0
            Dim subtotalAmt As Double = 0
            Dim disAmt As Double = 0
            For i = 0 To rptList.Count - 1
                Dim rowItem(7) As String
                rowItem = rptList(i)
                subtotalAmt = subtotalAmt + Round(Convert.ToDouble(rowItem(6).ToString), 3)
                disAmt = disAmt + Round(Convert.ToDouble(rowItem(7).ToString), 3)
                totalAmt = Round(subtotalAmt - disAmt, 3)
            Next

            lblRptSubTotal.Text = subtotalAmt.ToString("0.000")
            lblRptDiscount.Text = disAmt.ToString("0.000")
            totalAmt = totalAmt + expenseAmount
            lblRptTOTAL.Text = totalAmt.ToString("0.000")
            lblRptExpense.Text = expenseAmount.ToString("0.000")

            If TXN_Code = "SO" Then
                lblRptBalance.Text = (totalAmt - Convert.ToDouble(lblRptAdvancedPaid.Text.ToString)).ToString("0.000")
                lblRptRptType.Text = "Sales Order"
                lblRptINVSONO.Text = "SOrder No. :"

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub txtRptPage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRptPage.KeyPress

        Try

            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtRptPage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRptPage.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtRptPage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRptPage.TextChanged
        Try
            If txtRptPage.Text = "0" Then
                MsgBox("Page Number cannot be 0")
                Exit Sub
            Else
                If txtRptPage.Text = "" Then
                    txtRptPage.Undo()
                    Exit Sub
                End If

                Dim itemStart As Integer = (Convert.ToInt64(txtRptPage.Text) * 6) - 6
                Dim itemEnd As Integer = itemStart + 5

                If itemStart >= rptList.Count Then
                    MsgBox("Page " + txtRptPage.Text + " not found!")
                    Exit Sub
                End If

                pnlRptItemsHolder.Controls.Clear()

                pnlRptItemsHolder.Controls.Add(Panel21)
                Panel21.BringToFront()

                Dim pnl As Panel
                pnl = New Panel
                With pnl
                    .Location = New Point(32, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep1"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                pnl = New Panel
                With pnl
                    .Location = New Point(151, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep2"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                pnl = New Panel
                With pnl
                    .Location = New Point(436, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep3"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                pnl = New Panel
                With pnl
                    .Location = New Point(495, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep4"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                pnl = New Panel
                With pnl
                    .Location = New Point(565, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep5"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                pnl = New Panel
                With pnl
                    .Location = New Point(615, 11)
                    .Size = New Size(1, 278)
                    .Name = "pnlRptSep6"
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.pnlRptItemsHolder.Controls.Add(pnl)

                Dim k = 0
                For i = itemStart To itemEnd

                    If i >= rptList.Count Then
                        lblRptSubTotal.Visible = True
                        lblRptDiscount.Visible = True
                        lblRptTOTAL.Visible = True
                        lblRptExpense.Visible = True
                        If TXN_Code = "SO" Then
                            lblRptAdvancedPaid.Visible = True
                            lblRptBalance.Visible = True
                        End If
                        If transtype = "Sales Invoice" Then
                            lblRptAdvancedPaid.Visible = True
                            lblRptBalance.Visible = True
                            lblLblAdvance.Visible = True
                            lblLblBalance.Visible = True
                        End If
                        lblRptPageBottom.Text = "Page " + txtRptPage.Text
                        callReportTotal()
                        Exit Sub
                    End If
                    lblRptPageBottom.Text = "Page " + txtRptPage.Text
                    Dim rowItem() As String
                    rowItem = rptList(i)
                    Me.pnlRptItemsHolder.AutoScrollPosition = New System.Drawing.Point(0, 0)

                    Dim lbl As Label
                    Dim n As Integer
                    n = k + 1
                    lbl = New Label
                    With lbl
                        .Location = New Point(0, (n * 40))
                        .Name = "lblSNO" & n.ToString
                        .Size = New Size(lblRptSNOHead.Width - 1, 20)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = i + 1
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptItemCodeHead.Location.X + 1, (n * 40))
                        .Name = "lblItemCode" & n.ToString
                        .Size = New Size(lblRptItemCodeHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = rowItem(1).ToString
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptItemDescHead.Location.X + 1, (n * 40))
                        .Name = "lblItemDesc" & n.ToString
                        .Size = New Size(lblRptItemDescHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = rowItem(2).ToString
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptUOMHead.Location.X + 1, (n * 40))
                        .Name = "lblItemUOM" & n.ToString
                        .Size = New Size(lblRptUOMHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = rowItem(3).ToString
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptRateHead.Location.X + 1, (n * 40))
                        .Name = "lblItemRate" & n.ToString
                        .Size = New Size(lblRptRateHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = rowItem(4).ToString
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptQtyHead.Location.X + 1, (n * 40))
                        .Name = "lblItemQty" & n.ToString
                        .Size = New Size(lblRptQtyHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopCenter
                    End With
                    lbl.Text = rowItem(5).ToString
                    Me.pnlRptItemsHolder.Controls.Add(lbl)

                    lbl = New Label
                    With lbl
                        .Location = New Point(lblRptAmtHead.Location.X + 1, (n * 40))
                        .Name = "lblItemAmt" & n.ToString
                        .Size = New Size(lblRptAmtHead.Width - 2, 35)
                        .TextAlign = ContentAlignment.TopRight
                    End With

                    lbl.Text = Round(Convert.ToDouble(rowItem(6)), 3).ToString(".000")
                    Me.pnlRptItemsHolder.Controls.Add(lbl)


                    k = k + 1
                    lblRptSubTotal.Visible = False
                    lblRptDiscount.Visible = False
                    lblRptTOTAL.Visible = False
                    lblRptAdvancedPaid.Visible = False
                    lblRptBalance.Visible = False
                    lblRptExpense.Visible = False
                Next
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnProceedTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceedTransaction.Click
        'loadReport()
        'Exit Sub
        lblTransLoader.BringToFront()
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Function SalesReturnDone() As Boolean
        Try
            'Using connection As New OdbcConnection(db.GetConnectionString(""))
            Using connection As New OracleConnection(db.GetConnectionString(""))
                Dim command As New OracleCommand
                'Dim command As New OdbcCommand()
                Dim transaction As OracleTransaction
                'Dim transaction As OdbcTransaction
                command.Connection = connection
                Try
                    connection.Open()
                    transaction = connection.BeginTransaction()

                    command.Connection = connection
                    command.Transaction = transaction

                    Dim ds As DataSet
                    Dim i As Integer = 0
                    Dim stQuery As String
                    Dim count As Integer
                    Dim maxSYS_ID As Integer
                    Dim maxInv As Integer
                    Dim TXN_FM_NO As Integer
                    Dim TXN_CURR_NO As Integer
                    Dim TXN_TO_NO As Integer
                    Dim row As System.Data.DataRow

                    Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
                    txtSalesmanCode.Text = strSalesArr(0)

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "SELECT CSRH_SYS_ID.NEXTVAL FROM DUAL"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxSYS_ID = Convert.ToInt32(row.Item(0).ToString)
                    End If

                    stQuery = "SELECT TXN_CODE,TXN_TYPE  FROM OM_TXN WHERE TXN_CODE = 'SARTN' AND TXN_TYPE = 'SARTN'"
                    errLog.WriteToErrorLog("txn query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "txn Query  -  " & stQuery)

                    ds = db.SelectFromTableODBC(stQuery)

                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        TXN_Code = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                        TXN_Type = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                    End If

                    stQuery = New String("")
                    stQuery = "SELECT TXND_CURR_NO, TXND_TO_NO, TXND_FM_NO FROM OM_TXN_DOC_RANGE WHERE TXND_COMP_CODE ='" & CompanyCode & "' AND TXND_TXN_CODE ='" & TXN_Code & "' AND TXND_LOCN_CODE ='" & Location_Code & "'"
                    errLog.WriteToErrorLog("txnd Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "txnd Query  -  " & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        TXN_CURR_NO = Convert.ToInt32(row.Item(0).ToString)
                        TXN_FM_NO = Convert.ToInt32(row.Item(2).ToString)
                        TXN_TO_NO = Convert.ToInt32(row.Item(1).ToString)
                    End If

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select nvl(max(CSRH_NO),0) from OT_CUST_SALE_RET_HEAD where CSRH_NO between " & TXN_FM_NO & " and " & TXN_TO_NO
                    errLog.WriteToErrorLog("csrh Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "csrh Query  -  " & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxInv = Convert.ToInt32(row.Item(0).ToString)
                        If maxInv = 0 Then
                            maxInv = TXN_FM_NO
                        Else
                            maxInv = maxInv + 1
                            CSRH_NO = maxInv
                        End If
                    End If

                    stQuery = "UPDATE OM_TXN_DOC_RANGE SET TXND_CURR_NO =" & maxInv & " WHERE TXND_COMP_CODE = '" & CompanyCode & "' AND TXND_TXN_CODE = '" & TXN_Code & "' AND TXND_LOCN_CODE = '" & Location_Code & "'"
                    errLog.WriteToErrorLog("Update OM_TXN_DOC_RANGE SALESRETURN", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "Update OM_TXN_DOC_RANGE SALESRETURN  -  " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    Dim sms_shift_Code As New String("")
                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & txtSalesmanCode.Text & "'"
                    errLog.WriteToErrorLog("shift Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "shift Query  -  " & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        sms_shift_Code = row.Item(0).ToString
                    End If

                    stQuery = "Select INVH_SYS_ID,INVH_CUST_CODE,INVH_SHIP_TO_ADDR_CODE,INVH_BILL_TO_ADDR_CODE,INVH_CUST_NAME,INVH_BILL_ADDR_LINE_1,INVH_BILL_ADDR_LINE_2,INVH_BILL_ADDR_LINE_3,INVH_BILL_ADDR_LINE_4,INVH_BILL_ADDR_LINE_5,INVH_BILL_CITY_CODE,INVH_BILL_COUNTY_CODE,INVH_BILL_STATE_CODE,INVH_BILL_POSTAL_CODE,INVH_BILL_COUNTRY_CODE,INVH_BILL_CONTACT,INVH_BILL_EMAIL,INVH_BILL_FAX,INVH_BILL_TEL,INVH_BILL_MOBILE,INVH_FLEX_03,INVH_FLEX_04 from OT_INVOICE_HEAD where INVH_NO=" & txtSRTransNo.Text
                    errLog.WriteToErrorLog("invhsys Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "invhsys Query" & stQuery)
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then

                        row = ds.Tables("Table").Rows.Item(0)
                        stQuery = "INSERT INTO OT_CUST_SALE_RET_HEAD (CSRH_SYS_ID,CSRH_COMP_CODE,CSRH_TXN_CODE,CSRH_NO,CSRH_DT,CSRH_ACNT_YEAR,CSRH_CAL_YEAR,CSRH_CAL_PERIOD,CSRH_DOC_SRC_LOCN_CODE,CSRH_REF_FROM,CSRH_REF_FROM_NUM,CSRH_REF_TXN_CODE,CSRH_REF_NO,CSRH_REF_SYS_ID,CSRH_LOCN_CODE,CSRH_DEL_LOCN_CODE,CSRH_CUST_CODE,CSRH_CURR_CODE,CSRH_SHIP_TO_ADDR_CODE,CSRH_BILL_TO_ADDR_CODE,CSRH_EXGE_RATE,CSRH_DISC_PERC,CSRH_FC_DISC_VAL,CSRH_SM_CODE,CSRH_STATUS,CSRH_PRINT_STATUS,CSRH_APPR_STATUS,CSRH_APPR_UID,CSRH_COS_FIN_STATUS,CSRH_ANNOTATION,CSRH_FLEX_10,CSRH_FLEX_14,CSRH_FLEX_15,CSRH_FLEX_16,CSRH_FLEX_17,CSRH_FLEX_18,CSRH_FLEX_19,CSRH_FLEX_20,CSRH_CUST_NAME,CSRH_BILL_ADDR_LINE_1,CSRH_BILL_ADDR_LINE_2,CSRH_BILL_ADDR_LINE_3,CSRH_BILL_ADDR_LINE_4, CSRH_BILL_ADDR_LINE_5,CSRH_BILL_CITY_CODE,CSRH_BILL_COUNTY_CODE,CSRH_BILL_STATE_CODE,CSRH_BILL_POSTAL_CODE,CSRH_BILL_COUNTRY_CODE,CSRH_BILL_CONTACT ,CSRH_BILL_EMAIL,CSRH_BILL_FAX,CSRH_BILL_TEL,CSRH_BILL_MOBILE,CSRH_CR_UID,CSRH_CR_DT,CSRH_FLEX_03,CSRH_FLEX_04) VALUES("
                        stQuery = stQuery & maxSYS_ID & ",'" & CompanyCode & "','" & TXN_Code & "'," & maxInv & ",'" & dtpick.Value.ToString("dd-MMM-yy") & "'," & PC_Account_Year & "," & PC_CAL_Year & "," & PC_CAL_Period & ",'" & Location_Code & "','IN',15,'POSINV'," & txtSRTransNo.Text & "," & row.Item(0).ToString & ",'" & Location_Code & "','" & Location_Code & "','" & row.Item(1).ToString & "','" & Currency_Code & "','" & row.Item(2).ToString & "','" & row.Item(3).ToString & "'," & Exchange_Rate & ",0,0,'" & txtSalesmanCode.Text & "','',1,0,'" & LogonUser & "','','','N','N','','','','','" & sms_shift_Code & "','" & POSCounterNumber & "','" & row.Item(4).ToString & "','" & row.Item(5).ToString & "','" & row.Item(6).ToString & "','" & row.Item(7).ToString & "','" & row.Item(8).ToString & "','" & row.Item(9).ToString & "','" & row.Item(10).ToString & "','" & row.Item(11).ToString & "','" & row.Item(12).ToString & "','" & row.Item(13).ToString & "','" & row.Item(14).ToString & "','" & row.Item(15).ToString & "','" & row.Item(16).ToString & "','" & row.Item(17).ToString & "','" & row.Item(18).ToString & "','" & row.Item(19).ToString & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'),'" & row.Item(20).ToString.Replace("'", "''") & "','" & row.Item(21).ToString.Replace("'", "''") & "')"
                        errLog.WriteToErrorLog("INSERT QUERY OT_CUST_SALE_RET_HEAD", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_CUST_SALE_RET_HEAD  -  " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        Dim maxREF As String = ""
                        Dim dsIN As DataSet
                        stQuery = "SELECT CSRR_SYS_ID.NEXTVAL FROM DUAL"
                        dsIN = db.SelectFromTableODBC(stQuery)
                        count = dsIN.Tables("Table").Rows.Count
                        If count > 0 Then
                            maxREF = dsIN.Tables("Table").Rows.Item(0).Item(0).ToString()
                        End If

                        stQuery = "INSERT INTO OT_CUST_SALE_RET_REF (CSRR_SYS_ID,CSRR_CSRH_SYS_ID,CSRR_REF_TXN_CODE,CSRR_REF_NO,CSRR_REF_SYS_ID,CSRR_CR_DT,CSRR_CR_UID) VALUES ("
                        stQuery = stQuery & maxREF & "," & maxSYS_ID & ",'POSINV'," & txtSRTransNo.Text & "," & row.Item(0).ToString & ",current_timestamp,'" & LogonUser & "')"
                        errLog.WriteToErrorLog("INSERT QUERY OT_CUST_SALE_RET_REF", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_CUST_SALE_RET_REF  -  " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                        Dim taxImpl As New TaxImpl(db)

                        For k = 1 To txtItemCode.Count
                            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & k, True)
                            Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" & k, True)
                            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & k, True)
                            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" & k, True)
                            Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisc" & k, True)
                            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" & k, True)
                            Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemNetamt" & k, True)
                            Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" & k, True)

                            Dim dsI As DataSet
                            stQuery = "SELECT INVI_SYS_ID,INVI_INVH_SYS_ID,INVI_ITEM_CODE,INVI_ITEM_STK_YN_NUM,INVI_ITEM_DESC,INVI_UOM_CODE,INVI_RATE,INVI_GRADE_CODE_1,INVI_GRADE_CODE_2 FROM OT_INVOICE_ITEM A, OT_CUST_SALE_RET_ITEM B WHERE "
                            stQuery = stQuery + "A.INVI_ITEM_CODE = B.CSRI_ITEM_CODE AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ") AND "
                            stQuery = stQuery + "CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO = " + txtSRTransNo.Text + ")  AND INVI_ITEM_CODE='" + ItmCodeFound(0).Text + "' "
                            stQuery = stQuery + "GROUP BY INVI_SYS_ID,INVI_INVH_SYS_ID,INVI_ITEM_CODE,INVI_ITEM_STK_YN_NUM,INVI_ITEM_DESC,INVI_UOM_CODE,INVI_RATE,INVI_GRADE_CODE_1,INVI_GRADE_CODE_2 "
                            stQuery = stQuery + "UNION "
                            stQuery = stQuery + "SELECT INVI_SYS_ID,INVI_INVH_SYS_ID,INVI_ITEM_CODE,INVI_ITEM_STK_YN_NUM,INVI_ITEM_DESC,INVI_UOM_CODE,INVI_RATE,INVI_GRADE_CODE_1,INVI_GRADE_CODE_2 FROM OT_INVOICE_ITEM A WHERE INVI_ITEM_CODE='" + ItmCodeFound(0).Text + "' AND "
                            stQuery = stQuery + "A.INVI_ITEM_CODE NOT IN (SELECT CSRI_ITEM_CODE FROM OT_CUST_SALE_RET_ITEM B WHERE CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO =" + txtSRTransNo.Text + ")) AND "
                            stQuery = stQuery + "INVI_QTY_BU > 0 AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ")"
                            dsI = db.SelectFromTableODBC(stQuery)
                            Dim rowI As System.Data.DataRow
                            If dsI.Tables("Table").Rows.Count > 0 Then
                                rowI = dsI.Tables("Table").Rows.Item(0)

                                Dim maxItemSYSID As String = ""
                                stQuery = "SELECT CSRI_SYS_ID.NEXTVAL FROM DUAL"
                                dsIN = db.SelectFromTableODBC(stQuery)
                                count = dsIN.Tables("Table").Rows.Count
                                If count > 0 Then
                                    maxItemSYSID = dsIN.Tables("Table").Rows.Item(0).Item(0).ToString()
                                End If

                                stQuery = "SELECT ITEM_STK_YN_NUM, ITEM_SNO_YN_NUM, ITEM_BATCH_YN_NUM FROM OM_ITEM WHERE ITEM_CODE='" & ItmCodeFound(0).Text & "'"
                                dsIN = db.SelectFromTableODBC(stQuery)

                                'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                                Dim itemCodeForTaxEntry As String = ""
                                Dim itemPriceForTaxEntry As Double = (Convert.ToDouble(ItmQtyFound(0).Text) * Convert.ToDouble(ItmPriceFound(0).Text)) - Convert.ToDouble(ItmDisamtFound(0).Text)
                                itemCodeForTaxEntry = rowI.Item(2).ToString

                                'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                                Dim taxCode As String = taxImpl.getLocationTaxCodeForItem(Location_Code, itemCodeForTaxEntry)
                                Dim taxPercentage As Double = taxImpl.getTaxPercentageofItem(itemCodeForTaxEntry, Location_Code, TXN_Code, taxCode)
                                Dim unroundtaxValueOfItem As Double = taxImpl.calculateTaxValueofItem(itemCodeForTaxEntry, itemPriceForTaxEntry, Location_Code, TXN_Code, taxCode, taxPercentage)
                                Dim taxValueOfItem As Double = Round(unroundtaxValueOfItem, 3)
                                'taxValueOfItem = taxValueOfItem


                                stQuery = "INSERT INTO OT_CUST_SALE_RET_ITEM (CSRI_SYS_ID,CSRI_CSRH_SYS_ID,CSRI_INVI_SYS_ID,CSRI_ITEM_CODE,CSRI_ITEM_STK_YN_NUM,CSRI_ITEM_DESC,CSRI_UOM_CODE,CSRI_QTY,CSRI_QTY_LS,CSRI_QTY_BU,CSRI_INVI_QTY_BU,CSRI_DNI_QTY_BU,CSRI_RATE,CSRI_DISC_PERC,CSRI_FC_DISC_VAL,CSRI_FC_VAL,CSRI_FC_VAL_AFT_H_DISC,CSRI_UPD_STK_YN,CSRI_LOCN_CODE,CSRI_DEL_LOCN_CODE,CSRI_SM_CODE,CSRI_GRADE_CODE_1,CSRI_GRADE_CODE_2,CSRI_FLEX_18,CSRI_FLEX_19,CSRI_FLEX_20,CSRI_CR_UID,CSRI_CR_DT, CSRI_FC_ACT_VAL) VALUES ("
                                stQuery = stQuery & maxItemSYSID & "," & maxSYS_ID & "," & rowI.Item(0).ToString & ",'" & rowI.Item(2).ToString & "','" & dsIN.Tables("Table").Rows.Item(0).Item(0).ToString() & "','" & rowI.Item(4).ToString & "','" & rowI.Item(5).ToString & "'," & ItmQtyFound(0).Text & ",0," & ItmQtyFound(0).Text & ",0,0," & rowI.Item(6).ToString & ",0," & ItmDisamtFound(0).Text & "," & (Convert.ToDouble(ItmQtyFound(0).Text) * Convert.ToDouble(ItmPriceFound(0).Text)) - Convert.ToDouble(ItmDisamtFound(0).Text) - taxValueOfItem & "," & Convert.ToDouble(ItmQtyFound(0).Text) * Convert.ToDouble(ItmPriceFound(0).Text) & ",'Y','" & Location_Code & "','" & Location_Code & "','" & txtSalesmanCode.Text & "','" & rowI.Item(7).ToString & "','" & rowI.Item(8).ToString & "','" & rowI.Item(2).ToString & "','','','" & LogonUser & "',current_timestamp," & (Convert.ToDouble(ItmQtyFound(0).Text) * Convert.ToDouble(ItmPriceFound(0).Text)) - Convert.ToDouble(ItmDisamtFound(0).Text) - taxValueOfItem & ")"
                                errLog.WriteToErrorLog("INSERT QUERY OT_CUST_SALE_RET_ITEM", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_CUST_SALE_RET_ITEM  -   " & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()

                                
                                If Not taxValueOfItem.Equals(0) Then
                                    stQuery = New String("")
                                    stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TAX'"
                                    Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                                    Dim TEDTAX_NUM As String = ""
                                    If dsTED.Tables("Table").Rows.Count > 0 Then
                                        TEDTAX_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                                    End If

                                    stQuery = "SELECT ITED_TED_RATE,ITED_TED_CODE,ITED_SYS_ID from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=" & rowI.Item(0).ToString & " and ITED_H_SYS_ID=" & rowI.Item(1).ToString & " and ITED_TED_TYPE_NUM=" & TEDTAX_NUM
                                    dsTED = db.SelectFromTableODBC(stQuery)
                                    Dim TEDRATE As String = "0"
                                    Dim TEDCODE As String = ""
                                    If dsTED.Tables("Table").Rows.Count > 0 Then
                                        If dsTED.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                                            TEDRATE = "0"
                                        Else
                                            TEDRATE = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                                        End If

                                        TEDCODE = dsTED.Tables("Table").Rows.Item(0).Item(1).ToString
                                        stQuery = "INSERT INTO OT_CUST_SALE_RET_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT, ITED_INVH_SYS_ID, ITED_INVI_SYS_ID, ITED_ITED_SYS_ID, ITED_TED_TYPE_CODE, ITED_TED_INCL_EXCL_FLAG, ITED_TXN_UOM, ITED_TXN_QTY, ITED_USER_QTY, ITED_SET_OFF_PERC, ITED_SET_OFF_FC_AMT, ITED_SET_OFF_LC_AMT, ITED_UNROUND_FC_AMT,ITED_UNROUND_LC_AMT, ITED_UPDATED_YN_NUM, ITED_POST_TYPE_NUM)VALUES("
                                        stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxItemSYSID & ",'" & TEDCODE & "'," & TEDTAX_NUM & ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," & TEDRATE & "," & itemPriceForTaxEntry - taxValueOfItem & "," & itemPriceForTaxEntry - taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & ",'" + LogonUser + "',sysdate," & rowI.Item(1) & "," & rowI.Item(0) & "," & dsTED.Tables("Table").Rows.Item(0).Item(2).ToString & ",'TAX','E','" & rowI.Item(5).ToString & "'," & rowI.Item(3) & "," & ItmQtyFound(0).Text & ",0,0,0," & unroundtaxValueOfItem & "," & unroundtaxValueOfItem & ",2,1)"
                                        errLog.WriteToErrorLog("QUERY INSERT ITEM TED TAX", stQuery, "")
                                        command.CommandText = stQuery
                                        command.ExecuteNonQuery()
                                    End If
                                End If

                                If Not ItmDisamtFound(0).Text = "" Or Not ItmDisamtFound(0).Text = "0" Then
                                    stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS'"
                                    Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                                    Dim TEDDIS_NUM As String = ""
                                    If dsTED.Tables("Table").Rows.Count > 0 Then
                                        TEDDIS_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                                    End If

                                    stQuery = "SELECT ITED_TED_RATE,ITED_TED_CODE,ITED_SYS_ID from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=" & rowI.Item(0).ToString & " and ITED_H_SYS_ID=" & rowI.Item(1).ToString & " and ITED_TED_TYPE_NUM=" & TEDDIS_NUM
                                    dsTED = db.SelectFromTableODBC(stQuery)
                                    Dim TEDRATE As String = "0"
                                    Dim TEDCODE As String = ""
                                    If dsTED.Tables("Table").Rows.Count > 0 Then
                                        If dsTED.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                                            TEDRATE = "0"
                                        Else
                                            TEDRATE = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                                        End If

                                        TEDCODE = dsTED.Tables("Table").Rows.Item(0).Item(1).ToString
                                        stQuery = "INSERT INTO OT_CUST_SALE_RET_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT, ITED_INVH_SYS_ID, ITED_INVI_SYS_ID, ITED_ITED_SYS_ID, ITED_TED_TYPE_CODE, ITED_TED_INCL_EXCL_FLAG, ITED_TXN_UOM, ITED_TXN_QTY, ITED_USER_QTY, ITED_SET_OFF_PERC, ITED_SET_OFF_FC_AMT, ITED_SET_OFF_LC_AMT, ITED_UNROUND_FC_AMT,ITED_UNROUND_LC_AMT, ITED_UPDATED_YN_NUM, ITED_POST_TYPE_NUM)VALUES("
                                        stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxItemSYSID & ",'" & TEDCODE & "'," + TEDDIS_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + "0" + "," + rowI.Item(6).ToString + "," + rowI.Item(6).ToString + "," + ItmDisamtFound(0).Text + "," + ItmDisamtFound(0).Text + "," + ItmDisamtFound(0).Text + "," + ItmDisamtFound(0).Text + ",'" + LogonUser + "',sysdate," & rowI.Item(1) & "," & rowI.Item(0) & "," & dsTED.Tables("Table").Rows.Item(0).Item(2).ToString & ",'TEDDIS','E','" & rowI.Item(5).ToString & "'," & rowI.Item(3) & "," & ItmQtyFound(0).Text & ",0,0,0," & ItmDisamtFound(0).Text & "," & ItmDisamtFound(0).Text & ",2,1)"
                                        errLog.WriteToErrorLog("QUERY INSERT ITEM TEDDIS", stQuery, "")
                                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                        'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDDIS  -  " & stQuery)
                                        command.CommandText = stQuery
                                        command.ExecuteNonQuery()
                                    End If


                                End If

                                If Not ItmAddvalFound(0).Text = "" Or Not ItmAddvalFound(0).Text = "0" Then

                                    stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP'"
                                    Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                                    Dim TEDEXP_NUM As String = ""
                                    If dsTED.Tables("Table").Rows.Count > 0 Then
                                        TEDEXP_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                                        stQuery = "SELECT ITED_TED_RATE,ITED_TED_CODE from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=" & rowI.Item(0).ToString & " and ITED_H_SYS_ID=" & rowI.Item(1).ToString & " and ITED_TED_TYPE_NUM=" & TEDEXP_NUM
                                        dsTED = db.SelectFromTableODBC(stQuery)

                                        If dsTED.Tables("Table").Rows.Count > 0 Then
                                            Dim TEDCODE As String = ""
                                            TEDCODE = dsTED.Tables("Table").Rows.Item(0).Item(1).ToString
                                            stQuery = "INSERT INTO OT_CUST_SALE_RET_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TAXABLE_FC_AMT,ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxItemSYSID & ",'" & TEDCODE & "'," + TEDEXP_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + rowI.Item(6).ToString + "," + rowI.Item(6).ToString + "," + ItmAddvalFound(0).Text + "," + ItmAddvalFound(0).Text + "," + ItmAddvalFound(0).Text + "," + ItmAddvalFound(0).Text + ",'" + LogonUser + "',sysdate)"
                                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDEXP", stQuery, "")
                                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDEXP  -  " & stQuery)
                                            command.CommandText = stQuery
                                            command.ExecuteNonQuery()
                                        End If
                                    End If
                                End If

                            End If

                        Next

                        Dim listPayment As New List(Of String())
                        i = 0
                        listPayment = cardpay.GetPaymentItems()

                        count = listPayment.Count

                        If count > 0 Then
                            Dim rowPayment(8) As String
                            While count > 0
                                stQuery = New String("")

                                rowPayment = listPayment.Item(i)

                                If Not rowPayment(8) = "0" Then
                                    rowPayment(3) = Convert.ToDouble(rowPayment(3).ToString) - Convert.ToDouble(rowPayment(8).ToString)
                                End If

                                'stQuery = "insert into OT_POS_CUST_SALE_RET_PAYMENT(PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID,PINVP_LC_VAL)values("
                                stQuery = "INSERT INTO OT_POS_CUST_SALE_RET_PAYMENT (PCSRTP_SYS_ID,PCSRTP_CSRH_SYS_ID ,PCSRTP_PPD_CODE,PCSRTP_PPD_DESC,PCSRTP_CURR_CODE,PCSRTP_FC_VAL,PCSRTP_FLEX_20,PCSRTP_CR_DT,PCSRTP_CR_UID,PCSRTP_LC_VAL) VALUES ("
                                stQuery = stQuery & "POS_CSRT_SYS_ID.NEXTVAL" & "," & maxSYS_ID & ",'" & rowPayment(1).ToString & "','" & rowPayment(6).ToString & "','" & rowPayment(2).ToString & "'," & Convert.ToDouble(rowPayment(3).ToString) & ",'" & rowPayment(6).ToString & "',sysdate,'" & LogonUser & "'," & Convert.ToDouble(rowPayment(3).ToString) & ")"
                                errLog.WriteToErrorLog("QUERY Payment Insertion Sales Return", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, "QERY PAYMENT INSERTION SALES RETURN  -  " & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()

                                i = i + 1
                                count = count - 1
                            End While
                        End If

                    End If
                    transaction.Commit()
                    lblTransLoader.SendToBack()
                    MsgBox("Sales Return transaction completed Successfully!")
                    Return True
                Catch ex As Exception
                    qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                    transaction.Rollback()
                    Return False
                End Try
            End Using

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.StackTrace, ex.Message)
            Return False
        End Try
    End Function

    Private Function SalesInvoiceDone() As Boolean
        Try

            'Using connection As New OdbcConnection(db.GetConnectionString(""))
            Using connection As New OracleConnection(db.GetConnectionString(""))
                Dim command As New OracleCommand
                'Dim command As New OdbcCommand()
                Dim transaction As OracleTransaction
                'Dim transaction As OdbcTransaction
                command.Connection = connection
                Try
                    connection.Open()
                    transaction = connection.BeginTransaction()

                    command.Connection = connection
                    command.Transaction = transaction

                    Dim ds As DataSet
                    Dim i As Integer = 0
                    Dim stQuery As String
                    Dim count As Integer
                    Dim maxSYS_ID As Integer
                    Dim maxInv As Integer
                    Dim TXN_FM_NO As Integer
                    Dim TXN_CURR_NO As Integer
                    Dim TXN_TO_NO As Integer
                    Dim row As System.Data.DataRow
                    Dim expAmount As Double = 0

                    Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
                    txtSalesmanCode.Text = strSalesArr(0)

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "SELECT INVH_SYS_ID.NEXTVAL FROM DUAL"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxSYS_ID = Convert.ToInt32(row.Item(0).ToString)
                    End If

                    stQuery = New String("")
                    stQuery = "SELECT TXND_CURR_NO, TXND_TO_NO, TXND_FM_NO FROM OM_TXN_DOC_RANGE WHERE TXND_COMP_CODE ='" & CompanyCode & "' AND TXND_TXN_CODE ='" & TXN_Code & "' AND TXND_LOCN_CODE ='" & Location_Code & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        TXN_CURR_NO = Convert.ToInt32(row.Item(0).ToString)
                        TXN_FM_NO = Convert.ToInt32(row.Item(2).ToString)
                        TXN_TO_NO = Convert.ToInt32(row.Item(1).ToString)
                    End If

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select nvl(max(INVH_NO),0) from ot_invoice_head where INVH_NO between " & TXN_FM_NO & " and " & TXN_TO_NO
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxInv = Convert.ToInt32(row.Item(0).ToString)
                        If maxInv = 0 Then
                            maxInv = TXN_FM_NO
                        Else
                            maxInv = maxInv + 1
                        End If
                    End If

                    stQuery = "UPDATE OM_TXN_DOC_RANGE SET TXND_CURR_NO =" & maxInv & " WHERE TXND_COMP_CODE = '" & CompanyCode & "' AND TXND_TXN_CODE = '" & TXN_Code & "' AND TXND_LOCN_CODE = '" & Location_Code & "'"
                    errLog.WriteToErrorLog("Update OM_TXN_DOC_RANGE SALESINVOICE", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE OM_TXN_DOC_RANGE SALESINVOICE  -  " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()


                    Dim headRows(26) As String
                    stQuery = "select * from OT_SO_HEAD where SOH_NO=" & Convert.ToInt32(lstviewSOSelected.Items(0).SubItems.Item(1).Text)
                    ds = db.SelectFromTableODBC(stQuery)
                    If ds.Tables("Table").Rows.Count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        headRows(0) = row.Item(0).ToString
                        headRows(1) = row.Item(6).ToString
                        headRows(2) = row.Item(22).ToString
                        headRows(3) = row.Item(23).ToString
                        headRows(4) = row.Item(24).ToString
                        headRows(5) = row.Item(27).ToString
                        headRows(6) = row.Item(65).ToString
                        headRows(7) = row.Item(33).ToString

                        headRows(8) = row.Item(84).ToString
                        headRows(9) = row.Item(85).ToString
                        headRows(10) = row.Item(86).ToString
                        headRows(11) = row.Item(87).ToString
                        headRows(12) = row.Item(88).ToString
                        headRows(13) = row.Item(89).ToString
                        headRows(14) = row.Item(90).ToString
                        headRows(15) = row.Item(91).ToString
                        headRows(16) = row.Item(92).ToString
                        headRows(17) = row.Item(93).ToString
                        headRows(18) = row.Item(94).ToString
                        headRows(19) = row.Item(95).ToString
                        headRows(20) = row.Item(96).ToString
                        headRows(21) = row.Item(97).ToString
                        headRows(22) = row.Item(98).ToString
                        headRows(23) = row.Item(61).ToString
                        Dim time As DateTime = row.Item(7).ToString
                        Dim format As String = "dd-MMM-yy"
                        headRows(24) = time.ToString(format)
                        headRows(25) = (row.Item(104).ToString).Replace("'", "''")
                        headRows(26) = row.Item(105).ToString
                    End If

                    Dim sms_shift_Code As New String("")
                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & txtSalesmanCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        sms_shift_Code = row.Item(0).ToString
                    End If

                    stQuery = "INSERT INTO OT_INVOICE_HEAD(INVH_SYS_ID,INVH_COMP_CODE,INVH_TXN_CODE,INVH_NO,INVH_DT,INVH_ACNT_YEAR,INVH_CAL_YEAR,INVH_CAL_PERIOD,INVH_DOC_SRC_LOCN_CODE,INVH_REF_FROM,INVH_REF_FROM_NUM,INVH_REF_TXN_CODE,INVH_REF_NO,INVH_REF_SYS_ID,INVH_REF_DEL_DT,INVH_LOCN_CODE,INVH_DEL_LOCN_CODE,INVH_DEL_DT,INVH_CUST_CODE,INVH_SHIP_TO_ADDR_CODE,INVH_BILL_TO_ADDR_CODE,INVH_CURR_CODE,INVH_EXGE_RATE,INVH_DISC_PERC,INVH_DISC_VAL,INVH_SM_CODE,INVH_TERM_CODE,INVH_DNTOFOLLOW_NUM,INVH_STOP_AT_INV,INVH_STATUS,INVH_PRINT_STATUS,INVH_APPR_STATUS,INVH_APPR_UID,INVH_COS_FIN_STATUS,INVH_ANNOTATION,INVH_CUST_NAME,INVH_MODE_SHIP_CODE,INVH_BILL_ADDR_LINE_1,INVH_BILL_ADDR_LINE_2,INVH_BILL_ADDR_LINE_3,INVH_BILL_ADDR_LINE_4,INVH_BILL_ADDR_LINE_5,INVH_BILL_CITY_CODE,INVH_BILL_COUNTY_CODE,INVH_BILL_STATE_CODE,INVH_BILL_POSTAL_CODE,INVH_BILL_COUNTRY_CODE,INVH_BILL_CONTACT,INVH_BILL_EMAIL,INVH_BILL_FAX,INVH_BILL_TEL,INVH_BILL_MOBILE,INVH_ADVANCE,INVH_EXC_YN,INVH_ST_CODE,INVH_EXC_INV_YN,INVH_SALE_FIN_STATUS,INVH_SUBMIT_STATUS,INVH_EL_CATG_TYPE,INVH_FLEX_03,INVH_FLEX_04,INVH_FLEX_08,INVH_FLEX_10,INVH_FLEX_15,INVH_FLEX_16,INVH_FLEX_17,INVH_FLEX_18,INVH_FLEX_19,INVH_FLEX_20,INVH_TYPE,INVH_CR_UID,INVH_CR_DT)VALUES("
                    stQuery = stQuery & maxSYS_ID & ",'" & CompanyCode & "','" & TXN_Code & "'," & maxInv & ",'" & dtpick.Value.ToString("dd-MMM-yy") & "'," & PC_Account_Year & "," & PC_CAL_Year & "," & PC_CAL_Period & ",'" & Location_Code & "','SO',11,'SO'," & headRows(1).ToString & "," & headRows(0).ToString & ",'" & headRows(24).ToString & "','" & Location_Code & "','" & Location_Code & "','" & dtpick.Value.ToString("dd-MMM-yy") & "','"
                    stQuery = stQuery & headRows(2).ToString & "','" & headRows(3).ToString & "','" & headRows(4).ToString & "','" & Currency_Code & "'," & Convert.ToInt32(Exchange_Rate.ToString) & "," & "0" & "," & "0" & ",'" & txtSalesmanCode.Text & "','" & CPT_TermCode & "',2,'Y',0,1,0,'" & LogonUser & "','','','" & headRows(6).ToString & "','" & headRows(7).ToString & "','" & headRows(8).ToString & "','" & headRows(9).ToString & "','" & headRows(10).ToString & "','" & headRows(11).ToString & "','" & headRows(12).ToString & "','" & headRows(13).ToString & "','" & headRows(14).ToString & "','" & headRows(15).ToString & "','" & headRows(16).ToString & "','" & headRows(17).ToString & "','" & headRows(18).ToString & "','" & headRows(19).ToString & "','" & headRows(20).ToString & "','" & headRows(21).ToString & "','" & headRows(22).ToString & "'," & "0" & ",'N','CASH','','',0,'','" + headRows(25).ToString + "','" + headRows(26).ToString.Replace("'", "''") + "',0,'N','','N','','','" & sms_shift_Code & "','" & POSCounterNumber & "',3,'" & LogonUser & "',current_timestamp)"
                    errLog.WriteToErrorLog("Sales Invoice Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "SALES INVOICE QUERY  -  " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()
                    INVHNO = maxInv

                    'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                    Dim taxImpl As New TaxImpl(db)

                    For i = 0 To lstviewSOSelected.Items.Count - 1
                        Dim sono As String = lstviewSOSelected.Items.Item(i).SubItems(1).Text
                        If i = 0 Then
                            salesorders = sono
                        Else
                            salesorders = salesorders & "," & sono
                        End If

                        Dim maxRefID As Integer
                        Dim refNo_SO As Integer
                        Dim refNo_SOPK As Integer
                        Dim refNo_SODATE As String = ""
                        stQuery = " SELECT INVR_SYS_ID.NEXTVAL FROM DUAL"
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        If count > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            maxRefID = Convert.ToInt32(row.Item(0).ToString)
                        End If

                        stQuery = "select SOH_NO,SOH_SYS_ID,SOH_DT from ot_so_head where soh_no=" & sono
                        ds = db.SelectFromTableODBC(stQuery)
                        If ds.Tables("Table").Rows.Count > 0 Then
                            refNo_SO = Convert.ToInt32(ds.Tables("Table").Rows.Item(0).Item(0).ToString)
                            refNo_SOPK = Convert.ToInt32(ds.Tables("Table").Rows.Item(0).Item(1).ToString)
                            Dim time As DateTime = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                            Dim format As String = "dd-MMM-yy"
                            refNo_SODATE = time.ToString(format)
                        End If

                        stQuery = "INSERT INTO OT_INVOICE_REF(INVR_SYS_ID,INVR_INVH_SYS_ID,INVR_REF_TXN_CODE,INVR_REF_NO,INVR_REF_SYS_ID,INVR_REF_DEL_DT,INVR_CR_DT,INVR_CR_UID)VALUES("
                        stQuery = stQuery & maxRefID & "," & maxSYS_ID & ",'SO'," & refNo_SO & "," & refNo_SOPK & ",'" & refNo_SODATE & "',to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("Sales Invoice REF query", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "SALES INVOICE REF QUERY  -  " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        Dim soCount As Integer
                        stQuery = "select OT_SO_ITEM.* from OT_SO_HEAD,OT_SO_ITEM WHERE SOH_SYS_ID=SOI_SOH_SYS_ID AND SOH_NO=" & sono
                        ds = db.SelectFromTableODBC(stQuery)
                        soCount = ds.Tables("Table").Rows.Count
                        Dim k As Integer = 0
                        While soCount > 0
                            row = ds.Tables("Table").Rows.Item(k)
                            'MsgBox(row.Item(0).ToString)
                            Dim maxSOISYSID As Integer
                            Dim ds1 As DataSet
                            'stQuery = "SELECT INVI_SYS_ID.NEXTVAL FROM DUAL"
                            stQuery = "SELECT INVI_SYS_ID.NEXTVAL FROM DUAL"
                            ds1 = db.SelectFromTableODBC(stQuery)
                            If ds1.Tables("Table").Rows.Count > 0 Then
                                maxSOISYSID = Convert.ToInt32(ds1.Tables("Table").Rows.Item(0).Item(0).ToString)
                                'maxSOISYSID = maxSOISYSID + 1
                            End If

                            'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                            Dim itemCodeForTaxEntry As String = ""
                            Dim itemPriceForTaxEntry As Double = row.Item(45)
                            itemCodeForTaxEntry = row.Item(15)
                            Dim discountAmt As Double = 0

                            stQuery = "INSERT INTO OT_INVOICE_ITEM(INVI_SYS_ID,INVI_INVH_SYS_ID,INVI_INVR_SYS_ID,INVI_SOI_SYS_ID,INVI_LOCN_CODE,INVI_DEL_LOCN_CODE,INVI_ITEM_CODE,INVI_ITEM_STK_YN_NUM,INVI_ITEM_DESC,INVI_UOM_CODE,INVI_SM_CODE,INVI_PL_CODE,INVI_PL_RATE,INVI_UPD_STK_YN,INVI_QTY,INVI_QTY_LS,INVI_QTY_BU,INVI_RESV_QTY,INVI_RESV_QTY_LS,INVI_RESV_QTY_BU,INVI_PII_QTY_BU,INVI_PAI_QTY_BU,INVI_DNI_QTY_BU,INVI_CSRI_QTY_BU,INVI_LOCN_GRP_RESV_UTIL_BU,INVI_LOCN_RESV_UTIL_BU,INVI_RATE,INVI_DISC_PERC,INVI_FC_VAL,INVI_FC_VAL_AFT_H_DISC,INVI_FOC_YN_NUM,INVI_FOC_YN,INVI_DUTY_PAID_YN,INVI_ASSESS_RATE,INVI_ASSESS_VAL,INVI_GRADE_CODE_1,INVI_GRADE_CODE_2,INVI_CENVAT_IND,INVI_FC_DISC_VAL,INVI_FLEX_16,INVI_FLEX_17,INVI_FLEX_18,INVI_FLEX_19,INVI_FLEX_20,INVI_CR_UID,INVI_CR_DT, INVI_FC_ACT_VAL)VALUES("
                            stQuery = stQuery & maxSOISYSID & "," & maxSYS_ID & "," & maxRefID & "," & row.Item(0).ToString & ",'" & Location_Code & "','" & Location_Code & "','" & row.Item(15).ToString & "',1,'" & row.Item(17).ToString & "','" & row.Item(18).ToString & "','" & txtSalesmanCode.Text & "','" & row.Item(19).ToString & "','" & row.Item(20).ToString & "','Y'," & Convert.ToInt32(row.Item(25).ToString) & "," & Convert.ToInt32(row.Item(26).ToString) & "," & Convert.ToInt32(row.Item(27).ToString) & "," & Convert.ToInt32(row.Item(38).ToString) & ","
                            stQuery = stQuery & Convert.ToInt32(row.Item(39).ToString) & "," & Convert.ToInt32(row.Item(40).ToString) & "," & Convert.ToInt32(row.Item(30).ToString) & "," & Convert.ToInt32(row.Item(31).ToString) & "," & Convert.ToInt32(row.Item(32).ToString) & ",0,0,0," & Convert.ToDouble(row.Item(36).ToString) & "," & Convert.ToDouble(row.Item(37).ToString) & "," & Convert.ToDouble(row.Item(45).ToString) & "," & Convert.ToDouble(row.Item(46).ToString) & ",2,'N','N'," & Convert.ToInt32(row.Item(54).ToString) & "," & Convert.ToInt32(row.Item(55).ToString) & ",'" & row.Item(56).ToString & "','" & row.Item(57).ToString & "',0," & Convert.ToDouble(row.Item(63).ToString) & ",'','','" & row.Item(84).ToString & "','','','" & LogonUser & "',current_timestamp," & row.Item(45).ToString & ")"
                            errLog.WriteToErrorLog("Sales Invoice ITEM QUERY", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "SALES INVOICE ITEM QUERY  -  " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()

                            'stQuery = New String("")
                            'Dim maxLedgerID As Integer
                            Dim ds2 As New DataSet

                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDDIS_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDDIS_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If

                            stQuery = "select ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT, ITED_SYS_ID from OT_SO_ITEM_TED where ITED_I_SYS_ID=" + row.Item(0).ToString + " and ITED_TED_TYPE_NUM=" + TEDDIS_NUM
                            ds2 = db.SelectFromTableODBC(stQuery)
                            Dim rowval As System.Data.DataRow
                            If ds2.Tables("Table").Rows.Count > 0 Then
                                rowval = ds2.Tables("Table").Rows.Item(0)
                                discountAmt = Convert.ToDouble(rowval(9).ToString)

                                stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT, ITED_SOH_SYS_ID,ITED_SOI_SYS_ID,ITED_ITED_SYS_ID, ITED_TED_TYPE_CODE,ITED_TED_INCL_EXCL_FLAG, ITED_TXN_UOM, ITED_TXN_QTY, ITED_USER_QTY, ITED_SET_OFF_PERC, ITED_SET_OFF_FC_AMT, ITED_SET_OFF_LC_AMT, ITED_UNROUND_FC_AMT, ITED_UNROUND_LC_AMT, ITED_UPDATED_YN_NUM )VALUES("
                                stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOISYSID & ",'" & rowval(0).ToString & "'," + TEDDIS_NUM + ",'2','','" + Currency_Code + "','" + Currency_Code + "'," + "0" + "," + rowval(7).ToString + "," + rowval(8).ToString + "," + rowval(9).ToString + "," + rowval(10).ToString + "," + rowval(11).ToString + "," + rowval(12).ToString + ",'" + LogonUser + "',sysdate," & headRows(0) & "," & row.Item(0).ToString & "," & rowval.Item(13).ToString & ", 'TEDDIS','E','" & row.Item(18).ToString & "'," & row.Item(16).ToString & "," & row.Item(16).ToString & ",0,0,0," & rowval.Item(9).ToString & "," & rowval.Item(9).ToString & ",1)"
                                errLog.WriteToErrorLog("QUERY INSERT ITEM TEDDIS", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDDIS" & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            End If

                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TAX'"
                            Dim TEDTAX_NUM As String = ""
                            ds2 = db.SelectFromTableODBC(stQuery)
                            If ds2.Tables("Table").Rows.Count > 0 Then
                                TEDTAX_NUM = ds2.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If

                            stQuery = "select ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT, ITED_SYS_ID from OT_SO_ITEM_TED where ITED_I_SYS_ID=" + row.Item(0).ToString + " and ITED_TED_TYPE_NUM=" + TEDTAX_NUM
                            ds2 = db.SelectFromTableODBC(stQuery)
                            If ds2.Tables("Table").Rows.Count > 0 Then
                                rowval = ds2.Tables("Table").Rows.Item(0)
                                stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT, ITED_SOH_SYS_ID,ITED_SOI_SYS_ID,ITED_ITED_SYS_ID, ITED_TED_TYPE_CODE,ITED_TED_INCL_EXCL_FLAG, ITED_TXN_UOM, ITED_TXN_QTY, ITED_USER_QTY, ITED_SET_OFF_PERC, ITED_SET_OFF_FC_AMT, ITED_SET_OFF_LC_AMT, ITED_UNROUND_FC_AMT, ITED_UNROUND_LC_AMT, ITED_UPDATED_YN_NUM)VALUES("
                                stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOISYSID & ",'" & rowval(0).ToString & "'," + TEDTAX_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," & rowval(6).ToString & "," + rowval(7).ToString + "," + rowval(8).ToString + "," + rowval(9).ToString + "," + rowval(10).ToString + "," + rowval(11).ToString + "," + rowval(12).ToString + ",'" + LogonUser + "',sysdate," & headRows(0) & "," & row.Item(0).ToString & "," & rowval.Item(13).ToString & ", 'TAX','E','" & row.Item(18).ToString & "'," & row.Item(16).ToString & "," & row.Item(16).ToString & ",0,0,0," & rowval.Item(9).ToString & "," & rowval.Item(9).ToString & ",2)"
                                errLog.WriteToErrorLog("QUERY INSERT ITEM TAX", stQuery, "")
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            End If

                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP'"
                            dsTED = db.SelectFromTableODBC(stQuery)
                            Dim TEDEXP_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDEXP_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If

                            stQuery = "select ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TAXABLE_FC_AMT,ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID=" + row.Item(0).ToString + " and ITED_TED_TYPE_NUM=" + TEDEXP_NUM
                            ds2 = db.SelectFromTableODBC(stQuery)

                            If ds2.Tables("Table").Rows.Count > 0 Then
                                rowval = ds2.Tables("Table").Rows.Item(0)
                                stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TAXABLE_FC_AMT,ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                                stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOISYSID & ",'" & rowval(0).ToString & "'," + TEDEXP_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + rowval(6).ToString + "," + rowval(7).ToString + "," + rowval(8).ToString + "," + rowval(9).ToString + "," + rowval(10).ToString + "," + rowval(11).ToString + ",'" + LogonUser + "',sysdate)"
                                errLog.WriteToErrorLog("QUERY INSERT ITEM TEDEXP", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDEXP  -  " & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            End If

                            k = k + 1
                            soCount = soCount - 1
                        End While
                        Dim stQueryAdvPayment As String
                        stQueryAdvPayment = "Select PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID, PINVP_LC_VAL from OT_POS_SO_PAYMENT, OT_SO_HEAD  where PINVP_INVH_SYS_ID = SOH_SYS_ID AND SOH_NO=" + sono
                        Dim dsAdvPay As DataSet
                        dsAdvPay = db.SelectFromTableODBC(stQueryAdvPayment)
                        Dim countAdvPay As Integer = dsAdvPay.Tables("Table").Rows.Count
                        Dim advpay As Integer = 0
                        Dim rowadvpay As System.Data.DataRow
                        While countAdvPay > 0
                            rowadvpay = dsAdvPay.Tables("Table").Rows.Item(advpay)
                            Dim maxPINVP_SYS_ID As Long
                            stQueryAdvPayment = "select pos_invp_sys_id.nextval from dual"
                            Dim dsMAX As DataSet = db.SelectFromTableODBC(stQueryAdvPayment)
                            Dim count1 As Integer
                            count1 = dsMAX.Tables("Table").Rows.Count
                            If count1 > 0 Then
                                row = dsMAX.Tables("Table").Rows.Item(0)
                                maxPINVP_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                                'maxPINVP_SYS_ID = maxPINVP_SYS_ID + 1
                            End If

                            stQueryAdvPayment = "insert into ot_pos_invoice_payment(PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID,PINVP_LC_VAL,PINVP_FLEX_19)values("
                            stQueryAdvPayment = stQueryAdvPayment + maxPINVP_SYS_ID.ToString & "," & maxSYS_ID.ToString & ",'" & rowadvpay(2).ToString & "','" & rowadvpay(3).ToString & "','" & rowadvpay(4).ToString & "'," & rowadvpay(5).ToString & ",'" & rowadvpay(6).ToString & "',sysdate,'" & LogonUser & "'," & rowadvpay(5).ToString & ",'ADVANCE')"
                            errLog.WriteToErrorLog("Sales Invoice Advance PAYMENT Query", stQueryAdvPayment, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "Sales Invoice Advance PAYMENT Query  -  " & stQueryAdvPayment)
                            command.CommandText = stQueryAdvPayment
                            command.ExecuteNonQuery()

                            If rowadvpay(6).ToString = "GV" Then
                                stQuery = "INSERT INTO OT_POS_INVOICE_GV(PINVGV_SYS_ID,PINVGV_INVH_SYS_ID,PINVGV_PINVP_SYS_ID,PINVGV_GVNO,PINVGV_GV_CURR_CODE,PINVGV_GV_VALUE,PINVGV_CR_DT,PINVGV_CR_UID,PINVGV_GVCODE)VALUES("
                                stQuery = stQuery + "PINVGV_SYS_ID.NEXTVAL," + maxSYS_ID.ToString + "," + maxPINVP_SYS_ID.ToString + ",'" + "" + "','" + rowadvpay(4).ToString + "'," + rowadvpay(5).ToString + ",sysdate,'" + LogonUser + "','" + rowadvpay(2).ToString + "')"
                                errLog.WriteToErrorLog("GIFT VOUCHER QUERY", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, "GIFT VOUCHER QUERY  -  " & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            End If

                            advpay = advpay + 1
                            countAdvPay = countAdvPay - 1
                        End While
                    Next

                    Dim listPayment As New List(Of String())
                    i = 0
                    listPayment = cardpay.GetPaymentItems()

                    count = listPayment.Count

                    If count > 0 Then
                        Dim rowPayment(8) As String
                        While count > 0
                            stQuery = New String("")
                            Dim maxPINVP_SYS_ID As Long
                            ds = New DataSet
                            stQuery = "select pos_invp_sys_id.nextval from dual"
                            ds = db.SelectFromTableODBC(stQuery)
                            Dim count1 As Integer
                            count1 = ds.Tables("Table").Rows.Count
                            If count1 > 0 Then
                                row = ds.Tables("Table").Rows.Item(0)
                                maxPINVP_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                                'maxPINVP_SYS_ID = maxPINVP_SYS_ID + 1
                            End If

                            stQuery = New String("")
                            rowPayment = listPayment.Item(i)

                            If Not rowPayment(8) = "0" Then
                                rowPayment(3) = Convert.ToDouble(rowPayment(3).ToString) - Convert.ToDouble(rowPayment(8).ToString)
                            End If
                            'PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_LC_VAL,PINVP_FLEX_19,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID
                            stQuery = "insert into ot_pos_invoice_payment(PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID,PINVP_LC_VAL,PINVP_FLEX_19)values("
                            stQuery = stQuery & maxPINVP_SYS_ID & "," & maxSYS_ID & ",'" & rowPayment(1).ToString & "','" & rowPayment(6).ToString & "','" & rowPayment(2).ToString & "'," & Convert.ToDouble(rowPayment(3).ToString) & ",'" & rowPayment(7).ToString & "',sysdate,'" & LogonUser & "'," & Convert.ToDouble(rowPayment(3).ToString) & ",'" + "" + "')"
                            errLog.WriteToErrorLog("Sales Invoice PAYMENT Query", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "Sales Invoice PAYMENT Query  -  " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()

                            i = i + 1
                            count = count - 1
                        End While
                    End If


                    For i = 0 To lstRCDetail.Items.Count - 1
                        Dim stQueryRef As String
                        'stQueryRef = "insert into OT_INVOICE_ROYALTY_CARD(INVRC_SYS_ID,INVRC_INVH_SYS_ID, INVRC_COMP_CODE, INVRC_LOCN_CODE, INVRC_INVH_TXN_CODE,INVRC_INVH_NO, INVRC_ROYALTY_CARD,INVRC_ROYALTY_NO, INVRC_CR_UID,INVRC_CR_DT)values("
                        stQueryRef = "INSERT INTO OT_INVOICE_LOYALTY_CARD(INVLC_SYS_ID,INVLC_INVH_SYS_ID,INVLC_COMP_CODE,INVLC_LOCN_CODE,INVLC_INVH_TXN_CODE,INVLC_INVH_NO,INVLC_LOYALTY_CARD,INVLC_LOYALTY_NO,INVLC_CR_UID,INVLC_CR_DT)VALUES("
                        stQueryRef = stQueryRef & "INVLC_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstRCDetail.Items(i).SubItems(1).Text & "','" & lstRCDetail.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_LOYALTY_CARD", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_LOYALTY_CARD  -  " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewReferal.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into OT_INVOICE_REF_HOSPITAL(INVRH_SYS_ID,INVRH_INVH_SYS_ID, INVRH_COMP_CODE, INVRH_LOCN_CODE, INVRH_INVH_TXN_CODE,INVRH_INVH_NO, INVRH_REF_HOSPITAL_CODE,INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, INVRH_CR_UID,INVRH_CR_DT)values("
                        stQueryRef = stQueryRef & "INVRH_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & lstviewReferal.Items(i).SubItems(4).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_REF_HOSPITAL", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_REF_HOSPITAL  -  " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewInsurance.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into PERF_OT_INVOICE_INSURANCE(INVRI_INVH_SYS_ID, INVRI_COMP_CODE, INVRI_LOCN_CODE, INVRI_INVH_TXN_CODE,INVRI_INVH_NO, INVR1_INSURANCE_CODE,INVR1_INSURANCE_NO, INVRI_CR_UID,INVRI_CR_DT)values("
                        stQueryRef = stQueryRef & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_REF_HOSPITAL", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_REF_HOSPITAL  -  " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    transaction.Commit()
                    lblTransLoader.SendToBack()
                    MsgBox("Sales Invoice transaction completed Successfully!")
                    Return True
                Catch ex As Exception
                    qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                    transaction.Rollback()
                    Return False
                End Try
            End Using

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
            Return False
        End Try
    End Function

    Private Function SalesOrderDone() As Boolean
        Try
            'Using connection As New OdbcConnection(db.GetConnectionString(""))
            Using connection As New OracleConnection(db.GetConnectionString(""))
                Dim command As New OracleCommand
                'Dim command As New OdbcCommand()
                Dim transaction As OracleTransaction
                'Dim transaction As OdbcTransaction
                command.Connection = connection
                Try
                    connection.Open()
                    transaction = connection.BeginTransaction()

                    command.Connection = connection
                    command.Transaction = transaction

                    Dim ds As DataSet
                    Dim i As Integer = 0
                    Dim stQuery As String
                    Dim count As Integer
                    'Dim maxInv As Integer
                    Dim maxSYS_ID As Integer
                    Dim row As System.Data.DataRow
                    'Dim TXN_FM_NO As Integer
                    'Dim TXN_CURR_NO As Integer
                    'Dim TXN_TO_NO As Integer
                    Dim advanceamount As Double = 0
                    Dim success As Integer = 1
                    Dim SOH_FLEX_03_Val As String = ""

                    Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
                    txtSalesmanCode.Text = strSalesArr(0)

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "SELECT SOH_SYS_ID.NEXTVAL FROM DUAL"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxSYS_ID = Convert.ToInt32(row.Item(0).ToString)
                        'maxSYS_ID = maxSYS_ID + 1
                    End If

                    Dim sms_shift_Code As New String("")
                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & txtSalesmanCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        sms_shift_Code = row.Item(0).ToString
                    End If

                    stQuery = New String("")
                    stQuery = "INSERT INTO OT_SO_HEAD(SOH_SYS_ID,SOH_COMP_CODE,SOH_LOCN_CODE,SOH_TXN_CODE,SOH_NO,SOH_DT,SOH_DOC_SRC_LOCN_CODE,SOH_AMD_DT,SOH_REF_FROM,SOH_REF_FROM_NUM,SOH_DEL_DT,SOH_DEL_LOCN_CODE,SOH_CUST_CODE,SOH_SHIP_TO_ADDR_CODE,SOH_BILL_TO_ADDR_CODE,SOH_CURR_CODE,SOH_EXGE_RATE,SOH_DISC_PERC,SOH_DISC_VAL,SOH_SM_CODE,SOH_TERM_CODE,SOH_RESVATCL_NUM,SOH_MODE_SHIP_CODE,SOH_STATUS,SOH_PRINT_STATUS,SOH_APPR_STATUS,SOH_APPR_UID,SOH_CLO_STATUS,SOH_PARTSHIP_YN,SOH_ALLOCATE_YN,SOH_ANNOTATION,SOH_ADVANCE,SOH_ST_CODE,SOH_SUBMIT_STATUS,SOH_CUST_NAME,SOH_BILL_ADDR_LINE_1,SOH_BILL_ADDR_LINE_2,SOH_BILL_ADDR_LINE_3,SOH_BILL_ADDR_LINE_4,SOH_BILL_ADDR_LINE_5,SOH_BILL_CITY_CODE,SOH_BILL_COUNTY_CODE,SOH_BILL_STATE_CODE,SOH_BILL_POSTAL_CODE,SOH_BILL_COUNTRY_CODE,SOH_BILL_CONTACT,SOH_BILL_EMAIL,SOH_BILL_FAX,SOH_BILL_TEL,SOH_BILL_MOBILE,SOH_FLEX_01,SOH_FLEX_03,SOH_FLEX_04,SOH_FLEX_08,SOH_FLEX_10,SOH_FLEX_15,SOH_FLEX_16,SOH_FLEX_17,SOH_FLEX_18,SOH_FLEX_19,SOH_FLEX_20,SOH_CR_UID,SOH_CR_DT,SOH_BLANKET_SO_YN)VALUES("
                    stQuery = stQuery & maxSYS_ID & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & dtpick.Value.ToString("dd-MMM-yy") & "','" & Location_Code & "','" & dtpick.Value.ToString("dd-MMM-yy") & "','D',1,'" & dtpick.Value.ToString("dd-MMM-yy") & "','" & Location_Code & "','" & custDetails(0) & "','" & custDetails(3) & "','" & custDetails(2) & "','" & Currency_Code & "'," & Exchange_Rate & ",0," & "0" & ",'" & txtSalesmanCode.Text & "','" & CPT_TermCode & "',2,'RD',0,1,0,'" & LogonUser & "','','Y','N','" + "" + "'," & advanceamount & ",'CASH',0,'" & custDetails(1) & "','" & custDetails(4) & "','" & custDetails(5) & "','" & custDetails(30) & "','" & custDetails(31) & "','" & custDetails(32) & "','" & custDetails(6) & "','" & custDetails(7) & "','" & custDetails(8) & "','" & custDetails(9) & "','" & custDetails(11) & "','" & custDetails(12) & "','" & custDetails(13) & "','" & custDetails(14) & "','" & custDetails(15) & "','" & custDetails(16) & "','" & "" & "','" & txtPatientNo.Text & "','" & txtPatPatientName.Text.Replace("'", "''") & "','0','N','','','','','" & sms_shift_Code & "','" & POSCounterNumber & "','" & LogonUser & "',current_timestamp,'N')"
                    errLog.WriteToErrorLog("Sales Order Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "Sales Order Query  -  " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    stQuery = "insert into CRM_INV_CAMP(INV_CAMP_CODE,INV_CAMP_INVHNO,INV_CAMP_COMP_CODE,INV_CAMP_LOCN_CODE,INV_CAMP_SM_CODE,INV_CAMP_CR_UID,INV_CAMP_CR_DT)values("
                    stQuery = stQuery & "'" & txtCampaignCode.Text & "','" & SOHNO & "','" & CompanyCode & "','" & Location_Code & "','" & txtSalesmanCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                    errLog.WriteToErrorLog("INSERT QUERY CRM_CAMPAIGN", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY CRM_CAMPAIGN  -  " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                    Dim taxImpl As New TaxImpl(db)

                    Dim dsAll As DataSet
                    stQuery = "select PROD_SOI_SOH_SYS_ID,PRODCODE,PRODDESC,PRODITEMUOM,PRODQTY,PRODPRICE,PRODBARCODE,PRODRATE,PRODMINRATE,PRODDISCPER,PRODDISCAMT,PRODIGCODE,PRODGRADECODE_1,PRODGRADECODE_2,PRODPLCODE,PRODPLRATE,PRODBATCHNO,PRODSRNO,PRODRI,PRODDISCCODE,PRODVATCODE,PRODVATAMT,PRODEXPCODE,PRODEXPAMT,PRODBONUSCODE,PRODREASONCODE,PRODFOCITEM,PRODWARRANTYDT,PRODWARRANTYNO,PRODSTKITEM,PRODLOCNCODE,PRODSTOCKRESERV,PRODAFTHEADERDISC,PRODTAXABLEAMT,PRODUSERID,PRODITEMSTATUS,PRODWARRANTYTYPE,PRODWARRANTYDAYS from OT_POS_SO_ITEM_LOG where PROD_SOI_SOH_SYS_ID = " + SOHSYSID.ToString
                    dsAll = db.SelectFromTableODBC(stQuery)
                    Dim countval = dsAll.Tables("Table").Rows.Count
                    i = 0
                    While countval > 0
                        Dim rowItem(24) As String
                        rowItem(0) = dsAll.Tables("Table").Rows.Item(i).Item(0).ToString
                        rowItem(1) = dsAll.Tables("Table").Rows.Item(i).Item(1).ToString
                        rowItem(2) = dsAll.Tables("Table").Rows.Item(i).Item(2).ToString
                        rowItem(10) = dsAll.Tables("Table").Rows.Item(i).Item(3).ToString
                        rowItem(9) = dsAll.Tables("Table").Rows.Item(i).Item(7).ToString
                        rowItem(4) = dsAll.Tables("Table").Rows.Item(i).Item(15).ToString
                        rowItem(3) = dsAll.Tables("Table").Rows.Item(i).Item(4).ToString
                        rowItem(11) = dsAll.Tables("Table").Rows.Item(i).Item(9).ToString
                        rowItem(5) = (Convert.ToInt64(rowItem(3).ToString) * Convert.ToDouble(rowItem(9).ToString)).ToString
                        rowItem(8) = dsAll.Tables("Table").Rows.Item(i).Item(5).ToString
                        rowItem(12) = dsAll.Tables("Table").Rows.Item(i).Item(12).ToString
                        rowItem(13) = dsAll.Tables("Table").Rows.Item(i).Item(13).ToString
                        rowItem(14) = dsAll.Tables("Table").Rows.Item(i).Item(14).ToString
                        rowItem(6) = dsAll.Tables("Table").Rows.Item(i).Item(10).ToString
                        rowItem(7) = dsAll.Tables("Table").Rows.Item(i).Item(6).ToString
                        rowItem(19) = dsAll.Tables("Table").Rows.Item(i).Item(19).ToString
                        rowItem(22) = dsAll.Tables("Table").Rows.Item(i).Item(22).ToString
                        rowItem(23) = dsAll.Tables("Table").Rows.Item(i).Item(23).ToString

                        Dim maxSOI_SYS_ID As Integer
                        Dim count1 As Integer
                        stQuery = New String("")
                        ds = New DataSet
                        stQuery = "SELECT SOI_SYS_ID.NEXTVAL FROM DUAL"
                        ds = db.SelectFromTableODBC(stQuery)
                        count1 = ds.Tables("Table").Rows.Count
                        If count1 > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            maxSOI_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                            'maxSOI_SYS_ID = maxSOI_SYS_ID + 1
                        End If

                        'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                        Dim itemCodeForTaxEntry As String = ""
                        Dim itemPriceForTaxEntry As Double = rowItem(8)
                        itemCodeForTaxEntry = rowItem(1).ToString

                        'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                        Dim taxCode As String = taxImpl.getLocationTaxCodeForItem(Location_Code, itemCodeForTaxEntry)
                        Dim taxPercentage As Double = taxImpl.getTaxPercentageofItem(itemCodeForTaxEntry, Location_Code, TXN_Code, taxCode)
                        Dim taxValueOfItem As Double = Round(taxImpl.calculateTaxValueofItem(itemCodeForTaxEntry, itemPriceForTaxEntry, Location_Code, TXN_Code, taxCode, taxPercentage), 3)
                        'taxValueOfItem = taxValueOfItem

                        stQuery = New String("")

                        stQuery = "INSERT INTO OT_SO_ITEM(SOI_SYS_ID,SOI_SOH_SYS_ID,SOI_COMP_CODE,SOI_LOCN_CODE,SOI_DEL_LOCN_CODE,SOI_SM_CODE,SOI_ITEM_CODE,SOI_ITEM_STK_YN_NUM,SOI_ITEM_DESC,SOI_UOM_CODE,SOI_PL_CODE,SOI_PL_RATE,SOI_QTY,SOI_QTY_LS,SOI_QTY_BU,SOI_PI_QTY_BU,SOI_GI_QTY_BU,SOI_PII_QTY_BU,SOI_PAI_QTY_BU,SOI_DNI_QTY_BU,SOI_INVI_DN_QTY_BU,SOI_INVI_QTY_BU,SOI_WOH_QTY_BU,SOI_RATE,SOI_DISC_PERC,SOI_GI_RESV_QTY_BU,SOI_PSI_QTY_BU,SOI_PPI_QTY_BU,SOI_OVERRES_QTY_BU,SOI_FC_VAL,SOI_FC_VAL_AFT_H_DISC,SOI_FC_TAX_AMT,SOI_FOC_YN,SOI_ASSESS_RATE,SOI_ASSESS_VAL,SOI_GRADE_CODE_1,SOI_GRADE_CODE_2,SOI_FC_DISC_VAL,SOI_FLEX_15,SOI_FLEX_16,SOI_FLEX_17,SOI_FLEX_18,SOI_FLEX_19,SOI_FLEX_20,SOI_CR_UID,SOI_CR_DT)VALUES("
                        stQuery = stQuery & maxSOI_SYS_ID & "," & maxSYS_ID & ",'" & CompanyCode & "','" & Location_Code & "','" & Location_Code & "','" & txtSalesmanCode.Text & "','" & rowItem(1).ToString & "',1,'" & rowItem(2).ToString & "','" & rowItem(10).ToString & "','" & rowItem(14).ToString & "'," & Convert.ToDouble(rowItem(4).ToString) & "," & Convert.ToInt32(rowItem(3).ToString) & ",0," & Convert.ToInt32(rowItem(3).ToString) & ",0,0,0,0,0,0,0,0," & rowItem(4).ToString & "," & "0" & ",0,0,0,0," & Convert.ToDouble(rowItem(5).ToString) - Convert.ToDouble(rowItem(6).ToString) - taxValueOfItem & "," & Convert.ToDouble(rowItem(5).ToString) & ",0,'N',0,0,'" & rowItem(12).ToString & "','" & rowItem(13).ToString & "'," & rowItem(6).ToString & ",'+','','','" & rowItem(7).ToString & "','','','" & LogonUser & "',current_timestamp)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_SO_ITEM", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_SO_ITEM  -  " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        stQuery = "DELETE FROM OT_POS_SO_ITEM_LOG WHERE PRODITEMSTATUS='I' and PROD_SOI_SOH_SYS_ID=" & Convert.ToInt32(rowItem(0).ToString) & " and PRODCODE='" & rowItem(1).ToString & "'"
                        errLog.WriteToErrorLog("OT_SO_ITEM LOG DELETE", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "OT_SO_ITEM LOG DELETE  - " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        stQuery = New String("")
                        Dim maxLedgerID As Integer
                        ds = New DataSet
                        stQuery = "select soid_sys_id.nextval from dual"
                        ds = db.SelectFromTableODBC(stQuery)
                        count1 = ds.Tables("Table").Rows.Count
                        If count1 > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            maxLedgerID = Convert.ToInt32(row.Item(0).ToString)
                            'maxLedgerID = maxLedgerID + 1
                        End If

                        stQuery = New String("")
                        stQuery = "INSERT INTO OT_SO_ITEM_DEL(SOID_SYS_ID,SOID_SOI_SYS_ID,SOID_DEL_DT,SOID_DEL_QTY,SOID_DEL_QTY_LS,SOID_DEL_QTY_BU,SOID_CR_DT,SOID_CR_UID)VALUES("
                        stQuery = stQuery & maxLedgerID & "," & maxSOI_SYS_ID & ",to_date(sysdate,'DD-MM-YY')," & Convert.ToInt32(rowItem(3).ToString) & ",0," & Convert.ToInt32(rowItem(3).ToString) & ",to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("SO ITEM DEL", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "SO ITEM DEL  - " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        If Not taxValueOfItem.Equals(0) Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TAX'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDTAX_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDTAX_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_SO_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & taxCode & "'," & TEDTAX_NUM & ",'2','R','" & Currency_Code & "','" & Currency_Code & "'," & taxPercentage & "," & itemPriceForTaxEntry - taxValueOfItem & "," & itemPriceForTaxEntry - taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & ",'" & LogonUser & "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TAX", stQuery, "")
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        If Not rowItem(19).ToString = "" Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDDIS_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDDIS_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_SO_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & rowItem(19).ToString & "'," + TEDDIS_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + "0" + "," + rowItem(9).ToString + "," + rowItem(9).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + ",'" + LogonUser + "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDDIS", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDIS  - " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        If Not rowItem(23).ToString = "0" Or rowItem(23).ToString = "" Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDEXP_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDEXP_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_SO_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TAXABLE_FC_AMT,ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & rowItem(22).ToString & "'," + TEDEXP_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + rowItem(9).ToString + "," + "0" + "," + rowItem(23).ToString + "," + rowItem(23).ToString + "," + rowItem(23).ToString + "," + rowItem(23).ToString + ",'" + LogonUser + "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDEXP", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM TEDEXP  - " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        i = i + 1
                        countval = countval - 1
                    End While

                    Dim listPayment As New List(Of String())
                    i = 0
                    listPayment = cardpay.GetPaymentItems()

                    count = listPayment.Count

                    If count > 0 Then
                        Dim rowPayment(8) As String
                        While count > 0
                            stQuery = New String("")
                            Dim maxPINVP_SYS_ID As Long
                            ds = New DataSet
                            stQuery = "SELECT POS_INVP_SYS_ID.NEXTVAL FROM DUAL"
                            ds = db.SelectFromTableODBC(stQuery)
                            Dim count1 As Integer
                            count1 = ds.Tables("Table").Rows.Count
                            If count1 > 0 Then
                                row = ds.Tables("Table").Rows.Item(0)
                                maxPINVP_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                                'maxPINVP_SYS_ID = maxPINVP_SYS_ID + 1
                            End If

                            stQuery = New String("")
                            rowPayment = listPayment.Item(i)

                            If Not rowPayment(8) = "0" Then
                                rowPayment(3) = Convert.ToDouble(rowPayment(3).ToString) - Convert.ToDouble(rowPayment(8).ToString)
                            End If

                            advanceamount = advanceamount + Convert.ToDouble(rowPayment(3).ToString)
                            stQuery = "insert into OT_POS_SO_PAYMENT(PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID,PINVP_LC_VAL)values("
                            stQuery = stQuery & maxPINVP_SYS_ID & "," & maxSYS_ID & ",'" & rowPayment(1).ToString & "','" & rowPayment(6).ToString & "','" & rowPayment(2).ToString & "'," & Convert.ToDouble(rowPayment(3).ToString) & ",'" & rowPayment(7).ToString & "',current_timestamp,'" & LogonUser & "'," & Convert.ToDouble(rowPayment(3).ToString) & ")"
                            errLog.WriteToErrorLog("QUERY OT_POS_SO_PAYMENT", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY OT_POS_SO_PAYMENT  - " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                            SOH_FLEX_03_Val = SOH_FLEX_03_Val & rowPayment(1).ToString & "-"
                            i = i + 1
                            count = count - 1
                        End While
                    End If


                    For i = 0 To lstRCDetail.Items.Count - 1
                        Dim stQueryRef As String
                        'stQueryRef = "insert into OT_INVOICE_ROYALTY_CARD(INVRC_SYS_ID,INVRC_INVH_SYS_ID, INVRC_COMP_CODE, INVRC_LOCN_CODE, INVRC_INVH_TXN_CODE,INVRC_INVH_NO, INVRC_ROYALTY_CARD,INVRC_ROYALTY_NO, INVRC_CR_UID,INVRC_CR_DT)values("
                        stQueryRef = "INSERT INTO OT_INVOICE_LOYALTY_CARD(INVLC_SYS_ID,INVLC_SOH_SYS_ID,INVLC_COMP_CODE,INVLC_LOCN_CODE,INVLC_SOH_TXN_CODE,INVLC_SOH_NO,INVLC_LOYALTY_CARD,INVLC_LOYALTY_NO,INVLC_CR_UID,INVLC_CR_DT)VALUES("
                        stQueryRef = stQueryRef & "INVLC_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & lstRCDetail.Items(i).SubItems(1).Text & "','" & lstRCDetail.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_ROYALTY_CARD", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_ROYALTY_CARD  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewReferal.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into OT_INVOICE_REF_HOSPITAL(INVRH_SYS_ID,INVRH_SOH_SYS_ID, INVRH_COMP_CODE, INVRH_LOCN_CODE,  INVRH_SOH_TXN_CODE,INVRH_SOH_NO, INVRH_REF_HOSPITAL_CODE,INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, INVRH_CR_UID,INVRH_CR_DT)values("
                        stQueryRef = stQueryRef & "INVRH_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & lstviewReferal.Items(i).SubItems(4).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_REF_HOSPITAL", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_REF_HOSPITAL  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To GVListView.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into OT_INVOICE_GIFT_VOUCHER(INVGV_SYS_ID,INVGV_SOH_SYS_ID, INVGV_COMP_CODE, INVGV_LOCN_CODE,  INVGV_SOH_TXN_CODE,INVGV_SOH_NO, INVGV_GV_TYPE,INVGV_GV_NO,INVGV_GV_AMT, INVGV_CR_UID,INVGV_CR_DT)values("
                        stQueryRef = stQueryRef & "INVGV_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & GVListView.Items(i).SubItems(1).Text & "','" & GVListView.Items(i).SubItems(4).Text & "','" & GVListView.Items(i).SubItems(5).Text & "','" & LogonUser & "',sysdate)"
                        ''7311 cONTINUE FROM THIS
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_GIFT_VOUCHER", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_GIFT_VOUCHER  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewInsurance.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into PERF_OT_INVOICE_INSURANCE(INVRI_SOH_SYS_ID, INVRI_COMP_CODE, INVRI_LOCN_CODE,  INVRI_SOH_TXN_CODE,INVRI_SOH_NO, INVR1_INSURANCE_CODE,INVR1_INSURANCE_NO, INVRI_CR_UID,INVRI_CR_DT)values("
                        stQueryRef = stQueryRef & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & SOHNO & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY PERF_OT_INVOICE_INSURANCE", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY PERF OT_INVOICE_INSURANCE  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    stQuery = "DELETE FROM OT_POS_SO_ITEM_LOG WHERE PROD_SOI_SOH_SYS_ID=" & SOHSYSID.ToString
                    errLog.WriteToErrorLog("REST OT_POS_SO_ITEM_LOG DELETE", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "REST OT_POS_SO_ITEM_LOG DELETE  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    stQuery = "DELETE FROM OT_POS_SO_HEAD_LOG WHERE SOH_SYS_ID =" & SOHSYSID.ToString
                    errLog.WriteToErrorLog("QUERY DELETE OT_POS_SO_HEAD_LOG ", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY DELETE OT_POS_SO_HEAD_LOG  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    stQuery = "update ot_so_head set soh_advance =" & advanceamount.ToString & ", SOH_FLEX_01='" & SOH_FLEX_03_Val & "' where soh_no=" & SOHNO.ToString
                    errLog.WriteToErrorLog("UPDATE SOH_ADVANCE", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE SOH_ADVANCE  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    transaction.Commit()

                    cardpay.SetBalanceAmount(0)
                    cardpay.setReceivedAmount(0)
                    PaymentlistView.Items.Clear()
                    lblTransLoader.SendToBack()
                    MsgBox("Sales Order transaction completed Successfully!")
                    Return True

                Catch ex As Exception
                    qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                    transaction.Rollback()
                    Return False
                End Try
            End Using

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
            Return False
        End Try
    End Function


    Private Function DirectInvoiceDone() As Boolean
        Try
            'Using connection As New OdbcConnection(db.GetConnectionString(""))
            Using connection As New OracleConnection(db.GetConnectionString(""))
                Dim command As New OracleCommand
                'Dim command As New OdbcCommand()
                Dim transaction As OracleTransaction
                'Dim transaction As OdbcTransaction
                command.Connection = connection
                Try
                    connection.Open()
                    transaction = connection.BeginTransaction()

                    command.Connection = connection
                    command.Transaction = transaction

                    Dim ds As DataSet
                    Dim i As Integer = 0
                    Dim stQuery As String
                    Dim count As Integer
                    Dim maxInv As Integer
                    Dim maxSYS_ID As Integer
                    Dim row As System.Data.DataRow
                    maxInv = INVHNO

                    Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")
                    txtSalesmanCode.Text = strSalesArr(0)

                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "SELECT INVH_SYS_ID.NEXTVAL FROM DUAL"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        maxSYS_ID = Convert.ToInt32(row.Item(0).ToString)
                    End If

                    Dim sms_shift_Code As New String("")
                    stQuery = New String("")
                    ds = New DataSet
                    stQuery = "select sms_shift_code from om_pos_salesman_shift where sms_Code='" & txtSalesmanCode.Text & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    count = ds.Tables("Table").Rows.Count
                    If count > 0 Then
                        row = ds.Tables("Table").Rows.Item(0)
                        sms_shift_Code = row.Item(0).ToString
                    End If

                    ' Dim custDetails(35) As String

                    'custDetails = INVH_Details.getCustomerDetails
                    'frmFeedback.CUSTCODE = custDetails(0).ToString
                    stQuery = New String("")
                    Dim strCustArr() As String = txtCustomerCode.Text.Split(" - ")
                    txtCustomerCode.Text = strCustArr(0)

                    stQuery = "insert into ot_invoice_head(INVH_SYS_ID,INVH_COMP_CODE,INVH_LOCN_CODE,INVH_TXN_CODE,INVH_NO,INVH_DT,INVH_DOC_SRC_LOCN_CODE,INVH_CUST_CODE,INVH_CURR_CODE,INVH_DEL_LOCN_CODE,INVH_SM_CODE,INVH_TERM_CODE,INVH_PRINT_STATUS,INVH_BILL_TO_ADDR_CODE,INVH_SHIP_TO_ADDR_CODE,INVH_BILL_ADDR_LINE_1,INVH_BILL_ADDR_LINE_2,INVH_BILL_CITY_CODE,INVH_BILL_COUNTY_CODE,INVH_BILL_STATE_CODE,INVH_BILL_POSTAL_CODE,INVH_BILL_PROVINCE_CODE,INVH_BILL_COUNTRY_CODE,INVH_BILL_CONTACT,INVH_BILL_EMAIL,INVH_BILL_FAX,INVH_BILL_TEL,INVH_BILL_MOBILE,INVH_SHIP_ADDR_LINE_1,INVH_SHIP_ADDR_LINE_2,INVH_SHIP_CITY_CODE,INVH_SHIP_COUNTY_CODE,INVH_SHIP_STATE_CODE,INVH_SHIP_POSTAL_CODE,INVH_SHIP_PROVINCE_CODE,INVH_SHIP_COUNTRY_CODE,INVH_SHIP_CONTACT,INVH_SHIP_EMAIL,INVH_SHIP_FAX,INVH_SHIP_TEL,INVH_SHIP_MOBILE,INVH_CUST_NAME,INVH_DISC_VAL,INVH_DISC_PERC,INVH_ACNT_YEAR,INVH_CAL_YEAR,INVH_REF_FROM,INVH_REF_FROM_NUM,INVH_REF_TXN_CODE,INVH_DEL_DT,INVH_EXGE_RATE,INVH_STATUS,INVH_APPR_STATUS,INVH_APPR_UID,INVH_APPR_DT,INVH_MODE_SHIP_CODE,INVH_COS_FIN_STATUS,INVH_CAL_PERIOD,INVH_DNTOFOLLOW_NUM,INVH_STOP_AT_INV,INVH_ADVANCE,INVH_EXC_INV_YN,INVH_SUBMIT_STATUS,INVH_FLEX_19,INVH_CR_UID,INVH_CR_DT,INVH_FLEX_14,INVH_FLEX_20,INVH_FLEX_03,INVH_ANNOTATION,INVH_TYPE,INVH_EXC_YN,INVH_ST_CODE,INVH_FLEX_10,INVH_FLEX_04)values("
                    stQuery = stQuery & maxSYS_ID & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ", '" & dtpick.Value.ToString("dd-MMM-yy") & "','" & Location_Code & "','" & txtCustomerCode.Text & "','" & Currency_Code & "','" & Location_Code & "','" & txtSalesmanCode.Text & "','" & CPT_TermCode & "',1,'"
                    stQuery = stQuery & custDetails(2) & "','" & custDetails(3) & "','" & custDetails(4) & "','" & custDetails(5) & "','" & custDetails(6) & "','" & custDetails(7) & "','" & custDetails(8) & "','" & custDetails(9) & "','" & custDetails(10) & "','" & custDetails(11) & "','" & custDetails(12) & "','" & custDetails(13) & "','" & custDetails(14) & "','" & custDetails(15) & "','" & custDetails(16) & "','" & custDetails(17) & "','" & custDetails(18) & "','" & custDetails(19) & "','" & custDetails(20) & "','" & custDetails(21) & "','" & custDetails(22) & "','" & custDetails(23) & "','" & custDetails(24) & "','" & custDetails(25) & "','" & custDetails(26) & "','" & custDetails(27) & "','" & custDetails(28) & "','" & custDetails(29) & "','" & custDetails(1) & "'," & "0"
                    stQuery = stQuery & ",0," & PC_Account_Year & "," & PC_CAL_Year & ",'D',1,'', sysdate + 1," & Exchange_Rate & ",0,0,'" & LogonUser & "','','RD',''," & PC_CAL_Period & ",2,'Y',0,'N',0,'" & sms_shift_Code & "','" & LogonUser & "',current_timestamp,'','" & POSCounterNumber & "','" + txtPatientNo.Text + "','" + "" + "','3','N','CASH','N','" & txtPatPatientName.Text.Replace("'", "''").ToString & "')"
                    errLog.WriteToErrorLog("INSERT QUERY HEAD", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY HEAD  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    stQuery = "insert into CRM_INV_CAMP(INV_CAMP_CODE,INV_CAMP_INVHNO,INV_CAMP_COMP_CODE,INV_CAMP_LOCN_CODE,INV_CAMP_SM_CODE,INV_CAMP_CR_UID,INV_CAMP_CR_DT)values("
                    stQuery = stQuery & "'" & txtCampaignCode.Text & "','" & INVHNO & "','" & CompanyCode & "','" & Location_Code & "','" & txtSalesmanCode.Text & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'))"
                    errLog.WriteToErrorLog("INSERT QUERY CRM_CAMPAIGN", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY CRM_CAMPAIGN  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()


                    Dim dsAll As DataSet
                    stQuery = "select PROD_INVI_INVH_SYS_ID,PRODCODE,PRODDESC,PRODITEMUOM,PRODQTY,PRODPRICE,PRODBARCODE,PRODRATE,PRODMINRATE,PRODDISCPER,PRODDISCAMT,PRODIGCODE,PRODGRADECODE_1,PRODGRADECODE_2,PRODPLCODE,PRODPLRATE,PRODBATCHNO,PRODSRNO,PRODRI,PRODDISCCODE,PRODVATCODE,PRODVATAMT,PRODEXPCODE,PRODEXPAMT,PRODBONUSCODE,PRODREASONCODE,PRODFOCITEM,PRODWARRANTYDT,PRODWARRANTYNO,PRODSTKITEM,PRODLOCNCODE,PRODSTOCKRESERV,PRODAFTHEADERDISC,PRODTAXABLEAMT,PRODUSERID,PRODITEMSTATUS,PRODWARRANTYTYPE,PRODWARRANTYDAYS from OT_POS_INVOICE_ITEM_LOG where PROD_INVI_INVH_SYS_ID=" + INVHSYSID.ToString
                    dsAll = db.SelectFromTableODBC(stQuery)
                    Dim countval = dsAll.Tables("Table").Rows.Count

                    'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                    Dim taxImpl As New TaxImpl(db)

                    i = 0
                    While countval > 0
                        Dim rowItem(24) As String
                        rowItem(0) = dsAll.Tables("Table").Rows.Item(i).Item(0).ToString
                        rowItem(1) = dsAll.Tables("Table").Rows.Item(i).Item(1).ToString
                        rowItem(2) = dsAll.Tables("Table").Rows.Item(i).Item(2).ToString
                        rowItem(10) = dsAll.Tables("Table").Rows.Item(i).Item(3).ToString
                        rowItem(9) = dsAll.Tables("Table").Rows.Item(i).Item(7).ToString
                        rowItem(14) = dsAll.Tables("Table").Rows.Item(i).Item(14).ToString
                        rowItem(4) = dsAll.Tables("Table").Rows.Item(i).Item(15).ToString
                        rowItem(3) = dsAll.Tables("Table").Rows.Item(i).Item(4).ToString
                        rowItem(11) = dsAll.Tables("Table").Rows.Item(i).Item(9).ToString
                        rowItem(5) = (Convert.ToInt64(rowItem(3).ToString) * Convert.ToDouble(rowItem(9).ToString)).ToString
                        rowItem(8) = dsAll.Tables("Table").Rows.Item(i).Item(5).ToString
                        rowItem(12) = dsAll.Tables("Table").Rows.Item(i).Item(12).ToString
                        rowItem(13) = dsAll.Tables("Table").Rows.Item(i).Item(13).ToString
                        rowItem(6) = dsAll.Tables("Table").Rows.Item(i).Item(10).ToString
                        rowItem(7) = dsAll.Tables("Table").Rows.Item(i).Item(6).ToString
                        rowItem(19) = dsAll.Tables("Table").Rows.Item(i).Item(19).ToString
                        rowItem(22) = dsAll.Tables("Table").Rows.Item(i).Item(22).ToString
                        rowItem(23) = dsAll.Tables("Table").Rows.Item(i).Item(23).ToString

                        Dim maxSOI_SYS_ID As Integer
                        Dim count1 As Integer
                        stQuery = New String("")
                        ds = New DataSet
                        stQuery = "SELECT INVI_SYS_ID.NEXTVAL FROM DUAL"
                        ds = db.SelectFromTableODBC(stQuery)
                        count1 = ds.Tables("Table").Rows.Count
                        If count1 > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            maxSOI_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                        End If

                        'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                        Dim itemCodeForTaxEntry As String = ""
                        Dim itemPriceForTaxEntry As Double = rowItem(8)
                        itemCodeForTaxEntry = rowItem(1).ToString

                        'Added this code to implement tax in POS  - Ticket-https://evisionsoft.freshdesk.com/helpdesk/tickets/40 
                        Dim taxCode As String = taxImpl.getLocationTaxCodeForItem(Location_Code, itemCodeForTaxEntry)
                        Dim taxPercentage As Double = taxImpl.getTaxPercentageofItem(itemCodeForTaxEntry, Location_Code, TXN_Code, taxCode)
                        Dim taxValueOfItem As Double = Round(taxImpl.calculateTaxValueofItem(itemCodeForTaxEntry, itemPriceForTaxEntry, Location_Code, TXN_Code, taxCode, taxPercentage), 3)
                        'taxValueOfItem = taxValueOfItem

                        stQuery = "insert  into  ot_invoice_item(INVI_SYS_ID,INVI_INVH_SYS_ID,INVI_LOCN_CODE,INVI_DEL_LOCN_CODE,INVI_ITEM_CODE,INVI_ITEM_STK_YN_NUM,INVI_ITEM_DESC,INVI_UOM_CODE,INVI_SM_CODE,INVI_PL_CODE,INVI_PL_RATE,INVI_UPD_STK_YN,INVI_QTY,INVI_QTY_LS,INVI_QTY_BU,INVI_RESV_QTY,INVI_RESV_QTY_LS,INVI_RESV_QTY_BU,INVI_PII_QTY_BU,INVI_PAI_QTY_BU,INVI_DNI_QTY_BU,INVI_CSRI_QTY_BU,INVI_RATE,INVI_DISC_PERC,INVI_FC_VAL,INVI_FC_VAL_AFT_H_DISC,INVI_FOC_YN_NUM,INVI_DUTY_PAID_YN,INVI_ASSESS_RATE,INVI_ASSESS_VAL,INVI_GRADE_CODE_1,INVI_GRADE_CODE_2,INVI_CENVAT_IND,INVI_FC_DISC_VAL,INVI_CR_UID,INVI_CR_DT,INVI_FLEX_18,INVI_FOC_YN, INVI_FC_ACT_VAL)values("
                        stQuery = stQuery & maxSOI_SYS_ID & "," & maxSYS_ID & ",'" & Location_Code & "','" & Location_Code & "','" & rowItem(1).ToString & "',1,'" & rowItem(2).ToString & "','" & rowItem(10).ToString & "','" & txtSalesmanCode.Text & "','" & rowItem(14).ToString & "'," & Convert.ToDouble(rowItem(4).ToString) & ",'Y'," & Convert.ToInt32(rowItem(3).ToString) & ",0," & Convert.ToInt32(rowItem(3).ToString) & ",0,0,0,0,0,0,0," & Convert.ToDouble(rowItem(4).ToString) & "," & "0" & "," & Convert.ToDouble(rowItem(5).ToString) - Convert.ToDouble(rowItem(6).ToString) - taxValueOfItem & "," & Convert.ToDouble(rowItem(5).ToString) & ",2,'N',0,0,'" & rowItem(12).ToString & "','" & rowItem(13).ToString & "',0," & rowItem(6).ToString & ",'" & LogonUser & "',sysdate,'" & rowItem(7).ToString & "','N', " & Convert.ToDouble(rowItem(5).ToString) - Convert.ToDouble(rowItem(6).ToString) - taxValueOfItem & ")"
                        errLog.WriteToErrorLog("INVOICE ITEM QUERY", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INVOICE ITEM  QUERY  - " & stQuery)
                        command.CommandText = stQuery

                        command.ExecuteNonQuery()

                        stQuery = "DELETE FROM OT_POS_INVOICE_ITEM_LOG WHERE PRODITEMSTATUS <> 'D' AND PROD_INVI_INVH_SYS_ID =" + INVHSYSID.ToString + " AND PRODCODE='" + rowItem(1).ToString + "'"
                        errLog.WriteToErrorLog("DELETE QUERY ITEM LOG", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY ITEM LOG  - " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        stQuery = New String("")
                        Dim maxLedgerID As Integer
                        ds = New DataSet
                        stQuery = "select INVID_SYS_ID.NEXTVAL from dual"
                        ds = db.SelectFromTableODBC(stQuery)
                        count1 = ds.Tables("Table").Rows.Count
                        If count1 > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            maxLedgerID = Convert.ToInt32(row.Item(0).ToString)
                            'maxLedgerID = maxLedgerID + 1
                        End If

                        stQuery = New String("")
                        stQuery = "INSERT INTO OT_INVOICE_ITEM_DEL(INVID_SYS_ID,INVID_INVI_SYS_ID,INVID_DEL_DT,INVID_DEL_QTY,INVID_DEL_QTY_LS,INVID_DEL_QTY_BU,INVID_CR_DT,INVID_CR_UID)VALUES("
                        stQuery = stQuery & maxLedgerID & "," & maxSOI_SYS_ID & ",to_date(sysdate,'DD-MM-YY')," & Convert.ToInt32(rowItem(3).ToString) & ",0," & Convert.ToInt32(rowItem(3).ToString) & ",to_date(sysdate,'DD-MM-YY'),'" & LogonUser & "')"
                        errLog.WriteToErrorLog("QUERY INSERT ITEM DEL", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM DEL  - " & stQuery)
                        command.CommandText = stQuery
                        command.ExecuteNonQuery()

                        If Not taxValueOfItem.Equals(0) Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TAX'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDTAX_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDTAX_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & taxCode & "'," & TEDTAX_NUM & ",'2','R','" & Currency_Code & "','" & Currency_Code & "'," & taxPercentage & "," & itemPriceForTaxEntry - taxValueOfItem & "," & itemPriceForTaxEntry - taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & "," & taxValueOfItem & ",'" & LogonUser & "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDDIS", stQuery, "")
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        If Not rowItem(19).ToString = "" Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDDIS_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDDIS_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TED_RATE,ITED_TAXABLE_FC_AMT, ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & rowItem(19).ToString & "'," + TEDDIS_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," & "0" & "," & rowItem(9).ToString + "," + rowItem(9).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + "," + rowItem(6).ToString + ",'" + LogonUser + "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDDIS", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM  TEDDIS  - " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        If Not rowItem(23).ToString = "0" Or rowItem(23).ToString = "" Then
                            stQuery = New String("")
                            stQuery = "select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP'"
                            Dim dsTED As DataSet = db.SelectFromTableODBC(stQuery)
                            Dim TEDEXP_NUM As String = ""
                            If dsTED.Tables("Table").Rows.Count > 0 Then
                                TEDEXP_NUM = dsTED.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If
                            stQuery = "INSERT INTO OT_INVOICE_ITEM_TED (ITED_SYS_ID,ITED_H_SYS_ID,ITED_I_SYS_ID ,ITED_TED_CODE,ITED_TED_TYPE_NUM,ITED_TED_HEAD_ITEM_NUM,ITED_TED_BASIS, ITED_TED_CURR_CODE,ITED_TXN_CURR_CODE,ITED_TAXABLE_FC_AMT,ITED_TAXABLE_LC_AMT,ITED_FC_AMT,ITED_LC_AMT,ITED_NET_FC_AMT,ITED_NET_LC_AMT,ITED_CR_UID,ITED_CR_DT)VALUES("
                            stQuery = stQuery & "ITED_SYS_ID.NEXTVAL" & "," & maxSYS_ID & "," & maxSOI_SYS_ID & ",'" & rowItem(22).ToString & "'," + TEDEXP_NUM + ",'2','R','" + Currency_Code + "','" + Currency_Code + "'," + rowItem(9).ToString + "," + rowItem(9).ToString + "," + rowItem(23).ToString + "," + rowItem(23).ToString + "," + rowItem(23).ToString + "," + rowItem(23).ToString + ",'" + LogonUser + "',sysdate)"
                            errLog.WriteToErrorLog("QUERY INSERT ITEM TEDEXP", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY INSERT ITEM  TEDEXP  -  " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        End If

                        i = i + 1
                        countval = countval - 1
                    End While


                    Dim listPayment As New List(Of String())
                    i = 0
                    listPayment = cardpay.GetPaymentItems()

                    count = listPayment.Count

                    If count > 0 Then
                        Dim rowPayment(8) As String
                        While count > 0

                            stQuery = New String("")
                            Dim maxPINVP_SYS_ID As Long
                            ds = New DataSet
                            stQuery = "select pos_invp_sys_id.nextval from dual"
                            ds = db.SelectFromTableODBC(stQuery)
                            Dim count1 As Integer
                            count1 = ds.Tables("Table").Rows.Count
                            If count1 > 0 Then
                                row = ds.Tables("Table").Rows.Item(0)
                                maxPINVP_SYS_ID = Convert.ToInt32(row.Item(0).ToString)
                                'maxPINVP_SYS_ID = maxPINVP_SYS_ID + 1
                            End If

                            stQuery = New String("")
                            rowPayment = listPayment.Item(i)

                            If Not rowPayment(8) = "0" Then
                                rowPayment(3) = Convert.ToDouble(rowPayment(3).ToString) - Convert.ToDouble(rowPayment(8).ToString)
                            End If

                            stQuery = "insert into ot_pos_invoice_payment(PINVP_SYS_ID,PINVP_INVH_SYS_ID,PINVP_PPD_CODE,PINVP_PPD_DESC,PINVP_CURR_CODE,PINVP_FC_VAL,PINVP_FLEX_20,PINVP_CR_DT,PINVP_CR_UID,PINVP_LC_VAL)values("
                            stQuery = stQuery & maxPINVP_SYS_ID & "," & maxSYS_ID & ",'" & rowPayment(1).ToString & "','" & rowPayment(6).ToString & "','" & rowPayment(2).ToString & "'," & Convert.ToDouble(rowPayment(3).ToString) & ",'" & rowPayment(7).ToString & "',sysdate,'" & LogonUser & "'," & Convert.ToDouble(rowPayment(3).ToString) & ")"
                            errLog.WriteToErrorLog("QUERY Payment Insertion", stQuery, "")
                            'MAILLOGROWNUM = MAILLOGROWNUM + 1
                            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY PAYMENT INSERTIONS  -  " & stQuery)
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                            If rowPayment(7) = "GV" Then
                                stQuery = "INSERT INTO OT_POS_INVOICE_GV(PINVGV_SYS_ID,PINVGV_INVH_SYS_ID,PINVGV_PINVP_SYS_ID,PINVGV_GVNO,PINVGV_GV_CURR_CODE,PINVGV_GV_VALUE,PINVGV_CR_DT,PINVGV_CR_UID,PINVGV_GVCODE)VALUES("
                                stQuery = stQuery + "PINVGV_SYS_ID.NEXTVAL," + maxSYS_ID.ToString + "," + maxPINVP_SYS_ID.ToString + ",'" + rowPayment(4).ToString + "','" + rowPayment(2).ToString + "'," + rowPayment(3).ToString + ",sysdate,'" + LogonUser + "','" + rowPayment(1).ToString + "')"
                                errLog.WriteToErrorLog("GIFT VOUCHER QUERY", stQuery, "")
                                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                                'MAILLOGValues.Add(MAILLOGROWNUM, " GIFT VOUCHER  QUERY" & stQuery)
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            End If

                            i = i + 1
                            count = count - 1
                        End While
                    End If

                    For i = 0 To lstRCDetail.Items.Count - 1
                        Dim stQueryRef As String
                        'stQueryRef = "insert into OT_INVOICE_ROYALTY_CARD(INVRC_SYS_ID,INVRC_INVH_SYS_ID, INVRC_COMP_CODE, INVRC_LOCN_CODE, INVRC_INVH_TXN_CODE,INVRC_INVH_NO, INVRC_ROYALTY_CARD,INVRC_ROYALTY_NO, INVRC_CR_UID,INVRC_CR_DT)values("
                        stQueryRef = "INSERT INTO OT_INVOICE_LOYALTY_CARD(INVLC_SYS_ID,INVLC_INVH_SYS_ID,INVLC_COMP_CODE,INVLC_LOCN_CODE,INVLC_INVH_TXN_CODE,INVLC_INVH_NO,INVLC_LOYALTY_CARD,INVLC_LOYALTY_NO,INVLC_CR_UID,INVLC_CR_DT)VALUES("
                        stQueryRef = stQueryRef & "INVLC_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstRCDetail.Items(i).SubItems(1).Text & "','" & lstRCDetail.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_LOYALTY_CARD", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_LOYALTY_CARD  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewReferal.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into OT_INVOICE_REF_HOSPITAL(INVRH_SYS_ID,INVRH_INVH_SYS_ID, INVRH_COMP_CODE, INVRH_LOCN_CODE, INVRH_INVH_TXN_CODE,INVRH_INVH_NO, INVRH_REF_HOSPITAL_CODE,INVRH_DOCTOR_ID,INVRH_DOCTOR_NAME, INVRH_CR_UID,INVRH_CR_DT)values("
                        stQueryRef = stQueryRef & "INVRH_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & lstviewReferal.Items(i).SubItems(4).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_REF_HOSPITAL", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_RED_HOSPITAL" & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To GVListView.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into OT_INVOICE_GIFT_VOUCHER(INVGV_SYS_ID,INVGV_INVH_SYS_ID,INVGV_COMP_CODE,INVGV_LOCN_CODE,INVGV_INVH_TXN_CODE,INVGV_INVH_NO, INVGV_GV_TYPE,INVGV_GV_NO,INVGV_GV_AMT, INVGV_CR_UID,INVGV_CR_DT)values("
                        stQueryRef = stQueryRef & "INVGV_SYS_ID.nextval," & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & GVListView.Items(i).SubItems(1).Text & "','" & GVListView.Items(i).SubItems(4).Text & "','" & GVListView.Items(i).SubItems(5).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_GIFT_VOUCHER", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_GIFT_VOUCHER  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    For i = 0 To lstviewInsurance.Items.Count - 1
                        Dim stQueryRef As String
                        stQueryRef = "insert into PERF_OT_INVOICE_INSURANCE(INVRI_INVH_SYS_ID, INVRI_COMP_CODE, INVRI_LOCN_CODE, INVRI_INVH_TXN_CODE,INVRI_INVH_NO, INVR1_INSURANCE_CODE,INVR1_INSURANCE_NO, INVRI_CR_UID,INVRI_CR_DT)values("
                        stQueryRef = stQueryRef & maxSYS_ID.ToString & ",'" & CompanyCode & "','" & Location_Code & "','" & TXN_Code & "'," & maxInv & ",'" & lstviewReferal.Items(i).SubItems(1).Text & "','" & lstviewReferal.Items(i).SubItems(3).Text & "','" & LogonUser & "',sysdate)"
                        errLog.WriteToErrorLog("INSERT QUERY OT_INVOICE_REF_HOSPITAL", stQueryRef, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "INSERT QUERY OT_INVOICE_REF_HOSPITAL  - " & stQueryRef)
                        command.CommandText = stQueryRef
                        command.ExecuteNonQuery()
                    Next

                    stQuery = "DELETE FROM OT_POS_INVOICE_HEAD_LOG WHERE INVH_SYS_ID =" & INVHSYSID
                    errLog.WriteToErrorLog("DELETE OT_POS_INVOICE_HEAD_LOG", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE OT_POS_INVOICE_HEAD_LOG  - " & stQuery)
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()


                    transaction.Commit()

                    i = 0
                    stQuery = New String("")
                    ds = New DataSet
                    'stQuery = "insert into tempInvoice select a.invh_no as invoice_no,a.invh_CUST_CODE as cust_code,a.invh_CUST_NAME as cust_name, a.invh_BILL_ADDR_LINE_1||' '||a.invh_BILL_ADDR_LINE_2||' '||a.invh_BILL_COUNTRY_CODE as billing_addr,a.INVH_BILL_TEL as billing_phone, a.invh_BILL_EMAIL as billing_email, a.invh_SHIP_ADDR_LINE_1||' '||a.invh_SHIP_ADDR_LINE_2||' '||a.invh_SHIP_COUNTRY_CODE as shipping_addr,b.invi_ITEM_CODE as item_code,b.invi_ITEM_DESC as item_name,b.invi_QTY as quantity,b.invi_UOM_CODE as uom,b.invi_PL_RATE as rate,'' as tax,a.invh_disc_val as discount,b.invi_FC_VAL_AFT_H_DISC as expense,b.invi_FC_VAL as amount, trunc(a.invh_DT) as invdate from ot_invoice_head a,ot_invoice_item b where a.invh_SYS_ID = b.invi_invh_SYS_ID and a.invh_status=0 and a.invh_no=" & maxInv
                    'errLog.WriteToErrorLog("INSERT TEMPINVOICE", stQuery, "")
                    'db.SaveToTableODBC(stQuery)

                    cardpay.SetBalanceAmount(0)
                    cardpay.setReceivedAmount(0)
                    PaymentlistView.Items.Clear()
                    lblTransLoader.SendToBack()
                    MsgBox("Transaction completed Successfully!")
                    Return True
                Catch ex As Exception
                    qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                    transaction.Rollback()
                    Return False
                End Try
            End Using
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
            Return False
        End Try
    End Function

    Private Sub UpdateItemLog()
        Try
            If TXN_Code = "POSINV" Then

                Dim success As Boolean = False
                Dim ItemSelected As New List(Of String)
                For k = 1 To txtItemCode.Count
                    Dim ItmCode_name As String = "ItemCode" & k
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                    Dim stQueryItemBar As String
                    stQueryItemBar = "select distinct item_code from om_item where (item_code='" + ItmCodeFound(0).Text + "' OR item_harmonised_code='" + ItmCodeFound(0).Text + "')"
                    Dim dsIB As DataSet
                    dsIB = db.SelectFromTableODBC(stQueryItemBar)
                    If dsIB.Tables("Table").Rows.Count > 0 Then
                        ItemSelected.Add(dsIB.Tables("Table").Rows.Item(0).Item(0).ToString)
                    End If
                    'ItemSelected.Add(ItmCodeFound(0).Text)
                Next
                Dim stQuery As String
                Dim ds As DataSet
                stQuery = "select PRODCODE from OT_POS_INVOICE_ITEM_LOG where PROD_INVI_INVH_SYS_ID=" + INVHSYSID.ToString
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer = ds.Tables("Table").Rows.Count
                Dim i As Integer = 0
                While count > 0
                    Dim itemCode As String = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                    If ItemSelected.Contains(itemCode) Then
                    Else
                        stQuery = "DELETE FROM OT_POS_INVOICE_ITEM_LOG WHERE PRODITEMSTATUS<>'D' and PROD_INVI_INVH_SYS_ID=" & INVHSYSID.ToString & " and PRODCODE='" + itemCode + "'"
                        errLog.WriteToErrorLog("Delete Query ITEM LOG", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY ITEM LOG  -  " & stQuery)
                        db.SaveToTableODBC(stQuery)
                    End If
                    'If success = False Then
                    '    MsgBox(itemCode + " has been deleted  - " + i.ToString)
                    'End If
                    i = i + 1
                    count = count - 1
                End While
            ElseIf TXN_Code = "SO" Then
                Dim success As Boolean = False
                Dim ItemSelected As New List(Of String)
                For k = 1 To txtItemCode.Count
                    Dim ItmCode_name As String = "ItemCode" & k
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                    Dim stQueryItemBar As String
                    stQueryItemBar = "select distinct item_code from om_item where (item_code='" + ItmCodeFound(0).Text + "' OR item_harmonised_code='" + ItmCodeFound(0).Text + "')"
                    Dim dsIB As DataSet
                    dsIB = db.SelectFromTableODBC(stQueryItemBar)
                    If dsIB.Tables("Table").Rows.Count > 0 Then
                        ItemSelected.Add(dsIB.Tables("Table").Rows.Item(0).Item(0).ToString)
                    End If
                    'ItemSelected.Add(ItmCodeFound(0).Text)
                Next
                Dim stQuery As String
                Dim ds As DataSet
                'select PROD_SOI_SOH_SYS_ID,PRODCODE,PRODDESC,PRODITEMUOM,PRODQTY,PRODPRICE,PRODBARCODE,PRODRATE,PRODMINRATE,PRODDISCPER,PRODDISCAMT,PRODIGCODE,PRODGRADECODE_1,PRODGRADECODE_2,PRODPLCODE,PRODPLRATE,PRODBATCHNO,PRODSRNO,PRODRI,PRODDISCCODE,PRODVATCODE,PRODVATAMT,PRODEXPCODE,PRODEXPAMT,PRODBONUSCODE,PRODREASONCODE,PRODFOCITEM,PRODWARRANTYDT,PRODWARRANTYNO,PRODSTKITEM,PRODLOCNCODE,PRODSTOCKRESERV,PRODAFTHEADERDISC,PRODTAXABLEAMT,PRODUSERID,PRODITEMSTATUS,PRODWARRANTYTYPE,PRODWARRANTYDAYS from OT_POS_SO_ITEM_LOG where PROD_SOI_SOH_SYS_ID = " + SOHSYSID.ToString
                stQuery = "select PRODCODE from OT_POS_SO_ITEM_LOG where PROD_SOI_SOH_SYS_ID=" + SOHSYSID.ToString
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer = ds.Tables("Table").Rows.Count
                Dim i As Integer = 0
                While count > 0
                    Dim itemCode As String = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                    If ItemSelected.Contains(itemCode) Then
                    Else
                        stQuery = "DELETE FROM OT_POS_SO_ITEM_LOG WHERE PROD_SOI_SOH_SYS_ID=" & SOHSYSID.ToString & " and PRODCODE='" + itemCode + "'"
                        errLog.WriteToErrorLog("Delete Query ITEM LOG", stQuery, "")
                        'MAILLOGROWNUM = MAILLOGROWNUM + 1
                        'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY ITEM LOG  - " & stQuery)
                        db.SaveToTableODBC(stQuery)
                    End If
                    'If success = False Then
                    '    MsgBox(itemCode + " has been deleted  - " + i.ToString)
                    'End If
                    i = i + 1
                    count = count - 1
                End While
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Function FinalizeSalesReturntransaction()
        Dim success As Boolean = True
        Try
            If txtCustomerName.Text = "" Then
                MsgBox("Please select a valid customer")
                success = False
            ElseIf txtSalesmanName.Text = "" Then
                MsgBox("Please select a valid Salesman")
                success = False
            ElseIf txtItemCode.Count < 1 Then
                MsgBox("No sales return item selected!")
                success = False
            Else
                success = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
        Return success
    End Function

    Private Function FinalizeSalesInvoicetransaction()
        Dim success As Boolean = True
        Try
            If txtCustomerName.Text = "" Then
                MsgBox("Please select a valid customer")
                success = False
            ElseIf txtSalesmanName.Text = "" Then
                MsgBox("Please select a valid Salesman")
                success = False
            ElseIf txtItemCode.Count < 1 Then
                MsgBox("No Sales order selected!")
                success = False
            Else
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer
                Dim row As System.Data.DataRow

                For k = 0 To txtItemCode.Count - 1
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & (k + 1).ToString, True)
                    Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & (k + 1).ToString, True)
                    Dim itemCode As String = ItmCodeFound(0).Text
                    stQuery = "select item_stk_yn_num from om_item where item_code='" & itemCode & "'"
                    ds = db.SelectFromTableODBC(stQuery)
                    Dim itmstkynnumval As String = ""
                    If ds.Tables("Table").Rows.Count > 0 Then
                        itmstkynnumval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                    End If
                    If itmstkynnumval = "1" Then
                        Dim itemAvailable As Integer = 0
                        stQuery = "select (LCS_STK_QTY_BU + LCS_RCVD_QTY_BU) - (LCS_ISSD_QTY_BU+LCS_HOLD_QTY_BU+LCS_REJECT_QTY_BU+LCS_PICK_QTY_BU+LCS_PACK_QTY_BU) as AvailableStk from OS_LOCN_CURR_STK where (LCS_ITEM_CODE = '" & itemCode & "' OR LCS_ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & itemCode & "')) and LCS_LOCN_CODE='" & Location_Code & "'"
                        ds = db.SelectFromTableODBC(stQuery)
                        count = ds.Tables("Table").Rows.Count
                        i = 0
                        If count > 0 Then
                            row = ds.Tables("Table").Rows.Item(0)
                            itemAvailable = Convert.ToInt64(row.Item(0).ToString)
                        End If
                        If itemAvailable >= Convert.ToInt64(ItmQtyFound(0).Text) Then

                        Else
                            MsgBox("Availability of Item  '" + itemCode + "' is low")
                            success = False
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
        Return success
    End Function

    Private Function Finalizetransaction()

        Dim success As Boolean = True
        Try

            For k = 1 To txtItemCode.Count
                Dim ItmCode_name As String = "ItemCode" & k
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & k, True)
                Dim itemCode As String = ItmCodeFound(0).Text
                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim i As Integer
                'Dim row As System.Data.DataRow
                'stQuery = "SELECT OM_ITEM.ITEM_NAME as Item_Name FROM OM_ITEM, OM_POS_ITEM A WHERE NVL (OM_ITEM.ITEM_FRZ_FLAG_NUM,2) = 2 AND OM_ITEM.ITEM_CODE = A.ITEM_CODE AND A.ITEM_PLI_PL_CODE='OGENPL' AND ITEM_COMP_CODE='" + CompanyCode + "' AND (OM_ITEM.ITEM_CODE='" + itemCode + "' OR OM_ITEM.ITEM_HARMONISED_CODE='" + itemCode + "')"
                stQuery = "SELECT ITEM_SHORT_NAME FROM OM_PRICE_LIST_ITEM, OM_ITEM WHERE NVL(ITEM_FRZ_FLAG_NUM,2) = 2 AND NVL(PLI_FRZ_FLAG_NUM,2)=2 AND ITEM_CODE = PLI_ITEM_CODE AND (PLI_ITEM_CODE='" + itemCode + "' OR ITEM_HARMONISED_CODE='" + itemCode + "' ) AND PLI_PL_CODE='" + Setup_Values.Item("PL_CODE") + "'"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                If count < 1 Then
                    If Not itemCode = "" Then
                        MsgBox("Error in Item Code!" + vbNewLine + "Transaction cannot be Proceeded!")
                        success = False
                        Exit For
                    End If
                Else
                    FindItmQty_TextChanged(ItmQtyFound(0), EventArgs.Empty)
                    If ItmQtyFound(0).ForeColor = Color.Red Then
                        success = False
                    Else
                        success = True
                    End If
                End If

            Next

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
        Return success
    End Function

    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Try
            Dim bmp = New Bitmap(pnlReportContainer.Width, pnlReportContainer.Height)
            pnlReportContainer.DrawToBitmap(bmp, pnlReportContainer.ClientRectangle)
            'bmp.Save("ImageName.png", System.Drawing.Imaging.ImageFormat.Png)
            Dim doc As New PdfDocument()
            doc.Pages.Add(New PdfPage())
            Dim xgr As XGraphics = XGraphics.FromPdfPage(doc.Pages(0))
            Dim img As XImage = XImage.FromGdiPlusImage(bmp)
            xgr.DrawImage(img, 0, 0)
            SaveFileDialog1.Filter = "PDF Files (*.pdf*)|*.pdf"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                doc.Save(SaveFileDialog1.FileName)
                doc.Close()
                MsgBox("File has been saved successfully at '" + SaveFileDialog1.FileName + "'")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnCloseReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseReport.Click
        Try
            Me.Close()
            SubHomeForm.MdiParent = Home
            SubHomeForm.Show()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub
    Private Sub txtPatientNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatientNo.TextChanged
        Try
            If txtPatientNo.Text = "" Then
                Exit Sub
            End If
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim i As Integer
            i = 0
            stQuery = "select PM_CUST_CODE as custcode,CUST_NAME as customername,PM_CUST_NAME as pcustname,PM_PATIENT_NAME as patientname,PM_GENDER as gender,to_char(PM_DOB,'dd/mm/yyyy') as dob,PM_CITY as city,PM_ZIPCODE as zipcode,PM_TEL_OFF as offphn,PM_TEL_RES as resphn,PM_TEL_MOB as mobphn,PM_EMAIL as pemail,PM_NATIONALITY as pnationality,PM_COMPANY as pcompany,pm_occupation as occupation,PM_REMARKS as premarks,PM_NOTES as pnotes from om_patient_master a, om_customer b where pm_cust_no='" & txtPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)

                If row.Item(5).ToString = "" Then
                    dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtpickPatDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                If row.Item(4).ToString = "MALE" Then
                    radMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    radFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientNo.Text = txtPatientNo.Text
                txtPatPatientName.Text = row.Item(3).ToString

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
            errLog.WriteToErrorLog("RX-GLASSES", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "RX-GLASSES  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
                txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
                txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
                txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
                txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
                txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
                txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
                txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
                txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
                txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
                txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
                txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
                txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
                txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
                txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
                txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
                txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
                txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

                i = i + 1
                count = count - 1
            Else

            End If


            i = 0
            stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
            errLog.WriteToErrorLog("LENSE  -  ", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "LENSE" & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
                txtRXC_RE_Dia_I.Text = row.Item(1).ToString
                txtRXC_RE_Pow_I.Text = row.Item(2).ToString
                txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
                txtRXC_RE_Dia_II.Text = row.Item(4).ToString
                txtRXC_RE_Pow_II.Text = row.Item(5).ToString
                txtRXC_RE_Brand1.Text = row.Item(6).ToString

                txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
                txtRXC_LE_Dia_I.Text = row.Item(8).ToString
                txtRXC_LE_Pow_I.Text = row.Item(9).ToString
                txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
                txtRXC_LE_Dia_II.Text = row.Item(11).ToString
                txtRXC_LE_Pow_II.Text = row.Item(12).ToString
                txtRXC_LE_Brand2.Text = row.Item(13).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
            errLog.WriteToErrorLog("SLIT AND K", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SLIT AND K  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtSlit_Re.Text = row.Item(0).ToString
                txtSlit_Le.Text = row.Item(1).ToString
                txtSlit_LrisDia.Text = row.Item(2).ToString
                txtK_Re_Hori.Text = row.Item(3).ToString
                txtK_Le_Hori.Text = row.Item(4).ToString
                txtK_Re_Vert.Text = row.Item(5).ToString
                txtK_Le_Vert.Text = row.Item(6).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
            errLog.WriteToErrorLog("Trial Details  - ", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "TRIAL DETAILS  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtTrail_Re.Text = row.Item(0).ToString
                txtTrail_Re_Add.Text = row.Item(1).ToString
                txtTrail_Re_Via.Text = row.Item(2).ToString
                txtTrail_Le.Text = row.Item(3).ToString
                txtTrail_Le_Add.Text = row.Item(4).ToString
                txtTrail_Le_Via.Text = row.Item(5).ToString
                txtTrail_Re_Remarks.Text = row.Item(6).ToString
                txtTrail_Le_Remarks.Text = row.Item(7).ToString
                i = i + 1
                count = count - 1
            End If

            TabControl1.Enabled = True

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnCancelInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelInvoice.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            Dim stQuery As String = ""
            If INVHNO > 0 Then
                Dim result As DialogResult = MsgBox("Do you want to cancel this Invoice?", MessageBoxButtons.YesNo, "Alert")
                If result = Windows.Forms.DialogResult.Yes Then
                    stQuery = "Update OT_POS_INVOICE_HEAD_LOG set INVH_STATUS=5 where INVH_NO=" & INVHNO.ToString
                    errLog.WriteToErrorLog("Cancel Direct Invoice Query  ", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "CANCEL DIRECT INVOICE QUERY  - " & stQuery)
                    db.SaveToTableODBC(stQuery)
                    Me.Close()
                    Home.NewTransaction_Click(sender, e)
                Else

                End If

            ElseIf SOHNO > 0 Then
                Dim result As DialogResult = MsgBox("Do you want to cancel this Sales Order?", MessageBoxButtons.YesNo, "Alert")
                If result = Windows.Forms.DialogResult.Yes Then
                    stQuery = "Update OT_POS_SO_HEAD_LOG set SOH_STATUS=5 where SOH_NO=" & SOHNO.ToString
                    errLog.WriteToErrorLog("Cancel SalesORder Query", stQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "CANCEL SALES ORDER QUERY  - " & stQuery)
                    db.SaveToTableODBC(stQuery)
                    Me.Close()
                    Home.NewTransaction_Click(sender, e)
                Else

                End If
            ElseIf transtype = "Sales Invoice" Then
                Dim result As DialogResult = MsgBox("Do you want to cancel this Sales Invoice?", MessageBoxButtons.YesNo, "Alert")
                If result = Windows.Forms.DialogResult.Yes Then
                    Me.Close()
                    Home.NewTransaction_Click(sender, e)
                Else

                End If
            Else
                If txtItemCode.Count > 0 And txtItemCode(0).ReadOnly Then
                    Dim result As DialogResult = MsgBox("Do you want to cancel this Sales Return?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Me.Close()
                        Home.NewTransaction_Click(sender, e)
                    Else

                    End If
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    'Process for button transaction type slider\\\\\\\\\\\\\\\\\\\\

    Private Sub picboxInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picboxInvoice.Click
        Try
            picboxTransactionType.Location = New Point(0, picboxTransactionType.Location.Y)
            picboxTransactionType.BackgroundImage = My.Resources.invoice
            GetTransactionCode("INV")
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub picboxSalesOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picboxSalesOrder.Click
        Try
            picboxTransactionType.Location = New Point(picboxTransactionType.Width, picboxTransactionType.Location.Y)
            picboxTransactionType.BackgroundImage = My.Resources.sales
            GetTransactionCode("SO")
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub picboxTransactionType_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picboxTransactionType.MouseDown
        Try
            startx = MousePosition.X
            holderWidth = pnlBtnSliderHolder.Width
            piclocationX = picboxTransactionType.Location.X
            maxInvPOS = (holderWidth * 0.25)
            mdown = True
            valx = False
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub picboxTransactionType_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picboxTransactionType.MouseMove
        Try
            If mdown = True Then
                endx = (MousePosition.X - Me.Left)
                If valx = False Then
                    startx = endx - sender.left
                    valx = True
                End If
                sender.left = endx - startx
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub picboxTransactionType_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picboxTransactionType.MouseUp
        Try
            mdown = False
            valx = False
            If picboxTransactionType.Location.X > maxInvPOS Then
                picboxTransactionType.Location = New Point(holderWidth - 2 - picboxTransactionType.Width, picboxTransactionType.Location.Y)
                mdown = False
                valx = True
                picboxTransactionType.BackgroundImage = My.Resources.sales
                GetTransactionCode("SO")
                Exit Sub
            Else
                picboxTransactionType.Location = New Point(0, picboxTransactionType.Location.Y)
                mdown = False
                valx = True
                picboxTransactionType.BackgroundImage = My.Resources.invoice
                GetTransactionCode("INV")
                Exit Sub
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////

    Private Sub GetTransactionCode(ByVal ttype As String)
        Try
            Dim ds As DataSet
            Dim stQuery As String = ""
            Dim row As System.Data.DataRow
            If ttype = "INV" Then
                stQuery = "SELECT TXN_CODE,TXN_TYPE  FROM OM_TXN WHERE TXN_CODE = 'POSINV' AND TXN_TYPE = 'INV'"
            ElseIf ttype = "SO" Then
                stQuery = "SELECT TXN_CODE,TXN_TYPE  FROM OM_TXN WHERE TXN_CODE = 'SO' AND TXN_TYPE ='SO'"
            End If
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                row = ds.Tables("Table").Rows.Item(0)
                TXN_Code = row.Item(0)
                TXN_Type = row.Item(1)
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSalesOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesOrders.Click

        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If TXN_Code = "POSINV" Then

            Else
                MsgBox("Please change transaction type as 'Invoice'")
                Exit Sub
            End If

            If INVHNO > 0 Then
                MsgBox("Please cancel the current invoice!")
                Exit Sub
            End If

            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = False
            Next

            Dim btn As Button = New Button
            With btn
                .Name = "btnSOAdd"
                .Location = New Point(txtSONO.Location.X + txtSONO.Width + 20, txtSONO.Location.Y - 1)
                .Size = New Size(60, 22)
                .BackColor = Color.MediumTurquoise
                .Text = "Add"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOAdd_Click
            Me.pnlSOSONO.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnSOOk"
                .Location = New Point(pnlSOlstview.Width / 2 + 5, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Ok"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOOk_Click
            Me.pnlSOlstview.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnSOCancel"
                .Location = New Point(pnlSOlstview.Width / 2 + 75, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Cancel"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOCancel_Click
            Me.pnlSOlstview.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnRemoveSO"
                .Location = New Point(pnlSOlstview.Width / 2 + 145, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Remove"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnRemoveSO_Click
            Me.pnlSOlstview.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnSOSearch"
                .Location = New Point(pnlSOlstview.Width / 2 + 215, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Search"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOSearch_Click
            Me.pnlSOlstview.Controls.Add(btn)

            'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
            pnlSalesOrders.Height = Me.Height
            pnlButtonHolder.Visible = False
            pnlButtonHolder.SendToBack()
            pnlSalesOrders.BringToFront()
            'pnl1Patient.Width = Me.Width
            For i = 0 To pnlSalesOrders.Width
                pnlSalesOrders.Location = New Point(Me.Width - i, Me.Height - pnlSalesOrders.Height)
                pnlSalesOrders.Show()
                'Threading.Thread.Sleep(0.5)
                i = i + 5
            Next i
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSOSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim btn As Button = New Button
            With btn
                .Name = "btnSOFilter"
                .Location = New Point(dtpickSOToDate.Location.X + dtpickSOToDate.Width + 30, dtpickSOToDate.Location.Y - 1)
                .Size = New Size(60, 22)
                .BackColor = Color.MediumTurquoise
                .Text = "Filter"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOFilter_Click
            Me.pnlSOSOLISTS.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnSOSelect"
                .Location = New Point(pnlSOSOLISTS.Width / 2 + 80, lstviewSO.Location.Y + lstviewSO.Height + 25)
                .Size = New Size(60, 22)
                .BackColor = Color.MediumTurquoise
                .Text = "Select"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOSelect_Click
            Me.pnlSOSOLISTS.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnSOCanSOLIST"
                .Location = New Point(pnlSOSOLISTS.Width / 2 + 150, lstviewSO.Location.Y + lstviewSO.Height + 25)
                .Size = New Size(60, 22)
                .BackColor = Color.MediumTurquoise
                .Text = "Cancel"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnSOCanSOLIST_Click
            Me.pnlSOSOLISTS.Controls.Add(btn)


            lstviewSO.Items.Clear()
            pnlSOSOLISTS.Height = pnlSOlstview.Height + lblSO_SNO.Height + 1
            pnlSOlstview.SendToBack()
            Dim i As Integer = pnlSOlstview.Height + pnlSOlstview.Location.Y
            While i >= pnlSOlstview.Location.Y
                pnlSOSOLISTS.Location = New Point(pnlSOlstview.Location.X, i)
                pnlSOSOLISTS.Show()
                Threading.Thread.Sleep(0.5)
                i = (i - 1)
            End While
            For Each ctl As Control In pnlSOSONO.Controls
                ctl.Enabled = False
            Next
            dtpickSOFromDate.Value = Date.Now
            dtpickSOToDate.Value = Date.Now
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try

    End Sub

    Private Sub GetSODetails()
        Try
            Dim ctlNO As Integer = 0
            Dim totCtl As Integer = txtItemCode.Count
            While totCtl > 0
                txtItemCode.RemoveAt(totCtl - 1)
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDesc.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDesc" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemQty.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemQty" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemPrice.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemPrice" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDisc.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDisc" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDisamt.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDisamt" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemNetamt.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemNetamt" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemAddval.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemAddval" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                picItemDel.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDel" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))

                totCtl = totCtl - 1
            End While
            Dim patnoval As String = ""
            Dim totalAmount As Double = 0
            Dim totalAdv As Double = 0
            For i = 0 To lstviewSOSelected.Items.Count - 1
                Dim sono As String = lstviewSOSelected.Items.Item(i).SubItems(1).Text
                Dim stQuery As String
                Dim ds As DataSet
                Dim row As System.Data.DataRow
                stQuery = "SELECT b.SOI_ITEM_CODE as ITEM_CODE,b.SOI_ITEM_DESC as ITEM_DESC,nvl(b.SOI_QTY,0) as ITEM_QTY, nvl(b.SOI_PL_RATE,0) as ITEM_RATE,(select ITED_TED_CODE from OT_SO_ITEM_TED where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_I_SYS_ID=SOI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')) as ITEM_DISC,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_I_SYS_ID=SOI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as ITEM_DISAMT,nvl(b.SOI_FC_VAL,0) - nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_I_SYS_ID=SOI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as ITEM_NETAMT,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_I_SYS_ID=SOI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as ITEM_ADDVAL,a.SOH_CUST_CODE as CUST_CODE,SOH_FLEX_03 as PATNO  FROM OT_SO_HEAD a, OT_SO_ITEM b WHERE a.SOH_SYS_ID=b.SOI_SOH_SYS_ID AND a.SOH_NO =" & sono
                errLog.WriteToErrorLog("GET SO details", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "GET SO DETAILS  - " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer = ds.Tables("Table").Rows.Count
                Dim k As Integer = 0

                While count > 0
                    row = ds.Tables("Table").Rows.Item(k)
                    patnoval = row.Item(9).ToString
                    Me.pnlItemDetails.AutoScrollPosition = New System.Drawing.Point(0, 0)
                    Dim txt As TextBox
                    Dim n As Integer
                    n = txtItemCode.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmHead.Location.X, (n * 24))
                        .Name = "ItemCode" & n.ToString
                        .Size = New Size(lblItmHead.Width, 20)
                        .Text = row.Item(0).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                    End With
                    Me.txtItemCode.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemDesc.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmDesc.Location.X, (n * 24))
                        .Name = "ItemDesc" & n.ToString
                        .Size = New Size(lblItmDesc.Width, 20)
                        .Text = row.Item(1).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                    End With
                    Me.txtItemDesc.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemQty.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmQty.Location.X, (n * 24))
                        .Name = "ItemQty" & n.ToString
                        .Size = New Size(lblItmQty.Width, 20)
                        .Text = row.Item(2).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                        .TextAlign = HorizontalAlignment.Center
                    End With
                    Me.txtItemQty.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemPrice.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmPrice.Location.X, (n * 24))
                        .Name = "ItemPrice" & n.ToString
                        .Size = New Size(lblItmPrice.Width, 20)
                        .Text = row.Item(3).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                        .TextAlign = HorizontalAlignment.Right
                    End With
                    Me.txtItemPrice.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemDisc.Count + 1
                    Dim txtcmb As ComboBox
                    txtcmb = New ComboBox
                    With txtcmb
                        .Location = New Point(lblItmDiscCode.Location.X, (n * 24))
                        .Name = "ItemDisc" & n.ToString
                        .Size = New Size(lblItmDiscCode.Width, 20)
                        .Text = row.Item(4).ToString
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                    Me.txtItemDisc.Add(txtcmb)
                    Me.pnlItemDetails.Controls.Add(txtcmb)

                    n = txtItemDisamt.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmDiscAmt.Location.X, (n * 24))
                        .Name = "ItemDisamt" & n.ToString
                        .Size = New Size(lblItmDiscAmt.Width, 20)
                        .Text = row.Item(5).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                        .TextAlign = HorizontalAlignment.Right
                    End With
                    Me.txtItemDisamt.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemNetamt.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmNetAmt.Location.X, (n * 24))
                        .Name = "ItemNetamt" & n.ToString
                        .Size = New Size(lblItmNetAmt.Width, 20)
                        .Text = (Convert.ToDouble(row.Item(2).ToString) * Convert.ToDouble(row.Item(3).ToString)) - Convert.ToDouble(row.Item(5).ToString)  'row.Item(6).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                        .TextAlign = HorizontalAlignment.Right
                    End With
                    Me.txtItemNetamt.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    n = txtItemAddval.Count + 1
                    txt = New TextBox
                    With txt
                        .Location = New Point(lblItmAddValue.Location.X, (n * 24))
                        .Name = "ItemAddval" & n.ToString
                        .Size = New Size(lblItmAddValue.Width, 20)
                        .Text = row.Item(7).ToString
                        .ReadOnly = True
                        .BackColor = Color.White
                        .TextAlign = HorizontalAlignment.Right
                    End With
                    Me.txtItemAddval.Add(txt)
                    Me.pnlItemDetails.Controls.Add(txt)

                    Dim pic As PictureBox
                    n = picItemDel.Count + 1
                    pic = New PictureBox
                    With pic
                        .Location = New Point(lblItmDel.Location.X + lblItmDel.Width / 4, (n * 24))
                        .Name = "ItemDel" & n.ToString
                        .Size = New Size(lblItmDel.Width - lblItmDel.Width / 2, 20)
                    End With
                    Me.picItemDel.Add(pic)
                    pic.Image = My.Resources.recycle_full
                    pic.SizeMode = PictureBoxSizeMode.StretchImage
                    pic.Cursor = Cursors.Hand
                    Dim tt As New ToolTip()
                    tt.SetToolTip(pic, "Cannot Delete")
                    Me.pnlItemDetails.Controls.Add(pic)

                    totalAmount = totalAmount + ((Convert.ToDouble(row.Item(2).ToString) * Convert.ToDouble(row.Item(3).ToString)) - Convert.ToDouble(row.Item(5).ToString)) + Convert.ToDouble(row.Item(7).ToString)

                    txtCustomerCode.Text = row.Item(8).ToString
                    txtCustomerCode_SelectedValueChanged(txtCustomerCode, New System.EventArgs)
                    count = count - 1
                    k = k + 1
                End While
                stQuery = "SELECT nvl(SUM(a.PINVP_FC_VAL),0) as ADVANCE FROM OT_POS_SO_PAYMENT a,OT_SO_HEAD b WHERE b.SOH_SYS_ID=a.PINVP_INVH_SYS_ID AND b.SOH_NO = " & sono
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    totalAdv = totalAdv + Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString)
                End If
            Next

            If Not patnoval = "" Then
                txtPatientNo.Text = patnoval
                patientnovalue = patnoval
            End If
            txtTotalAmount.Text = totalAmount
            txtAdvPaid.Text = totalAdv
            txtBalancePay.Text = totalAmount - totalAdv
            pnlSalesINVBalance.Visible = True
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSOOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If lstviewSOSelected.Items.Count > 0 Then
                Dim i As Integer
                If Not transtype = "Sales Invoice" Then
                    transtype = "Sales Invoice"
                    pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                    Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                    Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                    For z = oldloc To newloc
                        pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                        Threading.Thread.Sleep(1)
                    Next
                    pnlBtnSliderHolder.Enabled = False
                End If

                GetSODetails()
                txtPatientNo_TextChanged(sender, e)

                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = True
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = True
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = True
                Next

                pnlBtnSliderHolder.Enabled = False
                btnAddItem.Enabled = False

                i = pnlSalesOrders.Width
                pnlSalesOrders.BringToFront()
                Do While i > 0
                    pnlSalesOrders.Location = New Point(Me.Width - 2, Me.Height - pnlSalesOrders.Height)
                    'Threading.Thread.Sleep(1)
                    i = i - 2
                Loop
                pnlSalesOrders.Visible = False
                pnlButtonHolder.Visible = True
                pnlButtonHolder.BringToFront()

                Home.ToolStripStatusLabel.Text = "Salesman Code cannot be empty!"
                txtSalesmanCode.Select()
            Else
                MsgBox("Please select atleat one Sales Order!")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnHoldCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next

            For Each ctl As Control In pnlHoldSearch.Controls
                If ctl.Name = "btnHoldAdd" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnHoldAdd"
                    Me.pnlSOSONO.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnHoldOk" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnHoldOk"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnHoldCancel" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnHoldCancel"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            i = pnlHoldInvoice.Width
            pnlHoldInvoice.BringToFront()
            Do While i > 0
                pnlHoldInvoice.Location = New Point(Me.Width - 2, Me.Height - pnlHoldInvoice.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlHoldInvoice.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnSOCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next

            For Each ctl As Control In pnlSOSONO.Controls
                If ctl.Name = "btnSOAdd" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOAdd"
                    Me.pnlSOSONO.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnSOOk" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOOk"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnSOCancel" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOCancel"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnRemoveSO" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnRemoveSO"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlSOlstview.Controls
                If ctl.Name = "btnSOSearch" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOSearch"
                    Me.pnlSOlstview.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            i = pnlSalesOrders.Width
            pnlSalesOrders.BringToFront()
            Do While i > 0
                pnlSalesOrders.Location = New Point(Me.Width - 2, Me.Height - pnlSalesOrders.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlSalesOrders.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSOCanSOLIST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            pnlSOlstview.BringToFront()
            pnlSOSOLISTS.Hide()

            For Each ctl As Control In pnlSOSOLISTS.Controls
                If ctl.Name = "btnSOSelect" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOSelect"
                    Me.pnlSOSOLISTS.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlSOSOLISTS.Controls
                If ctl.Name = "btnSOCanSOLIST" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnSOCanSOLIST"
                    Me.pnlSOSOLISTS.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlSOSONO.Controls
                ctl.Enabled = True
            Next
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSOAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not txtSONO.Text = "" Then
            Try
                Dim StringToCheck As String = txtSONO.Text
                For i = 0 To StringToCheck.Length - 1
                    If Char.IsLetter(StringToCheck.Chars(i)) Then
                        MsgBox("Invalid Sales Order No.")
                        Exit Sub
                    End If
                Next

                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                Dim row As System.Data.DataRow

                For i = 0 To lstviewSOSelected.Items.Count - 1
                    Dim sono As String = lstviewSOSelected.Items.Item(i).SubItems(1).Text
                    If String.Compare(sono, txtSONO.Text, True) = 0 Then
                        MsgBox("Sales Order already added!")
                        Exit Sub
                    End If
                Next

                stQuery = " SELECT SOH_TXN_CODE, SOH_NO, to_char(SOH_DT,'dd-MM-yyyy') as SOH_DT FROM OT_SO_HEAD,OT_INVOICE_REF,OM_TXN_LINK WHERE SOH_COMP_CODE ='" + CompanyCode + "' AND SOH_LOCN_CODE ='" + Location_Code + "' AND TXNL_TXN_CODE ='POSINV' AND SOH_TXN_CODE = TXNL_PREV_TXN_CODE AND SOH_TXN_CODE =INVR_REF_TXN_CODE(+) AND SOH_NO = INVR_REF_NO (+) AND SOH_NO=" + txtSONO.Text + " AND INVR_REF_NO IS NULL AND SOH_NO NOT IN (SELECT SOH_NO FROM OT_POS_SO_HEAD_LOG WHERE SOH_STATUS = 5 ) "
                ds = db.SelectFromTableODBC(stQuery)
                If Not ds.Tables("Table").Rows.Count > 0 Then
                    MsgBox("Sales Order Not found!")
                    Exit Sub
                End If

                stQuery = "select b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,(select sum(SOI_PL_RATE * SOI_QTY) from ot_so_item where soi_soh_sys_id=b.soh_sys_id) as TotalAmt, nvl((select sum(pinvp_fc_val) from ot_pos_so_payment where pinvp_invh_sys_id=b.soh_sys_id),0) as Advance,nvl((select SUM(ITED_FC_AMT) from OT_SO_ITEM_TED,OT_SO_HEAD where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')  AND SOH_NO=" + txtSONO.Text + ")   ,0) as EXPENSE,nvl((select SUM(ITED_FC_AMT) from OT_SO_ITEM_TED,OT_SO_HEAD where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')  AND SOH_NO=" + txtSONO.Text + ")   ,0) as DISCOUNT from ot_so_head b where b.soh_no =" + txtSONO.Text
                errLog.WriteToErrorLog("Sales Order Search Query", stQuery, "")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "SALES ORDER SEARCH QUERY  - " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    row = ds.Tables("Table").Rows.Item(0)
                    Dim listcountt As Integer = lstviewSOSelected.Items.Count
                    lstviewSOSelected.Items.Add(listcountt + 1)
                    lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(0).ToString)
                    lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(1).ToString)
                    lstviewSOSelected.Items(listcountt).SubItems.Add((Convert.ToDouble(row.Item(2).ToString) + Convert.ToDouble(row.Item(4).ToString)) - Convert.ToDouble(row.Item(5).ToString))
                    lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(3).ToString)
                    lstviewSOSelected.Items(listcountt).SubItems.Add(((Convert.ToDouble(row.Item(2).ToString) + Convert.ToDouble(row.Item(4).ToString)) - Convert.ToDouble(row.Item(5).ToString)) - Convert.ToDouble(row.Item(3).ToString))
                Else
                    MsgBox("Sales Order not found!")
                    Exit Sub
                End If
            Catch ex As Exception
                qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try
        Else
            MsgBox("Please enter Sales Order No.!")
        End If
    End Sub

    Private Sub lstviewSOSelected_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSOSelected.DoubleClick
        Try
            If lstviewSOSelected.SelectedItems.Count < 1 Then
                MsgBox("Please select a sales order")
                Exit Sub
            End If

            Dim soSelectrowno As String = lstviewSOSelected.SelectedItems.Item(0).SubItems(0).Text
            Dim soSelectsono As String = lstviewSOSelected.SelectedItems.Item(0).SubItems(1).Text
            Dim result As DialogResult = MsgBox("Do you want to remove Salesorder - " + soSelectsono + " ?", MessageBoxButtons.YesNo, "Alert")
            If result = Windows.Forms.DialogResult.Yes Then
                Dim lstSo As New List(Of List(Of String))
                For i = 0 To lstviewSOSelected.Items.Count - 1
                    Dim sorow_no As String = lstviewSOSelected.Items.Item(i).SubItems(0).Text
                    If Not String.Compare(sorow_no, soSelectrowno, True) = 0 Then
                        Dim lst As New List(Of String)
                        lst.Add(lstSo.Count + 1)
                        lst.Add(lstviewSOSelected.Items.Item(i).SubItems(1).Text)
                        lst.Add(lstviewSOSelected.Items.Item(i).SubItems(2).Text)
                        lst.Add(lstviewSOSelected.Items.Item(i).SubItems(3).Text)
                        lst.Add(lstviewSOSelected.Items.Item(i).SubItems(4).Text)
                        lst.Add(lstviewSOSelected.Items.Item(i).SubItems(5).Text)
                        lstSo.Add(lst)
                    End If
                Next

                lstviewSOSelected.Items.Clear()

                For i = 0 To lstSo.Count - 1
                    Dim lst As New List(Of String)
                    lst = lstSo(i)
                    lstviewSOSelected.Items.Add(lst(0).ToString)
                    lstviewSOSelected.Items(i).SubItems.Add(lst(1).ToString)
                    lstviewSOSelected.Items(i).SubItems.Add(lst(2).ToString)
                    lstviewSOSelected.Items(i).SubItems.Add(lst(3).ToString)
                    lstviewSOSelected.Items(i).SubItems.Add(lst(4).ToString)
                    lstviewSOSelected.Items(i).SubItems.Add(lst(5).ToString)
                Next
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnRemoveSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            lstviewSOSelected_DoubleClick(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSOFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim i As Integer = 0
            stQuery = " SELECT SOH_TXN_CODE, SOH_NO, to_char(SOH_DT,'dd-MM-yyyy') as SOH_DT FROM OT_SO_HEAD,OT_INVOICE_REF,OM_TXN_LINK WHERE SOH_COMP_CODE ='" + CompanyCode + "' AND SOH_LOCN_CODE ='" + Location_Code + "' AND TXNL_TXN_CODE ='POSINV' AND SOH_TXN_CODE = TXNL_PREV_TXN_CODE AND SOH_TXN_CODE =INVR_REF_TXN_CODE(+) AND SOH_NO = INVR_REF_NO (+) AND SOH_DT >= TO_DATE('" + dtpickSOFromDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND SOH_DT <= TO_DATE('" + dtpickSOToDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND INVR_REF_NO IS NULL AND SOH_NO NOT IN (SELECT SOH_NO FROM OT_POS_SO_HEAD_LOG WHERE SOH_STATUS = 5 ) ORDER BY SOH_NO DESC "
            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("SO Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SO QUERY  - " & stQuery)
            count = ds.Tables("Table").Rows.Count
            lstviewSO.Items.Clear()
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewSO.Items.Add(i + 1)
                lstviewSO.Items(i).SubItems.Add(row.Item(1).ToString)
                lstviewSO.Items(i).SubItems.Add(row.Item(2).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "ERROR")
        End Try
    End Sub

    Private Sub lstviewSO_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSO.DoubleClick
        Try
            If lstviewSO.SelectedItems.Count < 1 Then
                MsgBox("Please select a sales order")
                Exit Sub
            End If
            Dim soSelectsono As String = lstviewSO.SelectedItems.Item(0).SubItems(1).Text
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow

            For i = 0 To lstviewSOSelected.Items.Count - 1
                Dim sono As String = lstviewSOSelected.Items.Item(i).SubItems(1).Text
                If String.Compare(sono, soSelectsono, True) = 0 Then
                    MsgBox("Sales Order already added!")
                    Exit Sub
                End If
            Next

            'stQuery = "select b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,(select sum(SOI_FC_VAL_AFT_H_DISC) from ot_so_item where soi_soh_sys_id=b.soh_sys_id) as TotalAmt, nvl((select sum(pinvp_fc_val) from ot_pos_so_payment where pinvp_invh_sys_id=b.soh_sys_id),0) as Advance from ot_so_head b where b.soh_no =" + soSelectsono
            stQuery = "select b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,(select sum(SOI_FC_VAL) from ot_so_item where soi_soh_sys_id=b.soh_sys_id) as TotalAmt, nvl((select sum(pinvp_fc_val) from ot_pos_so_payment where pinvp_invh_sys_id=b.soh_sys_id),0) as Advance,nvl((select SUM(ITED_FC_AMT) from OT_SO_ITEM_TED,OT_SO_HEAD where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')  AND SOH_NO=" + soSelectsono + ")   ,0) as EXPENSE,nvl((select SUM(ITED_FC_AMT) from OT_SO_ITEM_TED,OT_SO_HEAD where ITED_H_SYS_ID=SOH_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')  AND SOH_NO=" + soSelectsono + ")   ,0) as DISCOUNT from ot_so_head b where b.soh_no =" + soSelectsono
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(0)
                Dim listcountt As Integer = lstviewSOSelected.Items.Count
                lstviewSOSelected.Items.Add(listcountt + 1)
                lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(0).ToString)
                lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(1).ToString)
                lstviewSOSelected.Items(listcountt).SubItems.Add((Convert.ToDouble(row.Item(2).ToString) + Convert.ToDouble(row.Item(4).ToString)) - Convert.ToDouble(row.Item(5).ToString))
                lstviewSOSelected.Items(listcountt).SubItems.Add(row.Item(3).ToString)
                lstviewSOSelected.Items(listcountt).SubItems.Add(((Convert.ToDouble(row.Item(2).ToString) + Convert.ToDouble(row.Item(4).ToString)) - Convert.ToDouble(row.Item(5).ToString)) - Convert.ToDouble(row.Item(3).ToString))
                btnSOCanSOLIST_Click(sender, e)
            Else
                MsgBox("Sales Order not found!")
                Exit Sub
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "ERROR")
        End Try
    End Sub

    Private Sub btnSOSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            lstviewSO_DoubleClick(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnSalesReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesReturn.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If INVHNO > 0 Then
                MsgBox("Please cancel the current Invoice!")
                Exit Sub
            ElseIf SOHNO > 0 Then
                MsgBox("Please cancel the current Sales Order!")
                Exit Sub
            ElseIf transtype = "Sales Invoice" Then
                MsgBox("Please cancel the current Sales Invoice!")
                Exit Sub
            ElseIf txtCustomerName.Text = "" Then
                MsgBox("Please select Customer!")
                Exit Sub
            ElseIf txtSalesmanName.Text = "" Then
                MsgBox("Please select Salesman!")
                Exit Sub
            Else
                For Each ctl As Control In pnlINVDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlItemDetails.Controls
                    ctl.Enabled = False
                Next
                For Each ctl As Control In pnlBottomHolder.Controls
                    ctl.Enabled = False
                Next

                'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
                pnlSalesReturn.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlSalesReturn.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlSalesReturn.Width
                    pnlSalesReturn.Location = New Point(Me.Width - i, Me.Height - pnlSalesReturn.Height)
                    pnlSalesReturn.Show()
                    'Threading.Thread.Sleep(0.5)
                    i = i + 5
                Next i
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnSRAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSRAdd.Click

        If txtSRTransNo.Text = "" Then
            MsgBox("Enter a valid Sales Return No.")
            Exit Sub
        End If
        Dim StringToCheck As String = txtSRTransNo.Text
        For i = 0 To StringToCheck.Length - 1
            If Char.IsLetter(StringToCheck.Chars(i)) Then
                MsgBox("Invalid Sales Return No.")
                Exit Sub
            End If
        Next
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim i As Integer = 0
            lstviewSRDetails.Items.Clear()
            stQuery = "SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO =" + txtSRTransNo.Text
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
            Else
                MsgBox("Transaction not found!")
                Exit Sub
            End If
            stQuery = "SELECT INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE, A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU QTY FROM OT_INVOICE_ITEM A, OT_CUST_SALE_RET_ITEM B WHERE "
            stQuery = stQuery + "A.INVI_ITEM_CODE = B.CSRI_ITEM_CODE AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ") AND "
            stQuery = stQuery + "CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO = " + txtSRTransNo.Text + ")  "
            stQuery = stQuery + "GROUP BY INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE,A.INVI_QTY_BU,A.INVI_CSRI_QTY_BU HAVING A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU > 0 "
            stQuery = stQuery + "UNION "
            stQuery = stQuery + "SELECT INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE, INVI_QTY_BU QTY FROM OT_INVOICE_ITEM A WHERE "
            stQuery = stQuery + "A.INVI_ITEM_CODE NOT IN (SELECT CSRI_ITEM_CODE FROM OT_CUST_SALE_RET_ITEM B WHERE CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO =" + txtSRTransNo.Text + ")) AND "
            stQuery = stQuery + "INVI_QTY_BU > 0 AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ")"
            errLog.WriteToErrorLog("Sales Return Add Query ", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SALES RETURN ADD QUERY  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            If count > 0 Then
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstviewSRDetails.Items.Add(i + 1)
                    lstviewSRDetails.Items(i).SubItems.Add(row.Item(1).ToString)
                    lstviewSRDetails.Items(i).SubItems.Add(row.Item(2).ToString)
                    lstviewSRDetails.Items(i).SubItems.Add(row.Item(4).ToString)
                    lstviewSRDetails.Items(i).SubItems.Add(row.Item(3).ToString)
                    count = count - 1
                    i = i + 1
                End While
            Else
                MsgBox("Sales Return already made for this transaction!")
                Exit Sub
            End If
            Dim totCtl As Integer = txtItemCode.Count
            While totCtl > 0
                txtItemCode.RemoveAt(totCtl - 1)
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDesc.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDesc" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemQty.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemQty" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemPrice.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemPrice" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDisc.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDisc" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemDisamt.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDisamt" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemNetamt.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemNetamt" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                txtItemAddval.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemAddval" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                picItemDel.RemoveAt(totCtl - 1)
                objectsToBeFound = Me.Controls.Find("ItemDel" + (totCtl).ToString, True)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                totCtl = totCtl - 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnSROK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSROK.Click
        Try
            lstviewSRDetails_DoubleClick(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub lstviewSRDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewSRDetails.DoubleClick
        Try
            If lstviewSRDetails.SelectedItems.Count < 1 Then
                MsgBox("Please select an Item!")
                Exit Sub
            End If

            Dim SRSelectItem As String = lstviewSRDetails.SelectedItems.Item(0).SubItems(1).Text
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim i As Integer = 0
            'Dim row As System.Data.DataRow



            stQuery = "select invh_flex_03 from ot_invoice_head where invh_no='" & txtSRTransNo.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then

                txtPatientNo.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString

                txtPatientNo_TextChanged(sender, e)
            End If


            For k = 1 To txtItemCode.Count
                Dim ItmCode_name As String = "ItemCode" & k
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                If String.Compare(ItmCodeFound(0).Text, SRSelectItem, True) = 0 Then
                    Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & k, True)

                    For Each ctl As Control In pnlINVDetails.Controls
                        ctl.Enabled = True
                    Next
                    For Each ctl As Control In pnlItemDetails.Controls
                        ctl.Enabled = True
                    Next
                    For Each ctl As Control In pnlBottomHolder.Controls
                        ctl.Enabled = True
                    Next
                    ItmQtyFound(0).Select()
                    i = pnlSalesReturn.Width
                    pnlSalesReturn.BringToFront()
                    Do While i > 0
                        pnlSalesReturn.Location = New Point(Me.Width - 2, Me.Height - pnlSalesReturn.Height)
                        'Threading.Thread.Sleep(1)
                        i = i - 2
                    Loop
                    pnlSalesReturn.Visible = False
                    pnlButtonHolder.Visible = True
                    pnlButtonHolder.BringToFront()
                    btnAddItem.Enabled = False
                    Exit Sub

                End If
            Next

            Dim TEDCODE As String = ""
            Dim TEDAMOUNT As Double = 0
            Dim TEDEXPENSE As Double = 0
            Dim netAmt As Double = 0
            stQuery = "SELECT ITED_TED_CODE,ITED_TED_RATE,ITED_FC_AMT from OT_INVOICE_ITEM A, OT_INVOICE_HEAD B, OT_INVOICE_ITEM_TED C where INVH_SYS_ID = INVI_INVH_SYS_ID AND ITED_I_SYS_ID =INVI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')  AND INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + " AND INVI_ITEM_CODE='" + SRSelectItem + "'"
            errLog.WriteToErrorLog("SARTN TEDDIS", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SARTN TEDDIS  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                TEDCODE = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                'MsgBox(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(1).ToString))
                TEDAMOUNT = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(2).ToString), 3)
            End If

            stQuery = "SELECT ITED_TED_CODE,ITED_TED_RATE,ITED_FC_AMT from OT_INVOICE_ITEM A, OT_INVOICE_HEAD B, OT_INVOICE_ITEM_TED C where INVH_SYS_ID = INVI_INVH_SYS_ID AND ITED_I_SYS_ID =INVI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')  AND INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + " AND INVI_ITEM_CODE='" + SRSelectItem + "'"
            errLog.WriteToErrorLog("SARTN TEDEXP", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SARTN TEDEXP  - " & stQuery)

            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                TEDEXPENSE = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(2).ToString), 3)
            End If

            netAmt = Round(((Convert.ToDouble(lstviewSRDetails.SelectedItems.Item(0).SubItems(4).Text) * Convert.ToDouble(lstviewSRDetails.SelectedItems.Item(0).SubItems(3).Text)) - TEDAMOUNT), 3)

            Me.pnlItemDetails.AutoScrollPosition = New System.Drawing.Point(0, 0)
            Dim txt As TextBox
            Dim n As Integer
            n = txtItemCode.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmHead.Location.X, (n * 24))
                .Name = "ItemCode" & n.ToString
                .Size = New Size(lblItmHead.Width, 20)
                .Text = SRSelectItem
                .ReadOnly = True
                .BackColor = Color.White
            End With
            Me.txtItemCode.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemDesc.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmDesc.Location.X, (n * 24))
                .Name = "ItemDesc" & n.ToString
                .Size = New Size(lblItmDesc.Width, 20)
                .Text = lstviewSRDetails.SelectedItems.Item(0).SubItems(2).Text
                .ReadOnly = True
                .BackColor = Color.White
            End With
            Me.txtItemDesc.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemQty.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmQty.Location.X, (n * 24))
                .Name = "ItemQty" & n.ToString
                .Size = New Size(lblItmQty.Width, 20)
                .Text = Convert.ToDouble(lstviewSRDetails.SelectedItems.Item(0).SubItems(3).Text)
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Center
                .Select()
            End With
            AddHandler txt.KeyPress, AddressOf Me.FindItmQty_KeyPress
            AddHandler txt.Leave, AddressOf Me.FindItmQty_Leave
            'AddHandler txt.KeyDown, AddressOf Me.FindItemQty
            AddHandler txt.TextChanged, AddressOf Me.FindItmQtySR_TextChanged

            Me.txtItemQty.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemPrice.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmPrice.Location.X, (n * 24))
                .Name = "ItemPrice" & n.ToString
                .Size = New Size(lblItmPrice.Width, 20)
                .Text = lstviewSRDetails.SelectedItems.Item(0).SubItems(4).Text
                .ReadOnly = True
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Right
            End With
            Me.txtItemPrice.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemDisc.Count + 1
            Dim txtcmb As ComboBox
            txtcmb = New ComboBox
            With txtcmb
                .Location = New Point(lblItmDiscCode.Location.X, (n * 24))
                .Name = "ItemDisc" & n.ToString
                .Size = New Size(lblItmDiscCode.Width, 20)
                .Text = TEDCODE
                .Enabled = True
                .BackColor = Color.White
            End With
            Me.txtItemDisc.Add(txtcmb)
            Me.pnlItemDetails.Controls.Add(txtcmb)

            n = txtItemDisamt.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmDiscAmt.Location.X, (n * 24))
                .Name = "ItemDisamt" & n.ToString
                .Size = New Size(lblItmDiscAmt.Width, 20)
                .Text = TEDAMOUNT.ToString
                .ReadOnly = True
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Right
            End With
            Me.txtItemDisamt.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemNetamt.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmNetAmt.Location.X, (n * 24))
                .Name = "ItemNetamt" & n.ToString
                .Size = New Size(lblItmNetAmt.Width, 20)
                .Text = netAmt.ToString
                .ReadOnly = True
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Right
            End With
            Me.txtItemNetamt.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            n = txtItemAddval.Count + 1
            txt = New TextBox
            With txt
                .Location = New Point(lblItmAddValue.Location.X, (n * 24))
                .Name = "ItemAddval" & n.ToString
                .Size = New Size(lblItmAddValue.Width, 20)
                .Text = TEDEXPENSE.ToString 
                .ReadOnly = True
                .BackColor = Color.White
                .TextAlign = HorizontalAlignment.Right
            End With
            Me.txtItemAddval.Add(txt)
            Me.pnlItemDetails.Controls.Add(txt)

            Dim pic As PictureBox
            n = picItemDel.Count + 1
            pic = New PictureBox
            With pic
                .Location = New Point(lblItmDel.Location.X + lblItmDel.Width / 4, (n * 24))
                .Name = "ItemDel" & n.ToString
                .Size = New Size(lblItmDel.Width - lblItmDel.Width / 2, 20)
            End With
            Me.picItemDel.Add(pic)
            pic.Image = My.Resources.recycle_full
            pic.SizeMode = PictureBoxSizeMode.StretchImage
            pic.Cursor = Cursors.Hand
            Dim tt As New ToolTip()
            tt.SetToolTip(pic, "Delete")
            Me.pnlItemDetails.Controls.Add(pic)
            AddHandler pic.Click, AddressOf Me.RemoveItemRowSR
            Calculate_TotalAmount()
            transtype = "Sales Return"

            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            i = pnlSalesReturn.Width
            pnlSalesReturn.BringToFront()
            Do While i > 0
                pnlSalesReturn.Location = New Point(Me.Width - 2, Me.Height - pnlSalesReturn.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlSalesReturn.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()

            If not btnCancelInvoice.Text = "Cancel Return" Then
                picboxTransactionType.BackgroundImage = My.Resources.salesreturn
                btnCancelInvoice.Text = "Cancel Return"
                Threading.Thread.Sleep(10)
                pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                For z = oldloc To newloc
                    pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                    'Threading.Thread.Sleep(1)
                Next
                pnlBtnSliderHolder.Enabled = False
                btnAddItem.Enabled = False
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try

    End Sub

    Private Sub FindItmQtySR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim tbx As System.Windows.Forms.TextBox = sender
            If tbx.Text = "" Or tbx.TextAlign = "0" Then
                Exit Sub
            Else

                Dim qty As Double = Convert.ToDouble(tbx.Text.ToString)
                Dim price As Double
                Dim disamt As Double
                Dim addval As Double
                Dim text As String = DirectCast(sender, TextBox).Name
                Dim parts As String() = text.Split(New String() {"ItemQty"}, StringSplitOptions.None)
                Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
                Dim itemCode As String = ItmCodeFound(0).Text

                Dim stQuery As String
                Dim ds As DataSet
                Dim count As Integer
                'Dim i As Integer
                'Dim row As System.Data.DataRow

                Dim TEDAMOUNT As Double = 0
                Dim netAmt As Double = 0

                stQuery = "SELECT INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_RATE, A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU QTY FROM OT_INVOICE_ITEM A, OT_CUST_SALE_RET_ITEM B WHERE "
                stQuery = stQuery + "A.INVI_ITEM_CODE = B.CSRI_ITEM_CODE AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ") AND "
                stQuery = stQuery + "CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO = " + txtSRTransNo.Text + ")  AND INVI_ITEM_CODE='" + itemCode + "' "
                stQuery = stQuery + "GROUP BY INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_RATE,A.INVI_QTY_BU,A.INVI_CSRI_QTY_BU HAVING A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU > 0 "
                stQuery = stQuery + "UNION "
                stQuery = stQuery + "SELECT INVI_SYS_ID, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_RATE, INVI_QTY_BU QTY FROM OT_INVOICE_ITEM A WHERE INVI_ITEM_CODE='" + itemCode + "' AND "
                stQuery = stQuery + "A.INVI_ITEM_CODE NOT IN (SELECT CSRI_ITEM_CODE FROM OT_CUST_SALE_RET_ITEM B WHERE CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO =" + txtSRTransNo.Text + ")) AND "
                stQuery = stQuery + "INVI_QTY_BU > 0 AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + ")"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    Dim totQty As Integer = Convert.ToInt64(ds.Tables("Table").Rows.Item(0).Item(4).ToString)
                    If qty > totQty Then
                        MsgBox("Quantity exceeds the return limit")
                        tbx.Text = 1
                        Exit Sub
                    End If
                Else
                    MsgBox("Sales Return cannot be made for this item!")
                    Exit Sub
                End If

                Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
                Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
                price = Convert.ToDouble(ItmPriceFound(0).Text.ToString)
                stQuery = "SELECT ITED_TED_CODE,ITED_TED_RATE,ITED_FC_AMT from OT_INVOICE_ITEM A, OT_INVOICE_HEAD B, OT_INVOICE_ITEM_TED C where INVH_SYS_ID = INVI_INVH_SYS_ID AND ITED_I_SYS_ID =INVI_SYS_ID AND ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')  AND INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = " + txtSRTransNo.Text + " AND INVI_ITEM_CODE='" + itemCode + "'"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    'TEDAMOUNT = Round(((Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(1).ToString) * price) / 100) * qty, 0)
                    TEDAMOUNT = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(2).ToString), 3)
                End If

                Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
                ItmDisamtFound(0).Text = TEDAMOUNT
                disamt = TEDAMOUNT
                Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
                Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
                addval = Convert.ToDouble(ItmAddvalFound(0).Text.ToString)
                Dim ItmNetamt_name As String = "ItemNetamt" & parts(1).ToString
                Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmNetamt_name, True)

                Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
                Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
                Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
                Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
                ItmNetamtFound(0).Text = Round((((qty * price) - disamt)), 3).ToString
                'MsgBox(alreadyAddedItemCount)
                Calculate_TotalAmount()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub RemoveItemRowSR(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim text As String = DirectCast(sender, PictureBox).Name
            Dim parts As String() = text.Split(New String() {"ItemDel"}, StringSplitOptions.None)
            Dim rowPosition As Integer = Convert.ToInt64(parts(1).ToString)
            Dim rowsCount As Integer = picItemDel.Count
            If rowPosition = rowsCount Then
                Dim itmcode_name As String = "ItemCode" & parts(1).ToString
                Dim itmdesc_name As String = "ItemDesc" & parts(1).ToString
                Dim itmqty_name As String = "ItemQty" & parts(1).ToString
                Dim itmprice_name As String = "ItemPrice" & parts(1).ToString
                Dim itmdisc_name As String = "ItemDisc" & parts(1).ToString
                Dim itmdisamt_name As String = "ItemDisamt" & parts(1).ToString
                Dim itmnetamt_name As String = "ItemNetamt" & parts(1).ToString
                Dim itmaddval_name As String = "ItemAddval" & parts(1).ToString
                Dim itmdel_name As String = "ItemDel" & parts(1).ToString
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find(itmcode_name, True)
                txtItemCode.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdesc_name, True)
                txtItemDesc.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmqty_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemQty.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmprice_name, True)
                txtItemPrice.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisc_name, True)
                txtItemDisc.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisamt_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemDisamt.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmnetamt_name, True)
                txtItemNetamt.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddval_name, True)
                'objectsToBeFound(0).Text = ""
                txtItemAddval.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdel_name, True)
                picItemDel.RemoveAt(rowPosition - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))

                Calculate_TotalAmount()
                'ElseIf rowPosition = 1 Then

            Else

                For i = rowPosition + 1 To rowsCount

                    Dim itmdesc_name_into As String = "ItemDesc" & i - 1
                    Dim itmdesc_name_from As String = "ItemDesc" & i
                    Dim objectsToBeFound_From As System.Windows.Forms.Control() = Me.Controls.Find(itmdesc_name_from, True)

                    Dim objectsToBeFound_Into As System.Windows.Forms.Control() = Me.Controls.Find(itmdesc_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmcode_name_into As String = "ItemCode" & i - 1
                    Dim itmcode_name_from As String = "ItemCode" & i
                    objectsToBeFound_From = Me.Controls.Find(itmcode_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmcode_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmqty_name_into As String = "ItemQty" & i - 1
                    Dim itmqty_name_from As String = "ItemQty" & i
                    objectsToBeFound_From = Me.Controls.Find(itmqty_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmqty_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmprice_name_into As String = "ItemPrice" & i - 1
                    Dim itmprice_name_from As String = "ItemPrice" & i
                    objectsToBeFound_From = Me.Controls.Find(itmprice_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmprice_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmdisc_name_into As String = "ItemDisc" & i - 1
                    Dim itmdisc_name_from As String = "ItemDisc" & i
                    objectsToBeFound_From = Me.Controls.Find(itmdisc_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmdisc_name_into, True)
                    Dim tbxinto As System.Windows.Forms.TextBox = objectsToBeFound_Into(0)
                    Dim tbxfrom As System.Windows.Forms.TextBox = objectsToBeFound_From(0)
                    tbxinto.AutoCompleteCustomSource = tbxfrom.AutoCompleteCustomSource
                    tbxinto.AutoCompleteSource = tbxfrom.AutoCompleteSource
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmdisamt_name_into As String = "ItemDisamt" & i - 1
                    Dim itmdisamt_name_from As String = "ItemDisamt" & i
                    objectsToBeFound_From = Me.Controls.Find(itmdisamt_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmdisamt_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmnetamt_name_into As String = "ItemNetamt" & i - 1
                    Dim itmnetamt_name_from As String = "ItemNetamt" & i
                    objectsToBeFound_From = Me.Controls.Find(itmnetamt_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmnetamt_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                    Dim itmaddval_name_into As String = "ItemAddval" & i - 1
                    Dim itmaddval_name_from As String = "ItemAddval" & i
                    objectsToBeFound_From = Me.Controls.Find(itmaddval_name_from, True)
                    objectsToBeFound_Into = Me.Controls.Find(itmaddval_name_into, True)
                    objectsToBeFound_Into(0).Text = objectsToBeFound_From(0).Text

                Next

                Dim itmcode_name As String = "ItemCode" & txtItemCode.Count
                Dim itmdesc_name As String = "ItemDesc" & txtItemDesc.Count
                Dim itmqty_name As String = "ItemQty" & txtItemQty.Count
                Dim itmprice_name As String = "ItemPrice" & txtItemPrice.Count
                Dim itmdisc_name As String = "ItemDisc" & txtItemDisc.Count
                Dim itmdisamt_name As String = "ItemDisamt" & txtItemDisamt.Count
                Dim itmnetamt_name As String = "ItemNetamt" & txtItemNetamt.Count
                Dim itmaddval_name As String = "ItemAddval" & txtItemAddval.Count
                Dim itmdel_name As String = "ItemDel" & picItemDel.Count
                Dim objectsToBeFound As System.Windows.Forms.Control() = Me.Controls.Find(itmcode_name, True)
                txtItemCode.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdesc_name, True)
                txtItemDesc.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmqty_name, True)
                txtItemQty.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmprice_name, True)
                txtItemPrice.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisc_name, True)
                txtItemDisc.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdisamt_name, True)
                txtItemDisamt.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmnetamt_name, True)
                txtItemNetamt.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmaddval_name, True)
                txtItemAddval.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))
                objectsToBeFound = Me.Controls.Find(itmdel_name, True)
                picItemDel.RemoveAt(rowsCount - 1)
                Me.pnlItemDetails.Controls.Remove(objectsToBeFound(0))

                Calculate_TotalAmount()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnSRCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSRCancel.Click
        Try
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            Dim i As Integer = 0
            i = pnlSalesReturn.Width
            pnlSalesReturn.BringToFront()
            Do While i > 0
                pnlSalesReturn.Location = New Point(Me.Width - 2, Me.Height - pnlSalesReturn.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlSalesReturn.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnCancelRoyalty_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            i = pnlRoyalty.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlRoyalty.Location = New Point(Me.Width - 2, Me.Height - pnlRoyalty.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlRoyalty.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnRCAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtRCCardName.Text = "" Then
                MsgBox("Please select a valid Royalty Card Type")
                Exit Sub
            End If
            If txtRCCardNo.Text = "" Then
                MsgBox("Please enter the Royalty Card Number")
                Exit Sub
            End If
            'lstRCDetail.Items.Add("1", 2)
            'lstRCDetail.Items.Add(txtRCCardType.Text.ToString, 80)
            'lstRCDetail.Items.Add(txtRCCardName.Text.ToString, 2000)
            'lstRCDetail.Items.Add(txtRCCardNo.Text.ToString, 5000)
            Dim count As Integer = lstRCDetail.Items.Count
            lstRCDetail.Items.Add(count + 1)
            lstRCDetail.Items(count).SubItems.Add(txtRCCardType.Text.Split(" - ")(0))
            lstRCDetail.Items(count).SubItems.Add(txtRCCardName.Text)
            lstRCDetail.Items(count).SubItems.Add(txtRCCardNo.Text)
            txtRCCardType.Text = ""
            txtRCCardName.Text = ""
            txtRCCardNo.Text = ""

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnRCOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each ctl As Control In pnlRoyaltySearch.Controls
                If ctl.Name = "btnRCAdd" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnRCAdd"
                    Me.pnlRoyaltySearch.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In pnlRoyaltySearch.Controls
                If ctl.Name = "btnRCOk" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnRCOk"
                    Me.pnlRoyaltySearch.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            Dim i As Integer
            i = pnlRoyalty.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlRoyalty.Location = New Point(Me.Width - 2, Me.Height - pnlRoyalty.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlRoyalty.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnReferal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferal.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                ItmCodeFound(0).Select()
                Exit Sub
            Else

                Dim ds As DataSet
                Dim row As System.Data.DataRow
                Dim stQuery As String
                Dim count As Integer
                Dim i As Integer = 0
                stQuery = "SELECT VSSV_CODE,VSSV_NAME,VSSV_BL_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='REF_HOSPITAL' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S')"
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                i = 0
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    'Royalty_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                    Referal_Codes.Add(row.Item(0).ToString)
                    txtRefHospid.Items.Add(row.Item(0).ToString & " - " & row.Item(1).ToString)
                    count = count - 1
                    i = i + 1
                End While


                Dim btn As Button = New Button
                With btn
                    .Name = "btnReferalAdd"
                    .Location = New Point(txtRefDocName.Location.X, txtRefDocName.Location.Y + 40)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Add"
                End With
                AddHandler btn.Click, AddressOf Me.btnReferalAdd_Click
                Me.Pnl_Refferal_hospdetails.Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnReferalOK"
                    .Location = New Point(txtRefDocName.Location.X + 70, txtRefDocName.Location.Y + 40)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Close"
                End With
                AddHandler btn.Click, AddressOf Me.btnReferalOK_Click
                Me.Pnl_Refferal_hospdetails.Controls.Add(btn)

                pnlReferal.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlReferal.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlReferal.Width
                    pnlReferal.Location = New Point(Me.Width - i, Me.Height - pnlReferal.Height)
                    pnlReferal.Show()
                    Threading.Thread.Sleep(0.5)
                    i = i + 1
                Next
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnReferalOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each ctl As Control In Pnl_Refferal_hospdetails.Controls
                If ctl.Name = "btnReferalAdd" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnReferalAdd"
                    Me.Pnl_Refferal_hospdetails.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In Pnl_Refferal_hospdetails.Controls
                If ctl.Name = "btnReferalOK" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnReferalOK"
                    Me.Pnl_Refferal_hospdetails.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            Dim i As Integer
            i = pnlReferal.Width
            pnlReferal.BringToFront()
            Do While i > 0
                pnlReferal.Location = New Point(Me.Width - 2, Me.Height - pnlReferal.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlReferal.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnInsuranceOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each ctl As Control In Pnl_Insurance_Details.Controls
                If ctl.Name = "tbnInsuranceAdd" Then
                    Dim btnrem As New Button
                    btnrem.Name = "tbnInsuranceAdd"
                    Me.Pnl_Insurance_Details.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In Pnl_Insurance_Details.Controls
                If ctl.Name = "btnInsuranceOK" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnInsuranceOK"
                    Me.Pnl_Insurance_Details.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            Dim i As Integer
            i = pnlInsurance.Width
            pnlInsurance.BringToFront()
            Do While i > 0
                pnlInsurance.Location = New Point(Me.Width - 2, Me.Height - pnlInsurance.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlInsurance.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnInsuranceCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            i = pnlInsurance.Width
            pnlInsurance.BringToFront()
            Do While i > 0
                pnlInsurance.Location = New Point(Me.Width - 2, Me.Height - pnlInsurance.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlInsurance.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnInsurance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsurance.Click
        If Not CheckShiftTimings() Then
            MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
            Exit Sub
        End If
        Try
            If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode1", True)
                Home.ToolStripStatusLabel.Text = "Item Cart is empty!"
                ItmCodeFound(0).Select()
                Exit Sub
            Else

                Dim btn As Button = New Button
                With btn
                    .Name = "tbnInsuranceAdd"
                    .Location = New Point(txtInsuranceNo.Location.X + txtInsuranceNo.Width + 100, txtInsuranceNo.Location.Y + 15)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Add"
                End With
                AddHandler btn.Click, AddressOf Me.tbnInsuranceAdd_Click
                Me.Pnl_Insurance_Details.Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnInsuranceOK"
                    .Location = New Point(txtInsuranceNo.Location.X + txtInsuranceNo.Width + 170, txtInsuranceNo.Location.Y + 15)
                    .Size = New Size(60, 23)
                    .BackColor = Color.MediumTurquoise
                    .Text = "Close"
                End With
                AddHandler btn.Click, AddressOf Me.btnInsuranceOK_Click
                Me.Pnl_Insurance_Details.Controls.Add(btn)

                pnlInsurance.Height = Me.Height
                pnlButtonHolder.Visible = False
                pnlButtonHolder.SendToBack()
                pnlInsurance.BringToFront()
                'pnl1Patient.Width = Me.Width
                For i = 0 To pnlInsurance.Width
                    pnlInsurance.Location = New Point(Me.Width - i, Me.Height - pnlInsurance.Height)
                    pnlInsurance.Show()
                    Threading.Thread.Sleep(0.5)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub lstRCDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRCDetail.DoubleClick
        Try
            If lstRCDetail.SelectedItems.Count > 0 Then
                Dim rowid As String = lstRCDetail.SelectedItems.Item(0).SubItems(0).Text

                Dim lst As New List(Of String())

                For i = 0 To lstRCDetail.Items.Count - 1
                    Dim rowItem(3) As String
                    rowItem(0) = lstRCDetail.Items(i).SubItems(0).Text
                    rowItem(1) = lstRCDetail.Items(i).SubItems(1).Text
                    rowItem(2) = lstRCDetail.Items(i).SubItems(2).Text
                    rowItem(3) = lstRCDetail.Items(i).SubItems(3).Text
                    lst.Add(rowItem)
                Next
                lstRCDetail.Items.Clear()
                Dim k As Integer = 0
                For i = 0 To lst.Count - 1
                    Dim rowitem(3) As String
                    rowitem = lst.Item(i)
                    If Not rowitem(0) = rowid Then
                        lstRCDetail.Items.Add(k + 1)
                        lstRCDetail.Items(k).SubItems.Add(rowitem(1))
                        lstRCDetail.Items(k).SubItems.Add(rowitem(2))
                        lstRCDetail.Items(k).SubItems.Add(rowitem(3))
                        k = k + 1
                    End If
                Next

                MsgBox("Successfully removed!")
            Else
                MsgBox("Please select a row")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnReferalAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtRefHospName.Text = "" Then
                MsgBox("Please select a valid Hospital")
                Exit Sub
            End If
            If txtRefDocid.Text = "" Then
                MsgBox("Please enter the Doctor ID")
                Exit Sub
            End If
            If txtRefDocName.Text = "" Then
                MsgBox("Please enter the Doctor Name")
                Exit Sub
            End If

            Dim count As Integer = lstviewReferal.Items.Count
            lstviewReferal.Items.Add(count + 1)
            lstviewReferal.Items(count).SubItems.Add(txtRefHospid.Text.Split(" - ")(0))
            lstviewReferal.Items(count).SubItems.Add(txtRefHospName.Text)
            lstviewReferal.Items(count).SubItems.Add(txtRefDocid.Text)
            lstviewReferal.Items(count).SubItems.Add(txtRefDocName.Text)
            txtRefHospid.Text = ""
            txtRefHospName.Text = ""
            txtRefDocid.Text = ""
            txtRefDocName.Text = ""
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub lstviewReferal_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewReferal.DoubleClick
        Try
            If lstviewReferal.SelectedItems.Count > 0 Then
                Dim rowid As String = lstviewReferal.SelectedItems.Item(0).SubItems(0).Text

                Dim lst As New List(Of String())

                For i = 0 To lstviewReferal.Items.Count - 1
                    Dim rowItem(4) As String
                    rowItem(0) = lstviewReferal.Items(i).SubItems(0).Text
                    rowItem(1) = lstviewReferal.Items(i).SubItems(1).Text
                    rowItem(2) = lstviewReferal.Items(i).SubItems(2).Text
                    rowItem(3) = lstviewReferal.Items(i).SubItems(3).Text
                    rowItem(4) = lstviewReferal.Items(i).SubItems(4).Text
                    lst.Add(rowItem)
                Next
                lstviewReferal.Items.Clear()
                Dim k As Integer = 0
                For i = 0 To lst.Count - 1
                    Dim rowitem(4) As String
                    rowitem = lst.Item(i)
                    If Not rowitem(0) = rowid Then
                        lstviewReferal.Items.Add(k + 1)
                        lstviewReferal.Items(k).SubItems.Add(rowitem(1))
                        lstviewReferal.Items(k).SubItems.Add(rowitem(2))
                        lstviewReferal.Items(k).SubItems.Add(rowitem(3))
                        lstviewReferal.Items(k).SubItems.Add(rowitem(4))
                        k = k + 1
                    End If
                Next

                MsgBox("Successfully removed!")
            Else
                MsgBox("Please select a row")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub tbnInsuranceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtInsuranceName.Text = "" Then
                MsgBox("Please select a valid Insurance")
                Exit Sub
            End If
            If txtInsuranceNo.Text = "" Then
                MsgBox("Please enter the Insurance Number")
                Exit Sub
            End If

            Dim count As Integer = lstviewInsurance.Items.Count
            lstviewInsurance.Items.Add(count + 1)
            lstviewInsurance.Items(count).SubItems.Add(txtInsuranceCode.Text)
            lstviewInsurance.Items(count).SubItems.Add(txtInsuranceName.Text)
            lstviewInsurance.Items(count).SubItems.Add(txtInsuranceNo.Text)

            txtInsuranceCode.Text = ""
            txtInsuranceName.Text = ""
            txtInsuranceNo.Text = ""

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub lstviewInsurance_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewInsurance.DoubleClick
        Try
            If lstviewInsurance.SelectedItems.Count > 0 Then
                Dim rowid As String = lstviewInsurance.SelectedItems.Item(0).SubItems(0).Text

                Dim lst As New List(Of String())

                For i = 0 To lstviewInsurance.Items.Count - 1
                    Dim rowItem(3) As String
                    rowItem(0) = lstviewInsurance.Items(i).SubItems(0).Text
                    rowItem(1) = lstviewInsurance.Items(i).SubItems(1).Text
                    rowItem(2) = lstviewInsurance.Items(i).SubItems(2).Text
                    rowItem(3) = lstviewInsurance.Items(i).SubItems(3).Text

                    lst.Add(rowItem)
                Next
                lstviewInsurance.Items.Clear()
                Dim k As Integer = 0
                For i = 0 To lst.Count - 1
                    Dim rowitem(3) As String
                    rowitem = lst.Item(i)
                    If Not rowitem(0) = rowid Then
                        lstviewInsurance.Items.Add(k + 1)
                        lstviewInsurance.Items(k).SubItems.Add(rowitem(1))
                        lstviewInsurance.Items(k).SubItems.Add(rowitem(2))
                        lstviewInsurance.Items(k).SubItems.Add(rowitem(3))

                        k = k + 1
                    End If
                Next

                MsgBox("Successfully removed!")
            Else
                MsgBox("Please select a row")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnLineAddvalue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLineAddvalue.Click
        Dim ItmQtyValFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & lastActiveItem, True)

        If Not ItmQtyValFound(0).Text = "0" Then
            Dim ItmAddValFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" & lastActiveItem, True)
            AddvalueButtonclicked = True
            ItmAddValFound(0).Select()
        End If
    End Sub


    Private Sub btnLineAddValCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each ctl As Control In Pnl_Lineaddvalue_details.Controls
                If ctl.Name = "btnLineAddValOK" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnLineAddValOK"
                    Me.Pnl_Lineaddvalue_details.Controls.Remove(btnrem)
                    Exit For
                End If
            Next
            For Each ctl As Control In Pnl_Lineaddvalue_details.Controls
                If ctl.Name = "btnLineAddValCancel" Then
                    Dim btnrem As New Button
                    btnrem.Name = "btnLineAddValCancel"
                    Me.Pnl_Lineaddvalue_details.Controls.Remove(btnrem)
                    Exit For
                End If
            Next

            Dim i As Integer
            i = pnlLineAddValue.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlLineAddValue.Location = New Point(Me.Width - 2, Me.Height - pnlLineAddValue.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlLineAddValue.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtLineAddValPerc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLineAddValPerc.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then
                If Asc(e.KeyChar) = 46 Then
                    If tbx.Text.IndexOf(".") <> -1 Or Val(tbx.Text.Trim & e.KeyChar) >= 4 Then
                        e.Handled = True
                    Else
                        'e.Handled = False
                        Exit Sub
                    End If
                Else
                    'e.Handled = False
                    Exit Sub
                End If
            End If
            If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtLineAddValPerc_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineAddValPerc.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 2)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    'Private Sub FindItmQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'Dim text1 As String = DirectCast(sender, TextBox).Name
    '        'Dim parts1 As String() = text1.Split(New String() {"ItemQty"}, StringSplitOptions.None)
    '        'lastActiveItem = parts1(1).ToString

    '        If Not CheckShiftTimings() Then
    '            MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
    '            Exit Sub
    '        End If
    '        Dim tbx As System.Windows.Forms.TextBox = sender
    '        If tbx.Text = "" Or tbx.Text = "0" Then
    '            Exit Sub
    '        Else

    '            Dim qty As Double = Convert.ToDouble(tbx.Text.ToString)
    '            Dim price As Double
    '            Dim disamt As Double
    '            Dim addval As Double
    '            Dim text As String = DirectCast(sender, TextBox).Name
    '            Dim parts As String() = text.Split(New String() {"ItemQty"}, StringSplitOptions.None)
    '            Dim ItmCode_name As String = "ItemCode" & parts(1).ToString
    '            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmCode_name, True)
    '            Dim itemCode As String = ItmCodeFound(0).Text
    '            If itemCode = "" Then
    '                Exit Sub
    '            End If
    '            Dim alreadyAddedItemCount As Integer = 0
    '            For k = 1 To txtItemCode.Count
    '                Dim ItmCodeCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" + k.ToString, True)
    '                If String.Compare(ItmCodeCtl(0).Text, itemCode, True) = 0 Then
    '                    Dim ItmQtyCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" + k.ToString, True)
    '                    alreadyAddedItemCount = alreadyAddedItemCount + Convert.ToInt64(ItmQtyCtl(0).Text)
    '                Else
    '                End If
    '            Next

    '            Dim stQuery As String
    '            Dim ds As DataSet
    '            Dim count As Integer
    '            Dim i As Integer
    '            Dim row As System.Data.DataRow
    '            Dim itemAvailable As Integer = 0
    '            stQuery = "SELECT ITEM_STK_YN_NUM FROM OM_POS_ITEM WHERE ITEM_CODE = '" & itemCode & "' OR ITEM_BAR_CODE='" & itemCode & "'"
    '            errLog.WriteToErrorLog("ITEM_STK_YN_NUM Query", stQuery, "")
    '            ds = db.SelectFromTableODBC(stQuery)
    '            count = ds.Tables("Table").Rows.Count
    '            Dim ITEM_STK_YN_NUM As String = ""
    '            If count > 0 Then
    '                ITEM_STK_YN_NUM = ds.Tables("Table").Rows.Item(0).Item(0).ToString
    '            End If
    '            'MsgBox("ITEM_STK_YN_NUM " & ITEM_STK_YN_NUM)

    '            Dim ItmPrice_name As String = "ItemPrice" & parts(1).ToString
    '            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmPrice_name, True)
    '            price = Convert.ToDouble(ItmPriceFound(0).Text.ToString)
    '            Dim ItmDisamt_name As String = "ItemDisamt" & parts(1).ToString
    '            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisamt_name, True)
    '            disamt = Convert.ToDouble(ItmDisamtFound(0).Text.ToString)
    '            Dim ItmAddval_name As String = "ItemAddval" & parts(1).ToString
    '            Dim ItmAddvalFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddval_name, True)
    '            addval = Convert.ToDouble(ItmAddvalFound(0).Text.ToString)
    '            Dim ItmAddvalCode_name As String = "ItemAddvalCode" & parts(1).ToString
    '            Dim ItmAddvalCodeFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmAddvalCode_name, True)

    '            Dim ItmNetamt_name As String = "ItemNetamt" & parts(1).ToString
    '            Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmNetamt_name, True)

    '            Dim ItmDesc_name As String = "ItemDesc" & parts(1).ToString
    '            Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDesc_name, True)
    '            Dim ItmDisc_name As String = "ItemDisc" & parts(1).ToString
    '            Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find(ItmDisc_name, True)
    '            'ItmNetamtFound(0).Text = Round((((qty * price) - disamt) + addval), 3).ToString
    '            'MsgBox(alreadyAddedItemCount)
    '            If ITEM_STK_YN_NUM = "1" Then
    '                stQuery = "select item_code from om_pos_item where item_code='" & itemCode & "' or item_bar_code='" & itemCode & "'"
    '                ds = db.SelectFromTableODBC(stQuery)
    '                Dim itemcodeval As String = ""
    '                If ds.Tables("Table").Rows.Count > 0 Then
    '                    itemcodeval = ds.Tables("Table").Rows.Item(0).Item(0).ToString
    '                End If

    '                stQuery = "select (LCS_STK_QTY_BU + LCS_RCVD_QTY_BU) - (LCS_ISSD_QTY_BU+LCS_HOLD_QTY_BU+LCS_REJECT_QTY_BU+LCS_PICK_QTY_BU+LCS_PACK_QTY_BU) as AvailableStk from OS_LOCN_CURR_STK where LCS_ITEM_CODE = '" & itemcodeval & "' and LCS_LOCN_CODE='" & Location_Code & "'"
    '                errLog.WriteToErrorLog("Stock Check Query", stQuery, "")
    '                ds = db.SelectFromTableODBC(stQuery)
    '                count = ds.Tables("Table").Rows.Count
    '                i = 0
    '                If count > 0 Then
    '                    row = ds.Tables("Table").Rows.Item(0)
    '                    itemAvailable = Convert.ToInt64(row.Item(0).ToString)
    '                Else
    '                    itemAvailable = 0
    '                End If
    '                If Not itemAvailable >= alreadyAddedItemCount Then
    '                    If Not TXN_Code = "SO" Then
    '                        ItmCodeFound(0).ForeColor = Color.Red
    '                        ItmDescFound(0).ForeColor = Color.Red
    '                        ItmPriceFound(0).ForeColor = Color.Red
    '                        ItmDiscFound(0).ForeColor = Color.Red
    '                        ItmDisamtFound(0).ForeColor = Color.Red
    '                        ItmNetamtFound(0).ForeColor = Color.Red
    '                        ItmNetamtFound(0).Text = "0"
    '                        ItmAddvalFound(0).ForeColor = Color.Red
    '                        ItmAddvalFound(0).Text = "0"
    '                        tbx.ForeColor = Color.Red
    '                        Home.ToolStripStatusLabel.Text = "Item '" + ItmCodeFound(0).Text + "' is low"
    '                        If itemAvailable = 0 Then
    '                            tbx.Text = "0"
    '                            MsgBox("Item stock not available in this location, select some other Delivery location!")
    '                            Exit Sub
    '                        End If
    '                        MsgBox("Item '" + ItmCodeFound(0).Text + "' is low")
    '                    Else
    '                        Home.ToolStripStatusLabel.Text = "POS"
    '                        createHeadLog()
    '                        createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
    '                        ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
    '                        Calculate_TotalAmount()
    '                    End If
    '                Else
    '                    ItmCodeFound(0).ForeColor = Color.Black
    '                    ItmDescFound(0).ForeColor = Color.Black
    '                    ItmPriceFound(0).ForeColor = Color.Black
    '                    ItmDiscFound(0).ForeColor = Color.Black
    '                    ItmDisamtFound(0).ForeColor = Color.Black
    '                    ItmNetamtFound(0).ForeColor = Color.Black
    '                    ItmAddvalFound(0).ForeColor = Color.Black
    '                    tbx.ForeColor = Color.Black
    '                    Home.ToolStripStatusLabel.Text = "POS"
    '                    createHeadLog()
    '                    createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
    '                    ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
    '                    Calculate_TotalAmount()
    '                End If
    '            Else
    '                ItmCodeFound(0).ForeColor = Color.Black
    '                ItmDescFound(0).ForeColor = Color.Black
    '                ItmPriceFound(0).ForeColor = Color.Black
    '                ItmDiscFound(0).ForeColor = Color.Black
    '                ItmDisamtFound(0).ForeColor = Color.Black
    '                ItmNetamtFound(0).ForeColor = Color.Black
    '                ItmAddvalFound(0).ForeColor = Color.Black
    '                tbx.ForeColor = Color.Black
    '                Home.ToolStripStatusLabel.Text = "POS"
    '                createHeadLog()
    '                createItemLog(ItmCodeFound(0), ItmDescFound(0), tbx, ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalFound(0), ItmAddvalCodeFound(0))
    '                ItmNetamtFound(0).Text = Round(((qty * price) - disamt), 3).ToString
    '                Calculate_TotalAmount()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    Private Sub btnLineAddValOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtLineAddValDesc.Text = "" Then
                MsgBox("Please select a valid Expense Code")
                Exit Sub
            End If
            If txtLineAddValAmount.Text = "" Then
                MsgBox("Please enter Add Value")
                Exit Sub
            End If

            Dim ItmAddvalCtl As System.Windows.Forms.Control() = Me.Controls.Find(itemrowAddvalName.Text, True)
            ItmAddvalCtl(0).Text = txtLineAddValAmount.Text
            Dim parts As String() = itemrowAddvalName.Text.Split(New String() {"ItemAddval"}, StringSplitOptions.None)
            Dim ItmAddvalCodeCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddvalCode" & parts(1).ToString, True)
            ItmAddvalCodeCtl(0).Text = txtLineAddValCode.Text

            txtLineAddValCode.Text = ""
            txtLineAddValDesc.Text = ""
            txtLineAddValPerc.Text = ""
            txtLineAddValAmount.Text = "0"

            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & parts(1).ToString, True)
            Dim ItmDescFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDesc" & parts(1).ToString, True)
            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & parts(1).ToString, True)
            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" & parts(1).ToString, True)
            Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisc" & parts(1).ToString, True)
            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" & parts(1).ToString, True)
            Dim ItmNetamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemNetamt" & parts(1).ToString, True)

            createItemLog(ItmCodeFound(0), ItmDescFound(0), ItmQtyFound(0), ItmPriceFound(0), ItmDiscFound(0), ItmDisamtFound(0), ItmNetamtFound(0), ItmAddvalCtl(0), ItmAddvalCodeCtl(0))

            Dim i As Integer
            i = pnlLineAddValue.Width
            pnlRoyalty.BringToFront()
            Do While i > 0
                pnlLineAddValue.Location = New Point(Me.Width - 2, Me.Height - pnlLineAddValue.Height)
                'Threading.Thread.Sleep(1)
                i = i - 2
            Loop
            pnlLineAddValue.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()

            btnAddItem_Click(sender, e)

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btn_SR_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SR_Search.Click
        Try
            'pnlCon_SalesReturn.Height = PnlSR_SEARCHLIST.Height
            'lstviewSRDetails.SendToBack()
            LstView_SR_Search.Items.Clear()
            Dim i As Integer = pnlCon_SalesReturn.Height + lblSR_SNO.Height  'pnlSalesReturn.Height + lstviewSRDetails.Location.Y

            While i >= pnlCon_SalesReturn.Location.Y

                PnlSR_SEARCHLIST.Location = New Point(pnlCon_SalesReturn.Location.X, i)
                PnlSR_SEARCHLIST.Show()
                Threading.Thread.Sleep(0.5)
                i = (i - 1)
            End While
            For Each ctl As Control In pnlSR_TRANSNO.Controls
                ctl.Enabled = False
            Next
            dtSRFromdate.Value = Date.Now
            dtSRToDate.Value = Date.Now
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub btn_SR_Search_Select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SR_Search_Select.Click
        Try
            If LstView_SR_Search.SelectedItems.Count < 1 Then

                MsgBox("Please select a sales Return")
                Exit Sub

            End If
            Dim srSelectsrno As String = LstView_SR_Search.SelectedItems.Item(0).SubItems(1).Text

            For i = 0 To LstView_SR_Search.Items.Count - 1
                Dim srno As String = LstView_SR_Search.Items.Item(i).SubItems(0).Text

            Next
            txtSRTransNo.Text = srSelectsrno
            btnSRAdd_Click(sender, e)
            PnlSR_SEARCHLIST.Hide()
            For Each ctl As Control In pnlSR_TRANSNO.Controls
                ctl.Enabled = True
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_SR_Search_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SR_Search_Cancel.Click
        Try
            'pnlCon_SalesReturn.BringToFront()
            PnlSR_SEARCHLIST.Hide()
            For Each ctl As Control In pnlSR_TRANSNO.Controls
                ctl.Enabled = True
            Next
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btn_SR_Search__Filter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SR_Search__Filter.Click
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim i As Integer = 0

            If dtSRFromdate.Value > dtSRToDate.Value Then
                MsgBox("Provide least date in from date field")
            End If

            Dim listinvhno As New List(Of String)

            stQuery = "SELECT C.INVH_NO,to_char(C.INVH_DT,'dd-MM-YY'), INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE, A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU QTY FROM OT_INVOICE_ITEM A, OT_CUST_SALE_RET_ITEM B, OT_INVOICE_HEAD C WHERE "
            stQuery = stQuery + "A.INVI_INVH_SYS_ID = C.INVH_SYS_ID  AND  C.INVH_DT >= TO_DATE('" & dtSRFromdate.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') AND C.INVH_DT <= TO_DATE('" & dtSRToDate.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') "
            stQuery = stQuery + "AND A.INVI_ITEM_CODE = B.CSRI_ITEM_CODE AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = C.INVH_NO) AND "
            stQuery = stQuery + "CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO = C.INVH_NO) "
            stQuery = stQuery + "GROUP BY C.INVH_NO,C.INVH_DT, INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE,A.INVI_QTY_BU,A.INVI_CSRI_QTY_BU HAVING A.INVI_QTY_BU - A.INVI_CSRI_QTY_BU > 0 "
            stQuery = stQuery + "UNION "
            stQuery = stQuery + "SELECT B.INVH_NO,to_char(B.INVH_DT,'dd-MM-YY'), INVI_ITEM_CODE, INVI_ITEM_DESC, INVI_PL_RATE, INVI_QTY_BU QTY FROM OT_INVOICE_ITEM A,OT_INVOICE_HEAD B WHERE A.INVI_INVH_SYS_ID=B.INVH_SYS_ID AND B.INVH_DT >= TO_DATE('" + dtSRFromdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')AND B.INVH_DT <= TO_DATE('" + dtSRToDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.INVI_ITEM_CODE NOT IN (SELECT CSRI_ITEM_CODE FROM OT_CUST_SALE_RET_ITEM B WHERE "
            stQuery = stQuery + "CSRI_CSRH_SYS_ID IN (SELECT CSRR_CSRH_SYS_ID FROM OT_CUST_SALE_RET_REF WHERE CSRR_REF_TXN_CODE = 'POSINV' AND CSRR_REF_NO =B.INVH_NO)) AND INVI_QTY_BU > 0 AND INVI_INVH_SYS_ID IN (SELECT INVH_SYS_ID FROM OT_INVOICE_HEAD WHERE INVH_COMP_CODE = '" + CompanyCode + "' AND INVH_LOCN_CODE ='" + Location_Code + "' AND INVH_TXN_CODE = 'POSINV' AND INVH_NO = B.INVH_NO AND INVH_DT >= TO_DATE('" + dtSRFromdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND INVH_DT <= TO_DATE('" + dtSRToDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')) "

            errLog.WriteToErrorLog("SR Filter", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "SR FILTER  - " & stQuery)

            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count

            LstView_SR_Search.Items.Clear()

            While count > 0

                row = ds.Tables("Table").Rows.Item(i)

                If listinvhno.Contains(row.Item(0).ToString) Then
                    count = count - 1
                    Continue While
                End If
                listinvhno.Add(row.Item(0).ToString)
                LstView_SR_Search.Items.Add(i + 1)
                LstView_SR_Search.Items(i).SubItems.Add(row.Item(0).ToString)
                LstView_SR_Search.Items(i).SubItems.Add(row.Item(1).ToString)
                i = i + 1
                count = count - 1

            End While



        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try


    End Sub


    Private Sub btn_Print_Report_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print_Report.Click

        Try
            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
            PrintDialog1.AllowSomePages = True

            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
                PrintDocument1.Print()
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dtpick_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpick.LostFocus
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If CheckTransactionDate() Then

            Else
                Dim stQuery As String
                Dim ds As DataSet
                MsgBox("Selected date is greater than the Server date!")
                stQuery = "select to_char(sysdate,'dd-mm-yyyy') from dual"
                ds = db.SelectFromTableODBC(stQuery)
                dtpick.Value = DateTime.ParseExact(ds.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)
                dtpick.Select()
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Function CheckTransactionDate() As Boolean
        Dim success As Boolean = True
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select * from dual where sysdate>'" & dtpick.Value.ToString("dd-MMM-yy") & "'"
            errLog.WriteToErrorLog("", stQuery, "Date Checking query")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "DATE CHECKING QUERY  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            If count > 0 Then
                success = True
            Else
                success = False

            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return success
    End Function

    Public Function CheckShiftTimings() As Boolean
        Dim success As Boolean = True
        'Try
        '    Dim stQuery As String
        '    Dim ds As DataSet
        '    stQuery = "select ROUND(1440 * (to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_TO") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') - sysdate),2) from dual where sysdate between to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_FROM") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') and  to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_TO") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') "
        '    'stQuery = "select * from dual where sysdate between to_date('" & Location_Setup_Values("BUSINESS_HOUR_FROM") & "', 'DD/MM/YY HH:MI:SS AM') and  to_date('" & Location_Setup_Values("BUSINESS_HOUR_TO") & "', 'DD/MM/YY HH:MI:SS AM') "
        '    errLog.WriteToErrorLog(stQuery, "", "CheckShiftTimings Query")
        '    ds = db.SelectFromTableODBC(stQuery)
        '    Dim count As Integer = ds.Tables("Table").Rows.Count

        '    If count > 0 Then
        '        If Not alertOnOff Then
        '            If Not Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString) - Convert.ToDouble(Location_Setup_Values("SHIFT_ALERT_BEFORE").ToString) > 0 Then

        '                Dim intervalvalue As Double = Convert.ToDouble(Location_Setup_Values("SHIFT_ALERT_INTERVAL").ToString)
        '                Dim countdownmin As Double = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString), 0)
        '                alertTimer.Interval = intervalvalue * 1000 * 60
        '                alertTimer.Enabled = True
        '                alertOnOff = True
        '                AlertMinutes = countdownmin

        '                alertTimer.Start()
        '                Dim sender As New Object
        '                alertTimer_Tick(sender, New System.EventArgs)

        '                'MsgBox("Your shift will ends in " & Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString), 0) & "  minutes!")
        '            End If
        '        End If
        '    Else
        '        success = False
        '    End If
        'Catch ex As Exception
        '    errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error - CheckShiftTimings")
        'End Try
        Return success
    End Function

    Private Sub alertTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles alertTimer.Tick

        Try
            If Not AlertMinutes = 0 Then
                Dim stQuery As String
                Dim ds As DataSet
                stQuery = "select ROUND(1440 * (to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_TO") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') - sysdate),2) from dual where sysdate between to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_FROM") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') and  to_date(sysdate || ' ' || to_char(  to_date('" & Location_Setup_Values("BUSINESS_HOUR_TO") & "','DD/MM/YY HH:MI:SS AM') ,  'HH:MI:SS AM'),'dd-MM-yy HH:MI:SS AM') "
                errLog.WriteToErrorLog(stQuery, "", "CheckShiftTimings")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "CheckShiftTimings - " & stQuery)
                ds = db.SelectFromTableODBC(stQuery)
                Dim count As Integer = ds.Tables("Table").Rows.Count
                If count > 0 Then
                    Dim countdownmin As Double = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(0).Item(0).ToString), 0)
                    AlertMinutes = countdownmin
                    MsgBox("Shift ends in " & countdownmin.ToString & " minutes!", MsgBoxStyle.Information, "POS")
                End If
            Else
                MsgBox("Transaction cannot be proceeded!", MsgBoxStyle.Critical, "Shift Timings Alert!")
                alertTimer.Enabled = False
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bmp = New Bitmap(pnlReportContainer.Width, pnlReportContainer.Height)
        pnlReportContainer.DrawToBitmap(bmp, pnlReportContainer.ClientRectangle)
        Dim x As Integer = e.MarginBounds.X - 110
        Dim y As Integer = e.MarginBounds.Y - 65
        e.Graphics.DrawImage(bmp, x, y)
        e.HasMorePages = False
    End Sub

    Private Sub btnPatientAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientAdd.Click

        For Each ctl As Control In pnlPatientDetails.Controls
            Select Case ctl.GetType.ToString
                Case "System.Windows.Forms.TextBox"
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                        .Text = ""
                    End With

                Case "System.Windows.Forms.CheckBox"
                    With DirectCast(ctl, CheckBox)
                        .Enabled = True
                        .BackColor = Color.White
                        .CheckState = CheckState.Unchecked
                    End With
                Case "System.Windows.Forms.DateTimePicker"
                    With DirectCast(ctl, DateTimePicker)
                        .Enabled = True
                        .BackColor = Color.White

                    End With
                Case "System.Windows.Forms.RadioButton"
                    With DirectCast(ctl, RadioButton)
                        .Enabled = True
                        .BackColor = Color.White
                        .Checked = CheckState.Unchecked
                    End With
                Case "System.Windows.Forms.TabControl"
                    With DirectCast(ctl, TabControl)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                Case "System.Windows.Forms.GroupBox"
                    With DirectCast(ctl, GroupBox)
                        .Enabled = True
                        .BackColor = Color.White

                    End With
            End Select
        Next

      
        For Each ctl In RX_GLASSES.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In RX_CONTACTLENS.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In SLIT_K.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In TRIAL_D.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next
        btnPatientSaveNew.BringToFront()
        txtPatientNo.Enabled = False

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientQuery" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientTelOffSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientMobileSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientNameSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientTelResSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientEmailSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                Exit For
            End If
        Next

        Try
            txtPatientNo.Text = ""
            txtPatPatientNo.ReadOnly = True
            txtPatCustCode.Text = Setup_Values("CUST_CODE")
            txtPatCustCode.ReadOnly = True
            txtPatCustCode_TextChanged(sender, e)
            btnPatientEdit.Enabled = False
            btnPatientDelete.Enabled = False
            btnPatientSearch.Enabled = False
            btnPatientSaveNew.Enabled = True
            btnPatientSaveNew.BringToFront()
            radMale.Checked = True
            dtpickPatDOB.Value = "01/01/1900"
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPatientEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientEdit.Click
        If txtPatPatientNo.Text = "" Then
            MsgBox("Please select a valid patient")
            Exit Sub
        End If
        For Each ctl As Control In pnlPatientDetails.Controls
            Select Case ctl.GetType.ToString
                Case "System.Windows.Forms.TextBox"
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                    End With

                Case "System.Windows.Forms.CheckBox"
                    With DirectCast(ctl, CheckBox)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                Case "System.Windows.Forms.DateTimePicker"
                    With DirectCast(ctl, DateTimePicker)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                Case "System.Windows.Forms.RadioButton"
                    With DirectCast(ctl, RadioButton)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                Case "System.Windows.Forms.TabControl"
                    With DirectCast(ctl, TabControl)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
                Case "System.Windows.Forms.GroupBox"
                    With DirectCast(ctl, GroupBox)
                        .Enabled = True
                        .BackColor = Color.White
                    End With
            End Select
        Next

        For Each ctl In RX_GLASSES.Controls
            If TypeOf ctl Is TextBox Then
                With DirectCast(ctl, TextBox)
                    .ReadOnly = False
                    .BackColor = Color.White
                End With
            End If
        Next
      
        For Each ctl In RX_CONTACTLENS.Controls
            If TypeOf ctl Is TextBox Then
                With DirectCast(ctl, TextBox)
                    .ReadOnly = False
                    .BackColor = Color.White
                End With
            End If
        Next

        For Each ctl In SLIT_K.Controls
            If TypeOf ctl Is TextBox Then
                With DirectCast(ctl, TextBox)
                    .ReadOnly = False
                    .BackColor = Color.White
                End With
            End If
        Next


        For Each ctl In TRIAL_D.Controls
            If TypeOf ctl Is TextBox Then
                With DirectCast(ctl, TextBox)
                    .ReadOnly = False
                    .BackColor = Color.White
                End With
            End If
        Next
        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientQuery" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientTelOffSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientMobileSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientNameSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientTelResSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                Exit For
            End If
        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientEmailSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                Exit For
            End If

        Next

        For Each ctl As Control In pnlPatientDetails.Controls
            If ctl.Name = "btnPatientNoSearch" Then
                pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                Exit For
            End If

        Next
        btnPatientAdd.SendToBack()
        btnPatientSaveNew.SendToBack()
        btnPatientUpdate.Enabled = True
        btnPatientUpdate.BringToFront()
        btnPatientAdd.Enabled = False
        btnPatientSearch.Enabled = False
        btnPatientDelete.Enabled = False
        btnPatientEdit.Enabled = False
        txtPatCustCode.ReadOnly = True
        txtPatCustName.ReadOnly = True


    End Sub

    Private Sub txtPatCustName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatCustName.TextChanged

    End Sub

    Private Sub btnPatientUpdateNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientUpdate.Click
        Try
            Dim stQuery, gender, DOB As String

            If radMale.Checked = True Then
                gender = "MALE"
            ElseIf radFemale.Checked = True Then
                gender = "FEMALE"
            End If

            If dtpickPatDOB.Value = "01/01/1900" Then
                DOB = ""
            Else
                DOB = Format(dtpickPatDOB.Value, "dd-MMM-yyyy")
            End If

            stQuery = "update om_patient_master set PM_GENDER='" & gender & "',PM_DOB= '" & DOB & "',PM_CITY = '" & txtPatCity.Text & "',PM_PATIENT_NAME = '" & txtPatPatientName.Text & "',PM_ZIPCODE = '" & txtPatZipcode.Text & "',PM_TEL_OFF='" & txtPatTelOff.Text & "',PM_TEL_RES='" & txtPatTelRes.Text & "',PM_TEL_MOB='" & txtPatMobile.Text & "',PM_EMAIL='" & txtPatEmail.Text & "',PM_NATIONALITY='" & txtPatNation.Text & "',PM_COMPANY='" & txtPatCompany.Text & "',pm_occupation='" & txtPatOccupation.Text & "',PM_REMARKS='" & txtPatRemarks.Text & "',PM_NOTES='" & txtPatNotes.Text & "',PM_UPD_UID='" & LogonUser & "',PM_UPD_DT=sysdate  where pm_cust_no='" & txtPatPatientNo.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_PATTIENT_MASTER", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE QUERY OM_PATIENNT_MASTER  - " & stQuery)
            db.SaveToTableODBC(stQuery)


            stQuery = "update  OM_PAT_RX_GLASSES set PRXG_R_D_SPH='" & txtRXG_RE_Sph_D1.Text & "',PRXG_R_D_CYL='" & txtRXG_RE_Cyl_D1.Text & "',PRXG_R_D_AXIS='" & txtRXG_RE_Axi_D1.Text & "',PRXG_R_D_VISION='" & txtRXG_RE_Vis_D1.Text & "',PRXG_R_N_SPH='" & txtRXG_RE_Sph_N1.Text & "',PRXG_R_N_CYL='" & txtRXG_RE_Cyl_N1.Text & "',PRXG_R_N_AXIS='" & txtRXG_RE_Axi_N1.Text & "',PRXG_R_N_VISION='" & txtRXG_RE_Vis_N1.Text & "',PRXG_R_PD='" & txtRXG_LE_IPD_D1.Text & "',PRXG_L_D_SPH='" & txtRXG_LE_Sph_D1.Text & "',PRXG_L_D_CYL='" & txtRXG_LE_Cyl_D1.Text & "',PRXG_L_D_AXIS='" & txtRXG_LE_Axi_D1.Text & "',PRXG_L_D_VISION='" & txtRXG_LE_Vis_D1.Text & "',PRXG_L_N_SPH='" & txtRXG_LE_Sph_N1.Text & "',PRXG_L_N_CYL='" & txtRXG_LE_Cyl_N1.Text & "',PRXG_L_N_AXIS='" & txtRXG_LE_Axi_N1.Text & "',PRXG_L_N_VISION='" & txtRXG_LE_Vis_N1.Text & "',PRXG_L_PD='" & txtRXG_LE_IPD_N1.Text & "',PRXG_UPD_UID='" & LogonUser & "',PRXG_UPD_DT=sysdate where PRXG_SYS_ID=( select PRXG_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID)"
            errLog.WriteToErrorLog("Update Query RXGlasses", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE QUERY RXGLASSES  - " & stQuery)
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_CONTACT_LENS set PRXCL_R_I_BCOR='" & txtRXC_RE_Bcor_I.Text & "', PRXCL_R_I_DIA='" & txtRXC_RE_Dia_I.Text & "',PRXCL_R_I_POWER='" & txtRXC_RE_Pow_I.Text & "',PRXCL_R_II_BCOR='" & txtRXC_RE_Bcor_II.Text & "',PRXCL_R_II_DIA='" & txtRXC_RE_Dia_II.Text & "',PRXCL_R_II_POWER='" & txtRXC_RE_Pow_II.Text & "',PRXCL_R_BRAND='" & txtRXC_RE_Brand1.Text & "',PRXCL_L_I_BCOR='" & txtRXC_LE_Bcor_I.Text & "',PRXCL_L_I_DIA='" & txtRXC_LE_Dia_I.Text & "', PRXCL_L_I_POWER='" & txtRXC_LE_Pow_I.Text & "',PRXCL_L_II_BCOR='" & txtRXC_LE_Bcor_II.Text & "',PRXCL_L_II_DIA='" & txtRXC_LE_Dia_II.Text & "',PRXCL_L_II_POWER='" & txtRXC_LE_Pow_II.Text & "',PRXCL_L_BRAND='" & txtRXC_LE_Brand2.Text & "',PRXCL_UPD_UID='" & LogonUser & "',PRXCL_UPD_DT=sysdate where PRXCL_SYS_ID=( select PRXCL_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID)"
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_CONTACT_LENS", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE QUERY OM_PAT_RX_CONTACT_LENS  - " & stQuery)
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_SLITK_READING set PRXSKR_SLIT_RE='" & txtSlit_Re.Text & "',PRXSKR_SLIT_LE='" & txtSlit_Le.Text & "', PRXSKR_SLIT_LRIS='" & txtSlit_LrisDia.Text & "',PRXSKR_K_RE_HORIZONTAL='" & txtK_Re_Hori.Text & "',PRXSKR_K_LE_HORIZONTAL='" & txtK_Le_Hori.Text & "',PRXSKR_K_RE_VERTICAL='" & txtK_Re_Vert.Text & "',PRXSKR_K_LE_VERTICAL='" & txtK_Le_Vert.Text & "', PRXSKR_UPD_UID='" & LogonUser & "', PRXSKR_UPD_DT=sysdate where PRXSKR_SYS_ID=( select PRXSKR_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID) "
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_SLITK_READING", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE QUERY OM_PAT_RX_SLITK_READING  - " & stQuery)
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_TRIAL_DETAILS set PRXTD_LENS_USED_RE='" & txtTrail_Re.Text & "',PRXTD_LENS_USED_RE_ADD='" & txtTrail_Re_Add.Text & "',PRXTD_LENS_USED_RE_VIA='" & txtTrail_Re_Via.Text & "',PRXTD_LENS_USED_LE='" & txtTrail_Le.Text & "', PRXTD_LENS_USED_LE_ADD='" & txtTrail_Le_Add.Text & "', PRXTD_LENS_USED_LE_VIA='" & txtTrail_Le_Via.Text & "',PRXTD_RE_REMARKS='" & txtTrail_Re_Remarks.Text & "', PRXTD_LE_REMARKS='" & txtTrail_Le_Remarks.Text & "', PRXTD_UPD_UID='" & LogonUser & "',PRXTD_UPD_DT=sysdate where PRXTD_SYS_ID=( select PRXTD_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXTD_PM_SYS_ID) "
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "UPDATE QUERY OM_PAT_RX_TRIAL_DETAILS  - " & stQuery)
            db.SaveToTableODBC(stQuery)

            MsgBox("Updated Sucessfully!")

            For Each ctl As Control In pnlPatientDetails.Controls
                Select Case ctl.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        With DirectCast(ctl, TextBox)
                            .ReadOnly = True
                            .BackColor = Color.White
                        End With

                    Case "System.Windows.Forms.CheckBox"
                        With DirectCast(ctl, CheckBox)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.DateTimePicker"
                        With DirectCast(ctl, DateTimePicker)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.RadioButton"
                        With DirectCast(ctl, RadioButton)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.TabControl"
                        With DirectCast(ctl, TabControl)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                End Select
            Next

            btnPatientAdd.BringToFront()
            txtPatientNo.Enabled = True
            btnPatientUpdate.Enabled = True
            btnPatientAdd.Enabled = True
            btnPatientSearch.Enabled = True
            btnPatientDelete.Enabled = True
            btnPatientEdit.Enabled = True
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPatientSaveNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientSaveNew.Click
        Try
            Dim stQuery As String
            Dim gender As String
            Dim DOB As String

            If txtPatPatientName.Text = "" Then
                MsgBox("Please enter Patient Name!")
                Exit Sub
            End If
            If radMale.Checked = True Then
                gender = "MALE"
            ElseIf radFemale.Checked = True Then
                gender = "FEMALE"
            End If

            If dtpickPatDOB.Value = "01/01/1900" Then
                DOB = ""
            Else
                DOB = Format(dtpickPatDOB.Value, "dd-MMM-yyyy")
            End If

            Dim ds As DataSet
            Dim Custno As String = ""
            Dim Patno As String = ""
            stQuery = "SELECT PM_SYS_ID.NEXTVAL as patno FROM DUAL"
            ds = db.SelectFromTableODBC(stQuery)

            Dim strSalesArr() As String = txtSalesmanCode.Text.Split(" - ")

            If ds.Tables("Table").Rows.Count > 0 Then
                Custno = Location_Code & "-" & ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Patno = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            stQuery = "INSERT INTO OM_PATIENT_MASTER(PM_SYS_ID,PM_COMP_CODE,PM_COUNTER_CODE,PM_CR_UID,PM_CR_DT,PM_LOCN_CODE,PM_SM_CODE,PM_CUST_NO,PM_PATIENT_NAME,PM_GENDER,PM_DOB,PM_CITY,PM_ZIPCODE,PM_NATIONALITY, PM_OCCUPATION,PM_COMPANY,PM_TEL_MOB,PM_EMAIL,PM_TEL_OFF,PM_TEL_RES,PM_NOTES,PM_REMARKS,PM_CUST_CODE,PM_CUST_NAME,PM_FRZ_FLAG_NUM) VALUES ("
            stQuery = stQuery & "'" & Patno & "','" & CompanyCode & "','" & POSCounterNumber & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'),'" & Location_Code & "','" & strSalesArr(0) & "','" & Custno & "','" & txtPatPatientName.Text.Replace("'", "''") & "','" & gender & "','" & DOB & "','" & txtPatCity.Text & "','" & txtPatZipcode.Text & "','" & txtPatNation.Text & "','" & txtPatOccupation.Text & "','" & txtPatCompany.Text & "','" & txtPatMobile.Text & "','" & txtPatEmail.Text & "','" & txtPatTelOff.Text & "','" & txtPatTelRes.Text & "','" & txtPatNotes.Text & "','" & txtPatRemarks.Text & "','" & txtPatCustCode.Text & "','" & txtPatCustName.Text & "',2)"
            errLog.WriteToErrorLog("Query OM_PATIENT_MASTER", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY OM_PATIENT_MASTER  - " & stQuery)
            db.SaveToTableODBC(stQuery)
            ''cONTINUE FROM 11206
            stQuery = "INSERT INTO OM_PAT_RX_GLASSES(PRXG_SYS_ID,PRXG_PM_SYS_ID,PRXG_COMP_CODE,PRXG_CR_UID,PRXG_CR_DT,PRXG_DATE,PRXG_LOCN_CODE,PRXG_COUNTER_CODE,PRXG_SM_CODE,PRXG_R_D_SPH,PRXG_R_D_CYL,PRXG_R_D_AXIS,PRXG_R_D_VISION,PRXG_R_N_SPH,PRXG_R_N_CYL,PRXG_R_N_AXIS,PRXG_R_N_VISION,PRXG_R_PD,PRXG_L_D_SPH,PRXG_L_D_CYL,PRXG_L_D_AXIS,PRXG_L_D_VISION,PRXG_L_N_SPH,PRXG_L_N_CYL,PRXG_L_N_AXIS,PRXG_L_N_VISION,PRXG_L_PD,PRXG_FRZ_FLAG_NUM) VALUES ("
            stQuery = stQuery & "PRXG_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'),to_date(sysdate,'DD-MM-YY'),'" & Location_Code & "','" & POSCounterNumber & "','" & strSalesArr(0) & "','" & txtRXG_RE_Sph_D1.Text & "','" & txtRXG_RE_Cyl_D1.Text & "','" & txtRXG_RE_Axi_D1.Text & "','" & txtRXG_RE_Vis_D1.Text & "','" & txtRXG_RE_Sph_N1.Text & "','" & txtRXG_RE_Cyl_N1.Text & "','" & txtRXG_RE_Axi_N1.Text & "','" & txtRXG_RE_Vis_N1.Text & "','" & txtRXG_LE_IPD_D1.Text & "','" & txtRXG_LE_Sph_D1.Text & "','" & txtRXG_LE_Cyl_D1.Text & "','" & txtRXG_LE_Axi_D1.Text & "','" & txtRXG_LE_Vis_D1.Text & "','" & txtRXG_LE_Sph_N1.Text & "','" & txtRXG_LE_Cyl_N1.Text & "','" & txtRXG_LE_Axi_N1.Text & "','" & txtRXG_LE_Vis_N1.Text & "','" & txtRXG_LE_IPD_N1.Text & "',2)"
            errLog.WriteToErrorLog("Query OM_PAT_RX_GLASSES", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY OM_PAT_RX_GLASSES  - " & stQuery)

            db.SaveToTableODBC(stQuery)

            stQuery = "insert into OM_PAT_RX_CONTACT_LENS(PRXCL_SYS_ID,PRXCL_PM_SYS_ID,PRXCL_COMP_CODE, PRXCL_LOCN_CODE,PRXCL_COUNTER_CODE, PRXCL_SM_CODE,PRXCL_R_I_BCOR, PRXCL_R_I_DIA,PRXCL_R_I_POWER,PRXCL_R_II_BCOR,PRXCL_R_II_DIA,PRXCL_R_II_POWER,PRXCL_R_BRAND,PRXCL_L_I_BCOR,PRXCL_L_I_DIA, PRXCL_L_I_POWER,PRXCL_L_II_BCOR,PRXCL_L_II_DIA,PRXCL_L_II_POWER,PRXCL_L_BRAND, PRXCL_CR_UID,PRXCL_CR_DT,PRXCL_FRZ_FLAG_NUM,PRXCL_DATE)Values("
            stQuery = stQuery & "PRXCL_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & strSalesArr(0) & "','" & txtRXC_RE_Bcor_I.Text & "','" & txtRXC_RE_Dia_I.Text & "','" & txtRXC_RE_Pow_I.Text & "','" & txtRXC_RE_Bcor_II.Text & "','" & txtRXC_RE_Dia_II.Text & "','" & txtRXC_RE_Pow_II.Text & "','" & txtRXC_RE_Brand1.Text & "','" & txtRXC_LE_Bcor_I.Text & "','" & txtRXC_LE_Dia_I.Text & "','" & txtRXC_LE_Pow_I.Text & "','" & txtRXC_LE_Bcor_II.Text & "','" & txtRXC_LE_Dia_II.Text & "','" & txtRXC_LE_Pow_II.Text & "','" & txtRXC_LE_Brand2.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_CONTACT_LENS", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY OM_PATIENT_RX_CONTACT_LENS  -" & stQuery)
            db.SaveToTableODBC(stQuery)

            stQuery = "insert into OM_PAT_RX_SLITK_READING(PRXSKR_SYS_ID,PRXSKR_PM_SYS_ID, PRXSKR_COMP_CODE,PRXSKR_LOCN_CODE,PRXSKR_COUNTER_CODE,PRXSKR_SM_CODE,PRXSKR_SLIT_RE,PRXSKR_SLIT_LE, PRXSKR_SLIT_LRIS,PRXSKR_K_RE_HORIZONTAL,PRXSKR_K_LE_HORIZONTAL,PRXSKR_K_RE_VERTICAL,PRXSKR_K_LE_VERTICAL,PRXSKR_CR_UID,PRXSKR_CR_DT,PRXSKR_FRZ_FLAG_NUM,PRXSKR_DATE)values("
            stQuery = stQuery & "PRXSKR_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & strSalesArr(0) & "','" & txtSlit_Re.Text & "','" & txtSlit_Le.Text & "','" & txtSlit_LrisDia.Text & "','" & txtK_Re_Hori.Text & "','" & txtK_Le_Hori.Text & "','" & txtK_Re_Vert.Text & "','" & txtK_Le_Vert.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_SLITK_READING", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "QUERY OM_PAT_RX_SLITK_READING  - " & stQuery)

            db.SaveToTableODBC(stQuery)

            stQuery = "insert into OM_PAT_RX_TRIAL_DETAILS(PRXTD_SYS_ID,PRXTD_PM_SYS_ID,PRXTD_COMP_CODE,PRXTD_LOCN_CODE, PRXTD_COUNTER_CODE, PRXTD_SM_CODE,PRXTD_LENS_USED_RE,PRXTD_LENS_USED_RE_ADD,PRXTD_LENS_USED_RE_VIA,PRXTD_LENS_USED_LE, PRXTD_LENS_USED_LE_ADD, PRXTD_LENS_USED_LE_VIA,PRXTD_RE_REMARKS, PRXTD_LE_REMARKS,PRXTD_CR_UID,PRXTD_CR_DT,PRXTD_FRZ_FLAG_NUM, PRXTD_DATE)values("
            stQuery = stQuery & " PRXTD_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & strSalesArr(0) & "','" & txtTrail_Re.Text & "','" & txtTrail_Re_Add.Text & "','" & txtTrail_Re_Via.Text & "','" & txtTrail_Le.Text & "','" & txtTrail_Le_Add.Text & "','" & txtTrail_Le_Via.Text & "','" & txtTrail_Re_Remarks.Text & "','" & txtTrail_Le_Remarks.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "OM_PAT_RX_TRIAL_DETAILS  - " & stQuery)
            db.SaveToTableODBC(stQuery)

            MsgBox("Patient Details Added!")
            'Dim i As Integer = pnl1Patient.Width
            'pnl1Patient.BringToFront()
            'Do While i > 0
            '    pnl1Patient.Location = New Point(Me.Width - 2, Me.Height - pnl1Patient.Height)
            '    'Threading.Thread.Sleep(1)
            '    i = i - 2
            'Loop
            'pnl1Patient.Visible = False
            'pnlButtonHolder.Visible = True
            'pnlButtonHolder.BringToFront()
            btnPatientAdd.BringToFront()
            btnPatientEdit.Enabled = True
            btnPatientDelete.Enabled = True
            btnPatientSearch.Enabled = True

            txtPatientNo.Text = Custno
            txtPatientNo_TextChanged(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtPatCustCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatCustCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select cust_name from om_customer where cust_code='" & txtPatCustCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPatCustName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPatCustName.Text = ""
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub


    Private Sub LstView_SR_Search_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstView_SR_Search.DoubleClick
        Try
            btn_SR_Search_Select_Click(sender, e)
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientDelete.Click
        If txtPatPatientName.Text = "" Then
            MsgBox("Please select a valid Patient No.")
        Else
            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In pnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "Select * from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "'"
            errLog.WriteToErrorLog("Delete Query Check om_patient_master", stQuery, "Error")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY CHECK OM_PATIENT_MASTER  - " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                stQuery = "Delete from OM_PAT_RX_CONTACT_LENS where PRXCL_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatientNo.Text & "')"
                errLog.WriteToErrorLog("Delete Query OM_PAT_RX_CONTACT_LENS", stQuery, "Error")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY OM_PAT_RX_CONTACT_LENS  - " & stQuery)
                db.SaveToTableODBC(stQuery)
                stQuery = "Delete from OM_PAT_RX_TRIAL_DETAILS where PRXTD_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatientNo.Text & "')"
                errLog.WriteToErrorLog("Delete Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "Error")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY OM_PAT_RX_TRIAL_DETAILS   - " & stQuery)
                db.SaveToTableODBC(stQuery)
                stQuery = "Delete from OM_PAT_RX_GLASSES where PRXG_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatientNo.Text & "')"
                errLog.WriteToErrorLog("Delete Query OM_PAT_RX_GLASSES", stQuery, "Error")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY OM_PAT_RX_GLASSES  - " & stQuery)
                db.SaveToTableODBC(stQuery)
                stQuery = "Delete from OM_PAT_RX_SLITK_READING where PRXSKR_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatientNo.Text & "')"
                errLog.WriteToErrorLog("Delete Query OM_PAT_RX_SLITK_READING", stQuery, "Error")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY OM_PAT_RX_SLITK_READING  -  " & stQuery)
                db.SaveToTableODBC(stQuery)
                stQuery = "Delete from om_patient_master where pm_cust_no='" & txtPatientNo.Text & "'"
                errLog.WriteToErrorLog("Delete Query om_patient_master", stQuery, "Error")
                'MAILLOGROWNUM = MAILLOGROWNUM + 1
                'MAILLOGValues.Add(MAILLOGROWNUM, "DELETE QUERY OM_PATIENT_MASTER  - " & stQuery)
                db.SaveToTableODBC(stQuery)
                MsgBox("Patient Deleted Successfully!")
                txtPatientNo.Text = ""
                btnCancelPatient_Click(sender, e)
            Else
                MsgBox("Please select a valid Patient")
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If Not CheckShiftTimings() Then
                lblTransLoader.SendToBack()
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If transtype = "Sales Invoice" Then
                If (FinalizeSalesInvoicetransaction()) Then
                    If cardpay.GetReceivedAmount >= Convert.ToDouble(txtBalancePay.Text.ToString) Then
                        For k = 1 To txtItemAddval.Count
                            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" + k.ToString, True)
                            expenseAmount = expenseAmount + Convert.ToDouble((ItmPriceFound(0).Text).ToString)
                        Next
                        If SalesInvoiceDone() Then

                            For Each ChildForm As Form In Home.MdiChildren
                                ChildForm.Close()
                            Next
                            TransactionSlip.MdiParent = Home
                            TransactionSlip.TXN_NO = INVHNO.ToString
                            TransactionSlip.TXN_TYPE = "Sales Invoice"
                            TransactionSlip.Show()

                            'loadReport()
                            'btnPatient.Enabled = False
                            'btnGiftVouchers.Enabled = False
                            'btnRoyalty.Enabled = False
                            'btnPayments.Enabled = False
                            'btnSalesOrders.Enabled = False
                            'btnInsurance.Enabled = False
                            'btnReferal.Enabled = False
                            'btnLineAddvalue.Enabled = False
                            'btnSalesReturn.Enabled = False
                            'btnReprint.Enabled = False
                        Else
                            lblTransLoader.SendToBack()
                            MsgBox("Error Occured During Transaction!")
                            Exit Sub
                        End If
                    Else
                        lblTransLoader.SendToBack()
                        MsgBox("Payment was not fully made!")
                        Exit Sub
                    End If
                Else
                    lblTransLoader.SendToBack()
                End If
            ElseIf transtype = "Sales Return" Then
                If FinalizeSalesReturntransaction() Then
                    If cardpay.GetReceivedAmount >= Convert.ToDouble(txtTotalAmount.Text) Then
                        If SalesReturnDone() Then
                            For k = 1 To txtItemAddval.Count
                                Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" + k.ToString, True)
                                expenseAmount = expenseAmount + Convert.ToDouble((ItmPriceFound(0).Text).ToString)
                            Next

                            For Each ChildForm As Form In Home.MdiChildren
                                ChildForm.Close()
                            Next
                            TransactionSlip.MdiParent = Home
                            TransactionSlip.TXN_NO = CSRH_NO.ToString
                            TransactionSlip.TXN_TYPE = "Sales Return"
                            TransactionSlip.Show()

                            'loadReport()
                            'btnPatient.Enabled = False
                            'btnGiftVouchers.Enabled = False
                            'btnRoyalty.Enabled = False
                            'btnPayments.Enabled = False
                            'btnSalesOrders.Enabled = False
                            'btnInsurance.Enabled = False
                            'btnReferal.Enabled = False
                            'btnLineAddvalue.Enabled = False
                            'btnSalesReturn.Enabled = False
                            'btnReprint.Enabled = False
                        Else
                            lblTransLoader.SendToBack()
                            MsgBox("Error Occured During Transaction!")
                            Exit Sub
                        End If
                    Else
                        lblTransLoader.SendToBack()
                        MsgBox("Payment was not fully made!")
                        Exit Sub
                    End If
                Else
                    lblTransLoader.SendToBack()
                End If
            Else
                If Finalizetransaction() = True Then
                    UpdateItemLog()
                    'If Not Convert.ToDouble(txtTotalAmount.Text) > 0 Then
                    '    lblTransLoader.SendToBack()
                    '    MsgBox("Please select Items")
                    '    Exit Sub
                    'End If
                    If Not Convert.ToDouble(txtItemPrice(0).Text) > 0 Then
                        lblTransLoader.SendToBack()
                        MsgBox("Please select Items")
                        Exit Sub
                    End If

                    For k = 1 To txtItemAddval.Count
                        Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemAddval" + k.ToString, True)
                        expenseAmount = expenseAmount + Convert.ToDouble((ItmPriceFound(0).Text).ToString)
                    Next
                    If TXN_Code = "POSINV" Then
                        If cardpay.GetReceivedAmount = Convert.ToDouble(txtTotalAmount.Text.ToString) Then
                            If DirectInvoiceDone() Then

                                For Each ChildForm As Form In Home.MdiChildren
                                    ChildForm.Close()
                                Next
                                TransactionSlip.MdiParent = Home
                                TransactionSlip.TXN_NO = INVHNO.ToString
                                TransactionSlip.TXN_TYPE = "Invoice"
                                TransactionSlip.Show()

                                'loadReport()
                                'btnPatient.Enabled = False
                                'btnGiftVouchers.Enabled = False
                                'btnRoyalty.Enabled = False
                                'btnPayments.Enabled = False
                                'btnSalesOrders.Enabled = False
                                'btnInsurance.Enabled = False
                                'btnReferal.Enabled = False
                                'btnLineAddvalue.Enabled = False
                                'btnSalesReturn.Enabled = False
                                'btnReprint.Enabled = False
                            Else
                                lblTransLoader.SendToBack()
                                MsgBox("Error Occured During Transaction!")
                                Exit Sub
                            End If
                        Else
                            lblTransLoader.SendToBack()
                            MsgBox("Payment not done for the transaction")
                        End If
                    ElseIf TXN_Code = "SO" Then
                        If SalesOrderDone() Then

                            For Each ChildForm As Form In Home.MdiChildren
                                ChildForm.Close()
                            Next
                            TransactionSlip.MdiParent = Home
                            TransactionSlip.TXN_NO = SOHNO.ToString
                            TransactionSlip.TXN_TYPE = "Sales Order"
                            TransactionSlip.Show()

                            'loadReport()
                            'btnPatient.Enabled = False
                            'btnGiftVouchers.Enabled = False
                            'btnRoyalty.Enabled = False
                            'btnPayments.Enabled = False
                            'btnSalesOrders.Enabled = False
                            'btnInsurance.Enabled = False
                            'btnReferal.Enabled = False
                            'btnLineAddvalue.Enabled = False
                            'btnSalesReturn.Enabled = False
                            'btnReprint.Enabled = False
                        Else
                            lblTransLoader.SendToBack()
                            MsgBox("Error Occured During Transaction!")
                            Exit Sub
                        End If
                    End If
                Else
                    lblTransLoader.SendToBack()
                    MsgBox("Transaction Cannot be Proceeded")
                End If
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub txtLineAddValCode_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineAddValCode.SelectedValueChanged
        Try
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String = ""
            Dim count As Integer
            Dim i As Integer = 0

            'select ROY_CARD from PERF_ROYALTY_CARD where ROY_TYPE='A001';

            stQuery = "SELECT EXP_NAME, OM_EXPENSE.ROWID FROM OM_EXPENSE WHERE EXP_CODE = '" & txtLineAddValCode.Text & "'"
            errLog.WriteToErrorLog("OM_EXPENSE", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "OM_EXPENSE  - " & stQuery)

            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count = 0 Then
                txtLineAddValDesc.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtLineAddValDesc.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtLineAddValAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLineAddValAmount.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") < 0) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") >= 0 And tbx.SelectionLength > 0 And tbx.SelectionStart <= tbx.Text.IndexOf(".") And tbx.SelectionStart + tbx.SelectionLength > tbx.Text.IndexOf("."))) Then
                e.Handled = True

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtLineAddValAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineAddValAmount.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 0
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0

            ElseIf value >= 0 Then
                tbx.Text = Round(value, 3)
            Else
                tbx.Text = Abs(Round(value, 3))
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtLineAddValAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineAddValAmount.TextChanged
        'If Not txtLineAddValAmount.Text = "0" Or txtLineAddValAmount.Text = "" Then
        Try
            Dim ctlPresent As Boolean = False
            For Each ctl As Control In pnlItemDetails.Controls
                If ctl.Name = itemrowAddvalName.Text Then
                    ctlPresent = True
                End If
            Next
            If ctlPresent Then
                If txtItemAddval.Count > 0 Then
                    Dim ItmAddvalCtl As System.Windows.Forms.Control() = Me.Controls.Find(itemrowAddvalName.Text, True)
                    Dim parts As String() = itemrowAddvalName.Text.Split(New String() {"ItemAddval"}, StringSplitOptions.None)
                    Dim ItmQtyvalCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & parts(1).ToString, True)
                    Dim ItmPricevalCtl As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" & parts(1).ToString, True)
                    Dim amt As Double = 0
                    If txtLineAddValAmount.Text = "" Then
                        amt = 0
                    Else
                        amt = Convert.ToDouble(txtLineAddValAmount.Text)
                    End If

                    Dim qty As Double = Convert.ToDouble(ItmQtyvalCtl(0).Text)
                    Dim price As Double = Convert.ToDouble(ItmPricevalCtl(0).Text)
                    txtLineAddValPerc.Text = Round(((amt / (qty * price)) * 100), 2).ToString
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub cmbPayType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPayType.SelectedValueChanged
        Try
            Dim ds As DataSet
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer
            Dim row As System.Data.DataRow
            stQuery = "select PPD_NAME,PPD_TYPE from OM_POS_PAYMENT_DET where PPD_CODE='" & cmbPayType.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            If count < 1 Then
                txtPaymentDesc.Text = ""
            End If
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                txtPaymentDesc.Text = (row.Item(0).ToString)
                'MsgBox(row.Item(1).ToString)
                txtppdtype.Text = (row.Item(1).ToString)
                count = count - 1
                i = i + 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtRefHospid_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRefHospid.TextChanged
        Try
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer = 0
            Dim strRefArr() As String = txtRefHospid.Text.Split(" - ")
            'select ROY_CARD from PERF_ROYALTY_CARD where ROY_TYPE='A001';
            stQuery = "SELECT VSSV_NAME,VSSV_BL_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='REF_HOSPITAL' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S') AND VSSV_CODE='" + strRefArr(0) + "'"
            errLog.WriteToErrorLog("REF_HOSPITAL", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "REF_HOSPITAL   -   " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            'MsgBox(ds.Tables("Table").Rows.Count)
            count = ds.Tables("Table").Rows.Count
            'MsgBox("C:" & count)
            If count = 0 Then
                txtRefHospName.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtRefHospName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub txtRCCardType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRCCardType.TextChanged
        Try
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer = 0
            Dim strROYArr() As String = txtRCCardType.Text.Split(" - ")
            'select ROY_CARD from PERF_ROYALTY_CARD where ROY_TYPE='A001';
            stQuery = "SELECT VSSV_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='LOYALTY_CARD' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S') AND VSSV_CODE='" + strROYArr(0) + "'"
            errLog.WriteToErrorLog("LOYALTY_CARD", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "LOYALTY_CARD  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            'MsgBox(ds.Tables("Table").Rows.Count)
            count = ds.Tables("Table").Rows.Count
            'MsgBox("C:" & count)
            If count = 0 Then
                txtRCCardName.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtRCCardName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtGiftVoucherCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGiftVoucherCode.TextChanged
        Try
            Dim ds As DataSet
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer
            Dim row As System.Data.DataRow
            Dim strGVArr() As String = txtGiftVoucherCode.Text.Split(" - ")
            stQuery = "SELECT VSSV_NAME FROM IM_VS_STATIC_VALUE WHERE VSSV_VS_CODE ='GIFT_VOUCHER' AND NVL(VSSV_FRZ_FLAG_NUM,2) = 2 AND VSSV_VS_CODE IN (SELECT VS_CODE FROM IM_VALUE_SET WHERE VS_TYPE = 'S') AND VSSV_CODE='" & strGVArr(0) & "'"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            If count < 1 Then
                txtGiftVoucherDesc.Text = ""
            End If
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                txtGiftVoucherDesc.Text = (row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtInsuranceCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInsuranceCode.TextChanged
        Try
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer = 0
            'select ROY_CARD from PERF_ROYALTY_CARD where ROY_TYPE='A001';
            stQuery = "SELECT INSURANCE_NAME FROM PERF_INSURANCE WHERE INSURANCE_CODE='" + txtInsuranceCode.Text + "'"
            errLog.WriteToErrorLog("PERF_INSURANCE", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "PERF_INSURANCE  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            'MsgBox(ds.Tables("Table").Rows.Count)
            count = ds.Tables("Table").Rows.Count
            'MsgBox("C:" & count)
            If count = 0 Then
                txtInsuranceName.Text = ""
            Else
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    txtInsuranceName.Text = row.Item(0).ToString
                    count = count - 1
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

     
    Private Sub cmbPayType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPayType.TextChanged
        Try
            Dim ds As DataSet
            Dim stQuery As String
            Dim count As Integer
            Dim i As Integer
            Dim row As System.Data.DataRow
            stQuery = "select PPD_NAME,PPD_TYPE from OM_POS_PAYMENT_DET where PPD_CODE='" & cmbPayType.Text & "' and PPD_TYPE !='GV'"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            If count < 1 Then
                txtPaymentDesc.Text = ""
            End If
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                txtPaymentDesc.Text = (row.Item(0).ToString)
                'MsgBox(row.Item(1).ToString)
                txtppdtype.Text = (row.Item(1).ToString)
                count = count - 1
                i = i + 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim ds As New DataSet
            Dim i As Integer
            Dim stQuery As String
            Dim row As System.Data.DataRow
            Dim patientno As String


            i = 0
            flagval = 0
            txtPatientNo.Text = ""
            stQuery = "select PM_CUST_CODE as custcode,CUST_NAME as customername,PM_CUST_NAME as pcustname,PM_PATIENT_NAME as patientname,PM_GENDER as gender,to_char(PM_DOB,'dd/mm/yyyy') as dob,PM_CITY as city,PM_ZIPCODE as zipcode,PM_TEL_OFF as offphn,PM_TEL_RES as resphn,PM_TEL_MOB as mobphn,PM_EMAIL as pemail,PM_NATIONALITY as pnationality,PM_COMPANY as pcompany,pm_occupation as occupation,PM_REMARKS as premarks,PM_NOTES as pnotes,pm_cust_no from om_patient_master a, om_customer b where a.PM_CUST_CODE=b.CUST_CODE"

            If txtPatPatientName.Text <> "" Then
                stQuery = stQuery + " and PM_PATIENT_NAME='" & txtPatPatientName.Text & "'"
                flagval = 1
            End If
            If txtPatTelOff.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_OFF='" & txtPatTelOff.Text & "'"
                flagval = 1
            End If
            If txtPatTelRes.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_RES='" & txtPatTelRes.Text & "'"
                flagval = 1
            End If
            If txtPatMobile.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_MOB='" & txtPatMobile.Text & "'"
                flagval = 1
            End If
            If txtPatEmail.Text <> "" Then
                stQuery = stQuery + " and PM_EMAIL='" & txtPatEmail.Text & "'"
                flagval = 1
            End If
            If flagval = 1 Then
                totds = db.SelectFromTableODBC(stQuery)
                totcount = totds.Tables("Table").Rows.Count
            End If

            If totcount = 0 Then
                For Each ctl As Control In pnlPatientDetails.Controls
                    Select Case ctl.GetType.ToString
                        Case "System.Windows.Forms.TextBox"
                            With DirectCast(ctl, TextBox)
                                .ReadOnly = False
                                .BackColor = Color.White
                                .Text = ""
                            End With
                    End Select
                Next

                For Each ctl In RX_GLASSES.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next


                For Each ctl In RX_CONTACTLENS.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In SLIT_K.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In TRIAL_D.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                MsgBox("No Record Found")
            ElseIf totcount = 1 Then
                row = totds.Tables("Table").Rows.Item(toti)

                If row.Item(4).ToString = "MALE" Then
                    radMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    radFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtpickPatDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                txtPatientNo.Text = row.Item(17).ToString

                'i = 0
                'stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "

                'errLog.WriteToErrorLog("RX-GLASSESsass", stQuery, "")
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count


                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
                '    txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
                '    txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
                '    txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
                '    txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
                '    txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
                '    txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
                '    txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
                '    txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
                '    txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
                '    txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
                '    txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
                '    txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
                '    txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
                '    txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
                '    txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
                '    txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
                '    txtRXG_LE_IPD_N1.Text = row.Item(17).ToString
                '    i = i + 1
                '    count = count - 1
                'End If


                'i = 0
                'stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
                'errLog.WriteToErrorLog("RX-CONTACT", stQuery, "")
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
                '    txtRXC_RE_Dia_I.Text = row.Item(1).ToString
                '    txtRXC_RE_Pow_I.Text = row.Item(2).ToString
                '    txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
                '    txtRXC_RE_Dia_II.Text = row.Item(4).ToString
                '    txtRXC_RE_Pow_II.Text = row.Item(5).ToString
                '    txtRXC_RE_Brand1.Text = row.Item(6).ToString
                '    txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
                '    txtRXC_LE_Dia_I.Text = row.Item(8).ToString
                '    txtRXC_LE_Pow_I.Text = row.Item(9).ToString
                '    txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
                '    txtRXC_LE_Dia_II.Text = row.Item(11).ToString
                '    txtRXC_LE_Pow_II.Text = row.Item(12).ToString
                '    txtRXC_LE_Brand2.Text = row.Item(13).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                'i = 0
                'stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
                'errLog.WriteToErrorLog("SLIT AND K", stQuery, "")
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtSlit_Re.Text = row.Item(0).ToString
                '    txtSlit_Le.Text = row.Item(1).ToString
                '    txtSlit_LrisDia.Text = row.Item(2).ToString
                '    txtK_Re_Hori.Text = row.Item(3).ToString
                '    txtK_Le_Hori.Text = row.Item(4).ToString
                '    txtK_Re_Vert.Text = row.Item(5).ToString
                '    txtK_Le_Vert.Text = row.Item(6).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                'i = 0
                'stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
                'errLog.WriteToErrorLog("Trail Details", stQuery, "")
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtTrail_Re.Text = row.Item(0).ToString
                '    txtTrail_Re_Add.Text = row.Item(1).ToString
                '    txtTrail_Re_Via.Text = row.Item(2).ToString
                '    txtTrail_Le.Text = row.Item(3).ToString
                '    txtTrail_Le_Add.Text = row.Item(4).ToString
                '    txtTrail_Le_Via.Text = row.Item(5).ToString
                '    txtTrail_Re_Remarks.Text = row.Item(6).ToString
                '    txtTrail_Le_Remarks.Text = row.Item(7).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                        Exit For
                    End If
                Next

            ElseIf totcount > 1 Then

                btnPatientNext.Visible = True
                btnPatientNext.Enabled = True

                btnPatientPrev.Visible = True
                btnPatientPrev.Enabled = True

                row = totds.Tables("Table").Rows.Item(i)


                If row.Item(4).ToString = "MALE" Then
                    radMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    radFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtpickPatDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                txtPatientNo.Text = row.Item(17).ToString

                'i = 0
                'stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
                '    txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
                '    txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
                '    txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
                '    txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
                '    txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
                '    txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
                '    txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
                '    txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
                '    txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
                '    txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
                '    txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
                '    txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
                '    txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
                '    txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
                '    txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
                '    txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
                '    txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

                '    i = i + 1
                '    count = count - 1
                'End If


                'i = 0
                'stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
                '    txtRXC_RE_Dia_I.Text = row.Item(1).ToString
                '    txtRXC_RE_Pow_I.Text = row.Item(2).ToString
                '    txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
                '    txtRXC_RE_Dia_II.Text = row.Item(4).ToString
                '    txtRXC_RE_Pow_II.Text = row.Item(5).ToString
                '    txtRXC_RE_Brand1.Text = row.Item(6).ToString
                '    txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
                '    txtRXC_LE_Dia_I.Text = row.Item(8).ToString
                '    txtRXC_LE_Pow_I.Text = row.Item(9).ToString
                '    txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
                '    txtRXC_LE_Dia_II.Text = row.Item(11).ToString
                '    txtRXC_LE_Pow_II.Text = row.Item(12).ToString
                '    txtRXC_LE_Brand2.Text = row.Item(13).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                'i = 0
                'stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtSlit_Re.Text = row.Item(0).ToString
                '    txtSlit_Le.Text = row.Item(1).ToString
                '    txtSlit_LrisDia.Text = row.Item(2).ToString
                '    txtK_Re_Hori.Text = row.Item(3).ToString
                '    txtK_Le_Hori.Text = row.Item(4).ToString
                '    txtK_Re_Vert.Text = row.Item(5).ToString
                '    txtK_Le_Vert.Text = row.Item(6).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                'i = 0
                'stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
                'ds = db.SelectFromTableODBC(stQuery)
                'count = ds.Tables("Table").Rows.Count
                'If count > 0 Then
                '    row = ds.Tables("Table").Rows.Item(i)
                '    txtTrail_Re.Text = row.Item(0).ToString
                '    txtTrail_Re_Add.Text = row.Item(1).ToString
                '    txtTrail_Re_Via.Text = row.Item(2).ToString
                '    txtTrail_Le.Text = row.Item(3).ToString
                '    txtTrail_Le_Add.Text = row.Item(4).ToString
                '    txtTrail_Le_Via.Text = row.Item(5).ToString
                '    txtTrail_Re_Remarks.Text = row.Item(6).ToString
                '    txtTrail_Le_Remarks.Text = row.Item(7).ToString
                '    i = i + 1
                '    count = count - 1
                'End If

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In pnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        pnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                        Exit For
                    End If
                Next


                'btnPatientNext_Click(sender, e, count, i, row)
                'While count > 0
                '    row = ds.Tables("Table").Rows.Item(i)
                '    MsgBox(row.Item(11).ToString())
                '    i = i + 1
                'End While



            End If

            'btnPatientUpdateNew.Enabled = False
            btnPatientAdd.Enabled = True
            btnPatientEdit.Enabled = True
            btnPatientDelete.Enabled = True

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientQuerySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim buttonclicked As String = DirectCast(sender, Button).Name
        If buttonclicked = "btnPatientTelOffSearch" Then
            Dim pnl As New Panel
            With pnl
                .Name = "pnlPatTelOffSearch"
                .Location = New Point(pnlPatientDetails.Location.X, pnlPatientDetails.Location.Y)
                .Size = New Size(pnlPatientDetails.Width, pnlPatientDetails.Height)
                .BackColor = Color.Azure
                .BorderStyle = BorderStyle.FixedSingle
                .BringToFront()
                Dim lbl As New Label
                With lbl
                    .Name = "lblHeaderPatTelOffText"
                    .Text = "Patient Office Telephone No. Search"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BackColor = Color.DarkCyan
                    .Location = New Point(1, 1)
                    .Font = New Font(lbl.Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .Size = New Size(pnlPatientDetails.Width - 4, 20)
                End With
                .Controls.Add(lbl)

                Dim lbl1 As New Label
                With lbl1
                    .Name = "lblPatTelOffText"
                    .Text = "Search"
                    .Location = New Point(pnlPatientDetails.Width / 5, 55)
                    .Font = New Font(lbl1.Font, FontStyle.Bold)
                    .Size = New Size(50, 20)
                    .ForeColor = Color.DarkGreen
                End With
                .Controls.Add(lbl1)

                Dim txt As New TextBox
                With txt
                    .Name = "txtPatTelOffText"
                    .Location = New Point((pnlPatientDetails.Width / 5) + 50, 53)
                    .Size = New Size(pnlPatientDetails.Width / 2, 20)
                End With
                AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                .Controls.Add(txt)

                Dim btn As New Button
                With btn
                    .Name = "btnPatTelOffTextFilter"
                    .Location = New Point(txt.Location.X + txt.Width + 5, 53)
                    .Text = "Filter"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatTelOffTextFilter_Click
                .Controls.Add(btn)

                Dim lstview As New ListView
                With lstview
                    .Name = "lstviewPatTelOffText"
                    .Location = New Point(30, 90)
                    .Size = New Size(pnlPatientDetails.Width - 50, pnlPatientDetails.Height - 180)
                    .GridLines = True
                    .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                    .Columns.Add("Office Telephone No.", pnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                    .View = View.Details
                    .FullRowSelect = True
                End With
                AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                .Controls.Add(lstview)

                btn = New Button
                With btn
                    .Name = "btnPatTelOffTextSelect"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 100, pnlPatientDetails.Height - 50)
                    .Text = "Select"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                .Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnPatTelOffTextClose"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 170, pnlPatientDetails.Height - 50)
                    .Text = "Close"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                .Controls.Add(btn)

            End With
            Me.pnl1Patient.Controls.Add(pnl)

            pnlPatientDetails.Hide()
        ElseIf buttonclicked = "btnPatientMobileSearch" Then
            Dim pnl As New Panel
            With pnl
                .Name = "pnlPatMobSearch"
                .Location = New Point(pnlPatientDetails.Location.X, pnlPatientDetails.Location.Y)
                .Size = New Size(pnlPatientDetails.Width, pnlPatientDetails.Height)
                .BackColor = Color.Azure
                .BorderStyle = BorderStyle.FixedSingle
                .BringToFront()
                Dim lbl As New Label
                With lbl
                    .Name = "lblHeaderPatMobText"
                    .Text = "Patient Mobile No. Search"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BackColor = Color.DarkCyan
                    .Location = New Point(1, 1)
                    .Font = New Font(lbl.Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .Size = New Size(pnlPatientDetails.Width - 4, 20)
                End With
                .Controls.Add(lbl)

                Dim lbl1 As New Label
                With lbl1
                    .Name = "lblPatMobText"
                    .Text = "Search"
                    .Location = New Point(pnlPatientDetails.Width / 5, 55)
                    .Font = New Font(lbl1.Font, FontStyle.Bold)
                    .Size = New Size(50, 20)
                    .ForeColor = Color.DarkGreen
                End With
                .Controls.Add(lbl1)

                Dim txt As New TextBox
                With txt
                    .Name = "txtPatMobText"
                    .Location = New Point((pnlPatientDetails.Width / 5) + 50, 53)
                    .Size = New Size(pnlPatientDetails.Width / 2, 20)
                End With
                AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                .Controls.Add(txt)

                Dim btn As New Button
                With btn
                    .Name = "btnPatMobTextFilter"
                    .Location = New Point(txt.Location.X + txt.Width + 5, 53)
                    .Text = "Filter"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatTelOffTextFilter_Click
                .Controls.Add(btn)

                Dim lstview As New ListView
                With lstview
                    .Name = "lstviewPatMobText"
                    .Location = New Point(30, 90)
                    .Size = New Size(pnlPatientDetails.Width - 50, pnlPatientDetails.Height - 180)
                    .GridLines = True
                    .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                    .Columns.Add("Mobile Number", pnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                    .View = View.Details
                    .FullRowSelect = True
                End With
                AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                .Controls.Add(lstview)

                btn = New Button
                With btn
                    .Name = "btnPatMobTextSelect"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 100, pnlPatientDetails.Height - 50)
                    .Text = "Select"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                .Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnPatMobTextClose"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 170, pnlPatientDetails.Height - 50)
                    .Text = "Close"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                .Controls.Add(btn)

            End With
            Me.pnl1Patient.Controls.Add(pnl)

            pnlPatientDetails.Hide()
        ElseIf buttonclicked = "btnPatientNameSearch" Then
            Dim pnl As New Panel
            With pnl
                .Name = "pnlPatNameSearch"
                .Location = New Point(pnlPatientDetails.Location.X, pnlPatientDetails.Location.Y)
                .Size = New Size(pnlPatientDetails.Width, pnlPatientDetails.Height)
                .BackColor = Color.Azure
                .BorderStyle = BorderStyle.FixedSingle
                .BringToFront()
                Dim lbl As New Label
                With lbl
                    .Name = "lblHeaderPatNameText"
                    .Text = "Patient Name Search"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BackColor = Color.DarkCyan
                    .Location = New Point(1, 1)
                    .Font = New Font(lbl.Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .Size = New Size(pnlPatientDetails.Width - 4, 20)
                End With
                .Controls.Add(lbl)

                Dim lbl1 As New Label
                With lbl1
                    .Name = "lblPatNameText"
                    .Text = "Search"
                    .Location = New Point(pnlPatientDetails.Width / 5, 55)
                    .Font = New Font(lbl1.Font, FontStyle.Bold)
                    .Size = New Size(50, 20)
                    .ForeColor = Color.DarkGreen
                End With
                .Controls.Add(lbl1)

                Dim txt As New TextBox
                With txt
                    .Name = "txtPatNameText"
                    .Location = New Point((pnlPatientDetails.Width / 5) + 50, 53)
                    .Size = New Size(pnlPatientDetails.Width / 2, 20)
                End With
                AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                .Controls.Add(txt)

                Dim btn As New Button
                With btn
                    .Name = "btnPatNameTextFilter"
                    .Location = New Point(txt.Location.X + txt.Width + 5, 53)
                    .Text = "Filter"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatTelOffTextFilter_Click
                .Controls.Add(btn)

                Dim lstview As New ListView
                With lstview
                    .Name = "lstviewPatNameText"
                    .Location = New Point(30, 90)
                    .Size = New Size(pnlPatientDetails.Width - 50, pnlPatientDetails.Height - 180)
                    .GridLines = True
                    .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                    .Columns.Add("Patient Name", pnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                    .View = View.Details
                    .FullRowSelect = True
                End With
                AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                .Controls.Add(lstview)

                btn = New Button
                With btn
                    .Name = "btnPatNameTextSelect"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 100, pnlPatientDetails.Height - 50)
                    .Text = "Select"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                .Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnPatNameTextClose"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 170, pnlPatientDetails.Height - 50)
                    .Text = "Close"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                .Controls.Add(btn)

            End With
            Me.pnl1Patient.Controls.Add(pnl)

            pnlPatientDetails.Hide()
        ElseIf buttonclicked = "btnPatientTelResSearch" Then
            Dim pnl As New Panel
            With pnl
                .Name = "pnlPatTelResSearch"
                .Location = New Point(pnlPatientDetails.Location.X, pnlPatientDetails.Location.Y)
                .Size = New Size(pnlPatientDetails.Width, pnlPatientDetails.Height)
                .BackColor = Color.Azure
                .BorderStyle = BorderStyle.FixedSingle
                .BringToFront()
                Dim lbl As New Label
                With lbl
                    .Name = "lblHeaderPatTelResText"
                    .Text = "Patient Residence Telephone No. Search"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BackColor = Color.DarkCyan
                    .Location = New Point(1, 1)
                    .Font = New Font(lbl.Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .Size = New Size(pnlPatientDetails.Width - 4, 20)
                End With
                .Controls.Add(lbl)

                Dim lbl1 As New Label
                With lbl1
                    .Name = "lblPatTelResText"
                    .Text = "Search"
                    .Location = New Point(pnlPatientDetails.Width / 5, 55)
                    .Font = New Font(lbl1.Font, FontStyle.Bold)
                    .Size = New Size(50, 20)
                    .ForeColor = Color.DarkGreen
                End With
                .Controls.Add(lbl1)

                Dim txt As New TextBox
                With txt
                    .Name = "txtPatTelResText"
                    .Location = New Point((pnlPatientDetails.Width / 5) + 50, 53)
                    .Size = New Size(pnlPatientDetails.Width / 2, 20)
                End With
                AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                .Controls.Add(txt)

                Dim btn As New Button
                With btn
                    .Name = "btnPatTelResTextFilter"
                    .Location = New Point(txt.Location.X + txt.Width + 5, 53)
                    .Text = "Filter"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatTelOffTextFilter_Click
                .Controls.Add(btn)

                Dim lstview As New ListView
                With lstview
                    .Name = "lstviewPatTelResText"
                    .Location = New Point(30, 90)
                    .Size = New Size(pnlPatientDetails.Width - 50, pnlPatientDetails.Height - 180)
                    .GridLines = True
                    .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                    .Columns.Add("Residence Telephone No.", pnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                    .View = View.Details
                    .FullRowSelect = True
                End With
                AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                .Controls.Add(lstview)

                btn = New Button
                With btn
                    .Name = "btnPatTelResTextSelect"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 100, pnlPatientDetails.Height - 50)
                    .Text = "Select"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                .Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnPatTelResTextClose"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 170, pnlPatientDetails.Height - 50)
                    .Text = "Close"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                .Controls.Add(btn)

            End With
            Me.pnl1Patient.Controls.Add(pnl)
            pnlPatientDetails.Hide()
        ElseIf buttonclicked = "btnPatientEmailSearch" Then
            Dim pnl As New Panel
            With pnl
                .Name = "pnlPatEmailSearch"
                .Location = New Point(pnlPatientDetails.Location.X, pnlPatientDetails.Location.Y)
                .Size = New Size(pnlPatientDetails.Width, pnlPatientDetails.Height)
                .BackColor = Color.Azure
                .BorderStyle = BorderStyle.FixedSingle
                .BringToFront()
                Dim lbl As New Label
                With lbl
                    .Name = "lblHeaderPatEmailText"
                    .Text = "Patient Email ID Search"
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BackColor = Color.DarkCyan
                    .Location = New Point(1, 1)
                    .Font = New Font(lbl.Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .Size = New Size(pnlPatientDetails.Width - 4, 20)
                End With
                .Controls.Add(lbl)

                Dim lbl1 As New Label
                With lbl1
                    .Name = "lblPatEmailText"
                    .Text = "Search"
                    .Location = New Point(pnlPatientDetails.Width / 5, 55)
                    .Font = New Font(lbl1.Font, FontStyle.Bold)
                    .Size = New Size(50, 20)
                    .ForeColor = Color.DarkGreen
                End With
                .Controls.Add(lbl1)

                Dim txt As New TextBox
                With txt
                    .Name = "txtPatEmailText"
                    .Location = New Point((pnlPatientDetails.Width / 5) + 50, 53)
                    .Size = New Size(pnlPatientDetails.Width / 2, 20)
                End With
                AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                .Controls.Add(txt)

                Dim btn As New Button
                With btn
                    .Name = "btnPatEmailTextFilter"
                    .Location = New Point(txt.Location.X + txt.Width + 5, 53)
                    .Text = "Filter"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatTelOffTextFilter_Click
                .Controls.Add(btn)

                Dim lstview As New ListView
                With lstview
                    .Name = "lstviewPatEmailText"
                    .Location = New Point(30, 90)
                    .Size = New Size(pnlPatientDetails.Width - 50, pnlPatientDetails.Height - 180)
                    .GridLines = True
                    .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                    .Columns.Add("Email ID", pnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                    .View = View.Details
                    .FullRowSelect = True
                End With
                AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                .Controls.Add(lstview)

                btn = New Button
                With btn
                    .Name = "btnPatEmailTextSelect"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 100, pnlPatientDetails.Height - 50)
                    .Text = "Select"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                .Controls.Add(btn)

                btn = New Button
                With btn
                    .Name = "btnPatEmailTextClose"
                    .Location = New Point((pnlPatientDetails.Width / 2) + 170, pnlPatientDetails.Height - 50)
                    .Text = "Close"
                    .Size = New Size(60, 20)
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    .BackColor = Color.MediumTurquoise
                    .ForeColor = Color.SaddleBrown
                End With
                AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                .Controls.Add(btn)

            End With
            Me.pnl1Patient.Controls.Add(pnl)
            pnlPatientDetails.Hide()
        End If
    End Sub

    Private Sub btnPatTelOffTextFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer = 0
            Dim count As Integer = 0
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            If txtName = "txtPatTelOffText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelOffText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Or txtVal = "%" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_OFF FROM OM_PATIENT_MASTER where UPPER(PM_TEL_OFF) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                If count < 1 Then
                    MsgBox("No rows found! Please try again with some other value!")
                End If
                If count > 1000 Then
                    Dim result As DialogResult = MsgBox("More than " & (count - 1).ToString & " rows found! Do you want to continue?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Dim result1 As DialogResult = MsgBox("Loading.....", MsgBoxStyle.Information, "Please Wait")
                        If result1 = Windows.Forms.DialogResult.OK Then
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                lstview.Items.Add(i + 1)
                                lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                                i = i + 1
                                count = count - 1
                            End While
                        End If
                    End If
                Else
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        lstview.Items.Add(i + 1)
                        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                End If
            ElseIf txtName = "txtPatTelResText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelResText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Or txtVal = "%" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_RES FROM OM_PATIENT_MASTER where UPPER(PM_TEL_RES) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                If count < 1 Then
                    MsgBox("No rows found! Please try again with some other value!")
                End If
                If count > 1000 Then
                    Dim result As DialogResult = MsgBox("More than " & (count - 1).ToString & " rows found! Do you want to continue?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Dim result1 As DialogResult = MsgBox("Loading.....", MsgBoxStyle.Information, "Please Wait")
                        If result1 = Windows.Forms.DialogResult.OK Then
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                lstview.Items.Add(i + 1)
                                lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                                i = i + 1
                                count = count - 1
                            End While
                        End If
                    End If
                Else
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        lstview.Items.Add(i + 1)
                        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                End If

            ElseIf txtName = "txtPatNameText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatNameText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Or txtVal = "%" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_PATIENT_NAME FROM OM_PATIENT_MASTER where UPPER(PM_PATIENT_NAME) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                If count < 1 Then
                    MsgBox("No rows found! Please try again with some other value!")
                End If
                If count > 1000 Then
                    Dim result As DialogResult = MsgBox("More than " & (count - 1).ToString & " rows found! Do you want to continue?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Dim result1 As DialogResult = MsgBox("Loading.....", MsgBoxStyle.Information, "Please Wait")
                        If result1 = Windows.Forms.DialogResult.OK Then
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                lstview.Items.Add(i + 1)
                                lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                                i = i + 1
                                count = count - 1
                            End While
                        End If
                    End If
                Else
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        lstview.Items.Add(i + 1)
                        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                End If
            ElseIf txtName = "txtPatMobText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatMobText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Or txtVal = "%" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_MOB FROM OM_PATIENT_MASTER where UPPER(PM_TEL_MOB) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                If count < 1 Then
                    MsgBox("No rows found! Please try again with some other value!")
                End If
                If count > 1000 Then
                    Dim result As DialogResult = MsgBox("More than " & (count - 1).ToString & " rows found! Do you want to continue?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Dim result1 As DialogResult = MsgBox("Loading.....", MsgBoxStyle.Information, "Please Wait")
                        If result1 = Windows.Forms.DialogResult.OK Then
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                lstview.Items.Add(i + 1)
                                lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                                i = i + 1
                                count = count - 1
                            End While
                        End If
                    End If
                Else
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        lstview.Items.Add(i + 1)
                        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                End If
            ElseIf txtName = "txtPatEmailText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Or txtVal = "%" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_EMAIL FROM OM_PATIENT_MASTER where UPPER(PM_EMAIL) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                If count < 1 Then
                    MsgBox("No rows found! Please try again with some other value!")
                End If
                If count > 1000 Then
                    Dim result As DialogResult = MsgBox("More than " & (count - 1).ToString & " rows found! Do you want to continue?", MessageBoxButtons.YesNo, "Alert")
                    If result = Windows.Forms.DialogResult.Yes Then
                        Dim result1 As DialogResult = MsgBox("Loading.....", MsgBoxStyle.Information, "Please Wait")
                        If result1 = Windows.Forms.DialogResult.OK Then
                            While count > 0
                                row = ds.Tables("Table").Rows.Item(i)
                                lstview.Items.Add(i + 1)
                                lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                                i = i + 1
                                count = count - 1
                            End While
                        End If
                    End If
                Else
                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)
                        lstview.Items.Add(i + 1)
                        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                        i = i + 1
                        count = count - 1
                    End While
                End If
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientSearchSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim buttonclicked As String = DirectCast(sender, Button).Name
        If buttonclicked = "btnPatTelOffTextSelect" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
            Dim lst As ListView = pnl.Controls.Find("lstviewPatTelOffText", True)(0)
            If Not lst.SelectedItems.Count > 0 Then
                MsgBox("Please select a row!")
            Else
                lstviewPatSearch_DoubleClick(lst, e)
            End If
        ElseIf buttonclicked = "btnPatTelResTextSelect" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
            Dim lst As ListView = pnl.Controls.Find("lstviewPatTelResText", True)(0)
            If Not lst.SelectedItems.Count > 0 Then
                MsgBox("Please select a row!")
            Else
                lstviewPatSearch_DoubleClick(lst, e)
            End If
        ElseIf buttonclicked = "btnPatNameTextSelect" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
            Dim lst As ListView = pnl.Controls.Find("lstviewPatNameText", True)(0)
            If Not lst.SelectedItems.Count > 0 Then
                MsgBox("Please select a row!")
            Else
                lstviewPatSearch_DoubleClick(lst, e)
            End If
        ElseIf buttonclicked = "btnPatMobTextSelect" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
            Dim lst As ListView = pnl.Controls.Find("lstviewPatMobText", True)(0)
            If Not lst.SelectedItems.Count > 0 Then
                MsgBox("Please select a row!")
            Else
                lstviewPatSearch_DoubleClick(lst, e)
            End If
        ElseIf buttonclicked = "btnPatEmailTextSelect" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
            Dim lst As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
            If Not lst.SelectedItems.Count > 0 Then
                MsgBox("Please select a row!")
            Else
                lstviewPatSearch_DoubleClick(lst, e)
            End If
        End If

    End Sub

    Private Sub btnPatSearchPnlClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim buttonclicked As String = DirectCast(sender, Button).Name
        If buttonclicked = "btnPatTelOffTextClose" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
            Dim lst As New List(Of String)
            For Each ctl As Control In pnl.Controls
                lst.Add(ctl.Name)
            Next
            For i = 0 To lst.Count - 1
                pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
            Next
            pnl1Patient.Controls.Remove(Me.Controls.Find("pnlPatTelOffSearch", True)(0))
            pnlPatientDetails.Show()
        ElseIf buttonclicked = "btnPatTelResTextClose" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
            Dim lst As New List(Of String)
            For Each ctl As Control In pnl.Controls
                lst.Add(ctl.Name)
            Next
            For i = 0 To lst.Count - 1
                pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
            Next
            pnl1Patient.Controls.Remove(Me.Controls.Find("pnlPatTelResSearch", True)(0))
            pnlPatientDetails.Show()
        ElseIf buttonclicked = "btnPatNameTextClose" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
            Dim lst As New List(Of String)
            For Each ctl As Control In pnl.Controls
                lst.Add(ctl.Name)
            Next
            For i = 0 To lst.Count - 1
                pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
            Next
            pnl1Patient.Controls.Remove(Me.Controls.Find("pnlPatNameSearch", True)(0))
            pnlPatientDetails.Show()
        ElseIf buttonclicked = "btnPatMobTextClose" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
            Dim lst As New List(Of String)
            For Each ctl As Control In pnl.Controls
                lst.Add(ctl.Name)
            Next
            For i = 0 To lst.Count - 1
                pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
            Next
            pnl1Patient.Controls.Remove(Me.Controls.Find("pnlPatMobSearch", True)(0))
            pnlPatientDetails.Show()
        ElseIf buttonclicked = "btnPatEmailTextClose" Then
            Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
            Dim lst As New List(Of String)
            For Each ctl As Control In pnl.Controls
                lst.Add(ctl.Name)
            Next
            For i = 0 To lst.Count - 1
                pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
            Next
            pnl1Patient.Controls.Remove(Me.Controls.Find("pnlPatEmailSearch", True)(0))
            pnlPatientDetails.Show()
        End If
    End Sub

    Private Sub lstviewPatSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'If lstviewSOSelected.SelectedItems.Count < 1 Then
        '    MsgBox("Please select a sales order")
        '    Exit Sub
        'End If
        Dim lstviewclicked As String = DirectCast(sender, ListView).Name
        Dim lstval As String = DirectCast(sender, ListView).SelectedItems.Item(0).SubItems(1).Text
        If lstviewclicked = "lstviewPatTelOffText" Then
            txtPatTelOff.Text = lstval
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
            Dim btn As Button = pnl.Controls.Find("btnPatTelOffTextClose", True)(0)
            btnPatSearchPnlClose_Click(btn, e)
        ElseIf lstviewclicked = "lstviewPatTelResText" Then
            txtPatTelRes.Text = lstval
            Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
            Dim btn As Button = pnl.Controls.Find("btnPatTelResTextClose", True)(0)
            btnPatSearchPnlClose_Click(btn, e)
        ElseIf lstviewclicked = "lstviewPatNameText" Then
            txtPatPatientName.Text = lstval
            Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
            Dim btn As Button = pnl.Controls.Find("btnPatNameTextClose", True)(0)
            btnPatSearchPnlClose_Click(btn, e)
        ElseIf lstviewclicked = "lstviewPatMobText" Then
            txtPatMobile.Text = lstval
            Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
            Dim btn As Button = pnl.Controls.Find("btnPatMobTextClose", True)(0)
            btnPatSearchPnlClose_Click(btn, e)
        ElseIf lstviewclicked = "lstviewPatEmailText" Then
            txtPatEmail.Text = lstval
            Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
            Dim btn As Button = pnl.Controls.Find("btnPatEmailTextClose", True)(0)
            btnPatSearchPnlClose_Click(btn, e)
        End If
    End Sub

    Private Sub txtPatSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtName = DirectCast(sender, TextBox).Name
        txtVal = DirectCast(sender, TextBox).Text
        'Dim i As Integer = 0
        'Dim count As Integer = 0
        'Dim stQuery As String = ""
        'Dim ds As DataSet
        'Dim row As System.Data.DataRow
        'If txtName = "txtPatTelOffText" Then
        '    Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
        '    Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelOffText", True)(0)
        '    lstview.Items.Clear()
        '    If txtVal = "" Or txtVal = "%" Then
        '        Exit Sub
        '    End If
        '    stQuery = "SELECT PM_TEL_OFF FROM OM_PATIENT_MASTER where UPPER(PM_TEL_OFF) like '" & txtVal.ToUpper & "%'"
        '    ds = db.SelectFromTableODBC(stQuery)
        '    i = 0
        '    count = ds.Tables("Table").Rows.Count
        '    While count > 0
        '        row = ds.Tables("Table").Rows.Item(i)
        '        lstview.Items.Add(i + 1)
        '        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
        '        i = i + 1
        '        count = count - 1
        '    End While
        'ElseIf txtName = "txtPatTelResText" Then
        '    Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
        '    Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelResText", True)(0)
        '    lstview.Items.Clear()
        '    If txtVal = "" Or txtVal = "%" Then
        '        Exit Sub
        '    End If
        '    stQuery = "SELECT PM_TEL_RES FROM OM_PATIENT_MASTER where UPPER(PM_TEL_RES) like '" & txtVal.ToUpper & "%'"
        '    ds = db.SelectFromTableODBC(stQuery)
        '    i = 0
        '    count = ds.Tables("Table").Rows.Count
        '    While count > 0
        '        row = ds.Tables("Table").Rows.Item(i)
        '        lstview.Items.Add(i + 1)
        '        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
        '        i = i + 1
        '        count = count - 1
        '    End While
        'ElseIf txtName = "txtPatNameText" Then
        '    Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
        '    Dim lstview As ListView = pnl.Controls.Find("lstviewPatNameText", True)(0)
        '    lstview.Items.Clear()
        '    If txtVal = "" Or txtVal = "%" Then
        '        Exit Sub
        '    End If
        '    stQuery = "SELECT PM_PATIENT_NAME FROM OM_PATIENT_MASTER where UPPER(PM_PATIENT_NAME) like '" & txtVal.ToUpper & "%'"
        '    ds = db.SelectFromTableODBC(stQuery)
        '    i = 0
        '    count = ds.Tables("Table").Rows.Count
        '    While count > 0
        '        row = ds.Tables("Table").Rows.Item(i)
        '        lstview.Items.Add(i + 1)
        '        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
        '        i = i + 1
        '        count = count - 1
        '    End While
        'ElseIf txtName = "txtPatMobText" Then
        '    Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
        '    Dim lstview As ListView = pnl.Controls.Find("lstviewPatMobText", True)(0)
        '    lstview.Items.Clear()
        '    If txtVal = "" Or txtVal = "%" Then
        '        Exit Sub
        '    End If
        '    stQuery = "SELECT PM_TEL_MOB FROM OM_PATIENT_MASTER where UPPER(PM_TEL_MOB) like '" & txtVal.ToUpper & "%'"
        '    ds = db.SelectFromTableODBC(stQuery)
        '    i = 0
        '    count = ds.Tables("Table").Rows.Count
        '    While count > 0
        '        row = ds.Tables("Table").Rows.Item(i)
        '        lstview.Items.Add(i + 1)
        '        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
        '        i = i + 1
        '        count = count - 1
        '    End While
        'ElseIf txtName = "txtPatEmailText" Then
        '    Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
        '    Dim lstview As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
        '    lstview.Items.Clear()
        '    If txtVal = "" Or txtVal = "%" Then
        '        Exit Sub
        '    End If
        '    stQuery = "SELECT PM_EMAIL FROM OM_PATIENT_MASTER where UPPER(PM_EMAIL) like '" & txtVal.ToUpper & "%'"
        '    ds = db.SelectFromTableODBC(stQuery)
        '    i = 0
        '    count = ds.Tables("Table").Rows.Count
        '    While count > 0
        '        row = ds.Tables("Table").Rows.Item(i)
        '        lstview.Items.Add(i + 1)
        '        lstview.Items(i).SubItems.Add(row.Item(0).ToString)
        '        i = i + 1
        '        count = count - 1
        '    End While
        'End If
    End Sub

    Private Sub btnPatientSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientSearch.Click
        totcount = 0
        toti = 0
        totds.Clear()
        btnPatientPrev.Visible = False
        btnPatientNext.Visible = False

        Dim btn As New Button
        With btn
            .Name = "btnPatientQuery"
            .Location = New Point(btnPatientSearch.Location.X, btnPatientSearch.Location.Y)
            .Size = New Size(btnPatientSearch.Width, btnPatientSearch.Height)
            .Text = "Query"
            .TextAlign = ContentAlignment.BottomCenter
            .Image = My.Resources.recycle_bin_icon
            .ImageAlign = ContentAlignment.TopCenter
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .BringToFront()
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuery_Click
        Me.pnlPatientDetails.Controls.Add(btn)
        btnPatientSearch.SendToBack()

        'btn = New Button
        'With btn
        '    .Name = "btnPatientPatNoSearch"
        '    .Location = New Point(txtPatPatientNo.Location.X + txtPatPatientNo.Width, txtPatPatientNo.Location.Y)
        '    .Size = New Size(20, 20)
        '    .Image = My.Resources.search
        '    .FlatStyle = FlatStyle.Flat
        '    .FlatAppearance.BorderSize = 0
        'End With
        'AddHandler btn.Click, AddressOf Me.btnPatientQuery_Click
        'Me.pnlPatientDetails.Controls.Add(btn)

        btn = New Button
        With btn
            .Name = "btnPatientTelOffSearch"
            .Location = New Point(txtPatTelOff.Location.X + txtPatTelOff.Width, txtPatTelOff.Location.Y)
            .Size = New Size(20, 20)
            .Image = My.Resources.search
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
        Me.pnlPatientDetails.Controls.Add(btn)

        btn = New Button
        With btn
            .Name = "btnPatientMobileSearch"
            .Location = New Point(txtPatMobile.Location.X + txtPatMobile.Width, txtPatMobile.Location.Y)
            .Size = New Size(20, 20)
            .Image = My.Resources.search
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
        Me.pnlPatientDetails.Controls.Add(btn)

        btn = New Button
        With btn
            .Name = "btnPatientNameSearch"
            .Location = New Point(txtPatPatientName.Location.X + txtPatPatientName.Width, txtPatPatientName.Location.Y)
            .Size = New Size(20, 20)
            .Image = My.Resources.search
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
        Me.pnlPatientDetails.Controls.Add(btn)

        btn = New Button
        With btn
            .Name = "btnPatientTelResSearch"
            .Location = New Point(txtPatTelRes.Location.X + txtPatTelRes.Width, txtPatTelRes.Location.Y)
            .Size = New Size(20, 20)
            .Image = My.Resources.search
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
        Me.pnlPatientDetails.Controls.Add(btn)

        btn = New Button
        With btn
            .Name = "btnPatientEmailSearch"
            .Location = New Point(txtPatEmail.Location.X + txtPatEmail.Width, txtPatEmail.Location.Y)
            .Size = New Size(20, 20)
            .Image = My.Resources.search
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
        End With
        AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
        Me.pnlPatientDetails.Controls.Add(btn)

        For Each ctl As Control In pnlPatientDetails.Controls
            Select Case ctl.GetType.ToString
                Case "System.Windows.Forms.TextBox"
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = True
                        .BackColor = Color.White
                        .Text = ""
                    End With
            End Select
        Next

        For Each ctl In RX_GLASSES.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In RX_CONTACTLENS.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In SLIT_K.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        For Each ctl In TRIAL_D.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Readonly = False
                ctl.Text = ""
            End If
        Next

        txtPatPatientName.ReadOnly = False
        txtPatTelOff.ReadOnly = False
        txtPatMobile.ReadOnly = False
        txtPatTelRes.ReadOnly = False
        txtPatEmail.ReadOnly = False
        btnPatientAdd.Enabled = False
        btnPatientAdd.BringToFront()
        btnPatientEdit.Enabled = False
        btnPatientDelete.Enabled = False
        btnPatientSaveNew.Enabled = False
        btnPatientUpdate.Enabled = False
        txtPatientNo.Text = ""
    End Sub

    Private Sub btnReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprint.Click
        For Each ctl As Control In pnlINVDetails.Controls
            ctl.Enabled = False
        Next
        For Each ctl As Control In pnlItemDetails.Controls
            ctl.Enabled = False
        Next
        For Each ctl As Control In pnlBottomHolder.Controls
            ctl.Enabled = False
        Next
        For Each ctl As Control In pnlButtonHolder.Controls
            ctl.Enabled = False
        Next
        PnlGrpBoxCont.Height = Me.Height
        PnlReprintReport.Height = PnlGrpBoxCont.Height - (GrpBoxRepMenu.Height + 60)
        pnlButtonHolder.Visible = False
        pnlButtonHolder.SendToBack()
        PnlGrpBoxCont.BringToFront()
        For i = 0 To PnlGrpBoxCont.Width
            PnlGrpBoxCont.Location = New Point(Me.Width - i, Me.Height - PnlGrpBoxCont.Height)
            PnlGrpBoxCont.Show()
            i = i + 5
        Next i
    End Sub



    Private Sub btnRepViewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepViewReport.Click
        Try

            Dim i As Integer
            Dim StringToCheck As String = txtRepTransNo.Text
            For i = 0 To StringToCheck.Length - 1
                If Char.IsLetter(StringToCheck.Chars(i)) Then
                    MsgBox("Invalid Transaction No.")
                    txtRepTransNo.Select()
                    txtRepTransNo.Text = ""
                    Exit Sub
                End If
            Next
            For Each ctl In pnlButtonHolder.Controls
                ctl.Enabled = False
            Next
            If txtRepTransNo.Text = "" Then
                MsgBox("Please Enter a valid transaction Number")
                Exit Sub
            End If

            Dim stQuery As String
            Dim ds As DataSet
            If ChkInv.Checked = True Then
                stQuery = "select invh_no from ot_invoice_head where invh_no=" & txtRepTransNo.Text & " and INVH_REF_NO is null"
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    TXN_Code = "POSINV"
                    TransactionSlip.TXN_NO = txtRepTransNo.Text
                    TransactionSlip.TXN_TYPE = "Invoice"
                    For Each ChildForm As Form In Home.MdiChildren
                        ChildForm.Close()
                    Next
                    TransactionSlip.MdiParent = Home
                    TransactionSlip.Show()
                Else
                    MsgBox("Direct Invoice Transaction No:" & txtRepTransNo.Text & " not found!")
                    Exit Sub
                End If
            ElseIf ChkSO.Checked = True Then
                stQuery = "select soh_no from ot_so_head where soh_no=" & txtRepTransNo.Text
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    TXN_Code = "SO"
                    TransactionSlip.TXN_NO = txtRepTransNo.Text
                    TransactionSlip.TXN_TYPE = "Sales Order"
                    For Each ChildForm As Form In Home.MdiChildren
                        ChildForm.Close()
                    Next
                    TransactionSlip.MdiParent = Home
                    TransactionSlip.Show()
                Else
                    MsgBox("Saler Order Transaction No:" & txtRepTransNo.Text & " not found!")
                    Exit Sub
                End If
            ElseIf ChkSI.Checked = True Then
                stQuery = "select invh_no from ot_invoice_head where invh_no=" & txtRepTransNo.Text & " and trim(invh_ref_no) is not null"
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    TXN_Code = "POSINV"
                    transtype = "Sales Invoice"
                    TransactionSlip.TXN_NO = txtRepTransNo.Text
                    TransactionSlip.TXN_TYPE = "Sales Invoice"
                    For Each ChildForm As Form In Home.MdiChildren
                        ChildForm.Close()
                    Next
                    TransactionSlip.MdiParent = Home
                    TransactionSlip.Show()

                Else
                    MsgBox("Sales Invoice Transaction No:" & txtRepTransNo.Text & " not found!")
                    Exit Sub
                End If
            ElseIf ChkSR.Checked = True Then
                stQuery = "select csrh_no from OT_CUST_SALE_RET_HEAD where csrh_no=" & txtRepTransNo.Text
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    TXN_Code = "SARTN"

                    TransactionSlip.TXN_NO = txtRepTransNo.Text
                    TransactionSlip.TXN_TYPE = "Sales Return"
                    For Each ChildForm As Form In Home.MdiChildren
                        ChildForm.Close()
                    Next
                    TransactionSlip.MdiParent = Home
                    TransactionSlip.Show()
                Else
                    MsgBox("Sales Return Transaction No:" & txtRepTransNo.Text & " not found!")
                    Exit Sub
                End If
            End If


            PnlGrpBoxCont.Hide()

            'pnlReport.Show()
            'pnlReport.BringToFront()
            'pnlButtonHolder.Show()


            'pnlReport.Location = New Point(0, 0)
            'pnlReport.Height = Me.Height
            'loadReprintReport()

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub loadReprintReport()
        Try
            'MsgBox(transtype)
            Dim stQuery As String = ""
            Dim ds As DataSet
            If TXN_Code = "POSINV" Then
                lblReport.Text = "Invoice Reprint"
                stQuery = stQuery + " select rownum,b.invh_no ,to_char( b.invh_dt,'DD/MM/YYYY') as InvoiceDate, "
                stQuery = stQuery + " locn_name,"
                stQuery = stQuery + " d.addr_name|| d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||"
                stQuery = stQuery + "d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||"
                stQuery = stQuery + "d.ADDR_PROVINCE_CODE as Location_Address,"
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
                stQuery = stQuery + " nvl((select ITED_FC_AMT from OT_INVOICE_ITEM_TED where ITED_I_SYS_ID=INVI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,INVH_SM_CODE as salesman,INVH_REF_NO as refno"
                stQuery = stQuery + " from "
                stQuery = stQuery + " ot_invoice_head b,ot_invoice_item a,om_location c,om_address d"
                stQuery = stQuery + " where b.invh_no = " + txtRepTransNo.Text + " and"
                stQuery = stQuery + " b.invh_sys_id = a.invi_invh_sys_id and"
                stQuery = stQuery + " b.invh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"

            ElseIf TXN_Code = "SARTN" Then
                'lblRptINVSONO.Text = "Return No.:"
                lblReport.Text = "Sales Return Reprint"
                stQuery = stQuery + " select rownum,b.CSRH_NO ,to_char( b.CSRH_DT,'DD/MM/YYYY') as InvoiceDate, "
                stQuery = stQuery + " locn_name,"
                stQuery = stQuery + " d.addr_name|| d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||"
                stQuery = stQuery + "d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||"
                stQuery = stQuery + "d.ADDR_PROVINCE_CODE as Location_Address,"
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
                stQuery = stQuery + " (SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')) as disamt,"
                stQuery = stQuery & " (SELECT ITED_FC_AMT from OT_CUST_SALE_RET_ITEM_TED where ITED_I_SYS_ID= a.CSRI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM  from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')) as expamt,CSRH_SM_CODE as salesman"
                stQuery = stQuery + " from "
                stQuery = stQuery + " OT_CUST_SALE_RET_HEAD b,OT_CUST_SALE_RET_ITEM a,om_location c,om_address d"
                stQuery = stQuery + " where b.CSRH_NO = " + txtRepTransNo.Text + " and"
                stQuery = stQuery + " b.CSRH_SYS_ID = a.CSRI_CSRH_SYS_ID and"
                stQuery = stQuery + " b.CSRH_LOCN_CODE = c.locn_code and c.locn_addr_code = d.addr_code"
                lblRptRptType.Text = "Sales Return"
                lblRptINVSONO.Text = "SRTN. No.:"

            ElseIf TXN_Code = "SO" Then
                lblRptINVSONO.Text = "Order No.:"
                lblReport.Text = "Sales Order Reprint"
                stQuery = stQuery + " select rownum,b.soh_no ,to_char( b.soh_dt,'DD/MM/YYYY') as InvoiceDate,locn_name,"
                stQuery = stQuery + " d.addr_name||d.ADDR_LINE_1||d.ADDR_LINE_2||d.ADDR_LINE_3||d.ADDR_LINE_4||d.ADDR_CITY_CODE||d.ADDR_COUNTY_CODE||d.ADDR_STATE_CODE||d.ADDR_ZIP_POSTAL_CODE||d.ADDR_COUNTRY_CODE||d.ADDR_PROVINCE_CODE as Location_Address,"
                stQuery = stQuery + " d.addr_tel as Phone,d.addr_email as Email,"
                stQuery = stQuery + " case nvl(b.soH_FLEX_03,0) when '0' then (select cust_name from om_customer where cust_code = b.soh_cust_code)"
                stQuery = stQuery + " else (select PM_PATIENT_NAME from om_patient_master where pm_cust_code = b.soh_flex_03) end as CustName,"
                stQuery = stQuery + " b.soh_BILL_ADDR_LINE_1||' '||b.soh_BILL_ADDR_LINE_2||' '||b.soh_BILL_COUNTRY_CODE as billing_addr,b.soH_BILL_TEL as billing_phone, b.soh_BILL_EMAIL as billing_email, b.soh_SHIP_ADDR_LINE_1||' '||b.soh_SHIP_ADDR_LINE_2||' '||b.soh_SHIP_COUNTRY_CODE as shipping_addr,"
                stQuery = stQuery + " a.soI_ITEM_CODE as ItemCode,a.soI_ITEM_DESC as ItemDesc,a.soI_UOM_CODE as ItmUOM,a.soI_PL_RATE as ItmPrice ,a.soI_QTY as ItmQty,a.soI_FC_VAL as ItmAmt,nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDDIS')),0) as disamt, "
                stQuery = stQuery & " nvl((select ITED_FC_AMT from OT_SO_ITEM_TED where ITED_I_SYS_ID= SOI_SYS_ID and ITED_TED_TYPE_NUM=(select TED_TAX_DISC_EXP_NUM from OM_TED_TYPE where TED_TYPE_CODE='TEDEXP')),0) as expamt,SOH_SM_CODE as salesman"
                stQuery = stQuery + " from "
                stQuery = stQuery + " ot_so_head b,ot_so_item a, om_location c,om_address d"
                stQuery = stQuery + " where b.soh_no = " + txtRepTransNo.Text + " and b.soh_sys_id = a.soi_soh_sys_id and b.soh_locn_code = c.locn_code and c.locn_addr_code = d.addr_code"
                lblLblAdvance.Visible = True
                lblLblBalance.Visible = True

                Dim stBalanceQuery As String
                stBalanceQuery = "select sum(pinvp_fc_val) as advance from ot_pos_so_payment a,ot_so_head b where b.soh_no = " + txtRepTransNo.Text + " and b.soh_sys_id = a.pinvp_invh_sys_id "
                Dim dsb As DataSet = db.SelectFromTableODBC(stBalanceQuery)
                If dsb.Tables("Table").Rows.Count > 0 Then
                    errLog.WriteToErrorLog("BalanceCheck Query", stBalanceQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "BALANCECHECK QUERY  -  " & stBalanceQuery)
                    If Not dsb.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                        lblRptAdvancedPaid.Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                        'MsgBox(lblRptAdvancedPaid.Text)
                    Else
                        lblRptAdvancedPaid.Text = "0"
                        ' MsgBox(lblRptAdvancedPaid.Text)
                    End If
                End If
            End If
            errLog.WriteToErrorLog("REPORT QUERY", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "REPORT QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim count As Integer = ds.Tables("Table").Rows.Count
            If count > 0 Then
                lblRptInvoiceNo.Text = ds.Tables("Table").Rows.Item(0).Item(1).ToString
                lblRptInvoiceDate.Text = ds.Tables("Table").Rows.Item(0).Item(2).ToString
                lblRptLocationName.Text = ds.Tables("Table").Rows.Item(0).Item(3).ToString
                lblRptLocationAddress.Text = ds.Tables("Table").Rows.Item(0).Item(4).ToString
                If Not ds.Tables("Table").Rows.Item(0).Item(5).ToString = "" Then
                    lblPhone.Text = "Phone: " + ds.Tables("Table").Rows.Item(0).Item(5).ToString
                Else
                    lblPhone.Text = "Phone: " + "   -             "
                End If
                If Not ds.Tables("Table").Rows.Item(0).Item(6).ToString = "" Then
                    lblEmail.Text = "Email: " + ds.Tables("Table").Rows.Item(0).Item(6).ToString
                Else
                    lblEmail.Text = "Email: " + "   -             "
                End If
                If txtPatPatientNo.Text = "" Then
                    lblRptCustomerName.Text = ds.Tables("Table").Rows.Item(0).Item(7).ToString
                    lblRptBillingAddress.Text = ds.Tables("Table").Rows.Item(0).Item(8).ToString
                    lblRptCustomerPhone.Text = ds.Tables("Table").Rows.Item(0).Item(9).ToString
                    lblRptCustomerEmail.Text = ds.Tables("Table").Rows.Item(0).Item(10).ToString
                    lblRptShippingAddress.Text = ds.Tables("Table").Rows.Item(0).Item(11).ToString
                Else
                    Dim stQueryPatient As String
                    stQueryPatient = "select PM_PATIENT_NAME as PatName,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as ShipAddr,PM_TEL_MOB,PM_EMAIL,PM_ADDRESS_1||PM_ADDRESS_2||PM_ADDRESS_3||PM_ADDRESS_4||PM_ADDRESS_5||PM_CITY||PM_COUNTRY||PM_REGION||PM_ZIPCODE as BillAddr from om_patient_master where PM_CUST_NO = '" + txtPatPatientNo.Text + "'"
                    Dim dsP As DataSet = db.SelectFromTableODBC(stQueryPatient)
                    If dsP.Tables("Table").Rows.Count > 0 Then
                        lblRptCustomerName.Text = dsP.Tables("Table").Rows.Item(0).Item(0).ToString
                        lblRptBillingAddress.Text = dsP.Tables("Table").Rows.Item(0).Item(4).ToString
                        lblRptCustomerPhone.Text = dsP.Tables("Table").Rows.Item(0).Item(2).ToString
                        lblRptCustomerEmail.Text = dsP.Tables("Table").Rows.Item(0).Item(3).ToString
                        lblRptShippingAddress.Text = dsP.Tables("Table").Rows.Item(0).Item(1).ToString
                    End If
                End If
                lblTotalPages.Text = "Tot.Pages: " + Math.Ceiling(count / 6).ToString

                lblSalesmanName.Text = ds.Tables("Table").Rows.Item(0).Item(20).ToString

            End If
            Dim i As Integer = 0

            Dim j As Integer = 0

            While count > 0

                Dim rowItem(7) As String
                rowItem(0) = ds.Tables("Table").Rows.Item(i).Item(0).ToString
                rowItem(1) = ds.Tables("Table").Rows.Item(i).Item(12).ToString
                rowItem(2) = ds.Tables("Table").Rows.Item(i).Item(13).ToString
                rowItem(3) = ds.Tables("Table").Rows.Item(i).Item(14).ToString
                rowItem(4) = ds.Tables("Table").Rows.Item(i).Item(15).ToString
                rowItem(5) = ds.Tables("Table").Rows.Item(i).Item(16).ToString
                rowItem(6) = ds.Tables("Table").Rows.Item(i).Item(17).ToString
                rowItem(7) = ds.Tables("Table").Rows.Item(i).Item(18).ToString
                i = i + 1
                count = count - 1
                rptList.Add(rowItem)
            End While
            Dim totalAmt As Double = 0
            Dim subtotalAmt As Double = 0
            Dim disAmt As Double = 0
            i = 0
            count = ds.Tables("Table").Rows.Count
            While count > 0

                If i / 5 > 1 Then
                    lblRptSubTotal.Visible = False
                    lblRptDiscount.Visible = False
                    lblRptTOTAL.Visible = False
                    lblRptExpense.Visible = False
                    lblRptAdvancedPaid.Visible = False
                    lblRptBalance.Visible = False

                    Exit Sub
                Else
                    If TXN_Code = "SO" Then
                        lblRptAdvancedPaid.Visible = True
                        lblRptBalance.Visible = True

                    End If
                    If transtype = "Sales Invoice" Then
                        lblRptAdvancedPaid.Visible = True
                        lblRptBalance.Visible = True
                        lblLblAdvance.Visible = True
                        lblLblBalance.Visible = True
                        lblRptRptType.Text = "Sales Invoice"

                    End If
                End If

                Me.pnlRptItemsHolder.AutoScrollPosition = New System.Drawing.Point(0, 0)
                Dim lbl As Label
                Dim n As Integer
                n = i + 1
                lbl = New Label
                With lbl
                    .Location = New Point(0, (n * 40))
                    .Name = "lblSNO" & n.ToString
                    .Size = New Size(lblRptSNOHead.Width - 1, 20)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = n
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptItemCodeHead.Location.X + 1, (n * 40))
                    .Name = "lblItemCode" & n.ToString
                    .Size = New Size(lblRptItemCodeHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(12).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptItemDescHead.Location.X + 1, (n * 40))
                    .Name = "lblItemDesc" & n.ToString
                    .Size = New Size(lblRptItemDescHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(13).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptUOMHead.Location.X + 1, (n * 40))
                    .Name = "lblItemUOM" & n.ToString
                    .Size = New Size(lblRptUOMHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(14).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptRateHead.Location.X + 1, (n * 40))
                    .Name = "lblItemRate" & n.ToString
                    .Size = New Size(lblRptRateHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(15).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptQtyHead.Location.X + 1, (n * 40))
                    .Name = "lblItemQty" & n.ToString
                    .Size = New Size(lblRptQtyHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopCenter
                End With
                lbl.Text = ds.Tables("Table").Rows.Item(i).Item(16).ToString
                Me.pnlRptItemsHolder.Controls.Add(lbl)

                lbl = New Label
                With lbl
                    .Location = New Point(lblRptAmtHead.Location.X + 1, (n * 40))
                    .Name = "lblItemAmt" & n.ToString
                    .Size = New Size(lblRptAmtHead.Width - 2, 35)
                    .TextAlign = ContentAlignment.TopRight
                End With

                lbl.Text = Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(17).ToString), 3).ToString(".000")
                Me.pnlRptItemsHolder.Controls.Add(lbl)
                subtotalAmt = subtotalAmt + Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(17).ToString), 3)
                If ds.Tables("Table").Rows.Item(i).Item(18).ToString = "" Then
                    disAmt = disAmt + 0
                Else
                    disAmt = disAmt + Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(18).ToString), 3)
                End If

                expenseAmount = expenseAmount + Round(Convert.ToDouble(ds.Tables("Table").Rows.Item(i).Item(19).ToString), 3)

                lblRptSubTotal.Text = subtotalAmt.ToString("0.000")
                lblRptDiscount.Text = disAmt.ToString("0.000")
                totalAmt = Round(subtotalAmt - disAmt, 3)
                lblRptTOTAL.Text = totalAmt.ToString("0.000")

                i = i + 1
                count = count - 1
            End While
            callReportTotal()
            If transtype = "Sales Invoice" Then
                lblRptINVSONO.Text = "Sales Invoice No.:"
                lblRptAdvancedPaid.Text = Convert.ToDouble(txtAdvPaid.Text).ToString("0.000")
                lblRptBalance.Text = Convert.ToDouble(txtBalancePay.Text).ToString("0.000")
                Label22.Visible = True
                salesorders = ds.Tables("Table").Rows.Item(0).Item(21).ToString
                Label28.Text = salesorders

                Dim stBalanceQuery As String
                stBalanceQuery = "select sum(pinvp_fc_val) as advance from ot_pos_so_payment a,ot_so_head b where b.soh_no = " & ds.Tables("Table").Rows.Item(0).Item(21).ToString & " and b.soh_sys_id = a.pinvp_invh_sys_id "
                Dim dsb As DataSet = db.SelectFromTableODBC(stBalanceQuery)
                If dsb.Tables("Table").Rows.Count > 0 Then
                    errLog.WriteToErrorLog("BalanceCheck Query", stBalanceQuery, "")
                    'MAILLOGROWNUM = MAILLOGROWNUM + 1
                    'MAILLOGValues.Add(MAILLOGROWNUM, "BALANCECHECK QUERY  -  " & stBalanceQuery)
                    If Not dsb.Tables("Table").Rows.Item(0).Item(0).ToString = "" Then
                        lblRptAdvancedPaid.Text = Convert.ToDouble(dsb.Tables("Table").Rows.Item(0).Item(0).ToString).ToString("0.000")
                        'MsgBox(lblRptAdvancedPaid.Text)
                    Else
                        lblRptAdvancedPaid.Text = "0"
                        ' MsgBox(lblRptAdvancedPaid.Text)
                    End If
                End If

            End If

            PnlGrpBoxCont.Hide()
            pnlReport.Show()
            pnlReport.BringToFront()
            pnlButtonHolder.Show()
            pnlReport.Location = New Point(0, 0)
            pnlReport.Height = Me.Height
            'If txtRepTransNo.Text = lblRptInvoiceNo.Text Then
            '    pnlReport.Show()
            '    pnlReport.BringToFront()
            '    pnlButtonHolder.Show()
            '    pnlReport.Location = New Point(0, 0)
            '    pnlReport.Height = Me.Height
            'Else
            '    pnlReport.Hide()
            '    PnlGrpBoxCont.Show()
            '    lblReprintReport.Text = "No Data Found"
            '    'MsgBox("Please check transaction type and transaction No.")
            'End If

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ChkInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkInv.CheckedChanged
        If ChkInv.Checked = True Then
            ChkSI.Checked = False
            ChkSO.Checked = False
            ChkSR.Checked = False
        End If
    End Sub
    Private Sub ChkSO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSO.CheckedChanged
        If ChkSO.Checked = True Then
            ChkInv.Checked = False
            ChkSI.Checked = False
            ChkSR.Checked = False
        End If
    End Sub
    Private Sub ChkSI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSI.CheckedChanged
        If ChkSI.Checked = True Then
            ChkInv.Checked = False
            ChkSO.Checked = False
            ChkSR.Checked = False
        End If
    End Sub
    Private Sub ChkSR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSR.CheckedChanged
        If ChkSR.Checked = True Then
            ChkInv.Checked = False
            ChkSO.Checked = False
            ChkSI.Checked = False
        End If
    End Sub
    Private Sub btnRepCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepCancel.Click
        Try
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlButtonHolder.Controls
                ctl.Enabled = True
            Next
            Dim i As Integer = 0
            i = PnlGrpBoxCont.Width
            PnlGrpBoxCont.BringToFront()
            Do While i > 0
                PnlGrpBoxCont.Location = New Point(Me.Width - 2, Me.Height - PnlGrpBoxCont.Height)
                i = i - 2
            Loop
            PnlGrpBoxCont.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub
    Private Sub radMale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radMale.CheckedChanged
        If radMale.Checked = True Then
            radFemale.Checked = False
        ElseIf radMale.Checked = False Then
            radFemale.Checked = True
        End If
    End Sub

    Private Sub txtDiscPercValue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDiscPercValue.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtDiscPercValue_LostFocus(sender, e)
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtDiscPercValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscPercValue.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") < 0) Or (e.KeyChar = "." And tbx.Text.IndexOf(".") >= 0 And tbx.SelectionLength > 0 And tbx.SelectionStart <= tbx.Text.IndexOf(".") And tbx.SelectionStart + tbx.SelectionLength > tbx.Text.IndexOf("."))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub txtDiscPercValue_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPercValue.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 0
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0

            ElseIf value >= 0 Then
                tbx.Text = Round(value, 2)
            Else
                tbx.Text = Abs(Round(value, 2))
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub txtDiscPercValue_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPercValue.LostFocus
        Try
            pnlMasked.Visible = False
            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" & lastActiveItem, True)
            ItmDisamtFound(0).Text = txtDiscPercValueAmt.Text
            ItmDisamtFound(0).Select()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub txtDiscPercValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPercValue.TextChanged
        Try
            Dim ItmQtyFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemQty" & lastActiveItem, True)
            Dim ItmPriceFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemPrice" & lastActiveItem, True)
            'MsgBox(ItmQtyFound(0).Text & "      " & ItmPriceFound(0).Text)
            Dim discperc As Double = Round(Convert.ToDouble(txtDiscPercValue.Text), 2)
            Dim qty As Double = Round(Convert.ToDouble(ItmQtyFound(0).Text), 2)
            Dim price As Double = Round(Convert.ToDouble(ItmPriceFound(0).Text), 3)
            txtDiscPercValueAmt.Text = (qty * price) * (discperc / 100)
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub FindItemDisamt_Hover(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ttDisamt As New ToolTip()
        ttDisamt.SetToolTip(DirectCast(sender, TextBox), "Press F12 for Discount Percentage!")
    End Sub

    Private Sub btnPatientNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientNext.Click
        Try
            Dim row As System.Data.DataRow
            Dim patientno As String
            btnPatientPrev.Enabled = True
            toti = toti + 1
            If toti < totcount Then
                row = totds.Tables("Table").Rows.Item(toti)
                If row.Item(4).ToString = "MALE" Then
                    radMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    radFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtpickPatDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                txtPatientNo.Text = row.Item(17).ToString


            ElseIf toti = totcount Then
                btnPatientNext.Enabled = False
                btnPatientPrev.Enabled = True
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnPatientPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientPrev.Click
        Try
            Dim row As System.Data.DataRow
            Dim patientno As String

            toti = toti - 1
            btnPatientNext.Enabled = True
            If toti >= 0 Then
                row = totds.Tables("Table").Rows.Item(toti)
                If row.Item(4).ToString = "MALE" Then
                    radMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    radFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtpickPatDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtpickPatDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                txtPatientNo.Text = row.Item(17).ToString

            ElseIf toti < 0 Then
                btnPatientNext.Enabled = True
                btnPatientPrev.Enabled = False
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCurrencyType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrencyType.SelectedValueChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "SELECT CER_EXG_RATE FROM FM_EXCHANGE_RATE WHERE CER_CONV_FM_CURR_CODE = '" & txtCurrencyType.Text & "' AND CER_CONV_TO_CURR_CODE = '" & Currency_Code & "' AND CER_EXG_RATE_TYPE= 'B' AND '" & dtpick.Value.ToString("dd-MMM-yy") & "' BETWEEN CER_EFF_FRM_DT AND CER_EFF_TO_DT"
            errLog.WriteToErrorLog(stQuery, "", "Exchange rate query")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "EXCHANGE RATE QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            Dim Exchange_Rate_val As String = Exchange_Rate
            If ds.Tables("Table").Rows.Count Then
                Exchange_Rate = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            If Not Amountpayable.Text = "" Then
                Dim amtaed As Double = Convert.ToDouble(Amountpayable.Text) * Convert.ToDouble(Exchange_Rate_val)
                amountinaed.Text = Round(amtaed, 3).ToString("0.000")
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Function checkCustomerCode() As Boolean
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim success As Boolean = False
            stQuery = " SELECT CUST_CODE FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) and CUST_CODE='" & txtCustomerCode.Text.Split(" - ")(0).ToString & "'"
            'stQuery = "select CUST_NAME from OM_CUSTOMER where CUST_CODE='" & txtCustomerCode.Text.Split(" - ")(0).ToString & "'"
            errLog.WriteToErrorLog("check customer query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "CHECK CUSTOMER QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                success = True
                'txtCustomerCode.SelectedText = Setup_Values("CUST_CODE").Trim & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                'txtCustomerName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                'txtCustomerCode.Text = Setup_Values("CUST_CODE") & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            Return success
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Function

    Public Function checkSalesmanCode() As Boolean
        Try
            Dim stQuery As String
            Dim ds As DataSet
            Dim count As Integer
            Dim success As Boolean = False
            stQuery = "SELECT SM_CODE || ' - ' ||  SM_NAME , SM_NAME  FROM OM_SALESMAN WHERE SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE='" & txtSalesmanCode.Text.Split(" - ")(0).ToString & "'"
            'stQuery = " SELECT CUST_CODE FROM OM_CUSTOMER WHERE CUST_FRZ_FLAG_NUM = 2 AND (CUST_CREDIT_CTRL_YN = 'N' AND CUST_REGULAR_YN_NUM = 1) and CUST_CODE='" & txtSalesmanCode.Text.Split(" - ")(0).ToString & "'"
            'stQuery = "select CUST_NAME from OM_CUSTOMER where CUST_CODE='" & txtCustomerCode.Text.Split(" - ")(0).ToString & "'"
            errLog.WriteToErrorLog("check salesman query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "CHECK SALESMAN QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                success = True
                'txtCustomerCode.SelectedText = Setup_Values("CUST_CODE").Trim & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                'txtCustomerName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString.Trim
                'txtCustomerCode.Text = Setup_Values("CUST_CODE") & " - " & ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If

            Return success
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Function

    Public Sub clickPatientF1Key(ByVal sender As Object, ByVal e As System.EventArgs)
        btnPatient_Click(sender, e)
    End Sub

    Public Sub clickAddItemF2Key(ByVal sender As Object, ByVal e As System.EventArgs)
        btnAddItem_Click(sender, e)
    End Sub

    Public Sub clickPaymentF3Key(ByVal sender As Object, ByVal e As System.EventArgs)
        btnPayments_Click(sender, e)
    End Sub

    Public Sub clickCustomerF5Key(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCustomerCode.Select()
    End Sub

    Public Sub clickCancelInvoiceF10Key(ByVal sender As Object, ByVal e As System.EventArgs)
        btnCancelInvoice_Click(sender, e)
    End Sub

    Private Sub btnHoldInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHoldInvoice.Click
        Try
            If Not CheckShiftTimings() Then
                MsgBox("Transactions Cannot be proceeded", MsgBoxStyle.Critical, "Shift Timings Alert")
                Exit Sub
            End If
            If INVHNO > 0 Then
                MsgBox("Please cancel the current Invoice Transaction!")
                Exit Sub
            ElseIf SOHNO > 0 Then
                MsgBox("Please cancel the current Sales Order Transaction!")
                Exit Sub
            ElseIf transtype = "Sales Return" Then
                MsgBox("Please cancel the current Sales Return Transaction!")
                Exit Sub
            End If

            'If TXN_Code = "POSINV" Then

            'Else
            '    MsgBox("Please change transaction type as 'Invoice'")
            '    Exit Sub
            'End If

            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = False
            Next

            Dim btn As Button = New Button
            With btn
                .Name = "btnHoldAdd"
                .Location = New Point(txtHoldNO.Location.X + txtHoldNO.Width + 20, txtHoldNO.Location.Y - 1)
                .Size = New Size(60, 22)
                .BackColor = Color.MediumTurquoise
                .Text = "Add"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnHoldAdd_Click
            Me.pnlHoldAdd.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnHoldOk"
                .Location = New Point(pnlHoldSearch.Width / 2 + 55, lstviewHoldInvoices.Location.Y + lstviewHoldInvoices.Height + 25)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Ok"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnHoldOK_Click
            Me.pnlHoldSearch.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnHoldCancel"
                .Location = New Point(pnlHoldSearch.Width / 2 + 125, lstviewHoldInvoices.Location.Y + lstviewHoldInvoices.Height + 25)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Cancel"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnHoldCancel_Click
            Me.pnlHoldSearch.Controls.Add(btn)

            'btn = New Button
            'With btn
            '    .Name = "btnSOCancel"
            '    .Location = New Point(pnlSOlstview.Width / 2 + 75, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
            '    .Size = New Size(65, 23)
            '    .BackColor = Color.MediumTurquoise
            '    .Text = "Cancel"
            '    .Font = New Font(btn.Font, FontStyle.Bold)
            '    .ForeColor = Color.SaddleBrown
            'End With
            'AddHandler btn.Click, AddressOf Me.btnSOCancel_Click
            'Me.pnlSOlstview.Controls.Add(btn)

            'btn = New Button
            'With btn
            '    .Name = "btnRemoveSO"
            '    .Location = New Point(pnlSOlstview.Width / 2 + 145, lstviewSOSelected.Location.Y + lstviewSOSelected.Height + 30)
            '    .Size = New Size(65, 23)
            '    .BackColor = Color.MediumTurquoise
            '    .Text = "Remove"
            '    .Font = New Font(btn.Font, FontStyle.Bold)
            '    .ForeColor = Color.SaddleBrown
            'End With
            'AddHandler btn.Click, AddressOf Me.btnRemoveSO_Click
            'Me.pnlSOlstview.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnHoldSearch"
                .Location = New Point(dtpickHoldTo.Location.X + dtpickHoldTo.Width + 20, dtpickHoldTo.Location.Y)
                .Size = New Size(65, 23)
                .BackColor = Color.MediumTurquoise
                .Text = "Search"
                .Font = New Font(btn.Font, FontStyle.Bold)
                .ForeColor = Color.SaddleBrown
            End With
            AddHandler btn.Click, AddressOf Me.btnHoldSearch_Click
            Me.pnlHoldSearch.Controls.Add(btn)

            Dim dtQuery As String
            Dim dt As DataSet
            dtQuery = "select to_char(sysdate,'dd-mm-yyyy') from dual"
            'errLog.WriteToErrorLog(dtQuery, "", "")
            dt = db.SelectFromTableODBC(dtQuery)
            dtpickHoldFrom.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)
            dtpickHoldTo.Value = DateTime.ParseExact(dt.Tables("Table").Rows.Item(0).Item(0), "dd-MM-yyyy", Nothing)

            'MsgBox(Button1.Height.ToString + "," + Button1.Width.ToString)
            pnlHoldInvoice.Height = Me.Height
            pnlButtonHolder.Visible = False
            pnlButtonHolder.SendToBack()
            pnlHoldInvoice.BringToFront()
            'pnl1Patient.Width = Me.Width
            For i = 0 To pnlHoldInvoice.Width
                pnlHoldInvoice.Location = New Point(Me.Width - i, Me.Height - pnlHoldInvoice.Height)
                pnlHoldInvoice.Show()
                'Threading.Thread.Sleep(0.5)
                i = i + 5
            Next i
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnHoldAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtHoldNO.Text = "" Then

        Else
            If chkboxHoldDINV.CheckState = CheckState.Checked Then
                LoadHoldedTransactionsDetials(txtHoldNO.Text, "DINV")
            ElseIf chkboxHoldSO.CheckState = CheckState.Checked Then
                LoadHoldedTransactionsDetials(txtHoldNO.Text, "SO")
            End If
        End If
    End Sub

    Private Sub btnHoldSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim i As Integer = 0
            If chkboxHoldDINV.CheckState = CheckState.Checked Then
            stQuery = "select distinct INVH_NO,to_char(INVH_DT,'dd-MM-yyyy') from OT_POS_INVOICE_ITEM_LOG,OT_POS_INVOICE_HEAD_LOG where PROD_INVI_INVH_SYS_ID=INVH_SYS_ID and INVH_STATUS=4 AND INVH_COMP_CODE='" & CompanyCode & "' AND INVH_DOC_SRC_LOCN_CODE='" & Location_Code & "' AND INVH_DT>=TO_DATE('" & dtpickHoldFrom.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') AND INVH_DT<=TO_DATE('" & dtpickHoldTo.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') ORDER BY INVH_NO"
            'stQuery = " SELECT SOH_TXN_CODE, SOH_NO, to_char(SOH_DT,'dd-MM-yyyy') as SOH_DT FROM OT_SO_HEAD,OT_INVOICE_REF,OM_TXN_LINK WHERE SOH_COMP_CODE ='" + CompanyCode + "' AND SOH_LOCN_CODE ='" + Location_Code + "' AND TXNL_TXN_CODE ='POSINV' AND SOH_TXN_CODE = TXNL_PREV_TXN_CODE AND SOH_TXN_CODE =INVR_REF_TXN_CODE(+) AND SOH_NO = INVR_REF_NO (+) AND SOH_DT >= TO_DATE('" + dtpickSOFromDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND SOH_DT <= TO_DATE('" + dtpickSOToDate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND INVR_REF_NO IS NULL AND SOH_NO NOT IN (SELECT SOH_NO FROM OT_POS_SO_HEAD_LOG WHERE SOH_STATUS = 5 ) ORDER BY SOH_NO DESC "
            ElseIf chkboxHoldSO.CheckState = CheckState.Checked Then
                stQuery = "select distinct SOH_NO,to_char(SOH_DT,'dd-MM-yyyy') from OT_POS_SO_ITEM_LOG,OT_POS_SO_HEAD_LOG where PROD_SOI_SOH_SYS_ID=SOH_SYS_ID and SOH_STATUS=4 AND SOH_COMP_CODE='" & CompanyCode & "' AND SOH_DOC_SRC_LOCN_CODE='" & Location_Code & "' AND SOH_DT>=TO_DATE('" & dtpickHoldFrom.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') AND SOH_DT<=TO_DATE('" & dtpickHoldTo.Value.ToString("dd-MM-yyyy") & "','dd-MM-yyyy') ORDER BY SOH_NO"
            End If
            errLog.WriteToErrorLog("Hold Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "HOLD QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            lstviewHoldInvoices.Items.Clear()
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstviewHoldInvoices.Items.Add(i + 1)
                lstviewHoldInvoices.Items(i).SubItems.Add(row.Item(0).ToString)
                lstviewHoldInvoices.Items(i).SubItems.Add(row.Item(1).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub lstviewHoldInvoices_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstviewHoldInvoices.DoubleClick
        Try
            If lstviewHoldInvoices.SelectedItems.Count < 1 Then
                MsgBox("Please select one from the list!")
                Exit Sub
            End If
            Dim transno As String = lstviewHoldInvoices.SelectedItems.Item(0).SubItems(1).Text
            If chkboxHoldDINV.CheckState = CheckState.Checked Then
                LoadHoldedTransactionsDetials(transno, "DINV")
            ElseIf chkboxHoldSO.CheckState = CheckState.Checked Then
                LoadHoldedTransactionsDetials(transno, "SO")
            End If

        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "ERROR")
        End Try
    End Sub

    Private Sub btnHoldOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lstviewHoldInvoices_DoubleClick(sender, e)
    End Sub

    Private Sub LoadHoldedTransactionsDetials(ByVal transno As String, ByVal transtype As String)
        Dim stQuery As String = ""
        Dim ds As DataSet
        Dim count As Integer
        Dim row As System.Data.DataRow
        Dim i As Integer = 0
        If transtype = "DINV" Then
            stQuery = "select INVH_SYS_ID,INVH_NO,PRODCODE,PRODDESC,PRODQTY,PRODRATE,PRODDISCCODE,PRODDISCAMT,PRODEXPCODE,PRODEXPAMT,PRODPRICE,INVH_CUST_CODE,INVH_SM_CODE from OT_POS_INVOICE_ITEM_LOG,OT_POS_INVOICE_HEAD_LOG where PROD_INVI_INVH_SYS_ID=INVH_SYS_ID and INVH_NO=" & transno
            errLog.WriteToErrorLog("Hold Invoice Details Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "HOLD INVOICE DETAILS QUERY  -  " & stQuery)
        ElseIf transtype = "SO" Then
            stQuery = "select SOH_SYS_ID,SOH_NO,PRODCODE,PRODDESC,PRODQTY,PRODRATE,PRODDISCCODE,PRODDISCAMT,PRODEXPCODE,PRODEXPAMT,PRODPRICE,SOH_CUST_CODE,SOH_SM_CODE from OT_POS_SO_ITEM_LOG,OT_POS_SO_HEAD_LOG where PROD_SOI_SOH_SYS_ID=SOH_SYS_ID and SOH_NO=" & transno
            errLog.WriteToErrorLog("Hold Invoice Details Query DINV", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "hOLD INVOICE DETAILS QUERY SO  -  " & stQuery)
        End If
        ds = db.SelectFromTableODBC(stQuery)
        count = ds.Tables("Table").Rows.Count
        If count > 0 Then
            row = ds.Tables("Table").Rows.Item(0)
            If transtype = "DINV" Then
                INVHSYSID = row.Item(0)
                INVHNO = row.Item(1)
                pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                For z = oldloc To newloc
                    pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                    Threading.Thread.Sleep(1)
                Next
                pnlBtnSliderHolder.Enabled = False
            ElseIf transtype = "SO" Then
                TXN_Code = "SO"
                SOHSYSID = row.Item(0)
                SOHNO = row.Item(1)
                picboxTransactionType.Location = New Point(0, picboxTransactionType.Location.Y)
                pnlBtnSliderHolder.Width = pnlBtnSliderHolder.Width / 2
                Dim oldloc As Integer = pnlBtnSliderHolder.Location.X
                Dim newloc As Integer = pnlBtnSliderHolder.Location.X + (pnlBtnSliderHolder.Width)
                For z = oldloc To newloc
                    pnlBtnSliderHolder.Location = New Point(z, pnlBtnSliderHolder.Location.Y)
                    Threading.Thread.Sleep(1)
                Next
                pnlBtnSliderHolder.Enabled = False
                picboxTransactionType.Image = My.Resources.sales
                btnCancelInvoice.Text = "Cancel Order"
            End If
            txtCustomerCode.Text = row.Item(11)
            txtSalesmanCode.Text = row.Item(12)
           
        Else
            MsgBox("Unable to retrieve Items from Holding!")
            Exit Sub
        End If
            Dim sender As New System.Object
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Dim pos As Integer = txtItemCode.Count
                btnAddItem_Click(sender, New System.EventArgs)
                txtItemCode(i).Text = row.Item(2).ToString
                txtItemDesc(i).Text = row.Item(3).ToString
                txtItemQty(i).Text = row.Item(4).ToString
                txtItemPrice(i).Text = row.Item(5).ToString
                txtItemDisc(i).Text = row.Item(6).ToString
                txtItemDisamt(i).Text = row.Item(7).ToString
                txtItemAddvalCode(i).Text = row.Item(8).ToString
                txtItemAddval(i).Text = row.Item(9).ToString
                txtItemNetamt(i).Text = row.Item(10).ToString
                btnHoldCancel_Click(sender, New System.EventArgs)
                count = count - 1
                i = i + 1
            End While
            btnAddItem_Click(sender, New System.EventArgs)
    End Sub

    Private Sub btnPutTransHold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPutTransHold.Click
        If TXN_Code = "POSINV" Or TXN_Code = "SO" Then
            Dim result As DialogResult = MsgBox("Do you want to put this Transaction on HOLD?", MessageBoxButtons.YesNo, "Alert")
            If result = Windows.Forms.DialogResult.Yes Then
                If TXN_Code = "POSINV" Then
                    MsgBox("Direct Invoice Transaction No.: " & INVHNO.ToString & " is put on Hold!")
                ElseIf TXN_Code = "SO" Then
                    MsgBox("Sales Order Transaction No.: " & SOHNO.ToString & " is put on Hold!")
                End If
                Home.NewTransaction_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub chkboxHoldDINV_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkboxHoldDINV.CheckStateChanged
        Try
            Dim tbx As System.Windows.Forms.CheckBox = sender
            If tbx.CheckState = CheckState.Checked Then
                chkboxHoldSO.CheckState = CheckState.Unchecked
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub chkboxHoldSO_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkboxHoldSO.CheckStateChanged
        Try
            Dim tbx As System.Windows.Forms.CheckBox = sender
            If tbx.CheckState = CheckState.Checked Then
                chkboxHoldDINV.CheckState = CheckState.Unchecked
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    'Private Sub lstboxItemNames_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstboxItemNames.KeyUp
    '    Try
    '        e.SuppressKeyPress = True
    '        If e.KeyCode = Keys.Enter Then
    '            MsgBox("")
    '            If lstboxItemNames.SelectedItems.Count > 0 Then
    '                Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & lastActiveItem, True)
    '                ItmCodeFound(0).Text = lstboxItemNames.SelectedItems.Item(0).ToString
    '            End If
    '        End If
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    Public Sub lstboxItemName_LostFocusCall(ByVal sender As Object, ByVal e As System.EventArgs)
        lstboxItemNames_LostFocus(sender, e)
    End Sub

    Private Sub lstboxItemNames_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxItemNames.DoubleClick
        If lstboxItemNames.SelectedItems.Count > 0 Then
            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & lastActiveItem, True)
            ItmCodeFound(0).Text = lstboxItemNames.SelectedItems.Item(0).ToString
            lstboxItemName_LostFocusCall(sender, e)
            lstboxItemNames.Items.Clear()
            pnlItemNameListHolder.Visible = False
        End If
    End Sub

    Private Sub lstboxItemNames_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxItemNames.LostFocus
        Try
            pnlItemNameListHolder.Visible = False
            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & lastActiveItem, True)
            ItmCodeFound(0).Select()
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Sub loadItems(ByVal itemCode As String, ByVal ItmCodeFound As System.Windows.Forms.Control)
        Try
            Dim stQuery As String = ""
            Dim count As Integer
            Dim i As Integer = 0
            Dim ds As DataSet
            stQuery = "select distinct ITEM_CODE from OM_POS_ITEM where ITEM_CODE like '" & itemCode & "%' union select distinct ITEM_BAR_CODE from OM_POS_ITEM where ITEM_BAR_CODE like '" & itemCode & "%' order by ITEM_CODE"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            lstboxItemNames.Items.Clear()
            If count > 0 Then
                While count > 0
                    lstboxItemNames.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                    count = count - 1
                    i = i + 1
                End While
                lstboxItemNames.SetSelected(0, True)
                lstboxItemNames.Select()
                lstboxItemNames.Focus()

                With pnlItemNameListHolder
                    .BringToFront()
                    .Location = New Point(ItmCodeFound.Location.X + pnlItemDetails.Location.X + 1, ItmCodeFound.Location.Y + pnlINVDetails.Height + 27)
                    .Visible = True
                End With
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStock.Click
        Try
             
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = False
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = False
            Next

            pnlStockQuery.Height = Me.Height
            pnlButtonHolder.Visible = False
            pnlButtonHolder.SendToBack()
            pnlStockQuery.BringToFront()

            'For i = 0 To pnlStockQuery.Width
            '    pnlStockQuery.Location = New Point(Me.Width - i, Me.Height - pnlStockQuery.Height)
            '    pnlStockQuery.Show()
            '    'Threading.Thread.Sleep(0.5)
            '    i = i + 1
            'Next
            pnlStockQuery.Location = New Point(Me.Width - pnlStockQuery.Width, Me.Height - pnlStockQuery.Height)
            pnlStockQuery.Show()

            'listProduct.Items.Clear()
            cbLocationfrom.Text = Location_Code
            cmbmaingrp.Text = ""
            cmbsubgrp.Text = ""
            cmbitemfrom.Text = ""
            cmbitemto.Text = ""
            cmbmaingrp.Select()
         

            'frmStockReport.btnCloseForm.Visible = True
            'frmStockReport.ShowDialog()
            'frmStockReport.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            'frmStockReport.WindowState = FormWindowState.Maximized
            'frmStockReport.StartPosition = FormStartPosition.WindowsDefaultBounds

            'frmStockReport.ControlBox = True
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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

    Public Sub CloseStockQuery(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            frmStockReport.Close()

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbsubgrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsubgrp.LostFocus
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
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)

        End Try
    End Sub

    Private Sub itemfrom()
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count
        Me.cmbitemfrom.DataSource = ds.Tables("Table")
        Me.cmbitemfrom.DisplayMember = "ItemCode"
        Me.cmbitemfrom.ValueMember = "ItemCode"
    End Sub

    Private Sub itemto()
        ds = db.SelectFromTableODBC(stQuery)
        Count = ds.Tables("Table").Rows.Count

        Me.cmbitemto.DataSource = ds.Tables("Table")
        Me.cmbitemto.DisplayMember = "ItemCode"
        Me.cmbitemto.ValueMember = "ItemCode"

    End Sub

    Public Sub listProduct_DrawColumnHeader(ByVal sender As Object, ByVal e As DrawListViewColumnHeaderEventArgs) Handles listProduct.DrawColumnHeader

        'comfun.changeListHeaderColor(e)

        Dim strFormat As New StringFormat()
        strFormat.Alignment = StringAlignment.Center
        strFormat.LineAlignment = StringAlignment.Center
        e.DrawBackground()
        e.Graphics.FillRectangle(Brushes.DarkCyan, e.Bounds)
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

    Private Sub btnViewStockQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewStockQuery.Click

        itemlist = ""
        conditionst = ""
        groupval = ""
        listProduct.Items.Clear()

        'lblNoList.Image = My.Resources.loading
        'lblNoList.Text = vbCrLf & vbCrLf & vbCrLf & "Loading..."
        'butAddExcel.Enabled = False
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

        lblNoList.Visible = True
        lblNoList.Text = "Loading..."
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
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub TestWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles TestWorker.ProgressChanged
        Try

        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Public Sub callbtnStockQueryClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnStock_Click(sender, e)
    End Sub

    Private Sub TestWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles TestWorker.RunWorkerCompleted
        Try
            datalist()
            listProduct.Focus()
            If listProduct.Items.Count > 0 Then
                lblNoList.Hide()
                listProduct.Items(0).Selected = True
            Else
                lblNoList.Text = "No Items Found"
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Public Sub callListProductDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If listProduct.SelectedItems.Count > 0 Then
                listProduct_DoubleClick(sender, e)
            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub listProduct_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles listProduct.DoubleClick
        'MsgBox(lastActiveItem)
        Try
            btnCancelStockQuery_Click(sender, e)
            If Not Me.Controls.Find("ItemCode" & lastActiveItem, True).Length = 0 Then
                Me.Controls.Find("ItemCode" & lastActiveItem, True)(0).Text = listProduct.SelectedItems.Item(0).SubItems(0).Text
                If Not Me.Controls.Find("ItemCode" & (lastActiveItem + 1), True).Length = 0 Then
                    Me.Controls.Find("ItemCode" & (lastActiveItem + 1), True)(0).Select()
                End If
            Else

            End If
        Catch ex As Exception
            qryLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Sub stocklist()
        Try
            stQuery = "SELECT om_item.item_code Item_Code,  lcs_grade_code_1 GradeCode_1, om_item.item_name Item_Description, A.item_bar_code Item_Bar_Code, A.item_price_type_1 AS PRICE1, A.item_price_type_2  AS PRICE2, ( ( lcs_stk_qty_bu + lcs_rcvd_qty_bu ) - ( lcs_issd_qty_bu + lcs_hold_qty_bu + lcs_reject_qty_bu +  lcs_overres_qty_bu + lcs_pick_qty_bu + lcs_pack_qty_bu)) AS Avail_Stock_Qty FROM   os_locn_curr_stk, om_item, om_pos_item A,  crm_om_location WHERE  om_item.item_code = A.item_code  AND om_item.item_code = lcs_item_code  AND om_item.item_frz_flag_num = 2  AND lcs_comp_code = '001'  AND lcs_locn_code = locn_code  " & conditionst & "  AND item_pli_pl_code = '" + Setup_Values.Item("PL_CODE") + "' " + itemlist + groupval + " GROUP  BY om_item.item_code,  om_item.item_name, A.item_bar_code,  locn_name,  om_item.item_uom_code,  lcs_grade_code_1,  lcs_grade_code_2,  A.item_pli_pl_code,  A.item_price_type_1,  A.item_price_type_2,  lcs_stk_qty_bu,  lcs_rcvd_qty_bu,  lcs_issd_qty_bu,  lcs_hold_qty_bu,  lcs_reject_qty_bu,  lcs_overres_qty_bu,  lcs_pick_qty_bu,  lcs_pack_qty_bu,  lcs_resv_qty_bu,  om_item.item_anly_code_01,  om_item.item_anly_code_02 HAVING SUM(( ( lcs_stk_qty_bu + lcs_rcvd_qty_bu ) -  ( lcs_issd_qty_bu + lcs_hold_qty_bu  + lcs_reject_qty_bu +  lcs_overres_qty_bu  + lcs_pick_qty_bu +  lcs_pack_qty_bu )  - lcs_resv_qty_bu )) > 0 ORDER  BY locn_name, item_code"
            errLog.WriteToErrorLog("Stock Query", stQuery, "")
            'MAILLOGROWNUM = MAILLOGROWNUM + 1
            'MAILLOGValues.Add(MAILLOGROWNUM, "STOCK QUERY  -  " & stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            dt = ds.Tables("Table")
            Count = ds.Tables("Table").Rows.Count
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Public Sub datalist()
        If Count <> 0 Then
            Dim i As Integer
            For i = 0 To ds.Tables("Table").Rows.Count - 1
                listProduct.Items.Add(dt.Rows(i).Item(0).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(6).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(1).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(2).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(3).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(4).ToString)
                listProduct.Items(i).SubItems.Add(dt.Rows(i).Item(4).ToString)

               
            Next
            lblNoList.Hide()
            'butAddExcel.Enabled = True
        Else
            lblNoList.Text = "No Records Found"
            'lblNoList.Image = Nothing
        End If
    End Sub

    
    Private Sub btnCancelStockQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelStockQuery.Click
        Try
            For Each ctl As Control In pnlINVDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlItemDetails.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlBottomHolder.Controls
                ctl.Enabled = True
            Next
            For Each ctl As Control In pnlButtonHolder.Controls
                ctl.Enabled = True
            Next
            Dim i As Integer = 0
            i = pnlStockQuery.Width
            pnlStockQuery.BringToFront()
            Do While i > 0
                pnlStockQuery.Location = New Point(Me.Width - 2, Me.Height - pnlStockQuery.Height)
                i = i - 2
            Loop
            pnlStockQuery.Visible = False
            pnlButtonHolder.Visible = True
            pnlButtonHolder.BringToFront()
            Me.Controls.Find("ItemCode" & lastActiveItem, True)(0).Select()
        Catch ex As Exception
            qryLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

End Class










