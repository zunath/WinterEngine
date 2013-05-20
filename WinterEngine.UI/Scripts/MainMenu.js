﻿
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

    if (!$('#formUserProfile').valid()) {
        return false;
    }

    var profileMode = $('#btnSaveProfile').data('AccountMode');

    // Perform validation checks on data before sending to server
    var password = $('#txtProfilePassword').val();
    var confirmPassword = $('#txtConfirmProfilePassword').val();
    var email = $('#txtEmail').val();
    var firstName = $('#txtFirstName').val();
    var lastName = $('#txtLastName').val();
    var dob = $('#txtDOB').val();


    if (profileMode == 'create') {
        var username = $('#txtProfileUsername').val();

        // Note: Refer to UserProfileResponseTypeEnum.cs for values
        var responseType = GlobalJavascriptObject.CreateProfileButtonClick(username, password, confirmPassword, email, firstName, lastName, dob);

        // 1 = Success
        if (responseType == 1) {
            $('#btnCancelProfile').data('AccountMode', '');
            CloseUserProfileBox();
            $('#lblSuccessMessage').text('Account created successfully. Please check your email for an activation link.');
            $('#divSuccessBox').dialog('open');
        }
        // 2 = Username already exists
        else if (responseType == 2) {
            $('#lblProfileError').removeClass('clsHidden');
            $('#lblProfileError').text("Error: Username already exists.");
        }
        // 3 = Invalid password
        else if (responseType == 3) {
            $('#lblProfileError').removeClass('clsHidden');
            $('#lblProfileError').text("Error: Invalid password.");
        }
        // 4 = Failure (unknown reasons)
        else if (responseType == 4) {
            $('#lblProfileError').removeClass('clsHidden');
            $('#lblProfileError').text("Unknown error. Please try again.");
        }
        // 5 = Password mismatch
        else if (responseType == 5) {
            $('#lblProfileError').removeClass('clsHidden');
            $('#lblProfileError').text("Error: Passwords don't match.");
        }
        // 7 = Email already in use
        else if (responseType == 7) {
            $('#lblProfileError').removeClass('clsHidden');
            $('#lblProfileError').text("Error: Email already in use.");
        }

    }
    else if (profileMode == 'modify') {
        GlobalJavascriptObject.SaveProfileButtonClick(username, password, email, firstName, lastName, dob);
    }

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
    $('#btnSaveProfile').val("Create");

    $('#btnSaveProfile').data('AccountMode', 'create');
    $('#btnCancelProfile').data('AccountMode', 'create');
    
    CloseLoginBox();
    $('#divUserProfile').dialog("open");
}

function OpenUserProfileBox() {
    $('#lblProfilePassword').text("Change Password:");
    $('#lblConfirmProfilePassword').text("Confirm Change Password:");

    $('#btnSaveProfile').data('AccountMode', 'modify');
    $('#btnCancelProfile').data('AccountMode', 'modify');
    $('#txtProfileUsername').attr('disabled', 'disabled');
    $('#txtProfileUsername').attr('readonly', true);
    $('#btnSaveProfile').val("Save Changes");

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
    $('#lblProfileError').addClass('clsHidden');

    var accountMode = $('#btnCancelProfile').data('AccountMode');
    $('#divUserProfile').dialog("close");

    if (accountMode == 'create') {
        OpenLoginBox();
    }
}

/* Page Initialization */
function Initialize() {

    InitializeValidation();

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

    $('#divSuccessBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Success',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeValidation() {

    $('#formUserProfile').validate({
        rules: {
            txtEmail: {
                email: true,
                required: true
            }
        }
    });
}

/* SUCCESS BOX */

function CloseSuccessBox() {
    $('#divSuccessBox').dialog('close');
}

function ResendAccountActivationEmail() {
    GlobalJavascriptObject.ResendAccountActivationEmail();
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