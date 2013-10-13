// Called from Toolset.TreeViews.js: OnTreeViewNodeSelected()
function LoadObjectData(resourceID) {
    var mode = ToolsetViewModel.CurrentObjectMode();
    Entity.LoadObjectData(mode, resourceID);
}

function LoadObjectData_Callback(jsonObject) {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var gameObject = JSON.parse(jsonObject);

    if (gameObject.GraphicResourceID == null || gameObject.GraphicResourceID == undefined) {
        gameObject.GraphicResourceID = 0;
    }

    if (mode == 'Area') {
        ToolsetViewModel.ActiveArea(gameObject);

        $('.clsAreaObjectField').removeAttr('disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Creature') {
        ToolsetViewModel.ActiveCreature(gameObject);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').removeAttr('disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Item') {
        ToolsetViewModel.ActiveItem(gameObject);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').removeAttr('disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Placeable') {
        ToolsetViewModel.ActivePlaceable(gameObject);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').removeAttr('disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Conversation') {
        ToolsetViewModel.ActiveConversation(gameObject);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').removeAttr('disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Script') {
        ToolsetViewModel.ActiveScript(gameObject);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').removeAttr('disabled');
        $('.clsTilesetObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Tileset') {
        ToolsetViewModel.ActiveTileset(gameObject);
        ToolsetViewModel.SelectedTilesetSpriteSheetID(gameObject.GraphicResourceID);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
        $('.clsTilesetObjectField').removeAttr('disabled');
    }
}

function ObjectTabApplyChanges() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var jsonModel = ko.toJSON(ToolsetViewModel);
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