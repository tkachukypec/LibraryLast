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
    class CatalogStatus
    {
        public static ObservableCollection<q_order_status> Status;
        public static ObservableCollection<q_order_status> GetStatus()
        {
            CreateStatus();

            return Status;
        }
        private static void CreateStatus()
        {
            if (Status == null)
            {
                Status = new ObservableCollection<q_order_status>();
                Update();
            }

        }

        public static void Update()
        {
            CreateStatus();

            Status.Clear();

            Entities context = DataBase.GetEntities();
            List<q_order_status> status = context.q_order_status.ToList();

            foreach (q_order_status s in status)
            {
                Status.Add(s);
            }
        }
    }
}
