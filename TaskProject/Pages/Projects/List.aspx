<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="TaskProject.Pages.ProjectManager.List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/ProjectList.js"></script>
    <style>
        .clickable:hover {
            cursor: pointer;
        }

        #pagination tr {
          display: none;
        }

        .btn-light:not(:disabled):not(.disabled):active, .btn-light:not(:disabled):not(.disabled).active, .show > .btn-light.dropdown-toggle{
            background-color: cornflowerblue;
        }
        
    </style>

    <script>
        var num = 1;
        function MakePagination(tableId) {
            $(tableId).after('<div style="text-align:center" id="nav"></div>');
            var rowsShown = 4;
            var rowsTotal = $(tableId+' tbody tr').length;
            var numPages = rowsTotal / rowsShown;
            for (i = 0; i < numPages; i++) {
                var pageNum = i + 1;
                if (pageNum == 1)
                    $('#nav').append('<a active class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
                else
                    $('#nav').append('<a class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
            }
            $(tableId+' tbody tr').hide();
            $(tableId+' tbody tr').slice(0, rowsShown).show();
            $(tableId+' a:first').addClass('active');
            $('#nav a').click(function () {
                $('#nav a').removeClass('active');
                $(this).addClass('active');
                var currPage = $(this).attr('rel');
                var startItem = currPage * rowsShown;
                var endItem = startItem + rowsShown;
                $(tableId+' tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                    css('display', 'table-row').animate({ opacity: 1 }, 300);
            });
        }
        $(document).ready(function () {
            MakePagination('#pagination');
        });
    </script>

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
