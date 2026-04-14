var MainModule = MainModule || {
    currentNoteMainId: 0
};

function onMainGridContextMenu(e) {
    if (e.row && e.row.rowType === "data") {
        if (!e.items) e.items = [];

        e.items.push(
            {
                text: "Detay Git",
                icon: "info",
                onItemClick: function () {
                    MainModule.openDetail(e.row.data.Id);
                }
            },
            {
                text: "Düzenle",
                icon: "edit",
                onItemClick: function () {
                    e.component.editRow(e.row.rowIndex);
                }
            },
            {
                text: "Notlar",
                icon: "comment",
                onItemClick: function () {
                    MainModule.openNotes(e.row.data.Id);
                }
            },
            {
                text: "Sil",
                icon: "trash",
                onItemClick: function () {
                    DevExpress.ui.dialog
                        .confirm("Bu kaydı silmek istediğine emin misin?", "Silme Onayı")
                        .done(function (result) {
                            if (result) {
                                e.component.deleteRow(e.row.rowIndex);
                            }
                        });
                }
            }
        );
    }
}

MainModule.openDetail = function (mainId) {
    $("#recordDetailPopup").remove();
    $("<div id='recordDetailPopup'></div>").appendTo("body");

    var popup = $("#recordDetailPopup").dxPopup({
        title: "Kayıt Detay Ekranı - #" + mainId,
        width: 1100,
        height: 700,
        visible: false,
        showCloseButton: true,
        dragEnabled: true,
        hideOnOutsideClick: true,
        contentTemplate: function (container) {
            container.append(
                "<div id='detailPopupLoader' style='padding:40px; text-align:center;'>Yükleniyor...</div>" +
                "<div id='detailPopupContent' style='display:none;'></div>"
            );
        },
        onShown: function () {
            if (typeof DetailModule !== "undefined" && DetailModule.init) {
                DetailModule.init(mainId);
            }
        },
        onHidden: function () {
            $("#recordDetailPopup").remove();
        }
    }).dxPopup("instance");

    popup.show();

    $.get("/Main/Details", { id: mainId }, function (html) {
        $("#detailPopupContent").html(html).show();
        $("#detailPopupLoader").hide();
    });
};

MainModule.openNotes = function (mainId) {
    MainModule.currentNoteMainId = mainId;
    $("#notesPopup").dxPopup("instance").show();
    loadNotes(mainId);
};

function loadNotes(mainId) {
    $("#notesGrid").dxDataGrid("instance").option("dataSource", {
        load: function () {
            return $.getJSON("/Notes/GetByMainId", { id: mainId });
        }
    });
}

function saveNote() {
    var noteText = $("#noteTextArea").dxTextArea("instance").option("value");
    var mainId = MainModule.currentNoteMainId;

    if (!noteText) {
        DevExpress.ui.notify("Not boş olamaz", "warning", 2000);
        return;
    }

    $.post("/Notes/AddNote", {
        mainId: mainId,
        noteText: noteText,
        __RequestVerificationToken: getAntiForgeryToken()
    }, function (res) {
        DevExpress.ui.notify(res.message, res.success ? "success" : "error", 2500);

        if (res.success) {
            $("#noteTextArea").dxTextArea("instance").option("value", "");
            loadNotes(mainId);
            $("#mainGrid").dxDataGrid("instance").refresh();
        }
    });
}

function deleteNoteRow(e) {
    var noteId = e.row.data.Id;

    DevExpress.ui.dialog.confirm("Bu notu silmek istiyor musun?", "Onay")
        .done(function (result) {
            if (!result) return;

            $.post("/Notes/DeleteNote", {
                id: noteId,
                __RequestVerificationToken: getAntiForgeryToken()
            }, function (res) {
                DevExpress.ui.notify(res.message, res.success ? "success" : "error", 2500);

                if (res.success) {
                    loadNotes(MainModule.currentNoteMainId);
                    $("#mainGrid").dxDataGrid("instance").refresh();
                }
            });
        });
}

function createStatusBadge(icon, hint, extraClass) {
    return $("<div>")
        .addClass("status-badge " + extraClass)
        .attr("title", hint)
        .append($("<i>").addClass("dx-icon dx-icon-" + icon));
}

function statusIconsTemplate(container, options) {
    var data = options.data;
    var wrapper = $("<div>").addClass("status-icons-wrapper");

    if (data.NoteCount > 0) {
        wrapper.append(createStatusBadge("comment", "Not var", "status-note"));
    }

    if (data.DeleteStatus && data.DeleteStatus.indexOf("Bekliyor") !== -1) {
        wrapper.append(createStatusBadge("clock", "Silme onayı bekliyor", "status-waiting"));
    }

    if (data.DeleteStatus === "Reddedildi") {
        wrapper.append(createStatusBadge("close", "Silme reddedildi", "status-rejected"));
    }

    $(container).append(wrapper);
}

function checkRejectedNotifications() {
    $.get("/Main/GetRejectedNotifications", function (res) {
        if (!res || !res.success || !res.data || res.data.length === 0) return;

        res.data.forEach(function (item) {
            var key = "rejected_notification_seen_" + item.Id + "_" + item.LastApprovalDate;

            if (localStorage.getItem(key)) return;

            localStorage.setItem(key, "1");

            var msg = "'" + item.Title + "' silme talebi reddedildi";
            if (item.AdminNote) {
                msg += " | Not: " + item.AdminNote;
            }

            DevExpress.ui.notify(msg, "error", 5000);
        });
    });
}

function addAntiForgeryToken(method, ajaxOptions) {
    if (method === "insert" || method === "update" || method === "delete") {
        ajaxOptions.data = ajaxOptions.data || {};
        ajaxOptions.data.__RequestVerificationToken = getAntiForgeryToken();
    }
}

$(function () {
    checkRejectedNotifications();
});