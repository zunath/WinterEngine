/* Button Functionality - File Menu */

function NewModuleButtonClick() {
    $('#divNewModuleBox').dialog('open');
}

function CloseNewModuleBox() {
    $('#divNewModuleBox').dialog('close');
    $('#lblNewModuleError').addClass('clsHidden');
}

function NewModuleBoxOKClick() {

    if (!$('#formNewModule').valid()) {
        return false;
    }

    var moduleName = $('#txtModuleName').val();
    var moduleTag = $('#txtModuleTag').val();

    Entity.NewModuleButtonClick(moduleName, moduleTag);
}

function NewModuleBoxOKClick_Callback(success) {
    if (success) {
        CloseNewModuleBox();
    }
    else {
        $('#lblNewModuleError').removeClass('clsHidden');
    }
}

function NewModuleBoxCancelClick() {
    $('#txtModuleName').val('');
    $('#txtModuleTag').val('');
    $('#divNewModuleBox').dialog('close');
}

function OpenModuleButtonClick() {
    Entity.OpenModuleButtonClick();
}

function CloseModuleButtonClick() {
    Entity.CloseModuleButtonClick();
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



/* General Methods */

function ToggleMenuButton(selector, enable) {
    if (enable) {
        $(selector).addClass('ui-state-disabled');
    }
    else {
        $(selector).removeClass('ui-state-disabled');
    }
}