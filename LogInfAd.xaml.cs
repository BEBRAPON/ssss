using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.avtorizDataSetTableAdapters;

namespace Lab5
{
    public partial class LogInfAd : Page
    {
        rolesTableAdapter role = new rolesTableAdapter();
        usersTableAdapter user = new usersTableAdapter();
        public LogInfAd()
        {
            InitializeComponent();
            users.ItemsSource = user.GetData();
            FKChoice.ItemsSource = role.GetData();
            FKChoice.DisplayMemberPath = "role_name";
            FKChoice.SelectedValuePath = "role_id";
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {

            if (login.Text == "" || password.Text == "" || FKChoice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                user.InsertQuery(login.Text, password.Text, (int)FKChoice.SelectedValue);
                users.ItemsSource = user.GetData();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            if (users.SelectedItem != null)
            {
                if (login.Text == "" || password.Text == "" || FKChoice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = users.SelectedItem as DataRowView;
                    user.UpdateQuery(login.Text, password.Text, (int)FKChoice.SelectedValue, (int)item.Row[0]);
                    users.ItemsSource = user.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (users.SelectedItem != null)
            {
                int id = (int)(users.SelectedItem as DataRowView).Row[0];
                user.DeleteQuery(id);
                users.ItemsSource = user.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (users.SelectedItem != null)
            {
                var item = users.SelectedItem as DataRowView;
                login.Text = item.Row[1] as string;
                password.Text = item.Row[2] as string;
                FKChoice.SelectedValue = (int)item.Row[3];
            }
        }
    }
}
