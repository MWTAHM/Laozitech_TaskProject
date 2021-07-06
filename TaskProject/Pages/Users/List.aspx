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
    <script>
        $(document).ready(function () {
            $('#UsersTable').after('<div style="text-align:center" id="nav"></div>');
            var rowsShown = 4;
            var rowsTotal = $('#UsersTable tbody tr').length;
            var numPages = rowsTotal / rowsShown;
            for (i = 0; i < numPages; i++) {
                var pageNum = i + 1;
                if (pageNum == 1)
                    $('#nav').append('<a active class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
                else
                    $('#nav').append('<a class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
            }
            $('#UsersTable tbody tr').hide();
            $('#UsersTable tbody tr').slice(0, rowsShown).show();
            $('#UsersTable a:first').addClass('active');
            $('#nav a').click(function () {
                $('#nav a').removeClass('active');
                $(this).addClass('active');
                var currPage = $(this).attr('rel');
                var startItem = currPage * rowsShown;
                var endItem = startItem + rowsShown;
                $('#UsersTable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                    css('display', 'table-row').animate({ opacity: 1 }, 300);
            });
        });
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
