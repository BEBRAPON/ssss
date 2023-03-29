using System.Data;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    public partial class DetailsOfDishesAd : Page
    {
        dishesTableAdapter dish = new dishesTableAdapter();
        productsTableAdapter product = new productsTableAdapter();
        details_of_dishesTableAdapter detail = new details_of_dishesTableAdapter();
        public DetailsOfDishesAd()
        {
            InitializeComponent();

            details_of_dishes.ItemsSource = detail.GetData();

            FKChoice.ItemsSource = dish.GetData();
            FKChoice.DisplayMemberPath = "dish_name";
            FKChoice.SelectedValuePath = "dish_id";

            FK2Choice.ItemsSource = product.GetData();
            FK2Choice.DisplayMemberPath = "product_name";
            FK2Choice.SelectedValuePath = "product_id";

            
        }

        private void details_of_dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (details_of_dishes.SelectedItem != null)
            {
                var item = details_of_dishes.SelectedItem as DataRowView;
                FKChoice.SelectedValue = (int)item.Row[0];
                FK2Choice.SelectedValue = (int)item.Row[1];
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (FKChoice.SelectedValue == null || FK2Choice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                int id = (int)FKChoice.SelectedValue;
                int id2 = (int)FK2Choice.SelectedValue;
                detail.InsertQuery(id, id2);
                details_of_dishes.ItemsSource = detail.GetData();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (details_of_dishes.SelectedItem != null)
            {
                if (FKChoice.SelectedValue == null || FK2Choice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = details_of_dishes.SelectedItem as DataRowView;
                    detail.UpdateQuery((int)FKChoice.SelectedValue, (int)FK2Choice.SelectedValue, (int)item.Row[0]);
                    details_of_dishes.ItemsSource = detail.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (details_of_dishes.SelectedItem != null)
            {
                int id = (int)(details_of_dishes.SelectedItem as DataRowView).Row[0];
                detail.DeleteQuery(id);
                details_of_dishes.ItemsSource = detail.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void FKChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
