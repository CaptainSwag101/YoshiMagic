Public Class UserControl1
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        '84 '890
        displaywarpdata()
    End Sub
    Public Sub displaywarpdata()
        Dim w1 As Integer = roomproperties.warpdataint1(NumericUpDown1.Value)
        Dim w2 As Integer = roomproperties.warpdataint2(NumericUpDown1.Value)
        ComboBox1.SelectedIndex = w1 And &H1
        ComboBox2.SelectedIndex = (w1 And &HE) >> 1
        ComboBox3.SelectedIndex = (w1 And &H10) >> 4
        ComboBox4.SelectedIndex = (w1 And &H20) >> 5
        NumericUpDown2.Value = (w1 And &H7FC0) >> 6
        NumericUpDown3.Value = w1 >> 24
        NumericUpDown4.Value = w2 And &HFF
        NumericUpDown5.Value = (w2 And &HF00) >> 8
        NumericUpDown6.Value = (w2 And &HF000) >> 12
        CheckBox1.Checked = (w2 And &H10000) >> 16
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFFFFFE) Or ComboBox1.SelectedIndex
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFFFFF1) Or (ComboBox2.SelectedIndex << 1)
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFFFFEF) Or (ComboBox3.SelectedIndex << 4)
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFFFFDF) Or (ComboBox4.SelectedIndex << 5)
    End Sub
    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFF803F) Or (NumericUpDown2.Value << 6)
    End Sub
    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        roomproperties.warpdataint1(NumericUpDown1.Value) = _
            (roomproperties.warpdataint1(NumericUpDown1.Value) And &HFFFFFF) Or (NumericUpDown3.Value << 24)
    End Sub
    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        roomproperties.warpdataint2(NumericUpDown1.Value) = _
            (roomproperties.warpdataint2(NumericUpDown1.Value) And &HFFFFFF00) Or NumericUpDown4.Value
    End Sub
    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        roomproperties.warpdataint2(NumericUpDown1.Value) = _
            (roomproperties.warpdataint2(NumericUpDown1.Value) And &HFFFFF0FF) Or (NumericUpDown5.Value << 8)
    End Sub
    Private Sub NumericUpDown6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        roomproperties.warpdataint2(NumericUpDown1.Value) = _
            (roomproperties.warpdataint2(NumericUpDown1.Value) And &HFFFF0FFF) Or (NumericUpDown6.Value << 12)
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        roomproperties.warpdataint2(NumericUpDown1.Value) = _
            (roomproperties.warpdataint2(NumericUpDown1.Value) And &HFFFEFFFF) Or ((CheckBox1.Checked * -1) << 16)
    End Sub
End Class
