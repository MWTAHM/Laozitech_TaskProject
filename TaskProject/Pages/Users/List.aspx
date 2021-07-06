<%@ Page Title="All User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="TaskProject.Pages.Users.List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function Delete(Id) {
            try {
                $.ajax({
                    async: true,
                    method: 'GET',
                    url: '/Pages/Users/ConfirmDelete',
                    data: {
                        Id:Id
                    },
                    success: function (res) {
                        $('.modal-title').html('Delete');
                        $('.modal-body').html(res);
                        $('.modal').modal('show');
                    }
                });
            } catch (e) {
                return false;
            }
            return true;
        }
    </script>
    <br />  
    <div class="main">
        <asp:PlaceHolder runat="server" ID="ProjectsTable">
            <a class="btn btn-success" runat="server" href="~/Pages/Users/Registration.aspx">Register</a>
            <br />
            <br />
        </asp:PlaceHolder>
    </div>
</asp:Content>
