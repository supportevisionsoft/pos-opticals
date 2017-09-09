<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockReport
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
        Me.pnlReportHead = New System.Windows.Forms.Panel
        Me.btnCloseForm = New System.Windows.Forms.Button
        Me.lblusernam = New System.Windows.Forms.Label
        Me.lblusername = New System.Windows.Forms.Label
        Me.lblhead = New System.Windows.Forms.Label
        Me.picHead = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtSubGroup = New System.Windows.Forms.TextBox
        Me.txtmaingrp = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbitemfrom = New System.Windows.Forms.ComboBox
        Me.cmbitemto = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbsubgrp = New System.Windows.Forms.ComboBox
        Me.cmbmaingrp = New System.Windows.Forms.ComboBox
        Me.cbLocationfrom = New System.Windows.Forms.ComboBox
        Me.lblLoc = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btView = New System.Windows.Forms.Button
        Me.listProduct = New System.Windows.Forms.ListView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblNoList = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.butAddExcel = New System.Windows.Forms.Button
        Me.pnlReportHead.SuspendLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlReportHead
        '
        Me.pnlReportHead.BackColor = System.Drawing.Color.MediumTurquoise
        Me.pnlReportHead.Controls.Add(Me.btnCloseForm)
        Me.pnlReportHead.Controls.Add(Me.lblusernam)
        Me.pnlReportHead.Controls.Add(Me.lblusername)
        Me.pnlReportHead.Controls.Add(Me.lblhead)

        Me.pnlReportHead.Controls.Add(Me.picHead)
        Me.pnlReportHead.Location = New System.Drawing.Point(-2, 2)
        Me.pnlReportHead.Name = "pnlReportHead"
        Me.pnlReportHead.Size = New System.Drawing.Size(1020, 45)
        Me.pnlReportHead.TabIndex = 76
        '
        'btnCloseForm
        '
        Me.btnCloseForm.FlatAppearance.BorderSize = 0
        Me.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseForm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseForm.ForeColor = System.Drawing.SystemColors.Desktop
        Me.btnCloseForm.Image = Global.POS.My.Resources.Resources.Exit_DI
        Me.btnCloseForm.Location = New System.Drawing.Point(977, 4)
        Me.btnCloseForm.Name = "btnCloseForm"
        Me.btnCloseForm.Size = New System.Drawing.Size(40, 36)
        Me.btnCloseForm.TabIndex = 103
        Me.btnCloseForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCloseForm.UseVisualStyleBackColor = True
        Me.btnCloseForm.Visible = False
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
        Me.lblhead.Size = New System.Drawing.Size(194, 22)
        Me.lblhead.TabIndex = 8
        Me.lblhead.Text = "Stock Status Report"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSubGroup)
        Me.GroupBox1.Controls.Add(Me.txtmaingrp)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbitemfrom)
        Me.GroupBox1.Controls.Add(Me.cmbitemto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbsubgrp)
        Me.GroupBox1.Controls.Add(Me.cmbmaingrp)
        Me.GroupBox1.Controls.Add(Me.cbLocationfrom)
        Me.GroupBox1.Controls.Add(Me.lblLoc)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btView)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(880, 114)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose Options"
        '
        'txtSubGroup
        '
        Me.txtSubGroup.Location = New System.Drawing.Point(572, 22)
        Me.txtSubGroup.Name = "txtSubGroup"
        Me.txtSubGroup.Size = New System.Drawing.Size(168, 20)
        Me.txtSubGroup.TabIndex = 104
        Me.txtSubGroup.Visible = False
        '
        'txtmaingrp
        '
        Me.txtmaingrp.Location = New System.Drawing.Point(398, 23)
        Me.txtmaingrp.Name = "txtmaingrp"
        Me.txtmaingrp.Size = New System.Drawing.Size(168, 20)
        Me.txtmaingrp.TabIndex = 103
        Me.txtmaingrp.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(383, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 14)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "To Item"
        '
        'cmbitemfrom
        '
        Me.cmbitemfrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemfrom.FormattingEnabled = True
        Me.cmbitemfrom.Location = New System.Drawing.Point(159, 83)
        Me.cmbitemfrom.Name = "cmbitemfrom"
        Me.cmbitemfrom.Size = New System.Drawing.Size(194, 22)
        Me.cmbitemfrom.TabIndex = 101
        '
        'cmbitemto
        '
        Me.cmbitemto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemto.FormattingEnabled = True
        Me.cmbitemto.Location = New System.Drawing.Point(480, 83)
        Me.cmbitemto.Name = "cmbitemto"
        Me.cmbitemto.Size = New System.Drawing.Size(209, 22)
        Me.cmbitemto.TabIndex = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(383, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 14)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Sub Group"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(46, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 14)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Main Group"
        '
        'cmbsubgrp
        '
        Me.cmbsubgrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsubgrp.FormattingEnabled = True
        Me.cmbsubgrp.Location = New System.Drawing.Point(480, 51)
        Me.cmbsubgrp.Name = "cmbsubgrp"
        Me.cmbsubgrp.Size = New System.Drawing.Size(207, 22)
        Me.cmbsubgrp.TabIndex = 96
        '
        'cmbmaingrp
        '
        Me.cmbmaingrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbmaingrp.FormattingEnabled = True
        Me.cmbmaingrp.Location = New System.Drawing.Point(159, 51)
        Me.cmbmaingrp.Name = "cmbmaingrp"
        Me.cmbmaingrp.Size = New System.Drawing.Size(194, 22)
        Me.cmbmaingrp.TabIndex = 95
        '
        'cbLocationfrom
        '
        Me.cbLocationfrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLocationfrom.FormattingEnabled = True
        Me.cbLocationfrom.Location = New System.Drawing.Point(159, 20)
        Me.cbLocationfrom.Name = "cbLocationfrom"
        Me.cbLocationfrom.Size = New System.Drawing.Size(194, 22)
        Me.cbLocationfrom.TabIndex = 61
        '
        'lblLoc
        '
        Me.lblLoc.AutoSize = True
        Me.lblLoc.Location = New System.Drawing.Point(45, 23)
        Me.lblLoc.Name = "lblLoc"
        Me.lblLoc.Size = New System.Drawing.Size(57, 14)
        Me.lblLoc.TabIndex = 60
        Me.lblLoc.Text = "Location "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(48, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 14)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "From Item"
        '
        'btView
        '
        Me.btView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btView.ForeColor = System.Drawing.SystemColors.Desktop
        Me.btView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btView.Location = New System.Drawing.Point(762, 73)
        Me.btView.Name = "btView"
        Me.btView.Size = New System.Drawing.Size(100, 30)
        Me.btView.TabIndex = 50
        Me.btView.Text = "View Report"
        Me.btView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btView.UseVisualStyleBackColor = True
        '
        'listProduct
        '
        Me.listProduct.AllowDrop = True
        Me.listProduct.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listProduct.Location = New System.Drawing.Point(3, 48)
        Me.listProduct.Name = "listProduct"
        Me.listProduct.Size = New System.Drawing.Size(877, 318)
        Me.listProduct.TabIndex = 76
        Me.listProduct.UseCompatibleStateImageBehavior = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblNoList)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.butAddExcel)
        Me.Panel1.Controls.Add(Me.listProduct)
        Me.Panel1.Location = New System.Drawing.Point(10, 205)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(880, 364)
        Me.Panel1.TabIndex = 77
        '
        'lblNoList
        '
        Me.lblNoList.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblNoList.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoList.Location = New System.Drawing.Point(0, 1)
        Me.lblNoList.Name = "lblNoList"
        Me.lblNoList.Size = New System.Drawing.Size(984, 366)
        Me.lblNoList.TabIndex = 112
        Me.lblNoList.Text = "List View"
        Me.lblNoList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(347, 246)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(202, 29)
        Me.ProgressBar1.TabIndex = 113
        Me.ProgressBar1.Visible = False
        '
        'butAddExcel
        '
        Me.butAddExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butAddExcel.ForeColor = System.Drawing.SystemColors.Desktop
        Me.butAddExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.butAddExcel.Location = New System.Drawing.Point(755, 10)
        Me.butAddExcel.Name = "butAddExcel"
        Me.butAddExcel.Size = New System.Drawing.Size(122, 29)
        Me.butAddExcel.TabIndex = 80
        Me.butAddExcel.Text = "Export to Excel"
        Me.butAddExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.butAddExcel.UseVisualStyleBackColor = True
        '
        'frmStockReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlReportHead)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmStockReport"
        Me.pnlReportHead.ResumeLayout(False)
        Me.pnlReportHead.PerformLayout()
        CType(Me.picHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlReportHead As System.Windows.Forms.Panel
    Friend WithEvents lblusernam As System.Windows.Forms.Label
    Friend WithEvents lblusername As System.Windows.Forms.Label
    Friend WithEvents lblhead As System.Windows.Forms.Label
    Friend WithEvents picHead As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btView As System.Windows.Forms.Button
    Friend WithEvents listProduct As System.Windows.Forms.ListView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents butAddExcel As System.Windows.Forms.Button
    Friend WithEvents lblNoList As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents cbLocationfrom As System.Windows.Forms.ComboBox
    Friend WithEvents lblLoc As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbsubgrp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbmaingrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbitemfrom As System.Windows.Forms.ComboBox
    Friend WithEvents cmbitemto As System.Windows.Forms.ComboBox
    Friend WithEvents btnCloseForm As System.Windows.Forms.Button
    Friend WithEvents txtmaingrp As System.Windows.Forms.TextBox
    Friend WithEvents txtSubGroup As System.Windows.Forms.TextBox
End Class
