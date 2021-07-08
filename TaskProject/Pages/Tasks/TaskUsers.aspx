<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskUsers.aspx.cs" Inherits="TaskProject.Pages.Tasks.TaskUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <asp:Label runat="server" ID="TitleLabel" class="w-100 text-center font-weight-bolder" Style="font-size: 40px;">Users Working On Task</asp:Label>
    <asp:Table CssClass="table table-hover" runat="server" ID="UsersTable">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>UserName</asp:TableHeaderCell>
            <asp:TableHeaderCell>Full Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Email</asp:TableHeaderCell>
            <asp:TableHeaderCell>Country</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
