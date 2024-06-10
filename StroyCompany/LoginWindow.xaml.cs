using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace StroyCompany
{
    public partial class LoginWindow : Window
    {
        private DataBase database;

        public LoginWindow()
        {
            InitializeComponent();
            database = new DataBase();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(username))
            {
                int? userId = GetUserIdByUsername(username);

                if (userId.HasValue)
                {
                    MainWindow mainWindow = new MainWindow(userId.Value);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь с такой фамилией не найден.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите фамилию.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int? GetUserIdByUsername(string username)
        {
            int? userId = null;
            string query = "SELECT id_Люди FROM Люди WHERE Фамилия = @username";

            database.openConnection();
            using (SqlCommand cmd = new SqlCommand(query, database.sqlConnection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
            }
            database.closeConnection();

            return userId;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
