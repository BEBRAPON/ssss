using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.avtorizDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для RoleInfAd.xaml
    /// </summary>
    public partial class RoleInfAd : Page
    {
        rolesTableAdapter role = new rolesTableAdapter();
        public RoleInfAd()
        {
            InitializeComponent();
            roles.ItemsSource = role.GetData();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                role.InsertQuery(name.Text);
                roles.ItemsSource = role.GetData();
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (roles.SelectedItem != null)
            {
                if (name.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = roles.SelectedItem as DataRowView;
                    role.UpdateQuery(name.Text, (int)item.Row[0]);
                    roles.ItemsSource = role.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (roles.SelectedItem != null)
            {
                int id = (int)(roles.SelectedItem as DataRowView).Row[0];
                role.DeleteQuery(id);
                roles.ItemsSource = role.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
        private void roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roles.SelectedItem != null)
            {
                var item = roles.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string;
            }
        }
        private void PreviewTextInput2(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) { MessageBox.Show("Вы не можете вводить цифры в это поле."); e.Handled = true; }
        }
    }
}
