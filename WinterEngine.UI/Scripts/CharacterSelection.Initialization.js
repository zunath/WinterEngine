
function Initialize() {
    InitializeCharacterSelectionViewModel();
    InitializeLevelProgressBar();

    InitializeDialogBox('#divLoading', 'Loading...');
    InitializeDialogBox('#divLostConnection', 'Connection Lost!');
    InitializeDialogBox('#divDeleteCharacterPopUpBox', 'Confirm Delete Character');
    InitializeDialogBox('#btnDeleteCharacterResponsePopUpBoxOKButton', 'Delete Character');

    $('#divLoading').dialog('open');
}

function InitializeLevelProgressBar() {
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 0
    });
}

function InitializePage() {
    // Request relevant data from server
    Entity.InitializePage();
}

function InitializeServerInformation_Callback(serverName, announcementMessage, canDeleteCharacters, characterList) {
    $('#spnServerName').text(serverName);
    $('#tarServerAnnouncements').text('Announcement: ' + announcementMessage);
    CanDeleteCharacters = canDeleteCharacters

    BuildCharacterList(characterList);

    $('#divLoading').dialog('close');
}