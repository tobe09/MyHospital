<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster.Master" AutoEventWireup="true" CodeBehind="Doctors.aspx.cs" Inherits="HospitalManager.Doctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPH" runat="server">
    <div style="margin-left: 25px; margin-top: 10px;">
        <asp:Table ID="DocTable" runat="server" CssClass="centerText" Width="1200px" BackColor="#ffffcc" Font-Size="18px" BorderWidth="1px" GridLines="Both">
            <asp:TableHeaderRow Font-Bold="true" Font-Size="26px" CssClass="success" Height="40px">
                <asp:TableHeaderCell Width="20px">S/N</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="300px">Image</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="330px">Contact</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="600px">About</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
    </div>
</asp:Content>
