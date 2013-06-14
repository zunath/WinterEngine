
/* Button Functionality - File Menu */

function NewModuleButtonClick() {
    $('#divNewModuleBox').dialog('open');
}

function NewModuleBoxOKClick() {
    var moduleName = $('#txtModuleName').val();
    var moduleTag = $('#txtModuleTag').val();

    Entity.NewModuleButtonClick(moduleName, moduleTag);
}

function NewModuleBoxCancelClick() {
    $('#txtModuleName').val('');
    $('#txtModuleTag').val('');
    $('#divNewModuleBox').dialog('close');
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
    $('#divAboutBox').dialog('open');
}

function AboutBoxClose() {
    $('#divAboutBox').dialog('close');
}