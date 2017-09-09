Imports System.IO

Public Class AdSelection
    Dim db As New DBConnection
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim Query As String
    Dim count As Integer = 0
    Dim Imgdest As String = "" 'Share_Name & "Ads\"
    Private Sub AdSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetResolution()
            ds.Dispose()
            'For Each ctl As Control In Me.Controls
            '    If Not ctl.Name = "butCampCrtChoose" And Not ctl.Name = "butCampCrtRemove" And Not ctl.Name = "adselPanel" Then
            '        Me.Controls.Remove(ctl)
            '    End If
            'Next
            'For Each ctl As Control In Me.adselPanel.Controls
            '    Me.adselPanel.Controls.Remove(ctl)
            'Next
            'Me.ResumeLayout()

            Dim di As New DirectoryInfo(Application.StartupPath & "\LOGOS\")
            ' Get a reference to each file in that directory.
            Dim fiArr As FileInfo() = di.GetFiles()
            ' Display the names of the files.
            Dim fri As FileInfo
            Dim ctlName As PictureBox
            Dim chkInput As RadioButton
            Dim testResult As Double
            Dim X As Integer
            Dim Y As Integer
            X = 50
            Y = 10
            Dim pos As Integer = 0
            Dim imgtypeArray(7) As String
            imgtypeArray(0) = ".JPG"
            imgtypeArray(1) = ".jpg"
            imgtypeArray(2) = ".BMP"
            imgtypeArray(3) = ".bmp"
            imgtypeArray(4) = ".PNG"
            imgtypeArray(5) = ".png"
            imgtypeArray(6) = ".GIF"
            imgtypeArray(7) = ".gif"

            For Each fri In fiArr
                testResult = pos Mod 3
                If pos <> 0 And testResult = 0 Then
                    Y = Y + 120
                    X = 50
                End If

                If imgtypeArray.Contains(fri.Extension) Then
                    ctlName = New PictureBox 'create an instance of a TextBox control.
                    ctlName.Location = New Point(X, Y) 'location points
                    ctlName.Name = fri.Name 'name for the textboxes (box1, box2, etc...
                    ctlName.Image = Image.FromFile(fri.FullName)
                    ctlName.Width = 100
                    ctlName.Height = 100
                    ctlName.SizeMode = PictureBoxSizeMode.StretchImage
                    Me.adselPanel.Controls.Add(ctlName)

                    chkInput = New RadioButton
                    chkInput.Name = "adchk" & fri.Name
                    chkInput.Location = New Point(X + 45, Y + 100) 'location points
                    chkInput.Text = ""
                    chkInput.Height = 15
                    chkInput.Width = 15
                    chkInput.TabIndex = pos
                    Me.adselPanel.Controls.Add(chkInput)
                    chkInput.BringToFront()
                    X = X + 130
                End If
                pos = pos + 1
            Next fri

            'Query = "select AD_ID,AD_IMAGEURL  as  from CRM_AD order by AD_ID ASC"
            'ds = db.SelectFromTableODBC(Query)
            'dt = ds.Tables("Table")
            'count = ds.Tables("Table").Rows.Count
            'Dim ctlName As PictureBox
            'Dim chkInput As RadioButton
            'Dim testResult As Double
            'Dim X As Integer
            'Dim Y As Integer
            'X = 50
            'Y = 10
            'For iIt = 0 To count - 1
            '    testResult = iIt Mod 3
            '    If iIt <> 0 And testResult = 0 Then
            '        Y = Y + 120
            '        X = 50
            '    End If

            '    ctlName = New PictureBox 'create an instance of a TextBox control.
            '    ctlName.Location = New Point(X, Y) 'location points
            '    ctlName.Name = "adimage" & dt.Rows(iIt).Item(0).ToString 'name for the textboxes (box1, box2, etc...
            '    ctlName.Image = Image.FromFile(Imgdest & dt.Rows(iIt).Item(1).ToString)
            '    ctlName.Width = 100
            '    ctlName.Height = 100
            '    ctlName.SizeMode = PictureBoxSizeMode.StretchImage
            '    Me.adselPanel.Controls.Add(ctlName)

            '    chkInput = New RadioButton
            '    chkInput.Name = "adchk" & dt.Rows(iIt).Item(0).ToString
            '    chkInput.Location = New Point(X + 45, Y + 100) 'location points
            '    chkInput.Text = ""
            '    chkInput.Height = 15
            '    chkInput.Width = 15
            '    chkInput.TabIndex = dt.Rows(iIt).Item(0).ToString
            '    Me.adselPanel.Controls.Add(chkInput)
            '    chkInput.BringToFront()
            '    X = X + 130

            'Next iIt

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub butCampCrtChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCampCrtChoose.Click
        Try
            Dim RadChecked As String = ""

            For Each ctl As Windows.Forms.Control In adselPanel.Controls

                If TypeOf ctl Is System.Windows.Forms.RadioButton Then

                    Dim cb As System.Windows.Forms.RadioButton = ctl

                    If cb.Checked = True Then

                        RadChecked = cb.Name.Replace("adchk", "")

                    End If

                End If

            Next


            AdminSettings.lblLogoNameEdit.Text = RadChecked
            AdminSettings.picboxLogoEdit.BackgroundImage = Image.FromFile(Application.StartupPath & "\LOGOS\" & RadChecked)
            'frmCampaign.txtSelectedAd.Text = RadChecked
            'frmCampaign.AdImageDisp(RadChecked)

            Me.Close()

        Catch ex As Exception
            errLog.WriteToErrorLog("Error", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub butCampCrtRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCampCrtRemove.Click
        Me.Close()
    End Sub
    Private Sub SetResolution()
        ' set resolution sub checks all the controls on the screen. Containers (tabcontrol, panel, ‘groupbox, tablelayoutpanel) do not resize on general control search for the form – so ‘they have to be done separate by name

        Dim perX, perY As Double, prvheight, prvWidth As Int32
        Dim shoAdd As Short
        Dim p_shoWhatSize As Double

        Dim desktopSize As Size = Windows.Forms.SystemInformation.PrimaryMonitorSize
        prvheight = desktopSize.Height
        prvWidth = desktopSize.Width

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

            ' do groupboxs separate also – separate for/next for each control by ‘name

            ' do panels separate also – separate for/next for each ‘panel by name



            For Each ctl As Control In adselPanel.Controls
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
    End Sub
End Class