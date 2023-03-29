using Lab5.tables.restDataSetTableAdapters;
using System.Data;
using System.Windows.Controls;
using Lab5.tables.restDataSet1TableAdapters;
using System.Windows;

namespace Lab5
{
    public partial class OrdersInfAd : Page
    {
        customersTableAdapter client = new customersTableAdapter();
        workersTableAdapter worker = new workersTableAdapter();
        ordersTableAdapter order = new ordersTableAdapter();
        deletesTableAdapter worker2 = new deletesTableAdapter();
        
        public OrdersInfAd()
        {
            InitializeComponent();
            var all_workers2 = worker2.GetData().Rows;

            for (int i = 0; i < all_workers2.Count; i++)
            {
                int id2 = (int)all_workers2[i][0];
                worker2.DeleteQuery(id2);
            }
            orders.ItemsSource = order.GetData();
            FKChoice.ItemsSource = client.GetData();
            FKChoice.DisplayMemberPath = "client_username";
            FKChoice.SelectedValuePath = "client_id";

            
            var all_workers = worker.GetData().Rows;

            for (int i = 0; i < all_workers.Count; i++)
            {
                int id = (int)all_workers[i][3];
                string surname = (string)all_workers[i][2];
                string name = (string)all_workers[i][1];
                int id2 = (int)all_workers[i][0];
                worker2.InsertQuery(id2, name, surname, id);
                if (id != 1)
                {
                    worker2.DeleteQuery(id2);
                }
            }
            FK2Choice.ItemsSource = worker2.GetData();
            FK2Choice.DisplayMemberPath = "worker_name";
            FK2Choice.SelectedValuePath = "worker_id";
        }

        private void orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orders.SelectedItem != null)
            {
                var item = orders.SelectedItem as DataRowView;
                FKChoice.SelectedValue = (int)item.Row[1];
                FK2Choice.SelectedValue = (int)item.Row[2];
            }
        }

        private void Insert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FKChoice.SelectedValue == null || FK2Choice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                int id = (int)FKChoice.SelectedValue;
                int id2 = (int)FK2Choice.SelectedValue;
                order.InsertQuery(id, id2);
                orders.ItemsSource = order.GetData();
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (orders.SelectedItem != null)
            {
                if (FKChoice.SelectedValue == null || FK2Choice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = orders.SelectedItem as DataRowView;
                    order.UpdateQuery( (int)FKChoice.SelectedValue, (int)FK2Choice.SelectedValue, (int)item.Row[0]);
                    orders.ItemsSource = order.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (orders.SelectedItem != null)
            {
                int id = (int)(orders.SelectedItem as DataRowView).Row[0];
                order.DeleteQuery(id);
                orders.ItemsSource = order.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
    }
}
