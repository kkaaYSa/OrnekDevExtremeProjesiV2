var MainDataGrid = (function () {

    var gridInstance = null;
    var selectors = {
        grid: "#gridContainer"
    };

    function init() {
        gridInstance = $(selectors.grid).dxDataGrid("instance");
    }

    function refresh() {
        if (gridInstance) {
            gridInstance.refresh();
        }
    }

    function onInitialized(e) {
        gridInstance = e.component;
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
                    if (window.MainPopup && typeof MainPopup.open === "function") {
                        MainPopup.open(0);
                    } else {
                        DevExpress.ui.notify("Popup modülü henüz bağlanmadı.", "warning", 2000);
                    }
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
                text: "Detay",
                icon: "info",
                onItemClick: function () {
                    if (window.MainDetail && typeof MainDetail.open === "function") {
                        MainDetail.open(e.row.data.Id);
                    } else {
                        console.log("Detay modülü henüz bağlı değil. Id:", e.row.data.Id);
                    }
                }
            },
            {
                text: "Düzenle",
                icon: "edit",
                onItemClick: function () {
                    if (window.MainPopup && typeof MainPopup.open === "function") {
                        MainPopup.open(e.row.data.Id);
                    } else {
                        DevExpress.ui.notify("Popup modülü henüz bağlanmadı.", "warning", 2000);
                    }
                }
            },
            {
                text: "Sil",
                icon: "trash",
                onItemClick: function () {
                    deleteRow(e.row.data.Id);
                }
            }
        );
    }

    function deleteRow(id) {
        if (!confirm("Silmek istediğine emin misin?")) {
            return;
        }

        $.post("/Main/DeleteMain", { id: id })
            .done(function (res) {
                if (res.success) {
                    DevExpress.ui.notify(res.message, "success", 3000);
                    refresh();
                    return;
                }

                DevExpress.ui.notify(res.message || "Silme işlemi başarısız.", "error", 3000);
            })
            .fail(function () {
                DevExpress.ui.notify("Sunucu hatası oluştu.", "error", 3000);
            });
    }

    return {
        init: init,
        refresh: refresh,
        onInitialized: onInitialized,
        onToolbarPreparing: onToolbarPreparing,
        onContextMenuPreparing: onContextMenuPreparing
    };

})();