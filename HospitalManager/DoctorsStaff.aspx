<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="DoctorsStaff.aspx.cs" Inherits="HospitalManager.Staffs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centerText paraNormal"><b>Employee related activities</b></div>
    <br />
    <br />
    <asp:LinkButton ID="DocActLinkButton" runat="server" CssClass="link" OnClick="DocActLinkButton_Click">1. Work Schedule</asp:LinkButton>
    <br /><br />
    <asp:LinkButton ID="DocEmpLinkButton" runat="server" CssClass="link" OnClick="DocEmpLinkButton_Click">2. Doctor's Employment history</asp:LinkButton>
    <br /><br />
    <asp:LinkButton ID="StaffEmpLinkButton" runat="server" CssClass="link" OnClick="StaffEmpLinkButton_Click">3. Staff's Employment history</asp:LinkButton>
    <br /><br />

    <style>.link{font-size:25px;}</style>
</asp:Content>
