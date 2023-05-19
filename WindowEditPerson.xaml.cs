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
    /// Логика взаимодействия для WindowEditPerson.xaml
    /// </summary>
    public partial class WindowEditPerson : Window
    {
        private bool Add;
        public q_persons Person;
        public WindowEditPerson(q_persons person = null)
        {
            InitializeComponent();

            if (person == null)
            {
                this.Add = true;
                person = new q_persons();
            }

            this.Person = person;

            CopyPersonToTextBoxes();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            q_persons person = GetPersonFromTextBoxes();

            if (person == null)
                return;

            if (this.Add)
            {
                Entities context = DataBase.GetEntities();
                context.q_persons.Add(person);
            }

            DataBase.SaveChanges();
            CatalogPersons.Update();
            WindowPersons window = this.Owner as WindowPersons;
            window.Show();

            this.Close();
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            WindowPersons window = this.Owner as WindowPersons;
            window.Show();

            this.Close();
        }

        private q_persons GetPersonFromTextBoxes()
        {
            q_persons person = this.Person;

            person.code = tb_Code.Text;
            if (String.IsNullOrEmpty(person.code))
            {
                MessageBox.Show("Введен  невалидный код!");
                return null;
            }

            person.title = tb_Title.Text;
            if (String.IsNullOrEmpty(person.title))
            {
                MessageBox.Show("Введено  невалидное название!");
                return null;
            }

            person.address = tb_Address.Text;
            if(String.IsNullOrEmpty(person.address))
            {
                MessageBox.Show("Введен  невалидный адрес!");
                return null;
            }

            person.INN = tb_INN.Text;
            if (String.IsNullOrEmpty(person.INN))
            {
                MessageBox.Show("Введен  невалидный ИНН!");
                return null;
            }

            person.pc = tb_pc.Text;
            if (String.IsNullOrEmpty(person.pc))
            {
                MessageBox.Show("Введен  невалидный pc!");
                return null;
            }

            person.BIK = tb_BIK.Text;
            if (String.IsNullOrEmpty(person.BIK))
            {
                MessageBox.Show("Введен  невалидный BIK!");
                return null;
            }

            person.head = tb_Head.Text;
            if (String.IsNullOrEmpty(person.head))
            {
                MessageBox.Show("Введен  невалидный Глава организации!");
                return null;
            }

            person.contact = tb_Contact.Text;
            if (String.IsNullOrEmpty(person.contact))
            {
                MessageBox.Show("Введено  невалидное Контактное лицо!");
                return null;
            }

            person.phone = tb_Phone.Text;
            if (String.IsNullOrEmpty(person.phone))
            {
                MessageBox.Show("Введен  невалидный номер телефона!");
                return null;
            }

            person.email = tb_Email.Text;
            if (String.IsNullOrEmpty(person.email))
            {
                MessageBox.Show("Введена  невалидная почта!");
                return null;
            }

            person.password = tb_Password.Text;
            if (String.IsNullOrEmpty(person.password))
            {
                MessageBox.Show("Введен  невалидный пароль!");
                return null;
            }

            return person;
        }
        private void CopyPersonToTextBoxes()
        {
            q_persons person = this.Person;

            tb_Id.Text = person.id.ToString();
            tb_Code.Text = person.code;
            tb_Title.Text = person.title;
            tb_Address.Text = person.address;
            tb_INN.Text = person.INN;
            tb_pc.Text = person.pc;
            tb_BIK.Text = person.BIK;
            tb_Head.Text = person.head;
            tb_Contact.Text = person.contact;
            tb_Phone.Text = person.phone;
            tb_Email.Text = person.email;
            tb_Password.Text = person.password;

        }
    }
}
