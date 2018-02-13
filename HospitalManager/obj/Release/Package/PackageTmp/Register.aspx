<%@ Page Language="C#" Title="Registration Page" MasterPageFile="~/HospitalMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HospitalManager.Registration" %>

<asp:Content ContentPlaceHolderID="ContentPH" ID="RegisterContent" runat="server">
    <br /><div style="text-align:center; font-size:26px">Registration Page</div>
    <div style="margin-left:400px;"><br /><br />
        <table id="InfoTable"  runat="server">
        <tr><td colspan="3" class="heading">Personal Information</td></tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="FirstNameLabel" runat="server" Text="First Name"></asp:Label> </td>
            <td> <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox> </td>
            <td style="width: 10px"> <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your name" ControlToValidate="FirstNameBox">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="SurnameLabel" runat="server" Text="Surname"></asp:Label> </td>
            <td> <asp:TextBox ID="SurnameBox" runat="server"></asp:TextBox> </td>
            <td style="width: 10px"><asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your surname" ControlToValidate="SurnameBox">*</asp:RequiredFieldValidator> </td>
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="OtherNameLabel" runat="server" Text="Other Name(s)"></asp:Label> </td>
            <td> <asp:TextBox ID="OtherNameBox" runat="server"></asp:TextBox> </td>
            <td style="width: 10px"></td>
        </tr>
        <tr>
            <td style="width: 299px"><asp:Label ID="UsernameLabel" runat="server" Text="Choose your username"></asp:Label> </td>
            <td><asp:TextBox ID="UsernameBox" runat="server"></asp:TextBox> </td>
            <td style="width: 10px"><asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your username" ControlToValidate="UsernameBox">*</asp:RequiredFieldValidator> </td>
        </tr>
            <tr>
                <td style="width: 299px; height: 26px;">Password</td>
                <td style="height: 26px"> <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox></td>
                <td style="width: 10px; height: 26px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="error" ErrorMessage="Enter your password" ControlToValidate="PasswordBox" Text="*"></asp:RequiredFieldValidator> </td>
            </tr>
            <tr>
                <td style="width: 299px">Confirm Password</td>
                <td> <asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password"></asp:TextBox></td>
                <td style="width: 10px"><asp:CompareValidator CssClass="error" ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch" ControlToCompare="PasswordBox" ControlToValidate="ConfirmPasswordBox">*</asp:CompareValidator></td>
            </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="DobLabel" runat="server" Text="Date of Birth"></asp:Label> </td>
            <td> <asp:TextBox ID="DobBox" runat="server"></asp:TextBox></td>
            <td style="width: 10px"> </td>        
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label></td>
            <td> <asp:DropDownList ID="GenderList" runat="server">
                <asp:ListItem>Male</asp:ListItem><asp:ListItem>Female</asp:ListItem>
                 </asp:DropDownList></td>
            <td style="width: 10px"> </td>
        </tr>
        <tr><td colspan="3" class="heading">Contact Information</td></tr>
        <tr>
            <td style="width: 299px"><asp:Label ID="PhoneLabel" runat="server" Text="Phone Number"></asp:Label> </td>
            <td> <asp:TextBox ID="PhoneBox" runat="server" TextMode="SingleLine"></asp:TextBox></td>
            <td style="width: 10px"> <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter your phone number" ControlToValidate="PhoneBox">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 299px"><asp:Label ID="EmailLabel" runat="server" Text="Email Address"></asp:Label></td>
            <td> <asp:TextBox ID="EmailBox" runat="server" TextMode="Email"></asp:TextBox></td>
            <td style="width: 10px">
                <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter your email address" ControlToValidate="EmailBox">*</asp:RequiredFieldValidator> </td>
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="CountryLabel" runat="server" Text="Country"></asp:Label></td>
            <td> <asp:TextBox ID="CountryBox" runat="server"></asp:TextBox></td>
            <td style="width: 10px"> </td>
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="StateLabel" runat="server" Text="State of Origin"></asp:Label></td>
            <td> <asp:TextBox ID="StateBox" runat="server"></asp:TextBox></td>
            <td style="width: 10px"> </td>
        </tr>
        <tr>
            <td style="width: 299px"> <asp:Label ID="LocationLabel" runat="server" Text="Location"></asp:Label></td>
            <td> <asp:TextBox ID="LocationBox" runat="server"></asp:TextBox> </td>
            <td style="width: 10px">
                <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter your location" ControlToValidate="LocationBox">*</asp:RequiredFieldValidator> </td>
        </tr>
        <tr><td colspan="3" class="heading">Biological Information</td></tr>
        <tr>
            <td style="width: 299px"><asp:Label ID="GroupLabel" runat="server" Text="Blood Group"></asp:Label></td>
            <td> <asp:DropDownList ID="GroupList" runat="server">
                <asp:ListItem>A</asp:ListItem> <asp:ListItem>B</asp:ListItem>
                <asp:ListItem>AB</asp:ListItem> <asp:ListItem>O</asp:ListItem>
                 </asp:DropDownList></td>
            <td style="width: 10px"> </td>
        </tr>
        <tr>
            <td style="width: 299px"><asp:Label ID="TypeLabel" runat="server" Text="BloodType"></asp:Label></td>
            <td><asp:DropDownList ID="TypeList" runat="server">
                <asp:ListItem>AA</asp:ListItem><asp:ListItem>AS</asp:ListItem><asp:ListItem>SS</asp:ListItem>
                </asp:DropDownList> </td>
            <td style="width: 10px"> </td>
        </tr>
        <tr><td style="width: 299px"><br /><br /></td></tr>
        <tr>
            <td style="width: 299px"></td>
            <td> <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" /></td>
            <td style="width: 10px"> </td>
        </tr>
        </table>
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" HeaderText="Please correct rhe following errors: " />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label CssClass="success" ID="InfoLabel" runat="server" Text="" Visible="false"></asp:Label>
    </div>
</asp:Content>