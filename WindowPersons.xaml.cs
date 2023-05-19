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
    /// Логика взаимодействия для WindowPersons.xaml
    /// </summary>
    public partial class WindowPersons : Window
    {
        public WindowPersons(q_stuff stuff)
        {
            InitializeComponent();

            DataGrid_Persons.ItemsSource = CatalogPersons.GetPersons();
            CatalogPersons.Update();

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
            q_persons Person = DataGrid_Persons.SelectedItem as q_persons;

            if (Person == null)
                return;

            Entities context = DataBase.GetEntities();
            context.q_persons.Remove(Person);
            DataBase.SaveChanges();
            CatalogPersons.Update();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            q_persons Person = DataGrid_Persons.SelectedItem as q_persons;

            if (Person == null)
                return;

            WindowEditPerson window = new WindowEditPerson(Person);
            window.Show();
            window.Owner = this;

            this.Hide();
        }


        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowEditPerson window = new WindowEditPerson();
            window.Show();
            window.Owner = this;

            this.Hide();
        }

        private void OnSearchStringTextChanged(object sender, TextChangedEventArgs e)
        {
            CatalogPersons.Update(tb_SearchString.Text);
        }
    }
}
