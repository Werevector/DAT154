using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebApp
{
    
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                Response.Redirect("/RoomSearch.aspx");
            }

        }

        protected void login(object sender, AuthenticateEventArgs e)
        {
            string usrName = Login1.UserName;
            string psswrd = Login1.Password;
            int userID = Logic.login(usrName, psswrd);
            if(userID > 0) {
                Session["userID"] = userID;
                Response.Redirect("/RoomSearch.aspx");
            } else
            {
                Response.Redirect("/Default.aspx");
            }
        }
    }
}