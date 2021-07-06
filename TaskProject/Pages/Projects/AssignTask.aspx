<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignTask.aspx.cs" Inherits="TaskProject.Pages.Tasks.TaskAssign" %>

<html>
<body>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <form runat="server">
        <asp:Label runat="server" ID="taskText"></asp:Label>
        <asp:TextBox runat="server" ID="Id" hidden="true"></asp:TextBox>
        <asp:DropDownList runat="server" ID="Users" CssClass="form-control">
        </asp:DropDownList>
        <br />
        <asp:Button runat="server" Text="Confirm" class="btn btn-info" OnClick="Confirmed"></asp:Button>
    </form>
</body>
</html>
