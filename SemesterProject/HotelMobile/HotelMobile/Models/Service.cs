using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HotelMobile.Models
{
    public class Service
    {
        public int srv_ID { get; set; }
        public string note { get; set; }
        public int room_ID { get; set; }

        //Edits already existing data of model in database
        public async Task<bool> editModelData()
        {
            bool success = true;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PutAsJsonAsync("edit/service", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            return success;
        }

        //Sends new instance of model data to server
        public async void createModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);

            await client.PostAsJsonAsync("add/service", this)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

        }

        //Delete existing model in database
        public void deleteModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RestServerData.URL);
            HttpResponseMessage response;

            response = client.DeleteAsync("delete/service/?id=" + srv_ID).Result;
        }
    }
}
