
function NewCharacterButton() {
    Entity.NewCharacter();
}

function DeleteCharacterButton() {
    $('#lblDeleteCharacterConfirmation').text('Are you sure you want to permanently delete your character?');
    $('#divDeleteCharacterPopUpBox').dialog('open');
}

function ConfirmDeleteCharacterButton() {
    Entity.DeleteCharacter();
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

function LoadCharacterInformation(index) {
    Entity.LoadCharacter(index);
}

function LoadCharacterInformation_Callback() {

    if (CharacterSelectionViewModel.CanDeleteCharacters()) {
        $('#btnDeleteCharacter').button('enable');
    }

    $('#btnJoinServer').button('enable');

    CharacterSelectionViewModel.Refresh();
}

/* Lost Connection */

function LostConnectionOKButton() {
    Entity.CancelCharacterSelection();
}

function DisplayLostConnectionBox() {
    $('#divLostConnection').dialog('open');
}