Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    Dim connectionString As String = "Server=localhost;user id=root;password=;database=parking system;"

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Register.Show()
        Me.Close()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If CheckLogin(Username.Text, Pass.Text) Then
            MainForm.Show()
            Me.Close()
        ElseIf String.IsNullOrWhiteSpace(Username.Text) OrElse String.IsNullOrWhiteSpace(Pass.Text) Then
            MessageBox.Show("Please fill in all fields !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Username or Password does not match!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    Private Function CheckLogin(username As String, password As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT * FROM register_table WHERE User_Name = @UserName AND Password = @Password"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@UserName", username)
                    Dim hashedPassword As String = HashPassword(password)
                    cmd.Parameters.AddWithValue("@Password", hashedPassword)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Return reader.HasRows
                    End Using
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim sb As New StringBuilder()
            For Each b As Byte In hashedBytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    Private Sub Guna2ImageCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ImageCheckBox1.CheckedChanged
        If Guna2ImageCheckBox1.Checked Then
            Pass.PasswordChar = ""
        Else
            Pass.PasswordChar = "•"
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Pass.PasswordChar = "•"
    End Sub
End Class

