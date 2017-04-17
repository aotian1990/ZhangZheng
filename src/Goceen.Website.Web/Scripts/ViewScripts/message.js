$(function () {
    $('#grid').datagrid({
        title: '留言信息',
        iconCls: 'icon-save',
        nowrap: false,
        striped: true,
        url: '/Message/LoadAllByPage/',
        sortName: 'Phone',//加到哪个标题上面，就会多个背景，好奇怪，所以加到未显示的一个标题上
        sortOrder: 'desc',
        remoteSort: true,
        fitColumns: true,
        //fit: true,
        idField: 'Id',
        frozenColumns: [[
            { field: 'Id', checkbox: true }
        ]],
        columns: [[
            { field: 'OrderNo', title: '序号', width: 40, align: 'left' },
            { field: 'Name', title: '用户名称', width: 40, align: 'right' },
            { field: 'Content', title: '留言内容', width: 80, align: 'right' },
            { field: 'Reply', title: '回复内容', width: 80, align: 'right' },
            {
                field: 'IsReview', title: '审核状态', width: 40, align: 'right',
                formatter: function (value, rec) {
                    return value ? '已审核' : '未审核';
                }
            }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: ['-', {
            id: 'btnSave',
            text: '添加',
            iconCls: 'icon-add',
            handler: function () {

                //$("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0'' src='/Channel/AddOrEdit'></iframe>");
                //$("#modalwindow").window({ title: '添加频道', width: 390, height: 320, iconCls: 'icon-add', modal: true, resizable: false }).window('open').window('center');
                $("#modalwindow").window({ title: '添加留言', width: 400, height: 355, iconCls: 'icon-add', modal: true, resizable: false }).window('open').window('center');
                $("#modalwindow").window('refresh', '/Message/AddOrEdit');
            }
        }, '-', {
            id: 'btnUpdate',
            text: '修改',
            iconCls: 'icon-save',
            handler: function () {
                var row = $('#grid').datagrid('getSelected');
                if (row) {
                    var url = "/Message/AddOrEdit/" + row.Id;
                    //$("#modalwindow").html("<iframe width='100%' height='98%' scrolling='no' frameborder='0'' src='" + url + "'></iframe>");
                    $("#modalwindow").window({ title: '修改留言', width: 400, height: 355, iconCls: 'icon-add', modal: true, resizable: false }).window('open').window('center');
                    $("#modalwindow").window('refresh', url);

                    //window.location.href = "/Forum/View/" + row.Id;
                }
                else {
                    $.messager.alert('提示', '请选择要修改的数据');
                    return;
                }

            }
        }, '-', {
            id: 'btnDelete',
            text: '删除',
            disabled: false,
            iconCls: 'icon-cut',
            handler: function () {

                var rows = $('#grid').datagrid('getSelections');
                if (!rows || rows.length == 0) {
                    $.messager.alert('提示', '请选择要删除的数据');
                    return;
                }
                var parm;
                $.each(rows, function (i, n) {
                    if (i == 0) {
                        parm = "idList=" + n.Id;
                    }
                    else {
                        parm += "&idList=" + n.Id;
                    }
                });
                $.messager.confirm('提示', '是否删除这些数据?', function (r) {
                    if (!r) {
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "/Message/Delete/",
                        data: parm,
                        success: function (msg) {
                            if (msg.Success) {
                                $.messager.alert('提示', '删除成功！', "info", function () {
                                    $('#grid').datagrid("reload");
                                });
                            }
                        },
                        error: function () {
                            $.messager.alert('错误', '删除失败！', "error");
                        }
                    });
                });
            }
        }, '-']
    });

    $(window).resize(function () { $('#grid').datagrid('resize') });

});