<%@ Page Title="User Profile" Language="C#" MasterPageFile="~/HospitalMaster2.Master" AutoEventWireup="true" CodeBehind="UpdateRegistration.aspx.cs" Inherits="HospitalManager.UpdateRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #userProfile {
            background-color: #c3de7e;
        }

        table {
            width: 800px;
            text-align: center;
            margin-left: 150px;
        }

        .left {
            margin-left: 100px;
        }
        .ajax__calendar_container { z-index : 100; } 
    </style>

    <h2 style="text-align: center;">User Profile Page</h2>
    <table>
        <tr>
            <td colspan="3">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="800px" CssClass="error normal" HeaderText="Please correct the following errors: " />
                <asp:Label ID="StatusLabel" runat="server" CssClass="error paraNormal"></asp:Label>
                <br />
                <br />
            </td>
            <td style="width: 50px">
            </td>
        </tr>
        <tr>
            <td colspan="2" class="heading centerText">Personal Information</td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label1" runat="server" Text="User ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="UserIdBox" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label4" runat="server" Text="First Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstNameBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="FirstNameBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid first name" Text="*" ValidationExpression="^[\w|'|\-| ]{2,32}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FirstNameBox" CssClass="error" Display="Dynamic" ErrorMessage="First name cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LastNameBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="LastNameBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid last name" Text="*" ValidationExpression="^[\w|'|\-| ]{2,32}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastNameBox" CssClass="error" Display="Dynamic" ErrorMessage="Last name cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label3" runat="server" Text="Other Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="OtherNameBox" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label5" runat="server" Text="Gender"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="GenderList" runat="server" Width="120px" CssClass="TextBox">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Other Gender</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label6" runat="server" Text="Date of birth (DD/MM/YYYY)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="DobBox" runat="server" Width="200px"></asp:TextBox>
                <ajax:CalendarExtender ID="calExtender" runat="server" TargetControlID="DobBox" Format="dd/mm/yyyy" ClientIDMode="Inherit" DefaultView="Months" FirstDayOfWeek="Sunday" />
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DobBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid date of birth (Format: 25/12/2017 or 25-12-2017)" Text="*" ValidationExpression="^(?:0[1-9]|[12]\d|3[01])([\/.-])(?:0[1-9]|1[012])([\/.-])(?:19|20)\d\d$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DobBox" CssClass="error" Display="Dynamic" ErrorMessage="Date of birth cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label7" runat="server" Text="Marital Status"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="MaritalList" runat="server" Width="120px" CssClass="TextBox">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>Single</asp:ListItem>
                    <asp:ListItem>Married</asp:ListItem>
                    <asp:ListItem>Other Marital Status</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label8" runat="server" Text="Country of origin"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CountryOriBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="CountryOriBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid country of origin" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CountryOriBox" CssClass="error" Display="Dynamic" ErrorMessage="Country of origin cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label9" runat="server" Text="State of origin"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="StateOriBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="StateOriBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid state of origin" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="StateOriBox" CssClass="error" Display="Dynamic" ErrorMessage="State of origin cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label10" runat="server" Text="Local Government Area (LGA) of origin"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LocalOriBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="LocalOriBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid LGA of origin" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="LocalOriBox" CssClass="error" Display="Dynamic" ErrorMessage="LGA of origin cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="heading centerText">Contact Information</td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="PhoneLabel" runat="server" Text="Mobile Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PhoneBox" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="PhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid phone number" Text="*" ValidationExpression="^\+?[\d]{4,16}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="PhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Phone number cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="EmailLabel" runat="server" Text="Email Address"></asp:Label></td>
            <td>
                <asp:TextBox CssClass="TextBox" ID="EmailBox" runat="server" TextMode="Email" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="EmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid email address" Text="*" ValidationExpression="^[(\w)+@(\w)+\.(\w)+]${5,32}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="EmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Email Address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label11" runat="server" Text="Country of residence"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CountryResBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="CountryResBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid country of residence" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="CountryResBox" CssClass="error" Display="Dynamic" ErrorMessage="Country of residence cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label12" runat="server" Text="State of residence"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="StateResBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="StateResBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid state of residence" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="StateResBox" CssClass="error" Display="Dynamic" ErrorMessage="State of residence cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label13" runat="server" Text="Local Government Area (LGA) of residence"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LocalResBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="LocalResBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid LGA of residence" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{2,64}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="LocalResBox" CssClass="error" Display="Dynamic" ErrorMessage="LGA of residence cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label14" runat="server" Text="Home Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="HomeAdrBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="HomeAdrBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid home address" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{3,128}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="HomeAdrBox" CssClass="error" Display="Dynamic" ErrorMessage="Home address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="heading centerText">Identification</td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label15" runat="server" Text="Identification type"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="IdTypeList" runat="server" CssClass="TextBox">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>National Id</asp:ListItem>
                    <asp:ListItem>School Id</asp:ListItem>
                    <asp:ListItem>Voters Card</asp:ListItem>
                    <asp:ListItem>Other Identification type</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label16" runat="server" Text="Identification Card number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="IdNoBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="IdNoBox" CssClass="error" Display="Dynamic" ErrorMessage="ID Card number cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label27" runat="server" Text="School ID number (if applicable)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SchoolIdBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td colspan="3" class="heading centerText">Next of Kin Information</td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label17" runat="server" Text="Full name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NextNameBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="NextNameBox" CssClass="error" Display="Dynamic" ErrorMessage="Next of kin's name cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label21" runat="server" Text="Relationship"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NextRelBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="NextRelBox" CssClass="error" Display="Dynamic" ErrorMessage="Next of kin's relationship cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label18" runat="server" Text="Mobile Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NextPhoneBox" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="NextPhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid phone number for next of kin" Text="*" ValidationExpression="^\+?[\d]{4,16}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="NextPhoneBox" CssClass="error" Display="Dynamic" ErrorMessage="Next of kin's phone number cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label19" runat="server" Text="Email Address"></asp:Label></td>
            <td>
                <asp:TextBox ID="NextEmailBox" CssClass="TextBox" runat="server" TextMode="Email" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="NextEmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid email address for next of kin" Text="*" ValidationExpression="^[(\w)+@(\w)+\.(\w)+$]{5,32}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="NextEmailBox" CssClass="error" Display="Dynamic" ErrorMessage="Next of kin's email Address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label20" runat="server" Text="Residential Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NextAdrBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="NextAdrBox" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid home address for next of kin" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{3,128}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="NextAdrBox" CssClass="error" Display="Dynamic" ErrorMessage="Next of kin's home address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="heading centerText">Biological Information</td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label28" runat="server" Text="Genotype/Blood type"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="TypeList" runat="server" CssClass="TextBox">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>AA</asp:ListItem>
                    <asp:ListItem>AS</asp:ListItem>
                    <asp:ListItem>SS</asp:ListItem>
                    <asp:ListItem>Other genotype</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label29" runat="server" Text="Blood group"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="GroupList" runat="server" CssClass="TextBox">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>AB</asp:ListItem>
                    <asp:ListItem>O</asp:ListItem>
                    <asp:ListItem>Other bloodtype</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 50px"></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="OtherInfoLabel" runat="server" Text="Other Information"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="OtherInfoBox" runat="server" TextMode="MultiLine" Width="250px" CssClass="TextBox" Height="50px"></asp:TextBox></td>
        </tr>
    </table>

    <table runat="server" id="EduRefTable">
        <tr>
            <td colspan="3" class="heading centerText">Educational Information</td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label22" runat="server" Text="Primary School"></asp:Label></td>
            <td>
                <asp:TextBox ID="PriBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <asp:DropDownList ID="PriList" runat="server" CssClass="TextBox" Width="150px">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>First School Leaving Certificate (FSLT)</asp:ListItem>
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Other primary school certificate</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label23" runat="server" Text="Secondary School"></asp:Label></td>
            <td>
                <asp:TextBox ID="SecBox" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <asp:DropDownList ID="SecList" runat="server" CssClass="TextBox" Width="150px">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>WASSCE/WAEC</asp:ListItem>
                    <asp:ListItem>GCE</asp:ListItem>
                    <asp:ListItem>NECO</asp:ListItem>
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Other secondary school certificate</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label24" runat="server" Text="University"></asp:Label></td>
            <td>
                <asp:TextBox ID="UniBox" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 200px;">
                <asp:DropDownList ID="UniList" runat="server" CssClass="TextBox" Width="150px">
                    <asp:ListItem>Please select...</asp:ListItem>
                    <asp:ListItem>Pass (Doctor)</asp:ListItem>
                    <asp:ListItem>First Class</asp:ListItem>
                    <asp:ListItem>Second class, Upper Division</asp:ListItem>
                    <asp:ListItem>Second class, Lower division</asp:ListItem>
                    <asp:ListItem>Third Class</asp:ListItem>
                    <asp:ListItem>Pass (Staff)</asp:ListItem>
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Other university certificate</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label25" runat="server" Text="Other Institution 1"></asp:Label></td>
            <td>
                <asp:TextBox ID="OtherBox1" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 200px;">
                <asp:TextBox ID="OtherCert1" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label26" runat="server" Text="Other Institution 2"></asp:Label></td>
            <td>
                <asp:TextBox ID="OtherBox2" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 200px;">
                <asp:TextBox ID="OtherCert2" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3" class="heading centerText">Referee Information</td>
        </tr>
        <tr>
            <td></td>
            <td>First Referee</td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label30" runat="server" Text="Full name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefNameBox1" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="RefNameBox1" CssClass="error" Display="Dynamic" ErrorMessage="Referee's name cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label31" runat="server" Text="Relationship"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefRelBox1" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="RefRelBox1" CssClass="error" Display="Dynamic" ErrorMessage="Referee's relationship cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label32" runat="server" Text="Mobile Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefPhoneBox1" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="RefPhoneBox1" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid phone number for referee" Text="*" ValidationExpression="^\+?[\d]{4,16}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="RefPhoneBox1" CssClass="error" Display="Dynamic" ErrorMessage="Referee's phone number cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label33" runat="server" Text="Email Address"></asp:Label></td>
            <td>
                <asp:TextBox CssClass="TextBox" ID="RefEmailBox1" runat="server" TextMode="Email" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="RefEmailBox1" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid email address for referee" Text="*" ValidationExpression="^[(\w)+@(\w)+\.(\w)+$]{5,32}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="RefEmailBox1" CssClass="error" Display="Dynamic" ErrorMessage="Referee's email Address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label34" runat="server" Text="Residential Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefAdrBox1" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="RefAdrBox1" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid home address for Referee" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{3,128}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="RefAdrBox1" CssClass="error" Display="Dynamic" ErrorMessage="Referee's home address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Second Referee</td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label35" runat="server" Text="Full name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefNameBox2" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="RefNameBox2" CssClass="error" Display="Dynamic" ErrorMessage="Referee 2's name cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label36" runat="server" Text="Relationship"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefRelBox2" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="RefRelBox2" CssClass="error" Display="Dynamic" ErrorMessage="Referee 2's relationship cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label37" runat="server" Text="Mobile Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefPhoneBox2" runat="server" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="RefPhoneBox2" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid phone number for referee" Text="*" ValidationExpression="^\+?[\d]{4,16}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="RefPhoneBox2" CssClass="error" Display="Dynamic" ErrorMessage="Referee 2's phone number cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px;">
                <asp:Label ID="Label38" runat="server" Text="Email Address"></asp:Label></td>
            <td>
                <asp:TextBox CssClass="TextBox" ID="RefEmailBox2" runat="server" TextMode="Email" Width="200px"></asp:TextBox></td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="RefEmailBox2" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid email address for referee" Text="*" ValidationExpression="^[(\w)+@(\w)+\.(\w)+$]{5,32}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="RefEmailBox2" CssClass="error" Display="Dynamic" ErrorMessage="Referee 2's email Address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 300px">
                <asp:Label ID="Label39" runat="server" Text="Residential Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="RefAdrBox2" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 50px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="RefAdrBox2" CssClass="error" Display="Dynamic" ErrorMessage="Enter a valid home address for Referee" Text="*" ValidationExpression="^[\w|'|\-|,|.| ]{3,128}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="RefAdrBox2" CssClass="error" Display="Dynamic" ErrorMessage="Referee 2's home address cannot be empty" Text="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="ValidateButton" runat="server" Text="Validate" CssClass="left" Height="40px" Width="150px" OnClick="ValidateButton_Click" /></td>
        </tr>
    </table>
    <br />

</asp:Content>
