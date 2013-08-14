/* Page Initialization */

function Initialize() {
    InitializeValidation();
    InitializeMainMenu();
    InitializeObjectSelectionMenu();
    InitializeAboutBox();
    InitializeNewModuleBox();
    InitializeOpenModuleBox();
    InitializeImportBox();
    InitializeExportBox();
    InitializeModulePropertiesBox();
    InitializeManageContentPackagesBox();
    InitializeContentPackageCreatorBox();
    InitializeAlertBox();
    InitializeCreateNewObjectBox();
    InitializeDeleteObjectBox();
    InitializeCreateCategoryBox();
}

function InitializeMainMenu() {

    $('#ulMenu').menu({
        position: { my: 'left top', at: 'left bottom' }
    });
}

function InitializeObjectSelectionMenu() {
    $('#ulObjectBar').menu({
    });
}
function InitializeAboutBox() {
    $('#divAboutBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'About',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeNewModuleBox() {
    $('#divNewModuleBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'New Module',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeOpenModuleBox() {
}

function InitializeImportBox() {
    $('#divImportBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Import ERF',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeExportBox() {
    $('#divExportBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Export ERF',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeModulePropertiesBox() {
    $('#divModulePropertiesBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Module Properties',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeManageContentPackagesBox() {
    $('#divManageContentPackagesBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Manage Content Packages',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeContentPackageCreatorBox() {
    $('#divContentPackageCreatorBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Content Package Creator',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
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

function InitializeAlertBox() {
    $('#divAlertBox').dialog({
        modal: true,
        autoOpen: false,
        title: 'Alert',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeCreateNewObjectBox() {
    $('#divNewObject').dialog({
        modal: true,
        autoOpen: false,
        title: 'New Object',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeDeleteObjectBox() {
    $('#divConfirmDelete').dialog({
        modal: true,
        autoOpen: false,
        title: 'Delete Object?',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeCreateCategoryBox() {
    $('#divCreateCategory').dialog({
        modal: true,
        autoOpen: false,
        title: 'Create Category',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}