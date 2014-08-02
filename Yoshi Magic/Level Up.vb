Public Class Party_Editor

    Dim Levels = &H63
    Dim Distance = &H18
    Dim Levelup(Levels, Distance) As Byte

    Dim HP(2, Levels) As Byte
    Dim BP(2, Levels) As Byte
    Dim POWER(2, Levels) As Byte
    Dim DEFENSE(2, Levels) As Byte
    Dim SPEED(2, Levels) As Byte
    Dim STACHE(2, Levels) As Byte

    Dim EXP(2, Levels) As Integer

    Dim LevelStartM As Byte, LevelStartL As Byte
    Dim EXPStartM As Integer, EXPStartL As Integer
    Dim BadgeStartM As Byte, BadgeStartL As Byte
    Dim GearStartM As Byte, GearStartL As Byte
    Dim PinStartM As Byte, PinStartL As Byte

    ' Dim gearpointer(45) As Integer 'Pointers in Gears Database leading to Gear text.
    ' Dim gearchr(45, 15) As Byte 'Each letter of each gear.

    Dim HPStartM As Short, HPBaseM As Short, HPMaxM As Short
    Dim BPStartM As Short, BPBaseM As Short, BPMaxM As Short
    Dim HPStartL As Short, HPBaseL As Short, HPMaxL As Short
    Dim BPStartL As Short, BPBaseL As Short, BPMaxL As Short

    Dim BasePOWERM As Short, POWERM As Short
    Dim BaseSPEEDM As Short, SPEEDM As Short
    Dim BaseDEFENSEM As Short, DEFENSEM As Short
    Dim BaseSTACHEM As Short, STACHEM As Short
    Dim BasePOWERL As Short, POWERL As Short
    Dim BaseSPEEDL As Short, SPEEDL As Short
    Dim BaseDEFENSEL As Short, DEFENSEL As Short
    Dim BaseSTACHEL As Short, STACHEL As Short

    Private Sub Level_Up_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Seek(1, &H3BAEAC + 1)
        For ml = 0 To 1
            For a = 0 To Levels - 1
                'For b = 0 To Distance
                '    FileGet(1, Levelup(a, b), &H3BAEAC + (&H19 * a) + b + 1)
                'Next
                FileGet(1, HP(ml, a + 1), &H3BAEAC + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, BP(ml, a + 1)) ', &H3BAEAD + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, POWER(ml, a + 1)) ', &H3BAEAE + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, SPEED(ml, a + 1)) ', &H3BAEAF + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, STACHE(ml, a + 1)) ', &H3BAEB0 + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, DEFENSE(ml, a + 1)) ', &H3BAEB1 + (&HC * a) + (&H4A4 * ml) + 1)
                FileGet(1, EXP(ml, a + 1)) ', &H3BAEB2 + (&HC * a) + (&H4A4 * ml) + 1)
            Next
        Next
        FileGet(1, LevelStartM, &H3C05A8 + 1)
        FileGet(1, EXPStartM, &H3C057B + 1)
        FileGet(1, HPStartM, &H3C0586 + 1)
        'MsgBox(Hex(Loc(1)))
        FileGet(1, HPBaseM) ', &H3C0588 + 1)
        FileGet(1, HPMaxM) ', &H3C058A + 1)
        FileGet(1, BPStartM) ', &H3C058C + 1)
        FileGet(1, BPBaseM) ', &H3C058E + 1)
        FileGet(1, BPMaxM) ', &H3C0590 + 1)
        FileGet(1, BasePOWERM) ', &H3C0592 + 1)
        FileGet(1, POWERM) ', &H3C0594 + 1)
        FileGet(1, BaseSPEEDM) ', &H3C0596 + 1)
        FileGet(1, SPEEDM) ', &H3C0598 + 1)
        FileGet(1, BaseDEFENSEM) ', &H3C059A + 1)
        FileGet(1, DEFENSEM) ', &H3C059C + 1)
        FileGet(1, BaseSTACHEM) ', &H3C059E + 1)
        FileGet(1, STACHEM) ', &H3C05A0 + 1)

        FileGet(1, LevelStartL, &H3C05E4 + 1)
        FileGet(1, EXPStartL, &H3C05B7 + 1)

        FileGet(1, HPStartL, &H3C05C2 + 1)
        FileGet(1, HPBaseL) ', &H3C05C4 + 1)
        FileGet(1, HPMaxL) ', &H3C05C6 + 1)
        FileGet(1, BPStartL) ', &H3C05C8 + 1)
        FileGet(1, BPBaseL) ', &H3C05CA + 1)
        FileGet(1, BPMaxL) ', &H3C05CC + 1)
        FileGet(1, BasePOWERL) ', &H3C05CE + 1)
        FileGet(1, POWERL) ', &H3C05D0 + 1)
        FileGet(1, BaseSPEEDL) ', &H3C05D2 + 1)
        FileGet(1, SPEEDL) ', &H3C05D4 + 1)
        FileGet(1, BaseDEFENSEL) ', &H3C05D6 + 1)
        FileGet(1, DEFENSEL) ', &H3C05D8 + 1)
        FileGet(1, BaseSTACHEL) ', &H3C05DA + 1)
        FileGet(1, STACHEL) ', &H3C05DC + 1)

        TextBox17.Text = LevelStartM 'We do want to display the text in this textbox on form_load.
        TextBox30.Text = EXPStartM
        TextBox19.Text = HPStartM
        TextBox21.Text = HPBaseM
        TextBox64.Text = HPMaxM
        TextBox61.Text = BPStartM
        TextBox62.Text = BPBaseM
        TextBox63.Text = BPMaxM
        TextBox22.Text = BasePOWERM
        TextBox23.Text = POWERM
        TextBox26.Text = BaseSPEEDM
        TextBox27.Text = SPEEDM
        TextBox24.Text = BaseDEFENSEM
        TextBox25.Text = DEFENSEM
        TextBox28.Text = BaseSTACHEM
        TextBox16.Text = STACHEM

        TextBox73.Text = LevelStartL
        TextBox70.Text = EXPStartL
        TextBox75.Text = HPStartL
        TextBox77.Text = HPBaseL
        TextBox66.Text = HPMaxL
        TextBox67.Text = BPStartL
        TextBox68.Text = BPBaseL
        TextBox65.Text = BPMaxL
        TextBox78.Text = BasePOWERL
        TextBox79.Text = POWERL
        TextBox82.Text = BaseSPEEDL
        TextBox83.Text = SPEEDL
        TextBox80.Text = BaseDEFENSEL
        TextBox81.Text = DEFENSEL
        TextBox84.Text = BaseSTACHEL
        TextBox72.Text = STACHEL
        'Change value of numericupdown1 twice to update lovely numbers.
        NumericUpDown1.Value = 2
        NumericUpDown1.Value = 1
    End Sub
    'We want to store values that were changed so we do not lose them for when we save.
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            HP(0, NumericUpDown1.Value) = TextBox1.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Try
            BP(0, NumericUpDown1.Value) = TextBox2.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Try
            POWER(0, NumericUpDown1.Value) = TextBox3.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Try
            DEFENSE(0, NumericUpDown1.Value) = TextBox4.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Try
            SPEED(0, NumericUpDown1.Value) = TextBox5.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Try
            STACHE(0, NumericUpDown1.Value) = TextBox6.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Try
            EXP(0, NumericUpDown1.Value) = TextBox7.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        Try
            HP(1, NumericUpDown1.Value) = TextBox8.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        Try
            BP(1, NumericUpDown1.Value) = TextBox9.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        Try
            POWER(1, NumericUpDown1.Value) = TextBox10.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        Try
            DEFENSE(1, NumericUpDown1.Value) = TextBox11.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        Try
            SPEED(1, NumericUpDown1.Value) = TextBox12.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        Try
            STACHE(1, NumericUpDown1.Value) = TextBox13.Text
        Catch
        End Try
    End Sub
    Private Sub TextBox14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox14.TextChanged
        Try
            EXP(1, NumericUpDown1.Value) = TextBox14.Text
        Catch
        End Try
    End Sub

    'We'll save our data. All your data are belong to us. ;)
    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        'SaveToolStripMenuItem1.Click
        'On Error GoTo err
        For ML = 0 To 1
            For a = 0 To &H63 - 1
                'FilePut(1, HP, &H3BAEAC + (&H19 * a) + 1)
                FilePut(1, HP(ML, a + 1), &H3BAEAC + (&HC * a) + (&H4A4 * ML) + 1) 'are belong to us!
                FilePut(1, BP(ML, a + 1)) ', &H3BAEAD + (&HC * a) + (&H4A4 * ML) + 1)
                FilePut(1, POWER(ML, a + 1)) ', &H3BAEAE + (&HC * a) + (&H4A4 * ML) + 1)
                FilePut(1, SPEED(ML, a + 1)) ', &H3BAEAF + (&HC * a) + (&H4A4 * ML) + 1)
                FilePut(1, STACHE(ML, a + 1)) ', &H3BAEB0 + (&HC * a) + (&H4A4 * ML) + 1)
                FilePut(1, DEFENSE(ML, a + 1)) ', &H3BAEB1 + (&HC * a) + (&H4A4 * ML) + 1)
                FilePut(1, EXP(ML, a + 1)) ', &H3BAEB2 + (&HC * a) + (&H4A4 * ML) + 1)

            Next
        Next

        LevelStartM = TextBox17.Text  'Since this is not an array, this could be moved to the Save Sub.
        EXPStartM = TextBox30.Text
        HPStartM = TextBox19.Text
        HPBaseM = TextBox21.Text
        HPMaxM = TextBox64.Text
        BPStartM = TextBox61.Text
        BPBaseM = TextBox62.Text
        BPMaxM = TextBox63.Text
        BasePOWERM = TextBox22.Text
        POWERM = TextBox23.Text
        BaseDEFENSEM = TextBox24.Text
        DEFENSEM = TextBox25.Text
        BaseSPEEDM = TextBox26.Text
        SPEEDM = TextBox27.Text
        BaseSTACHEM = TextBox28.Text
        STACHEM = TextBox16.Text
        LevelStartL = TextBox73.Text
        EXPStartL = TextBox70.Text
        HPStartL = TextBox75.Text
        HPBaseL = TextBox77.Text
        HPMaxL = TextBox66.Text
        BPStartL = TextBox67.Text
        BPBaseL = TextBox68.Text
        BPMaxL = TextBox65.Text
        BasePOWERL = TextBox78.Text
        POWERL = TextBox79.Text
        BaseDEFENSEL = TextBox80.Text
        DEFENSEL = TextBox81.Text
        BaseSPEEDL = TextBox82.Text
        SPEEDL = TextBox83.Text
        BaseSTACHEL = TextBox84.Text
        STACHEL = TextBox72.Text

        FilePut(1, LevelStartM, &H3C05A8 + 1)
        FilePut(1, EXPStartM, &H3C057B + 1)
        FilePut(1, HPStartM, &H3C0586 + 1)
        FilePut(1, HPBaseM) ', &H3C0588 + 1)
        FilePut(1, HPMaxM) ', &H3C058A + 1)
        FilePut(1, BPStartM) ', &H3C058C + 1)
        FilePut(1, BPBaseM) ', &H3C058E + 1)
        FilePut(1, BPMaxM) ', &H3C0590 + 1)
        FilePut(1, BasePOWERM) ', &H3C0592 + 1)
        FilePut(1, POWERM) ', &H3C0594 + 1)
        FilePut(1, BaseSPEEDM) ', &H3C0596 + 1)
        FilePut(1, SPEEDM) ', &H3C0598 + 1)
        FilePut(1, BaseDEFENSEM) ', &H3C059A + 1)
        FilePut(1, DEFENSEM) ', &H3C059C + 1)
        FilePut(1, BaseSTACHEM) ', &H3C059E + 1)
        FilePut(1, STACHEM) ', &H3C05A0 + 1)

        FilePut(1, LevelStartL, &H3C05E4 + 1)
        FilePut(1, EXPStartL, &H3C05B7 + 1)
        FilePut(1, HPStartL, &H3C05C2 + 1)
        FilePut(1, HPBaseL) ', &H3C05C4 + 1)
        FilePut(1, HPMaxL) ', &H3C05C6 + 1)
        FilePut(1, BPStartL) ', &H3C05C8 + 1)
        FilePut(1, BPBaseL) ', &H3C05CA + 1)
        FilePut(1, BPMaxL) ', &H3C05CC + 1)
        FilePut(1, BasePOWERL) ', &H3C05CE + 1)
        FilePut(1, POWERL) ', &H3C05D0 + 1)
        FilePut(1, BaseSPEEDL) ', &H3C05D2 + 1)
        FilePut(1, SPEEDL) ', &H3C05D4 + 1)
        FilePut(1, BaseDEFENSEL) ', &H3C05D6 + 1)
        FilePut(1, DEFENSEL) ', &H3C05D8 + 1)
        FilePut(1, BaseSTACHEL) ', &H3C05DA + 1)
        FilePut(1, STACHEL) ', &H3C05DC + 1)
        'Next
        'FileClose(1)
        MsgBox("Saved!")
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Try
            ' If TextBox15.Text = "" Then
            'GoTo ERROR1
            'End If
            'Levelup(Levels, Distance) = TextBox15.Text
            TextBox1.Text = HP(0, NumericUpDown1.Value)
            TextBox2.Text = BP(0, NumericUpDown1.Value)
            TextBox3.Text = POWER(0, NumericUpDown1.Value)
            TextBox4.Text = DEFENSE(0, NumericUpDown1.Value)
            TextBox5.Text = SPEED(0, NumericUpDown1.Value)
            TextBox6.Text = STACHE(0, NumericUpDown1.Value)
            TextBox7.Text = EXP(0, NumericUpDown1.Value)
            'TextBox7.Text
            TextBox8.Text = HP(1, NumericUpDown1.Value)
            TextBox9.Text = BP(1, NumericUpDown1.Value)
            TextBox10.Text = POWER(1, NumericUpDown1.Value)
            TextBox11.Text = DEFENSE(1, NumericUpDown1.Value)
            TextBox12.Text = SPEED(1, NumericUpDown1.Value)
            TextBox13.Text = STACHE(1, NumericUpDown1.Value)
            TextBox14.Text = EXP(1, NumericUpDown1.Value)
            'TextBox14.Text
            Exit Try
ERROR1:     'If TextBox15.Text = "" Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""

            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
            TextBox13.Text = ""
            'End If
        Catch
        End Try
    End Sub
    'Private Sub TextBox17_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox17.TextChanged
    '    LevelStartM = TextBox17.Text  'Since this is not an array, this could be moved to the Save Sub.
    'End Sub
    'Private Sub TextBox30_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox30.TextChanged
    '    EXPStartM = TextBox30.Text
    'End Sub
    'Private Sub TextBox19_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox19.TextChanged
    '    HPStartM = TextBox19.Text
    'End Sub
    'Private Sub TextBox21_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox21.TextChanged
    '    HPBaseM = TextBox21.Text
    'End Sub
    'Private Sub TextBox64_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox64.TextChanged
    '    HPMaxM = TextBox64.Text
    'End Sub
    'Private Sub TextBox61_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox61.TextChanged
    '    BPStartM = TextBox61.Text
    'End Sub
    'Private Sub TextBox62_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox62.TextChanged
    '    BPBaseM = TextBox62.Text
    'End Sub
    'Private Sub TextBox63_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox63.TextChanged
    '    BPMaxM = TextBox63.Text
    'End Sub
    'Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged
    '    BasePOWERM = TextBox22.Text
    'End Sub
    'Private Sub TextBox23_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox23.TextChanged
    '    POWERM = TextBox23.Text
    'End Sub
    'Private Sub TextBox24_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox24.TextChanged
    '    BaseDEFENSEM = TextBox24.Text
    'End Sub
    'Private Sub TextBox25_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox25.TextChanged
    '    DEFENSEM = TextBox25.Text
    'End Sub
    'Private Sub TextBox26_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox26.TextChanged
    '    BaseSPEEDM = TextBox26.Text
    'End Sub
    'Private Sub TextBox27_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox27.TextChanged
    '    SPEEDM = TextBox27.Text
    'End Sub
    'Private Sub TextBox28_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox28.TextChanged
    '    BaseSTACHEM = TextBox28.Text
    'End Sub
    'Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
    '    STACHEM = TextBox16.Text
    'End Sub
    'Private Sub TextBox73_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox73.TextChanged
    '    LevelStartL = TextBox73.Text
    'End Sub
    'Private Sub TextBox70_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox70.TextChanged
    '    EXPStartL = TextBox70.Text
    'End Sub
    'Private Sub TextBox75_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox75.TextChanged
    '    HPStartL = TextBox75.Text
    'End Sub
    'Private Sub TextBox77_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox77.TextChanged
    '    HPBaseL = TextBox77.Text
    'End Sub
    'Private Sub TextBox66_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox66.TextChanged
    '    HPMaxL = TextBox66.Text
    'End Sub
    'Private Sub TextBox67_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox67.TextChanged
    '    BPStartL = TextBox67.Text
    'End Sub
    'Private Sub TextBox68_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox68.TextChanged
    '    BPBaseL = TextBox68.Text
    'End Sub
    'Private Sub TextBox65_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox65.TextChanged
    '    BPMaxL = TextBox65.Text
    'End Sub
    'Private Sub TextBox78_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox78.TextChanged
    '    BasePOWERL = TextBox78.Text
    'End Sub
    'Private Sub TextBox79_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox79.TextChanged
    '    POWERL = TextBox79.Text
    'End Sub
    'Private Sub TextBox80_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox80.TextChanged
    '    BaseDEFENSEL = TextBox80.Text
    'End Sub
    'Private Sub TextBox81_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox81.TextChanged
    '    DEFENSEL = TextBox81.Text
    'End Sub
    'Private Sub TextBox82_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox82.TextChanged
    '    BaseSPEEDL = TextBox82.Text
    'End Sub
    'Private Sub TextBox83_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox83.TextChanged
    '    SPEEDL = TextBox83.Text
    'End Sub
    'Private Sub TextBox84_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox84.TextChanged
    '    BaseSTACHEL = TextBox84.Text
    'End Sub
    'Private Sub TextBox72_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox72.TextChanged
    '    STACHEL = TextBox72.Text
    'End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        'Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        'Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        Dim oXL As Microsoft.Office.Interop.Excel.Application

        Dim oWB As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim oRng As Microsoft.Office.Interop.Excel.Range

        'On Error GoTo Err_Handler

        ' Start Excel and get Application object.
        oXL = CreateObject("Excel.Application")
        'oXL.Visible = True

        ' Get a new workbook.
        oWB = oXL.Workbooks.Add
        oSheet = oWB.ActiveSheet

        Button1.Text = "Exporting Mario..."
        ' Add table headers going cell by cell.
        oSheet.Cells(1, 1).Value = "Mario"
        oSheet.Cells(2, 1).Value = "Level"
        oSheet.Cells(2, 2).Value = "HP"
        oSheet.Cells(2, 3).Value = "BP"
        oSheet.Cells(2, 4).Value = "Power"
        oSheet.Cells(2, 5).Value = "Defense"
        oSheet.Cells(2, 6).Value = "Speed"
        oSheet.Cells(2, 7).Value = "Stache"
        oSheet.Cells(2, 8).Value = "Experience"
        For a = 1 To 99
            oSheet.Cells(a + 2, 1).value = a
            oSheet.Cells(a + 2, 2).value = HP(0, a)
            oSheet.Cells(a + 2, 3).value = BP(0, a)
            oSheet.Cells(a + 2, 4).value = POWER(0, a)
            oSheet.Cells(a + 2, 5).value = DEFENSE(0, a)
            oSheet.Cells(a + 2, 6).value = SPEED(0, a)
            oSheet.Cells(a + 2, 7).value = STACHE(0, a)
            oSheet.Cells(a + 2, 8).value = EXP(0, a)
        Next
        Button1.Text = "Exporting Luigi..."
        oSheet.Cells(1, 10).Value = "Luigi"
        oSheet.Cells(2, 10).Value = "Level"
        oSheet.Cells(2, 11).Value = "HP"
        oSheet.Cells(2, 12).Value = "BP"
        oSheet.Cells(2, 13).Value = "Power"
        oSheet.Cells(2, 14).Value = "Defense"
        oSheet.Cells(2, 15).Value = "Speed"
        oSheet.Cells(2, 16).Value = "Stache"
        oSheet.Cells(2, 17).Value = "Experience"
        For a = 1 To 99
            oSheet.Cells(a + 2, 10).value = a
            oSheet.Cells(a + 2, 11).value = HP(1, a)
            oSheet.Cells(a + 2, 12).value = BP(1, a)
            oSheet.Cells(a + 2, 13).value = POWER(1, a)
            oSheet.Cells(a + 2, 14).value = DEFENSE(1, a)
            oSheet.Cells(a + 2, 15).value = SPEED(1, a)
            oSheet.Cells(a + 2, 16).value = STACHE(1, a)
            oSheet.Cells(a + 2, 17).value = EXP(1, a)
        Next
        oSheet.Range("A1", "G1").EntireColumn.ColumnWidth = 8 'AutoFit()
        oSheet.Range("H1", "H1").EntireColumn.ColumnWidth = 10
        oSheet.Range("I1", "P1").EntireColumn.ColumnWidth = 8
        oSheet.Range("Q1", "Q1").EntireColumn.ColumnWidth = 10
        Button1.Text = "Export to Excel"
        'Controls.Add(oWB)
        ' Make sure Excel is visible and give the user control
        ' of Microsoft Excel's lifetime.
        oXL.Visible = True
        oXL.UserControl = True
        ' Make sure you release object references.
        'oRng = Nothing
        oSheet = Nothing
        oWB = Nothing
        oXL = Nothing
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog2.Title = "Open an Excel file"
        'OpenFileDialog2.Filter = "GBA/NDS file(*.gba;*.nds)|*.gba;*.nds"
        Select Case OpenFileDialog2.ShowDialog
            Case DialogResult.Cancel : Exit Sub
        End Select
        'Do
        '    Try
        '        FileOpen(1, OpenFileDialog2.FileName, OpenMode.Binary)
        '        Exit Do
        '    Catch
        '        Select Case MsgBox("Error: " & Err.Description & "  Error # " & Err.Number, MsgBoxStyle.RetryCancel)
        '            Case MsgBoxResult.Cancel
        '                Exit Sub
        '        End Select
        '    End Try
        'Loop

        Dim xlApp As Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim range As Microsoft.Office.Interop.Excel.Range
        'Dim rCnt As Integer
        'Dim cCnt As Integer
        'Dim Obj As Object

        xlApp = New Microsoft.Office.Interop.Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(OpenFileDialog2.FileName)
        xlWorkSheet = xlWorkBook.Worksheets("sheet1")

        'range = xlWorkSheet.UsedRange

        'For rCnt = 1 To range.Rows.Count
        '    For cCnt = 1 To range.Columns.Count
        '        Obj = CType(range.Cells(rCnt, cCnt), Microsoft.Office.Interop.Excel.Range)
        '        MsgBox(Obj.value)
        '    Next
        'Next
        Button2.Text = "Importing Mario..."
        ' Add table headers going cell by cell.
        'oSheet.Cells(1, 1).Value = "Mario"
        'oSheet.Cells(2, 1).Value = "Level"
        'oSheet.Cells(2, 2).Value = "HP"
        'oSheet.Cells(2, 3).Value = "BP"
        'oSheet.Cells(2, 4).Value = "Power"
        'oSheet.Cells(2, 5).Value = "Defense"
        'oSheet.Cells(2, 6).Value = "Speed"
        'oSheet.Cells(2, 7).Value = "Stache"
        'oSheet.Cells(2, 8).Value = "Experience"
        For a = 1 To 99
            'oSheet.Cells(a + 2, 1).value = a
            HP(0, a) = xlWorkSheet.Cells(a + 2, 2).value
            BP(0, a) = xlWorkSheet.Cells(a + 2, 3).value
            POWER(0, a) = xlWorkSheet.Cells(a + 2, 4).value
            DEFENSE(0, a) = xlWorkSheet.Cells(a + 2, 5).value
            SPEED(0, a) = xlWorkSheet.Cells(a + 2, 6).value
            STACHE(0, a) = xlWorkSheet.Cells(a + 2, 7).value
            EXP(0, a) = xlWorkSheet.Cells(a + 2, 8).value
        Next
        Button2.Text = "Importing Luigi..."
        'oSheet.Cells(1, 10).Value = "Luigi"
        'oSheet.Cells(2, 10).Value = "Level"
        'oSheet.Cells(2, 11).Value = "HP"
        'oSheet.Cells(2, 12).Value = "BP"
        'oSheet.Cells(2, 13).Value = "Power"
        'oSheet.Cells(2, 14).Value = "Defense"
        'oSheet.Cells(2, 15).Value = "Speed"
        'oSheet.Cells(2, 16).Value = "Stache"
        'oSheet.Cells(2, 17).Value = "Experience"
        For a = 1 To 99
            'oSheet.Cells(a + 2, 10).value = a
            HP(1, a) = xlWorkSheet.Cells(a + 2, 11).value
            BP(1, a) = xlWorkSheet.Cells(a + 2, 12).value
            POWER(1, a) = xlWorkSheet.Cells(a + 2, 13).value
            DEFENSE(1, a) = xlWorkSheet.Cells(a + 2, 14).value
            SPEED(1, a) = xlWorkSheet.Cells(a + 2, 15).value
            STACHE(1, a) = xlWorkSheet.Cells(a + 2, 16).value
            EXP(1, a) = xlWorkSheet.Cells(a + 2, 17).value
        Next
        'oSheet.Range("A1", "G1").EntireColumn.ColumnWidth = 8 'AutoFit()
        'oSheet.Range("H1", "H1").EntireColumn.ColumnWidth = 10
        'oSheet.Range("I1", "P1").EntireColumn.ColumnWidth = 8
        'oSheet.Range("Q1", "Q1").EntireColumn.ColumnWidth = 10
        Button2.Text = "Import to Excel"
        NumericUpDown1_ValueChanged(sender, e)

        xlWorkBook.Close()
        xlApp.Quit()
        xlApp = Nothing
        xlWorkBook = Nothing
        xlWorkSheet = Nothing
    End Sub
End Class