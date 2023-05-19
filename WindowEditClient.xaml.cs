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
    /// Логика взаимодействия для WindowEditClient.xaml
    /// </summary>
    public partial class WindowEditClient : Window
    {
        private bool Add;
        public q_clients Client;
        public WindowEditClient(q_clients client = null)
        {
            InitializeComponent();

            if(client == null)
            {
                this.Add = true;
                client = new q_clients();
            }

            this.Client = client;

            CopyClientToTextBoxes();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            q_clients client = GetClientFromTextBoxes();

            if (client == null)
                return;

            if(this.Add)
            {
                Entities context = DataBase.GetEntities();
                context.q_clients.Add(client);
            }

            DataBase.SaveChanges();
            CatalogClients.Update();
            WindowClients window = this.Owner as WindowClients;
            window.Show();

            this.Close();
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            WindowClients window = this.Owner as WindowClients;
            window.Show();

            this.Close();
        }

        private q_clients GetClientFromTextBoxes()
        {
            q_clients client = this.Client;

            client.surname = tb_Surname.Text;
            client.name = tb_Name.Text;
            client.patronymic = tb_Patronymic.Text;
            client.address = tb_Address.Text;
            client.email = tb_Login.Text;
            client.password = tb_Password.Text;

            int seria;
            if(!Int32.TryParse(tb_Seria.Text, out seria))
            {
                MessageBox.Show("Введена невалидная серия пасспорта");
                return null;
            }
            int number;
            if (!Int32.TryParse(tb_Number.Text, out number))
            {
                MessageBox.Show("Введен невалидный номер пасспорта");
                return null;
            }

            client.passport_seria = seria;
            client.passport_number = number;

            return client;
        }
        private void CopyClientToTextBoxes()
        {
            q_clients client = this.Client;

            tb_Id.Text = client.id.ToString();
            tb_Surname.Text = client.surname;
            tb_Name.Text = client.name;
            tb_Patronymic.Text = client.patronymic;
            tb_Seria.Text = client.passport_seria.ToString();
            tb_Number.Text = client.passport_number.ToString();
            tb_Address.Text = client.address;
            tb_Login.Text = client.email;
            tb_Password.Text = client.password;
        }
    }
}
