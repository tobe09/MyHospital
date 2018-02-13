<%@ Page Language="C#" Title="Home Page" MasterPageFile="~/HospitalMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HospitalManager.Home" %>

<asp:Content ContentPlaceHolderID="ContentPH" ID="HomeContent" runat="server">
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl.Text += "Different contents will be added ";
            Page_Load();
        }
    </script>

    <div style="margin-left: 100px;">
        <br />
        <br />
        <h1>Welcome to Neptune Hospital</h1>
        <p>
            You can register with us easily as a  patient for enquiries and access to our services,
            or as a doctor for communication and recommendation.
        </p>
        <br />
        <br />
        <br />
        <p style="font-size: 20px;">We treat, God heals</p>
        <br />
        <br />
        <br />
        <h3><asp:Label ID="lbl" runat="server"></asp:Label></h3>
    </div>
</asp:Content>
