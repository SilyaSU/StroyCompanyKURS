using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace StroyCompany
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string middleName = middleNameTextBox.Text;
            string address = addressTextBox.Text;
            string phone = phoneTextBox.Text;
            string password = passwordBox.Password;
            string role = "Клиент";

            using (var db = new DataBase())
            {
                db.openConnection();
                string query = "INSERT INTO Люди (Фамилия, Имя, Отчество, Адрес, Телефон, Пароль, Роль) " +
                               $"VALUES ('{lastName}', '{firstName}', '{middleName}', '{address}', '{phone}', '{password}', '{role}')";
                var cmd = new SqlCommand(query, db.sqlConnection);
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }

            MessageBox.Show("Успешная регистрация!");
            this.Close();
        }
    }
}
