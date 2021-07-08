function Delete(Id) {
    try {
        $.ajax({
            async: true,
            method: 'GET',
            url: '/Pages/Projects/ConfirmDelete',
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

function Archive(Id) {
    try {
        $.ajax({
            async: true,
            method: 'GET',
            url: '/Pages/Projects/ConfirmArchive',
            data: {
                Id: Id
            },
            success: function (res) {
                $('.modal-title').html('Archive');
                $('.modal-body').html(res);
                $('.modal').modal('show');
            }
        });
    } catch (e) {
        return false;
    }
    return true;
}


function UnArchive(Id) {
    try {
        $.ajax({
            async: true,
            method: 'GET',
            url: '/Pages/Projects/ConfirmUnArchive',
            data: {
                Id: Id
            },
            success: function (res) {
                $('.modal-title').html('UnArchive');
                $('.modal-body').html(res);
                $('.modal').modal('show');
            }
        });
    } catch (e) {
        return false;
    }
    return true;
}

function Create() {
    try {
        $.ajax({
            async: true,
            method: 'GET',
            url: '/Pages/Projects/AddOrEdit',
            success: function (res) {
                $('.modal-title').html('New Project');
                $('.modal-body').html(res);
                $('.modal').modal('show');
            }
        });
    } catch (e) {
        return false;
    }
    return true;
}

var num = 1;
function MakePagination(tableId) {
    $(tableId).after('<div style="text-align:center" id="nav"></div>');
    var rowsShown = 4;
    var rowsTotal = $(tableId + ' tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        if (pageNum == 1)
            $('#nav').append('<a active class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
        else
            $('#nav').append('<a class="btn btn-light" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $(tableId + ' tbody tr').hide();
    $(tableId + ' tbody tr').slice(0, rowsShown).show();
    $(tableId + ' a:first').addClass('active');
    $('#nav a').click(function () {
        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $(tableId + ' tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
$(document).ready(function () {
    MakePagination('#pagination');
});