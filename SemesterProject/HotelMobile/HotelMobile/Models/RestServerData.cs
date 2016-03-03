using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HotelMobile.Models
{
    class RestServerData
    {
        public static string URL = "http://92.221.124.167/rest/";

        public static IEnumerable<Maintenance> getMaintenanceList()
        {
            IEnumerable<Maintenance> maintenanceList = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/maintenance").Result;

            if (response.IsSuccessStatusCode)
            {
                maintenanceList = response.Content.ReadAsAsync<IEnumerable<Maintenance>>().Result;
            }

            return maintenanceList;
        }

        public static IEnumerable<Reservation> getReservationList()
        {
            IEnumerable<Reservation> reservationList = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/reservations").Result;

            if (response.IsSuccessStatusCode)
            {
                reservationList = response.Content.ReadAsAsync<IEnumerable<Reservation>>().Result;
            }

            return reservationList;
        }

        public static List<Reservation> getCheckedReservationList()
        {
            IEnumerable<Reservation> reservationList = null;
            List<Reservation> checkedList = new List<Reservation>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/reservations").Result;

            if (response.IsSuccessStatusCode)
            {
                reservationList = response.Content.ReadAsAsync<IEnumerable<Reservation>>().Result;

                foreach(var item in reservationList)
                {
                    if(item.status == "CHECKED")
                    {
                        checkedList.Add(item);
                    }
                }
                
            }

            return checkedList;
        }

        public static IEnumerable<Service> getServiceList()
        {
            IEnumerable<Service> serviceList = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/services").Result;

            if (response.IsSuccessStatusCode)
            {
                serviceList = response.Content.ReadAsAsync<IEnumerable<Service>>().Result;
            }

            return serviceList;
        }

    }
}
