<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmArchive.aspx.cs" Inherits="TaskProject.Pages.Projects.ConfirmArchive" %>


<%-- Popup --%>
<html>
<body>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <form runat="server">
        <asp:TextBox runat="server" ID="Id" hidden="true"></asp:TextBox>
        <asp:Label runat="server" ID="DeleteText" class="text-danger">Archive?</asp:Label><br />
        <br />
        <input type="submit" value="I'm Sure" class="btn btn-danger">
    </form>
</body>
</html>
