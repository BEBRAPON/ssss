using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    public partial class DishsesInfAd : Page
    {
        types_of_dishesTableAdapter type = new types_of_dishesTableAdapter();
        dishesTableAdapter dish = new dishesTableAdapter();
        public DishsesInfAd()
        {
            InitializeComponent();
            dishes.ItemsSource = dish.GetData();
            FKChoice.ItemsSource = type.GetData();
            FKChoice.DisplayMemberPath = "type_name";
            FKChoice.SelectedValuePath = "type_id";
        }

        private void dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dishes.SelectedItem != null)
            {
                var item = dishes.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string;
                cooktime.Text = Convert.ToString(item.Row[2]);
                cost.Text = Convert.ToString(item.Row[3]);
                FKChoice.SelectedValue = (int)item.Row[4];
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || cooktime.Text == "" || cost.Text == "" || FKChoice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                int id = (int)FKChoice.SelectedValue;
                dish.InsertQuery(name.Text, cooktime.Text, Convert.ToInt32( cost.Text), id);
                dishes.ItemsSource = dish.GetData();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dishes.SelectedItem != null)
            {
                if (name.Text == "" || cooktime.Text == "" || cost.Text == "" || FKChoice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = dishes.SelectedItem as DataRowView;
                    dish.UpdateQuery(name.Text, cooktime.Text, Convert.ToInt32(cost.Text),(int)FKChoice.SelectedValue, (int)item.Row[0]);
                    dishes.ItemsSource = dish.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dishes.SelectedItem != null)
            {
                int id = (int)(dishes.SelectedItem as DataRowView).Row[0];
                dish.DeleteQuery(id);
                dishes.ItemsSource = dish.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) { e.Handled = true; MessageBox.Show("Вы дложны вводить только цифры в это поле."); }
        }
    }
}
