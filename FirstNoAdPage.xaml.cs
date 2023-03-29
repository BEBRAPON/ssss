using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для FirstNoAdPage.xaml
    /// </summary>
    public partial class FirstNoAdPage : Page
    {
        public FirstNoAdPage()
        {
            InitializeComponent();
            tableChoice.Items.Add("Оформление заказа");
            tableChoice.Items.Add("Сохранённые чеки");
            tableChoice.Items.Add("Возврат к авторизации");

            tableChoice.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableChoice.SelectedIndex == 0)
            {
                PageAd1.Content = new OrderAdd();
            }
            if (tableChoice.SelectedIndex == 1)
            {
                PageAd1.Content = new ChequesInf();
            }
            if (tableChoice.SelectedIndex == 2)
            {
                tableChoice.Visibility = Visibility.Hidden;
                PageAd12.Content = new AvtorizPage();
            }

        }
    }
}
