/* Page Initialization */

function Initialize() {
    InitializeMainMenu();
    InitializeTreeView();
}

function InitializeMainMenu() {

    $('#ulMenu').menu({
        position: { my: 'left top', at: 'left bottom' }
    });
}

function InitializeTreeView() {
    $('#divTreeView').jstree({
        "plugins": ["html_data", "ui", "themeroller", "sort", "contextmenu"],
        "animation": 0
    });
}

/* Button Functionality - File Menu */

function NewModuleButtonClick() {
}

function OpenModuleButtonClick() {
}

function CloseModuleButtonClick() {
}

function SaveModuleButtonClick() {
}

function SaveAsModuleButtonClick() {
}

function ImportButtonClick() {
}

function ExportButtonClick() {
}

function ExitButtonClick() {
}

/* Button Functionality - Edit Menu */

function UndoButtonClick() {
}

function RedoButtonClick() {
}

function CopyButtonClick() {
}

function CutButtonClick() {
}

function PasteButtonClick() {
}

function ModulePropertiesButtonClick() {
}

/* Button Functionality - Content Menu */

function ManageContentPackagesButtonClick() {
}

function ContentPackageCreatorButtonClick() {
}

function BuildModuleButtonClick() {
}

/* Button Functionality - Help Menu */

function WinterEngineWebsiteButtonClick() {
    Entity.WinterEngineWebsiteButtonClick();
}

function AboutButtonClick() {
}
