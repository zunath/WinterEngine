/* Page Initialization */

function Initialize() {
    InitializeMainMenu();
    InitializeObjectSelectionMenu();
    InitializeTreeView();
    InitializeAboutBox();
    InitializeNewModuleBox();
    InitializeOpenModuleBox();
    InitializeImportBox();
    InitializeExportBox();
    InitializeModulePropertiesBox();
    InitializeManageContentPackagesBox();
    InitializeContentPackageCreatorBox();
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

function InitializeTreeView() {
    $('#divTreeView').jstree({
        "plugins": ["html_data", "ui", "themeroller", "sort", "contextmenu"],
        "animation": 0
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