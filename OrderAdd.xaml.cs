using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для OrderAdd.xaml
    /// </summary>
    public partial class OrderAdd : Page
    {
        dishesTableAdapter dish = new dishesTableAdapter();
        chequeTableAdapter cheq = new chequeTableAdapter();
        ordersTableAdapter order = new ordersTableAdapter();
        List<Cheq> cheqInf = new List<Cheq>();

        public OrderAdd()
        {
            InitializeComponent();
            dishes.ItemsSource = dish.GetData();

            cheques.ItemsSource = cheqInf;


            var orders = order.GetData().Rows;
            for (int i = 0;  i < orders.Count; i++)
            {
                string path = @"F:\\chequesInf\\cheque№" + (int) orders[i][0];
                if (!File.Exists(path)){
                    OrderChoice.Items.Add(orders[i][0]);
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (OrderChoice.SelectedValue != null)
            {
                if (dishes.SelectedItem == null)
                {
                    MessageBox.Show("Строка таблицы не выбрана.");
                }
                else
                {
                    cheqInf.Add(new Cheq { orderid = (int)OrderChoice.SelectedValue, dishid = Convert.ToInt32(dishid.Text), dishname = dishname.Text, dishcost = Convert.ToInt32(dishcost.Text) });
                    cheques.Items.Refresh();
                }
            }
            if (OrderChoice.SelectedValue == null)
            {
                MessageBox.Show("Номер заказа не выбран.");
            }
        }

        private void rem_Click(object sender, RoutedEventArgs e)
        {
            if (cheques.SelectedItem != null)
            {
                Cheq selected = (Cheq)cheques.SelectedItem;
                cheqInf.Remove(selected);
                cheques.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dishes.SelectedItem != null)
            {
                dishid.Visibility = Visibility.Visible;
                dishcost.Visibility = Visibility.Visible;
                var item = dishes.SelectedItem as DataRowView;
                dishid.Text = Convert.ToString(item.Row[0]);
                dishname.Text = Convert.ToString(item.Row[1]);
                dishcost.Text = Convert.ToString(item.Row[3]);
                dishid.Visibility = Visibility.Hidden;
                dishcost.Visibility = Visibility.Hidden;
            }
        }

        private void rem2_Click(object sender, RoutedEventArgs e)
        {
            cheqInf.Clear();
            cheques.Items.Refresh();
            OrderChoice.IsEnabled = true;
        }

        private void keep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int sum = 0;
                foreach (var item in cheqInf)
                {
                    sum += item.dishcost;
                }

                if (sum > Convert.ToInt32(money.Text))
                {
                    MessageBox.Show("Клиент заплатил не полную сумму за блюда. Полная цена равна - " + sum + ". Сумма, котрую дал клиент - " + money.Text);
                }
                else
                {

                    string path = @"F:\\chequesInf\\cheque№" + OrderChoice.SelectedValue.ToString();
            
                    if (File.Exists(path))
                    {
                        MessageBox.Show("Чек под таким номер уже сущестует. Вы не можете создать ещё один.");
                        cheqInf.Clear();
                        cheques.Items.Refresh();
                        OrderChoice.IsEnabled = true;
                    }
                    else
                    {
                        foreach (var item in cheqInf)
                        {
                            cheq.InsertQuery(item.orderid, item.dishid, item.dishname, item.dishcost);
                        }

                        File.Create(path).Close();
                        File.AppendAllText(path, "          Уффф кефтеме\n");
                        File.AppendAllText(path, "          Кассовый чек №" + OrderChoice.SelectedValue.ToString() + "\n");

                        List<string> inf = new List<string>();
                        foreach (var item in cheqInf)
                        {
                            inf.Add(item.dishname + "....." + item.dishcost);
                        }
                        foreach (var item in inf)
                        {
                            File.AppendAllText(path,"\n     " +  item.ToString() + "\n");
                        }

                        File.AppendAllText(path, "\n\nИтоговая сумма - " + sum + "\n");
                        File.AppendAllText(path, "Внесено - " + money.Text + "\n");
                        int sdac = Convert.ToInt32(money.Text) - sum;
                        File.AppendAllText(path, "Сдача - " + sdac + "\n");
                        cheqInf.Clear();
                        cheques.Items.Refresh();
                        OrderChoice.IsEnabled = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Таблица пустая.");
            }
            
        }

        private void OrderChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderChoice.IsEnabled = false;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || money.Text.Length > 5) { 
                e.Handled = true;
                if (!Char.IsDigit(e.Text, 0)) MessageBox.Show("Вы дложны вводить только цифры в это поле.");
                if (money.Text.Length > 5) MessageBox.Show("Слишком большое число.");
            }
        }
    }
}
