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

    $('#divNewModuleBox').dialog('open');
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
}

function InitializeExportBox() {
}

function InitializeModulePropertiesBox() {
}

function InitializeManageContentPackagesBox() {
}

function InitializeContentPackageCreatorBox() {
}