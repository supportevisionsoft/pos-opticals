<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransactionSlip
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TransactionSlip))
        Me.pnlOuterContainer = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel30 = New System.Windows.Forms.Panel
        Me.Label62 = New System.Windows.Forms.Label
        Me.Panel23 = New System.Windows.Forms.Panel
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label61 = New System.Windows.Forms.Label
        Me.Panel22 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblRptTOTAL = New System.Windows.Forms.Label
        Me.Label60 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblRptAdvancedPaid = New System.Windows.Forms.Label
        Me.lblRptBalance = New System.Windows.Forms.Label
        Me.lblRptExpense = New System.Windows.Forms.Label
        Me.lblLblBalance = New System.Windows.Forms.Label
        Me.lblLblAdvance = New System.Windows.Forms.Label
        Me.lblRptDiscount = New System.Windows.Forms.Label
        Me.lblRptSubTotal = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Panel21 = New System.Windows.Forms.Panel
        Me.lblRptAmtHead = New System.Windows.Forms.Label
        Me.lblRptQtyHead = New System.Windows.Forms.Label
        Me.lblRptRateHead = New System.Windows.Forms.Label
        Me.lblRptUOMHead = New System.Windows.Forms.Label
        Me.lblRptItemCodeHead = New System.Windows.Forms.Label
        Me.lblRptSNOHead = New System.Windows.Forms.Label
        Me.pnlRptItemsHolder = New System.Windows.Forms.Panel
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.lblRptCustomerEmail = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.lblRptCustomerPhone = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lblRptCustomerName = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblSalesmanName = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblRptInvoiceDate = New System.Windows.Forms.Label
        Me.lblRptInvoiceNo = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.lblRptINVSONO = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.lblRptRptType = New System.Windows.Forms.Label
        Me.lblEmail = New System.Windows.Forms.Label
        Me.lblPhone = New System.Windows.Forms.Label
        Me.lblRptLocationAddress = New System.Windows.Forms.Label
        Me.lblRptLocationName = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.btn_Print_Report = New System.Windows.Forms.Button
        Me.btnCloseReport = New System.Windows.Forms.Button
        Me.btnExportPDF = New System.Windows.Forms.Button
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.pnlReportHead = New System.Windows.Forms.Panel
        Me.lblusernam = New System.Windows.Forms.Label
        Me.lblusername = New System.Windows.Forms.Label
        Me.lblhead = New System.Windows.Forms.Label
        Me.picHead = New System.Windows.Forms.PictureBox
        Me.pnlOuterContainer.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel30.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlReportHead.SuspendLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlOuterContainer
        '
        Me.pnlOuterContainer.AutoScroll = True
        Me.pnlOuterContainer.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlOuterContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlOuterContainer.Controls.Add(Me.Panel2)
        Me.pnlOuterContainer.Location = New System.Drawing.Point(200, 47)
        Me.pnlOuterContainer.Name = "pnlOuterContainer"
        Me.pnlOuterContainer.Size = New System.Drawing.Size(790, 564)
        Me.pnlOuterContainer.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Panel30)
        Me.Panel2.Controls.Add(Me.Panel23)
        Me.Panel2.Controls.Add(Me.Panel22)
        Me.Panel2.Controls.Add(Me.Panel9)
        Me.Panel2.Controls.Add(Me.Panel21)
        Me.Panel2.Controls.Add(Me.pnlRptItemsHolder)
        Me.Panel2.Controls.Add(Me.Panel16)
        Me.Panel2.Controls.Add(Me.Panel15)
        Me.Panel2.Controls.Add(Me.Panel14)
        Me.Panel2.Controls.Add(Me.lblEmail)
        Me.Panel2.Controls.Add(Me.lblPhone)
        Me.Panel2.Controls.Add(Me.lblRptLocationAddress)
        Me.Panel2.Controls.Add(Me.lblRptLocationName)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Location = New System.Drawing.Point(6, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(560, 794)
        Me.Panel2.TabIndex = 0
        '
        'Panel30
        '
        Me.Panel30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel30.Controls.Add(Me.Label62)
        Me.Panel30.Location = New System.Drawing.Point(329, 682)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(212, 63)
        Me.Panel30.TabIndex = 27
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(5, 10)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(107, 16)
        Me.Label62.TabIndex = 8
        Me.Label62.Text = "Authorized Signature"
        '
        'Panel23
        '
        Me.Panel23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel23.Controls.Add(Me.Label63)
        Me.Panel23.Controls.Add(Me.Label61)
        Me.Panel23.Location = New System.Drawing.Point(19, 682)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(311, 63)
        Me.Panel23.TabIndex = 26
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(44, 30)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(232, 14)
        Me.Label63.TabIndex = 9
        Me.Label63.Text = "The above said information is true and correct."
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(3, 8)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(60, 16)
        Me.Label61.TabIndex = 8
        Me.Label61.Text = "Declaration"
        '
        'Panel22
        '
        Me.Panel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel22.Controls.Add(Me.Label16)
        Me.Panel22.Controls.Add(Me.lblRptTOTAL)
        Me.Panel22.Controls.Add(Me.Label60)
        Me.Panel22.Location = New System.Drawing.Point(19, 654)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(522, 29)
        Me.Panel22.TabIndex = 25
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 15)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "E && OE"
        '
        'lblRptTOTAL
        '
        Me.lblRptTOTAL.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptTOTAL.Location = New System.Drawing.Point(388, 7)
        Me.lblRptTOTAL.Name = "lblRptTOTAL"
        Me.lblRptTOTAL.Size = New System.Drawing.Size(127, 14)
        Me.lblRptTOTAL.TabIndex = 10
        Me.lblRptTOTAL.Text = "0"
        Me.lblRptTOTAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(329, 6)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(38, 16)
        Me.Label60.TabIndex = 7
        Me.Label60.Text = "Total :"
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.lblRptAdvancedPaid)
        Me.Panel9.Controls.Add(Me.lblRptBalance)
        Me.Panel9.Controls.Add(Me.lblRptExpense)
        Me.Panel9.Controls.Add(Me.lblLblBalance)
        Me.Panel9.Controls.Add(Me.lblLblAdvance)
        Me.Panel9.Controls.Add(Me.lblRptDiscount)
        Me.Panel9.Controls.Add(Me.lblRptSubTotal)
        Me.Panel9.Controls.Add(Me.Label52)
        Me.Panel9.Controls.Add(Me.Label51)
        Me.Panel9.Controls.Add(Me.Label50)
        Me.Panel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(19, 587)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(522, 68)
        Me.Panel9.TabIndex = 24
        '
        'lblRptAdvancedPaid
        '
        Me.lblRptAdvancedPaid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptAdvancedPaid.Location = New System.Drawing.Point(102, 22)
        Me.lblRptAdvancedPaid.Name = "lblRptAdvancedPaid"
        Me.lblRptAdvancedPaid.Size = New System.Drawing.Size(97, 14)
        Me.lblRptAdvancedPaid.TabIndex = 16
        Me.lblRptAdvancedPaid.Text = "0"
        Me.lblRptAdvancedPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRptBalance
        '
        Me.lblRptBalance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptBalance.Location = New System.Drawing.Point(102, 41)
        Me.lblRptBalance.Name = "lblRptBalance"
        Me.lblRptBalance.Size = New System.Drawing.Size(97, 14)
        Me.lblRptBalance.TabIndex = 15
        Me.lblRptBalance.Text = "0"
        Me.lblRptBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRptExpense
        '
        Me.lblRptExpense.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptExpense.Location = New System.Drawing.Point(419, 41)
        Me.lblRptExpense.Name = "lblRptExpense"
        Me.lblRptExpense.Size = New System.Drawing.Size(96, 14)
        Me.lblRptExpense.TabIndex = 13
        Me.lblRptExpense.Text = "0.000"
        Me.lblRptExpense.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLblBalance
        '
        Me.lblLblBalance.AutoSize = True
        Me.lblLblBalance.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLblBalance.Location = New System.Drawing.Point(21, 41)
        Me.lblLblBalance.Name = "lblLblBalance"
        Me.lblLblBalance.Size = New System.Drawing.Size(49, 15)
        Me.lblLblBalance.TabIndex = 12
        Me.lblLblBalance.Text = "Balance :"
        '
        'lblLblAdvance
        '
        Me.lblLblAdvance.AutoSize = True
        Me.lblLblAdvance.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLblAdvance.Location = New System.Drawing.Point(21, 21)
        Me.lblLblAdvance.Name = "lblLblAdvance"
        Me.lblLblAdvance.Size = New System.Drawing.Size(75, 15)
        Me.lblLblAdvance.TabIndex = 11
        Me.lblLblAdvance.Text = "Advance Paid :"
        '
        'lblRptDiscount
        '
        Me.lblRptDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptDiscount.Location = New System.Drawing.Point(419, 23)
        Me.lblRptDiscount.Name = "lblRptDiscount"
        Me.lblRptDiscount.Size = New System.Drawing.Size(96, 14)
        Me.lblRptDiscount.TabIndex = 10
        Me.lblRptDiscount.Text = "0"
        Me.lblRptDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRptSubTotal
        '
        Me.lblRptSubTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptSubTotal.Location = New System.Drawing.Point(419, 5)
        Me.lblRptSubTotal.Name = "lblRptSubTotal"
        Me.lblRptSubTotal.Size = New System.Drawing.Size(96, 14)
        Me.lblRptSubTotal.TabIndex = 9
        Me.lblRptSubTotal.Text = "0"
        Me.lblRptSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(329, 42)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(55, 14)
        Me.Label52.TabIndex = 8
        Me.Label52.Text = "Expense :"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(329, 24)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(55, 14)
        Me.Label51.TabIndex = 7
        Me.Label51.Text = "Discount :"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(329, 5)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(56, 15)
        Me.Label50.TabIndex = 6
        Me.Label50.Text = "Sub Total :"
        '
        'Panel21
        '
        Me.Panel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel21.Controls.Add(Me.lblRptAmtHead)
        Me.Panel21.Controls.Add(Me.lblRptQtyHead)
        Me.Panel21.Controls.Add(Me.lblRptRateHead)
        Me.Panel21.Controls.Add(Me.lblRptUOMHead)
        Me.Panel21.Controls.Add(Me.lblRptItemCodeHead)
        Me.Panel21.Controls.Add(Me.lblRptSNOHead)
        Me.Panel21.Location = New System.Drawing.Point(19, 248)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(522, 30)
        Me.Panel21.TabIndex = 19
        '
        'lblRptAmtHead
        '
        Me.lblRptAmtHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptAmtHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptAmtHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptAmtHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptAmtHead.Location = New System.Drawing.Point(430, 0)
        Me.lblRptAmtHead.Name = "lblRptAmtHead"
        Me.lblRptAmtHead.Size = New System.Drawing.Size(90, 29)
        Me.lblRptAmtHead.TabIndex = 14
        Me.lblRptAmtHead.Text = "Amount"
        Me.lblRptAmtHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptQtyHead
        '
        Me.lblRptQtyHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptQtyHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptQtyHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptQtyHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptQtyHead.Location = New System.Drawing.Point(385, 0)
        Me.lblRptQtyHead.Name = "lblRptQtyHead"
        Me.lblRptQtyHead.Size = New System.Drawing.Size(45, 29)
        Me.lblRptQtyHead.TabIndex = 13
        Me.lblRptQtyHead.Text = "Qty"
        Me.lblRptQtyHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptRateHead
        '
        Me.lblRptRateHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptRateHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptRateHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptRateHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptRateHead.Location = New System.Drawing.Point(325, 0)
        Me.lblRptRateHead.Name = "lblRptRateHead"
        Me.lblRptRateHead.Size = New System.Drawing.Size(60, 29)
        Me.lblRptRateHead.TabIndex = 12
        Me.lblRptRateHead.Text = "Rate"
        Me.lblRptRateHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptUOMHead
        '
        Me.lblRptUOMHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptUOMHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptUOMHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptUOMHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptUOMHead.Location = New System.Drawing.Point(280, 0)
        Me.lblRptUOMHead.Name = "lblRptUOMHead"
        Me.lblRptUOMHead.Size = New System.Drawing.Size(45, 29)
        Me.lblRptUOMHead.TabIndex = 11
        Me.lblRptUOMHead.Text = "UOM"
        Me.lblRptUOMHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptItemCodeHead
        '
        Me.lblRptItemCodeHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptItemCodeHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptItemCodeHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptItemCodeHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptItemCodeHead.Location = New System.Drawing.Point(32, 0)
        Me.lblRptItemCodeHead.Name = "lblRptItemCodeHead"
        Me.lblRptItemCodeHead.Size = New System.Drawing.Size(248, 29)
        Me.lblRptItemCodeHead.TabIndex = 9
        Me.lblRptItemCodeHead.Text = "Item"
        Me.lblRptItemCodeHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptSNOHead
        '
        Me.lblRptSNOHead.BackColor = System.Drawing.Color.MintCream
        Me.lblRptSNOHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRptSNOHead.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptSNOHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRptSNOHead.Location = New System.Drawing.Point(0, 0)
        Me.lblRptSNOHead.Name = "lblRptSNOHead"
        Me.lblRptSNOHead.Size = New System.Drawing.Size(32, 29)
        Me.lblRptSNOHead.TabIndex = 8
        Me.lblRptSNOHead.Text = "SNo."
        Me.lblRptSNOHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlRptItemsHolder
        '
        Me.pnlRptItemsHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRptItemsHolder.Location = New System.Drawing.Point(19, 278)
        Me.pnlRptItemsHolder.Name = "pnlRptItemsHolder"
        Me.pnlRptItemsHolder.Size = New System.Drawing.Size(522, 310)
        Me.pnlRptItemsHolder.TabIndex = 23
        '
        'Panel16
        '
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Controls.Add(Me.lblRptCustomerEmail)
        Me.Panel16.Controls.Add(Me.Label48)
        Me.Panel16.Controls.Add(Me.lblRptCustomerPhone)
        Me.Panel16.Controls.Add(Me.Label47)
        Me.Panel16.Controls.Add(Me.lblRptCustomerName)
        Me.Panel16.Controls.Add(Me.Label44)
        Me.Panel16.Location = New System.Drawing.Point(19, 198)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(522, 48)
        Me.Panel16.TabIndex = 22
        '
        'lblRptCustomerEmail
        '
        Me.lblRptCustomerEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptCustomerEmail.Location = New System.Drawing.Point(328, 28)
        Me.lblRptCustomerEmail.Name = "lblRptCustomerEmail"
        Me.lblRptCustomerEmail.Size = New System.Drawing.Size(183, 14)
        Me.lblRptCustomerEmail.TabIndex = 11
        Me.lblRptCustomerEmail.Text = "-"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(257, 27)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(66, 14)
        Me.Label48.TabIndex = 10
        Me.Label48.Text = "Email         :"
        '
        'lblRptCustomerPhone
        '
        Me.lblRptCustomerPhone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptCustomerPhone.Location = New System.Drawing.Point(87, 28)
        Me.lblRptCustomerPhone.Name = "lblRptCustomerPhone"
        Me.lblRptCustomerPhone.Size = New System.Drawing.Size(164, 14)
        Me.lblRptCustomerPhone.TabIndex = 9
        Me.lblRptCustomerPhone.Text = "-"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(13, 27)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(72, 14)
        Me.Label47.TabIndex = 8
        Me.Label47.Text = "Phone         :"
        '
        'lblRptCustomerName
        '
        Me.lblRptCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptCustomerName.Location = New System.Drawing.Point(87, 8)
        Me.lblRptCustomerName.Name = "lblRptCustomerName"
        Me.lblRptCustomerName.Size = New System.Drawing.Size(424, 14)
        Me.lblRptCustomerName.TabIndex = 3
        Me.lblRptCustomerName.Text = " "
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(13, 8)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(72, 14)
        Me.Label44.TabIndex = 2
        Me.Label44.Text = "Customer  :"
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.Label28)
        Me.Panel15.Controls.Add(Me.Label22)
        Me.Panel15.Controls.Add(Me.lblSalesmanName)
        Me.Panel15.Controls.Add(Me.Label19)
        Me.Panel15.Controls.Add(Me.lblRptInvoiceDate)
        Me.Panel15.Controls.Add(Me.lblRptInvoiceNo)
        Me.Panel15.Controls.Add(Me.Label43)
        Me.Panel15.Controls.Add(Me.lblRptINVSONO)
        Me.Panel15.Location = New System.Drawing.Point(19, 151)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(522, 48)
        Me.Panel15.TabIndex = 21
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(325, 6)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(192, 14)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = " "
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(256, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(69, 14)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "SO.  No       :"
        Me.Label22.Visible = False
        '
        'lblSalesmanName
        '
        Me.lblSalesmanName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesmanName.Location = New System.Drawing.Point(325, 29)
        Me.lblSalesmanName.Name = "lblSalesmanName"
        Me.lblSalesmanName.Size = New System.Drawing.Size(192, 14)
        Me.lblSalesmanName.TabIndex = 5
        Me.lblSalesmanName.Text = " "
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(256, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(70, 14)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Salesman  :"
        '
        'lblRptInvoiceDate
        '
        Me.lblRptInvoiceDate.AutoSize = True
        Me.lblRptInvoiceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptInvoiceDate.Location = New System.Drawing.Point(94, 29)
        Me.lblRptInvoiceDate.Name = "lblRptInvoiceDate"
        Me.lblRptInvoiceDate.Size = New System.Drawing.Size(0, 14)
        Me.lblRptInvoiceDate.TabIndex = 3
        '
        'lblRptInvoiceNo
        '
        Me.lblRptInvoiceNo.AutoSize = True
        Me.lblRptInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptInvoiceNo.Location = New System.Drawing.Point(93, 7)
        Me.lblRptInvoiceNo.Name = "lblRptInvoiceNo"
        Me.lblRptInvoiceNo.Size = New System.Drawing.Size(10, 14)
        Me.lblRptInvoiceNo.TabIndex = 2
        Me.lblRptInvoiceNo.Text = " "
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(13, 28)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(73, 14)
        Me.Label43.TabIndex = 1
        Me.Label43.Text = "Date             :"
        '
        'lblRptINVSONO
        '
        Me.lblRptINVSONO.AutoSize = True
        Me.lblRptINVSONO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptINVSONO.Location = New System.Drawing.Point(13, 6)
        Me.lblRptINVSONO.Name = "lblRptINVSONO"
        Me.lblRptINVSONO.Size = New System.Drawing.Size(72, 14)
        Me.lblRptINVSONO.TabIndex = 0
        Me.lblRptINVSONO.Text = "Invoice No. :"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Silver
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Controls.Add(Me.lblRptRptType)
        Me.Panel14.Location = New System.Drawing.Point(19, 131)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(522, 18)
        Me.Panel14.TabIndex = 20
        '
        'lblRptRptType
        '
        Me.lblRptRptType.AutoSize = True
        Me.lblRptRptType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptRptType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblRptRptType.Location = New System.Drawing.Point(215, 0)
        Me.lblRptRptType.Name = "lblRptRptType"
        Me.lblRptRptType.Size = New System.Drawing.Size(84, 15)
        Me.lblRptRptType.TabIndex = 0
        Me.lblRptRptType.Text = "Direct Invoice"
        '
        'lblEmail
        '
        Me.lblEmail.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(176, 111)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(208, 15)
        Me.lblEmail.TabIndex = 19
        Me.lblEmail.Text = "Email:"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPhone
        '
        Me.lblPhone.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(176, 96)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(208, 15)
        Me.lblPhone.TabIndex = 18
        Me.lblPhone.Text = "Phone:"
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptLocationAddress
        '
        Me.lblRptLocationAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptLocationAddress.Location = New System.Drawing.Point(27, 81)
        Me.lblRptLocationAddress.Name = "lblRptLocationAddress"
        Me.lblRptLocationAddress.Size = New System.Drawing.Size(504, 15)
        Me.lblRptLocationAddress.TabIndex = 16
        Me.lblRptLocationAddress.Text = "Location Address"
        Me.lblRptLocationAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRptLocationName
        '
        Me.lblRptLocationName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRptLocationName.Location = New System.Drawing.Point(27, 66)
        Me.lblRptLocationName.Name = "lblRptLocationName"
        Me.lblRptLocationName.Size = New System.Drawing.Size(504, 15)
        Me.lblRptLocationName.TabIndex = 15
        Me.lblRptLocationName.Text = "Location Name"
        Me.lblRptLocationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.POS.My.Resources.Resources.clientlogo12
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(19, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(522, 57)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'btn_Print_Report
        '
        Me.btn_Print_Report.BackColor = System.Drawing.Color.SkyBlue
        Me.btn_Print_Report.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Print_Report.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Print_Report.Image = Global.POS.My.Resources.Resources.Printer_icon
        Me.btn_Print_Report.Location = New System.Drawing.Point(30, 174)
        Me.btn_Print_Report.Name = "btn_Print_Report"
        Me.btn_Print_Report.Size = New System.Drawing.Size(72, 72)
        Me.btn_Print_Report.TabIndex = 17
        Me.btn_Print_Report.Text = "Print"
        Me.btn_Print_Report.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_Print_Report.UseVisualStyleBackColor = False
        '
        'btnCloseReport
        '
        Me.btnCloseReport.BackColor = System.Drawing.Color.SkyBlue
        Me.btnCloseReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseReport.Image = Global.POS.My.Resources.Resources.File_Delete_icon
        Me.btnCloseReport.Location = New System.Drawing.Point(30, 249)
        Me.btnCloseReport.Name = "btnCloseReport"
        Me.btnCloseReport.Size = New System.Drawing.Size(72, 72)
        Me.btnCloseReport.TabIndex = 18
        Me.btnCloseReport.Text = "Close"
        Me.btnCloseReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCloseReport.UseVisualStyleBackColor = False
        '
        'btnExportPDF
        '
        Me.btnExportPDF.BackColor = System.Drawing.Color.SkyBlue
        Me.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportPDF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportPDF.Image = Global.POS.My.Resources.Resources.Export_To_File_icon
        Me.btnExportPDF.Location = New System.Drawing.Point(30, 99)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(72, 72)
        Me.btnExportPDF.TabIndex = 13
        Me.btnExportPDF.Text = "Export"
        Me.btnExportPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExportPDF.UseVisualStyleBackColor = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'pnlReportHead
        '
        Me.pnlReportHead.BackColor = System.Drawing.Color.MediumTurquoise
        Me.pnlReportHead.Controls.Add(Me.lblusernam)
        Me.pnlReportHead.Controls.Add(Me.lblusername)
        Me.pnlReportHead.Controls.Add(Me.lblhead)
        Me.pnlReportHead.Controls.Add(Me.picHead)
        Me.pnlReportHead.Location = New System.Drawing.Point(1, 0)
        Me.pnlReportHead.Name = "pnlReportHead"
        Me.pnlReportHead.Size = New System.Drawing.Size(1027, 45)
        Me.pnlReportHead.TabIndex = 95
        '
        'lblusernam
        '
        Me.lblusernam.AutoSize = True
        Me.lblusernam.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblusernam.Location = New System.Drawing.Point(720, 15)
        Me.lblusernam.Name = "lblusernam"
        Me.lblusernam.Size = New System.Drawing.Size(0, 15)
        Me.lblusernam.TabIndex = 14
        '
        'lblusername
        '
        Me.lblusername.AutoSize = True
        Me.lblusername.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblusername.Location = New System.Drawing.Point(800, 15)
        Me.lblusername.Name = "lblusername"
        Me.lblusername.Size = New System.Drawing.Size(0, 15)
        Me.lblusername.TabIndex = 13
        '
        'lblhead
        '
        Me.lblhead.AutoSize = True
        Me.lblhead.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhead.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblhead.Location = New System.Drawing.Point(53, 16)
        Me.lblhead.Name = "lblhead"
        Me.lblhead.Size = New System.Drawing.Size(162, 22)
        Me.lblhead.TabIndex = 8
        Me.lblhead.Text = "Transaction Slip"
        '
        'picHead
        '
        Me.picHead.Image = CType(resources.GetObject("picHead.Image"), System.Drawing.Image)
        Me.picHead.Location = New System.Drawing.Point(-2, -1)
        Me.picHead.Name = "picHead"
        Me.picHead.Size = New System.Drawing.Size(41, 43)
        Me.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picHead.TabIndex = 7
        Me.picHead.TabStop = False
        '
        'TransactionSlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlReportHead)
        Me.Controls.Add(Me.btnCloseReport)
        Me.Controls.Add(Me.btn_Print_Report)
        Me.Controls.Add(Me.btnExportPDF)
        Me.Controls.Add(Me.pnlOuterContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "TransactionSlip"
        Me.Text = "Transaction Slip"
        Me.pnlOuterContainer.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlReportHead.ResumeLayout(False)
        Me.pnlReportHead.PerformLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlOuterContainer As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblRptLocationName As System.Windows.Forms.Label
    Friend WithEvents lblRptLocationAddress As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents lblRptRptType As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblSalesmanName As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblRptInvoiceDate As System.Windows.Forms.Label
    Friend WithEvents lblRptInvoiceNo As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblRptINVSONO As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents lblRptCustomerName As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblRptCustomerPhone As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lblRptCustomerEmail As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnlRptItemsHolder As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents lblRptAmtHead As System.Windows.Forms.Label
    Friend WithEvents lblRptQtyHead As System.Windows.Forms.Label
    Friend WithEvents lblRptRateHead As System.Windows.Forms.Label
    Friend WithEvents lblRptUOMHead As System.Windows.Forms.Label
    Friend WithEvents lblRptItemCodeHead As System.Windows.Forms.Label
    Friend WithEvents lblRptSNOHead As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblRptAdvancedPaid As System.Windows.Forms.Label
    Friend WithEvents lblRptBalance As System.Windows.Forms.Label
    Friend WithEvents lblRptExpense As System.Windows.Forms.Label
    Friend WithEvents lblLblBalance As System.Windows.Forms.Label
    Friend WithEvents lblLblAdvance As System.Windows.Forms.Label
    Friend WithEvents lblRptDiscount As System.Windows.Forms.Label
    Friend WithEvents lblRptSubTotal As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents lblRptTOTAL As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btn_Print_Report As System.Windows.Forms.Button
    Friend WithEvents btnCloseReport As System.Windows.Forms.Button
    Friend WithEvents btnExportPDF As System.Windows.Forms.Button
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents pnlReportHead As System.Windows.Forms.Panel
    Friend WithEvents lblusernam As System.Windows.Forms.Label
    Friend WithEvents lblusername As System.Windows.Forms.Label
    Friend WithEvents lblhead As System.Windows.Forms.Label
    Friend WithEvents picHead As System.Windows.Forms.PictureBox
End Class
