$(function () {
    GetSelectData();
    InitFileInput();
    InitTinyMce();
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
            $("#categorySelect").append(optionStr);
            $("#categorySelect").selectpicker("refresh");
        },
        error: function (e) { }
    });
}

function InitTinyMce() {
    tinymce.init({
        selector: "#tinyMce",
        theme: "modern",
        height: 300,
        plugins: [
            'advlist autolink lists link image charmap print preview hr anchor pagebreak',
            'searchreplace wordcount visualblocks visualchars code fullscreen',
            'insertdatetime media nonbreaking save table contextmenu directionality',
            'emoticons template paste textcolor colorpicker textpattern imagetools'
        ],
        toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
        toolbar2: 'print preview media | forecolor backcolor emoticons',
        image_advtab: true,
        language: "zh_CN"
    });
    tinymce.suffix = ".min";
}

function InitFileInput() {
    $("#inputAttachment").fileinput({
        uploadAsync: false,
        maxFileSize: 102400,
        language: "zh",
        theme: "fa",
        allowedFileExtensions: ["jpg", "png", "jpeg", "zip"],
        showUpload: false,
        showCaption: false,
        browseClass: "btn btn-sm btn-primary",
        fileActionSettings: { showRemove: true, showUpload: false, showZoom: false },
        uploadUrl: "#",
        dropZoneTitle: "添加文章封面图片!! 拖拽文件到这里, 只支持 jpg, png, jpeg, zip的文件!"
    });
}