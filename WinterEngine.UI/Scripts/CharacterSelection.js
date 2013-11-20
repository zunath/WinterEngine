
function NewCharacterButton() {
    Entity.NewCharacter();
}

function DeleteCharacterButton() {
    $('#lblDeleteCharacterConfirmation').text('Are you sure you want to permanently delete your character?');
    $('#divDeleteCharacterPopUpBox').dialog('open');
}

function ConfirmDeleteCharacterButton() {
    $('#lblDeleteCharacterConfirmation').text('Deleting character...');
    $('#divDeleteCharacterLoadingBar').removeClass('clsHidden');

    $('#btnConfirmDeleteCharacter').button('disable');
    $('#btnCancelDeleteCharacter').button('disable');

    Entity.DeleteCharacter();
}

function DeleteCharacter_Callback() {
    CharacterSelectionViewModel.Refresh();

    $('#divDeleteCharacterLoadingBar').addClass('clsHidden');
    $('#btnConfirmDeleteCharacter').button('enable');
    $('#btnCancelDeleteCharacter').button('enable');

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