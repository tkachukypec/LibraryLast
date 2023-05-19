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
    /// Логика взаимодействия для WindowClients.xaml
    /// </summary>
    public partial class WindowClients : Window
    {
        public WindowClients(q_stuff stuff)
        {
            InitializeComponent();

            DataGrid_Clients.ItemsSource = CatalogClients.GetClients();
            CatalogClients.Update();

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
            q_clients Client = DataGrid_Clients.SelectedItem as q_clients;

            if (Client == null)
                return;

            Entities context = DataBase.GetEntities();
            context.q_clients.Remove(Client);
            DataBase.SaveChanges();
            CatalogClients.Update();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            q_clients Client = DataGrid_Clients.SelectedItem as q_clients;

            if (Client == null)
                return;

            WindowEditClient window = new WindowEditClient(Client);
            window.Show();
            window.Owner = this;

            this.Hide();
        }


        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowEditClient window = new WindowEditClient();
            window.Show();
            window.Owner = this;

            this.Hide();
        }

        private void OnSearchStringTextChanged(object sender, TextChangedEventArgs e)
        {
            CatalogClients.Update(tb_SearchString.Text);
        }
    }
}
