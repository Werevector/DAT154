using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HotelMobile.Models;

namespace HotelMobile.Models
{
    public class Maintenance
    {
        public int mtr_ID { get; set; }
        public string mtr_Note { get; set; }
        public string mtr_status { get; set; }
        public int mtr_room { get; set; }

        //Edits already existing data of model in database
        public async Task<bool> editModelData()
        {
            bool success = true;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PutAsJsonAsync("edit/maintenance", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return success;
        }

        //Sends new instance of model data to server
        public async void createModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PostAsJsonAsync("add/maintenance", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

        }

        //Delete existing model in database
        public void deleteModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);
            HttpResponseMessage response;

            response = client.DeleteAsync("delete/maintenance/?id=" + mtr_ID).Result;
        }
    }
}
