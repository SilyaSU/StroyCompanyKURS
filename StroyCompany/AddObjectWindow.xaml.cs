using System.Windows;

namespace StroyCompany
{
    public partial class AddObjectWindow : Window
    {
        public AddObjectWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string type = typeTextBox.Text;
            string groupName = groupNameTextBox.Text;
            string cost = costTextBox.Text;

            string query = $"INSERT INTO Объект (Название, Адрес, Тип, Стоимость, FK_Рабочая_группа) " +
                           $"VALUES ('{name}', '{address}', '{type}', {cost}, " +
                           $"(SELECT id_Рабочая_группа FROM Рабочая_группа WHERE Название = '{groupName}'))";

            using (DataBase db = new DataBase())
            {
                db.openConnection();
                try
                {
                    db.ExecuteQuery(query);
                    MessageBox.Show("Object added successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding object: {ex.Message}");
                }
                finally
                {
                    db.closeConnection();
                }
            }
        }


    }
}
