var roleId = 0;
$.fn.dataTable.ext.errMode = 'none';
$(document).ready(function () {
    var table = $('#dataTableRole').DataTable({
        ajax: {
            url: '/api/Admin/RoleManagement/GetAllRoles',
            type: 'GET',
        },
        scrollY: "300px",
        scrollX: false,
        scrollCollapse: false,
        dom: 'Bfrtip',
        buttons: [{
            text: '<button type="button" onclick="showPopupCreateRole()" class="btn btn-success" data-toggle="modal">Thêm mới</button>'
        }],
        columns: [
            {
                title: 'STT',
                data: 'index'
            },
            {
                data: 'id',
                visible: false
            },
            {
                title: 'Tên quyền',
                data: 'roleName'
            },
            {
                title: 'Mô tả',
                data: 'description'
            },
            {
                title: 'Ngày tạo',
                data: 'createdDate',
                type: 'date-euro'
                //render: function (data, type, row) {//data
                //    return moment(row.updatedDate).format('DD/MM/YYYY hh:mm:ss')
                //}
            },
            {
                title: 'Hành động',
                data: null,
                render: function (data, type, row) {
                    data = '<button type="button" onclick="showPopupUpdateRole(`' + row.id + '`)" class="btn btn-success btn-icon" data-toggle="modal"><i class="fas fa-user-edit"></i></button><button type="button" class="btn btn-danger btn-icon" data-toggle="modal" onclick="showPopupDeleteRole(`' + row.id + '`)"><i class="fas fa-trash-alt"></i></button>';
                    return data;
                }
            }
        ],
        columnDefs: [{
            sortable: false,
            "class": "index",
            targets: 0
        }],
        order: [[1, 'desc']],
        fixedColumns: true
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

//------ refresh data Table
function refreshTable() {
    $('#dataTableRole').DataTable().ajax.reload();
}

//------ Hiển thị popup thêm mới quyền
function showPopupCreateRole() {
    $('#form_add_role')[0].reset();
    $('#modal_create_role').modal('show');
}

//------ Thêm mới quyền
function createRole() {
    var checkIsShow = false;
    var checkIsDownload = false;
    if ($('input[name="isShowCheck"]:checked').length > 0) {
        checkIsShow = true;
    } else {
        checkIsShow = false;
    }
    if ($('input[name="isDownloadCheck"]:checked').length > 0) {
        checkIsDownload = true;
    } else {
        checkIsDownload = false;
    }

    var formCheckbox = {};
    formCheckbox.IsShow = checkIsShow;
    formCheckbox.IsDowload = checkIsDownload;

    var formData = {};
    formData.RoleName = $("#roleName").val();
    formData.Description = $("#description").val();
    formData.DetailRole = formCheckbox;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/RoleManagement/CreateRole',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        console.log('data', data)
        if (data.status == true)
        {
            $('#modal_create_role').modal('hide');
            toastrShow('Thêm mới thành công!', 1);
            refreshTable();
        }
        else
        {
            toastrShow('Thêm mới thất bại!', 0);
        }
    });
}

//------ Hiển thị popup chỉnh sửa quyền
function showPopupUpdateRole(id) {
    roleId = id;
    $.ajax({
        type: 'POST',
        url: '/api/Admin/RoleManagement/GetInfoRole?idRole=' + id,
        contentType: 'application/json'
    }).done(function (data) {
        console.log('data', data)
        if (data.status == true) {
            $('#form_edit_role')[0].reset();
            $('#roleNameEdit').val(data.data.roleName);
            $('#descriptionEdit').val(data.data.description);
            $('#isShowCheckEdit').prop('checked', (data.data.detailRole.isShow == true));
            $('#isDownloadCheckEdit').prop('checked', (data.data.detailRole.isDowload == true));
            $('#modal_edit_role').modal('show');
        }
        else {
            toastrShow('Đã có lỗi xảy ra!', 0);
        }
    });
}

//------ Chỉnh sửa quyền
function editRole() {
    var checkIsShow = false;
    var checkIsDownload = false;
    if ($('input[name="isShowCheckEdit"]:checked').length > 0) {
        checkIsShow = true;
    } else {
        checkIsShow = false;
    }
    if ($('input[name="isDownloadCheckEdit"]:checked').length > 0) {
        checkIsDownload = true;
    } else {
        checkIsDownload = false;
    }

    var formCheckbox = {};
    formCheckbox.IsShow = checkIsShow;
    formCheckbox.IsDowload = checkIsDownload;

    var formData = {};
    formData.RoleId = roleId;
    formData.RoleName = $("#roleNameEdit").val();
    formData.Description = $("#descriptionEdit").val();
    formData.DetailRole = formCheckbox;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/RoleManagement/UpdateRole',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status == true)
        {
            toastrShow('Chỉnh sửa thành công!', 1);
            $('#modal_edit_role').modal('hide');
            refreshTable();
        }
        else
        {
            toastrShow('Chỉnh sửa thất bại!', 0);
        }
    });
}

//------ Hiển thị popup xóa quyền
function showPopupDeleteRole(id) {
    roleId = id;
    $('#modal_delete_role').modal('show');
}

//------ Xóa quyền
function deleteRole() {
    var formData = {};
    formData.RoleId = roleId;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/RoleManagement/DeleteRole',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status == true)
        {
            toastrShow('Xóa thành công!', 1);
            $('#modal_delete_role').modal('hide');
            refreshTable();
        }
        else
        {
            toastrShow('Xóa thất bại!', 0);
        }
    });
}