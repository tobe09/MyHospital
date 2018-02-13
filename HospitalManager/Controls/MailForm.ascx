<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailForm.ascx.cs" Inherits="HospitalManager.MailForm" %>

<div style="border:2px; width:810px; text-align:center; margin-left:140px">
    <table style="width:800px;">
        <tr><td colspan="2">
    <asp:Label ID="SentLabel" Font-Size="X-Large" runat="server" Text=""></asp:Label><br /><br /><br /></td></tr>
        <tr>
            <td> <asp:Label ID="UsernameLabel" runat="server" Text="Enter Your Name/User Id: "></asp:Label></td>
            <td> <asp:TextBox CssClass="TextBox" ID="UsernameBox" runat="server" Width="250px" Height="20px"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /><br /></td></tr>
        <tr>
            <td> <asp:Label ID="EmailLabel" runat="server" Text="Enter Your Email Address: "></asp:Label></td>
            <td> <asp:TextBox CssClass="TextBox" ID="EmailBox" runat="server" Width="250px" Height="20px"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /><br /></td></tr>
        <tr>
            <td> <asp:Label ID="SubjectLabel" runat="server" Text="Enter the Subject: "></asp:Label></td>
            <td> <asp:TextBox ID="SubjectBox" CssClass="TextBox" runat="server" Width="250px" Height="30px"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /><br /></td></tr>
        <tr>
            <td> <asp:Label ID="MessageLabel" runat="server" Text="Enter the Message: "></asp:Label></td>
            <td> <asp:TextBox CssClass="TextBox" ID="MessageBox" runat="server" TextMode="MultiLine" Height="90px" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr><td><br /><br /></td></tr>
        <tr><td colspan="2">
            <br />
       Recieve a Copy? Check if yes&nbsp; <asp:CheckBox ID="CheckBox1" runat="server" /><br /><br /><br />
            </td></tr>
        <tr><td></td></tr>
        <tr>
            <td colspan="2"> <asp:Button ID="MessageButton" CssClass="Button" runat="server" Text="Send" Height="26px" Width="108px" OnClick="MessageButton_Click" />
                <br />
            </td>
        </tr>
    </table><br />
    </div>