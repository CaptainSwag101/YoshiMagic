Public Class Text_Editor

    Public lines As Integer = 2500 '&H86 
    'The Max number of text lines. (Optional?)
    'Public for Find and Replace form.
    'Story - 0 - 2433 lines
    'Battle - 0 - 204 lines

    Dim namepointer(lines) As Int32
    'Each pointer to each text string is put in here. (Array is optional?)

    Public strtxtline(lines) As String
    'Each text string is placed here. And is the text that will also be saved on saving.

    'Dim storytext(lines, characters) As Byte

    'Dim curitm As Integer
    'Holds the index of the selected item.
    'Used so that the item can be reselected on events where the listbox repopulates with items. 
    '(Form Resize/Scrollbar...)

    'Dim fixlb1si As Boolean = 0
    'Used alongside curitm.

    Public saved As Byte '= 1
    '0 if changes which could be lost were made, else 1.
    'This is specifically used to help know when to show the Save alert. (On closing, and combobox1.)
    
#Region "FIND PREVIOUS/NEXT"
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        find(-1, 0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        find(1, lines)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="direction">-1 for Find Previous; 1 for Find Next.</param>
    ''' <param name="goalindex">Should be 0 for Previous; lines for Next.</param>
    ''' <remarks></remarks>
    Sub find(ByVal direction As SByte, ByVal goalindex As Integer)
        For a = ListBox1.SelectedIndex + direction To goalindex Step direction 'Check items from Selected Item to End.
            'Previous: curitm - 1 to 0 Step -1
            'Next: curitm + 1 to lines Step 1
            If strtxtline(a).Contains(TextBox1.Text) Then ListBox1.SelectedIndex = a : Exit Sub 'GoTo itemfound 'For
        Next a
        MsgBox("No matches.")
        'itemfound:
    End Sub
#End Region
    Private Sub Text_Editor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If saved = 0 Then
            'Me.Focus()
            Select Case MsgBox("Would you like to save before closing?", vbYesNoCancel)
                Case vbYes
                    save()
                    Find_and_Replace.Close()
                Case vbNo
                    Find_and_Replace.Close()
                Case vbCancel
                    e.Cancel = 1
            End Select
        End If
    End Sub
    'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Form1.Show()
    'Me.Close()
    'End Sub
    '#Region "Toolbar:Edit"
    '    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
    '        'Remove Text from text box and place it in Clipboard
    '        Clipboard.SetDataObject(CType(ActiveControl, TextBox).SelectedText)
    '        ActiveControl.Text = String.Empty
    '    End Sub

    '    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
    '        'Copy Text from text box and place it in clipboard
    '        Clipboard.SetDataObject(CType(ActiveControl, TextBox).SelectedText)
    '    End Sub

    '    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
    '        'Retrieve data from clipboard and place it in text box
    '        Dim oDataObject As IDataObject
    '        oDataObject = Clipboard.GetDataObject()
    '        If oDataObject.GetDataPresent(DataFormats.Text) Then
    '            CType(ActiveControl, TextBox).SelectedText = CType(oDataObject.GetData(DataFormats.Text), String)
    '        End If
    '    End Sub
    '#End Region
    '#Region "Toolbar:Options"
    '    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
    '        Form2.Show()
    '    End Sub
    '    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
    '        AboutBox1.Show()
    '    End Sub
    '#End Region

    'Private Sub SaveROMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveROMToolStripMenuItem.Click
    '    SaveFD.InitialDirectory = "C:\"
    '    SaveFD.Title = "Please save your Mario and Luigi: SS ROM"
    '    SaveFD.Filter = "GBA ROM|*.GBA"
    '    SaveFD.OverwritePrompt = True
    '    MsgBox("I'm sorry, but saving the data has not been coded yet.")
    'End Sub

    Private Sub Text_Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.Enter
        'ComboBox1_Enter for mouse/keyboard
        If saved = 0 Then
            'Me.Focus()
            Select Case MsgBox("Would you like to save before changing the text group?  " & _
                               "Changing the text group without saving will cause you to lose changes.", vbYesNo)
                Case vbYes
                    save()
            End Select
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        saved = 1
        If Find_and_Replace.Visible = True Then Find_and_Replace.Button1.Enabled = False ' "Replace" button
        Array.Clear(strtxtline, 0, 2434)
        'If ComboBox1.SelectedIndex = 0 Then
        '    lines = 2433
        'ElseIf ComboBox1.SelectedIndex = 1 Then
        '    lines = 204
        'ElseIf ComboBox1.SelectedIndex = 2 Then
        '    lines = 12
        'End If
        Dim baseoffset As Integer
        Select Case ComboBox1.SelectedIndex
            Case 0
                baseoffset = &H4E8898
                lines = 2433
            Case 1
                baseoffset = &H516E98
                lines = 204
            Case 2
                baseoffset = &H518C88
                lines = 12
        End Select
        'FileOpen(1, OpenFD.FileName, OpenMode.Binary)
        'ListBox1.BeginUpdate()
        ListBox1.Items.Clear()
        Dim letter As Byte
        For a = 0 To lines
            'Dim namepointer(lines) As Int32
            'strtxtline(a) = ""
            'Dim storytext(lines, characters) As Byte
            FileGet(1, namepointer(a), baseoffset + (a * 20) + 1) '(a << 4) + (a << 2)
            'Select Case ComboBox1.SelectedIndex
            '    Case 0
            '        FileGet(1, namepointer(a), &H4E8898 + (a * 20) + 1)  '&H4 * 5 * TextBox2.Text) + 1) 'GET DATA 
            '    Case 1
            '        FileGet(1, namepointer(a), &H516E98 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1) '&H516EFC
            '    Case 2
            '        FileGet(1, namepointer(a), &H518C88 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1)
            'End Select
            'If ComboBox1.SelectedIndex = 0 Then
            '    FileGet(1, namepointer(a), &H4E8898 + (a * 20) + 1) '(a << 4) + (a << 2) '&H4 * 5 * TextBox2.Text) + 1) 'GET DATA 
            'ElseIf ComboBox1.SelectedIndex = 1 Then
            '    FileGet(1, namepointer(a), &H516E98 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1) '&H516EFC
            'ElseIf ComboBox1.SelectedIndex = 2 Then
            '    FileGet(1, namepointer(a), &H518C88 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1)
            'End If
            Seek(1, namepointer(a) - &H8000000 + 1)
            FileGet(1, letter)
            strtxtline(a) += a & " <" & Hex(letter)
            FileGet(1, letter)
            strtxtline(a) += ", " & Hex(letter) & "> "
            Do 'For n = 0 To characters
                FileGet(1, letter)
                Select Case letter
                    Case &HFF
                        FileGet(1, letter)
                        strtxtline(a) += "[" & Hex(letter)
                        Select Case letter
                            Case &H1, &HB To &H11
                                FileGet(1, letter)
                                strtxtline(a) += "=" & Hex(letter)
                        End Select
                        strtxtline(a) += "]"
                    Case &H0 To &H1F
                        strtxtline(a) += "{" & Hex(letter) & "}"
                        If letter = 0 Then Exit Do 'For
                    Case &H5C '\ (Backslash)
                        strtxtline(a) += "\\" 'Add another backslash since first has been made a special char for reading { and [.
                    Case &H5B
                        strtxtline(a) += "\["
                    Case &H7B
                        strtxtline(a) += "\{"
                    Case Else
                        strtxtline(a) += Chr(letter)
                End Select
            Loop 'Next 'n
            'ListBox1.Items.Add(strtxtline(a))
        Next a
        ListBox1.BeginUpdate()
        For a = 0 To lines
            ListBox1.Items.Add(strtxtline(a))
        Next
        ListBox1.SelectedIndex = 0
        ListBox1.EndUpdate()
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        RichTextBox1.Text = strtxtline(ListBox1.SelectedIndex)
        If ComboBox1.SelectedIndex = 0 Then
            ToolStripStatusLabel1.Text = "Pointer Bank: " & Hex(&H4E8898) & _
    "  Pointer: " & Hex(&H4E8898 + (ListBox1.SelectedIndex * 20)) & _
    "  Text: " & Hex(namepointer(ListBox1.SelectedIndex) - &H8000000)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            ToolStripStatusLabel1.Text = "Pointer Bank: " & Hex(&H516E98) & _
    "  Pointer: " & Hex(&H516E98 + (ListBox1.SelectedIndex * 20)) & _
    "  Text: " & Hex(namepointer(ListBox1.SelectedIndex) - &H8000000)
        ElseIf ComboBox1.SelectedIndex = 2 Then
            ToolStripStatusLabel1.Text = "Pointer Bank: " & Hex(&H518C88) & _
    "  Pointer: " & Hex(&H518C88 + (ListBox1.SelectedIndex * 20)) & _
    "  Text: " & Hex(namepointer(ListBox1.SelectedIndex) - &H8000000)
        End If
    End Sub
    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListBox1.TopIndex = TextBox2.Text
    End Sub
    Private Sub save()
        'strtxtline(0).
        Dim thebyte As Byte '= &H0
        Dim ind As Integer
        'MsgBox(Convert.ToByte(strtxtline(0).Substring(strtxtline(0).IndexOf("<") + 1, _
        '                                       strtxtline(0).IndexOf(",") - strtxtline(0).IndexOf("<") - 1), 16))
        ' Convert.ToByte(strtxtline(0).Substring(ind, _
        '                                        strtxtline(0).IndexOf(",") - strtxtline(0).IndexOf("<") - 1), 16)
        'Placepointerto text...
        If ComboBox1.SelectedIndex = 0 Then
            FileGet(1, namepointer(0), &H4E8898 + 1) '(a << 4) + (a << 2) '&H4 * 5 * TextBox2.Text) + 1) 'GET DATA 
        ElseIf ComboBox1.SelectedIndex = 1 Then
            FileGet(1, namepointer(0), &H516E98 + 1) '(&H4 * 5 * TextBox2.Text) + 1) '&H516EFC
        ElseIf ComboBox1.SelectedIndex = 2 Then
            FileGet(1, namepointer(0), &H518C88 + 1) '(&H4 * 5 * TextBox2.Text) + 1)
        End If
        Seek(1, namepointer(0) - &H8000000 + 1)
        For a = 0 To lines '2save
            FilePut(1, thebyte) : FilePut(1, thebyte) : FilePut(1, thebyte)
            namepointer(a) = (&H8000000 + Loc(1)) And &HFFFCS '(word-aligned) (Above is +3 and zero-fill)
            If ComboBox1.SelectedIndex = 0 Then
                FilePut(1, namepointer(a), &H4E8898 + (a * 20) + 1) '(a << 4) + (a << 2) '&H4 * 5 * TextBox2.Text) + 1) 'GET DATA 
            ElseIf ComboBox1.SelectedIndex = 1 Then
                FilePut(1, namepointer(a), &H516E98 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1) '&H516EFC
            ElseIf ComboBox1.SelectedIndex = 2 Then
                FilePut(1, namepointer(a), &H518C88 + (a * 20) + 1) '(&H4 * 5 * TextBox2.Text) + 1)
            End If
            Seek(1, namepointer(a) - &H8000000 + 1) 'Set Loc to write
            ind = strtxtline(a).IndexOf("<") + 1 ' Set ind to read
            'Byte 1 of string
            thebyte = Convert.ToByte(strtxtline(a).Chars(ind), 16)
            ind += 1
            If strtxtline(a).Chars(ind) <> "," Then
                thebyte = (thebyte << 4) + Convert.ToByte(strtxtline(a).Chars(ind), 16)
                ind += 1
            End If
            If strtxtline(a).Chars(ind) = "," Then
                'MsgBox(thebyte)
                'Seek(1, Loc(1) + 1 + 1)
                FilePut(1, thebyte)
                ind += 1
                If strtxtline(a).Chars(ind) = " " Then ind += 1
            End If
            'Byte 2 of string
            thebyte = Convert.ToByte(strtxtline(a).Chars(ind), 16)
            ind += 1
            If strtxtline(a).Chars(ind) <> ">" Then
                thebyte = (thebyte << 4) + Convert.ToByte(strtxtline(a).Chars(ind), 16)
                ind += 1
            End If
            If strtxtline(a).Chars(ind) = ">" Then
                FilePut(1, thebyte)
                ind += 1
                If strtxtline(a).Chars(ind) = " " Then ind += 1
            End If
            'Byte 3+
            Do 'For b = 0 To characters
                If strtxtline(a).Chars(ind) = "[" Then
                    thebyte = &HFF
                    FilePut(1, thebyte)
a1:                 ind += 1
                    thebyte = Convert.ToByte(strtxtline(a).Chars(ind), 16)
                    ind += 1
                    If strtxtline(a).Chars(ind) <> "]" Then
                        If strtxtline(a).Chars(ind) <> "=" Then
                            thebyte = (thebyte << 4) + Convert.ToByte(strtxtline(a).Chars(ind), 16)
                            ind += 1
                        End If
                    End If
                    FilePut(1, thebyte)
                    If strtxtline(a).Chars(ind) = "=" Then GoTo a1
                    If strtxtline(a).Chars(ind) = "]" Then ind += 1
                ElseIf strtxtline(a).Chars(ind) = "{" Then
                    ind += 1
                    thebyte = Convert.ToByte(strtxtline(a).Chars(ind), 16)
                    ind += 1
                    If strtxtline(a).Chars(ind) <> "}" Then
                        thebyte = (thebyte << 4) + Convert.ToByte(strtxtline(a).Chars(ind), 16)
                        ind += 1
                    End If
                    FilePut(1, thebyte)
                    If thebyte = 0 Then Exit Do 'For 'End of Line
                    If strtxtline(a).Chars(ind) = "}" Then ind += 1
                ElseIf strtxtline(a).Chars(ind) = "\" Then
                    ind += 1
                    thebyte = Asc(strtxtline(a).Chars(ind))
                    FilePut(1, thebyte)
                    ind += 1
                Else
                    thebyte = Asc(strtxtline(a).Chars(ind))
                    FilePut(1, thebyte)
                    ind += 1
                End If
            Loop
        Next a
        MsgBox("Saved!")
        saved = 1
    End Sub
    'EDIT BUTTON
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If strtxtline(ListBox1.SelectedIndex) <> RichTextBox1.Text Then
            saved = 0
            strtxtline(ListBox1.SelectedIndex) = RichTextBox1.Text
            ListBox1.BeginUpdate()
            ListBox1.Items.Insert(ListBox1.SelectedIndex, RichTextBox1.Text)
            ListBox1.SelectedIndex -= 1
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex + 1)
            ListBox1.EndUpdate()
        End If
    End Sub
    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        save()
    End Sub
    Private Sub FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FToolStripMenuItem.Click
        Find_and_Replace.Visible = True
    End Sub
    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        Form2.Show()
    End Sub

    Private Sub Panel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
        '(((MousePosition.Y.ToString - Panel1.PointToScreen(New Point(0, 0)).Y) >> 4)<<6 ) or ((MousePosition.X.ToString - Panel1.PointToScreen(New Point(0, 0)).X) >> 4)
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Dim enemytext As New Bitmap(1100, 50) ' 21)
        'Dim txtx As Short = 96 'Text at (96, 6)
        'Dim spaces(TextBox1.Text.Length - 1) As Byte
        'Decrement text by charwidth-1.
        'For a = 0 To TextBox1.Text.Length - 1
        '    'MsgBox(Asc(TextBox1.Text.Chars(TextBox1.Text.Length - 1 - a)))
        '    FileGet(1, spaces(a), &H51E120 + (Asc(TextBox1.Text.Chars(a)) >> 1) + 1)
        '    spaces(a) = (spaces(a) >> ((Asc(TextBox1.Text.Chars(a)) And 1) << 2)) And &HF
        '    txtx -= (spaces(a) + 1)
        'Next
        'If txtx < 0 Then TextBox1.ForeColor = Color.Red : GoTo cleanup
        'TextBox1.ForeColor = Color.Black
        'Dim fcolors() As Color = {Color.Transparent, Color.White, Color.FromArgb(255, 40, 96, 240)}
        Dim fcolors() As Color = {Color.Transparent, Color.FromArgb(255, 48, 48, 48), Color.FromArgb(255, 208, 200, 216)}
        ': fcolors(0) = Color.Transparent : fcolors(1) = Color.White : fcolors(2) = Color.FromArgb(255, 40, 96, 240)
        For row = 0 To 3
            For a = 0 To &H3F 'TextBox1.Text.Length - 1
                For b = 0 To 2
                    Dim cblock As Long : Dim colorbyte As Byte
                    'FileGet(1, cblock, &H51E1A0 + (row * &H600) + (a * &H18) + (b << 3) + 1)
                    'FileGet(1, cblock, &H51C91C + (row * &H600) + (a * &H18) + (b << 3) + 1)
                    FileGet(1, cblock, &H51B098 + (row * &H600) + (a * &H18) + (b << 3) + 1)
                    For x = 0 To 7
                        For y = 0 To 3
                            colorbyte = ((cblock >> (x << 2) >> y) And 1) + (((cblock >> &H20) >> (x << 2) >> y) And 1)
                            enemytext.SetPixel((a << 3) + x, (row * 12) + (b << 2) + y, fcolors(colorbyte))
                        Next
                    Next
                Next
                'txtx += spaces(a) + 1
            Next
        Next
        Panel1.CreateGraphics.DrawImage(enemytext, New Point(0, 0))
cleanup:  'enemytext2 = enemytext
        'Panel3.Refresh()
    End Sub

End Class