Public Class Form6
    Dim address0 As Integer 'For script
    Dim address1 As Integer

    Dim offset1 As Integer
    Dim command As Byte 'Command
    Dim args As Byte 'Arguments
    Dim args2 As Byte 'Arguments- Same as "args" but used in calculation of bits,
    Dim commandflag As Byte 'Command Flag
    Dim offsetshort As Short
    Dim numofbits As Byte
    Dim bitcount As Integer

    Dim argbyte As Byte
    Dim argnum As Long
    Dim bytestring As String
    Dim argstring As String
    Dim bitnum As SByte = 8

    Dim commanddesc As String

    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'bitcount = 0
        ListBox1.Items.Clear()
        address0 = NumericUpDown1.Value
        address1 = NumericUpDown1.Value
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For b = 0 To 99
            bytestring = ""
            argstring = ""
            bitnum = 8
            argnum = 0
            FileGet(1, command, address0 + 1) '//Get Script Command
            address0 += 1
            'Check Database 1
            FileGet(1, offset1, &H3B98C4 + (ComboBox1.SelectedIndex << 2) + 1) '//Get 1 of 3 pointers for # of Args database
            FileGet(1, args, offset1 - &H8000000 + command + 1) '//Get Number of Args Byte
            bitcount = args
            bitread(args)
            argstring += CStr(Hex(argnum)) : argnum = 0
            'Check Database 2
            FileGet(1, offset1, &H3B9704 + (ComboBox1.SelectedIndex << 2) + 1) '//Get 1 of 3 pointers for Special Arg Flag(?) database
            FileGet(1, commandflag, offset1 - &H8000000 + command + 1) '//Get Special Arg Flag(?)
            Dim aind As Byte = 0
            Dim argvals(16) As Long
            If commandflag = 1 Then bitcount += &HD : bitread(&HD) : argstring += CStr(", " & Hex(argnum)) _
                : argvals(aind) = argnum : aind += 1 : argnum = 0 _
                Else argstring += CStr(", " & "X")
            'Do Database 3
            FileGet(1, offset1, &H3BA4A8 + (ComboBox1.SelectedIndex << 2) + 1) '//Get 1 of 3 pointers for offsetarg database
            FileGet(1, offsetshort, offset1 - &H8000000 + (command << 1) + 1)
            'Check Database 4
            FileGet(1, offset1, &H3B9D00 + (ComboBox1.SelectedIndex << 2) + 1) '//Get 1 of 3 pointers for num of bits/args database
            For a = 0 To args - 1
                FileGet(1, numofbits, offset1 - &H8000000 + offsetshort + a + 1)
                If numofbits >> 7 = 1 Then
                    bitcount += 32
                    If bitnum <> 8 Then
                        bitnum = 8 : address0 += 1
                    End If
                    'bitread(32)
                    FileGet(1, argnum, address0 + 1)
                    argnum = argnum And &H7FFFFFFF : address0 += 4
                    'argstring += CStr(", " & Hex(argnum)) : argnum = 0
                Else
                    bitcount += numofbits
                    bitread(numofbits)
                    'argstring += CStr(", " & Hex(argnum)) : argnum = 0
                End If
                argvals(aind) = argnum : aind += 1
                argstring += CStr(", " & Hex(argnum)) : argnum = 0
            Next
            bitcount += 7
            bitcount = bitcount >> 3
            Select Case command
                Case &H0
                    commanddesc = "End"
                Case &H1
                    commanddesc = "Return"
                Case &H2
                    Dim flag1() As String = {"Jump", "Call", "Jump(2)", "Jump(3)"}
                    commanddesc = flag1(argvals(0)) & " to " & Hex(argvals(1))
                Case &H3
                    commanddesc = "Wait for " & argvals(0) & " frames."
                Case &H4
                    commanddesc = "Using obj. #" & argvals(1) & ", call to " & Hex(argvals(2))
                Case &H5
                    Dim onoff() As String = {"Pause", "Resume"}
                    commanddesc = onoff(argvals(0)) & "Idle Script for obj. #" & argvals(1)
                Case &H18
                    commanddesc = "Pause script during dialogue."
                Case &H31
                    Dim characters() As String = {"Mario", "Luigi", "Party"}
                    Dim stat1() As String = {"Current HP", "Current BP"}
                    Dim operation() As String = {"Add", "Subtract"}
                    commanddesc = characters(argvals(0)) & "'s " & stat1(argvals(1)) & ":  " & operation(argvals(2)) & " " & argvals(3)
                Case &H34
                    Dim reward1() As String = {"Items", "Coins", "Key Items"}
                    Dim operation() As String = {"Add", "Subtract"}
                    Dim flag1 As String
                    If argvals(0) = &H1FFF Then flag1 = "Set no flags?" Else flag1 = "Set flag: " & argvals(0)
                    commanddesc = flag1 & " ; " & reward1(argvals(1)) & ": ? : " & operation(argvals(3)) & " " & argvals(4)
                Case &H38
                    commanddesc = "Change movement directions by " & argvals(0) & " degrees."
                Case &H4B
                    commanddesc = "Check for action."
                Case &H70
                    commanddesc = "Position camera on NPC."
                Case &H7C
                    commanddesc = "Disable buttons. (What is flag?)"
                Case &H87
                    commanddesc = "Warp"
                Case &H8F
                    commanddesc = "Activate Map Tile Script."
                Case &H93
                    commanddesc = "Shake screen."
                Case &H98
                    commanddesc = "Start a Battle."
                Case &H99
                    commanddesc = "Mini-game"
                Case &H9A
                    commanddesc = "Suitcase cutscene / Save menu"
                Case &H9B
                    commanddesc = "Shop"
                Case &H9C
                    commanddesc = "Display Map"
                Case &H9D
                    commanddesc = "Load file menu / Credits cutscene"
            End Select
            'ListBox1.Items.Add(Hex(address1) & " = Command: " & Hex(command) & " ; Bytes: " & Hex(bitcount) & " ; Args: " & Hex(args) & " BYTE VALUES: " & bytestring & " ARG VALUES: " & argstring)
            ListBox1.Items.Add(Hex(address1) & " = Command:" & command.ToString("X2") & " | " & argstring & " (" & commanddesc & ")")
            commanddesc = ""
            '" Bytes:" & Hex(bitcount) & " Args:" & Hex(args) &
            'address0 += 'bitcount ' + 1
            address1 += bitcount + 1
            address0 = address1
        Next
        'FileClose(1)
    End Sub

    Sub bitread(ByVal bits As Integer)
        'bitread(args)
        'Get bits from script
        Dim r6 As Integer = bitnum
        Dim r7 As Integer = 0 - bits
b2:     FileGet(1, argbyte, address0 + 1) ': bytestring += CStr(Hex(argbyte))
        r7 += r6
        If r7 >= 0 Then GoTo b1
        r6 = bits + r7
        If r6 >= 0 And r6 <= 8 Then
            argnum = (argbyte And ((1 << r6) - 1)) + (argnum << 8)
        Else
            argnum = (argbyte And &HFF) + (argnum << 8)
        End If
        r6 = 8
        address0 += 1
        'MsgBox(address0)
        GoTo b2
b1:     r6 = 8 - r7
        argnum = ((argbyte >> r7) And ((CLng(1) << bits) - 1)) + (argnum << r6)
        If r7 = 0 Then r7 = 8 : address0 += 1
        bitnum = r7
    End Sub
End Class