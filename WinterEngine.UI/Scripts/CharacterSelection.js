
function NewCharacterButton() {
    Entity.NewCharacter();
}

function DeleteCharacterButton() {
    $('#lblDeleteCharacterConfirmation').text('Are you sure you want to permanently delete your character?');
    $('#divDeleteCharacterPopUpBox').modal('show');
}

function ConfirmDeleteCharacterButton() {
    $('#lblDeleteCharacterConfirmation').text('Deleting character...');
    $('#divDeleteCharacterLoadingBar').removeClass('clsHidden');

    $('#btnConfirmDeleteCharacter').attr('disable', 'disabled');
    $('#btnCancelDeleteCharacter').attr('disabled', 'disabled');

    Entity.DeleteCharacter();
}

function DeleteCharacter_Callback() {
    CharacterSelectionViewModel.Refresh();

    $('#divDeleteCharacterLoadingBar').addClass('clsHidden');
    $('#btnConfirmDeleteCharacter').removeAttr('disabled');
    $('#btnCancelDeleteCharacter').removeAttr('disabled');

    $('#divDeleteCharacterResponsePopUpBox').modal('show');
}

function JoinServerButton() {
    Entity.JoinServer();
}

function CancelCharacterSelectionButton() {
    Entity.CancelCharacterSelection();
}

function LoadCharacterInformation(index) {
    Entity.LoadCharacter(index);
}

function LoadCharacterInformation_Callback() {

    if (CharacterSelectionViewModel.CanDeleteCharacters()) {
        $('#btnDeleteCharacter').removeAttr('disabled');
    }

    $('#btnJoinServer').removeAttr('disabled');

    CharacterSelectionViewModel.Refresh();
}

function SelectCharacter(index) {
    CharacterSelectionViewModel.ActiveCharacterIndex(index);
}

/* Lost Connection */

function LostConnectionOKButton() {
    Entity.CancelCharacterSelection();
}

function DisplayLostConnectionBox() {
    $('#divLostConnection').modal('show');
}