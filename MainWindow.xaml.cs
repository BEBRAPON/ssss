using System.Windows;
using Lab5.tables.avtorizDataSetTableAdapters;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        usersTableAdapter adapter = new usersTableAdapter();
        public MainWindow()
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
;               }
            }
        }
    }
}
