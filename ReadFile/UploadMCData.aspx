<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadMCData.aspx.cs" Inherits="ReadFile.UploadMCData" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Import MasterCard File</h1>
    <br />
    <div>
        <asp:FileUpload ID="MCFileUpload" runat="server" />
        <br/>
        <asp:Button ID="BtnMCData" runat="server" Text="Import MasterCard" OnClick="BtnMCData_Click"/>
        &nbsp;<asp:Button ID="ExportBtn" runat="server" OnClick="ExportBtn_Click" Text="Export Data" />
        <br />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
</asp:Content>



