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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp4
{
    class CatalogServices
    {
        public static ObservableCollection<q_service> Services;
        public static ObservableCollection<ServiceFilter> ServicesFilters;
        public static ObservableCollection<q_service> GetServices()
        {
            CreateServices();

            return Services;
        }
        public static ObservableCollection<ServiceFilter> GetServicesFilter()
        {
            CreateServices();

            return ServicesFilters;
        }
        private static void CreateServices()
        {
            if (Services == null)
            {
                Services = new ObservableCollection<q_service>();
                Update();
            }
            if(ServicesFilters == null)
            {
                ServicesFilters = new ObservableCollection<ServiceFilter>();
                Update();
            }
        }

        public static void Update(string searchString = "")
        {
            CreateServices();

            Services.Clear();
            ServicesFilters.Clear();

            Entities context = DataBase.GetEntities();
            List<q_service> services = context.q_service.ToList();

            SearchServicesByString(ref services, searchString);

            foreach (q_service service in services)
            {
                ServiceFilter sf = new ServiceFilter();
                sf.Service = service;
                sf.IsChecked = true;

                Services.Add(service);
                ServicesFilters.Add(sf);
            }
        }
        private static void SearchServicesByString(ref List<q_service> services, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return;

            services = services.Where(service => service.name.Contains(searchString.Trim())).ToList();
        }
    }
}
