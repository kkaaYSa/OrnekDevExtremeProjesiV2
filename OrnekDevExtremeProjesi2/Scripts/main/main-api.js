var MainApi = (function () {

    function getById(id) {
        return $.ajax({
            url: "/Main/GetById",
            type: "GET",
            data: { id: id }
        });
    }

    function create(model) {
        return $.ajax({
            url: "/Main/CreateMain",
            type: "POST",
            data: model
        });
    }

    function update(model) {
        return $.ajax({
            url: "/Main/UpdateMain",
            type: "POST",
            data: model
        });
    }

    function remove(id) {
        return $.ajax({
            url: "/Main/DeleteMain",
            type: "POST",
            data: { id: id }
        });
    }

    function getCategoryList() {
        return $.ajax({
            url: "/Main/GetCategoryList",
            type: "GET"
        });
    }

    function getRejectedNotifications() {
        return $.ajax({
            url: "/Main/GetRejectedNotifications",
            type: "GET"
        });
    }

    return {
        getById: getById,
        create: create,
        update: update,
        remove: remove,
        getCategoryList: getCategoryList,
        getRejectedNotifications: getRejectedNotifications
    };

})();