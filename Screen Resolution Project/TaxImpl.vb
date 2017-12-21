Public Class TaxImpl

    Private db As DBConnection

    Public Sub New(ByVal dbConnection As DBConnection)
        db = dbConnection
    End Sub

        Dim taxValue As Double = 0

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
        stQuery = "select TR_TAX_PERC from OM_TAX_RATE where TR_TAX_CODE='" & taxCode & "' and TR_EFF_FM_DT <= sysdate  and TR_EFF_TO_DT >= sysdate "
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
        'stQuery = "select LTP_TAX_CODE from OS_LOCN_TAX_PERIOD where LTP_COMP_CODE='" & CompanyCode & "' and LTP_LOCN_CODE='" & locnCode & "' and LTP_ACNT_YEAR='" & PC_Account_Year & "' and LTP_CAL_YEAR='" & PC_CAL_Year & "' and LTP_CAL_PERIOD='" & PC_CAL_Period & "'   "

        stQuery = "select ITCD_ITAX_CLASS_CODE from OM_ITEM_TAX_CLASS_DEFN where ITCD_COMP_CODE='" & CompanyCode & "' and '" & itemCode & "'  BETWEEN ITCD_FROM_ITEM_CODE and ITCD_TO_ITEM_CODE "
        'stQuery = stQuery & ""       ------- Here analysis code is not checked

        ds = db.SelectFromTableODBC(stQuery)
        Dim count As Integer = 0
        count = ds.Tables("Table").Rows.Count
        Dim row As System.Data.DataRow
        Dim i As Integer = 0
        While (count > 0)
            row = ds.Tables("Table").Rows.Item(i)
            taxCode = row.Item(0).ToString
            count = count - 1
            i = i + 1
        End While

        Return taxCode
    End Function


End Class
