<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DailyTransReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DailyTransReport))
        Me.cmbLocation = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtstDate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnPrintReport = New System.Windows.Forms.Button
        Me.Label33 = New System.Windows.Forms.Label
        Me.cmbSm = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtendDate = New System.Windows.Forms.DateTimePicker
        Me.btView = New System.Windows.Forms.Button
        Me.btnrefreshEOD = New System.Windows.Forms.Button
        Me.pnlReportHead = New System.Windows.Forms.Panel
        Me.lblusernam = New System.Windows.Forms.Label
        Me.lblusername = New System.Windows.Forms.Label
        Me.lblhead = New System.Windows.Forms.Label
        Me.picHead = New System.Windows.Forms.PictureBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblReportTitle = New System.Windows.Forms.Label
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.pnlReportHead.SuspendLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbLocation
        '
        Me.cmbLocation.AllowDrop = True
        Me.cmbLocation.Enabled = False
        Me.cmbLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Items.AddRange(New Object() {"001", "013", "002"})
        Me.cmbLocation.Location = New System.Drawing.Point(83, 103)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(121, 22)
        Me.cmbLocation.TabIndex = 88
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 14)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Location"
        '
        'dtstDate
        '
        Me.dtstDate.CustomFormat = "dd/MM/yyyy"
        Me.dtstDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtstDate.Location = New System.Drawing.Point(83, 31)
        Me.dtstDate.Name = "dtstDate"
        Me.dtstDate.Size = New System.Drawing.Size(121, 20)
        Me.dtstDate.TabIndex = 91
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 20)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Start Date"
        '
        'btnPrintReport
        '
        Me.btnPrintReport.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnPrintReport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrintReport.FlatAppearance.BorderColor = System.Drawing.Color.MediumTurquoise
        Me.btnPrintReport.FlatAppearance.BorderSize = 2
        Me.btnPrintReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.btnPrintReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintReport.ForeColor = System.Drawing.Color.Black
        Me.btnPrintReport.Image = Global.POS.My.Resources.Resources.Printer_icon
        Me.btnPrintReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrintReport.Location = New System.Drawing.Point(77, 192)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(65, 89)
        Me.btnPrintReport.TabIndex = 101
        Me.btnPrintReport.Text = "Print Report"
        Me.btnPrintReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrintReport.UseVisualStyleBackColor = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(17, 144)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(61, 14)
        Me.Label33.TabIndex = 99
        Me.Label33.Text = "Salesman"
        '
        'cmbSm
        '
        Me.cmbSm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSm.FormattingEnabled = True
        Me.cmbSm.Location = New System.Drawing.Point(83, 140)
        Me.cmbSm.Name = "cmbSm"
        Me.cmbSm.Size = New System.Drawing.Size(121, 22)
        Me.cmbSm.TabIndex = 100
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "End Date"
        '
        'dtendDate
        '
        Me.dtendDate.CustomFormat = "dd/MM/yyyy"
        Me.dtendDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtendDate.Location = New System.Drawing.Point(83, 67)
        Me.dtendDate.Name = "dtendDate"
        Me.dtendDate.Size = New System.Drawing.Size(121, 20)
        Me.dtendDate.TabIndex = 94
        '
        'btView
        '
        Me.btView.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btView.FlatAppearance.BorderColor = System.Drawing.Color.MediumTurquoise
        Me.btView.FlatAppearance.BorderSize = 2
        Me.btView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.btView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btView.Image = Global.POS.My.Resources.Resources.Reports_ICON
        Me.btView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btView.Location = New System.Drawing.Point(7, 192)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(65, 89)
        Me.btView.TabIndex = 92
        Me.btView.Text = "View Report"
        Me.btView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btView.UseVisualStyleBackColor = False
        '
        'btnrefreshEOD
        '
        Me.btnrefreshEOD.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnrefreshEOD.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrefreshEOD.FlatAppearance.BorderColor = System.Drawing.Color.MediumTurquoise
        Me.btnrefreshEOD.FlatAppearance.BorderSize = 2
        Me.btnrefreshEOD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.btnrefreshEOD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefreshEOD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefreshEOD.ForeColor = System.Drawing.Color.Black
        Me.btnrefreshEOD.Image = Global.POS.My.Resources.Resources.Export_To_File_icon
        Me.btnrefreshEOD.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnrefreshEOD.Location = New System.Drawing.Point(148, 192)
        Me.btnrefreshEOD.Name = "btnrefreshEOD"
        Me.btnrefreshEOD.Size = New System.Drawing.Size(65, 89)
        Me.btnrefreshEOD.TabIndex = 102
        Me.btnrefreshEOD.Text = "Export Report"
        Me.btnrefreshEOD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnrefreshEOD.UseVisualStyleBackColor = False
        '
        'pnlReportHead
        '
        Me.pnlReportHead.BackColor = System.Drawing.Color.MediumTurquoise
        Me.pnlReportHead.Controls.Add(Me.lblusernam)
        Me.pnlReportHead.Controls.Add(Me.lblusername)
        Me.pnlReportHead.Controls.Add(Me.lblhead)
        Me.pnlReportHead.Controls.Add(Me.picHead)
        Me.pnlReportHead.Location = New System.Drawing.Point(0, 1)
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
        Me.lblhead.Location = New System.Drawing.Point(53, 16)
        Me.lblhead.Name = "lblhead"
        Me.lblhead.Size = New System.Drawing.Size(296, 22)
        Me.lblhead.TabIndex = 8
        Me.lblhead.Text = "Daily Sales Transaction Report"
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
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 49)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtendDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPrintReport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnrefreshEOD)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label33)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btView)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSm)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtstDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblReportTitle)
        Me.SplitContainer1.Size = New System.Drawing.Size(1024, 575)
        Me.SplitContainer1.SplitterDistance = 226
        Me.SplitContainer1.TabIndex = 97
        '
        'lblReportTitle
        '
        Me.lblReportTitle.AutoSize = True
        Me.lblReportTitle.Font = New System.Drawing.Font("Times New Roman", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportTitle.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblReportTitle.Location = New System.Drawing.Point(312, 240)
        Me.lblReportTitle.Name = "lblReportTitle"
        Me.lblReportTitle.Size = New System.Drawing.Size(223, 73)
        Me.lblReportTitle.TabIndex = 0
        Me.lblReportTitle.Text = "Report"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'DailyTransReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(1028, 732)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.pnlReportHead)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DailyTransReport"
        Me.Text = "frmEndofthedayrep"
        Me.pnlReportHead.ResumeLayout(False)
        Me.pnlReportHead.PerformLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
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
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnrefreshEOD As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblReportTitle As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
End Class
