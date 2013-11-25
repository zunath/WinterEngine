/* Button Functionality - File Menu */

function NewModuleButtonClick(element) {
    
    $('#divNewModuleBox').modal('show');
}

function CloseNewModuleBox() {

    $('#lblNewModuleError').text('');
    $('#txtModuleName').val('');
    $('#txtModuleTag').val('');
    $('#txtModuleResref').val('');
    $('#divNewModuleBox').modal('hide');

}

function NewModuleBoxOKClick() {
    if (!$('#formNewModule').valid()) return;

    var moduleName = $('#txtModuleName').val();
    var moduleTag = $('#txtModuleTag').val();
    var moduleResref = $('#txtModuleResref').val();

    $('#btnNewModuleOK, #btnNewModuleCancel').attr('disabled', 'disabled');
    $('#divCreatingModuleProgressBar').removeClass('clsHidden');

    Entity.NewModuleButtonClick(moduleName, moduleTag, moduleResref);
}

function NewModuleBoxOKClick_Callback(success) {
    $('#divCreatingModuleProgressBar').addClass('clsHidden');
    $('#btnNewModuleOK, #btnNewModuleCancel').removeAttr('disabled');

    if (success) {
        CloseNewModuleBox();
        Entity.LoadTreeViewData();
        ChangeObjectMode("Area");
        ToolsetViewModel.Refresh();
    }
    else {
        $('#lblNewModuleError').text('There was an error creating a new module.');
    }
}

function NewModuleBoxCancelClick() {
    CloseNewModuleBox();
}

function ShowOpenModulePopUp(element) {
	
	Entity.GetModulesList();
	ToolsetViewModel.Refresh();
    
    $('#divOpenModuleBox').modal('show');
	
}

function CloseOpenModulePopUp() {
    $('#divOpenModuleBox').modal('hide');
}

function OpenModuleButtonClick(element) {
    var selectedModule = $('#selModulesList option:selected').text();

    if (selectedModule != '') {
        $('#btnOpenModuleOpenButton, #btnOpenModuleCancelButton').attr('disabled', 'disabled');
        $('#divOpeningModuleProgressBar').removeClass('clsHidden');
        Entity.OpenModuleButtonClick(selectedModule);
    }
}

function OpenModuleButtonClick_Callback(success) {
    if (success) {
        Entity.LoadTreeViewData();
        ChangeObjectMode("Area");
        CloseOpenModulePopUp();
        ToolsetViewModel.Refresh();

        $('#divOpeningModuleProgressBar').addClass('clsHidden');
        $('#btnOpenModuleOpenButton, #btnOpenModuleCancelButton').removeAttr('disabled');
    }
}

function CloseModuleButtonClick(element) {
    
    Entity.CloseModuleButtonClick();
}

function CloseModuleButtonClick_Callback() {
    ChangeObjectMode('');
}

function SaveModuleButtonClick(element) {
    

    Entity.SaveModuleButtonClick();
    ToolsetViewModel.Refresh();
}

function ShowSaveAsModulePopUp(element) {
    

    Entity.GetModulesList();
    ToolsetViewModel.Refresh();

    $('#divSaveAsModuleBox').modal('show');
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
    $('#divSaveAsOverwriteConfirmation').modal('show');
    $('#divSaveAsModuleBox').modal('hide');
}

function SelectExistingSaveAsModule() {
    var selectedName = $('#selModulesListSaveAs option:selected').text();
    $('#txtSaveAsModuleFileName').val(selectedName);
}

function CloseSaveAsModulePopUp() {
    $('#divSaveAsModuleBox').modal('hide');
    $('#txtSaveAsModuleFileName').val('');
}

function CloseSaveAsModuleConfirmationPopUp(showSaveAsPopUp) {
    $('#divSaveAsOverwriteConfirmation').modal('hide');
    $('#spnSaveAsModuleOverwriteName').text('');
    if (showSaveAsPopUp) {
        $('#divSaveAsModuleBox').modal('show');
    }
}

function ImportButtonClick(element) {
    //Entity.ImportButtonClick();
}

function ImportButtonClick_Callback(jsonObjectList) {
    $('#divImportBox').modal('show');
}

function ExportButtonClick(element) {
}

function ExportButtonClick_Callback(jsonObjectList) {
    $('#divExportBox').modal('show');
}

function ExitButtonClick(element) {
    Entity.ExitButtonClick();
}

/* Button Functionality - Edit Menu */

function UndoButtonClick(element) {
}

function RedoButtonClick(element) {
}

function CopyButtonClick(element) {
}

function CutButtonClick(element) {
}

function PasteButtonClick(element) {
}

function ModulePropertiesButtonClick(element) {
    
    ToolsetViewModel.RefreshModuleProperties();
    $('#ulModulePropertiesTab a:first').tab('show');
    $('#divModulePropertiesBox').modal('show');
}

function SaveModulePropertiesChanges() {
    if (!$('#formMPBasicDetails').valid()) {
        $('#ulModulePropertiesTab li:eq(0)').tab('show');

    }
    else if (!$('#formMPEvents').valid()) {
        $('#ulModulePropertiesTab li:eq(1)').tab('show');
    }
    else if (!$('#formMPLevelChart').valid()) {
        $('#ulModulePropertiesTab li:eq(2)').tab('show');

    }
    else if (!$('#formMPText').valid()) {
        $('#ulModulePropertiesTab li:eq(3)').tab('show');
    }
    else {
        ToolsetViewModel.SaveModuleProperties();
    }
}

function SaveModuleProperties_Callback() {
    CloseModulePropertiesBox();
}

function CloseModulePropertiesBox() {
    $('#divModulePropertiesBox').modal('hide');
    ToolsetViewModel.RefreshModuleProperties();
}

/* Button Functionality - Content Menu */

function ManageContentPackagesButtonClick(element) {
    

    Entity.ManageContentPackagesButtonClick();
}

function ManageContentPackagesButton_Callback(jsonAttachedContentPackages, jsonAvailableContentPackages) {
    ToolsetViewModel.Refresh();
    $('#divManageContentPackages').modal('show');
}

function ManageContentPackagesAddButton() {
    var selectedItemName = $('#selAvailableContentPackages option:selected').text();
    var selectedItemFileName = $('#selAvailableContentPackages option:selected').val();
    
    var match = ko.utils.arrayFirst(ToolsetViewModel.AttachedContentPackages(), function (item) {
        return (item.Name === selectedItemName || item.FileName === selectedItemFileName ||
                item.Name() === selectedItemName || item.FileName() === selectedItemFileName);
    });

    if (!match) {
        ToolsetViewModel.AttachedContentPackages.push({ Name: selectedItemName, FileName: selectedItemFileName });
    }
}

function ManageContentPackagesRemoveButton() {
    var selectedItemName = $('#selAttachedContentPackages option:selected').text();
    var selectedItemFileName = $('#selAvailableContentPackages option:selected').val();
    
    var match = ko.utils.arrayFirst(ToolsetViewModel.AttachedContentPackages(), function (item) {

        if (item.Name === selectedItemName || item.FileName === selectedItemFileName ||
            item.Name() === selectedItemName || item.FileName() === selectedItemName)
            return item;
    });

    ToolsetViewModel.AttachedContentPackages.remove(match);
}

function ManageContentPackagesSaveChanges() {
    ToolsetViewModel.SaveContentPackages();
}

function ManageContentPackagesSaveChanges_Callback() {

    ToolsetViewModel.Refresh();
    $('#divManageContentPackages').modal('hide');
}

function CloseManageContentPackagesBox() {
    $('#divManageContentPackages').modal('hide');
    $('#selAvailableContentPackages').empty();
    $('#selAttachedContentPackages').empty();
}

function BuildModuleButtonClick(element) {
    

    $('#lblAlertBox').text('Rebuilding module. Please wait...');
    $('#divAlertBox').modal('show');
    $('#btnAlertBoxOK').attr('disabled', 'disabled');
    Entity.BuildModuleButtonClick();
}

function BuildModuleButtonClick_Callback(success, exception) {

    if (success) {
        $('#lblAlertBox').text('Rebuild completed successfully');
        ToolsetViewModel.Refresh();
    }
    else {
        $('#lblAlertBox').text('Error occurred during rebuild.<br /><br />' +
            'Exception details: ' + exception);
    }
    $('#btnAlertBoxOK').removeAttr('disabled');
}

/* Button Functionality - Help Menu */

function WinterEngineWebsiteButtonClick(element) {
    Entity.WinterEngineWebsiteButtonClick();
}

function AboutBoxClose() {
    $('#divAboutBox').modal('hide');
}


