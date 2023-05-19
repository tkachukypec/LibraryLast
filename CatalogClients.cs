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
    class CatalogClients
    {
        public static ObservableCollection<q_clients> Clients;
        public static ObservableCollection<q_clients> GetClients()
        {
            CreateClients();

            return Clients;
        }
        private static void CreateClients()
        {
            if (Clients == null)
            {
                Clients = new ObservableCollection<q_clients>();
                Update();
            }

        }

        public static void Update(string searchString = "")
        {
            CreateClients();

            Clients.Clear();

            Entities context = DataBase.GetEntities();
            List<q_clients> clients = context.q_clients.ToList();

            SearchClientsByString(ref clients, searchString);

            foreach(q_clients client in clients)
            {
                Clients.Add(client);
            }
        }

        private static void SearchClientsByString(ref List<q_clients> clients, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return;

            clients = clients.Where(client => client.surname.Contains(searchString.Trim())).ToList();
        }
    }
}
