'Imports System.ComponentModel
Public Class Form1
    Public version
    'Dim this
    'Public a
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Text = "Loading..."
        Button1.Update()
        Text_Editor.Show()
        Button1.Text = "Text Editor"
        'user()
    End Sub
    'Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
    '    AboutBox1.Show()
    'End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button2.Text = "Loading..."
        Button2.Update() 'So "Loading..." will actually show up.
        EnemyEditor2.Show()
        Button2.Text = "Enemy Editor"
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button3.Text = "Loading..."
        Button3.Update()
        Item_Editor.Show()
        Button3.Text = "Item Editor"
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Graphics_Viewer.Show()
        Button4.Text = "Loading..."
        Button4.Update()
        'Sprite_Viewer.Show()
        Form5.Show()
        Button4.Text = "Sprite Viewer (Beta)"
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Debugger.IsAttached Then GroupBox1.Visible = True
        For Each argument As String In My.Application.CommandLineArgs
            If argument = "d" Then GroupBox1.Visible = True Else Me.Width = 580
        Next
        'Me.Scale(New SizeF(1.5, 1.5))
        FileIO.FileSystem.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory 'Application.StartupPath
        'MsgBox(AppDomain.CurrentDomain.BaseDirectory)
        'Dim doc As New Xml.XmlDocument
        'Try
        '    doc.Load("settings.xml")
        '    'xmlfile = Filename 
        'Catch ex As Exception
        'End Try
        'Dim node As Xml.XmlNode
        'Try
        '    node = doc.SelectSingleNode("/sections/section[@name='settings']/item[@key='filepath']")
        '    If node Is Nothing Then
        '        OpenFileDialog1.FileName = ""
        '    Else
        '        OpenFileDialog1.FileName = node.Attributes("value").Value
        '    End If
        'Catch ex As Exception
        'MsgBox()
        'Application.
        'End Try
        ''''' 
        'Dim xmlsettings As New CXMLINI
        '''''  
        'xmlsettings = New CXMLINI("C:/Users/charleysdrpepper/Desktop/settings.ini")
        'xmlsettings.
        '"C:\Users\charleysdrpepper\Desktop\settings.ini")
        '(CurDir() & "\settings.xml")
        '''''
        'OpenFileDialog1.FileName = xmlsettings.GetSetting("settings", "filepath", "")
        'Text_Editor.
        OpenFileDialog1.Title = "Open a GBA/NDS File"
        OpenFileDialog1.Filter = "GBA/NDS file(*.gba;*.nds)|*.gba;*.nds"
        'OpenFileDialog1.FileName = "TESTING"
        'MsgBox(CurDir)
        'ini.SetINIString("settings.ini", "Settings", "file", "test")
        'ini.SetINIString("C:\Users\charleysdrpepper\Desktop\settings.ini", "settings", "file", "2")
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then End
        '''''
        'xmlsettings.SaveSetting("settings", "filepath", OpenFileDialog1.FileName)
        'Finalize() ' xmlsettings)

        ' Try
        'ini.SetINIString("settings.ini", "Settings", "file", "test")
        'ini.SetINIInt("settings.ini", "Settings", "file", 2)
        'MsgBox("v")
        ' Catch
        'MsgBox("n")
        ' End Try
        Do
            Try
                FileOpen(1, OpenFileDialog1.FileName, OpenMode.Binary, OpenAccess.Default, OpenShare.Shared)
                Exit Do
            Catch
                Select Case MsgBox("Unable to Open File, try closing whatever is using your file, and try again.", MsgBoxStyle.RetryCancel)
                    Case MsgBoxResult.Cancel
                        End
                End Select
            End Try
        Loop
        Dim versioncheck(1) As Int64 '2 numbers
        For a = 0 To 1
            FileGet(1, versioncheck(a), &HA0 + (a << 3) + 1)
        Next
        Select Case versioncheck(0)
            Case &H554C264F4952414D 'MARIO&LU
                Select Case versioncheck(1)
                    Case &H4538384155494749 'NA VERSION #1 'IGIUA88E
                        Label1.Text = "You are using the North American version. Please enjoy the editor!"
                        version = "NA"
                    Case &H5038384150494749 'EU VERSION #1 'IGIPA88P
                        Label1.Text = "You are using the European Version. Please get the North American version."
                        version = "EU"
                    Case &H4A3838414A494749 'J VERSION #1 'IGIJA88J
                        Label1.Text = "You are using the Japanese Version. Please get the North American version."
                        version = "J"
                End Select
            Case &H4F4D4544204C264D 'M&L DEMO
                Select Case versioncheck(1)
                    Case &H4538384241535520 'NA VERSION #1 ' USAB88E
                        Label1.Text = "This is the North American Demo version, you need the original one for this editor."
                        version = "NADEMO"
                End Select
            Case Else
                Label1.Text = "You have an unknown version of the ROM. If anything looks totally wrong, please try a North American ROM."
        End Select
        Dim versioncds(15) As Byte
        Dim vcdsstr As String = ""
        For a = 0 To 15
            FileGet(1, versioncds(a), a + 1)
            vcdsstr &= Chr(versioncds(a))
        Next
        Select Case vcdsstr
            Case "MARIO&LUIGI2ARME"
                Label1.Text = "Thank you for using the right version of Partners In Time!"
            Case "MARIO&LUIGI3CLJE"
                Label1.Text = "Thank you for using the right version of Bowser's Inside Story!"
        End Select
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Button5.Text = "Loading..."
        Button5.Update()
        Party_Editor.Show()
        Button5.Text = "Party Editor"
    End Sub
    'Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    'Scripts.Show()
    'Object_Editor.Show()
    'RectangleShape1.SetBounds(MousePosition.X - Me.Left - 20, MousePosition.Y - 35 - Me.Top, 30, 30)
    'Label1.SetBounds(MousePosition.X - Me.Left, MousePosition.Y - Me.Top + 5, 100, 30)
    'End Sub
    'Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
    'AboutBox1.Show()
    'End Sub
    'Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    'Audio_Editor.Show()
    'End Sub
    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    'Warp_Editor.Show()
    'End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        pitenemyedit.Show()
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Button10.Text = "Loading..."
        Button10.Update()
        roomproperties.Show()
        Button10.Text = "Room Properties"
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Button11.Text = "Loading..."
        Button11.Update()
        Battle_Editor.Show()
        Button11.Text = "Battle Editor"
    End Sub
    'Private Sub Panel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Click
    ' If Debugger.IsAttached Then
    'Panel2.Visible = False
    'End If
    'End Sub
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        AboutBox1.Show()
    End Sub
    'Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim test1 As New Bitmap("Sprites\Enemy Sprites\32.gif")
    '    Dim test2 As New Bitmap("Sprites\Enemy Sprites\32.gif")
    '    Dim aww As Integer
    '    If Panel1.Visible = False Then aww = 50
    '    e.Graphics.DrawImage(test1, 30, 30)
    '    e.Graphics.DrawImage(test1, 20 + aww, 70)
    '    'e.Graphics.Fl()
    'End Sub
    'Private Sub Panel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
    '    Panel1.Visible = False
    '    Me.Refresh()
    'End Sub
    'Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
    'End Sub
    'Friend Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr,ByVal msg As UInt32, ByVal sparam As UInt32,  ByVal lparam As UInt32) As UInt32
    'Friend Const BCM_SETSHIELD As Int32 = &H160C
    'Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
    ' My.User.IsInRole(Microsoft.VisualBasic.ApplicationServices.BuiltInRole.Administratorr)
    'Dim process As Diagnostics.Process = Diagnostics.Process.Start( startInfo)
    '_3d.Show()
    'End Sub
    'Private Sub Button13_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button13.VisibleChanged
    'Button13.FlatStyle = Windows.Forms.FlatStyle.System
    'Dim success As UInt32 = SendMessage(Button13.Handle, BCM_SETSHIELD, 0, 1)
    'My.User.IsInRole(Microsoft.VisualBasic.ApplicationServices.BuiltInRole.Administrator)
    'ProcessStartInfo()
    'Dim process As Diagnostics.Process = Diagnostics.Process.Start("Yoshi Magic.exe")
    'End Sub
    ' Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
    'MsgBox(Convert.ToString(Label1.Bounds))
    'Applicatio()
    'Media.SystemSounds.
    'se()
    'Label1.
    ' End Sub
    '  Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Dim test As String
    'test = "testthis  " "
    'alloc()
    'Debug.Write("Button pressed...")
    'Debugger.Launch()
    'Console.Write("This is a gigantic test...")
    'MsgBox(Convert.ToString(Microsoft.VisualBasic.VBCodeProvider.GetAllCompilerInfo))
    ''OpenShare.
    'Module1.Main()
    'Console()
    ' End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        battlemaps.Show()
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Form6.Show()
    End Sub
    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Form7.Show()
    End Sub
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Form4.Show()
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Text_Editor3.Show()
    End Sub


    'Private WithEvents _wsks As New Winsock_Orcas.WinsockCollection(True)
    'Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
    '    AxWinsock1.Listen()
    '    'AxWinsock1.
    'End Sub
    ''Private Sub wskListener_ConnectionRequest( _
    ''  ByVal sender As System.Object, _
    ''  ByVal e As  _
    ''  Winsock_Orcas.WinsockConnectionRequestEventArgs) _
    ''  Handles AxWinsock1.ConnectionRequest
    ''    AxWinsock1.Accept(e.Client)
    ''End Sub
    'Private Sub AxWinsock1_ConnectionRequest(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_ConnectionRequestEvent)
    '    AxWinsock1.Accept(0)
    '    MsgBox("!")
    'End Sub

    'Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
    '    AxWinsock1.Connect()
    '    'AxWinsock1.SendData("Connected.")
    'End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Form8.Show()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        BISMaps.Show()
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        PITMaps.Show()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Form9.Show()
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Form10.Show()
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Form11.Show()
    End Sub
End Class