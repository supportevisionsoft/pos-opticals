<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Home
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Home))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.EditMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolsMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.TransactionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MastersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EndOfTheDayReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GiftVoucherReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DailySalesTransactionReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReferralReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RoyaltyReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AccountsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockReportToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(27, 17)
        Me.ToolStripStatusLabel.Text = "POS"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 724)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1028, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'FileMenu
        '
        Me.FileMenu.Image = Global.POS.My.Resources.Resources.clientlogo
        Me.FileMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.FileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
        Me.FileMenu.Name = "FileMenu"
        Me.FileMenu.Size = New System.Drawing.Size(76, 66)
        '
        'EditMenu
        '
        Me.EditMenu.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditMenu.Image = Global.POS.My.Resources.Resources.Home
        Me.EditMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.EditMenu.Name = "EditMenu"
        Me.EditMenu.Size = New System.Drawing.Size(87, 66)
        Me.EditMenu.Text = "       Home     "
        Me.EditMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.EditMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.EditMenu.ToolTipText = "Home"
        '
        'ToolsMenu
        '
        Me.ToolsMenu.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolsMenu.Image = Global.POS.My.Resources.Resources.Customer_ICON
        Me.ToolsMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolsMenu.Name = "ToolsMenu"
        Me.ToolsMenu.Size = New System.Drawing.Size(69, 66)
        Me.ToolsMenu.Text = "  Patient  "
        Me.ToolsMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolsMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolsMenu.ToolTipText = "Customer"
        '
        'TransactionToolStripMenuItem
        '
        Me.TransactionToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TransactionToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Transaction_ICON
        Me.TransactionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TransactionToolStripMenuItem.Name = "TransactionToolStripMenuItem"
        Me.TransactionToolStripMenuItem.Size = New System.Drawing.Size(91, 66)
        Me.TransactionToolStripMenuItem.Text = "Transactions"
        Me.TransactionToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.TransactionToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TransactionToolStripMenuItem.ToolTipText = "Transactions"
        '
        'MastersToolStripMenuItem
        '
        Me.MastersToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MastersToolStripMenuItem.Image = Global.POS.My.Resources.Resources.masters
        Me.MastersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MastersToolStripMenuItem.Name = "MastersToolStripMenuItem"
        Me.MastersToolStripMenuItem.Size = New System.Drawing.Size(65, 66)
        Me.MastersToolStripMenuItem.Text = "Masters"
        Me.MastersToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MastersToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.MastersToolStripMenuItem.ToolTipText = "Inventory"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EndOfTheDayReportToolStripMenuItem, Me.GiftVoucherReportToolStripMenuItem, Me.DailySalesTransactionReportToolStripMenuItem, Me.ReferralReportToolStripMenuItem, Me.RoyaltyReportToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportsToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Reports_ICON
        Me.ReportsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(72, 66)
        Me.ReportsToolStripMenuItem.Text = "  Reports "
        Me.ReportsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ReportsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ReportsToolStripMenuItem.ToolTipText = "Reports"
        '
        'EndOfTheDayReportToolStripMenuItem
        '
        Me.EndOfTheDayReportToolStripMenuItem.Name = "EndOfTheDayReportToolStripMenuItem"
        Me.EndOfTheDayReportToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.EndOfTheDayReportToolStripMenuItem.Text = "End of the Day Report"
        '
        'GiftVoucherReportToolStripMenuItem
        '
        Me.GiftVoucherReportToolStripMenuItem.Name = "GiftVoucherReportToolStripMenuItem"
        Me.GiftVoucherReportToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.GiftVoucherReportToolStripMenuItem.Text = "Gift Voucher Report"
        '
        'DailySalesTransactionReportToolStripMenuItem
        '
        Me.DailySalesTransactionReportToolStripMenuItem.Name = "DailySalesTransactionReportToolStripMenuItem"
        Me.DailySalesTransactionReportToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.DailySalesTransactionReportToolStripMenuItem.Text = "Daily Sales Transaction Report"
        '
        'ReferralReportToolStripMenuItem
        '
        Me.ReferralReportToolStripMenuItem.Name = "ReferralReportToolStripMenuItem"
        Me.ReferralReportToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.ReferralReportToolStripMenuItem.Text = "Referral Report"
        '
        'RoyaltyReportToolStripMenuItem
        '
        Me.RoyaltyReportToolStripMenuItem.Name = "RoyaltyReportToolStripMenuItem"
        Me.RoyaltyReportToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.RoyaltyReportToolStripMenuItem.Text = "Royalty Report"
        '
        'AccountsToolStripMenuItem
        '
        Me.AccountsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StockReportToolStripMenuItem1})
        Me.AccountsToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountsToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Size_48X48
        Me.AccountsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.AccountsToolStripMenuItem.Name = "AccountsToolStripMenuItem"
        Me.AccountsToolStripMenuItem.Size = New System.Drawing.Size(60, 66)
        Me.AccountsToolStripMenuItem.Text = "Query"
        Me.AccountsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.AccountsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'StockReportToolStripMenuItem1
        '
        Me.StockReportToolStripMenuItem1.Name = "StockReportToolStripMenuItem1"
        Me.StockReportToolStripMenuItem1.Size = New System.Drawing.Size(153, 22)
        Me.StockReportToolStripMenuItem1.Text = "Stock Report"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Help_ICON
        Me.HelpToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(70, 66)
        Me.HelpToolStripMenuItem.Text = "     Help    "
        Me.HelpToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.HelpToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.HelpToolStripMenuItem.ToolTipText = "Help"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Exit_ICON
        Me.ExitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(60, 66)
        Me.ExitToolStripMenuItem.Text = "Exit"
        Me.ExitToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ExitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'MenuStrip
        '
        Me.MenuStrip.BackColor = System.Drawing.Color.PowderBlue
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.EditMenu, Me.ToolsMenu, Me.TransactionToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.MastersToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.AccountsToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1028, 70)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingsToolStripMenuItem.Image = Global.POS.My.Resources.Resources.Settings_ICON
        Me.SettingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(65, 66)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        Me.SettingsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.SettingsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.SettingsToolStripMenuItem.ToolTipText = "Masters"
        '
        'Home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "Home"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aljaber - POS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TransactionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MastersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EndOfTheDayReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GiftVoucherReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccountsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DailySalesTransactionReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReferralReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RoyaltyReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockReportToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem

End Class
