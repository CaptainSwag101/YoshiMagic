Public Class Item_Editor
    Dim letter As Byte
    Dim txtaddr As Integer
    ' ITEM
    Dim Items = &H18 'total number of Items
    Dim ItemName(Items) As String
    Dim namepointer(Items) As Int32
    Dim BlockName(Items) As String
    Dim Desc(Items) As String
    Dim Descpointer(Items) As Int32
    Dim scicon(Items) As Byte ' 'Suitcase Item Icons
    Dim stacheval(Items) As Byte
    Dim priceval(Items) As Short
    Dim amountval(Items) As Short
    ' KEY ITEM
    Dim kitems = &H33
    Dim kItemName(kitems) As String
    Dim knamepointer(kitems) As Int32
    Dim kDesc(kitems) As String
    Dim kDescpointer(kitems) As Int32
    ' BEAN
    Dim bitems = &H3
    Dim bItemName(bitems) As String
    Dim bnamepointer(bitems) As Int32
    Dim bBlockName(bitems) As String
    Dim bDesc(bitems) As String
    Dim bDescpointer(bitems) As Int32
    ' Badges
    Dim baitems = &H2B
    Dim baItemName(baitems) As String
    Dim banamepointer(baitems) As Int32
    Dim baDesc(baitems) As String
    Dim baDescpointer(baitems) As Int32
    ' Gear
    Dim gitems = &H2D
    Dim gItemName(gitems) As String
    Dim gnamepointer(gitems) As Int32
    Dim gDesc(gitems) As String
    Dim gDescpointer(gitems) As Int32
    Dim wearability(gitems) As Byte
    ' Pins
    Dim pitems = &H6
    Dim pItemName(pitems) As String
    Dim pnamepointer(pitems) As Int32
    Dim pDesc(pitems) As String
    Dim pDescpointer(pitems) As Int32

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'OpenFileDialog1.FileName = Form1.OpenFileDialog1.FileName
        Timer1.Enabled = True
        'ComboBox1.SelectedIndex = 0 'We only have Items menu available so far.
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For a = 0 To Items 'Items
            FileGet(1, namepointer(a), &H3BBDDC + (a << 4) + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            Seek(1, namepointer(a) - &H8000000 + 1)
            Do 'Until letter = 0
                FileGet(1, letter)
                ItemName(a) += Chr(letter)
                If letter = 0 Then
                    If a < 18 Then 'Items 18+ do not have block text?
                        Do
                            FileGet(1, letter)
                            BlockName(a) += Chr(letter)
                            If letter = 0 Then GoTo Exita
                        Loop
                    End If
                    GoTo Exita
                End If
            Loop
Exita:      'ListBox1.Items.Add(Hex(a) & " - " & ItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(0).Nodes.Add(Hex(a) & " - " & ItemName(a))

            'Item Descriptions
            FileGet(1, Descpointer(a), &H3BBDDC + (a << 4) + 1)
            FileGet(1, Descpointer(a), Descpointer(a) - &H8000000 + 1)
            FileGet(1, Descpointer(a), Descpointer(a) - &H8000000 + 4 + 1)
            Seek(1, Descpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                Desc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop

            'Icon Data
            FileGet(1, scicon(a), &H3BBDD2 + (a << 4) + 1)
            TreeView1.Nodes(0).Nodes(a).ImageIndex = scicon(a)
            TreeView1.Nodes(0).Nodes(a).selectedImageIndex = scicon(a)
            'Numeral Data
            FileGet(1, amountval(a), &H3BBDD8 + (a << 4) + 1)
            FileGet(1, priceval(a), &H3BBDDA + (a << 4) + 1)
            FileGet(1, stacheval(a), &H3BBDD3 + (a << 4) + 1)
        Next a

        For a = 0 To kitems 'Key Items
            FileGet(1, knamepointer(a), &H3BCA64 + (&HC * a) + 1)
            FileGet(1, knamepointer(a), knamepointer(a) - &H8000000 + 1)
            FileGet(1, knamepointer(a), knamepointer(a) - &H8000000 + 1)
            Seek(1, knamepointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                kItemName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            'ListBox2.Items.Add(Hex(a) & " - " & kItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(1).Nodes.Add(Hex(a) & " - " & kItemName(a))

            'Item Descriptions
            FileGet(1, kDescpointer(a), &H3BCA64 + (&HC * a) + 1)
            FileGet(1, kDescpointer(a), kDescpointer(a) - &H8000000 + 1)
            FileGet(1, kDescpointer(a), kDescpointer(a) - &H8000000 + 4 + 1)
            Seek(1, kDescpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                kDesc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            TreeView1.Nodes(1).Nodes(a).ImageIndex = 19
            TreeView1.Nodes(1).Nodes(a).selectedImageIndex = 19
        Next

        For a = 0 To bitems 'Beans
            FileGet(1, bnamepointer(a), &H3BCDC4 + (&HC * a) + 1)
            FileGet(1, bnamepointer(a), bnamepointer(a) - &H8000000 + 1)
            FileGet(1, bnamepointer(a), bnamepointer(a) - &H8000000 + 1)
            Seek(1, bnamepointer(a) - &H8000000 + 1)
            Do 'Primary Name
                FileGet(1, letter)
                bItemName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            Do 'Secondary Name
                FileGet(1, letter)
                bBlockName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop ' ListBox3.Items.Add(Hex(a) & " - " & bItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(2).Nodes.Add(Hex(a) & " - " & bItemName(a))

            'Item Descriptions
            FileGet(1, bDescpointer(a), &H3BCDC4 + (&HC * a) + 1)
            FileGet(1, bDescpointer(a), bDescpointer(a) - &H8000000 + 1)
            FileGet(1, bDescpointer(a), bDescpointer(a) - &H8000000 + 4 + 1)
            Seek(1, bDescpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                bDesc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
        Next a
        TreeView1.Nodes(2).Nodes(0).ImageIndex = 9
        TreeView1.Nodes(2).Nodes(0).SelectedImageIndex = 9
        TreeView1.Nodes(2).Nodes(1).ImageIndex = 10
        TreeView1.Nodes(2).Nodes(1).SelectedImageIndex = 10
        TreeView1.Nodes(2).Nodes(2).ImageIndex = 11
        TreeView1.Nodes(2).Nodes(2).SelectedImageIndex = 11
        TreeView1.Nodes(2).Nodes(3).ImageIndex = 12
        TreeView1.Nodes(2).Nodes(3).SelectedImageIndex = 12

        For a = 0 To baitems 'Badges
            FileGet(1, banamepointer(a), &H3BD844 + (&H14 * a) + 1)
            FileGet(1, banamepointer(a), banamepointer(a) - &H8000000 + 1)
            FileGet(1, banamepointer(a), banamepointer(a) - &H8000000 + 1)
            Seek(1, banamepointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                baItemName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            'ListBox4.Items.Add(Hex(a) & " - " & baItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(3).Nodes.Add(Hex(a) & " - " & baItemName(a))

            'Item Descriptions
            FileGet(1, baDescpointer(a), &H3BD844 + (&H14 * a) + 1)
            FileGet(1, baDescpointer(a), baDescpointer(a) - &H8000000 + 1)
            FileGet(1, baDescpointer(a), baDescpointer(a) - &H8000000 + 4 + 1)
            Seek(1, baDescpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                baDesc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            TreeView1.Nodes(3).Nodes(a).ImageIndex = 14
            TreeView1.Nodes(3).Nodes(a).selectedImageIndex = 14
        Next

        For a = 0 To gitems 'Gear
            FileGet(1, gnamepointer(a), &H3BE67C + (&H14 * a) + 1)
            FileGet(1, gnamepointer(a), gnamepointer(a) - &H8000000 + 1)
            FileGet(1, gnamepointer(a), gnamepointer(a) - &H8000000 + 1)
            Seek(1, gnamepointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                gItemName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            'ListBox5.Items.Add(Hex(a) & " - " & gItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(4).Nodes.Add(Hex(a) & " - " & gItemName(a))

            'Item Descriptions
            FileGet(1, gDescpointer(a), &H3BE67C + (&H14 * a) + 1)
            FileGet(1, gDescpointer(a), gDescpointer(a) - &H8000000 + 1)
            FileGet(1, gDescpointer(a), gDescpointer(a) - &H8000000 + 4 + 1)
            Seek(1, gDescpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                gDesc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop

            'Wearability Data
            FileGet(1, wearability(a), &H3BE68D + (&H14 * a) + 1)
            TreeView1.Nodes(4).Nodes(a).ImageIndex = wearability(a) + 14
            TreeView1.Nodes(4).Nodes(a).selectedImageIndex = wearability(a) + 14
        Next

        For a = 0 To pitems 'Pins
            FileGet(1, pnamepointer(a), &H3BEBB8 + (a << 3) + 1)
            FileGet(1, pnamepointer(a), pnamepointer(a) - &H8000000 + 1)
            FileGet(1, pnamepointer(a), pnamepointer(a) - &H8000000 + 1)
            Seek(1, pnamepointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                pItemName(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            'ListBox6.Items.Add(Hex(a) & " - " & pItemName(a)) 'Adds the list of Items.
            TreeView1.Nodes(5).Nodes.Add(Hex(a) & " - " & pItemName(a))

            'Item Descriptions
            FileGet(1, pDescpointer(a), &H3BEBB8 + (a << 3) + 1)
            FileGet(1, pDescpointer(a), pDescpointer(a) - &H8000000 + 1)
            FileGet(1, pDescpointer(a), pDescpointer(a) - &H8000000 + 4 + 1)
            Seek(1, pDescpointer(a) - &H8000000 + 1)
            Do
                FileGet(1, letter)
                pDesc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop

            TreeView1.Nodes(5).Nodes(a).ImageIndex = 18
            TreeView1.Nodes(5).Nodes(a).selectedImageIndex = 18
        Next
        'FileClose(1)
        'ListBox1.SelectedIndex = 0 'Select the first enemy, as this is based on resizing.
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Remove Text from text box and place it in Clipboard
        Clipboard.SetDataObject(CType(ActiveControl, TextBox).SelectedText)
        ActiveControl.Text = String.Empty
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Copy Text from text box and place it in clipboard
        Clipboard.SetDataObject(CType(ActiveControl, TextBox).SelectedText)
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Retrieve data from clipboard and place it in text box
        Dim oDataObject As IDataObject
        oDataObject = Clipboard.GetDataObject()
        If oDataObject.GetDataPresent(DataFormats.Text) Then
            CType(ActiveControl, TextBox).SelectedText = CType(oDataObject.GetData(DataFormats.Text), String)
        End If
    End Sub

    'Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    AboutBox1.Show()
    'End Sub

    Private Sub SaveROMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveROMToolStripMenuItem.Click
        'On Error GoTo err
        'MsgBox("I'm sorry, but Saving has not been coded yet.") : Exit Sub
        ''''' Anything below here needs fixing.
        Dim ind As Integer
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        FileGet(1, namepointer(0), &H3BBDDC + 1)
        FileGet(1, namepointer(0), namepointer(0) - &H8000000 + 1)
        FileGet(1, txtaddr, namepointer(0) - &H8000000 + 1)
        For a = 0 To &H18 'Items
            Seek(1, txtaddr - &H8000000 + 1)
            ind = 0
            'Try 'START PUTTING IN ITEM NAME DATA (plus Automatic Repointering)
            'MsgBox(Hex(Loc(1)))
            FilePut(1, CByte(0)) : FilePut(1, CByte(0)) : FilePut(1, CByte(0))
            txtaddr = (&H8000000 + Loc(1)) And &HFFFCS
            'MsgBox(Hex(Loc(1)))
            FileGet(1, namepointer(a), &H3BBDDC + (a << 4) + 1)
            FileGet(1, namepointer(a), namepointer(a) - &H8000000 + 1)
            FilePut(1, txtaddr, namepointer(a) - &H8000000 + 1)
            Seek(1, txtaddr - &H8000000 + 1)
            Do 'Until letter = 0
                Try
                    letter = Asc(ItemName(a).Chars(ind))
                Catch ex As Exception
                    letter = 0
                End Try
                ind += 1
                'MsgBox(Hex(Loc(1)) & " : " & letter & " / " & Chr(letter))
                FilePut(1, letter)
                'MsgBox(Hex(txtaddr))
                If letter = 0 Then
                    'MsgBox(Hex(txtaddr) & "!!!")
                    ind = 0
                    If a < 18 Then 'Items 18+ do not have block text?
                        Do
                            Try
                                letter = Asc(BlockName(a).Chars(ind))
                            Catch ex As Exception
                                letter = 0
                            End Try
                            ind += 1
                            'MsgBox(Hex(Loc(1)) & " :: " & letter & " // " & Chr(letter))
                            FilePut(1, letter)
                            If letter = 0 Then GoTo Exita
                        Loop
                    End If
                    GoTo Exita
                End If
            Loop
exita:      'Exit Sub
            FilePut(1, CByte(0)) : FilePut(1, CByte(0)) : FilePut(1, CByte(0))
            txtaddr = (&H8000000 + Loc(1)) And &HFFFCS
            FilePut(1, txtaddr, namepointer(a) - &H8000000 + 4 + 1)
            Seek(1, txtaddr - &H8000000 + 1)
            ind = 0
            Do
                Try
                    letter = Asc(Desc(a).Chars(ind))
                Catch ex As Exception
                    letter = 0
                End Try
                ind += 1
                FilePut(1, letter)
                'Desc(a) += Chr(letter)
                If letter = 0 Then Exit Do
            Loop
            txtaddr = (&H8000000 + Loc(1)) 'And &HFFFCS
            'FileGet(1, namepointer(a), &H3BBDDC + (a << 4) + 1)
            'FileGet(1, namepointer(a), namepointer(a) + 1)
            'Dim repointer As Integer = &H82033F0 + (a * &H11)
            'FilePut(1, repointer, &H500000 + namepointer(a) + 1)
            'FileGet(1, namepointer(a), &H500000 + namepointer(a) + 1)
            'For n = 0 To 16
            '    If n < ItemName(a).Count Then
            '        letter = Asc(ItemName(a).Chars(n))
            '    Else
            '        For o = n To 16
            '            letter = 0
            '        Next
            '    End If
            '    FilePut(1, letter, &H220000 + namepointer(a) + n + 1)
            'Next n

            'Updating Listbox items.
            'If ListBox1.Items.Item(a) <> ItemName(a) Then
            'ListBox1.Items.Item(a) = ItemName(a)
            'End If
            FilePut(1, amountval(a), &H3BBDD8 + (a << 4) + 1)
            FilePut(1, priceval(a), &H3BBDDA + (a << 4) + 1)
            FilePut(1, stacheval(a), &H3BBDD3 + (a << 4) + 1)
            'Catch
            'Error Reports
            'ListBox1.SelectedIndex = a ' So we can get the actual error picture we want
            'err:            'printscreen
            'Refresh()
            'SendKeys.SendWait("%{PRTSC}")
            ' Dialog1.Label6.Text = Err.Number

            'Dialog1.Label2.Text = Err.Description
            'Dialog1.Label4.Text = ErrorToString() 'LastDllError
            'Dialog1.Opacity = 0.5
            'Dialog1.ShowDialog() '.Visible = True
            'MsgBox(Err.Description, vbOKOnly, "Error: " & Err.Number)
            ''FileClose(1) 'Errors skip over the above and we mustn't skip 'FileClose(1)

            'GoTo err
            'End Try

        Next a
        'FileClose(1)
        MsgBox("Yoshi has successfully saved your data.")
        Exit Sub
err:    'FileClose(1)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'On Error GoTo err
        'START PUTTING DATA IN ROM
        SaveFD.Filter = "GBA file(*.gba)|*.gba"
        SaveFD.ShowDialog()
        '//////////////////////////////////////////////////
        'SAVE AS...
        'My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, SaveFD.FileName, True)
        'OpenFileDialog1.FileName = SaveFD.FileName
        '//////////////////////////////////////////////////
        'FileCopy(OpenFileDialog1.FileName, SaveFD.FileName)
        'On Error GoTo err
        'START PUTTING DATA IN ROOM
        'FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary)
        For a = 0 To &H18 'Items
            Try
                'START PUTTING IN Item NAME DATA (plus Automatic Repointering)
                'FileGet(1, namepointer(a), &H500A98 + (&H10 * a) + 1)
                'FileGet(1, namepointer(a), &H500000 + namepointer(a) + 1)
                'Dim repointer As Integer = &H82033F0 + (a * &H11)
                'FilePut(1, repointer, &H500000 + namepointer(a) + 1)
                'FileGet(1, namepointer(a), &H500000 + namepointer(a) + 1)
                'For n = 0 To 16
                'If n < ItemName(a).Text.Count Then
                'Nametxt(a, n) = Asc(ItemName(a).Text.Chars(n))
                'Else
                'For o = n To 16
                'Nametxt(a, o) = 0
                'Next
                'End If
                'FilePut(1, Nametxt(a, n), &H220000 + namepointer(a) + n + 1)
                'Next n
                'Updating Listbox items.
                'If ListBox1.Items.Item(a) <> ItemName(a).Text Then
                'ListBox1.Items.Item(a) = ItemName(a).Text
                'End If
                'FINISHED PUTTING IN ENEMY NAME DATA

                'If panelcreated(a) = True Then

                'START PUTTING IN AMOUNT DATA
                'amountval(a) = amount(a).Text
                'FilePut(1, amountval(a), &H3BBDD8 + (&H10 * a) + 1)
                'FINISHED PUTTING IN AMOUNT DATA

                'START PUTTING IN PRICE DATA
                'priceval(a) = price(a).Text
                'FilePut(1, priceval(a), &H3BBDDA + (&H10 * a) + 1)
                'FINISHED PUTTING IN PRICE DATA

                'START PUTTING IN STACHE DATA
                'stacheval(a) = stache(a).Text
                'FilePut(1, stacheval(a), &H3BBDD3 + (&H10 * a) + 1)
                'FINISHED PUTTING IN PRICE DATA
                'End If
            Catch
                'Error Reports
                'ListBox1.SelectedIndex = a ' So we can get the actual error picture we want
                'err:            'printscreen
                Refresh()
                SendKeys.SendWait("%{PRTSC}")
                'Dialog1.Label6.Text = Err.Number

                'Dialog1.Label2.Text = Err.Description
                'Dialog1.Label4.Text = ErrorToString() 'LastDllError
                'Dialog1.Opacity = 0.5
                'Dialog1.ShowDialog() '.Visible = True
                'MsgBox(Err.Description, vbOKOnly, "Error: " & Err.Number)
                ''FileClose(1) 'Errors skip over the above and we mustn't skip 'FileClose(1)

                GoTo err
            End Try

        Next a
        'FileClose(1)
        MsgBox("Yoshi has successfully saved your data.")
        Exit Sub
err:    'FileClose(1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''For FIND = ListBox1.SelectedIndex + 1 To ListBox1.Items.Count
        ''Try
        ''ListBox1.SelectedIndex = FIND
        'loadpanel = False
        ''Try
        'ListBox1.SetSelected
        ''ListBox1.FindString(ListBox1.Text.Substring(0, ListBox1.Text.IndexOf(TextBox7.Text)) + TextBox7.Text, FIND - 1) ', True)
        'ListBox1.SelectedIndex = FIND
        'ListBox1.SetSelected(ListBox1.FindString(ListBox1.Text.Substring(0, ListBox1.Text.IndexOf(TextBox7.Text)) + TextBox7.Text, FIND - 1), True)
        'loadpanel = True
        'Below is to make the whole panel "load"
        ''ListBox1.SelectedIndex = -1
        ''ListBox1.SelectedIndex = FIND
        '' Exit Sub
        ''Catch

        'If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
        'ListBox1.SelectedIndex = "-1"
        'MsgBox("Yoshi can not find any more matches.")
        'End If
        ''End Try
        ''Catch
        ''ListBox1.SelectedIndex = "-1"
        'loadpanel = True
        '' MsgBox("Yoshi can not find any more matches.")
        '' End Try
        '' Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolTip1.SetToolTip(TrackBar1, TrackBar1.Value)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'ItemName(ListBox1.SelectedIndex) = TextBox1.Text
        Try
            Select Case TreeView1.SelectedNode.Parent.Index
                Case 0
                    ItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
                Case 1
                    kItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
                Case 2
                    bItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
                Case 3
                    baItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
                Case 4
                    gItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
                Case 5
                    pItemName(TreeView1.SelectedNode.Index) = TextBox1.Text
            End Select
            TextBox1.ForeColor = Color.Black
        Catch
            TextBox1.ForeColor = Color.Red
        End Try
        'MsgBox(TreeView1.SelectedNode.Index)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'BlockName(ListBox1.SelectedIndex) = TextBox2.Text
        Try
            Select Case TreeView1.SelectedNode.Parent.Index
                Case 0
                    BlockName(TreeView1.SelectedNode.Index) = TextBox2.Text
                Case 2
                    bBlockName(TreeView1.SelectedNode.Index) = TextBox2.Text
            End Select
            TextBox2.ForeColor = Color.Black
        Catch
            TextBox2.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'Desc(ListBox1.SelectedIndex) = TextBox3.Text
        Try
            Select Case TreeView1.SelectedNode.Parent.Index
                Case 0
                    Desc(TreeView1.SelectedNode.Index) = TextBox3.Text
                Case 1
                    kDesc(TreeView1.SelectedNode.Index) = TextBox3.Text
                Case 2
                    bDesc(TreeView1.SelectedNode.Index) = TextBox3.Text
                Case 3
                    baDesc(TreeView1.SelectedNode.Index) = TextBox3.Text
                Case 4
                    gDesc(TreeView1.SelectedNode.Index) = TextBox3.Text
                Case 5
                    pDesc(TreeView1.SelectedNode.Index) = TextBox3.Text
            End Select
            TextBox3.ForeColor = Color.Black
        Catch
            TextBox3.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        'amountval(ListBox1.SelectedIndex) = TextBox4.Text
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        'priceval(ListBox1.SelectedIndex) = TextBox5.Text
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        'stacheval(ListBox1.SelectedIndex) = TrackBar1.Value
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        Dim a = TreeView1.SelectedNode.Index

        If TreeView1.SelectedNode.Level = 0 Then
            'Don't want the parent nodes to do anything.
            Exit Sub
        End If

        'Items
        If TreeView1.SelectedNode.Parent.Index = 0 Then
            TextBox1.Text = ItemName(a)
            TextBox2.Text = BlockName(a)
            TextBox3.Text = Desc(a)
            TextBox4.Text = amountval(a)
            TextBox5.Text = priceval(a)
            TrackBar1.Value = stacheval(a)

            If a > 17 Then
                TextBox2.Enabled = False 'We won't be able to save this right now, anyway?
            Else
                TextBox2.Enabled = True
            End If
        End If

        'Key Items
        If TreeView1.SelectedNode.Parent.Index = 1 Then
            TextBox2.Enabled = False
            TextBox1.Text = kItemName(a)
            TextBox2.Text = "" 'kBlockName(a)
            TextBox3.Text = kDesc(a)
            TextBox4.Text = "" 'amountval(a)
            TextBox5.Text = "" 'priceval(a)
            TrackBar1.Value = 0 'stacheval(a)
        End If

        'Beans
        If TreeView1.SelectedNode.Parent.Index = 2 Then
            TextBox2.Enabled = True
            TextBox1.Text = bItemName(a)
            TextBox2.Text = bBlockName(a)
            TextBox3.Text = bDesc(a)
            TextBox4.Text = "" 'amountval(a)
            TextBox5.Text = "" 'priceval(a)
            TrackBar1.Value = 0 'stacheval(a)
        End If

        'Badges
        If TreeView1.SelectedNode.Parent.Index = 3 Then
            TextBox2.Enabled = False
            TextBox1.Text = baItemName(a)
            TextBox2.Text = "" 'BlockName(a)
            TextBox3.Text = baDesc(a)
            TextBox4.Text = "" 'amountval(a)
            TextBox5.Text = "" 'priceval(a)
            TrackBar1.Value = 0 'stacheval(a)
        End If

        'Key Items
        If TreeView1.SelectedNode.Parent.Index = 4 Then
            TextBox2.Enabled = False
            TextBox1.Text = gItemName(a)
            TextBox2.Text = "" 'kBlockName(a)
            TextBox3.Text = gDesc(a)
            TextBox4.Text = "" 'amountval(a)
            TextBox5.Text = "" 'priceval(a)
            TrackBar1.Value = 0 'stacheval(a)
        End If

        'Key Items
        If TreeView1.SelectedNode.Parent.Index = 5 Then
            TextBox2.Enabled = False
            TextBox1.Text = pItemName(a)
            TextBox2.Text = "" 'kBlockName(a)
            TextBox3.Text = pDesc(a)
            TextBox4.Text = "" 'amountval(a)
            TextBox5.Text = "" 'priceval(a)
            TrackBar1.Value = 0 'stacheval(a)
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For b = 0 To 25
            For a = 0 To &H30
                Try
                    If ItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(0).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
                Try
                    If kItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(1).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
                Try
                    If bItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(2).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
                Try
                    If baItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(3).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
                Try
                    If gItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(4).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
                Try
                    If pItemName(a).Substring(b, TextBox7.TextLength) = TextBox7.Text Then
                        TreeView1.SelectedNode = TreeView1.Nodes(5).Nodes(a)
                        Exit Sub
                    End If
                Catch
                End Try
            Next a
        Next b
        MsgBox("No matches found.")
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        PictureBox1.Image = ImageList1.Images.Item(2)
        'ImageList1.Images.
    End Sub
End Class