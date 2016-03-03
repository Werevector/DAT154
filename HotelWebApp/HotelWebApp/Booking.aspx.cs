using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebApp
{
    public class Room
    {
        //Room class that looks like the rest room class
        public int room_ID { get; set; }
        public int bedsNr { get; set; }
        public string roomSize { get; set; }
        public string pricing { get; set; }
    }


    public partial class Contact : Page
    {
        // I use something called RestSharp which have built in support for easy rest server use
        // I create a new rest client
        string startDate, endDate;
        int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            List<Room> rooms = Logic.getRoomList();
            List<Reservation> reserv = Logic.getReservationList();

            if (Session["startdate"] != null && Session["enddate"] != null) { 
            startDate = (string)(Session["startdate"]);
            endDate = (string)(Session["enddate"]);
            
            } else
            {
                Response.Redirect("/Default.aspx");
            }
            userID = (int)(Session["userID"]);

            roomGrid.DataSource = Logic.search(startDate, endDate, reserv, rooms);
            roomGrid.DataBind();
        }

        protected void bindData(List<Room> roomList)
        {
            roomGrid.DataSource = roomList;
            roomGrid.DataBind();
        }

        protected void priceChange(object sender, EventArgs e)
        {
            List<Room> newRooms = new List<Room>();
            foreach (Room room in (List<Room>)roomGrid.DataSource)
            {
                if (room.pricing == pricingDropDown.SelectedValue &&
                    (room.roomSize == roomSizeDropDown.SelectedValue || roomSizeDropDown.SelectedValue == "ALL"))
                {
                    newRooms.Add(room);
                }
            }
            bindData(newRooms);
        }

        protected void sizeChange(object sender, EventArgs e)
        {
            List<Room> newRooms = new List<Room>();
            foreach (Room room in (List<Room>)roomGrid.DataSource)
            {
                if (room.roomSize == roomSizeDropDown.SelectedValue &&
                    (room.pricing == pricingDropDown.SelectedValue || pricingDropDown.SelectedValue == "ALL"))
                {
                    newRooms.Add(room);
                }
            }
            bindData(newRooms);
        }

        protected void Reserve(object sender, EventArgs e)
        {
            GridViewRow row = roomGrid.SelectedRow;
            string hei = row.Cells[0].Text;
            int room_ID = int.Parse(hei);
            Logic.createReservation(room_ID, startDate, endDate, userID);
            Response.Redirect("/Default.aspx");
        }
    }
}