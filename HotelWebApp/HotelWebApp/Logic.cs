using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HotelWebApp
{
    public class Logic
    {
        private const string URL = "http://92.221.124.167/rest";
        public static List<Room> search(string startDate, string endDate, List<Reservation> reservations, List<Room> rooms)
        {
            List<Room> unAvailableRooms = new List<Room>();
            bool valid = false;
            foreach (Room room in rooms) 
            {
                foreach (Reservation res in reservations)
                {
                    valid = validDate(res.startDate, res.endDate, startDate, endDate);
                    if(!valid && room.room_ID == res.rsvr_rID)
                    {
                       unAvailableRooms.Add(room);
                    }
                }
            }

            foreach (Room room in unAvailableRooms)
            {
                rooms.RemoveAll(x => x.room_ID.Equals(room.room_ID));
            }
            return rooms;  
        }

        public static List<Room> getRoomList()
        {
            RestClient rest = new RestClient(URL);
            var request = new RestRequest("/listing/rooms", Method.GET);

            var response = rest.Execute(request);
            string result = response.Content;

            List<Room> rooms = new List<Room>();
            dynamic data = JsonConvert.DeserializeObject(result);

            for (var i = 0; i < data.Count; i++)
            {
                //Converting from json string objects to room objects and add them to a list of rooms.
                Room room = JsonConvert.DeserializeObject<Room>(data[i].ToString());
                rooms.Add(room);
            }
            rooms.Sort(delegate (Room x, Room y)
            {
                if (x.bedsNr == 0 && y.bedsNr == 0) return 0;
                else if (x.bedsNr == 0) return -1;
                else if (y.bedsNr == 0) return 1;
                else return x.bedsNr.CompareTo(y.bedsNr);
            });

            return rooms;

        }

        public static List<Reservation> getReservationList()
        {
            RestClient rest = new RestClient(URL);
            var request = new RestRequest("/listing/reservations", Method.GET);

            var response = rest.Execute(request);
            string result = response.Content;

            dynamic data = JsonConvert.DeserializeObject(result);
            List<Reservation> reserv = new List<Reservation>();
            for (var i = 0; i < data.Count; i++)
            {
                Reservation res = JsonConvert.DeserializeObject<Reservation>(data[i].ToString());
                reserv.Add(res);
            }

                return reserv;
        }

        public static void createReservation(int room_ID, string startDate, string endDate, int userID)
        {
            List<Reservation> temp = getReservationList();
            int id = 0;
            foreach (Reservation tempRes in temp)
            {
                if (tempRes.rsvr_ID > id)
                {
                    id = tempRes.rsvr_ID;
                }
            }
            
            string status = "RESERVED";

            Reservation res = new Reservation();
            res.rsvr_ID = id+1;
            res.rsvr_rID = room_ID;
            res.startDate = startDate;
            res.status = status;
            res.endDate = endDate;
            res.cstmr_ID = userID;
            string output = JsonConvert.SerializeObject(res);
            RestClient rest = new RestClient(URL);
            var request = new RestRequest("/add/reservation", Method.POST);

            request.AddParameter("application/json; charset=utf-8", output, ParameterType.RequestBody);
           // request.RequestFormat = DataFormat.Json;

            try
            {
                rest.ExecuteAsync(request, response =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string test = "ok";
                    }
                    else
                    {
                        string test = "nope";
                    }
                });
            }
            catch (Exception error)
            {
                // Error
            }
        }

        public static int login(string userName, string password)
        {
            RestClient rest = new RestClient(URL);
            var request = new RestRequest("/listing/account/?usrName="+userName, Method.GET);

            var response = rest.Execute(request);
            string result = response.Content;

            dynamic data = JsonConvert.DeserializeObject(result);
            if(data == null)
            {
                return 0;
            } else if(data[0].Password == password)
            {
                return data[0].account_ID;
            }

            return 0;
        }

        public static bool validateDates(string startDate, string endDate)
        {
            DateTime res = new DateTime();
            if (!DateTime.TryParse(startDate, out res) || !DateTime.TryParse(endDate, out res))
            {
                return false;
            }

            if (DateTime.Compare(DateTime.Parse(startDate), DateTime.Parse(endDate)) < 0) {
                return true;
            }

            return false;
        }

        public static bool validDate(string resStartDate, string resEndDate, string startDate, string endDate)
        {
            DateTime resStartDateParsed = DateTime.Parse(resStartDate);
            DateTime startDateParsed = DateTime.Parse(startDate);
            DateTime resEndDateParsed = DateTime.Parse(resEndDate);
            DateTime endDateParsed = DateTime.Parse(endDate);

            if (DateTime.Compare(endDateParsed, resStartDateParsed) < 0)
            {
                return true;
            }
            if (DateTime.Compare(startDateParsed, resEndDateParsed) > 0)
            {
                return true;
            }
            return false;
        }
    }
}