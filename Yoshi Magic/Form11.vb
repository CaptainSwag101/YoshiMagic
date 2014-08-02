Public Class Form11
    Dim db1() As Integer = {&H1C3A00, &H1D3400, &H1E5800, &H1F9C00, &H209400, &H21FE00, &H232C00, &H294000, &H29DA00, &H2B4A00, &H2C5800, &H2DAE00, &H2F3A00, &H319A00}
    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub NumericUpDown10_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        'SCRIPT VIEWER = SCRIPT # CHANGED = LOAD SCRIPT
        scriptview()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        scriptview()
    End Sub
    Sub scriptview()
        ListBox1.Items.Clear()
        Dim tempi As Integer
        FileGet(1, tempi, db1(ComboBox1.SelectedIndex) + (NumericUpDown1.Value << 2) + 1)
        Dim scriptcur As Integer = db1(ComboBox1.SelectedIndex) + tempi
        FileGet(1, tempi, scriptcur + 1)
        scriptcur += tempi
        'Label1.Text = Hex(scriptcur)
        Dim cmd As Short
        Dim memwrite As Short
        Dim varflags As Short
        Dim dataargs(31) As Short
        For c = 0 To 499
            Dim lbstring As String = "x" & scriptcur.ToString("X8") & ":"
            FileGet(1, cmd, scriptcur + 1) : scriptcur += 2
            lbstring = lbstring & " " & cmd.ToString("X4")
            'If cmd = 0 Then lbstring = lbstring & " (End)" : ListBox2.Items.Add(lbstring) : Exit For
            'If cmd = 1 Then lbstring = lbstring & " (Return)" : ListBox2.Items.Add(lbstring) : Exit For
            Dim sattr As Byte
            FileGet(1, sattr, &H10DEC8 + (cmd << 2) + 1)
            If sattr And &H20 Then 'Write-to flag.
                FileGet(1, memwrite, scriptcur + 1) : scriptcur += 2
                lbstring = lbstring & " " & memwrite.ToString("X4")
            Else
                'memwrite = 0
                lbstring &= " ----"
            End If
            If sattr And &H40 Then 'Flags flag.
                FileGet(1, varflags, scriptcur + 1) : scriptcur += 2
                lbstring &= " " & varflags.ToString("X4")
            Else
                varflags = 0
                lbstring &= " ----"
            End If
            If sattr And &H1F <> 0 Then
                For args = 0 To (sattr And &H1F) - 1
                    FileGet(1, dataargs(args), scriptcur + 1) : scriptcur += 2
                    lbstring = lbstring & " " & dataargs(args).ToString("X4")
                Next
            End If
            lbstring = lbstring & " : "
            Select Case cmd
                Case &H0
                    lbstring &= "Return all"
                Case &H1
                    lbstring &= "Return"
                Case &H2
                    Dim var1 As String
                    Select Case dataargs(0)
                        Case 0 : var1 = "Jump"
                        Case 1 : var1 = "Call"
                        Case Else : var1 = "?"
                    End Select
                    lbstring &= var1 & " to x" & (scriptcur + (dataargs(1) << 1)).ToString("X8")
                Case &H4
                    lbstring &= "(Conditional Jump)"
                Case &HF
                    lbstring &= "[x" & memwrite.ToString("X4") & "] = " & argit(varflags And 1, dataargs(0))
                Case &H10
                    lbstring &= "[x" & memwrite.ToString("X4") & "] = " & argit(varflags And 1, dataargs(0)) & " + " & argit((varflags >> 1) And 1, dataargs(1))
                Case &H23
                    lbstring &= "[x" & memwrite.ToString("X4") & "] = a random number below " & argit(varflags And 1, dataargs(0))
                Case &H35
                    Dim desc1 As String
                    Select Case dataargs(0)
                        Case Is < &H48
                            desc1 = " (sprite), sprite x"
                        Case Else
                            desc1 = " (enemy), enemy x"
                    End Select
                    lbstring &= "Load into data slot x" & dataargs(0).ToString("X4") & desc1 & dataargs(2).ToString("X4") & dataargs(1).ToString("X4")
                Case &H3B
                    Dim desc1 As String
                    Select Case dataargs(0)
                        Case &H38 To &H3B
                            desc1 = " (ally " & dataargs(0) - &H38 & ") "
                        Case &H3C To &H43
                            desc1 = " (enemy " & dataargs(0) - &H3C & ") "
                        Case Else
                            desc1 = " "
                    End Select
                    Dim desc2 As String
                    Select Case dataargs(1)
                        Case Is < &H48
                            desc2 = " (sprite) "
                        Case Else
                            desc2 = " (enemy) "
                    End Select
                    lbstring &= "Link object slot " & argit(varflags And 1, dataargs(0)) & desc1 & "to data slot " & argit((varflags >> 1) And 1, dataargs(1)) & desc2
                Case &H52
                    Dim desc1 As String
                    Select Case dataargs(0)
                        Case &H38 To &H3B
                            desc1 = " (ally " & dataargs(0) - &H38 & ") "
                        Case &H3C To &H43
                            desc1 = " (enemy " & dataargs(0) - &H3C & ") "
                        Case Else
                            desc1 = " "
                    End Select
                    lbstring &= "Position object " & argit(varflags And 1, dataargs(0)) & desc1 _
                        & "at X:" & argit((varflags >> 1) And 1, dataargs(1)) _
                        & " Y:" & argit((varflags >> 2) And 1, dataargs(2)) _
                        & " Z:" & argit((varflags >> 3) And 1, dataargs(3))
            End Select
            ListBox1.Items.Add(lbstring)
        Next
    End Sub
    Function argit(ByVal flag As Boolean, ByVal value As Short)
        If flag Then
            Return "[x" & value.ToString("X4") & "]"
        Else
            Return "x" & value.ToString("X4")
        End If
    End Function
End Class