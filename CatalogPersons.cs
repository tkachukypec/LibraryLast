using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WpfApp4
{
    class CatalogPersons
    {
        public static ObservableCollection<q_persons> Persons;
        public static ObservableCollection<q_persons> GetPersons()
        {
            CreatePersons();

            return Persons;
        }
        private static void CreatePersons()
        {
            if (Persons == null)
            {
                Persons = new ObservableCollection<q_persons>();
                Update();
            }

        }

        public static void Update(string searchString = "")
        {
            CreatePersons();

            Persons.Clear();

            Entities context = DataBase.GetEntities();
            List<q_persons> persons = context.q_persons.ToList();

            SearchPersonsByString(ref persons, searchString);

            foreach (q_persons person in persons)
            {
                Persons.Add(person);
            }
        }
        private static void SearchPersonsByString(ref List<q_persons> persons, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return;

            persons = persons.Where(person => person.title.Contains(searchString.Trim())).ToList();
        }
    }
}
