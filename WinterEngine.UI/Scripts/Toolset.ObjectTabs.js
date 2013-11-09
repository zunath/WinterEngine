// Called from Toolset.TreeViews.js: OnTreeViewNodeSelected()
function LoadObjectData(resourceID) {
    Entity.LoadObjectData(resourceID);
}

function LoadObjectData_Callback() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    
    if (mode == 'Area') {
        $('.clsAreaObjectField').removeAttr('disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Creature') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').removeAttr('disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Item') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').removeAttr('disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Placeable') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').removeAttr('disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Conversation') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').removeAttr('disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Script') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').removeAttr('disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Tileset') {
        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').removeAttr('disabled');
    }

    ToolsetViewModel.Refresh();
}

function ObjectTabApplyChanges() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var jsonModel = JSON.stringify(ko.viewmodel.toModel(ToolsetViewModel));
    Entity.SaveObjectData(mode, jsonModel);
}

function ObjectTabApplyChanges_Callback() {
    ToolsetViewModel.Refresh();
}

function ObjectTabDiscardChanges() {
    LoadObjectData();
}


function SelectTilesetSpriteSheet() {
    var selectedSpritesheetID = parseInt($('#selTilesetDetails-Spritesheet option:selected').val());

    Entity.LoadTilesetSpritesheet(selectedSpritesheetID);
}

function OpenEditLocalVariableBox(isModuleProperties, mode, variableObject) {
    $('#divEditLocalVariableBox').data('ismoduleproperties', isModuleProperties);
    $('#divEditLocalVariableBox').data('mode', mode);
    $('#divEditLocalVariableBox').data('variableobject', variableObject);
    $('#divEditLocalVariableBox').dialog('open');

    if (mode == 'edit') {
        try
        {
            $('#txtLocalVariableName').val(variableObject.Name());
            $('#txtLocalVariableValue').val(variableObject.Value());
        }
        catch(error)
        {
            $('#txtLocalVariableName').val(variableObject.Name);
            $('#txtLocalVariableValue').val(variableObject.Value);
        }
    }
}

function EditLocalVariableCancelClick() {
    $('#txtLocalVariableName').val('');
    $('#txtLocalVariableValue').val('');
    $('#divEditLocalVariableBox').data('ismoduleproperties', '');
    $('#divEditLocalVariableBox').data('mode', '');
    $('#divEditLocalVariableBox').data('variableobject', '');
    $('#divEditLocalVariableBox').dialog('close');
}

function EditLocalVariableBoxOKClick() {
    if (!$('#formEditLocalVariable').valid()) return;
    var isModuleProperties = $('#divEditLocalVariableBox').data('ismoduleproperties');
    var activeObject = isModuleProperties == true ? ToolsetViewModel.ActiveModule : ToolsetViewModel.GetActiveObject();
    var mode = $('#divEditLocalVariableBox').data('mode');

    if (mode == 'edit') {
        var variableObject = $('#divEditLocalVariableBox').data('variableobject');
        activeObject.LocalVariables.remove(function (item) {return item.LocalVariableID == variableObject.LocalVariableID });
        activeObject.LocalVariables.push({
            Name: $('#txtLocalVariableName').val(),
            Value: $('#txtLocalVariableValue').val()
        });
    }
    else if (mode == 'create') {
        activeObject.LocalVariables.push({
            Name: $('#txtLocalVariableName').val(),
            Value: $('#txtLocalVariableValue').val()
        });
    }

    $('#txtLocalVariableName').val('');
    $('#txtLocalVariableValue').val('');
    $('#divEditLocalVariableBox').dialog('close');
}

function OpenDeleteLocalVariableBox(variableObject) {
    $('#divConfirmDeleteLocalVariable').data('variableobject', variableObject);
    $('#divConfirmDeleteLocalVariable').dialog('open');

}

function ConfirmDeleteLocalVariableClick() {
    var name = $('#divConfirmDeleteLocalVariable').data('name');
    var variableObject = $('#divConfirmDeleteLocalVariable').data('variableobject');
    var isModuleProperties = $('#divEditLocalVariableBox').data('ismoduleproperties');
    var activeObject = isModuleProperties == true ? ToolsetViewModel.ActiveModule : ToolsetViewModel.GetActiveObject();
    activeObject.LocalVariables.remove(function (variable) { return variable.LocalVariableID == variableObject.LocalVariableID; });
    CloseDeleteLocalVariableClick();
}

function CloseDeleteLocalVariableClick() {
    $('#divConfirmDeleteLocalVariable').data('name', '');
    $('#divConfirmDeleteLocalVariable').data('localvariableid', '');
    $('#divConfirmDeleteLocalVariable').dialog('close');
}