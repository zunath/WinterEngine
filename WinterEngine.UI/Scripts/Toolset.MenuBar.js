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
        ChangeObjectMode("Area");
        $('#divObjectBar').removeClass('clsHidden');
    }
    else {
        $('#lblNewModuleError').text('There was an error creating a new module.');
    }
}

function NewModuleBoxCancelClick() {
    CloseNewModuleBox();
}

function ShowOpenModulePopUp(element) {
	
	if (IsMenuButtonDisabled($(element))) return;

    var jsonModuleList = Entity.GetModulesList();
    ToolsetViewModel.ModuleList(JSON.parse(jsonModuleList));

    $('#divOpenModuleBox').dialog('open');
	
}

function CloseOpenModulePopUp() {
    $('#divOpenModuleBox').dialog('close');
}

function OpenModuleButtonClick(element) {
    var selectedModule = $('#selModulesList option:selected').text();

    if (selectedModule != '') {
        Entity.OpenModuleButtonClick(selectedModule);
    }
}

function OpenModuleButtonClick_Callback(success) {
    if (success) {
        ToggleModuleActionButtons(true);
        Entity.LoadTreeViewData();
        ChangeObjectMode("Area");
        $('#divObjectBar').removeClass('clsHidden');
        CloseOpenModulePopUp();
        PopulateToolsetViewModel();
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
    ChangeObjectMode();
    HideAllTreeViews();
    ToggleModuleActionButtons(false);
    $('#divObjectBar').addClass('clsHidden');
}

function SaveModuleButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    Entity.SaveModuleButtonClick();
}

function ShowSaveAsModulePopUp(element) {
    if (IsMenuButtonDisabled($(element))) return;

    var jsonModuleList = Entity.GetModulesList();
    ToolsetViewModel.ModuleList(JSON.parse(jsonModuleList));
    $('#divSaveAsModuleBox').dialog('open');
}

function SaveAsModuleButtonClick(element) {
    var moduleName = $('#txtSaveAsModuleFileName').val();
    Entity.SaveAsModuleButtonClick(moduleName, false);
}

function SaveAsModuleButtonClick_Callback(response, moduleName) {
    if (response == 0) { // 0 = Save Failed
        $('#lblSaveAsErrors').text('An error has occurred.');
    }
    else if (response == 1) { // 1 = Save Successful
        CloseSaveAsModulePopUp();
        CloseSaveAsModuleConfirmationPopUp(false);
    }
    else if (response == 2) { // 2 = File already exists
        ShowSaveAsModuleConfirmationPopUp(moduleName);
    }
}

function SaveAsModuleOverwriteButtonClick() {
    var moduleName = $('#txtSaveAsModuleFileName').val();
    Entity.SaveAsModuleButtonClick(moduleName, true);
}

function ShowSaveAsModuleConfirmationPopUp(moduleName) {
    $('#spnSaveAsModuleOverwriteName').text(moduleName);
    $('#divSaveAsOverwriteConfirmation').dialog('open');
    $('#divSaveAsModuleBox').dialog('close');
}

function SelectExistingSaveAsModule() {
    var selectedName = $('#selModulesListSaveAs option:selected').text();
    $('#txtSaveAsModuleFileName').val(selectedName);
}

function CloseSaveAsModulePopUp() {
    $('#divSaveAsModuleBox').dialog('close');
    $('#txtSaveAsModuleFileName').val('');
}

function CloseSaveAsModuleConfirmationPopUp(showSaveAsPopUp) {
    $('#divSaveAsOverwriteConfirmation').dialog('close');
    $('#spnSaveAsModuleOverwriteName').text('');
    if (showSaveAsPopUp) {
        $('#divSaveAsModuleBox').dialog('open');
    }
}

function ImportButtonClick(element) {
    if (IsMenuButtonDisabled($(element))) return;

    //Entity.ImportButtonClick();
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

    Entity.ManageContentPackagesButtonClick();
}

function ManageContentPackagesButton_Callback(jsonAttachedContentPackages, jsonAvailableContentPackages) {

    ToolsetViewModel.AvailableContentPackages(JSON.parse(jsonAvailableContentPackages));
    ToolsetViewModel.AttachedContentPackages(JSON.parse(jsonAttachedContentPackages));

    $('#divManageContentPackages').dialog('open');
}

function ManageContentPackagesAddButton() {
    var selectedItemName = $('#selAvailableContentPackages option:selected').text();
    var selectedItemFileName = $('#selAvailableContentPackages option:selected').val();
    
    var match = ko.utils.arrayFirst(ToolsetViewModel.AttachedContentPackages(), function (item) {
        return item.Name === selectedItemName;
    });

    if (!match) {
        ToolsetViewModel.AttachedContentPackages.push({ Name: selectedItemName, FileName: selectedItemFileName });
    }
}

function ManageContentPackagesRemoveButton() {
    var selectedItemName = $('#selAttachedContentPackages option:selected').text();

    var match = ko.utils.arrayFirst(ToolsetViewModel.AttachedContentPackages(), function (item) {
        if (item.Name === selectedItemName)
            return item;
    });

    ToolsetViewModel.AttachedContentPackages.remove(match);
}

function ManageContentPackagesSaveChanges() {
    var jsonUpdatedContentPackages = JSON.stringify(ToolsetViewModel.AttachedContentPackages());

    Entity.UpdateContentPackages(jsonUpdatedContentPackages);
}

function ManageContentPackagesSaveChanges_Callback() {

    $('#divManageContentPackages').dialog('close');
}

function CloseManageContentPackagesBox() {
    $('#divManageContentPackages').dialog('close');
    $('#selAvailableContentPackages').empty();
    $('#selAttachedContentPackages').empty();
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
        PopulateToolsetViewModel();
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
        $('#liManageContentPackagesButton').removeClass('ui-state-disabled');
        $('#liBuildModuleButton').removeClass('ui-state-disabled');
    }
    else {
        $('#liCloseModuleButton').addClass('ui-state-disabled');
        $('#liSaveModuleButton').addClass('ui-state-disabled');
        $('#liSaveAsButton').addClass('ui-state-disabled');
        $('#liImportButton').addClass('ui-state-disabled');
        $('#liExportButton').addClass('ui-state-disabled');
        $('#liManageContentPackagesButton').addClass('ui-state-disabled');
        $('#liBuildModuleButton').addClass('ui-state-disabled');
    }
}