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
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для WindowOrders.xaml
    /// </summary>
    public partial class WindowOrders : Window
    {
        public WindowOrders(q_stuff stuff)
        {
            InitializeComponent();

            DataGrid_Orders.ItemsSource = CatalogOrders.GetOrders();

            Label_FIO.Content = stuff.surname + " " + stuff.name;
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();

            this.Close();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            WindowMainStuff window = this.Owner as WindowMainStuff;
            window.Show();

            this.Close();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            q_orders Order = DataGrid_Orders.SelectedItem as q_orders;

            if (Order == null)
                return;

            Entities context = DataBase.GetEntities();
            context.q_orders.Remove(Order);
            DataBase.SaveChanges();
            CatalogOrders.Update();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            q_orders Order = DataGrid_Orders.SelectedItem as q_orders;

            if (Order == null)
                return;

            WindowEditOrder window = new WindowEditOrder(Order);
            window.Show();
            window.Owner = this;

            this.Hide();
        }


        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowEditOrder window = new WindowEditOrder();
            window.Show();
            window.Owner = this;

            this.Hide();
        }
    }
}
