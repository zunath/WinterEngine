/* Page Initialization */
var CanDeleteCharacters = false;
var SelectedCharacterDiv;

function Initialize() {
    InitializeLoadingPopUpBox();
    InitializeLostConnectionPopUpBox();
    InitializeLevelProgressBar();
    InitializeDeleteCharacterPopUpBox();
    InitializeDeleteCharacterResponseBox();
}

function InitializeLevelProgressBar() {
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 0
    });
}

function InitializeServerInformation_Callback(serverName, announcementMessage, canDeleteCharacters, characterList) {
    $('#spnServerName').text(serverName);
    $('#tarServerAnnouncements').text('Announcement: ' + announcementMessage);
    CanDeleteCharacters = canDeleteCharacters

    BuildCharacterList(characterList);

    $('#divLoading').dialog('close');
}

function InitializePage() {
    Entity.InitializePage();
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

function InitializeDeleteCharacterResponseBox() {
    $('#btnDeleteCharacterResponsePopUpBoxOKButton').dialog({
        modal: true,
        autoOpen: false,
        title: 'Delete Character',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeLostConnectionPopUpBox() {
    $('#divLostConnection').dialog({
        modal: true,
        autoOpen: false,
        title: 'Connection Lost!',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false,
        closeOnEscape: false
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
    $('#lblDeleteCharacterConfirmation').text('Are you sure you want to permanently delete your character?');
    $('#divDeleteCharacterPopUpBox').dialog('open');
}

function ConfirmDeleteCharacterButton() {
    var fileName = $(SelectedCharacterDiv).data('fileName');
    Entity.DeleteCharacter(fileName);
}

function ConfirmDeleteCharacterButton_Callback(responseID) {
    // Response IDs are found in the DeleteCharacterTypeEnum.cs file

    $('#divDeleteCharacterPopUpBox').dialog('close');

    // 2 = Accepted
    if (responseID == 2) {
        $('.clsCharacterActive').hide();
        return;
    }
    // 3 = Denied
    else if (responseID == 3) {
        $('#lblDeleteCharacterResponsePopUpBox').text('Your request to delete your character has been denied.');
    }
    // 4 = DeniedDisabled
    else if (responseID == 4) {
        $('#lblDeleteCharacterResponsePopUpBox').text('The server has disabled character deletion.');
    }
    // 5 = FileNotFound
    else if (responseID == 5) {
        $('#lblDeleteCharacterResponsePopUpBox').text('Character does not exist.');
    }
    // 6 = Error
    else if (responseID == 6) {
        $('#lblDeleteCharacterResponsePopUpBox').text('An error occurred. Your character has not been deleted.');
    }

    $('#divDeleteCharacterResponsePopUpBox').dialog('open');
}

function CloseDeleteCharacterResponsePopUpBox() {
    $('#btnDeleteCharacterResponsePopUpBoxOKButton').dialog('close');
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

function LoadCharacterInformation(characterID) {

    SelectedCharacterDiv = $('#divCharacter' + characterID);
    $('.clsCharacterActive').removeClass('clsCharacterActive');
    $(selectedCharacter).addClass('clsCharacterActive');

    $('#spnCharacterName').text($(selectedCharacter).data('name'));
    $('#spnCharacterRace').text($(selectedCharacter).data('race'));

    $('#tdStrengthValue').text($(selectedCharacter).data('str'));
    $('#tdConstitutionValue').text($(selectedCharacter).data('con'));
    $('#tdDexterityValue').text($(selectedCharacter).data('dex'));
    $('#tdIntelligenceValue').text($(selectedCharacter).data('int'));
    $('#tdWisdomValue').text($(selectedCharacter).data('wis'));

    if (CanDeleteCharacters) {
        $('#btnDeleteCharacter').removeAttr('disabled');
    }

    $('#btnJoinServer').removeAttr('disabled');
}

/* Character List Creation */

function BuildCharacterList(characterList) {
    var characters = JSON.parse(characterList);

    for (var index = 0; index < characters.length; index++) {
        var currentCharacter = characters[index];

        var divContent = '<div id="divCharacter' + index + '" class="clsCharacterEntry">';

        divContent += '<img src="" class="clsCharacterPortrait" />';
        divContent += '<span id="spnCharacterName' + index + '"></span>';
        divContent += '<br />';
        divContent += '</div>';

        $('#divCharacters').append(divContent);

        var selector = '#spnCharacterName' + index;
        $(selector).text(currentCharacter.FirstName + ' ' + currentCharacter.LastName);

        selector = '#divCharacter' + index;
        $(selector).data('name', currentCharacter.FirstName + ' ' + currentCharacter.LastName);
        $(selector).data('race', currentCharacter.Race); 

        $(selector).data('str', '1');
        $(selector).data('con', '2');
        $(selector).data('dex', '3');
        $(selector).data('int', '4');
        $(selector).data('wis', '5');

        $(selector).data('fileName', currentCharacter.FileName);

        $(selector).click(LoadCharacterInformation(index));
    }
}

/* Lost Connection */

function LostConnectionOKButton() {
    Entity.CancelCharacterSelection();
}

function DisplayLostConnectionBox() {
    $('#divLostConnection').dialog('open');
}