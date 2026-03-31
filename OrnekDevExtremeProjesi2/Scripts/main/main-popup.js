var MainPopup = (function () {

    var popupInstance = null;
    var popupSelector = "#popupContainer";
    var formHostSelector = "#mainFormHost";
    var currentId = 0;
    var categories = [];

    function init() {
        popupInstance = $(popupSelector).dxPopup({
            title: "Kayıt Formu",
            width: 500,
            visible: false,
            showCloseButton: true,
            hideOnOutsideClick: false,
            contentTemplate: function (contentElement) {
                $("<div id='mainFormHost'>").appendTo(contentElement);
            },
            toolbarItems: [
                {
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "after",
                    options: {
                        text: "Kaydet",
                        type: "success",
                        onClick: function () {
                            save();
                        }
                    }
                },
                {
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "after",
                    options: {
                        text: "Vazgeç",
                        onClick: function () {
                            close();
                        }
                    }
                }
            ]
        }).dxPopup("instance");
    }

    function open(id) {
        currentId = id || 0;

        popupInstance.option(
            "title",
            currentId > 0 ? "Veri Düzenleme Ekranı" : "Yeni Kayıt Oluştur"
        );

        popupInstance.show();
        loadForm();
    }

    function loadForm() {
        MainApi.getCategoryList()
            .done(function (categoryData) {
                categories = categoryData || [];

                if (currentId > 0) {
                    MainApi.getById(currentId)
                        .done(function (res) {
                            if (res && res.success && res.data) {
                                MainForm.render(formHostSelector, res.data, categories);
                                return;
                            }

                            DevExpress.ui.notify("Kayıt verisi alınamadı.", "error", 3000);
                        })
                        .fail(function () {
                            DevExpress.ui.notify("Kayıt verisi yüklenirken hata oluştu.", "error", 3000);
                        });

                    return;
                }

                MainForm.render(formHostSelector, MainForm.getEmptyModel(), categories);
            })
            .fail(function () {
                DevExpress.ui.notify("Kategori listesi yüklenemedi.", "error", 3000);
            });
    }

    function save() {
        var validationResult = MainForm.validate();

        if (!validationResult.isValid) {
            return;
        }

        var formData = MainForm.getData();
        var request = (formData.Id && formData.Id > 0)
            ? MainApi.update(formData)
            : MainApi.create(formData);

        request
            .done(function (res) {
                if (res && res.success) {
                    close();
                    MainGrid.refresh();
                    DevExpress.ui.notify(res.message, "success", 3000);
                    return;
                }

                DevExpress.ui.notify(
                    (res && res.message) ? res.message : "İşlem başarısız.",
                    "error",
                    4000
                );
            })
            .fail(function () {
                DevExpress.ui.notify("Sunucu hatası", "error", 4000);
            });
    }

    function close() {
        if (popupInstance) {
            popupInstance.hide();
        }
    }

    return {
        init: init,
        open: open,
        close: close
    };

})();