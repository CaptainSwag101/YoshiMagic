Public Class PITMaps
    'Dim decdata(&H1FFFF) As Byte
    'Dim decsize As Integer
    Dim mapaddr1 As Integer
    Dim charind(4) As Integer
    Dim char1(&HFFFF) As Byte
    Dim char2(&HFFFF) As Byte
    Dim char3(&HFFFF) As Byte
    Dim sprchar1(&HFFFF) As Byte
    Dim mapdat(&H7FFFF) As Byte
    Dim charbmp1 As New Bitmap(256, 256)
    Dim charbmp2 As New Bitmap(256, 256)
    Dim charbmp3 As New Bitmap(256, 256)
    Dim b1(&HFF) As Color
    Dim b2(&HFF) As Color
    Dim b3(&HFF) As Color
    Dim anidat1(&HFFFF) As Byte
    Dim seqaddr, frmaddr, clpaddr, lyraddr As Integer
    Dim db1(1) As Integer
    Private Sub Form9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For a = 0 To &H25A '&H21D
            ListBox1.Items.Add(Hex(a))
        Next
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim temp As Integer
        'Const mapdataaddr1 = &H7C3800 
        Const mapdataaddr1 = &HD42600
        mapaddr1 = &H51CC0 + (ListBox1.SelectedIndex * &H14)
        'Dim temp As Integer
        'Step 1:  Decompress Data!
        FileGet(1, charind(0), mapaddr1 + 1)
        'charind(0) = form_integer(decdata, mapaddr1)
        NumericUpDown1.Value = charind(0)
        If charind(0) <> -1 Then
            FileGet(1, temp, mapdataaddr1 + (charind(0) << 2) + 1)
            Seek(1, mapdataaddr1 + temp + 1)
            Array.Clear(char1, 0, &HFFFF)
            mldsdecomp(char1)
        End If
        FileGet(1, charind(1), mapaddr1 + 4 + 1)
        'charind(1) = form_integer(decdata, mapaddr1 + 4)
        NumericUpDown2.Value = charind(1)
        If charind(1) <> -1 Then
            FileGet(1, temp, mapdataaddr1 + (charind(1) << 2) + 1)
            Seek(1, mapdataaddr1 + temp + 1)
            'Seek(1, &H17FBC00 + form_integer(decdata, &H11314 + (charind(1) << 2)) + 1)
            Array.Clear(char2, 0, &HFFFF)
            mldsdecomp(char2)
        End If
        FileGet(1, charind(2), mapaddr1 + 8 + 1)
        ' charind(2) = form_integer(decdata, mapaddr1 + 8)
        NumericUpDown3.Value = charind(2)
        If charind(2) <> -1 Then
            FileGet(1, temp, mapdataaddr1 + (charind(2) << 2) + 1)
            Seek(1, mapdataaddr1 + temp + 1)
            'Seek(1, &H17FBC00 + form_integer(decdata, &H11314 + (charind(2) << 2)) + 1)
            Array.Clear(char3, 0, &HFFFF)
            mldsdecomp(char3)
        End If
        FileGet(1, charind(3), mapaddr1 + 12 + 1)
        'charind(3) = form_integer(decdata, mapaddr1 + &HC)
        NumericUpDown4.Value = charind(3)
        FileGet(1, temp, mapdataaddr1 + (charind(3) << 2) + 1)
        Seek(1, mapdataaddr1 + temp + 1)
        'Seek(1, &H17FBC00 + form_integer(decdata, &H11314 + (charind(3) << 2)) + 1)
        Array.Clear(mapdat, 0, &HFFFF) 'Maybe.
        mldsdecomp(mapdat)
        'Unknown?
        FileGet(1, charind(4), mapaddr1 + 16 + 1)
        'charind(4) = form_integer(decdata, mapaddr1 + &H10)
        NumericUpDown5.Value = charind(4)
        Dim temp2 As UInteger
        Dim mappropsaddr1 As Integer = form_integer(mapdat, &H18)
        temp2 = form_integer(mapdat, mappropsaddr1)
        NumericUpDown6.Value = temp2
        temp2 = form_integer(mapdat, mappropsaddr1 + 4)
        NumericUpDown7.Value = temp2
        temp2 = form_integer(mapdat, mappropsaddr1 + 8)
        NumericUpDown8.Value = temp2

        'Step 2:  Display Palettes
        Dim palbmp1 As New Bitmap(128, 128)
        Dim paladdr1 As Integer = form_integer(mapdat, &HC)
        Dim palpix1(&H3FF) As Integer
        For a = 0 To &H3FF
            palpix1(a) = form_short(mapdat, paladdr1 + (a << 1))
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

        Dim palbmp2 As New Bitmap(128, 128)
        Dim paladdr2 As Integer = form_integer(mapdat, &H10)
        Dim palpix2(&H3FF) As Integer
        For a = 0 To &H3FF
            palpix2(a) = form_short(mapdat, paladdr2 + (a << 1))
        Next
        'Dim b2(&HFF) As Color
        For row = 0 To 15
            For col = 0 To 15
                b2((row << 4) + col) = Color.FromArgb((palpix2((row << 4) + col) And &H1F) << 3, (palpix2((row << 4) + col) >> 5 And &H1F) << 3, (palpix2((row << 4) + col) >> 10 And &H1F) << 3)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp2.SetPixel((col << 3) + xpix, (row << 3) + ypix, b2((row << 4) + col))
                    Next
                Next
            Next
        Next
        PictureBox2.Image = palbmp2

        Dim palbmp3 As New Bitmap(128, 128)
        Dim paladdr3 As Integer = form_integer(mapdat, &H14)
        Dim palpix3(&H3FF) As Integer
        For a = 0 To &H3FF
            palpix3(a) = form_short(mapdat, paladdr3 + (a << 1))
        Next
        'Dim b3(&HFF) As Color
        For row = 0 To 15
            For col = 0 To 15
                b3((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp3.SetPixel((col << 3) + xpix, (row << 3) + ypix, b3((row << 4) + col))
                    Next
                Next
            Next
        Next
        PictureBox3.Image = palbmp3

        'Step 3:  Display pixel data.
        'ComboBox1.SelectedIndex = 0 'temp
        'Dim charbmp1 As New Bitmap(256, 256)
        'char1(A)
        Dim num1 = 0
        Select Case form_integer(mapdat, mappropsaddr1 + 5) And 1
            Case 0
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
            Case 1
                For row = 0 To 31
                    For col = 0 To 31
                        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                        For ypix = 0 To 7
                            For xpix = 0 To 7 '16*8
                                charbmp1.SetPixel((col << 3) + xpix, (row << 3) + ypix, b1(char1(num1)))
                                num1 += 1
                            Next
                        Next
                    Next
                Next
        End Select
        num1 = 0
        Select Case form_integer(mapdat, mappropsaddr1 + 5) >> 1 And 1
            Case 0
                For row = 0 To 31  '&H4000 
                    For col = 0 To 31 '&H0800 
                        For ypix = 0 To 7 '&H0040 
                            For xpix = 0 To 3 '&H0008 '16*8
                                For xypix = 0 To 1 '&H0002
                                    charbmp2.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b2((char2(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))
                                Next
                                num1 += 1
                            Next
                        Next
                    Next
                Next
            Case 1
                For row = 0 To 31
                    For col = 0 To 31
                        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                        For ypix = 0 To 7
                            For xpix = 0 To 7 '16*8
                                charbmp2.SetPixel((col << 3) + xpix, (row << 3) + ypix, b2(char2(num1)))
                                num1 += 1
                            Next
                        Next
                    Next
                Next
        End Select
        num1 = 0
        Select Case form_integer(mapdat, mappropsaddr1 + 5) >> 2 And 1
            Case 0
                For row = 0 To 31  '&H4000 
                    For col = 0 To 31 '&H0800 
                        For ypix = 0 To 7 '&H0040 
                            For xpix = 0 To 3 '&H0008 '16*8
                                For xypix = 0 To 1 '&H0002
                                    charbmp3.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))
                                Next
                                num1 += 1
                            Next
                        Next
                    Next
                Next
            Case 1
                For row = 0 To 31
                    For col = 0 To 31
                        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                        For ypix = 0 To 7
                            For xpix = 0 To 7 '16*8
                                charbmp3.SetPixel((col << 3) + xpix, (row << 3) + ypix, b3(char3(num1)))
                                num1 += 1
                            Next
                        Next
                    Next
                Next
        End Select
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

        'Step 4:  Display maps! (In Generate Map button.)
        'Step 5: NPC Objects! (In Generate Map button.)
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
                PictureBox4.Image = charbmp2
            Case 2
                PictureBox4.Image = charbmp3
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Text = "Generating Map..."
        Button1.Refresh()
        Dim lay3bmp As New Bitmap(&HA18, &HA18)
        Dim mapdaddr3 As Integer = form_integer(mapdat, 8)
        Dim mapdaddr2 As Integer = form_integer(mapdat, 4)
        Dim mapdaddr1 As Integer = form_integer(mapdat, 0)
        'Dim temp As UInteger
        Dim mappropsaddr1 As Integer = form_integer(mapdat, &H18)
        Dim mapx As Integer = form_short(mapdat, mappropsaddr1)
        Dim mapy As Integer = form_short(mapdat, mappropsaddr1 + 2)
        'mapdat(mappropsaddr1 + 5)
        Dim num1 As Integer = 0
        Dim pix As Byte
        If charind(2) <> -1 Then
            If CheckBox3.Checked Then
                Select Case mapdat(mappropsaddr1 + 5) >> 2 And 1
                    Case 0
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr3)
                                mapdaddr3 += 2
                                For ypix = 0 To 7
                                    For xpix = 0 To 3 '16*8
                                        For xypix = 0 To 1
                                            '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                            'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                            '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                            '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
                                            ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)

                                            pix = char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                            lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                            (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                            b3(((tile >> 12) << 4) + pix))
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Case 1
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr3)
                                mapdaddr3 += 2
                                For ypix = 0 To 7
                                    For xpix = 0 To 7 '16*8
                                        'For xypix = 0 To 1
                                        pix = char3(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
                                        lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
                                                        (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                        b3(pix))
                                        'Next
                                    Next
                                Next
                            Next
                        Next
                End Select
            End If
        End If
        If charind(1) <> -1 Then
            If CheckBox2.Checked Then
                Select Case mapdat(mappropsaddr1 + 5) >> 1 And 1
                    Case 0
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr2)
                                mapdaddr2 += 2
                                For ypix = 0 To 7
                                    For xpix = 0 To 3 '16*8
                                        For xypix = 0 To 1
                                            '' pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                            'lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                            '                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                            '                b3(((tile >> 12) << 4) + (char3(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF)))
                                            ''b3((char3(num1) >> (xypix << 2) And &HF) + (NumericUpDown9.Value << 4)))char3(num1)
                                            pix = char2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                            If pix <> 0 Then
                                                lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                                (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                                b2(((tile >> 12) << 4) + pix))
                                            End If
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Case 1
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr2)
                                mapdaddr2 += 2
                                For ypix = 0 To 7
                                    For xpix = 0 To 7 '16*8
                                        'For xypix = 0 To 1
                                        pix = char2(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
                                        If pix <> 0 Then
                                            lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
                                                            (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                            b2(pix))
                                        End If
                                        'Next
                                    Next
                                Next
                            Next
                        Next
                End Select
            End If
        End If
        If charind(0) <> -1 Then
            If CheckBox1.Checked Then
                Select Case mapdat(mappropsaddr1 + 5) And 1
                    Case 0
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr1)
                                mapdaddr1 += 2
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
                    Case 1
                        'mapdaddr3
                        For row = 0 To mapy - 1
                            For col = 0 To mapx - 1
                                Dim tile As Integer = form_short(mapdat, mapdaddr1)
                                mapdaddr1 += 2
                                For ypix = 0 To 7
                                    For xpix = 0 To 7 '16*8
                                        'For xypix = 0 To 1
                                        pix = char1(((tile And &H3FF) << 6) + (ypix << 3) + xpix)
                                        If pix <> 0 Then
                                            lay3bmp.SetPixel((col << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + xpix), _
                                                            (row << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                            b1(pix))
                                        End If
                                        'Next
                                    Next
                                Next
                            Next
                        Next
                End Select
            End If
        End If
        PictureBox5.Width = mapx << 3
        PictureBox5.Height = mapy << 3
        PictureBox5.Image = lay3bmp
        Button1.Text = "Generating Objects..."
        Button1.Refresh()
        'FIELD EVENTS!
        Dim tempi As Integer '= 0
        Dim temps As Short
        FileGet(1, tempi, &H803800 + (ListBox1.SelectedIndex * &HC) + 1)
        'Dim db1(1) As Integer
        For d = 0 To 1
            db1(d) = &H803800 + tempi
            FileGet(1, temps, db1(d) + &H4 + 1)
            Dim sprgaddr As Integer = db1(d) + temps + 4
            FileGet(1, temps, db1(d) + &H8 + 1)
            Dim palgaddr As Integer = db1(d) + temps + 4
            FileGet(1, temps, db1(d) + &HE + 1)
            Dim objgaddr As Integer = db1(d) + temps
            Dim numofobjs As Integer
            FileGet(1, numofobjs, objgaddr + 1)
            objgaddr += 4
            Dim objx As Integer
            Dim objy As Integer
            Dim objz As Integer
            Dim objxt As Short
            Dim objyt As Short
            Dim objzt As Short
            Dim objxp As Short
            Dim objyp As Short
            Dim objzp As Short
            Dim objdfseq As Byte
            Dim objspr As Byte
            Dim objpal As Byte
            Dim sprfaddr As Integer
            For a = 0 To numofobjs - 1
                FileGet(1, objxt, objgaddr + 1)
                FileGet(1, objyt)
                FileGet(1, objzt)
                FileGet(1, objxp)
                FileGet(1, objyp)
                FileGet(1, objzp)
                objx = (CInt(objxt) << 3) + objxp
                objy = (CInt(objyt) << 3) + objyp
                objz = (CInt(objzt) << 3) + objzp
                FileGet(1, objdfseq, objgaddr + &HC + 1)
                FileGet(1, objspr, objgaddr + &HE + 1)
                objspr = objspr >> 1
                FileGet(1, objpal)
                objpal = objpal And &H7F
                Dim palnum As Integer
                FileGet(1, palnum, palgaddr + (CInt(objpal) << 2) + 1)
                Select Case palnum >> &H18
                    Case 1 : sprfaddr = &H287A000 'FObj.dat 
                    Case 2 : sprfaddr = &H2A73400 'FObjMon.dat 
                    Case 0 : sprfaddr = &H2AA9800 'FObjPc.dat
                    Case Else : MsgBox("?") : Exit Sub
                End Select
                Dim b(&HFF) As Color
                Dim palind As Short
                Dim sprs As Integer
                FileGet(1, tempi, sprfaddr + 1)
                FileGet(1, sprs, sprfaddr + tempi + 1)
                FileGet(1, palind, sprfaddr + tempi + 8 + (sprs << 3) + ((palnum And &HFFFF) << 3) + 1)
                'GET PALETTE DATA!
                FileGet(1, tempi, sprfaddr + (CInt(palind) << 2) + 1)
                Seek(1, sprfaddr + tempi + 1)
                Dim palette1 As Short
                For c = 0 To &HFF
                    FileGet(1, palette1)
                    b(c) = Color.FromArgb((palette1 And &H1F) << 3, (palette1 >> 5 And &H1F) << 3, (palette1 >> 10 And &H1F) << 3)
                Next
                Dim sprnum As Integer
                Dim anipixind As Short
                FileGet(1, sprnum, sprgaddr + (CInt(objspr) << 2) + 1)
                Select Case sprnum >> &H18
                    Case 1 : sprfaddr = &H287A000 'FObj.dat 
                    Case 2 : sprfaddr = &H2A73400 'FObjMon.dat 
                    Case 0 : sprfaddr = &H2AA9800 'FObjPc.dat
                    Case Else : MsgBox("?") : Exit Sub
                End Select
                FileGet(1, tempi, sprfaddr + 1)
                FileGet(1, anipixind, sprfaddr + tempi + 8 + ((sprnum And &HFFFF) << 3) + 1)
                'PIXEL DATA!
                FileGet(1, tempi, sprfaddr + ((CInt(anipixind) + 1) << 2) + 1)
                Seek(1, sprfaddr + tempi + 1)
                Array.Clear(sprchar1, 0, &HFFFF)
                mldsdecomp(sprchar1)
                'Yay, the bulk of sprite editing! Animation Data!
                FileGet(1, tempi, sprfaddr + ((CInt(anipixind)) << 2) + 1)
                Seek(1, sprfaddr + tempi + 1)
                Array.Clear(anidat1, 0, &HFFFF)
                mldsdecomp(anidat1)
                'Dim anidat1(&HFFFF) As Byte
                'Dim spr1bmp As New Bitmap(576, 320)
                'Dim seqaddr, frmaddr, clpaddr, lyraddr As Integer
                seqaddr = &H18
                frmaddr = seqaddr + (form_short(anidat1, &HC) << 3)
                clpaddr = frmaddr + (form_short(anidat1, &HE) << 2)
                lyraddr = clpaddr + (form_short(anidat1, &H10) << 2)
                'ListBox1.Items.Clear() 'Clips
                'For a = 0 To form_short(anidat1, &H10) - 1
                'ListBox1.Items.Add(a)
                'Next
                'ListBox2.Items.Clear() 'Sequences
                'For a = 0 To form_short(anidat1, &HC) - 1
                'ListBox2.Items.Add(a)
                updclip(lay3bmp, b, objx, objy - objz, form_short(anidat1, frmaddr + (form_short(anidat1, seqaddr + (CInt(objdfseq) << 3)) << 2)))



                'objdfseq



                For c = 0 To 15
                    Try
                        lay3bmp.SetPixel(objx + c, objy, b3(0))
                        lay3bmp.SetPixel(objx + c, objy + 16, b3(0))
                        lay3bmp.SetPixel(objx, objy + c, b3(0))
                        lay3bmp.SetPixel(objx + 16, objy + c, b3(0))
                    Catch
                    End Try
                Next
                objgaddr += &H1C
            Next
            FileGet(1, tempi, &H803800 + (ListBox1.SelectedIndex * &HC) + 8 + 1)
            Dim tempi2 As Integer
            FileGet(1, tempi2)
            If tempi = tempi2 Then Exit For
        Next
        'Try
        '    For a = 0 To &H20
        '        For c = 0 To &H20 * 16
        '            lay3bmp.SetPixel(c, (a << 4), b3(0))
        '            lay3bmp.SetPixel((a << 4), c, b3(0))
        '        Next
        '    Next
        'Catch
        'End Try
        'Try
        '    For a = 0 To &H31
        '        For c = 0 To &H190
        '            lay3bmp.SetPixel((a << 3) + c, (a << 3), b3(0))
        '            lay3bmp.SetPixel((a << 3) + c, (a << 3) + 8, b3(0))
        '            lay3bmp.SetPixel((a << 3), (a << 3) + c, b3(0))
        '            lay3bmp.SetPixel((a << 3) + 8, (a << 3) + c, b3(0))
        '        Next
        '    Next
        'Catch
        'End Try
cleanup: Button1.Text = "Generate Map"
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Button1.PerformClick()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Button1.PerformClick()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Button1.PerformClick()
    End Sub
    Sub updclip(ByVal lay3bmp As Bitmap, ByVal b() As Color, ByVal objx As Integer, ByVal objy As Integer, ByVal clip As Short)
        'PictureBox5.Image = lay3bmp
        Dim strtlyr As Integer = form_short(anidat1, clpaddr + (CInt(clip) << 2)) ' + (ListBox1.SelectedIndex << 2))
        Dim lastlyr As Integer = form_short(anidat1, clpaddr + 2 + (CInt(clip) << 2)) ' + (ListBox1.SelectedIndex << 2))
        Dim curladr As Integer = lyraddr + (lastlyr * &HC)
        'spr1bmp = New Bitmap(576, 320)
        'If CheckBox1.Checked Then
        '    For a = 0 To &HDF
        '        'spr1bmp.SetPixel(a + &H90, &H80, b(0))
        '        'spr1bmp.SetPixel(&H100, a + &H10, b(0))
        '        lay3bmp.SetPixel(a + &H90, &H80, b(0))
        '        lay3bmp.SetPixel(&H100, a + &H10, b(0))
        '    Next
        'End If
        Dim shpsz() As Byte = {8, 8, 16, 16, 32, 32, 64, 64, _
                               16, 8, 32, 8, 32, 16, 64, 32, _
                               8, 16, 8, 32, 16, 32, 32, 64}
        For a = 0 To lastlyr - strtlyr - 1
            curladr -= &HC
            Dim lyry As Integer = (CInt(anidat1(curladr)) Xor &H80) - &H80
            Dim lyrx As Integer = ((form_short(anidat1, curladr + 2) And &H1FF) Xor &H100) - &H100
            'For c = 0 To 7
            'spr1bmp.SetPixel(lyrx + c + 1, lyry, b(0))
            'spr1bmp.SetPixel(lyrx, lyry + c + 1, b(2))
            Dim num1 As Integer = CInt(form_short(anidat1, curladr + 4)) << 5
            Dim shpsz1 As Byte = ((anidat1(curladr + 1) >> 3) And &H18) Or ((anidat1(curladr + 3) >> 5) And &H6)
            Select Case (anidat1(curladr + 1) >> 5) And 1
                Case 0 '16 colors
                    Dim txflip1 As Byte
                    Dim txflip2 As Byte
                    Dim xstep As Short
                    If (anidat1(curladr + 3) >> 4) And 1 Then '(t >> 2 And 1) Then
                        txflip1 = (shpsz(shpsz1) >> 3) - 1 '(tx >> 3) - 1
                        txflip2 = 0
                        xstep = -1
                    Else
                        txflip1 = 0
                        txflip2 = (shpsz(shpsz1) >> 3) - 1 '(tx >> 3) - 1
                        xstep = 1
                    End If
                    Dim tyflip1 As Byte
                    Dim tyflip2 As Byte
                    Dim ystep As Short
                    If (anidat1(curladr + 3) >> 5) And 1 Then '(t >> 3 And 1) Then
                        tyflip1 = (shpsz(shpsz1 + 1) >> 3) - 1 '(ty >> 3) - 1
                        tyflip2 = 0
                        ystep = -1
                    Else
                        tyflip1 = 0
                        tyflip2 = (shpsz(shpsz1 + 1) >> 3) - 1 '(ty >> 3) - 1
                        ystep = 1
                    End If
                    'For tiley = 0 To (ty >> 3) - 1
                    'Try
                    For tiley = tyflip1 To tyflip2 Step ystep
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            Try
                                'For row = 0 To (shpsz(shpsz1 + 1) >> 3) - 1  '&H4000 
                                'For col = 0 To (shpsz(shpsz1) >> 3) - 1 '&H0800 
                                For ypix = 0 To 7 '&H0040 
                                    For xpix = 0 To 3 '&H0008 '16*8
                                        For xypix = 0 To 1 '&H0002
                                            'char1bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b((char1(num1) >> (xypix << 2) And &HF))) ' + (NumericUpDown9.Value << 4)))
                                            Dim pix = (sprchar1(num1) >> (xypix << 2) And &HF)
                                            If pix <> 0 Then
                                                ' spr1bmp.SetPixel(lyrx + (col << 3) + (xpix << 1) + xypix, _
                                                '                  lyry + (row << 3) + ypix, b(pix)) ' + (NumericUpDown9.Value << 4)))
                                                'spr1bmp.SetPixel(lyrx + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + (xpix << 1) + xypix), _
                                                '                 lyry + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(pix)) ' + (NumericUpDown9.Value << 4)))
                                                lay3bmp.SetPixel((objx) + lyrx + (tilex << 3) + 8 + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + (xpix << 1) + xypix), _
                                                                 (objy) + lyry + (tiley << 3) + 8 + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(pix)) ' + (NumericUpDown9.Value << 4)))
                                                'lay3bmp.SetPixel((objx << 3) + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + (xpix << 1) + xypix), _
                                                '                 (objy << 3) + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(pix)) ' + (NumericUpDown9.Value << 4)))
                                                '
                                                ' Math.Abs((-7 * (t >> 2 And 1)) + (xpix << 1) + xypix)
                                            End If
                                        Next
                                        num1 += 1
                                    Next
                                Next
                            Catch
                            End Try
                        Next
                    Next
                    'Catch
                    'End Try
                Case 1 '256 colors
                    num1 = num1 << 1
                    'num1 = 0
                    Dim txflip1 As Byte
                    Dim txflip2 As Byte
                    Dim xstep As Short
                    If (anidat1(curladr + 3) >> 4) And 1 Then '(t >> 2 And 1) Then
                        txflip1 = (shpsz(shpsz1) >> 3) - 1 '(tx >> 3) - 1
                        txflip2 = 0
                        xstep = -1
                    Else
                        txflip1 = 0
                        txflip2 = (shpsz(shpsz1) >> 3) - 1 '(tx >> 3) - 1
                        xstep = 1
                    End If
                    Dim tyflip1 As Byte
                    Dim tyflip2 As Byte
                    Dim ystep As Short
                    If (anidat1(curladr + 3) >> 5) And 1 Then '(t >> 3 And 1) Then
                        tyflip1 = (shpsz(shpsz1 + 1) >> 3) - 1 '(ty >> 3) - 1
                        tyflip2 = 0
                        ystep = -1
                    Else
                        tyflip1 = 0
                        tyflip2 = (shpsz(shpsz1 + 1) >> 3) - 1 '(ty >> 3) - 1
                        ystep = 1
                    End If
                    'For tiley = 0 To (ty >> 3) - 1
                    'Try
                    For tiley = tyflip1 To tyflip2 Step ystep
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            Try
                                'For row = 0 To (shpsz(shpsz1 + 1) >> 3) - 1
                                'For col = 0 To (shpsz(shpsz1) >> 3) - 1
                                '        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                                For ypix = 0 To 7
                                    For xpix = 0 To 7 '16*8
                                        '                spr1bmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b(char1(num1)))
                                        If sprchar1(num1) <> 0 Then
                                            'spr1bmp.SetPixel(lyrx + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + xpix), _
                                            '                 lyry + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(char1(num1))) ' + (NumericUpDown9.Value << 4)))
                                            lay3bmp.SetPixel((objx) + lyrx + 8 + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + xpix), _
                                                            (objy) + lyry + 8 + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(sprchar1(num1))) ' + (NumericUpDown9.Value << 4)))
                                            'lay3bmp.SetPixel((objx << 3) + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + xpix), _
                                            '               (objy << 3) + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(sprchar1(num1))) ' + (NumericUpDown9.Value << 4)))
                                            'spr1bmp.SetPixel(lyrx + (col << 3) + xpix, lyry + (row << 3) + ypix, b(char1(num1)))
                                        End If
                                        num1 += 1
                                    Next
                                Next
                            Catch
                            End Try
                        Next
                    Next
                    'Catch
                    'End Try
            End Select
            'Next
            'curladr += &HC
        Next
        'PictureBox4.Image = spr1bmp
        'PictureBox5.Image = lay3bmp
    End Sub

    Private Sub NumericUpDown10_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown10.ValueChanged
        'SCRIPT VIEWER = SCRIPT # CHANGED = LOAD SCRIPT
        scriptview()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        scriptview()
    End Sub
    Sub scriptview()
        ListBox2.Items.Clear()
        Dim temps As Short
        FileGet(1, temps, db1(ComboBox2.SelectedIndex) + &H12 + (NumericUpDown10.Value << 1) + 1)
        Dim scriptcur As Integer = db1(ComboBox2.SelectedIndex) + temps
        Label1.Text = Hex(scriptcur)
        Dim cmd As Short
        For c = 0 To 199
            FileGet(1, cmd, scriptcur + 1) : scriptcur += 2
            Dim lbstring As String = cmd.ToString("X4")
            'If cmd = 0 Then lbstring = lbstring & " (End)" : ListBox2.Items.Add(lbstring) : Exit For
            'If cmd = 1 Then lbstring = lbstring & " (Return)" : ListBox2.Items.Add(lbstring) : Exit For
            Dim sattr As Byte
            FileGet(1, sattr, &HB3534 + (cmd << 2) + 1)
            If sattr And &H20 Then 'Write-to flag.
                FileGet(1, cmd, scriptcur + 1) : scriptcur += 2
                lbstring = lbstring & " " & cmd.ToString("X4")
            Else
                lbstring = lbstring & " " & "----"
            End If
            If sattr And &H40 Then 'Flags flag.
                FileGet(1, cmd, scriptcur + 1) : scriptcur += 2
                lbstring = lbstring & " " & cmd.ToString("X4")
            Else
                lbstring = lbstring & " " & "----"
            End If
            If sattr And &H1F <> 0 Then
                For args = 0 To (sattr And &H1F) - 1
                    FileGet(1, cmd, scriptcur + 1) : scriptcur += 2
                    lbstring = lbstring & " " & cmd.ToString("X4")
                Next
            End If
            ListBox2.Items.Add(lbstring)
        Next
    End Sub
End Class
