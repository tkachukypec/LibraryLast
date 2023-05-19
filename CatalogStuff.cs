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
    class CatalogStuff
    {
        public static ObservableCollection<q_stuff> Stuff;
        public static ObservableCollection<q_stuff> GetStuff()
        {
            CreateStuff();

            return Stuff;
        }
        private static void CreateStuff()
        {
            if (Stuff == null)
            {
                Stuff = new ObservableCollection<q_stuff>();
                Update();
            }

        }

        public static void Update()
        {
            CreateStuff();

            Stuff.Clear();

            Entities context = DataBase.GetEntities();
            List<q_stuff> stuff = context.q_stuff.ToList();

            foreach (q_stuff s in stuff)
            {
                Stuff.Add(s);
            }
        }
    }
}
