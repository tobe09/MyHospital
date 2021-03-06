﻿<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.Master" AutoEventWireup="true" CodeBehind="RelDeptWardPage.aspx.cs" Inherits="HospitalManager.DeptWardRelPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="font-size: 23px; text-align: center;"><b>Relate Departments to Wards</b></div>
    <br />
    <div>
        
        <div class="centerText">
            <asp:Label ID="StatusLabel" runat="server" Text="" Font-Size="25px" EnableViewState="false"></asp:Label><br />
        </div>
        <asp:Label ID="DeptMessageLabel" runat="server" Text="" Font-Italic="true" Font-Size="18px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CancelButton" OnClick="CancelButton_Click" runat="server" Text="Cancel" Width="80px" Height="30px" CssClass="cancelColor" /><br />
        <asp:DropDownList ID="DeptList" Width="150px" CssClass="TextBox" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="DeptSelectButton" runat="server" OnClick="DeptSelectButton_Click" Width="60px" Text="Select" />&nbsp;&nbsp;
        <br />
        <br />
        <asp:Table ID="DeptTable" runat="server" CssClass="table">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3" CssClass="paraNormal">Selected Department</asp:TableCell></asp:TableRow>
            <asp:TableHeaderRow CssClass="aspTableHeader">
                <asp:TableHeaderCell><b>Department Id</b></asp:TableHeaderCell>
                <asp:TableHeaderCell><b>Department Name</b></asp:TableHeaderCell>
                <asp:TableHeaderCell><b>Description</b></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="DeptIdLabel" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="DeptNameLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="DeptDescLabel" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />

        <div id="wardDiv" runat="server">
            <asp:ListView ID="ListView1" runat="server">
                <LayoutTemplate>
                    <table runat="server" style="text-align: center; width: 1000px; font-size: 15px; background-color: #f5f8fe">
                        <tr>
                            <td colspan="5" style="font-size: 22px;">Wards to relate/add</td>
                        </tr>
                        <tr style="background-color: #a6c4ee; height: 50px; font-size: 18px">
                            <td style="width: 50px"><b>Select</b></td>
                            <td style="width: 20px"><b>S/N</b></td>
                            <td style="width: 70px"><b>Ward ID</b></td>
                            <td style="width: 200px"><b>Ward Name</b></td>
                            <td style="width: 660px"><b>Description</b></td>
                        </tr>
                        <tr runat="server" id="ItemPlaceHolder"></tr>
                        <tr>
                            <td>
                                <asp:Button ID="RemoveButton" OnClick="RemoveButton_Click" runat="server" CssClass="removeColor" Text="Remove" /></td>
                        </tr>
                        <tr style="height: 80px">
                            <td colspan="5">
                                <asp:Button ID="RelateButton" runat="server" OnClick="RelateButton_Click" CssClass="addColor" Text="Relate" Width="100px" Height="40px" />
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="itemTemplate">
                        <td>
                            <asp:LinkButton ID="SelectLinkButton" runat="server" CommandName="Select" Text="--->"></asp:LinkButton></td>
                        <td>
                            <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardIdLabel" runat="server" Visible="true" Text='<%#Eval("WARD_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardNameLabel" runat="server" Text='<%#Eval("WARD_NAME") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardDescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="altItemTemplate">
                        <td>
                            <asp:LinkButton ID="SelectLinkButton" runat="server" CommandName="Select" Text="--->"></asp:LinkButton></td>
                        <td>
                            <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardIdLabel" runat="server" Visible="true" Text='<%#Eval("WARD_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardNameLabel" runat="server" Text='<%#Eval("WARD_NAME") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardDescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                    </tr>
                </AlternatingItemTemplate>
                <SelectedItemTemplate>
                    <tr class="selItemTemplate">
                        <td>
                            <asp:Label ID="SelectedLabel" runat="server" Text="Selected"></asp:Label></td>
                        <td>
                            <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardIdLabel" runat="server" Visible="true" Text='<%#Eval("WARD_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardNameLabel" runat="server" Text='<%#Eval("WARD_NAME") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="WardDescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>

            <i style="font-size: 18px">Add ward(s) by name</i><br />
            <br />
            <asp:DropDownList ID="WardList" Width="150px" CssClass="TextBox" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Button ID="WardAddButton" OnClick="WardAddButton_Click" runat="server" Width="60px" Text="Add" /><br />
        </div><br />
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

</asp:Content>
