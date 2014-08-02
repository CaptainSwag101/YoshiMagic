Public Class Form8
    Dim decdata(&H4F4FF) As Byte
    Dim decsize As Integer
    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arm9binaddr As Integer
        Dim arm9binsize As Integer
        Dim locaddr As Integer
        FileGet(1, arm9binaddr, &H20 + 1)
        FileGet(1, arm9binsize, &H2C + 1)
        locaddr = arm9binaddr + arm9binsize
        Dim h1, h2 As Integer
        FileGet(1, h1, locaddr - 8 + 1)
        FileGet(1, h2, locaddr - 4 + 1)
        locaddr -= (h1 >> &H18)
        h2 += (h1 And &HFFFFFF)
        decsize = h2
        'Dispose(h1)
        Dim d1, d3, d4 As Byte, d2 As SByte
newset: If h2 <= 0 Then GoTo exit1
        locaddr -= 1
        FileGet(1, d1, locaddr + 1)
        d2 = 8
nextb:  d2 -= 1
        If d2 < 0 Then GoTo newset
        If d1 And &H80 Then GoTo distlen
        locaddr -= 1
        FileGet(1, d3, locaddr + 1)
        h2 -= 1
        decdata(h2) = d3
        GoTo nexta
distlen: locaddr -= 1
        FileGet(1, d3, locaddr + 1)
        locaddr -= 1
        FileGet(1, d4, locaddr + 1)
        h1 = (((CInt(d3) << 8) Or d4) And &HFFF) + 2
        Dim d5 As Integer
        d5 = d3 + &H20
n1:     d4 = decdata(h2 + h1)
        h2 -= 1
        decdata(h2) = d4
        d5 -= &H10
        If d5 >= 0 Then GoTo n1
nexta:  d1 = d1 << 1
        If h2 > 0 Then GoTo nextb
exit1:
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileOpen(2, SaveFileDialog1.FileName, OpenMode.Binary, OpenAccess.Default, OpenShare.Shared)
        For a As Integer = 0 To decsize - 1
            'Dim temp As Byte =
            FilePut(2, decdata(a))
        Next
        FileClose(2)
    End Sub
End Class