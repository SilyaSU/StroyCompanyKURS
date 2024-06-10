using System;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace StroyCompany
{
    public partial class EditObjectWindow : Window
    {
        private DataRowView selectedRow;
        private DataBase database;

        public EditObjectWindow(DataRowView row)
        {
            InitializeComponent();
            selectedRow = row;
            PopulateFields();
            database = new DataBase();
        }

        private void PopulateFields()
        {
            nameTextBox.Text = selectedRow["Название"].ToString();
            addressTextBox.Text = selectedRow["Адрес"].ToString();
            typeTextBox.Text = selectedRow["Тип"].ToString();
            groupNameTextBox.Text = selectedRow["Рабочая_группа"].ToString();
            costTextBox.Text = selectedRow["Стоимость"].ToString();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string type = typeTextBox.Text;
            string groupName = groupNameTextBox.Text;
            decimal cost;

            if (!decimal.TryParse(costTextBox.Text, out cost))
            {
                MessageBox.Show("Invalid cost value.");
                return;
            }

            string updateQuery = @"
            UPDATE Объект 
            SET Название = @name, Адрес = @address, Тип = @type, Стоимость = @cost, 
                FK_Рабочая_группа = (SELECT id_Рабочая_группа FROM Рабочая_группа WHERE Название = @groupName) 
            WHERE Название = @originalName";

            database.openConnection();
            using (SqlCommand cmd = new SqlCommand(updateQuery, database.sqlConnection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Parameters.AddWithValue("@groupName", groupName);
                cmd.Parameters.AddWithValue("@originalName", selectedRow["Название"].ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successful.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error updating object: " + ex.Message);
                }
            }
            database.closeConnection();
            this.Close();
        }
    }
}
