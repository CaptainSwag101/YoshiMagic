Public Class Form7

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo end1
        'FileOpen(1, Form1.OpenFileDialog1.FileName, OpenMode.Binary)
        Dim address As Integer = 0
        Dim tvalue As Short 'Thumb Value
        Dim tvalue2 As UShort 'Thumb value
        Dim tvalue3 As Integer
        Dim branchto As Integer
        Seek(1, 1)
        If ModifierKeys = Keys.Control Then
            For a = 0 To &HD0000
                FileGet(1, tvalue) ', address + 1)
                If (tvalue And &HFFC0) = &H980 Then ListBox1.Items.Add((Loc(1) - 2).ToString("X8") & ":" & Hex(tvalue) & "  lsr")
                If (tvalue And &HFFC0) = &H7F00 Then ListBox1.Items.Add((Loc(1) - 2).ToString("X8") & ":" & Hex(tvalue) & "  ldr")
                'If (tvalue And &HF8FF) = &H2040 Then ListBox1.Items.Add((Loc(1) - 2).ToString("X8") & ":" & Hex(tvalue) & "  mov")
                'Dim pc As Integer = Loc(1)
                'If tvalue2 >> 11 = 9 Then 'THUMB6 (PC-RELATIVE load)
                '    Dim intval As Integer
                '    FileGet(1, intval, ((tvalue2 And &HFF) << 2) + ((Loc(1) + 3) And &HFFFDS))
                '    Seek(1, pc + 1)
                '    If (intval >> 24) = 8 Or (intval >> 24) = 2 Or (intval >> 24) = 3 Or (intval >> 24) = 4 Or (intval >> 24) = 5 Or (intval >> 24) = 6 Or (intval >> 24) = 7 Then
                '        ListBox1.Items.Add("LDR: " & (Loc(1) - 2).ToString("X8") & "   Offset: " & Loc(1).ToString("X8") & "   Pointer: " & intval.ToString("X8"))
                '    End If
                '    'ListBox4.Items.Add(((pc - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " ldr " & rlist(tvalu >> 8 And 3) & ", [$" & ((Loc(1) - 4) Or &H8000000).ToString("X8") & "] (=$" & intval.ToString("X8") & ")")
                'End If
                'Seek(1, pc + 1)
            Next
            ListBox1.Items.Add(Hex(Loc(1)))
            Exit Sub
            ListBox1.Items.Add("Congratulations! You have found a hidden feature. Listing databases.")
            For a = 0 To &HD0000
                FileGet(1, tvalue) ', address + 1)
                tvalue2 = tvalue And &HFFFF
                Dim pc As Integer = Loc(1)
                If tvalue2 >> 11 = 9 Then 'THUMB6 (PC-RELATIVE load)
                    Dim intval As Integer
                    FileGet(1, intval, ((tvalue2 And &HFF) << 2) + ((Loc(1) + 3) And &HFFFDS))
                    Seek(1, pc + 1)
                    If (intval >> 24) = 8 Or (intval >> 24) = 2 Or (intval >> 24) = 3 Or (intval >> 24) = 4 Or (intval >> 24) = 5 Or (intval >> 24) = 6 Or (intval >> 24) = 7 Then
                        ListBox1.Items.Add("LDR: " & (Loc(1) - 2).ToString("X8") & "   Offset: " & Loc(1).ToString("X8") & "   Pointer: " & intval.ToString("X8"))
                    End If
                    'ListBox4.Items.Add(((pc - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " ldr " & rlist(tvalu >> 8 And 3) & ", [$" & ((Loc(1) - 4) Or &H8000000).ToString("X8") & "] (=$" & intval.ToString("X8") & ")")
                End If
                Seek(1, pc + 1)
            Next
            ListBox1.Items.Add(Hex(Loc(1)))
            Exit Sub
        End If
        Do
            For a = 0 To ((ComboBox1.SelectedIndex + 1) << 20) - 1 '400000, 800000, C00000, 1000000, 1400000

                FileGet(1, tvalue) ', address + 1)
                tvalue2 = tvalue And &HFFFF
                If tvalue2 >= &HF000 Then
                    'blval = tvalue2 And &H7FF
                    FileGet(1, tvalue) ', address + 2 + 1)
                    tvalue3 = tvalue And &HFFFF
                    'If tvalue3 >= &HE000 Then
                    branchto = Loc(1) - ((tvalue2 And &H400) << 12) + ((tvalue2 And &H3FF) << 12) + ((tvalue3 And &H7FF) << 1)
                    If (NumericUpDown1.Value And &HFFFES) = branchto Then
                        'branchto = Loc(1) - ((tvalue2 And &H400) << 11) + ((tvalue2 And &H3FF) << 11) + ((tvalue3 And &H7FF))
                        'If (NumericUpDown1.Value >> 1) = branchto Then
                        'MsgBox(Hex(address) & " - " & Hex(tvalue2) & " " & Hex(tvalue3) & " - " & Hex(branchto))
                        ListBox1.Items.Add(Hex(Loc(1) - 4) & " - " & Hex(tvalue2) & " " & Hex(tvalue3) & " - " & Hex(branchto))
                        ListBox1.Refresh()
                    End If
                    'address += 2
                    'End If
                End If
                'address += 2
            Next
            Select Case MsgBox("Continue from address " & Hex(Loc(1)) & "?", MsgBoxStyle.YesNo)
                Case MsgBoxResult.No
                    Exit Do
            End Select
        Loop
        ListBox1.Items.Add("Stopped at: " & Hex(Loc(1)))
        'FileClose(1)
        Exit Sub
end1:   ListBox1.Items.Add("Error at: " & Hex(Loc(1)))
        'FileClose(1)
    End Sub

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub
    'IMAGE VIEWER
    Dim palbmp As New Bitmap(256, 256)
    Dim b(255) As Color
    Dim data2(&H87FFF) As Byte
    Dim data3(&H7FFF) As Byte 'Short
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If Focused = 0 Then MsgBox("TEST")
        'ON PALETTE SET CHANGE (GET PALETTE DATA > TURN TO COLORS > DISPLAY IN PALETTE BOX > UPDATE COMPRESSED IMAGES > UPDATE TILESETS)
        Dim palette1 As Short
        'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?) Also, move outside SUB.
        'Dim palette1(255) As Short
        'Dim b(255) As Color ' = Color.FromArgb(0, 0, 0) 'Red
        'PALETTE SECTION
        'Get Palette #
        'Dim nval As Integer = NumericUpDown1.Value
        Seek(1, NumericUpDown2.Value + 1)
        'Seek(1, &H8D49A0 + (&H1E0 * nval) + 1) '***THE ALTERNATE PALETTE DATABASE*** Not sure how it's used.
        For row = 0 To 15
            For col = 0 To 15
                '//Get Palette data.
                FileGet(1, palette1)
                'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?)
                'FileGet(1, palette1(row * 16 + col)) ', &H8C88C8 + (&H1E0 * nval) + ((row * 16 + col) * 2 + 1))
                'FileGet(1, palette1(row * 16 + col)) ', &H8D49A0 + (&H1E0 * nval) + ((row * 16 + col) * 2 + 1))
                '//Turn data into colors.
                b((row << 4) + col) = Color.FromArgb((palette1 And &H1F) << 3, (palette1 >> 5 And &H1F) << 3, (palette1 >> 10 And &H1F) << 3)
                'b((row << 4) + col) =

                'Imaging.BitmapData.PixelFormat '=
                'palbmp.PixelFormat.Format16bppRgb555() '= Imaging.BitmapData.PixelFormat '.Format16bppRgb555()
                'MsgBox(palbmp.PixelFormat.)
                '2498570
                'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?)
                'b(row * 16 + col) = Color.FromArgb((palette1(row * 16 + col) And &H1F) * 8, (palette1(row * 16 + col) >> 5 And &H1F) * 8, (palette1(row * 16 + col) >> 10 And &H1F) * 8) 'row * 32, col * 16, row + col)
                '//Display the palettes. (8x8 a color)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b((row << 4) + col))
                    Next
                Next
                'For xypix = 0 To 63
                'Next
            Next
        Next
        PictureBox1.Image = palbmp
        'Me.TabPage4.
        'CreateGraphics.DrawImageUnscaled(palbmp, New Point(0, 0)) 'PictureBox6.Left, PictureBox6.Top))
        'CreateGraphics.c()
        'image3(False) 'Updates the 3 "compressed" images
        'tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        'tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        'layer1(Panel4, layer1bmp, 0) 'Updates the map.
        'layer1(Panel6, layer2bmp, 1)
        'layer1(Panel7, layer3bmp, 2)
    End Sub 'Palette Display button
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim palette1 As Short
        Randomize()
        For row = 0 To 15
            For col = 0 To 15
                palette1 = (Rnd() * &H8000) And &H7FFF
                b((row << 4) + col) = Color.FromArgb((palette1 And &H1F) << 3, (palette1 >> 5 And &H1F) << 3, (palette1 >> 10 And &H1F) << 3)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b((row << 4) + col))
                    Next
                Next
            Next
        Next
        PictureBox1.Image = palbmp
    End Sub 'Random button
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Select Case ComboBox2.SelectedIndex
            'Case -1
            'MsgBox("Select a compression.")
            'Exit Sub
            Case -1, 0 'Uncompressed
                Seek(1, NumericUpDown3.Value + 1)
                Dim databyte As Byte
                For num = 0 To &H7FFF
                    FileGet(1, databyte)
                    data2(num) = databyte
                Next
            Case 1 'LZSS (?) Compression
                Array.Clear(data2, 0, &H8000)
                Seek(1, NumericUpDown3.Value + 1)
                decomp()
            Case 2
                Array.Clear(data2, 0, &H8000)
                Seek(1, NumericUpDown3.Value + 1)
                mldsdecomp(data2)
        End Select
        Dim bmp As New Bitmap(256, 256)
        Dim pix As Byte
        'Timer1.Start()
        'For row = 0 To 7  '&H4000 
        '    For col = 0 To 31 '&H0800 
        '        For ypix = 0 To 7 '&H0040 
        '            For xpix = 0 To 3 '&H0008 '16*8
        '                For xypix = 0 To 1 '&H0002
        '                    'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
        '                    'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
        '                    pix = data2((numb << 13) + (row << 10) + (col << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
        '                    bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b(pix + (NumericUpDown5.Value << 4)))
        '                Next
        '            Next
        '        Next
        '    Next
        'Next
        'Timer1.Stop()
        'MsgBox(ms)
        'Timer1.Start()
        If CheckBox1.Checked Then
            For a = 0 To &HFFFF
                pix = data2(a)
                bmp.SetPixel(((a And &H7C0) >> 3) + (a And &H7), ((a And &HF800) >> 8) + ((a And &H38) >> 3), b(pix)) ' + (NumericUpDown5.Value << 4)))
            Next
        Else
            For a = 0 To &HFFFF
                pix = data2(a >> 1) >> ((a And 1) << 2) And &HF
                bmp.SetPixel(((a And &H7C0) >> 3) + (a And &H7), ((a And &HF800) >> 8) + ((a And &H38) >> 3), b(pix)) ' + (NumericUpDown5.Value << 4)))
            Next
        End If
        PictureBox2.Image = bmp
    End Sub 'Pixel Data Display button
    Sub decomp() 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1 As Byte, num As Integer  'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
        FileGet(1, data1) ', Loc(1) + 1)  'FileGet(1, data1, offset1 + 1) 'offset1 += (data1 >> 6) + 1
        Seek(1, Loc(1) + (data1 >> 6) + 1) '+1  'Seek(1, offset1 + (data1 >> 6) + 1 + 1)
        For a = 0 To &HFFF
            FileGet(1, arg1) ', offset1 + 1) 'Get argument 'offset1 += 1
            Select Case arg1 >> 5
                Case Is < 4 '0-7F
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    If arg1 = &H7F And data1 = &HFF Then Exit For
                    For c = 0 To (arg1 >> 2) + 1
                        data2(num) = data2(num + &HFC00S + ((arg1 And &H3) << 8) + data1) : num += 1
                    Next
                Case 4 '80-9F
                    For c = 0 To (arg1 And &H1F)
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data2(num) = data1 : num += 1
                    Next
                Case 5  'A0-BF
                    For c = 0 To (arg1 And &H1F)
                        data2(num) = 0 : num += 1
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data2(num) = data1 : num += 1
                    Next
                Case 6 'C0-DF
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    For c = 0 To (arg1 And &H1F) + 1
                        data2(num) = data1 : num += 1
                    Next
                Case 7
                    For c = 0 To arg1 And &H1F
                        data2(num) = 0 : num += 1
                    Next
                    If arg1 = &HFF Then
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1 
                        For c = 0 To data1 - 1 '+ &H1F
                            data2(num) = 0 : num += 1
                        Next
                    End If
            End Select
        Next
    End Sub
    Sub mldsdecomp(ByVal data2() As Byte) 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1, data1a As Byte, num As Integer  'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
        'HEADER DATA! YAY!
        Dim blen As Byte
        Dim decsize As Integer
        Dim comblks As Integer
        FileGet(1, data1) ', Loc(1) + 1)  'FileGet(1, data1, offset1 + 1) 'offset1 += (data1 >> 6) + 1 'Decompressed Size
        blen = data1 >> 6
        decsize = data1 And &H3F
        If blen = 0 Then GoTo a1
        FileGet(1, data1)
        decsize = decsize Or (CInt(data1) << 6)
        blen -= 1 : If blen = 0 Then GoTo a1
        FileGet(1, data1)
        decsize = decsize Or (CInt(data1) << &HC)
        blen -= 1 : If blen = 0 Then GoTo a1
        FileGet(1, data1)
        decsize = decsize Or (CInt(data1) << &H12)
a1:     FileGet(1, data1) 'Compression Blocks
        blen = data1 >> 6
        comblks = data1 And &H3F
        If blen = 0 Then GoTo a2
        FileGet(1, data1)
        comblks = comblks Or (CInt(data1) << 6)
        blen -= 1 : If blen = 0 Then GoTo a2
        FileGet(1, data1)
        comblks = comblks Or (CInt(data1) << &HC)
        blen -= 1 : If blen = 0 Then GoTo a2
        FileGet(1, data1)
        comblks = comblks Or (CInt(data1) << &H12)
a2:     'COMPRESSION BLOCKS! YAY AGAIN!
        For cblk = 0 To comblks
            'To-be-continued. (16-bit header)
            Seek(1, Loc(1) + 2 + 1)
            'MsgBox(Hex(Loc(1)) & ":" & Hex(data1))
            'Seek(1, Loc(1) + 3 + (data1 >> 6) + 1) '+1  'Seek(1, offset1 + (data1 >> 6) + 1 + 1)
            'Dim debug1 As Integer
            'Dim debug2 As Integer
            For a = 0 To &HFF
                FileGet(1, arg1) ', offset1 + 1) 'Get argument 'offset1 += 1
                'debug1 = arg1
                'MsgBox(Hex(Loc(1)) & ":" & Hex(arg1))
                For c = 0 To 3
                    Select Case arg1 And 3
                        Case 0
                            'MsgBox(Hex(Loc(1)) & " - " & Hex(num) & " (" & Hex(debug2) & " " & Hex(arg1) & ")")
                            GoTo a3
                        Case 1
                            FileGet(1, data1)
                            data2(num) = data1 : num += 1
                        Case 2
                            FileGet(1, data1)
                            FileGet(1, data1a)
                            For d = 0 To (data1a And &HF) + 1
                                data2(num) = data2(num - (((data1a And &HF0) << 4) Or data1)) : num += 1
                            Next
                        Case 3
                            FileGet(1, data1)
                            FileGet(1, data1a)
                            For d = 0 To data1 + 1
                                data2(num) = data1a : num += 1
                            Next
                    End Select
                    arg1 = arg1 >> 2
                Next
                'debug2 = debug1
            Next
a3:
        Next
    End Sub
    Sub decomp3() 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1 As Byte, num As Integer  'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
        FileGet(1, data1) ', Loc(1) + 1)  'FileGet(1, data1, offset1 + 1) 'offset1 += (data1 >> 6) + 1
        Seek(1, Loc(1) + (data1 >> 6) + 1) '+1  'Seek(1, offset1 + (data1 >> 6) + 1 + 1)
        For a = 0 To &HFFF
            FileGet(1, arg1) ', offset1 + 1) 'Get argument 'offset1 += 1
            Select Case arg1 >> 5
                Case Is < 4 '0-7F
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    If arg1 = &H7F And data1 = &HFF Then Exit For
                    For c = 0 To (arg1 >> 2) + 1
                        data3(num) = data3(num + &HFC00S + ((arg1 And &H3) << 8) + data1) : num += 1
                    Next
                Case 4 '80-9F
                    For c = 0 To (arg1 And &H1F)
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data3(num) = data1 : num += 1
                    Next
                Case 5  'A0-BF
                    For c = 0 To (arg1 And &H1F)
                        data3(num) = 0 : num += 1
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data3(num) = data1 : num += 1
                    Next
                Case 6 'C0-DF
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    For c = 0 To (arg1 And &H1F) + 1
                        data3(num) = data1 : num += 1
                    Next
                Case 7
                    For c = 0 To arg1 And &H1F
                        data3(num) = 0 : num += 1
                    Next
                    If arg1 = &HFF Then
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1 
                        For c = 0 To data1 - 1 '+ &H1F
                            data3(num) = 0 : num += 1
                        Next
                    End If
            End Select
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Seek(1, NumericUpDown4.Value + 1)
        Select Case ComboBox3.SelectedIndex
            'Case -1
            'MsgBox("Select a compression.")
            'Exit Sub
            Case -1, 0 'Uncompressed
                Seek(1, NumericUpDown4.Value + 1)
                Dim databyte As Short 'Byte
                For num = 0 To &H7FFF
                    FileGet(1, databyte)
                    data3(num) = databyte
                Next
            Case 1 'LZSS (?) Compression
                Array.Clear(data3, 0, &H8000)
                Seek(1, NumericUpDown3.Value + 1)
                decomp3()
            Case 2
                Array.Clear(data3, 0, &H8000)
                Seek(1, NumericUpDown3.Value + 1)
                Try
                    mldsdecomp(data3)
                Catch
                End Try
        End Select
        'Dim mdata As Short
        Dim bmp As New Bitmap(256, 256)
        Dim pix As Byte
        'Timer1.Start()
        Try
            If CheckBox1.Checked Then
                For row = 0 To 31  '&H4000 
                    For col = 0 To 31 '&H0800 65
                        'FileGet(1, mdata)
                        For ypix = 0 To 7 '&H0040 
                            For xpix = 0 To 7 '&H0008 '16*8
                                'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                pix = data2((CInt(data3((row << 5) Or col)) << 6) + (ypix << 3) + xpix)
                                bmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b(pix)) ' + (NumericUpDown5.Value << 4)))
                            Next
                        Next
                    Next
                Next
            Else
                For row = 0 To 31  '&H4000 
                    For col = 0 To 31 '&H0800 65
                        'FileGet(1, mdata)
                        For ypix = 0 To 7 '&H0040 
                            For xpix = 0 To 3 '&H0008 '16*8
                                For xypix = 0 To 1 '&H0002
                                    'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                    'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                    pix = data2((data3((row << 5) Or col) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b(pix)) ' + (NumericUpDown5.Value << 4)))
                                Next
                            Next
                        Next
                    Next
                Next
            End If
        Catch
            MsgBox(Hex(Loc(1)))
        End Try
        'Timer1.Stop()
        'MsgBox(ms)
        'Timer1.Start()
        'For a = 0 To &HFFFF
        '    pix = data2(a >> 1) >> ((a And 1) << 2) And &HF
        '    bmp.SetPixel(((a And &H7C0) >> 3) + (a And &H7), ((a And &HF800) >> 8) + ((a And &H38) >> 3), b(pix)) ' + (NumericUpDown5.Value << 4)))
        'Next
        PictureBox3.Image = bmp
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        NumericUpDown2.Value = &H7C384C '&H201790
        Button2.PerformClick()
        NumericUpDown3.Value = &H7C3A4C '&H2017B0
        Button4.PerformClick()
        NumericUpDown4.Value = &H7CC3B8 '&H201210
    End Sub
    'Compressor
    Dim cdata(&HFFFF) As Byte 'Compressed data
    Dim ddata(&HFFFF) As Byte 'Decompressed data
    Dim rdata(&HFFFF) As Byte 'Recompressed data
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Seek(1, NumericUpDown6.Value + 1)
        decomprec()
        Seek(1, NumericUpDown6.Value + 1)
        compress()
    End Sub 'Recompression button
    Sub compress()
        ListBox4.Items.Add("Compression is in Beta.")
        Dim num2 = 0
        Dim numc = 2 'Temp
        Dim datacomp(1000) As Byte
        For c = 0 To 5 '# of arguments..
            Dim compstr As String = ""
            For a = 0 To &H1F 'ARG 80-9F
                'ARG E0-FF
                If data2(num2) = 0 Then
                    If data2(num2 + 1) = 0 Then
                        If a > 0 Then
                            num2 -= a
                            datacomp(numc) = &H80 Or a
                            numc += 1
                            For d = 0 To a - 1
                                datacomp(numc) = data2(num2)
                                numc += 1
                                num2 += 1
                            Next
                        End If
                        For z = 0 To &H1F '+ &H7F 'Value of attr.
                            If data2(num2) = 0 Then datacomp(numc) = &HE0 + z Else compstr = compstr & datacomp(numc).ToString("X2") & " " : GoTo nextarg
                            num2 += 1 'next uncomp byte
                        Next
                        'd-atacomp(numc) = &HFF
                        compstr = compstr & datacomp(numc).ToString("X2") & " "
                        numc += 1 'next attr
                        For z2 = 0 To &H7F
                            '+&H20
                            If data2(num2) = 0 Then datacomp(numc) = z2 + 1 Else compstr = compstr & datacomp(numc).ToString("X2") & " " : GoTo nextarg
                            num2 += 1 'next uncomp byte
                        Next

                    Else
                        'A0-BF
                    End If
                End If

                If data2(num2) = data2(num2 + 1) Then
                    If a > 0 Then
                        num2 -= a
                        datacomp(numc) = &H80 Or a
                        numc += 1
                        For d = 0 To a - 1
                            datacomp(numc) = data2(num2)
                            numc += 1
                            num2 += 1
                        Next
                    End If
                    'C0-DF
                    Dim abyte As Byte = data2(num2)
                    num2 += 1
                    For z = 0 To &H1F '+ &H7F 'Value of attr.
                        If data2(num2) = abyte Then datacomp(numc) = &HC0 + z Else Exit For
                        num2 += 1 'next uncomp byte
                    Next
                    compstr = compstr & datacomp(numc).ToString("X2") & " "
                    numc += 1
                    datacomp(numc) = abyte
                    compstr = compstr & datacomp(numc).ToString("X2") & " "
                    GoTo nextarg
                End If

                'For a = 0 To &H1F
                '80-9F
                num2 += 1
            Next
            '67D188
nextarg:    ListBox4.Items.Add(compstr)
            numc += 1
        Next
    End Sub
    Sub decomprec() 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1 As Byte, num As Integer ', cnum, dnum As Integer  'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
        FileGet(1, data1) ', Loc(1) + 1)  'FileGet(1, data1, offset1 + 1) 'offset1 += (data1 >> 6) + 1
        'FileGet(1, cdata(cnum)) : cnum += 1
        Seek(1, Loc(1) + (data1 >> 6) + 1) '+1  'Seek(1, offset1 + (data1 >> 6) + 1 + 1)
        For a = 0 To &HFFF
            FileGet(1, arg1) ', offset1 + 1) 'Get argument 'offset1 += 1
            Select Case arg1 >> 5
                Case Is < 4 '0-7F
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    ListBox2.Items.Add(arg1.ToString("X2") & " " & data1.ToString("X2"))
                    If arg1 = &H7F And data1 = &HFF Then Exit For
                    Dim udatastr As String = num.ToString("X4") & ": "
                    For c = 0 To (arg1 >> 2) + 1
                        data2(num) = data2(num + &HFC00S + ((arg1 And &H3) << 8) + data1) : num += 1
                        udatastr = udatastr & data2(num - 1).ToString("X2") & " "
                    Next
                    ListBox3.Items.Add(udatastr)
                Case 4 '80-9F 
                    Dim cdatastr As String = ""
                    Dim udatastr As String = num.ToString("X4") & ": "
                    For c = 0 To (arg1 And &H1F)
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data2(num) = data1 : num += 1
                        cdatastr = cdatastr & data1.ToString("X2") & " "
                        udatastr = udatastr & data1.ToString("X2") & " "
                        '67D188
                    Next
                    ListBox2.Items.Add(arg1.ToString("X2") & " " & cdatastr)
                    ListBox3.Items.Add(udatastr)
                Case 5  'A0-BF
                    Dim cdatastr As String = ""
                    Dim udatastr As String = num.ToString("X4") & ": "
                    For c = 0 To (arg1 And &H1F)
                        data2(num) = 0 : num += 1
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                        data2(num) = data1 : num += 1
                        cdatastr = cdatastr & data1.ToString("X2") & " "
                        udatastr = udatastr & "00 " & data1.ToString("X2") & " "
                    Next
                    ListBox2.Items.Add(arg1.ToString("X2") & " " & cdatastr)
                    ListBox3.Items.Add(udatastr)
                Case 6 'C0-DF
                    FileGet(1, data1) ', offset1 + 1) 'offset1 += 1
                    Dim udatastr As String = num.ToString("X4") & ": "
                    For c = 0 To (arg1 And &H1F) + 1
                        data2(num) = data1 : num += 1
                        udatastr = udatastr & data1.ToString("X2") & " "
                    Next
                    ListBox2.Items.Add(arg1.ToString("X2") & " " & data1.ToString("X2"))
                    ListBox3.Items.Add(udatastr)
                Case 7
                    Dim cdatastr As String = ""
                    Dim udatastr As String = num.ToString("X4") & ": "
                    For c = 0 To arg1 And &H1F
                        data2(num) = 0 : num += 1
                        udatastr = udatastr & "00 "
                    Next
                    If arg1 = &HFF Then
                        FileGet(1, data1) ', offset1 + 1) 'offset1 += 1 
                        cdatastr = data1.ToString("X2")
                        For c = 0 To data1 - 1 '+ &H1F
                            data2(num) = 0 : num += 1
                            udatastr = udatastr & "00 "
                        Next
                    End If
                    ListBox2.Items.Add(arg1.ToString("X2") & " " & cdatastr)
                    ListBox3.Items.Add(udatastr)
            End Select
        Next
    End Sub
End Class