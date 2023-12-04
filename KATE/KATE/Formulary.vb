Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Formulary

    Dim connectionString As String = "Server=localhost;Database=DR_KATE;User ID=root;Password=;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)
    Private Sub Formulary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            connection.Open()
            Dim query As String = "SELECT * FROM MED_LIST"
            Dim adapter As New MySqlDataAdapter(query, connection)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchText As String = TextBox1.Text.Trim()

        If Not String.IsNullOrEmpty(searchText) Then
            CType(DataGridView1.DataSource, DataTable).DefaultView.RowFilter = $"`NAME` LIKE '%{searchText}%'"
        Else
            CType(DataGridView1.DataSource, DataTable).DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox2.Show()
        Label2.Show()
        TextBox3.Show()
        Label3.Show()
        Button4.Show()
        Button2.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to update the table?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            UpdateTable()
        Else
            MessageBox.Show("Update canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub UpdateTable()
        Try
            Dim meddoses As String = TextBox3.Text
            Dim medid As String = TextBox2.Text
            connection.Open()

            Dim query As String = "UPDATE MED_LIST SET `DOSES` = '" & meddoses & "' WHERE `MED ID` = '" & medid & "'"
            Dim cmd As New MySqlCommand(query, connection)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Table updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Dim dataTable As New DataTable()
            Using adapter As New MySqlDataAdapter("SELECT * FROM MED_LIST", connection)
                adapter.Fill(dataTable)
            End Using
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Error updating table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            connection.Close()
        End Try
    End Sub
End Class