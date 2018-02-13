<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="PatientDisabilities.aspx.cs" Inherits="HospitalManager.Patient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centerText paraNormal"><b>Patient Issues and Disabilities</b></div>
    <br />
    <div id="SearchTopDiv" runat="server">
        <i>Search (by patient id):&nbsp;&nbsp;&nbsp;&nbsp; </i>
        <asp:TextBox ID="SearchBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="SearchButton" Text="Search" OnClick="SearchButton_Click" runat="server" />
        <span style="margin-left: 466px"><a href="../Patients.aspx">Go back to previous page</a></span>
    </div>
    <br />

    <div id="SearchDiv" runat="server" visible="false">
        <asp:Label ID="SearchNameLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:Label ID="SearchUserIdLabel" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:ListView ID="SearchListView" runat="server" OnSelectedIndexChanged="SearchListView_SelectedIndexChanged">
            <LayoutTemplate>
                <table runat="server" class="listViewSearchTable">
                    <tr class="listViewSearchheader">
                        <td style="width: 50px"><b>Select</b></td>
                        <td style="width: 20px"><b>S/N</b></td>
                        <td style="width: 80px"><b>Organ</b></td>
                        <td style="width: 500px"><b>Description</b></td>
                        <td style="width: 150px"><b>Status</b></td>
                    </tr>
                    <tr runat="server" id="ItemPlaceHolder"></tr>
                    <tr style="height: 30px;">
                        <td colspan="6">
                            <br />
                            <asp:Button ID="SearchModifyButton" runat="server" Text="Modify Status" OnClick="SearchModifyButton_Click" Width="120px" Height="30px" CausesValidation="false"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="SearchDeleteButton" runat="server" OnClick="SearchDeleteButton_Click" Text="Delete" Width="120px" Height="30px" CausesValidation="false" />
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
                        <asp:Label ID="SearchIdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchOrganLabel" runat="server" Text='<%#Eval("ORGAN") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchDescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchStatusLabel" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr class="selItemTemplate">
                    <td>
                        <asp:Label ID="SelectedLabel" runat="server" Text="Selected"></asp:Label></td>
                    <td>
                        <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        <asp:Label ID="SearchIdLabel" runat="server" Text='<%#Eval("CREATE_ID") %>' Visible="false"></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchOrganLabel" runat="server" Text='<%#Eval("ORGAN") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchDescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="SearchStatusLabel" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:Label ID="SearchStatusLabel" runat="server" EnableViewState="false" Font-Size="Large"></asp:Label><br />
        <asp:LinkButton ID="CloseButton" OnClick="CloseButton_Click" runat="server" Font-Size="Large" Text="Close search" CssClass="cancelColor"></asp:LinkButton>
        <br />
        <br />
    </div>
    <div class="centerText">
        <asp:Label ID="StatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
        <br />
    </div>
    <div id="MainDiv" runat="server">
        <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" OnPagePropertiesChanged="ListView1_PagePropertiesChanged">
            <LayoutTemplate>
                <table runat="server" class="listViewMainTable">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="NameLabel" runat="server" Text="All Patients" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr class="listViewMainHeader">
                        <td style="width: 40px"><b>Select</b></td>
                        <td style="width: 20px"><b>S/N</b></td>
                        <td style="width: 80px"><b>User Id</b></td>
                        <td style="width: 80px"><b>Organ</b></td>
                        <td style="width: 500px"><b>Description</b></td>
                        <td style="width: 150px"><b>Status</b></td>
                    </tr>
                    <tr runat="server" id="ItemPlaceHolder"></tr>
                    <tr>
                        <td>
                            <asp:Button ID="CancelButton" OnClick="CancelButton_Click" runat="server" Text="Cancel" CssClass="cancelColor" /></td>
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
                            <asp:Button ID="ModifyButton" runat="server" Text="Modify Status" OnClick="ModifyButton_Click" Width="120px" Height="35px" CausesValidation="false"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" Text="Delete" Width="120px" Height="35px" CausesValidation="false"/>
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
                        <asp:Label ID="OrganLabel" runat="server" Text='<%#Eval("ORGAN") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="StatusLabel" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label></td>
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
                        <asp:Label ID="OrganLabel" runat="server" Text='<%#Eval("ORGAN") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="DescLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    <td>
                        <asp:Label ID="StatusLabel" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <br />

        <asp:Button ID="AddButton" runat="server" CssClass="addColor" Text="Add" Width="100px" Height="35px" OnClick="AddButton_Click" />
        <br />

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
                    <td>Organ </td>
                    <td>&nbsp;&nbsp;
                    <asp:DropDownList ID="DivOrganList" runat="server" CssClass="textBox" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="DivOrganList_SelectedIndexChanged">
                        <asp:ListItem>Please select...</asp:ListItem>
                        <asp:ListItem>Ear</asp:ListItem>
                        <asp:ListItem>Eye</asp:ListItem>
                        <asp:ListItem>Hand</asp:ListItem>
                        <asp:ListItem>Heart</asp:ListItem>
                        <asp:ListItem>Kidney</asp:ListItem>
                        <asp:ListItem>Leg</asp:ListItem>
                        <asp:ListItem>Lung</asp:ListItem>
                        <asp:ListItem>Mouth</asp:ListItem>
                        <asp:ListItem>Nose</asp:ListItem>
                        <asp:ListItem>Other Organ</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="OtherOrganBox" runat="server" Width="200px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Description of issue</td>
                    <td>
                        <asp:TextBox ID="DivDescBox" runat="server" TextMode="MultiLine" CssClass="TextBox" Width="200px" Height="40px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Status</td>
                    <td>
                        <asp:DropDownList ID="DivStatusList" runat="server" Width="200px" CssClass="textBox">
                            <asp:ListItem>Please select...</asp:ListItem>
                            <asp:ListItem>Being Treated</asp:ListItem>
                            <asp:ListItem>Treated</asp:ListItem>
                            <asp:ListItem>Untreated</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br /><asp:Button ID="ModifyDivButton" runat="server" Text="Enforce Modify..." OnClick="ModifyDivButton_Click" Width="150px" Height="40px" />
                        <asp:Button ID="DivAddButton" runat="server" Text="Enforce Add..." Width="150px" Height="40px" OnClick="DivAddButton_Click" />
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
