$(function () {
    $('input:text:first').focus();
    var $inp = $('input:text');
    $inp.bind('keydown', function (e) {
        var key = e.which;
        if (key == 13) {
            $('#ibtnenter').click();
        }
    });
});

function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function logOn() {
    var div = $("#msg");
    var account = $("#txtAccount").val();
    var password = $("#txtPassword").val();
    var code = $("#txtCode").val();

    if (account == "" || password == "" || code == "") {
        div.html("请输入用户名，密码和验证码");
        return;
    }

    var loading = "<img alt='载入中，请稍候...' height='28' width='28' src='/Content/Images/loading.gif' />";
    div.html(loading + "载入中，请稍候...");
    var params = { account: account, password: password, code: code };

    $.ajax({
        type: "POST",
        url: "/Admin/CheckLogin/",
        data: $.param(params),
        success: function (msg) {
            if (msg) {
                if (msg.IsSuccess) {
                    div.html("登陆成功");
                    var href = unescape(request("ReturnUrl"));
                    if (href == "/" || href == "") {
                        href = "/Admin/";
                    }

                    window.location.href = href;
                }
                else {
                    div.html(msg.Message);
                }
            }
            else {
                div.html("为载入相关数据，请重试");
            }
        }
    });
}