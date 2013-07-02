/* Button Functionality - File Menu */

function NewModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
    $('#divNewModuleBox').dialog('open');
}

function CloseNewModuleBox() {

    $('#lblNewModuleError').text('');
    $('#txtModuleName').val('');
    $('#txtModuleTag').val('');
    $('#divNewModuleBox').dialog('close');
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
        $('#lblNewModuleError').text('There was an error creating a new module.');
    }
}

function NewModuleBoxCancelClick() {
    CloseNewModuleBox();
}

function OpenModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
    Entity.OpenModuleButtonClick();
}

function CloseModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
    Entity.CloseModuleButtonClick();
}

function SaveModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function SaveAsModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function ImportButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

    Entity.ImportButtonClick();
}

function ImportButtonClick_Callback(jsonObjectList) {
    $('#divImportBox').dialog('open');
}

function ExportButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

}

function ExportButtonClick_Callback(jsonObjectList) {
    $('#divExportBox').dialog('open');
}

function ExitButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

    Entity.ExitButtonClick();
}

/* Button Functionality - Edit Menu */

function UndoButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function RedoButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function CopyButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function CutButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function PasteButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function ModulePropertiesButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

/* Button Functionality - Content Menu */

function ManageContentPackagesButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function ContentPackageCreatorButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;
}

function BuildModuleButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

    $('#lblAlertBox').text('Rebuilding module. Please wait...');
    $('#divAlertBox').dialog('open');
    $('#btnAlertBoxOK').attr('disabled', 'disabled');
    Entity.BuildModuleButtonClick();
}

function BuildModuleButtonClick_Callback(success, exception) {

    if (success) {
        $('#lblAlertBox').text('Rebuild completed successfully');
    }
    else {
        $('#lblAlertBox').text('Error occurred during rebuild.<br /><br />' +
            'Exception details: ' + exception);
    }
    $('#btnAlertBoxOK').removeAttr('disabled');
}

/* Button Functionality - Help Menu */

function WinterEngineWebsiteButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

    Entity.WinterEngineWebsiteButtonClick();
}

function AboutButtonClick() {
    if (IsMenuButtonDisabled($(this))) return;

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

function CloseAlertBox() {
    $('#divAlertBox').dialog('close');
}

function IsMenuButtonDisabled(button) {
    var isDisabled = button.parent().hasClass('ui-state-disabled');

    return isDisabled;
}