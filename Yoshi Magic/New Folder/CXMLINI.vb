Public Class CXMLINI
    Private doc As New Xml.XmlDocument
    Private xmlfile As String
    Public Sub New(ByVal Filename As String)
        'MsgBox("New(File)")
        ' open xml file for use
        Try
            'doc.Save("C:\Users\charleysdrpepper\Desktop\settings.xml")
            xmlfile = Filename
            doc.Load(Filename)
            'MsgBox("NEW(FILE)")

        Catch ex As Exception
            'Xml.
        End Try

    End Sub
    Public Sub New()
        'MsgBox("NEW()")
        ' open default xml file for use
        Dim s As String
        s = System.AppDomain.CurrentDomain.BaseDirectory
        s &= Application.ProductName
        s &= ".xml"
        MsgBox(s)
        Try
            doc.Load(s)
            xmlfile = s
        Catch ex As Exception
            Debug.Write(" NEW() error ")
        End Try
    End Sub

    Protected Overrides Sub Finalize()

        ' save changes to settings here, if we haven't already
        Try
            ' save changes to data
            doc.Save(xmlfile)
            'MsgBox("Saved: " & xmlfile)
        Catch ex As Exception
            Debug.Write(" FINALIZE error ")
            ' MsgBox("Error: " & xmlfile)
        End Try
        MyBase.Finalize()
    End Sub

    Public Function GetSetting(ByVal Section As String, ByVal Key As String, _
                            ByVal DefaultValue As String) As String
        Dim node As Xml.XmlNode
        Try
            node = doc.SelectSingleNode("/sections/section[@name='" & Section & "']/item[@key='" & Key & "']")
            If node Is Nothing Then
            GetSetting = DefaultValue
         Else
            GetSetting = node.Attributes("value").Value
         End If
        Catch ex As Exception
            GetSetting = DefaultValue
            Debug.Write(" GETSETTING error ")
        End Try
    End Function

    Public Sub SaveSetting(ByVal Section As String, ByVal Key As String, ByVal NewValue As String)
        'Debug.Write(Section & " - " & Key & " - " & NewValue)
        Dim node As Xml.XmlNode, keynode As Xml.XmlNode
        'Try
        ' check for/create section
        node = doc.SelectSingleNode("/sections/section[@name='" & Section & "']")
        If node Is Nothing Then
            ' section doesn't exist, create
            node = doc.SelectSingleNode("/sections")
            Dim newnode As Xml.XmlNode = doc.CreateElement("section")
            Dim att As Xml.XmlAttribute = doc.CreateAttribute("name")
            'Debug.Write(Section & " - " & Key & " - " & NewValue)
            att.Value = Section
            newnode.Attributes.Append(att)
            node.AppendChild(newnode)
            node = doc.SelectSingleNode("/sections/section[@name='" & Section & "']")
        End If
        ' get key
        keynode = doc.SelectSingleNode("/sections/section[@name='" & Section & "']/item[@key='" & Key & "']")
        If keynode Is Nothing Then
            ' create key
            Dim newnode As Xml.XmlNode = doc.CreateElement("item")
            Dim att As Xml.XmlAttribute = doc.CreateAttribute("key")
            att.Value = Key
            newnode.Attributes.Append(att)
            att = doc.CreateAttribute("value")
            att.Value = NewValue
            newnode.Attributes.Append(att)
            node.AppendChild(newnode)
        Else
            ' just update key value
            keynode.Attributes("value").Value = NewValue
        End If
        'Catch ex As Exception
        'Debug.Write(" SAVESETTING error ")
        'End Try
    End Sub
End Class
