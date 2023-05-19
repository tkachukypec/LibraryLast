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
    /// Логика взаимодействия для WindowEditService.xaml
    /// </summary>
    public partial class WindowEditService : Window
    {
        public bool Add;
        public q_service Service;
        public WindowEditService(q_service service = null)
        {
            InitializeComponent();

            if (service == null)
            {
                this.Add = true;
                service = new q_service();
            }

            this.Service = service;

            CopyServiceToTextBoxes();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            q_service service = GetServiceFromTextBoxes();

            if (service == null)
                return;

            if (this.Add)
            {
                Entities context = DataBase.GetEntities();
                context.q_service.Add(service);
            }

            DataBase.SaveChanges();
            CatalogServices.Update();

            WindowServices window = this.Owner as WindowServices;
            window.Show();

            this.Close();
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            WindowServices window = this.Owner as WindowServices;
            window.Show();

            this.Close();
        }
        private q_service GetServiceFromTextBoxes()
        {
            q_service service = this.Service;

            service.name = tb_Name.Text;
            service.code = tb_Code.Text;
            service.period = tb_Period.Text;
            service.avg_variance = tb_AvgVariance.Text;

            int price;
            if (!Int32.TryParse(tb_Price.Text, out price))
            {
                MessageBox.Show("Неверно введена цена!");
                return null;
            }
            service.price = price;

            return service;
        }
        private void CopyServiceToTextBoxes()
        {
            q_service service = this.Service;

            tb_Id.Text = service.id.ToString();
            tb_Name.Text = service.name;
            tb_Code.Text = service.code;
            tb_Period.Text = service.period;
            tb_AvgVariance.Text = service.avg_variance;
            tb_Price.Text = service.price.ToString();
        }
    }
}
