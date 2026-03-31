var MainForm = (function () {

    var formInstance = null;
    var formSelector = "#mainForm";

    function render(containerSelector, model, categories) {
        $(containerSelector).empty();

        $("<div id='mainForm'>").appendTo(containerSelector);

        formInstance = $(formSelector).dxForm({
            formData: model || getEmptyModel(),
            colCount: 1,
            labelLocation: "top",
            items: [
                {
                    dataField: "Id",
                    visible: false
                },
                {
                    dataField: "Title",
                    label: { text: "Başlık" },
                    validationRules: [
                        { type: "required", message: "Başlık boş bırakılamaz!" }
                    ]
                },
                {
                    dataField: "Description",
                    label: { text: "Açıklama" },
                    editorType: "dxTextArea",
                    editorOptions: {
                        height: 100
                    },
                    validationRules: [
                        { type: "required", message: "Açıklama boş bırakılamaz!" }
                    ]
                },
                {
                    dataField: "CategoryId",
                    label: { text: "Kategori" },
                    editorType: "dxSelectBox",
                    editorOptions: {
                        dataSource: categories || [],
                        displayExpr: "Name",
                        valueExpr: "Id",
                        searchEnabled: true,
                        placeholder: "Kategori seçiniz"
                    },
                    validationRules: [
                        { type: "required", message: "Kategori boş bırakılamaz!" }
                    ]
                },
                {
                    dataField: "IsActive",
                    editorType: "dxSwitch",
                    label: { text: "Aktiflik" }
                }
            ]
        }).dxForm("instance");
    }

    function validate() {
        if (!formInstance) {
            return { isValid: false };
        }

        return formInstance.validate();
    }

    function getData() {
        if (!formInstance) {
            return null;
        }

        return formInstance.option("formData");
    }

    function getEmptyModel() {
        return {
            Id: 0,
            Title: "",
            Description: "",
            CategoryId: null,
            IsActive: true
        };
    }

    return {
        render: render,
        validate: validate,
        getData: getData,
        getEmptyModel: getEmptyModel
    };

})();