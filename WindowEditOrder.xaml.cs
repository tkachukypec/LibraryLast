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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для WindowEditOrder.xaml
    /// </summary>


    public partial class WindowEditOrder : Window
    {
        public bool Add;
        public q_orders Order;
        public WindowEditOrder(q_orders order = null)
        {
            InitializeComponent();
            //ObservableCollection<q_service> Services = CatalogServices.GetServices();
            //ObservableCollection<ServiceFilter> Services = CatalogServices.GetServicesFilter();
            //MessageBox.Show($"{Services.Count}, {Services[0].Service.name}");
            //ListBox_Services.ItemsSource = Services;

            //ObservableCollection<ServiceFilter2> services2 = new ObservableCollection<ServiceFilter2>(DataBase.GetEntities().q_service.Select(p => new ServiceFilter2() { Service = p }));

            //ListBox_Services.ItemsSource = services2;


            ComboBox_Stuff.ItemsSource = CatalogStuff.GetStuff();
            ComboBox_Client.ItemsSource = CatalogClients.GetClients();
            ComboBox_Person.ItemsSource = CatalogPersons.GetPersons();
            ComboBox_Status.ItemsSource = CatalogStatus.GetStatus();
            if (order == null)
            {
                this.Add = true;
                order = new q_orders();
            }

            this.Order = order;

            CopyOrderToTextBoxes();
            //CopyOrderServicesToListBox();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            q_orders order = GetOrderFromTextBoxes();

            if (order == null)
                return;

            if (this.Add)
            {
                Entities context = DataBase.GetEntities();
                context.q_orders.Add(order);
            }

            DataBase.SaveChanges();
            CatalogOrders.Update();

            WindowOrders window = this.Owner as WindowOrders;
            window.Show();

            this.Close();
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            WindowOrders window = this.Owner as WindowOrders;
            window.Show();

            this.Close();
        }
        private void Button_OrderServices_Click(object sender, RoutedEventArgs e)
        {
            q_orders order = this.Order;

            WindowEditOrderServices window = new WindowEditOrderServices(order);
            window.Show();
            window.Owner = this;

            this.Hide();
        }
        private q_orders GetOrderFromTextBoxes()
        {
            q_orders order = this.Order;

            order.q_stuff = ComboBox_Stuff.SelectedItem as q_stuff;
            if (order.q_stuff == null)
            {
                MessageBox.Show("Выберите сотрудника!");
                return null;
            }

            order.q_clients = ComboBox_Client.SelectedItem as q_clients;
            order.q_persons = ComboBox_Person.SelectedItem as q_persons;
            if (!(order.q_clients != null || order.q_persons != null))
            {
                MessageBox.Show("Выберите клиента или юридическое лицо!");
                return null;
            }

            order.q_order_status = ComboBox_Status.SelectedItem as q_order_status;
            if (order.q_order_status == null)
            {
                MessageBox.Show("Выберите статус!");
                return null;
            }

            order.period = tb_Period.Text;
            if (String.IsNullOrEmpty(order.period))
            {
                MessageBox.Show("Заполните период!");
                return null;

            }


            return order;
        }
        private void CopyOrderToTextBoxes()
        {
            q_orders order = this.Order;

            tb_Id.Text = order.id.ToString();
            tb_Period.Text = order.period;
            ComboBox_Stuff.SelectedItem = order.q_stuff;
            ComboBox_Client.SelectedItem = order.q_clients;
            ComboBox_Person.SelectedItem = order.q_persons;
            ComboBox_Status.SelectedItem = order.q_order_status;
        }
        private void CopyOrderServicesToListBox()
        {
            q_orders order = this.Order;
            List<q_services_orders> services = order.q_services_orders.ToList();

            List<q_service> services2 = new List<q_service>();

            for(int i = 0; i < services.Count; i++)
            {
                CopyOrderServiceToListBox(services[i].q_service);
            }
        }
        private void CopyOrderServiceToListBox(q_service service)
        {
            /*ObservableCollection <ServiceFilter> services = CatalogServices.GetServicesFilter();
            ItemCollection items = ListBox_Services.Items;
            for (int i = 0; i < services.Count; i++)
            {
                int index = items.IndexOf(services[i]);
                
                if(index > 0)
                {
                    ServiceFilter sf = items[index] as ServiceFilter;
                    sf.IsChecked = true;
                }
            }*/
            /*ItemCollection items = ListBox_Services.Items;
            int index = items.IndexOf(service);*/
        }

        private void OnClientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Client.SelectedItem != null)
            {
                ComboBox_Person.SelectedItem = null;

            }
        }
        private void OnPersonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Person.SelectedItem != null)
            {
                ComboBox_Client.SelectedItem = null;

            }
        }
    }
}
