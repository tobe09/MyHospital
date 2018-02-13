<%@ Page Language="C#" MasterPageFile="~/HospitalMaster.Master" Title="Pick a Category" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HospitalManager.Register" %>

<asp:Content ContentPlaceHolderID="ContentPH" ID="RegisterContent" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 130px;">
                <div style="margin-left: 300px;" id="MainDiv" runat="server">
                    <div style="margin-left: 60px; font-size: 26px">
                        <br />
                        <asp:Label ID="RegLabel" runat="server" Text="Patient's Registration Page"></asp:Label>
                    </div>
                    <br />
                    <div id="CheckBoxDiv" visible="false" runat="server" style="margin-left: 80px;">
                        <asp:RadioButton ID="DocRadioButton" Text="Doctor" AutoPostBack="true" GroupName="role" runat="server" OnCheckedChanged="DoctorRadioButton_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="PatientRadioButton" Text="Patient" AutoPostBack="true" GroupName="role" runat="server" OnCheckedChanged="PatientRadioButton_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="StfRadioButton" Text="Staff" AutoPostBack="true" GroupName="role" runat="server" OnCheckedChanged="StaffRadioButton_CheckedChanged" />
                    </div>
                </div>
                <br />
                <div id="StatusDiv" runat="server" visible="false" style="margin-left: 280px;">
                        <asp:Label ID="StatusLabel" runat="server" Text="" CssClass="error paraNormal" Visible="false"></asp:Label><br />
                    <a id="goToLogin" class="goto normal" runat="server" visible="false" href="Login.aspx">Go to login page</a>
                </div>

                <div style="margin-left: 300px;" id="InfoDiv" runat="server" visible="false">
                    <table id="PersonalInfo" runat="server" style="width: 600px">
                        <tr>
                            <td colspan="2" class="heading" style="height: 29px">
                                <div style="margin-left: 100px">Personal Information</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px">
                                <asp:Label ID="FirstNameLabel" runat="server" Text="First Name"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:TextBox CssClass="TextBox" ID="FirstNameBox" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FirstNameBox" CssClass="error" Display="Dynamic" ErrorMessage="First name must be between 2 and 32 characters" Text="*" ValidationExpression="^[\w\W]{2,32}$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstNameBox" CssClass="error" Display="Dynamic" ErrorMessage="First name cannot be empty" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 40px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 300px; height: 32px;">
                                <asp:Label ID="LastnameLabel" runat="server" Text="Last name/Surname"></asp:Label>
                            </td>
                            <td style="width: 300px; height: 32px;">
                                <asp:TextBox CssClass="TextBox" ID="LastnameBox" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="LastnameBox" CssClass="error" Display="Dynamic" ErrorMessage="Last name must be between 2 and 32 characters" Text="*" ValidationExpression="^[\w\W]{2,32}$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="LastnameBox" CssClass="error" Display="Dynamic" ErrorMessage="Lastname cannot be empty" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 300px">
                                <asp:Label ID="OtherNameLabel" runat="server" Text="Other Name(s)"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:TextBox CssClass="TextBox" ID="OtherNameBox" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 300px; height: 30px;">Password</td>
                            <td style="width: 300px; height: 30px;">
                                <asp:TextBox CssClass="TextBox" ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="PasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Password must be between 4 and 32 characters" Text="*" ValidationExpression="^[\w\W]{4,32}$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="PasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Password cannot be empty" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="height: 30px"></td>
                        </tr>
                        <tr>
                            <td style="height: 32px; width: 300px;">Confirm Password</td>
                            <td style="height: 32px; width: 300px;">
                                <asp:TextBox CssClass="TextBox" ID="ConfirmPasswordBox" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ConfirmPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Confirm your password" Text="*"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="PasswordBox" ControlToValidate="ConfirmPasswordBox" CssClass="error" Display="Dynamic" ErrorMessage="Password Mismatch" Text="*"></asp:CompareValidator>
                            </td>
                            <td style="height: 32px"></td>
                        </tr>
                        <tr>
                            <td style="width: 300px">
                                <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label></td>
                            <td style="width: 300px">
                                <asp:DropDownList ID="GenderList" BackColor="#ffffcc" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                    <asp:ListItem>Other Gender</asp:ListItem>
                                </asp:DropDownList></td>
                            <td></td>
                        </tr>
                    </table>
                    <br />

                    <table style="width: 600px">
                        <tr>
                            <td colspan="2" class="heading">
                                <div style="margin-left: 100px">Contact Information</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 271px; height: 31px;">
                                <asp:Label ID="PhoneLabel" runat="server" Text="Mobile Number"></asp:Label>
                            </td>
                            <td style="width: 300px; height: 31px;">
                                <asp:TextBox ID="PhoneBox" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="PhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid phone number" Text="*" ValidationExpression="^\+?[\d]{4,16}$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="PhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Phone number cannot be empty" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="height: 31px"></td>
                        </tr>
                        <tr>
                            <td style="width: 271px; height: 32px;">
                                <asp:Label ID="EmailLabel" runat="server" Text="Email Address"></asp:Label></td>
                            <td style="width: 300px; height: 32px;">
                                <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="EmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid email address" Text="*" ValidationExpression="^[(\w)+@(\w)+\.(\w)+$]{5,64}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="EmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Email Address cannot be empty" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="height: 32px">&nbsp;</td>
                        </tr>
                    </table>

                    <table style="width: 600px" runat="server" id="DocRoleTable" visible="false">
                        <tr>
                            <td colspan="2" class="heading" style="height: 29px">
                                <div style="margin-left: 75px">Specialization Information</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 269px; height: 31px;">
                                <asp:Label ID="Label2" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td style="width: 300px; height: 31px;">
                                <asp:DropDownList ID="DocRoleList" BackColor="#ffffcc" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="height: 31px"></td>
                        </tr>
                    </table>

                    <table style="width: 600px" runat="server" id="StfRoleTable" visible="false">
                        <tr>
                            <td colspan="2" class="heading">
                                <div style="margin-left: 90px">Staff Role Information</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 267px; height: 31px;">
                                <asp:Label ID="Label1" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td style="width: 300px; height: 31px;">
                                <asp:DropDownList ID="StfRoleList" BackColor="#ffffcc" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="height: 31px"></td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="margin-left: 125px">
                                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" Height="32px" Width="123px" OnClick="SubmitButton_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:ValidationSummary Style="margin-left: 50px;" ID="ValidationSummary1" runat="server" CssClass="error normal" HeaderText="Please correct the following errors: " />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
