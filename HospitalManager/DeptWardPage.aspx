<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.Master" AutoEventWireup="true" CodeBehind="DeptWardPage.aspx.cs" Inherits="HospitalManager.DeptWardPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="infoDiv">
        <table>
            <tr>
                <td colspan="2">
                    <h2>Add Departments</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Enter department name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DeptAddNameBox" runat="server" CssClass="text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Enter department description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DeptAddDescBox" runat="server" CssClass="text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="DeptAddButton" runat="server" Text="Add" Width="100px" OnClick="DeptAddButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="DeptAddLabel" runat="server" Font-Size="20px"></asp:Label></td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div class="infoDiv">
        <table>
            <tr>
                <td colspan="2">
                    <h2>Add Wards</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Enter ward name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="WardAddNameBox" runat="server" CssClass="text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="WardAddButton" runat="server" Text="Add" Width="100px" OnClick="WardAddButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="WardAddLabel" runat="server" Font-Size="20px"></asp:Label></td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div class="infoDiv">
        <table>
            <tr>
                <td colspan="2">
                    <h2>Delete Departments</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DeptDelList" runat="server" CssClass="TextBox text" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="DeptDelCheckBox" runat="server" Text="Check to enforce deletion" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="DeptDelDescLabel" runat="server" Text="" Font-Size="18px"></asp:Label></td>
            </tr>
            <tr>
                <td><i>Note: Only unlinked depts can be deleted. This operation cannot be undone</i></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="DeptDelButton" runat="server" Text="Delete" Width="100px" OnClick="DeptDelButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="DeptDelLabel" runat="server" Font-Size="20px"></asp:Label></td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div class="infoDiv">
        <table>
            <tr>
                <td colspan="2">
                    <h2>Delete Wards</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="WardDelList" runat="server" CssClass="TextBox text" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="WardDelCheckBox" runat="server" Text="Check to enforce deletion" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="WardDelDescLabel" runat="server" Text="" Font-Size="18px"></asp:Label></td>
            </tr>
            <tr>
                <td><i>Note: Only unlinked wards can be deleted. This operation cannot be undone</i></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="WardDelButton" runat="server" Text="Delete" Width="100px" OnClick="WardDelButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="WardDelLabel" runat="server" Font-Size="20px"></asp:Label></td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <style>
        table {
            width: 700px;
            text-align: center;
            margin-left: 200px;
        }

        .text {
            width: 200px;
        }

        .infoDiv {
            border: 2px solid;
            border-color: gray;
        }
    </style>
</asp:Content>
