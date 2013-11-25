function Awesomium_LoadPartialViews() {
    
    var partialDivs = $('[data-partial]').filter(
        function () {
            return ($(this).data('partial').length > 0);
        });

    $(partialDivs).each(function (index, value) {
        var partialPath = $(this).data('partial');
        if (partialPath != null && partialPath != undefined && partialPath != "") {
            var html = Entity.GetPartialViewHTML(partialPath);
            $(this).replaceWith(html);
        }
    });
}