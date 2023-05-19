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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string Login = tb_Login.Text;
            string Password = pb_Password.Password;

            Entities context = DataBase.GetEntities();

            q_stuff Stuff = context.q_stuff.FirstOrDefault(stuff => stuff.login == Login && stuff.password == Password);
            
            if(Stuff != null)
            {
                MessageBox.Show($"Добро пожаловать, {Stuff.surname} {Stuff.name}");
                WindowMainStuff window = new WindowMainStuff(Stuff);
                window.Show();
                this.Close();

                return;
            }

            q_clients Client = context.q_clients.FirstOrDefault(client => client.email == Login && client.password == Password);

            if (Client != null)
            {
                MessageBox.Show($"Добро пожаловать, {Client.surname} {Client.name}");

                return;
            }

            // Физ лица
            q_persons Person = context.q_persons.FirstOrDefault(person => person.email == Login && person.password == Password);

            if(Person != null)
            {
                MessageBox.Show($"Добро пожаловать, {Person.head}");

                return;
            }

            MessageBox.Show("Неверный логин или пароль.");

        }
    }
}
