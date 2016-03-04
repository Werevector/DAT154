using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWebApp
{
    public class Reservation
    {
        public int rsvr_ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string status { get; set; }
        public int cstmr_ID { get; set; }
        public int rsvr_rID { get; set; }
    }

    public class Room
    {
        //Room class that looks like the rest room class
        public int room_ID { get; set; }
        public int bedsNr { get; set; }
        public string roomSize { get; set; }
        public string pricing { get; set; }
    }
    
}