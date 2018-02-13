<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HospitalManager.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td colspan="3"><b style="font-size: 20px;">Modify your password<br />
            </b></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ValidationSummary Style="margin-left: 50px;" ID="ValidationSummary1" runat="server" CssClass="error normal" HeaderText="Please correct the following errors: " />
            </td>
        </tr>
        <tr>
            <td>Old Password </td>
            <td>
                <asp:TextBox ID="OldPasswordBox" TextMode="Password" runat="server" CssClass="TextBox length"></asp:TextBox>
            </td>
            <td style="width: 50px;">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="OldPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Old password must be between 4 and 32 characters" Text="*" ValidationExpression="^[\w\W]{4,32}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="OldPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Old password cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>New Password </td>
            <td>
                <asp:TextBox ID="NewPasswordBox" TextMode="Password" runat="server" CssClass="TextBox length"></asp:TextBox>
            </td>
            <td style="width: 50px;">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="New password must be between 4 and 32 characters" Text="*" ValidationExpression="^[\w\W]{4,32}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NewPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="New password cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>New Password </td>
            <td>
                <asp:TextBox ID="ConfirmPasswordBox" TextMode="Password" runat="server" CssClass="TextBox length"></asp:TextBox>
            </td>
            <td style="width: 50px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ConfirmPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Confirm your password" Text="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="NewPasswordBox" ControlToValidate="ConfirmPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Password Mismatch" Text="*"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <asp:Button ID="ChangeButton" OnClick="ChangeButton_Click" runat="server" Text="Change" Width="150px" Height="30px" /></td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="UserStatusLabel" runat="server" Text="" CssClass="error paraNormal"></asp:Label><br />
                <br />
            </td>
        </tr>

        <tr>
            <td colspan="3"><b style="font-size: 20px;">Retrieve your password<br />
            </b></td>
        </tr>
        <tr>
            <td>Enter your email address</td>
            <td>
                <asp:TextBox ID="EmailBox" TextMode="Email" runat="server" CssClass="TextBox length"></asp:TextBox></td>
            <td style="width: 50px;"></td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <asp:Button ID="EmailButton" runat="server" Text="Send" CausesValidation="false" Width="150px" Height="30px" OnClick="EmailButton_Click" /></td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="EmailLabel" runat="server" Text="" CssClass="error paraNormal"></asp:Label></td>
        </tr>
    </table>
    <br />
    <br />
    <br />

    <div id="SuDiv" runat="server" visible="false">
        <table style="height: 100px">
            <tr>
                <td colspan="3"><b style="font-size: 20px;">Get User password<br />
                </b></td>
            </tr>
            <tr>
                <td>Enter your user Id</td>
                <td>
                    <asp:TextBox ID="UserIdBox" runat="server" CssClass="length"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Button ID="getPasswordButton" CausesValidation="false" runat="server" Text="Retrieve" Width="150px" Height="30px" OnClick="getPasswordButton_Click" /></td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <asp:Label ID="PasswordStatusLabel" runat="server" CssClass="error paraNormal" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>

    <style>
        table {
            margin-left: 150px;
            width: 700px;
            text-align: center;
            height: 200px;
        }

        .length {
            width: 200px;
        }
    </style>
</asp:Content>
