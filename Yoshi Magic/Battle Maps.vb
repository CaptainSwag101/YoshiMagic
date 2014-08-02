Public Class battlemaps
    'Room Properties
    Dim bbgmbank As Integer = &H83AFC5C '83A78D4
    Dim rooms As Short = &H27 + 1
    Dim ctind(rooms) As Byte
    Dim tsind(rooms) As Byte '+4 tileset
    Dim palind(rooms) As Byte
    Dim maplind(rooms) As Byte
    Dim aniind(rooms) As Byte
    'Compressed Tiling (Groups)
    Dim ctdbase As Integer = &H83AFE48 '&H83AAA6C
    Dim cts As Short = &H1D '119
    Dim ct1(cts), ct2(cts) As Byte
    'Compressed Tiling (Pixels)
    '*Dim ctdbase2 As Integer = &H89FC058
    '*Dim cts2 As Short = 32 '161 'A0
    'Dim ctp1(cts2) As Integer
    Dim palbmp As New Bitmap(16 * 8, 16 * 8, Imaging.PixelFormat.Format16bppRgb555) 'With {.imaging.PixelFormat}
    Dim b(255) As Color 'Palette Colors
    Dim data2(&H3FFF) As Byte 'uncompressed '1fff
    Dim offset1 As Integer '= &H6527F4 'Compressed Tiles Database
    Dim pic1bmp As New Bitmap(16 * 8 * 2, 16 * 4) ' 16 * 8)
    Dim pic2bmp As New Bitmap(16 * 8 * 2, 16 * 4)
    'Dim pic3bmp As New Bitmap(16 * 8 * 2, 16 * 4)
    Dim num As Integer = 0 'for offset in data2
    'Palette Related
    Dim palgrps As Short = &H20 '+1 '103
    Dim palind2(palgrps) As Byte
    'Tilesets (Groups)
    Dim tsgrps As Short = &H1E '70
    Dim ts1(tsgrps), ts2(tsgrps) As Short
    Dim ts1bmp As New Bitmap(512, 256) '(256, 128)
    Dim ts2bmp As New Bitmap(512, 256) '(256, 128)
    '083AAD68
    'Tilesets (Data)
    ' Dim tilesets As Short = ? '84
    Dim offset2 As Integer
    'Map 'Dim layer1bmp As New Bitmap(4000, 4000)
    Dim offset3 As Integer
    Dim layer1bmp As Bitmap '(100 * 480, 6 * 320)
    Dim layer2bmp As Bitmap '(100 * 480, 6 * 320)
    Dim layer1bmp2 As Bitmap
    Dim layer2bmp2 As Bitmap

    Dim maptabclick As Boolean = 0

    Private Sub roomproperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        '//----- GET Room Battle Backgrounds MAIN Data
        For a = 0 To rooms
            FileGet(1, ctind(a), bbgmbank - &H8000000 + (&HC * a) + 1)  'Compressed Images Index
            FileGet(1, tsind(a)) 'Tilesets Index
            FileGet(1, palind(a)) 'Palette Index
            FileGet(1, maplind(a)) 'Map Layers Index
            FileGet(1, aniind(a)) 'Animation Index

            'ListBox1.Items.Add(Hex(a) & " - " & ListBox2.Items.Item(rnind(a)))
            ListBox1.Items.Add(Hex(a)) 'List Rooms
        Next
        '//----- Compressed Image Groups
        For a = 0 To cts
            FileGet(1, ct1(a), ctdbase - &H8000000 + (a << 2) + 1)
            FileGet(1, ct2(a)) ', ctdbase - &H8000000 + (&H4 * a) + 1 + 1)
            'FileGet(1, ct3(a)) ', ctdbase - &H8000000 + (&H4 * a) + 2 + 1)
            Dim ctlist As String = ""
            If ct1(a) <> &HFF Then ctlist += "  T1:" & Hex(ct1(a))
            If ct2(a) <> &HFF Then ctlist += "  T2:" & Hex(ct2(a))
            'If ct3(a) <> &HFF Then ctlist += "  T3:" & Hex(ct3(a))
            'ListBox4.Items.Add(Hex(a) & " - T1:" & ct1(a) & " T2:" & ct2(a) & " T3:" & ct3(a))
            ListBox4.Items.Add(Hex(a) & " - " & ctlist)
            ComboBox3.Items.Add(ListBox4.Items.Item(a))
        Next
        'For a = 0 To cts2
        '    FileGet(1, ctp1(a), ctdbase2 - &H8000000 + (&H4 * a) + 1)
        '    ListBox5.Items.Add(Hex(a) & " - " & Hex(ctp1(a)))
        '    ComboBox5.Items.Add(ListBox5.Items.Item(a))
        '    ComboBox6.Items.Add(ListBox5.Items.Item(a))
        '    ComboBox7.Items.Add(ListBox5.Items.Item(a))
        'Next
        '//----- Tileset Groups
        Seek(1, &H3AFEC0 + 1) '&H3AAC4C
        For a = 0 To tsgrps
            FileGet(1, ts1(a)) ', &H3AAC4C + (a << 2) + 1)
            FileGet(1, ts2(a)) ', &H3AAC4C + (a << 2) + 2 + 1)
            ListBox6.Items.Add(Hex(a))
            ComboBox4.Items.Add(Hex(a))
        Next
        '//----- Map Layers
        'Grabbed in layer1 Sub
        '//----- Palette Groups
        Seek(1, &H3AFFD8 + 1) '3AAD68 + 1)
        For a = 0 To palgrps
            FileGet(1, palind2(a)) ', &H3AAD68 + a + 1)
            ListBox5.Items.Add(Hex(a))
        Next
        'FileClose(1)
        ListBox1.SelectedIndex = 0
        'MsgBox(TimeOfDay.Ticks - secs)
        'Me.SuspendLayout()
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'ComboBox1.SelectedIndex = rnind(a) 'Room Names
        'ComboBox2.SelectedIndex = mlind(a) 'Mario/Luigi
        'CheckBox1.Checked = underwaterflag(a) >> 4 And 1 'flag
        'Me.SuspendLayout()
        'NumericUpDown14.Left += 15
        ComboBox3.SelectedIndex = ctind(a) 'Comp. Tiles
        ListBox4.SelectedIndex = ctind(a)
        NumericUpDown6.Value = palind(a)
        ListBox5.SelectedIndex = palind(a)
        ComboBox4.SelectedIndex = tsind(a) 'tilesets
        ListBox6.SelectedIndex = tsind(a)

        NumericUpDown13.Value = maplind(a)
        NumericUpDown14.Value = aniind(a)

        ToolStripStatusLabel2.Text = "Room Props. Database: " & Hex(bbgmbank) & "  Room Props. Offset: " & Hex(bbgmbank - &H8000000 + (&HC * a))
        'ToolStripStatusLabel1.Text = rndpointer(-&H8000000 + (&H4 * a))
        'layer1()
        'layer1(Panel4, layer1bmp, 0)
        'layer1(Panel6, layer2bmp, 1)
        'layer1(Panel7, layer3bmp, 2)
        'layer2()
        'layer3()
        'Panel5.BackColor = b(0)
    End Sub
    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        '//----- PUT Room Properties Data
        For a = 0 To rooms
            Seek(1, bbgmbank - &H8000000 + (&HC * a) + 1)
            'FilePut(1, rnind(a)) ' rpbank - &H8000000 + (&H18 * a) + 0 + 1) 'Room Names Index
            'FilePut(1, mlind(a)) ', rpbank - &H8000000 + (&H18 * a) + 1 + 1) 'Mario/Luigi
            'FilePut(1, underwaterflag(a)) 'flag
            FilePut(1, ctind(a)) ', rpbank - &H8000000 + (&H18 * a) + 3 + 1) 'Comp. Tiles
            FilePut(1, tsind(a)) ', rpbank - &H8000000 + (&H18 * a) + 4 + 1) 'tileset
            FilePut(1, palind(a)) ', rpbank - &H8000000 + (&H18 * a) + 5 + 1) 'Palette Index
            FilePut(1, maplind(a))
            FilePut(1, aniind(a))

            'ListBox1.Items.Add(Hex(a) & " - " & ListBox2.Items.Item(rnind(a)))
            'List Rooms
            'ListBox1.Items.Add(Hex(a) & " -" & RoomName(rnind(a)) & " : " & RoomName2(rnind(a)))
        Next
        'FileClose(1)
        MsgBox("Saved!")
    End Sub
    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        'ON COMPRESSED IMAGES LISTBOX SELECT (UPDATE COMPRESSED IMAGES NUMBERICS)
        'If ListBox4.ContainsFocus Then 'MsgBox("!")
        Dim a = ListBox4.SelectedIndex
        NumericUpDown2.Value = ct1(a)
        NumericUpDown3.Value = ct2(a)
        'NumericUpDown4.Value = ct3(a)
        'End If
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        'If Focused = 0 Then MsgBox("TEST")
        'MsgBox("!")
        'ON PALETTE SET CHANGE (GET PALETTE DATA > TURN TO COLORS > DISPLAY IN PALETTE BOX > UPDATE COMPRESSED IMAGES > UPDATE TILESETS)
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        Dim palette1 As Short
        'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?) Also, move outside SUB.
        'Dim palette1(255) As Short
        'Dim b(255) As Color ' = Color.FromArgb(0, 0, 0) 'Red
        'PALETTE SECTION
        'Get Palette #
        Dim nval As Integer = NumericUpDown1.Value
        Seek(1, &HA4FD50 + &H1E0 * nval + 1) 'A4FCCC
        'Seek(1, &HA53BB4 + (&H1E0 * nval) + 1) 'A53B30 ***THE ALTERNATE PALETTE DATABASE*** Not sure how it's used.
        For row = 0 To 15
            For col = 0 To 15
                '//Get Palette data.
                FileGet(1, palette1)
                'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?)
                'FileGet(1, palette1(row * 16 + col)) ', &H8C88C8 + (&H1E0 * nval) + ((row * 16 + col) * 2 + 1))
                'FileGet(1, palette1(row * 16 + col)) ', &H8D49A0 + (&H1E0 * nval) + ((row * 16 + col) * 2 + 1))
                '//Turn data into colors.
                b((row << 4) + col) = Color.FromArgb((palette1 And &H1F) << 3, (palette1 >> 5 And &H1F) << 3, (palette1 >> 10 And &H1F) << 3)

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
        PictureBox6.Image = palbmp
        'Me.TabPage4.
        'CreateGraphics.DrawImageUnscaled(palbmp, New Point(0, 0)) 'PictureBox6.Left, PictureBox6.Top))
        'CreateGraphics.c()
        image3() 'False) 'Updates the 3 "compressed" images
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        'FileClose(1)
        'layer1(Panel4, layer1bmp, 0) 'Updates the map.
        'layer1(Panel6, layer2bmp, 1)
        'layer1(Panel7, layer3bmp, 2)
    End Sub
    Sub image3()
        'UPDATE COMPRESSED IMAGES (All-in-One)
        cimage(NumericUpDown2, pic1bmp, PictureBox1, 0)
        cimage(NumericUpDown3, pic2bmp, PictureBox2, 1)
        'cimage(NumericUpDown4, pic3bmp, PictureBox3, 2)
    End Sub
    Sub cimage(ByVal nud As NumericUpDown, ByVal bm As Bitmap, ByVal pb As PictureBox, ByVal n As Byte)
        'UPDATE A COMPRESSED IMAGE (Decompress > Display Image)
        'bm = New Bitmap(16 * 8 * 2, 16 * 4) '(20, 23)
        Dim numud2val As Byte = nud.Value 'NumericUpDown2.Value
        If numud2val <> &HFF Then
            'Dim offset1 As Integer
            'FileGet(1, offset1, &H6527F4 + (CInt(numud2val) << 2) + 1) 'Maps
            'FileGet(1, offset1, &HA57994 + (CInt(numud2val) << 2) + 1) 'Suitcase
            'FileGet(1, offset1, &H9F808C + (CInt(numud2val) << 2) + 1) 'Battle Menus
            FileGet(1, offset1, &H9FC058 + (CInt(numud2val) << 2) + 1) 'Battle BGs
            'offset1 = &H6527F4 + offset1
            Try
                'Seek(1, &H6527F4 + offset1 + 1)
                'Seek(1, &HA57994 + offset1 + 1)
                'Seek(1, &H9F808C + offset1 + 1)
                Seek(1, &H9FC058 + offset1 + 1)
                'Array.Clear(data2, 0, &H6000)
                num = CShort(n) << 13 'Offset to put data in variable data2 for sub decomp.
                decomp()
                nud.BackColor = Color.White
            Catch
                nud.BackColor = Color.Red
            End Try
            disimg(bm, n)
            pb.Image = bm
        Else
            pb.Image = Nothing
        End If
    End Sub
    Sub decomp() 'DECOMPRESSION ROUTINE '40 lines
        Dim arg1, data1 As Byte 'compressed args 'compressed attributes 'Dim data2(&H1FFF) As Byte 'uncompressed
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
    ''' <summary>
    ''' Display a pixel compressed image. (It should already be decompressed into variable data2.)
    ''' </summary>
    ''' <param name="bmp">Bitmap to display image on.</param>
    ''' <param name="numb">Which image? (0-2)</param>
    ''' <remarks></remarks>
    Sub disimg(ByVal bmp As Bitmap, ByVal numb As Integer) 'DISPLAYING COMPRESSED IMAGES (PIXELS)
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
        For a = 0 To &H3FFF
            pix = data2((numb << 13) + (a >> 1)) >> ((a And 1) << 2) And &HF
            bmp.SetPixel(((a And &H7C0) >> 3) + (a And &H7), ((a And &H3800) >> 8) + ((a And &H38) >> 3), b(pix + (NumericUpDown5.Value << 4)))
        Next
        'Timer1.Stop()
        'MsgBox(ms)
    End Sub
    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        'If NumericUpDown2.Focused = 0 Then Exit Sub 'Compreesed Image 1
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        cimage(NumericUpDown2, pic1bmp, PictureBox1, 0) ', True)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        ''FileClose(1)
    End Sub
    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        'If NumericUpDown3.Focused = 0 Then Exit Sub 'Compressed Image 2
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        cimage(NumericUpDown3, pic2bmp, PictureBox2, 1) ', True)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        ''FileClose(1)
    End Sub
    'Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'If NumericUpDown4.Focused = 0 Then Exit Sub
    '    'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
    '    cimage(NumericUpDown4, pic3bmp, PictureBox3, 2) ', True)
    '    tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
    '    tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
    '    'FileClose(1)
    'End Sub
    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        'If NumericUpDown5.Focused = 0 Then Exit Sub 'Update 16 colors of images (Palette Index)
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        image3() 'False)
        FileClose()
    End Sub
    Private Sub ListBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox5.SelectedIndexChanged
        Dim a = ListBox5.SelectedIndex
        NumericUpDown1.Value = palind2(a)
    End Sub
    Private Sub ListBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox6.SelectedIndexChanged
        Dim a = ListBox6.SelectedIndex
        NumericUpDown7.Value = ts1(a)
        NumericUpDown8.Value = ts2(a)
    End Sub
    Private Sub NumericUpDown7_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown7.ValueChanged
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp)
        'FileClose(1)
    End Sub
    ''' <summary>
    ''' Displays a tileset.
    ''' </summary>
    ''' <param name="nud">Numeric U/D that holds the Tileset number.</param>
    ''' <param name="pb">PictureBox to put Bitmap in.</param>
    ''' <param name="bm">Bitmap to put tileset image in.</param>
    ''' <remarks></remarks>
    Sub tilesetnum(ByVal nud As NumericUpDown, ByVal pb As PictureBox, ByVal bm As Bitmap)
        If nud.Value = -1 Then pb.Image = Nothing : Exit Sub
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        Dim ts2num As Short = nud.Value
        FileGet(1, offset2, &HA2711C + (ts2num << 2) + 1) '&H6FFC20
        'offset2 = &H6FFC20 + offset2
        Seek(1, &HA2711C + offset2 + 1)
        pb.Image = bm
        Dim tile As UShort 'Intevger 'Short
        Dim stile As Short
        Dim pix As Byte
        For row = 0 To 15 '7 '15
            For col = 0 To 31 '15
                For r = 0 To 1
                    For c = 0 To 1
                        'FileGet(1, tile, offset2 + 1)
                        FileGet(1, stile)
                        'tile = CType(stile, Short)
                        tile = stile And &HFFFF 'Convert.ToUInt16(stile
                        'If stile < 0 Then tile = &H8000 + (stile And &H7FFF) Else tile = stile And &H7FFF
                        'tile = Convert.ToUInt16(stile)
                        ' tile = tile And &HFFFF ' Avoiding SIGNED.
                        'offset2 += 2
                        For ypix = 0 To 7
                            For xpix = 0 To 3 '16*8
                                For xypix = 0 To 1
                                    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> (xypix << 2) And &HF
                                    bm.SetPixel((col << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + xypix), _
                                                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                    b(((tile >> 12) << 4) + pix))
                                Next
                            Next
                        Next

                    Next
                Next
            Next
        Next
        ''FileClose(1)
    End Sub
    Private Sub NumericUpDown8_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown8.ValueChanged
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        'FileClose(1)
    End Sub

    ''' <summary>
    ''' Sets a map layer to a Panel.
    ''' </summary>
    ''' <param name="a">Panel to show layer on.</param>
    ''' <param name="lay">Map layer index number (0, 1, or 2)</param>
    ''' <remarks>REMARKS!</remarks>
    Public Sub layer1(ByVal a As Panel, ByVal bm As Bitmap, ByVal lay As Integer)
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'Dim roomnum As Short = ListBox1.SelectedIndex
        Dim layersets As Byte
        Dim layer1ind As Short
        FileGet(1, layersets, &H3AFC5C + (ListBox1.SelectedIndex * &HC) + 3 + 1)
        FileGet(1, layer1ind, &H3AFF3C + (layersets << 2) + (lay << 1) + 1)
        'FileGet(1, layer1ind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + (lay << 1) + 1)
        If layer1ind = -1 Then a.BackgroundImage = Nothing 'FileClose(1) : Exit Sub
        FileGet(1, offset3, &HA46198 + (layer1ind << 2) + 1) ' &H7550BC + 1) '754D74
        'offset3 = &H754D74 + offset3
        'Seek(1, &H754D74 + offset3 + 1)
        offset3 = &HA46198 + offset3 ' Loc(1)
        a.Width = 512 'scnw * 240
        a.Height = 256 'scnh * 160
        'Dim bm As New Bitmap(scnw * 15 * 16 * 2, scnh * 10 * 16 * 2)
        'bm = New Bitmap(scnw * 15 * 16 * 2, scnh * 10 * 16 * 2)
        ' bm = New Bitmap(scnw * 480, scnh * 320, Imaging.PixelFormat.Format16bppArgb1555)
        bm = New Bitmap(512, 256, Imaging.PixelFormat.Format16bppArgb1555)
        'Panel4.BackColor.
        'CreateGraphic()
        'Dim maptile As Short
        Dim tile As UShort
        Dim stile As Short
        'Dim stem As Byte
        Dim leaf As Byte
        Dim pix As Byte
        For row = 0 To 15 '15 '7 '15
            For col = 0 To 31 ' Math.Ceiling((scnw * 15 / 4)) - 1 '31 '15
                'FileGet(1, stem, offset3 + 1)
                'For stemleaf = 0 To 3
                'pix = stem >> (Math.Abs(stemleaf - 3) << 1)
                'Select Case pix And 2
                'Case 0
                'FileGet(1, offset2, &H6FFC20 + (NumericUpDown7.Value << 2) + 1)
                'Case 2
                'If NumericUpDown8.Value <> -1 Then FileGet(1, offset2, &H6FFC20 + (NumericUpDown8.Value << 2) + 1)
                'End Select
                FileGet(1, leaf, offset3 + 1) '+ (row << 5) + col + 1)
                'maptile = ((CShort(stem) << 8) + leaf)
                'Seek(1, &H6FFC20 + offset2 + (maptile << 3) + 1)
                'Seek(1, &H6FFC20 + offset2 + (((CShort(pix And 1) << 8) + leaf) << 3) + 1)
                FileGet(1, offset2, &HA2711C + (NumericUpDown7.Value << 2) + 1)
                Seek(1, &HA2711C + offset2 + (CInt(leaf) << 3) + 1) '(((CShort(pix And 1) << 8) + leaf) << 3) + 1)
                'MsgBox(Hex(&HA2711C + (NumericUpDown7.Value << 2)) & " / " & Hex(&HA2711C + offset2) & " / " & Hex(&HA2711C + offset2 + (CInt(leaf) << 3)) _
                '       & " / " & Hex(leaf) & " / " & "  X: " & (col) & "  Y: " & (row))
                For r = 0 To 1
                    For c = 0 To 1
                        FileGet(1, stile) ', offset2 + (maptile << 3) + (r << 2) + (c << 1) + 1)
                        tile = stile And &HFFFF ' Avoiding SIGNED.
                        For ypix = 0 To 7
                            For xpix = 0 To 3 '16*8
                                'If lay = 2 Then 'BLENDING test...
                                '    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) And &HF
                                '    If pix = 0 Then
                                '        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                '                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                '                         Color.Transparent)
                                '    Else
                                '        'bm.PixelFormat.Format16bppRgb555()
                                '        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                '                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                '                        b(((tile >> 12) << 4) + pix))
                                '    End If
                                '    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> 4
                                '    If pix = 0 Then
                                '        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                '                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                '                         Color.Transparent)
                                '    Else
                                '        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                '                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                '                        b(((tile >> 12) << 4) + pix))
                                '    End If
                                'Else
                                pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) And &HF
                                If pix = 0 Then
                                    'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                    '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                    '                 Color.Transparent)
                                Else
                                    bm.SetPixel((((col << 4))) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                    b(((tile >> 12) << 4) + pix))
                                End If
                                pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> 4
                                If pix = 0 Then
                                    'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                    '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                    '                 Color.Transparent)
                                Else
                                    bm.SetPixel((((col << 4))) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                    b(((tile >> 12) << 4) + pix))
                                End If
                                '                                    End If
                            Next
                        Next

                    Next
                Next
                'Next
                offset3 += 1
            Next
        Next
        'layer1bmp2 = Panel4.BackgroundImage
        'layer2bmp2 = Panel6.BackgroundImage
        'layer3bmp2 = Panel7.BackgroundImage
        'If maptabclick = 0 Then
        '    Dim pixcol As Color
        '    If NumericUpDown10.Value < 16 Then
        '        For pixy = 0 To 15
        '            For pixx = 0 To 15
        '                pixcol = ts1bmp.GetPixel(NumericUpDown9.Value * 16 + pixx, NumericUpDown10.Value * 16 + pixy)
        '                bm.SetPixel(NumericUpDown11.Value * 16 + pixx, NumericUpDown12.Value * 16 + pixy, pixcol)
        '            Next
        '        Next
        '    Else
        '        'PictureBox8
        '    End If
        'End If
        'FileClose(1)
        a.BackgroundImage = bm 'layer1bmp
    End Sub
    Public Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If Panel4.BackgroundImage IsNot Nothing Then layer1bmp2 = Panel4.BackgroundImage
        If CheckBox15.Checked Then Panel4.BackgroundImage = layer1bmp2 Else Panel4.BackgroundImage = Nothing
    End Sub
    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        If Panel6.BackgroundImage IsNot Nothing Then layer2bmp2 = Panel6.BackgroundImage
        If CheckBox16.Checked Then Panel6.BackgroundImage = layer2bmp2 Else Panel6.BackgroundImage = Nothing
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 4 Then
            If ListBox1.SelectedIndex = -1 Then Exit Sub
            'maptabclick = 1
            'Dim seconds = TimeOfDay.Ticks
            'Panel5.BackColor = b(0)
            'TabPage10.BackColor = b(0)
            TabPage10.Text = "Loading..."
            TabControl1.Update()
            Panel4.Hide()
            'Panel6.Hide()
            'Panel7.Hide()
            layer1(Panel4, layer1bmp, 0) 'Updates the map.
            layer1(Panel6, layer2bmp, 1)
            'layer1(Panel7, layer3bmp, 2)
            layer1bmp2 = Panel4.BackgroundImage
            layer2bmp2 = Panel6.BackgroundImage
            'layer3bmp2 = Panel7.BackgroundImage
            'Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            'Case 1
            'Panel4.BackgroundImage = layer2bmp2
            'Panel6.BackgroundImage = layer1bmp2
            'Panel7.BackgroundImage = layer3bmp2
            'layer1bmp = Panel4.BackgroundImage
            'layer2bmp = Panel6.BackgroundImage
            'layer3bmp = Panel7.BackgroundImage
            'End Select
            Panel4.Show()
            TabPage10.Text = "Map Editor"
            'Panel6.Show()
            'Panel7.show()
            'PictureBox7.Image = ts1bmp
            'PictureBox8.Image = ts2bmp
            'If NumericUpDown8.Value = &HFFFFS Then PictureBox8.Image = Nothing
            'MsgBox(seconds & " to " & TimeOfDay.Ticks & " = " & (TimeOfDay.Ticks - seconds) / 10000000 & " seconds.")
            'maptabclick = 0
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'NumericUpDown9 'Tileset X
        'NumericUpDown10 'Tileset Y
        'NumericUpDown11 'Map X
        'NumericUpDown12 'Map Y
        'Dim pixcol As Color
        'If NumericUpDown10.Value < 16 Then
        '    For pixy = 0 To 15
        '        For pixx = 0 To 15
        '            pixcol = ts1bmp.GetPixel(NumericUpDown9.Value * 16 + pixx, NumericUpDown10.Value * 16 + pixy)
        '            layer1bmp.SetPixel(NumericUpDown11.Value * 16 + pixx, NumericUpDown12.Value * 16 + pixy, pixcol)
        '        Next
        '    Next
        'Else
        '    'PictureBox8
        'End If
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        Dim layer1ind As Short
        Dim lay As Byte = 0 'Layer # (0-2)
        'layer1bmp2 = Panel4.BackgroundImage
        'layer2bmp2 = Panel6.BackgroundImage
        'layer3bmp2 = Panel7.BackgroundImage
        If RadioButton1.Checked Then lay = 0
        If RadioButton2.Checked Then lay = 1
        'If RadioButton3.Checked Then lay = 2
        'Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
        '    Case 1
        '        'Panel4.BackgroundImage = layer2bmp2
        '        'Panel6.BackgroundImage = layer1bmp2
        '        'Panel7.BackgroundImage = layer3bmp2
        '        If RadioButton1.Checked Then lay = 1
        '        If RadioButton2.Checked Then lay = 0
        '        'If RadioButton3.Checked Then lay = 2
        'End Select
        For h = 0 To NumericUpDown29.Value - 1
            For w = 0 To NumericUpDown28.Value - 1
                FileGet(1, layer1ind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + (lay << 1) + 1)
                If layer1ind = -1 Then Panel4.BackgroundImage = Nothing 'FileClose(1) : Exit Sub
                FileGet(1, offset3, &H754D74 + (layer1ind << 2) + 1) ' &H7550BC + 1)
                'offset3 = &H754D74 + offset3
                'Seek(1, &H754D74 + offset3 + 1)
                Dim scnw As Byte
                FileGet(1, scnw, &H754D74 + offset3 + 1)
                Dim scnh As Byte
                FileGet(1, scnh)
                Dim stem As Byte
                If (NumericUpDown12.Value + h) = 0 Then
                    FileGet(1, stem, Loc(1) + 1 + ((NumericUpDown11.Value + w) >> 2) * 5)
                Else
                    FileGet(1, stem, CInt(((Loc(1) + (NumericUpDown12.Value + h) * (((scnw * 15) >> 2) + 1) * 5 + 3) And &HFFFCS) + ((NumericUpDown11.Value + w) >> 2) * 5) - 1)
                End If
                'stem = (stem And (&HFF Xor (&H3 << (Math.Abs((NumericUpDown11.Value And &H3) - 3) << 1)))) + _
                '(((NumericUpDown10.Value >> 3) And &H3) << (Math.Abs((NumericUpDown11.Value And &H3) - 3) << 1))
                'NumericUpDown9 'Tileset X
                'NumericUpDown10 'Tileset Y
                'NumericUpDown11 'Map X
                'NumericUpDown12 'Map Y
                stem = (stem And (Not (&H3 << (Math.Abs(((NumericUpDown11.Value + w) And &H3) - 3) << 1)))) + _
                        ((((NumericUpDown10.Value + h) >> 3) And &H3) << (Math.Abs(((NumericUpDown11.Value + w) And &H3) - 3) << 1))
                'stem = (stem >> (Math.Abs(((NumericUpDown10.Value >> 3) And &H3) - 3) << 1)) And 3
                FilePut(1, stem, Loc(1))
                Dim leaf As Byte
                Seek(1, Loc(1) + 1 + ((NumericUpDown11.Value + w) And &H3))
                leaf = (((NumericUpDown10.Value + h) And &H7) << 5) Or ((NumericUpDown9.Value + w) And &H1F)
                FilePut(1, leaf)
            Next
        Next
        'If maptabclick = 0 Then
        'Dim pixcol As Color
        'If NumericUpDown10.Value < 16 Then
        '    For row = 0 To scnh * 10 - 1 '15 '7 '15
        '        For col = 0 To (((scnw * 15 + 3) And &HFFFCS) / 4) - 1 ' Math.Ceiling((scnw * 15 / 4)) - 1 '31 '15
        '            For stemleaf = 0 To 3
        '                FileGet(1, stem, offset3 + 1)
        '            Next
        '        Next
        '    Next
        'For pixy = 0 To 15
        '    For pixx = 0 To 15
        '        pixcol = ts1bmp.GetPixel(NumericUpDown9.Value * 16 + pixx, NumericUpDown10.Value * 16 + pixy)
        '        bm.SetPixel(NumericUpDown11.Value * 16 + pixx, NumericUpDown12.Value * 16 + pixy, pixcol)
        '    Next
        'Next
        'Else
        ''PictureBox8
        'End If
        'End If
        'FileClose(1)
        'layer1(Panel4, layer1bmp, 0)
        'Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
        '    Case 0
        '        If lay = 0 Then layer1(Panel4, layer1bmp, 0) 'Updates the map.
        '        If lay = 1 Then layer1(Panel6, layer2bmp, 1)
        '        'If lay = 2 Then layer1(Panel7, layer3bmp, 2)
        '    Case 1
        '        If lay = 1 Then layer1(Panel4, layer2bmp, 1) 'Updates the map.
        '        If lay = 0 Then layer1(Panel6, layer1bmp, 0)
        '        'If lay = 2 Then layer1(Panel7, layer3bmp, 2)
        'End Select
        Exit Sub
cleanup:  'FileClose(1)
    End Sub
    'Sub savetile(ByVal a As Panel, ByVal lay As Integer)
    '    'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
    '    Dim roomnum As Short = ListBox1.SelectedIndex
    '    Dim layer1ind As Short
    '    FileGet(1, layer1ind, &H3AAE08 + (ListBox1.SelectedIndex * 8) + (2 * lay) + 1)
    '    If layer1ind = -1 Then a.BackgroundImage = Nothing : 'FileClose(1) : Exit Sub
    '    FileGet(1, offset3, &H754D74 + (layer1ind * 4) + 1) ' &H7550BC + 1)
    '    offset3 = &H754D74 + offset3
    'End Sub
    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'Dim rect As New Rectangle(0, 0, 256, 64)
        Dim savimg As New Bitmap(256, 64 + 16)
        'savimg = pic1bmp.Clone(rect, pic1bmp.PixelFormat) '(rect, pic1bmp.PixelFormat)
        Dim savcol As Color
        For y = 0 To 63
            For x = 0 To 255
                savcol = pic1bmp.GetPixel(x, y)
                savimg.SetPixel(x, y, savcol)
            Next
        Next
        For a = 0 To 15 'color
            savcol = palbmp.GetPixel(a * 8, NumericUpDown5.Value * 8)
            For y = 0 To 15 'pixels of a color (y)
                For x = 0 To 15 'pixels of a color (x)
                    savimg.SetPixel(a * 16 + x, 64 + y, savcol)
                Next
            Next
        Next
        savimg.Save(SaveFileDialog1.FileName)
        'savimg.Clone()
    End Sub
    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        If OpenFileDialog3.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim loadpic As Bitmap
        Dim loadcolors(15) As Color
        Dim checkcolor As Color
        'Dim num
        loadpic = Image.FromFile(OpenFileDialog3.FileName)
        For a = 0 To 15
            loadcolors(a) = loadpic.GetPixel(a * 16, 64)
        Next
        'Loadimage to data2 uncompressed data
        For row = 0 To 7
            For col = 0 To 31
                For ypix = 0 To 7
                    For xpix = 0 To 3
                        data2((row << 10) + (col << 5) + (ypix << 2) + xpix) = 0
                        For xypix = 0 To 1
                            checkcolor = loadpic.GetPixel((col << 3) + (xpix << 1) + xypix, (row << 3) + ypix)
                            For c = 0 To 15
                                If checkcolor = loadcolors(c) Then data2((row << 10) + (col << 5) + (ypix << 2) + xpix) += (c << (xypix << 2)) : Exit For
                                If c = 15 Then MsgBox("Color does not match palette at X:" & (col << 3) + (xpix << 1) + xypix & " Y:" & (row << 3) + ypix)
                            Next
                        Next
                        ''xypix = 0
                        'checkcolor = loadpic.GetPixel(col * 8 + xpix * 2, row * 8 + ypix)
                        'For c = 0 To 15
                        '    If checkcolor = loadcolors(c) Then data2(row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) = c : Exit For
                        '    If c = 15 Then MsgBox("Color does not match palette at X:" & col * 8 + xpix * 2 & " Y:" & row * 8 + ypix)
                        'Next
                        ''xypix = 1
                        'checkcolor = loadpic.GetPixel(col * 8 + xpix * 2 + 1, row * 8 + ypix)
                        'For c = 0 To 15
                        '    If checkcolor = loadcolors(c) Then data2(row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) += (c << 4) : Exit For
                        '    If c = 15 Then MsgBox("Color does not match palette at X:" & col * 8 + xpix * 2 + 1 & " Y:" & row * 8 + ypix)
                        'Next
                    Next
                Next
            Next
        Next

        'ln382
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        disimg(pic1bmp, 0)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        PictureBox1.Image = pic1bmp
        'FileClose(1)
        'Dim pix As Byte
        'For row = 0 To 7 '15
        '    For col = 0 To 31 '15
        '        For ypix = 0 To 7
        '            For xpix = 0 To 3 '16*8
        '                For xypix = 0 To 1
        '                    pix = data2(&H2000 * num + row * &H20 * &H20 + col * &H20 + ypix * 4 + xpix) >> xypix * 4 And &HF
        '                    bmp.SetPixel(col * 8 + xpix * 2 + xypix, row * 8 + ypix, b(pix + NumericUpDown5.Value * 16))
        '                Next
        '            Next
        '        Next
        '    Next
        'Next
        'compress()
    End Sub

    Sub compress()
        Dim num2 = 0
        Dim numc = 2 'Temp
        Dim datacomp(1000) As Byte
        For c = 0 To 200 '# of arguments..
            'ARG E0-FF
            If data2(num2) = 0 Then
                If data2(num2 + 1) = 0 Then
                    For z = 0 To &H1F '+ &H7F 'Value of attr.
                        If data2(num2 + z) = 0 Then datacomp(numc) = &HE0 + z Else GoTo nextarg
                        num2 += 1 'next uncomp byte
                    Next
                    numc += 1 'next attr
                    For z2 = 0 To &H7F
                        '+&H20
                        If data2(num2 + z2 + &H20) = 0 Then datacomp(numc) = z2 Else GoTo nextarg
                        num2 += 1 'next uncomp byte
                    Next

                Else
                    'A0-BF
                End If
            End If

            If data2(num2) = data2(num2 + 1) Then
                'C0-DF

            End If

            For a = 0 To &HF
                '80-9F
            Next

nextarg:    numc += 1
        Next
    End Sub
    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    'ms += 1
    'End Sub
    ' Private Sub Panel9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel9.Click
    'For y = 0 To 31
    '    For x = 0 To 31
    '        Panel1.PointToScreen(New Point(3, 3))
    '    Next
    'Next
    'MsgBox(MousePosition.Y.ToString)
    ' End Sub
    Private Sub Panel6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel6.Click
        NumericUpDown11.Value = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown12.Value = (MousePosition.Y.ToString - Panel6.PointToScreen(New Point(0, 0)).Y) >> 4
        If ModifierKeys = Keys.Control Then Button2.PerformClick()
    End Sub
    Private Sub Panel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.Click
        NumericUpDown11.Value = (MousePosition.X.ToString - Panel4.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown12.Value = (MousePosition.Y.ToString - Panel4.PointToScreen(New Point(0, 0)).Y) >> 4
        If ModifierKeys = Keys.Control Then Button2.PerformClick()
    End Sub
    'Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
    'End Sub
    ' Private Sub TabPage10_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TabPage10.Paint
    'pen1.Color = b(0)
    'Dim pen2 As New Pen(
    'MsgBox("!")
    'TabPage10.BackColor = b(0) 'e.grap
    ' End Sub
    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint
        Dim pen1 As New SolidBrush(b(0))
        e.Graphics.FillRectangle(pen1, 0, 0, Panel4.Width, Panel4.Height) 'DrawImageUnscaled(
    End Sub
    ' Private Sub Panel4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.MouseHover
    ' End Sub
    ' Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
    'Dim img As Image
    'img = layer1bmp
    'Dim bru1 As Brush
    'bru1.layer1bmp()
    'e.Graphics.FillRectangle(bru1, 0, 0, Panel4.Width, Panel4.Height) ' layer1bmp.
    '.FromImage(layer1bmp) ', 0, 0)
    'Image.FromHbitmap(1)
    'Graphics(g)
    ' End Sub
    'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    layer1bmp2 = Panel4.BackgroundImage
    '    layer2bmp2 = Panel6.BackgroundImage
    '    '480:
    '    '320:
    '    Dim col1 As Color
    '    Dim col2 As Color
    '    Dim col3 As Color
    '    For h = 0 To layer3bmp2.Height - 1
    '        For w = 0 To layer3bmp2.Width - 1
    '            col1 = layer1bmp2.GetPixel(w, h)
    '            col2 = layer2bmp2.GetPixel(w, h)
    '            col3 = layer3bmp2.GetPixel(w, h)
    '            'MsgBox(col2.ToString)
    '            'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb + col1.ToArgb))
    '            If col2 = Color.FromArgb(0, 0, 0, 0) Then ' 255, 255, 255) Then
    '                'MsgBox(col2.ToString)
    '                layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col1.ToArgb + Color.FromArgb(0, 255, 255, 255).ToArgb))
    '                'layer3bmp2.SetPixel(w, h, Color.FromArgb((col3.ToArgb + col1.ToArgb)))
    '                'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col1.ToArgb))
    '            Else
    '                layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb + Color.FromArgb(0, 255, 255, 255).ToArgb))
    '                'layer3bmp2.SetPixel(w, h, Color.FromArgb((col3.ToArgb + col2.ToArgb)))
    '                'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb)) ' + Color.FromArgb(0, 255, 255, 255).ToArgb))
    '            End If
    '            'col1()
    '            'ColorConverter()
    '            'Microsoft.Visu
    '        Next
    '    Next
    'pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) And &HF

    'If pix = 0 Then
    '    bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
    '                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                     Color.Transparent)
    'Else
    '    bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
    '                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                    b(((tile >> 12) << 4) + pix))
    'End If
    'pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> 4
    'If pix = 0 Then
    '    bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
    '                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                     Color.Transparent)
    'Else
    '    bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
    '                    (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                    b(((tile >> 12) << 4) + pix))
    'End If
    'End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        ctind(a) = ComboBox3.SelectedIndex  'Comp. Tiles
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        tsind(a) = ComboBox4.SelectedIndex 'tilesets
    End Sub
    Private Sub NumericUpDown6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        Dim a = ListBox1.SelectedIndex
        palind(a) = NumericUpDown6.Value
    End Sub
    Private Sub NumericUpDown13_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown13.ValueChanged
        Dim a = ListBox1.SelectedIndex
        maplind(a) = NumericUpDown13.Value
    End Sub
    Private Sub NumericUpDown14_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown14.ValueChanged
        Dim a = ListBox1.SelectedIndex
        aniind(a) = NumericUpDown14.Value
    End Sub
    Private Sub Panel11_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel11.MouseDown
        NumericUpDown28.Value = 1
        NumericUpDown29.Value = 1
        NumericUpDown9.Value = (MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown10.Value = (MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4
        Panel11.Refresh()
    End Sub
    Private Sub Panel11_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel11.MouseMove
        If MouseButtons = MouseButtons.Left Then
            If NumericUpDown28.Value <> ((MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4) - NumericUpDown9.Value + 1 Then
                Try
                    NumericUpDown28.Value = ((MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4) - NumericUpDown9.Value + 1
                    If NumericUpDown9.Value + NumericUpDown28.Value > 32 Then NumericUpDown28.Value = 32 - NumericUpDown9.Value
                    Panel11.Refresh()
                Catch ex As Exception
                End Try
            End If
            If NumericUpDown29.Value <> ((MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4) - NumericUpDown10.Value + 1 Then
                Try
                    NumericUpDown29.Value = ((MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4) - NumericUpDown10.Value + 1
                    If NumericUpDown10.Value + NumericUpDown29.Value > 32 Then NumericUpDown29.Value = 32 - NumericUpDown10.Value
                    Panel11.Refresh()
                Catch ex As Exception
                End Try
            End If
            'Panel9.AutoScrollPosition = New Point(0, (MousePosition.Y.ToString - Panel9.PointToScreen(New Point(0, 0)).Y))
            'If (MousePosition.Y.ToString - Panel9.PointToScreen(New Point(0, 128)).Y) > 0 Then
            'Panel9.AutoScrollPosition = New Point(0, +8) ' (NumericUpDown10.Value + NumericUpDown29.Value) * 16)

            'End If
        End If
    End Sub
    'Private Sub Panel11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel11.Click
    '    If ModifierKeys = Keys.Control Then
    '        NumericUpDown28.Value = ((MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4) - NumericUpDown9.Value + 1
    '        NumericUpDown29.Value = ((MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4) - NumericUpDown10.Value + 1
    '    Else
    '        NumericUpDown9.Value = (MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4
    '        NumericUpDown10.Value = (MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4
    '    End If
    '    Panel11.Refresh()
    'End Sub
    Private Sub Panel11_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel11.Paint
            e.Graphics.DrawImage(ts1bmp, New Point(0, 0))
            If NumericUpDown8.Value <> &HFFFFS Then
                e.Graphics.DrawImage(ts2bmp, New Point(0, 256))
            End If
            e.Graphics.DrawRectangle(New Pen(Brushes.Red, 2), New Rectangle(NumericUpDown9.Value << 4, NumericUpDown10.Value << 4, NumericUpDown28.Value << 4, NumericUpDown29.Value << 4))
    End Sub
End Class