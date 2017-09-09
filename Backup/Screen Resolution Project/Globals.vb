Module Globals
    Public errLog As New Error_Log
    Public CPT_TermCode As New String("")
    Public currentMod As New String("")
    Public CompanyCode As New String("")
    Public CompanyName As New String("")
    Public Staff_Code As New String("")
    Public changedStaffCode As New String("")
    Public Staff_Name As New String("")
    Public changedStaffName As New String("")
    Public Location_Code As New String("")
    Public Location_Name As New String("")
    Public TXN_Code As New String("")
    Public TXN_Type As New String("")
    Public salesmanCode As New String("")
    Public LogonUser As New String("")
    Public POSCounterName As New String("")
    Public POSCounterIPAddress As New String("")
    Public POSCounterNumber As New String("")
    Public Currency_Code As New String("")
    Public Exchange_Rate As New String("")
    Public PC_Account_Year As New String("")
    Public PC_CAL_Period As New String("")
    Public PC_CAL_Year As New String("")
    Public Setup_Values As New Dictionary(Of String, String)
    Public Location_Setup_Values As New Dictionary(Of String, String)
    Public AlertMinutes As Double = 0
    Public alertOnOff As Boolean = False
    Public logoYN As Boolean = False
    Public locationLogo As String = ""

    Public lastDInv As Integer = 0
    Public lastSR As Integer = 0

End Module
