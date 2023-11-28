Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Register
    ' Define your MySQL connection string
    Dim connectionString As String = "Server=localhost;user id=root;password=;database=parking system;"
    Private Sub Guna2TextBox3_TextChanged(sender As Object, e As EventArgs) Handles Password.TextChanged
        If Password.Text.Length < 6 Then
            Label1.Visible = True
        Else
            Label1.Visible = False
        End If
    End Sub

    ' ... (Your existing code for other events)

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If String.IsNullOrWhiteSpace(emailorphone.Text) OrElse
            String.IsNullOrWhiteSpace(Fullname.Text) OrElse
            String.IsNullOrWhiteSpace(UserName.Text) OrElse
            String.IsNullOrWhiteSpace(Password.Text) OrElse
            String.IsNullOrWhiteSpace(CPassword.Text) Then
            MessageBox.Show("Fill In all Fields!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf Password.Text.Length < 6 Then
            MessageBox.Show("Minimum of 6 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf CPassword.Text.Length < 6 Then
            MessageBox.Show("Minimum of 6 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf Password.Text <> CPassword.Text Then
            MessageBox.Show("Passwords do not match", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Password hashing using SHA256 (you may want to enhance this based on your security requirements)
            Dim hashedPassword As String = HashPassword(Password.Text)

            ' Insert data into the database
            If InsertData(emailorphone.Text, Fullname.Text, UserName.Text, hashedPassword) Then
                MessageBox.Show("Registration successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form1.Show()
                Me.Close()
            Else
                MessageBox.Show("Error registering user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Form1.Show()
        Me.Close()
    End Sub
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

    Private Function InsertData(emailOrPhone As String, fullName As String, userName As String, hashedPassword As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Your SQL query to insert data
                Dim query As String = "INSERT INTO register_table (Email_Address, Full_Name, User_Name, Password) VALUES (@Email, @FullName, @UserName, @Password)"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@Email", emailOrPhone)
                    cmd.Parameters.AddWithValue("@FullName", fullName)
                    cmd.Parameters.AddWithValue("@UserName", userName)
                    cmd.Parameters.AddWithValue("@Password", hashedPassword)

                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            Catch ex As Exception
                ' Handle the exception (you might want to log it)
                Return False
            End Try
        End Using
    End Function
End Class
