Module ini
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
        ByVal lpApplicationName As String, _
        ByVal lpKeyName As String, _
        ByVal lpString As String, _
        ByVal lpFileName As String) _
    As Integer

    Private Declare Ansi Function WritePrivateProfileStringI Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
        ByVal lpApplicationName As String, _
        ByVal lpKeyName As String, _
        ByVal lpString As Integer, _
        ByVal lpFileName As String) _
    As Integer

    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
    ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, _
    ByVal lpDefault As String, _
    ByVal lpReturnedString As String, _
    ByVal nSize As Integer, _
    ByVal lpFileName As String) _
    As Integer

    Private Declare Ansi Function GetPrivateProfileStringI Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
    ByVal lpApplicationName As String, _
    ByVal lpKeyName As String, _
    ByVal lpDefault As String, _
    ByVal lpReturnedString As Integer, _
    ByVal nSize As Integer, _
    ByVal lpFileName As String) _
    As Integer

    ''' <summary>
    ''' Gets an integer Value from a specified Ini-File
    ''' </summary>
    ''' <param name="INIFile">The File to read from. Full path</param>
    ''' <param name="Section">The Section name</param>
    ''' <param name="Key">Name of the Key to read</param>
    ''' <param name="DefaultValue">This value is returned, if Section or Key is not found</param>
    ''' <param name="BufferSize">Size of the Buffer, 1024 by default</param>
    ''' <returns>The integer Value stored in the ini file, or the default value</returns>
    ''' <remarks></remarks>
    Public Function GetINIInt(ByVal INIFile As String, ByVal Section As String, ByVal Key As String, Optional ByVal DefaultValue As Integer = 0, Optional ByVal BufferSize As Integer = 1024) As String
        Dim sTemp As String = Space(BufferSize)
        Dim Length As Integer = GetPrivateProfileString(Section, Key, DefaultValue, sTemp, BufferSize, INIFile)
        Return Left(sTemp, Length)
    End Function

    ''' <summary>
    ''' Gets a String Value from a specified Ini-File
    ''' </summary>
    ''' <param name="INIFile">The File to read from. Full path</param>
    ''' <param name="Section">The Section name</param>
    ''' <param name="Key">Name of the Key to read</param>
    ''' <param name="DefaultValue">This value is returned, if Section or Key is not found</param>
    ''' <param name="BufferSize">Size of the Buffer, 1024 by default</param>
    ''' <returns>The String Value stored in the ini file, or the default value</returns>
    ''' <remarks></remarks>
    Public Function GetINIString(ByVal INIFile As String, ByVal Section As String, ByVal Key As String, Optional ByVal DefaultValue As String = "", Optional ByVal BufferSize As Integer = 1024) As String
        Dim sTemp As String = Space(BufferSize)
        Dim Length As Integer = GetPrivateProfileString(Section, Key, DefaultValue, sTemp, BufferSize, INIFile)
        Return Left(sTemp, Length)
    End Function

    ''' <summary>
    ''' Sets an ini Key to specified value
    ''' </summary>
    ''' <param name="INIFile">The File to write to. Full path</param>
    ''' <param name="Section">The Section name</param>
    ''' <param name="Key">Name of the Key to update</param>
    ''' <param name="Value">The Value to set</param>
    ''' <remarks></remarks>
    Public Sub SetINIString(ByVal INIFile As String, ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, INIFile)
    End Sub

    ''' <summary>
    ''' Sets an ini Key to specified value
    ''' </summary>
    ''' <param name="INIFile">The File to write to. Full path</param>
    ''' <param name="Section">The Section name</param>
    ''' <param name="Key">Name of the Key to update</param>
    ''' <param name="Value">The Value to set</param>
    ''' <remarks></remarks>
    Public Sub SetINIInt(ByVal INIFile As String, ByVal Section As String, ByVal Key As String, ByVal Value As Integer)
        WritePrivateProfileStringI(Section, Key, Value, INIFile)
    End Sub
End Module
