<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.Master" AutoEventWireup="true" CodeBehind="AddInformationPage.aspx.cs" Inherits="HospitalManager.InformationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="InfoTable" runat="server" Width="700px" Height="500px" Font-Size="20px">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="3"><b style="font-size:25px;">Enter Information Details</b></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                <asp:ValidationSummary Style="margin-left: 50px;" ID="ValidationSummary1" runat="server" CssClass="error normal" HeaderText="Please correct the following errors: " />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="StatusLabel" Font-Size="20px" runat="server" Text=""></asp:Label><br />
            </asp:TableCell>
        </asp:TableFooterRow>
        <asp:TableRow>
            <asp:TableCell Width="500px">
                User Id
                <br />
                <i style="font-size: 12px;">Tick for external user</i>
                <asp:CheckBox ID="ExtUserCheckBox" AutoPostBack="true" runat="server" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="UserIdBox" CssClass="width" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="50px"> 
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Topic </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TopicBox" CssClass="width" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="error" ControlToValidate="TopicBox" Text="*" ErrorMessage="Topic cannot be empty"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="error" ControlToValidate="TopicBox" Text="*" ErrorMessage="Enter a valid topic" ValidationExpression="[\w\W]{2,}"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Information </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="InfoBox" TextMode="MultiLine" CssClass="TextBox textArea" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="error" ControlToValidate="InfoBox" Text="*" ErrorMessage="Information cannot be empty"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="error" ControlToValidate="InfoBox" Text="*" ErrorMessage="Enter valid information" ValidationExpression="[\w\W]{4,}"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Recipient </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="RecipientList" CssClass="TextBox width" runat="server">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem Value="ALL">All users</asp:ListItem>
                    <asp:ListItem Value="DC">All doctors</asp:ListItem>
                    <asp:ListItem Value="ST">All staff</asp:ListItem>
                    <asp:ListItem Value="DCST">All doctors and staff</asp:ListItem>
                    <asp:ListItem Value="PAT">All patients</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Width="50px"> </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                <asp:Button ID="SubmitButton" Width="150px" Height="40px" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <style>
        table {
            text-align: center;
            margin-left: 150px;
        }

        .width {
            width: 200px;
        }

        .textArea {
            width: 250px;
            height: 50px;
        }
    </style>
</asp:Content>
