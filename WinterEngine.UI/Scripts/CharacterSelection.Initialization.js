
function Initialize() {
    InitializeCharacterSelectionViewModel();
    
    InitializeDialogBox('#divLoading', 'Loading...');
    InitializeDialogBox('#divLostConnection', 'Connection Lost!');
    InitializeDialogBox('#divDeleteCharacterPopUpBox', 'Confirm Delete Character');
    InitializeDialogBox('#btnDeleteCharacterResponsePopUpBoxOKButton', 'Delete Character');

    $('#divLoading').dialog('open');
    $('input:button').button();
    $('.clsProgressBar').progressbar({
        value: false
    });

    Entity.RequestServerInformation();
}

function RequestServerInformation_Callback() {
    CharacterSelectionViewModel.Refresh();

    $('#divLoading').dialog('close');
}