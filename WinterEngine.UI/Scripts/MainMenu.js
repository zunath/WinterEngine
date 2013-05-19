
/* Awesomium Events */

function LoginButton() {

    // Disable controls while logging in
    ToggleLoginPopUpControls(true);

    $('#divLoggingInProgressBarContainer').removeClass('clsHidden');
    var loginSuccessful = GlobalJavascriptObject.LoginButtonClick($('#txtUsername').val(), $('#txtPassword').val());
    $('#divLoggingInProgressBarContainer').addClass('clsHidden');
    ToggleLoginPopUpControls(false);

    if (loginSuccessful) {

    }
    else {
        $('#lblErrorMessage').removeClass('clsHidden');
    }
}

function FindServerButton() {
    GlobalJavascriptObject.FindServerButtonClick();
}

function ToolsetButton() {
    GlobalJavascriptObject.ToolsetButtonClick();
}

function SettingsButton() {
    GlobalJavascriptObject.SettingsButtonClick();
}

function WebsiteButton() {
    GlobalJavascriptObject.WebsiteButtonClick();
}

function ForumsButton() {
    GlobalJavascriptObject.ForumsButtonClick();
}

function ExitButton() {
    GlobalJavascriptObject.ExitButtonClick();
}

function SaveProfileButton() {

    // Perform validation checks on data before sending to server
    var username = GlobalJavascriptObject.GetUserName();
    var password = $('#txtProfilePassword').val();
    var confirmPassword = $('#txtConfirmProfilePassword').val();
    var email = $('#txtEmail').val();
    var firstName = $('#txtFirstName').val();
    var lastName = $('#txtLastName').val();
    var dob = $('#txtDOB').val();

    GlobalJavascriptObject.SaveProfileButtonClick(username, password, email, firstName, lastName, dob);
}

/* Account Login */

function OpenLoginBox() {
    $('#divLogin').dialog("open");
}

function CloseLoginBox() {
    $('#divLogin').dialog("close");
    $('#txtUsername').val("");
    $('#txtPassword').val("");
    $('#lblErrorMessage').addClass('clsHidden');
}

/* User Profile */

function CreateAccountOpenBox() {
    $('#lblProfilePassword').text("Password:");
    $('#lblConfirmProfilePassword').text("Confirm Password:");
    $('#txtProfileUsername').removeAttr('disabled');
    $('#txtProfileUsername').removeAttr('readonly', false);

    $('#btnCancelProfile').data('AccountMode', 'create');
    
    CloseLoginBox();
    $('#divUserProfile').dialog("open");
}

function OpenUserProfileBox() {
    $('#lblProfilePassword').text("Change Password:");
    $('#lblConfirmProfilePassword').text("Confirm Change Password:");
    $('#btnCancelProfile').data('AccountMode', 'modify');
    $('#txtProfileUsername').attr('disabled', 'disabled');
    $('#txtProfileUsername').attr('readonly', true);

    $('#txtProfileUsername').val(GlobalJavascriptObject.GetUserName());
    $('#txtEmail').val(GlobalJavascriptObject.GetEmail());
    $('#txtFirstName').val(GlobalJavascriptObject.GetFirstName());
    $('#txtLastName').val(GlobalJavascriptObject.GetLastName());
    $('#txtDOB').val(GlobalJavascriptObject.GetDateOfBirth());

    $('#divUserProfile').dialog("open");
}

function CloseUserProfileBox() {
    // Wipe out data fields
    $('#txtProfileUsername').val("");
    $('#txtProfilePassword').val("");
    $('#txtConfirmProfilePassword').val("");
    $('#txtEmail').val("");
    $('#txtFirstName').val("");
    $('#txtLastName').val("");
    $('#txtDOB').val("");

    var accountMode = $('#btnCancelProfile').data('AccountMode');
    $('#divUserProfile').dialog("close");

    if (accountMode == 'create') {
        OpenLoginBox();
    }
}

/* Page Initialization */
function Initialize() {

    $('#divLogin').dialog({
        modal: true,
        autoOpen: false,
        title: 'Login',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });

    $('#divLoggingInProgressBar').progressbar({
        value: false
    });

    $('#divUserProfile').dialog({
        modal: true,
        autoOpen: false,
        title: 'My Profile',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });

    $('#txtDOB').datepicker({
        changeYear: true,
        yearRange: '1900:+0',
        maxDate : '+0d'
    });
}

/* HELPER METHODS */

function ToggleLoginPopUpControls(disabled) {
    $('#txtUsername').attr('disabled', disabled);
    $('#txtPassword').attr('disabled', disabled);
    $('#chkSaveCredentials').attr('disabled', disabled);
    $('#btnLogin').attr('disabled', disabled);
    $('#btnCancelLogin').attr('disabled', disabled);
    $('#btnCreateAccount').attr('disabled', disabled);

}

/* Logo Links */

function FlatRedBallLogoLink() {
    GlobalJavascriptObject.FlatRedBallLogoLinkClick();
}

function XNALogoLink() {
    GlobalJavascriptObject.XNALogoLinkClick();
}