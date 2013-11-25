 

function Initialize() {
    InitializeToolsetViewModel(); // Located in WinterEngine.ViewModels.js
    InitializeValidation();
    

    // Unblock the UI - the UI blocking is done to prevent the user from making javascript calls before Awesomium has loaded.
    $.unblockUI();
}

function InitializeValidation() {
    $.validator.setDefaults({ ignore: [] });
    $('.clsValidationForm').validate({
        errorPlacement: $.noop
    });
}
