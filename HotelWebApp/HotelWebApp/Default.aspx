<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Hotel room rental</h1>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
        <asp:Login 
            ID="Login1" 
            runat="server" 
            OnAuthenticate="login"
            DestinationPageUrl="~/RoomSearch.aspx">
        </asp:Login>
        <p>&nbsp;</p>
    </div>

    <div class="row">
    </div>

</asp:Content>
