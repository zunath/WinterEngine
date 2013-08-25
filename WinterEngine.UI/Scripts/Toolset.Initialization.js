
var ToolsetViewModel;
function InitializeViewModel() {

    var data = Entity.GetModelJSON();
    ToolsetViewModel = ko.mapping.fromJSON(data);
    ko.applyBindings(ToolsetViewModel);
}

function Initialize() {
    InitializeViewModel();
    InitializeValidation();
    InitializeMainMenu();
    $('#ulObjectBar').menu({}); // Object selection menu

    InitializeDialogBox('#divAboutBox', 'About');
    InitializeDialogBox('#divNewModuleBox', 'New Module');
    InitializeDialogBox('#divOpenModuleBox', 'Open Module');
    InitializeDialogBox("#divSaveAsModuleBox", "Save Module");
    InitializeDialogBox('#divSaveAsOverwriteConfirmation', 'Save Confirmation');
    InitializeDialogBox('#divImportBox', 'Import ERF');
    InitializeDialogBox('#divExportBox', 'Export ERF');
    InitializeDialogBox('#divModulePropertiesBox', 'Module Properties');
    InitializeDialogBox('#divManageContentPackagesBox', 'Manage Content Packages');
    InitializeDialogBox('#divContentPackageCreatorBox', 'Content Package Creator');
    InitializeDialogBox('#divAlertBox', 'Alert');
    InitializeDialogBox('#divNewObject', 'New Object');
    InitializeDialogBox('#divConfirmDelete', 'Delete Object');
    InitializeDialogBox('#divCreateCategory', 'Create Category');
    InitializeDialogBox('#divRenameTreeNode', 'Rename Object');
    
    InitializeTabbedContainers();
    InitializeManageContentPackagesBox();

    $("input[type=button]").button();
}

function InitializeMainMenu() {

    $('#ulMenu').menu({
        position: { my: 'left top', at: 'left bottom' }
    });
}

function InitializeValidation() {
    $('#formNewModule').validate({
        errorPlacement: $.noop
    });
    $('#formNewObject').validate({
        errorPlacement: $.noop
    });
    $('#formNewCategory').validate({
        errorPlacement: $.noop
    });

}

function InitializeManageContentPackagesBox() {
    InitializeDialogBox('#divManageContentPackages', 'Manage Packages');

    $('#olAvailableContentPackages').selectable();
    $('#olAttachedContentPackages').selectable();
}


function InitializeDialogBox(selector, dialogTitle) {
    $(selector).dialog({
        modal: true,
        autoOpen: false,
        title: dialogTitle,
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeTabbedContainers() {
    $('#divAreasTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divCreaturesTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divItemsTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divPlaceablesTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divConversationsTab').accordion({ collapsible: true, heightStyle: "content" });
    $('#divScriptsTab').accordion({ collapsible: true, heightStyle: "content" });

    // Seems to be a bug with making a div an accordion that is hidden.
    // This is to correct that issue.
    $('.clsObjectTabDiv').addClass('clsHidden');
}