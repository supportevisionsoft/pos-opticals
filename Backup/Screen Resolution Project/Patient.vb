Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class Patientfrm
    Inherits System.Windows.Forms.Form
    Dim db As New DBConnection
    Dim settingsType As String = ""
    Dim Location_Codes As New List(Of String)
    Dim Salesman_Codes As New List(Of String)
    Dim Custno As String
    Dim totds As New DataSet
    Dim totcount As Integer = 0
    Dim toti As Integer = 0
    Dim flagval As Integer
     


    Dim MySource_LocationCodes As New AutoCompleteStringCollection()
    Dim MySource_SalesManCodes As New AutoCompleteStringCollection()

    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.Dock = DockStyle.Fill
            SetResolution()
            btnPatientNext.Enabled = False
            btnPatientPrev.Enabled = False

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    
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

                For Each ctl As Control In SlitAndReadings.Controls
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
                For Each ctl As Control In RXContactLens.Controls
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

                For Each ctl As Control In TrailDetails.Controls
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

                For Each ctl As Control In pnlBottomHolder.Controls
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

                For Each ctl As Control In pnlpatientholder.Controls
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

                For Each ctl As Control In PnlPatientDetails.Controls
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

                For Each ctl As Control In PnlPatientCOntainer.Controls
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


                For Each ctl As Control In PnlPatientEdit.Controls
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
            Else
                
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnCounterMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each child As Form In Home.MdiChildren
                child.Close()
                child.Dispose()
            Next child
            SettingsCounterMaster.MdiParent = Home
            SettingsCounterMaster.Show()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientCancel.Click
        Try
            PnlPatientEdit.Hide()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPatientAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientAdd.Click
        Try
            txtPatPatientNo.ReadOnly = True
            txtSalesManCode_TextChanged(sender, e)
            dtPatientDOB.Value = Date.Now
            If RadPatMale.Checked = True Then
                RadPatFemale.Checked = False
            ElseIf RadPatFemale.Checked = True Then
                RadPatMale.Checked = False
            End If

            For Each ctl As Control In PnlPatientDetails.Controls
                Select Case ctl.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        With DirectCast(ctl, TextBox)
                            .ReadOnly = False
                            .BackColor = Color.White
                            .Text = ""
                        End With

                    Case "System.Windows.Forms.CheckBox"
                        With DirectCast(ctl, CheckBox)
                            .Enabled = True
                            .BackColor = Color.White
                            .CheckState = CheckState.Unchecked
                        End With
                    Case "System.Windows.Forms.DateTimePicker"
                        With DirectCast(ctl, DateTimePicker)
                            .Enabled = True
                            .BackColor = Color.White

                        End With
                    Case "System.Windows.Forms.RadioButton"
                        With DirectCast(ctl, RadioButton)
                            .Enabled = True
                            .BackColor = Color.White
                            .Checked = CheckState.Checked
                        End With
                    Case "System.Windows.Forms.TabControl"
                        With DirectCast(ctl, TabControl)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.GroupBox"
                        With DirectCast(ctl, GroupBox)
                            .Enabled = True
                            .BackColor = Color.White

                        End With
                End Select
            Next
            DateTimePicker2.Value = DateTime.Now

            For Each ctl In RX_GLASSESS.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.Readonly = False
                    ctl.Text = ""
                End If
            Next

            For Each ctl In RXContactLens.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.Readonly = False
                    ctl.Text = ""
                End If
            Next

            For Each ctl In SlitAndReadings.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.Readonly = False
                    ctl.Text = ""
                End If
            Next

            For Each ctl In TrailDetails.Controls
                If TypeOf ctl Is TextBox Then
                    ctl.Readonly = False
                    ctl.Text = ""
                End If
            Next
            btnPatientSaveNew.BringToFront()

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientNoSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
                    Exit For
                End If
            Next



            txtPatCustCode.Text = Setup_Values("CUST_CODE")
            txtPatCustCode.ReadOnly = True
            txtPatCustCode_TextChanged(sender, e)
            btnPatientEdit.Enabled = False
            btnPatientDelete.Enabled = False
            btnPatientSearch.Enabled = False
            btnPatientexit.BringToFront()
            btnPatientexit.Enabled = True
            txtPatPatientNo.ReadOnly = True
            RadPatFemale_CheckedChanged(sender, e)
            RadPatMale_CheckedChanged(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub


    Private Sub txtPatCustCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatCustCode.TextChanged
        Try
            Dim stQuery As String
            Dim ds As DataSet
            stQuery = "select cust_name from om_customer where cust_code='" & txtPatCustCode.Text & "'"
            ds = db.SelectFromTableODBC(stQuery)
            If ds.Tables("Table").Rows.Count > 0 Then
                txtPatCustName.Text = ds.Tables("Table").Rows.Item(0).Item(0).ToString
            Else
                txtPatCustName.Text = ""
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientSaveNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientSaveNew.Click
        Try
            txtPatPatientNo.ReadOnly = True
            Dim stQuery As String
            Dim gender As String
            Dim DOB As String

            If txtPatPatientName.Text = "" Then
                MsgBox("Please enter Patient Name!")
                Exit Sub
            End If
            gender = "MALE"
            If RadPatMale.Checked = True Then
                gender = "MALE"
            ElseIf RadPatFemale.Checked = True Then
                gender = "FEMALE"
            End If


            DOB = Format(dtPatientDOB.Value, "dd-MMM-yyyy")
            'End If


            Dim ds As DataSet
            Dim Custno As String = ""
            Dim Patno As String = ""
            stQuery = "SELECT PM_SYS_ID.NEXTVAL as patno FROM DUAL"
            ds = db.SelectFromTableODBC(stQuery)

            If ds.Tables("Table").Rows.Count > 0 Then
                Custno = Location_Code & "-" & ds.Tables("Table").Rows.Item(0).Item(0).ToString
                Patno = ds.Tables("Table").Rows.Item(0).Item(0).ToString
                txtPatPatientNo.Text = Custno
                'txtPatPatientNo_TextChanged(sender, e)
            End If

            stQuery = "INSERT INTO OM_PATIENT_MASTER(PM_SYS_ID,PM_COMP_CODE,PM_COUNTER_CODE,PM_CR_UID,PM_CR_DT,PM_LOCN_CODE,PM_SM_CODE,PM_CUST_NO,PM_PATIENT_NAME,PM_GENDER,PM_DOB,PM_CITY,PM_ZIPCODE,PM_NATIONALITY, PM_OCCUPATION,PM_COMPANY,PM_TEL_MOB,PM_EMAIL,PM_TEL_OFF,PM_TEL_RES,PM_NOTES,PM_REMARKS,PM_CUST_CODE,PM_CUST_NAME,PM_FRZ_FLAG_NUM) VALUES ("
            stQuery = stQuery & "'" & Patno & "','" & CompanyCode & "','" & POSCounterNumber & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'),'" & Location_Code & "','" & txtSalesManCode.Text & "','" & Custno & "','" & txtPatPatientName.Text & "','" & gender & "','" & DOB & "','" & txtPatCity.Text & "','" & txtPatZipcode.Text & "','" & txtPatNation.Text & "','" & txtPatOccupation.Text & "','" & txtPatCompany.Text & "','" & txtPatMobile.Text & "','" & txtPatEmail.Text & "','" & txtPatTelOff.Text & "','" & txtPatTelRes.Text & "','" & txtPatNotes.Text & "','" & txtPatRemarks.Text & "','" & txtPatCustCode.Text & "','" & txtPatCustName.Text & "',2)"
            errLog.WriteToErrorLog("Query OM_PATIENT_MASTER", stQuery, "")
            db.SaveToTableODBC(stQuery)


            stQuery = "INSERT INTO OM_PAT_RX_GLASSES(PRXG_SYS_ID,PRXG_PM_SYS_ID,PRXG_COMP_CODE,PRXG_CR_UID,PRXG_CR_DT,PRXG_DATE,PRXG_LOCN_CODE,PRXG_COUNTER_CODE,PRXG_SM_CODE,PRXG_R_D_SPH,PRXG_R_D_CYL,PRXG_R_D_AXIS,PRXG_R_D_VISION,PRXG_R_N_SPH,PRXG_R_N_CYL,PRXG_R_N_AXIS,PRXG_R_N_VISION,PRXG_R_PD,PRXG_L_D_SPH,PRXG_L_D_CYL,PRXG_L_D_AXIS,PRXG_L_D_VISION,PRXG_L_N_SPH,PRXG_L_N_CYL,PRXG_L_N_AXIS,PRXG_L_N_VISION,PRXG_L_PD,PRXG_FRZ_FLAG_NUM) VALUES ("
            stQuery = stQuery & "PRXG_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & LogonUser & "',to_date(sysdate,'DD-MM-YY'),to_date(sysdate,'DD-MM-YY'),'" & Location_Code & "','" & POSCounterNumber & "','" & txtSalesManCode.Text & "','" & txtRXG_RE_Sph_D1.Text & "','" & txtRXG_RE_Cyl_D1.Text & "','" & txtRXG_RE_Axi_D1.Text & "','" & txtRXG_RE_Vis_D1.Text & "','" & txtRXG_RE_Sph_N1.Text & "','" & txtRXG_RE_Cyl_N1.Text & "','" & txtRXG_RE_Axi_N1.Text & "','" & txtRXG_RE_Vis_N1.Text & "','" & txtRXG_LE_IPD_D1.Text & "','" & txtRXG_LE_Sph_D1.Text & "','" & txtRXG_LE_Cyl_D1.Text & "','" & txtRXG_LE_Axi_D1.Text & "','" & txtRXG_LE_Vis_D1.Text & "','" & txtRXG_LE_Sph_N1.Text & "','" & txtRXG_LE_Cyl_N1.Text & "','" & txtRXG_LE_Axi_N1.Text & "','" & txtRXG_LE_Vis_N1.Text & "','" & txtRXG_LE_IPD_N1.Text & "',2)"
            errLog.WriteToErrorLog("Query OM_PAT_RX_GLASSES", stQuery, "")
            db.SaveToTableODBC(stQuery)




            stQuery = "insert into OM_PAT_RX_CONTACT_LENS(PRXCL_SYS_ID,PRXCL_PM_SYS_ID,PRXCL_COMP_CODE, PRXCL_LOCN_CODE,PRXCL_COUNTER_CODE, PRXCL_SM_CODE,PRXCL_R_I_BCOR, PRXCL_R_I_DIA,PRXCL_R_I_POWER,PRXCL_R_II_BCOR,PRXCL_R_II_DIA,PRXCL_R_II_POWER,PRXCL_R_BRAND,PRXCL_L_I_BCOR,PRXCL_L_I_DIA, PRXCL_L_I_POWER,PRXCL_L_II_BCOR,PRXCL_L_II_DIA,PRXCL_L_II_POWER,PRXCL_L_BRAND, PRXCL_CR_UID,PRXCL_CR_DT,PRXCL_FRZ_FLAG_NUM,PRXCL_DATE)Values("
            stQuery = stQuery & "PRXCL_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & txtSalesManCode.Text & "','" & txtRXC_RE_Bcor_I.Text & "','" & txtRXC_RE_Dia_I.Text & "','" & txtRXC_RE_Pow_I.Text & "','" & txtRXC_RE_Bcor_II.Text & "','" & txtRXC_RE_Dia_II.Text & "','" & txtRXC_RE_Pow_II.Text & "','" & txtRXC_RE_Brand1.Text & "','" & txtRXC_LE_Bcor_I.Text & "','" & txtRXC_LE_Dia_I.Text & "','" & txtRXC_LE_Pow_I.Text & "','" & txtRXC_LE_Bcor_II.Text & "','" & txtRXC_LE_Dia_II.Text & "','" & txtRXC_LE_Pow_II.Text & "','" & txtRXC_LE_Brand2.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_CONTACT_LENS", stQuery, "")
            db.SaveToTableODBC(stQuery)


            stQuery = "insert into OM_PAT_RX_SLITK_READING(PRXSKR_SYS_ID,PRXSKR_PM_SYS_ID, PRXSKR_COMP_CODE,PRXSKR_LOCN_CODE,PRXSKR_COUNTER_CODE,PRXSKR_SM_CODE,PRXSKR_SLIT_RE,PRXSKR_SLIT_LE, PRXSKR_SLIT_LRIS,PRXSKR_K_RE_HORIZONTAL,PRXSKR_K_LE_HORIZONTAL,PRXSKR_K_RE_VERTICAL,PRXSKR_K_LE_VERTICAL,PRXSKR_CR_UID,PRXSKR_CR_DT,PRXSKR_FRZ_FLAG_NUM,PRXSKR_DATE)values("
            stQuery = stQuery & "PRXSKR_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & txtSalesManCode.Text & "','" & txtSlit_Re.Text & "','" & txtSlit_Le.Text & "','" & txtSlit_LrisDia.Text & "','" & txtK_Re_Hori.Text & "','" & txtK_Le_Hori.Text & "','" & txtK_Re_Vert.Text & "','" & txtK_Le_Vert.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_SLITK_READING", stQuery, "")
            db.SaveToTableODBC(stQuery)


            stQuery = "insert into OM_PAT_RX_TRIAL_DETAILS(PRXTD_SYS_ID,PRXTD_PM_SYS_ID,PRXTD_COMP_CODE,PRXTD_LOCN_CODE, PRXTD_COUNTER_CODE, PRXTD_SM_CODE,PRXTD_LENS_USED_RE,PRXTD_LENS_USED_RE_ADD,PRXTD_LENS_USED_RE_VIA,PRXTD_LENS_USED_LE, PRXTD_LENS_USED_LE_ADD, PRXTD_LENS_USED_LE_VIA,PRXTD_RE_REMARKS, PRXTD_LE_REMARKS,PRXTD_CR_UID,PRXTD_CR_DT,PRXTD_FRZ_FLAG_NUM, PRXTD_DATE)values("
            stQuery = stQuery & " PRXTD_SYS_ID.NEXTVAL," & Patno & ",'" & CompanyCode & "','" & Location_Code & "','" & POSCounterNumber & "','" & txtSalesManCode.Text & "','" & txtTrail_Re.Text & "','" & txtTrail_Re_Add.Text & "','" & txtTrail_Re_Via.Text & "','" & txtTrail_Le.Text & "','" & txtTrail_Le_Add.Text & "','" & txtTrail_Le_Via.Text & "','" & txtTrail_Re_Remarks.Text & "','" & txtTrail_Le_Remarks.Text & "','" & LogonUser & "',to_date(sysdate,'dd-MM-yy'),2,to_date(sysdate,'dd-MM-yy'))"
            errLog.WriteToErrorLog("Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "")
            db.SaveToTableODBC(stQuery)

            MsgBox("Patient Details Added!")

            btnPatientAdd.BringToFront()
            btnPatientEdit.Enabled = True
            btnPatientDelete.Enabled = True



        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")

        End Try
    End Sub
    Private Sub btnPatientUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientUpdate.Click

        Try
            txtPatPatientNo.ReadOnly = True
            Dim stQuery, gender, DOB As String
            gender = "MALE"
            If RadPatMale.Checked = True Then
                gender = "MALE"
            ElseIf RadPatFemale.Checked = True Then
                gender = "FEMALE"
            End If


            DOB = Format(DateTimePicker2.Value, "dd-MMM-yyyy")

            stQuery = "update om_patient_master set PM_GENDER='" & gender & "',PM_DOB= '" & DOB & "',PM_PATIENT_NAME= '" & txtPatPatientName.Text & "',PM_CITY = '" & txtPatCity.Text & "',PM_ZIPCODE = '" & txtPatZipcode.Text & "',PM_TEL_OFF='" & txtPatTelOff.Text & "',PM_TEL_RES='" & txtPatTelRes.Text & "',PM_TEL_MOB='" & txtPatMobile.Text & "',PM_EMAIL='" & txtPatEmail.Text & "',PM_NATIONALITY='" & txtPatNation.Text & "',PM_COMPANY='" & txtPatCompany.Text & "',pm_occupation='" & txtPatOccupation.Text & "',PM_REMARKS='" & txtPatRemarks.Text & "',PM_NOTES='" & txtPatNotes.Text & "',PM_UPD_UID='" & LogonUser & "',PM_UPD_DT=sysdate  where pm_cust_no='" & txtPatPatientNo.Text & "'"
            errLog.WriteToErrorLog("Update Query OM_PATIENT_MASTER", stQuery, "")
            db.SaveToTableODBC(stQuery)


            stQuery = "update  OM_PAT_RX_GLASSES set PRXG_R_D_SPH='" & txtRXG_RE_Sph_D1.Text & "',PRXG_R_D_CYL='" & txtRXG_RE_Cyl_D1.Text & "',PRXG_R_D_AXIS='" & txtRXG_RE_Axi_D1.Text & "',PRXG_R_D_VISION='" & txtRXG_RE_Vis_D1.Text & "',PRXG_R_N_SPH='" & txtRXG_RE_Sph_N1.Text & "',PRXG_R_N_CYL='" & txtRXG_RE_Cyl_N1.Text & "',PRXG_R_N_AXIS='" & txtRXG_RE_Axi_N1.Text & "',PRXG_R_N_VISION='" & txtRXG_RE_Vis_N1.Text & "',PRXG_R_PD='" & txtRXG_LE_IPD_D1.Text & "',PRXG_L_D_SPH='" & txtRXG_LE_Sph_D1.Text & "',PRXG_L_D_CYL='" & txtRXG_LE_Cyl_D1.Text & "',PRXG_L_D_AXIS='" & txtRXG_LE_Axi_D1.Text & "',PRXG_L_D_VISION='" & txtRXG_LE_Vis_D1.Text & "',PRXG_L_N_SPH='" & txtRXG_LE_Sph_N1.Text & "',PRXG_L_N_CYL='" & txtRXG_LE_Cyl_N1.Text & "',PRXG_L_N_AXIS='" & txtRXG_LE_Axi_N1.Text & "',PRXG_L_N_VISION='" & txtRXG_LE_Vis_N1.Text & "',PRXG_L_PD='" & txtRXG_LE_IPD_N1.Text & "',PRXG_UPD_UID='" & LogonUser & "',PRXG_UPD_DT=sysdate where PRXG_SYS_ID=( select PRXG_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID)"
            errLog.WriteToErrorLog("Update Query RXGlasses", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_CONTACT_LENS set PRXCL_R_I_BCOR='" & txtRXC_RE_Bcor_I.Text & "', PRXCL_R_I_DIA='" & txtRXC_RE_Dia_I.Text & "',PRXCL_R_I_POWER='" & txtRXC_RE_Pow_I.Text & "',PRXCL_R_II_BCOR='" & txtRXC_RE_Bcor_II.Text & "',PRXCL_R_II_DIA='" & txtRXC_RE_Dia_II.Text & "',PRXCL_R_II_POWER='" & txtRXC_RE_Pow_II.Text & "',PRXCL_R_BRAND='" & txtRXC_RE_Brand1.Text & "',PRXCL_L_I_BCOR='" & txtRXC_LE_Bcor_I.Text & "',PRXCL_L_I_DIA='" & txtRXC_LE_Dia_I.Text & "', PRXCL_L_I_POWER='" & txtRXC_LE_Pow_I.Text & "',PRXCL_L_II_BCOR='" & txtRXC_LE_Bcor_II.Text & "',PRXCL_L_II_DIA='" & txtRXC_LE_Dia_II.Text & "',PRXCL_L_II_POWER='" & txtRXC_LE_Pow_II.Text & "',PRXCL_L_BRAND='" & txtRXC_LE_Brand2.Text & "',PRXCL_UPD_UID='" & LogonUser & "',PRXCL_UPD_DT=sysdate where PRXCL_SYS_ID=( select PRXCL_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID)"
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_CONTACT_LENS", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_SLITK_READING set PRXSKR_SLIT_RE='" & txtSlit_Re.Text & "',PRXSKR_SLIT_LE='" & txtSlit_Le.Text & "', PRXSKR_SLIT_LRIS='" & txtSlit_LrisDia.Text & "',PRXSKR_K_RE_HORIZONTAL='" & txtK_Re_Hori.Text & "',PRXSKR_K_LE_HORIZONTAL='" & txtK_Le_Hori.Text & "',PRXSKR_K_RE_VERTICAL='" & txtK_Re_Vert.Text & "',PRXSKR_K_LE_VERTICAL='" & txtK_Le_Vert.Text & "', PRXSKR_UPD_UID='" & LogonUser & "', PRXSKR_UPD_DT=sysdate where PRXSKR_SYS_ID=( select PRXSKR_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID) "
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_SLITK_READING", stQuery, "")
            db.SaveToTableODBC(stQuery)

            stQuery = "update OM_PAT_RX_TRIAL_DETAILS set PRXTD_LENS_USED_RE='" & txtTrail_Re.Text & "',PRXTD_LENS_USED_RE_ADD='" & txtTrail_Re_Add.Text & "',PRXTD_LENS_USED_RE_VIA='" & txtTrail_Re_Via.Text & "',PRXTD_LENS_USED_LE='" & txtTrail_Le.Text & "', PRXTD_LENS_USED_LE_ADD='" & txtTrail_Le_Add.Text & "', PRXTD_LENS_USED_LE_VIA='" & txtTrail_Le_Via.Text & "',PRXTD_RE_REMARKS='" & txtTrail_Re_Remarks.Text & "', PRXTD_LE_REMARKS='" & txtTrail_Le_Remarks.Text & "', PRXTD_UPD_UID='" & LogonUser & "',PRXTD_UPD_DT=sysdate where PRXTD_SYS_ID=( select PRXTD_SYS_ID from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXTD_PM_SYS_ID) "
            errLog.WriteToErrorLog("Update Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "")
            db.SaveToTableODBC(stQuery)

            MsgBox("Updated Sucessfully!")

            For Each ctl As Control In PnlPatientDetails.Controls
                Select Case ctl.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        With DirectCast(ctl, TextBox)
                            .ReadOnly = True
                            .BackColor = Color.White
                        End With

                    Case "System.Windows.Forms.CheckBox"
                        With DirectCast(ctl, CheckBox)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.DateTimePicker"
                        With DirectCast(ctl, DateTimePicker)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.RadioButton"
                        With DirectCast(ctl, RadioButton)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.TabControl"
                        With DirectCast(ctl, TabControl)
                            .Enabled = False
                            .BackColor = Color.White
                        End With
                End Select
            Next

            btnPatientAdd.BringToFront()
            ' txtPatPatientNo.Enabled = True
            txtPatCustCode.ReadOnly = True
            txtPatCustName.ReadOnly = True
            txtPatPatientNo.ReadOnly = True

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try


    End Sub

    'Private Sub txtPatPatientNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatPatientNo.TextChanged
    '    Try
    '        If txtPatPatientNo.Text = "" Then
    '            Exit Sub
    '        End If

    '        Dim stQuery As String = ""
    '        Dim ds As DataSet
    '        Dim count As Integer
    '        Dim row As System.Data.DataRow
    '        Dim i As Integer
    '        i = 0
    '        stQuery = "select PM_CUST_CODE as custcode,CUST_NAME as customername,PM_CUST_NAME as pcustname,PM_PATIENT_NAME as patientname,PM_GENDER as gender,to_char(PM_DOB,'dd/mm/yyyy') as dob,PM_CITY as city,PM_ZIPCODE as zipcode,PM_TEL_OFF as offphn,PM_TEL_RES as resphn,PM_TEL_MOB as mobphn,PM_EMAIL as pemail,PM_NATIONALITY as pnationality,PM_COMPANY as pcompany,pm_occupation as occupation,PM_REMARKS as premarks,PM_NOTES as pnotes from om_patient_master a, om_customer b where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE"
    '        ds = db.SelectFromTableODBC(stQuery)
    '        count = ds.Tables("Table").Rows.Count
    '        If count > 0 Then
    '            row = ds.Tables("Table").Rows.Item(i)
    '            txtPatCustCode.Text = row.Item(0).ToString
    '            txtPatCustName.Text = row.Item(2).ToString
    '            txtPatPatientName.Text = row.Item(3).ToString
    '            txtPatCity.Text = row.Item(6).ToString
    '            txtPatZipcode.Text = row.Item(7).ToString
    '            txtPatTelOff.Text = row.Item(8).ToString
    '            txtPatTelRes.Text = row.Item(9).ToString
    '            txtPatMobile.Text = row.Item(10).ToString
    '            txtPatEmail.Text = row.Item(11).ToString
    '            txtPatNation.Text = row.Item(12).ToString
    '            txtPatCompany.Text = row.Item(13).ToString
    '            txtPatOccupation.Text = row.Item(14).ToString
    '            txtPatRemarks.Text = row.Item(15).ToString
    '            txtPatNotes.Text = row.Item(16).ToString
    '            i = i + 1
    '            count = count - 1
    '        End If

    '        i = 0
    '        stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
    '        ds = db.SelectFromTableODBC(stQuery)
    '        count = ds.Tables("Table").Rows.Count
    '        If count > 0 Then
    '            row = ds.Tables("Table").Rows.Item(i)
    '            txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
    '            txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
    '            txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
    '            txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
    '            txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
    '            txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
    '            txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
    '            txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
    '            txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
    '            txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
    '            txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
    '            txtRXG_LE_Vis_D1.Text = row.Item(11).ToString
    '            txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
    '            txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
    '            txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
    '            txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
    '            txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
    '            txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

    '            i = i + 1
    '            count = count - 1
    '            Detailscontainer.Enabled = True
    '        Else

    '        End If


    '        i = 0
    '        stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
    '        ds = db.SelectFromTableODBC(stQuery)
    '        count = ds.Tables("Table").Rows.Count
    '        If count > 0 Then
    '            row = ds.Tables("Table").Rows.Item(i)
    '            txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
    '            txtRXC_RE_Dia_I.Text = row.Item(1).ToString
    '            txtRXC_RE_Pow_I.Text = row.Item(2).ToString
    '            txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
    '            txtRXC_RE_Dia_II.Text = row.Item(4).ToString
    '            txtRXC_RE_Pow_II.Text = row.Item(5).ToString
    '            txtRXC_LE_Pow_II.Text = row.Item(6).ToString

    '            txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
    '            txtRXC_LE_Dia_I.Text = row.Item(8).ToString
    '            txtRXC_LE_Pow_I.Text = row.Item(9).ToString
    '            txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
    '            txtRXC_LE_Dia_II.Text = row.Item(11).ToString
    '            txtRXC_LE_Pow_II.Text = row.Item(12).ToString
    '            txtRXC_LE_Brand2.Text = row.Item(13).ToString
    '            i = i + 1
    '            count = count - 1
    '            Detailscontainer.Enabled = True
    '        End If

    '        i = 0
    '        stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
    '        ds = db.SelectFromTableODBC(stQuery)
    '        count = ds.Tables("Table").Rows.Count
    '        If count > 0 Then
    '            row = ds.Tables("Table").Rows.Item(i)
    '            txtSlit_Re.Text = row.Item(0).ToString
    '            txtSlit_Le.Text = row.Item(1).ToString
    '            txtSlit_LrisDia.Text = row.Item(2).ToString
    '            txtK_Re_Hori.Text = row.Item(3).ToString
    '            txtK_Le_Hori.Text = row.Item(4).ToString
    '            txtK_Re_Vert.Text = row.Item(5).ToString
    '            txtK_Le_Vert.Text = row.Item(6).ToString
    '            i = i + 1
    '            count = count - 1
    '            Detailscontainer.Enabled = True
    '        End If

    '        i = 0
    '        stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
    '        ds = db.SelectFromTableODBC(stQuery)
    '        count = ds.Tables("Table").Rows.Count
    '        If count > 0 Then
    '            row = ds.Tables("Table").Rows.Item(i)
    '            txtTrail_Re.Text = row.Item(0).ToString
    '            txtTrail_Re_Add.Text = row.Item(1).ToString
    '            txtTrail_Re_Via.Text = row.Item(2).ToString
    '            txtTrail_Le.Text = row.Item(3).ToString
    '            txtTrail_Le_Add.Text = row.Item(4).ToString
    '            txtTrail_Le_Via.Text = row.Item(5).ToString
    '            txtTrail_Re_Remarks.Text = row.Item(6).ToString
    '            txtTrail_Le_Remarks.Text = row.Item(7).ToString
    '            i = i + 1
    '            count = count - 1
    '            Detailscontainer.Enabled = True
    '        End If
    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientQuery" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next

    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientTelOffSearch" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next

    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientMobileSearch" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next

    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientNameSearch" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next

    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientTelResSearch" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next

    '        'For Each ctl As Control In PnlPatientDetails.Controls
    '        '    If ctl.Name = "btnPatientEmailSearch" Then
    '        '        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
    '        '        Exit For
    '        '    End If
    '        'Next
    '    Catch ex As Exception
    '        errLog.WriteToErrorLog("Error", ex.ToString, ex.StackTrace)
    '    End Try

    'End Sub

    Private Sub txtSalesManCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesManCode.TextChanged

        Try
            Dim ds As DataSet
            Dim count As Integer
            Dim row As System.Data.DataRow
            Dim stQuery As String
            Dim i As Integer

            'stQuery = "select distinct pm_sm_code from om_patient_master a,om_salesman b where a.pm_sm_code=b.sm_code"
            'stQuery = "select pm_sm_code from om_patient_master"
            'stQuery = "SELECT SM_NAME  FROM OM_SALESMAN WHERE SM_CODE='" & txtSalesManCode.Text & "' AND SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"
            stQuery = "SELECT SM_CODE  FROM OM_SALESMAN WHERE  SM_FRZ_FLAG_NUM = 2 AND SM_CODE IN (SELECT SMC_CODE FROM OM_SALESMAN_COMP WHERE SMC_COMP_CODE = '" & CompanyCode & "' AND SMC_FRZ_FLAG_NUM = 2) AND SM_CODE IN (SELECT SMC_CODE FROM OM_POS_SALESMAN_COUNTER WHERE SMC_LOCN_CODE = '" & Location_Code & "' AND SMC_COUNT_CODE = '" & POSCounterNumber & "' AND SMC_FRZ_FLAG_NUM = 2) ORDER BY SM_CODE"

            ds = db.SelectFromTableODBC(stQuery)
            errLog.WriteToErrorLog("LocationQuery", stQuery, "")
            count = ds.Tables("Table").Rows.Count
            i = 0
            While count > 0
                row = ds.Tables("Table").Rows.Item(i)
                Salesman_Codes.Add(row.Item(0).ToString)
                count = count - 1
                i = i + 1
            End While
            MySource_SalesManCodes.AddRange(Location_Codes.ToArray)
            txtSalesManCode.AutoCompleteCustomSource = MySource_SalesManCodes
            txtSalesManCode.AutoCompleteMode = AutoCompleteMode.Append
            txtSalesManCode.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtSalesManCode.Text = row.Item(0).ToString

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPatientEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientEdit.Click

        Try
            txtPatPatientNo.ReadOnly = True
            If txtPatPatientNo.Text = "" Then
                MsgBox("Please select a valid patient")
            Else
                btnPatientEdit.Enabled = False
                btnPatientSearch.Enabled = False
                btnPatientDelete.Enabled = False
                btnPatientAdd.SendToBack()
                btnPatientSaveNew.SendToBack()
                btnPatientexit.BringToFront()
                Exit Sub
            End If

            txtPatCompany.ReadOnly = False



            For Each ctl As Control In PnlPatientDetails.Controls
                Select Case ctl.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        With DirectCast(ctl, TextBox)
                            .ReadOnly = False
                            .BackColor = Color.White
                        End With

                    Case "System.Windows.Forms.CheckBox"
                        With DirectCast(ctl, CheckBox)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.DateTimePicker"
                        With DirectCast(ctl, DateTimePicker)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.RadioButton"
                        With DirectCast(ctl, RadioButton)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.TabControl"
                        With DirectCast(ctl, TabControl)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                    Case "System.Windows.Forms.GroupBox"
                        With DirectCast(ctl, GroupBox)
                            .Enabled = True
                            .BackColor = Color.White
                        End With
                End Select
            Next

            For Each ctl In RX_GLASSESS.Controls
                If TypeOf ctl Is TextBox Then
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                    End With
                End If
            Next


            For Each ctl In RXContactLens.Controls
                If TypeOf ctl Is TextBox Then
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                    End With
                End If
            Next

            For Each ctl In SlitAndReadings.Controls
                If TypeOf ctl Is TextBox Then
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                    End With
                End If
            Next

            For Each ctl In TrailDetails.Controls
                If TypeOf ctl Is TextBox Then
                    With DirectCast(ctl, TextBox)
                        .ReadOnly = False
                        .BackColor = Color.White
                    End With
                End If
            Next
            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientQuery" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelOffSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientMobileSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientNameSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientTelResSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientEmailSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                    Exit For
                End If
            Next

            For Each ctl As Control In PnlPatientDetails.Controls
                If ctl.Name = "btnPatientNoSearch" Then
                    PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
                    Exit For
                End If
            Next

            txtPatPatientNo.ReadOnly = True
            txtPatCustCode.ReadOnly = True
            txtPatCustName.ReadOnly = True

            txtPatCompany.ReadOnly = False
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub btnPatientDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientDelete.Click
        Try
            If txtPatPatientNo.Text = "" Then
                MsgBox("Please select a valid Patient No.")
            Else
                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNoSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
                        Exit For
                    End If
                Next

                Dim stQuery As String
                Dim ds As DataSet
                stQuery = "Select * from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "'"
                errLog.WriteToErrorLog("Delete Query Check om_patient_master", stQuery, "Error")
                ds = db.SelectFromTableODBC(stQuery)
                If ds.Tables("Table").Rows.Count > 0 Then
                    stQuery = "Delete from OM_PAT_RX_CONTACT_LENS where PRXCL_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "')"
                    errLog.WriteToErrorLog("Delete Query OM_PAT_RX_CONTACT_LENS", stQuery, "Error")
                    db.SaveToTableODBC(stQuery)
                    stQuery = "Delete from OM_PAT_RX_TRIAL_DETAILS where PRXTD_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "')"
                    errLog.WriteToErrorLog("Delete Query OM_PAT_RX_TRIAL_DETAILS", stQuery, "Error")
                    db.SaveToTableODBC(stQuery)
                    stQuery = "Delete from OM_PAT_RX_GLASSES where PRXG_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "')"
                    errLog.WriteToErrorLog("Delete Query OM_PAT_RX_GLASSES", stQuery, "Error")
                    db.SaveToTableODBC(stQuery)
                    stQuery = "Delete from OM_PAT_RX_SLITK_READING where PRXSKR_PM_SYS_ID=(select PM_SYS_ID from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "')"
                    errLog.WriteToErrorLog("Delete Query OM_PAT_RX_SLITK_READING", stQuery, "Error")
                    db.SaveToTableODBC(stQuery)
                    stQuery = "Delete from om_patient_master where pm_cust_no='" & txtPatPatientNo.Text & "'"
                    errLog.WriteToErrorLog("Delete Query om_patient_master", stQuery, "Error")
                    db.SaveToTableODBC(stQuery)
                    MsgBox("Patient Deleted Successfully!")
                    txtPatPatientNo.Text = ""
                    Home.RefreshPatient(sender, e)
                Else
                    MsgBox("Please select a valid Patient")
                End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientSearch.Click
        Try
            totcount = 0
            toti = 0
            totds.Clear()
            'btnPatientPrev.Enabled = False
            'btnPatientNext.Enabled = False
            btnPatientPrev.Visible = False
            btnPatientNext.Visible = False

            Dim btn As New Button
            txtPatPatientNo.ReadOnly = False
            txtPatPatientNo.Enabled = True

            With btn
                .Name = "btnPatientQuery"
                .Location = New Point(btnPatientSearch.Location.X, btnPatientSearch.Location.Y)
                .Size = New Size(btnPatientSearch.Width, btnPatientSearch.Height)
                .Text = "Query"
                .TextAlign = ContentAlignment.BottomCenter
                .Image = My.Resources.recycle_bin_icon
                .ImageAlign = ContentAlignment.TopCenter
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .BringToFront()
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuery_Click
            Me.PnlPatientCOntainer.Controls.Add(btn)
            btnPatientSearch.SendToBack()

            btn = New Button
            With btn
                .Name = "btnPatientTelOffSearch"
                .Location = New Point(txtPatTelOff.Location.X + txtPatTelOff.Width, txtPatTelOff.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)


            'Patient No Search
            btn = New Button
            With btn
                .Name = "btnPatientNoSearch"
                .Location = New Point(txtPatPatientNo.Location.X + txtPatPatientNo.Width, txtPatPatientNo.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnPatientMobileSearch"
                .Location = New Point(txtPatMobile.Location.X + txtPatMobile.Width, txtPatMobile.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnPatientNameSearch"
                .Location = New Point(txtPatPatientName.Location.X + txtPatPatientName.Width, txtPatPatientName.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnPatientTelResSearch"
                .Location = New Point(txtPatTelRes.Location.X + txtPatTelRes.Width, txtPatTelRes.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)

            btn = New Button
            With btn
                .Name = "btnPatientEmailSearch"
                .Location = New Point(txtPatEmail.Location.X + txtPatEmail.Width, txtPatEmail.Location.Y)
                .Size = New Size(20, 20)
                .Image = My.Resources.search
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
            End With
            AddHandler btn.Click, AddressOf Me.btnPatientQuerySearch_Click
            Me.PnlPatientDetails.Controls.Add(btn)

            For Each ctl As Control In PnlPatientDetails.Controls
                Select Case ctl.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        With DirectCast(ctl, TextBox)
                            .ReadOnly = True
                            .BackColor = Color.White
                            .Text = ""
                        End With
                End Select
            Next

            txtPatPatientName.ReadOnly = False
            txtPatPatientNo.ReadOnly = False
            txtPatTelOff.ReadOnly = False
            txtPatMobile.ReadOnly = False
            txtPatTelRes.ReadOnly = False
            txtPatEmail.ReadOnly = False

            btnPatientAdd.Enabled = False
            btnPatientEdit.Enabled = False
            btnPatientDelete.Enabled = False

        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try



            Dim ds As New DataSet
            Dim i As Integer
            Dim stQuery As String
            Dim row As System.Data.DataRow
            Dim patientno As String
            Dim count As String

            i = 0
            flagval = 0
            'txtPatientNo.Text = ""
            stQuery = "select PM_CUST_CODE as custcode,CUST_NAME as customername,PM_CUST_NAME as pcustname,PM_PATIENT_NAME as patientname,PM_GENDER as gender,to_char(PM_DOB,'dd/mm/yyyy') as dob,PM_CITY as city,PM_ZIPCODE as zipcode,PM_TEL_OFF as offphn,PM_TEL_RES as resphn,PM_TEL_MOB as mobphn,PM_EMAIL as pemail,PM_NATIONALITY as pnationality,PM_COMPANY as pcompany,pm_occupation as occupation,PM_REMARKS as premarks,PM_NOTES as pnotes,pm_cust_no from om_patient_master a, om_customer b where a.PM_CUST_CODE=b.CUST_CODE"

            If txtPatPatientName.Text <> "" Then
                stQuery = stQuery + " and PM_PATIENT_NAME='" & txtPatPatientName.Text & "'"
                flagval = 1
            End If
            If txtPatTelOff.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_OFF='" & txtPatTelOff.Text & "'"
                flagval = 1
            End If
            If txtPatTelRes.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_RES='" & txtPatTelRes.Text & "'"
                flagval = 1
            End If
            If txtPatMobile.Text <> "" Then
                stQuery = stQuery + " and PM_TEL_MOB='" & txtPatMobile.Text & "'"
                flagval = 1
            End If
            If txtPatEmail.Text <> "" Then
                stQuery = stQuery + " and PM_EMAIL='" & txtPatEmail.Text & "'"
                flagval = 1
            End If
            If txtPatPatientNo.Text <> "" Then
                stQuery = stQuery + " and CUST_NO='" & txtPatPatientNo.Text & "'"
                flagval = 1
            End If
            If flagval = 1 Then
                totds = db.SelectFromTableODBC(stQuery)
                totcount = totds.Tables("Table").Rows.Count
            End If

            If totcount = 0 Then
                For Each ctl As Control In PnlPatientDetails.Controls
                    Select Case ctl.GetType.ToString
                        Case "System.Windows.Forms.TextBox"
                            With DirectCast(ctl, TextBox)
                                .ReadOnly = False
                                .BackColor = Color.White
                                .Text = ""
                            End With
                    End Select
                Next

                For Each ctl In RX_GLASSESS.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                        MsgBox(ctl.Text)
                    End If
                Next


                For Each ctl In RXContactLens.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In SlitAndReadings.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                For Each ctl In TrailDetails.Controls
                    If TypeOf ctl Is TextBox Then
                        ctl.Readonly = False
                        ctl.Text = ""
                    End If
                Next

                MsgBox("No Record Found")
            ElseIf totcount = 1 Then
                btnPatientSearch.BringToFront()
                row = totds.Tables("Table").Rows.Item(toti)

                If row.Item(4).ToString = "MALE" Then
                    RadPatMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    RadPatFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                'txtPatientNo.Text = row.Item(17).ToString



                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                        Exit For
                    End If
                Next
                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNoSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
                        Exit For
                    End If
                Next

            ElseIf totcount > 1 Then
                btnPatientSearch.BringToFront()


                btnPatientNext.Enabled = True
                btnPatientNext.Visible = True
                btnPatientPrev.Visible = True
                row = totds.Tables("Table").Rows.Item(i)


                If row.Item(4).ToString = "MALE" Then
                    RadPatMale.Checked = True
                ElseIf row.Item(4).ToString = "FEMALE" Then
                    RadPatFemale.Checked = True
                End If

                txtPatCustCode.Text = row.Item(0).ToString
                txtPatCustName.Text = row.Item(2).ToString
                txtPatPatientName.Text = row.Item(3).ToString

                If row.Item(5).ToString = "" Then
                    dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
                Else
                    dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
                End If

                txtPatCity.Text = row.Item(6).ToString
                txtPatZipcode.Text = row.Item(7).ToString
                txtPatTelOff.Text = row.Item(8).ToString
                txtPatTelRes.Text = row.Item(9).ToString
                txtPatMobile.Text = row.Item(10).ToString
                txtPatEmail.Text = row.Item(11).ToString
                txtPatNation.Text = row.Item(12).ToString
                txtPatCompany.Text = row.Item(13).ToString
                txtPatOccupation.Text = row.Item(14).ToString
                txtPatRemarks.Text = row.Item(15).ToString
                txtPatNotes.Text = row.Item(16).ToString
                txtPatPatientNo.Text = row.Item(17).ToString
                patientno = row.Item(17).ToString
                'txtPatientNo.Text = row.Item(17).ToString

             

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientQuery" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelOffSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientMobileSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNameSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientTelResSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
                        Exit For
                    End If
                Next

                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientEmailSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
                        Exit For
                    End If
                Next
                For Each ctl As Control In PnlPatientDetails.Controls
                    If ctl.Name = "btnPatientNoSearch" Then
                        PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
                        Exit For
                    End If
                Next


                'btnPatientNext_Click(sender, e, count, i, row)
                'While count > 0
                '    row = ds.Tables("Table").Rows.Item(i)
                '    MsgBox(row.Item(11).ToString())
                '    i = i + 1
                'End While



            End If

            'btnPatientUpdateNew.Enabled = False
            btnPatientAdd.Enabled = True
            btnPatientEdit.Enabled = True
            btnPatientDelete.Enabled = True










            'Dim stQuery As String = ""
            'Dim ds As New DataSet
            'Dim count As Integer = 0
            'Dim row As System.Data.DataRow
            'Dim i As Integer
            'Dim flagval As Integer
            'Dim patientno As String
            'i = 0
            'flagval = 0
            'RadPatFemale.Enabled = True
            'RadPatMale.Enabled = True
            'dtPatientDOB.Enabled = True
            'stQuery = "select PM_CUST_CODE as custcode,CUST_NAME as customername,PM_CUST_NAME as pcustname,PM_PATIENT_NAME as patientname,PM_GENDER as gender,to_char(PM_DOB,'dd/mm/yyyy') as dob,PM_CITY as city,PM_ZIPCODE as zipcode,PM_TEL_OFF as offphn,PM_TEL_RES as resphn,PM_TEL_MOB as mobphn,PM_EMAIL as pemail,PM_NATIONALITY as pnationality,PM_COMPANY as pcompany,pm_occupation as occupation,PM_REMARKS as premarks,PM_NOTES as pnotes,pm_cust_no from om_patient_master a, om_customer b where a.PM_CUST_CODE=b.CUST_CODE"

            'If txtPatPatientName.Text <> "" Then
            '    stQuery = stQuery + " and PM_PATIENT_NAME='" & txtPatPatientName.Text & "'"
            '    flagval = 1
            'End If
            'If txtPatTelOff.Text <> "" Then
            '    stQuery = stQuery + " and PM_TEL_OFF='" & txtPatTelOff.Text & "'"
            '    flagval = 1
            'End If
            'If txtPatTelRes.Text <> "" Then
            '    stQuery = stQuery + " and PM_TEL_RES='" & txtPatTelRes.Text & "'"
            '    flagval = 1
            'End If
            'If txtPatMobile.Text <> "" Then
            '    stQuery = stQuery + " and PM_TEL_MOB='" & txtPatMobile.Text & "'"
            '    flagval = 1
            'End If
            'If txtPatEmail.Text <> "" Then
            '    stQuery = stQuery + " and PM_EMAIL='" & txtPatEmail.Text & "'"
            '    flagval = 1
            'End If
            'If txtPatPatientNo.Text <> "" Then
            '    stQuery = stQuery + " and PM_CUST_NO='" & txtPatPatientNo.Text & "'"
            '    flagval = 1
            'End If
            'If flagval = 1 Then
            '    totds = db.SelectFromTableODBC(stQuery)
            '    totcount = totds.Tables("Table").Rows.Count
            'End If

            'If totcount > 0 Then
            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        Select Case ctl.GetType.ToString
            '            Case "System.Windows.Forms.TextBox"
            '                With DirectCast(ctl, TextBox)
            '                    .ReadOnly = False
            '                    .BackColor = Color.White
            '                    .Text = ""
            '                End With
            '        End Select
            '    Next

            '    For Each ctl In RX_GLASSESS.Controls
            '        If TypeOf ctl Is TextBox Then
            '            ctl.Readonly = False
            '            ctl.Text = ""
            '        End If
            '    Next


            '    For Each ctl In RXContactLens.Controls
            '        If TypeOf ctl Is TextBox Then
            '            ctl.Readonly = False
            '            ctl.Text = ""
            '        End If
            '    Next

            '    For Each ctl In SlitAndReadings.Controls
            '        If TypeOf ctl Is TextBox Then
            '            ctl.Readonly = False
            '            ctl.Text = ""
            '        End If
            '    Next

            '    For Each ctl In TrailDetails.Controls
            '        If TypeOf ctl Is TextBox Then
            '            ctl.Readonly = False
            '            ctl.Text = ""
            '        End If
            '    Next

            '    MsgBox("No Record Found")
            'ElseIf totcount = 1 Then
            '    row = totds.Tables("Table").Rows.Item(toti)


            '    ' row = ds.Tables("Table").Rows.Item(i)
            '    If row.Item(4).ToString = "MALE" Then
            '        RadPatMale.Checked = True
            '    ElseIf row.Item(4).ToString = "FEMALE" Then
            '        RadPatFemale.Checked = True
            '    End If


            '    If row.Item(5).ToString = "" Then
            '        dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
            '    Else
            '        dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
            '    End If

            '    txtPatCustCode.Text = row.Item(0).ToString
            '    txtPatCustName.Text = row.Item(2).ToString
            '    txtPatPatientName.Text = row.Item(3).ToString

            '    txtPatCity.Text = row.Item(6).ToString
            '    txtPatZipcode.Text = row.Item(7).ToString
            '    txtPatTelOff.Text = row.Item(8).ToString
            '    txtPatTelRes.Text = row.Item(9).ToString
            '    txtPatMobile.Text = row.Item(10).ToString
            '    txtPatEmail.Text = row.Item(11).ToString
            '    txtPatNation.Text = row.Item(12).ToString
            '    txtPatCompany.Text = row.Item(13).ToString
            '    txtPatOccupation.Text = row.Item(14).ToString
            '    txtPatRemarks.Text = row.Item(15).ToString
            '    txtPatNotes.Text = row.Item(16).ToString
            '    txtPatPatientNo.Text = row.Item(17).ToString
            '    patientno = row.Item(17).ToString

            '    'i = 0
            '    'stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
            '    'ds = db.SelectFromTableODBC(stQuery)
            '    'count = ds.Tables("Table").Rows.Count
            '    'If count > 0 Then
            '    '    row = ds.Tables("Table").Rows.Item(i)
            '    '    txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
            '    '    txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
            '    '    txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
            '    '    txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
            '    '    txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
            '    '    txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
            '    '    txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
            '    '    txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
            '    '    txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
            '    '    txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
            '    '    txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
            '    '    txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
            '    '    txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
            '    '    txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
            '    '    txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
            '    '    txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
            '    '    txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
            '    '    txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

            '    '    i = i + 1
            '    '    count = count - 1
            '    'End If


            '    'i = 0
            '    'stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
            '    'ds = db.SelectFromTableODBC(stQuery)
            '    'count = ds.Tables("Table").Rows.Count
            '    'If count > 0 Then
            '    '    row = ds.Tables("Table").Rows.Item(i)
            '    '    txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
            '    '    txtRXC_RE_Dia_I.Text = row.Item(1).ToString
            '    '    txtRXC_RE_Pow_I.Text = row.Item(2).ToString
            '    '    txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
            '    '    txtRXC_RE_Dia_II.Text = row.Item(4).ToString
            '    '    txtRXC_RE_Pow_II.Text = row.Item(5).ToString
            '    '    txtRXC_RE_Brand1.Text = row.Item(6).ToString
            '    '    txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
            '    '    txtRXC_LE_Dia_I.Text = row.Item(8).ToString
            '    '    txtRXC_LE_Pow_I.Text = row.Item(9).ToString
            '    '    txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
            '    '    txtRXC_LE_Dia_II.Text = row.Item(11).ToString
            '    '    txtRXC_LE_Pow_II.Text = row.Item(12).ToString
            '    '    txtRXC_LE_Brand2.Text = row.Item(13).ToString
            '    '    i = i + 1
            '    '    count = count - 1
            '    'End If

            '    'i = 0
            '    'stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
            '    'ds = db.SelectFromTableODBC(stQuery)
            '    'count = ds.Tables("Table").Rows.Count
            '    'If count > 0 Then
            '    '    row = ds.Tables("Table").Rows.Item(i)
            '    '    txtSlit_Re.Text = row.Item(0).ToString
            '    '    txtSlit_Le.Text = row.Item(1).ToString
            '    '    txtSlit_LrisDia.Text = row.Item(2).ToString
            '    '    txtK_Re_Hori.Text = row.Item(3).ToString
            '    '    txtK_Le_Hori.Text = row.Item(4).ToString
            '    '    txtK_Re_Vert.Text = row.Item(5).ToString
            '    '    txtK_Le_Vert.Text = row.Item(6).ToString
            '    '    i = i + 1
            '    '    count = count - 1
            '    'End If

            '    'i = 0
            '    'stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & patientno & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
            '    'ds = db.SelectFromTableODBC(stQuery)
            '    'count = ds.Tables("Table").Rows.Count
            '    'If count > 0 Then
            '    '    row = ds.Tables("Table").Rows.Item(i)
            '    '    txtTrail_Re.Text = row.Item(0).ToString
            '    '    txtTrail_Re_Add.Text = row.Item(1).ToString
            '    '    txtTrail_Re_Via.Text = row.Item(2).ToString
            '    '    txtTrail_Le.Text = row.Item(3).ToString
            '    '    txtTrail_Le_Add.Text = row.Item(4).ToString
            '    '    txtTrail_Le_Via.Text = row.Item(5).ToString
            '    '    txtTrail_Re_Remarks.Text = row.Item(6).ToString
            '    '    txtTrail_Le_Remarks.Text = row.Item(7).ToString
            '    '    i = i + 1
            '    '    count = count - 1
            '    'End If

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientQuery" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientTelOffSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientMobileSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientNameSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientTelResSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientEmailSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
            '            Exit For
            '        End If
            '    Next


            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientNoSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            'ElseIf totcount > 1 Then

            '    btnPatientNext.Enabled = True

            '    row = totds.Tables("Table").Rows.Item(toti)


            '    If row.Item(4).ToString = "MALE" Then
            '        RadPatMale.Checked = True
            '    ElseIf row.Item(4).ToString = "FEMALE" Then
            '        RadPatFemale.Checked = True
            '    End If


            '    txtPatCustCode.Text = row.Item(0).ToString
            '    txtPatCustName.Text = row.Item(2).ToString
            '    txtPatPatientName.Text = row.Item(3).ToString

            '    If row.Item(5).ToString = "" Then
            '        dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
            '    Else
            '        dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
            '    End If

            '    txtPatCity.Text = row.Item(6).ToString
            '    txtPatZipcode.Text = row.Item(7).ToString
            '    txtPatTelOff.Text = row.Item(8).ToString
            '    txtPatTelRes.Text = row.Item(9).ToString
            '    txtPatMobile.Text = row.Item(10).ToString
            '    txtPatEmail.Text = row.Item(11).ToString
            '    txtPatNation.Text = row.Item(12).ToString
            '    txtPatCompany.Text = row.Item(13).ToString
            '    txtPatOccupation.Text = row.Item(14).ToString
            '    txtPatRemarks.Text = row.Item(15).ToString
            '    txtPatNotes.Text = row.Item(16).ToString
            '    txtPatPatientNo.Text = row.Item(17).ToString
            '    patientno = row.Item(17).ToString
            '    'txtPatientNo.Text = row.Item(17).ToString





            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientQuery" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientQuery", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientTelOffSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelOffSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientMobileSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientMobileSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientNameSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNameSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientTelResSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientTelResSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientEmailSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientEmailSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    For Each ctl As Control In PnlPatientDetails.Controls
            '        If ctl.Name = "btnPatientNoSearch" Then
            '            PnlPatientDetails.Controls.Remove(Me.Controls.Find("btnPatientNoSearch", True)(0))
            '            Exit For
            '        End If
            '    Next

            '    btnPatientSearch.BringToFront()
            '    btnPatientAdd.Enabled = True
            '    btnPatientEdit.Enabled = True
            '    btnPatientDelete.Enabled = True

            'End If
            'txtPatCity.ReadOnly = False
            'txtPatNation.ReadOnly = False
            'txtPatOccupation.ReadOnly = False
            'txtPatRemarks.ReadOnly = False
            'txtPatNotes.ReadOnly = False
            'txtPatCompany.ReadOnly = False
            'txtPatZipcode.ReadOnly = False

            '' btnPatientSearch.BringToFront()
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientQuerySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim buttonclicked As String = DirectCast(sender, Button).Name
            If buttonclicked = "btnPatientTelOffSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatTelOffSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatTelOffText"
                        .Text = "Patient Office Telephone No. Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatTelOffText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatTelOffText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatTelOffText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Office Telephone No.", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatTelOffTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatTelOffTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)

                PnlPatientDetails.Hide()
            ElseIf buttonclicked = "btnPatientMobileSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatMobSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatMobText"
                        .Text = "Patient Mobile No. Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatMobText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatMobText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatMobText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Mobile Number", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatMobTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatMobTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)

                PnlPatientDetails.Hide()

                'Patno Search starts

            ElseIf buttonclicked = "btnPatientNoSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatNoSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatNoText"
                        .Text = "Patient No. Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatNoText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatNoText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatNoText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Patient No", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatNoTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatNoTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)

                PnlPatientDetails.Hide()

            ElseIf buttonclicked = "btnPatientNameSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatNameSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatNameText"
                        .Text = "Patient Name Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatNameText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatNameText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatNameText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Patient Name", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatNameTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatNameTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)

                PnlPatientDetails.Hide()
            ElseIf buttonclicked = "btnPatientTelResSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatTelResSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatTelResText"
                        .Text = "Patient Residence Telephone No. Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatTelResText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatTelResText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatTelResText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Residence Telephone No.", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatTelResTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatTelResTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 20)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)
                PnlPatientDetails.Hide()
            ElseIf buttonclicked = "btnPatientEmailSearch" Then
                Dim pnl As New Panel
                With pnl
                    .Name = "pnlPatEmailSearch"
                    .Location = New Point(PnlPatientDetails.Location.X, PnlPatientDetails.Location.Y)
                    .Size = New Size(PnlPatientDetails.Width, PnlPatientDetails.Height)
                    .BackColor = Color.Azure
                    .BorderStyle = BorderStyle.FixedSingle
                    .BringToFront()
                    Dim lbl As New Label
                    With lbl
                        .Name = "lblHeaderPatEmailText"
                        .Text = "Patient Email ID Search"
                        .TextAlign = ContentAlignment.MiddleLeft
                        .BackColor = Color.DarkCyan
                        .Location = New Point(1, 1)
                        .Font = New Font(lbl.Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .Size = New Size(PnlPatientDetails.Width - 4, 20)
                    End With
                    .Controls.Add(lbl)

                    Dim lbl1 As New Label
                    With lbl1
                        .Name = "lblPatEmailText"
                        .Text = "Search"
                        .Location = New Point(PnlPatientDetails.Width / 5, 55)
                        .Font = New Font(lbl1.Font, FontStyle.Bold)
                        .Size = New Size(50, 20)
                        .ForeColor = Color.DarkGreen
                    End With
                    .Controls.Add(lbl1)

                    Dim txt As New TextBox
                    With txt
                        .Name = "txtPatEmailText"
                        .Location = New Point((PnlPatientDetails.Width / 5) + 50, 53)
                        .Size = New Size(PnlPatientDetails.Width / 2, 20)
                    End With
                    AddHandler txt.TextChanged, AddressOf Me.txtPatSearch_TextChanged
                    .Controls.Add(txt)

                    Dim lstview As New ListView
                    With lstview
                        .Name = "lstviewPatEmailText"
                        .Location = New Point(30, 90)
                        .Size = New Size(PnlPatientDetails.Width - 50, PnlPatientDetails.Height - 180)
                        .GridLines = True
                        .Columns.Add("SNo.", 50, HorizontalAlignment.Center)
                        .Columns.Add("Email ID", PnlPatientDetails.Width - 105, HorizontalAlignment.Left)
                        .View = View.Details
                        .FullRowSelect = True
                    End With
                    AddHandler lstview.DoubleClick, AddressOf Me.lstviewPatSearch_DoubleClick
                    .Controls.Add(lstview)

                    Dim btn As New Button
                    With btn
                        .Name = "btnPatEmailTextSelect"
                        .Location = New Point((PnlPatientDetails.Width / 2) - 90, PnlPatientDetails.Height - 50)
                        .Text = "Select"
                        .Size = New Size(60, 25)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatientSearchSelect_Click
                    .Controls.Add(btn)

                    btn = New Button
                    With btn
                        .Name = "btnPatEmailTextClose"
                        .Location = New Point((PnlPatientDetails.Width / 2), PnlPatientDetails.Height - 50)
                        .Text = "Close"
                        .Size = New Size(60, 25)
                        .Font = New Font(btn.Font, FontStyle.Bold)
                        .BackColor = Color.MediumTurquoise
                        .ForeColor = Color.SaddleBrown
                    End With
                    AddHandler btn.Click, AddressOf Me.btnPatSearchPnlClose_Click
                    .Controls.Add(btn)

                End With
                Me.PnlPatientCOntainer.Controls.Add(pnl)
                PnlPatientDetails.Hide()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnPatientSearchSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim buttonclicked As String = DirectCast(sender, Button).Name
            If buttonclicked = "btnPatTelOffTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatTelOffText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If
            ElseIf buttonclicked = "btnPatTelResTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatTelResText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If
            ElseIf buttonclicked = "btnPatNoTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNoSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatNoText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If
            ElseIf buttonclicked = "btnPatNameTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatNameText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If
            ElseIf buttonclicked = "btnPatMobTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatMobText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If


            ElseIf buttonclicked = "btnPatEmailTextSelect" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                Dim lst As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
                If Not lst.SelectedItems.Count > 0 Then
                    MsgBox("Please select a row!")
                Else
                    lstviewPatSearch_DoubleClick(lst, e)
                End If



                'ElseIf buttonclicked = "btnPatEmailTextSelect" Then
                '    'MsgBox("Email")
                '    Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                '    Dim lst As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
                '    If Not lst.SelectedItems.Count > 0 Then
                '        MsgBox("Please select a row!")
                '    Else
                '        lstviewPatSearch_DoubleClick(lst, e)
                '    End If
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub

    Private Sub lstviewPatSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lstviewclicked As String = DirectCast(sender, ListView).Name
            Dim lstval As String = DirectCast(sender, ListView).SelectedItems.Item(0).SubItems(1).Text
            If lstviewclicked = "lstviewPatTelOffText" Then
                txtPatTelOff.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatTelOffTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)
            ElseIf lstviewclicked = "lstviewPatTelResText" Then
                txtPatTelRes.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatTelResTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)
            ElseIf lstviewclicked = "lstviewPatNameText" Then
                txtPatPatientName.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatNameTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)
            ElseIf lstviewclicked = "lstviewPatMobText" Then
                txtPatMobile.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatMobTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)
            ElseIf lstviewclicked = "lstviewPatEmailText" Then
                txtPatEmail.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatEmailTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)

            ElseIf lstviewclicked = "lstviewPatNoText" Then
                txtPatPatientNo.Text = lstval
                Dim pnl As Panel = Me.Controls.Find("pnlPatNoSearch", True)(0)
                Dim btn As Button = pnl.Controls.Find("btnPatNoTextClose", True)(0)
                btnPatSearchPnlClose_Click(btn, e)
                'ElseIf lstviewclicked = "lstviewPatNoText" Then
                '    txtPatPatientNo.Text = lstval
                '    Dim pnl As Panel = Me.Controls.Find("pnlPatNoSearch", True)(0)
                '    Dim btn As Button = pnl.Controls.Find("btnPatNoClose", True)(0)
                '    btnPatSearchPnlClose_Click(btn, e)
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnPatSearchPnlClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim buttonclicked As String = DirectCast(sender, Button).Name
            If buttonclicked = "btnPatTelOffTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatTelOffSearch", True)(0))
                PnlPatientDetails.Show()
            ElseIf buttonclicked = "btnPatTelResTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatTelResSearch", True)(0))
                PnlPatientDetails.Show()
            ElseIf buttonclicked = "btnPatNameTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatNameSearch", True)(0))
                PnlPatientDetails.Show()
            ElseIf buttonclicked = "btnPatMobTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatMobSearch", True)(0))
                PnlPatientDetails.Show()


            ElseIf buttonclicked = "btnPatEmailTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatEmailSearch", True)(0))
                PnlPatientDetails.Show()


                'ElseIf buttonclicked = "btnPatEmailTextClose" Then
                '    'MsgBox("Close")
                '    Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                '    Dim lst As New List(Of String)
                '    For Each ctl As Control In pnl.Controls
                '        lst.Add(ctl.Name)
                '    Next




            ElseIf buttonclicked = "btnPatNoTextClose" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNoSearch", True)(0)
                Dim lst As New List(Of String)
                For Each ctl As Control In pnl.Controls
                    lst.Add(ctl.Name)
                Next
                For i = 0 To lst.Count - 1
                    pnl.Controls.Remove(Me.Controls.Find(lst(i), True)(0))
                Next
                PnlPatientCOntainer.Controls.Remove(Me.Controls.Find("pnlPatNoSearch", True)(0))
                PnlPatientDetails.Show()
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub txtPatSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim txtName As String = DirectCast(sender, TextBox).Name
            Dim txtVal As String = DirectCast(sender, TextBox).Text
            Dim i As Integer = 0
            Dim count As Integer = 0
            Dim stQuery As String = ""
            Dim ds As DataSet
            Dim row As System.Data.DataRow
            If txtName = "txtPatTelOffText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelOffSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelOffText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_OFF FROM OM_PATIENT_MASTER where UPPER(PM_TEL_OFF) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
            ElseIf txtName = "txtPatTelResText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatTelResSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatTelResText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_RES FROM OM_PATIENT_MASTER where UPPER(PM_TEL_RES) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While


            ElseIf txtName = "txtPatNoText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNoSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatNoText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_CUST_NO FROM OM_PATIENT_MASTER where UPPER(PM_CUST_NO)='" + txtVal.ToUpper + "'"

                ds = db.SelectFromTableODBC(stQuery)
                errLog.WriteToErrorLog("PatientNo Search Query", stQuery, "Error")
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While


            ElseIf txtName = "txtPatNameText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatNameSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatNameText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_PATIENT_NAME FROM OM_PATIENT_MASTER where UPPER(PM_PATIENT_NAME) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
            ElseIf txtName = "txtPatMobText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatMobSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatMobText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_TEL_MOB FROM OM_PATIENT_MASTER where UPPER(PM_TEL_MOB) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
            ElseIf txtName = "txtPatEmailText" Then
                Dim pnl As Panel = Me.Controls.Find("pnlPatEmailSearch", True)(0)
                Dim lstview As ListView = pnl.Controls.Find("lstviewPatEmailText", True)(0)
                lstview.Items.Clear()
                If txtVal = "" Then
                    Exit Sub
                End If
                stQuery = "SELECT PM_EMAIL FROM OM_PATIENT_MASTER where UPPER(PM_EMAIL) like '" & txtVal.ToUpper & "%'"
                ds = db.SelectFromTableODBC(stQuery)
                i = 0
                count = ds.Tables("Table").Rows.Count
                While count > 0
                    row = ds.Tables("Table").Rows.Item(i)
                    lstview.Items.Add(i + 1)
                    lstview.Items(i).SubItems.Add(row.Item(0).ToString)
                    i = i + 1
                    count = count - 1
                End While
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    Private Sub btnPatientexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientexit.Click
        Try
            Home.RefreshPatient(sender, e)
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RadPatMale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPatMale.CheckedChanged
        Try
            If RadPatMale.Checked = True Then
                RadPatFemale.Checked = False
            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RadPatFemale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPatFemale.CheckedChanged
        Try
            If RadPatFemale.Checked = True Then
                RadPatMale.Checked = False

            End If
        Catch ex As Exception
            errLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPatientNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientNext.Click
        Dim row As System.Data.DataRow
        Dim patientno As String
        Dim count As Integer
        Dim i As Integer
        Dim stQuery As String
        Dim ds As New DataSet

        btnPatientPrev.Enabled = True
        toti = toti + 1
        If toti < totcount Then
            row = totds.Tables("Table").Rows.Item(toti)
            If row.Item(4).ToString = "MALE" Then
                RadPatMale.Checked = True
            ElseIf row.Item(4).ToString = "FEMALE" Then
                RadPatFemale.Checked = True
            End If

            txtPatCustCode.Text = row.Item(0).ToString
            txtPatCustName.Text = row.Item(2).ToString
            txtPatPatientName.Text = row.Item(3).ToString

            If row.Item(5).ToString = "" Then
                dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
            Else
                dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
            End If

            txtPatCity.Text = row.Item(6).ToString
            txtPatZipcode.Text = row.Item(7).ToString
            txtPatTelOff.Text = row.Item(8).ToString
            txtPatTelRes.Text = row.Item(9).ToString
            txtPatMobile.Text = row.Item(10).ToString
            txtPatEmail.Text = row.Item(11).ToString
            txtPatNation.Text = row.Item(12).ToString
            txtPatCompany.Text = row.Item(13).ToString
            txtPatOccupation.Text = row.Item(14).ToString
            txtPatRemarks.Text = row.Item(15).ToString
            txtPatNotes.Text = row.Item(16).ToString
            txtPatPatientNo.Text = row.Item(17).ToString
            patientno = row.Item(17).ToString
            ' txtPatientNo.Text = row.Item(17).ToString

            i = 0
            stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
            errLog.WriteToErrorLog("RX-GLASSES", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
                txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
                txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
                txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
                txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
                txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
                txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
                txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
                txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
                txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
                txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
                txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
                txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
                txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
                txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
                txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
                txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
                txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

                i = i + 1
                count = count - 1
            Else

            End If


            i = 0
            stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
            errLog.WriteToErrorLog("LENSE", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
                txtRXC_RE_Dia_I.Text = row.Item(1).ToString
                txtRXC_RE_Pow_I.Text = row.Item(2).ToString
                txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
                txtRXC_RE_Dia_II.Text = row.Item(4).ToString
                txtRXC_RE_Pow_II.Text = row.Item(5).ToString
                txtRXC_RE_Brand1.Text = row.Item(6).ToString

                txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
                txtRXC_LE_Dia_I.Text = row.Item(8).ToString
                txtRXC_LE_Pow_I.Text = row.Item(9).ToString
                txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
                txtRXC_LE_Dia_II.Text = row.Item(11).ToString
                txtRXC_LE_Pow_II.Text = row.Item(12).ToString
                txtRXC_LE_Brand2.Text = row.Item(13).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
            errLog.WriteToErrorLog("SLIT AND K", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtSlit_Re.Text = row.Item(0).ToString
                txtSlit_Le.Text = row.Item(1).ToString
                txtSlit_LrisDia.Text = row.Item(2).ToString
                txtK_Re_Hori.Text = row.Item(3).ToString
                txtK_Le_Hori.Text = row.Item(4).ToString
                txtK_Re_Vert.Text = row.Item(5).ToString
                txtK_Le_Vert.Text = row.Item(6).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
            errLog.WriteToErrorLog("Trial Details", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtTrail_Re.Text = row.Item(0).ToString
                txtTrail_Re_Add.Text = row.Item(1).ToString
                txtTrail_Re_Via.Text = row.Item(2).ToString
                txtTrail_Le.Text = row.Item(3).ToString
                txtTrail_Le_Add.Text = row.Item(4).ToString
                txtTrail_Le_Via.Text = row.Item(5).ToString
                txtTrail_Re_Remarks.Text = row.Item(6).ToString
                txtTrail_Le_Remarks.Text = row.Item(7).ToString
                i = i + 1
                count = count - 1
            End If

        ElseIf toti = totcount Then
            btnPatientNext.Enabled = False
            btnPatientPrev.Enabled = True
        End If
    End Sub

    Private Sub btnPatientPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientPrev.Click
        Dim row As System.Data.DataRow
        Dim patientno As String
        Dim count As Integer
        Dim i As Integer
        Dim stQuery As String
        Dim ds As New DataSet

        toti = toti - 1
        btnPatientNext.Enabled = True
        If toti >= 0 Then
            row = totds.Tables("Table").Rows.Item(toti)
            If row.Item(4).ToString = "MALE" Then
                RadPatMale.Checked = True
            ElseIf row.Item(4).ToString = "FEMALE" Then
                RadPatFemale.Checked = True
            End If

            txtPatCustCode.Text = row.Item(0).ToString
            txtPatCustName.Text = row.Item(2).ToString
            txtPatPatientName.Text = row.Item(3).ToString

            If row.Item(5).ToString = "" Then
                dtPatientDOB.Value = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", Nothing)
            Else
                dtPatientDOB.Value = DateTime.ParseExact(row.Item(5).ToString, "dd/MM/yyyy", Nothing)
            End If

            txtPatCity.Text = row.Item(6).ToString
            txtPatZipcode.Text = row.Item(7).ToString
            txtPatTelOff.Text = row.Item(8).ToString
            txtPatTelRes.Text = row.Item(9).ToString
            txtPatMobile.Text = row.Item(10).ToString
            txtPatEmail.Text = row.Item(11).ToString
            txtPatNation.Text = row.Item(12).ToString
            txtPatCompany.Text = row.Item(13).ToString
            txtPatOccupation.Text = row.Item(14).ToString
            txtPatRemarks.Text = row.Item(15).ToString
            txtPatNotes.Text = row.Item(16).ToString
            txtPatPatientNo.Text = row.Item(17).ToString
            patientno = row.Item(17).ToString
            'txtPatientNo.Text = row.Item(17).ToString

            i = 0
            stQuery = "select  NVL(PRXG_R_D_SPH,0) ,NVL(PRXG_R_D_CYL,0) ,NVL(PRXG_R_D_AXIS,0),NVL(PRXG_R_D_VISION,0),NVL(PRXG_R_N_SPH,0),NVL(PRXG_R_N_CYL,0),NVL(PRXG_R_N_AXIS,0),NVL(PRXG_R_N_VISION,0),NVL(PRXG_R_PD,0),NVL(PRXG_L_D_SPH,0),NVL(PRXG_L_D_CYL,0),NVL(PRXG_L_D_AXIS,0),NVL(PRXG_L_D_VISION,0),NVL(PRXG_L_N_SPH,0),NVL(PRXG_L_N_CYL,0),NVL(PRXG_L_N_AXIS,0),NVL(PRXG_L_N_VISION,0),NVL(PRXG_L_PD,0) from om_patient_master a, om_customer b,OM_PAT_RX_GLASSES c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXG_PM_SYS_ID "
            errLog.WriteToErrorLog("RX-GLASSES", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXG_RE_Sph_D1.Text = row.Item(0).ToString
                txtRXG_RE_Cyl_D1.Text = row.Item(1).ToString
                txtRXG_RE_Axi_D1.Text = row.Item(2).ToString
                txtRXG_RE_Vis_D1.Text = row.Item(3).ToString
                txtRXG_RE_Sph_N1.Text = row.Item(4).ToString
                txtRXG_RE_Cyl_N1.Text = row.Item(5).ToString
                txtRXG_RE_Axi_N1.Text = row.Item(6).ToString
                txtRXG_RE_Vis_N1.Text = row.Item(7).ToString
                txtRXG_LE_IPD_D1.Text = row.Item(8).ToString
                txtRXG_LE_Sph_D1.Text = row.Item(9).ToString
                txtRXG_LE_Cyl_D1.Text = row.Item(10).ToString
                txtRXG_LE_Axi_D1.Text = row.Item(11).ToString
                txtRXG_LE_Vis_D1.Text = row.Item(12).ToString
                txtRXG_LE_Sph_N1.Text = row.Item(13).ToString
                txtRXG_LE_Cyl_N1.Text = row.Item(14).ToString
                txtRXG_LE_Axi_N1.Text = row.Item(15).ToString
                txtRXG_LE_Vis_N1.Text = row.Item(16).ToString
                txtRXG_LE_IPD_N1.Text = row.Item(17).ToString

                i = i + 1
                count = count - 1
            Else

            End If


            i = 0
            stQuery = "select  nvl(PRXCL_R_I_BCOR,0),NVL(PRXCL_R_I_DIA,0),NVL(PRXCL_R_I_POWER,0),NVL(PRXCL_R_II_BCOR,0),NVL(PRXCL_R_II_DIA,0),NVL(PRXCL_R_II_POWER,0),NVL(PRXCL_R_BRAND,0),NVL(PRXCL_L_I_BCOR,0),NVL(PRXCL_L_I_DIA,0),NVL(PRXCL_L_I_POWER,0),NVL(PRXCL_L_II_BCOR,0),NVL(PRXCL_L_II_DIA,0),NVL(PRXCL_L_II_POWER,0),NVL(PRXCL_L_BRAND,0) from om_patient_master a, om_customer b,OM_PAT_RX_CONTACT_LENS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXCL_PM_SYS_ID "
            errLog.WriteToErrorLog("LENSE", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtRXC_RE_Bcor_I.Text = row.Item(0).ToString
                txtRXC_RE_Dia_I.Text = row.Item(1).ToString
                txtRXC_RE_Pow_I.Text = row.Item(2).ToString
                txtRXC_RE_Bcor_II.Text = row.Item(3).ToString
                txtRXC_RE_Dia_II.Text = row.Item(4).ToString
                txtRXC_RE_Pow_II.Text = row.Item(5).ToString
                txtRXC_RE_Brand1.Text = row.Item(6).ToString

                txtRXC_LE_Bcor_I.Text = row.Item(7).ToString
                txtRXC_LE_Dia_I.Text = row.Item(8).ToString
                txtRXC_LE_Pow_I.Text = row.Item(9).ToString
                txtRXC_LE_Bcor_II.Text = row.Item(10).ToString
                txtRXC_LE_Dia_II.Text = row.Item(11).ToString
                txtRXC_LE_Pow_II.Text = row.Item(12).ToString
                txtRXC_LE_Brand2.Text = row.Item(13).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select nvl(PRXSKR_SLIT_RE,0), nvl(PRXSKR_SLIT_LE,0),nvl(PRXSKR_SLIT_LRIS,0),NVL(PRXSKR_K_RE_HORIZONTAL,0),NVL(PRXSKR_K_LE_HORIZONTAL,0),NVL(PRXSKR_K_RE_VERTICAL,0), NVL(PRXSKR_K_LE_VERTICAL,0) from om_patient_master a, om_customer b,OM_PAT_RX_SLITK_READING c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID =c.PRXSKR_PM_SYS_ID "
            errLog.WriteToErrorLog("SLIT AND K", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtSlit_Re.Text = row.Item(0).ToString
                txtSlit_Le.Text = row.Item(1).ToString
                txtSlit_LrisDia.Text = row.Item(2).ToString
                txtK_Re_Hori.Text = row.Item(3).ToString
                txtK_Le_Hori.Text = row.Item(4).ToString
                txtK_Re_Vert.Text = row.Item(5).ToString
                txtK_Le_Vert.Text = row.Item(6).ToString
                i = i + 1
                count = count - 1
            End If

            i = 0
            stQuery = "select  nvl(PRXTD_LENS_USED_RE,0),NVL(PRXTD_LENS_USED_RE_ADD,0),NVL(PRXTD_LENS_USED_RE_VIA,0),NVL(PRXTD_LENS_USED_LE,0),NVL(PRXTD_LENS_USED_LE_ADD,0),NVL(PRXTD_LENS_USED_LE_VIA,0),NVL(PRXTD_RE_REMARKS,0),NVL(PRXTD_LE_REMARKS,0) from om_patient_master a, om_customer b,OM_PAT_RX_TRIAL_DETAILS c where pm_cust_no='" & txtPatPatientNo.Text & "' and a.PM_CUST_CODE=b.CUST_CODE and a.PM_SYS_ID = c.PRXTD_PM_SYS_ID "
            errLog.WriteToErrorLog("Trial Details", stQuery, "")
            ds = db.SelectFromTableODBC(stQuery)
            count = ds.Tables("Table").Rows.Count
            If count > 0 Then
                row = ds.Tables("Table").Rows.Item(i)
                txtTrail_Re.Text = row.Item(0).ToString
                txtTrail_Re_Add.Text = row.Item(1).ToString
                txtTrail_Re_Via.Text = row.Item(2).ToString
                txtTrail_Le.Text = row.Item(3).ToString
                txtTrail_Le_Add.Text = row.Item(4).ToString
                txtTrail_Le_Via.Text = row.Item(5).ToString
                txtTrail_Re_Remarks.Text = row.Item(6).ToString
                txtTrail_Le_Remarks.Text = row.Item(7).ToString
                i = i + 1
                count = count - 1
            End If

        ElseIf toti < 0 Then
            btnPatientNext.Enabled = True
            btnPatientPrev.Enabled = False
        End If
    End Sub

    Private Sub txtRXG_RE_Sph_D1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRXG_RE_Sph_D1.TextChanged

    End Sub
End Class