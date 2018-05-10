$(function () {
    GetSelectData();
    InitTable();
});

function GetSelectData() {
    $.ajax({
        url: abp.appPath + "ArticleCategory/GetArticleCategorySelectData",
        type: "GET",
        data: "isOpenShown=true",
        contentType: "application/json",
        success: function (data) {
            var optionStr = "";
            $.each(data, function (key, value) {
                var selectedStr = value.isSelected ? "selected" : "";
                optionStr += "<option data-subtext=" + value.desc + " value=" + value.id + selectedStr + ">" + value.name + "</option>";
            });
            $(".selectpicker").append(optionStr);
            $(".selectpicker").selectpicker("refresh");
        },
        error: function (e) { }
    });
}

function InitTable() {
    var table = $("#resultTable").dataTable({
        "serverSide": true,
        "ordering": true,
        "ajax": {
            "url": $("#articleForm")[0].action,
            "type": "post",
            "contentType": "application/json; charset=utf-8",
            "data": function (data) {
                var params = $("#articleForm").serializeArray();
                $.each(params, function (i, field) {
                    data[field.name] = field.value;
                });
                return JSON.stringify(data);
            }
        },
        "lengthMenu": [5, 10, 25, 50, 75, 100],
        "pageLength": 10,
        "processing": true,
        "searching": false,
        "language": {
            "processing": "加载中...",
            "lengthMenu": "每页显示 _MENU_ 条数据",
            "zeroRecords": "没有匹配结果",
            "info": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "infoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "infoFiltered": "(由 _MAX_ 项结果过滤)",
            "infoPostFix": "",
            "emptyTable": "没有匹配结果",
            "loadingRecords": "载入中...",
            "thousands": ",",
            "paginate": {
                "first": "首页",
                "previous": "上一页",
                "next": "下一页",
                "last": "末页"
            }
        },
        "columns": [
            {
                "data": "id",
                "class": "align-center"
            },
            {
                "data": "categoryName",
                "class": "align-center",
                "orderable": false
            },
            {
                "data": "title",
                "class": "align-center"
            },
            {
                "data": "shortDesc",
                "class": "align-center"
            },
            {
                "data": "creationTime",
                "class": "align-center",
                "render": function (data) {
                    return new Date(data).toLocaleString("zh-cn", { hour12: false });
                }
            }
        ],
        "columnDefs": [
            {
                "targets": 5,
                "data": "id",
                "class": "dropdown align-center",
                "render": function (data, type, row, meta) {
                    return "<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'><i class='material-icons'>menu</i></a><ul class='dropdown-menu pull-right'><li><a href='#' class='waves-effect waves-block edit-articlecategory' data-articlecategory-id='" + data + "' data-toggle='modal' data-target='#EditArticleCategoryModal'><i class='material-icons'>edit</i>编辑</a></li><li><a href='#' class='waves-effect waves-block delete-articlecategory' data-articlecategory-id='" + data + "' data-articlecategory-name='" + row.name + "'><i class='material-icons'>delete_sweep</i>删除</a></li></ul>";
                }
            }
        ]
    });

    $("#btnSearch").on("click", function () {
        $("#btnSearch").button("loading");
        table.api().ajax.reload();
    });

    $("#resultTable").on("xhr.dt", function () {
        $("#btnSearch").button("reset");
    });
}