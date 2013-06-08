/* Page Initialization */
var ServerInformationReceived = false;
var CharacterListReceived = false;

function Initialize() {
    InitializeLoadingPopUpBox();
    InitializeLevelProgressBar();
    InitializeDeleteCharacterPopUpBox();
    InitializePage();
}

function InitializeLevelProgressBar() {
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 0
    });
}

function InitializeServerInformation_Callback(serverName, announcementMessage) {
    $('#spnServerName').text(serverName);
    $('#tarServerAnnouncements').text('Announcement: ' + announcementMessage);
}

function InitializePage() {
    Entity.InitializeCharacterList();
}

function InitializePage_Callback() {
}

function InitializeDeleteCharacterPopUpBox() {
    $('#divDeleteCharacterPopUpBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Confirm Delete Character',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeLoadingPopUpBox() {
    $('#divLoading').dialog({
        modal: true,
        autoOpen: false,
        title: 'Loading...',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false,
        closeOnEscape: false
    });
}

function InitializeLoadingPopUpBox_Callback() {
    $('#divLoading').dialog('open');
}

/* Button Functionality */

function NewCharacterButton() {
    Entity.NewCharacter();
}

function DeleteCharacterButton() {
    $('#divDeleteCharacterPopUpBox').dialog('open');
}

function ConfirmDeleteCharacterButton() {
    Entity.DeleteCharacter();
}

function JoinServerButton() {
    Entity.JoinServer();
}

function CancelCharacterSelectionButton() {
    Entity.CancelCharacterSelection();
}

function CancelDeleteCharacterButton() {
    $('#divDeleteCharacterPopUpBox').dialog('close');
}

function LoadCharacterInformation(name, race, str, con, dex, int, wis, exp, maxExp) {
    $('#spnCharacterName').text(name);
    $('#spnCharacterRace').text(race);

    $('#tdStrengthValue').text(str);
    $('#tdConstitutionValue').text(con);
    $('#tdDexterityValue').text(dex);
    $('#tdIntelligenceValue').text(int);
    $('#tdWisdomValue').text(wis);
}