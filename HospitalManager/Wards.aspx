<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="Wards.aspx.cs" Inherits="HospitalManager.Wards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="centerText">
                <b style="font-size: 24px">Ward Tasks</b>
                <div id="SortDiv">
                    &nbsp; <asp:Button ID="SortDivButton" runat="server" CausesValidation="false" BackColor="#faeede" Text="Sort: " Font-Bold="false" OnClick="SortDivButton_Click"/> &nbsp;&nbsp;
                    <asp:RadioButton ID="AlphabeticRadioButton" GroupName="FirstSort" Text="Alphabetically" runat="server" AutoPostBack="true" OnCheckedChanged="Sorting" />
                    &nbsp; or&nbsp;
                    <asp:RadioButton ID="DateRadioButton" GroupName="FirstSort" Text="By date created" runat="server" AutoPostBack="true" OnCheckedChanged="Sorting" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <span id="AscDescSpan" runat="server" visible="false">=>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="AscRadioButton" GroupName="SecondSort" AutoPostBack="true" Text="Ascending" runat="server" />
            &nbsp; or&nbsp;
            <asp:RadioButton ID="DescRadioButton" GroupName="SecondSort" AutoPostBack="true" Text="Descending" runat="server" />
        </span>
                </div>
            </div>
            <br />
            <div class="centerText">
                <asp:Label ID="StatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label></div>
            <br />
            <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" OnPagePropertiesChanged="ListView1_PagePropertiesChanged">
                <LayoutTemplate>
                    <table runat="server" style="text-align: center; width: 1000px; font-size: 15px; background-color: #f5f8fe">
                        <tr style="background-color: #64b9f8; height: 50px; font-size: 20px">
                            <td style="width: 50px"><b>Select</b></td>
                            <td style="width: 20px"><b>S/N</b></td>
                            <td style="width: 70px"><b>Ward ID</b></td>
                            <td style="width: 180px"><b>Name</b></td>
                            <td style="width: 600px"><b>Description</b></td>
                            <td style="width: 80px"><b>Department</b></td>
                        </tr>
                        <tr runat="server" id="ItemPlaceHolder"></tr>
                        <tr>
                            <td>
                                <asp:Button ID="CancelButton" OnClick="CancelButton_Click" runat="server" Text="Cancel" /></td>
                        </tr>
                        <tr style="height: 20px; font-size: 20px;">
                            <td colspan="6">
                                <asp:DataPager ID="DataPager1" PagedControlID="ListView1" runat="server" PageSize="10">
                                    <Fields>
                                        <asp:NumericPagerField ButtonCount="1" PreviousPageText="Prev" NextPageText="Next" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                        <tr style="height: 80px">
                            <td colspan="6">
                                <asp:Button ID="ModifyButton" runat="server" OnClick="ModifyButton_Click" Text="Modify" Width="120px" Height="30px" />
                                &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" Text="Delete" Width="120px" Height="30px" />
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
                        <td>
                            <asp:Label ID="WardDeptLabel" runat="server" Text='<%#Eval("PARENT_DEPT") %>'></asp:Label></td>
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
                        <td>
                            <asp:Label ID="WardDeptLabel" runat="server" Text='<%#Eval("PARENT_DEPT") %>'></asp:Label></td>
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
                        <td>
                            <asp:Label ID="WardDeptLabel" runat="server" Text='<%#Eval("PARENT_DEPT") %>'></asp:Label></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <br />
            <asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add" Width="100px" Height="30px" />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="addDelDiv" runat="server" visible="false" class="addDelDiv">
        <table style="text-align: center; width: 700px; font-size: 13px;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="DivHeaderLabel" runat="server" Font-Italic="true" Font-Size="20px"></asp:Label></td>
            </tr>
            <tr>
                <td>Ward id </td>
                <td>
                    <asp:TextBox ID="DivIdBox" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Ward name </td>
                <td>
                    <asp:TextBox ID="DivNameBox" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Ward description </td>
                <td>
                    <asp:TextBox ID="DivDescBox" runat="server" TextMode="MultiLine" CssClass="TextBox" Width="200px" Height="40px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Button ID="DivAddButton" OnClick="DivAddButton_Click" runat="server" Text="Enforce Add..." Width="150px" Height="35px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="DivModifyButton" runat="server" OnClick="DivModifyButton_Click" Text="Enforce Mod..." Width="150px" Height="35px" /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DivStatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <br />


    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

</asp:Content>
