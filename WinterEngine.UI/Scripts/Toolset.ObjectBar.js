﻿
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
        ToolsetViewModel.CurrentObjectMode('Area');
        ToolsetViewModel.CurrentObjectTreeSelector('#divAreaTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divAreasTab');
    }
    else if (objectMode == "Creature") {
        $('#liCreatures').addClass('clsActiveObjectType');
        $('#divCreaturesTab').removeClass('clsHidden');
        $('#divCreatureTreeView').removeClass('clsHidden');

        ToolsetViewModel.CurrentObjectMode('Creature');
        ToolsetViewModel.CurrentObjectTreeSelector('#divCreatureTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divCreaturesTab');
    }
    else if (objectMode == "Item") {
        $('#liItems').addClass('clsActiveObjectType');
        $('#divItemsTab').removeClass('clsHidden');
        $('#divItemTreeView').removeClass('clsHidden');
        ToolsetViewModel.CurrentObjectMode('Item');
        ToolsetViewModel.CurrentObjectTreeSelector('#divItemTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divItemsTab');
    }
    else if (objectMode == "Placeable") {
        $('#liPlaceables').addClass('clsActiveObjectType');
        $('#divPlaceablesTab').removeClass('clsHidden');
        $('#divPlaceableTreeView').removeClass('clsHidden');
        ToolsetViewModel.CurrentObjectMode('Placeable');
        ToolsetViewModel.CurrentObjectTreeSelector('#divPlaceableTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divPlaceablesTab');
    }
    else if (objectMode == "Conversation") {
        $('#liConversations').addClass('clsActiveObjectType');
        $('#divConversationsTab').removeClass('clsHidden');
        $('#divConversationTreeView').removeClass('clsHidden');
        ToolsetViewModel.CurrentObjectMode('Conversation');
        ToolsetViewModel.CurrentObjectTreeSelector('#divConversationTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divConversationsTab');
    }
    else if (objectMode == "Script") {
        $('#liScripts').addClass('clsActiveObjectType');
        $('#divScriptsTab').removeClass('clsHidden');
        $('#divScriptTreeView').removeClass('clsHidden');
        ToolsetViewModel.CurrentObjectMode('Script');
        ToolsetViewModel.CurrentObjectTreeSelector('#divScriptTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divScriptsTab');
    }
    else if (objectMode == "Tileset") {
        $('#liTilesets').addClass('clsActiveObjectType');
        $('#divTilesetsTab').removeClass('clsHidden');
        $('#divTilesetTreeView').removeClass('clsHidden');
        ToolsetViewModel.CurrentObjectMode('Tileset');
        ToolsetViewModel.CurrentObjectTreeSelector('#divTilesetTreeView');
        ToolsetViewModel.CurrentObjectTabSelector('#divTilesetsTab');
    }
    // Otherwise, hide all.
    else {
        ToolsetViewModel.CurrentObjectTreeSelector('');
        ToolsetViewModel.CurrentObjectTabSelector('');
        $('#divObjectTabContainerButtons').addClass('clsHidden');
    }

}