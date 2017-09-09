Public Class CardPayment
    Private lst As New List(Of String())
    Private receivedamount As New Double
    Private balanceamount As New Double
    Private totalAmount As New Double

    Public Function setTotalAmountPayment(ByVal val As Double) As Boolean
        Try
            totalAmount = val
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function getTotalAmountPayment() As Double
        Try
            Return totalAmount
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function
    Public Function addToPayment(ByVal cartValues As Object) As Boolean
        Try
            lst.Add(cartValues)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function removeFromPayment(ByVal itemNum As Int32) As Boolean
        Try
            lst.RemoveAt(itemNum)
            Return True
        Catch exc As Exception
            Return False
        End Try
    End Function

    Public Function GetPaymentItems() As Object

        Return lst
        'Catch ex As Exception
        '    errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        'End Try
    End Function

    Public Function GetRowFromPayment(ByVal itemNum As Int32) As Array
        Dim rowItem(8) As String
        Try
            rowItem = lst.Item(itemNum)
        Catch exc As Exception
            MsgBox("Item not Found")
        End Try

        Return rowItem
    End Function

    Public Function UpdateRowPayment(ByVal itemNum As Int32, ByVal rowItem As Array) As Boolean
        Try
            lst.Item(itemNum) = rowItem
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function GetPaymentDetails() As Object

        Try
            Return lst
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function countCart() As Integer
        Try
            Return lst.Count
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "")
        End Try
    End Function

    Public Sub New()
        Try
            receivedamount = 0
            balanceamount = 0
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Function calBalance(ByVal value As Double, ByVal totAmount As Double) As Double
        Try
            Dim returnReceived As New Double
            'MsgBox(value.ToString + " " + totAmount.ToString)
            returnReceived = receivedamount + value

            Return returnReceived
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function addCardPayment(ByVal amount As Double) As Double
        Try
            receivedamount = receivedamount + amount
            Return 0.0
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function
    Public Function subCardPayment(ByVal amount As Double) As Double
        Try
            receivedamount = receivedamount - amount
            Return receivedamount
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function subBalanceAmount(ByVal amount As Double) As Double
        Try
            balanceamount = balanceamount + amount
            Return balanceamount
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Function

    Public Function GetReceivedAmount() As Double
        Try
            Return receivedamount
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Function

    Public Function GetBalanceAmount() As Double
        Try
            Return balanceamount
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function SetBalanceAmount(ByVal value As Double) As Double
        Try
            balanceamount = value
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Function setReceivedAmount(ByVal value As Double) As Double
        Try
            receivedamount = value
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function
End Class
