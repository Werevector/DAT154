using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HotelMobile.Models
{
    class Reservation
    {
        public int rsvr_ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string status { get; set; }
        public int cstmr_ID { get; set; }
        public int rsvr_rID { get; set; }

        //Edits already existing data of model in database
        public async Task<bool> editModelData()
        {
            bool success = true;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PutAsJsonAsync("edit/reservation", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return success;
        }

        //Sends new instance of model data to server
        public async void createModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PostAsJsonAsync("add/reservation", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

        }

        //Delete existing model in database
        public void deleteModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);
            HttpResponseMessage response;

            response = client.DeleteAsync("delete/reservation/?id=" + rsvr_rID).Result;
        }
    }

}
