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
    /// Логика взаимодействия для FirstAdPage.xaml
    /// </summary>
    public partial class FirstAdPage : Page
    {
        
        public FirstAdPage()
        {
            InitializeComponent();
            tableChoice.Items.Add("Данные о ролях");
            tableChoice.Items.Add("Данные для входа");
            tableChoice.Items.Add("Данные о должностях");
            tableChoice.Items.Add("Данные о сотрудниках");
            tableChoice.Items.Add("Данные о клиентах");
            tableChoice.Items.Add("Данные о заказах");
            tableChoice.Items.Add("Данные о поставщиках");
            tableChoice.Items.Add("Данные о продуктах");
            tableChoice.Items.Add("Данные о видах блюд");
            tableChoice.Items.Add("Данные о блюдах");
            tableChoice.Items.Add("Данные о деталях блюда");
            tableChoice.Items.Add("Данные о чеках");
            tableChoice.Items.Add("Возврат к авторизации");

            tableChoice.Visibility = Visibility.Visible;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableChoice.SelectedIndex == 0)
            {
                PageAd1.Content = new RoleInfAd();
            }
            if (tableChoice.SelectedIndex == 1)
            {
                PageAd1.Content = new LogInfAd();
            }
            if (tableChoice.SelectedIndex == 2)
            {
                PageAd1.Content = new PositionsInfAd();
            }
            if (tableChoice.SelectedIndex == 3)
            {
                PageAd1.Content = new WorkersInfAd();
            }
            if (tableChoice.SelectedIndex == 4)
            {
                PageAd1.Content = new ClientInfAd();
            }
            if (tableChoice.SelectedIndex == 5)
            {
                PageAd1.Content = new OrdersInfAd();
            }
            if (tableChoice.SelectedIndex == 6)
            {
                PageAd1.Content = new ProdviderInfAd();
            }
            if (tableChoice.SelectedIndex == 7)
            {
                PageAd1.Content = new ProductInfAd();
            }
            if (tableChoice.SelectedIndex == 8)
            {
                PageAd1.Content = new TypeOfDishesInfAd();
            }
            if (tableChoice.SelectedIndex == 9)
            {
                PageAd1.Content = new DishsesInfAd();
            }
            if (tableChoice.SelectedIndex == 10)
            {
                PageAd1.Content = new DetailsOfDishesAd();
            }
            if (tableChoice.SelectedIndex == 11)
            {
                PageAd1.Content = new ChequesInf();
            }
            if (tableChoice.SelectedIndex == 12)
            {
                tableChoice.Visibility = Visibility.Hidden;
                pageFr.Content = new AvtorizPage();
            }
        }
    }
}
