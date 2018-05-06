$(function () {
    var _articleCategoryService = abp.services.app.articleCategory;

    $("#btnCreate").click(function (e) {
        $.ajax({
            url: abp.appPath + "ArticleCategory/Create",
            type: "GET",
            contentType: "application/html",
            success: function (content) {
                $("#CreateArticleCategoryModal div.modal-content").html(content);
            },
            error: function (e) { }
        });
    });

    InitTable();

    $("#resultTable").on("xhr.dt", function () {
        $("#btnSearch").button("reset");
    });

    $("#btnExport").on("click", function () {
        $("#btnExport").button("loading");
        var xhr = new XMLHttpRequest();
        xhr.open("get", $(this).data("url") + "?" + $("#articleCategoryForm").serialize(), true);
        xhr.responseType = "blob";
        xhr.onload = function (e) {
            $("#btnExport").button("reset");
            if (xhr.status === 200) {
                var blob = new Blob([xhr.response], { type: "application/vnd.ms-excel" });
                var downloadUrl = URL.createObjectURL(blob);
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = "ArticleCategory" + Date.now() + ".xlsx";
                document.body.appendChild(a);
                a.click();
            } else {
                abp.message.alert("导出Excel失败！")
            }
        };
        xhr.send();
    });

    $("#resultTable").on("click", ".edit-articlecategory", function (e) {
        var articleCategoryId = $(this).data("articlecategory-id");
        e.preventDefault();
        $.ajax({
            url: abp.appPath + "ArticleCategory/Edit?articleCategoryId=" + articleCategoryId,
            type: "get",
            contentType: "application/html",
            success: function (content) {
                $("#EditArticleCategoryModal div.modal-content").html(content);
            },
            error: function (e) { }
        });
    });

    $("#resultTable").on("click", ".delete-articlecategory", function () {
        var articleCategoryId = $(this).data("articlecategory-id");
        var articleCategoryName = $(this).data("articlecategory-name");
        abp.message.confirm(
            "确认删除文章分类 " + articleCategoryName + " ?",
            function (isConfirmed) {
                if (isConfirmed) {
                    _articleCategoryService.delete({
                        id: articleCategoryId
                    }).done(function () {
                        location.reload(true);
                    });
                }
            }
        );
    });
});

function InitTable() {
    var table = $("#resultTable").dataTable({
        "serverSide": true,
        "ordering": true,
        "ajax": {
            "url": $("#articleCategoryForm")[0].action,
            "type": "post",
            "contentType": "application/json; charset=utf-8",
            "data": function (data) {
                var params = $("#articleCategoryForm").serializeArray();
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
                "data": "name",
                "class": "align-center"
            },
            {
                "data": "desc",
                "class": "align-center"
            },
            {
                "data": "creationTime",
                "class": "align-center",
                "render": function (data) {
                    return new Date(data).toLocaleString("zh-cn", { hour12: false });
                }
            },
            {
                "data": "lastModificationTime",
                "class": "align-center",
                "render": function (data) {
                    if (data === null) {
                        return "无数据";
                    } else
                        return new Date(data).toLocaleString("zh-cn", { hour12: false })
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
}