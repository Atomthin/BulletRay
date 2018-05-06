﻿(function ($) {
    var _articleCategoryService = abp.services.app.articleCategory;
    var _$modal = $('#EditArticleCategoryModal');
    var _$form = $('form[name=EditArticleCategoryForm]');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var articleCategory = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _articleCategoryService.update(articleCategory).done(function () {
            _$modal.modal('hide');
            location.reload(true);
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);