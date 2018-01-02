Public Class TaxImpl

    Private db As DBConnection

    Public Sub New(ByVal dbConnection As DBConnection)
        db = dbConnection
    End Sub

    Public Function calculateTaxValueofItem(ByVal itemCode As String, ByVal itemPrice As Double, ByVal locnCode As String, ByVal transactionCode As String, ByVal taxCode As String, ByVal taxPercentage As Double) As Double
        Dim taxValue As Double = 0
        'Dim taxPercentage As Double = getTaxPercentageofItem(itemCode, locnCode, transactionCode, taxCode)

        taxValue = itemPrice - ((itemPrice / (100 + taxPercentage)) * 100)

        Return taxValue
    End Function

    Public Function getTaxPercentageofItem(ByVal itemCode As String, ByVal locnCode As String, ByVal transactionCode As String, ByVal taxCode As String) As Double
        Dim taxPercentage As Double = 0
        Dim stQuery As String = ""
        Dim ds As New DataSet
        'Dim taxCode As String = getLocationTaxCodeForItem(locnCode, itemCode)
        If taxCode.Equals("") Then
            Return taxPercentage
        End If
        'stQuery = "select TR_TAX_PERC from OM_TAX_RATE where TR_TAX_CODE='" & taxCode & "' and TR_EFF_FM_DT <= sysdate  and TR_EFF_TO_DT >= sysdate "
        stQuery = "select TR_TAX_PERC from OM_TAX_RATE where TR_TAX_CODE='" & taxCode & "' and TR_EFF_FM_DT <= sysdate  and TO_DATE(TO_CHAR(TR_EFF_TO_DT, 'DD/MM/YYYY') || ' 23:59:59', 'DD/MM/YYYY HH24:MI:SS') >= sysdate"
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        count = ds.Tables("Table").Rows.Count
        Dim row As System.Data.DataRow
        Dim i As Integer = 0
        While (count > 0)
            row = ds.Tables("Table").Rows.Item(i)
            taxPercentage = row.Item(0)
            count = count - 1
            i = i + 1
        End While
        Return taxPercentage
    End Function

    Public Function getLocationTaxCodeForItem(ByVal locnCode As String, ByVal itemCode As String) As String
        Dim taxCode As String = ""
        Dim stQuery As String = ""
        Dim ds As New DataSet

        stQuery = "SELECT ITEM_ANLY_CODE_01,ITEM_ANLY_CODE_02,ITEM_ANLY_CODE_03,ITEM_ANLY_CODE_04 FROM OM_ITEM where ITEM_CODE='" + itemCode + "' OR ITEM_CODE = (select distinct item_code from OM_POS_ITEM where item_bar_code='" & itemCode & "')"
        errLog.WriteToErrorLog("ITEM ANALY CODE in TAX impl", stQuery, "")
        ds = db.SelectFromTableODBC(stQuery)

        Dim countDisc As Integer
        Dim row As System.Data.DataRow
        Dim iDisc As Integer
        Dim anlycode1 As String = ""
        Dim anlycode2 As String = ""
        Dim anlycode3 As String = ""
        Dim anlycode4 As String = ""
        countDisc = ds.Tables("Table").Rows.Count
        iDisc = 0
        While countDisc > 0
            row = ds.Tables("Table").Rows.Item(iDisc)
            anlycode1 = row.Item(0).ToString
            anlycode2 = row.Item(1).ToString
            anlycode3 = row.Item(2).ToString
            anlycode4 = row.Item(3).ToString
            countDisc = countDisc - 1
            iDisc = iDisc + 1
        End While

        'stQuery = "select LTP_TAX_CODE from OS_LOCN_TAX_PERIOD where LTP_COMP_CODE='" & CompanyCode & "' and LTP_LOCN_CODE='" & locnCode & "' and LTP_ACNT_YEAR='" & PC_Account_Year & "' and LTP_CAL_YEAR='" & PC_CAL_Year & "' and LTP_CAL_PERIOD='" & PC_CAL_Period & "'   "

        'stQuery = "select ITCD_ITAX_CLASS_CODE from OM_ITEM_TAX_CLASS_DEFN where ITCD_COMP_CODE='" & CompanyCode & "' and '" & itemCode & "'  BETWEEN ITCD_FROM_ITEM_CODE and ITCD_TO_ITEM_CODE and ITCD_FRZ_FLAG_NUM=2"
        stQuery = "SELECT OM_TAX.TAX_CODE FROM OM_ITEM_TAX_CLASS_DEFN, OM_TAX, OM_TAX_RATE WHERE OM_TAX.TAX_CODE=OM_ITEM_TAX_CLASS_DEFN.ITCD_ITAX_CLASS_CODE AND OM_TAX.TAX_CODE=TR_TAX_CODE AND '" & itemCode & "' BETWEEN ITCD_FROM_ITEM_CODE AND ITCD_TO_ITEM_CODE AND '" & anlycode1 & "' BETWEEN ITCD_FROM_ANLY_CODE_01 AND ITCD_TO_ANLY_CODE_01 AND '" & anlycode2 & "' BETWEEN ITCD_FROM_ANLY_CODE_02 AND ITCD_TO_ANLY_CODE_02 AND '" & anlycode3 & "' BETWEEN ITCD_FROM_ANLY_CODE_03 AND ITCD_TO_ANLY_CODE_03 AND '" & anlycode4 & "' BETWEEN ITCD_FROM_ANLY_CODE_04 AND ITCD_TO_ANLY_CODE_04 AND TR_EFF_FM_DT <= sysdate  and TO_DATE(TO_CHAR(TR_EFF_TO_DT, 'DD/MM/YYYY') || ' 23:59:59', 'DD/MM/YYYY HH24:MI:SS') >= sysdate AND ITCD_FRZ_FLAG_NUM=2"
        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        count = ds.Tables("Table").Rows.Count

        If (count > 0) Then
            row = ds.Tables("Table").Rows.Item(0)
            taxCode = row.Item(0).ToString
            'count = count - 1
            'i = i + 1
        End If

        Return taxCode
    End Function


End Class
