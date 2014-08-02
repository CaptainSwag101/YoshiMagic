Public Class Find_and_Replace
    Dim founditms(&H1500) '1433 'For Ctrl+Clicking Checkedlistbox Items
    Dim findwhat As String
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Find Button
        ComboBox1.Items.Add(ComboBox1.Text)
        Array.Clear(founditms, 0, CheckedListBox1.Items.Count)
        CheckedListBox1.Items.Clear()
        Dim ind = 0
        For a = 0 To Text_Editor.lines
            Try
                'If String.Compare(ComboBox1.Text) Then
                If Text_Editor.strtxtline(a).Contains(ComboBox1.Text) Then
                    CheckedListBox1.Items.Add(Text_Editor.strtxtline(a))
                    founditms(ind) = a : ind += 1
                End If
            Catch ex As Exception
                Exit For
            End Try
        Next a
        If CheckedListBox1.Items.Count > 0 Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
        Else
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            MsgBox("Yoshi can not find any matches.")
        End If
        Label4.Text = CheckedListBox1.Items.Count
        findwhat = ComboBox1.Text
    End Sub
    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.GotFocus
        Me.AcceptButton = Button5
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus
        Me.AcceptButton = Nothing
    End Sub
    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        If findwhat = ComboBox1.Text Then Button1.Enabled = True Else Button1.Enabled = False
        If ComboBox1.Text = "" Then
            Button5.Enabled = False
            Button1.Enabled = False
        Else
            Button5.Enabled = True
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For a = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(a, True)
        Next
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For a = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(a, False)
        Next
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For a = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(a, Not CheckedListBox1.GetItemChecked(a))
        Next
    End Sub
    Private Sub CheckedListBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox1.Click
        If ModifierKeys = Keys.Control Then Text_Editor.ListBox1.SelectedIndex = founditms(CheckedListBox1.SelectedIndex)
    End Sub
    Private Sub ComboBox2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.GotFocus
        Me.AcceptButton = Button1
    End Sub
    Private Sub ComboBox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.LostFocus
        Me.AcceptButton = Nothing
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Text_Editor.saved = 0
        ComboBox2.Items.Add(ComboBox2.Text)
        Dim theind = 0
        For a = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(a) = True Then
                'Text_Editor.strtxtline(founditms(a)).Replace(ComboBox1.Text, ComboBox2.Text)
                theind = Text_Editor.strtxtline(founditms(a)).IndexOf(ComboBox1.Text)
                Try
repeat:             Text_Editor.strtxtline(founditms(a)) = Text_Editor.strtxtline(founditms(a)).Remove( _
                    theind, _
                    ComboBox1.Text.Length)
                    Text_Editor.strtxtline(founditms(a)) = Text_Editor.strtxtline(founditms(a)).Insert( _
                    theind, _
                    ComboBox2.Text)
                    theind = Text_Editor.strtxtline(founditms(a)).IndexOf(ComboBox1.Text, theind + ComboBox2.Text.Length)
                    GoTo repeat
                Catch
                End Try
            End If
        Next
        Text_Editor.ListBox1.BeginUpdate()
        Text_Editor.ListBox1.Items.Clear()
        For a = 0 To Text_Editor.lines
            Text_Editor.ListBox1.Items.Add(Text_Editor.strtxtline(a))
        Next
        Text_Editor.ListBox1.EndUpdate()
        MsgBox("All occurances have been replaced!")
        CheckedListBox1.Items.Clear()
    End Sub
End Class