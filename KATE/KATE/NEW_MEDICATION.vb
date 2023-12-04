Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class NEW_MEDICATION

    Dim connectionString As String = "Server=localhost;Database=DR_KATE;User ID=root;Password=;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Private Sub NEW_MEDICATION_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub UpdateTable()
        Try
            Dim medid As String = TextBox1.Text
            Dim MENAME As String = TextBox2.Text
            Dim meddoses As String = TextBox3.Text

            connection.Open()

            Dim query As String = "UPDATE `MED_LIST` SET `DOSES` = '" & meddoses & "' WHERE `MED ID` = '" & medid & "'"
            Dim cmd As New MySqlCommand(query, connection)
            cmd.ExecuteNonQuery()

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


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then

            Dim firstColumnIndex As Integer = 0
            Dim secondColumnIndex As Integer = 1
            Dim thirdColumnIndex As Integer = 2

            Dim firstColumnValue As Object = DataGridView1.Rows(e.RowIndex).Cells(firstColumnIndex).Value
            Dim secondColumnValue As Object = DataGridView1.Rows(e.RowIndex).Cells(secondColumnIndex).Value
            Dim thirdColumnValue As Object = DataGridView1.Rows(e.RowIndex).Cells(thirdColumnIndex).Value

            TextBox1.Text = If(firstColumnValue IsNot Nothing, firstColumnValue.ToString(), "")
            TextBox2.Text = If(secondColumnValue IsNot Nothing, secondColumnValue.ToString(), "")
            TextBox3.Text = If(thirdColumnValue IsNot Nothing, thirdColumnValue.ToString(), "")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim result As DialogResult = MessageBox.Show("Do you want to insert the data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            add_data()
            UpdateTable()
        Else
            MessageBox.Show("Add canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub add_data()
        Try
            connection.Open()

            Dim query As String = "INSERT INTO `med_list`(`MED ID`, `NAME`, `DOSES`) VALUES (Null,'" & TextBox2.Text & "','" & TextBox3.Text & "');"
            Dim cmd As New MySqlCommand(query, connection)

            cmd.ExecuteNonQuery()


        Catch ex As Exception
            MessageBox.Show("Error inserting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
            UpdateTable()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to update the table?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            UpdateTable()
        Else
            MessageBox.Show("Update canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub delete_data()
        Try
            connection.Open()

            Dim query As String = "DELETE FROM `med_list` WHERE `MED ID` = '" & TextBox1.Text & "';"
            Dim cmd As New MySqlCommand(query, connection)

            cmd.ExecuteNonQuery()


        Catch ex As Exception
            MessageBox.Show("Error removing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
            UpdateTable()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to remove the data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            delete_data()
            UpdateTable()
        Else
            MessageBox.Show("Deleting canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class