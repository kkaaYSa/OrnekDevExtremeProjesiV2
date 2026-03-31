var MainGrid = (function () {

    var gridInstance = null;
    var gridSelector = "#gridContainer";
    var categories = [];

    function init() {
        MainApi.getCategoryList()
            .done(function (categoryData) {
                categories = categoryData || [];
                gridInstance = $(gridSelector).dxDataGrid("instance");
            });
    }

    function onInitialized(e) {
        gridInstance = e.component;
    }

    function refresh() {
        if (gridInstance) {
            gridInstance.refresh();
        }
    }

    function onToolbarPreparing(e) {
        e.toolbarOptions.items.push({
            location: "after",
            widget: "dxButton",
            options: {
                text: "Yeni Kayıt",
                icon: "plus",
                type: "success",
                onClick: function () {
                    MainPopup.open(0);
                }
            }
        });
    }

    function onContextMenuPreparing(e) {
        if (!e.row || e.row.rowType !== "data") {
            return;
        }

        if (!e.items) {
            e.items = [];
        }

        e.items.push(
            {
                text: "Detay Git",
                icon: "info",
                onItemClick: function () {
                    if (window.MainDetail && typeof MainDetail.open === "function") {
                        MainDetail.open(e.row.data.Id);
                    }
                }
            },
            {
                text: "Düzenle",
                icon: "edit",
                onItemClick: function () {
                    MainPopup.open(e.row.data.Id);
                }
            },
            {
                text: "Sil",
                icon: "trash",
                onItemClick: function () {
                    remove(e.row.data.Id);
                }
            },
            {
                text: "Notlar",
                icon: "comment",
                onItemClick: function () {
                    if (window.MainNotes && typeof MainNotes.open === "function") {
                        MainNotes.open(e.row.data.Id);
                    }
                }
            }
        );
    }

    function remove(id) {
        if (!confirm("Silmek istediğine emin misin?")) {
            return;
        }

        MainApi.remove(id)
            .done(function (res) {
                if (res && res.success) {
                    DevExpress.ui.notify(res.message, "info", 3000);
                    refresh();
                    return;
                }

                DevExpress.ui.notify(
                    (res && res.message) ? res.message : "Silme işlemi başarısız.",
                    "error",
                    3000
                );
            })
            .fail(function () {
                DevExpress.ui.notify("Sunucu hatası", "error", 3000);
            });
    }

    return {
        init: init,
        onInitialized: onInitialized,
        onToolbarPreparing: onToolbarPreparing,
        onContextMenuPreparing: onContextMenuPreparing,
        refresh: refresh
    };

})();