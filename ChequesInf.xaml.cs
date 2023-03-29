using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для ChequesInf.xaml
    /// </summary>
    public partial class ChequesInf : Page
    {
        chequeTableAdapter cheq = new chequeTableAdapter();
        ordersTableAdapter order = new ordersTableAdapter();
        customersTableAdapter client = new customersTableAdapter();
        workersTableAdapter worker = new workersTableAdapter();
        List<Cheq> chequesInf = new List<Cheq>();
        public ChequesInf()
        {
            InitializeComponent();

            clientname.Visibility= Visibility.Hidden;
            clientname1.Visibility= Visibility.Hidden;
            workername.Visibility = Visibility.Hidden;
            workername1.Visibility = Visibility.Hidden;
            money.Visibility = Visibility.Hidden;
            money1.Visibility = Visibility.Hidden;
            timeDate.Visibility = Visibility.Hidden;
            timeDate1.Visibility = Visibility.Hidden;

            var orders = order.GetData().Rows;
            for (int i = 0; i < orders.Count; i++)
            {
                string path = @"F:\\chequesInf\\cheque№" + (int)orders[i][0];
                if (File.Exists(path))
                {
                    OrderChoice.Items.Add("Чек №" + orders[i][0]);
                }
            }

            cheques.ItemsSource = chequesInf;
        }

        private void OrderChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chequesInf.Clear();
            cheques.Items.Refresh();

            clientname.Visibility = Visibility.Visible;
            clientname1.Visibility = Visibility.Visible;
            workername.Visibility = Visibility.Visible;
            workername1.Visibility = Visibility.Visible;
            money.Visibility = Visibility.Visible;
            money1.Visibility = Visibility.Visible;
            timeDate.Visibility = Visibility.Visible;
            timeDate1.Visibility = Visibility.Visible;

            var chequess = cheq.GetData().Rows; 
            var clients = client.GetData().Rows;
            var orders = order.GetData().Rows;
            var workers = worker.GetData().Rows;

            string ordern = (string)OrderChoice.SelectedValue;
            ordern = ordern.Substring(5);

            for (int j = 0; j < chequess.Count; j++)
            {
                if ((int)chequess[j][0] == Convert.ToInt32(ordern))
                {
                    chequesInf.Add(new Cheq { orderid = (int)chequess[j][0], dishid = (int)chequess[j][1], dishname = (string)chequess[j][2], dishcost = (int)chequess[j][3] });
                }
            }
            cheques.Items.Refresh();

            for (int i = 0; i < orders.Count; i++)
            { 
                if (Convert.ToInt32(ordern) == (int)orders[i][0])
                {
                    for (int j = 0; j < clients.Count; j++)
                    {
                        if ((int)clients[j][0] == (int)orders[i][1])
                        {
                            clientname.Text = clients[j][1].ToString();

                        }
                    }
                    for (int j = 0; j < workers.Count; j++)
                    {
                        if ((int)workers[j][0] == (int)orders[i][2])
                        {
                            workername.Text = workers[j][1].ToString() + " " + workers[j][2].ToString();

                        }
                    }
                }
            }
            string[] lines = File.ReadAllLines(@"F:\\chequesInf\\cheque№" + Convert.ToInt32(ordern));
            int len = lines.Length;
            string clientCash = lines[len - 2].Substring(10);
            money.Text = clientCash;

            timeDate.Text =  File.GetCreationTime(@"F:\\chequesInf\\cheque№" + Convert.ToInt32(ordern)).ToString();
        }
    }
}
