/* Button Functionality - File Menu */

function NewModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
    $('#divNewModuleBox').dialog('open');
}

function CloseNewModuleBox() {

    $('#lblNewModuleError').text('');
    $('#txtModuleName').val('');
    $('#txtModuleTag').val('');
    $('#divNewModuleBox').dialog('close');
}

function NewModuleBoxOKClick() {
    if (!$('#formNewModule').valid()) return;

    var moduleName = $('#txtModuleName').val();
    var moduleTag = $('#txtModuleTag').val();

    Entity.NewModuleButtonClick(moduleName, moduleTag);
}

function NewModuleBoxOKClick_Callback(success) {
    if (success) {
        CloseNewModuleBox();
        ToggleModuleActionButtons(true);
        Entity.LoadTreeViewData();
    }
    else {
        $('#lblNewModuleError').text('There was an error creating a new module.');
    }
}

function NewModuleBoxCancelClick() {
    CloseNewModuleBox();
}

function OpenModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
    Entity.OpenModuleButtonClick();
}

function OpenModuleButtonClick_Callback(success) {
    if (success) {
        ToggleModuleActionButtons(true);
        Entity.LoadTreeViewData();
    }
    else {
        ToggleModuleActionButtons(false);
    }
}

function CloseModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
    Entity.CloseModuleButtonClick();
}

function CloseModuleButtonClick_Callback() {
    HideAllTreeViews();
    ToggleModuleActionButtons(false);
}

function SaveModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.SaveModuleButtonClick();
}

function SaveAsModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.SaveAsModuleButtonClick();
}

function ImportButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.ImportButtonClick();
}

function ImportButtonClick_Callback(jsonObjectList) {
    $('#divImportBox').dialog('open');
}

function ExportButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

}

function ExportButtonClick_Callback(jsonObjectList) {
    $('#divExportBox').dialog('open');
}

function ExitButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.ExitButtonClick();
}

/* Button Functionality - Edit Menu */

function UndoButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function RedoButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function CopyButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function CutButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function PasteButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function ModulePropertiesButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

/* Button Functionality - Content Menu */

function ManageContentPackagesButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function ContentPackageCreatorButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;
}

function BuildModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

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

function WinterEngineWebsiteButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.WinterEngineWebsiteButtonClick();
}

function AboutButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    $('#divAboutBox').dialog('open');
}

function AboutBoxClose() {
    $('#divAboutBox').dialog('close');
}



/* General Methods */

function CloseAlertBox() {
    $('#divAlertBox').dialog('close');
}

function IsMenuButtonDisabled(button) {
    var isDisabled = button.parent().hasClass('ui-state-disabled');

    return isDisabled;
}

function ToggleModuleActionButtons(isEnabled) {
    if (isEnabled) {
        $('#liCloseModuleButton').removeClass('ui-state-disabled');
        $('#liSaveModuleButton').removeClass('ui-state-disabled');
        $('#liSaveAsButton').removeClass('ui-state-disabled');
        $('#liImportButton').removeClass('ui-state-disabled');
        $('#liExportButton').removeClass('ui-state-disabled');
    }
    else {
        $('#liCloseModuleButton').addClass('ui-state-disabled');
        $('#liSaveModuleButton').addClass('ui-state-disabled');
        $('#liSaveAsButton').addClass('ui-state-disabled');
        $('#liImportButton').addClass('ui-state-disabled');
        $('#liExportButton').addClass('ui-state-disabled');
    }
}