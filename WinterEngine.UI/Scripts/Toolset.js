/* Page Initialization */

function Initialize() {
    InitializeMainMenu();
}

function InitializeMainMenu() {

    $('#ulMenu').menu().hide();

    $('#ulMenu>li').each(function (index, el) {

        $(el).hover(function () {
            $(this).closest('li')
                .find('ul')
                .show()
                .animate({ "opacity": 1 }, 250);
        },
        function () {
            $(this).closest('li')
                .find('ul')
                .animate({ "opacity": 0 }, 250, function () {
                    $(this).hide();
            });
        });
    });
}