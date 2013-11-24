 

function Initialize() {
    InitializeToolsetViewModel(); // Located in WinterEngine.ViewModels.js
    InitializeValidation();
    InitializeDropDownMenus();
    
    InitializeDialogBox('#divAboutBox', 'About');
    InitializeDialogBox('#divNewModuleBox', 'New Module');
    InitializeDialogBox('#divOpenModuleBox', 'Open Module');
    InitializeDialogBox("#divSaveAsModuleBox", "Save Module");
    InitializeDialogBox('#divSaveAsOverwriteConfirmation', 'Save Confirmation');
    InitializeDialogBox('#divImportBox', 'Import ERF');
    InitializeDialogBox('#divExportBox', 'Export ERF');
    InitializeDialogBox('#divAlertBox', 'Alert');
    InitializeDialogBox('#divNewObject', 'New Object');
    InitializeDialogBox('#divConfirmDelete', 'Delete Object');
    InitializeDialogBox('#divCreateCategory', 'Create Category');
    InitializeDialogBox('#divRenameTreeNode', 'Rename Object');
    InitializeDialogBox('#divEditLocalVariableBox', 'New Local Variable');
    InitializeDialogBox("#divConfirmDeleteLocalVariable", "Delete Local Variable");
    
    InitializeTabbedContainers();
    InitializeAccordions();
    InitializeManageContentPackagesBox();
    InitializeDialogBox('#divModulePropertiesBox', 'Module Properties');
    $('#divModulePropertiesBox').dialog('option', 'width', '450');
    $('#divModulePropertiesBox').dialog('option', 'height', '500');

    $("input[type=button]").button();
    $('#btnObjectTabApplyChanges').button('disable');
    $('#btnObjectTabDiscardChanges').button('disable');

    $('.clsProgressBar').progressbar({
        value: false
    });

    $('.clsResizable').resizable();

    // Unblock the UI - the UI blocking is done to prevent the user from making javascript calls before Awesomium has loaded.
    $.unblockUI();
}

function InitializeDropDownMenus() {

    $('.clsDropDownMenuBar').menu({
        position: { my: 'left top', at: 'left bottom' }
    });
}

function InitializeValidation() {
    $.validator.setDefaults({ ignore: [] });
    $('.clsValidationForm').validate({
        errorPlacement: $.noop
    });
}

function InitializeManageContentPackagesBox() {
    InitializeDialogBox('#divManageContentPackages', 'Manage Packages');

    $('#olAvailableContentPackages').selectable();
    $('#olAttachedContentPackages').selectable();
}

function InitializeTabbedContainers() {
    $('#divModulePropertiesBox').tabs();

}

function InitializeAccordions() {
    $('#divAreaTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divCreatureTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divItemTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divPlaceableTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divConversationTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divScriptTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divTilesetTab').accordion({ collapsible: true, heightStyle: "content" });

    // Seems to be a bug with making a div an accordion that is hidden.
    // This is to correct that issue.
    $('.clsObjectTabDiv').addClass('clsHidden');
}