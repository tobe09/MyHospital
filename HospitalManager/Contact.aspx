<%@ Page Language="C#" MasterPageFile="~/HospitalMaster.Master" Title="Contact Form" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HospitalManager.Contact_Form" %>

<asp:Content ID="ContactContent" ContentPlaceHolderID="ContentPH" runat="server">
    <div style="margin: 100px; margin-top:35px;">
        <uc1:MailForm ID="Mail" runat="server"></uc1:MailForm>
    </div>
</asp:Content>
