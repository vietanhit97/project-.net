var accountId = 0;
$.fn.dataTable.ext.errMode = 'none';
$(document).ready(function () {
    var table = $('#dataTableReport').DataTable({
        ajax: {
            url: '/api/Admin/Report/GetAllHistoryGenPin',
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
                data: 'Id',
                visible: false
            },
            {
                title: 'Tên tài khoản',
                data: 'UserName'
            },
            {
                title: 'Ngày tạo',
                data: 'CreatedDate',
            },
            {
                title: 'Hành động',
                data: null,
                render: function (data, type, row) {
                    data = '<button type="button" onclick="showPopupEditAccount(`' + row.Id + '`)" class="btn btn-success btn-icon" data-toggle="modal"><i class="fas fa-user-edit"></i></button><button type="button" class="btn btn-danger btn-icon" data-toggle="modal" onclick="showPopupDeleteAccount(`' + row.Id + '`)"><i class="fas fa-trash-alt"></i></button><button type="button" onclick="showPopupChangePassword(`' + row.Id + '`)" class="btn btn-secondary btn-icon" data-toggle="modal"><i class="fas fa-user-tag"></i></button>';
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
    $('#dataTableReport').DataTable().ajax.reload();
}