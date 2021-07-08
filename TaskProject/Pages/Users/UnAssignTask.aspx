<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnAssignTask.aspx.cs" Inherits="TaskProject.Pages.Users.TaskUnAssign" %>

<%-- Popup --%>
<html>
<body>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <form runat="server">
        <asp:PlaceHolder runat="server" ID="MainPlaceholder">
            <asp:Label runat="server" ID="taskText"></asp:Label>
            <asp:TextBox runat="server" ID="TaskId" hidden="true"></asp:TextBox>
            <asp:TextBox runat="server" ID="UserId" hidden="true"></asp:TextBox>
            <br />
            <asp:Button runat="server" Text="Confirm" class="btn btn-info" OnClick="Confirme_Clicked"></asp:Button>
        </asp:PlaceHolder>
    </form>
</body>
</html>
