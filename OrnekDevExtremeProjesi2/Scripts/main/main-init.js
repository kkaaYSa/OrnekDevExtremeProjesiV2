var MainInit = (function () {

    function init() {
        MainPopup.init();
        MainGrid.init();
    }

    return {
        init: init
    };

})();

$(function () {
    MainInit.init();
});