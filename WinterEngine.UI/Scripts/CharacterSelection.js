/* Page Initialization */

function Initialize() {

    InitializeLevelProgressBar();
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

function LoadCharacterInformation(name, race, str, con, dex, int, wis, exp, maxExp) {

}

/* Button Functionality */

function NewCharacterButton() {
    Entity.NewCharacter();
}

function DeleteCharacterButton() {
    Entity.DeleteCharacter();
}

function JoinServerButton() {
    Entity.JoinServer();
}

function CancelCharacterSelectionButton() {
    Entity.CancelCharacterSelection();
}