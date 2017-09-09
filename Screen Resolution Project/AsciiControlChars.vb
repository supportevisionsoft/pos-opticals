Public NotInheritable Class AsciiControlChars
    Private Sub New()
    End Sub
    ''' <summary>
    ''' Usually indicates the end of a string.
    ''' </summary>
    Public Const Nul As Char = ChrW(&H0)

    ''' <summary>
    ''' Meant to be used for printers. When receiving this code the 
    ''' printer moves to the next sheet of paper.
    ''' </summary>
    Public Const FormFeed As Char = ChrW(&HC)

    ''' <summary>
    ''' Starts an extended sequence of control codes.
    ''' </summary>
    Public Const Escape As Char = ChrW(&H1B)
    Public Const CenterAlign As Char = ChrW(&H1B) + ChrW(&H61) + "1,49"
    ''' <summary>
    ''' Advances to the next line.
    ''' </summary>
    Public Const Newline As Char = ChrW(&HA)

    ''' <summary>
    ''' Defined to separate tables or different sets of data in a serial
    ''' data storage system.
    ''' </summary>
    Public Const GroupSeparator As Char = ChrW(&H1D)

    ''' <summary>
    ''' A horizontal tab.
    ''' </summary>
    Public Const HorizontalTab As Char = ChrW(&H9)

    ''' <summary>
    ''' Returns the carriage to the start of the line.
    ''' </summary>
    Public Const CarriageReturn As Char = ChrW(&HD)

    ''' <summary>
    ''' Cancels the operation.
    ''' </summary>
    Public Const Cancel As Char = ChrW(&H18)

    ''' <summary>
    ''' Indicates that control characters present in the stream should
    ''' be passed through as transmitted and not interpreted as control
    ''' characters.
    ''' </summary>
    Public Const DataLinkEscape As Char = ChrW(&H10)

    ''' <summary>
    ''' Signals the end of a transmission.
    ''' </summary>
    Public Const EndOfTransmission As Char = ChrW(&H4)

    ''' <summary>
    ''' In serial storage, signals the separation of two files.
    ''' </summary>
    Public Const FileSeparator As Char = ChrW(&H1C)

End Class