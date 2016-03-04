using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.SessionState;
using Newtonsoft.Json;
using RestSharp;


namespace HotelWebApp
{

    public partial class RoomSearch : System.Web.UI.Page
    {
        // So this class needs to search for reservations from a start date to a finished date.
        // Then it needs to create a list of reservations and it needs to send this to room somehow
        string errorMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string startDt;
            string endDt;

            startDt = startYearDrop.SelectedValue + "-" + startMonthDrop.SelectedValue + "-" + startDayDrop.SelectedValue;
            endDt = endYearDrop.SelectedValue + "-" + endMonthDrop.SelectedValue + "-" + endDayDrop.SelectedValue;

            if (Logic.validateDates(startDt, endDt))
            {
                Session["startdate"] = startDt;
                Session["enddate"] = endDt;

                Response.Redirect("Booking.aspx");

            } else {
                errorMsg = "Wrong date";
            }

        }
    }
}