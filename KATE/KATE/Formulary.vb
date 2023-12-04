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
        Button4.Show()
        Button2.Hide()
    End Sub


End Class