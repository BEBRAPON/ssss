using Lab5.tables.restDataSetTableAdapters;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Lab5
{
    public partial class TypeOfDishesInfAd : Page
    {
        types_of_dishesTableAdapter type_of_dish = new types_of_dishesTableAdapter();
        public TypeOfDishesInfAd()
        {
            InitializeComponent();
            types_of_dishes.ItemsSource = type_of_dish.GetData();
        }

        private void Insert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (name.Text == "" || kitchenname.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                type_of_dish.InsertQuery(name.Text, kitchenname.Text);
                types_of_dishes.ItemsSource = type_of_dish.GetData();
            }
        }

        private void types_of_dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (types_of_dishes.SelectedItem != null)
            {
                var item = types_of_dishes.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string;
                kitchenname.Text = item.Row[2] as string;
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (types_of_dishes.SelectedItem != null)
            {
                if (name.Text == "" || kitchenname.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = types_of_dishes.SelectedItem as DataRowView;
                    type_of_dish.UpdateQuery(name.Text, kitchenname.Text, (int)item.Row[0]);
                    types_of_dishes.ItemsSource = type_of_dish.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (types_of_dishes.SelectedItem != null)
            {
                int id = (int)(types_of_dishes.SelectedItem as DataRowView).Row[0];
                type_of_dish.DeleteQuery(id);
                types_of_dishes.ItemsSource = type_of_dish.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }
        private void PreviewTextInput2(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) { MessageBox.Show("Вы не можете вводить цифры в это поле."); e.Handled = true; }
        }
    }
}
