using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для ProdviderInfAd.xaml
    /// </summary>
    public partial class ProdviderInfAd : Page
    {
        providersTableAdapter provider = new providersTableAdapter();
        public ProdviderInfAd()
        {
            InitializeComponent();
            providers.ItemsSource = provider.GetData();
        }

        private void Insert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || address.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                provider.InsertQuery(name.Text, phone.Text, address.Text);
                providers.ItemsSource = provider.GetData();
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (providers.SelectedItem != null)
            {
                if (name.Text == "" || phone.Text == "" || address.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = providers.SelectedItem as DataRowView;
                    provider.UpdateQuery(name.Text, phone.Text, address.Text, (int)item.Row[0]);
                    providers.ItemsSource = provider.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (providers.SelectedItem != null)
            {
                int id = (int)(providers.SelectedItem as DataRowView).Row[0];
                provider.DeleteQuery(id);
                providers.ItemsSource = provider.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void providers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (providers.SelectedItem != null)
            {
                var item = providers.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string;
                phone.Text = item.Row[2] as string;
                address.Text = item.Row[3] as string;
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
    }
}
