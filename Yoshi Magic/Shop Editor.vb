Class Shop_Editor
    Dim shops(3) As Integer 'Shop Pointers
    Dim shop1(19) As Short
    Dim shop2(40) As Short
    Dim shop3(100) As Short
    Dim shop4(100) As Short


    Dim Items = &H18

    Dim ItemName(Items) As String
    Dim namepointer(Items) As Int32
    Dim Nametxt(Items, 20) As Byte

    Private Sub Shop_Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For a = 0 To 3
            FileGet(1, shops(a), &H211AC0 + (a << 2) + 1)
        Next
        For a = 0 To 19 'SHOP1
            FileGet(1, shop1(a), shops(0) - &H8000000 + (a << 1) + 1)
        Next
        For a = 0 To 40 '2
            FileGet(1, shop2(a), shops(1) - &H8000000 + (a << 1) + 1)
        Next
        For a = 0 To 100
            FileGet(1, shop3(a), shops(2) - &H8000000 + (a << 1) + 1)
        Next
        For a = 0 To 100
            FileGet(1, shop4(a), shops(3) - &H8000000 + (a << 1) + 1)
        Next

        For a = 0 To Items 'Panel Creations of Items from 0 - 25
            'START GETTING ITEM TEXT DATA
            FileGet(1, namepointer(a), &H3BBDDC + (a << 4) + 1) 'GET DATA 
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1) 'GET DATA
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1) 'GET DATA 
            For n = 0 To 25
                FileGet(1, Nametxt(a, n), namepointer(a) - &H8000000 + n + 1) 'GET DATA
                ItemName(a) += Chr(Nametxt(a, n)) 'Copy Item Names from ROM to DIMs.
                If Nametxt(a, n) = 0 Then GoTo Exita
            Next n
        Next a
Exita:
        'FileClose(1)
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 2
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    'End Sub

    'Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
    '    ListBox1.Items.Add("")
    'End Sub
End Class