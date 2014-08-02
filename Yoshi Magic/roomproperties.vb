'Imports System.ComponentModel

Public Class roomproperties
    'Dim ms = 0
    'mapdaddr
    'Room Properties
    Dim rpbank As Integer = &H83A78D4
    Dim rooms As Short = 511 + 8 + 9
    Dim rnind(rooms) As Byte
    Dim mlind(rooms) As Byte
    Dim underwaterflag(rooms) As Byte '+2Flag byte
    Dim ctind(rooms) As Byte
    Dim tsind(rooms) As Byte '+4 tileset
    Dim palind(rooms) As Byte
    Dim solidind(rooms) As Byte
    Dim aniind(rooms) As Byte
    Dim unk1ind(rooms) As Byte
    Dim laybind(rooms) As Byte
    Dim tmodsind(rooms) As Byte
    Dim unk2ind(rooms) As Byte
    Dim ls1ind(rooms) As Byte
    Dim ls2ind(rooms) As Byte
    Dim mapscrind(rooms) As Short
    Dim npcind(rooms) As Short
    Dim ls3ind(rooms) As Byte
    Dim ls4ind(rooms) As Byte
    Dim songind(rooms) As Byte
    Dim unk3ind(rooms) As Byte
    Dim itmbkind(rooms) As Short
    'Room Names
    Dim rndpointer = &H83C03E8
    Dim rnames As Short = 100
    Dim namepointer(rnames) As Integer
    '    Dim Nametxt(rnames, 40) As Byte
    Dim RoomName(rnames) As String
    '    Dim Nametxt2(rnames, 40) As Byte
    '    Dim RoomName2(rnames) As String
    '    Dim rn2offset(rnames) As Integer
    'Map Locations
    Dim locdbase As Integer = &H83BADD4
    Dim locs As Short = 53
    Dim locx(locs) As Byte
    Dim locy(locs) As Byte
    Dim loc1(locs) As Short
    'Compressed Tiling (Groups)
    Dim ctdbase As Integer = &H83AAA6C
    Dim cts As Short = 119
    Dim ct1(cts), ct2(cts), ct3(cts) As Byte
    'Compressed Tiling (Pixels)
    Dim ctdbase2 As Integer = &H86527F4
    Dim cts2 As Short = 161 'A0
    'Dim ctp1(cts2) As Integer
    Dim palbmp As New Bitmap(16 * 8, 16 * 8, Imaging.PixelFormat.Format16bppRgb555) 'With {.imaging.PixelFormat}
    Dim b(255) As Color 'Palette Colors
    Dim data2(&H5FFF) As Byte 'uncompressed '1fff
    Dim offset1 As Integer '= &H6527F4 'Compressed Tiles Database
    Dim pic1bmp As New Bitmap(16 * 8 * 2, 16 * 4) ' 16 * 8)
    Dim pic2bmp As New Bitmap(16 * 8 * 2, 16 * 4)
    Dim pic3bmp As New Bitmap(16 * 8 * 2, 16 * 4)
    Dim num As Integer = 0 'for offset in data2
    'Palette Related
    Dim palgrps As Short = 103
    Dim palind2(palgrps) As Byte
    'Tilesets (Groups)
    Dim tsgrps As Short = 70
    Dim ts1(tsgrps), ts2(tsgrps) As Short
    Dim ts1bmp As New Bitmap(512, 256) '(256, 128)
    Dim ts2bmp As New Bitmap(512, 256) '(256, 128)
    '083AAD68
    'Tilesets (Data)
    Dim tilesets As Short = 84
    Dim offset2 As Integer
    'Map 'Dim layer1bmp As New Bitmap(4000, 4000)
    Dim offset3 As Integer
    Dim layer1bmp As Bitmap '(100 * 480, 6 * 320)
    Dim layer2bmp As Bitmap '(100 * 480, 6 * 320)
    Dim layer3bmp As Bitmap '(100 * 480, 6 * 320)
    Dim layer1bmp2 As Bitmap
    Dim layer2bmp2 As Bitmap
    Dim layer3bmp2 As Bitmap

    Dim maptabclick As Boolean = 0

    'WARPS!
    Dim warppointer As Integer
    Dim swrps As Byte = 20
    Dim x1(swrps), x2(swrps), y1(swrps), y2(swrps) As Byte
    Dim warps As Short
    Dim awx1, awx2, awy1, awy2 As Byte
    Dim ucwarps As UserControl1
    Dim warpflags As Integer
    Public warpdataint1(swrps), warpdataint2(swrps) As Integer

    'Item blocks!
    Dim itmblkpointer As Integer
    Dim sitmblks As Byte = 20
    Dim xib(sitmblks), yib(sitmblks), zib(sitmblks) As Byte
    Dim itmblks As Short
    'Dim awx1, awx2, awy1, awy2 As Byte
    Dim uciblks As UserControl1
    'Dim warpflags As Integer
    'Public warpdataint1(swrps), warpdataint2(swrps) As Integer

    Private Sub roomproperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ucwarps = New UserControl1
        ucwarps.Left = 535
        ucwarps.Top = 80
        ucwarps.Anchor = 9
        Me.TabPage10.Controls.Add(ucwarps)
        uciblks = New UserControl1
        uciblks.Left = 535
        uciblks.Top = 80
        uciblks.Anchor = 9
        Me.TabPage10.Controls.Add(uciblks)
        'Application.scre
        ComboBox5.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        'NumericUpDown1.Value
        'Dim secs = TimeOfDay.Ticks
        For a = 0 To 99 'rooms
            FileGet(1, namepointer(a), rndpointer - &H8000000 + (a << 2) + 1)
            If namepointer(a) <> 0 Then
                FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
                FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
                Seek(1, namepointer(a) - &H8000000 + 1)
                Dim letter As Byte
                Do 'For n = 0 To characters
                    FileGet(1, letter)
                    Select Case letter
                        Case &HFF
                            FileGet(1, letter)
                            RoomName(a) += "[" & Hex(letter)
                            Select Case letter
                                Case &H1, &HB To &H11
                                    FileGet(1, letter)
                                    RoomName(a) += "=" & Hex(letter)
                            End Select
                            RoomName(a) += "]"
                        Case Is <= &H1F '&H0 To &H1F
                            RoomName(a) += "{" & Hex(letter) & "}"
                            If letter = 0 Then Exit Do 'For
                        Case &H5C '\ (Backslash)
                            RoomName(a) += "\\" 'Add another backslash since first has been made a special char for reading { and [.
                        Case &H5B
                            RoomName(a) += "\["
                        Case &H7B
                            RoomName(a) += "\{"
                        Case Else
                            RoomName(a) += Chr(letter)
                    End Select
                Loop 'Next 'n
            End If
            ListBox2.Items.Add(Hex(a) & " - " & RoomName(a)) '& " : " & RoomName2(a)) 'Adds the list of enemies to List Box.
            ComboBox1.Items.Add(ListBox2.Items.Item(a))
        Next
        '//----- Map Locations
        For a = 0 To locs
            FileGet(1, locx(a), locdbase - &H8000000 + (a << 2) + 1)
            FileGet(1, locy(a)) ', locdbase - &H8000000 + (&H4 * a) + 1 + 1)
            FileGet(1, loc1(a)) ', locdbase - &H8000000 + (&H4 * a) + 2 + 1)
            ListBox3.Items.Add(Hex(a) & " - (" & locx(a) & ", " & locy(a) & ") - " & RoomName(loc1(a)))
            ComboBox2.Items.Add(ListBox3.Items.Item(a))
        Next
        '//----- Compressed Image Groups
        For a = 0 To cts
            FileGet(1, ct1(a), ctdbase - &H8000000 + (a << 2) + 1)
            FileGet(1, ct2(a)) ', ctdbase - &H8000000 + (&H4 * a) + 1 + 1)
            FileGet(1, ct3(a)) ', ctdbase - &H8000000 + (&H4 * a) + 2 + 1)
            Dim ctlist As String = ""
            If ct1(a) <> &HFF Then ctlist += "  T1:" & Hex(ct1(a))
            If ct2(a) <> &HFF Then ctlist += "  T2:" & Hex(ct2(a))
            If ct3(a) <> &HFF Then ctlist += "  T3:" & Hex(ct3(a))
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
        Seek(1, &H3AAC4C + 1)
        For a = 0 To tsgrps
            FileGet(1, ts1(a)) ', &H3AAC4C + (a << 2) + 1)
            FileGet(1, ts2(a)) ', &H3AAC4C + (a << 2) + 2 + 1)
            ListBox6.Items.Add(Hex(a))
            ComboBox4.Items.Add(Hex(a))
        Next
        Seek(1, &H3AAD68 + 1)
        For a = 0 To palgrps
            FileGet(1, palind2(a)) ', &H3AAD68 + a + 1)
            ListBox5.Items.Add(Hex(a))
        Next
        '//----- GET Room Properties Data
        For a = 0 To rooms
            FileGet(1, rnind(a), rpbank - &H8000000 + (&H18 * a) + 1) 'Room Names Index
            FileGet(1, mlind(a)) ', rpbank - &H8000000 + (&H18 * a) + 1 + 1) 'Mario/Luigi
            FileGet(1, underwaterflag(a)) 'flag
            FileGet(1, ctind(a)) ', rpbank - &H8000000 + (&H18 * a) + 3 + 1) 'Comp. Tiles
            FileGet(1, tsind(a)) ', rpbank - &H8000000 + (&H18 * a) + 4 + 1) 'tileset
            FileGet(1, palind(a)) ', rpbank - &H8000000 + (&H18 * a) + 5 + 1) 'Palette Index

            FileGet(1, solidind(a))
            FileGet(1, aniind(a))
            FileGet(1, unk1ind(a))
            FileGet(1, laybind(a))
            FileGet(1, tmodsind(a))
            FileGet(1, unk2ind(a))
            FileGet(1, ls1ind(a))
            FileGet(1, ls2ind(a))
            FileGet(1, mapscrind(a))
            FileGet(1, npcind(a))
            FileGet(1, ls3ind(a))
            FileGet(1, ls4ind(a))
            FileGet(1, songind(a))
            FileGet(1, unk3ind(a))
            FileGet(1, itmbkind(a))

            'ListBox1.Items.Add(Hex(a) & " - " & ListBox2.Items.Item(rnind(a)))
            'List Rooms
            ListBox1.Items.Add(Hex(a) & " - " & RoomName(rnind(a))) '& " : " & RoomName2(rnind(a)))
        Next
        ListBox9.Items.Add("Regular Map")
        ListBox9.Items.Add("Full Map")
        ListBox2.SelectedIndex = 0
        ListBox3.SelectedIndex = 0
        ListBox9.SelectedIndex = 0
        ListBox1.SelectedIndex = 0
        'MsgBox(TimeOfDay.Ticks - secs)
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim a = ListBox2.SelectedIndex
        TextBox1.Text = RoomName(a)
        'TextBox2.Text = RoomName2(a)
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        ''FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'Dim songbyte As Byte
        'FileGet(1, songbyte, rpbank - &H8000000 + (&H18 * a) + &H14 + 1)
        'NumericUpDown25.Value = songbyte
        ''FileClose(1)
        'Exit Sub
        ComboBox1.SelectedIndex = rnind(a) 'Room Names
        ComboBox2.SelectedIndex = mlind(a) 'Mario/Luigi
        CheckBox1.Checked = underwaterflag(a) >> 4 And 1 'flag
        ComboBox3.SelectedIndex = ctind(a) 'Comp. Tiles
        ListBox4.SelectedIndex = ctind(a)
        NumericUpDown6.Value = palind(a)
        ListBox5.SelectedIndex = palind(a)
        ComboBox4.SelectedIndex = tsind(a) 'tilesets
        ListBox6.SelectedIndex = tsind(a)

        NumericUpDown13.Value = solidind(a)
        NumericUpDown14.Value = aniind(a)
        NumericUpDown15.Value = unk1ind(a)
        NumericUpDown16.Value = laybind(a)
        NumericUpDown17.Value = tmodsind(a)
        NumericUpDown18.Value = unk2ind(a)
        NumericUpDown19.Value = ls1ind(a)
        NumericUpDown20.Value = ls2ind(a)
        NumericUpDown21.Value = mapscrind(a)
        NumericUpDown22.Value = npcind(a)
        NumericUpDown23.Value = ls3ind(a)
        NumericUpDown24.Value = ls4ind(a)
        NumericUpDown25.Value = songind(a)
        NumericUpDown26.Value = unk3ind(a)
        NumericUpDown27.Value = itmbkind(a)

        ToolStripStatusLabel2.Text = "Room Props. Database: " & Hex(rpbank) & "  Room Props. Offset: " & Hex(rpbank - &H8000000 + (&H18 * a))
        'ToolStripStatusLabel1.Text = rndpointer(-&H8000000 + (&H4 * a))
        'layer1()
        'layer1(Panel4, layer1bmp, 0)
        'layer1(Panel6, layer2bmp, 1)
        'layer1(Panel7, layer3bmp, 2)
        'layer2()
        'layer3()
        'Panel5.BackColor = b(0)
    End Sub
#Region " Map Locations TAB "
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Dim a = ListBox3.SelectedIndex
        TextBox3.Text = locx(a)
        TextBox4.Text = locy(a)
        TextBox5.Text = loc1(a)

        Panel2.Left = TextBox3.Text - 16
        Panel2.Top = TextBox4.Text - 16
        Try
            HScrollBar1.Value = Panel2.Left + 12
            HScrollBar2.Value = HScrollBar1.Value
            VScrollBar1.Value = Panel2.Top + 4
            VScrollBar2.Value = VScrollBar1.Value
        Catch
        End Try
        'Panel2.Left = locx(a) - 16
        'Panel2.Top = locy(a) - 16
        If a = 0 Then Label2.Visible = True Else Label2.Visible = False
    End Sub
    Private Sub ListBox9_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox9.SelectedIndexChanged
        Panel1.BackgroundImage = ImageList1.Images.Item(ListBox9.SelectedIndex)
    End Sub
    Private Sub TextBox3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.LostFocus
        'Dim a = ListBox3.SelectedIndex
        'If TextBox3.Text = locx(a) Then

        'Else
        '    MsgBox("Yoshi won't be able to save that value.")
        '    TextBox3.Text = locx(a)
        '    TextBox3.Focus()
        '    Exit Sub
        '    'Else
        'End If
        'TextBox3.Text = locx(a) ' To remove zeroes. (Ex: 0000066)

        'TextBox3.Text.
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Dim a = ListBox3.SelectedIndex
        Try
            locx(a) = TextBox3.Text
        Catch ex As Exception
            'If TextBox3.Text = "" Then
            '    locx(a) = 0
            'Else
            '    TextBox3.Text = locx(a)

            'End If
        End Try
        Try
            Panel2.Left = TextBox3.Text - 16
        Catch
        End Try
        Try
            HScrollBar1.Value = TextBox3.Text - 12
        Catch ex As Exception 'If value is out of range...
            Try
                If TextBox3.Text < 12 Then
                    HScrollBar1.Value = 0
                Else
                    HScrollBar1.Value = 240
                End If
            Catch ex2 As Exception 'If not a value, but text...
            End Try
        End Try
        HScrollBar2.Value = HScrollBar1.Value
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Dim a = ListBox3.SelectedIndex
        Try
            locy(a) = TextBox4.Text
        Catch ex As Exception
        End Try
        Try
            Panel2.Top = TextBox4.Text - 16
        Catch
        End Try
        Try
            VScrollBar1.Value = TextBox4.Text - 12
        Catch ex As Exception 'If value is out of range...
            Try
                If TextBox3.Text < 4 Then
                    VScrollBar1.Value = 0
                Else
                    VScrollBar1.Value = 160
                End If
            Catch 'If not a value, but text...
            End Try
        End Try
        VScrollBar2.Value = VScrollBar1.Value
    End Sub
    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        HScrollBar2.Value = HScrollBar1.Value
        Panel2.Left = HScrollBar1.Value - 12
        TextBox3.Text = Panel2.Left + 16
    End Sub
    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar1.Scroll
        VScrollBar2.Value = VScrollBar1.Value
        Panel2.Top = VScrollBar1.Value - 4
        TextBox4.Text = Panel2.Top + 16
    End Sub
    Private Sub VScrollBar2_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar2.Scroll
        VScrollBar1.Value = VScrollBar2.Value
        Panel2.Top = VScrollBar2.Value - 4
        TextBox4.Text = Panel2.Top + 16
    End Sub
    Private Sub HScrollBar2_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar2.Scroll
        HScrollBar1.Value = HScrollBar2.Value
        Panel2.Left = HScrollBar2.Value - 12
        TextBox3.Text = Panel2.Left + 16
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Dim a = ListBox3.SelectedIndex
        Try
            loc1(a) = TextBox5.Text
        Catch ex As Exception
        End Try
    End Sub
#End Region
    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        '//----- PUT Room Properties Data
        For a = 0 To rooms
            Seek(1, rpbank - &H8000000 + (&H18 * a) + 1)
            FilePut(1, rnind(a)) ' rpbank - &H8000000 + (&H18 * a) + 0 + 1) 'Room Names Index
            FilePut(1, mlind(a)) ', rpbank - &H8000000 + (&H18 * a) + 1 + 1) 'Mario/Luigi
            FilePut(1, underwaterflag(a)) 'flag
            FilePut(1, ctind(a)) ', rpbank - &H8000000 + (&H18 * a) + 3 + 1) 'Comp. Tiles
            FilePut(1, tsind(a)) ', rpbank - &H8000000 + (&H18 * a) + 4 + 1) 'tileset
            FilePut(1, palind(a)) ', rpbank - &H8000000 + (&H18 * a) + 5 + 1) 'Palette Index

            FilePut(1, solidind(a))
            FilePut(1, aniind(a))
            FilePut(1, unk1ind(a))
            FilePut(1, laybind(a))
            FilePut(1, tmodsind(a))
            FilePut(1, unk2ind(a))
            FilePut(1, ls1ind(a))
            FilePut(1, ls2ind(a))
            FilePut(1, mapscrind(a))
            FilePut(1, npcind(a))
            FilePut(1, ls3ind(a))
            FilePut(1, ls4ind(a))
            FilePut(1, songind(a))
            FilePut(1, unk3ind(a))
            FilePut(1, itmbkind(a))
            'ListBox1.Items.Add(Hex(a) & " - " & ListBox2.Items.Item(rnind(a)))
            'List Rooms
            'ListBox1.Items.Add(Hex(a) & " -" & RoomName(rnind(a)) & " : " & RoomName2(rnind(a)))
        Next
        Dim locdbase = &H83BADD4
        'Dim locs = 53
        'Dim locx(locs) As Byte
        'Dim locy(locs) As Byte
        'Dim loc1(locs) As Short
        'Save Map Locations
        For a = 0 To ListBox3.Items.Count - 1 'locs
            FilePut(1, locx(a), locdbase - &H8000000 + (a << 2) + 1)
            FilePut(1, locy(a)) ', locdbase - &H8000000 + (&H4 * a) + 1 + 1)
            FilePut(1, loc1(a)) ', locdbase - &H8000000 + (&H4 * a) + 2 + 1)
            'ListBox3.Items.Add(Hex(a) & " - X:" & locx(a) & " Y:" & locy(a) & " ???:" & loc1(a))
            'ComboBox2.Items.Add(ListBox3.Items.Item(a))
        Next
        MsgBox("Saved!")
    End Sub
    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        'ON COMPRESSED IMAGES LISTBOX SELECT (UPDATE COMPRESSED IMAGES NUMBERICS)
        Dim a = ListBox4.SelectedIndex
        NumericUpDown2.Value = ct1(a)
        NumericUpDown3.Value = ct2(a)
        NumericUpDown4.Value = ct3(a)
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        'If Focused = 0 Then MsgBox("TEST")
        'ON PALETTE SET CHANGE (GET PALETTE DATA > TURN TO COLORS > DISPLAY IN PALETTE BOX > UPDATE COMPRESSED IMAGES > UPDATE TILESETS)
        Dim palette1 As Short
        'USE BELOW WHEN SAVING IS APPROACHED (and make 2-dim?) Also, move outside SUB.
        'Dim palette1(255) As Short
        'Dim b(255) As Color ' = Color.FromArgb(0, 0, 0) 'Red
        'PALETTE SECTION
        'Get Palette #
        Dim nval As Integer = NumericUpDown1.Value
        Seek(1, &H8C88C8 + &H1E0 * nval + 1)
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
        PictureBox6.Image = palbmp
        'Me.TabPage4.
        'CreateGraphics.DrawImageUnscaled(palbmp, New Point(0, 0)) 'PictureBox6.Left, PictureBox6.Top))
        'CreateGraphics.c()
        image3(False) 'Updates the 3 "compressed" images
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        'layer1(Panel4, layer1bmp, 0) 'Updates the map.
        'layer1(Panel6, layer2bmp, 1)
        'layer1(Panel7, layer3bmp, 2)
    End Sub
    Sub image3(ByVal decompress As Boolean)
        'UPDATE COMPRESSED IMAGES (All-in-One)
        cimage(NumericUpDown2, pic1bmp, PictureBox1, 0) ', decompress)
        cimage(NumericUpDown3, pic2bmp, PictureBox2, 1) ', decompress)
        cimage(NumericUpDown4, pic3bmp, PictureBox3, 2) ', decompress)
    End Sub
    Sub cimage(ByVal nud As NumericUpDown, ByVal bm As Bitmap, ByVal pb As PictureBox, ByVal n As Byte) ', ByVal decompress As Boolean)
        'UPDATE A COMPRESSED IMAGE (Decompress if needed > Display Image)
        'bm = New Bitmap(16 * 8 * 2, 16 * 4) '(20, 23)
        Dim numud2val As Byte = nud.Value 'NumericUpDown2.Value
        If numud2val <> &HFF Then
            'If decompress = True Then
            'Dim offset1 As Integer
            FileGet(1, offset1, &H6527F4 + (CInt(numud2val) << 2) + 1) 'Maps
            'FileGet(1, offset1, &HA57994 + (CInt(numud2val) << 2) + 1) 'Suitcase
            'FileGet(1, offset1, &H9F808C + (CInt(numud2val) << 2) + 1) 'Battle Menus
            'FileGet(1, offset1, &H9FC058 + (CInt(numud2val) << 2) + 1) 'Battle BGs
            'offset1 = &H6527F4 + offset1
            Try
                Seek(1, &H6527F4 + offset1 + 1)
                'Seek(1, &HA57994 + offset1 + 1)
                'Seek(1, &H9F808C + offset1 + 1)
                'Seek(1, &H9FC058 + offset1 + 1)
                'Array.Clear(data2, 0, &H6000)
                num = &H2000 * n 'Offset to put data in variable data2 for sub decomp.
                decomp()
                nud.BackColor = Color.White
            Catch
                nud.BackColor = Color.Red
            End Try
            'End If
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
        'If NumericUpDown4.Focused = 0 Then Exit Sub
        cimage(NumericUpDown2, pic1bmp, PictureBox1, 0) ', True)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
    End Sub
    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        'If NumericUpDown4.Focused = 0 Then Exit Sub
        cimage(NumericUpDown3, pic2bmp, PictureBox2, 1) ', True)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
    End Sub
    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        'If NumericUpDown4.Focused = 0 Then Exit Sub
        cimage(NumericUpDown4, pic3bmp, PictureBox3, 2) ', True)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
    End Sub
    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        'Update 16 colors of images (Palette Index)
        image3(False)
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
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp)
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
        'Dim ts2num As Short = nud.Value
        FileGet(1, offset2, &H6FFC20 + (nud.Value << 2) + 1)
        'offset2 = &H6FFC20 + offset2
        Seek(1, &H6FFC20 + offset2 + 1)
        pb.Image = bm
        Dim tile As UShort 'Integer 'Short
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
    End Sub
    Private Sub NumericUpDown8_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown8.ValueChanged
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
    End Sub
    ''' <summary>
    ''' Sets a map layer to a Panel.
    ''' </summary>
    ''' <param name="a">Panel to show layer on.</param>
    ''' <param name="lay">Map layer index number (0, 1, or 2)</param>
    ''' <remarks>REMARKS!</remarks>
    Public Sub layer1(ByVal a As Panel, ByVal bm As Bitmap, ByVal lay As Integer)
        'Dim roomnum As Short = ListBox1.SelectedIndex
        Dim layer1ind As Short
        FileGet(1, layer1ind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + (lay << 1) + 1)
        If layer1ind = -1 Then a.BackgroundImage = Nothing : Exit Sub
        FileGet(1, offset3, &H754D74 + (layer1ind << 2) + 1) ' &H7550BC + 1)
        'offset3 = &H754D74 + offset3
        'Seek(1, &H754D74 + offset3 + 1)
        Dim scnw As Byte
        FileGet(1, scnw, &H754D74 + offset3 + 1)
        'FileGet(1, scnw, offset3 + 1)
        'offset3 += 1
        Dim scnh As Byte
        FileGet(1, scnh)
        'FileGet(1, scnh, offset3 + 1)
        'offset3 += 1
        offset3 = Loc(1)
        a.Width = scnw * 240
        a.Height = scnh * 160
        'Dim bm As New Bitmap(scnw * 15 * 16 * 2, scnh * 10 * 16 * 2)
        'bm = New Bitmap(scnw * 15 * 16 * 2, scnh * 10 * 16 * 2)
        bm = New Bitmap(scnw * 480, scnh * 320, Imaging.PixelFormat.Format16bppArgb1555)
        'Panel4.BackColor.
        'CreateGraphic()
        'Dim maptile As Short
        Dim tile As UShort
        Dim stile As Short
        Dim stem As Byte
        Dim leaf As Byte
        Dim pix As Byte
        For row = 0 To scnh * 10 - 1 '15 '7 '15
            For col = 0 To (((scnw * 15 + 3) And &HFC) >> 2) - 1 ' Math.Ceiling((scnw * 15 / 4)) - 1 '31 '15
                FileGet(1, stem, offset3 + 1)
                For stemleaf = 0 To 3
                    pix = stem >> (Math.Abs(stemleaf - 3) << 1)
                    'pix = (stem And &HC0) >> 6
                    'stem = stem << 2
                    Select Case pix And 2
                        Case 0
                            FileGet(1, offset2, &H6FFC20 + (NumericUpDown7.Value << 2) + 1)
                        Case 2
                            If NumericUpDown8.Value <> -1 Then FileGet(1, offset2, &H6FFC20 + (NumericUpDown8.Value << 2) + 1)
                    End Select
                    FileGet(1, leaf, offset3 + stemleaf + 1 + 1)
                    'maptile = ((CShort(stem) << 8) + leaf)
                    'Seek(1, &H6FFC20 + offset2 + (maptile << 3) + 1)
                    Seek(1, &H6FFC20 + offset2 + (((CShort(pix And 1) << 8) + leaf) << 3) + 1)
                    For r = 0 To 1
                        For c = 0 To 1
                            FileGet(1, stile) ', offset2 + (maptile << 3) + (r << 2) + (c << 1) + 1)
                            tile = stile And &HFFFF ' Avoiding SIGNED.
                            'Dim d2tile As Short = (tile And &H3FF) << 5
                            'Dim xtile As Integer = (((col << 2) + stemleaf) << 4) + (c << 3)
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
                                    'pix = data2(d2tile) And &HF
                                    If pix = 0 Then
                                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                        '                 Color.Transparent)
                                    Else
                                        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
                                                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                        b(((tile >> 12) << 4) + pix))
                                    End If
                                    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> 4
                                    'pix = data2(d2tile) >> 4
                                    If pix = 0 Then
                                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                        '                 Color.Transparent)
                                    Else
                                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                        '                b(((tile >> 12) << 4) + pix))
                                        bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
                                                        (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
                                                        b(((tile >> 12) << 4) + pix))
                                    End If
                                    'd2tile += 1 '                                    End If
                                Next
                            Next

                        Next
                    Next
                Next
                offset3 += 5
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
    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If Panel7.BackgroundImage IsNot Nothing Then layer3bmp2 = Panel7.BackgroundImage
        If CheckBox17.Checked Then Panel7.BackgroundImage = layer3bmp2 Else Panel7.BackgroundImage = Nothing
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 4 Then
            If ListBox1.SelectedIndex = -1 Then Exit Sub
            'maptabclick = 1
            'Dim seconds = TimeOfDay.Ticks
            'Panel5.BackColor = b(0)
            'TabPage10.BackColor = b(0)
            TabPage10.Text = "Loading..."
            Dim atime As Date = Now
            TabControl1.Update()
            Panel4.Hide()
            'Panel6.Hide()
            'Panel7.Hide()
            layer1(Panel4, layer1bmp, 0) 'Updates the map.
            layer1(Panel6, layer2bmp, 1)
            layer1(Panel7, layer3bmp, 2)

            layer1bmp2 = Panel4.BackgroundImage
            layer2bmp2 = Panel6.BackgroundImage
            layer3bmp2 = Panel7.BackgroundImage

            Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
                Case 1
                    Panel4.BackgroundImage = layer2bmp2
                    Panel6.BackgroundImage = layer1bmp2
                    Panel7.BackgroundImage = layer3bmp2
                    'layer1bmp = Panel4.BackgroundImage
                    'layer2bmp = Panel6.BackgroundImage
                    'layer3bmp = Panel7.BackgroundImage
            End Select
            Panel4.Show()
            Label34.Text = "Load Time: " & (Now - atime).ToString
            TabPage10.Text = "Map Editor"
            'Panel6.Show()
            'Panel7.show()
            'PictureBox7.Image = ts1bmp
            'PictureBox8.Image = ts2bmp
            'If NumericUpDown8.Value = &HFFFFS Then PictureBox8.Image = Nothing
            'MsgBox(seconds & " to " & TimeOfDay.Ticks & " = " & (TimeOfDay.Ticks - seconds) / 10000000 & " seconds.")
            'maptabclick = 0

            'WARPS!
            'Dim warppointer As Integer
            'Dim swrps As Byte = 20
            'Dim x1(swrps), x2(swrps), y1(swrps), y2(swrps) As Byte
            FileGet(1, warppointer, &H3AF418 + (ListBox1.SelectedIndex << 2) + 1)
            Dim wls As Short = 0
            'Dim num = 0
            warps = -1
            Do Until wls = 2
                warps += 1
                FileGet(1, wls, warppointer - &H8000000 + &HA + (&HC * warps) + 1)
                wls = wls And 2
                'num += 1
                'MsgBox("wls - " & wls)
            Loop
            For w = 0 To warps 'cwarps '(a) '10 'warppointer(a) '10
                'MsgBox(a & " " & b)
                FileGet(1, x1(w), warppointer - &H8000000 + (&HC * w) + 1)
                FileGet(1, y1(w)) ', warppointer - &H8000000 + &H1 + (&HC * w) + 1)
                FileGet(1, x2(w)) ', warppointer - &H8000000 + &H2 + (&HC * w) + 1)
                FileGet(1, y2(w)) ', warppointer - &H8000000 + &H3 + (&HC * w) + 1)
                'FileGet(1, warpto(a, b), warppointer(a) - &H8000000 + &H4 + (&HC * b) + 1)

                'FileGet(1, unk(a, b), warppointer(a) - &H8000000 + &H6 + (&HC * b) + 1)
                'FileGet(1, x(a, b), warppointer(a) - &H8000000 + &H7 + (&HC * b) + 1)
                'FileGet(1, y(a, b), warppointer(a) - &H8000000 + &H8 + (&HC * b) + 1)
                'FileGet(1, xy(a, b), warppointer(a) - &H8000000 + &H9 + (&HC * b) + 1)
                FileGet(1, warpdataint1(w))
                FileGet(1, warpdataint2(w))
            Next
            CheckBox2.Text = "Show Warps (" & Hex(warppointer) & ")"
            ucwarps.displaywarpdata()


            FileGet(1, itmblkpointer, &H51FA00 + (ListBox1.SelectedIndex << 2) + 1)
            FileGet(1, itmblks, itmblkpointer - &H8000000 + 1)
            If itmblks > 0 Then
                Dim rpntr As Short
                FileGet(1, rpntr, Loc(1) + 2)
                Seek(1, itmblkpointer - rpntr)

                ' MsgBox(rpntr)
                ' End
            End If
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
        Dim layer1ind As Short
        Dim lay As Byte = 0 'Layer # (0-2)
        'layer1bmp2 = Panel4.BackgroundImage
        'layer2bmp2 = Panel6.BackgroundImage
        'layer3bmp2 = Panel7.BackgroundImage
        If RadioButton1.Checked Then lay = 0
        If RadioButton2.Checked Then lay = 1
        If RadioButton3.Checked Then lay = 2
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 1
                'Panel4.BackgroundImage = layer2bmp2
                'Panel6.BackgroundImage = layer1bmp2
                'Panel7.BackgroundImage = layer3bmp2
                If RadioButton1.Checked Then lay = 1
                If RadioButton2.Checked Then lay = 0
                If RadioButton3.Checked Then lay = 2
        End Select

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

                'Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
                '    Case 0
                '        If lay = 0 Then updtile(layer1bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                '        If lay = 1 Then updtile(layer2bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                '        If lay = 2 Then updtile(layer3bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                '    Case 1
                '        If lay = 1 Then updtile(layer2bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                '        If lay = 0 Then updtile(layer1bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                '        If lay = 2 Then updtile(layer3bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h, (((NumericUpDown10.Value + h) And &H1F) << 5) Or ((NumericUpDown9.Value + w) And &H1F))
                'End Select                '
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
        'layer1(Panel4, layer1bmp, 0)
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 0
                If lay = 0 Then layer1(Panel4, layer1bmp, 0) 'Updates the map.
                If lay = 1 Then layer1(Panel6, layer2bmp, 1)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
            Case 1
                If lay = 1 Then layer1(Panel4, layer2bmp, 1) 'Updates the map.
                If lay = 0 Then layer1(Panel6, layer1bmp, 0)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
        End Select
        Exit Sub
cleanup:
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
        disimg(pic1bmp, 0)
        tilesetnum(NumericUpDown7, PictureBox4, ts1bmp) 'Updates the tileset images
        tilesetnum(NumericUpDown8, PictureBox5, ts2bmp)
        PictureBox1.Image = pic1bmp
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
    'Dim etsbloc As Integer
    Private Sub Panel7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel7.Click
        MapLayer_Click(Panel7)
    End Sub
    Sub fillsection1x1(ByVal twidth As Integer, ByVal theight As Integer)
        If twidth < 0 Then Exit Sub
        If theight < 0 Then Exit Sub
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
        Dim layer1ind As Short
        Dim lay As Byte = 0 'Layer # (0-2)
        'layer1bmp2 = Panel4.BackgroundImage
        'layer2bmp2 = Panel6.BackgroundImage
        'layer3bmp2 = Panel7.BackgroundImage
        If RadioButton1.Checked Then lay = 0
        If RadioButton2.Checked Then lay = 1
        If RadioButton3.Checked Then lay = 2
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 1
                'Panel4.BackgroundImage = layer2bmp2
                'Panel6.BackgroundImage = layer1bmp2
                'Panel7.BackgroundImage = layer3bmp2
                If RadioButton1.Checked Then lay = 1
                If RadioButton2.Checked Then lay = 0
                If RadioButton3.Checked Then lay = 2
        End Select

        For h = 0 To theight 'NumericUpDown29.Value - 1
            For w = 0 To twidth 'NumericUpDown28.Value - 1

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
                        ((((NumericUpDown10.Value) >> 3) And &H3) << (Math.Abs(((NumericUpDown11.Value + w) And &H3) - 3) << 1))
                'stem = (stem >> (Math.Abs(((NumericUpDown10.Value >> 3) And &H3) - 3) << 1)) And 3
                FilePut(1, stem, Loc(1))
                Dim leaf As Byte
                Seek(1, Loc(1) + 1 + ((NumericUpDown11.Value + w) And &H3))
                leaf = (((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
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
        'layer1(Panel4, layer1bmp, 0)
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 0
                If lay = 0 Then layer1(Panel4, layer1bmp, 0) 'Updates the map.
                If lay = 1 Then layer1(Panel6, layer2bmp, 1)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
            Case 1
                If lay = 1 Then layer1(Panel4, layer2bmp, 1) 'Updates the map.
                If lay = 0 Then layer1(Panel6, layer1bmp, 0)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
        End Select
        Exit Sub
cleanup:  'updtile(layer1bmp, NumericUpDown11.Value + w, NumericUpDown12.Value + h)
        '(((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
    End Sub
    'Sub updtile(ByVal bm As Bitmap, ByVal mapx As Integer, ByVal mapy As Integer, ByVal tilenumber As Integer)
    '    Dim tile As UShort
    '    Dim stile As Short
    '    'Dim stem As Byte
    '    'Dim leaf As Byte
    '    Dim pix As Byte
    '    Select Case tilenumber >> 9
    '        Case 0
    '            FileGet(1, offset2, &H6FFC20 + (NumericUpDown7.Value << 2) + 1)
    '        Case 1
    '            If NumericUpDown8.Value <> -1 Then FileGet(1, offset2, &H6FFC20 + (NumericUpDown8.Value << 2) + 1)
    '    End Select
    '    Seek(1, &H6FFC20 + offset2 + ((tilenumber And &H1FF) << 3) + 1)
    '    '(((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
    '    For r = 0 To 1
    '        For c = 0 To 1
    '            FileGet(1, stile) ', offset2 + (maptile << 3) + (r << 2) + (c << 1) + 1)
    '            tile = stile And &HFFFF ' Avoiding SIGNED.
    '            'Dim d2tile As Short = (tile And &H3FF) << 5
    '            'Dim xtile As Integer = (((col << 2) + stemleaf) << 4) + (c << 3)
    '            For ypix = 0 To 7
    '                For xpix = 0 To 3 '16*8
    '                    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) And &HF
    '                    'pix = data2(d2tile) And &HF
    '                    If pix = 0 Then
    '                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
    '                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                        '                 Color.Transparent)
    '                    Else
    '                        bm.SetPixel((mapx << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1)), _
    '                                        (mapy << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                        b(((tile >> 12) << 4) + pix))
    '                    End If
    '                    pix = data2(((tile And &H3FF) << 5) + (ypix << 2) + xpix) >> 4
    '                    'pix = data2(d2tile) >> 4
    '                    If pix = 0 Then
    '                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
    '                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                        '                 Color.Transparent)
    '                    Else
    '                        'bm.SetPixel((((col << 2) + stemleaf) << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
    '                        '                (row << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                        '                b(((tile >> 12) << 4) + pix))
    '                        bm.SetPixel((mapx << 4) + (c << 3) + Math.Abs((-7 * (tile >> 10 And 1)) + (xpix << 1) + 1), _
    '                                        (mapy << 4) + (r << 3) + Math.Abs((-7 * (tile >> 11 And 1)) + ypix), _
    '                                        b(((tile >> 12) << 4) + pix))
    '                    End If
    '                    'd2tile += 1 '                                    End If
    '                Next
    '            Next

    '        Next
    '    Next
    'End Sub
    Private Sub Panel7_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.MouseDown
        NumericUpDown11.Value = (MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown12.Value = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 4
       Select ComboBox6.SelectedIndex
            Case 2
                awx1 = x1(ucwarps.NumericUpDown1.Value)
                awx2 = x2(ucwarps.NumericUpDown1.Value)
                awy1 = y1(ucwarps.NumericUpDown1.Value)
                awy2 = y2(ucwarps.NumericUpDown1.Value)
                Dim w As Integer = ucwarps.NumericUpDown1.Value
                If x1(w) <= NumericUpDown11.Value Then
                    If NumericUpDown11.Value <= x2(w) Then
                        If y1(w) <= NumericUpDown12.Value Then
                            If NumericUpDown12.Value <= y2(w) Then
                                If x1(w) << 2 = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 2 Then
                                    warpflags = warpflags Or &H1
                                End If
                                If ((x2(w) + 1) << 2) - 1 = ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 2) Then
                                    warpflags = warpflags Or &H2
                                End If
                                If y1(w) << 2 = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2 Then
                                    warpflags = warpflags Or &H4
                                End If
                                If ((y2(w) + 1) << 2) - 1 = ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2) Then
                                    warpflags = warpflags Or &H8
                                End If
                                If warpflags = 0 Then warpflags = warpflags Or &HF
                                Label34.Text = warpflags
                            End If
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub Panel6_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel6.MouseDown
        NumericUpDown11.Value = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown12.Value = (MousePosition.Y.ToString - Panel6.PointToScreen(New Point(0, 0)).Y) >> 4
        Select Case ComboBox6.SelectedIndex
            Case 2
                awx1 = x1(ucwarps.NumericUpDown1.Value)
                awx2 = x2(ucwarps.NumericUpDown1.Value)
                awy1 = y1(ucwarps.NumericUpDown1.Value)
                awy2 = y2(ucwarps.NumericUpDown1.Value)
                Dim w As Integer = ucwarps.NumericUpDown1.Value
                If x1(w) <= NumericUpDown11.Value Then
                    If NumericUpDown11.Value <= x2(w) Then
                        If y1(w) <= NumericUpDown12.Value Then
                            If NumericUpDown12.Value <= y2(w) Then
                                If x1(w) << 2 = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 2 Then
                                    warpflags = warpflags Or &H1
                                End If
                                If ((x2(w) + 1) << 2) - 1 = ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 2) Then
                                    warpflags = warpflags Or &H2
                                End If
                                If y1(w) << 2 = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2 Then
                                    warpflags = warpflags Or &H4
                                End If
                                If ((y2(w) + 1) << 2) - 1 = ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2) Then
                                    warpflags = warpflags Or &H8
                                End If
                                If warpflags = 0 Then warpflags = warpflags Or &HF
                                Label34.Text = warpflags
                            End If
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub Panel4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel4.MouseDown
        NumericUpDown11.Value = (MousePosition.X.ToString - Panel4.PointToScreen(New Point(0, 0)).X) >> 4
        NumericUpDown12.Value = (MousePosition.Y.ToString - Panel4.PointToScreen(New Point(0, 0)).Y) >> 4
        Select Case ComboBox6.SelectedIndex
            Case 2
                awx1 = x1(ucwarps.NumericUpDown1.Value)
                awx2 = x2(ucwarps.NumericUpDown1.Value)
                awy1 = y1(ucwarps.NumericUpDown1.Value)
                awy2 = y2(ucwarps.NumericUpDown1.Value)
                Dim w As Integer = ucwarps.NumericUpDown1.Value
                If x1(w) <= NumericUpDown11.Value Then
                    If NumericUpDown11.Value <= x2(w) Then
                        If y1(w) <= NumericUpDown12.Value Then
                            If NumericUpDown12.Value <= y2(w) Then
                                If x1(w) << 2 = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 2 Then
                                    warpflags = warpflags Or &H1
                                End If
                                If ((x2(w) + 1) << 2) - 1 = ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 2) Then
                                    warpflags = warpflags Or &H2
                                End If
                                If y1(w) << 2 = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2 Then
                                    warpflags = warpflags Or &H4
                                End If
                                If ((y2(w) + 1) << 2) - 1 = ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2) Then
                                    warpflags = warpflags Or &H8
                                End If
                                If warpflags = 0 Then warpflags = warpflags Or &HF
                                Label34.Text = warpflags
                            End If
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub Panel6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel6.Click
        MapLayer_Click(Panel6)
    End Sub
    Private Sub Panel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.Click
        MapLayer_Click(Panel4)
    End Sub
    Private Sub MapLayer_Click(ByVal maplayer As Panel)
        'If ModifierKeys = Keys.Shift Then
        Select Case ComboBox6.SelectedIndex
            Case 1
                If CheckBox3.Checked Then
                    Dim tsolidind As Short
                    FileGet(1, tsolidind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + 6 + 1)
                    FileGet(1, offset3, &H8E08E0 + (tsolidind << 2) + 1)
                    FilePut(1, CByte(NumericUpDown30.Value), &H8E08E0 + offset3 + ((Panel7.Width >> 4) * ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 4)) + ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 4) + 1)
                    Panel7.Refresh()
                End If
            Case 0
                'NumericUpDown11.Value = (MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 4
                'NumericUpDown12.Value = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 4
                'If ComboBox6.SelectedIndex = 0 Then Button2.PerformClick()
                    Dim newx As Integer = (MousePosition.X.ToString - maplayer.PointToScreen(New Point(0, 0)).X) >> 4
                    Dim newy As Integer = (MousePosition.Y.ToString - maplayer.PointToScreen(New Point(0, 0)).Y) >> 4
                    If NumericUpDown11.Value = newx Then
                        If NumericUpDown12.Value = newy Then
                            Button2.PerformClick()
                            Exit Sub
                        End If
                    End If
                fillsection1x1(newx - NumericUpDown11.Value, newy - NumericUpDown12.Value)
            Case 2
                Dim newx As Integer = (MousePosition.X.ToString - maplayer.PointToScreen(New Point(0, 0)).X) >> 4
                Dim newy As Integer = (MousePosition.Y.ToString - maplayer.PointToScreen(New Point(0, 0)).Y) >> 4
                'Dim pen1 As New Pen(Brushes.Brown, 4)
                warpflags = warpflags And &HFFFFFFF0
                If NumericUpDown11.Value = newx Then
                    If NumericUpDown12.Value = newy Then
                        For w = 0 To warps
                            'Select Case newx
                            '    Case Is >= x1(w) and
                            'End Select
                            If x1(w) <= newx Then
                                If newx <= x2(w) Then
                                    If y1(w) <= newy Then
                                        If newy <= y2(w) Then
                                            If ucwarps.NumericUpDown1.Value <> w Then
                                                ucwarps.NumericUpDown1.Value = w
                                                Exit For
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            'e.Graphics.DrawRectangle(pen1, CShort(x1(w)) << 4, CShort(y1(w)) << 4, (CShort(x2(w) - x1(w)) << 4) + 16, (CShort(y2(w) - y1(w)) << 4) + 16)
                            'e.Graphics.DrawRectangle(pen1, x1(w) * 16, y1(w) * 16, (x2(w) - x1(w)) * 16 + 16, (y2(w) - y1(w)) * 16 + 16)
                        Next
                        Exit Select
                    End If
                End If
        End Select

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

    'Private Sub Panel7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.MouseMove
    '    maplmousemove()
    'End Sub
    'Private Sub Panel6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel6.MouseMove
    '    maplmousemove()
    'End Sub
    'Private Sub Panel4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel4.MouseMove
    '    maplmousemove()
    'End Sub
    'Private Sub maplmousemove()
    '    'Cursor = Cursors.SizeNS
    '    'Cursor = Cursors.SizeWE
    'End Sub

    'Private Sub Panel7_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.MouseDown

    'End Sub

    'Private Sub Panel7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.MouseMove

    'End Sub

    'Private Sub Panel11_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel11.MouseDown
    '    NumericUpDown28.Value = 1
    '    NumericUpDown29.Value = 1
    '    NumericUpDown9.Value = (MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4
    '    NumericUpDown10.Value = (MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4
    '    Panel11.Refresh()
    'End Sub
    'Private Sub Panel11_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel11.MouseMove
    '    If MouseButtons = MouseButtons.Left Then
    '        If NumericUpDown28.Value <> ((MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4) - NumericUpDown9.Value + 1 Then
    '            Try
    '                NumericUpDown28.Value = ((MousePosition.X.ToString - Panel11.PointToScreen(New Point(0, 0)).X) >> 4) - NumericUpDown9.Value + 1
    '                If NumericUpDown9.Value + NumericUpDown28.Value > 32 Then NumericUpDown28.Value = 32 - NumericUpDown9.Value
    '                Panel11.Refresh()
    '            Catch ex As Exception
    '            End Try
    '        End If
    '        If NumericUpDown29.Value <> ((MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4) - NumericUpDown10.Value + 1 Then
    '            Try
    '                NumericUpDown29.Value = ((MousePosition.Y.ToString - Panel11.PointToScreen(New Point(0, 0)).Y) >> 4) - NumericUpDown10.Value + 1
    '                If NumericUpDown10.Value + NumericUpDown29.Value > 32 Then NumericUpDown29.Value = 32 - NumericUpDown10.Value
    '                Panel11.Refresh()
    '                'Panel11.Invalidate(New Rectangle(NumericUpDown9.Value * 16, NumericUpDown10.Value * 16, NumericUpDown28.Value * 16, NumericUpDown29.Value * 16))
    '                'Panel11.CreateGraphics.DrawRectangle(New Pen(Brushes.Red, 2), New Rectangle(NumericUpDown9.Value << 4, NumericUpDown10.Value << 4, NumericUpDown28.Value << 4, NumericUpDown29.Value << 4))
    '                'DrawEllipse(New Pen(Brushes.Aqua, 3), New Rectangle(0, 0, 100, 100))
    '            Catch ex As Exception
    '            End Try
    '        End If
    '        'Panel9.AutoScrollPosition = New Point(0, (MousePosition.Y.ToString - Panel9.PointToScreen(New Point(0, 0)).Y))
    '        'If (MousePosition.Y.ToString - Panel9.PointToScreen(New Point(0, 128)).Y) > 0 Then
    '        'Panel9.AutoScrollPosition = New Point(0, +8) ' (NumericUpDown10.Value + NumericUpDown29.Value) * 16)
    '        'End If
    '    End If
    'End Sub

    ' Private Sub Panel4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.MouseHover

    ' End Sub

    ' Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
    'ln673
    'ln839
    'Dim img As Image
    'img = layer1bmp
    'Dim bru1 As Brush
    'bru1.layer1bmp()
    'e.Graphics.FillRectangle(bru1, 0, 0, Panel4.Width, Panel4.Height) ' layer1bmp.
    '.FromImage(layer1bmp) ', 0, 0)
    'Image.FromHbitmap(1)
    'Graphics(g)
    ' End Sub

    Private Sub Panel7_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel7.Paint
        '    Panel4.Refresh()
        'End Sub
        'Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

        'WARPS!
        'Dim pen1 As New SolidBrush(b(0))
        If CheckBox2.Checked Then
            Dim pen1 As New Pen(Brushes.Brown, 4)
            For w = 0 To warps
                e.Graphics.DrawRectangle(pen1, CShort(x1(w)) << 4, CShort(y1(w)) << 4, (CShort(x2(w) - x1(w)) << 4) + 16, (CShort(y2(w) - y1(w)) << 4) + 16)
                'e.Graphics.DrawRectangle(pen1, x1(w) * 16, y1(w) * 16, (x2(w) - x1(w)) * 16 + 16, (y2(w) - y1(w)) * 16 + 16)
            Next
        End If

        'TILESCRIPTING/SOLIDITY MAP
        If CheckBox3.Checked Then
            'Get the solidity set pointer.
            '  Dim tssoffset As Integer
            '  FileGet(1, tssoffset, &H3AADD0 + (solidind(ListBox1.SelectedIndex) << 2) + 1)

            Dim tsolidind As Short
            FileGet(1, tsolidind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + 6 + 1)
            FileGet(1, offset3, &H8E08E0 + (tsolidind << 2) + 1)
            Dim tsbyte As Byte
            Seek(1, &H8E08E0 + offset3 + 1)
            If ComboBox5.SelectedIndex = -1 Then
                For ybyte = 0 To (Panel4.Height >> 4) - 1
                    For xbyte = 0 To (Panel4.Width >> 4) - 1
                        FileGet(1, tsbyte)
                        e.Graphics.DrawString(tsbyte.ToString("X2"), New System.Drawing.Font("Courier New", 8, FontStyle.Bold), Brushes.Black, xbyte << 4, (ybyte << 4) + 1)
                    Next
                Next
            Else
                Dim tcolors() As Brush = {Brushes.Black, Brushes.Gray, Brushes.White, Brushes.Red, Brushes.Orange, Brushes.Yellow, Brushes.Green, Brushes.Blue, Brushes.Indigo, Brushes.Violet}
                'Dim rectpen
                For ybyte = 0 To (Panel4.Height >> 4) - 1
                    For xbyte = 0 To (Panel4.Width >> 4) - 1
                        FileGet(1, tsbyte)
                        'Brushes.
                        'Dim aaa As Color = Color.FromArgb(255, (tsbyte << 5) Or (tsbyte >> 3), (tsbyte << 4) Or (tsbyte >> 4), (tsbyte << 3) Or (tsbyte >> 5))
                        'Dim aaa2 As SolidBrush = New SolidBrush(aaa)
                        'e.Graphics.FillRectangle(aaa2, New Rectangle(xbyte << 4, ybyte << 4, (xbyte << 4) + 15, (ybyte << 4) + 15))
                        e.Graphics.DrawString(tsbyte.ToString("X2"), New System.Drawing.Font("Courier New", 8, FontStyle.Bold), tcolors(ComboBox5.SelectedIndex), xbyte << 4, (ybyte << 4) + 1)
                    Next
                Next
            End If
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Panel7.Refresh()
        'Dim w = 0
        'MsgBox(Hex(x1(w)) & " " & Hex(y1(w)) & " " & Hex(x2(w)) & " " & Hex(y2(w)))
        'Panel7.Update()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        layer1bmp2 = Panel4.BackgroundImage
        layer2bmp2 = Panel6.BackgroundImage
        layer3bmp2 = Panel7.BackgroundImage
        '480:
        '320:
        Dim col1 As Color
        Dim col2 As Color
        Dim col3 As Color
        For h = 0 To layer3bmp2.Height - 1
            For w = 0 To layer3bmp2.Width - 1
                col1 = layer1bmp2.GetPixel(w, h)
                col2 = layer2bmp2.GetPixel(w, h)
                col3 = layer3bmp2.GetPixel(w, h)
                'MsgBox(col2.ToString)
                'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb + col1.ToArgb))
                'Dim dred As Byte = (CInt(col3.R >> 1) + CInt(col1.R >> 1)) And &HFF
                'Dim dgreen As Byte = (CInt(col3.G >> 1) + CInt(col1.G >> 1)) And &HFF
                'Dim dblue As Byte = (CInt(col3.B >> 1) + CInt(col1.B >> 1)) And &HFF
                'Try
                '    layer3bmp2.SetPixel(w, h, Color.FromArgb(Color.FromArgb(255, dred, dgreen, dblue).ToArgb))
                '    'layer3bmp2.SetPixel(w, h, Color.FromArgb(Color.FromArgb(255, CByte(col3.R + col1.R), CByte(col3.G + col1.G), CByte(col3.B + col1.B)).ToArgb))
                '    'layer3bmp2.SetPixel(w, h, Color.FromArgb(Color.FromArgb(128, CByte(col3.R + col2.R + col1.R), CByte(col3.G + col2.G + col1.G), CByte(col3.B + col2.B + col1.B)).ToArgb))
                'Catch
                '    GoTo clean
                'End Try
                'layer3bmp2.SetPixel(w, h, Color.FromArgb(Color.FromArgb(127, col3.R, col3.G, col3.B).ToArgb))
                If col2 = Color.FromArgb(0, 0, 0, 0) Then ' 255, 255, 255) Then
                    'MsgBox(col2.ToString)
                    layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col1.ToArgb + Color.FromArgb(0, 255, 255, 255).ToArgb))
                    'layer3bmp2.SetPixel(w, h, Color.FromArgb((col3.ToArgb + col1.ToArgb)))
                    'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col1.ToArgb))
                Else
                    layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb + Color.FromArgb(0, 255, 255, 255).ToArgb))
                    'layer3bmp2.SetPixel(w, h, Color.FromArgb((col3.ToArgb + col2.ToArgb)))
                    'layer3bmp2.SetPixel(w, h, Color.FromArgb(col3.ToArgb + col2.ToArgb)) ' + Color.FromArgb(0, 255, 255, 255).ToArgb))
                End If
                'col1()
                'ColorConverter()
                'Microsoft.Visu
                'layer3bmp2.MakeTransparent()
            Next
        Next
clean:
        Panel7.BackgroundImage = layer3bmp2
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
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        rnind(ListBox1.SelectedIndex) = ComboBox1.SelectedIndex  'Room Names
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        mlind(ListBox1.SelectedIndex) = ComboBox2.SelectedIndex 'Mario/Luigi
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        underwaterflag(ListBox1.SelectedIndex) = CheckBox1.Checked * -1 << 4
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        ctind(ListBox1.SelectedIndex) = ComboBox3.SelectedIndex  'Comp. Tiles
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        tsind(ListBox1.SelectedIndex) = ComboBox4.SelectedIndex 'tilesets
    End Sub
    Private Sub NumericUpDown6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        palind(ListBox1.SelectedIndex) = NumericUpDown6.Value
    End Sub
    Private Sub NumericUpDown13_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown13.ValueChanged
        solidind(ListBox1.SelectedIndex) = NumericUpDown13.Value
    End Sub
    Private Sub NumericUpDown14_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown14.ValueChanged
        aniind(ListBox1.SelectedIndex) = NumericUpDown14.Value
    End Sub
    Private Sub NumericUpDown15_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown15.ValueChanged
        unk1ind(ListBox1.SelectedIndex) = NumericUpDown15.Value
    End Sub
    Private Sub NumericUpDown16_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown16.ValueChanged
        laybind(ListBox1.SelectedIndex) = NumericUpDown16.Value
    End Sub
    Private Sub NumericUpDown17_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown17.ValueChanged
        tmodsind(ListBox1.SelectedIndex) = NumericUpDown17.Value
    End Sub
    Private Sub NumericUpDown18_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown18.ValueChanged
        unk2ind(ListBox1.SelectedIndex) = NumericUpDown18.Value
    End Sub
    Private Sub NumericUpDown19_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown19.ValueChanged
        ls1ind(ListBox1.SelectedIndex) = NumericUpDown19.Value
    End Sub
    Private Sub NumericUpDown20_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown20.ValueChanged
        ls2ind(ListBox1.SelectedIndex) = NumericUpDown20.Value
    End Sub
    Private Sub NumericUpDown21_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown21.ValueChanged
        mapscrind(ListBox1.SelectedIndex) = NumericUpDown21.Value
    End Sub
    Private Sub NumericUpDown22_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown22.ValueChanged
        npcind(ListBox1.SelectedIndex) = NumericUpDown22.Value
    End Sub
    Private Sub NumericUpDown23_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown23.ValueChanged
        ls3ind(ListBox1.SelectedIndex) = NumericUpDown23.Value
    End Sub
    Private Sub NumericUpDown24_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown24.ValueChanged
        ls4ind(ListBox1.SelectedIndex) = NumericUpDown24.Value
    End Sub
    Private Sub NumericUpDown25_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown25.ValueChanged
        songind(ListBox1.SelectedIndex) = NumericUpDown25.Value
    End Sub
    Private Sub NumericUpDown26_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown26.ValueChanged
        unk3ind(ListBox1.SelectedIndex) = NumericUpDown26.Value
    End Sub
    Private Sub NumericUpDown27_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown27.ValueChanged
        itmbkind(ListBox1.SelectedIndex) = NumericUpDown27.Value
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
                    'Panel11.Invalidate(New Rectangle(NumericUpDown9.Value * 16, NumericUpDown10.Value * 16, NumericUpDown28.Value * 16, NumericUpDown29.Value * 16))
                    'Panel11.CreateGraphics.DrawRectangle(New Pen(Brushes.Red, 2), New Rectangle(NumericUpDown9.Value << 4, NumericUpDown10.Value << 4, NumericUpDown28.Value << 4, NumericUpDown29.Value << 4))
                    'DrawEllipse(New Pen(Brushes.Aqua, 3), New Rectangle(0, 0, 100, 100))
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
        If NumericUpDown8.Value <> &HFFFFS Then e.Graphics.DrawImage(ts2bmp, New Point(0, 256))
        e.Graphics.DrawRectangle(New Pen(Brushes.Red, 2), New Rectangle(NumericUpDown9.Value << 4, NumericUpDown10.Value << 4, NumericUpDown28.Value << 4, NumericUpDown29.Value << 4))
    End Sub
    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Panel7.Refresh()
    End Sub
    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Panel7.Refresh()
    End Sub

    'Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    FilePut(1, CByte(NumericUpDown30.Value), etsbloc) '&H8E08E0 + offset3 + ((Panel7.Width >> 4) * ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 4)) + ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 4) + 1)
    '    Panel7.Refresh()
    'End Sub

    Private Sub NumericUpDown30_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown30.ValueChanged
        'Get the solidity set pointer.
        Dim tssoffset As Integer
        FileGet(1, tssoffset, &H3AADD0 + (solidind(ListBox1.SelectedIndex) << 2) + 1)
        Dim tssdata As Integer
        FileGet(1, tssdata, tssoffset - &H8000000 + (CInt(NumericUpDown30.Value) << 2) + 1)
        Label17.Text = (tssoffset + (CInt(NumericUpDown30.Value) << 2)).ToString("X8") & ":" & tssdata.ToString("X8")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
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
        Dim layer1ind As Short
        Dim lay As Byte = 0 'Layer # (0-2)
        'layer1bmp2 = Panel4.BackgroundImage
        'layer2bmp2 = Panel6.BackgroundImage
        'layer3bmp2 = Panel7.BackgroundImage
        If RadioButton1.Checked Then lay = 0
        If RadioButton2.Checked Then lay = 1
        If RadioButton3.Checked Then lay = 2
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 1
                'Panel4.BackgroundImage = layer2bmp2
                'Panel6.BackgroundImage = layer1bmp2
                'Panel7.BackgroundImage = layer3bmp2
                If RadioButton1.Checked Then lay = 1
                If RadioButton2.Checked Then lay = 0
                If RadioButton3.Checked Then lay = 2
        End Select

        'For h = 0 To NumericUpDown29.Value - 1
        '    For w = 0 To NumericUpDown28.Value - 1

        FileGet(1, layer1ind, &H3AAE08 + (ListBox1.SelectedIndex << 3) + (lay << 1) + 1)
        If layer1ind = -1 Then Panel4.BackgroundImage = Nothing 'FileClose(1) : Exit Sub
        FileGet(1, offset3, &H754D74 + (layer1ind << 2) + 1) ' &H7550BC + 1)
        'offset3 = &H754D74 + offset3
        'Seek(1, &H754D74 + offset3 + 1)
        Dim scnw As Byte
        FileGet(1, scnw, &H754D74 + offset3 + 1)
        Dim scnh As Byte
        FileGet(1, scnh)

        'For h = 0 To scnh * 10 'NumericUpDown29.Value - 1
        'For w = 0 To scnw * 15 'NumericUpDown28.Value - 1

        Dim stem As Byte = (((NumericUpDown10.Value) >> 3) And &H3) * &H55
        Dim leaf As Byte = (((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
        For h = 0 To scnh * 10 - 1 '15 '7 '15
            For w = 0 To (((scnw * 15 + 3) And &HFFFCS) >> 2) - 1
                'If h = 0 Then
                'FileGet(1, stem) ', Loc(1) + 1 + (w >> 2) * 5)
                ' Else
                'FileGet(1, stem, CInt(((Loc(1) + h * (((scnw * 15) >> 2) + 1) * 5 + 3) And &HFFFCS) + (w >> 2) * 5) - 1)
                'End If
                'stem = (stem And (&HFF Xor (&H3 << (Math.Abs((NumericUpDown11.Value And &H3) - 3) << 1)))) + _
                '(((NumericUpDown10.Value >> 3) And &H3) << (Math.Abs((NumericUpDown11.Value And &H3) - 3) << 1))
                'NumericUpDown9 'Tileset X
                'NumericUpDown10 'Tileset Y
                'NumericUpDown11 'Map X
                'NumericUpDown12 'Map Y
                'stem = (stem And (Not (&H3 << (Math.Abs((w And &H3) - 3) << 1)))) + _
                '        ((((NumericUpDown10.Value) >> 3) And &H3) << (Math.Abs((w And &H3) - 3) << 1))
                'stem = (((NumericUpDown10.Value) >> 3) And &H3) * &H55 ' Or (((NumericUpDown10.Value) >> 3) And &H3) Or (((NumericUpDown10.Value) >> 3) And &H3) Or (((NumericUpDown10.Value) >> 3) And &H3)
                'stem = (stem >> (Math.Abs(((NumericUpDown10.Value >> 3) And &H3) - 3) << 1)) And 3
                FilePut(1, stem) ', Loc(1))
                'Dim leaf As Byte = (((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
                'Seek(1, Loc(1) + 1 + (w And &H3))
                'leaf = (((NumericUpDown10.Value) And &H7) << 5) Or ((NumericUpDown9.Value) And &H1F)
                FilePut(1, leaf)
                FilePut(1, leaf)
                FilePut(1, leaf)
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
        'layer1(Panel4, layer1bmp, 0)
        Select Case underwaterflag(ListBox1.SelectedIndex) >> 5
            Case 0
                If lay = 0 Then layer1(Panel4, layer1bmp, 0) 'Updates the map.
                If lay = 1 Then layer1(Panel6, layer2bmp, 1)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
            Case 1
                If lay = 1 Then layer1(Panel4, layer2bmp, 1) 'Updates the map.
                If lay = 0 Then layer1(Panel6, layer1bmp, 0)
                If lay = 2 Then layer1(Panel7, layer3bmp, 2)
        End Select
        Exit Sub
cleanup:
    End Sub

    Dim disposethis As Integer
    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        'Me.Controls.Add(UserControl1)
        'this.
        'If disposethis > 0 Then Me.Controls.RemoveAt(disposethis - 1)
        If ComboBox6.SelectedIndex = 2 Then
            'this.Show()
            ucwarps.BringToFront()
        Else
            Panel12.BringToFront()
            'Me.TabPage10.Controls.Remove(UserControl1)
            'this.Hide()
            'this.Dispose()
        End If


        'this.Top = 
        'UserControl1.
    End Sub

    Private Sub Panel4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel4.MouseMove
        maplmousemove(Panel4)
    End Sub
    Private Sub Panel6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel6.MouseMove
        maplmousemove(Panel6)
    End Sub
    Private Sub Panel7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.MouseMove
        maplmousemove(Panel7)
    End Sub
    Private Sub maplmousemove(ByVal maplayer As Panel)
        Dim newx As Integer = (MousePosition.X.ToString - maplayer.PointToScreen(New Point(0, 0)).X) >> 4
        Dim newy As Integer = (MousePosition.Y.ToString - maplayer.PointToScreen(New Point(0, 0)).Y) >> 4
        Dim cursorvar As Integer
        Select Case ComboBox6.SelectedIndex
            Case 2
                If warpflags = 0 Then
                    Dim w As Integer = ucwarps.NumericUpDown1.Value
                    If x1(w) <= newx Then
                        If newx <= x2(w) Then
                            If y1(w) <= newy Then
                                If newy <= y2(w) Then
                                    'warpflags = warpflags Or &HF

                                    If x1(w) << 2 = (MousePosition.X.ToString - Panel6.PointToScreen(New Point(0, 0)).X) >> 2 Then
                                        'warpflags = warpflags Or &H1
                                        cursorvar = cursorvar Or 1
                                    End If
                                    If ((x2(w) + 1) << 2) - 1 = ((MousePosition.X.ToString - Panel7.PointToScreen(New Point(0, 0)).X) >> 2) Then
                                        'warpflags = warpflags Or &H2
                                        cursorvar = cursorvar Or 2
                                    End If
                                    If y1(w) << 2 = (MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2 Then
                                        'warpflags = warpflags Or &H4
                                        cursorvar = cursorvar Or 4
                                    End If
                                    If ((y2(w) + 1) << 2) - 1 = ((MousePosition.Y.ToString - Panel7.PointToScreen(New Point(0, 0)).Y) >> 2) Then
                                        'warpflags = warpflags Or &H8
                                        cursorvar = cursorvar Or 8
                                    End If
                                    Select Case cursorvar
                                        Case 0 '3 and 7 do not occur.
                                            Cursor = Cursors.SizeAll
                                        Case 1, 2
                                            Cursor = Cursors.SizeWE
                                        Case 4, 8
                                            Cursor = Cursors.SizeNS
                                        Case 5, 10
                                            Cursor = Cursors.SizeNWSE
                                        Case 6, 9
                                            Cursor = Cursors.SizeNESW
                                    End Select
                                    GoTo bucheck 'Label34.Text = warpflags
                                End If
                            End If
                        End If
                    End If
                    Cursor = Cursors.Arrow
                End If
bucheck:
                If MouseButtons = Windows.Forms.MouseButtons.Left Then
                    If warpflags > 0 Then
                        'If x1(ucwarps.NumericUpDown1.Value) = Int(awx1) + (newx - NumericUpDown11.Value) Then _
                        'If x2(ucwarps.NumericUpDown1.Value) = Int(awx2) + (newx - NumericUpDown11.Value) Then _
                        'If y1(ucwarps.NumericUpDown1.Value) = Int(awy1) + (newy - NumericUpDown12.Value) Then _
                        'If y2(ucwarps.NumericUpDown1.Value) = Int(awy2) + (newy - NumericUpDown12.Value) Then Exit Select

                        If warpflags And 1 Then x1(ucwarps.NumericUpDown1.Value) = Int(awx1) + (newx - NumericUpDown11.Value)
                        If (warpflags >> 1) And 1 Then x2(ucwarps.NumericUpDown1.Value) = Int(awx2) + (newx - NumericUpDown11.Value)
                        If (warpflags >> 2) And 1 Then y1(ucwarps.NumericUpDown1.Value) = Int(awy1) + (newy - NumericUpDown12.Value)
                        If (warpflags >> 3) And 1 Then y2(ucwarps.NumericUpDown1.Value) = Int(awy2) + (newy - NumericUpDown12.Value)
                        If x2(ucwarps.NumericUpDown1.Value) < x1(ucwarps.NumericUpDown1.Value) Then x1(ucwarps.NumericUpDown1.Value) = x2(ucwarps.NumericUpDown1.Value)
                        If y2(ucwarps.NumericUpDown1.Value) < y1(ucwarps.NumericUpDown1.Value) Then y1(ucwarps.NumericUpDown1.Value) = y2(ucwarps.NumericUpDown1.Value)
                        Panel7.Refresh()
                        '1280
                    End If
                End If
        End Select
    End Sub

    Private Sub SaveWarpDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveWarpDataToolStripMenuItem.Click
        For w = 0 To warps 'cwarps '(a) '10 'warppointer(a) '10
            'MsgBox(a & " " & b)
            FilePut(1, x1(w), warppointer - &H8000000 + (&HC * w) + 1)
            FilePut(1, y1(w)) ', warppointer - &H8000000 + &H1 + (&HC * w) + 1)
            FilePut(1, x2(w)) ', warppointer - &H8000000 + &H2 + (&HC * w) + 1)
            FilePut(1, y2(w)) ', warppointer - &H8000000 + &H3 + (&HC * w) + 1)
            'FileGet(1, warpto(a, b), warppointer(a) - &H8000000 + &H4 + (&HC * b) + 1)

            'FileGet(1, unk(a, b), warppointer(a) - &H8000000 + &H6 + (&HC * b) + 1)
            'FileGet(1, x(a, b), warppointer(a) - &H8000000 + &H7 + (&HC * b) + 1)
            'FileGet(1, y(a, b), warppointer(a) - &H8000000 + &H8 + (&HC * b) + 1)
            'FileGet(1, xy(a, b), warppointer(a) - &H8000000 + &H9 + (&HC * b) + 1)
            FilePut(1, warpdataint1(w))
            FilePut(1, warpdataint2(w))
        Next
    End Sub
End Class