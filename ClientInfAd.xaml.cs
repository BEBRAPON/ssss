using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    public partial class ClientInfAd : Page
    {
        customersTableAdapter client = new customersTableAdapter();
        public ClientInfAd()
        {
            InitializeComponent();
            clients.ItemsSource = client.GetData();
        }

        private void Insert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (username.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                client.InsertQuery(username.Text);
                clients.ItemsSource = client.GetData();
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (clients.SelectedItem != null)
            {
                if (username.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = clients.SelectedItem as DataRowView;
                    client.UpdateQuery(username.Text,  (int)item.Row[0]);
                    clients.ItemsSource = client.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (clients.SelectedItem != null)
            {
                int id = (int)(clients.SelectedItem as DataRowView).Row[0];
                client.DeleteQuery(id);
                clients.ItemsSource = client.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clients.SelectedItem != null)
            {
                var item = clients.SelectedItem as DataRowView;
                username.Text = item.Row[1] as string;
            }
        }
    }
}
