
/* Page Initialization */
function Initialize() {
    InitializeValidation();
    InitializeLoginBox();
    InitializeLogoutBox();
    InitializeUserProfileBox();
    InitializeSuccessBox();
    InitializeAccountNotActivatedBox();
}

// Method is called from the entity's OnDocumentReady method.
function CheckIfLoggedIn() {
    var isLoggedIn = Entity.GetIsLoggedIn();
    ToggleIsLoggedIn(isLoggedIn);
}

/* Account login/logout */

function LoginButton() {

    var mode = $('#btnLoginLogout').data('mode');
    $('#lblErrorMessage').addClass('clsHidden');

    if (mode == 'login') {
        OpenLoginBox();
    }
    else if (mode == 'logout') {
        ShowLogoutConfirmation();
    }
}


/* Other main menu buttons */

function FindServerButton() {
    Entity.FindServerButtonClick();
}

function ToolsetButton() {
    Entity.ToolsetButtonClick();
}

function SettingsButton() {
    Entity.SettingsButtonClick();
}

function WebsiteButton() {
    Entity.WebsiteButtonClick();
}

function ForumsButton() {
    Entity.ForumsButtonClick();
}

function ExitButton() {
    Entity.ExitButtonClick();
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


    var username = $('#txtProfileUsername').val();

    // Note: Refer to UserProfileResponseTypeEnum.cs for values
    var responseType;

    if (profileMode == 'create') {
        responseType = Entity.UpsertProfileButtonClick(username, password, confirmPassword, email, firstName, lastName, dob, true);
    }
    else if (profileMode == 'modify') {
        responseType = Entity.UpsertProfileButtonClick(username, password, confirmPassword, email, firstName, lastName, dob, false);
    }


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

function ToggleIsLoggedIn(isLoggedIn) {

    if (isLoggedIn) {
        $('#btnProfile').removeAttr('disabled');
        $('#btnFindServer').removeAttr('disabled');
        $('#btnLoginLogout').val('Logout');
        $('#btnLoginLogout').data('mode', 'logout');
    }
    else {
        $('#btnProfile').attr('disabled', 'disabled');
        $('#btnFindServer').attr('disabled', 'disabled');
        $('#btnLoginLogout').val('Login');
        $('#btnLoginLogout').data('mode', 'login');
    }
}

function InitializeLogoutBox() {
    $('#divConfirmLogout').dialog({
        modal: true,
        autoOpen: false,
        title: 'Logout?',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
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

function DoLogin() {

    if (!$('#formLogin').valid()) {
        return false;
    }

    // Disable controls while logging in
    ToggleLoginPopUpControls(true);

    $('#divLoggingInProgressBarContainer').removeClass('clsHidden');
    var loginStatus = Entity.LoginButtonClick($('#txtUsername').val(), $('#txtPassword').val());
    $('#divLoggingInProgressBarContainer').addClass('clsHidden');
    ToggleLoginPopUpControls(false);

    // 1 = Login succeeded
    if (loginStatus == 1) {
        CloseLoginBox();
        ToggleIsLoggedIn(true);
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

function ShowLogoutConfirmation() {
    $('#divConfirmLogout').dialog('open');
}

function DoLogout() {
    Entity.LogoutButtonClick();

    $('#btnProfile').attr('disabled', 'disabled');
    $('#btnFindServer').attr('disabled', 'disabled');
    $('#btnLoginLogout').val('Login');
    $('#btnLoginLogout').data('mode', 'login');
    CloseConfirmLogoutBox();
}

function CloseConfirmLogoutBox() {
    $('#divConfirmLogout').dialog('close');
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

    $('#txtProfileUsername').val(Entity.GetUserName());
    $('#txtEmail').val(Entity.GetEmail());
    $('#txtFirstName').val(Entity.GetFirstName());
    $('#txtLastName').val(Entity.GetLastName());
    $('#txtDOB').val(Entity.GetDateOfBirth());

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
    var email = Entity.GetEmail();
    $('#btnAccountNotActivatedResendEmail').attr('disabled', 'disabled');
    $('#lblAccountNotActivatedMessage').text('Activation email has been resent to ' + email);
    Entity.ResendAccountActivationEmail();
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
    Entity.FlatRedBallLogoLinkClick();
}

function XNALogoLink() {
    Entity.XNALogoLinkClick();
}