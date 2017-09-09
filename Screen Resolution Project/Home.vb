Imports System.Windows.Forms

Public Class Home
    Dim db As New DBConnection
    Private Sub Home_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub
    'Try
    Private Sub Home_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        Try
            e.SuppressKeyPress = True
            If e.KeyCode = Keys.Enter Then
                If Transactions.Visible Then
                    Dim activectl As String = Transactions.ActiveControl.Name
                    If activectl = "txtCustomerCode" Then
                        If Transactions.checkCustomerCode() Then
                            Transactions.txtSalesmanCode.Select()
                            Exit Sub
                        End If
                    ElseIf activectl = "txtSalesmanCode" Then
                        If Transactions.checkSalesmanCode() Then
                            Transactions.clickAddItemF2Key(sender, e)
                            Exit Sub
                        End If
                    ElseIf activectl = "cbLocationfrom" Then
                        Transactions.cmbmaingrp.Select()
                    ElseIf activectl = "cmbmaingrp" Then
                        Transactions.cmbsubgrp.Select()
                    ElseIf activectl = "cmbsubgrp" Then
                        Transactions.cmbitemfrom.Select()
                    ElseIf activectl = "cmbitemfrom" Then
                        Transactions.cmbitemto.Select()
                    ElseIf activectl = "cmbitemto" Then
                        Transactions.btnViewStockQuery.Select()
                    ElseIf activectl = "listProduct" Then
                        Transactions.callListProductDoubleClick(sender, e)
                    Else
                        If Transactions.lstboxItemNames.SelectedItems.Count > 0 Then
                            Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & Transactions.lastActiveItem, True)
                            ItmCodeFound(0).Text = Transactions.lstboxItemNames.SelectedItems.Item(0).ToString
                            Transactions.lstboxItemName_LostFocusCall(sender, e)
                            Transactions.lstboxItemNames.Items.Clear()
                            Transactions.pnlItemNameListHolder.Visible = False
                        End If
                    End If
                End If
            ElseIf e.KeyCode = Keys.F12 Then
                If Transactions.Visible Then
                    Dim activectl As String = Transactions.ActiveControl.Name
                    Dim parts1 As String() = activectl.Split(New String() {"ItemDisamt"}, StringSplitOptions.None)
                    If parts1.Count > 1 Then
                        'Transactions.pnlINVDetails.Enabled = False
                        'Transactions.pnlBottomHolder.Enabled = False
                        'Transactions.pnlItemDetails.Enabled = False
                        'Transactions.pnlButtonHolder.Enabled = False
                        Dim ItmDiscFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisc" & Transactions.lastActiveItem, True)
                        If Not ItmDiscFound(0).Text = "" Then
                            Dim ItmDisamtFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemDisamt" & Transactions.lastActiveItem, True)
                            With Transactions.pnlMasked
                                .BringToFront()
                                .Location = New Point(ItmDisamtFound(0).Location.X + Transactions.pnlItemDetails.Location.X, ItmDisamtFound(0).Location.Y + Transactions.pnlINVDetails.Height + 27)
                                .Visible = True
                            End With
                            Transactions.txtDiscPercValue.Text = "0"
                            Transactions.txtDiscPercValueAmt.Text = "0"
                            Transactions.txtDiscPercValue.Select()
                        Else
                            MsgBox("Please select a discount code!")
                            ItmDiscFound(0).Select()
                        End If
                    End If
                End If
            ElseIf e.KeyCode = Keys.F1 Then
                Transactions.clickPatientF1Key(sender, e)
            ElseIf e.KeyCode = Keys.F2 Then
                'Transactions.clickAddItemF2Key(sender, e)
                Dim activectl As String = Transactions.ActiveControl.Name
                Dim parts1 As String() = activectl.Split(New String() {"ItemCode"}, StringSplitOptions.None)
                If parts1.Count > 1 Then
                    Dim ItmCodeFound As System.Windows.Forms.Control() = Me.Controls.Find("ItemCode" & Transactions.lastActiveItem, True)

                    Transactions.callbtnStockQueryClick(sender, e)
                    Transactions.cmbitemfrom.Text = ItmCodeFound(0).Text
                    'If Not ItmCodeFound(0).Text = "" Then
                    '    Dim itemCode As String = ItmCodeFound(0).Text
                    '    Transactions.loadItems(itemCode, ItmCodeFound(0))
                    '    'Dim stQuery As String = ""
                    '    'Dim count As Integer
                    '    'Dim i As Integer = 0
                    '    'Dim ds As DataSet
                    '    'stQuery = "select distinct ITEM_CODE from OM_POS_ITEM where ITEM_CODE like '" & itemCode & "%' or ITEM_BAR_CODE like '" & itemCode & "'"
                    '    'ds = db.SelectFromTableODBC(stQuery)
                    '    'count = ds.Tables("Table").Rows.Count
                    '    'If count > 0 Then
                    '    '    Transactions.lstboxItemNames.Items.Clear()
                    '    '    While count > 0
                    '    '        Transactions.lstboxItemNames.Items.Add(ds.Tables("Table").Rows.Item(i).Item(0).ToString)
                    '    '        count = count - 1
                    '    '        i = i + 1
                    '    '    End While
                    '    '    Transactions.lstboxItemNames.SetSelected(0, True)
                    '    '    Transactions.lstboxItemNames.Select()
                    '    '    Transactions.lstboxItemNames.Focus()
                    '    '    With Transactions.pnlItemNameListHolder
                    '    '        .BringToFront()
                    '    '        .Location = New Point(ItmCodeFound(0).Location.X + Transactions.pnlItemDetails.Location.X + 1, ItmCodeFound(0).Location.Y + Transactions.pnlINVDetails.Height + 27)
                    '    '        .Visible = True
                    '    '        .Select()
                    '    '    End With
                    '    'End If
                    'End If
                End If
                ElseIf e.KeyCode = Keys.F3 Then
                    Transactions.clickPaymentF3Key(sender, e)
                ElseIf e.KeyCode = Keys.F5 Then
                    Transactions.clickCustomerF5Key(sender, e)
                ElseIf e.KeyCode = Keys.F10 Then
                    Transactions.clickCancelInvoiceF10Key(sender, e)
                End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
        ' Catch ex As Exception
        '    errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        'End Try
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim OpenFileDialog As New OpenFileDialog
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim FileName As String = OpenFileDialog.FileName
                ' TODO: Add code here to open the file.
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim SaveFileDialog As New SaveFileDialog
            SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

            If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim FileName As String = SaveFileDialog.FileName
                ' TODO: Add code here to save the current contents of the form to a file.
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub



    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.LayoutMdi(MdiLayout.Cascade)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.LayoutMdi(MdiLayout.TileVertical)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.LayoutMdi(MdiLayout.TileHorizontal)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.LayoutMdi(MdiLayout.ArrangeIcons)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        Try
            For Each ChildForm As Form In Me.MdiChildren
                ChildForm.Close()
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            For Each ChildForm As Form In Me.MdiChildren
                ChildForm.Close()
            Next
            SubHomeForm.MdiParent = Me
            SubHomeForm.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub EditMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditMenu.Click
        Try
            For Each ChildForm As Form In Me.MdiChildren
                ChildForm.Close()
            Next
            SubHomeForm.MdiParent = Me
            SubHomeForm.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub



    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub MDIParent1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'Me.WindowState = FormWindowState.Maximized

    End Sub


    Private Sub TransactionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransactionToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            Transactions.MdiParent = Me
            Transactions.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Sub NewTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TransactionToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub ReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportsToolStripMenuItem.Click

    End Sub

    Private Sub EndOfTheDayReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndOfTheDayReportToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            frmEndofthedayrep.MdiParent = Me
            frmEndofthedayrep.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Public Sub RefreshEndoftheday(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            EndOfTheDayReportToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub
    Public Sub RefreshPatient(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'SettingsToolStripMenuItem_Click(sender, e)
            PatientMaintenanceToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub Master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        Try
            If AdminSettings.btnSalesOrders.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child

                AdminSettings.MdiParent = Me
                AdminSettings.Show()
            Else
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child

                CounterSettings.MdiParent = Me
                CounterSettings.Show()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub GiftVoucherReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GiftVoucherReportToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            frmGVRep.MdiParent = Me
            frmGVRep.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub SuppliersToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MastersToolStripMenuItem.Click
        Try
            If SettingsShiftMaster.btnShift_SalesOrders.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child
                SettingsShiftMaster.MdiParent = Me
                SettingsShiftMaster.Show()
            ElseIf SettingsCounterMaster.btnCounterMaster.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child
                SettingsCounterMaster.MdiParent = Me
                SettingsCounterMaster.Show()
            ElseIf SettingsSalesmanMaster.btnSalesmanMaster.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child
                SettingsSalesmanMaster.MdiParent = Me
                SettingsSalesmanMaster.Show()
            ElseIf SettingsDenominationMaster.btnDenominationMaster.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child
                SettingsDenominationMaster.MdiParent = Me
                SettingsDenominationMaster.Show()
            ElseIf SettingsPaymentMaster.btnPaymentMaster.Enabled Then
                For Each child As Form In Me.MdiChildren
                    child.Close()
                    child.Dispose()
                Next child
                SettingsPaymentMaster.MdiParent = Me
                SettingsPaymentMaster.Show()
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try

    End Sub

    Private Sub DailySalesTransactionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailySalesTransactionReportToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            DailyTransReport.MdiParent = Me
            DailyTransReport.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub ReferralReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReferralReportToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            ReferalReport.MdiParent = Me
            ReferalReport.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub RoyaltyReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoyaltyReportToolStripMenuItem.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            RoyaltyReport.MdiParent = Me
            RoyaltyReport.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub PatientMaintenanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            Patientfrm.MdiParent = Me
            Patientfrm.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub

    Private Sub StockReportToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReportToolStripMenuItem1.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            frmStockReport.MdiParent = Me
            frmStockReport.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub


    Private Sub ToolsMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolsMenu.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            Patientfrm.MdiParent = Me
            Patientfrm.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub




    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            For Each child As Form In Me.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SlipDrawer.MdiParent = Me
            SlipDrawer.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
        End Try
    End Sub
End Class
