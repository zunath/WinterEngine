function LoadObjectData(resourceID) {
    Entity.LoadObjectData(resourceID);
}

function LoadObjectData_Callback() {
    ToolsetViewModel.RefreshActiveObject();
    ToolsetViewModel.RefreshStatusVariables();
}

function ObjectTabApplyChanges() {
    ToolsetViewModel.SaveActiveObject();
}

function ObjectTabApplyChanges_Callback() {
    ToolsetViewModel.RefreshActiveObject();
}

function ObjectTabDiscardChanges() {
    var selectedNode = $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('get_selected');
    var resourceID = $(selectedNode).data('resourceid');
    LoadObjectData(resourceID);
}


function SelectTilesetSpriteSheet() {
    var selectedSpritesheetID = parseInt($('#selTilesetDetails-Spritesheet option:selected').val());
    if (selectedSpritesheetID > 0) {
        Entity.LoadTilesetSpritesheet(selectedSpritesheetID);
    }
}

function OpenEditLocalVariableBox(isModuleProperties, mode, variableObject) {
    $('#divEditLocalVariableBox').data('ismoduleproperties', isModuleProperties);
    $('#divEditLocalVariableBox').data('mode', mode);
    $('#divEditLocalVariableBox').data('variableobject', variableObject);
    $('#divEditLocalVariableBox').modal('show');

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
    $('#divEditLocalVariableBox').modal('hide');
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
    $('#divEditLocalVariableBox').modal('hide');
}

function OpenDeleteLocalVariableBox(variableObject) {
    $('#divConfirmDeleteLocalVariable').data('variableobject', variableObject);
    $('#divConfirmDeleteLocalVariable').modal('show');

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
    $('#divConfirmDeleteLocalVariable').modal('hide');
}