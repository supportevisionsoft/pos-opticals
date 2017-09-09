Imports System.Runtime.InteropServices

Friend NotInheritable Class NativeMethods
    Private Sub New()
    End Sub
    ''' <summary>
    ''' An enumeration of GetDeviceCaps parameters.
    ''' </summary>
    Friend Enum DeviceCap As Integer
        ''' <summary>
        ''' Device driver version
        ''' </summary>
        DRIVERVERSION = 0

        ''' <summary>
        ''' Device classification
        ''' </summary>
        TECHNOLOGY = 2

        ''' <summary>
        ''' Horizontal size in millimeters
        ''' </summary>
        HORZSIZE = 4

        ''' <summary>
        ''' Vertical size in millimeters
        ''' </summary>
        VERTSIZE = 6

        ''' <summary>
        ''' Horizontal width in pixels
        ''' </summary>
        HORZRES = 8

        ''' <summary>
        ''' Vertical height in pixels
        ''' </summary>
        VERTRES = 10

        ''' <summary>
        ''' Number of bits per pixel
        ''' </summary>
        BITSPIXEL = 12

        ''' <summary>
        ''' Number of planes
        ''' </summary>
        PLANES = 14

        ''' <summary>
        ''' Number of brushes the device has
        ''' </summary>
        NUMBRUSHES = 16

        ''' <summary>
        ''' Number of pens the device has
        ''' </summary>
        NUMPENS = 18

        ''' <summary>
        ''' Number of markers the device has
        ''' </summary>
        NUMMARKERS = 20

        ''' <summary>
        ''' Number of fonts the device has
        ''' </summary>
        NUMFONTS = 22

        ''' <summary>
        ''' Number of colors the device supports
        ''' </summary>
        NUMCOLORS = 24

        ''' <summary>
        ''' Size required for device descriptor
        ''' </summary>
        PDEVICESIZE = 26

        ''' <summary>
        ''' Curve capabilities
        ''' </summary>
        CURVECAPS = 28

        ''' <summary>
        ''' Line capabilities
        ''' </summary>
        LINECAPS = 30

        ''' <summary>
        ''' Polygonal capabilities
        ''' </summary>
        POLYGONALCAPS = 32

        ''' <summary>
        ''' Text capabilities
        ''' </summary>
        TEXTCAPS = 34

        ''' <summary>
        ''' Clipping capabilities
        ''' </summary>
        CLIPCAPS = 36

        ''' <summary>
        ''' Bitblt capabilities
        ''' </summary>
        RASTERCAPS = 38

        ''' <summary>
        ''' Length of the X leg
        ''' </summary>
        ASPECTX = 40

        ''' <summary>
        ''' Length of the Y leg
        ''' </summary>
        ASPECTY = 42

        ''' <summary>
        ''' Length of the hypotenuse
        ''' </summary>
        ASPECTXY = 44

        ''' <summary>
        ''' Shading and Blending caps
        ''' </summary>
        SHADEBLENDCAPS = 45

        ''' <summary>
        ''' Logical pixels inch in X
        ''' </summary>
        LOGPIXELSX = 88

        ''' <summary>
        ''' Logical pixels inch in Y
        ''' </summary>
        LOGPIXELSY = 90

        ''' <summary>
        ''' Number of entries in physical palette
        ''' </summary>
        SIZEPALETTE = 104

        ''' <summary>
        ''' Number of reserved entries in palette
        ''' </summary>
        NUMRESERVED = 106

        ''' <summary>
        ''' Actual color resolution
        ''' </summary>
        COLORRES = 108

        ''' <summary>
        ''' Physical Width in device units
        ''' </summary>
        PHYSICALWIDTH = 110

        ''' <summary>
        ''' Physical Height in device units
        ''' </summary>
        PHYSICALHEIGHT = 111

        ''' <summary>
        ''' Physical Printable Area x margin
        ''' </summary>
        PHYSICALOFFSETX = 112

        ''' <summary>
        ''' Physical Printable Area y margin
        ''' </summary>
        PHYSICALOFFSETY = 113

        ''' <summary>
        ''' Scaling factor x
        ''' </summary>
        SCALINGFACTORX = 114

        ''' <summary>
        ''' Scaling factor y
        ''' </summary>
        SCALINGFACTORY = 115

        ''' <summary>
        ''' Current vertical refresh rate of the display device (for displays only) in Hz
        ''' </summary>
        VREFRESH = 116

        ''' <summary>
        ''' Horizontal width of entire desktop in pixels
        ''' </summary>
        DESKTOPVERTRES = 117

        ''' <summary>
        ''' Vertical height of entire desktop in pixels
        ''' </summary>
        DESKTOPHORZRES = 118

        ''' <summary>
        ''' Preferred blt alignment
        ''' </summary>
        BLTALIGNMENT = 119
    End Enum

    ''' <summary>
    ''' The CreateDC function creates a device context (DC) for a device 
    ''' using the specified name.
    ''' </summary>
    ''' <param name="lpszDriver">Pointer to a null-terminated character
    ''' string that specifies either DISPLAY or the name of a specific 
    ''' display device or the name of a print provider, which is usually WINSPOOL.</param>
    ''' <param name="lpszDevice">Pointer to a null-terminated character string 
    ''' that specifies the name of the specific output device being used, 
    ''' as shown by the Print Manager (for example, Epson FX-80). It is not 
    ''' the printer model name. The lpszDevice parameter must be used.</param>
    ''' <param name="lpszOutput">This parameter is ignored and should be set
    ''' to NULL. It is provided only for compatibility with 16-bit Windows.</param>
    ''' <param name="lpInitData">Pointer to a DEVMODE structure containing 
    ''' device-specific initialization data for the device driver. The 
    ''' DocumentProperties function retrieves this structure filled in for
    ''' a specified device. The lpInitData parameter must be NULL if the
    ''' device driver is to use the default initialization (if any) specified
    ''' by the user.</param>
    ''' <returns>If the function succeeds, the return value is the handle
    ''' to a DC for the specified device. If the function fails, the 
    ''' return value is NULL. The function will return NULL for a DEVMODE
    ''' structure other than the current DEVMODE.</returns>
    <DllImport("gdi32.dll", CharSet:=CharSet.Unicode)> _
    Friend Shared Function CreateDC(ByVal lpszDriver As String, ByVal lpszDevice As String, ByVal lpszOutput As String, ByVal lpInitData As IntPtr) As IntPtr
    End Function

    ''' <summary>
    ''' The DeleteDC function deletes the specified device context (DC).
    ''' </summary>
    ''' <param name="hdc">Handle to the device context.</param>
    ''' <returns>If the function succeeds, the return value is nonzero. 
    ''' If the function fails, the return value is zero.</returns>
    <DllImport("gdi32.dll")> _
    Friend Shared Function DeleteDC(ByVal hdc As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="OpenPrinterW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function OpenPrinter(<MarshalAs(UnmanagedType.LPWStr)> ByVal szPrinter As String, ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' Closes the specified printer object.
    ''' </summary>
    ''' <param name="hPrinter">Handle to the printer object to be closed.
    ''' This handle is returned by the OpenPrinter or AddPrinter function.</param>
    ''' <returns>If the function succeeds, the return value is a nonzero value.
    ''' If the function fails, the return value is zero</returns>
    <DllImport("winspool.drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' The StartDoc function starts a print job.
    ''' </summary>
    ''' <param name="hdc">Handle to the device context for the print job.</param>
    ''' <param name="lpdi">Pointer to a DOCINFO structure containing the name 
    ''' of the document file and the name of the output file.</param>
    ''' <returns>If the function succeeds, the return value is greater than
    ''' zero. This value is the print job identifier for the document.</returns>
    <DllImport("gdi32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Friend Shared Function StartDoc(ByVal hdc As IntPtr, ByVal lpdi As DOCINFO) As Integer
    End Function

    ''' <summary>
    ''' The EndDoc function ends a print job.
    ''' </summary>
    ''' <param name="hdc">Handle to the device context for the print job.</param>
    ''' <returns>If the function succeeds, the return value is greater than zero.
    ''' If the function fails, the return value is less than or equal
    ''' to zero.</returns>
    <DllImport("gdi32.dll")> _
    Friend Shared Function EndDoc(ByVal hdc As IntPtr) As Integer
    End Function

    ''' <summary>
    ''' The GetDeviceCaps function retrieves device-specific information 
    ''' for the specified device.
    ''' </summary>
    ''' <param name="hdc">Handle to the DC.</param>
    ''' <param name="capindex">Specifies the item to return.</param>
    ''' <returns>The return value specifies the value of the desired item.</returns>
    <DllImport("gdi32.dll")> _
    Friend Shared Function GetDeviceCaps(ByVal hdc As IntPtr, ByVal capindex As DeviceCap) As Integer
    End Function

    ''' <summary>
    ''' The StartPage function prepares the printer driver to accept data.
    ''' </summary>
    ''' <param name="hdc">Handle to the device context for the print job.</param>
    ''' <returns>If the function succeeds, the return value is greater than zero.
    ''' If the function fails, the return value is less than or equal to zero.</returns>
    <DllImport("gdi32.dll")> _
    Friend Shared Function StartPage(ByVal hdc As IntPtr) As Integer
    End Function

    ''' <summary>
    ''' The EndPage function notifies the device that the application has
    ''' finished writing to a page. This function is typically used to 
    ''' direct the device driver to advance to a new page.
    ''' </summary>
    ''' <param name="hdc">Handle to the device context for the print job.</param>
    ''' <returns>If the function succeeds, the return value is greater than zero.
    ''' If the function fails, the return value is less than or equal to zero.</returns>
    <DllImport("gdi32.dll")> _
    Friend Shared Function EndPage(ByVal hdc As IntPtr) As Integer
    End Function

    ''' <summary>
    ''' The StartDocPrinter function notifies the print spooler
    ''' that a document is to be spooled for printing.
    ''' </summary>
    ''' <param name="hPrinter">Handle to the printer. Use the OpenPrinter or
    ''' AddPrinter function to retrieve a printer handle.</param>
    ''' <param name="level">Specifies the version of the structure to 
    ''' which pDocInfo points. On WIndows NT/2000/XP, the value must be 1.</param>
    ''' <param name="di">Pointer to a structure that describes the document to print.</param>
    ''' <returns>If the function succeeds, the return value identifies the print job.
    ''' If the function fails, the return value is zero. </returns>
    <DllImport("winspool.drv", EntryPoint:="StartDocPrinterW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal di As DOC_INFO_1) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Friend Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, ByRef dwWritten As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' The DOCINFO structure contains the input and output file names and 
    ''' other information used by the StartDoc function.
    ''' </summary>
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Friend Class DOCINFO
        ''' <summary>
        ''' The size, in bytes, of the structure.
        ''' </summary>
        Public cbSize As Integer = 20

        ''' <summary>
        ''' Pointer to a null-terminated string that specifies the name
        ''' of the document.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public lpszDocName As String

        ''' <summary>
        ''' Pointer to a null-terminated string that specifies the name of 
        ''' an output file. If this pointer is NULL, the output will be 
        ''' sent to the device identified by the device context handle that 
        ''' was passed to the StartDoc function.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public lpszOutput As String

        ''' <summary>
        ''' Pointer to a null-terminated string that specifies the type of 
        ''' data used to record the print job. The legal values for this 
        ''' member can be found by calling EnumPrintProcessorDatatypes and 
        ''' can include such values as raw, emf, or XPS_PASS. This member 
        ''' can be NULL. Note that the requested data type might be ignored.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public lpszDatatype As String

        ''' <summary>
        ''' Specifies additional information about the print job. This 
        ''' member must be zero or one of the following values.
        ''' </summary>
        Public fwType As Integer
    End Class

    ''' <summary>
    ''' The DOC_INFO_1 structure describes a document that will be printed.
    ''' </summary>
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Friend Class DOC_INFO_1
        ''' <summary>
        ''' Pointer to a null-terminated string that specifies the name of
        ''' the document.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pDocName As String

        ''' <summary>
        ''' Pointer to a null-terminated string that specifies the name of
        ''' an output file. To print to a printer, set this to NULL.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pOutputFile As String

        ''' <summary>
        ''' Pointer to a null-terminated string that identifies the type 
        ''' of data used to record the document.
        ''' </summary>
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pDataType As String
    End Class
End Class
