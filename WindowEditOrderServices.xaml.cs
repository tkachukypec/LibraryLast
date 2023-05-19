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
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для WindowEditOrderServices.xaml
    /// </summary>
    public partial class WindowEditOrderServices : Window
    {
        public q_orders Order;
        public WindowEditOrderServices(q_orders order)
        {
            InitializeComponent();
            this.Order = order;

            Label_OrderId.Content = "Номер заказа: " + order.id.ToString();

            ListBox_OrderServices.ItemsSource = GetOrderServices(order);
            ComboBox_Services.ItemsSource = GetServicesNotInOrder(order);
        }

        private ObservableCollection<q_service> GetOrderServices(q_orders order)
        {
            List<q_services_orders> ServicesOrders = order.q_services_orders.ToList();
            ObservableCollection<q_service> Services = new ObservableCollection<q_service>();

            foreach (q_services_orders so in ServicesOrders)
                Services.Add(so.q_service);

            return Services;

        }
        private ObservableCollection<q_service> GetServicesNotInOrder(q_orders order)
        {
            List<q_service> serviceList = DataBase.GetEntities().q_service.ToList();
            ObservableCollection<q_service> orderServices = GetOrderServices(order);

            ObservableCollection<q_service> services = new ObservableCollection<q_service>();

            foreach (q_service service in serviceList)
            {
                if (!orderServices.Contains(service))
                    services.Add(service);
            }
            return services;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            WindowEditOrder window = this.Owner as WindowEditOrder;
            window.Show();

            this.Close();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            q_service service = ListBox_OrderServices.SelectedItem as q_service;

            if (service == null)
                return;

            q_orders order = this.Order;

            if (!GetOrderServices(this.Order).Contains(service))
                return;

            ICollection<q_services_orders> service_orders = order.q_services_orders;
            foreach(q_services_orders so in service_orders)
            {
                if (so.q_service != service)
                    continue;

                DataBase.GetEntities().q_services_orders.Remove(so);
                service_orders.Remove(so);
                break;
            }
            
            ListBox_OrderServices.ItemsSource = GetOrderServices(order);
            ComboBox_Services.ItemsSource = GetServicesNotInOrder(order);
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            q_service service = ComboBox_Services.SelectedItem as q_service;

            if (service == null)
                return;

            q_orders order = this.Order;

            if (GetOrderServices(this.Order).Contains(service))
                return;

            ICollection<q_services_orders> service_orders = order.q_services_orders;

            q_services_orders service_order = new q_services_orders();
            service_order.q_orders = order;
            service_order.q_service = service;
            service_orders.Add(service_order);
            DataBase.GetEntities().q_services_orders.Add(service_order);
            ListBox_OrderServices.ItemsSource = GetOrderServices(order);
            ComboBox_Services.SelectedItem = null;
            ComboBox_Services.ItemsSource = GetServicesNotInOrder(order);

        }
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            q_orders order = this.Order;
            ICollection<q_services_orders> service_orders = order.q_services_orders;
            DataBase.GetEntities().q_services_orders.RemoveRange(service_orders);
            service_orders.Clear();
            ListBox_OrderServices.ItemsSource = GetOrderServices(order);
            ComboBox_Services.ItemsSource = GetServicesNotInOrder(order);

        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            DataBase.SaveChanges();

            WindowEditOrder window = this.Owner as WindowEditOrder;
            window.Show();

            this.Close();
        }
    }
}
