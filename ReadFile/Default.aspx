<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReadFile._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Import CSV File</h1>
    <br />
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Import Data" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

</asp:Content>
