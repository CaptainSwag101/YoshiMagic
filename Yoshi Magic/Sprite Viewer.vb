Public Class Form5
    Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8) '(*8,)
    Dim palbmp As New Bitmap(15 * 16, 15 * 2) 'Palette Bitmap
    Dim pal2

    Dim pointer As Integer 'Pointers...
    Dim main As Integer 'Main Data
    Dim animain As Integer

    Dim data2(&H5FFF)
    Dim num = 0

    Dim sprshtsize As Short
    Dim clpaddr As Integer
    Dim palette1(31) As Short
    Dim spr(1) As Byte
    Dim b As Color
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'Player_Graphics.Show()
    'End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
    'MsgBox(a.GetPixel(MousePosition.X, MousePosition.Y).ToString)
    'End Sub

    Private Sub Graphics_Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        'PictureBox1.
        'NumericUpDown1.Value = &H2000
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary, OpenAccess.Default, OpenShare.Shared)


        'TableLayoutPanel1.BackColor = Color.DarkGray
        'TableLayoutPanel1.CellBorderStyle
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        'On Error GoTo errorhandler
        'Main Data Get
        'MsgBox(Hex(&H39EE60 + (NumericUpDown1.Value >> 12) * 4 - 4 + 1))
        FileGet(1, pointer, &H39EE60 + (NumericUpDown1.Value >> 12) * 4 - 4 + 1)
        FileGet(1, main, pointer - &H8000000 + (NumericUpDown1.Value And &HFFF) * 4 + 1)
        'Console.Write(Hex(pointer) & " -> " & Hex(main))
        '>>1F ' 80000000
        '>>12 and 1FF ' 07FC0000
        '>>9 AND 1FF ' 0003FE00
        'AND 1FF ' 000001FF
        ''FileClose(1)
        NumericUpDown2.Value = main And &H1FF
        NumericUpDown3.Value = (main >> 9) And &H1FF
        NumericUpDown4.Value = (main >> &H12) And &H1FF
        NumericUpDown5.Value = (main >> &H1B) And &H1F 'And &H1FF
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        ''Locate Animations Data
        'FileGet(1, pointer, &H39EE8C + (NumericUpDown1.Value >> 12) * 4 - 4 + 1)
        'FileGet(1, pointer, pointer - &H8000000 + (main >> &H12 And &H1FF) * 4 + 1)
        'Dim sequences As Byte
        'FileGet(1, sequences, pointer - &H8000000 + 7 + 1)
        'ListBox2.Items.Clear()
        'For seqs = 0 To sequences - 1
        '    ListBox2.Items.Add("Sequence " & seqs)
        'Next
        'Get Spritesheet Size
        'Dim sprshtsize As Short
        'MsgBox("")
        FileGet(1, pointer, &H39EE8C + (CInt(NumericUpDown1.Value) >> 12) * 4 - 4 + 1)
        'FileGet(1, animain, pointer - &H8000000 + (main >> &H12 And &H1FF) * 4 + 1)
        FileGet(1, animain, pointer - &H8000000 + CInt(NumericUpDown4.Value) * 4 + 1)
        FileGet(1, sprshtsize, animain - &H8000000 + 4 + 1)
        sprshtsize = sprshtsize And &H1FF

        'Locate Palette
        FileGet(1, pointer, &H39EEE4 + (CInt(NumericUpDown1.Value) >> 12) * 4 - 4 + 1)
        FileGet(1, pointer, pointer - &H8000000 + (main And &H1FF) * 4 + 1)
        '0839EEE4
        'Dim palette1(255) As Short
        'Dim palette1(31) As Short
        'Dim spr(1) As Byte
        'Dim b As Color ' = Color.FromArgb(0, 0, 0) 'Red
        For row = 0 To 1 '15
            For col = 0 To 15
                'Try
                'FileGet(1, palette1(row * 8 + col), &H4F4CCC + ((row * 8 + col) * 2 + 1))
                'FileGet(1, palette1(row * 16 + col), &H4F4CCC + ((row * 16 + col) * 2 + 1))
                FileGet(1, palette1(row * 16 + col), pointer - &H8000000 + ((row * 16 + col) * 2 + 1))
                'Catch
                'End Try
                'MsgBox(Hex(&H4F4CCC + ((row * 8 + col) * 2)))
                'Try
                b = Color.FromArgb((palette1(row * 16 + col) And &H1F) * 8, (palette1(row * 16 + col) >> 5 And &H1F) * 8, (palette1(row * 16 + col) >> 10 And &H1F) * 8) 'row * 32, col * 16, row + col)
                'MsgBox("1")
                For xpix = 0 To 14
                    For ypix = 0 To 14
                        palbmp.SetPixel(col * 15 + xpix, row * 15 + ypix, b)
                    Next
                Next
                'MsgBox("2")
                'Catch
                'End Try
            Next
        Next
        Panel1.Refresh()

        'Locate Sprites
        Dim pointer2 As Integer
        FileGet(1, pointer, &H39EEB8 + (CInt(NumericUpDown1.Value) >> 12) * 4 - 4 + 1)
        FileGet(1, pointer2, pointer - &H8000000 + (main >> 9 And &H1FF) * 4 + 1)
        'MsgBox(Hex(pointer) & " -  " & Hex((pointer - &H8000000) + (((main >> 9) And &H1FF) * 4)) & " Original: " & Hex(pointer - &H8000000 + (main >> 9 And &H1FF) * 4))
        pointer = pointer + pointer2

        'Which pixel clicked? [check]
        'Two bitmaps in picbox? [use same bitmap, and separate dims.]
        Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8)
        Dim maxcols = 29 '16 'Does not include the 0
        Seek(1, pointer - &H8000000 + 1)
        num = 0
        If (NumericUpDown5.Value >> 4) And 1 = 1 Then
            For row = 0 To 15 '1 '15
                For col = 0 To maxcols - 1 '1 '15 
                    For ypix = 0 To 7
                        For xpix = 0 To 3 '15
                            'MsgBox("1")
                            'Color.Transparent
                            'FileGet(1, spr(0), &H987680 + (row * &H20 * maxcols) + (col * &H20) + (ypix * 4) + xpix + 1) 
                            FileGet(1, spr(0)) ', pointer - &H8000000 + (row * &H20 * maxcols) + (col * &H20) + (ypix * 4) + xpix + 1)
                            data2(num) = spr(0)
                            num += 1
                            'MsgBox("2")
                            spr(1) = spr(0) And &HF
                            'MsgBox(spr(1))
                            b = Color.FromArgb((palette1(spr(1) + (&H10 * pal2)) And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 10 And &H1F) * 8)
                            'MsgBox("4")
                            a.SetPixel(col * 8 + (xpix * 2), row * 8 + ypix, b)
                            spr(1) = spr(0) >> 4
                            b = Color.FromArgb((palette1(spr(1) + (&H10 * pal2)) And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 10 And &H1F) * 8)
                            a.SetPixel(col * 8 + (xpix * 2) + 1, row * 8 + ypix, b)
                            'MsgBox(spr(0) & " - " & _
                            '       Hex(&H987690 + (row * 32 * 2) + (col * 32) + (ypix * 4) + xpix) & _
                            '       " - X:" & col * 8 + (xpix * 2) & _
                            '       " - Y:" & row * 8 + ypix)
                        Next
                    Next
                    If (row * 29) + (col + 1) = sprshtsize + 1 Then GoTo a1 '275 'TEMP for 2000 'PictureBox1.Refresh() : MsgBox("!")
                    'b = Color.FromArgb(0, 0, 0)
                Next
            Next
        Else
            'Compressed
            Array.Clear(data2, 0, &H3FFF)
            decomp()
            'disimg(a, 0)
            Dim pix As Byte
            'For ab = 0 To &H3FFF
            '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
            '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)

            '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
            'Next
            For row = 0 To 15  '&H4000 
                For col = 0 To maxcols - 1 '31 '&H0800 
                    For ypix = 0 To 7 '&H0040 
                        For xpix = 0 To 3 '&H0008 '16*8
                            For xypix = 0 To 1 '&H0002
                                'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                pix = data2((row * &H20 * maxcols) + (col << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                                a.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                            Next
                        Next
                    Next
                Next
            Next
        End If
a1:
        PictureBox1.Image = a
        'PictureBox2.Image = palbmp
        Panel1.BackgroundImage = palbmp
        PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage



        'Graphics.FromImage(

        ''FileClose(1)
        NumericUpDown1.BackColor = Color.White
        Exit Sub
errorhandler:
        ''FileClose(1)
        NumericUpDown1.BackColor = Color.Red
    End Sub

    Private Sub Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Click
        Dim test As New Point(14, 14)
        'PictureBox2.RectangleToScreen(test)
        'PictureBox2.PointToScreen(test)
        Panel1.PointToScreen(test)
        'MsgBox(PictureBox2.PointToScreen(test).ToString)
        'MsgBox(test.ToString)
        If Panel1.PointToScreen(test).Y.ToString < MousePosition.Y.ToString Then
            ''PictureBox2.PointToScreen(test).ToString Then
            ' MsgBox("GREATER")
            pal2 = 1
            NumericUpDown1_ValueChanged(sender, e)
            'NumericUpDown1_ValueChanged()
            'PictureBox2.D()
            'RectangleShape1.Top = 15
        Else
            ' MsgBox("LESSER")
            pal2 = 0
            NumericUpDown1_ValueChanged(sender, e)
            'NumericUpDown1_ValueChanged()
            'RectangleShape1.Top = 0
        End If
        'If test > PointToScreen(Point(3, 3)) Then
        '    MsgBox("22")
        'End If
    End Sub

    'Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    'a.Save("C:\Users\charleysdrpepper\Desktop\test1.bmp")
    'MsgBox("Picture Saved!")
    'End Sub

    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        ''Dim aa1 As Short
        ''Dim aa2 As Short
        ''Locate Animations Data
        FileGet(1, pointer, &H39EE8C + (CInt(NumericUpDown1.Value) >> 12) * 4 - 4 + 1)
        ''FileGet(1, animain, pointer - &H8000000 + (main >> &H12 And &H1FF) * 4 + 1)
        FileGet(1, animain, pointer - &H8000000 + CInt(NumericUpDown4.Value) * 4 + 1)
        'Sequences Listbox
        Dim sequences As Byte
        FileGet(1, sequences, animain - &H8000000 + 7 + 1)
        ListBox2.Items.Clear()
        For seqs = 0 To sequences - 1
            ListBox2.Items.Add("Sequence " & seqs)
        Next
        'Clips Listbox
        Dim clppointer As Short
        Dim clips As Byte
        FileGet(1, clppointer, animain - &H8000000 + 1)
        clpaddr = animain - &H8000000 - clppointer
        Label7.Text = Hex(clpaddr) 'DEBUG
        FileGet(1, clips, clpaddr + 1) 'animain - &H8000000 - clppointer + 1)
        ListBox1.Items.Clear()
        If clppointer = 0 Then
            ListBox1.BackColor = Color.Red
            'Exit Sub
        Else
            ListBox1.BackColor = Color.White
            For clps = 0 To clips - 1
                ListBox1.Items.Add("Clip " & clps)
            Next
        End If
        pointer = 0
        ''FileClose(1)
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        Dim seqpointer As Short
        Dim frames As Byte
        FileGet(1, seqpointer, animain - &H8000000 + 8 + (ListBox2.SelectedIndex * 2) + 1)
        FileGet(1, frames, animain - &H8000000 + seqpointer + 1)
        ListBox3.Items.Clear()
        For frms = 0 To frames - 1
            ListBox3.Items.Add("Frame " & frms)
        Next
        ''FileClose(1)
    End Sub




    'Room Properties stuff
    'Sub cimage(ByVal nud As NumericUpDown, ByVal bm As Bitmap, ByVal pb As PictureBox, ByVal n As Byte) ', ByVal decompress As Boolean)
    '    'UPDATE A COMPRESSED IMAGE (Decompress if needed > Display Image)
    '    'bm = New Bitmap(16 * 8 * 2, 16 * 4) '(20, 23)

    '    Dim numud2val As Byte = nud.Value 'NumericUpDown2.Value
    '    If numud2val <> &HFF Then
    '        'If decompress = True Then
    '        'Dim offset1 As Integer
    '        FileGet(1, offset1, &H6527F4 + (CInt(numud2val) << 2) + 1) 'Maps
    '        'FileGet(1, offset1, &HA57994 + (CInt(numud2val) << 2) + 1) 'Suitcase
    '        'FileGet(1, offset1, &H9F808C + (CInt(numud2val) << 2) + 1) 'Battle Menus
    '        'FileGet(1, offset1, &H9FC058 + (CInt(numud2val) << 2) + 1) 'Battle BGs
    '        'offset1 = &H6527F4 + offset1
    '        Try
    '            Seek(1, &H6527F4 + offset1 + 1)
    '            'Seek(1, &HA57994 + offset1 + 1)
    '            'Seek(1, &H9F808C + offset1 + 1)
    '            'Seek(1, &H9FC058 + offset1 + 1)
    '            'Array.Clear(data2, 0, &H6000)
    '            num = &H2000 * n 'Offset to put data in variable data2 for sub decomp.
    '            decomp()
    '            nud.BackColor = Color.White
    '        Catch
    '            nud.BackColor = Color.Red
    '        End Try
    '        'End If
    '        disimg(bm, n)
    '        pb.Image = bm
    '    Else
    '        pb.Image = Nothing
    '    End If
    'End Sub
    Sub decomp() 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1 As Byte 'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
        FileGet(1, data1) ', Loc(1) + 1)  'FileGet(1, data1, offset1 + 1) 'offset1 += (data1 >> 6) + 1
        Seek(1, Loc(1) + (data1 >> 6) + 1) '+1  'Seek(1, offset1 + (data1 >> 6) + 1 + 1)
        For aa = 0 To &HFFF
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

    'Sub disimg(ByVal bmp As Bitmap, ByVal numb As Integer) 'DISPLAYING COMPRESSED IMAGES (PIXELS)
    '    Dim pix As Byte
    '    'Timer1.Start()
    '    'For row = 0 To 7  '&H4000 
    '    '    For col = 0 To 31 '&H0800 
    '    '        For ypix = 0 To 7 '&H0040 
    '    '            For xpix = 0 To 3 '&H0008 '16*8
    '    '                For xypix = 0 To 1 '&H0002
    '    '                    'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
    '    '                    'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
    '    '                    pix = data2((numb << 13) + (row << 10) + (col << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
    '    '                    bmp.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b(pix + (NumericUpDown5.Value << 4)))
    '    '                Next
    '    '            Next
    '    '        Next
    '    '    Next
    '    'Next
    '    'Timer1.Stop()
    '    'MsgBox(ms)
    '    'Timer1.Start()
    '    For ab = 0 To &H3FFF
    '        pix = data2((numb << 13) + (ab >> 1)) >> ((ab And 1) << 2) And &HF
    '        'b = Color.FromArgb((palette1(spr(1) + (&H10 * pal2)) And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 10 And &H1F) * 8)

    '        bmp.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b(pix)) ' + (NumericUpDown5.Value << 4)))
    '    Next
    '    'Timer1.Stop()
    '    'MsgBox(ms)
    'End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        Dim clppoint As Short
        FileGet(1, clppoint, clpaddr + (ListBox1.SelectedIndex << 1) + 1 + 1)
        Dim layers As Byte
        FileGet(1, layers, clpaddr + clppoint + 1)
        'Label9.Text = "Layers: " & Hex(layers)
        'MsgBox("Clip List Address: 08" & Hex(clpaddr) & ":" & Hex(clppoint) & " Clip Address: " & Hex(clpaddr + clppoint) & " Layers: " & Hex(layers))
        Dim clipbmp As New Bitmap(PictureBox2.Width, PictureBox2.Height)
        PictureBox2.Image = clipbmp
        'For lay1 = 0 To (layers And &H7F) - 1
                Dim slays As Byte = 0
        If (layers >> 7) Then
            slays = layers
            FileGet(1, layers, clpaddr + clppoint + (((slays >> 7) * (slays And &H7F) * 6) + (slays >> 7)) + 1)
        End If
        Dim cliptype As Byte
        FileGet(1, cliptype)
        Label8.Text = "Clip Addr: " & Hex(clpaddr + clppoint) & " ;  Layers: " & Hex(slays) & "/" & Hex(layers) & " ;  Clip Type: " & Hex(cliptype)
        Select Case cliptype
            Case 1
                For lay = (layers And &H7F) - 1 To 0 Step -1
                    'Dim lay As Integer = 
                    Dim x As Byte
                    Dim y As Byte
                    ' FileGet(1, x, clpaddr + clppoint + 2 + (lay * 3) + ((layers >> 7) * 7) + 1)
                    FileGet(1, x, clpaddr + clppoint + (((slays >> 7) * (slays And &H7F) * 6) + (slays >> 7)) + 2 + (lay * 3) + ((layers >> 7) * 7) + 1)
                    'FileGet(1, x, clpaddr + clppoint + 2 + (lay << 2) + ((layers >> 7) * 7) + 1)
                    FileGet(1, y)
                    Dim xs As SByte = (x And &H7F) - (x And &H80)
                    Dim ys As SByte = (y And &H7F) - (y And &H80)
                    Dim q As Byte
                    FileGet(1, q)
                    Dim t As Byte
                    FileGet(1, t)
                    '0839EE04
                    '(x>>4)or(y>>6)
                    Dim tx As Byte
                    FileGet(1, tx, &H39EE04 + ((0) << 1) + 1)
                    Dim ty As Byte
                    FileGet(1, ty)
                    Dim tileoffsetpntr As Short
                    Dim tileoffsetaddr As Integer
                    FileGet(1, tileoffsetpntr, animain - &H8000000 + 2 + 1)
                    tileoffsetaddr = animain - &H8000000 - tileoffsetpntr
                    Dim maxlayers As Byte
                    FileGet(1, maxlayers, animain - &H8000000 + 6 + 1)
                    maxlayers = maxlayers And &H7F
                    Dim tileoffsetnum As Byte
                    FileGet(1, tileoffsetnum, tileoffsetaddr + 1 + (ListBox1.SelectedIndex * (maxlayers + 1)) + lay + 1)
                    'For tiley = 0 To (ty >> 3) - 1
                    '    For tilex = 0 To (tx >> 3) - 1
                    '        For fory = 0 To 7
                    '            For forx = 0 To 7
                    '                'clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + forx, (PictureBox2.Height >> 1) + ys + fory, Color.Red)
                    '                'Dim pix As Color
                    '                'For xypix = 0 To 1
                    '                'pix = data2((tileoffsetnum << 5) + (fory << 2) + forx) >> (xypix << 2) And &HF
                    '                'data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                    '                'b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                'a.Clone(New Rectangle(tileoffsetnum, tileoffsetnum, tileoffsetnum + 8, tileoffsetnum + 8), Imaging.PixelFormat.Format16bppRgb555)
                    '                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + forx, (PictureBox2.Height >> 1) + ys + (tiley << 3) + fory, Color.Red)
                    '                'Next
                    '            Next
                    '        Next
                    '    Next
                    'Next
                    Dim pix As Byte
                    'For ab = 0 To &H3FFF
                    '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
                    '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
                    'Next
                    Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8)
                    'Dim maxcols = 29 '16 'Does not include the 0
                    'For row = 0 To 15  '&H4000 
                    'For col = 0 To maxcols - 1 '31 '&H0800 
                    Dim pixbyte As Integer = CInt(tileoffsetnum) << 5
                    For tiley = 0 To (ty >> 3) - 1
                        For tilex = 0 To (tx >> 3) - 1
                            For ypix = 0 To 7 '&H0040 
                                For xpix = 0 To 3 '&H0008 '16*8
                                    For xypix = 0 To 1 '&H0002
                                        'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                        'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                        'pix = data2((CInt(tileoffsetnum) << 5) + (tiley * (tx >> 3)) + (tilex << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                        pix = data2(pixbyte) >> (xypix << 2) And &HF
                                        If pix <> 0 Then
                                            b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                                            Try
                                                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + (xpix << 1) + xypix, (PictureBox2.Height >> 1) + ys + (tiley << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                                            Catch ex As Exception
                                                PictureBox2.Refresh()
                                                MsgBox("ERROR")
                                                GoTo errorexit1
                                            End Try
                                        End If
                                    Next
                                    pixbyte += 1
                                Next
                            Next
                        Next
                    Next

errorexit1:
                Next
            Case 3
                Dim grphsecs As Byte
                FileGet(1, grphsecs, clpaddr + clppoint + 2 + ((layers And &H7F) << 2) + 1)
                Dim secoffsets(grphsecs - 1) As Short
                Dim tileammnts(grphsecs - 1) As Byte
                For sections = 0 To (grphsecs - 1)
                    FileGet(1, secoffsets(sections))
                    FileGet(1, tileammnts(sections))
                Next
                For lay = (layers And &H7F) - 1 To 0 Step -1
                    'Dim lay As Integer = 
                    Dim x As Byte
                    Dim y As Byte
                    FileGet(1, x, clpaddr + clppoint + 2 + (lay << 2) + ((layers >> 7) * 7) + 1)
                    FileGet(1, y)
                    Dim xs As SByte = (x And &H7F) - (x And &H80)
                    Dim ys As SByte = (y And &H7F) - (y And &H80)
                    Dim q As Byte
                    FileGet(1, q)
                    Dim t As Byte
                    FileGet(1, t)
                    '0839EE04
                    Dim tx As Byte
                    FileGet(1, tx, &H39EE04 + ((t >> 4) << 1) + 1)
                    Dim ty As Byte
                    FileGet(1, ty)
                    Dim tileoffsetpntr As Short
                    Dim tileoffsetaddr As Integer
                    FileGet(1, tileoffsetpntr, animain - &H8000000 + 2 + 1)
                    tileoffsetaddr = animain - &H8000000 - tileoffsetpntr
                    Dim maxlayers As Byte
                    FileGet(1, maxlayers, animain - &H8000000 + 6 + 1)
                    maxlayers = maxlayers And &H7F
                    Dim tileoffsetnum As Byte
                    FileGet(1, tileoffsetnum, tileoffsetaddr + 1 + (ListBox1.SelectedIndex * (maxlayers + 1)) + lay + 1)
                    Dim tileoffsetnum2 As Short
                    For sections = 0 To (grphsecs - 1)
                        If tileoffsetnum < tileammnts(sections) Then
                            tileoffsetnum2 = secoffsets(sections) + tileoffsetnum
                            Exit For
                        End If
                        tileoffsetnum -= tileammnts(sections)
                    Next

                    'For tiley = 0 To (ty >> 3) - 1
                    '    For tilex = 0 To (tx >> 3) - 1
                    '        For fory = 0 To 7
                    '            For forx = 0 To 7
                    '                'clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + forx, (PictureBox2.Height >> 1) + ys + fory, Color.Red)
                    '                'Dim pix As Color
                    '                'For xypix = 0 To 1
                    '                'pix = data2((tileoffsetnum << 5) + (fory << 2) + forx) >> (xypix << 2) And &HF
                    '                'data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                    '                'b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                'a.Clone(New Rectangle(tileoffsetnum, tileoffsetnum, tileoffsetnum + 8, tileoffsetnum + 8), Imaging.PixelFormat.Format16bppRgb555)
                    '                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + forx, (PictureBox2.Height >> 1) + ys + (tiley << 3) + fory, Color.Red)
                    '                'Next
                    '            Next
                    '        Next
                    '    Next
                    'Next
                    Dim pix As Byte
                    'For ab = 0 To &H3FFF
                    '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
                    '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
                    'Next
                    Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8)
                    'Dim maxcols = 29 '16 'Does not include the 0
                    'For row = 0 To 15  '&H4000 
                    'For col = 0 To maxcols - 1 '31 '&H0800 
                    Dim pixbyte As Integer = CInt(tileoffsetnum2) << 5
                    Dim txflip1 As Byte
                    Dim txflip2 As Byte
                    Dim xstep As Short
                    If (t >> 2 And 1) Then
                        txflip1 = (tx >> 3) - 1
                        txflip2 = 0
                        xstep = -1
                    Else
                        txflip1 = 0
                        txflip2 = (tx >> 3) - 1
                        xstep = 1
                    End If
                    Dim tyflip1 As Byte
                    Dim tyflip2 As Byte
                    Dim ystep As Short
                    If (t >> 3 And 1) Then
                        tyflip1 = (ty >> 3) - 1
                        tyflip2 = 0
                        ystep = -1
                    Else
                        tyflip1 = 0
                        tyflip2 = (ty >> 3) - 1
                        ystep = 1
                    End If
                    'For tiley = 0 To (ty >> 3) - 1
                    For tiley = tyflip1 To tyflip2 Step ystep
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            For ypix = 0 To 7 '&H0040 
                                For xpix = 0 To 3 '&H0008 '16*8
                                    For xypix = 0 To 1 '&H0002
                                        '04 = Horr. Flip
                                        '08 = Vertical Flip
                                        'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                        'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                        'pix = data2((CInt(tileoffsetnum) << 5) + (tiley * (tx >> 3)) + (tilex << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                        pix = data2(pixbyte) >> (xypix << 2) And &HF
                                        If pix <> 0 Then
                                            b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                                            Try
                                                'clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + (xpix << 1) + xypix, (PictureBox2.Height >> 1) + ys + (tiley << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                                                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + Math.Abs((-7 * (t >> 2 And 1)) + (xpix << 1) + xypix), (PictureBox2.Height >> 1) + ys + (tiley << 3) + Math.Abs((-7 * (t >> 3 And 1)) + ypix), b) '(pix + (NumericUpDown5.Value << 4)))
                                            Catch ex As Exception
                                                PictureBox2.Refresh()
                                                MsgBox("ERROR")
                                                GoTo errorexit
                                            End Try
                                        End If
                                    Next
                                    pixbyte += 1
                                Next
                            Next
                        Next
                    Next

errorexit:
                    'PictureBox2.Refresh()
                    'MsgBox(Hex(tileoffsetaddr) & " Max Layers:" & maxlayers & " -  " & Hex(tileoffsetaddr + 1 + (ListBox1.SelectedIndex * (maxlayers + 1)) + lay) & ":" & Hex(tileoffsetnum) _
                    ' & " // ")

                    'Next
                    'Next
                    ''//////////
                    '    Dim maxcols = 29 '16 'Does not include the 0
                    '    If (NumericUpDown5.Value >> 4) And 1 = 1 Then
                    '        For row = 0 To 15 '1 '15
                    '            For col = 0 To maxcols - 1 '1 '15 
                    '                For ypix = 0 To 7
                    '                    For xpix = 0 To 3 '15
                    '                        'MsgBox("1")
                    '                        'Color.Transparent
                    '                        'FileGet(1, spr(0), &H987680 + (row * &H20 * maxcols) + (col * &H20) + (ypix * 4) + xpix + 1) 
                    '                        'FileGet(1, spr(0), pointer - &H8000000 + (row * &H20 * maxcols) + (col * &H20) + (ypix * 4) + xpix + 1)
                    '                        spr(0) = data2(0)
                    '                        'MsgBox("2")
                    '                        spr(1) = spr(0) And &HF
                    '                        'MsgBox(spr(1))
                    '                        b = Color.FromArgb((palette1(spr(1) + (&H10 * pal2)) And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                        'MsgBox("4")
                    '                        a.SetPixel(col * 8 + (xpix * 2), row * 8 + ypix, b)
                    '                        spr(1) = spr(0) >> 4
                    '                        b = Color.FromArgb((palette1(spr(1) + (&H10 * pal2)) And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(spr(1) + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                        a.SetPixel(col * 8 + (xpix * 2) + 1, row * 8 + ypix, b)
                    '                        'MsgBox(spr(0) & " - " & _
                    '                        '       Hex(&H987690 + (row * 32 * 2) + (col * 32) + (ypix * 4) + xpix) & _
                    '                        '       " - X:" & col * 8 + (xpix * 2) & _
                    '                        '       " - Y:" & row * 8 + ypix)
                    '                    Next
                    '                Next
                    '                If (row * 29) + (col + 1) = sprshtsize + 1 Then GoTo a1 '275 'TEMP for 2000 'PictureBox1.Refresh() : MsgBox("!")
                    '                'b = Color.FromArgb(0, 0, 0)
                    '            Next
                    '        Next
                    '    Else
                    '        'Compressed
                    '        Seek(1, pointer - &H8000000 + 1)
                    '        num = 0
                    '        Array.Clear(data2, 0, &H3FFF)
                    '        decomp()
                    '        'disimg(a, 0)
                    '        Dim pix As Byte
                    '        'For ab = 0 To &H3FFF
                    '        '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
                    '        '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)

                    '        '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
                    '        'Next
                    '        For row = 0 To 15  '&H4000 
                    '            For col = 0 To maxcols - 1 '31 '&H0800 
                    '                For ypix = 0 To 7 '&H0040 
                    '                    For xpix = 0 To 3 '&H0008 '16*8
                    '                        For xypix = 0 To 1 '&H0002
                    '                            'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                    '                            'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                    '                            pix = data2((row * &H20 * maxcols) + (col << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                    '                            b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                            a.SetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                    '                        Next
                    '                    Next
                    '                Next
                    '            Next
                    '        Next
                    '    End If
                Next
            Case 4
                Dim grphsecs As Byte
                FileGet(1, grphsecs, clpaddr + clppoint + 2 + ((layers And &H7F) * 3) + 1)
                Dim secoffsets(grphsecs - 1) As Short
                Dim tileammnts(grphsecs - 1) As Byte
                For sections = 0 To (grphsecs - 1)
                    FileGet(1, secoffsets(sections))
                    FileGet(1, tileammnts(sections))
                Next
                For lay = (layers And &H7F) - 1 To 0 Step -1
                    'Dim lay As Integer = 
                    Dim x As Byte
                    Dim y As Byte
                    FileGet(1, x, clpaddr + clppoint + 2 + (lay * 3) + ((layers >> 7) * 7) + 1)
                    'FileGet(1, x, clpaddr + clppoint + 2 + (lay << 2) + ((layers >> 7) * 7) + 1)
                    FileGet(1, y)
                    Dim xs As SByte = (x And &H1F) - (x And &H20)
                    Dim ys As SByte = (y And &H1F) - (y And &H20)
                    Dim q As Byte
                    FileGet(1, q)
                    'Dim t As Byte
                    'FileGet(1, t)
                    '0839EE04
                    '(x>>4)or(y>>6)
                    Dim tx As Byte
                    FileGet(1, tx, &H39EE04 + ((((x >> 6) << 2) Or (y >> 6)) << 1) + 1)
                    Dim ty As Byte
                    FileGet(1, ty)
                    Dim tileoffsetpntr As Short
                    Dim tileoffsetaddr As Integer
                    FileGet(1, tileoffsetpntr, animain - &H8000000 + 2 + 1)
                    tileoffsetaddr = animain - &H8000000 - tileoffsetpntr
                    Dim maxlayers As Byte
                    FileGet(1, maxlayers, animain - &H8000000 + 6 + 1)
                    maxlayers = maxlayers And &H7F
                    Dim tileoffsetnum As Byte
                    FileGet(1, tileoffsetnum, tileoffsetaddr + 1 + (ListBox1.SelectedIndex * (maxlayers + 1)) + lay + 1)
                    Dim tileoffsetnum2 As Short
                    For sections = 0 To (grphsecs - 1)
                        If tileoffsetnum < tileammnts(sections) Then
                            tileoffsetnum2 = secoffsets(sections) + tileoffsetnum
                            Exit For
                        End If
                        tileoffsetnum -= tileammnts(sections)
                    Next
                    'For tiley = 0 To (ty >> 3) - 1
                    '    For tilex = 0 To (tx >> 3) - 1
                    '        For fory = 0 To 7
                    '            For forx = 0 To 7
                    '                'clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + forx, (PictureBox2.Height >> 1) + ys + fory, Color.Red)
                    '                'Dim pix As Color
                    '                'For xypix = 0 To 1
                    '                'pix = data2((tileoffsetnum << 5) + (fory << 2) + forx) >> (xypix << 2) And &HF
                    '                'data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                    '                'b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                'a.Clone(New Rectangle(tileoffsetnum, tileoffsetnum, tileoffsetnum + 8, tileoffsetnum + 8), Imaging.PixelFormat.Format16bppRgb555)
                    '                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + forx, (PictureBox2.Height >> 1) + ys + (tiley << 3) + fory, Color.Red)
                    '                'Next
                    '            Next
                    '        Next
                    '    Next
                    'Next
                    Dim pix As Byte
                    'For ab = 0 To &H3FFF
                    '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
                    '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
                    'Next
                    Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8)
                    'Dim maxcols = 29 '16 'Does not include the 0
                    'For row = 0 To 15  '&H4000 
                    'For col = 0 To maxcols - 1 '31 '&H0800 
                    Dim pixbyte As Integer = CInt(tileoffsetnum2) << 5
                    Dim txflip1 As Byte
                    Dim txflip2 As Byte
                    Dim xstep As Short
                    If (q >> 7 And 1) Then
                        txflip1 = (tx >> 3) - 1
                        txflip2 = 0
                        xstep = -1
                    Else
                        txflip1 = 0
                        txflip2 = (tx >> 3) - 1
                        xstep = 1
                    End If
                    For tiley = 0 To (ty >> 3) - 1
                        'For tilex = 0 To (tx >> 3) - 1
                        For tilex = txflip1 To txflip2 Step xstep
                            For ypix = 0 To 7 '&H0040 
                                For xpix = 0 To 3 '&H0008 '16*8
                                    For xypix = 0 To 1 '&H0002
                                        'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                        'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                        'pix = data2((CInt(tileoffsetnum) << 5) + (tiley * (tx >> 3)) + (tilex << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                        pix = data2(pixbyte) >> (xypix << 2) And &HF
                                        If pix <> 0 Then
                                            b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                                            Try
                                                ' clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + (xpix << 1) + xypix, (PictureBox2.Height >> 1) + ys + (tiley << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                                                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + Math.Abs((-7 * (q >> 7 And 1)) + (xpix << 1) + xypix), (PictureBox2.Height >> 1) + ys + (tiley << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                                            Catch ex As Exception
                                                PictureBox2.Refresh()
                                                MsgBox("ERROR")
                                                GoTo errorexit4
                                            End Try
                                        End If
                                    Next
                                    pixbyte += 1
                                Next
                            Next
                        Next
                    Next

errorexit4:
                Next
            Case 5
                Dim grphsecs As Byte
                FileGet(1, grphsecs, clpaddr + clppoint + 2 + ((layers And &H7F) * 3) + 1)
                Dim secoffsets(grphsecs - 1) As Short
                Dim tileammnts(grphsecs - 1) As Byte
                For sections = 0 To (grphsecs - 1)
                    FileGet(1, secoffsets(sections))
                    FileGet(1, tileammnts(sections))
                Next
                For lay = (layers And &H7F) - 1 To 0 Step -1
                    'Dim lay As Integer = 
                    Dim x As Byte
                    Dim y As Byte
                    FileGet(1, x, clpaddr + clppoint + 2 + (lay * 3) + ((layers >> 7) * 7) + 1)
                    'FileGet(1, x, clpaddr + clppoint + 2 + (lay << 2) + ((layers >> 7) * 7) + 1)
                    FileGet(1, y)
                    Dim xs As SByte = (x And &H1F) - (x And &H20)
                    Dim ys As SByte = (y And &H1F) - (y And &H20)
                    Dim q As Byte
                    FileGet(1, q)
                    Dim t As Byte
                    FileGet(1, t)
                    '0839EE04
                    '(x>>4)or(y>>6)
                    Dim tx As Byte
                    FileGet(1, tx, &H39EE04 + ((((x >> 6) << 2) Or (y >> 6)) << 1) + 1)
                    Dim ty As Byte
                    FileGet(1, ty)
                    Dim tileoffsetpntr As Short
                    Dim tileoffsetaddr As Integer
                    FileGet(1, tileoffsetpntr, animain - &H8000000 + 2 + 1)
                    tileoffsetaddr = animain - &H8000000 - tileoffsetpntr
                    Dim maxlayers As Byte
                    FileGet(1, maxlayers, animain - &H8000000 + 6 + 1)
                    maxlayers = maxlayers And &H7F
                    Dim tileoffsetnum As Byte
                    FileGet(1, tileoffsetnum, tileoffsetaddr + 1 + (ListBox1.SelectedIndex * (maxlayers + 1)) + lay + 1)
                    Dim tileoffsetnum2 As Short
                    For sections = 0 To (grphsecs - 1)
                        If tileoffsetnum < tileammnts(sections) Then
                            tileoffsetnum2 = secoffsets(sections) + tileoffsetnum
                            Exit For
                        End If
                        tileoffsetnum -= tileammnts(sections)
                    Next
                    'For tiley = 0 To (ty >> 3) - 1
                    '    For tilex = 0 To (tx >> 3) - 1
                    '        For fory = 0 To 7
                    '            For forx = 0 To 7
                    '                'clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + forx, (PictureBox2.Height >> 1) + ys + fory, Color.Red)
                    '                'Dim pix As Color
                    '                'For xypix = 0 To 1
                    '                'pix = data2((tileoffsetnum << 5) + (fory << 2) + forx) >> (xypix << 2) And &HF
                    '                'data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                    '                'b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '                'a.Clone(New Rectangle(tileoffsetnum, tileoffsetnum, tileoffsetnum + 8, tileoffsetnum + 8), Imaging.PixelFormat.Format16bppRgb555)
                    '                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + forx, (PictureBox2.Height >> 1) + ys + (tiley << 3) + fory, Color.Red)
                    '                'Next
                    '            Next
                    '        Next
                    '    Next
                    'Next
                    Dim pix As Byte
                    'For ab = 0 To &H3FFF
                    '    pix = data2((ab >> 1)) >> ((ab And 1) << 2) And &HF
                    '    b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                    '    a.SetPixel(((ab And &H7C0) >> 3) + (ab And &H7), ((ab And &H3800) >> 8) + ((ab And &H38) >> 3), b) 'b(pix + (NumericUpDown5.Value << 4)))
                    'Next
                    Dim a As New Bitmap(16 * 8 * 2 - 20, 16 * 8)
                    'Dim maxcols = 29 '16 'Does not include the 0
                    'For row = 0 To 15  '&H4000 
                    'For col = 0 To maxcols - 1 '31 '&H0800 
                    Dim pixbyte As Integer = CInt(tileoffsetnum2) << 5
                    For tiley = 0 To (ty >> 3) - 1
                        For tilex = 0 To (tx >> 3) - 1
                            For ypix = 0 To 7 '&H0040 
                                For xpix = 0 To 3 '&H0008 '16*8
                                    For xypix = 0 To 1 '&H0002
                                        'pix = data2(&H2000 * numb + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
                                        'bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
                                        'pix = data2((CInt(tileoffsetnum) << 5) + (tiley * (tx >> 3)) + (tilex << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                        pix = data2(pixbyte) >> (xypix << 2) And &HF
                                        If pix <> 0 Then
                                            b = Color.FromArgb((palette1(pix + (&H10 * pal2)) And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 5 And &H1F) * 8, (palette1(pix + (&H10 * pal2)) >> 10 And &H1F) * 8)
                                            Try
                                                clipbmp.SetPixel((PictureBox2.Width >> 1) + xs + (tilex << 3) + (xpix << 1) + xypix, (PictureBox2.Height >> 1) + ys + (tiley << 3) + ypix, b) '(pix + (NumericUpDown5.Value << 4)))
                                            Catch ex As Exception
                                                PictureBox2.Refresh()
                                                MsgBox("ERROR")
                                                GoTo errorexit5
                                            End Try
                                        End If
                                    Next
                                    pixbyte += 1
                                Next
                            Next
                        Next
                    Next

errorexit5:
                Next
        End Select
a1:
        'PictureBox2.Image = clipbmp
        ''FileClose(1)
    End Sub

    Dim colind As Byte = 0
    Dim bgcolor() As Color = {SystemColors.Control, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Violet, Color.White} ', Color.Black}
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        colind = (colind + 1) And 7
        PictureBox2.BackColor = bgcolor(colind)
    End Sub
End Class