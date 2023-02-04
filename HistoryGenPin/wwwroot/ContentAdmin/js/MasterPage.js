var tokenKey = 'accessToken';
var token = localStorage.getItem(tokenKey);
var headers = new Headers();
if (token) {
    headers.Authorization = 'Bearer ' + token;
}
var itemConfirmExit;

$(document).ready(function () {
    $("#fullNameAcc").html(localStorage.getItem('FullName'));
    if (localStorage.getItem('AvatarUrl') && localStorage.getItem('AvatarUrl') != "") {
        $("#avatarUser").attr('src', localStorage.getItem('AvatarUrl'));
    } else if (localStorage.getItem('AvatarUrl') == "") {
        $("#avatarUser").attr('src', '/Contents/dist/images/avatar.png');
    }
    var lang = localStorage.getItem('currentLang');
    $(document).ajaxError(function (e, xhr, opt) {
        if (xhr.status === 404) {
            alert("Element not found.");
        } else if (xhr.status === 401) {
            localStorage.removeItem(tokenKey);
            if (!itemConfirmExit) itemConfirmExit = $.confirm({
                type: 'red',
                title: '<i class="fa fa-warning fa-lg text-red"> Thông báo',
                content: 'Phiên đăng nhập kết thúc!',
                buttons: {
                    somethingElse: {
                        text: 'Đăng nhập lại',
                        btnClass: 'btn-blue',
                        keys: ['enter', 'shift'],
                        action: function () {
                            location.href = "/" + lang + '/Admin?ReturnUrl=' + window.location.pathname;
                        }
                    }
                }
            });
        } else if (xhr.status === 403) {
            localStorage.removeItem(tokenKey);
            location.href = "/" + lang + '/err/forbidden/?ReturnUrl=' + window.location.pathname;
        } else if (xhr.status === 400) {
            toastr.warning(xhr.responseJSON);
        }
    });
});

jQuery.ajaxSetup({
    //headers: headers,
    beforeSend: function (xhr, settings) {
        
        xhr.setRequestHeader('AuthorizeLocation', "CLI");
        xhr.setRequestHeader('Authorization', 'Bearer ' + token);
    },
    error: function (jqXHR, status, error) {
        var lang = localStorage.getItem('currentLang');
        console.log(jqXHR);
        if (jqXHR.status === 404) {
            alert("Element not found.");
        } else if (jqXHR.status === 401) {

            localStorage.removeItem(tokenKey);
            //if (!itemConfirmExit) itemConfirmExit = $.confirm({
            //    type: 'red',
            //    title: '<i class="fa fa-warning fa-lg text-red"> ' + $.i18n('notification'),
            //    content: $.i18n("sessionEnd"),
            //    buttons: {
            //        somethingElse: {
            //            text: 'Đăng nhập lại',
            //            btnClass: 'btn-blue',
            //            keys: ['enter', 'shift'],
            //            action: function () {
            //                location.href = "/" + lang + '/Admin?ReturnUrl=' + window.location.pathname;
            //            }
            //        }
            //    }
            //});
        } else if (jqXHR.status === 403) {
            localStorage.removeItem(tokenKey);
            location.href = "/" + lang + '/err/forbidden/?ReturnUrl=' + window.location.pathname;
        } else if (jqXHR.status === 400) {
            console.log(jqXHR);
            toastr.warning(jqXHR.responseJSON);
        }

    }

});