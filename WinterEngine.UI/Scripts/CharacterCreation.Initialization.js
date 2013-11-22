function Initialize() {

    $('#divCharacterCreationAccordion').accordion({
        collapsible: true, heightStyle: "content"
    });

    InitializeCharacterCreationViewModel();
    $('input:button').button();
}