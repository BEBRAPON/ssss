using System.Data;
using System.Security.Policy;
using System.Windows.Controls;
using System.Xml.Linq;
using Lab5.tables.restDataSetTableAdapters;

namespace Lab5
{
    public partial class ChequeInfAd : Page
    {
        chequeTableAdapter cheque = new chequeTableAdapter();
        ordersTableAdapter order = new ordersTableAdapter();
        dishesTableAdapter dish = new dishesTableAdapter();
        public ChequeInfAd()
        {
            InitializeComponent();
            cheques.ItemsSource = cheque.GetData();

            ordChoice.ItemsSource = order.GetData();
            ordChoice.DisplayMemberPath = "order_id";
            ordChoice.SelectedValuePath = "order_id";

            dishChoice.ItemsSource = dish.GetData();
            dishChoice.DisplayMemberPath = "dish_id";
            dishChoice.SelectedValuePath = "dish_id";

            if (dishChoice.SelectedValue != null)
            {
                var items = dish.GetData().Rows;
                for (int i = 0; i < items.Count; i++)
                {
                    if ((int) dishChoice.SelectedValue == (int)items[i][0])
                    {
                        dishanme.Items.Add(items[i][1]);
                        dishCost.Items.Add(items[i][3]);
                    }
                }
            }
        }

        private void cheques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cheques.SelectedItem != null)
            {
                var item = cheques.SelectedItem as DataRowView;
                ordChoice.SelectedValue= (int)item.Row[0];
                dishChoice.SelectedValue = (int)item.Row[1];
                dishanme.SelectedValue = (string) item.Row[2];
                dishCost.SelectedValue = (int)item.Row[3];
            }
        }
    }
}
