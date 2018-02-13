<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalMaster2.master" AutoEventWireup="true" CodeBehind="DeleteInformationPage.aspx.cs" Inherits="HospitalManager.DeleteInformationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="centerText">
                <b style="font-size: 24px">Delete Information</b>
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
                <asp:Label ID="TopicDelLabel" runat="server"></asp:Label>
            </div>
            <br />
            <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" OnPagePropertiesChanged="ListView1_PagePropertiesChanged">
                <LayoutTemplate>
                    <table style="text-align: center; width: 1000px; font-size: 15px;">
                        <tr style="background-color: #2dbb64; height: 50px; font-size: 20px">
                            <td style="width: 50px"><b>Select</b></td>
                            <td style="width: 20px"><b>S/N</b></td>
                            <td style="width: 80px"><b>Topic</b></td>
                            <td style="width: 600px"><b>Information</b></td>
                            <td style="width: 100px"><b>Sender</b></td>
                            <td style="width: 150px"><b>Recipient</b></td>
                        </tr>
                        <tr runat="server" id="ItemPlaceHolder"></tr>
                        <tr>
                            <td>
                                <asp:Button ID="CancelButton" OnClick="CancelButton_Click" runat="server" Text="Cancel" CssClass="standardColor" /></td>
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
                                <asp:Button ID="ModifyButton" OnClick="ModifyButton_Click" runat="server" Text="Modify" Width="120px" Height="40px" CssClass="standardColor" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="DeleteButton" OnClick="DeleteButton_Click" runat="server" Text="Delete" Width="120px" Height="40px" CssClass="standardColor" /></td>
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
                            <asp:Label ID="TopicLabel" runat="server" Text='<%#Eval("TOPIC") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="InformationLabel" runat="server" Text='<%#Eval("INFO") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="SenderLabel" runat="server" Text='<%#Eval("USER_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="RecipientLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="altItemTemplate">
                        <td>
                            <asp:LinkButton ID="SelectLinkButton" runat="server" CommandName="Select" Text="--->"></asp:LinkButton></td>
                        <td>
                            <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="TopicLabel" runat="server" Text='<%#Eval("TOPIC") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="InformationLabel" runat="server" Text='<%#Eval("INFO") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="SenderLabel" runat="server" Text='<%#Eval("USER_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="RecipientLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                </AlternatingItemTemplate>
                <SelectedItemTemplate>
                    <tr class="selItemTemplate">
                        <td>
                            <asp:Label ID="SelectedLabel" runat="server" Text="Selected"></asp:Label></td>
                        <td>
                            <asp:Label ID="SerialNoLabel" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="TopicLabel" runat="server" Text='<%#Eval("TOPIC") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="InformationLabel" runat="server" Text='<%#Eval("INFO") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="SenderLabel" runat="server" Text='<%#Eval("USER_ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="RecipientLabel" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="addDelDiv" runat="server" visible="false" class="addDelDiv">
        <table style="text-align: center; width: 700px; font-size: 13px;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="DivHeaderLabel" runat="server" Font-Italic="true" Font-Size="20px" Text="Enter Changes"></asp:Label></td>
            </tr>
            <tr>
                <td>User Id </td>
                <td>
                    <asp:TextBox ID="DivIdBox" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Topic </td>
                <td>
                    <asp:TextBox ID="DivTopicBox" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Information </td>
                <td>
                    <asp:TextBox ID="DivDescBox" runat="server" TextMode="MultiLine" CssClass="TextBox" Width="200px" Height="40px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Recipient</td>
                <td>
                    <asp:DropDownList ID="RecipientList" CssClass="TextBox" Width="200px" runat="server">
                        <asp:ListItem>Please select...</asp:ListItem>
                        <asp:ListItem Value="ALL">All users</asp:ListItem>
                        <asp:ListItem Value="DC">All doctors</asp:ListItem>
                        <asp:ListItem Value="ST">All staff</asp:ListItem>
                        <asp:ListItem Value="DCST">All doctors and staff</asp:ListItem>
                        <asp:ListItem Value="PAT">All patients</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Button ID="DivModifyButton" runat="server" OnClick="DivModifyButton_Click" Text="Enforce Modify" Width="150px" Height="35px" /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DivStatusLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <br />

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

</asp:Content>
