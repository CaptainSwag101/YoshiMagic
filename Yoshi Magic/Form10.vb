Public Class Form10
    Dim temp As Integer 'Mainly used to get relative pointers.
    Dim sprfaddr As Integer
    Dim sprs As Integer
    Dim pals As Integer
    Dim sprdbaddr As Integer

    Dim b(&HFF) As Color
    Dim palbmp As New Bitmap(128, 128)

    Dim char1(&HFFFF) As Byte
    Dim char1bmp As New Bitmap(256, 256)
    Dim char1bmp2 As New Bitmap(256, 256)

    Dim anidat1(&HFFFF) As Byte
    Dim spr1bmp As New Bitmap(576, 320)
    Dim seqaddr, frmaddr, clpaddr, lyraddr As Integer

    Dim headerstr As String = ""

    Dim tmr1 As Integer = 0
    Dim frm1 As Integer = 0
    Private Sub Form10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NumericUpDown2.Value = Timer1.Interval
        Dim tchr As Byte
        Seek(1, 1)
        For a = 0 To 15
            FileGet(1, tchr)
            headerstr += Chr(tchr)
        Next
        Select Case headerstr
            Case "MARIO&LUIGI2ARME"
                With ComboBox1.Items
                    .Add("BObjMon.dat")
                    .Add("BObjPc.dat")
                    .Add("BObjUI.dat")
                    .Add("FObj.dat")
                    .Add("FObjMon.dat")
                    .Add("FObjPc.dat")
                End With
                ComboBox1.SelectedIndex = 3
            Case "MARIO&LUIGI3CLJE"
                With ComboBox1.Items
                    .Add("BObjMap.dat")
                    .Add("BObjMon.dat")
                    .Add("BObjPc.dat")
                    .Add("BObjUI.dat")
                    .Add("EObjSave.dat")
                    .Add("FObj.dat")
                    .Add("FObjMap.dat")
                    .Add("FObjMon.dat")
                    .Add("FObjPc.dat")
                    .Add("MObj.dat")
                End With
            Case Else
                ComboBox1.Enabled = False
                NumericUpDown1.Enabled = False
                MsgBox(headerstr)
                Exit Sub
        End Select
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case headerstr
            Case "MARIO&LUIGI2ARME"
                Select Case ComboBox1.SelectedIndex
                    Case 0 : sprfaddr = &H55AA00 'BObjMon.dat 
                    Case 1 : sprfaddr = &H624800 'BObjPc.dat 
                    Case 2 : sprfaddr = &H72D000 'BObjUI.dat 
                    Case 3 : sprfaddr = &H287A000 'FObj.dat 
                    Case 4 : sprfaddr = &H2A73400 'FObjMon.dat 
                    Case 5 : sprfaddr = &H2AA9800 'FObjPc.dat
                    Case Else : Exit Sub
                End Select
            Case "MARIO&LUIGI3CLJE"
                MsgBox("Bowser's Inside Story (U) is currently not compatible with this Sprite Viewer.")
                Exit Sub
                Select Case ComboBox1.SelectedIndex
                    Case 0 : sprfaddr = &HA3C200 'BObjMap.dat
                    Case 1 : sprfaddr = &HA68800 'BObjMon.dat
                    Case 2 : sprfaddr = &HC46E00 'BObjPc.dat
                    Case 3 : sprfaddr = &HDB7000 'BObjUI.dat
                    Case 4 : sprfaddr = &HFE4E00 'EObjSave.dat
                    Case 5 : sprfaddr = &H3280200 'FObj.dat
                    Case 6 : sprfaddr = &H34C8000 'FObjMap.dat
                    Case 7 : sprfaddr = &H34C8E00 'FObjMon.dat
                    Case 8 : sprfaddr = &H34FF600 'FObjPc.dat
                    Case 9 : sprfaddr = &H3809200 'MObj.dat
                    Case Else : Exit Sub
                End Select
        End Select
        'Let's "do" the header!
        FileGet(1, temp, sprfaddr + 1)
        FileGet(1, sprs, sprfaddr + temp + 1)
        NumericUpDown1.Maximum = sprs - 1
        FileGet(1, pals)
        Label1.Text = "Sprites:  " & sprs.ToString("X8") & "   Palettes:  " & pals.ToString("X8")
        '(Simplify!)
        sprdbaddr = sprfaddr + temp + 8
        If NumericUpDown1.Value = 0 Then upddata() Else NumericUpDown1.Value = 0
    End Sub

    Sub upddata()
        Dim sprnum As Integer = NumericUpDown1.Value
        'SPRITE INDEXES!
        Dim anipixind As Short
        Dim dpal As Short
        Dim sunk1 As Short 'Sprite Unknowns
        Dim sunk2 As Short
        If ComboBox1.SelectedIndex < 3 Then
            FileGet(1, anipixind, sprdbaddr + (sprnum * &H14) + 1)
        Else
            FileGet(1, anipixind, sprdbaddr + (sprnum << 3) + 1)
        End If
        'FileGet(1, anipixind, sprdbaddr + (sprnum * &H14) + 1)
        FileGet(1, dpal)
        FileGet(1, sunk1)
        FileGet(1, sunk2)
        Label2.Text = "Sprite Data:  " & anipixind.ToString("X4") & " " & dpal.ToString("X4") _
            & " " & sunk1.ToString("X4") & " " & sunk2.ToString("X4")
        'PALETTE INDEXES!
        Dim palind As Short
        Dim punk1 As Short 'Palette Unknowns
        Dim punk2 As Short
        Dim punk3 As Short
        If ComboBox1.SelectedIndex < 3 Then
            FileGet(1, palind, sprdbaddr + (sprs * &H14) + (dpal << 3) + 1)
        Else
            FileGet(1, palind, sprdbaddr + (sprs << 3) + (dpal << 3) + 1)
        End If
        'FileGet(1, palind, sprdbaddr + (sprs * &H14) + (dpal << 3) + 1)
        FileGet(1, punk1)
        FileGet(1, punk2)
        FileGet(1, punk3)
        Label3.Text = "Palette Data:  " & palind.ToString("X4") & " " & punk1.ToString("X4") _
            & " " & punk2.ToString("X4") & " " & punk3.ToString("X4")
        'GET PALETTE DATA!
        FileGet(1, temp, sprfaddr + (palind << 2) + 1)
        Seek(1, sprfaddr + temp + 1)
        Dim palette1 As Short
        For row = 0 To 15
            For col = 0 To 15
                FileGet(1, palette1)
                b((row << 4) + col) = Color.FromArgb((palette1 And &H1F) << 3, (palette1 >> 5 And &H1F) << 3, (palette1 >> 10 And &H1F) << 3)
                For xpix = 0 To 7
                    For ypix = 0 To 7 '16*8
                        palbmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b((row << 4) + col))
                    Next
                Next
            Next
        Next
        PictureBox1.Image = palbmp
        'PIXEL DATA!
        FileGet(1, temp, sprfaddr + ((anipixind + 1) << 2) + 1)
        Seek(1, sprfaddr + temp + 1)
        Array.Clear(char1, 0, &HFFFF)

        'If ComboBox1.SelectedIndex < 3 Then
        '    For a = 0 To &HFFFF
        '        FileGet(1, char1(a))
        '        'char1(a)=
        '    Next
        'Else
        mldsdecomp(char1)
        'End If
        'Unnecessary: Displaying the pixels. (Non-animation data.)
        Dim num1 = 0
        'Select Case form_integer(mapdat, mappropsaddr1 + 5) And 1
        '   Case 0 '16 colors
        For row = 0 To 31  '&H4000 
            For col = 0 To 31 '&H0800 
                For ypix = 0 To 7 '&H0040 
                    For xpix = 0 To 3 '&H0008 '16*8
                        For xypix = 0 To 1 '&H0002
                            char1bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b((char1(num1) >> (xypix << 2) And &HF))) ' + (NumericUpDown9.Value << 4)))
                        Next
                        num1 += 1
                    Next
                Next
            Next
        Next
        '  Case 1 '256 colors
        num1 = 0
        For row = 0 To 31
            For col = 0 To 31
                '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                For ypix = 0 To 7
                    For xpix = 0 To 7 '16*8
                        char1bmp2.SetPixel((col << 3) + xpix, (row << 3) + ypix, b(char1(num1)))
                        num1 += 1
                    Next
                Next
            Next
        Next
        'End Select
        PictureBox2.Image = char1bmp
        PictureBox3.Image = char1bmp2
        'Yay, the bulk of sprite editing! Animation Data!
        FileGet(1, temp, sprfaddr + ((anipixind) << 2) + 1)
        Seek(1, sprfaddr + temp + 1)
        Array.Clear(anidat1, 0, &HFFFF)
        mldsdecomp(anidat1)
        'Dim anidat1(&HFFFF) As Byte
        'Dim spr1bmp As New Bitmap(576, 320)
        'Dim seqaddr, frmaddr, clpaddr, lyraddr As Integer
        seqaddr = &H18
        frmaddr = seqaddr + (form_short(anidat1, &HC) << 3)
        clpaddr = frmaddr + (form_short(anidat1, &HE) << 2)
        lyraddr = clpaddr + (form_short(anidat1, &H10) << 2)
        ListBox1.Items.Clear() 'Clips
        For a = 0 To form_short(anidat1, &H10) - 1
            ListBox1.Items.Add(a)
        Next
        ListBox2.Items.Clear() 'Sequences
        For a = 0 To form_short(anidat1, &HC) - 1
            ListBox2.Items.Add(a)
        Next
        'PictureBox4.Image = spr1bmp
    End Sub

    Function form_short(ByVal data() As Byte, ByVal offset As Integer) As Integer
        Return (CInt(data(offset)) Or (CInt(data(offset + 1)) << 8))
    End Function
    Function form_integer(ByVal data() As Byte, ByVal offset As Integer) As UInteger
        Return (CUInt(data(offset)) Or (CUInt(data(offset + 1)) << 8) Or (CUInt(data(offset + 2)) << 16) Or (CUInt(data(offset + 3)) << 24))
    End Function

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        upddata()
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

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        updclip()
    End Sub
    Sub updclip()
        ListBox3.Items.Clear()
        Dim strtlyr As Integer = form_short(anidat1, clpaddr + (ListBox1.SelectedIndex << 2))
        Dim lastlyr As Integer = form_short(anidat1, clpaddr + (ListBox1.SelectedIndex << 2) + 2)
        Dim curladr As Integer = lyraddr + (lastlyr * &HC)
        spr1bmp = New Bitmap(576, 320)
        If CheckBox1.Checked Then
            For a = 0 To &HDF
                spr1bmp.SetPixel(a + &H90, &H80, b(0))
                spr1bmp.SetPixel(&H100, a + &H10, b(0))
            Next
        End If
        Dim shpsz() As Byte = {8, 8, 16, 16, 32, 32, 64, 64, _
                               16, 8, 32, 8, 32, 16, 64, 32, _
                               8, 16, 8, 32, 16, 32, 32, 64}
        For a = 0 To lastlyr - strtlyr - 1
            curladr -= &HC
            ListBox3.Items.Add(form_integer(anidat1, curladr).ToString("X8") & " " & form_integer(anidat1, curladr + 4).ToString("X8") & " " & form_integer(anidat1, curladr + 8).ToString("X8"))
            Dim lyry As Integer = anidat1(curladr) Xor &H80
            Dim lyrx As Integer = (form_short(anidat1, curladr + 2) And &H1FF) Xor &H100
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
                    For tiley = tyflip1 To tyflip2 Step ystep
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            'For row = 0 To (shpsz(shpsz1 + 1) >> 3) - 1  '&H4000 
                            'For col = 0 To (shpsz(shpsz1) >> 3) - 1 '&H0800 
                            For ypix = 0 To 7 '&H0040 
                                For xpix = 0 To 3 '&H0008 '16*8
                                    For xypix = 0 To 1 '&H0002
                                        'char1bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b((char1(num1) >> (xypix << 2) And &HF))) ' + (NumericUpDown9.Value << 4)))
                                        Dim pix = (char1(num1) >> (xypix << 2) And &HF)
                                        If pix <> 0 Then
                                            ' spr1bmp.SetPixel(lyrx + (col << 3) + (xpix << 1) + xypix, _
                                            '                  lyry + (row << 3) + ypix, b(pix)) ' + (NumericUpDown9.Value << 4)))
                                            spr1bmp.SetPixel(lyrx + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + (xpix << 1) + xypix), _
                                                             lyry + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(pix)) ' + (NumericUpDown9.Value << 4)))
                                            ' Math.Abs((-7 * (t >> 2 And 1)) + (xpix << 1) + xypix)
                                        End If
                                    Next
                                    num1 += 1
                                Next
                            Next
                        Next
                    Next
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
                    For tiley = tyflip1 To tyflip2 Step ystep
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            'For row = 0 To (shpsz(shpsz1 + 1) >> 3) - 1
                            'For col = 0 To (shpsz(shpsz1) >> 3) - 1
                            '        '((row << 4) + col) = Color.FromArgb((palpix3((row << 4) + col) And &H1F) << 3, (palpix3((row << 4) + col) >> 5 And &H1F) << 3, (palpix3((row << 4) + col) >> 10 And &H1F) << 3)
                            For ypix = 0 To 7
                                For xpix = 0 To 7 '16*8
                                    '                spr1bmp.SetPixel((col << 3) + xpix, (row << 3) + ypix, b(char1(num1)))
                                    If char1(num1) <> 0 Then
                                        spr1bmp.SetPixel(lyrx + (tilex << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 4) And 1)) + xpix), _
                                                         lyry + (tiley << 3) + Math.Abs((-7 * ((anidat1(curladr + 3) >> 5) And 1)) + ypix), b(char1(num1))) ' + (NumericUpDown9.Value << 4)))
                                        'spr1bmp.SetPixel(lyrx + (col << 3) + xpix, lyry + (row << 3) + ypix, b(char1(num1)))
                                    End If
                                    num1 += 1
                                Next
                            Next
                        Next
                    Next
            End Select
            'Next
            'curladr += &HC
        Next
        PictureBox4.Image = spr1bmp
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If ListBox1.SelectedIndex = -1 Then ListBox1.SelectedIndex = 0 Else updclip()
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        'Timer1.Enabled = Timer1.Enabled Xor -1
        'If tmr1 = 0 Then Timer1.Start() Else Timer1.Stop() : tmr1 = 0
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox4.Image = spr1bmp

        'ListBox1.SelectedIndex = form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3))
        'If (form_short(anidat1, frmaddr + (ListBox1.SelectedIndex << 2) + 2)) = tmr1 Then
        If (form_short(anidat1, frmaddr + (frm1 << 2) + 2)) <= tmr1 Then
            If frm1 = (form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3) + 2) - 1) Then
                If CheckBox2.Checked = True Then
                    frm1 = form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3))
                Else
                    Timer1.Enabled = False
                    Button1.Text = "Play"
                End If
            Else
                Try
                    'ListBox1.SelectedIndex += 1
                    frm1 += 1
                    'ListBox1.SelectedIndex = form_short(anidat1, frmaddr + (frm1 << 2))
                Catch
                    Timer1.Enabled = False ': tmr1 = 0
                    Button1.Text = "Play"
                End Try
            End If
            ListBox1.SelectedIndex = form_short(anidat1, frmaddr + (frm1 << 2))
            tmr1 = 0 : Exit Sub
        End If

        'For a = 0 To 7
        '    For c = 0 To 7
        '        spr1bmp.SetPixel(tmr1 + a, c, b(0))
        '        '  If MouseButtons = MouseButtons.Left Then Timer1.Enabled = False : Exit Sub
        '    Next
        'Next

        tmr1 += 1
        'If tmr1 >= 400 Then Timer1.Enabled = False : tmr1 = 0
        'PictureBox4.Image = spr1bmp
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Select Case Button1.Text
            Case "Play"
                'Select first clip code.
                If ListBox2.SelectedIndex = -1 Then ListBox2.SelectedIndex = 0
                frm1 = form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3))
                ListBox1.SelectedIndex = form_short(anidat1, frmaddr + (frm1 << 2))
                Timer1.Enabled = True
                Button1.Text = "Stop"
            Case "Stop"
                Timer1.Enabled = False
                tmr1 = 0
                Button1.Text = "Play"
        End Select
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        'ListBox1.SelectedIndex = form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3))
        frm1 = form_short(anidat1, seqaddr + (ListBox2.SelectedIndex << 3))
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        Timer1.Interval = NumericUpDown2.Value
    End Sub
End Class