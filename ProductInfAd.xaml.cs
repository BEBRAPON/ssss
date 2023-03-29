using Lab5.tables.restDataSetTableAdapters;
using System;
using System.Windows.Controls;
using System.Data;
using System.Windows;

namespace Lab5
{
    public partial class ProductInfAd : Page
    {
        productsTableAdapter product = new productsTableAdapter();
        providersTableAdapter provider = new providersTableAdapter();
        public ProductInfAd()
        {
            InitializeComponent();
            products.ItemsSource = product.GetData();

            FKChoice.ItemsSource = provider.GetData();
            FKChoice.DisplayMemberPath = "provider_name";
            FKChoice.SelectedValuePath = "provider_id";
        }

        private void products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (products.SelectedItem != null)
            {
                var item = products.SelectedItem as DataRowView;
                name.Text = item.Row[1] as string;
                cost.Text = Convert.ToString(item.Row[2]);
                FKChoice.SelectedValue = (int)item.Row[3];
                quPP.Text = item.Row[4] as string;
                quKG.Text = item.Row[5] as string;
                quGR.Text = item.Row[6] as string;
            }
        }

        private void Insert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (name.Text == "" || cost.Text == "" || FKChoice.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                if (quPP.Text != "" || quKG.Text != "" || quGR.Text != "")
                {
                    int id = (int)FKChoice.SelectedValue;
                    product.InsertQuery(name.Text, Convert.ToInt32(cost.Text),id, quPP.Text, quKG.Text, quGR.Text);
                    products.ItemsSource = product.GetData();
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать хотя бы одно из полей, обозначающих кол-во продукта.");
                }
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (products.SelectedItem != null)
            {
                if (name.Text == "" || cost.Text == "" || FKChoice.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    var item = products.SelectedItem as DataRowView;
                    product.UpdateQuery(name.Text, Convert.ToInt32(cost.Text), (int)FKChoice.SelectedValue, quPP.Text, quKG.Text, quGR.Text, (int)item.Row[0]);
                    products.ItemsSource = product.GetData();
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (products.SelectedItem != null)
            {
                int id = (int)(products.SelectedItem as DataRowView).Row[0];
                product.DeleteQuery(id);
                products.ItemsSource = product.GetData();
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
        private void PreviewTextInput2(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0)) { MessageBox.Show("Вы не можете вводить цифры в это поле."); e.Handled = true; }
        }
    }
}
