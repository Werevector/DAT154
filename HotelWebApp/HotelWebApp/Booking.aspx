<%@ Page Title="Room" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="HotelWebApp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Available rooms</h2>
    <p> 
        <br /><br />
    <asp:Label runat="server" Text="Price:" />
    <asp:DropDownList runat="server"  AutoPostBack="True" ID="pricingDropDown" OnSelectedIndexChanged="priceChange">
        <asp:ListItem Text="All" Value="ALL" />
        <asp:ListItem Text="Low" Value="LOW"/>
        <asp:ListItem Text="Medium" Value="MEDIUM"/>
        <asp:ListItem Text="High" Value="HIGH"/>
    </asp:DropDownList>
    
    <asp:Label runat="server" Text="Size:" />
    <asp:DropDownList runat="server" AutoPostBack="True" ID="roomSizeDropDown" OnSelectedIndexChanged="sizeChange">
        <asp:ListItem Text="All" Value="ALL" />
        <asp:ListItem Text="Small" Value="SMALL"/>
        <asp:ListItem Text="Medium" Value="MEDIUM"/>
        <asp:ListItem Text="Large" Value="LARGE"/>
        <asp:ListItem Text="King" Value="KING"/>
    </asp:DropDownList>

        <asp:GridView ID="roomGrid" runat="server"
            AutoGenerateColumns="False" OnSelectedIndexChanged="Reserve">
            <Columns>
                <asp:BoundField DataField="room_ID" HeaderText="ID" />
                <asp:BoundField DataField="bedsNr" HeaderText="Number of beds" />
                <asp:BoundField DataField="roomSize" HeaderText="Room size" />
                <asp:BoundField DataField="pricing" HeaderText="Price" />
                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Reserve" ShowHeader="True" Text="Reserve" />
            </Columns>
        </asp:GridView>

    </p>
</asp:Content>
