using System;
using System.Data;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    public partial class PositionsInfAd : Page
    {
        positionsTableAdapter post = new positionsTableAdapter();
        public PositionsInfAd()
        {
            InitializeComponent();
            positions.ItemsSource = post.GetData();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || salaries.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                try
                {
                    int sal = Convert.ToInt32(salaries.Text);
                    if (sal < 0)
                    {
                        MessageBox.Show("Зарплата не может быть отрицательным числом.");
                    }
                    else
                    {
                        try
                        {
                            int name1 = Convert.ToInt32(name.Text);
                            MessageBox.Show("Название должности не может быть числом.");
                        }
                        catch
                        {
                            {
                                post.InsertQuery(name.Text, sal);
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Неверно введено значение. Зарплата должна быть числом.");
                }
                positions.ItemsSource = post.GetData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (positions.SelectedItem != null)
            {
                int id = (int)(positions.SelectedItem as DataRowView).Row[0];
                post.DeleteQuery(id);
                positions.ItemsSource = post.GetData();
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (positions.SelectedItem != null)
            {
                if (name.Text == "" || salaries.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    int sal = Convert.ToInt32(salaries.Text);
                    if (sal < 0)
                    {
                        MessageBox.Show("Зарплата не может быть отрицательным числом.");
                    }
                    else
                    {
                        var item = positions.SelectedItem as DataRowView;
                        post.UpdateQuery(name.Text, sal, (int)item.Row[0]);
                        positions.ItemsSource = post.GetData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Строка таблицы не выбрана.");
            }
        }

        private void positions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (positions.SelectedItem != null)
            {
                var item = positions.SelectedItem as DataRowView;
                name.Text = Convert.ToString(item.Row[1]);
                salaries.Text = Convert.ToString(item.Row[2]);
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
