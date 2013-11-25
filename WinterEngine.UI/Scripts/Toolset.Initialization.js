 

function Initialize() {
    InitializeToolsetViewModel(); // Located in WinterEngine.ViewModels.js
    InitializeValidation();
    
    InitializeTabbedContainers();
    InitializeDialogBox('#divModulePropertiesBox', 'Module Properties');
    $('#divModulePropertiesBox').dialog('option', 'width', '450');
    $('#divModulePropertiesBox').dialog('option', 'height', '500');

    $('#btnObjectTabApplyChanges').button('disable');
    $('#btnObjectTabDiscardChanges').button('disable');

    $('.clsProgressBar').progressbar({
        value: false
    });

    // Unblock the UI - the UI blocking is done to prevent the user from making javascript calls before Awesomium has loaded.
    $.unblockUI();
}

function InitializeValidation() {
    $.validator.setDefaults({ ignore: [] });
    $('.clsValidationForm').validate({
        errorPlacement: $.noop
    });
}

function InitializeTabbedContainers() {
    $('#divModulePropertiesBox').tabs();

}
