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
}

function ObjectTabDiscardChanges() {
    LoadObjectData();
}


function SelectTilesetSpriteSheet() {
    var selectedSpritesheetID = parseInt($('#selTilesetDetails-Spritesheet option:selected').val());

    Entity.LoadTilesetSpritesheet(selectedSpritesheetID);
}

function NewLocalVariableBoxOKClick() {
    if (!$('#formNewLocalVariable').valid()) return;

    var activeObject = ToolsetViewModel.GetActiveObject();

    activeObject.LocalVariables.push({
        Name: $('#txtLocalVariableName').val(),
        Value: $('#txtLocalVariableValue').val()
    });

    $('#txtLocalVariableName').val('');
    $('#txtLocalVariableValue').val('');
    $('#divNewLocalVariableBox').dialog('close');
}

function NewLocalVariableCancelClick() {
    $('#txtLocalVariableName').val('');
    $('#txtLocalVariableValue').val('');
    $('#divNewLocalVariableBox').dialog('close');
}

function ConfirmDeleteLocalVariable() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    
    alert(mode);
}