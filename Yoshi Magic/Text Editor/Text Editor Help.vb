Public Class Form2

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("http://s3.zetaboards.com/Lighthouse_of_Yoshi/")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://z3.invisionfree.com/Lighthouse_of_Yoshi/index.php?showtopic=128&st=0")
    End Sub
End Class