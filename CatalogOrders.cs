using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    class CatalogOrders
    {
        public static ObservableCollection<q_orders> Orders;
        public static ObservableCollection<q_orders> GetOrders()
        {
            CreateOrders();

            return Orders;
        }
        private static void CreateOrders()
        {
            if (Orders == null)
            {
                Orders = new ObservableCollection<q_orders>();
                Update();
            }
        }

        public static void Update()
        {
            CreateOrders();

            Orders.Clear();

            Entities context = DataBase.GetEntities();
            List<q_orders> orders = context.q_orders.ToList();

            foreach (q_orders order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
