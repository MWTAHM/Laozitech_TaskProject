<%@ Page Title="Project Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="TaskProject.Pages.Projects.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/ProjectDetails.js"></script>
    <script>
        function DeleteTask(Id) {
            try {
                $.ajax({
                    async: true,
                    method: 'GET',
                    url: '/Pages/Projects/ConfirmDeleteTask',
                    data: {
                        Id: Id
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

        function AssignTask(Id) {
            try {
                $.ajax({
                    async: true,
                    method: 'GET',
                    url: '/Pages/Projects/AssignTask',
                    data: {
                        Id: Id
                    },
                    success: function (res) {
                        $('.modal-title').html('Task Assign');
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
            $('#MainContent_Tasks').after('<div style="text-align:center" id="nav"></div>');
            var rowsShown = 4;
            var rowsTotal = $('#MainContent_Tasks tbody tr').length;
            var numPages = rowsTotal / rowsShown;
            for (i = 0; i < numPages; i++) {
                var pageNum = i + 1;
                if (pageNum == 1)
                    $('#nav').append('<a active class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
                else
                    $('#nav').append('<a class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
            }
            $('#MainContent_Tasks tbody tr').hide();
            $('#MainContent_Tasks tbody tr').slice(0, rowsShown).show();
            $('#MainContent_Tasks a:first').addClass('active');
            $('#nav a').click(function () {
                $('#nav a').removeClass('active');
                $(this).addClass('active');
                var currPage = $(this).attr('rel');
                var startItem = currPage * rowsShown;
                var endItem = startItem + rowsShown;
                $('#MainContent_Tasks tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                    css('display', 'table-row').animate({ opacity: 1 }, 300);
            });
        });
    </script>
    
    <label class="w-100 text-center font-weight-bolder" style="font-size:40px;">Project Details</label>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label runat="server">Project Name:</asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="PName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">End Time: </asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="PEnd"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <%-- Tasks Table --%>
    <label class="w-100 text-center font-weight-bolder" style="font-size:40px;">Tasks</label>
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
