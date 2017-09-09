<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGVRep
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
        Me.pnlCampaignHead = New System.Windows.Forms.Panel
        Me.lblusernam = New System.Windows.Forms.Label
        Me.lblusername = New System.Windows.Forms.Label
        Me.lblhead = New System.Windows.Forms.Label
        Me.picHead = New System.Windows.Forms.PictureBox
        Me.lblNoList = New System.Windows.Forms.Label
        Me.listGV = New System.Windows.Forms.ListView
        Me.butAddExcel = New System.Windows.Forms.Button
        Me.Grpbox_GVoptions = New System.Windows.Forms.GroupBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.cmbSm = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtendDate = New System.Windows.Forms.DateTimePicker
        Me.btView = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtstDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbLocation = New System.Windows.Forms.ComboBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.pnlCampaignHead.SuspendLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grpbox_GVoptions.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlCampaignHead
        '
        Me.pnlCampaignHead.BackColor = System.Drawing.Color.MediumTurquoise
        Me.pnlCampaignHead.Controls.Add(Me.lblusernam)
        Me.pnlCampaignHead.Controls.Add(Me.lblusername)
        Me.pnlCampaignHead.Controls.Add(Me.lblhead)
        Me.pnlCampaignHead.Controls.Add(Me.picHead)
        Me.pnlCampaignHead.Location = New System.Drawing.Point(0, 2)
        Me.pnlCampaignHead.Name = "pnlCampaignHead"
        Me.pnlCampaignHead.Size = New System.Drawing.Size(1035, 45)
        Me.pnlCampaignHead.TabIndex = 75
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
        Me.lblhead.Location = New System.Drawing.Point(57, 11)
        Me.lblhead.Name = "lblhead"
        Me.lblhead.Size = New System.Drawing.Size(204, 22)
        Me.lblhead.TabIndex = 8
        Me.lblhead.Text = "Gift Vouchers Report"
        '
        'picHead
        '
        Me.picHead.Image = Global.POS.My.Resources.Resources.Reports_ICON
        Me.picHead.Location = New System.Drawing.Point(0, 2)
        Me.picHead.Name = "picHead"
        Me.picHead.Size = New System.Drawing.Size(41, 43)
        Me.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picHead.TabIndex = 7
        Me.picHead.TabStop = False
        '
        'lblNoList
        '
        Me.lblNoList.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblNoList.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoList.Location = New System.Drawing.Point(13, 223)
        Me.lblNoList.Name = "lblNoList"
        Me.lblNoList.Size = New System.Drawing.Size(932, 371)
        Me.lblNoList.TabIndex = 111
        Me.lblNoList.Text = "Gift Voucher Report"
        Me.lblNoList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'listGV
        '
        Me.listGV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listGV.Location = New System.Drawing.Point(16, 275)
        Me.listGV.Name = "listGV"
        Me.listGV.OwnerDraw = True
        Me.listGV.Size = New System.Drawing.Size(919, 306)
        Me.listGV.TabIndex = 120
        Me.listGV.UseCompatibleStateImageBehavior = False
        '
        'butAddExcel
        '
        Me.butAddExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddExcel.ForeColor = System.Drawing.SystemColors.Desktop
        Me.butAddExcel.Image = Global.POS.My.Resources.Resources.Export_To_File_icon
        Me.butAddExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butAddExcel.Location = New System.Drawing.Point(793, 232)
        Me.butAddExcel.Name = "butAddExcel"
        Me.butAddExcel.Size = New System.Drawing.Size(142, 37)
        Me.butAddExcel.TabIndex = 121
        Me.butAddExcel.Text = "Export to Excel"
        Me.butAddExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.butAddExcel.UseVisualStyleBackColor = True
        '
        'Grpbox_GVoptions
        '
        Me.Grpbox_GVoptions.Controls.Add(Me.Label33)
        Me.Grpbox_GVoptions.Controls.Add(Me.cmbSm)
        Me.Grpbox_GVoptions.Controls.Add(Me.Label4)
        Me.Grpbox_GVoptions.Controls.Add(Me.dtendDate)
        Me.Grpbox_GVoptions.Controls.Add(Me.btView)
        Me.Grpbox_GVoptions.Controls.Add(Me.Label6)
        Me.Grpbox_GVoptions.Controls.Add(Me.dtstDate)
        Me.Grpbox_GVoptions.Controls.Add(Me.Label5)
        Me.Grpbox_GVoptions.Controls.Add(Me.cmbLocation)
        Me.Grpbox_GVoptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grpbox_GVoptions.Location = New System.Drawing.Point(15, 72)
        Me.Grpbox_GVoptions.Name = "Grpbox_GVoptions"
        Me.Grpbox_GVoptions.Size = New System.Drawing.Size(930, 135)
        Me.Grpbox_GVoptions.TabIndex = 122
        Me.Grpbox_GVoptions.TabStop = False
        Me.Grpbox_GVoptions.Text = "Choose Options"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(351, 63)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(61, 14)
        Me.Label33.TabIndex = 99
        Me.Label33.Text = "Salesman"
        '
        'cmbSm
        '
        Me.cmbSm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSm.FormattingEnabled = True
        Me.cmbSm.Location = New System.Drawing.Point(443, 59)
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
        Me.btView.ForeColor = System.Drawing.SystemColors.Desktop
        Me.btView.Image = Global.POS.My.Resources.Resources.Reports_ICON1
        Me.btView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btView.Location = New System.Drawing.Point(598, 82)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(135, 39)
        Me.btView.TabIndex = 92
        Me.btView.Text = "View Report  "
        Me.btView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btView.UseVisualStyleBackColor = True
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
        'dtstDate
        '
        Me.dtstDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtstDate.Location = New System.Drawing.Point(161, 24)
        Me.dtstDate.Name = "dtstDate"
        Me.dtstDate.Size = New System.Drawing.Size(134, 20)
        Me.dtstDate.TabIndex = 91
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
        'cmbLocation
        '
        Me.cmbLocation.Enabled = False
        Me.cmbLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(161, 61)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(134, 22)
        Me.cmbLocation.TabIndex = 88
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.POS.My.Resources.Resources.Export_To_File_icon
        Me.PictureBox1.Location = New System.Drawing.Point(815, 234)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 23)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 123
        Me.PictureBox1.TabStop = False
        '
        'frmGVRep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(1036, 780)
        Me.ControlBox = False
        Me.Controls.Add(Me.Grpbox_GVoptions)
        Me.Controls.Add(Me.pnlCampaignHead)
        Me.Controls.Add(Me.lblNoList)
        Me.Controls.Add(Me.listGV)
        Me.Controls.Add(Me.butAddExcel)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGVRep"
        Me.Text = "frmStatusinfoRep"
        Me.pnlCampaignHead.ResumeLayout(False)
        Me.pnlCampaignHead.PerformLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grpbox_GVoptions.ResumeLayout(False)
        Me.Grpbox_GVoptions.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCampaignHead As System.Windows.Forms.Panel
    Friend WithEvents lblusernam As System.Windows.Forms.Label
    Friend WithEvents lblusername As System.Windows.Forms.Label
    Friend WithEvents lblhead As System.Windows.Forms.Label
    Friend WithEvents picHead As System.Windows.Forms.PictureBox
    Friend WithEvents lblNoList As System.Windows.Forms.Label
    Friend WithEvents listGV As System.Windows.Forms.ListView
    Friend WithEvents butAddExcel As System.Windows.Forms.Button
    Friend WithEvents Grpbox_GVoptions As System.Windows.Forms.GroupBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cmbSm As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtendDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btView As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
End Class
