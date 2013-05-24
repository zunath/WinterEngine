
/* Page Initialization */
function Initialize() {
    InitializeValidation();
    InitializeLoginBox();
    InitializeUserProfileBox();
    InitializeSuccessBox();
    InitializeAccountNotActivatedBox();
}

/* Awesomium Events */

function LoginButton() {

    if (!$('#formLogin').valid()) {
        return false;
    }

    // Disable controls while logging in
    ToggleLoginPopUpControls(true);

    $('#divLoggingInProgressBarContainer').removeClass('clsHidden');
    var loginStatus = GlobalJavascriptObject.LoginButtonClick($('#txtUsername').val(), $('#txtPassword').val());
    $('#divLoggingInProgressBarContainer').addClass('clsHidden');
    ToggleLoginPopUpControls(false);

    // 1 = Login succeeded
    if (loginStatus == 1) {

    }
    // 8 = Account not activated
    else if (loginStatus == 8) {
        CloseLoginBox();
        $('#divAccountNotActivatedBox').dialog('open');
    }
    // 3 = Invalid password
    else if (loginStatus == 3) {
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

/* Form Validation */

function InitializeValidation() {

    $('#formUserProfile').validate({
        errorPlacement: $.noop, // Removes the text saying "this field is required"

        rules: {
            txtEmail: {
                email: true,
                required: true
            }
        }
    });

    $('#formLogin').validate({
        errorPlacement: $.noop
    });

    

}

/* Account Login */

function InitializeLoginBox() {
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
}

function OpenLoginBox() {
    $('#divLogin').dialog("open");
}

function CloseLoginBox() {
    $('#divLogin').dialog("close");
    $('#txtUsername').val("");
    $('#txtPassword').val("");
    $('#lblErrorMessage').addClass('clsHidden');

    $('.error').removeClass('error');
}

/* User Profile */

function InitializeUserProfileBox() {
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
        maxDate: '+0d'
    });
}

function CreateAccountOpenBox() {
    $('#lblProfilePassword').text("Password:");
    $('#lblConfirmProfilePassword').text("Confirm Password:");
    $('#txtProfileUsername').removeAttr('disabled');
    $('#txtProfileUsername').removeAttr('readonly', false);
    $('#btnSaveProfile').val("Create");

    $('#btnSaveProfile').data('AccountMode', 'create');
    $('#btnCancelProfile').data('AccountMode', 'create');

    CloseLoginBox();

    $('#txtProfilePassword').addClass('required');
    $('#txtConfirmProfilePassword').addClass('required');

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

    $('#txtProfilePassword').removeClass('required');
    $('#txtConfirmProfilePassword').removeClass('required');

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

    $('.error').removeClass('error');

    var accountMode = $('#btnCancelProfile').data('AccountMode');
    $('#divUserProfile').dialog("close");

    if (accountMode == 'create') {
        OpenLoginBox();
    }
}

/* SUCCESS BOX */

function InitializeSuccessBox() {
    $('#divSuccessBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Success',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function CloseSuccessBox() {
    $('#divSuccessBox').dialog('close');
}

/* ACCOUNT NOT ACTIVATED BOX */

function InitializeAccountNotActivatedBox() {
    $('#divAccountNotActivatedBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Account Inactive',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function CloseAccountNotActivatedBox() {
    $('#divAccountNotActivatedBox').dialog('close');
}

function ResendAccountActivationEmail() {
    var email = GlobalJavascriptObject.GetEmail();
    $('#btnAccountNotActivatedResendEmail').attr('disabled', 'disabled');
    $('#lblAccountNotActivatedMessage').text('Activation email has been resent to ' + email);
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