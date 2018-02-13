<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="Auditing.aspx.cs" Inherits="HospitalManager.Auditing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centerText" style="width: 700px; float: left; margin-bottom:10px;">
        <asp:TextBox ID="QueryBox" TextMode="MultiLine" CssClass="textBox" Width="600px" Height="100px" Font-Size="16px" runat="server"></asp:TextBox><br />
        <br />
        <span style="margin-left:30px">Specify an update code (Optional):&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="UpdCodeOverrideBox" runat="server" Text=""></asp:TextBox></span>
        <br />
        <asp:Button ID="ExecuteButton" runat="server" Text="Execute" Width="150px" Height="40px" OnClick="ExecuteButton_Click" /><br />
        <br />
        <div style="margin-left: 575px; width: 130px; position: absolute; margin-bottom: 0px;">
            <asp:LinkButton ID="ShowTableInfoLinkButton" runat="server" Width="120px" OnClick="ShowTableInfoLinkButton_Click" Text="Show DB tables"></asp:LinkButton></div>
        <asp:Label ID="StatusLabel" runat="server" EnableViewState="false" Font-Size="23px"></asp:Label>
        <br />
    </div>

    <div style="float: left; width: 300px; text-align: center;">
        <table>
            <tr>
                <th colspan="2">Audit trail information for action<br />
                </th>
            </tr>
            <tr>
                <td><i>User Id:</i> </td>
                <td>
                    <asp:TextBox ID="UserIdBox" Width="150px" runat="server" Text="To be generated..."></asp:TextBox></td>
            </tr>
            <tr>
                <td><i>Updater Id:</i></td>
                <td>
                    <asp:TextBox ID="UpdaterBox" ReadOnly="true" Width="150px" runat="server" Text="To be generated..."></asp:TextBox></td>
            </tr>
            <tr>
                <td><i>Update code:</i></td>
                <td>
                    <asp:TextBox ID="UpdateCodeBox" ReadOnly="true" Width="150px" runat="server" Text="To be generated..."></asp:TextBox></td>
            </tr>
            <tr>
                <td><i>Date Updated: </i></td>
                <td>
                    <asp:TextBox ID="DateUpdBox" ReadOnly="true" Width="150px" runat="server" Text="To be generated..."></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td><i>Transaction Id: </i></td>
                <td>
                    <asp:TextBox ID="TransBox" ReadOnly="true" Width="150px" runat="server" Text="To be generated..."></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="AuditCheckBox" runat="server" Text="&nbsp;Check to add to audit trail" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="AuditLabel" runat="server" EnableViewState="false"></asp:Label></td>
            </tr>
        </table>

    </div>
    <div id="TableDiv" visible="false" runat="server" style="float: none; text-align: center; overflow: auto; border: 2px solid; border-color: black; width: 1040px; height: 475px">
        <i style="font-size: 20px;">The requested table is shown below </i>
        <br />
        <div style="margin-top: 1px;">
            <asp:Label ID="TableLabel" runat="server"></asp:Label></div>
    </div>
    <br />
    <br />
    <div id="TableInfoDiv" runat="server" visible="false" class="centerText" style="clear: both;">
        <table border="1">
            <tr style="font-size: 20px; height: 30px;">
                <td style="width: 220px;"><b>DB Table Name</b></td>
                <td style="width: 800px;"><b>Description</b></td>
            </tr>
            <tr>
                <td>ADMINISTRATORS </td>
                <td>Holds the basic information of all administrators in the system</td>
            </tr>
            <tr>
                <td>AUDIT_TRAIL</td>
                <td>Holds basic information concerning all activities taking place in the system </td>
            </tr>
            <tr>
                <td>DEPARTMENTS </td>
                <td>Holds registered as well as deleted departments in the database</td>
            </tr>
            <tr>
                <td>DEPT_WARD </td>
                <td>Holds a normalized view of related departments and wards </td>
            </tr>
            <tr>
                <td>DOCTORS </td>
                <td>Holds basic information of all doctors in the system </td>
            </tr>
            <tr>
                <td>GENERAL_INFO </td>
                <td>Holds general information recorded by the administrator or superuser </td>
            </tr>
            <tr>
                <td>IMAGES </td>
                <td>Holds information about users images</td>
            </tr>
            <tr>
                <td>OTHER_CODES</td>
                <td>Holds information  about other codes used on the system </td>
            </tr>
            <tr>
                <td>PAT_DISAB</td>
                <td>Holds information concerning medical issues or disabilities of patients</td>
            </tr>
            <tr>
                <td>PATIENTS </td>
                <td>Holds information about all patients on the system</td>
            </tr>
            <tr>
                <td>ROLES</td>
                <td>Holds information concerning roles that can be assigned to a user </td>
            </tr>
            <tr>
                <td>STAFFS</td>
                <td>Holds information concerning all staffs in the system</td>
            </tr>
            <tr>
                <td>SUPERUSERS</td>
                <td>Holds information about superusers in the system</td>
            </tr>
            <tr>
                <td>UNSUBSCRIBED</td>
                <td>Holds information about all users who have unsubscribed from/left the system</td>
            </tr>
            <tr>
                <td>UPD_CODES </td>
                <td>Holds the update codes and their description</td>
            </tr>
            <tr>
                <td>UPDATES </td>
                <td>Holds identity information related to modification or deletion</td>
            </tr>
            <tr>
                <td>USERS</td>
                <td>Holds normalized information concerning all users in the system</td>
            </tr>
            <tr>
                <td>WARD_TEMP</td>
                <td>Temporarily holds wards before they are linked to a department. Very volatile </td>
            </tr>
            <tr>
                <td>WARDS</td>
                <td>Holds registered as well as deleted wards in the system</td>
            </tr>
        </table>
        <br />
    </div>

    <style>
        #ShowTableInfoLinkButton {
            margin: 0px;
        }
    </style>
</asp:Content>
