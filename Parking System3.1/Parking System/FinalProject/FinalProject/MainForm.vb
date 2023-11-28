Public Class MainForm

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        openFileDialog.Title = "Select an Image File"
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim selectedFilePath As String = openFileDialog.FileName
            Guna2CirclePictureBox1.Image = Image.FromFile(selectedFilePath)
            MessageBox.Show("Selected file: " & selectedFilePath, "File Selected")
        End If
    End Sub


End Class