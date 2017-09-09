<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEndofthedayrep
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEndofthedayrep))
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtstDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GrpBox_SalesSummary = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cmbSm = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtendDate = New System.Windows.Forms.DateTimePicker()
        Me.btView = New System.Windows.Forms.Button()
        Me.btnrefreshEOD = New System.Windows.Forms.Button()
        Me.pnlReportHead = New System.Windows.Forms.Panel()
        Me.lblusernam = New System.Windows.Forms.Label()
        Me.lblusername = New System.Windows.Forms.Label()
        Me.lblhead = New System.Windows.Forms.Label()
        Me.picHead = New System.Windows.Forms.PictureBox()
        Me.pnlRptContainer = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.pnl_EDRDetails = New System.Windows.Forms.Panel()
        Me.pnl_detailsofEDR = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSm = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblLoc = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.delval_LC = New System.Windows.Forms.Label()
        Me.holdval_LC = New System.Windows.Forms.Label()
        Me.invval_LC = New System.Windows.Forms.Label()
        Me.cancelval_LC = New System.Windows.Forms.Label()
        Me.salesval_LC = New System.Windows.Forms.Label()
        Me.delval = New System.Windows.Forms.Label()
        Me.delcount = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.holdval = New System.Windows.Forms.Label()
        Me.invcount = New System.Windows.Forms.Label()
        Me.holdcount = New System.Windows.Forms.Label()
        Me.invval = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.salescount = New System.Windows.Forms.Label()
        Me.cancelval = New System.Windows.Forms.Label()
        Me.salesval = New System.Windows.Forms.Label()
        Me.cancelcount = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnldate = New System.Windows.Forms.Panel()
        Me.lbltoDate = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblfrmDate = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.pnl_endofthereport = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.GrpBox_SalesSummary.SuspendLayout()
        Me.pnlReportHead.SuspendLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRptContainer.SuspendLayout()
        Me.pnl_EDRDetails.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnldate.SuspendLayout()
        Me.pnl_endofthereport.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbLocation
        '
        Me.cmbLocation.AllowDrop = True
        Me.cmbLocation.Enabled = False
        Me.cmbLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Items.AddRange(New Object() {"001", "013", "002"})
        Me.cmbLocation.Location = New System.Drawing.Point(161, 61)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(134, 22)
        Me.cmbLocation.TabIndex = 88
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(69, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 14)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Location"
        '
        'dtstDate
        '
        Me.dtstDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtstDate.Location = New System.Drawing.Point(161, 24)
        Me.dtstDate.Name = "dtstDate"
        Me.dtstDate.Size = New System.Drawing.Size(134, 20)
        Me.dtstDate.TabIndex = 91
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(69, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 20)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Start Date"
        '
        'GrpBox_SalesSummary
        '
        Me.GrpBox_SalesSummary.Controls.Add(Me.Label9)
        Me.GrpBox_SalesSummary.Controls.Add(Me.Button1)
        Me.GrpBox_SalesSummary.Controls.Add(Me.Label33)
        Me.GrpBox_SalesSummary.Controls.Add(Me.cmbSm)
        Me.GrpBox_SalesSummary.Controls.Add(Me.Label4)
        Me.GrpBox_SalesSummary.Controls.Add(Me.dtendDate)
        Me.GrpBox_SalesSummary.Controls.Add(Me.btView)
        Me.GrpBox_SalesSummary.Controls.Add(Me.Label6)
        Me.GrpBox_SalesSummary.Controls.Add(Me.dtstDate)
        Me.GrpBox_SalesSummary.Controls.Add(Me.Label5)
        Me.GrpBox_SalesSummary.Controls.Add(Me.cmbLocation)
        Me.GrpBox_SalesSummary.Controls.Add(Me.btnrefreshEOD)
        Me.GrpBox_SalesSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBox_SalesSummary.Location = New System.Drawing.Point(32, 76)
        Me.GrpBox_SalesSummary.Name = "GrpBox_SalesSummary"
        Me.GrpBox_SalesSummary.Size = New System.Drawing.Size(906, 163)
        Me.GrpBox_SalesSummary.TabIndex = 92
        Me.GrpBox_SalesSummary.TabStop = False
        Me.GrpBox_SalesSummary.Text = "Choose Options"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(69, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(264, 14)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "Note :    Click Refresh to regenerate the Report"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.POS.My.Resources.Resources.Printer_icon
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(731, 104)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 39)
        Me.Button1.TabIndex = 101
        Me.Button1.Text = "Print"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(351, 65)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(61, 14)
        Me.Label33.TabIndex = 99
        Me.Label33.Text = "Salesman"
        '
        'cmbSm
        '
        Me.cmbSm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSm.FormattingEnabled = True
        Me.cmbSm.Location = New System.Drawing.Point(443, 60)
        Me.cmbSm.Name = "cmbSm"
        Me.cmbSm.Size = New System.Drawing.Size(134, 22)
        Me.cmbSm.TabIndex = 100
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(351, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 20)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "End Date"
        '
        'dtendDate
        '
        Me.dtendDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtendDate.Location = New System.Drawing.Point(443, 21)
        Me.dtendDate.Name = "dtendDate"
        Me.dtendDate.Size = New System.Drawing.Size(134, 20)
        Me.dtendDate.TabIndex = 94
        '
        'btView
        '
        Me.btView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btView.Image = Global.POS.My.Resources.Resources.Reports_ICON
        Me.btView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btView.Location = New System.Drawing.Point(596, 104)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(129, 39)
        Me.btView.TabIndex = 92
        Me.btView.Text = "View Report"
        Me.btView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btView.UseVisualStyleBackColor = True
        '
        'btnrefreshEOD
        '
        Me.btnrefreshEOD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefreshEOD.ForeColor = System.Drawing.SystemColors.Desktop
        Me.btnrefreshEOD.Image = Global.POS.My.Resources.Resources.arrow_refresh_copy
        Me.btnrefreshEOD.Location = New System.Drawing.Point(825, 104)
        Me.btnrefreshEOD.Name = "btnrefreshEOD"
        Me.btnrefreshEOD.Size = New System.Drawing.Size(33, 39)
        Me.btnrefreshEOD.TabIndex = 102
        Me.btnrefreshEOD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnrefreshEOD.UseVisualStyleBackColor = True
        '
        'pnlReportHead
        '
        Me.pnlReportHead.BackColor = System.Drawing.Color.MediumTurquoise
        Me.pnlReportHead.Controls.Add(Me.lblusernam)
        Me.pnlReportHead.Controls.Add(Me.lblusername)
        Me.pnlReportHead.Controls.Add(Me.lblhead)
        Me.pnlReportHead.Controls.Add(Me.picHead)
        Me.pnlReportHead.Location = New System.Drawing.Point(0, 3)
        Me.pnlReportHead.Name = "pnlReportHead"
        Me.pnlReportHead.Size = New System.Drawing.Size(1027, 45)
        Me.pnlReportHead.TabIndex = 94
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
        Me.lblhead.Location = New System.Drawing.Point(53, 5)
        Me.lblhead.Name = "lblhead"
        Me.lblhead.Size = New System.Drawing.Size(145, 22)
        Me.lblhead.TabIndex = 8
        Me.lblhead.Text = "End of the Day"
        '
        'picHead
        '
        Me.picHead.Image = CType(resources.GetObject("picHead.Image"), System.Drawing.Image)
        Me.picHead.Location = New System.Drawing.Point(-4, -4)
        Me.picHead.Name = "picHead"
        Me.picHead.Size = New System.Drawing.Size(41, 43)
        Me.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picHead.TabIndex = 7
        Me.picHead.TabStop = False
        '
        'pnlRptContainer
        '
        Me.pnlRptContainer.AutoScroll = True
        Me.pnlRptContainer.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.pnlRptContainer.Controls.Add(Me.Panel6)
        Me.pnlRptContainer.Controls.Add(Me.pnl_EDRDetails)
        Me.pnlRptContainer.Location = New System.Drawing.Point(64, 244)
        Me.pnlRptContainer.Name = "pnlRptContainer"
        Me.pnlRptContainer.Size = New System.Drawing.Size(837, 333)
        Me.pnlRptContainer.TabIndex = 96
        Me.pnlRptContainer.Visible = False
        '
        'Panel6
        '
        Me.Panel6.Location = New System.Drawing.Point(7, 3011)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(808, 8)
        Me.Panel6.TabIndex = 91
        '
        'pnl_EDRDetails
        '
        Me.pnl_EDRDetails.AutoScroll = True
        Me.pnl_EDRDetails.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pnl_EDRDetails.Controls.Add(Me.pnl_detailsofEDR)
        Me.pnl_EDRDetails.Controls.Add(Me.Panel3)
        Me.pnl_EDRDetails.Controls.Add(Me.Panel1)
        Me.pnl_EDRDetails.Controls.Add(Me.Panel2)
        Me.pnl_EDRDetails.Controls.Add(Me.pnldate)
        Me.pnl_EDRDetails.Location = New System.Drawing.Point(5, 0)
        Me.pnl_EDRDetails.Name = "pnl_EDRDetails"
        Me.pnl_EDRDetails.Size = New System.Drawing.Size(811, 3005)
        Me.pnl_EDRDetails.TabIndex = 90
        '
        'pnl_detailsofEDR
        '
        Me.pnl_detailsofEDR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_detailsofEDR.Location = New System.Drawing.Point(36, 331)
        Me.pnl_detailsofEDR.Name = "pnl_detailsofEDR"
        Me.pnl_detailsofEDR.Size = New System.Drawing.Size(738, 2671)
        Me.pnl_detailsofEDR.TabIndex = 125
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(36, 49)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(738, 57)
        Me.Panel3.TabIndex = 123
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(284, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(147, 14)
        Me.Label18.TabIndex = 94
        Me.Label18.Text = "End of Day/Shift Summary"
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = Global.POS.My.Resources.Resources.clientlogo1
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(63, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblSm)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.lblLoc)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(36, 135)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(738, 100)
        Me.Panel1.TabIndex = 119
        '
        'lblSm
        '
        Me.lblSm.AutoSize = True
        Me.lblSm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSm.Location = New System.Drawing.Point(344, 14)
        Me.lblSm.Name = "lblSm"
        Me.lblSm.Size = New System.Drawing.Size(35, 14)
        Me.lblSm.TabIndex = 100
        Me.lblSm.Text = "Count"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(247, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(61, 14)
        Me.Label27.TabIndex = 98
        Me.Label27.Text = "Salesman"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(132, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(25, 14)
        Me.Label23.TabIndex = 96
        Me.Label23.Text = "001"
        '
        'lblLoc
        '
        Me.lblLoc.AutoSize = True
        Me.lblLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoc.Location = New System.Drawing.Point(573, 18)
        Me.lblLoc.Name = "lblLoc"
        Me.lblLoc.Size = New System.Drawing.Size(35, 14)
        Me.lblLoc.TabIndex = 97
        Me.lblLoc.Text = "Count"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(480, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(54, 14)
        Me.Label22.TabIndex = 93
        Me.Label22.Text = "Location"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(35, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 14)
        Me.Label21.TabIndex = 92
        Me.Label21.Text = "Company"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(630, 82)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(87, 14)
        Me.Label19.TabIndex = 91
        Me.Label19.Text = "LC Value (AED)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(34, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 14)
        Me.Label1.TabIndex = 88
        Me.Label1.Text = "Transaction"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(342, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 14)
        Me.Label2.TabIndex = 89
        Me.Label2.Text = "No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(487, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 14)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "FC Value (AED)"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.delval_LC)
        Me.Panel2.Controls.Add(Me.holdval_LC)
        Me.Panel2.Controls.Add(Me.invval_LC)
        Me.Panel2.Controls.Add(Me.cancelval_LC)
        Me.Panel2.Controls.Add(Me.salesval_LC)
        Me.Panel2.Controls.Add(Me.delval)
        Me.Panel2.Controls.Add(Me.delcount)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.holdval)
        Me.Panel2.Controls.Add(Me.invcount)
        Me.Panel2.Controls.Add(Me.holdcount)
        Me.Panel2.Controls.Add(Me.invval)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.salescount)
        Me.Panel2.Controls.Add(Me.cancelval)
        Me.Panel2.Controls.Add(Me.salesval)
        Me.Panel2.Controls.Add(Me.cancelcount)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Location = New System.Drawing.Point(36, 234)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(738, 99)
        Me.Panel2.TabIndex = 122
        '
        'delval_LC
        '
        Me.delval_LC.AutoSize = True
        Me.delval_LC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delval_LC.Location = New System.Drawing.Point(655, 76)
        Me.delval_LC.Name = "delval_LC"
        Me.delval_LC.Size = New System.Drawing.Size(13, 14)
        Me.delval_LC.TabIndex = 126
        Me.delval_LC.Text = "0"
        '
        'holdval_LC
        '
        Me.holdval_LC.AutoSize = True
        Me.holdval_LC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.holdval_LC.Location = New System.Drawing.Point(655, 60)
        Me.holdval_LC.Name = "holdval_LC"
        Me.holdval_LC.Size = New System.Drawing.Size(13, 14)
        Me.holdval_LC.TabIndex = 125
        Me.holdval_LC.Text = "0"
        '
        'invval_LC
        '
        Me.invval_LC.AutoSize = True
        Me.invval_LC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.invval_LC.Location = New System.Drawing.Point(655, 11)
        Me.invval_LC.Name = "invval_LC"
        Me.invval_LC.Size = New System.Drawing.Size(13, 14)
        Me.invval_LC.TabIndex = 122
        Me.invval_LC.Text = "0"
        '
        'cancelval_LC
        '
        Me.cancelval_LC.AutoSize = True
        Me.cancelval_LC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelval_LC.Location = New System.Drawing.Point(655, 44)
        Me.cancelval_LC.Name = "cancelval_LC"
        Me.cancelval_LC.Size = New System.Drawing.Size(13, 14)
        Me.cancelval_LC.TabIndex = 124
        Me.cancelval_LC.Text = "0"
        '
        'salesval_LC
        '
        Me.salesval_LC.AutoSize = True
        Me.salesval_LC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.salesval_LC.Location = New System.Drawing.Point(655, 28)
        Me.salesval_LC.Name = "salesval_LC"
        Me.salesval_LC.Size = New System.Drawing.Size(13, 14)
        Me.salesval_LC.TabIndex = 123
        Me.salesval_LC.Text = "0"
        '
        'delval
        '
        Me.delval.AutoSize = True
        Me.delval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delval.Location = New System.Drawing.Point(511, 76)
        Me.delval.Name = "delval"
        Me.delval.Size = New System.Drawing.Size(13, 14)
        Me.delval.TabIndex = 121
        Me.delval.Text = "0"
        '
        'delcount
        '
        Me.delcount.AutoSize = True
        Me.delcount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delcount.Location = New System.Drawing.Point(347, 76)
        Me.delcount.Name = "delcount"
        Me.delcount.Size = New System.Drawing.Size(13, 14)
        Me.delcount.TabIndex = 120
        Me.delcount.Text = "0"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(36, 76)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(43, 14)
        Me.Label20.TabIndex = 119
        Me.Label20.Text = "Deleted"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(36, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 14)
        Me.Label10.TabIndex = 94
        Me.Label10.Text = "Salesreturn"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(36, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 14)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Invoice"
        '
        'holdval
        '
        Me.holdval.AutoSize = True
        Me.holdval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.holdval.Location = New System.Drawing.Point(511, 60)
        Me.holdval.Name = "holdval"
        Me.holdval.Size = New System.Drawing.Size(13, 14)
        Me.holdval.TabIndex = 102
        Me.holdval.Text = "0"
        '
        'invcount
        '
        Me.invcount.AutoSize = True
        Me.invcount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.invcount.Location = New System.Drawing.Point(347, 11)
        Me.invcount.Name = "invcount"
        Me.invcount.Size = New System.Drawing.Size(13, 14)
        Me.invcount.TabIndex = 92
        Me.invcount.Text = "0"
        '
        'holdcount
        '
        Me.holdcount.AutoSize = True
        Me.holdcount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.holdcount.Location = New System.Drawing.Point(347, 60)
        Me.holdcount.Name = "holdcount"
        Me.holdcount.Size = New System.Drawing.Size(13, 14)
        Me.holdcount.TabIndex = 101
        Me.holdcount.Text = "0"
        '
        'invval
        '
        Me.invval.AutoSize = True
        Me.invval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.invval.Location = New System.Drawing.Point(511, 11)
        Me.invval.Name = "invval"
        Me.invval.Size = New System.Drawing.Size(13, 14)
        Me.invval.TabIndex = 93
        Me.invval.Text = "0"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(36, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 14)
        Me.Label16.TabIndex = 100
        Me.Label16.Text = "Holding"
        '
        'salescount
        '
        Me.salescount.AutoSize = True
        Me.salescount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.salescount.Location = New System.Drawing.Point(347, 28)
        Me.salescount.Name = "salescount"
        Me.salescount.Size = New System.Drawing.Size(13, 14)
        Me.salescount.TabIndex = 95
        Me.salescount.Text = "0"
        '
        'cancelval
        '
        Me.cancelval.AutoSize = True
        Me.cancelval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelval.Location = New System.Drawing.Point(511, 44)
        Me.cancelval.Name = "cancelval"
        Me.cancelval.Size = New System.Drawing.Size(13, 14)
        Me.cancelval.TabIndex = 99
        Me.cancelval.Text = "0"
        '
        'salesval
        '
        Me.salesval.AutoSize = True
        Me.salesval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.salesval.Location = New System.Drawing.Point(511, 28)
        Me.salesval.Name = "salesval"
        Me.salesval.Size = New System.Drawing.Size(13, 14)
        Me.salesval.TabIndex = 96
        Me.salesval.Text = "0"
        '
        'cancelcount
        '
        Me.cancelcount.AutoSize = True
        Me.cancelcount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelcount.Location = New System.Drawing.Point(347, 44)
        Me.cancelcount.Name = "cancelcount"
        Me.cancelcount.Size = New System.Drawing.Size(13, 14)
        Me.cancelcount.TabIndex = 98
        Me.cancelcount.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(36, 44)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 14)
        Me.Label13.TabIndex = 97
        Me.Label13.Text = "Cancel"
        '
        'pnldate
        '
        Me.pnldate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnldate.Controls.Add(Me.lbltoDate)
        Me.pnldate.Controls.Add(Me.Label14)
        Me.pnldate.Controls.Add(Me.lblfrmDate)
        Me.pnldate.Controls.Add(Me.Label11)
        Me.pnldate.Location = New System.Drawing.Point(36, 108)
        Me.pnldate.Name = "pnldate"
        Me.pnldate.Size = New System.Drawing.Size(738, 28)
        Me.pnldate.TabIndex = 118
        '
        'lbltoDate
        '
        Me.lbltoDate.AutoSize = True
        Me.lbltoDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltoDate.Location = New System.Drawing.Point(516, 5)
        Me.lbltoDate.Name = "lbltoDate"
        Me.lbltoDate.Size = New System.Drawing.Size(35, 14)
        Me.lbltoDate.TabIndex = 95
        Me.lbltoDate.Text = "Count"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(410, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 14)
        Me.Label14.TabIndex = 94
        Me.Label14.Text = "To"
        '
        'lblfrmDate
        '
        Me.lblfrmDate.AutoSize = True
        Me.lblfrmDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrmDate.Location = New System.Drawing.Point(293, 5)
        Me.lblfrmDate.Name = "lblfrmDate"
        Me.lblfrmDate.Size = New System.Drawing.Size(35, 14)
        Me.lblfrmDate.TabIndex = 93
        Me.lblfrmDate.Text = "Count"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(184, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 14)
        Me.Label11.TabIndex = 89
        Me.Label11.Text = "From"
        '
        'pnl_endofthereport
        '
        Me.pnl_endofthereport.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pnl_endofthereport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_endofthereport.Controls.Add(Me.Label8)
        Me.pnl_endofthereport.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.pnl_endofthereport.Location = New System.Drawing.Point(32, 245)
        Me.pnl_endofthereport.Name = "pnl_endofthereport"
        Me.pnl_endofthereport.Size = New System.Drawing.Size(906, 333)
        Me.pnl_endofthereport.TabIndex = 103
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label8.Location = New System.Drawing.Point(251, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(350, 46)
        Me.Label8.TabIndex = 100
        Me.Label8.Text = "End of the Day Report"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'frmEndofthedayrep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(1028, 732)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlReportHead)
        Me.Controls.Add(Me.GrpBox_SalesSummary)
        Me.Controls.Add(Me.pnlRptContainer)
        Me.Controls.Add(Me.pnl_endofthereport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEndofthedayrep"
        Me.Text = "frmEndofthedayrep"
        Me.GrpBox_SalesSummary.ResumeLayout(False)
        Me.GrpBox_SalesSummary.PerformLayout()
        Me.pnlReportHead.ResumeLayout(False)
        Me.pnlReportHead.PerformLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRptContainer.ResumeLayout(False)
        Me.pnl_EDRDetails.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnldate.ResumeLayout(False)
        Me.pnldate.PerformLayout()
        Me.pnl_endofthereport.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GrpBox_SalesSummary As System.Windows.Forms.GroupBox
    Friend WithEvents pnlReportHead As System.Windows.Forms.Panel
    Friend WithEvents lblusernam As System.Windows.Forms.Label
    Friend WithEvents lblusername As System.Windows.Forms.Label
    Friend WithEvents lblhead As System.Windows.Forms.Label
    Friend WithEvents picHead As System.Windows.Forms.PictureBox
    Friend WithEvents btView As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtendDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cmbSm As System.Windows.Forms.ComboBox
    Friend WithEvents pnlRptContainer As System.Windows.Forms.Panel
    Friend WithEvents pnl_EDRDetails As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSm As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblLoc As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents delval_LC As System.Windows.Forms.Label
    Friend WithEvents holdval_LC As System.Windows.Forms.Label
    Friend WithEvents invval_LC As System.Windows.Forms.Label
    Friend WithEvents cancelval_LC As System.Windows.Forms.Label
    Friend WithEvents salesval_LC As System.Windows.Forms.Label
    Friend WithEvents delval As System.Windows.Forms.Label
    Friend WithEvents delcount As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents holdval As System.Windows.Forms.Label
    Friend WithEvents invcount As System.Windows.Forms.Label
    Friend WithEvents holdcount As System.Windows.Forms.Label
    Friend WithEvents invval As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents salescount As System.Windows.Forms.Label
    Friend WithEvents cancelval As System.Windows.Forms.Label
    Friend WithEvents salesval As System.Windows.Forms.Label
    Friend WithEvents cancelcount As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnldate As System.Windows.Forms.Panel
    Friend WithEvents lbltoDate As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblfrmDate As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents pnl_detailsofEDR As System.Windows.Forms.Panel
    Friend WithEvents btnrefreshEOD As System.Windows.Forms.Button
    Friend WithEvents pnl_endofthereport As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
End Class
