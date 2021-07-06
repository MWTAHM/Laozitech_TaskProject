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