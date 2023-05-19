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
    /// Логика взаимодействия для WindowServices.xaml
    /// </summary>
    public partial class WindowServices : Window
    {
        public WindowServices(q_stuff stuff)
        {
            InitializeComponent();

            DataGrid_Services.ItemsSource = CatalogServices.GetServices();
            CatalogServices.Update();
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
            q_service Service = DataGrid_Services.SelectedItem as q_service;

            if (Service == null)
                return;

            Entities context = DataBase.GetEntities();
            context.q_service.Remove(Service);
            DataBase.SaveChanges();
            CatalogServices.Update();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            q_service Service = DataGrid_Services.SelectedItem as q_service;

            if (Service == null)
                return;

            WindowEditService window = new WindowEditService(Service);
            window.Show();
            window.Owner = this;

            this.Hide();
        }


        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {

            WindowEditService window = new WindowEditService();
            window.Show();
            window.Owner = this;

            this.Hide();
        }

        private void OnSearchStringTextChanged(object sender, TextChangedEventArgs e)
        {
            CatalogServices.Update(tb_SearchString.Text);
        }
    }
}
