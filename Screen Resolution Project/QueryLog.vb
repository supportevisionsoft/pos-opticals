Imports System.IO
Imports System.Net.Mail
Imports System.Configuration

Public Class QueryLog
    Public Sub WriteToErrorLog(ByVal msg As String, ByVal stkTrace As String, ByVal title As String)
        Try

        'check and make the directory if necessary; this is set to look in the application folder, you may wish to place the error log in another location depending upon the user's role and write access to different areas of the file system 
            'If System.IO.Directory.Exists(Application.StartupPath & "\Errors\") Then
            '    Dim mail As New MailMessage()
            '    Dim SmtpServer As New SmtpClient(ConfigurationManager.AppSettings("SMTP_SERVER").ToString())
            '    mail.From = New MailAddress(ConfigurationManager.AppSettings("EMAIL_FROM").ToString())
            '    Dim mailAddress As String = ConfigurationManager.AppSettings("EMAIL_TO").ToString()
            '    Dim parts As String() = mailAddress.Split(New String() {","}, StringSplitOptions.None)
            '    For Each mailAddr As String In parts
            '        mail.[To].Add(mailAddr)
            '    Next

            '    mail.Subject = "Error occured at Location - " & Location_Code & " on " & Now.Date
            '    mail.Body = "Hi Consultant, Error occured at the " & Location_Code & " - " & Location_Name & ". Please check and reply!"

            '        '''Dim attachment As System.Net.Mail.Attachment
            '        '''attachment = New System.Net.Mail.Attachment(Application.StartupPath & "\Errors\" & Location_Code & "_errlog_" & (Date.Now.ToShortDateString).Replace("/", "_") & ".txt")
            '        ''''mail.Attachments.Add(attachment)

            '    SmtpServer.Port = Convert.ToInt64(ConfigurationManager.AppSettings("SMTP_PORT").ToString())
            '    SmtpServer.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("EMAIL_USERNAME").ToString(), ConfigurationManager.AppSettings("EMAIL_PASSWORD").ToString())
            '    SmtpServer.EnableSsl = True

            '    SmtpServer.Send(mail)
            '    ' MessageBox.Show("mail Send")

            '    'Smtp_Server.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings("EMAIL_USERNAME").ToString(), ConfigurationManager.AppSettings("EMAIL_PASSWORD").ToString())

            'End If
        Catch ex As Exception
            errLog.WriteToErrorLog("Error at QueryLog", ex.StackTrace, "")
        End Try
    End Sub
End Class
