$(document).ready(function () {
   
});

//document.addEventListener("DOMContentLoaded", function (event) {
//    var tokenChk = localStorage.getItem('accessToken');
//    if (tokenChk != undefined && tokenChk != null && tokenChk != '') {
//        $.ajax({
//            type: 'POST',
//            url: '/api/Admin/LoginManagement/CheckToken',
//            contentType: 'application/json',
//            beforeSend: function (xhr, settings) {
//                xhr.setRequestHeader('AuthorizeLocation', "CLI");
//                xhr.setRequestHeader('Authorization', 'Bearer ' + tokenChk);
//            },
//            success: function (data, textStatus, jqXHR) {
//                location.href = "/Admin/AccountManagement";
//            }
//        });
//    }

//});

function btnLogin() {
    var formData = {};
    formData.UserName = $("#userNameLogin").val();
    formData.Password = $("#passwordLogin").val();
    var tokenKey = 'accessToken';
    $.ajax({
        type: 'POST',
        url: '/api/Admin/LoginManagement/Login',
        contentType: 'application/json',
        data: JSON.stringify(formData)
    }).done(function (data) {
        console.log('data', data)
        if (data.status == true) {

            localStorage.setItem(tokenKey, data.data.token);
            var dataToken = parseJwt(data.data.token);
            localStorage.setItem("UserId", dataToken.userId);
            

            location.href = "/Admin/AccountManagement";
        }
        else {
            toastrShow('Đăng nhập thất bại!', 0);
        }
    });
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};