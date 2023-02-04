var accountId = 0;
$.fn.dataTable.ext.errMode = 'none';
$(document).ready(function () {
    var table = $('#dataTableAccount').DataTable({
        ajax: {
            url: '/api/Admin/AccountManagement/GetAllUsers',
            type: 'GET',
        },
        scrollY: "50vh",
        scrollX: false,
        scrollCollapse: false,
        dom: 'Bfrtip',
        buttons: [{
            text: '<button type="button" onclick="showPopupCreateAccount()" class="btn btn-success" data-toggle="modal">Thêm mới</button>'
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
                title: 'Tên tài khoản',
                data: 'userName'
            },
            {
                title: 'Ngày tạo',
                data: 'createdDate',
                //type: 'date-euro'
                //render: function (data, type, row) {//data
                //    return moment(row.updatedDate).format('DD/MM/YYYY hh:mm:ss')
                //}
            },
            {
                title: 'Hành động',
                data: null,
                render: function (data, type, row) {
                    data = '<button type="button" onclick="showPopupEditAccount(`' + row.id + '`)" class="btn btn-success btn-icon" data-toggle="modal"><i class="fas fa-user-edit"></i></button><button type="button" class="btn btn-danger btn-icon" data-toggle="modal" onclick="showPopupDeleteAccount(`' + row.id + '`)"><i class="fas fa-trash-alt"></i></button><button type="button" onclick="showPopupChangePassword(`' + row.id +'`)" class="btn btn-secondary btn-icon" data-toggle="modal"><i class="fas fa-user-tag"></i></button>';
                    return data;
                }
            }
        ],
        columnDefs: [{
            sortable: false,
            "class": "index",
            targets: 0
        }],
    });
    loadListRole();
});

function refreshTable() {
    $('#dataTableAccount').DataTable().ajax.reload();
}

//------ Hiện thị popup thêm mới tài khoản
function showPopupCreateAccount() {
    $('#form_add_account')[0].reset();
    $('#modal_create_account').modal('show');
}

//------- Thêm mới tài khoản
function createAccount() {
    var formData = {};
    formData.UserName = $("#userName").val();
    formData.Password = $("#confirmPasswordCreate").val();
    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/CreateAccount',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        console.log('data', data)
        if (data.status) {
            $('#modal_create_account').modal('hide');
            toastrShow('Thêm mới thành công!', 1);
            refreshTable();
            //$("#isActive_account").val('1').trigger('change');
        }
        else
        {
            toastrShow('Thêm mới thất bại!', 0);
        }
    });
}

//------ Hiển thị popup chỉnh sửa tài khoản
function showPopupEditAccount(id) {
    accountId = id;
    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/InforAccount?idAccount=' + id,
        contentType: 'application/json'
    }).done(function (data) {
        console.log('data', data)
        if (data.status) {
            $('#form_edit_account')[0].reset();
            $('#userNameEdit').val(data.data.userName);
            $('#lstRoleEdit').val(data.data.roleId);
            $('#modal_edit_account').modal('show');
        }
        else {
            toastrShow('Đã có lỗi xảy ra!', 0);
        }
    });

}

//------- Chỉnh sửa tài khoản
function editAccount() {
    var formData = {};
    formData.Id = accountId;
    formData.UserName = $("#userNameEdit").val();
    formData.Password = $("#passwordEdit").val();
    formData.RoleId = $('#lstRoleEdit').val();

    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/EditAccount',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status) {
            toastrShow('Chỉnh sửa thành công!', 1);
            $('#modal_edit_account').modal('hide');
            refreshTable();
        }
        else {
            toastrShow('Chỉnh sửa thất bại!', 0);
        }
    });
}

//------ Hiển thị popup xóa tài khoản
function showPopupDeleteAccount(id) {
    accountId = id;
    $('#modal_delete_account').modal('show');
}

//------ Xòa tài khoản
function deleteAccount() {
    var formData = {};
    formData.Id = accountId;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/DeleteAccount',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status) {
            toastrShow('Xóa thành công!', 1);
            $('#modal_delete_account').modal('hide');
            refreshTable();
        }
        else
        {
            toastrShow('Xóa thất bại!', 0);
        }
    });
}

//------ Hiển thị popup đổi mật khẩu
function showPopupChangePassword() {
    $('#form_change_password')[0].reset();
    $('#modal_change_password').modal('show');
}

//------ Check tồn tại tên tài khoản
function checkExistNameAccount(userName) {
    var formData = {};
    formData.UserName = userName;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/CheckExistNameAccount',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status) {
        }
        else {
        }
    });
}

//------ Load form
function loadListRole() {
    $.ajax({
        type: 'GET',
        url: '/api/Admin/RoleManagement/GetAllRoles',
        contentType: 'application/json',
        dataType: "json",
    }).done(function (data) {
        if (data) {
            console.log('data', data);
            var html = '';
            html += `<option value="">--Chọn--</option>`;
            for (var i = 0; i < data.data.length; i++) {
                html += `<option value="` + data.data[i].id + `">` + data.data[i].roleName + `</option>`;
            }
            $("#lstRole").html(html);
            $("#lstRoleEdit").html(html);
        }
    });
}

//------ Đổi mật khẩu
function checkExistNameAccount(userName) {
    var formData = {};
    formData.UserName = userName;
    formData.UserName = userName;

    $.ajax({
        type: 'POST',
        url: '/api/Admin/AccountManagement/ChangePassword',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        if (data.status) {
        }
        else {
        }
    });
}
