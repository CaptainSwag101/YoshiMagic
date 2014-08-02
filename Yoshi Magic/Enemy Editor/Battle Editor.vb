Public Class Battle_Editor
    'TO DO?: Calculate Flying in Arrangements.
    Dim edpointer As Integer 'Enemy Database
    Dim enemies As Byte = &HBC
    Dim namepointer(enemies) As Integer
    'Dim Nametxt(enemies, 16) As Byte
    Dim EnemyName(enemies) As String
    Dim spritepointer(enemies) As Integer
    Dim spriteind(enemies) As Short
    'Arrangements
    Dim arrdbase As Integer 'Arrangements Database = &H8502B14
    'Dim arranges = 52
    'Dim arr(arranges, 5) As Integer
    Dim arrangement(52, 5) As Integer
    'Battle Editor Dims
    Dim c As Integer 'Listbox2.SelectedIndex
    Dim bdpointer As Integer 'Battle Database
    Dim battles As Byte = &HBA
    Dim blength As Byte = &H20
    Dim battledb(battles, blength) As Byte
    Dim byte1(battles) As Byte 'Run and Chance for battle
    Dim byte2(battles) As Byte 'Arrangements Index
    Dim byte3(battles) As Byte 'An unknown, 00, 01 or 02.
    Public battlebg(battles) As Byte 'Byte 4, the background.
    Dim benemy(battles, 5) As Short 'The enemies of each battle.
    Dim benemy2(battles, 5) As Short 'Flags (Hidden and unknowns)
    'Room specific battle backgrounds related.
    Dim rbgdbase As Integer ' = &H200205
    Dim rooms As Integer = &H1FF
    Dim roombg(rooms) As Byte
    'Room Names    
    Dim rpbank As Integer '= &H83A78D4
    Dim rnind(rooms) As Byte
    Dim rndpointer As Integer ' = &H83C03E8
    Dim rnames As Byte = 100
    'Dim namepointer1(rnames) As Integer
    'Dim Nametxt1(rnames, 40) As Byte
    Dim RoomName(rnames) As String
    'Dim Nametxt2(rnames, 40) As Byte
    'Dim RoomName2(rnames) As String
    'Dim rn2offset(rnames) As Byte
    '567,808 - 569,344
    'Sprite Size Database
    Dim spritesizes(2) As Integer
    'Dim panel4paint As Boolean = 0

#Region " Battle Editor "
#Region " Load "
    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'Version Check (Enemy Database)
        'Get Arrangements Database
        'Version Check (Battle Database)
        'Getting the List of Room Names.
        'Getting the Names of Rooms.
        'Getting Room-specific Backgrounds data.
        Select Case Form1.version
            Case "NA"
                FileGet(1, edpointer, &H7B514 + 1)
                FileGet(1, arrdbase, &H7B8C4 + 1) '&HF5180; &HF51A8; &HF51D0; &HF5208; &HF5240; &HF52A8; &HF7E7C; &H11324C
                FileGet(1, bdpointer, &H80668 + 1) '&H81280; &HFC308
                FileGet(1, rndpointer, &H123300 + 1) '&H123334
                FileGet(1, rpbank, &H22B78 + 1) '59804;59EAC;59EC4;59EE8;59F08;5BD90;5D934;EEE7C;118374;123338
                FileGet(1, rbgdbase, &HFC30C + 1)
            Case "EU"
                FileGet(1, edpointer, &H7BD50 + 1)
                FileGet(1, arrdbase, &H7C100 + 1) '&HF59BC; &HF59E4; &HF5A0C; &HF5A44; &HF5A7C; &HF5AE4; &HF86B8; &H113FBC
                FileGet(1, bdpointer, &H80EA4 + 1) '&H81ABC; &HFCB44
                FileGet(1, rndpointer, &H1240A4 + 1) '&H1240D8
                FileGet(1, rpbank, &H22B8C + 1) '5A040;5A6E8;5A700;5A724;5A744;5C5CC;5E170;EF6B8;1190E4;1240DC
                FileGet(1, rbgdbase, &HFCB48 + 1)
            Case "J"
                'enemies += 3
                FileGet(1, edpointer, &H7C1AC + 1)
                'MsgBox(Hex(enemies))
                FileGet(1, arrdbase, &H7C57C + 1) '&HEC828; &HEC850; &HEC878; &HEC8B0; &HEC8E8; &HEC950; &HEF540; &H10B338
                '3eb8f0
                FileGet(1, bdpointer, &H81344 + 1) '&H81F5C; &HF3A04
                FileGet(1, rndpointer, &H11B264 + 1) '&H11B288
                FileGet(1, rpbank, &H22C6C + 1) '5A360;5AA08;5AA20;5AA44;5AA64;5C8EC;5E490;E6504;110478;11B28C
                FileGet(1, rbgdbase, &HF3A08 + 1)
            Case "NADEMO"
                'MsgBox("!")
                FileGet(1, edpointer, &H6645C + 1) '&H66564; &H6CD18; &HE0CB0; &HEC7E8
                FileGet(1, arrdbase, &H6680C + 1) 'E00F8;E0120;E0148;E0180;E01B8;E0220;E2DF4;FE0FC
                FileGet(1, bdpointer, &H6B5B0 + 1) '6C1C8;E7280
                FileGet(1, rndpointer, &H10DF00 + 1)
                FileGet(1, rpbank, &HD824 + 1) '/to &H25E328 (11 pointers...)
                FileGet(1, rbgdbase, &HE7284 + 1) '/to &H1C94C5
                'MsgBox("!!")
            Case Else
                MsgBox("I am sorry, but your ROM is not compatible to this part of the editor.")
                Me.Close()
        End Select
        'Getting enemy-related data.
        Dim letter As Byte
        For a = 0 To enemies
            'Getting Enemy Names data.
            'FileGet(1, namepointer(a), (enemydb(a, 2) * 65536) + (enemydb(a, 1) * 256) + enemydb(a, 0) + 1)
            FileGet(1, namepointer(a), edpointer - &H8000000 + (&H2C * a) + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            'Seek(1, namepointer(a) - &H8000000 + 1)
            'EnemyName(a) = LineInput(1)
            Seek(1, namepointer(a) - &H8000000 + 1)
            For n = 0 To 16
                'FileGet(1, Nametxt(a, n), namepointer(a) - &H8000000 + n + 1)
                'If Nametxt(a, n) = 0 Then Exit For
                'EnemyName(a) += Chr(Nametxt(a, n)) 'Store Enemy Names
                FileGet(1, letter) ', namepointer(a) - &H8000000 + n + 1)
                If letter = 0 Then Exit For
                EnemyName(a) += Chr(letter) 'Store Enemy Names
            Next n
            'List all the enemies in combo boxes for adding to battles.
            ComboBox6.Items.Add(Hex(a) & " - " & EnemyName(a))
            ComboBox7.Items.Add(Hex(a) & " - " & EnemyName(a))
            ComboBox8.Items.Add(Hex(a) & " - " & EnemyName(a))
            ComboBox9.Items.Add(Hex(a) & " - " & EnemyName(a))
            ComboBox10.Items.Add(Hex(a) & " - " & EnemyName(a))
            ComboBox11.Items.Add(Hex(a) & " - " & EnemyName(a))
            'Getting Enemy Sprites data.
            FileGet(1, spritepointer(a), edpointer - &H8000000 + (&H2C * a) + 8 + 1)
            FileGet(1, spriteind(a), spritepointer(a) - &H8000000 + 1)
        Next

        'Get Arrangements Database
        Seek(1, arrdbase - &H8000000 + 1)
        'MsgBox("1a")
        For a = 0 To 52
            For b = 0 To 5
                FileGet(1, arrangement(a, b)) ', arrdbase - &H8000000 + (a * &H18) + (b << 2) + 1)
            Next
        Next
        'Getting Battle Data
        'MsgBox("1b")
        For a = 0 To &HBA
            For b = 0 To blength
                FileGet(1, battledb(a, b), bdpointer - &H8000000 + (a << 5) + b + 1)
            Next
            FileGet(1, byte1(a), bdpointer - &H8000000 + (a << 5) + 1)
            FileGet(1, byte2(a)) ', bdpointer - &H8000000 + 1 + (a << 5) + 1)
            FileGet(1, byte3(a)) ', bdpointer - &H8000000 + 2 + (a << 5) + 1)
            FileGet(1, battlebg(a)) ', bdpointer - &H8000000 + 3 + (a << 5) + 1)
            For d = 0 To 5
                FileGet(1, benemy(a, d), bdpointer - &H8000000 + 8 + (a << 5) + (d << 2) + 1)
                FileGet(1, benemy2(a, d)) ', bdpointer - &H8000000 + &HA + (a << 5) + (d << 2) + 1)
            Next
            'List first enemy of each battle in Listbox
            ListBox2.Items.Add(Hex(a) & " - " & EnemyName(benemy(a, 0)))
        Next
        'TAB 2

        'Getting the List of Room Names.
        'Dim rbgdbase As Integer = &H200205
        'Dim rpbank = &H83A78D4
        'Dim rndpointer = &H83C03E8
        For a = 0 To 99 'rooms
            FileGet(1, namepointer(a), rndpointer - &H8000000 + (a << 2) + 1)
            If namepointer(a) <> 0 Then
                FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
                FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
                Seek(1, namepointer(a) - &H8000000 + 1)
                'Dim letter As Byte
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
                        Case &H0 To &H1F
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
        Next
        'Getting the Names of Rooms. 
        'Dim rbgdbase As Integer = &H200205
        'Dim rpbank = &H83A78D4
        'Dim rndpointer = &H83C03E8
        For a = 0 To rooms
            FileGet(1, rnind(a), rpbank - &H8000000 + (&H18 * a) + 1) 'Names
            ListBox1.Items.Add(Hex(a) & " -" & RoomName(rnind(a))) '& " : " & RoomName2(rnind(a)))
        Next
        'Getting Room-specific Backgrounds data.
        'Seek(1, rbgdbase - &H8000000 + 1)
        'For a = 0 To rooms
        '    FileGet(1, roombg(a)) ', rbgdbase - &H8000000 + a + 1) 'Battle BGs
        'Next
        FileGet(1, roombg, rbgdbase - &H8000000 + 1)
        '//----- The Sprite Size Database
        'MsgBox("!")
        For a = 0 To 2
            FileGet(1, spritesizes(a), &H50476C + (a << 2) + 1)
            Dim ss1 = (spritesizes(a) And &H3FF) << 5 ' * &H20
            Dim ss2 = ((spritesizes(a) And &HFFC00) >> 10) << 5 '* &H20
            Dim ss3 = ((spritesizes(a) And &H3FF00000) >> 20) << 5 '* &H20
            ComboBox1.Items.Add("SS1: " & ss1 & " - SS2: " & ss2 & " - SS3: " & ss3)
        Next
        'MsgBox("!!")
        'FileClose(1)
        ListBox2.SelectedIndex = 0
        ListBox1.SelectedIndex = 0
        'panel4paint = True
    End Sub
#End Region
#Region " Display Data "
    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        c = ListBox2.SelectedIndex
        'ToolStripStatusLabel1()
        Label4.Text = "Battle Database: " & Hex(bdpointer) & "  Battle Offset: " & Hex(bdpointer + (c << 5))
        CheckBox1.Checked = byte1(c) >> 7  'Disable Run
        NumericUpDown1.Value = byte1(c) And &H7F '% for battle
        ListBox3.Items.Clear()
        Dim numbattles As SByte = 100
        Do Until numbattles <= 0
            numbattles -= (byte1(c + ListBox3.Items.Count) And &H7F)
            ListBox3.Items.Add(ListBox2.Items.Item(c + ListBox3.Items.Count) & " (" & (byte1(c + ListBox3.Items.Count) And &H7F) & "%)")
        Loop
        'NumericUpDown3.Value = byte2(c) 'Arrangements 'MOVED TO BOTTOM due to error if Arrangement is not 0 for first enemy.
        'TextBox1.Text = byte3(c) 'Unknown
        ComboBox1.SelectedIndex = byte3(c)
        NumericUpDown2.Value = battlebg(c) '(Byte 4) Backgrounds
        'Display Enemy 0 data
        ComboBox14.SelectedIndex = benemy2(c, 0) >> 4 And 3
        ComboBox2.SelectedIndex = benemy2(c, 0) >> 1 And 3
        CheckBox9.Checked = benemy2(c, 0) And 1
        ComboBox6.SelectedIndex = benemy(c, 0)
        'Diplay Enemy 1 data
        ComboBox15.SelectedIndex = benemy2(c, 1) >> 4 And 3
        ComboBox3.SelectedIndex = benemy2(c, 1) >> 1 And 3
        CheckBox10.Checked = benemy2(c, 1) And 1
        ComboBox7.SelectedIndex = benemy(c, 1)
        'Display Enemy 2 data
        ComboBox16.SelectedIndex = benemy2(c, 2) >> 4 And 3
        ComboBox4.SelectedIndex = benemy2(c, 2) >> 1 And 3
        CheckBox11.Checked = benemy2(c, 2) And 1
        ComboBox8.SelectedIndex = benemy(c, 2)
        'Display Enemy 3 data
        ComboBox17.SelectedIndex = benemy2(c, 3) >> 4 And 3
        ComboBox5.SelectedIndex = benemy2(c, 3) >> 1 And 3
        CheckBox12.Checked = benemy2(c, 3) And 1
        ComboBox9.SelectedIndex = benemy(c, 3)
        'Display Enemy 4 data
        ComboBox18.SelectedIndex = benemy2(c, 4) >> 4 And 3
        ComboBox12.SelectedIndex = benemy2(c, 4) >> 1 And 3
        CheckBox13.Checked = benemy2(c, 4) And 1
        ComboBox10.SelectedIndex = benemy(c, 4)
        'Display Enemy 5 data
        ComboBox19.SelectedIndex = benemy2(c, 5) >> 4 And 3
        ComboBox13.SelectedIndex = benemy2(c, 5) >> 1 And 3
        CheckBox14.Checked = benemy2(c, 5) And 1
        ComboBox11.SelectedIndex = benemy(c, 5)
        NumericUpDown3.Value = byte2(c) 'Arrangements
        'Try
        '    'Panel2.BackgroundImage = ImageList1.Images.Item(spriteind(a) - &H4000)
        '    Panel2.BackgroundImage = System.Drawing.Image.FromFile("Sprites\Enemy Sprites\" & spriteind(a) - &H4000 & ".GIF")
        'Catch
        '    Panel2.BackgroundImage = ImageList2.Images.Item(0) 'spriteind(a) - &H4000)
        '    If spriteind(a) = &H3089 Then
        '        Panel2.BackgroundImage = ImageList2.Images.Item(1)
        '    End If
        '    If spriteind(a) = &H3091 Then
        '        Panel2.BackgroundImage = ImageList2.Images.Item(2)
        '    End If
        'End Try
    End Sub
#End Region
#Region " Data changes "
    'Chance for battle change...
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        byte1(c) = (byte1(c) And &H80) + NumericUpDown1.Value
        ListBox3.Items.Clear()
        Dim numbattles = 100
        Do Until numbattles <= 0
            numbattles -= byte1(c + ListBox3.Items.Count) And &H7F
            ListBox3.Items.Add(ListBox2.Items.Item(c + ListBox3.Items.Count) & " (" & (byte1(c + ListBox3.Items.Count) And &H7F) & "%)")
        Loop
    End Sub
    'Disable Run change...
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        byte1(c) = (byte1(c) And &H7F) + (CheckBox1.Checked * -&H80)
    End Sub
    'Arrangements change...
    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        'On Error Resume Next 'Errors on Sprite &H0000
        byte2(c) = NumericUpDown3.Value
        Panel4.Refresh()
    End Sub
    'Unknown Byte change...
    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        byte3(c) = TextBox1.Text
    '    Catch
    '    End Try
    'End Sub
    'Backgrounds change...
    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        battlebg(c) = NumericUpDown2.Value
        Panel4.BackgroundImage = System.Drawing.Image.FromFile(CurDir() & "\Battle Backgrounds\" & battlebg(c) & ".PNG")
        If battlebg(c) = 0 Then Label17.Visible = True Else Label17.Visible = False
        'Label17.Visible = Math.Abs(Math.Ceiling(battlebg(c) / 39) - 1)
    End Sub
    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        benemy(c, 0) = ComboBox6.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        benemy(c, 1) = ComboBox7.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        benemy(c, 2) = ComboBox8.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub ComboBox9_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9.SelectedIndexChanged
        benemy(c, 3) = ComboBox9.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub ComboBox10_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox10.SelectedIndexChanged
        benemy(c, 4) = ComboBox10.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub ComboBox11_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox11.SelectedIndexChanged
        benemy(c, 5) = ComboBox11.SelectedIndex
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        benemy2(c, 0) = (benemy2(c, 0) And &HFFFE) + (CheckBox9.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        benemy2(c, 1) = (benemy2(c, 1) And &HFFFE) + (CheckBox10.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        benemy2(c, 2) = (benemy2(c, 2) And &HFFFE) + (CheckBox11.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        benemy2(c, 3) = (benemy2(c, 3) And &HFFFE) + (CheckBox12.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        benemy2(c, 4) = (benemy2(c, 4) And &HFFFE) + (CheckBox13.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        benemy2(c, 5) = (benemy2(c, 5) And &HFFFE) + (CheckBox14.Checked * -1)
        Panel4.Refresh()
    End Sub
    Private Sub CheckBox38_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 0) = (benemy2(c, 0) And &HFFCF) + (ComboBox14.SelectedIndex << 4)
    End Sub
    Private Sub CheckBox37_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 1) = (benemy2(c, 1) And &HFFCF) + (ComboBox15.SelectedIndex << 4)
    End Sub
    Private Sub CheckBox36_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 2) = (benemy2(c, 2) And &HFFCF) + (ComboBox16.SelectedIndex << 4)
    End Sub
    Private Sub CheckBox35_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 3) = (benemy2(c, 3) And &HFFCF) + (ComboBox17.SelectedIndex << 4)
    End Sub
    Private Sub CheckBox34_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 4) = (benemy2(c, 4) And &HFFCF) + (ComboBox18.SelectedIndex << 4)
    End Sub
    Private Sub CheckBox33_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        benemy2(c, 5) = (benemy2(c, 5) And &HFFCF) + (ComboBox19.SelectedIndex << 4)
    End Sub
#End Region
#Region " Saving "
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click  'SaveToolStripMenuItem.Click
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For a = 0 To &HBA
            'If Form1.version = "NA" Then 'Use these for repointering the database...
            'ElseIf Form1.version = "EU" Then
            'ElseIf Form1.version = "J" Then
            'End If
            FilePut(1, byte1(a), bdpointer - &H8000000 + (a << 5) + 1)
            FilePut(1, byte2(a)) ', bdpointer - &H8000000 + 1 + (&H20 * a) + 1)
            FilePut(1, byte3(a)) ', bdpointer - &H8000000 + 2 + (&H20 * a) + 1)
            FilePut(1, battlebg(a)) ', bdpointer - &H8000000 + 3 + (&H20 * a) + 1)
            For d = 0 To 5
                FilePut(1, benemy(a, d), bdpointer - &H8000000 + 8 + (a << 5) + (d << 2) + 1)
                FilePut(1, benemy2(a, d)) ', bdpointer - &H8000000 + 10 + (&H20 * a) + (&H4 * d) + 1)
            Next
        Next
        'Putting in Room-specific Backgrounds data.
        For a = 0 To rooms
            FilePut(1, roombg(a), rbgdbase - &H8000000 + a + 1)
        Next
        'FileClose(1)
        MsgBox("Saved!")
    End Sub
#End Region
#End Region
    'Room-Specific Backgrounds Listbox
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        NumericUpDown4.Value = roombg(a)
    End Sub
    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        Dim a = ListBox1.SelectedIndex
        roombg(a) = NumericUpDown4.Value
        Panel5.BackgroundImage = System.Drawing.Image.FromFile("Battle Backgrounds\" & roombg(a) & ".PNG")
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        byte3(c) = ComboBox1.SelectedIndex
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        benemy2(c, 0) = ((ComboBox2.SelectedIndex << 1) And &HE) + (benemy2(c, 0) And &HFFF1)
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        benemy2(c, 1) = ((ComboBox3.SelectedIndex << 1) And &HE) + (benemy2(c, 1) And &HFFF1)
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        benemy2(c, 2) = ((ComboBox4.SelectedIndex << 1) And &HE) + (benemy2(c, 2) And &HFFF1)
    End Sub
    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        benemy2(c, 3) = ((ComboBox5.SelectedIndex << 1) And &HE) + (benemy2(c, 3) And &HFFF1)
    End Sub
    Private Sub ComboBox12_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox12.SelectedIndexChanged
        benemy2(c, 4) = ((ComboBox12.SelectedIndex << 1) And &HE) + (benemy2(c, 4) And &HFFF1)
    End Sub
    Private Sub ComboBox13_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox13.SelectedIndexChanged
        benemy2(c, 5) = ((ComboBox13.SelectedIndex << 1) And &HE) + (benemy2(c, 5) And &HFFF1)
    End Sub

    Private Sub Panel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.Click
        'arrangement(byte2(c), 0) += (&H2400)
        'Panel4.Refresh()
        'arrangement(byte2(c), 0) = (arrangement(byte2(c), 0) And &HFFF003FF) + (&H1000)
    End Sub
    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
        'Dim lownum As Short
        'Dim num2 = 0
        ''num1 = 0
        'Dim drawo(5) As Byte '= {{&HFF, &HFF, &HFF, &HFF, &HFF, &HFF}} 'Sort Order
        ''           Dim already(5) As Byte 'Already Listed
        'For a = 0 To 5
        '    For draword = 0 To 5

        '        If (arrangement(byte2(c), num2) >> 10 And &H3FF) > (arrangement(byte2(c), draword) >> 10 And &H3FF) Then num2 += 1
        '        'Array.Sort(
        '    Next
        '    drawo(num2) = a
        'Next
        For theY = 0 To &H3FF
            For num1 = 0 To 5
                If Not benemy2(c, num1) And 1 Then
                    If theY = (arrangement(byte2(c), num1) >> 10 And &H3FF) Then
                        Dim sfolder = "" '= spriteind(a) >> 12
                        Select Case spriteind(benemy(c, num1)) >> 12
                            Case 0 : GoTo zero
                            Case 3 : sfolder = "NPC Sprites"
                            Case 4 : sfolder = "Enemy Sprites"
                        End Select
                        If (spriteind(benemy(c, num1)) And &HFFF) < 343 Then
                            Dim spr As Image = System.Drawing.Image.FromFile(CurDir() & "\Sprites\" & sfolder & "\" & (spriteind(benemy(c, num1)) And &HFFF) & ".GIF")
                            e.Graphics.DrawImage(spr, New Point((arrangement(byte2(c), num1) And &H3FF) - (spr.Width / 2), _
                                                           (arrangement(byte2(c), num1) >> 10 And &H3FF) - spr.Height))
                        End If
                    End If
                End If
                'Dim sfolder = "" '= spriteind(a) >> 12
                'Select Case spriteind(benemy(c, num1)) >> 12
                '    Case 0 : GoTo zero
                '    Case 3 : sfolder = "NPC Sprites"
                '    Case 4 : sfolder = "Enemy Sprites"
                'End Select
                'If Not benemy2(c, drawo(num1)) And 1 Then
                '    If (spriteind(benemy(c, drawo(num1))) And &HFFF) < 343 Then
                '        Dim spr As Image = System.Drawing.Image.FromFile(CurDir() & "\Sprites\" & sfolder & "\" & (spriteind(benemy(c, drawo(num1))) And &HFFF) & ".GIF")
                '        e.Graphics.DrawImage(spr, New Point((arrangement(byte2(c), drawo(num1)) And &H3FF) - (spr.Width / 2), _
                '                                       (arrangement(byte2(c), drawo(num1)) >> 10 And &H3FF) - spr.Height))
                '    End If
                'End If
zero:           'num2 = 0
            Next
        Next
    End Sub
End Class