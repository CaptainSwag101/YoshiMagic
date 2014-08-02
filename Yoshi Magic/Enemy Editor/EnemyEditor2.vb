Public Class EnemyEditor2
#Region " Enemy Editor "
#Region " Enemies Tab "
#Region " Dims "
    Dim lbobjcol1(20, 200) As String 'ListBox.ObjectCollection

    Dim edpointer As Integer
    Dim enemies = &HBC
    Dim attributes = &H2B
    'Dim edb(enemies, 10) As Integer
    Dim enemydb(enemies, attributes) As Byte
    Dim namepointer(enemies) As Integer
    'Dim Nametxt(enemies, 16) As Byte
    Dim letter As Byte
    'Dim listind(enemies) As Short 'THIS IS USED WITH FIND
    Dim listorder(enemies) 'Will be used for Sorting.
    Dim EnemyName(enemies) As String
    Dim spritepointer(enemies) As Integer
    Dim spriteind(enemies) As Short
    Dim unkp(enemies) As Short
    Dim HP(enemies) As Short
    Dim DEF(enemies) As Short
    Dim SPD(enemies) As Short
    Dim EXP(enemies) As Short
    Dim CNS(enemies) As Short
    Dim ITM(enemies) As Short
    Dim RITM(enemies) As Short
    'Dim badgespointer(43) As Integer 'Pointers in Badges Database leading to Badge text.
    'Dim bdgchr(43, 15) As Byte 'Each letter of each badge.
    Dim BadgeName(43) As String
    'Dim gearpointer(45) As Integer 'Pointers in Gears Database leading to Gear text.
    'Dim gearchr(45, 15) As Byte 'Each letter of each gear.
    Dim GearName(45) As String
    'Dim beanspointer(3) As Integer 'Pointers in Beans Database leading to Bean text.
    'Dim beanchr(3, 15) As Byte 'Each letter of each bean.
    Dim BeanName(3) As String
    'Dim itempointer(24) As Integer 'Pointers in Item Database leading to Item text.
    'Dim itmchr(24, 15) As Byte 'Each letter of each item.
    Dim ItemName(24) As String
    Dim unkval(enemies) As Byte
    Dim repelval(enemies) As Byte
    Dim firethunder(enemies) As Byte
    Dim scriptp(enemies) As Integer

    Dim sprdata(enemies, 3) As Integer 'Used for repointering
    Dim abilitypointer(enemies) As Integer
    Dim abildata(enemies, 7, 4) As Integer 'Used for repointering
    Dim abilbyte8(enemies, 7) As Byte
    Dim abilbyte9(enemies, 7) As Byte
    Dim catype(enemies, 7) As Short '(Byte)
    Dim power(enemies, 7) As Short
    Dim abilshortE(enemies, 7) As Short
    Dim abilshort10(enemies, 7) As Short
    Dim abilshort12(enemies, 7) As Short
#End Region
#Region " Load "
    'Private Sub EnemyEditor2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    If opened = 1 Then
    '        Dim saveclose = MsgBox("Would you like to save before closing?", MsgBoxStyle.YesNoCancel) '6,7,2
    '        'MsgBox(saveclose)
    '        If saveclose = MsgBoxResult.Yes Then
    '            'Timer2.Start() 'This will futurely activate saving?
    '        End If
    '        'MsgBoxResult.No does not do anything special and the form closes.
    '        If saveclose = MsgBoxResult.Cancel Then
    '            e.Cancel = True
    '        End If
    '    End If
    'End Sub
    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        If Form1.version = "NA" Then
            FileGet(1, edpointer, &H7B514 + 1)
        ElseIf Form1.version = "EU" Then
            'vfix = &H '7BD50' &H19D0 '&H10D8
            FileGet(1, edpointer, &H7BD50 + 1)
        ElseIf Form1.version = "J" Then
            'enemies += 3
            FileGet(1, edpointer, &H7C1AC + 1)
        ElseIf Form1.version = "NADEMO" Then
            edpointer = &H8322608
        Else
            MsgBox("I am sorry, but your ROM is not compatible to this part of the editor.")
            Me.Close()
        End If
        'Seek(1, edpointer - &H8000000 + 1)
        For a = 0 To enemies
            'listind(a) = a
            'Enemy Database: 08500A98 ; 08502468
            'ToolStripStatusLabel1.Text = "Enemy Database: " & Hex(edpointer)
            '0007B514 ; 0007B61C ; 00081DD0 ; 000F5D38 ; 000F5F28 ; 00101870
            'Grab Enemy Data (8-bit) for moving database.
            'Seek(1, edpointer - &H8000000 + 1)
            'For b = 0 To 10
            '    FileGet(1, edb(a, b))
            'Next
            'For b = 0 To attributes
            '    FileGet(1, enemydb(a, b), edpointer - &H8000000 + (&H2C * a) + b + 1)
            'Next
            FileGet(1, namepointer(a), edpointer - &H8000000 + (&H2C * a) + 1)
            FileGet(1, unkp(a)) ', edpointer - &H8000000 + &H4 + (&H2C * a) + 1)
            FileGet(1, HP(a)) ', edpointer - &H8000000 + &H6 + (&H2C * a) + 1)
            FileGet(1, spritepointer(a)) ', edpointer - &H8000000 + (&H2C * a) + 8 + 1)
            FileGet(1, abilitypointer(a)) ', edpointer - &H8000000 + (&H2C * a) + &HC + 1)
            Seek(1, Loc(1) + 8 + 1) 'Skipping 2 pointers
            FileGet(1, DEF(a)) ', edpointer - &H8000000 + &H18 + (&H2C * a) + 1)
            FileGet(1, SPD(a)) ', edpointer - &H8000000 + &H1A + (&H2C * a) + 1)
            FileGet(1, unkval(a)) ', edpointer - &H8000000 + &H1C + (&H2C * a) + 1)
            FileGet(1, repelval(a)) ', edpointer - &H8000000 + &H1D + (&H2C * a) + 1)
            FileGet(1, firethunder(a)) ', edpointer - &H8000000 + &H1E + (&H2C * a) + 1)
            Seek(1, Loc(1) + 1 + 1) '00 Byte
            FileGet(1, scriptp(a)) ', edpointer - &H8000000 + &H20 + (&H2C * a) + 1)
            FileGet(1, EXP(a)) ', edpointer - &H8000000 + &H24 + (&H2C * a) + 1)
            FileGet(1, CNS(a)) ', edpointer - &H8000000 + &H26 + (&H2C * a) + 1)
            FileGet(1, RITM(a)) ', edpointer - &H8000000 + &H28 + (&H2C * a) + 1)
            FileGet(1, ITM(a)) ', edpointer - &H8000000 + &H2A + (&H2C * a) + 1)
            'Getting Enemy Names
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            Seek(1, namepointer(a) - &H8000000 + 1)
            'EnemyName(a) = LineInput(1)
            For n = 0 To 96
                'Do
                'FileGet(1, Nametxt(a, n), namepointer(a) - &H8000000 + n + 1)
                'If Nametxt(a, n) = 0 Then Exit For
                'EnemyName(a) += Chr(Nametxt(a, n)) 'Store Enemy Names
                FileGet(1, letter) ', namepointer(a) - &H8000000 + n + 1)
                If letter = 0 Then Exit For
                EnemyName(a) += Chr(letter) 'Store Enemy Names
                'Loop
            Next n
            listorder(a) = a 'Sort order is numeral.
            ListBox1.Items.Add(Hex(a) & " - " & EnemyName(a)) 'Adds the list of enemies to List Box.
            FileGet(1, spriteind(a), spritepointer(a) - &H8000000 + 1)
            Seek(1, spritepointer(a) - &H8000000 + 1)
            For ints = 0 To 3
                FileGet(1, sprdata(a, ints)) ', spritepointer(a) - &H8000000 + (ints << 2) + 1)
            Next
            For abils = 0 To 7
                For ints = 0 To 4
                    FileGet(1, abildata(a, abils, ints), abilitypointer(a) - &H8000000 + (abils * &H14) + (ints << 2) + 1)
                Next
                FileGet(1, catype(a, abils), abilitypointer(a) - &H8000000 + (abils * &H14) + &HA + 1)
                FileGet(1, power(a, abils)) ', abilitypointer(a) - &H8000000 + (abils * &H14) + &HC + 1)
                FileGet(1, abilshortE(a, abils))
                FileGet(1, abilshort10(a, abils))
                FileGet(1, abilshort12(a, abils))
                'FileGet(1, eoabils(a, abils), abilitypointer(a) - &H8000000 + (abils * &H14) + &H13 + 1)
                If abilshort12(a, abils) < 0 Then Exit For
            Next
        Next a
        'Get Item Names ***In the future: Follow Item Database Pointer***
        If Form1.version = "NA" Then
            name_string(43, &H3BD844, &H14, BadgeName)
            name_string(45, &H3BE67C, &H14, GearName)
            name_string(3, &H3BCDC4, &HC, BeanName)
            name_string(24, &H3BBDDC, &H10, ItemName)
        ElseIf Form1.version = "EU" Then



            'FileGet(1, itempointer(b), &H3BD770 + (&H10 * b) + 1)
        ElseIf Form1.version = "J" Then



            'FileGet(1, itempointer(b), &H3A56B8 + &HC + (&H10 * b) + 1)
        End If
        'FileClose(1)
        ListBox1.SelectedIndex = 0
        '' COMMENTS
        If Form1.version = "J" Then MsgBox("I think the list for this version is suppose to go up to &HBF, but here it stops at &HBC.")
    End Sub
    Sub name_string(ByVal quantity As Integer, ByVal offset As Integer, ByVal datasize As Integer, ByVal store() As String)
        Dim pointer As Integer
        For b = 0 To quantity
            FileGet(1, pointer, offset + (datasize * b) + 1)
            FileGet(1, pointer, pointer - &H8000000 + 1)
            FileGet(1, pointer, pointer - &H8000000 + 1)
            Seek(1, pointer - &H8000000 + 1)
            For n = 0 To 16
                FileGet(1, letter) ', pointer - &H8000000 + n + 1)
                If letter = 0 Then Exit For
                store(b) += Chr(letter) 'Store Names.
            Next
        Next
    End Sub
    '    Sub unused()
    '        For b = 0 To 43
    '            Try
    '                If Form1.version = "NA" Then
    '                    FileGet(1, badgespointer(b), &H3BD844 + (&H14 * b) + 1)
    '                ElseIf Form1.version = "EU" Then
    '                End If
    '                FileGet(1, badgespointer(b), badgespointer(b) - &H8000000 + 1)
    '                FileGet(1, badgespointer(b), badgespointer(b) - &H8000000 + 1)
    '                For n = 0 To 15
    '                    FileGet(1, bdgchr(b, n), badgespointer(b) - &H8000000 + n + 1)
    '                    If bdgchr(b, n) = 0 Then Exit For
    '                    BadgeName(b) += Chr(bdgchr(b, n)) 'Store Item Names.
    '                Next n
    '            Catch
    '            End Try
    '        Next
    '        For b = 0 To 45
    '            Try
    '                If Form1.version = "NA" Then
    '                    FileGet(1, gearpointer(b), &H3BE67C + (&H14 * b) + 1)
    '                ElseIf Form1.version = "EU" Then
    '                End If
    '                FileGet(1, gearpointer(b), gearpointer(b) - &H8000000 + 1)
    '                FileGet(1, gearpointer(b), gearpointer(b) - &H8000000 + 1)
    '                For n = 0 To 15
    '                    FileGet(1, gearchr(b, n), gearpointer(b) - &H8000000 + n + 1)
    '                    If gearchr(b, n) = 0 Then GoTo Exitb
    '                    GearName(b) += Chr(gearchr(b, n)) 'Store Item Names.
    '                Next n
    '            Catch
    '            End Try
    'exitb:  Next
    '        For b = 0 To 3
    '            Try
    '                If Form1.version = "NA" Then
    '                    FileGet(1, beanspointer(b), &H3BCDC4 + (&HC * b) + 1)
    '                ElseIf Form1.version = "EU" Then
    '                End If
    '                FileGet(1, beanspointer(b), beanspointer(b) - &H8000000 + 1)
    '                FileGet(1, beanspointer(b), beanspointer(b) - &H8000000 + 1)
    '                For n = 0 To 15
    '                    FileGet(1, beanchr(b, n), beanspointer(b) - &H8000000 + n + 1)
    '                    If beanchr(b, n) = 0 Then GoTo Exitc
    '                    BeanName(b) += Chr(beanchr(b, n)) 'Store Item Names.
    '                Next n
    '            Catch
    '            End Try
    'exitc:  Next
    '        'itemnames(ItemName(0))
    '        'MsgBox(ItemName(0))
    '        For b = 0 To 24
    '            If Form1.version = "NA" Then
    '                FileGet(1, itempointer(b), &H3BBDDC + (&H10 * b) + 1)
    '            ElseIf Form1.version = "EU" Then
    '                FileGet(1, itempointer(b), &H3BD770 + (&H10 * b) + 1)
    '            ElseIf Form1.version = "J" Then
    '                FileGet(1, itempointer(b), &H3A56B8 + &HC + (&H10 * b) + 1)
    '            End If
    '            FileGet(1, itempointer(b), itempointer(b) - &H8000000 + 1)
    '            FileGet(1, itempointer(b), itempointer(b) - &H8000000 + 1)
    '            For n = 0 To 15
    '                FileGet(1, itmchr(b, n), itempointer(b) - &H8000000 + n + 1)
    '                If itmchr(b, n) = 0 Then GoTo Exiti
    '                ItemName(b) += Chr(itmchr(b, n)) 'Store Item Names.
    '            Next n
    'exiti:  Next
    '    End Sub
#End Region
#Region " Display Data "
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        Dim a = listorder(ListBox1.SelectedIndex)
        Try
            Label22.Text = "Enemy Database: " & Hex(edpointer) & _
            "  Enemy Offset: " & Hex(edpointer + (&H2C * a))
            ' 'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
            'Place data in textboxes. 08500A98
            TextBox1.Text = EnemyName(a) 'ENEMY NAMES
            NumericUpDown1.Value = spriteind(a)
            'TextBox8.Text = edb(a, 1) And &HFFFF
            NumericUpDown2.Value = unkp(a)
            'TextBox2.Text = edb(a, 1) >> 16
            NumericUpDown3.Value = HP(a)
            'TextBox3.Text = edb(a, 6) And &HFFFF
            NumericUpDown4.Value = DEF(a)
            'TextBox4.Text = edb(a, 6) >> 16
            NumericUpDown5.Value = SPD(a)
            'TextBox5.Text = edb(a, 9) And &HFFFF
            NumericUpDown6.Value = EXP(a)
            'TextBox6.Text = edb(a, 9) >> 16
            NumericUpDown7.Value = CNS(a)
            TextBox14.Text = Hex(spritepointer(a))
            TextBox13.Text = Hex(abilitypointer(a))
            'TextBox9.Text = Hex(scriptp(a))
            Try
                ComboBox1.Items.Clear()
                Select Case ITM(a) >> 12
                    Case 0 'If ITM(a) < &H1000 Then
                        ComboBox1.Items.Add("0 - None")
                    Case 1 'ElseIf ITM(a) < &H2000 Then
                        For b = 0 To 43
                            ComboBox1.Items.Add(Hex(b) & " - " & BadgeName(b))
                        Next
                    Case 2 'ElseIf ITM(a) < &H3000 Then
                        For b = 0 To 45
                            ComboBox1.Items.Add(Hex(b) & " - " & GearName(b))
                        Next
                    Case 3 'ElseIf ITM(a) < &H4000 Then
                        For b = 0 To 3
                            ComboBox1.Items.Add(Hex(b) & " - " & BeanName(b))
                        Next
                    Case 4 'ElseIf ITM(a) < &H5000 Then
                        For b = 0 To 24
                            ComboBox1.Items.Add(Hex(b) & " - " & ItemName(b))
                        Next
                End Select 'End If
                ComboBox1.SelectedIndex = ITM(a) >> 5 And &H3F ' - (ITM(a) >> 12 << 7)
            Catch
                ComboBox1.SelectedIndex = -1
            End Try
            TrackBar1.Value = ITM(a) And &H1F ' - (ITM(a) >> 5 << 5)
            Try
                ComboBox12.Items.Clear()
                Select Case RITM(a) >> 12
                    Case 0 'If RITM(a) < &H1000 Then
                        ComboBox12.Items.Add("0 - None")
                    Case 1 'ElseIf RITM(a) < &H2000 Then
                        For b = 0 To 43
                            ComboBox12.Items.Add(Hex(b) + " - " + BadgeName(b))
                        Next
                    Case 2 'ElseIf RITM(a) < &H3000 Then
                        For b = 0 To 45
                            ComboBox12.Items.Add(Hex(b) + " - " + GearName(b))
                        Next
                    Case 3 'ElseIf RITM(a) < &H4000 Then
                        For b = 0 To 3
                            ComboBox12.Items.Add(Hex(b) + " - " + BeanName(b))
                        Next
                    Case 4 'ElseIf RITM(a) < &H5000 Then
                        For b = 0 To 24
                            ComboBox12.Items.Add(Hex(b) + " - " + ItemName(b))
                        Next
                End Select 'End If
                ComboBox12.SelectedIndex = RITM(a) >> 5 And &H3F '- (RITM(a) >> 12 << 7)
            Catch
                ComboBox12.SelectedIndex = -1
            End Try
            TrackBar2.Value = RITM(a) And &H1F
            ComboBox5.SelectedIndex = unkval(a) >> 6 'Delete
            CheckBox1.Checked = unkval(a) >> 7 'Group Leader
            CheckBox2.Checked = unkval(a) >> 6 And &H1 'Unknown
            ComboBox8.SelectedIndex = unkval(a) >> 4 And &H3
            ComboBox9.SelectedIndex = unkval(a) >> 2 And &H3
            ComboBox10.SelectedIndex = unkval(a) And &H3
            ComboBox2.SelectedIndex = repelval(a) >> 6
            ComboBox3.SelectedIndex = repelval(a) >> 4 And &H3
            ComboBox4.SelectedIndex = repelval(a) >> 2 And &H3
            'ComboBox5.SelectedIndex = repelval(a) >> 0 And &H3
            ComboBox6.SelectedIndex = firethunder(a) >> 6
            ComboBox7.SelectedIndex = firethunder(a) >> 4 And &H3
            'TextBox13.Text = repelval(a) & " - " & Hex(repelval(a))
            ''FileClose(1)
            ' Catch
            'MsgBox(Err.Description, , "Error")
            ''FileClose(1)
            ' End Try
            ListBox2.Items.Clear()
            For abils = 0 To 7
                ListBox2.Items.Add(Hex(abils))
                If abilshort12(a, abils) < 0 Then Exit For
            Next
            ListBox2.SelectedIndex = 0
        Catch
        End Try
    End Sub
#End Region
#Region " Find "
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For FIND = ListBox1.SelectedIndex + 1 To ListBox1.Items.Count - 1
            If EnemyName(listorder(FIND)).Contains(TextBox7.Text) Then ListBox1.SelectedIndex = FIND : Exit Sub
        Next
        MsgBox("Yoshi can not find any more matches.")
    End Sub
#End Region
#Region " Item Rewards Shortcut Menu "
    Private Sub NoneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoneToolStripMenuItem.Click
        'Dim a = ListBox1.SelectedIndex
        ITM(listorder(ListBox1.SelectedIndex)) = &H0
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("0 - None")
        ComboBox1.SelectedIndex = 0
        'ActiveControl.Controls..items.clear()
    End Sub
    Private Sub BadgesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BadgesToolStripMenuItem.Click
        'Dim a = ListBox1.SelectedIndex
        ITM(listorder(ListBox1.SelectedIndex)) = &H1000
        ComboBox1.Items.Clear()
        For b = 0 To 43
            ComboBox1.Items.Add(Hex(b) + " - " + BadgeName(b))
        Next
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub GearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GearToolStripMenuItem.Click
        'Dim a = ListBox1.SelectedIndex
        ITM(listorder(ListBox1.SelectedIndex)) = &H2000
        ComboBox1.Items.Clear()
        For b = 0 To 45
            ComboBox1.Items.Add(Hex(b) + " - " + GearName(b))
        Next
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub BeansToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeansToolStripMenuItem.Click
        'Dim a = ListBox1.SelectedIndex
        ITM(listorder(ListBox1.SelectedIndex)) = &H3000
        ComboBox1.Items.Clear()
        For b = 0 To 3
            ComboBox1.Items.Add(Hex(b) + " - " + BeanName(b))
        Next
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub ItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripMenuItem.Click
        'Dim a = ListBox1.SelectedIndex
        ITM(listorder(ListBox1.SelectedIndex)) = &H4000
        ComboBox1.Items.Clear()
        For b = 0 To 24
            ComboBox1.Items.Add(Hex(b) + " - " + ItemName(b))
        Next
        ComboBox1.SelectedIndex = 0
    End Sub
#End Region
#Region " Trackbar "
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            ToolTip1.SetToolTip(TrackBar1, Math.Round(TrackBar1.Value / &H1F * 10000) / 100 & "%")
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region " Store on change "
    'Dim c = ListBox1.SelectedIndex
    Dim enemytext2 As New Bitmap(104, 21)
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        EnemyName(listorder(ListBox1.SelectedIndex)) = TextBox1.Text
        'Try : FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary) : Catch : End Try
        Dim enemytext As New Bitmap(104, 21)
        Dim txtx As Short = 96 'Text at (96, 6)
        Dim spaces(TextBox1.Text.Length - 1) As Byte
        'Decrement text by charwidth-1.
        For a = 0 To TextBox1.Text.Length - 1
            'MsgBox(Asc(TextBox1.Text.Chars(TextBox1.Text.Length - 1 - a)))
            FileGet(1, spaces(a), &H51E120 + (Asc(TextBox1.Text.Chars(a)) >> 1) + 1)
            spaces(a) = (spaces(a) >> ((Asc(TextBox1.Text.Chars(a)) And 1) << 2)) And &HF
            txtx -= (spaces(a) + 1)
        Next
        If txtx < 0 Then TextBox1.ForeColor = Color.Red : GoTo cleanup
        TextBox1.ForeColor = Color.Black
        Dim fcolors() As Color = {Color.Transparent, Color.White, Color.FromArgb(255, 40, 96, 240)}
        ': fcolors(0) = Color.Transparent : fcolors(1) = Color.White : fcolors(2) = Color.FromArgb(255, 40, 96, 240)
        For a = 0 To TextBox1.Text.Length - 1
            For b = 0 To 2
                Dim cblock As Long : Dim colorbyte As Byte
                FileGet(1, cblock, &H51E1A0 + (Asc(TextBox1.Text.Chars(a)) * &H18) + (b << 3) + 1)
                For x = 0 To 7
                    For y = 0 To 3
                        colorbyte = ((cblock >> (x << 2) >> y) And 1) + (((cblock >> &H20) >> (x << 2) >> y) And 1)
                        enemytext.SetPixel(txtx + x, 6 + (b << 2) + y, fcolors(colorbyte))
                    Next
                Next
            Next
            txtx += spaces(a) + 1
        Next
        'Panel3.CreateGraphics.DrawImage(enemytext, New Point(0, 0))
cleanup: enemytext2 = enemytext
        Panel3.Refresh()
        'Try : FileClose(1) : Catch : End Try
    End Sub
    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        unkp(listorder(ListBox1.SelectedIndex)) = NumericUpDown2.Value 'TextBox8.Text
        'edb(c, 1) = (edb(c, 1) And &HFFFF0000) + TextBox8.Text
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        HP(listorder(ListBox1.SelectedIndex)) = NumericUpDown3.Value
        'edb(c, 1) = (edb(c, 1) And &HFFFF) + (TextBox2.Text << 16)
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        DEF(listorder(ListBox1.SelectedIndex)) = NumericUpDown4.Value
        'edb(c, 6) = (edb(c, 6) And &HFFFF0000) + TextBox3.Text
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        SPD(listorder(ListBox1.SelectedIndex)) = NumericUpDown5.Value
        'edb(c, 6) = (edb(c, 6) And &HFFFF) + (TextBox4.Text << 16)
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        EXP(listorder(ListBox1.SelectedIndex)) = NumericUpDown6.Value
        'edb(c, 9) = (edb(c, 9) And &HFFFF0000) + TextBox5.Text
    End Sub
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown7.ValueChanged
        CNS(listorder(ListBox1.SelectedIndex)) = NumericUpDown7.Value
        'edb(c, 9) = (edb(c, 9) And &HFFFF) + (TextBox6.Text << 16)
    End Sub
#End Region
#Region " Save "
    Private Sub SaveROMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        'Handles SaveToolStripMenuItem1.Click
        'On Error GoTo err
        'START PUTTING DATA IN ROOM
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'Automatic Repointering of Enemy Database
        'Dim zero As Byte = 0
        'Dim edpointerold As Integer = edpointer
        'Try
        '    'edpointer = InputBox("Move Enemy Database to offset:", "Repointer", "&H" & Hex(edpointer))
        'Catch
        '    'edpoter = edpointerold 'Just incase
        '    'FileClose(1)
        '    MsgBox("Your data was not saved.")
        '    Exit Sub
        'End Try
        'Automatic Repointering of Enemy Database
        'If Form1.version = "NA" Then
        '    FilePut(1, edpointer, &H7B514 + 1)
        '    FilePut(1, edpointer, &H7B61C + 1)
        '    FilePut(1, edpointer, &H81DD0 + 1)
        '    FilePut(1, edpointer, &HF5D38 + 1)
        '    FilePut(1, edpointer, &HF5F28 + 1)
        '    FilePut(1, edpointer, &H101870 + 1)
        '    FilePut(1, edpointer + 8, &H806F8 + 1)
        '    FilePut(1, edpointer + 8, &H80748 + 1)
        '    FilePut(1, edpointer + 8, &H8079C + 1)
        'ElseIf Form1.version = "EU" Then
        '    FilePut(1, edpointer, &H7BD50 + 1)
        '    FilePut(1, edpointer, &H7BE58 + 1)
        '    FilePut(1, edpointer, &H8260C + 1)
        '    FilePut(1, edpointer, &HF6574 + 1)
        '    FilePut(1, edpointer, &HF6764 + 1)
        '    FilePut(1, edpointer, &H1020DC + 1)
        '    FilePut(1, edpointer + 8, &H80F34 + 1)
        '    FilePut(1, edpointer + 8, &H80F84 + 1)
        '    FilePut(1, edpointer + 8, &H80FD8 + 1)
        'ElseIf Form1.version = "J" Then
        '    FilePut(1, edpointer, &H7C1AC + 1)
        '    FilePut(1, edpointer, &H7C2B4 + 1)
        '    FilePut(1, edpointer, &H82AD4 + 1)
        '    FilePut(1, edpointer, &HED3FC + 1)
        '    FilePut(1, edpointer, &HED5EC + 1)
        '    FilePut(1, edpointer, &HF90B4 + 1)
        '    FilePut(1, edpointer + 8, &H813D4 + 1)
        '    FilePut(1, edpointer + 8, &H81424 + 1)
        '    FilePut(1, edpointer + 8, &H81478 + 1)
        'Else
        '    'MsgBox("To avoid corrupting your ROM, your data was not save. Please try a compatible version.")
        'End If
        '0007B514 ; 0007B61C ; 00081DD0 ; 000F5D38 ; 000F5F28 ; 00101870
        '000806F8 ; 00080748 ; 0008079C
        'Label22.Text = "Enemy Database: " & Hex(edpointer)
        'Label22.Update()
        'For a = 0 To ListBox1.Items.Count - 1 '&HBC 'enemies ***************************************
        '    Try
        '        'Delete the old database since we don't need it anymore.
        '        'This is in a separate FOR method for a reason.
        '        For b = 0 To &H2B 'attributes
        '            FilePut(1, zero, edpointerold - &H8000000 + (&H2C * a) + b + 1)
        '        Next
        '    Catch
        '    End Try ********************************************************************************
        'Next
        'Detect an ammount of zeroes to find where to auto-point database.
        'Not coded yet.
        'Dim namespacing = 0 ' I used this so each name can be directly after each other.
        Dim repointer = &H821B380
        Dim nameoffset(enemies) As Integer
        For a = 0 To &HBC 'enemies
            'Try
            'Paste the old location of the database over to the new location.
            'For b = 0 To &H2B 'attributes
            '    FilePut(1, enemydb(a, b), edpointer - &H8000000 + (&H2C * a) + b + 1)
            'Next
            'START PUTTING IN ENEMY NAME DATA (plus Automatic Repointering to Enemy Names)
            FileGet(1, namepointer(a), edpointer - &H8000000 + (&H2C * a) + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            'Dim repointer As Integer = &H821B380 + (a * &H11)
            For b = 0 To a
                If b = a Then 'No names found, so add name to end.
                    FilePut(1, repointer, namepointer(a) - &H8000000 + 1)
                    nameoffset(a) = repointer
                    repointer += EnemyName(a).Length + 1 'sets the offset for the NEXT repointering.

                    FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
                    Seek(1, namepointer(a) - &H8000000 + 1)
                    'EnemyName(a) = FileSystem.WriteLine(1)
                    'FilePut(1, EnemyName(a))
                    For n = 0 To EnemyName(a).Length '16
                        'If n < EnemyName(a).Length Then Nametxt(a, n) = Asc(EnemyName(a).Chars(n))
                        'FilePut(1, Nametxt(a, n), namepointer(a) - &H8000000 + n + 1)
                        If n < EnemyName(a).Length Then letter = Asc(EnemyName(a).Chars(n)) Else letter = 0
                        FilePut(1, letter) ', namepointer(a) - &H8000000 + n + 1)
                    Next n
                    Exit For
                End If
                If EnemyName(a) = EnemyName(b) Then 'Avoid Duplicate Names
                    FilePut(1, nameoffset(b), namepointer(a) - &H8000000 + 1)
                    nameoffset(a) = nameoffset(b)
                    Exit For
                End If
            Next
            FilePut(1, unkp(a), edpointer - &H8000000 + &H4 + (&H2C * a) + 1)
            FilePut(1, HP(a)) ', edpointer - &H8000000 + &H6 + (&H2C * a) + 1)

            FilePut(1, DEF(a), edpointer - &H8000000 + &H18 + (&H2C * a) + 1)
            FilePut(1, SPD(a)) ', edpointer - &H8000000 + &H1A + (&H2C * a) + 1)
            FilePut(1, unkval(a)) ', edpointer - &H8000000 + &H1C + (&H2C * a) + 1)
            FilePut(1, repelval(a)) ', edpointer - &H8000000 + &H1D + (&H2C * a) + 1)
            FilePut(1, firethunder(a)) ', edpointer - &H8000000 + &H1E + (&H2C * a) + 1)

            FilePut(1, EXP(a), edpointer - &H8000000 + &H24 + (&H2C * a) + 1)
            FilePut(1, CNS(a)) ', edpointer - &H8000000 + &H26 + (&H2C * a) + 1)
            FilePut(1, RITM(a)) ', edpointer - &H8000000 + &H28 + (&H2C * a) + 1)
            FilePut(1, ITM(a)) ', edpointer - &H8000000 + &H2A + (&H2C * a) + 1)
            'Sprites Section
            spritepointer(a) = &H8D00000 + (a * &H10)
            FilePut(1, spritepointer(a), edpointer - &H8000000 + (&H2C * a) + 8 + 1)
            For ints = 0 To 3
                FilePut(1, sprdata(a, ints), spritepointer(a) - &H8000000 + (ints << 2) + 1)
            Next
            FilePut(1, spriteind(a), spritepointer(a) - &H8000000 + 1)
            'Abilities Section
            abilitypointer(a) = &H8D10000 + (a * &HA0)
            FilePut(1, abilitypointer(a), edpointer - &H8000000 + (&H2C * a) + &HC + 1)
            For abils = 0 To 7
                For ints = 0 To 4
                    FilePut(1, abildata(a, abils, ints), abilitypointer(a) - &H8000000 + (abils * &H14) + (ints << 2) + 1)
                Next
                FilePut(1, catype(a, abils), abilitypointer(a) - &H8000000 + (abils * &H14) + &HA + 1)
                FilePut(1, power(a, abils)) ', abilitypointer(a) - &H8000000 + (abils * &H14) + &HC + 1)
                FilePut(1, abilshortE(a, abils))
                FilePut(1, abilshort10(a, abils))
                FilePut(1, abilshort12(a, abils))
                'FilePut(1, eoabils(a, abils), abilitypointer(a) - &H8000000 + (abils * &H14) + &H13 + 1)
                If abilshort12(a, abils) < 0 Then Exit For
            Next
            'SendKeys.SendWait("%{PRTSC}")
        Next a
        'FileClose(1)
        MsgBox("Yoshi has successfully saved your data.")
        Exit Sub
err:    'FileClose(1)
    End Sub
#End Region
#End Region
#End Region
    'Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
    '    AboutBox1.Show()
    'End Sub
    'Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'SaveFD.Filter = "GBA file(*.gba)|*.gba"
    'SaveFD.ShowDialog()
    'My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, SaveFD.FileName, True)
    'OpenFileDialog1.FileName = SaveFD.FileName
    'End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'MsgBox(ContextMenuStrip1)
        'Dim a = ListBox1.SelectedIndex
        RITM(listorder(ListBox1.SelectedIndex)) = &H0
        ComboBox12.Items.Clear()
        ComboBox12.Items.Add("0 - None")
        ComboBox12.SelectedIndex = 0
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        'Dim a = ListBox1.SelectedIndex
        RITM(listorder(ListBox1.SelectedIndex)) = &H1000
        ComboBox12.Items.Clear()
        For b = 0 To 43
            ComboBox12.Items.Add(Hex(b) + " - " + BadgeName(b))
        Next
        ComboBox12.SelectedIndex = 0
    End Sub
    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        'Dim a = ListBox1.SelectedIndex
        RITM(listorder(ListBox1.SelectedIndex)) = &H2000
        ComboBox12.Items.Clear()
        For b = 0 To 45
            ComboBox12.Items.Add(Hex(b) + " - " + GearName(b))
        Next
        ComboBox12.SelectedIndex = 0
    End Sub
    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        'Dim a = ListBox1.SelectedIndex
        RITM(listorder(ListBox1.SelectedIndex)) = &H3000
        ComboBox12.Items.Clear()
        For b = 0 To 3
            ComboBox12.Items.Add(Hex(b) + " - " + BeanName(b))
        Next
        ComboBox12.SelectedIndex = 0
    End Sub
    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        'Dim a = ListBox1.SelectedIndex
        RITM(listorder(ListBox1.SelectedIndex)) = &H4000
        ComboBox12.Items.Clear()
        For b = 0 To 24
            ComboBox12.Items.Add(Hex(b) + " - " + ItemName(b))
        Next
        ComboBox12.SelectedIndex = 0
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        ITM(a) = (ITM(a) And &HF01F) + (ComboBox1.SelectedIndex << 5)
    End Sub
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Dim a = listorder(ListBox1.SelectedIndex)
        ITM(a) = (ITM(a) And &HFFE0) + TrackBar1.Value
    End Sub
    Private Sub ComboBox12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox12.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        RITM(a) = (RITM(a) And &HF01F) + (ComboBox12.SelectedIndex << 5)
    End Sub
    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Dim a = listorder(ListBox1.SelectedIndex)
        RITM(a) = (RITM(a) And &HFFE0) + TrackBar2.Value
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        repelval(a) = (repelval(a) And &H3F) + (ComboBox2.SelectedIndex << 6)
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        repelval(a) = (repelval(a) And &HCF) + (ComboBox3.SelectedIndex << 4)
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        repelval(a) = (repelval(a) And &HF3) + (ComboBox4.SelectedIndex << 2)
    End Sub
    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        firethunder(a) = (firethunder(a) And &H3F) + (ComboBox6.SelectedIndex << 6)
    End Sub
    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        firethunder(a) = (firethunder(a) And &HCF) + (ComboBox7.SelectedIndex << 4)
    End Sub
    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        unkval(a) = (unkval(a) And &H3F) + (ComboBox5.SelectedIndex << 6)
    End Sub
    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        unkval(a) = (unkval(a) And &HCF) + (ComboBox8.SelectedIndex << 4)
    End Sub
    Private Sub ComboBox9_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        unkval(a) = (unkval(a) And &HF3) + (ComboBox9.SelectedIndex << 2)
    End Sub
    Private Sub ComboBox10_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox10.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        unkval(a) = (unkval(a) And &HFC) + ComboBox10.SelectedIndex
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Dim a = listorder(ListBox1.SelectedIndex)
        spriteind(a) = NumericUpDown1.Value
        'Panel2.BackgroundImage = ImageList1.Images.Item(spriteind(a) - &H4000)
        Dim sfolder = "" '= spriteind(a) >> 12
        Select Case spriteind(a) >> 12
            Case 0
                GoTo three
            Case 3
                sfolder = "NPC Sprites"
            Case 4
                sfolder = "Enemy Sprites"
            Case Else
                Exit Sub
        End Select
        If (spriteind(a) And &HFFF) < 343 Then
            Panel2.BackgroundImage = System.Drawing.Image.FromFile(CurDir() & "\Sprites\" & sfolder & "\" & (spriteind(a) And &HFFF) & ".GIF")
        Else
            Panel2.BackgroundImage = Nothing
        End If
        Exit Sub
three:  Panel2.BackgroundImage = ImageList2.Images.Item(0) 'spriteind(a) - &H4000)
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        'Dim c As Byte = 0 'Listbox3 # (ASM Block #)
        'Dim d As Byte = 0 'Listbox4 # (ASM Instruction #)
        ' Ability Data
        ComboBox13.SelectedIndex = catype(a, b)
        NumericUpDown8.Value = power(a, b)
        NumericUpDown9.Value = abilshortE(a, b)
        NumericUpDown10.Value = abilshort10(a, b)
        NumericUpDown11.Value = abilshort12(a, b) And &H7FFF

        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        Array.Clear(lbobjcol1, 0, 4200)
        'New lbobjcol1'.Clear()
        'lbobjcol1 = Array.Clear()
        ListBox5.Items.Clear()
        ListBox3.Items.Add(abildata(a, b, 1).ToString("X8"))
        Dim tval As Short
        Dim tvalu As UShort
        Dim rlist() As String = {"r0", "r1", "r2", "r3", "r4", "r5", "r6", "r7", "r8", "r9", "r10", "r11", "r12", "sp", "lr", "pc"}
        Dim asmblockn(20) As Integer
        Dim asmblknum As Byte = 0
        Seek(1, abildata(a, b, 1) And &HFFFFFF)
        For asmblock = 0 To 20
            For instructions = 0 To 200
                FileGet(1, tval)
                tvalu = tval And &HFFFF
                If tvalu >> 13 = 0 Then 'THUMB1
                    Select Case (tvalu >> 11) And &H3
                        Case 0
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " lsl " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
                        Case 1
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " lsr " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
                        Case 2
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " asr " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
                        Case 3 'THUMB2
                            Select Case (tvalu >> 9) And &H3
                                Case 0
                                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " add " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And &H7))
                                Case 1
                                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                    " sub " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And &H7))
                                Case 2
                                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " add " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H7).ToString("X2"))
                                Case 3
                                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " sub " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H7).ToString("X2"))
                            End Select
                    End Select
                ElseIf tvalu >> 13 = 1 Then 'THUMB3
                    Dim tinst() As String = {"mov", "cmp", "add", "sub"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 3) & " " & rlist(tvalu >> 8 And 3) & ", #0x" & (tvalu And &HFF).ToString("X2"))
                ElseIf tvalu >> 10 = 16 Then 'THUMB4
                    Dim tinst() As String = {"and", "eor", "lsl", "lsr", "asr", "adc", "sbc", "ror", "tst", "neg", "cmp", "cmn", "orr", "mul", "bic", "mvn"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " " & tinst(tvalu >> 6 And &HF) & " " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7))
                ElseIf tvalu >> 10 = 17 Then 'THUMB5
                    Select Case tvalu >> 8 And 3
                        Case 0
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " add " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
                        Case 1
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " cmp " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
                        Case 2
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                               " mov " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
                        Case 3
                            'Dim tinst() As String = {"bx", "blx"}
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                            " bx " & rlist(tvalu >> 3 And &HF))
                            Exit For
                    End Select
                ElseIf tvalu >> 11 = 9 Then 'THUMB6
                    Dim intval As Integer
                    Dim pc As Integer = Loc(1)
                    FileGet(1, intval, ((tvalu And &HFF) << 2) + ((Loc(1) + 3) And &HFFFDS))
                    lbobjcol1(asmblock, instructions) = (((pc - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " ldr " & rlist(tvalu >> 8 And 3) & ", [$" & ((Loc(1) - 4) Or &H8000000).ToString("X8") & "] (=$" & intval.ToString("X8") & ")")
                    Seek(1, pc + 1)
                    '//--- 
                    If (intval >> 24) = 8 Then
                        'Dim test(,) As Integer
                        'Dim lbobjcol1(10) As ListBox.ObjectCollection
                        'test2.Add(
                        'lbobjcol1(c,d) = Range(lbobjcol1(0))
                        Try
                            asmblockn(asmblknum) = intval
                            asmblknum += 1
                            ListBox3.Items.Add(intval.ToString("X8"))
                        Catch ex As Exception
                            'ListBox3.Items.Add("Max Limit")
                            'Exit For
                        End Try

                    End If
                ElseIf tvalu >> 12 = 5 Then 'THUMB7
                    If (tvalu >> 9) And 1 = 0 Then
                        Dim tinst() As String = {"str", "strb", "ldr", "ldrb"}
                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                           tinst(tvalu >> 10 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And 7) & "]")
                    Else 'THUMB8
                        Dim tinst() As String = {"strh", "ldsb", "ldrh", "ldsh"}
                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                           tinst(tvalu >> 10 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And 7) & "]")
                    End If
                ElseIf tvalu >> 13 = 3 Then 'THUMB9
                    Dim tinst() As String = {"str", "ldr", "strb", "ldrb"}
                    Select Case (tvalu >> 12) And 1
                        Case 0
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & ((tvalu >> 6 And &H1F) << 2).ToString("X2") & "]")
                        Case 1
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2") & "]")
                    End Select
                ElseIf tvalu >> 12 = 8 Then 'THUMB10
                    Dim tinst() As String = {"strh", "ldrh"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 1) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & ((tvalu >> 6 And &H1F) << 1).ToString("X2") & "]")
                ElseIf tvalu >> 12 = 9 Then 'THUMB11
                    'lbobjcol1(c,d) = ("THUMB11")
                    Dim tinst() As String = {"str", "ldr"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 1) & " " & rlist(tvalu >> 8 And 7) & ", [sp " & ", #0x" & ((tvalu And &HFF) << 2).ToString("X2") & "]")
                ElseIf tvalu >> 12 = 10 Then 'THUMB12
                    Select Case ((tvalu >> 11) And 1)
                        Case 0
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                         " add " & rlist(tvalu >> 8 And 7) & ", pc, #0x" & Hex((tvalu And &H7F) << 2) & " (=$" & (((tvalu And &H7F) << 2) + ((Loc(1) + 3) And &HFFFDS)).ToString("X8") & ")")
                        Case 1
                            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                                     " add " & rlist(tvalu >> 8 And 7) & ", sp, #0x" & Hex((tvalu And &H7F) << 2))
                    End Select
                ElseIf tvalu >> 8 = &HB0 Then 'THUMB13
                    Dim sign() As String = {"", "-"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                                         " add sp, " & sign(tvalu >> 7 And 1) & "#0x" & Hex((tvalu And &H7F) << 2))
                ElseIf tvalu >> 12 = 11 Then 'THUMB14
                    Dim tinst() As String = {"push", "pop"}
                    'Dim rlistind As Byte = 1
                    Dim reglist As String = ""
                    'Select Case (tvalu And 1)
                    'End Select
                    Dim multiple As Byte = 0
                    For a = 0 To 7
                        'rlistind << a
                        If (tvalu >> a) And 1 Then
                            If multiple = 0 Then
                                reglist &= rlist(a)
                                multiple = 1
                            Else
                                reglist &= ", " & rlist(a)
                            End If
                        End If
                    Next
                    If (tvalu >> 8) And 1 Then
                        If multiple = 0 Then
                            reglist &= rlist(14 + ((tvalu >> 11) And 1))
                        Else
                            reglist &= ", " & rlist(14 + ((tvalu >> 11) And 1))
                        End If
                    End If
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 1) & " {" & reglist & "}")
                ElseIf tvalu >> 12 = &HC Then 'THUMB15
                    Dim tinst() As String = {"stmia", "ldmia"}
                    'Dim rlistind As Byte = 1
                    Dim reglist As String = ""
                    'Select Case (tvalu And 1)
                    'End Select
                    Dim multiple As Byte = 0
                    For a = 0 To 7
                        'rlistind << a
                        If (tvalu >> a) And 1 Then
                            If multiple = 0 Then
                                reglist &= rlist(a)
                                multiple = 1
                            Else
                                reglist &= ", " & rlist(a)
                            End If
                        End If
                    Next
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 11 And 1) & " " & rlist(tvalu >> 8 And 7) & "!, {" & reglist & "}")
                ElseIf tvalu >> 12 = &HD Then 'THUMB16 (swi = 17)
                    Dim tinst() As String = {"beq", "bne", "bcs", "bcc", "bmi", "bpl", "bvs", "bvc", "bhi", "bls", "bge", "blt", "bgt", "ble", "???", "swi"}
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
                                       tinst(tvalu >> 8 And &HF) & " $" & ((Loc(1) - ((tvalu And &H80) << 1) + ((tvalu And &H7F) << 1) + 2) Or &H8000000).ToString("X8"))
                ElseIf tvalu >> 11 = &H1C Then 'THUMB18
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                       " b $" & ((Loc(1) - ((tvalu And &H400) << 1) + ((tvalu And &H3FF) << 1) + 2) Or &H8000000).ToString("X8"))
                ElseIf tvalu >> 11 = &H1E Then 'THUMB19
                    Dim tvalue3 As Short
                    FileGet(1, tvalue3)
                    Dim branchto As Integer
                    branchto = (Loc(1) - ((tvalu And &H400) << 12) + ((tvalu And &H3FF) << 12) + ((tvalue3 And &H7FF) << 1)) Or &H8000000
                    '080195B4 = Sound Effect
                    Dim desc As String
                    Select Case branchto
                        Case &H80195B4
                            desc = "(Sound Effect)"
                        Case &H8085B38
                            desc = "(Move NPC)"
                        Case Else
                            desc = ""
                    End Select
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                           " bl $" & branchto.ToString("X8") & " " & desc)
                    'Function Call List
                    ListBox5.Items.Add(((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                           " bl $" & branchto.ToString("X8") & " " & desc)
                ElseIf tvalu >> 11 = &H1F Then 'blh
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
                                           " blh $" & ((tvalu And &H7FF) << 1).ToString("X4"))
                Else
                    lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4"))
                End If
                'd += 1
            Next
            If asmblockn(asmblock) = 0 Then Exit For
            Seek(1, asmblockn(asmblock) And &HFFFFFF)
        Next
        ListBox3.SelectedIndex = 0
        'For f = 0 To 100
        '    ListBox4.Items.Add(lbobjcol1(0, f))
        'Next
    End Sub
    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown8.ValueChanged
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        power(a, b) = NumericUpDown8.Value
    End Sub
    Private Sub ComboBox13_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox13.SelectedIndexChanged
        'MsgBox("!")
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        catype(a, b) = ComboBox13.SelectedIndex
    End Sub
    Private Sub NumericUpDown9_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown9.ValueChanged
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        abilshortE(a, b) = NumericUpDown9.Value
    End Sub
    Private Sub NumericUpDown10_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown10.ValueChanged
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        abilshort10(a, b) = NumericUpDown10.Value
    End Sub
    Private Sub NumericUpDown11_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown11.ValueChanged
        Dim a = listorder(ListBox1.SelectedIndex) 'Enemy #
        Dim b = ListBox2.SelectedIndex 'Ability #
        abilshort12(a, b) = (abilshort12(a, b) And &H8000S) Or NumericUpDown11.Value
    End Sub

    'Private Sub ContextMenuStrip1_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
    '    ContextMenuStrip1.
    'End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        e.Graphics.DrawImage(enemytext2, New Point(0, 0))
    End Sub

    Private Sub ComboBox14_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox14.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.SelectedIndex = -1
        'listorder(3) = 10
        'listorder(10) = 3
        For a = 0 To enemies
            listorder(a) = enemies - a
        Next
        'For a = 0 To enemies
        '    For b = a To enemies
        '        If HP(listorder(a)) > HP(listorder(a + 1)) Then

        '        End If
        '    Next
        'Next
        'ListBox1.SelectedIndex = -1
        ListBox1.Items.Clear()
        For a = 0 To enemies
            ListBox1.Items.Add(Hex(listorder(a)) & " - " & EnemyName(listorder(a)))
        Next
        'ListBox1.SelectedIndex = 0
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        ListBox4.Items.Clear()
        Try
            For f = 0 To 200
                ListBox4.Items.Add(lbobjcol1(ListBox3.SelectedIndex, f))
            Next
        Catch
        End Try


        ''ListBox3.Items.Clear()
        ''ListBox4.Items.Clear()
        ''Array.Clear(lbobjcol1, 0, 4200)
        ''New lbobjcol1'.Clear()
        ''lbobjcol1 = Array.Clear()
        'ListBox5.Items.Clear()
        ''ListBox3.Items.Add(abildata(a, b, 1).ToString("X8"))
        'Dim tval As Short
        'Dim tvalu As UShort
        'Dim rlist() As String = {"r0", "r1", "r2", "r3", "r4", "r5", "r6", "r7", "r8", "r9", "r10", "r11", "r12", "sp", "lr", "pc"}
        'Dim asmblockn(20) As Integer
        'Dim asmblknum As Byte = 0
        'If asmblockn(ListBox3.SelectedIndex) = 0 Then Exit Sub
        'Seek(1, asmblockn(ListBox3.SelectedIndex) And &HFFFFFF)
        'Try
        '    'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        'Catch ex As Exception
        'End Try
        'For instructions = 0 To 200
        '    FileGet(1, tval)
        '    tvalu = tval And &HFFFF
        '    If tvalu >> 13 = 0 Then 'THUMB1
        '        Select Case (tvalu >> 11) And &H3
        '            Case 0
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " lsl " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
        '            Case 1
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " lsr " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
        '            Case 2
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " asr " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2"))
        '            Case 3 'THUMB2
        '                Select Case (tvalu >> 9) And &H3
        '                    Case 0
        '                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " add " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And &H7))
        '                    Case 1
        '                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                        " sub " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And &H7))
        '                    Case 2
        '                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " add " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H7).ToString("X2"))
        '                    Case 3
        '                        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " sub " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H7).ToString("X2"))
        '                End Select
        '        End Select
        '    ElseIf tvalu >> 13 = 1 Then 'THUMB3
        '        Dim tinst() As String = {"mov", "cmp", "add", "sub"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 3) & " " & rlist(tvalu >> 8 And 3) & ", #0x" & (tvalu And &HFF).ToString("X2"))
        '    ElseIf tvalu >> 10 = 16 Then 'THUMB4
        '        Dim tinst() As String = {"and", "eor", "lsl", "lsr", "asr", "adc", "sbc", "ror", "tst", "neg", "cmp", "cmn", "orr", "mul", "bic", "mvn"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " " & tinst(tvalu >> 6 And &HF) & " " & rlist(tvalu And 7) & ", " & rlist(tvalu >> 3 And 7))
        '    ElseIf tvalu >> 10 = 17 Then 'THUMB5
        '        Select Case tvalu >> 8 And 3
        '            Case 0
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " add " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
        '            Case 1
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " cmp " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
        '            Case 2
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                   " mov " & rlist((tvalu And 7) Or ((tvalu And 1) >> 4)) & ", " & rlist(tvalu >> 3 And &HF))
        '            Case 3
        '                'Dim tinst() As String = {"bx", "blx"}
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                " bx " & rlist(tvalu >> 3 And &HF))
        '                Exit For
        '        End Select
        '    ElseIf tvalu >> 11 = 9 Then 'THUMB6
        '        Dim intval As Integer
        '        Dim pc As Integer = Loc(1)
        '        FileGet(1, intval, ((tvalu And &HFF) << 2) + ((Loc(1) + 3) And &HFFFDS))
        '        lbobjcol1(asmblock, instructions) = (((pc - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " ldr " & rlist(tvalu >> 8 And 3) & ", [$" & ((Loc(1) - 4) Or &H8000000).ToString("X8") & "] (=$" & intval.ToString("X8") & ")")
        '        Seek(1, pc + 1)
        '        '//--- 
        '        If (intval >> 24) = 8 Then
        '            'Dim test(,) As Integer
        '            'Dim lbobjcol1(10) As ListBox.ObjectCollection
        '            'test2.Add(
        '            'lbobjcol1(c,d) = Range(lbobjcol1(0))
        '            Try
        '                asmblockn(asmblknum) = intval
        '                asmblknum += 1
        '                ListBox3.Items.Add(intval.ToString("X8"))
        '            Catch ex As Exception
        '                'ListBox3.Items.Add("Max Limit")
        '                'Exit For
        '            End Try

        '        End If
        '    ElseIf tvalu >> 12 = 5 Then 'THUMB7
        '        If (tvalu >> 9) And 1 = 0 Then
        '            Dim tinst() As String = {"str", "strb", "ldr", "ldrb"}
        '            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                               tinst(tvalu >> 10 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And 7) & "]")
        '        Else 'THUMB8
        '            Dim tinst() As String = {"strh", "ldsb", "ldrh", "ldsh"}
        '            lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                               tinst(tvalu >> 10 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", " & rlist(tvalu >> 6 And 7) & "]")
        '        End If
        '    ElseIf tvalu >> 13 = 3 Then 'THUMB9
        '        Dim tinst() As String = {"str", "ldr", "strb", "ldrb"}
        '        Select Case (tvalu >> 12) And 1
        '            Case 0
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & ((tvalu >> 6 And &H1F) << 2).ToString("X2") & "]")
        '            Case 1
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 3) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & (tvalu >> 6 And &H1F).ToString("X2") & "]")
        '        End Select
        '    ElseIf tvalu >> 12 = 8 Then 'THUMB10
        '        Dim tinst() As String = {"strh", "ldrh"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 1) & " " & rlist(tvalu And 7) & ", [" & rlist(tvalu >> 3 And 7) & ", #0x" & ((tvalu >> 6 And &H1F) << 1).ToString("X2") & "]")
        '    ElseIf tvalu >> 12 = 9 Then 'THUMB11
        '        'lbobjcol1(c,d) = ("THUMB11")
        '        Dim tinst() As String = {"str", "ldr"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 1) & " " & rlist(tvalu >> 8 And 7) & ", [sp " & ", #0x" & ((tvalu And &HFF) << 2).ToString("X2") & "]")
        '    ElseIf tvalu >> 12 = 10 Then 'THUMB12
        '        Select Case ((tvalu >> 11) And 1)
        '            Case 0
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                             " add " & rlist(tvalu >> 8 And 7) & ", pc, #0x" & Hex((tvalu And &H7F) << 2) & " (=$" & (((tvalu And &H7F) << 2) + ((Loc(1) + 3) And &HFFFDS)).ToString("X8") & ")")
        '            Case 1
        '                lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                         " add " & rlist(tvalu >> 8 And 7) & ", sp, #0x" & Hex((tvalu And &H7F) << 2))
        '        End Select
        '    ElseIf tvalu >> 8 = &HB0 Then 'THUMB13
        '        Dim sign() As String = {"", "-"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                                             " add sp, " & sign(tvalu >> 7 And 1) & "#0x" & Hex((tvalu And &H7F) << 2))
        '    ElseIf tvalu >> 12 = 11 Then 'THUMB14
        '        Dim tinst() As String = {"push", "pop"}
        '        'Dim rlistind As Byte = 1
        '        Dim reglist As String = ""
        '        'Select Case (tvalu And 1)
        '        'End Select
        '        Dim multiple As Byte = 0
        '        For a = 0 To 7
        '            'rlistind << a
        '            If (tvalu >> a) And 1 Then
        '                If multiple = 0 Then
        '                    reglist &= rlist(a)
        '                    multiple = 1
        '                Else
        '                    reglist &= ", " & rlist(a)
        '                End If
        '            End If
        '        Next
        '        If (tvalu >> 8) And 1 Then
        '            If multiple = 0 Then
        '                reglist &= rlist(14 + ((tvalu >> 11) And 1))
        '            Else
        '                reglist &= ", " & rlist(14 + ((tvalu >> 11) And 1))
        '            End If
        '        End If
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 1) & " {" & reglist & "}")
        '    ElseIf tvalu >> 12 = &HC Then 'THUMB15
        '        Dim tinst() As String = {"stmia", "ldmia"}
        '        'Dim rlistind As Byte = 1
        '        Dim reglist As String = ""
        '        'Select Case (tvalu And 1)
        '        'End Select
        '        Dim multiple As Byte = 0
        '        For a = 0 To 7
        '            'rlistind << a
        '            If (tvalu >> a) And 1 Then
        '                If multiple = 0 Then
        '                    reglist &= rlist(a)
        '                    multiple = 1
        '                Else
        '                    reglist &= ", " & rlist(a)
        '                End If
        '            End If
        '        Next
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 11 And 1) & " " & rlist(tvalu >> 8 And 7) & "!, {" & reglist & "}")
        '    ElseIf tvalu >> 12 = &HD Then 'THUMB16 (swi = 17)
        '        Dim tinst() As String = {"beq", "bne", "bcs", "bcc", "bmi", "bpl", "bvs", "bvc", "bhi", "bls", "bge", "blt", "bgt", "ble", "???", "swi"}
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & " " & _
        '                           tinst(tvalu >> 8 And &HF) & " $" & ((Loc(1) - ((tvalu And &H80) << 1) + ((tvalu And &H7F) << 1) + 2) Or &H8000000).ToString("X8"))
        '    ElseIf tvalu >> 11 = &H1C Then 'THUMB18
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                           " b $" & ((Loc(1) - ((tvalu And &H400) << 1) + ((tvalu And &H3FF) << 1) + 2) Or &H8000000).ToString("X8"))
        '    ElseIf tvalu >> 11 = &H1E Then 'THUMB19
        '        Dim tvalue3 As Short
        '        FileGet(1, tvalue3)
        '        Dim branchto As Integer
        '        branchto = (Loc(1) - ((tvalu And &H400) << 12) + ((tvalu And &H3FF) << 12) + ((tvalue3 And &H7FF) << 1)) Or &H8000000
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                               " bl $" & branchto.ToString("X8"))
        '        'Function Call List
        '        ListBox5.Items.Add(((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                               " bl $" & branchto.ToString("X8"))
        '    ElseIf tvalu >> 11 = &H1F Then 'blh
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 4) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4") & _
        '                               " blh $" & ((tvalu And &H7FF) << 1).ToString("X4"))
        '    Else
        '        lbobjcol1(asmblock, instructions) = (((Loc(1) - 2) Or &H8000000).ToString("X8") & " " & tvalu.ToString("X4"))
        '    End If
        '    'd += 1
        'Next
        ''FileClose(1)
    End Sub
End Class