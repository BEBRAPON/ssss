using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для StaffInfAd.xaml
    /// </summary>
    public partial class WorkersInfAd : Page
    {
        positionsTableAdapter post = new positionsTableAdapter();
        workersTableAdapter worker = new workersTableAdapter();
        public WorkersInfAd()
        {
            InitializeComponent();
            workers.ItemsSource = worker.GetData();
            FKChoice.ItemsSource = post.GetData();
            FKChoice.DisplayMemberPath = "post_name";
            FKChoice.SelectedValuePath = "post_id";
        }

        private void workers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                var item = workers.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string; 
                surname.Text = item.Row[2] as string;
                FKChoice.SelectedValue = (int) item.Row[3];
                email.Text = item.Row[4] as string; 
                phone.Text = item.Row[5] as string;
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || surname.Text == "" || email.Text == "" || FKChoice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                int id = (int)FKChoice.SelectedValue;
                worker.InsertQuery(name.Text, surname.Text, id, email.Text, phone.Text);
                workers.ItemsSource = worker.GetData();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                if (name.Text == "" || surname.Text == "" || email.Text == "" || FKChoice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = workers.SelectedItem as DataRowView;
                    worker.UpdateQuery(name.Text, surname.Text, (int) FKChoice.SelectedValue, email.Text, phone.Text, (int)item.Row[0]);
                    workers.ItemsSource = worker.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (workers.SelectedItem != null)
            {
                int id = (int)(workers.SelectedItem as DataRowView).Row[0];
                worker.DeleteQuery(id);
                workers.ItemsSource = worker.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || phone.Text.Length > 10) 
            { 
                e.Handled = true;
                if (!Char.IsDigit(e.Text, 0)) MessageBox.Show("Вы дложны вводить только цифры в это поле.");
                if (phone.Text.Length > 10) MessageBox.Show("Номер телефона не должен превышать 11 символов.");
            }
        }
        private void PreviewTextInput2(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) { MessageBox.Show("Вы не можете вводить цифры в это поле."); e.Handled = true; }
        }
    }
}
