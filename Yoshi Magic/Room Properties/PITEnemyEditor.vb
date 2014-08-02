Public Class PITEnemyEditor
    Dim enemies = &H5C
    Dim attributes = &H2B

    Dim eddata = &H326A00
    Dim edname = &H32C77C '&H32C400

    Dim nameindex(enemies) As Short
    Dim namepointer(enemies) As Integer
    Dim Nametxt(enemies, 16) As Byte
    Dim EnemyName(enemies) As String
    Dim u1(enemies) As Short
    Dim u2(enemies) As Short
    Dim HP(enemies) As Short
    Dim POW(enemies) As Short
    Dim DEF(enemies) As Short
    Dim SPD(enemies) As Short
    Dim u3(enemies) As Short
    Dim u4(enemies) As Short
    Dim EXP(enemies) As Short
    Dim CNS(enemies) As Short

    Private Sub PITEnemyEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName

        FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For a = 0 To enemies
            FileGet(1, nameindex(a), eddata + &H0 + (&H2C * a) + 1)
            FileGet(1, namepointer(a), edname + (nameindex(a) * 4) + 1)
            For b = 0 To 15
                FileGet(1, Nametxt(a, b), edname + namepointer(a) + b + 1)
                EnemyName(a) += Chr(Nametxt(a, b))
            Next
            ListBox1.Items.Add(Hex(a) & " - " & EnemyName(a))
            FileGet(1, u1(a), eddata + &H2 + (&H2C * a) + 1)
            FileGet(1, u2(a), eddata + &H4 + (&H2C * a) + 1)
            FileGet(1, HP(a), eddata + &H6 + (&H2C * a) + 1)
            FileGet(1, POW(a), eddata + &H8 + (&H2C * a) + 1)
            FileGet(1, DEF(a), eddata + &HA + (&H2C * a) + 1)
            FileGet(1, SPD(a), eddata + &HC + (&H2C * a) + 1)
            FileGet(1, u3(a), eddata + &HE + (&H2C * a) + 1)
            FileGet(1, u4(a), eddata + &H10 + (&H2C * a) + 1)
            FileGet(1, EXP(a), eddata + &H20 + (&H2C * a) + 1)
            FileGet(1, CNS(a), eddata + &H22 + (&H2C * a) + 1)
        Next
        FileClose(1)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim a = ListBox1.SelectedIndex
        TextBox1.Text = EnemyName(a)
        TextBox8.Text = Hex(u1(a))
        TextBox9.Text = Hex(u2(a))
        TextBox2.Text = HP(a)
        TextBox3.Text = POW(a)
        TextBox4.Text = DEF(a)
        TextBox5.Text = SPD(a)
        TextBox10.Text = Hex(u3(a))
        TextBox11.Text = Hex(u4(a))
        TextBox6.Text = EXP(a)
        TextBox7.Text = CNS(a)

        ToolStripStatusLabel1.Text = Hex(eddata + (&H2C * a))
    End Sub
End Class