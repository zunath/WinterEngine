
/* Page Initialization */
function Initialize() {
    InitializeValidation();
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

    $('#divProfileProgressBarContainer').removeClass('clsHidden');
    var profileMode = $('#btnSaveProfile').data('AccountMode');

    // Perform validation checks on data before sending to server
    var password = $('#txtProfilePassword').val();
    var confirmPassword = $('#txtConfirmProfilePassword').val();
    var email = $('#txtEmail').val();
    var firstName = $('#txtFirstName').val();
    var lastName = $('#txtLastName').val();
    var dob = $('#txtDOB').val();
    var username = $('#txtProfileUsername').val();

    if (profileMode == 'create') {
        responseType = Entity.SaveUserProfileClick(username, password, confirmPassword, email, firstName, lastName, dob, true);
    }
    else if (profileMode == 'modify') {
        responseType = Entity.SaveUserProfileClick(username, password, confirmPassword, email, firstName, lastName, dob, false);
    }
}

function SaveProfileButton_Callback(responseType) {
    // Note: Refer to UserProfileResponseTypeEnum.cs for values
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

    $('#divProfileProgressBarContainer').addClass('clsHidden');
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

function ToggleIsLoggedIn(isLoggedIn) {

    if (isLoggedIn) {
        $('#btnProfile').removeAttr('disabled');
        $('#btnFindServer').removeAttr('disabled');

        $('#btnLoginLogout').text('Logout');
        $('#btnLoginLogout').data('mode', 'logout');
    }
    else {
        $('#btnProfile').attr('disabled', 'disabled');
        $('#btnFindServer').attr('disabled', 'disabled');
        $('#btnLoginLogout').val('Login');
        $('#btnLoginLogout').data('mode', 'login');
    }
}


function OpenLoginBox() {
    $('#divLogin').modal("show");
}

function CloseLoginBox() {
    $('#divLogin').modal("hide");
    $('#txtUsername').val("");
    $('#txtPassword').val("");
    $('#lblErrorMessage').addClass('clsHidden');

    $('.error').removeClass('error');
}

function DoLogin() {

    if (!$('#formLogin').valid()) {
        return false;
    }

    $('#lblErrorMessage').addClass('clsHidden');

    // Disable controls while logging in
    ToggleLoginPopUpControls(true);

    $('#divLoggingInProgressBarContainer').removeClass('clsHidden');
    Entity.LoginButtonClick($('#txtUsername').val(), $('#txtPassword').val());
}

function DoLogin_Callback(loginStatus) {
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
    $('#divConfirmLogout').modal('show');
}

function DoLogout() {
    Entity.LogoutButtonClick();

    $('#btnProfile').attr('disabled', 'disabled');
    $('#btnFindServer').attr('disabled', 'disabled');
    $('#btnLoginLogout').text('Login');
    $('#btnLoginLogout').data('mode', 'login');
    $('#divConfirmLogout').modal('hide');
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

    $('#txtProfilePassword').addClass('required');
    $('#txtConfirmProfilePassword').addClass('required');

    $('#divUserProfile').modal("show");
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

    $('#divUserProfile').modal("show");
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
    $('#divUserProfile').modal("hide");

    if (accountMode == 'create') {
        OpenLoginBox();
    }
}

/* ACCOUNT NOT ACTIVATED BOX */

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

    if (disabled) {
        $('#btnLogin').attr('disabled', 'disabled');
        $('#btnCancelLogin').attr('disabled', 'disabled');
        $('#btnCreateAccount').attr('disabled', 'disabled');
    }
    else {
        $('#btnLogin').removeAttr('disabled');
        $('#btnCancelLogin').removeAttr('disabled');
        $('#btnCreateAccount').removeAttr('disabled');
    }

}

/* Logo Links */

function FlatRedBallLogoLink() {
    Entity.FlatRedBallLogoLinkClick();
}

function XNALogoLink() {
    Entity.XNALogoLinkClick();
}
