Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Data.Odbc
Imports Oracle.DataAccess.Client
Imports System.Math
Imports System.Xml
Imports System.IO


Public Class SlipDrawer


    Inherits System.Windows.Forms.Form

    ''Public qryLog As New QueryLog


    Dim Location_Codes As New List(Of String)
    Dim Transaction_Names As New List(Of String)
    Dim Templates_Names As New List(Of String)
    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_TransactionNames As New AutoCompleteStringCollection()
    Dim MySource_TemplatesNames As New AutoCompleteStringCollection()

    Dim currTemplateName As String = ""
    Dim currTemplateID As String = ""
    Dim db As New DBConnection
    Dim stQuery As String = ""
    Dim ds As DataSet
    Public Control_Values As New Dictionary(Of String, String)
    Public pageList As New List(Of String)
    Private totalDControlSet As New Dictionary(Of String, Dictionary(Of String, List(Of String)))
    Private totalDControlTypeValues As New Dictionary(Of String, List(Of String))
    Private totalDControlProperties As New Dictionary(Of String, Dictionary(Of String, String))

    ' This map is used to store all the run time control values for a printing page
    Private pagecontrolsPropertiesMap As New Dictionary(Of String, Dictionary(Of String, String))
    Private rc As ResizeableControl
    Private btnControls As New List(Of Button)
    Private btnControlsHashMap As New Dictionary(Of String, String)
    Dim dataSourceMap As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String)))
    Dim dragging As Boolean = False
    Dim startX As Integer
    Dim startY As Integer
    Dim lastActiveElement As String = ""
    Dim lblControls As New List(Of Label)
    Dim imgControls As New List(Of PictureBox)
    Dim hlineControls As New List(Of Panel)
    Dim vlineControls As New List(Of Panel)
    Dim pnlControls As New List(Of Panel)
    Dim queryFieldsControls As New List(Of Label)
    Dim tableControls As New List(Of Panel)

    Dim txtPropControls As New List(Of TextBox)
    Dim lblPropControls As New List(Of Label)
    Dim cmbPropControls As New List(Of ComboBox)

    Dim btnHistoryControls As New List(Of Button)

    Dim currentPageType As String = ""

    Private Sub SetResolution()
        Try
            ' set resolution sub checks all the controls on the screen. Containers (tabcontrol, panel, ‘groupbox, tablelayoutpanel) do not resize on general control search for the form – so ‘they have to be done separate by name

            Dim perX, perY As Double, prvheight, prvWidth As Int32
            Dim shoAdd As Short

            Dim desktopSize As Size = Windows.Forms.SystemInformation.PrimaryMonitorSize
            prvheight = desktopSize.Height
            prvWidth = desktopSize.Width
            Dim p_shoWhatSize As Double
            ' in Windows 7, in the display section of the control panel, a user can ‘be set to see their screen larger – the settings are 100%, 125%, and ‘150%. In my programs preferences, I allow my software user to select ‘if they are using the 125% or the 150% settings. I set the global ‘p_shoWhatSize (short) varible to 1 for 125% and 2 for 150% screen. ‘This section ajusts for this

            If p_shoWhatSize = 1 Then
                prvheight = prvheight * 0.8
                prvWidth = prvWidth * 0.8
            End If
            If p_shoWhatSize = 2 Then
                prvheight = prvheight * 0.6666
                prvWidth = prvWidth * 0.6666
            End If

            ' the development resolution for my project is 1024 x 768 – change this ‘to your development resolution
            ' get new 'ratio' for screen
            perX = prvWidth / 1024
            perY = prvheight / 768

            ' listboxes don’t resize vertically correctly for all resolutions due ‘to the font size. shoAdd is used to ‘tweek’ the size of the list ‘boxes to help adjust for this – requires some testing on your screens ‘in different resolutions. I have some set at 10 and some as high as ‘14.
            If prvheight > 768 Then shoAdd = Int((prvheight - 768) / 12)

            Dim shoFont As Short

            ' if res is 1024 x 768 then perX and PerY will equal 1
            If perX <> 1 Or perY <> 1 Then
                For Each ctl As Control In Me.Controls

                    ' if you change the fonts of panels or groupbox containers, it messes
                    ' with the controls in those containers. Therefore, I skip the font 
                    ' resize for these
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next ctl

                For Each ctl As Control In Me.pnlCtlProptiesTabHolder.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next

                For Each ctl As Control In Me.tabDataSources.Controls
                    If UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.PANEL" _
                    And UCase(ctl.GetType.ToString) <> "SYSTEM.WINDOWS.FORMS.GROUPBOX" Then
                        shoFont = ctl.Font.Size * perY
                        ctl.Font = New Font(ctl.Font.FontFamily, shoFont, ctl.Font.Style)
                    End If

                    'get new location
                    ctl.Location = New Point(ctl.Location.X * perX, ctl.Location.Y * perY)

                    If UCase(ctl.GetType.ToString) = "SYSTEM.WINDOWS.FORMS.LISTBOX" Then
                        ctl.Height = ctl.Size.Height * perY + shoAdd
                        ctl.Width = ctl.Size.Width * perX
                    Else
                        ' get new height & width
                        ctl.Height = ctl.Size.Height * perY
                        ctl.Width = ctl.Size.Width * perX
                    End If

                    Application.DoEvents()
                Next

                ' if you are not maximizing your screen afterwards, then include this code
                Me.Top = (prvheight / 2) - (Me.Height / 2)
                Me.Left = (prvWidth / 2) - (Me.Width / 2)
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Private Sub SlipDrawer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        SetResolution()

        loadPageDetails()
        loadControls()
        loadDataSourceListValues()
    End Sub

    Private Sub loadPageDetails()
        Try
            ds = New DataSet
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            stQuery = "Select POS_PAPER_TYPE_NAME,POS_PAPER_TYPE_HEIGHT,POS_PAPER_TYPE_WIDTH,POS_PAPER_TYPES_SYSID from POS_PAPER_TYPES where POS_PAPER_TYPE_FREEZE=2"
            errLog.WriteToErrorLog("POS_PAPER_TYPES query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count

            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                pageList.Add(row.Item(3).ToString)
                cmbPageTypes.Items.Add(row.Item(0).ToString + "  " + row.Item(1).ToString + "x" + row.Item(2).ToString)
                count = count - 1
                i = i + 1
            End While
            If i > 0 Then
                row = ds.Tables("Table").Rows.Item(0)
                cmbPageTypes.Text = row.Item(0).ToString + "  " + row.Item(1).ToString + "x" + row.Item(2).ToString
            End If

            ds = New DataSet
            stQuery = "SELECT POS_PRINT_CONTROL_PROP_SYSID, POS_PRINT_CONTROL_NAME, POS_PRINT_CONTROL_VALUES, POS_PRINT_CONTROL_TYPE, POS_PRINT_CONTROL_READONLY, POS_PRINT_CONTROL_DEFAULT  from POS_PRINT_CONTROL_PROP"
            errLog.WriteToErrorLog("POS_PRINT_CONTROL_PROP query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Dim ctlPropHash As New Dictionary(Of String, String)
                ctlPropHash.Add(row.Item(1).ToString, row.Item(2).ToString)
                totalDControlProperties.Add(row.Item(0).ToString, ctlPropHash)
                Dim lst As New List(Of String)
                lst.Add(row.Item(3).ToString)
                lst.Add(row.Item(4).ToString)
                lst.Add(row.Item(5).ToString)
                totalDControlTypeValues.Add(row.Item(0).ToString, lst)
                'createPropertyControl(row.Item(3).ToString, row.Item(0).ToString, row.Item(4).ToString, row.Item(2).ToString, row.Item(1).ToString)
                i = i + 1
                count = count - 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at Load Page Details", ex.Message, ex.StackTrace)
        End Try
    End Sub

    'Private Sub createPropertyControl(ByVal ctlType As String, ByVal ctlName As String, ByVal ctlReadOnly As String, ByVal ctlValuesString As String, ByVal ctlLabelString As String)
    '    Try
    '        Dim lbl As New Label
    '        Dim n As Integer = lblPropControls.Count
    '        With lbl
    '            .Name = "lblPropControls" & n.ToString
    '            .Text = ctlLabelString
    '            .Location = New Point(0, n * 20)
    '            .Size = New Size(80, 20)
    '            .TextAlign = ContentAlignment.MiddleLeft
    '            .BorderStyle = BorderStyle.FixedSingle
    '            .BackColor = Color.AntiqueWhite
    '        End With
    '        Me.pnlCtlProptiesTabHolder.Controls.Add(lbl)
    '        lblPropControls.Add(lbl)
    '        Select Case ctlType
    '            Case "TEXT"
    '                Dim txt As New TextBox
    '                With txt
    '                    .Name = ctlName
    '                    .Location = New Point(lbl.Width, n * 20)
    '                    .BorderStyle = BorderStyle.FixedSingle
    '                    .Size = New Size(pnlCtlProptiesTabHolder.Width - lbl.Width, 20)
    '                    If ctlReadOnly.Equals("2") Then
    '                        .ReadOnly = True
    '                    End If
    '                End With
    '                txtPropControls.Add(txt)
    '                Me.pnlCtlProptiesTabHolder.Controls.Add(txt)
    '            Case "COMBO"
    '                Dim cmb As New ComboBox
    '                With cmb
    '                    .Name = ctlName
    '                    .Location = New Point(lbl.Width, n * 20)
    '                    .Size = New Size(pnlCtlProptiesTabHolder.Width - lbl.Width, 20)
    '                    .DropDownStyle = ComboBoxStyle.DropDownList

    '                End With
    '                AddHandler cmb.LostFocus, AddressOf whenCtlLosesFocus
    '                cmbPropControls.Add(cmb)
    '                Me.pnlCtlProptiesTabHolder.Controls.Add(cmb)
    '        End Select

    '        'pnlCtlProptiesTabHolder
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error at Creating Property Control", ex.Message, ex.StackTrace)
    '    End Try
    'End Sub

    Private Sub cmbPageTypes_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPageTypes.SelectedValueChanged
        Try
            Dim currentPageSelected As String = DirectCast(sender, ComboBox).Text
            Dim parts As String() = currentPageSelected.Split(New String() {"  "}, StringSplitOptions.None)
            Dim sizeTemp As String() = parts(1).Split(New String() {"x"}, StringSplitOptions.None)
            pnlPaper.Size = New Size(Convert.ToDouble(sizeTemp(0)), Convert.ToDouble(sizeTemp(1)))
            pnlPaper.Visible = True

            Dim pnlMiddleDrawerWidth As Integer = pnlMiddleDrawer.Width
            Dim tempX As Integer = (pnlMiddleDrawerWidth - pnlPaper.Width) / 2
            pnlPaper.Location = New Point(tempX, pnlPaper.Location.Y)
            'Me.pnlMiddleDrawer.Controls.Add(pnlPaper)

            ds = New DataSet
            stQuery = "select POS_PAPER_TYPES_SYSID from POS_PAPER_TYPES where POS_PAPER_TYPE_NAME = '" & parts(0) & "'"
            errLog.WriteToErrorLog("POS_PRINT_CONTROLS query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If (ds.Tables("Table").Rows.Count > 0) Then
                currentPageType = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at cmbPageTypes SelectedValueChanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub loadControls()
        Try
            ds = New DataSet
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            stQuery = "select POS_PRINT_CONTROLS_SYSID,POS_PRINT_CONTROLS_NAME,POS_PRINT_CONTROLS_DESC from pos_print_controls"
            errLog.WriteToErrorLog("POS_PRINT_CONTROLS query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Control_Values.Add(row.Item(0).ToString, row.Item(1).ToString)
                Dim btn As Button
                Dim n As Integer
                n = btnControls.Count + 1
                btn = New Button
                Dim caseValue As String = row.Item(1).ToString
                With btn
                    .Name = row.Item(1).ToString + "____" + row.Item(0).ToString
                    .Text = row.Item(2).ToString
                    .Height = 35
                    .FlatAppearance.MouseDownBackColor = Color.LightSeaGreen
                    .FlatAppearance.MouseOverBackColor = Color.Aquamarine
                    .Font = New Font(btn.Font, FontStyle.Bold)
                    btnControlsHashMap.Add(caseValue, row.Item(0).ToString)
                    Select Case caseValue
                        Case "Table"
                            .Image = My.Resources.Table
                        Case "Image"
                            .Image = My.Resources.Image
                        Case "VLine"
                            .Image = My.Resources.VLine
                        Case "HLine"
                            .Image = My.Resources.HLine
                        Case "Panel"
                            .Image = My.Resources.Panel
                        Case "Label"
                            .Image = My.Resources.Label
                        Case "QueryField"
                            .Image = My.Resources.QueryField
                    End Select

                    .ImageAlign = ContentAlignment.MiddleLeft
                    .TextAlign = ContentAlignment.MiddleCenter
                    .TextImageRelation = TextImageRelation.ImageBeforeText
                    .Dock = DockStyle.Top
                    .FlatStyle = FlatStyle.Flat
                End With
                AddHandler btn.Click, AddressOf Me.btnControl_Click
                Me.btnControls.Add(btn)
                Me.pnlLeftControls.Controls.Add(btn)
                count = count - 1
                i = i + 1
            End While

            Dim pgid As String
            For Each pgid In pageList
                ds = New DataSet
                count = 0
                i = 0
                stQuery = "select POS_PRINT_ENUM_CTRL_ID,POS_PRINT_ENUM_PROP_ID from pos_print_enum where POS_PRINT_ENUM_PAGETYPE_ID=" & pgid
                'errLog.WriteToErrorLog("POS_PRINT_CONTROLS query", stQuery, "")
                ds = db.SelectFromTableODBC(stQuery)
                count = ds.Tables("Table").Rows.Count
                Dim ctlPropHash As New Dictionary(Of String, List(Of String))
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    If ctlPropHash.ContainsKey(row.Item(0).ToString) Then
                        Dim tempList As List(Of String) = ctlPropHash(row.Item(0).ToString)
                        tempList.Add(row.Item(1).ToString)
                    Else
                        Dim tempList As New List(Of String)
                        tempList.Add(row.Item(1).ToString)
                        ctlPropHash.Add(row.Item(0).ToString, tempList)
                    End If
                    count = count - 1
                    i = i + 1
                End While
                totalDControlSet.Add(pgid, ctlPropHash)
            Next


        Catch ex As Exception
            errLog.WriteToErrorLog("Error at Load Page Details", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not pnlPaper.Visible Then
                MsgBox("Please Select Paper Type!")
                Exit Sub
            End If
            Dim ctlName As String = DirectCast(sender, Button).Text
            Dim n As Integer
            Select Case ctlName
                Case "Label"
                    Dim lbl As New Label
                    n = lblControls.Count + 1
                    With lbl
                        .Name = "Label__" & n
                        .BorderStyle = BorderStyle.None
                        .BackColor = Color.Transparent
                        .Text = "Label" & n
                        .Location = New Point(pnlPaper.Location.X + 40, pnlPaper.Location.Y + 10)
                        .Size = New Size(50, 20)
                        .BringToFront()
                        .AutoSize = False
                        .TextAlign = ContentAlignment.MiddleLeft
                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") And Not pairVal.Key.Equals("13") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler lbl.MouseDown, AddressOf startDrag
                    AddHandler lbl.MouseMove, AddressOf whileDragging
                    AddHandler lbl.MouseUp, AddressOf endDrag
                    AddHandler lbl.MouseClick, AddressOf whiledelete

                    'AddHandler lbl.LostFocus, AddressOf whenCtlLosesFocus
                    'Dim ctlID As String = btnControlsHashMap(ctlName)


                    lblControls.Add(lbl)
                    loadProperties(lbl)
                    Me.pnlPaper.Controls.Add(lbl)
                    startDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "Label__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    btnHistoryControls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement

                Case "Image Box"
                    Dim img As New PictureBox
                    n = imgControls.Count + 1
                    With img
                        .Name = "Image__" & n
                        .BorderStyle = BorderStyle.FixedSingle
                        .BackColor = Me.pnlPaper.BackColor
                        .Location = New Point(pnlPaper.Location.X + 10, pnlPaper.Location.Y + 10)
                        .Size = New Size(100, 50)
                        .BringToFront()
                        .SizeMode = PictureBoxSizeMode.Zoom

                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler img.MouseDown, AddressOf startDrag
                    ''Commented to handle resize drag issue
                    ''AddHandler img.MouseMove, AddressOf whileDragging
                    AddHandler img.MouseUp, AddressOf endDrag
                    AddHandler img.MouseEnter, AddressOf mouseScalingEnter
                    AddHandler img.MouseWheel, AddressOf whileScaling
                    AddHandler img.MouseClick, AddressOf RightOptions
                    '' AddHandler img.MouseLeave, AddressOf whenlostfocus


                    imgControls.Add(img)
                    loadProperties(img)
                    Me.pnlPaper.Controls.Add(img)
                    startDrag(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    ''whileDragging(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "Image__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    btnHistoryControls.Add(btn)

                    AddHandler btn.Click, AddressOf FocusElement
                    rc = New ResizeableControl(img)

                Case "Horizontal Line"
                    Dim pnl As New Panel
                    n = hlineControls.Count + 1
                    With pnl
                        .Name = "HLine__" & n
                        .BorderStyle = BorderStyle.FixedSingle
                        .BackColor = Color.Black
                        .Location = New Point(pnlPaper.Location.X + 40, pnlPaper.Location.Y + 10)
                        .Size = New Size(100, 3)
                        .BringToFront()
                        .Cursor = Cursors.SizeAll
                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler pnl.MouseDown, AddressOf startDrag
                    AddHandler pnl.MouseMove, AddressOf whileDragging
                    AddHandler pnl.MouseUp, AddressOf endDrag
                    AddHandler pnl.MouseHover, AddressOf callMouseHover
                    AddHandler pnl.MouseLeave, AddressOf callMouseLeave
                    AddHandler pnl.MouseClick, AddressOf whiledelete
                    '' AddHandler pnl.MouseLeave, AddressOf whenlostfocus

                    loadProperties(pnl)
                    hlineControls.Add(pnl)
                    Me.pnlPaper.Controls.Add(pnl)
                    startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "HLine__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    btnHistoryControls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement
                Case "Vertical Line"
                    Dim pnl As New Panel
                    n = vlineControls.Count + 1
                    With pnl
                        .Name = "VLine__" & n
                        .BorderStyle = BorderStyle.FixedSingle
                        .BackColor = Color.Black
                        .Location = New Point(pnlPaper.Location.X + 70, pnlPaper.Location.Y + 10)
                        .Size = New Size(3, 100)
                        .BringToFront()
                        .Cursor = Cursors.SizeAll
                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler pnl.MouseDown, AddressOf startDrag
                    AddHandler pnl.MouseMove, AddressOf whileDragging
                    AddHandler pnl.MouseUp, AddressOf endDrag
                    AddHandler pnl.MouseHover, AddressOf callMouseHover
                    AddHandler pnl.MouseLeave, AddressOf callMouseLeave
                    AddHandler pnl.MouseClick, AddressOf whiledelete
                    'AddHandler pnl.LostFocus, AddressOf whenCtlLosesFocus
                    loadProperties(pnl)
                    vlineControls.Add(pnl)
                    Me.pnlPaper.Controls.Add(pnl)
                    startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "VLine__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement

                Case "Panel Control"
                    Dim pnl As New Panel
                    n = pnlControls.Count + 1
                    With pnl
                        .Name = "Panel__" & n
                        .BorderStyle = BorderStyle.FixedSingle
                        .BackColor = Color.White
                        .Location = New Point(pnlPaper.Location.X + 10, pnlPaper.Location.Y + 10)
                        .Size = New Size(200, 50)
                        .BringToFront()
                        .Cursor = Cursors.SizeAll
                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler pnl.MouseDown, AddressOf startDrag
                    AddHandler pnl.MouseMove, AddressOf whileDragging
                    AddHandler pnl.MouseUp, AddressOf endDrag
                    AddHandler pnl.MouseClick, AddressOf whiledelete
                    ''AddHandler pnl.MouseClick, AddressOf RightOptions
                    'AddHandler pnl.MouseDoubleClick, AddressOf whenCtlLosesFocus
                    loadProperties(pnl)

                    pnlControls.Add(pnl)
                    Me.pnlPaper.Controls.Add(pnl)
                    startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "Panel__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement

                Case "SQL Query Field"
                    Dim lbl As New Label
                    n = queryFieldsControls.Count + 1
                    With lbl
                        .Name = "QueryField__" & n
                        .BorderStyle = BorderStyle.None
                        .BackColor = Color.Transparent
                        .Text = "QueryField" & n
                        .Location = New Point(pnlPaper.Location.X + 40, pnlPaper.Location.Y + 10)
                        .Size = New Size(100, 20)
                        .BringToFront()
                        .AutoSize = False
                    End With
                    AddHandler lbl.MouseDown, AddressOf startDrag
                    AddHandler lbl.MouseMove, AddressOf whileDragging
                    AddHandler lbl.MouseUp, AddressOf endDrag
                    AddHandler lbl.MouseClick, AddressOf whiledelete
                    'AddHandler lbl.LostFocus, AddressOf whenCtlLosesFocus
                    'Dim ctlID As String = btnControlsHashMap(ctlName)

                    loadProperties(lbl)
                    queryFieldsControls.Add(lbl)
                    Me.pnlPaper.Controls.Add(lbl)
                    startDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "QueryField__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke

                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement

                Case "Table"
                    Dim pnl As New Panel
                    n = tableControls.Count + 1
                    With pnl
                        .Name = "Table__" & n
                        .BorderStyle = BorderStyle.FixedSingle
                        .BackColor = Color.White
                        .Location = New Point(20, 20)
                        .Size = New Size(pnlPaper.Width - 40, 100)
                        .BringToFront()
                        .Cursor = Cursors.SizeAll
                    End With
                    'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    'For Each pairVal In totalDControlProperties
                    '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    '    If Not pairVal.Key.Equals("1") Then
                    '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                    '    End If
                    'Next
                    AddHandler pnl.MouseDown, AddressOf startDrag
                    AddHandler pnl.MouseMove, AddressOf whileDragging
                    AddHandler pnl.MouseUp, AddressOf endDrag
                    AddHandler pnl.MouseClick, AddressOf whiledelete
                    'AddHandler pnl.MouseDoubleClick, AddressOf whenCtlLosesFocus
                    loadProperties(pnl)

                    tableControls.Add(pnl)
                    Me.pnlPaper.Controls.Add(pnl)
                    startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                    endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                    Dim btn As New Button
                    With btn
                        .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                        .Dock = DockStyle.Top
                        .Text = "Table__" & n
                        .FlatStyle = FlatStyle.Flat
                        .BackColor = Color.WhiteSmoke
                        .FlatAppearance.BorderSize = 1
                    End With
                    Me.pnlHistoryControls.Controls.Add(btn)
                    AddHandler btn.Click, AddressOf FocusElement
            End Select

            If pnlPaper.Controls.Count > 0 Then
                pnlCtlProptiesTabHolder.Enabled = True
            Else
                pnlCtlProptiesTabHolder.Enabled = False
            End If

        Catch ex As Exception
            errLog.WriteToErrorLog("Error at Load Page Details", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub callMouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As Control = sender
        ctl.Cursor = Cursors.SizeAll
    End Sub

    Private Sub callMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctl As Control = sender
        ctl.Cursor = Cursors.Default
    End Sub
    Private Sub RightOptions(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        loadProperties(sender)
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show()
            ContextMenuStrip1.Location = Cursor.Position
        End If
    End Sub
    Private Sub startDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        dragging = True
        startX = e.X
        startY = e.Y
        Dim ctl As Control = sender
        ctl.Cursor = Cursors.SizeAll
        'lastActiveElement = ctl.Name
        ctl.BringToFront()
    End Sub
    Private Sub whileDragging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If dragging = True Then
            sender.Location = New Point(sender.Location.X + e.X - startX, sender.Location.Y + e.Y - startY)

            Me.Refresh()
            'Me.Controls.Find("ctl11", True)(0).Text = sender.Location.X
            'Me.Controls.Find("ctl12", True)(0).Text = sender.Location.Y
        End If
    End Sub

    Private Sub FocusElement(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ctrl As Control = sender
            Dim ctrlname = ctrl.Text
            For Each ctl As Control In pnlPaper.Controls
                If ctl.Name = ctrlname Then
                    ctl.Focus()
                    loadProperties(ctl)
                End If
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at FocusElement Properties for Controls", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub endDrag(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dragging = False
        'My.Settings.controlLocations.Clear()
        'For Each Control As Control In Me.Controls
        'My.Settings.controlLocations.Add(Control.Name & "!" & Control.Location.X & "!" & Control.Location.Y)
        'Next

        Dim ctl As Control = sender
        Dim x As Integer = ctl.Location.X
        Dim y As Integer = ctl.Location.Y
        ctl.Cursor = Cursors.Default
        Me.Controls.Find("ctl11", True)(0).Text = x
        Me.Controls.Find("ctl12", True)(0).Text = y
        My.Settings.Save()
        loadProperties(ctl)
        'lastActiveElement = DirectCast(sender, Control).Name
        'If x > 26 AndAlso x < 145 AndAlso y > 419 AndAlso y < 555 Then
        '    'MessageBox.Show("Inside")
        '    Me.Controls.Remove(ctl)
        'End If
    End Sub

    Private Sub mouseScalingEnter(sender As System.Object, e As System.EventArgs)
        Dim ctl As Control = sender
        ctl.Focus()
        ctl.Cursor = Cursors.PanSE
    End Sub

    Private Sub whileScaling(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Dim ctl As Control = sender
        'Dim _originalSize As Size = ctl.Size

        'Dim _scaleDelta As Double = Math.Sqrt(ctl.Width * ctl.Height) * 0.00005
        'Dim _scale As Double
        'If e.Delta < 0 Then
        '    _scale -= _scaleDelta
        'ElseIf e.Delta > 0 Then
        '    _scale += _scaleDelta
        'End If

        'ctl.Size = New Size(CInt(Math.Round(_originalSize.Width * _scale)), _
        '                    CInt(Math.Round(_originalSize.Height * _scale)))

    End Sub

    Public Sub whiledelete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim ctl As Control = sender
        ctl.Focus()
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim cms = New ContextMenuStrip
            Dim item1 = cms.Items.Add("Remove")
            item1.Tag = 1
            ''cms.Show(pnlPaper, New Point(ctl.Location.X + (ctl.Width / 2), ctl.Location.Y - (ctl.Height / 2)))
            AddHandler item1.Click, AddressOf Deletefunc
            cms.Show()
            cms.Location = New Point(Cursor.Position)

        ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
            loadProperties(ctl)
        End If
    End Sub
    Private Sub Deletefunc(ByVal sender As System.Object, ByVal e As EventArgs)
        Try

            If (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Label")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label)
                ''pnlHistoryControls.Controls.Remove(CTRL)
                CTRL.Visible = False
                ctl15.Text = False
                ''CODE TO FIND THECORRESPONDING BUTTON OF EACH CONTROL IN PNLHISTORYCONTROLS
                ' Dim activebtn As Control
                'activebtn = DirectCast(pnlHistoryControls.Controls.Find(CTRL.Name, True)(0), Button)

                'MsgBox(activebtn.Name)
                'pnlHistoryControls.Controls.Remove(activebtn)

            ElseIf (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Panel")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Panel)
                CTRL.Visible = False
                ctl15.Text = False
            ElseIf (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("HLine")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Panel)
                CTRL.Visible = False
                ctl15.Text = False
            ElseIf (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("VLine")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Panel)
                CTRL.Visible = False
                ctl15.Text = False
            ElseIf (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Table")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Panel)
                CTRL.Visible = False
            ElseIf (Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Query")) Then
                Dim CTRL As Control = DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label)
                CTRL.Visible = False
            End If
            loadProperties(ActiveControl)
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at FocusElement Properties for Controls", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Public Sub loadProperties(ByVal ctl As Control)
        Try
            If lastActiveElement.Equals("") Then
                'MsgBox("first control added")
                lastActiveElement = ctl.Name
                Me.Controls.Find("ctl1", True)(0).Text = ctl.Name
                Me.Controls.Find("ctl13", True)(0).Text = ctl.Text
                Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                For Each pairVal In totalDControlProperties
                    Me.Controls.Find("ctl" & pairVal.Key, True)(0).Enabled = False
                    Dim lstVal As List(Of String) = totalDControlTypeValues(pairVal.Key)
                    If Not pairVal.Key.Equals("1") And Not pairVal.Key.Equals("13") Then
                        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lstVal(2)
                    End If
                Next

            ElseIf Not lastActiveElement.Equals(ctl.Name) Then

                Dim tmpDic As New Dictionary(Of String, String)

                Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                For Each pairVal In totalDControlProperties
                    Me.Controls.Find("ctl" & pairVal.Key, True)(0).Enabled = False

                    tmpDic.Add(pairVal.Key, Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text)
                Next
                If Not pagecontrolsPropertiesMap.ContainsKey(lastActiveElement) Then
                    pagecontrolsPropertiesMap.Add(lastActiveElement, tmpDic)
                Else
                    pagecontrolsPropertiesMap(lastActiveElement) = tmpDic
                    'pagecontrolsPropertiesMap.Add(lastActiveElement, tmpDic)
                End If


                lastActiveElement = ctl.Name
                Me.Controls.Find("ctl1", True)(0).Text = ctl.Name
            Else
                Dim tmpDic As New Dictionary(Of String, String)

                Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                For Each pairVal In totalDControlProperties
                    Me.Controls.Find("ctl" & pairVal.Key, True)(0).Enabled = False

                    tmpDic.Add(pairVal.Key, Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text)
                Next
                If Not pagecontrolsPropertiesMap.ContainsKey(lastActiveElement) Then
                    pagecontrolsPropertiesMap.Add(lastActiveElement, tmpDic)
                Else
                    pagecontrolsPropertiesMap(lastActiveElement) = tmpDic

                    'pagecontrolsPropertiesMap.Add(lastActiveElement, tmpDic)
                End If
            End If
            Dim ctlName As String = ctl.Name.Split(New String() {"__"}, StringSplitOptions.None)(0)
            Dim ctlID As String = btnControlsHashMap(ctlName)

            Dim pgDic As Dictionary(Of String, List(Of String)) = totalDControlSet(currentPageType)
            Dim lst As List(Of String) = pgDic(ctlID)
            'Dim lstType As List(Of String) = totalDControlTypeValues(ctlID)

            Dim propID As String
            For Each pgipropID In lst
                'Dim currProp As Dictionary(Of String, String) = totalDControlProperties(pgipropID)
                'Dim pair As KeyValuePair(Of String, String)
                'For Each pair In currProp
                Me.Controls.Find("ctl" & pgipropID, True)(0).Enabled = True
                'MsgBox(pair.Key & "   " & pair.Value)
                'Next
            Next

            If pagecontrolsPropertiesMap.ContainsKey(ctl.Name) Then
                Dim tempDic As Dictionary(Of String, String) = pagecontrolsPropertiesMap(ctl.Name)
                Dim pair As KeyValuePair(Of String, String)

                For Each pair In tempDic
                    If pair.Key.ToString.Contains("ctl") Then
                        If pair.Key.Equals("16") And ctl.Name.Contains("Label") Then
                            Me.Controls.Find(pair.Key, True)(0).Text = " none "
                        Else
                            Me.Controls.Find(pair.Key, True)(0).Text = pair.Value
                        End If
                    End If
                    If Me.Controls.Find("ctl" & pair.Key, True).Length > 0 Then
                        If pair.Key.Equals("16") And ctl.Name.Contains("Label") Then
                            Me.Controls.Find("ctl" & pair.Key, True)(0).Text = " none "
                        Else
                            Me.Controls.Find("ctl" & pair.Key, True)(0).Text = pair.Value
                        End If
                    End If
                Next
            Else
                Me.Controls.Find("ctl9", True)(0).Text = ctl.Height
                Me.Controls.Find("ctl10", True)(0).Text = ctl.Width
                Me.Controls.Find("ctl15", True)(0).Text = ctl.Visible
                '' Me.Controls.Find("ctl2", True)(0).Text = ctl.Bordersty
                'Dim FontFamily1 = New FontFamily("Arial")


                '' Me.Controls.Find("ctl3", True)(0).Text = FontFamily

                Me.Controls.Find("ctl4", True)(0).Text = ctl.Font.Size
                Me.Controls.Find("ctl5", True)(0).Text = FontStyle.Bold
                Me.Controls.Find("ctl6", True)(0).Text = FontStyle.Italic
                Me.Controls.Find("ctl7", True)(0).Text = FontStyle.Strikeout
                Me.Controls.Find("ctl8", True)(0).Text = FontStyle.Underline

                Me.Controls.Find("ctl13", True)(0).Text = ""
                ''Me.Controls.Find("ctl4", True)(0).Text = ctl.Text.TextAlign
                If ctlName.Equals("Label") Then
                    Me.Controls.Find("ctl13", True)(0).Text = ctl.Text

                End If
            End If
            Me.Controls.Find("ctl1", True)(0).Text = ctl.Name

        Catch ex As Exception
            errLog.WriteToErrorLog("Error at Loading Properties for Controls", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ctl11.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl11_Leave(sender As Object, e As EventArgs) Handles ctl11.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                'tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl11_TextChanged(sender As Object, e As EventArgs) Handles ctl11.TextChanged
        Try

            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim x As String = tbx.Text.ToString
            Dim y As String = Me.Controls.Find("ctl12", True)(0).Text.ToString
            If x.Trim.Equals("") Then
                x = "0"
            ElseIf y.Trim.Equals("") Then
                y = "0"
            End If
            If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)
                ''ctl.Location = New Point(Convert.ToInt64(x), Convert.ToInt64(y))
                ''ctl.Location = New Point(Integer.Parse(x), Integer.Parse(y))
                ctl.Location = New Point(Convert.ToInt64(x), Convert.ToInt64(y))
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl11_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ctl12.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl12_Leave(sender As Object, e As EventArgs) Handles ctl12.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                'tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl12_TextChanged(sender As Object, e As EventArgs) Handles ctl12.TextChanged
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim x As String = Me.Controls.Find("ctl11", True)(0).Text.ToString
            Dim y As String = tbx.Text.ToString
            If x.Trim.Equals("") Then
                x = "0"
            ElseIf y.Trim.Equals("") Then
                y = "0"
            End If
            If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)
                ctl.Location = New Point(Integer.Parse(x), Integer.Parse(y))
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl12_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl13_TextChanged(sender As Object, e As EventArgs) Handles ctl13.TextChanged
        Try
            If Not lastActiveElement.Equals("") Then
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Me.Controls.Find(lastActiveElement, True)(0).Text = Me.Controls.Find("ctl13", True)(0).Text
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl13_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ctl9.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl9_Leave(sender As Object, e As EventArgs) Handles ctl9.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                'tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl9_TextChanged(sender As Object, e As EventArgs) Handles ctl9.TextChanged
        Try

            Dim h As String = DirectCast(sender, TextBox).Text.ToString
            If h.Trim.Equals("") Then
                h = 0
            End If
            If Not lastActiveElement.Equals("") Then
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Me.Controls.Find(lastActiveElement, True)(0).Height = h

                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl9_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub








    Private Sub ctl10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ctl10.KeyPress
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                e.Handled = True
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl10_Leave(sender As Object, e As EventArgs) Handles ctl10.Leave
        Try
            Dim tbx As System.Windows.Forms.TextBox = sender
            Dim value As Double
            If tbx.Text = "" Then
                tbx.Text = 1
                Return
            ElseIf tbx.Text = "0" Then
                'tbx.Text = 1
                Return
            End If
            If Not Double.TryParse(tbx.Text, value) Then
                tbx.Text = 0
            ElseIf value > 0 Then
                tbx.Text = Round(value, 0)
            Else
                tbx.Text = 1
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub ctl10_SizeChanged(sender As Object, e As EventArgs) Handles ctl10.SizeChanged

    End Sub



    Private Sub ctl10_TextChanged(sender As Object, e As EventArgs) Handles ctl10.TextChanged
        Try
            Dim w As String = DirectCast(sender, TextBox).Text.ToString
            If w.Trim.Equals("") Then
                w = 0
            End If
            If Not lastActiveElement.Equals("") Then
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Me.Controls.Find(lastActiveElement, True)(0).Width = w
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl10_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub



    Private Sub ctl5_TextChanged(sender As Object, e As EventArgs) Handles ctl5.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Dim fntCase As String = ""
                    If ctl5.Text = "True" Then
                        fntCase = "1"
                    Else
                        fntCase = "0"
                    End If

                    If ctl6.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl7.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl8.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    Select Case fntCase
                        Case "0000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Regular)
                        Case "0001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Underline)
                        Case "0010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout)
                        Case "0011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout Or FontStyle.Underline)
                        Case "0100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic)
                        Case "0101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Underline)
                        Case "0110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout)
                        Case "0111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold)
                        Case "1001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Underline)
                        Case "1010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout)
                        Case "1011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic)
                        Case "1101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                        Case "1110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                        Case "1111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                    End Select
                End If


                'Dim fntbld As Boolean = DirectCast(sender, ComboBox).Text.ToString
                'If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                '    If fntbld.Equals(True) Then
                '        Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold)
                '    End If
                '    If fntbld.Equals(False) Then
                '        Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Regular)
                '    End If
                'End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl5_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try

            pnlNewTemplateAdd.Visible = True
            pnlNewTemplateAdd.BringToFront()
            txtTemplateNew.Text = "Template" & DateTime.Now.ToString("MMdyyyyHHmms")
            Dim ctl As Control = sender
            ctl.Enabled = False
            btnOpen.Enabled = False
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnNew_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub


    Private Sub btnCancelNewTemplate_Click(sender As Object, e As EventArgs) Handles btnCancelNewTemplate.Click
        btnNew.Enabled = True
        btnOpen.Enabled = True
        pnlNewTemplateAdd.Visible = False
        pnlNewTemplateAdd.SendToBack()
    End Sub

    Private Sub btnNewTemplateSave_Click(sender As Object, e As EventArgs) Handles btnNewTemplateSave.Click
        Try
            ds = New DataSet
            stQuery = "select POS_PRINT_TEMPLATE_NAME from pos_print_template where POS_PRINT_TEMPLATE_NAME='" & txtTemplateNew.Text & "'"
            errLog.WriteToErrorLog("POS_PRINT_TEMPLATE_NAME select query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                MsgBox("Template '" & txtTemplateNew.Text & "' already exists!!")
                Exit Sub
            End If
            'Dim parts As String() = cmbPageTypes.Text.Split(New String() {"  "}, StringSplitOptions.None)
            stQuery = "insert into pos_print_template (POS_PRINT_TEMPLATE_SYSID,POS_PRINT_TEMPLATE_NAME,POS_PRINT_TEMPLATE_DESC,POS_PRINT_TEMPLATE_PAGE_TYPE,POS_PRINT_TEMPLATE_FREEZE,POS_PRINT_TEMPLATE_CRUID,POS_PRINT_TEMPLATE_CRDT) values ("
            stQuery = stQuery & "pos_print_template_seq.nextval,'" & txtTemplateNew.Text & "','" & txtTemplateDesc.Text & "',1,1,'" & LogonUser & "',sysdate)"
            errLog.WriteToErrorLog("Query pos_print_template insert", stQuery, "")
            db.SaveToTableODBC(stQuery)

            ds = New DataSet
            stQuery = "select POS_PRINT_TEMPLATE_SYSID from pos_print_template where POS_PRINT_TEMPLATE_NAME='" & txtTemplateNew.Text & "'"
            errLog.WriteToErrorLog("POS_PRINT_TEMPLATE_NAME select ID query", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                currTemplateID = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            End If
            'MsgBox(currTemplateID)
            currTemplateName = txtTemplateNew.Text
            pnlNewTemplateAdd.Visible = False
            pnlNewTemplateAdd.SendToBack()
            pnlPaper.Enabled = True
            pnlLeftToolBox.Enabled = True
            btnSave.Enabled = True
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnNewTemplateSave_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            loadProperties(Me.Controls.Find(lastActiveElement, True)(0))
            Using connection As New OracleConnection(db.GetConnectionString(""))
                Dim command As New OracleCommand
                'Dim command As New OdbcCommand()
                Dim transaction As OracleTransaction
                'Dim transaction As OdbcTransaction
                command.Connection = connection
                Try
                    connection.Open()
                    transaction = connection.BeginTransaction()

                    command.Connection = connection
                    command.Transaction = transaction

                    ds = New DataSet
                    'stQuery = "UPDATE pos_print_template SET POS_PRINT_TEMPLATE_PAGE_TYPE = '" & cmbPageTypes.Text.Split("  ")(0).ToString & "' WHERE POS_PRINT_TEMPLATE_SYSID= " & currTemplateID
                    stQuery = "UPDATE pos_print_template SET POS_PRINT_TEMPLATE_PAGE_TYPE = 1 WHERE POS_PRINT_TEMPLATE_SYSID= " & currTemplateID
                    errLog.WriteToErrorLog("update pos_print_template", stQuery, "")
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()


                    stQuery = "delete from POS_PRINT_DATASRC where POS_PRINT_DATASRC_TEMPID=" & currTemplateID
                    errLog.WriteToErrorLog("delete POS_PRINT_DATASRC", stQuery, "")
                    command.CommandText = stQuery
                    command.ExecuteNonQuery()

                    Dim valueselect As String = cmbDataSourceLink.Text
                    If valueselect.Length > 0 And dataSourceMap.ContainsKey(valueselect) Then
                        Dim tmpDicData As Dictionary(Of String, String) = dataSourceMap(valueselect)("columns")
                        Dim strSqlQuery As String = dataSourceMap(valueselect)("sqlquery")("sqlquery")
                        Dim strFilterValue As String = dataSourceMap(valueselect)("filtervalue")("filternumber")
                        For Each Item In lstboxDataSource.CheckedItems
                            stQuery = "insert into POS_PRINT_DATASRC (POS_PRINT_DATASRC_SYSID,POS_PRINT_DATASRC_TEMPID,POS_PRINT_DATASRC_TEMPNAME,POS_PRINT_DATASRC_SRCNAME,POS_PRINT_DATASRC_ALSNAME,POS_PRINT_DATASRC_COLNAME,POS_PRINT_DATASRC_SQLQRYNAME,POS_PRINT_DATASRC_FLTRNAME) values ("
                            stQuery = stQuery & "pos_print_datasrc_seq.nextval," & currTemplateID & ",'" & currTemplateName & "','" & valueselect & "','" & Item.ToString & "','" & tmpDicData(Item.ToString).Replace("'", "''") & "','" & strSqlQuery.Replace("'", "''") & "','" & strFilterValue.Replace("'", "''") & "')"
                            errLog.WriteToErrorLog("insert  POS_PRINT_DATASRC", stQuery, "")
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()
                        Next

                    End If

                    Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                    For Each pairVal In pagecontrolsPropertiesMap

                        ds = New DataSet
                        stQuery = "select POS_PRINT_TEMPLATE_CTL_SYSID from POS_PRINT_TEMPLATE_CONTROLS where POS_PRINT_TEMPLATE_CTL_TEMPID=" & currTemplateID & " and POS_PRINT_TEMPLATE_CTL_NAME='" & pairVal.Key & "'"
                        ds = db.SelectFromTableODBC(stQuery)
                        errLog.WriteToErrorLog("SELECT POS_PRINT_TEMPLATE_CTRL_SYSID  POS_PRINT_DATASRC", stQuery, "")
                        If ds.Tables("Table").Rows.Count > 0 Then
                            Dim tmpDic As Dictionary(Of String, String) = pairVal.Value
                            For Each pair In tmpDic
                                stQuery = "update POS_PRINT_TEMPLATE_PROP set POS_PRINT_TEMPLATE_PROP_VAL='" & pair.Value & "' where POS_PRINT_TEMPLATE_PROP_CTLID=" & ds.Tables("Table").Rows.Item(0).Item(0).ToString & " and POS_PRINT_TEMPLATE_PROP_ID=" & (pair.Key).ToString.Replace("ctl", "")
                                errLog.WriteToErrorLog("update query POS_PRINT_TEMPLATE_PROP", stQuery, "")
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            Next
                        Else
                            ds = New DataSet
                            Dim tmpCTLID As String = ""
                            stQuery = "select pos_print_template_ctl_seq.nextval from dual"
                            ds = db.SelectFromTableODBC(stQuery)
                            If ds.Tables("Table").Rows.Count > 0 Then
                                tmpCTLID = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                            End If

                            stQuery = "insert into POS_PRINT_TEMPLATE_CONTROLS (POS_PRINT_TEMPLATE_CTL_SYSID,POS_PRINT_TEMPLATE_CTL_TEMPID,POS_PRINT_TEMPLATE_CTL_NAME,POS_PRINT_TEMPLATE_CTL_CRUID,POS_PRINT_TEMPLATE_CTL_CRDT) values("
                            stQuery = stQuery & tmpCTLID & "," & currTemplateID & ",'" & pairVal.Key & "','" & LogonUser & "',sysdate)"
                            errLog.WriteToErrorLog("Insert query POS_PRINT_TEMPLATE_CONTROLS", stQuery, "")
                            command.CommandText = stQuery
                            command.ExecuteNonQuery()

                            Dim tmpDic As Dictionary(Of String, String) = pairVal.Value
                            For Each pair In tmpDic
                                stQuery = "insert into POS_PRINT_TEMPLATE_PROP (POS_PRINT_TEMPLATE_PROP_SYSID,POS_PRINT_TEMPLATE_PROP_CTLID,POS_PRINT_TEMPLATE_PROP_ID,POS_PRINT_TEMPLATE_PROP_VAL) values("
                                stQuery = stQuery & "pos_print_template_prop_seq.nextval," & tmpCTLID & ",'" & pair.Key & "','" & pair.Value.Replace("'", "''") & "')"
                                errLog.WriteToErrorLog("Insert query POS_PRINT_TEMPLATE_PROP", stQuery, "")
                                command.CommandText = stQuery
                                command.ExecuteNonQuery()
                            Next
                        End If
                    Next

                    transaction.Commit()
                    MsgBox("Template '" & currTemplateName & "' saved successfully!")
                Catch ex As Exception
                    errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
                    transaction.Rollback()
                    MsgBox("Error occured while saving the template!")
                End Try
            End Using
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnSave_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Public Function loadDataSourceListValues() As Boolean
        Try
            cmbDataSourceLink.Items.Clear()
            Dim doc As New XmlDocument()
            doc.Load("dataSource.xml")
            Dim elemList As XmlNodeList = doc.GetElementsByTagName("source")
            Dim i As Integer

            For i = 0 To elemList.Count - 1
                Dim sourceElem As XmlNode = elemList.Item(i)
                Dim tmpSourceNameStr As String = sourceElem.Attributes(0).InnerText
                cmbDataSourceLink.Items.Add(tmpSourceNameStr)
                Dim cNode As XmlNodeList = sourceElem.ChildNodes
                If Not cNode.Count.Equals(3) Then
                    MsgBox(tmpSourceNameStr + " should have all the three expected fields such as Columns,Sqlquery & filter")
                    Return False
                End If
                Dim columnNodes As XmlNodeList = cNode.Item(0).ChildNodes

                Dim node As XmlNode
                Dim tmpDicSources As New Dictionary(Of String, Dictionary(Of String, String))

                Dim tmpDicColumn As New Dictionary(Of String, String)
                For Each node In columnNodes
                    tmpDicColumn.Add(node.Attributes(0).InnerText, node.InnerText)
                Next
                tmpDicSources.Add("columns", tmpDicColumn)

                Dim tmpDicSqlquery As New Dictionary(Of String, String)
                If cNode.Item(1).Attributes.Count > 0 Then
                    tmpDicSqlquery.Add(cNode.Item(1).Attributes(0).InnerText, cNode.Item(1).InnerText)
                End If
                tmpDicSources.Add("sqlquery", tmpDicSqlquery)

                Dim tmpDicFilter As New Dictionary(Of String, String)
                If cNode.Item(2).Attributes.Count > 0 Then
                    tmpDicFilter.Add(cNode.Item(2).Attributes(0).InnerText, cNode.Item(2).InnerText)
                End If
                tmpDicSources.Add("filtervalue", tmpDicFilter)

                dataSourceMap.Add(tmpSourceNameStr, tmpDicSources)
            Next i
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at loadDataSourceListValues", ex.Message, ex.StackTrace)
        End Try
    End Function

    Private Sub btnLoadDataSource_Click(sender As Object, e As EventArgs) Handles btnLoadDataSource.Click
        Try

            Dim valueselect As String = cmbDataSourceLink.Text
            If valueselect.Equals("") Then
                MsgBox("Please select a Source!")
            Else
                lstboxDataSource.Items.Clear()
                If dataSourceMap.ContainsKey(valueselect) And dataSourceMap(valueselect).ContainsKey("columns") Then
                    Dim tmpDic As Dictionary(Of String, String) = dataSourceMap(valueselect)("columns")
                    For Each pair In tmpDic
                        lstboxDataSource.Items.Add(pair.Key)
                    Next
                End If

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnLoadDataSource_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl15_TextChanged(sender As Object, e As EventArgs) Handles ctl15.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                Dim visible As Boolean = DirectCast(sender, ComboBox).Text.ToString
                If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    If visible.Equals(True) Then
                        Me.Controls.Find(lastActiveElement, True)(0).Visible = True
                        ctl15.Text = True
                    ElseIf visible.Equals(False) Then
                        ctl15.Text = False
                        Me.Controls.Find(lastActiveElement, True)(0).Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl15_textchanged", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub ctl2_TextChanged(sender As Object, e As EventArgs) Handles ctl2.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                Dim Border As Boolean = DirectCast(sender, ComboBox).Text.ToString
                If Not lastActiveElement.Equals("") Then
                    If Border.Equals(True) Then
                        If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                            If Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Label") Or Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("QueryField") Then
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).BorderStyle = BorderStyle.FixedSingle
                            ElseIf Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Image") Then
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), PictureBox).BorderStyle = BorderStyle.FixedSingle
                            End If
                        End If
                    Else
                        If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                            If Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Label") Or Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("QueryField") Then
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).BorderStyle = BorderStyle.None
                            ElseIf Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Image") Then
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), PictureBox).BorderStyle = BorderStyle.None
                            End If
                        End If
                    End If
                End If
            End If
            ''  system.Windows.Forms.BorderStyle.Fixed3D()

        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl2_textchanged", ex.Message, ex.StackTrace)
        End Try



    End Sub

    Private Sub ctl3_TextChanged(sender As Object, e As EventArgs) Handles ctl3.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                Dim fntname As String = DirectCast(sender, ComboBox).Text.ToString

                If Not lastActiveElement.Equals("") Then
                    If Me.Controls.Find(lastActiveElement, True).Length > 0 Then

                        Dim fontVal As Font = Me.Controls.Find(lastActiveElement, True)(0).Font
                        'Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, fontVal.Style)

                        Dim fntCase As String = ""
                        If ctl5.Text = "True" Then
                            fntCase = "1"
                        Else
                            fntCase = "0"
                        End If

                        If ctl6.Text = "True" Then
                            fntCase = fntCase & "1"
                        Else
                            fntCase = fntCase & "0"
                        End If

                        If ctl7.Text = "True" Then
                            fntCase = fntCase & "1"
                        Else
                            fntCase = fntCase & "0"
                        End If

                        If ctl8.Text = "True" Then
                            fntCase = fntCase & "1"
                        Else
                            fntCase = fntCase & "0"
                        End If

                        Select Case fntCase
                            Case "0000"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Regular)
                            Case "0001"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Underline)
                            Case "0010"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Strikeout)
                            Case "0011"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Strikeout Or FontStyle.Underline)
                            Case "0100"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Italic)
                            Case "0101"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Italic Or FontStyle.Underline)
                            Case "0110"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Italic Or FontStyle.Strikeout)
                            Case "0111"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                            Case "1000"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold)
                            Case "1001"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Underline)
                            Case "1010"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Strikeout)
                            Case "1011"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                            Case "1100"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Italic)
                            Case "1101"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                            Case "1110"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                            Case "1111"
                                Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        End Select

                        'Dim fontVal As Font = Me.Controls.Find(lastActiveElement, True)(0).Font
                        'Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, fontVal.Style)
                    End If
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl3_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnLinkFields_Click(sender As Object, e As EventArgs) Handles btnLinkFields.Click
        Try
            ctl16.Items.Clear()
            ctl16.Items.Add(" none ")
            For Each Item In lstboxDataSource.CheckedItems
                ctl16.Items.Add(Item.ToString)
            Next
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnLinkFields_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl16_SelectedValueChanged(sender As Object, e As EventArgs) Handles ctl16.SelectedValueChanged
        Try
            Dim val As String = DirectCast(sender, ComboBox).Text.ToString
            If Not val.Equals("") And Not val.Equals(" none ") Then
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Me.Controls.Find(lastActiveElement, True)(0).Text = val
                    ctl13.Text = val
                End If
            Else
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Me.Controls.Find(lastActiveElement, True)(0).Text = Me.Controls.Find(lastActiveElement, True)(0).Name.Replace("__", "")
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl16_SelectedValueChanged", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub ctl4_TextChanged(sender As Object, e As EventArgs) Handles ctl4.TextChanged
        Try
            Dim fntsize As String = DirectCast(sender, ComboBox).Text.ToString

            If Not lastActiveElement.Equals("") Then
                'fntsize = ctl4.Text
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then

                    Dim fntSizeval As Integer = Convert.ToInt64(fntsize)
                    Dim fontVal As Font = Me.Controls.Find(lastActiveElement, True)(0).Font
                    'Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(fntname, fontVal.Size, fontVal.Style)

                    Dim fntCase As String = ""
                    If ctl5.Text = "True" Then
                        fntCase = "1"
                    Else
                        fntCase = "0"
                    End If

                    If ctl6.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl7.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl8.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    Select Case fntCase
                        Case "0000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Regular)
                        Case "0001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Underline)
                        Case "0010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Strikeout)
                        Case "0011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Strikeout Or FontStyle.Underline)
                        Case "0100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Italic)
                        Case "0101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Italic Or FontStyle.Underline)
                        Case "0110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Italic Or FontStyle.Strikeout)
                        Case "0111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold)
                        Case "1001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Underline)
                        Case "1010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Strikeout)
                        Case "1011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Italic)
                        Case "1101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                        Case "1110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                        Case "1111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Font.Name, fntSizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                    End Select

                    'Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font.Size, fntsize)

                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl9_textchanged", ex.Message, ex.StackTrace)
        End Try

    End Sub
    Private Sub ctl6_TextChanged(sender As Object, e As EventArgs) Handles ctl6.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Dim fntCase As String = ""
                    If ctl5.Text = "True" Then
                        fntCase = "1"
                    Else
                        fntCase = "0"
                    End If

                    If ctl6.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl7.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl8.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    Select Case fntCase
                        Case "0000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Regular)
                        Case "0001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Underline)
                        Case "0010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout)
                        Case "0011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout Or FontStyle.Underline)
                        Case "0100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic)
                        Case "0101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Underline)
                        Case "0110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout)
                        Case "0111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold)
                        Case "1001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Underline)
                        Case "1010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout)
                        Case "1011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic)
                        Case "1101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                        Case "1110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                        Case "1111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                    End Select
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl6_textchanged", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub ctl7_TextChanged(sender As Object, e As EventArgs) Handles ctl7.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Dim fntCase As String = ""
                    If ctl5.Text = "True" Then
                        fntCase = "1"
                    Else
                        fntCase = "0"
                    End If

                    If ctl6.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl7.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl8.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    Select Case fntCase
                        Case "0000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Regular)
                        Case "0001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Underline)
                        Case "0010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout)
                        Case "0011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout Or FontStyle.Underline)
                        Case "0100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic)
                        Case "0101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Underline)
                        Case "0110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout)
                        Case "0111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold)
                        Case "1001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Underline)
                        Case "1010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout)
                        Case "1011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic)
                        Case "1101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                        Case "1110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                        Case "1111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                    End Select
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl6_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl8_TextChanged(sender As Object, e As EventArgs) Handles ctl8.TextChanged
        Try
            If Not DirectCast(sender, ComboBox).Text.Equals("") Then
                If Not lastActiveElement.Equals("") And Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    Dim fntCase As String = ""
                    If ctl5.Text = "True" Then
                        fntCase = "1"
                    Else
                        fntCase = "0"
                    End If

                    If ctl6.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl7.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    If ctl8.Text = "True" Then
                        fntCase = fntCase & "1"
                    Else
                        fntCase = fntCase & "0"
                    End If

                    Select Case fntCase
                        Case "0000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Regular)
                        Case "0001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Underline)
                        Case "0010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout)
                        Case "0011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Strikeout Or FontStyle.Underline)
                        Case "0100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic)
                        Case "0101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Underline)
                        Case "0110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout)
                        Case "0111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1000"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold)
                        Case "1001"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Underline)
                        Case "1010"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout)
                        Case "1011"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                        Case "1100"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic)
                        Case "1101"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                        Case "1110"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                        Case "1111"
                            Me.Controls.Find(lastActiveElement, True)(0).Font = New Font(Me.Controls.Find(lastActiveElement, True)(0).Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                    End Select
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl8_textchanged", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Try
            pnlOpenTemplates.BringToFront()
            loadTemplates()
            pnlOpenTemplates.Show()
            btnNew.Enabled = False
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnOpen_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnCancelOpenTemplate_Click(sender As Object, e As EventArgs) Handles btnCancelOpenTemplate.Click
        Try
            pnlOpenTemplates.SendToBack()
            pnlOpenTemplates.Hide()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnOpen_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Public Sub loadSettings()
        Try
            lstViewSettings.Clear()
            lstViewSettings.Columns.Add("SNo", 50, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Location Code", 100, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Location Name", 130, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Template ID", 0, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Template Name", 170, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Transaction Name", 170, HorizontalAlignment.Center)
            lstViewSettings.Columns.Add("Transaction Code", 0, HorizontalAlignment.Center)
            lstViewSettings.View = View.Details
            lstViewSettings.GridLines = True
            lstViewSettings.FullRowSelect = True
            Dim stQuery As String
            ds = New DataSet
            Dim count As Integer
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            stQuery = "select POS_PRINT_LOCN_SYSID,POS_PRINT_LOCN_CODE,POS_PRINT_LOCN_NAME,POS_PRINT_TEMP_ID,POS_PRINT_TEMP_NAME,POS_PRINT_TRANS_CODE,POS_PRINT_TRANS_NAME from POS_PRINT_LOCN_SETTINGS order by POS_PRINT_LOCN_SYSID"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                lstViewSettings.Items.Add(row.Item(0))
                lstViewSettings.Items(i).SubItems.Add(row.Item(1).ToString)
                lstViewSettings.Items(i).SubItems.Add(row.Item(2).ToString)
                lstViewSettings.Items(i).SubItems.Add(row.Item(3).ToString)
                lstViewSettings.Items(i).SubItems.Add(row.Item(4).ToString)
                lstViewSettings.Items(i).SubItems.Add(row.Item(6).ToString)
                count = count - 1
                i = i + 1
            End While

            Location_Codes.Clear()
            stQuery = "select LOCN_CODE from OM_Location where LOCN_FRZ_FLAG_NUM = 2"
            ds = db.SelectFromTableODBC(stQuery)

            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Location_Codes.Add(row.Item(0).ToString)
                i = i + 1
                count = count - 1
            End While
            MySource_LocationCodes.AddRange(Location_Codes.ToArray)
            txtCounterAddLocationCode.AutoCompleteCustomSource = MySource_LocationCodes
            txtCounterAddLocationCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtCounterAddLocationCode.AutoCompleteSource = AutoCompleteSource.CustomSource

            cmbTransactionNames.Items.Clear()
            Dim tempDataSourceMap As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, String))) = dataSourceMap
            For Each pair In tempDataSourceMap
                cmbTransactionNames.Items.Add(pair.Key)
            Next

            stQuery = "select POS_PRINT_TEMPLATE_NAME from pos_print_template "
            ds = db.SelectFromTableODBC(stQuery)
            cmbTemplateNames.Items.Clear()
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                cmbTemplateNames.Items.Add(row.Item(0).ToString)
                i = i + 1
                count = count - 1
            End While

        Catch ex As Exception
            errLog.WriteToErrorLog("Error at loadSettings", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCounterAddLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCounterAddLocationCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select LOCN_NAME from OM_Location where LOCN_FRZ_FLAG_NUM = 2 and LOCN_CODE='" & txtCounterAddLocationCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtCounterAddLocationDesc.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtCounterAddLocationDesc.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Sub loadTemplates()
        Try
            btnOpen.Enabled = False
            btnNew.Enabled = False
            listViewTemplates.Clear()
            listViewTemplates.Columns.Add("TemplateID", 0, HorizontalAlignment.Center)
            listViewTemplates.Columns.Add("SNo", 50, HorizontalAlignment.Center)
            listViewTemplates.Columns.Add("Template Name", 250, HorizontalAlignment.Center)
            listViewTemplates.Columns.Add("Created Date ", 130, HorizontalAlignment.Center)
            listViewTemplates.Columns.Add("Created User", 100, HorizontalAlignment.Center)
            listViewTemplates.Columns.Add("Editable", 80, HorizontalAlignment.Center)
            listViewTemplates.View = View.Details
            listViewTemplates.GridLines = True
            listViewTemplates.FullRowSelect = True

            Dim stQuery As String
            ds = New DataSet
            Dim count As Integer
            Dim i As Integer = 0
            Dim row As System.Data.DataRow
            stQuery = "select POS_PRINT_TEMPLATE_SYSID,rownum,POS_PRINT_TEMPLATE_NAME,POS_PRINT_TEMPLATE_CRDT,POS_PRINT_TEMPLATE_CRUID,POS_PRINT_TEMPLATE_FREEZE from pos_print_template order by POS_PRINT_TEMPLATE_CRDT"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                listViewTemplates.Items.Add(row.Item(0))
                listViewTemplates.Items(i).SubItems.Add(row.Item(1).ToString)
                listViewTemplates.Items(i).SubItems.Add(row.Item(2).ToString)
                listViewTemplates.Items(i).SubItems.Add(row.Item(3).ToString)
                listViewTemplates.Items(i).SubItems.Add(row.Item(4).ToString)
                If row.Item(5).ToString.Equals("1") Then
                    listViewTemplates.Items(i).SubItems.Add("Yes")
                ElseIf row.Item(5).ToString.Equals("2") Then
                    listViewTemplates.Items(i).SubItems.Add("No")
                End If
                count = count - 1
                i = i + 1
            End While
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at loadTemplates", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ctl14_TextChanged(sender As Object, e As EventArgs) Handles ctl14.TextChanged
        Try

            Dim txtalign As String = DirectCast(sender, ComboBox).Text.ToString
            If Not lastActiveElement.Equals("") Then
                If Me.Controls.Find(lastActiveElement, True).Length > 0 Then
                    If Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("Label") Or Me.Controls.Find(lastActiveElement, True)(0).Name.Contains("QueryField") Then
                        Select Case txtalign
                            Case "Top Left"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.TopLeft
                            Case "Top Center"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.TopCenter
                            Case "Top Right"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.TopRight
                            Case "Middle Left"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.MiddleLeft
                            Case "Middle Center"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.MiddleCenter
                            Case "Middle Right"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.MiddleRight
                            Case "Bottom Left"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.BottomLeft
                            Case "Bottom Center"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.BottomCenter
                            Case "Bottom Right"
                                DirectCast(Me.Controls.Find(lastActiveElement, True)(0), Label).TextAlign = ContentAlignment.BottomRight
                        End Select
                    End If
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at ctl9_textchanged", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnOpenSelectedTemplate_Click(sender As Object, e As EventArgs) Handles btnOpenSelectedTemplate.Click
        Try
            Dim pagecontrolsPropertiesMapTemp As New Dictionary(Of String, Dictionary(Of String, String))
            Dim tempID As String = ""
            Dim tempName As String = ""
            If listViewTemplates.SelectedItems.Count < 1 Then
                MsgBox("Please select a template from the row!")
                Exit Sub
            Else
                Dim editable As String = listViewTemplates.SelectedItems.Item(0).SubItems(5).Text
                If editable.Equals("No") Then
                    MsgBox("Sorry this template cannot be edited! Only a copy can be taken for working!")
                    Exit Sub
                Else
                    tempID = listViewTemplates.SelectedItems.Item(0).SubItems(0).Text
                    tempName = listViewTemplates.SelectedItems.Item(0).SubItems(2).Text
                    ds = New DataSet
                    stQuery = "select POS_PRINT_TEMPLATE_CTL_NAME,POS_PRINT_TEMPLATE_PROP_ID,POS_PRINT_TEMPLATE_PROP_VAL from pos_print_template_prop a,pos_print_template_controls b where a.pos_print_template_prop_ctlid=b.pos_print_template_ctl_sysid and b.pos_print_template_ctl_tempid=" & tempID
                    ds = db.SelectFromTable(stQuery)
                    Dim count As Integer = ds.Tables("Table").Rows.Count
                    Dim i As Integer = 0
                    Dim row As System.Data.DataRow

                    While count > 0
                        row = ds.Tables("Table").Rows.Item(i)

                        Dim tmpDic As New Dictionary(Of String, String)

                        If Not pagecontrolsPropertiesMapTemp.ContainsKey(row.Item(0).ToString) Then
                            tmpDic.Add("ctl" & row.Item(1).ToString, row.Item(2).ToString)
                            pagecontrolsPropertiesMapTemp.Add(row.Item(0).ToString, tmpDic)
                            pagecontrolsPropertiesMap.Add(row.Item(0).ToString, tmpDic)
                        Else
                            tmpDic = pagecontrolsPropertiesMapTemp(row.Item(0).ToString)
                            tmpDic.Add("ctl" & row.Item(1).ToString, row.Item(2).ToString)
                            pagecontrolsPropertiesMapTemp(row.Item(0).ToString) = tmpDic
                            pagecontrolsPropertiesMap(row.Item(0).ToString) = tmpDic
                        End If

                        count = count - 1
                        i = i + 1
                    End While
                End If
            End If

            ds = New DataSet
            stQuery = "select POS_PRINT_DATASRC_SRCNAME,POS_PRINT_DATASRC_ALSNAME from POS_PRINT_DATASRC where POS_PRINT_DATASRC_TEMPID=" & tempID
            ds = db.SelectFromTable(stQuery)
            Dim cnt As Integer = ds.Tables("Table").Rows.Count
            Dim row1 As System.Data.DataRow
            Dim j As Integer = 0
            If cnt > 0 Then
                cmbDataSourceLink.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                btnLoadDataSource_Click(sender, e)
                While cnt > 0
                    row1 = ds.Tables("Table").Rows.Item(j)
                    Dim I As Integer
                    For I = 0 To lstboxDataSource.Items.Count - 1
                        If lstboxDataSource.Items(I).IndexOf(row1.Item(1)) <> -1 Then
                            lstboxDataSource.SetItemChecked(I, True)
                        End If
                    Next
                    cnt = cnt - 1
                    j = j + 1
                End While
                btnLinkFields_Click(sender, e)
            End If

            For Each pair In pagecontrolsPropertiesMapTemp
                Dim ctlNamevals() As String = pair.Key.ToString.Split("__")
                Dim caseValue As String = ctlNamevals(0)
                Dim caseValueID As String = ctlNamevals(2)
                Dim ctlDicValue As Dictionary(Of String, String) = pair.Value
                Dim n As Integer
                Select Case caseValue
                    Case "Label"
                        Dim lbl As New Label
                        n = lblControls.Count + 1
                        With lbl
                            .Name = ctlDicValue("ctl1")
                            If ctlDicValue("ctl2").Equals("True") Then
                                .BorderStyle = BorderStyle.FixedSingle
                            Else
                                .BorderStyle = BorderStyle.None
                            End If
                            Dim fontsizeval As Integer = Convert.ToInt64(ctlDicValue("ctl4"))
                            Dim fontNameval As String = ctlDicValue("ctl3").ToString

                            Dim fntCase As String = ""
                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                fntCase = "1"
                            Else
                                fntCase = "0"
                            End If

                            If ctlDicValue("ctl6").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            If ctlDicValue("ctl7").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            If ctlDicValue("ctl8").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            Select Case fntCase
                                Case "0000"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Regular)
                                Case "0001"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Underline)
                                Case "0010"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Strikeout)
                                Case "0011"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Strikeout Or FontStyle.Underline)
                                Case "0100"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic)
                                Case "0101"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Underline)
                                Case "0110"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Strikeout)
                                Case "0111"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                                Case "1000"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold)
                                Case "1001"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Underline)
                                Case "1010"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Strikeout)
                                Case "1011"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                                Case "1100"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic)
                                Case "1101"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                                Case "1110"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                                Case "1111"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                            End Select

                            .BackColor = Color.Transparent
                            .Text = ctlDicValue("ctl13")
                            Dim txtalign As String = ctlDicValue("ctl14")
                            Select Case txtalign
                                Case "Top Left"
                                    .TextAlign = ContentAlignment.TopLeft
                                Case "Top Center"
                                    .TextAlign = ContentAlignment.TopCenter
                                Case "Top Right"
                                    .TextAlign = ContentAlignment.TopRight
                                Case "Middle Left"
                                    .TextAlign = ContentAlignment.MiddleLeft
                                Case "Middle Center"
                                    .TextAlign = ContentAlignment.MiddleCenter
                                Case "Middle Right"
                                    .TextAlign = ContentAlignment.MiddleRight
                                Case "Bottom Left"
                                    .TextAlign = ContentAlignment.BottomLeft
                                Case "Bottom Center"
                                    .TextAlign = ContentAlignment.BottomCenter
                                Case "Bottom Right"
                                    .TextAlign = ContentAlignment.BottomRight
                            End Select
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .BringToFront()

                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If

                        End With

                        AddHandler lbl.MouseDown, AddressOf startDrag
                        AddHandler lbl.MouseMove, AddressOf whileDragging
                        AddHandler lbl.MouseUp, AddressOf endDrag
                        AddHandler lbl.MouseClick, AddressOf whiledelete
                        'AddHandler lbl.LostFocus, AddressOf whenCtlLosesFocus
                        'Dim ctlID As String = btnControlsHashMap(ctlName)

                        loadProperties(lbl)
                        lblControls.Add(lbl)
                        Me.pnlPaper.Controls.Add(lbl)
                        lbl.BringToFront()
                        startDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "Label__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        btnHistoryControls.Add(btn)
                        'btnHistoryControls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                    Case "Image"
                        Dim img As New PictureBox
                        n = imgControls.Count + 1
                        With img
                            .Name = ctlDicValue("ctl1")
                            If ctlDicValue("ctl2").Equals("True") Then
                                .BorderStyle = BorderStyle.FixedSingle
                            Else
                                .BorderStyle = BorderStyle.None
                            End If
                            .BackColor = Me.pnlPaper.BackColor
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .BringToFront()
                            .SizeMode = PictureBoxSizeMode.StretchImage
                            .Image = Image.FromFile(ctlDicValue("ctl17"))

                            'Dim fntCase As String = ""
                            'If ctlDicValue("ctl5").ToString.Equals("True") Then
                            '    fntCase = "1"
                            '    .Visible = True
                            'Else
                            '    fntCase = "0"
                            '    .Visible = False

                            ''End If

                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If


                        End With
                        'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                        'For Each pairVal In totalDControlProperties
                        '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                        '    If Not pairVal.Key.Equals("1") Then
                        '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                        '    End If
                        'Next


                        AddHandler img.MouseDown, AddressOf startDrag
                        ''Commented to handle resize drag issue
                        ''AddHandler img.MouseMove, AddressOf whileDragging
                        AddHandler img.MouseUp, AddressOf endDrag
                        AddHandler img.MouseEnter, AddressOf mouseScalingEnter
                        AddHandler img.MouseWheel, AddressOf whileScaling
                        AddHandler img.MouseClick, AddressOf RightOptions
                        'AddHandler img.LostFocus, AddressOf whenCtlLosesFocus

                        loadProperties(img)
                        imgControls.Add(img)
                        Me.pnlPaper.Controls.Add(img)
                        startDrag(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(img, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "Image__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        btnHistoryControls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                        rc = New ResizeableControl(img)
                    Case "HLine"
                        Dim pnl As New Panel
                        n = hlineControls.Count + 1
                        With pnl
                            .Name = ctlDicValue("ctl1")
                            .BorderStyle = BorderStyle.FixedSingle
                            .BackColor = Color.Black
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .BringToFront()
                            .Cursor = Cursors.SizeAll

                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If


                        End With
                        'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                        'For Each pairVal In totalDControlProperties
                        '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                        '    If Not pairVal.Key.Equals("1") Then
                        '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                        '    End If
                        'Next

                        AddHandler pnl.MouseDown, AddressOf startDrag
                        AddHandler pnl.MouseMove, AddressOf whileDragging
                        AddHandler pnl.MouseUp, AddressOf endDrag
                        AddHandler pnl.MouseHover, AddressOf callMouseHover
                        AddHandler pnl.MouseLeave, AddressOf callMouseLeave
                        AddHandler pnl.MouseClick, AddressOf whiledelete
                        '' AddHandler pnl.MouseLeave, AddressOf whenlostfocus
                        'AddHandler pnl.LostFocus, AddressOf whenCtlLosesFocus
                        loadProperties(pnl)
                        hlineControls.Add(pnl)
                        Me.pnlPaper.Controls.Add(pnl)
                        startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "HLine__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)

                        btnHistoryControls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                    Case "VLine"
                        Dim pnl As New Panel
                        n = vlineControls.Count + 1
                        With pnl
                            .Name = ctlDicValue("ctl1")
                            .BorderStyle = BorderStyle.FixedSingle
                            .BackColor = Color.Black
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .BringToFront()
                            .Cursor = Cursors.SizeAll

                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If


                        End With
                        'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                        'For Each pairVal In totalDControlProperties
                        '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                        '    If Not pairVal.Key.Equals("1") Then
                        '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                        '    End If
                        'Next
                        AddHandler pnl.MouseDown, AddressOf startDrag
                        AddHandler pnl.MouseMove, AddressOf whileDragging
                        AddHandler pnl.MouseUp, AddressOf endDrag
                        AddHandler pnl.MouseHover, AddressOf callMouseHover
                        AddHandler pnl.MouseLeave, AddressOf callMouseLeave
                        AddHandler pnl.MouseClick, AddressOf whiledelete
                        'AddHandler pnl.LostFocus, AddressOf whenCtlLosesFocus
                        loadProperties(pnl)
                        vlineControls.Add(pnl)
                        Me.pnlPaper.Controls.Add(pnl)
                        startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "VLine__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                    Case "Panel"
                        Dim pnl As New Panel
                        n = pnlControls.Count + 1
                        With pnl
                            .Name = ctlDicValue("ctl1")
                            .BorderStyle = BorderStyle.FixedSingle
                            .BackColor = Color.White
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .SendToBack()
                            .Cursor = Cursors.SizeAll

                            Dim fntCase As String = ""
                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If
                        End With
                        'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                        'For Each pairVal In totalDControlProperties
                        '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                        '    If Not pairVal.Key.Equals("1") Then
                        '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                        '    End If
                        'Next
                        AddHandler pnl.MouseDown, AddressOf startDrag
                        AddHandler pnl.MouseMove, AddressOf whileDragging
                        AddHandler pnl.MouseUp, AddressOf endDrag
                        AddHandler pnl.MouseClick, AddressOf whiledelete
                        'AddHandler pnl.MouseDoubleClick, AddressOf whenCtlLosesFocus
                        loadProperties(pnl)

                        pnlControls.Add(pnl)
                        Me.pnlPaper.Controls.Add(pnl)
                        startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        pnl.SendToBack()
                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "Panel__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                    Case "QueryField"
                        Dim lbl As New Label
                        n = queryFieldsControls.Count + 1
                        With lbl
                            .Name = ctlDicValue("ctl1")
                            If ctlDicValue("ctl2").Equals("True") Then
                                .BorderStyle = BorderStyle.FixedSingle
                            Else
                                .BorderStyle = BorderStyle.None
                            End If
                            Dim fontsizeval As Integer = Convert.ToInt64(ctlDicValue("ctl4"))
                            Dim fontNameval As String = ctlDicValue("ctl3").ToString

                            Dim fntCase As String = ""
                            If ctlDicValue("ctl5").ToString.Equals("True") Then
                                fntCase = "1"
                                .Visible = True
                            Else
                                fntCase = "0"
                                .Visible = False
                            End If

                            If ctlDicValue("ctl6").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            If ctlDicValue("ctl7").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            If ctlDicValue("ctl8").ToString.Equals("True") Then
                                fntCase = fntCase & "1"
                            Else
                                fntCase = fntCase & "0"
                            End If

                            Select Case fntCase
                                Case "0000"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Regular)
                                Case "0001"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Underline)
                                Case "0010"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Strikeout)
                                Case "0011"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Strikeout Or FontStyle.Underline)
                                Case "0100"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic)
                                Case "0101"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Underline)
                                Case "0110"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Strikeout)
                                Case "0111"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                                Case "1000"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold)
                                Case "1001"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Underline)
                                Case "1010"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Strikeout)
                                Case "1011"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Strikeout Or FontStyle.Underline)
                                Case "1100"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic)
                                Case "1101"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
                                Case "1110"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout)
                                Case "1111"
                                    .Font = New Font(fontNameval, fontsizeval, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Strikeout Or FontStyle.Underline)
                            End Select

                            .BackColor = Color.Transparent
                            Dim txtalign As String = ctlDicValue("ctl14")
                            Select Case txtalign
                                Case "Top Left"
                                    .TextAlign = ContentAlignment.TopLeft
                                Case "Top Center"
                                    .TextAlign = ContentAlignment.TopCenter
                                Case "Top Right"
                                    .TextAlign = ContentAlignment.TopRight
                                Case "Middle Left"
                                    .TextAlign = ContentAlignment.MiddleLeft
                                Case "Middle Center"
                                    .TextAlign = ContentAlignment.MiddleCenter
                                Case "Middle Right"
                                    .TextAlign = ContentAlignment.MiddleRight
                                Case "Bottom Left"
                                    .TextAlign = ContentAlignment.BottomLeft
                                Case "Bottom Center"
                                    .TextAlign = ContentAlignment.BottomCenter
                                Case "Bottom Right"
                                    .TextAlign = ContentAlignment.BottomRight
                            End Select
                            '.Height = Convert.ToInt64(ctlDicValue("ctl9"))
                            '.Width = Convert.ToInt64(ctlDicValue("ctl10"))
                            .BackColor = Color.Transparent
                            .Text = ctlDicValue("ctl13")
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .BringToFront()

                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If


                        End With
                        AddHandler lbl.MouseDown, AddressOf startDrag
                        AddHandler lbl.MouseMove, AddressOf whileDragging
                        AddHandler lbl.MouseUp, AddressOf endDrag
                        AddHandler lbl.MouseClick, AddressOf whiledelete
                        'AddHandler lbl.LostFocus, AddressOf whenCtlLosesFocus
                        'Dim ctlID As String = btnControlsHashMap(ctlName)

                        loadProperties(lbl)
                        queryFieldsControls.Add(lbl)
                        Me.pnlPaper.Controls.Add(lbl)
                        startDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(lbl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "QueryField__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke

                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement
                    Case "Table"
                        Dim pnl As New Panel
                        n = tableControls.Count + 1
                        With pnl
                            .Name = "Table__" & n
                            .BorderStyle = BorderStyle.FixedSingle
                            .BackColor = Color.White
                            .Location = New Point(Convert.ToInt64(ctlDicValue("ctl11")), Convert.ToInt64(ctlDicValue("ctl12")))
                            .Size = New Size(Convert.ToInt64(ctlDicValue("ctl10")), Convert.ToInt64(ctlDicValue("ctl9")))
                            .SendToBack()
                            .Cursor = Cursors.SizeAll
                            Dim fntCase As String = ""
                            If ctlDicValue("ctl15").ToString.Equals("True") Then
                                .Visible = True
                            Else
                                .Visible = False
                            End If

                        End With
                        'Dim pairVal As KeyValuePair(Of String, Dictionary(Of String, String))
                        'For Each pairVal In totalDControlProperties
                        '    Dim lst As List(Of String) = totalDControlTypeValues(pairVal.Key)
                        '    If Not pairVal.Key.Equals("1") Then
                        '        Me.Controls.Find("ctl" & pairVal.Key, True)(0).Text = lst(2)
                        '    End If
                        'Next

                        AddHandler pnl.MouseDown, AddressOf startDrag
                        AddHandler pnl.MouseMove, AddressOf whileDragging
                        AddHandler pnl.MouseUp, AddressOf endDrag
                        AddHandler pnl.MouseClick, AddressOf whiledelete
                        'AddHandler pnl.MouseDoubleClick, AddressOf whenCtlLosesFocus
                        loadProperties(pnl)

                        tableControls.Add(pnl)
                        Me.pnlPaper.Controls.Add(pnl)
                        startDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        whileDragging(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))
                        endDrag(pnl, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 0, 0, 0, 0))

                        Dim btn As New Button
                        With btn
                            .Name = "btnHistoryControls__" & (btnHistoryControls.Count + 1)
                            .Dock = DockStyle.Top
                            .Text = "Table__" & n
                            .FlatStyle = FlatStyle.Flat
                            .BackColor = Color.WhiteSmoke
                            .FlatAppearance.BorderSize = 1
                        End With
                        Me.pnlHistoryControls.Controls.Add(btn)
                        AddHandler btn.Click, AddressOf FocusElement

                End Select
            Next
            'Dim ctlNamevals() As String = row.Item(0).ToString.Split("__")
            'Dim caseValue As String = ctlNamevals(0)
            'Dim caseValueID As String = ctlNamevals(2)
            currTemplateID = tempID
            currTemplateName = tempName
            pnlPaper.Enabled = True
            pnlLeftToolBox.Enabled = True
            btnSave.Enabled = True
            If pnlPaper.Controls.Count > 0 Then
                pnlCtlProptiesTabHolder.Enabled = True
            Else
                pnlCtlProptiesTabHolder.Enabled = False
            End If
            pnlOpenTemplates.Visible = False
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at btnOpenSelectedTemplate_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub FromFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromFolderToolStripMenuItem.Click
        Try
            Dim fullPath As String = IO.Path.GetFullPath(My.Resources.ResourceManager.BaseName)
            fullPath = fullPath.Substring(0, fullPath.Length - 39) & "Resources\"
            Dim Chosen_File As String = ""
            Dim picbox As PictureBox = Me.Controls.Find(lastActiveElement, True)(0)
            OpenFileDialog1.InitialDirectory = fullPath
            OpenFileDialog1.ShowDialog()
            Chosen_File = OpenFileDialog1.FileName
            picbox.Image = Image.FromFile(Chosen_File)
            ctl17.Text = Chosen_File
            picbox.SizeMode = PictureBoxSizeMode.StretchImage
            Me.pnlPaper.Controls.Add(picbox)
            AddHandler picbox.MouseClick, AddressOf RightOptions
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at FromFolderToolStripMenuItem_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)

            ctl.Hide()
            ctl15.Text = "False"

        Catch ex As Exception
            errLog.WriteToErrorLog("Error at DeleteToolStripMenuItem_Click", ex.Message, ex.StackTrace)
        End Try

    End Sub
    Private Sub BrowseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrowseToolStripMenuItem.Click
        Try
            OpenFileDialog1.ShowDialog()
            Dim Chosen_File As String = ""
            Chosen_File = OpenFileDialog1.FileName
            '' Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)
            If TypeOf Me.Controls.Find(lastActiveElement, True)(0) Is PictureBox Then
                Dim PictureBox As PictureBox = Me.Controls.Find(lastActiveElement, True)(0)
                Me.pnlPaper.Controls.Add(PictureBox)
                PictureBox.Image = Image.FromFile(Chosen_File)
                PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                ctl17.Text = Chosen_File
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at BrowseToolStripMenuItem_Click", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub btnTemplates_Click(sender As Object, e As EventArgs) Handles btnTemplates.Click

    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        pnlSettings.BringToFront()
        loadSettings()
        pnlSettings.Show()
    End Sub

    Private Sub btnSaveTempSettings_Click(sender As Object, e As EventArgs) Handles btnSaveTempSettings.Click
        Try
            If txtCounterAddLocationDesc.Text.Length < 1 Then
                MsgBox("Please select Location!")
                Exit Sub
            ElseIf cmbTemplateNames.Text.Length < 1 Then
                MsgBox("Please select Template Name!")
                Exit Sub
            ElseIf cmbTransactionNames.Text.Length < 1 Then
                MsgBox("Please select Transaction Name!")
                Exit Sub
            End If
            Dim stQuery As String = ""
            Dim count As Integer = 0
            Dim ds As DataSet
            Dim templateID As String = ""

            stQuery = "select POS_PRINT_TEMPLATE_SYSID from pos_print_template where POS_PRINT_TEMPLATE_NAME='" & cmbTemplateNames.Text & "'"
            errLog.WriteToErrorLog("Error", "", stQuery)
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                templateID = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                MsgBox("Error Occured at Template Code!")
                Exit Sub
            End If

            stQuery = "select POS_PRINT_LOCN_SYSID from POS_PRINT_LOCN_SETTINGS where POS_PRINT_LOCN_CODE='" & txtCounterAddLocationCode.Text & "' and POS_PRINT_TRANS_NAME='" & cmbTransactionNames.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                Dim locnsysID As String = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                stQuery = "update POS_PRINT_LOCN_SETTINGS SET POS_PRINT_TEMP_NAME='" & cmbTemplateNames.Text & "' where POS_PRINT_LOCN_CODE='" & txtCounterAddLocationCode.Text & "' and POS_PRINT_TRANS_NAME='" & cmbTransactionNames.Text & "'"
                errLog.WriteToErrorLog("Error", "", stQuery)
                db.SaveToTableODBC(stQuery)
                MsgBox("Updated Successfully!")
            Else
                stQuery = "insert into POS_PRINT_LOCN_SETTINGS(POS_PRINT_LOCN_SYSID,POS_PRINT_LOCN_CODE,POS_PRINT_LOCN_NAME,POS_PRINT_TEMP_ID,POS_PRINT_TEMP_NAME,POS_PRINT_TRANS_NAME,POS_PRINT_CR_UID,POS_PRINT_CRDT)values("
                stQuery = stQuery & "pos_print_locn_seq.nextval,'" & txtCounterAddLocationCode.Text & "','" & txtCounterAddLocationDesc.Text & "'," & templateID & ",'" & cmbTemplateNames.Text & "','" & cmbTransactionNames.Text & "','" & LogonUser & "',sysdate)"
                errLog.WriteToErrorLog("Error", "", stQuery)
                db.SaveToTableODBC(stQuery)
                MsgBox("Inserted Successfully!")
            End If
            loadSettings()
        Catch ex As Exception
            MsgBox("Error occured during updating setting..")
            errLog.WriteToErrorLog("Error at btnSaveTempSettings_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnCloseTempSettings_Click(sender As Object, e As EventArgs) Handles btnCloseTempSettings.Click
        pnlSettings.Visible = False
    End Sub

    Private Sub lstViewSettings_Click(sender As Object, e As EventArgs) Handles lstViewSettings.Click
        Try
            If Not lstViewSettings.SelectedItems.Count > 0 Then
                MsgBox("Select a row!")
                Exit Sub
            End If

            txtCounterAddLocationCode.Text = lstViewSettings.SelectedItems.Item(0).SubItems(1).Text
            cmbTemplateNames.Text = lstViewSettings.SelectedItems.Item(0).SubItems(4).Text
            cmbTransactionNames.Text = lstViewSettings.SelectedItems.Item(0).SubItems(5).Text
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at lstViewSettings_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub BringToFrontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BringToFrontToolStripMenuItem.Click
        Try
            Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)
            ctl.BringToFront()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at DeleteToolStripMenuItem_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub SendToBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToBackToolStripMenuItem.Click
        Try
            Dim ctl As Control = Me.Controls.Find(lastActiveElement, True)(0)
            ctl.SendToBack()
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at DeleteToolStripMenuItem_Click", ex.Message, ex.StackTrace)
        End Try
    End Sub

End Class

