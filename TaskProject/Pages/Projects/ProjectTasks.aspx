<%@ Page Title="Project Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectTasks.aspx.cs" Inherits="TaskProject.Pages.Projects.ProjectTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/ProjectDetails.js"></script>
    <%-- Tasks Table --%>
    <asp:Label runat="server" ID="TitleLabel" CssClass="w-100 text-center font-weight-bolder d-block" style="font-size:40px;" Text="Project Tasks"></asp:Label>
    <asp:TextBox hidden="true" runat="server" ID="ProjectIdTextBox"></asp:TextBox>
    <asp:Button Text="New Task" class="btn btn-success" runat="server" onclick="NewTask"></asp:Button>
    <br />
    <br />
    <asp:Table runat="server" ID="Tasks" CssClass="table table-hover">
        <asp:TableHeaderRow>
            <asp:TableCell>Name</asp:TableCell>
            <asp:TableCell>Description</asp:TableCell>
            <asp:TableCell>Date</asp:TableCell>
            <asp:TableCell>Controls</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
