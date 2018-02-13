<%@ Page Language="C#" MasterPageFile="~/HospitalMaster2.Master" Title="System User Page" AutoEventWireup="true" CodeBehind="LoggedInPage.aspx.cs" Inherits="HospitalManager.UserPage" %>


<asp:Content ID="PatientSide" runat="server" ContentPlaceHolderID="SideContent">
</asp:Content>
<asp:Content ID="PatientMain" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome " CssClass="success paraNormal"></asp:Label>
    <div style="margin-left: 330px; font-size: 23px; color: blue">
        <asp:Label ID="HistLabel" runat="server" Font-Italic="true" Text="Your History Pane"></asp:Label>
    </div>
    <div runat="server" id="HistoryDiv" class="historyDiv">
        <asp:Table ID="HistoryTable" runat="server" Width="800px" CssClass="centerText" BackColor="#EEEEEE" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" GridLines="Both">
            <asp:TableHeaderRow CssClass="histTableHeader">
                <asp:TableHeaderCell>S/N</asp:TableHeaderCell>
                <asp:TableHeaderCell>Activity</asp:TableHeaderCell>
                <asp:TableHeaderCell>Time Performed</asp:TableHeaderCell>
                <asp:TableHeaderCell>Updater ID</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

    <div class="profileDiv">
        <div class="profilePicDiv">
            <asp:Image ID="ProfilePic" Width="150px" Height="150px" ImageUrl="Images/UploadProfilePicture.PNG" runat="server" BorderColor="Black" />
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:LinkButton ID="showPicUploadButton1" runat="server" Text="Upload/Change" OnClick="showPicUploadButton1_Click" /><br />
        <div id="PicUploadDiv" runat="server" visible="false">
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <br />
            <asp:Button ID="UploadPicButton" runat="server" BackColor="Yellow" Text="Upload" OnClick="UploadPicButton_Click" Width="65px" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="DownloadButton" runat="server" BackColor="LightGreen" Text="Downl..." Width="50px" Height="25px" Font-Size="10px" OnClick="DownloadButton_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="DeletePicButton" runat="server" BackColor="#cc6600" Text="Del..." OnClick="DeletePicButton_Click" Width="45px" Font-Size="12px" />
            <br />
            <div>
                <asp:Label ID="UploadPicLabel" runat="server" Font-Bold="true" Text="Browse picture" CssClass="font"></asp:Label>
                &nbsp;&nbsp
                <asp:Button ID="Button1" runat="server" Text="X" BackColor="Red" ForeColor="White" Width="20px" Height="20px" OnClick="Button1_Click" />
                <b style="font-size: 12px;">CLOSE</b>
            </div>
        </div>
    </div>

    <div style="text-align: center; font-size: 23px; color: blue; margin-top:10px;">
        <asp:Label ID="InfoLabel" runat="server" Font-Italic="true" Text="Information Pane"></asp:Label>
    </div>
    <div runat="server" id="ActivitiesDiv" class="activitiesDiv">
        <div style="overflow: auto; height: 360px; width: 1050px;">
            <asp:Table ID="InformationTable" runat="server" CssClass="centerText" Width="1020px" BackColor="#CCCCEE" BorderWidth="1px" GridLines="Both">
                <asp:TableHeaderRow CssClass="histTableHeader">
                    <asp:TableHeaderCell Width="20px">S/N</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="150px">Topic</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="550px">Information</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="80px">Provider</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="200px">Time of update</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>

    <style>
        .histTableHeader {
            text-decoration: underline;
            font-size: 20px;
        }

        .font {
            font-size: 18px;
        }
    </style>
</asp:Content>
