using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab5.tables.avtorizDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для AvtorizPage.xaml
    /// </summary>
    public partial class AvtorizPage : Page
    {
        usersTableAdapter adapter = new usersTableAdapter();
        public AvtorizPage()
        {
            InitializeComponent();
        }

        private void Avtorization_Click(object sender, RoutedEventArgs e)
        {
            bool check2 = true;
            if (Login.Text == "" || Password.Password == "")
            {
                MessageBox.Show("Не все поля заполнены.");
                check2 = false;
            }
            if (check2 == true)
            {

                bool check = false;

                var all_logins = adapter.GetData().Rows;

                for (int i = 0; i < all_logins.Count; i++)
                {
                    if (all_logins[i][1].ToString() == Login.Text &&
                        all_logins[i][2].ToString() == Password.Password)
                    {
                        int role_id = (int)all_logins[i][3];

                        Login.Visibility = Visibility.Hidden;
                        Password.Visibility = Visibility.Hidden;
                        border.Visibility = Visibility.Hidden;
                        Avtorization.Visibility = Visibility.Hidden;
                        tb.Visibility = Visibility.Hidden;

                        check = true;

                        switch (role_id)
                        {
                            case 1:
                                FrameFr.Content = new FirstAdPage();
                                break;
                            case 2:
                                FrameFr.Content = new FirstNoAdPage();
                                break;
                                /*case 4:
                                    FrameFr.Content = new FirstNoAdPage();
                                    break;*/
                        }
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Ошибка авторизации. Неверный лоигн или пароль.");
                }
            }
        }
    }
}
