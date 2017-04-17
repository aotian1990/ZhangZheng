$(function () {
    $('#grid').datagrid({
        title: '新闻管理',
        iconCls: 'icon-save',
        nowrap: false,
        striped: true,
        url: '/Article/LoadAllByPage/',
        sortName: 'CreateDate',
        sortOrder: 'desc',
        remoteSort: true,
        fitColumns: true,
        idField: 'Id',
        columns: [[
                { field: 'Id', title: '', width: 10, checkbox: true },
                { field: 'Title', title: '标题', width: 200, align: 'left' },
                { field: 'CategoryName', title: '所属分类', width: 60, align: 'left' },
                { field: 'Keyword', title: '关键字', width: 160, align: 'left' },
                {
                    field: 'IsFirst', title: '是否置顶', width: 20, align: 'center',
                    formatter: function (value, rec) {
                        return value ? '是' : '否';
                    }
                },
                {
                    field: 'IsEnabled', title: '状态', width: 20, align: 'center',
                    formatter: function (value, rec) {
                        return value ? '显示' : '隐藏';
                    }
                },
                { field: 'ViewCount', title: '浏览次数', width: 20, align: 'center' },
                {
                    field: 'CreateDate', title: '建立日期', width: 60,
                    formatter: function (value, rec) {
                        return eval("new " + value.substr(1, value.length - 2)).toLocaleDateString();
                    }
                },
                {
                    field: 'UpdateDate', title: '最后更新日期', width: 60,
                    formatter: function (value, rec) {
                        return eval("new " + value.substr(1, value.length - 2)).toLocaleDateString();
                    }
                },
                {
                    field: 'opt', title: '操作', width: 30, align: 'center',
                    formatter: function (value, row, index) {
                        var btn = '<a class="editcls" onclick="editRow(\'' + index + '\',\'' + row.Id + '\')" href="javascript:void(0)">编辑</a>';
                        return btn;
                    }
                }
        ]],
        pagination: true,
        pageSize: 20,
        rownumbers: true,
        toolbar: ['-', {
            id: 'btnSave',
            text: '添加',
            iconCls: 'icon-add',
            handler: function () {
                window.open('/Article/AddOrEdit/', '_self');
                
            }
        }, '-', {
            id: 'btnUpdate',
            text: '修改',
            iconCls: 'icon-save',
            handler: function () {

                var row = $('#grid').datagrid('getSelected');
                if (row) {
                    window.open('/Article/AddOrEdit/' + row.Id + '/', '_self');
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
                        url: "/Article/Delete/",
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
        }, '-'],
        onLoadSuccess: function (data) {
            $('.editcls').linkbutton({ text: '编辑', plain: true, iconCls: 'icon-edit' });
        }
        
    });
    
});
function editRow(index, id) {
    var row = $('#grid').datagrid("getRows")[index];
    if (row && row.Id == id) {
        this.location.href = "/Article/AddOrEdit/" + row.Id;
    }
    else {
        $.messager.alert('提示', '请选择要修改的数据');
        return;
    }
}