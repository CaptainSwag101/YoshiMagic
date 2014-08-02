﻿Public Class Form9
    'Dim decdata(&H1FFFF) As Byte
    'Dim decsize As Integer
    'Dim mapaddr1 As Integer
    Dim mapdataaddr1 As Integer
    Dim charind(4) As Integer
    Dim char1(&HFFFF) As Byte
    'Dim char2(&HFFFF) As Byte
    'Dim char3(&HFFFF) As Byte
    Dim mapdat(&H7FFFF) As Byte
    Dim charbmp1 As New Bitmap(256, 256)
    'Dim charbmp2 As New Bitmap(256, 256)
    'Dim charbmp3 As New Bitmap(256, 256)
    Dim b1(&HFF) As Color
    Dim b2(&HFF) As Color
    Dim b3(&HFF) As Color
    Dim temp As Integer
    Dim temp2 As Integer
    Private Sub Form9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For a = 0 To &H6F '&H25A '&H21D
            ListBox1.Items.Add(Hex(a))
        Next
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        'Const mapdataaddr1 = &H7C3800 
        'Const mapdataaddr1 = &HD42600
        mapdataaddr1 = &H36A600 + (ListBox1.SelectedIndex << 5)
        'Step 1:  Decompress Data!
        FileGet(1, temp, mapdataaddr1 + 1)
        Seek(1, &H36A600 + temp + 1)
        'If NumericUpDown9.Value = 0 Then
        Array.Clear(char1, 0, &HFFFF)
        mldsdecomp(char1)
        'Else
        'For a = 0 To &HFFFF
        ' FileGet(1, char1(a))
        ' Next
        ' End If
        'Step 2:  Display Palettes
        Dim palbmp1 As New Bitmap(128, 128)
        'Dim paladdr1 As Integer '= form_integer(mapdat, &HC)
        Dim palpix1(&H3FF) As Short
        FileGet(1, temp, mapdataaddr1 + 4 + 1)
        Seek(1, &H36A600 + temp + 1)

        For a = 0 To &H3FF
            FileGet(1, palpix1(a))
            'palpix1(a) = form_short(mapdat, paladdr1 + (a << 1))
        Next
        'Dim b1(&HFF) As Color
        For row = 0 To 15
            For col = 0 To 15
                b1((row << 4) + col) = Color.FromArgb((palpix1((row << 4) + col) And &H1F) << 3, (palpix1((row << 4) + col) >> 5 And &H1F) << 3, (palpix1((row << 4) + col) >> 10 And &H1F) << 3)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp1.SetPixel((col << 3) + xpix, (row << 3) + ypix, b1((row << 4) + col))
                    Next
                Next
            Next
        Next
        PictureBox1.Image = palbmp1

        'Step 3:  Display pixel data.
        'ComboBox1.SelectedIndex = 0 'temp
        'Dim charbmp1 As New Bitmap(256, 256)
        'char1(A)
        Dim num1 = 0
        'Select Case form_integer(mapdat, mappropsaddr1 + 5) And 1
        'Case 0
        For row = 0 To 31  '&H4000 
            For col = 0 To 31 '&H0800 
                For ypix = 0 To 7 '&H0040 
                    For xpix = 0 To 3 '&H0008 '16*8
                        For xypix = 0 To 1 '&H0002
                            charbmp1.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b1((char1(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))
                        Next
                        num1 += 1
                    Next
                Next
            Next
        Next
        '    Case 1
        'For row = 0 To 31
        '    For col = 0 To 31
        '        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
        '        For ypix = 0 To 7
        '            For xpix = 0 To 7 '16*8
        '                charbmp1.SetPixel((col << 3) + xpix, (row << 3) + ypix, b1(char1(num1)))
        '                num1 += 1
        '            Next
        '        Next
        '    Next
        'Next
        'End Select
        disppixbmp()

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
        'PictureBox4.Image = charbmp1

        'Step 4:  Display maps!
    End Sub
    Function form_short(ByVal data() As Byte, ByVal offset As Integer) As Integer
        Return (CInt(data(offset)) Or (CInt(data(offset + 1)) << 8))
    End Function
    Function form_integer(ByVal data() As Byte, ByVal offset As Integer) As UInteger
        Return (CUInt(data(offset)) Or (CUInt(data(offset + 1)) << 8) Or (CUInt(data(offset + 2)) << 16) Or (CUInt(data(offset + 3)) << 24))
    End Function

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


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'charbmp1.
        'Dim charbmp1 As New Bitmap(256, 256)
        ''char1(A)
        'Dim num1 = 0
        'PictureBox4.Image = charbmp1
        disppixbmp()
        'PictureBox4.Image = charbmp2
        'PictureBox4:      .Image = charbmp2
    End Sub
    Sub disppixbmp()
        Select Case ComboBox1.SelectedIndex
            Case 0
                PictureBox4.Image = charbmp1
            Case 1
                'PictureBox4.Image = charbmp2
            Case 2
                'PictureBox4.Image = charbmp3
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mapdataaddr1 = &H36A600 + (ListBox1.SelectedIndex << 5)
        Button1.Text = "Generating..."
        Button1.Refresh()
        Dim lay3bmp As New Bitmap(&H3FF, &H3FF) '&HA18
        FileGet(1, temp, mapdataaddr1 + 16 + 1)
        FileGet(1, temp2) ', mapdataaddr1 + 12 + 1)
        If temp <> temp2 Then
            'Seek(1, &H36A600 + temp + 1)

            '    ' End If
            '    'Dim mapdaddr3 As Integer = form_integer(mapdat, 8)
            '    '    Dim mapdaddr2 As Integer = form_integer(mapdat, 4)
            '    '    Dim mapdaddr1 As Integer = form_integer(mapdat, 0)
            '    '    'Dim temp As UInteger
            '    '    Dim mappropsaddr1 As Integer = form_integer(mapdat, &H18)
            '    '    Dim mapx As Integer = form_short(mapdat, mappropsaddr1)
            '    '    Dim mapy As Integer = form_short(mapdat, mappropsaddr1 + 2)
            '    '    'mapdat(mappropsaddr1 + 5)
            '    '    Dim num1 As Integer = 0
            Dim pix As Byte
            '    '    If charind(2) <> -1 Then
            If CheckBox3.Checked Then
                '        '    Select mapdat(mappropsaddr1 + 5) >> 2 And 1
                '        'Case 0
                '        'mapdaddr3
                Dim tile As Integer
                Dim stile As Short
                Seek(1, &H36A600 + temp + 1)
                For row = 0 To &H1F 'mapy - 1
                    For col = 0 To &H3F 'mapx - 1

                        '= form_short(mapdat, mapdaddr3)
                        'mapdaddr3 += 2
                        FileGet(1, stile)
                        tile = stile And &HFFFF
                        'Seek(1, Loc(1) - 2)
                        For ypix = 0 To 7
                            For xpix = 0 To 3 '16*8
                                For xypix = 0 To 1
                                    '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                    '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                    '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
                                    ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)

                                    pix = char1(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                    (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                    b1(((tile >> 12) << 4) + pix))
                                Next
                            Next
                        Next
                    Next
                Next
            End If
        End If

        'NEXT LAYER
        FileGet(1, temp, mapdataaddr1 + 12 + 1)
        FileGet(1, temp2) ', mapdataaddr1 + 12 + 1)
        If temp <> temp2 Then
            'Seek(1, &H36A600 + temp + 1)

            '    ' End If
            '    'Dim mapdaddr3 As Integer = form_integer(mapdat, 8)
            '    '    Dim mapdaddr2 As Integer = form_integer(mapdat, 4)
            '    '    Dim mapdaddr1 As Integer = form_integer(mapdat, 0)
            '    '    'Dim temp As UInteger
            '    '    Dim mappropsaddr1 As Integer = form_integer(mapdat, &H18)
            '    '    Dim mapx As Integer = form_short(mapdat, mappropsaddr1)
            '    '    Dim mapy As Integer = form_short(mapdat, mappropsaddr1 + 2)
            '    '    'mapdat(mappropsaddr1 + 5)
            '    '    Dim num1 As Integer = 0
            Dim pix As Byte
            '    '    If charind(2) <> -1 Then
            If CheckBox2.Checked Then
                '        '    Select mapdat(mappropsaddr1 + 5) >> 2 And 1
                '        'Case 0
                '        'mapdaddr3
                Dim tile As Integer
                Dim stile As Short
                Seek(1, &H36A600 + temp + 1)
                For row = 0 To &H1F 'mapy - 1
                    For col = 0 To &H3F 'mapx - 1

                        '= form_short(mapdat, mapdaddr3)
                        'mapdaddr3 += 2
                        FileGet(1, stile)
                        tile = stile And &HFFFF
                        'Seek(1, Loc(1) - 2)
                        For ypix = 0 To 7
                            For xpix = 0 To 3 '16*8
                                For xypix = 0 To 1
                                    '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                    '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                    '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
                                    ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)
                                    pix = char1(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    If pix <> 0 Then
                                        lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                        (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                        b1(((tile >> 12) << 4) + pix))
                                    End If
                                Next
                            Next
                        Next
                    Next
                Next
            End If
        End If

        'NEXT LAYER
        FileGet(1, temp, mapdataaddr1 + 8 + 1)
        FileGet(1, temp2) ', mapdataaddr1 + 12 + 1)
        If temp <> temp2 Then
            'Seek(1, &H36A600 + temp + 1)

            '    ' End If
            '    'Dim mapdaddr3 As Integer = form_integer(mapdat, 8)
            '    '    Dim mapdaddr2 As Integer = form_integer(mapdat, 4)
            '    '    Dim mapdaddr1 As Integer = form_integer(mapdat, 0)
            '    '    'Dim temp As UInteger
            '    '    Dim mappropsaddr1 As Integer = form_integer(mapdat, &H18)
            '    '    Dim mapx As Integer = form_short(mapdat, mappropsaddr1)
            '    '    Dim mapy As Integer = form_short(mapdat, mappropsaddr1 + 2)
            '    '    'mapdat(mappropsaddr1 + 5)
            '    '    Dim num1 As Integer = 0
            Dim pix As Byte
            '    '    If charind(2) <> -1 Then
            If CheckBox1.Checked Then
                '        '    Select mapdat(mappropsaddr1 + 5) >> 2 And 1
                '        'Case 0
                '        'mapdaddr3
                Dim tile As Integer
                Dim stile As Short
                Seek(1, &H36A600 + temp + 1)
                For row = 0 To &H1F 'mapy - 1
                    For col = 0 To &H3F 'mapx - 1

                        '= form_short(mapdat, mapdaddr3)
                        'mapdaddr3 += 2
                        FileGet(1, stile)
                        tile = stile And &HFFFF
                        'Seek(1, Loc(1) - 2)
                        For ypix = 0 To 7
                            For xpix = 0 To 3 '16*8
                                For xypix = 0 To 1
                                    '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                    '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                    '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
                                    ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)
                                    pix = char1(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    If pix <> 0 Then
                                        lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                        (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                        b1(((tile >> 12) << 4) + pix))
                                    End If
                                Next
                            Next
                        Next
                    Next
                Next
            End If
        End If
        PictureBox5.Width = &H40 << 3
        PictureBox5.Height = &H20 << 3
        PictureBox5.Image = lay3bmp
        Button1.Text = "Generate Map"
    End Sub
    '                Case 1
    '                    'mapdaddr3
    '                    For row = 0 To mapy - 1
    '                        For col = 0 To mapx - 1
    '                            Dim tile As Integer = form_short(mapdat, mapdaddr3)
    '                            mapdaddr3 += 2
    '                            For ypix = 0 To 7
    '                                For xpix = 0 To 7 '16*8
    '                                    'For xypix = 0 To 1
    '                                    pix = char3(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
    '                                    lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
    '                                                    (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                                    b3(pix))
    '                                    'Next
    '                                Next
    '                            Next
    '                        Next
    '                    Next
    '            End Select
    '        End If
    '    End If
    '    If charind(1) <> -1 Then
    '        If CheckBox2.Checked Then
    '            Select Case mapdat(mappropsaddr1 + 5) >> 1 And 1
    '                Case 0
    '                    'mapdaddr3
    '                    For row = 0 To mapy - 1
    '                        For col = 0 To mapx - 1
    '                            Dim tile As Integer = form_short(mapdat, mapdaddr2)
    '                            mapdaddr2 += 2
    '                            For ypix = 0 To 7
    '                                For xpix = 0 To 3 '16*8
    '                                    For xypix = 0 To 1
    '                                        '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
    '                                        'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
    '                                        '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                        '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
    '                                        ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)
    '                                        pix = char2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
    '                                        If pix <> 0 Then
    '                                            lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
    '                                                            (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                                            b2(((tile >> 12) << 4) + pix))
    '                                        End If
    '                                    Next
    '                                Next
    '                            Next
    '                        Next
    '                    Next
    '                Case 1
    '                    'mapdaddr3
    '                    For row = 0 To mapy - 1
    '                        For col = 0 To mapx - 1
    '                            Dim tile As Integer = form_short(mapdat, mapdaddr2)
    '                            mapdaddr2 += 2
    '                            For ypix = 0 To 7
    '                                For xpix = 0 To 7 '16*8
    '                                    'For xypix = 0 To 1
    '                                    pix = char2(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
    '                                    If pix <> 0 Then
    '                                        lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
    '                                                        (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                                        b2(pix))
    '                                    End If
    '                                    'Next
    '                                Next
    '                            Next
    '                        Next
    '                    Next
    '            End Select
    '        End If
    '    End If
    '    If charind(0) <> -1 Then
    '        If CheckBox1.Checked Then
    '            Select Case mapdat(mappropsaddr1 + 5) And 1
    '                Case 0
    '                    'mapdaddr3
    '                    For row = 0 To mapy - 1
    '                        For col = 0 To mapx - 1
    '                            Dim tile As Integer = form_short(mapdat, mapdaddr1)
    '                            mapdaddr1 += 2
    '                            For ypix = 0 To 7
    '                                For xpix = 0 To 3 '16*8
    '                                    For xypix = 0 To 1
    '                                        '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
    '                                        'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
    '                                        '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                        '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
    '                                        ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)

    '                                        pix = char1(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
    '                                        If pix <> 0 Then
    '                                            lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
    '                                                            (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                                            b1(((tile >> 12) << 4) + pix))

    '                                        End If
    '                                    Next
    '                                Next
    '                            Next
    '                        Next
    '                    Next
    '                Case 1
    '                    'mapdaddr3
    '                    For row = 0 To mapy - 1
    '                        For col = 0 To mapx - 1
    '                            Dim tile As Integer = form_short(mapdat, mapdaddr1)
    '                            mapdaddr1 += 2
    '                            For ypix = 0 To 7
    '                                For xpix = 0 To 7 '16*8
    '                                    'For xypix = 0 To 1
    '                                    pix = char1(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
    '                                    If pix <> 0 Then
    '                                        lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
    '                                                        (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                                        b1(pix))
    '                                    End If
    '                                    'Next
    '                                Next
    '                            Next
    '                        Next
    '                    Next
    '            End Select
    '        End If
    '    End If
    '    PictureBox5.Width = mapx << 3
    '    PictureBox5.Height = mapy << 3
    '    PictureBox5.Image = lay3bmp
    '    Button1.Text = "Generate Map"
    'End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Button1.PerformClick()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Button1.PerformClick()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Button1.PerformClick()
    End Sub

    Private Sub NumericUpDown9_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown9.ValueChanged
        Dim num1 = 0
        'Select Case form_integer(mapdat, mappropsaddr1 + 5) And 1
        'Case 0
        For row = 0 To 31  '&H4000 
            For col = 0 To 31 '&H0800 
                For ypix = 0 To 7 '&H0040 
                    For xpix = 0 To 3 '&H0008 '16*8
                        For xypix = 0 To 1 '&H0002
                            charbmp1.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b1((char1(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))
                        Next
                        num1 += 1
                    Next
                Next
            Next
        Next
        disppixbmp()
    End Sub
End Class
