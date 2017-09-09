<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdSelection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdSelection))
        Me.adselPanel = New System.Windows.Forms.Panel
        Me.butCampCrtRemove = New System.Windows.Forms.Button
        Me.butCampCrtChoose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'adselPanel
        '
        Me.adselPanel.AutoScroll = True
        Me.adselPanel.Location = New System.Drawing.Point(6, 23)
        Me.adselPanel.Name = "adselPanel"
        Me.adselPanel.Size = New System.Drawing.Size(451, 370)
        Me.adselPanel.TabIndex = 2
        '
        'butCampCrtRemove
        '
        Me.butCampCrtRemove.FlatAppearance.BorderSize = 0
        Me.butCampCrtRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butCampCrtRemove.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCampCrtRemove.Image = CType(resources.GetObject("butCampCrtRemove.Image"), System.Drawing.Image)
        Me.butCampCrtRemove.Location = New System.Drawing.Point(223, 399)
        Me.butCampCrtRemove.Name = "butCampCrtRemove"
        Me.butCampCrtRemove.Size = New System.Drawing.Size(57, 39)
        Me.butCampCrtRemove.TabIndex = 4
        Me.butCampCrtRemove.Text = "Cancel"
        Me.butCampCrtRemove.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.butCampCrtRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.butCampCrtRemove.UseVisualStyleBackColor = True
        '
        'butCampCrtChoose
        '
        Me.butCampCrtChoose.FlatAppearance.BorderSize = 0
        Me.butCampCrtChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.butCampCrtChoose.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCampCrtChoose.Image = CType(resources.GetObject("butCampCrtChoose.Image"), System.Drawing.Image)
        Me.butCampCrtChoose.Location = New System.Drawing.Point(165, 399)
        Me.butCampCrtChoose.Name = "butCampCrtChoose"
        Me.butCampCrtChoose.Size = New System.Drawing.Size(53, 39)
        Me.butCampCrtChoose.TabIndex = 3
        Me.butCampCrtChoose.Text = "Select"
        Me.butCampCrtChoose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.butCampCrtChoose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.butCampCrtChoose.UseVisualStyleBackColor = True
        '
        'AdSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ClientSize = New System.Drawing.Size(462, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.butCampCrtRemove)
        Me.Controls.Add(Me.butCampCrtChoose)
        Me.Controls.Add(Me.adselPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AdSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Logo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents adselPanel As System.Windows.Forms.Panel
    Friend WithEvents butCampCrtRemove As System.Windows.Forms.Button
    Friend WithEvents butCampCrtChoose As System.Windows.Forms.Button
End Class
