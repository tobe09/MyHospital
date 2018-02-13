<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="DoctorStaffHistory.aspx.cs" Inherits="HospitalManager.DoctorsHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centerText paraNormal">
        <b>
            <asp:Label ID="HeadLabel" runat="server" Text="Employment History"></asp:Label></b>
    </div>
    <br />
    <div id="SearchTopDiv" runat="server">
        <i>
            <asp:Label ID="SearchToplabel" runat="server" Text="Search (by id)"></asp:Label>
            :&nbsp;&nbsp;&nbsp;&nbsp; </i>
        <asp:TextBox ID="SearchBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="SearchButton" OnClick="SearchButton_Click" Text="Search" runat="server" CausesValidation="false" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        Choose:&nbsp&nbsp;&nbsp;<asp:DropDownList ID="RoleList" runat="server" CssClass="textBox" Width="100px" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="RoleList_SelectedIndexChanged">
            <asp:ListItem>Doctors</asp:ListItem>
            <asp:ListItem>Staff</asp:ListItem>
        </asp:DropDownList>
        <span style="margin-left: 244px"><a href="../DoctorsStaff.aspx">Go back to previous page</a></span>
    </div>

    <div id="SearchDiv" runat="server" visible="false">
        <asp:Label ID="SearchNameLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:Label ID="SearchUserIdLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:ListView ID="SearchListView" runat="server" OnSelectedIndexChanged="SearchListView_SelectedIndexChanged">
            <LayoutTemplate>
                <table runat="server" class="listViewSearchTable">
                    <tr class="listViewSearchheader">
                        <td style="width: 40px"><b>Select</b></td>
                        <td style="width: 20px"><b>S/N</b></td>
                        <td style="width: 150px"><b>WorkPlace</b></td>
                        <td style="width: 120px"><b>Work Type</b></td>
                        <td style="width: 180px"><b>Position</b></td>
                        <td style="width: 150px"><b>Start Date</b></td>
                        <td style="width: 150px"><b>End_Date</b></td>
                    </tr>
                    <tr runat="server" id="ItemPlaceHolder"></tr>
                    <tr style="height: 30px;">
                        <td colspan="7">
                            <br />
                            <asp:Button ID="SearchModifyButton" runat="server" Text="Modify Status" OnClick="SearchModifyButton_Click" Width="120px" Height="30px" CausesValidation="false"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="SearchDeleteButton" runat="server" OnClick="SearchDeleteButton_Click" CssClass="deleteColor" Text="Delete" Width="120px" Height="30px" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="itemTemplate" style="height: 20px;">
                    <td>
                        <asp:LinkButton ID="SelectLinkButton" runat="server" CommandName="Select" Text="--->"></asp:LinkButton></td>
                    <td>
                        <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        <asp:Label ID="IdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkPlaceLabel" runat="server" Text='<%#Eval("WK_PLC") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkTypeLabel" runat="server" Text='<%#Eval("WK_TYP") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%#Eval("POS") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateStartLabel" runat="server" Text='<%#Eval("START_DATE") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateEndLabel" runat="server" Text='<%#Eval("END_DATE") %>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr class="selItemTemplate" style="height: 30px;">
                    <td>
                        <asp:Label ID="SelectedLabel" runat="server" Text="Selected"></asp:Label></td>
                    <td>
                        <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        <asp:Label ID="IdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkPlaceLabel" runat="server" Text='<%#Eval("WK_PLC") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkTypeLabel" runat="server" Text='<%#Eval("WK_TYP") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%#Eval("POS") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateStartLabel" runat="server" Text='<%#Eval("START_DATE") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateEndLabel" runat="server" Text='<%#Eval("END_DATE") %>'></asp:Label></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:Label ID="SearchStatusLabel" runat="server" EnableViewState="false" Font-Size="Large"></asp:Label><br />
        <asp:LinkButton ID="CloseButton" runat="server" OnClick="CloseButton_Click" Font-Size="Large" Text="Close search" CssClass="cancelColor" CausesValidation="false"></asp:LinkButton>
    </div>
    <br />

    <div class="centerText">
        <asp:Label ID="StatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
        <br />
    </div>
    <div id="MainDiv" runat="server">
        <table style="text-align: center; width: 1000px;">
            <tr>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text="Employees" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" OnPagePropertiesChanged="ListView1_PagePropertiesChanged">
            <LayoutTemplate>
                <table runat="server" class="listViewMainTable">
                    <tr class="listViewMainHeader">
                        <td style="width: 40px"><b>Select</b></td>
                        <td style="width: 20px"><b>S/N</b></td>
                        <td style="width: 80px"><b>User Id</b></td>
                        <td style="width: 150px"><b>WorkPlace</b></td>
                        <td style="width: 120px"><b>Work Type</b></td>
                        <td style="width: 180px"><b>Position</b></td>
                        <td style="width: 150px"><b>Start Date</b></td>
                        <td style="width: 150px"><b>End_Date</b></td>
                    </tr>
                    <tr runat="server" id="ItemPlaceHolder"></tr>
                    <tr>
                        <td>
                            <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" Text="Cancel" CssClass="cancelColor" CausesValidation="false" /></td>
                    </tr>
                    <tr style="height: 20px; font-size: 20px;">
                        <td colspan="8">
                            <asp:DataPager ID="DataPager1" PagedControlID="ListView1" runat="server" PageSize="10">
                                <Fields>
                                    <asp:NumericPagerField ButtonCount="1" PreviousPageText="Prev" NextPageText="Next" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                    <tr style="height: 80px">
                        <td colspan="8">
                            <asp:Button ID="ModifyButton" runat="server" Text="Modify Status" OnClick="ModifyButton_Click" Width="120px" Height="35px" CausesValidation="false" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" CssClass="deleteColor" Text="Delete" Width="120px" Height="35px" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="itemTemplate">
                    <td>
                        <asp:LinkButton ID="SelectLinkButton" runat="server" CommandName="Select" Text="--->"></asp:LinkButton></td>
                    <td>
                        <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        <asp:Label ID="IdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="UserIdLabel" runat="server" Text='<%#Eval("USER_ID") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkPlaceLabel" runat="server" Text='<%#Eval("WK_PLC") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkTypeLabel" runat="server" Text='<%#Eval("WK_TYP") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%#Eval("POS") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateStartLabel" runat="server" Text='<%#Eval("START_DATE") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateEndLabel" runat="server" Text='<%#Eval("END_DATE") %>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr class="selItemTemplate">
                    <td>
                        <asp:Label ID="SelectedLabel" runat="server" Text="Selected"></asp:Label></td>
                    <td>
                        <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        <asp:Label ID="IdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="UserIdLabel" runat="server" Text='<%#Eval("USER_ID") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkPlaceLabel" runat="server" Text='<%#Eval("WK_PLC") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="WorkTypeLabel" runat="server" Text='<%#Eval("WK_TYP") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%#Eval("POS") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateStartLabel" runat="server" Text='<%#Eval("START_DATE") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DateEndLabel" runat="server" Text='<%#Eval("END_DATE") %>'></asp:Label></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <br />
        <asp:Button ID="AddButton" runat="server" CssClass="addColor" Text="Add" Width="100px" Height="35px" CausesValidation="false" OnClick="AddButton_Click" />

        <div id="addDelDiv" runat="server" visible="false" class="addDelDiv">
            <table style="text-align: center; width: 700px; font-size: 15px;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="DivHeaderLabel" runat="server" Font-Italic="true" Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td>User Id</td>
                    <td>
                        <asp:TextBox ID="DivUserIdBox" ReadOnly="true" runat="server" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Work place </td>
                    <td>
                        <asp:TextBox ID="DivWorkPlaceBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Work Type </td>
                    <td>
                        <asp:TextBox ID="DivWorkTypeBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Position </td>
                    <td>
                        <asp:TextBox ID="DivPositionBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Date started </td>
                    <td>
                        <asp:TextBox ID="DivStartDateBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Date ended </td>
                    <td>
                        <asp:TextBox ID="DivEndDateBox" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br /><asp:Button ID="ModifyDivButton" runat="server" Text="Enforce Modify..." OnClick="ModifyDivButton_Click" Width="150px" Height="40px" />
                        <asp:Button ID="DivAddButton" runat="server" OnClick="DivAddButton_Click" Text="Enforce Add..." Width="150px" Height="40px" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="DivStatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
        <br />

    </div>
    
    <asp:Label ID="HoldLabel" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="TempLabel" runat="server" Visible="false"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>

</asp:Content>
