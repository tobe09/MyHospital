<%@ Page Language="C#" MasterPageFile="~/HospitalMaster.Master" Title="Testing Page" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="HospitalManager.TestPage" %>

<asp:Content ID="TestContent" runat="server" ContentPlaceHolderID="ContentPH">

    <br />
    <br />
    <br />
    First name&nbsp;&nbsp;<asp:TextBox ID="first" runat="server"></asp:TextBox><br />
    Last name&nbsp;&nbsp;<asp:TextBox ID="last" runat="server"></asp:TextBox><br />
    Date&nbsp;&nbsp;<asp:TextBox ID="date" runat="server"></asp:TextBox><br />
    <br />
    <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" /><br />
    <br />
    <br />
    <asp:Label ID="statusLabel" runat="server" Text="" CssClass="error big"></asp:Label>

    <br /><br />
    <div>
        <asp:Label ID="lblmessage" runat="server" Text="Current time" Font-Size="Larger"></asp:Label>
    </div>
    <br /><br />
    <asp:DropDownList ID="ddlTest" runat="server" Width="10%" CssClass="textBox"></asp:DropDownList>

</asp:Content>
