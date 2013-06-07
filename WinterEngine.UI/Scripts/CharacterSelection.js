/* Page Initialization */

function Initialize() {
    InitializeLevelProgressBar();
    InitializeDeleteCharacterPopUpBox();
    InitializeServerInformation();
    InitializeCharacterList();
}

function InitializeLevelProgressBar() {
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 0
    });
}

function InitializeServerInformation() {
    Entity.InitializeServerInformation();
}

function InitializeServerInformation_Callback(serverName, announcementMessage) {
    $('#spnServerName').text(serverName);
    $('#tarServerAnnouncements').text('Announcement: ' + announcementMessage);
}

function InitializeCharacterList() {
    Entity.InitializeCharacterList();
}

function InitializeCharacterList_Callback() {
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

function LoadCharacterInformation(name, race, str, con, dex, int, wis, exp, maxExp) {
    $('#spnCharacterName').text(name);
    $('#spnCharacterRace').text(race);

    $('#tdStrengthValue').text(str);
    $('#tdConstitutionValue').text(con);
    $('#tdDexterityValue').text(dex);
    $('#tdIntelligenceValue').text(int);
    $('#tdWisdomValue').text(wis);
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
