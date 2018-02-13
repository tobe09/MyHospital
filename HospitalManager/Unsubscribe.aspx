<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.Master" AutoEventWireup="true" CodeBehind="Unsubscribe.aspx.cs" Inherits="HospitalManager.Unsubscribe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; font-size: 20px">
        <b style="font-size: 30px">Unsubscription<br />
            <br />
        </b>
        <table style="width: 1000px; margin-left: 25px;">
            <tr>
                <td colspan="2" style="font-size: 23px;">Please fill this form<br />
                    <i style="font-size: 17px;">Note: Execute action after due consideration</i>
                </td>
            </tr>
            <tr>
                <td style="width: 336px" colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="StatusLabel" runat="server" Font-Size="25px" Text="" EnableViewState="false"></asp:Label><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 336px">User Id</td>
                <td>
                    <asp:TextBox ID="UserIdBox" runat="server" Width="250px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 336px">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 336px">Reason for leaving</td>
                <td>
                    <asp:TextBox ID="ReasonBox" CssClass="textBox" TextMode="MultiLine" Width="250px" Height="90px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:CheckBox ID="UnsubscribeCheckBox" runat="server" Text="Check to enforce unsubscription" Font-Size="14px" /></td>
            </tr>
            <tr>
                <td style="width: 336px">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" Width="150px" Height="40px" OnClick="SubmitButton_Click" /></td>
            </tr>
            <tr>
                <td style="width: 336px">
                    <br />
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
