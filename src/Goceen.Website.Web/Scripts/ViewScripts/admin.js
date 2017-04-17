$(function () {
    ReadDateTimeShow();
    var setTimeInterval = setInterval(ReadDateTimeShow, 1e3);
    windowResize();
    $(window).resize(function() {
        windowResize();
    });
    $('#btnok').bind('click', function () {
        changePassword();
    });
    $('#btncancel').bind('click', function () {
        $('#winPassword').window('close');
    });
});

function getWindowHeight() {
    return $(window).height();
}

function getWindowWidth() {
    return $(window).width();
}

function windowResize() {
    var width = getWindowWidth();
    var height = getWindowHeight();
    $("form#form1").width(width);
    $("form#form1").height(height);
    $("form#form1").layout();
}

function showChangePasswordWin() {
    $('#winPassword').window('open');
}

function ReadDateTimeShow() {
    var year = new Date().getFullYear();
    var month = new Date().getMonth() + 1;
    var day = new Date().getDate();
    var time = new Date().toLocaleTimeString();
    var addDate = year + "年" + month + "月" + day + "日 " + time;
    $("#date").text(addDate);

}

function changePassword() {
    var oldPassword = $("#iptOldPassword").val();
    if (oldPassword == "") {
        $.messager.alert('提示', '请输入旧密码！');
        return;
    }

    var password = $("#iptPassword").val();
    if (password == "") {
        $.messager.alert('提示', '请输入新密码！');
        return;
    }

    var newPassword = $("#iptNewPassword").val();
    if (newPassword == "") {
        $.messager.alert('提示', '请确认密码！');
        return;
    }

    if (newPassword != password) {
        $.messager.alert('提示', '两次密码不一致，请重新输入！');
        return;
    }
    var parm = { password: password, oldPassword: oldPassword };
    $.ajax({
        type: "POST",
        url: "/Admin/ChangedPassword/",
        data: parm,
        success: function (msg) {
            if (msg.IsSuccess) {
                $.messager.alert('提示', '修改成功！', "info", function () {
                    $('#winPassword').window('close');
                    $("#iptOldPassword").val("");
                    $("#iptPassword").val("");
                    $("#iptNewPassword").val("");
                });
            } else {
                $.messager.alert('提示', '密码错误，请重新输入！', "info");
            }
        },
        error: function () {
            $.messager.alert('错误', '修改失败！', "error");
        }
    });
}

function showTab(url, title) {
    var tab = $('#tab');
    if (tab.tabs('exists', title)) {
        tab.tabs('select', title);
    }
    else {
        tab.tabs('add', {
            title: title,
            content: "<iframe scrolling='yes' frameborder='0' src='/"
               + url + "/Index/' style='width:100%;height:100%;'/>",
            closable: true
        });

    }
}

function showArticle(id, title) {
    var tab = $('#tab');
    if (tab.tabs('exists', title)) {
        tab.tabs('select', title);
    }
    else {
        tab.tabs('add', {
            title: title,
            content: "<iframe scrolling='yes' frameborder='0' src='/Article/Admin/"
              + id + "/' style='width:100%;height:100%;'/>",
            closable: true
        });
    }
}

//init
