<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="HospitalManager.Patients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centerText paraNormal"><b>Patient related activities</b></div>
    <br />
    <br />
    <asp:LinkButton ID="PatDisabLinkButton" runat="server" CssClass="link" OnClick="PatDisabLinkButton_Click">1. Patient Disabilities</asp:LinkButton>
    <br /><br />
    
    <style>.link{font-size:25px;}</style>
</asp:Content>
