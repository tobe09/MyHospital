<%@ Page Language="C#" MasterPageFile="~/HospitalMaster.Master" Title="Login" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HospitalManager.Login1" %>

<asp:Content ContentPlaceHolderID="ContentPH" ID="LoginContent" runat="server">
    <div style="margin-left: 400px;">
        <br />
        <div style="margin-left: 170px" class="heading big">Login Page</div>
        <br />
        <br />
        <table style="width: 493px; background-color: #b9f1c4">
            <tr>
                <td colspan="2" style="text-align: center; font-size: 25px; color: #ad24e4;">Enter your login details</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="UsernameLabel" Font-Italic="true" Font-Size="18px" runat="server" Text="User ID/Email/First Name "></asp:Label>
                </td>
                <td>
                    <asp:TextBox CssClass="textBox" ID="UserIdBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                    <asp:Label ID="PasswordLabel" Font-Italic="true" Font-Size="18px" runat="server" Text="Password "></asp:Label>
                </td>
                <td style="height: 26px">
                    <asp:TextBox CssClass="textBox" ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td><a href="Register.aspx" style="color: blue; margin-right: 5px;" class="goto">Click to register</a></td>
                <td>
                    <asp:Button CssClass="button" ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" Height="34px" Width="132px" />
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="StatusLabel" runat="server" Text="" CssClass="error normal"></asp:Label>
    </div>
</asp:Content>
