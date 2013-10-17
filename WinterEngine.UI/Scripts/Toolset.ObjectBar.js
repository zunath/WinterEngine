
// Displays tree view, properties window, and other
// components based on object type.
function ChangeObjectMode(objectMode) {
    $('.clsObjectTabDiv').addClass('clsHidden');
    $('#divObjectTabContainerButtons').removeClass('clsHidden');
    $('.clsTreeViewDiv').addClass('clsHidden');
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    
    if (objectMode == "Area") {
        $('#liAreas').addClass('clsActiveObjectType');
        $('#divAreasTab').removeClass('clsHidden');
        $('#divAreaTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Area', '#divAreaTreeView', '#divAreasTab');
    }
    else if (objectMode == "Creature") {
        $('#liCreatures').addClass('clsActiveObjectType');
        $('#divCreaturesTab').removeClass('clsHidden');
        $('#divCreatureTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Creature', '#divCreatureTreeView', '#divCreaturesTab');
    }
    else if (objectMode == "Item") {
        $('#liItems').addClass('clsActiveObjectType');
        $('#divItemsTab').removeClass('clsHidden');
        $('#divItemTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Item', '#divItemTreeView', '#divItemsTab');
    }
    else if (objectMode == "Placeable") {
        $('#liPlaceables').addClass('clsActiveObjectType');
        $('#divPlaceablesTab').removeClass('clsHidden');
        $('#divPlaceableTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Placeable', '#divPlaceableTreeView', '#divPlaceablesTab');
    }
    else if (objectMode == "Conversation") {
        $('#liConversations').addClass('clsActiveObjectType');
        $('#divConversationsTab').removeClass('clsHidden');
        $('#divConversationTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Conversation', '#divConversationTreeView', '#divConversationsTab');
    }
    else if (objectMode == "Script") {
        $('#liScripts').addClass('clsActiveObjectType');
        $('#divScriptsTab').removeClass('clsHidden');
        $('#divScriptTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Script', '#divScriptTreeView', '#divScriptsTab');
    }
    else if (objectMode == "Tileset") {
        $('#liTilesets').addClass('clsActiveObjectType');
        $('#divTilesetsTab').removeClass('clsHidden');
        $('#divTilesetTreeView').removeClass('clsHidden');
        Entity.ChangeObjectMode('Tileset', '#divTilesetTreeView', '#divTilesetsTab');
    }
    // Otherwise, hide all.
    else {
        Entity.ChangeObjectMode('', '', '');
        $('#divObjectTabContainerButtons').addClass('clsHidden');
    }
}

function ChangeObjectMode_Callback() {
    ToolsetViewModel.Refresh();
}