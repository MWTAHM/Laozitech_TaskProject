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