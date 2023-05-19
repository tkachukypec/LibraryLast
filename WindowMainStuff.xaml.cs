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
    /// Логика взаимодействия для WindowMainStuff.xaml
    /// </summary>
    public partial class WindowMainStuff : Window
    {
        private q_stuff Stuff;
        public WindowMainStuff(q_stuff stuff)
        {
            InitializeComponent();

            this.Stuff = stuff;
            Label_StuffCategory.Content = stuff.q_category.name;
            Label_FIO.Content = stuff.surname + " " + stuff.name;
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();

            this.Close();
        }
        private void Button_Persons_Click(object sender, RoutedEventArgs e)
        {
            WindowPersons window = new WindowPersons(this.Stuff);
            window.Show();
            window.Owner = this;

            this.Hide();
        }

        private void Button_Clients_Click(object sender, RoutedEventArgs e)
        {
            WindowClients window = new WindowClients(this.Stuff);
            window.Show();
            window.Owner = this;

            this.Hide();
        }

        private void Button_Services_Click(object sender, RoutedEventArgs e)
        {
            WindowServices window = new WindowServices(this.Stuff);
            window.Show();
            window.Owner = this;

            this.Hide();

        }

        private void Button_Orders_Click(object sender, RoutedEventArgs e)
        {
            WindowOrders window = new WindowOrders(this.Stuff);
            window.Show();
            window.Owner = this;

            this.Hide();

        }
    }
}
