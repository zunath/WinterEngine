
function Initialize() {
    InitializeCharacterSelectionViewModel();
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 0
    });

    InitializeDialogBox('#divLoading', 'Loading...');
    InitializeDialogBox('#divLostConnection', 'Connection Lost!');
    InitializeDialogBox('#divDeleteCharacterPopUpBox', 'Confirm Delete Character');
    InitializeDialogBox('#btnDeleteCharacterResponsePopUpBoxOKButton', 'Delete Character');

    // TODO: Re-enable when done setting up layout
    //$('#divLoading').dialog('open');




    $('input:button').button();
    $('#tblCharacterAttributes').dataTable({
        "sDom": 'rtf',
        "bPaginate": false,
        "bFilter": false,
        "bSort": true,
        "bAutoWidth": false,
        //"sPaginationType": 'full_numbers',
        "bJQueryUI": true,
        "bSort": false
    });

    Entity.RequestServerInformation();
}

function RequestServerInformation_Callback(serverName, announcementMessage, canDeleteCharacters, characterList) {
    CharacterSelectionViewModel.Refresh();

    $('#divLoading').dialog('close');
}