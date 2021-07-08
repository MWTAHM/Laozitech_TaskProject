<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="TaskProject.Pages.ProjectManager.List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/ProjectList.js"></script>
    <br />
    <div class="main">
        <asp:PlaceHolder runat="server" ID="MainC">
            <a class="btn btn-success" runat="server" href="~/Pages/Projects/AddOrEdit.aspx">New Project</a>
            <br />
            <br />
            <asp:PlaceHolder runat="server" ID="ProjectsTable">
            </asp:PlaceHolder>
        </asp:PlaceHolder>
    </div>
    <div style="min-height: 100px;"></div>
    <label class="w-100 text-center font-weight-bolder text-danger" style="font-size: 40px;">Archived</label>
    <div>
        <asp:Table CssClass="table table-hover" runat="server" ID="ArchivedTable">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Budget</asp:TableHeaderCell>
                <asp:TableHeaderCell>Manager</asp:TableHeaderCell>
                <asp:TableHeaderCell>Achieved %</asp:TableHeaderCell>
                <asp:TableHeaderCell>Controls</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>

    </div>

</asp:Content>
