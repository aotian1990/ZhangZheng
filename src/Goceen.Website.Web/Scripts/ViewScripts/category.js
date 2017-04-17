$(function () {
    $('#grid').datagrid({
        title: '频道信息',
        iconCls: 'icon-save',
        nowrap: false,
        striped: true,
        url: '/Category/LoadAllByPage/',
        sortName: 'OrderNo',
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
                    { field: 'Name', title: '中文名', width: 80, align: 'right' },
                    { field: 'ChannelName', title: '版块', width: 80, align: 'right' },
                    { field: 'CategoryType', title: '类型', width: 80, align: 'right' },
                    {
                        field: 'IsEnabled', title: '状态', width: 40, align: 'right',
                        formatter: function (value, rec) {
                            return value ? '显示' : '隐藏';
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
                $("#modalwindow").window({ title: '添加分类', width: 750, height:  450, iconCls: 'icon-add', modal: true, resizable: false }).window('open').window('center');
                $("#modalwindow").window('refresh', '/Category/AddOrEdit');
            }
        }, '-', {
            id: 'btnUpdate',
            text: '修改',
            iconCls: 'icon-save',
            handler: function () {
                var row = $('#grid').datagrid('getSelected');
                if (row) {
                    var url = "/Category/AddOrEdit/" + row.Id;
                    $("#modalwindow").window({ title: '修改分类', width: 750, height: 450, iconCls: 'icon-add', modal: true, resizable: false }).window('open').window('center');
                    $("#modalwindow").window('refresh', url);
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
                        url: "/Category/Delete/",
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